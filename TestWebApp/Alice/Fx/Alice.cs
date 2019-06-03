using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Alice.Fx
{
    public class Alice
    {        
        public Response Ask(string message)
        {
            Response response;
            response = ProcessMessage(message);
            return response;
        }

        private Response ProcessMessage(string message)
        {
            AliceLearningRepository repository = new AliceLearningRepository();
            IList<Communication> communications = repository.List();
            Communication entry = FindEntry(communications, message);
            if(entry == null)
            {
                entry = FindEntry(communications, "*");
            }
            Response response = ProcessEntry(entry);

            return response;
        }
                
        private Response ProcessEntry(Communication entry)
        {
            Response response = new Response();
            response.ActionToPerform = entry.ClientAction;

            if (string.IsNullOrEmpty(entry.ServerAction))
            {
                response.Message = entry.GetRandomSuccessResponse();
            }
            else if (entry.ServerAction == "BookingParser.List(today)")
            {
                BookingAliceParser parser = new BookingAliceParser();
                string message = parser.ListBookings(DateTime.Now);
                response.Message = message;
            }

            return response;
        }

        private Communication FindEntry(IList<Communication> entries, string message)
        {
            Communication currenEntry = null;
            Communication matchedEntry = null;

            for (int entryIndex = 0; entryIndex < entries.Count; entryIndex++)
            {
                currenEntry = entries[entryIndex];
                bool isEntryMatched = MatchEntry(entries, currenEntry, message);

                if (isEntryMatched)
                {
                    matchedEntry = currenEntry;
                    break;
                }
            }

            return matchedEntry;
        }

        private bool MatchEntry(IList<Communication> entries, Communication entry, string message)
        {
            string[] userMessagesToMatch = entry.UserSays.Split('|');

            for (int messageIndex = 0; messageIndex < userMessagesToMatch.Length; messageIndex++)
            {
                if (message == userMessagesToMatch[messageIndex])
                {
                    return true;
                }
            }

            return false;
        }

    }
}