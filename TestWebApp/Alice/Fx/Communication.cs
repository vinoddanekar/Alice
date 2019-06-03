using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Alice.Fx
{
    public class Communication
    {
        public string UserSays { get; set; }
        public string ActionSuccessResponse { get; set; }
        public string ActionFailureResponse { get; set; }
        public string ValidationFailureResponse { get; set; }
        public string Validation { get; set; }
        public string ServerAction { get; set; }
        public string ClientAction { get; set; }
        public string UserRespondedTo { get; set; }

        public string GetRandomSuccessResponse()
        {
            string[] responses = ActionSuccessResponse.Split('|');
            Random random = new Random();
            int randomIndex = random.Next(0, responses.Length - 1);
            string response = responses[randomIndex];
            return response;
        }
    }
}