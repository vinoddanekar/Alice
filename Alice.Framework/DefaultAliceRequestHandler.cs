using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace Alice.Framework
{
    public class DefaultAliceRequestHandler : IAliceRequestHandler
    {
        public string RequestsDataFile { get { return "DefaultRequests.json"; } }

        public IAliceResponse Execute(IAliceRequest request)
        {
            throw new NotImplementedException("DefaultAliceRequestHandler not implemented");
        }
    }
}
