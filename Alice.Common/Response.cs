using System;
using System.Net;

namespace Alice.Common
{
    public interface IAliceResponse
    {
        string Message { get; set; }
        string ActionToPerform { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }

    public class AliceResponse : IAliceResponse
    {
        public string Message { get; set; }
        public string ActionToPerform { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public AliceResponse()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public AliceResponse(string message, string actionToPerform)
        {
            Message = message;
            ActionToPerform = actionToPerform;
        }
    }
}
