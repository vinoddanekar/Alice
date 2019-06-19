using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            switch (input)
            {
                case "today":
                    output = DateTime.Now.Date;
                    break;

                case "tom":
                case "tomm":
                case "tomorrow":
                    output = DateTime.Now.Date.AddDays(1);
                    break;

                case "day after tom":
                case "day after tomm":
                case "day after tomorrow":
                    output = DateTime.Now.Date.AddDays(2);
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }

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
