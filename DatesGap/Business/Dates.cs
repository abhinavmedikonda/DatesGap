using System;
using DatesGap.Models;

namespace DatesGap.Business
{
    public static class Dates
    {
        /// <summary>
        /// Calculates gap between 2 dates.
        /// </summary>
        /// <returns>Number of Days.</returns>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        public static int Gap(Date fromDate, Date toDate){
            if (!IsValidDateRange(fromDate, toDate))
                return 0;
            if (fromDate.Year == toDate.Year)
            {
                if (fromDate.Month == toDate.Month)
                {
                    return toDate.Day - fromDate.Day;
                }

                return DaysBeforeWholeMonths(fromDate) +
                    DaysInWholeMonths(fromDate.Year, Convert.ToInt16(fromDate.Month +1), Convert.ToInt16(toDate.Month-1)) +
                    DaysAfterWholeMonths(toDate);
            }

            return DaysBeforeWholeYears(fromDate) + 
                DaysInWholeYears(fromDate.Year, toDate.Year) + 
                DaysAfterWholeYears(toDate);
        }

        /// <summary>
        /// Checks if toDate comes after fromDate, returns true if so.
        /// </summary>
        /// <returns><c>true</c>, if valid date range was ised, <c>false</c> otherwise.</returns>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        public static bool IsValidDateRange(Date fromDate, Date toDate){
            if (toDate.Year > fromDate.Year)
                return true;
            else if (toDate.Year < fromDate.Year)
                return false;
            else if (toDate.Month > fromDate.Month)
                return true;
            else if (toDate.Month < fromDate.Month)
                return false;
            else if (toDate.Day > fromDate.Day)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if a year is leap year or not.
        /// </summary>
        /// <returns><c>true</c>, if leap year was ised, <c>false</c> otherwise.</returns>
        /// <param name="year">Year.</param>
        public static bool IsLeapYear(short year)
        {
            return (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) ? true : false;
        }

        /// <summary>
        /// Check if a date is in a Leap year month.
        /// </summary>
        /// <returns><c>true</c>, if leap month was ised, <c>false</c> otherwise.</returns>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        private static bool IsLeapMonth(short year, short month){
            return (month == DaysMap.LeapYearMonth && IsLeapYear(year)) ? true : false;
        }

        /// <summary>
        /// Days in current month after given date.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="fromDate">From date.</param>
        private static int DaysBeforeWholeMonths(Date fromDate)
        {
            if (IsLeapMonth(fromDate.Year, fromDate.Month))
                return DaysMap.MapMonthDays(fromDate.Month) + 1 - fromDate.Day;
            else
                return DaysMap.MapMonthDays(fromDate.Month) - fromDate.Day;
        }

        /// <summary>
        /// Days in current year after given date.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="fromDate">From date.</param>
        private static int DaysBeforeWholeYears(Date fromDate)
        {
            int totalDays = DaysBeforeWholeMonths(fromDate);
            totalDays += DaysInWholeMonths(fromDate.Year, Convert.ToInt16(fromDate.Month + 1), DaysMap.YearMonths);

            return totalDays;
        }

        /// <summary>
        /// Days in a range of months.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="year">Year.</param>
        /// <param name="fromMonth">From month.</param>
        /// <param name="toMonth">To month.</param>
        private static int DaysInWholeMonths(short year, short fromMonth, short toMonth){
            if(toMonth - fromMonth < 0)
                return 0;
            int totalDays = 0;
            for (short i = fromMonth; i <= toMonth; i++)
            {
                if (IsLeapMonth(year, i))
                    totalDays++;
                totalDays += DaysMap.MapMonthDays(i);
            }

            return totalDays;
        }

        /// <summary>
        /// Days in a range of years.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        private static int DaysInWholeYears(short fromYear, short toYear)
        {
            short range = Convert.ToInt16(toYear - fromYear - 1);
            if (range <= 0)
                return 0;
            int totalDays = 0;
            for (short i = Convert.ToInt16(fromYear + 1); i < toYear; i++)
            {
                if (IsLeapYear(i))
                {
                    totalDays++;
                    i += 3;
                }
            }
            totalDays += DaysMap.YearDays * range;

            return totalDays;
        }

        /// <summary>
        /// Days in current year till given date.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="toDate">To date.</param>
        private static int DaysAfterWholeYears(Date toDate)
        {
            int totalDays = DaysInWholeMonths(toDate.Year, 1, Convert.ToInt16(toDate.Month - 1));
            totalDays += toDate.Day;

            return totalDays;
        }

        /// <summary>
        /// Days in current month till given date.
        /// </summary>
        /// <returns>Number of days.</returns>
        /// <param name="toDate">To date.</param>
        private static int DaysAfterWholeMonths(Date toDate)
        {
            return toDate.Day;
        }
    }
}
