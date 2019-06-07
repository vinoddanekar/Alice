using System;

namespace Alice.Common
{
    public interface IAliceResponse
    {
        string Message { get; set; }
        string ActionToPerform { get; set; }
    }

    public class AliceResponse : IAliceResponse
    {
        public string Message { get; set; }
        public string ActionToPerform { get; set; }

        public AliceResponse()
        {

        }

        public AliceResponse(string message, string actionToPerform)
        {
            Message = message;
            ActionToPerform = actionToPerform;
        }
    }

}
