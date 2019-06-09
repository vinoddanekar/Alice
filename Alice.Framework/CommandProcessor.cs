using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            //TODO Move to room bookings
            //else if (entry.ServerAction == "BookingParser.List(today)")
            //{
            //    BookingAliceParser parser = new BookingAliceParser();
            //    string responseMessage = parser.ListBookings(DateTime.Now);
            //    response.Message = responseMessage;
            //}
            //if(entry.RequestFormat == "regex")
            //{
            //    BookingAliceParser parser = new BookingAliceParser();

            //    AliceRequest command = new AliceRequest();

            //    Regex regex = new Regex(entry.UserMessage);
            //    Match match = regex.Match(userMessage);
            //    for (int i =0; i<match.Groups.Count;i++)
            //    {
            //        AliceRequestParameter param = new AliceRequestParameter();
            //        param.Name = i.ToString();
            //        param.Value = match.Groups[i].Value;
            //        command.Parameters.Add(param);
            //    }
            //    response.Message = parser.Book(command);
            //}

            return response;

        }

        private IAliceRequest CreateRequest()
        {
            IAliceRequest request = new AliceRequest();
            request.ServerAction = _command.ServerAction;
            request.RequestMessage = _userMessage;
            return request;
        }

    }
}
