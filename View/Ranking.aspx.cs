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

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            Controller.GetAllTournaments();
            if (!IsPostBack)
            {
                ddlTour.DataBind();
            }

            //if (ActiveTournament != Convert.ToInt32(ddlTour.SelectedValue) && ddlTour.SelectedValue != "")
            //{
            //    ActiveTournament = Convert.ToInt32(ddlTour.SelectedValue);
            //}
            //tourButton.Command += TourButton_Command;
            //tourButton.CommandArgument = ddlTour.SelectedValue;
        }

        #endregion Methods

        protected void tourButton_Command1(object sender, CommandEventArgs e)
        {
            tblRanking.DataSource = Controller.GetRanking(Convert.ToInt32(ddlTour.SelectedValue));
            tblRanking.DataBind();
            //  Response.Redirect(Request.RawUrl);
        }
    }
}