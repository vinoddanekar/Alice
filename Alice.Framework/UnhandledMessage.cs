using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Framework
{
    class UnhandledMessage
    {
        public string UserMessage { get; set; }
        public int InstanceCount { get; set; }

        public UnhandledMessage()
        {
            InstanceCount = 1;
        }
    }
}
