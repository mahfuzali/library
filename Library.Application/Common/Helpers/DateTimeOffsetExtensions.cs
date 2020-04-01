using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Common.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public static string DateTimeToString(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("d"); 
        }
    }
}
