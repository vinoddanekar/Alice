using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alice.Common;
using System;

namespace Alice.Tests.Common
{
    [TestClass]
    public class Utility_ConvertToDate_Tests
    {
        [TestMethod]
        public void ConvertToDate_D_Test()
        {
            string input = "1";
            DateTime expectedOutput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Dm_Test()
        {
            string input = "1-1";
            DateTime expectedOutput = new DateTime(DateTime.Now.Year, 1, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Dmy_Test()
        {
            string input = "1-1-1";
            DateTime expectedOutput = new DateTime(2001, 1, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Dmmmy_Test()
        {
            string input = "1-apr";
            DateTime expectedOutput = new DateTime(DateTime.Now.Year, 4, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Dmmmmy_Test()
        {
            string input = "1-april";
            DateTime expectedOutput = new DateTime(DateTime.Now.Year, 4, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Dmmmmyy_Test()
        {
            string input = "1-april-19";
            DateTime expectedOutput = new DateTime(2019, 4, 1);

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Today_Test()
        {
            string input = "today";
            DateTime expectedOutput = DateTime.Now.Date;

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_Tomorrow_Test()
        {
            string input = "tomorrow";
            DateTime expectedOutput = DateTime.Now.AddDays(1).Date;

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToDate_DayAfterTomorrow_Test()
        {
            string input = "day after tomorrow";
            DateTime expectedOutput = DateTime.Now.AddDays(2).Date;

            DateTime actualOutput = Utility.ConvertToDate(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
}
