using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tournament_Management.View
{
    public partial class tournament : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAdd_Click(object sender, CommandEventArgs e)
        {
            //btnSubmit.Visible = true;
        }

        protected void btnSubmit_Click(object sender, CommandEventArgs e)
        {
            /*
           FootballPlayer tmp = Controller.NewParticipant as FootballPlayer;
           this.setBaseNew(tmp);
           tmp.Goals = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtGoals"]);
           tmp.Speed = Convert.ToDouble(Request.Form["ctl00$PersonalManagement$txtSpeed"]);
           tmp.Put(); break;
            btnSubmit.Visible = false;
            */
        }
    }
}