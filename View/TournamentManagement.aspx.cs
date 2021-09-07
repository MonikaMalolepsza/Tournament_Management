using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tournament_Management.ControllerNS;
using Tournament_Management.Helper;
using Tournament_Management.Model;

namespace Tournament_Management.View
{
    public partial class TournamentManagement : System.Web.UI.Page
    {
        private Controller _controller;

        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            Controller.GetAllTournaments();
            if (!IsPostBack)
            {
                tblTournament.DataBind();
                ddlTour2.DataBind();
            }
        }

        protected void btnShow_Click(object sender, CommandEventArgs e)
        {
            Controller.ActiveParticipant = Convert.ToInt32(e.CommandArgument);
            if (Convert.ToInt32(e.CommandArgument) == 0) Controller.GetAllTournaments();
            else Controller.GetAllTournamentsForType(Convert.ToInt32(e.CommandArgument));

            Response.Redirect(Request.RawUrl);
            //     LoadTournaments();
        }

        protected void btnSave_Click(object sender, CommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            Tournament tmp = Controller.Tournaments.First(x => x.Id == Convert.ToInt32(e.CommandArgument));
            tmp.Name = Request.Form[$"ctl00$TournamentManagement$edittxtName{index}"];
            tmp.Active = Request.Form[$"ctl00$TournamentManagement$edittxtActive{index}"].GetTrueFalseString(); ;
            tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$TournamentManagement$edittxtType{index}"]);
            tmp.Update();

            Response.Redirect(Request.RawUrl);
        }

        protected void btnOverview_Click(object sender, CommandEventArgs e)
        {
            Controller.ActiveParticipant = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/View/GameManagement");
        }

        protected void btnDeleteEdit_Click(object sender, CommandEventArgs e)
        {
            Controller.Tournaments.First(x => x.Id == Convert.ToInt32(e.CommandArgument)).Delete();
            Controller.GetAllTournaments();
        }

        public string typeConverter(object type_id)
        {
            return Controller.TypeList[Convert.ToInt32(type_id)];
        }
        protected void btnSubmit_Click(object sender, CommandEventArgs e)
        {
            Tournament tmp = new Tournament();
            string index = e.CommandArgument.ToString();
            tmp.Name = Request.Form[$"ctl00$TournamentManagement$txtName{index}"];
            tmp.Active = Request.Form[$"ctl00$TournamentManagement$txtActive{index}"].GetTrueFalseString();
            tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$TournamentManagement$txtType{index}"]);
            tmp.Put();
            Controller.Tournaments.Add(tmp);
        }

        protected void tourButton_Command(object sender, CommandEventArgs e)
        {
            tblTournament.DataSource = Controller.Tournaments.FindAll(x => x.Type == Convert.ToInt32(ddlTour2.SelectedValue));
            tblTournament.DataBind();
        }
    }
}