using Alice.Common;

namespace Alice.Framework.TextMatching
{
    class SoundsLikeTextMatchStrategy : ITextMatchStrategy
    {
        public bool Match(string text, string pattern)
        {
            string[] patternChunk = pattern.Split('|');

            for (int chunkIndex = 0; chunkIndex < patternChunk.Length; chunkIndex++)
            {
                if (text.SoundsLike(pattern))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
