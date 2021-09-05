using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tournament_Management
{
    public partial class BaseMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("~/ULogin.aspx");
            }
            if (Global.UserController.User.Role == 3)
            {
                form1.FindControl("usrsBtn").Visible = true;
            }
            if (Global.UserController.User.Role != 1)
            {
                form1.FindControl("usrBtn").Visible = true;
            }

            logoutBtn.Command += OnLogout;
        }

        private void OnLogout(object sender, CommandEventArgs e)
        {
            Global.UserController.logout();
            Response.Redirect("~/ULogin.aspx");
        }
    }
}