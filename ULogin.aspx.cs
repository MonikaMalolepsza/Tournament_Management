using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tournament_Management.ControllerNS;

namespace Tournament_Management
{
    public partial class ULogin : System.Web.UI.Page
    {
        private Controller _controller;

        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
        }

        protected void OnAuth(object sender, AuthenticateEventArgs e)
        {
            Controller.Authenticate((sender as Login).UserName, (sender as Login).Password);
            e.Authenticated = true;
        }
    }
}