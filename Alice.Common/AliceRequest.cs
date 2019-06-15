using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    public interface IAliceRequest
    {
        string RequestMessage { get; set; }
        List<AliceRequestParameter> Parameters { get; }
        string ServerAction { get; set; }
        IUserProfile UserProfile { get; }
    }
    
    public class AliceRequest : IAliceRequest
    {
        public string RequestMessage { get; set; }

        private List<AliceRequestParameter> _parameters;
        public List<AliceRequestParameter> Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new List<AliceRequestParameter>();
                return _parameters;
            }
        }
        public string ServerAction { get; set; }

        private IUserProfile _userProfile;
        public IUserProfile UserProfile
        {
            get
            {
                if (_userProfile == null)
                    _userProfile = new UserProfile();

                return _userProfile;
            }
        }
    }
}
