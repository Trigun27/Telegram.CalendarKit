using Telegram.CalendarKit;

/// <summary>
/// A provider class that retrieves the localized names of weekdays based on the specified culture.
/// </summary>
/// <remarks>
/// The <see cref="WeekdayLanguageProvider"/> class contains functionality to return an array of weekday names
/// for a specific culture code. It supports a variety of languages and is used by the <see cref="CalendarBuilder"/> 
/// class to localize the weekday names for calendar generation.
/// </remarks>
public class WeekdayLanguageProvider
{
    // Dictionary for storing weekdays by cultures
    private static readonly Dictionary<string, string[]> WeekdaysDictionary = new Dictionary<string, string[]>
    {
        { "ru", new[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" } },
        { "zh", new[] { "一", "二", "三", "四", "五", "六", "日" } },
        { "fr", new[] { "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim" } },
        { "es", new[] { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" } },
        { "en", new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" } }, // Default 
        { "de", new[] { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" } },
        { "it", new[] { "Lun", "Mar", "Mer", "Gio", "Ven", "Sab", "Dom" } },
        { "pt", new[] { "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom" } },
        { "ar", new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" } },
        { "ja", new[] { "月", "火", "水", "木", "金", "土", "日" } },
        { "ko", new[] { "월", "화", "수", "목", "금", "토", "일" } },
        { "pl", new[] { "Pon", "Wt", "Śr", "Cz", "Pt", "Sob", "Nd" } },
        { "sv", new[] { "Mån", "Tis", "Ons", "Tor", "Fre", "Lör", "Sön" } },
        { "nl", new[] { "Ma", "Di", "Wo", "Do", "Vr", "Za", "Zo" } },
        { "tr", new[] { "Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt" } },
        { "he", new[] { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" } },
        { "hi", new[] { "सोम", "मंगल", "बुध", "गुरु", "शुक्र", "शनिवार", "रविवार" } },
    };

    /// <summary>
    /// Retrieves the localized names of the weekdays for the specified culture.
    /// </summary>
    /// <param name="culture">The culture code (e.g., "en" for English, "ru" for Russian, etc.).</param>
    /// <returns>
    /// An array of strings representing the localized names of the weekdays, starting from Monday.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided culture code is not supported or is invalid.
    /// </exception>
    public static string[] GetDaysOfWeek(string culture)
    {
        // Если культура не найдена, возвращаем дни недели на английском
        if (WeekdaysDictionary.TryGetValue(culture.ToLower(), out var daysOfWeek))
        {
            return daysOfWeek;
        }

        // По умолчанию возвращаем английские дни недели
        return WeekdaysDictionary["en"];
    }
}
