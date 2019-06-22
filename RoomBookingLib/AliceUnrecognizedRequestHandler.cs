using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    class AliceUnrecognizedRequestHandler : IInternalRequestHandler
    {
        public IAliceResponse Handle(IAliceRequest request)
        {
            IAliceResponse response = new AliceResponse();
            response.Message = "Booking service could not serve this request.";
            
            return response;
        }
    }
}
