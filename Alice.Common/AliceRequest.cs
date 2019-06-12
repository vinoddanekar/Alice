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
        string UserName { get; set; }

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
        public string UserName { get; set; }
    }
}
