using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alice.Framework
{
    class CommandProcessor
    {
        private IAliceRequestHandler _handler;
        private Command _command;
        private string _userMessage;

        public CommandProcessor(IAliceRequestHandler handler, Command command, string userMessage)
        {
            _handler = handler;
            _command = command;
            _userMessage = userMessage;
        }

        public IAliceResponse Process()
        {
            IAliceResponse response;

            if (string.IsNullOrEmpty(_command.ServerAction))
            {
                response = new AliceResponse();
                response.ActionToPerform = _command.ClientAction;
                response.Message = _command.GetRandomSuccessResponse();
            }
            else
            {
                IAliceRequest request = CreateRequest();
                response = _handler.Execute(request);
            }

            return response;
        }

        private IAliceRequest CreateRequest()
        {
            RequestBuilder requestBuilder = new RequestBuilder(_command, _userMessage);
            IAliceRequest request = requestBuilder.Build();

            return request;
        }
    }
}
