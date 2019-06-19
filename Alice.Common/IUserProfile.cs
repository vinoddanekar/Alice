using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    public interface IUserProfile
    {
        string UserName { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
