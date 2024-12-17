using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.CalendarKit.Models;
using Telegram.CalendarKit.Models.Enums;

namespace Telegram.CalendarKit
{
    public class CalendarBuilder
    {
        private const int DayOfWeek = 7;

        string[] daysOfWeek = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        public CalendarBuilder() { }

        public InlineKeyboardMarkup GenerateCalendarButtons(int year, int month, CalendarViewType viewType)
        {
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
        /// <returns>A task that represents the asynchronous operation of sending the message.</returns>
        public async Task SendCalendarMessageAsync(ITelegramBotClient botClient, long chatId, string message, int year, int month, CalendarViewType viewType)
        {
            var calendarMarkup = GenerateCalendarButtons(year, month, viewType);
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
        /// Handles the navigation callback for moving between months in the calendar.
        /// Generates an updated inline keyboard for the new month.
        /// </summary>
        /// <param name="callbackData">The callback data string containing navigation information.</param>
        /// <returns>An updated inline keyboard markup for the new month's calendar.</returns>
        public async Task<InlineKeyboardMarkup> HandleMonthNavigation(string callbackData)
        {
            // Callback processing logic, month/year update
            // For example: "calendar:prev:2024-12" -> move back a month
            // Return a new set of buttons
            CalendarCallbackData callback = ParseCalendarCallback(callbackData);
            if (callback.Action == "prev")
            {
                if (callback.Month == 1)
                {
                    callback.Year = callback.Year - 1;
                    callback.Month = 12;
                }
                else
                {
                    callback.Month = callback.Month - 1;
                }
            }
            if (callback.Action == "next")
            {
                if (callback.Month == 12)
                {
                    callback.Year = callback.Year + 1;
                    callback.Month = 1;
                }
                else
                {
                    callback.Month = callback.Month + 1;
                }
            }
            return GenerateDefaultCalendarMarkup(callback.Year,callback.Month);
        }

        /// <summary>
        /// Handles the navigation callback for moving between months in the weekly calendar view.
        /// Generates an updated inline keyboard for the new month in weekly view.
        /// </summary>
        /// <param name="callbackData">The callback data string containing navigation information.</param>
        /// <returns>An updated inline keyboard markup for the new month's weekly calendar view.</returns>
        public async Task<InlineKeyboardMarkup> HandleMonthNavigationWeekly(string callbackData)
        {
            // Callback processing logic, month/ year update
            // For example: "calendar:prev:2024-12" -> move back a month
            // Return a new set of buttons
            CalendarCallbackData callback = ParseCalendarCallback(callbackData);
            if (callback.Action == "prev")
            {
                // Переключение на предыдущий месяц
                if (callback.Month == 1)
                {
                    callback.Year = callback.Year - 1;
                    callback.Month = 12;
                }
                else
                {
                    callback.Month = callback.Month - 1;
                }
            }
            if (callback.Action == "next")
            {
                // Переключение на предыдущий месяц
                if (callback.Month == 12)
                {
                    callback.Year = callback.Year + 1;
                    callback.Month = 1;
                }
                else
                {
                    callback.Month = callback.Month + 1;
                }
            }
            return GenerateWeeklyCalendarMarkup(callback.Year, callback.Month);
        }

        /// <summary>
        /// Parses a callback data string into a <see cref="CalendarCallbackData"/> object.
        /// </summary>
        /// <param name="callbackData">The callback data string to parse.</param>
        /// <returns>A <see cref="CalendarCallbackData"/> object representing the parsed data.</returns>
        private CalendarCallbackData ParseCalendarCallback(string callbackData)
        {
            string[] parts = callbackData.Split(':');

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
            else
            {
                return new CalendarCallbackData
                {
                    Action = parts[1],
                    Year = int.Parse(parts[2].Split('-')[0]),
                    Month = int.Parse(parts[2].Split('-')[1]),
                    Day = parts.Length > 3 ? int.Parse(parts[2].Split('-')[2]) : null
                };
            }
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
            InlineKeyboardButton[] navigationButtons = new[]
            {
                InlineKeyboardButton.WithCallbackData("<", $"calendar:prev:{year}-{month}"),
                InlineKeyboardButton.WithCallbackData(">", $"calendar:next:{year}-{month}")
            };

            var daysRow = daysOfWeek.Select(day => InlineKeyboardButton.WithCallbackData(day, "ignore")).ToList();


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
                daysOfWeek.Select(day => InlineKeyboardButton.WithCallbackData(day, "ignore")).ToList()
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
                    currentWeek = new List<InlineKeyboardButton>();
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
}
