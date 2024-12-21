namespace Telegram.CalendarKit.Models
{
    /// <summary>
    /// Represents the callback data for calendar navigation actions.
    /// This data is used to track the user's interaction with the calendar UI,
    /// such as navigating between months or selecting a specific day.
    /// </summary>
    public class CalendarCallbackData
    {
        /// <summary>
        /// Gets or sets the action associated with the calendar callback.
        /// This could be "prev" for moving to the previous month, 
        /// "next" for moving to the next month, or "day" for selecting a specific day.
        /// </summary>
        public required string Action { get; set; } // "prev", "next", "day"

        /// <summary>
        /// Gets or sets the year associated with the calendar action.
        /// This is used to navigate or display a specific year in the calendar.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the month associated with the calendar action.
        /// This is used to navigate or display a specific month in the calendar.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the day associated with the calendar action (if applicable).
        /// This is used for selecting a specific day when the action is "day".
        /// </summary>
        public int? Day { get; set; }
    }
}
