using System;
using System.Collections.Generic;

namespace DatesGap.Business
{
    public static class DaysMap
    {
        public static readonly short YearDays = 365;
        public static readonly short YearMonths = 12;
        public static readonly short LeapYearMonth = 2;
        private static readonly Dictionary<Int16, Int16> MonthDaysMap = new Dictionary<Int16, Int16>()
        {
            {1, 31},
            {2, 28},
            {3, 31},
            {4, 30},
            {5, 31},
            {6, 30},
            {7, 31},
            {8, 31},
            {9, 30},
            {10, 31},
            {11, 30},
            {12, 31}
        };

        public static int MapMonthDays(short month){
            return MonthDaysMap[month];
        }
    }
}
