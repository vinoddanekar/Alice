using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace Alice.Framework
{
    public class DefaultAliceRequestHandler : IAliceRequestHandler
    {
        public string RequestsDataFile { get { return "DefaultRequests.json"; } }
        private IAliceRequest _aliceRequest;
        public IAliceResponse Handle(IAliceRequest request)
        {
            _aliceRequest = request;
            IAliceResponse response;
            switch (request.ServerAction)
            {
                case "AddToUnhandled":
                    response = AddToUnhandled();
                    break;

                default:
                throw new NotImplementedException("DefaultAliceRequestHandler not implemented");
            }

            return response;
        }

        private IAliceResponse AddToUnhandled()
        {
            UnhandledMessage unhandledMessage = new UnhandledMessage();
            unhandledMessage.UserMessage = _aliceRequest.RequestMessage;

            UnhandledMessageRepository repository = new UnhandledMessageRepository("UnhandledMessages.json");
            repository.Add(unhandledMessage);

            IAliceResponse response = new AliceResponse();
            response.Message = "I did not get it but noted down. I will learn it soon. Show <a {aliceRequestAct}>Help</a>";
            return response;
        }
    }
}
