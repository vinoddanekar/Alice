using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Alice.Fx
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