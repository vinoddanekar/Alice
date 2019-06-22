using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    interface IInternalRequestHandler
    {
        IAliceResponse Handle(IAliceRequest request);
    }
}
