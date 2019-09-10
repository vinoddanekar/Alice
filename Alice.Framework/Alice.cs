using Alice.Common;
using System;

namespace Alice.Framework
{
    public class Alice
    {
        public IAliceResponse Ask(UserRequest request)
        {
            IAliceResponse response;
            response = ProcessMessage(request.Message);
            return response;
        }

        public IAliceResponse Ask(string userMessage)
        {
            userMessage = CleanupRequestMessage(userMessage);
            IAliceResponse response;
            response = ProcessMessage(userMessage);

            return response;
        }

        private IAliceResponse ProcessMessage(string userMessage)
        {
            userMessage = CleanupRequestMessage(userMessage);

            CommandFinder finder = new CommandFinder();
            Tuple<Command, IAliceRequestHandler> tuple;
            tuple = finder.FindOrDefault(userMessage);

            Command command = tuple.Item1;
            IAliceRequestHandler handler = tuple.Item2;
            CommandProcessor processor = new CommandProcessor(handler, command, userMessage);
            IAliceResponse response = processor.Process();

            return response;
        }

        private string CleanupRequestMessage(string userMessage)
        {
            string result = userMessage;
            if (result.StartsWith("\n"))
                result = result.Substring(1, result.Length - 1);

            if (result.EndsWith("\n"))
                result = result.Substring(0, result.Length - 1);

            // Quick fix to avoid \n in request. Later it should split request
            result = result.Replace("\n", string.Empty);
            
            return result;
        }
    }
}