using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class TimePicker : System.Web.UI.UserControl
    {
        private string hour;
        private string minute;
        public DateTime SelectedTime
        {
            set
            {
                try
                {
                    DateTime time = value;
                    CalendarExtender1.SelectedDate = time.Date;
                    dropHour.SelectedValue = time.Hour.ToString();
                    dropMinute.SelectedValue = time.Minute.ToString();
                }
                catch { }
            }
            get
            {
                DateTime time;
                if (CalendarExtender1.SelectedDate != null)
                {
                    int year = CalendarExtender1.SelectedDate.Year;
                    int mouth = CalendarExtender1.SelectedDate.Month;
                    int day = CalendarExtender1.SelectedDate.Day;
                    int hour = Convert.ToInt16(dropHour.SelectedValue);
                    int minute = Convert.ToInt16(dropMinute.SelectedValue);
                    time = new DateTime(year, mouth, day, hour, minute, 0);
                }
                else { time = new DateTime(); }
                return time;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int h = 0; h < 24; h++)
            {
                dropHour.Items.Add(new ListItem(h.ToString().PadLeft(2, '0'), h.ToString().PadLeft(2, '0')));
            }
            for (int m = 0; m < 60; m++)
            {
                dropMinute.Items.Add(new ListItem(m.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0')));
            }               
        }
    }
}