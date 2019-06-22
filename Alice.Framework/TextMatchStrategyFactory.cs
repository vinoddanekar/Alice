using Alice.Framework.TextMatching;

namespace Alice.Framework
{
    class TextMatchStrategyFactory
    {
        public ITextMatchStrategy GetStrategy(string strategyName)
        {
            ITextMatchStrategy strategy;

            if (strategyName == "regex")
                strategy = new RegexTextMatchStrategy();
            else
                strategy = new ExactTextMatchStrategy();

            return strategy;
        }
    }
}
