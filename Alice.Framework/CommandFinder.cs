using Alice.Common;
using Alice.Framework.TextMatching;
using System;
using System.Collections.Generic;

namespace Alice.Framework
{
    class CommandFinder
    {
        public Tuple<Command, IAliceRequestHandler> FindOrDefault(string userMessage)
        {
            Command command = null;
            IAliceRequestHandler handler = null;
            for (int index = 0; index < AliceContext.Current.Handlers.Count; index++)
            {
                IAliceRequestHandler currentHandler = AliceContext.Current.Handlers[index];
                command = Find(userMessage, currentHandler);

                if (command != null) {
                    handler = currentHandler;
                    break;
                }
            }

            if (command == null)
            {
                handler = AliceContext.Current.Handlers[0];
                command = CommandFinder.GetDefault();
            }

            Tuple<Command, IAliceRequestHandler> result = new Tuple<Command, IAliceRequestHandler>(command, handler);

            return result;
        }

        private Command Find(string userMessage, IAliceRequestHandler handler)
        {            
            AliceCommandRepository repository;
            repository = new AliceCommandRepository(handler.RequestsDataFile);
            IList<Command> commands = repository.List();
            Command command = Find(userMessage, commands);

            return command;
        }
        
        private Command Find(string userMessage, IList<Command> commands)
        {
            Command currentCommand = null;
            Command foundCommand = null;

            for (int entryIndex = 0; entryIndex < commands.Count; entryIndex++)
            {
                currentCommand = commands[entryIndex];
                TextMatchStrategyFactory textMatchStrategyFactory = new TextMatchStrategyFactory();
                ITextMatchStrategy strategy = textMatchStrategyFactory.GetStrategy(currentCommand.RequestFormat);

                bool isEntryMatched = strategy.Match(userMessage, currentCommand.UserMessage);

                if (isEntryMatched)
                {
                    foundCommand = currentCommand;
                    break;
                }
            }

            return foundCommand;
        }

        public static Command GetDefault()
        {
            Command command = new Command();
            command.ActionSuccessResponse = "I could not understand it.";
            command.ServerAction = "AddToUnhandled";
            return command;
        }
    }
}
