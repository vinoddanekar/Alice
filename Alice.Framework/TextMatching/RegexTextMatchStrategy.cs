﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alice.Framework.TextMatching
{
    class RegexTextMatchStrategy : ITextMatchStrategy
    {
        public bool Match(string text, string pattern)
        {
            Regex exp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            bool matchResult = exp.IsMatch(text);

            return matchResult;
        }
    }
}
