using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Alice.Common;

namespace Alice.Framework
{
    public class Alice
    {
        public IAliceResponse Ask(string userMessage)
        {
            IAliceResponse response;
            response = ProcessMessage(userMessage);
            return response;
        }

        private IAliceResponse ProcessMessage(string userMessage)
        {
            CommandFinder finder = new CommandFinder();
            IAliceRequestHandler handler;
            Command command = finder.FindOrDefault(userMessage, out handler);

            CommandProcessor processor = new CommandProcessor(handler, command, userMessage);
            IAliceResponse response = processor.Process();

            return response;
        }


        //private string ShowListResponse(Regex regex, string userMessage)
        //{
        //    Match match = regex.Match(userMessage);
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var item in match.Groups)
        //    {
        //        sb.Append(item.ToString() + "<br/>");
        //    }
        //    return sb.ToString();
        //}
    }
}