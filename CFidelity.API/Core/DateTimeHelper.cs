using System;

namespace CFidelity.API.Core
{
    public static class DateTimeHelper
    {
        private static DateTime? _dateTime = null;
        public static DateTime GetBrazilianDateTimeNow()
        {
            if (_dateTime.HasValue)
            {
                return _dateTime.Value;
            }
            else
            {
                DateTime brazilDateTimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, GetBrazilTimeZone());
                return brazilDateTimeNow;
            }
        }

        public static TimeZoneInfo GetBrazilTimeZone()
        {
            String timeZoneId = "E. South America Standard Time";
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return brazilTimeZone;
        }

        public static TimeZoneInfo GetLocalTimeZone()
        {
            return TimeZoneInfo.Local;
        }

#if DEBUG
        /// <summary>
        /// Método usado nos testes unitários para poder controlar o retorno da data pelo método
        /// GetBrazilianDateTimeNow(); O método dará o retorno desejado apenas uma única vez, depois
        /// voltará ao seu comportamento default.        
        /// </summary>
        /// <param name="date">Data que deseja retornar no GetBrazilianDateTimeNow()</param>

        public static void SetBrazilianDateTimeNow(DateTime date)
        {
            _dateTime = date;
        }
#endif

        /// <summary>
        /// Gets the 12:00:00 AM instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteStart(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 PM instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteEnd(this DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Gets the 12:00:00 AM instance of the first day of the month for a DateTime
        /// </summary>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Gets the 11:59:59 PM instance of the last day of the month for a DateTime
        /// </summary>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return AbsoluteEnd(new DateTime(dateTime.Year, dateTime.Month, daysInMonth));
        }

        /// <summary>
        /// Gets the 12:00:00 AM instance of the first day of the month prior to the month for a DateTime
        /// </summary>
        public static DateTime StartOfPreviousMonth(this DateTime dateTime)
        {
            var oneMonthAgoToday = dateTime.AddMonths(-1);
            return new DateTime(oneMonthAgoToday.Year, oneMonthAgoToday.Month, 1);
        }

        /// <summary>
        /// Gets the 11:59:59 PM instance of the last day of the month prior to the month for a DateTime
        /// </summary>
        public static DateTime EndOfPreviousMonth(this DateTime dateTime)
        {
            var oneMonthAgoToday = dateTime.AddMonths(-1);
            int daysInLastMonth = DateTime.DaysInMonth(oneMonthAgoToday.Year, oneMonthAgoToday.Month);
            return AbsoluteEnd(new DateTime(oneMonthAgoToday.Year, oneMonthAgoToday.Month, daysInLastMonth));
        }

        /// <summary>
        /// Gets the 12:00:00 instance of the first day of the standard Quarter for a DateTime
        /// </summary>
        public static DateTime StartOfStandardQuarter(this DateTime dateTime)
        {
            if (1 <= dateTime.Month && dateTime.Month <= 3)
                return new DateTime(dateTime.Year, 1, 1);
            else if (4 <= dateTime.Month && dateTime.Month <= 6)
                return new DateTime(dateTime.Year, 4, 1);
            else if (7 <= dateTime.Month && dateTime.Month <= 9)
                return new DateTime(dateTime.Year, 7, 1);
            else
                return new DateTime(dateTime.Year, 10, 1);
        }

        /// <summary>
        /// Gets the 11:59:59 PM instance of the last day of the standard Quarter for a DateTime
        /// </summary>
        public static DateTime EndOfStandardQuarter(this DateTime dateTime)
        {
            if (1 <= dateTime.Month && dateTime.Month <= 3)
                return AbsoluteEnd(new DateTime(dateTime.Year, 3, 31));
            else if (4 <= dateTime.Month && dateTime.Month <= 6)
                return AbsoluteEnd(new DateTime(dateTime.Year, 6, 30));
            else if (7 <= dateTime.Month && dateTime.Month <= 9)
                return AbsoluteEnd(new DateTime(dateTime.Year, 9, 30));
            else
                return AbsoluteEnd(new DateTime(dateTime.Year, 12, 31));
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime? ToUniversalTime(this DateTime? dateTime)
        {
            return dateTime.HasValue
                ? dateTime.Value.ToUniversalTime()
                : dateTime;
        }
    }
}
