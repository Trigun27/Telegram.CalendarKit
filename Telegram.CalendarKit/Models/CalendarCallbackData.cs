namespace Telegram.CalendarKit.Models
{
    public class CalendarCallbackData
    {
        public required string Action { get; set; } // "prev", "next", "day"
        public int Year { get; set; }
        public int Month { get; set; }
        public int? Day { get; set; }
    }
}
