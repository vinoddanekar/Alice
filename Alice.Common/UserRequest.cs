using System;
using System.Net;

namespace Alice.Common
{
    public interface IUserRequest
    {
        string Message { get; set; }
    }

    public class UserRequest : IUserRequest
    {
        public string Message { get; set; }

        public UserRequest(){}

        public UserRequest(string message)
        {
            Message = message;
        }
    }

}
