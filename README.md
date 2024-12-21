# Telegram.CalendarKit

## Library Description
This library provides functionality for generating and sending calendars in Telegram bots. It allows creating inline buttons to display calendars for various months with navigation support between months. The library supports two view types: a full monthly calendar and a weekly calendar view. Additionally, it enables sending the generated calendars to a chat and handling user actions, such as moving to the next or previous month.

### Dependencies
- Telegram.Bot (version 22.2.0): A library for interacting with the Telegram Bot API. It is used for sending messages and handling user commands within the Telegram environment. https://github.com/TelegramBots/Telegram.Bot
- .NET 8.0: The target framework for the library, which provides the necessary platform and runtime support for the application.

## Description
CalendarBuilder is a class designed for generating calendars in Telegram bots. It provides functionality for creating inline calendar buttons with month navigation and displaying the calendar in different views (full month or weekly). The class also allows sending the generated calendars to a chat and handling navigation between months.

![image](https://github.com/user-attachments/assets/7c4c04a8-2dcf-42e8-ae25-471c5f89de5c)
![image](https://github.com/user-attachments/assets/a58e3187-e31a-4c3b-975d-1b87e3278515)

### Methods

1. GenerateCalendarButtons(int year, int month, CalendarViewType viewType)
Generates buttons for displaying the calendar for the specified month and year. It supports two view types: Default (full month) and Weekly (week view).

```
var calendarMarkup = calendarBuilder.GenerateCalendarButtons(2024, 12, CalendarViewType.Default);
```
2. SendCalendarMessageAsync(TelegramBotClient botClient, long chatId, string text, int year, int month, CalendarViewType viewType)
Sends a message with the calendar to the specified chat, generating buttons using GenerateCalendarButtons.

```
await calendarBuilder.SendCalendarMessageAsync(botClient, chatId, "Here is your calendar:", 2024, 12, CalendarViewType.Default);
```
3. HandleNavigation(string callbackData, CalendarViewType viewType)
Handles calendar navigation, updating the year and month data based on the user’s action (e.g., "prev" or "next").

```
var updatedCalendar = await calendarBuilder.HandleNavigation("calendar:prev:2024-12", CalendarViewType.Default);
```
4. ParseCalendarCallback(string callbackData)
Parses the callback data string and extracts information about the action and the current date.

```
var callbackData = "calendar:prev:2024-12";
var parsedData = calendarBuilder.ParseCalendarCallback(callbackData);
```

5. GenerateDefaultCalendarMarkup(int year, int month)
Generates an inline keyboard for displaying a full month calendar.

```
var calendarMarkup = calendarBuilder.GenerateDefaultCalendarMarkup(2024, 12);
```

6. GenerateWeeklyCalendarMarkup(int year, int month)
Generates an inline keyboard for displaying a weekly calendar view.

```
var calendarMarkup = calendarBuilder.GenerateWeeklyCalendarMarkup(2024, 12);
```

# Supported Languages for Weekdays

This class provides weekday names in different languages and cultures. The supported languages are listed below along with their abbreviations for the days of the week.

## Supported Languages

| Language       | Abbreviation of Days of the Week |
|----------------|----------------------------------|
| **Russian**    | "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" |
| **Chinese**    | "一", "二", "三", "四", "五", "六", "日" |
| **French**     | "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim" |
| **Spanish**    | "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" |
| **English**    | "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" |
| **German**     | "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" |
| **Italian**    | "Lun", "Mar", "Mer", "Gio", "Ven", "Sab", "Dom" |
| **Portuguese** | "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom" |
| **Arabic**     | "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" |
| **Japanese**   | "月", "火", "水", "木", "金", "土", "日" |
| **Korean**     | "월", "화", "수", "목", "금", "토", "일" |
| **Polish**     | "Pon", "Wt", "Śr", "Cz", "Pt", "Sob", "Nd" |
| **Swedish**    | "Mån", "Tis", "Ons", "Tor", "Fre", "Lör", "Sön" |
| **Dutch**      | "Ma", "Di", "Wo", "Do", "Vr", "Za", "Zo" |
| **Turkish**    | "Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt" |
| **Hebrew**     | "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" |
| **Hindi**      | "सोम", "मंगल", "बुध", "गुरु", "शुक्र", "शनिवार", "रविवार" |

## Usage

To use this provider for retrieving the correct weekday names based on the selected culture, you can call the `GetDaysOfWeek` method with the culture code of the desired language.

```csharp
string[] weekdays = WeekdayLanguageProvider.GetDaysOfWeek("fr");
```
This will return the French weekday names: `{"Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim"}`.

<hr>

## Описание библиотеки
Эта библиотека предоставляет функциональность для генерации и отправки календарей в Telegram-ботах. Она позволяет создавать inline кнопки для отображения календарей различных месяцев с поддержкой навигации между месяцами. Библиотека поддерживает два типа представлений: полное отображение месяца и отображение календаря по неделям. Также есть возможность отправить сгенерированные календари в чат и обрабатывать действия пользователя, такие как переходы к следующему или предыдущему месяцу.

### Зависимости
Telegram.Bot (версия 22.2.0): библиотека для взаимодействия с Telegram Bot API. Она используется для отправки сообщений и обработки команд пользователя через Telegram.

## Описание
CalendarBuilder — это класс для генерации календарей в Telegram-ботах. Он предоставляет функциональность для создания inline кнопок календаря с возможностью навигации по месяцам и отображения календаря в разных представлениях (полный месяц или неделя). Класс также позволяет отправлять сгенерированные календари в чат и обрабатывать навигацию между месяцами.

![image](https://github.com/user-attachments/assets/7c4c04a8-2dcf-42e8-ae25-471c5f89de5c)
![image](https://github.com/user-attachments/assets/a58e3187-e31a-4c3b-975d-1b87e3278515)

### Методы
1. GenerateCalendarButtons(int year, int month, CalendarViewType viewType)
Генерирует кнопки для отображения календаря для указанного месяца и года. Поддерживает два типа представлений: Default (полный месяц) и Weekly (неделя).

```
var calendarMarkup = calendarBuilder.GenerateCalendarButtons(2024, 12, CalendarViewType.Default);
```
2. SendCalendarMessageAsync(TelegramBotClient botClient, long chatId, string text, int year, int month, CalendarViewType viewType)
Отправляет сообщение с календарем в указанный чат, генерируя кнопки с помощью GenerateCalendarButtons.

```
await calendarBuilder.SendCalendarMessageAsync(botClient, chatId, "Вот ваш календарь:", 2024, 12, CalendarViewType.Default);
```

3. HandleNavigation(string callbackData, CalendarViewType viewType)
Обрабатывает навигацию по календарю, обновляя данные о годе и месяце в зависимости от действия пользователя (например, "prev" или "next").

```
var updatedCalendar = await calendarBuilder.HandleNavigation("calendar:prev:2024-12", CalendarViewType.Default);
```

4. ParseCalendarCallback(string callbackData)
Парсит строку callback данных и извлекает информацию о действии и текущей дате.

```
var callbackData = "calendar:prev:2024-12";
var parsedData = calendarBuilder.ParseCalendarCallback(callbackData);
```

5. GenerateDefaultCalendarMarkup(int year, int month)
Генерирует inline клавиатуру для отображения полного календаря месяца.

```
var calendarMarkup = calendarBuilder.GenerateDefaultCalendarMarkup(2024, 12);
```

6. GenerateWeeklyCalendarMarkup(int year, int month)
Генерирует inline клавиатуру для отображения календаря в виде недели.
```
var calendarMarkup = calendarBuilder.GenerateWeeklyCalendarMarkup(2024, 12);
```

# Поддерживаемые Языки для Дней Недели

Этот класс предоставляет названия дней недели на различных языках и культурах. Поддерживаемые языки приведены ниже с их аббревиатурами для дней недели.

## Поддерживаемые Языки

| Язык          | Аббревиатуры Дней Недели            |
|---------------|-------------------------------------|
| **Русский**   | "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" |
| **Китайский** | "一", "二", "三", "四", "五", "六", "日" |
| **Французский**| "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim" |
| **Испанский** | "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" |
| **Английский** | "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" |
| **Немецкий**  | "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" |
| **Итальянский**| "Lun", "Mar", "Mer", "Gio", "Ven", "Sab", "Dom" |
| **Португальский** | "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom" |
| **Арабский**  | "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" |
| **Японский**  | "月", "火", "水", "木", "金", "土", "日" |
| **Корейский** | "월", "화", "수", "목", "금", "토", "일" |
| **Польский**  | "Pon", "Wt", "Śr", "Cz", "Pt", "Sob", "Nd" |
| **Шведский**  | "Mån", "Tis", "Ons", "Tor", "Fre", "Lör", "Sön" |
| **Голландский** | "Ma", "Di", "Wo", "Do", "Vr", "Za", "Zo" |
| **Турецкий**  | "Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt" |
| **Иврит**     | "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" |
| **Хинди**     | "सोम", "मंगल", "बुध", "गुरु", "शुक्र", "शनिवार", "रविवार" |

## Использование

Для использования этого провайдера и получения правильных названий дней недели в зависимости от выбранной культуры, вы можете вызвать метод `GetDaysOfWeek` с кодом культуры желаемого языка.

```csharp
string[] weekdays = WeekdayLanguageProvider.GetDaysOfWeek("fr");
```

Этот код вернет французские названия дней недели: `{"Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim"}`.