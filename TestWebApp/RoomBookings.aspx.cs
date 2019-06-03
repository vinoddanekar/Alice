using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class RoomBookings : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FillRoomList(ddlRooms);
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                txtBookDate.Text = DateTime.Today.ToString("dd-MMM-yy");
            }
            TestFunction();
        }

        private void TestFunction()
        {
            /*
             https://www.regextester.com/95367
             \b(book[\sroom]*)\s(\w[\s\w]*)\s(from)\s(\w)\s(to)\s(\w)\b
             https://regexr.com/
             \b(book[\sroom]*)\s(\w[\s\w]*)\s(from)\s(\w)[\s](to|-)[\s](\w)\b

            book room amsterdam from 1 to 2
book amsterdam from 1 to 2
book training room from 1 to 2
book room training room from 1 to 2
book room amsterdam from 1 - 2
book amsterdam from 1 - 2
book training room from 1 - 2
book room training room from 1 - 2

             */
            Regex exp = new Regex(@"\b(book[\sroom]*)\s(\w[\s\w]*)\s(from)\s(\w)[\s](to|-)[\s](\w)\b");
            string text = "book room training room from 1 - 2";
            Match match = exp.Match(text);

           // Regex exp2 = new Regex(@"\b(\w*book\w*)\s(\w*)\s(\w*from\w*)\s(\w)\s(\w*to\w*)\s(\w)\b");
            string text2 = "book room training from 1 to 2";
            Match match2 = exp.Match(text2);

        }

        private void FillRoomList(DropDownList dropDownList)
        {
            RoomRepository roomRepository = new RoomRepository();
            IList<Room> rooms = roomRepository.List();
            dropDownList.DataTextField = "Name";
            dropDownList.DataSource = rooms;
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, "Any");
        }

        private void FillTimeSlots(DropDownList dropDownList)
        {
            DateTime date = new DateTime(2018, 05, 30, 9, 0, 0);
            short intervalInMinutes = 30;

            dropDownList.Items.Clear();
            for (short slotIndex = 0; slotIndex < 23; slotIndex++)
            {
                string slot = date.AddMinutes(slotIndex * intervalInMinutes).ToString("h:mm tt");
                dropDownList.Items.Add(slot);
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {

            BookingRequest request = new BookingRequest
            {
                RoomName = ddlRooms.Text,
                BookedBy = "Vinod",
                BookedFor = "Meeting",
                //BookFrom = Convert.ToDateTime(string.Format("{0} {1}", txtBookDate.Text, ddlBookFrom.Text)),
                //BookTo = Convert.ToDateTime(string.Format("{0} {1}", txtBookDate.Text, ddlBookTo.Text))
            };

            BookingRepository bookingRepository = new BookingRepository();
            bookingRepository.Book(request);
        }
        
        protected void ddlRooms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    
}