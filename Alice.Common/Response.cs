using System;

namespace Alice.Common
{
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
