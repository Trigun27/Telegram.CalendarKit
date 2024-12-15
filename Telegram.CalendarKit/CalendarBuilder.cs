using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.CalendarKit.Models;

namespace Telegram.CalendarKit
{
    public class CalendarBuilder
    {
        public string Action { get; set; } // "prev", "next", "day"
        public int Year { get; set; }
        public int Month { get; set; }
        public int? Day { get; set; }


        public CalendarBuilder() 
        {
            
        }
        // Генерация календаря для указанного месяца и года
        public InlineKeyboardMarkup GenerateCalendarButtons(int year, int month)
        {
            var days = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => InlineKeyboardButton.WithCallbackData(day.ToString(), $"calendar:day:{year}-{month}-{day}"));

            var rows = days
                .Chunk(7) // Недели по 7 дней
                .Select(week => week.ToArray());

            // Добавляем кнопки навигации
            var navigationButtons = new[]
            {
                InlineKeyboardButton.WithCallbackData("<", $"calendar:prev:{year}-{month}"),
                InlineKeyboardButton.WithCallbackData(">", $"calendar:next:{year}-{month}")
            };

            return new InlineKeyboardMarkup(rows.Append(navigationButtons));
        }

        // Отправка календаря
        public async Task SendCalendarAsync(ITelegramBotClient botClient, long chatId, string message, int year, int month)
        {
            var calendarMarkup = GenerateCalendarButtons(year, month);
            await botClient.SendMessage(chatId, message, replyMarkup: calendarMarkup);
        }

        // Обработка переключения месяцев
        public async Task<InlineKeyboardMarkup> HandleNavigation(string callbackData)
        {
            // Логика обработки callback, обновление месяца/года
            // Например: "calendar:prev:2024-12" -> сдвигаем на месяц назад
            // Возвращаем новый набор кнопок
            CalendarCallbackData callback = ParseCallback(callbackData);
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
                    callback.Year = callback.Year - 1;
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
                    callback.Year = callback.Year + 1;
                    callback.Month = callback.Month + 1;
                }
            }
            return GenerateCalendarButtons(callback.Year,callback.Month);
        }

        private CalendarCallbackData ParseCallback(string callbackData)
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
    }
}
