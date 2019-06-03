﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Response response = ProcessEntry(entry, message);

            return response;
        }
                
        private Response ProcessEntry(Communication entry, string userMessage)
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
                string responseMessage = parser.ListBookings(DateTime.Now);
                response.Message = responseMessage;
            }            
            if(entry.RequestFormat == "regex")
            {
                Regex regex = new Regex(entry.UserSays);
                Match match = regex.Match(userMessage);
                StringBuilder sb = new StringBuilder();
                foreach (var item in match.Groups)
                {
                    sb.Append(item.ToString() + "<br/>");
                }
                response.Message = sb.ToString();
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
                EntryMatcher matcher = new EntryMatcher();
                bool isEntryMatched = matcher.MatchEntry(currenEntry, message);

                if (isEntryMatched)
                {
                    matchedEntry = currenEntry;
                    break;
                }
            }

            return matchedEntry;
        }

        

    }
}