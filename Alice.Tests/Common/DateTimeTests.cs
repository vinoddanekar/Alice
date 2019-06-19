using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alice.Common;
using System;

namespace Alice.Tests.Common
{
    [TestClass]
    public class DateTimeTests
    {
        /// <summary>
        /// Local IST: 18:10:00 should be UTC: 12:40:00
        /// </summary>
        [TestMethod]
        public void ConvertToUtc()
        {
            DateTime expectedOutput = DateTime.Parse("21-Oct-1982 12:40:00");

            DateTime input = DateTime.Parse("21-Oct-1982 18:10:00");
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(input, "India Standard Time");
            DateTime actualOutput = localTime.ToUniversalTime();
            
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
