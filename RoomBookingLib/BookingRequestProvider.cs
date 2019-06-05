using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class BookingRequestProvider
    {
        public IList<BookingRequest> GetBulkRequests(string roomName, string bookie, string subject, string requestedDates, string requestedSlots)
        {
            string[] dates = requestedDates.Split(',');
            throw new NotImplementedException();
        }

        private DateTime[] GetDateTimes(string input)
        {
            string[] dates = input.Split(',');
            DateTime[] dateTimes = new DateTime[dates.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                dateTimes[i] = Convert.ToDateTime(dates[i]);
            }

            return dateTimes;
        }
    }
}
