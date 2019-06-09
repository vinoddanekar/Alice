using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Framework
{
    class ExactTextMatchStrategy : ITextMatchStrategy
    {
        public bool Match(string text, string pattern)
        {
            string[] patternChunk = pattern.Split('|');

            for (int chunkIndex = 0; chunkIndex < patternChunk.Length; chunkIndex++)
            {
                if (text == patternChunk[chunkIndex])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
