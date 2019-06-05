using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    public interface IAliceRequestHandler
    {
        IAliceResponse Execute(IAliceRequest request);
    }
}
