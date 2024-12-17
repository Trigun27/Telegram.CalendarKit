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

