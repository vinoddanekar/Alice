using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alice.Common;
using System;

namespace Alice.Tests.Common
{
    [TestClass]
    public class Utility_ConvertToTime_Tests
    {
        [TestMethod]
        public void ConvertToTime_Am_H_Test()
        {
            string input = "1";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(1);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Am_Htt_Test()
        {
            string input = "1am";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(1);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Am_Hmm_Test()
        {
            string input = "1:00";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(1);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Am_HmmTt_Test()
        {
            string input = "1:00AM";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(1);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Am_Hmmtt_Test()
        {
            string input = "1:00am";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(1);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Pm_H_Test()
        {
            string input = "13";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(13);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Pm_Htt_Test()
        {
            string input = "1pm";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(13);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Pm_Hmm_Test()
        {
            string input = "13:00";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(13);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Pm_HmmTt_Test()
        {
            string input = "1:00PM";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(13);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ConvertToTime_Pm_Hmmtt_Test()
        {
            string input = "1:00pm";
            DateTime expectedOutput = DateTime.Now.Date.AddHours(13);

            DateTime actualOutput = Utility.ConvertToTime(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
}
