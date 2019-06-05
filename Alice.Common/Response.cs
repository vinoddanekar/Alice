using System;

namespace Alice.Common
{
    public interface IAliceResponse
    {
        string Message { get; set; }
        string ActionToPerform { get; set; }
    }

    public class Response
    {
        public string Message { get; set; }
        public string ActionToPerform { get; set; }

        public Response()
        {

        }

        public Response(string message, string actionToPerform)
        {
            Message = message;
            ActionToPerform = actionToPerform;
        }
    }

}
