using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    /*
     * According to Soundex algorithm: https://en.wikipedia.org/wiki/Soundex
     * 1. Save the first letter. Map all occurrences of a, e, i, o, u, y, h, w. to zero(0)
     * 2. Replace all consonants (include the first letter) with digits as below -
     *      Replace consonants with digits as follows (after the first letter):
     *      b, f, p, v → 1
     *      c, g, j, k, q, s, x, z → 2
     *      d, t → 3
     *      l → 4
     *      m, n → 5
     *      r → 6
     * 3. Replace all adjacent same digits with one digit, and then remove all the zero (0) digits
     * 4. If the saved letter's digit is the same as the resulting first digit, remove the digit (keep the letter).
     * 5. Append 3 zeros if result contains less than 3 digits. Remove all except first letter and 3 digits after it (This step same as [4.] in explanation above).
    */
    public static class StringExtensions
    {
        public static bool SoundsLike(this string source, string targetWord)
        {


            return true;
        }

        private static void GetSound(string text)
        {
            string textLowered = text.ToLower();
            char firstLetter = text[0];
            string remainingString = text.Substring(1);

            string vowelsReplaced = ReplaceVowels(remainingString);
            textLowered = firstLetter + vowelsReplaced;

            string consonentsRemoved = ReplaceVowels(textLowered);
            string sameAdjecantDigitsRemoved = ReplaceSameAdjecantDigitsToOne(consonentsRemoved);
            string zerosRemoved = sameAdjecantDigitsRemoved.Replace("0", string.Empty);

        }

        private static string ReplaceConsonents(string text)
        {
            text = ReplaceWithToken(text, "bfpv", '1');
            text = ReplaceWithToken(text, "cgjkqsxz", '2');
            text = ReplaceWithToken(text, "dt", '3');
            text = ReplaceWithToken(text, "l", '4');
            text = ReplaceWithToken(text, "mn", '5');
            text = ReplaceWithToken(text, "r", '6');

            return text;
        }

        private static string ReplaceVowels(string text)
        {
            text = ReplaceWithToken(text, "aeiouyhw", '0');
            return text;
        }

        private static string ReplaceWithToken(string text, string chars, char digit)
        {
            for (int index = 0; index < chars.Length; index++)
            {
                text = text.Replace(chars[index], digit);
            }

            return text;
        }

        private static string ReplaceSameAdjecantDigitsToOne(string text)
        {
            int index = 0;
            bool canIncrease;
            while(index > text.Length - 1)
            {
                canIncrease = true;
                if (text[index] == text[index + 1])
                {
                    text = text.Remove(index, 1);
                    canIncrease = false;
                }

                if(canIncrease)
                    index++;
            }

            return text;
        }
    }
}
