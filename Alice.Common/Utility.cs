using System;
using System.Linq;

namespace Alice.Common
{
    public static class Utility
    {
        /// <summary>
        /// Converts string to time
        /// </summary>
        /// <param name="input">e.g. 1, 13, 1pm, 1:00pm</param>
        /// <returns></returns>
        public static DateTime ConvertToTime(string input)
        {
            DateTime time;
            int inputToInt;
            string inputToConvert;
            if (int.TryParse(input, out inputToInt))
                inputToConvert = inputToInt.ToString("00") + ":00";
            else
                inputToConvert = input;

            string[] formats = new string[] {"h:mmtt", "h:m tt", "h:m", "htt", "h tt", "H:mm" };
            bool isParsed;
            isParsed = DateTime.TryParseExact(inputToConvert, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out time);
            if (!isParsed)
                throw new Exception(string.Format("Can not convert {0} to time", input));

            return time;
        }

        public static DateTime ConvertToDate(string input)
        {
            DateTime date;
            string inputToConvert;

            if (TryParseKnownDateTime(input, out date))
                return date;

            inputToConvert = ConvertToDateFormat(input);

            string[] formats = new string[] { "dd-MMMM-yyyy", "dd-MMM-yyyy", "dd-MM-yyyy", "dd-MMMM-yy", "dd-MMM-yy", "dd-MM-yy" };
            bool isParsed;
            isParsed = DateTime.TryParseExact(inputToConvert, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date);
            if (!isParsed)
                throw new Exception(string.Format("Can not convert {0} to date", input));

            return date;
        }

        private static bool TryParseKnownDateTime(string input, out DateTime output)
        {
            input = input.ToLower();
            bool result = true;
            output = DateTime.MinValue;
            DateTime currentDate = DateTime.Now.Date;

            if (input.SoundsLike("today"))
                output = currentDate;
            else if (input.SoundsLike("tomorrow") || input.SoundsLike("tom"))
                output = currentDate.AddDays(1);
            else if (input.SoundsLike("day after tomorrow") || input.SoundsLike("day after tom"))
                output = currentDate.AddDays(2);
            else if (input.StartsWith("next"))
            {

            }
            else
                result = false;
            
            return result;
        }

        //private static DateTime GetDateFromNextWeekDay(DateTime currentDate, string day)
        //{
            
        //}

        private static string ConvertToDateFormat(string input)
        {
            char[] splitChars = new char[] { '-', '/', ' ' };
            string[] parts = input.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            string output = string.Empty;
            string day, month, year;

            GetDateParts(parts, out year, out month, out day);            
            output = string.Format("{0}-{1}-{2}", day, month, year);

            return output;
        }

        private static void GetDateParts(string[] parts, out string year, out string month, out string day)
        {
            day = string.Empty;
            month = DateTime.Now.Month.ToString("00");
            year = DateTime.Now.Year.ToString("0000");

            if (parts.Length > 0)
            {
                day = Convert.ToInt16(parts[0]).ToString("00");
            }
            if (parts.Length > 1)
            {
                month = parts[1];
                if (IsNumeric(month))
                    month = Convert.ToInt16(month).ToString("00");
            }
            if (parts.Length > 2)
            {
                year = parts[2];
                if (year.Length == 1)
                    year = Convert.ToInt16(year).ToString("00");
            }
        }

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public static DateTime ConvertToDateTime(DateTime date, string timeText)
        {
            DateTime time = ConvertToTime(timeText);
            
            DateTime result;
            result = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            return result;
        }

        public static DateTime ConvertToDateTime(string dateText, string timeText)
        {
            DateTime date = ConvertToDate(dateText);
            DateTime time = ConvertToTime(timeText);

            DateTime result;
            result = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            return result;
        }
    }
}
