using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alice.Common;
using System;

namespace Alice.Tests.Common
{
    [TestClass]
    public class StringSoundExtensionsTests
    {
        [TestMethod]
        public void SoundsLikeTest()
        {
            bool result = "how are you".SoundsLike("h r u?");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SoundsLikeTest2()
        {
            bool result = "book room for me".SoundsLike("buk rum 4 me");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReplaceVowelsTest()
        {
            string input = "the quick brown fox jumped over the little lazy dogs";

            string expected = "t00 q00ck br00n f0x j0mp0d 0v0r t00 l0ttl0 l0z0 d0gs";
            string actual = StringSoundExtensions.ReplaceVowels(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReplaceConsonentsTest()
        {
            string input = "the quick brown fox jumped over the little lazy dogs";

          //string expected = "300 20022 160w5 102 205103 0106 300 403340 4020 3022";
            string expected = "3he 2ui22 16ow5 1o2 2u51e3 o1e6 3he 4i334e 4a2y 3o22";
            string actual = StringSoundExtensions.ReplaceConsonents(input);

            Assert.AreEqual(expected, actual);
        }

    }
}
