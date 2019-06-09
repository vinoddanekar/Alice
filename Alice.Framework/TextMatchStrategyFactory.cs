using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
