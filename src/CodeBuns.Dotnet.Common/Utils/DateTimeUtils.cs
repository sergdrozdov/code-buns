using System.Globalization;

namespace CodeBuns.Dotnet.Common
{
    public static class DateTimeUtils
    {
        public static DateTime GetFirstDayOfCurrentWeek()
        {
            var dateFrom = DateTime.Now.Date;
            var diff = (7 + (dateFrom.DayOfWeek - DayOfWeek.Monday)) % 7;
            var monday = dateFrom.AddDays(-1 * diff).Date;

            return monday;
        }

        public static DateTime GetLastDayOfCurrentWeek()
        {
            var monday = DateTime.Now.Date.AddDays(-1 * (int)DateTime.Now.DayOfWeek).AddDays(1);
            var dateTo = monday.AddDays(6);

            return new DateTime(dateTo.Year, dateTo.Month, dateTo.Day);
        }

        public static DateTime GetFirstDayOfPreviousWeek()
        {
            var dateFrom = DateTime.Now.Date;
            var diff = (7 + (dateFrom.DayOfWeek - DayOfWeek.Monday)) % 7;
            var monday = dateFrom.AddDays(-1 * diff).Date;

            dateFrom = monday.AddDays(-7);

            return new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day);
        }

        public static DateTime GetLastDayOfPreviousWeek()
        {
            var dateFrom = DateTime.Now.Date;
            var diff = (7 + (dateFrom.DayOfWeek - DayOfWeek.Monday)) % 7;
            var monday = dateFrom.AddDays(-1 * diff).Date;
            dateFrom = monday.AddDays(-7);
            var dateTo = dateFrom.AddDays(6);

            return new DateTime(dateTo.Year, dateTo.Month, dateTo.Day);
        }

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

        public static DateTime GetMonthLastDay(int year, int month)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }
    }
}
