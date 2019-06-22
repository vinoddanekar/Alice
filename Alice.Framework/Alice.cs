﻿using System;
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
            Tuple<Command, IAliceRequestHandler> tuple;
            tuple = finder.FindOrDefault(userMessage);

            Command command = tuple.Item1;
            IAliceRequestHandler handler = tuple.Item2;
            CommandProcessor processor = new CommandProcessor(handler, command, userMessage);
            IAliceResponse response = processor.Process();

            return response;
        }
    }
}