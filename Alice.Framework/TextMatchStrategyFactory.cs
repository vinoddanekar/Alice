using Alice.Framework.TextMatching;

namespace Alice.Framework
{
    class TextMatchStrategyFactory
    {
        public ITextMatchStrategy GetStrategy(string strategyName)
        {
            ITextMatchStrategy strategy;
            switch (strategyName)
            {
                case "regex":
                    strategy = new RegexTextMatchStrategy();
                    break;
                case "soundslike":
                    strategy = new SoundsLikeTextMatchStrategy();
                    break;
                default:
                    strategy = new ExactTextMatchStrategy();
                    break;
            }

            return strategy;
        }
    }
}
