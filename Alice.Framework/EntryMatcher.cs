using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Alice.Common;

namespace Alice.Framework
{
    public class EntryMatcher
    {
        public bool MatchEntry(Command entry, string message)
        {
            bool matchResult = false;
            if (entry.RequestFormat == "regex")
                matchResult = MatchRegex(entry, message);
            else if (entry.RequestFormat == "pattern")
                matchResult = MatchRegex(entry, message);
            else
                matchResult = MatchExact(entry, message);

            return matchResult;
        }

        private bool MatchExact(Command entry, string message)
        {
            string[] userMessagesToMatch = entry.UserMessage.Split('|');

            for (int messageIndex = 0; messageIndex < userMessagesToMatch.Length; messageIndex++)
            {
                if (message == userMessagesToMatch[messageIndex])
                {
                    return true;
                }
            }

            return false;
        }

        private bool MatchRegex(Command entry, string message)
        {
            Regex exp = new Regex(entry.UserMessage);
            bool matchResult = exp.IsMatch(message);

            return matchResult;
        }
    }
}