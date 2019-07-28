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
    public static class StringSoundExtensions
    {
        public static bool SoundsLike(this string source, string target)
        {
            string sourceSound = GetSound(source);
            string targetSound = GetSound(target);

            return sourceSound == targetSound;
        }

        private static string GetSound(string text)
        {
            string textLowered = text.ToLower();
            char firstLetter = textLowered[0];
            string remainingString = textLowered.Substring(1);

            string spacesRemoved = remainingString.Replace(" ", string.Empty);
            string vowelsReplaced = ReplaceVowels(spacesRemoved);
            textLowered = firstLetter + vowelsReplaced;

            string consonentsRemoved = ReplaceConsonents(textLowered);
            string sameAdjecantDigitsRemoved = ReplaceSameAdjacentDigitsToOne(consonentsRemoved);
            string zerosRemoved = sameAdjecantDigitsRemoved.Replace("0", string.Empty);
            string nonDigitsRemoved = RemoveNonDigits(zerosRemoved);
            string result = Limit(nonDigitsRemoved);

            return result;
        }

        private static string Limit(string input)
        {
            string result = input;
            if(input.Length < 4)
            {
                result = input.PadRight(4, '0');
            }

            return result;
        }
        private static string RemoveNonDigits(string input)
        {
            string result = input;
            for (int index = input.Length - 1; index > 0 ; index--)
            {
                if (!char.IsDigit(result[index]) && !char.IsLetter(result[index]))
                {
                    result = result.Remove(index, 1);
                }
            }

            return result;
        }

        public static string ReplaceConsonents(string input)
        {
            input = ReplaceWithToken(input, "bfpv", '1');
            input = ReplaceWithToken(input, "cgjkqsxz", '2');
            input = ReplaceWithToken(input, "dt", '3');
            input = ReplaceWithToken(input, "l", '4');
            input = ReplaceWithToken(input, "mn", '5');
            input = ReplaceWithToken(input, "r", '6');

            return input;
        }

        public static string ReplaceVowels(string input)
        {
            input = ReplaceWithToken(input, "aeiouyhw", '0');
            return input;
        }

        public static string ReplaceWithToken(string input, string chars, char token)
        {
            for (int index = 0; index < chars.Length; index++)
            {
                input = input.Replace(chars[index], token);
            }

            return input;
        }

        public static string ReplaceSameAdjacentDigitsToOne(string input)
        {
            int index = 0;
            bool canIncrease;
            while(index < input.Length - 1)
            {
                canIncrease = true;
                if (input[index] == input[index + 1])
                {
                    input = input.Remove(index, 1);
                    canIncrease = false;
                }

                if(canIncrease)
                    index++;
            }

            return input;
        }
    }
}
