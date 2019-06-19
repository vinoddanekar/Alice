using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace Alice.Common
{
    public class UserProfile : IUserProfile
    {
        public string UserName { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }

        public UserProfile()
        {

        }
    }
}
