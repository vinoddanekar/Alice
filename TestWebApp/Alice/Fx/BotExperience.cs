using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp
{
    public class BotExperience
    {
        /// <summary>
        /// Converts string to time
        /// </summary>
        /// <param name="input">e.g. 1, 13, 1pm, 1:00pm</param>
        /// <returns></returns>
        public DateTime FormatTime(string input)
        {
            DateTime time;
            int inputToInt;
            string inputToConvert;
            if (int.TryParse(input, out inputToInt))
                inputToConvert = inputToInt.ToString("00") + ":00";
            else
                inputToConvert = input;

            string[] formats = new string[] {"h:m tt", "h:m", "htt", "h tt", "H:mm" };
            bool isParsed;
            isParsed = DateTime.TryParseExact(inputToConvert, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out time);
            if (!isParsed)
                throw new Exception("Invalid input");

                return time;
        }
    }
}