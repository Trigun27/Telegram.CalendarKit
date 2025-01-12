﻿using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.CalendarKit.Models;
using Telegram.CalendarKit.Models.Enums;

namespace Telegram.CalendarKit;

/// <summary>
/// A class responsible for generating calendar views, including full month and weekly calendars,
/// with support for multiple languages (cultures) for weekdays.
/// </summary>
/// <remarks>
/// The <see cref="CalendarBuilder"/> class allows for the creation of interactive calendar interfaces,
/// with weekday names localized according to the selected culture. It supports both monthly and weekly calendar views.
/// </remarks>
public class CalendarBuilder
{
    private const int DayOfWeek = 7;
    private const string DefaultCulture = "en";

    private string[] _daysOfWeek = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

    /// <summary>
    /// Constructor for creating a <see cref="CalendarBuilder"/> object.
    /// </summary>
    public CalendarBuilder() { }

    /// <summary>
    /// Constructor for creating a <see cref="CalendarBuilder"/> object with a specified culture for the weekdays.
    /// </summary>
    /// <param name="cultureWeek">The culture code for displaying the weekdays (e.g., "en" for English, "ru" for Russian, etc.).</param>
    /// <remarks>
    /// This constructor initializes the object with localized weekday names using the <see cref="WeekdayLanguageProvider.GetDaysOfWeek"/>
    /// method to fetch the list of weekdays corresponding to the selected culture.
    /// </remarks>
    public CalendarBuilder(string cultureWeek)
    {
        // localized
        _daysOfWeek = WeekdayLanguageProvider.GetDaysOfWeek(cultureWeek);
    }

    /// <summary>
    /// Generates an inline keyboard markup for a calendar based on the specified year, month, and view type.
    /// </summary>
    /// <param name="year">The year for which the calendar is generated.</param>
    /// <param name="month">The month for which the calendar is generated.</param>
    /// <param name="viewType">
    /// The type of calendar view to generate. Supported types are:
    /// <see cref="CalendarViewType.Default"/> for a full monthly calendar and 
    /// <see cref="CalendarViewType.Weekly"/> for a weekly calendar view.
    /// </param>
    /// <param name="cultureWeek"></param>
    /// <returns>
    /// An <see cref="InlineKeyboardMarkup"/> object representing the calendar buttons for the specified view type.
    /// </returns>
    /// <remarks>
    /// This method uses a switch expression to determine the calendar generation logic based on the <paramref name="viewType"/>:
    /// - <see cref="CalendarViewType.Default"/> generates a full month calendar with day navigation.
    /// - <see cref="CalendarViewType.Weekly"/> generates a weekly calendar layout.
    /// Throws an <see cref="ArgumentException"/> if an unsupported view type is provided.
    /// </remarks>
    /// <example>
    /// <code>
    /// var calendarMarkup = GenerateCalendarButtons(2024, 12, CalendarViewType.Default);
    /// </code>
    /// This will create a default calendar for December 2024.
    /// </example>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided <paramref name="viewType"/> is not supported.
    /// </exception>
    public InlineKeyboardMarkup GenerateCalendarButtons(int year, int month, CalendarViewType viewType, string cultureWeek = DefaultCulture)
    {
        _daysOfWeek = WeekdayLanguageProvider.GetDaysOfWeek(cultureWeek);

        return viewType switch
        {
            CalendarViewType.Default => GenerateDefaultCalendarMarkup(year, month),
            CalendarViewType.Weekly => GenerateWeeklyCalendarMarkup(year, month),
            _ => throw new ArgumentException("Unsupported view type", nameof(viewType))
        };
    }

    /// <summary>
    /// Sends a calendar message to a Telegram chat.
    /// </summary>
    /// <param name="botClient">The Telegram Bot API client used to send the message.</param>
    /// <param name="chatId">The chat ID where the calendar should be sent.</param>
    /// <param name="message">The text message accompanying the calendar.</param>
    /// <param name="year">The year of the calendar to send.</param>
    /// <param name="month">The month of the calendar to send.</param>
    /// <param name="viewType"></param>
    /// <param name="cultureWeek"></param>
    /// <returns>A task that represents the asynchronous operation of sending the message.</returns>
    public async Task SendCalendarMessageAsync(ITelegramBotClient botClient, long chatId, string message, int year, int month, CalendarViewType viewType, string cultureWeek = DefaultCulture)
    {
        _daysOfWeek = WeekdayLanguageProvider.GetDaysOfWeek(cultureWeek);

        var calendarMarkup = GenerateCalendarButtons(year, month, viewType, cultureWeek);
        try
        {
            await botClient.SendMessage(chatId, message, replyMarkup: calendarMarkup);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send calendar: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles navigation through a calendar based on the provided callback data.
    /// Adjusts the year and month based on the action (e.g., "prev" or "next") 
    /// and regenerates the calendar markup for the specified view type.
    /// </summary>
    /// <param name="callbackData">The callback data containing navigation action and current date.</param>
    /// <param name="viewType">The type of calendar view (e.g., Default or Weekly).</param>
    /// <param name="cultureWeek"></param>
    /// <returns>
    /// An <see cref="InlineKeyboardMarkup"/> representing the updated calendar with new navigation buttons.
    /// </returns>
    /// <remarks>
    /// This method supports two navigation actions:
    /// - "prev" to navigate to the previous month.
    /// - "next" to navigate to the next month.
    /// The year is adjusted automatically when transitioning between December and January.
    /// </remarks>
    /// <example>
    /// Example callback data: "calendar:prev:2024-12".
    /// Result: A calendar for November 2024 if the action is "prev".
    /// </example>
    /// <exception cref="ArgumentException">
    /// Thrown if the callback data cannot be parsed.
    /// </exception>
    public async Task<InlineKeyboardMarkup> HandleNavigation(string callbackData, CalendarViewType viewType, string cultureWeek = DefaultCulture)
    {
        CalendarCallbackData callback = await Task.Run(() => ParseCalendarCallback(callbackData));
        if (callback.Action == "prev")
        {
            callback.Month = callback.Month == 1 ? 12 : callback.Month - 1;
            callback.Year -= callback.Month == 12 ? 1 : 0;
        }
        else if (callback.Action == "next")
        {
            callback.Month = callback.Month == 12 ? 1 : callback.Month + 1;
            callback.Year += callback.Month == 1 ? 1 : 0;
        }
        return GenerateCalendarButtons(callback.Year, callback.Month, viewType, cultureWeek);
    }


    /// <summary>
    /// Parses a callback data string into a <see cref="CalendarCallbackData"/> object.
    /// </summary>
    /// <param name="callbackData">The callback data string to parse.</param>
    /// <returns>A <see cref="CalendarCallbackData"/> object representing the parsed data.</returns>
    private static CalendarCallbackData ParseCalendarCallback(string callbackData)
    {
        var parts = callbackData.Split(':');

        if (parts[1] == "day")
        {
            return new CalendarCallbackData
            {
                Action = parts[1],
                Year = int.Parse(parts[2].Split('-')[0]),
                Month = int.Parse(parts[2].Split('-')[1]),
                Day = parts.Length >= 3 ? int.Parse(parts[2].Split('-')[2]) : null
            };
        }

        return new CalendarCallbackData
        {
            Action = parts[1],
            Year = int.Parse(parts[2].Split('-')[0]),
            Month = int.Parse(parts[2].Split('-')[1]),
            Day = parts.Length > 3 ? int.Parse(parts[2].Split('-')[2]) : null
        };
    }

    /// <summary>
    /// Generates an inline keyboard for displaying a monthly calendar for the specified year and month.
    /// </summary>
    /// <param name="year">The year of the calendar to display.</param>
    /// <param name="month">The month of the calendar to display.</param>
    /// <returns>An inline keyboard markup with buttons representing the days of the month.</returns>
    private InlineKeyboardMarkup GenerateDefaultCalendarMarkup(int year, int month)
    {
        var days = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
            .Select(day => InlineKeyboardButton.WithCallbackData(day.ToString(), $"calendar:day:{year}-{month}-{day}"));

        var rows = days
            .Chunk(DayOfWeek) // Weeks of 7 days
            .Select(week => week.ToArray());

        // Adding navigation buttons
        var navigationButtons = new[]
        {
            InlineKeyboardButton.WithCallbackData("<", $"calendar:prev:{year}-{month}"),
            InlineKeyboardButton.WithCallbackData(">", $"calendar:next:{year}-{month}")
        };

        var daysRow = _daysOfWeek.Select(day => InlineKeyboardButton.WithCallbackData(day, "ignore")).ToList();
        
        return new InlineKeyboardMarkup(rows.Append(navigationButtons));
    }

    /// <summary>
    /// Generates an inline keyboard for displaying a weekly calendar view for the specified year and month.
    /// </summary>
    /// <param name="year">The year of the calendar to display.</param>
    /// <param name="month">The month of the calendar to display.</param>
    /// <returns>An inline keyboard markup with buttons representing the weekly view of the calendar.</returns>
    private InlineKeyboardMarkup GenerateWeeklyCalendarMarkup(int year, int month)
    {
        var buttons = new List<List<InlineKeyboardButton>>
        {
            _daysOfWeek.Select(day => InlineKeyboardButton.WithCallbackData(day, "ignore")).ToList()
        };

        var firstDayOfMonth = new DateTime(year, month, 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);

        // Correction of the offset for Monday as the first day of the week.
        // In the American system, the beginning of the week is Sunday.
        int startDayIndex = ((int)firstDayOfMonth.DayOfWeek - 1 + DayOfWeek) % DayOfWeek;

        var currentWeek = new List<InlineKeyboardButton>();
        for (int i = 0; i < startDayIndex; i++)
        {
            currentWeek.Add(InlineKeyboardButton.WithCallbackData(" ", "ignore"));
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            currentWeek.Add(InlineKeyboardButton.WithCallbackData(day.ToString(), $"date:{year}-{month:00}-{day:00}"));

            if (currentWeek.Count == DayOfWeek)
            {
                buttons.Add(currentWeek);
                currentWeek = [];
            }
        }

        if (currentWeek.Any())
        {
            while (currentWeek.Count < DayOfWeek)
            {
                currentWeek.Add(InlineKeyboardButton.WithCallbackData(" ", "ignore"));
            }
            buttons.Add(currentWeek);
        }

        List<InlineKeyboardButton> navigationRow = new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("<", $"calendar:prev:{year}-{month}"),
            InlineKeyboardButton.WithCallbackData($"{year}-{month}", $"ignore"),
            InlineKeyboardButton.WithCallbackData(">", $"calendar:next:{year}-{month}")
        };

        buttons.Add(navigationRow);

        return new InlineKeyboardMarkup(buttons);
    }

}