using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tournament_Management.ControllerNS;

namespace Tournament_Management.View
{
    public partial class Ranking : System.Web.UI.Page
    {
        #region Attributes

        private Controller _controller;
        private int _activeTournament;

        #endregion Attributes

        #region Properties

        public Controller Controller { get => _controller; set => _controller = value; }
        public int ActiveTournament { get => _activeTournament; set => _activeTournament = value; }

        #endregion Properties

        #region Methods

        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("../ULogin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            Controller.GetAllTournaments();
            if (!IsPostBack) Initialize();
            if (ActiveTournament != Convert.ToInt32(ddlTour.SelectedValue) && ddlTour.SelectedValue != "")
            {
                ActiveTournament = Convert.ToInt32(ddlTour.SelectedValue);
            }
            tourButton.Command += TourButton_Command;
            tourButton.CommandArgument = ddlTour.SelectedValue;
        }

        private void Initialize()
        {
            ddlTour.DataSource = Controller.Tournaments;
            ddlTour.DataTextField = "Name";
            ddlTour.DataValueField = "Id";
            ddlTour.DataBind();
            tblRanking.DataSource = Controller.GetRanking(1);
            tblRanking.DataBind();
        }

        private void TourButton_Command(object sender, CommandEventArgs e)
        {
            // TODO: this doesn't work as intended!

            tblRanking.DataSource = Controller.GetRanking(Convert.ToInt32(e.CommandArgument));
            tblRanking.DataBind();

            Response.Redirect(Request.RawUrl);
        }

        #endregion Methods
    }
}