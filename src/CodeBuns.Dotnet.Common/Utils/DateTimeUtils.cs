using System.Globalization;

namespace CodeBuns.Dotnet.Common
{
    public static class DateTimeUtils
    {
        public static DateTime GetWeekFirstDay(DateTime date)
        {
            // Get the first day of the week (Monday)
            var firstDayOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

            return firstDayOfWeek.Date;
        }

        public static DateTime GetWeekFirstDay(int year, int month, int day)
        {
            if (year < 1 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("Invalid date provided.");
            }

            var date = new DateTime(year, month, day);

            // Get the first day of the week (Monday)
            var firstDayOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

            return firstDayOfWeek.Date;
        }

        public static DateTime GetCurrentWeekFirstDay()
        {
            var date = DateTime.Now;

            // Get the first day of the week (Monday)
            var firstDayOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

            return firstDayOfWeek.Date;
        }

        public static int GetWeekNumber(DateTime date)
        {
            var calendar = CultureInfo.CurrentCulture.Calendar;

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            var firstDayOfWeek = dateTimeFormat.FirstDayOfWeek;
            var calendarWeekRule = dateTimeFormat.CalendarWeekRule;

            return calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);
        }
    }
}
