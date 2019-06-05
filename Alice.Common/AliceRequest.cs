using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    public class AliceRequest
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
    }
}
