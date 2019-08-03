using Alice.Common;
using System.Text.RegularExpressions;

namespace Alice.Framework
{
    class RequestBuilder
    {
        private Command _command;
        private string _userMessage;
        private UserProfile _userProfile;
        
        public RequestBuilder(Command command, string userMessage, UserProfile userProfile)
        {
            _command = command;
            _userMessage = userMessage;
            _userProfile = userProfile;
        }

        public IAliceRequest Build()
        {
            IAliceRequest request = new AliceRequest();
            request.ServerAction = _command.ServerAction;
            request.RequestMessage = _userMessage;
            AddParameters(request);
            request.UserProfile.UserName = _userProfile.UserName;
            request.UserProfile.TimeZoneInfo = _userProfile.TimeZoneInfo;

            return request;
        }

        private void AddParameters(IAliceRequest request)
        {
            if (_command.MatchingPattern == "regex")
            {
                Regex regex = new Regex(_command.UserMessage, RegexOptions.IgnoreCase);
                Match match = regex.Match(_userMessage);
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    AliceRequestParameter param = new AliceRequestParameter();
                    param.Name = i.ToString();
                    param.Value = match.Groups[i].Value;
                    request.Parameters.Add(param);
                }
            }
        }
    }
}
