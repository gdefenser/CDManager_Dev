using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class CalendarExtender : System.Web.UI.UserControl
    {
        private int year;
        //当前日期
        public DateTime SelectedDate
        {
            set { this.TextBox.Text = value.ToString().Split(' ')[0]; }
            get
            {
                try
                {
                    DateTime selected = DateTime.Parse(TextBox.Text);
                    return selected;
                }
                catch { return new DateTime(0001,1,1); }
            }
        }

        public int Year
        {
            set { year = value; }
        }

        public void Clear()
        {
            TextBox.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}