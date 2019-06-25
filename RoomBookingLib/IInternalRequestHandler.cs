using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    interface IInternalRequestHandler
    {
        IAliceResponse Handle(IAliceRequest request);
    }
}
