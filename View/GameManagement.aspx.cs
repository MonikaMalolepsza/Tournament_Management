using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tournament_Management.ControllerNS;
using Tournament_Management.Model;

namespace Tournament_Management.View
{
    public partial class GameManagement : System.Web.UI.Page
    {
        private Controller _controller;

        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["TournamentId"];
            }
            Controller = Global.Controller;
            Controller.GetAllTournaments();
        }
    }
}