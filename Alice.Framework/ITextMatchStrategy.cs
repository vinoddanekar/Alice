using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Framework
{
    interface ITextMatchStrategy
    {
        bool Match(string text, string pattern);
    }
}
