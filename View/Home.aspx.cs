using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tournament_Management
{
    public partial class Home : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("~/ULogin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}