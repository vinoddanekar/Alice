using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class BookingRequestsBuilder
    {
        IList<Alice.Common.AliceRequestParameter> _parameters;
        IUserProfile _userProfile;

        public BookingRequestsBuilder(IList<Alice.Common.AliceRequestParameter> parameters)
        {
            _parameters = parameters;
        }

        public BookingRequestsBuilder(IList<Alice.Common.AliceRequestParameter> parameters, IUserProfile userProfile)
        {
            _parameters = parameters;
            _userProfile = userProfile;
        }

        public BookingRequest Build()
        {
            BookingRequest bookingRequest = null;

            switch (_parameters.Count)
            {
                case 7:
                    bookingRequest = CreateBasicRequest();
                    break;
                case 9:
                    bookingRequest = CreateRequestWithBookie();
                    break;
                case 11:
                    bookingRequest = CreateCompleteRequest();
                    break;

                default:
                    break;
            }

            return bookingRequest;
        }

        /*
        *0 book am from 1 - 2
        *1 book
        *2 am
        *3 from
        *4 1
        *5 -
        *6 2
        */
        private BookingRequest CreateBasicRequest()
        {
            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.RoomName = _parameters[2].Value;
            bookingRequest.BookFromUtc = GetTimeInUtc(DateTime.Now.Date, _parameters[4].Value);
            bookingRequest.BookToUtc = GetTimeInUtc(DateTime.Now.Date, _parameters[6].Value);
            bookingRequest.BookedBy = _userProfile.UserName;

            return bookingRequest;
        }

        /* 0 book am from 1 - 2
         * 0-6 from CreateShortRequest
         * 7 for
         * 8 vinod
         */
        private BookingRequest CreateRequestWithBookie()
        {
            BookingRequest bookingRequest;
            bookingRequest = CreateBasicRequest();
            bookingRequest.BookedFor = _parameters[8].Value;

            return bookingRequest;
        }

        /*
         *0 book someroom on 1/2 from 1 - 2 for meeting
        *1 book
        *2 someroom
        *3 on
        *4 1/2
        *5 from
        *6 1
        *7 -
        *8 2
        *9 for
        *10 meeting
         */
        private BookingRequest CreateCompleteRequest()
        {            
            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.RoomName = _parameters[2].Value;

            bookingRequest.BookFromUtc = GetTimeInUtc(_parameters[4].Value, _parameters[6].Value);
            bookingRequest.BookToUtc = GetTimeInUtc(_parameters[4].Value, _parameters[8].Value);
            bookingRequest.BookedFor = _parameters[10].Value;
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
