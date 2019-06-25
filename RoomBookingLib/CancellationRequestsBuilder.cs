using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    public class CancellationRequestsBuilder
    {
        IList<Alice.Common.AliceRequestParameter> _parameters;
        IUserProfile _userProfile;

        public CancellationRequestsBuilder(IList<Alice.Common.AliceRequestParameter> parameters)
        {
            _parameters = parameters;
        }

        public CancellationRequestsBuilder(IList<Alice.Common.AliceRequestParameter> parameters, IUserProfile userProfile)
        {
            _parameters = parameters;
            _userProfile = userProfile;
        }

        public BookingRequest Build()
        {
            BookingRequest bookingRequest = null;

            switch (_parameters.Count)
            {
                case 8:
                    bookingRequest = CreateCompleteRequest();
                    break;
                default:
                    break;
            }

            return bookingRequest;
        }

        /*
         *0 Cancel my booking on 12/1 from 12 of room 1
        *1 cancel my booking
        *2 on
        *3 12/1
        *4 from
        *5 12
        *6 of
        *7 room 1
         */
        private BookingRequest CreateCompleteRequest()
        {            
            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.RoomName = _parameters[7].Value;
            bookingRequest.BookFromUtc = GetTimeInUtc(_parameters[3].Value, _parameters[5].Value);
            bookingRequest.BookedBy = _userProfile.UserName;

            return bookingRequest;
        }

        private DateTime GetTimeInUtc(string datePart, string timePart)
        {
            DateTime dateTime = Utility.ConvertToDateTime(datePart, timePart);
            DateTime dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(dateTime, _userProfile.TimeZoneInfo);

            return dateTimeUtc;
        }

        private DateTime GetTimeInUtc(DateTime datePart, string timePart)
        {
            DateTime dateTime = Utility.ConvertToDateTime(datePart, timePart);
            DateTime dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(dateTime, _userProfile.TimeZoneInfo);

            return dateTimeUtc;
        }
    }
}
