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

        public List<Team> Members
        {
            get
            {
                if (ViewState["Members"] != null)
                    return (List<Team>)ViewState["Members"];
                return null;
            }
            set => ViewState["Members"] = value;
        }

        public List<Team> Candidates
        {
            get
            {
                if (ViewState["Candidates"] != null)
                    return (List<Team>)ViewState["Candidates"];
                return null;
            }
            set => ViewState["Candidates"] = value;
        }

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

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (CandidatesFront.SelectedIndex != -1)
            {
                Team mmbrNew = null;
                mmbrNew = Candidates.Find(x => (x.Name).Contains(MembersFront.SelectedValue));
                if (mmbrNew != null)
                {
                    Candidates.Remove(mmbrNew);
                    Members.Add(mmbrNew);
                }
            }
            CandidatesFront.DataBind();
            MembersFront.DataBind();
        }

        protected void teamGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tblTournament.PageIndex = e.NewPageIndex;
            tblTournament.DataSource = Controller.Teams;
            tblTournament.DataBind();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            if (MembersFront.SelectedIndex != -1)
            {
                Team mmbr = Members.Find(x => (x.Name).Contains(MembersFront.SelectedValue));
                if (mmbr != null)
                {
                    // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
                    Members.Remove(mmbr);
                    Candidates.Add(mmbr);
                }
            }
            CandidatesFront.DataBind();
            MembersFront.DataBind();
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

        protected void btnAdd_Submit(object sender, CommandEventArgs e)
        {
            Tournament newTeam = new Tournament();
            newTeam.Get(Controller.ActiveParticipant);
            newTeam.Type = Convert.ToInt32(addNewT.SelectedValue);
            newTeam.Teams = Members;
            newTeam.Name = (nameT.Text != "" ? nameT.Text : newTeam.Name);
            newTeam.Update();
            Response.Redirect(Request.RawUrl);
        }

        protected void SaveNewI_Command(object sender, CommandEventArgs e)
        {
            Tournament newTeam = new Tournament();
            newTeam.Type = Convert.ToInt32(addNewT.SelectedValue);
            newTeam.Teams = Members;
            newTeam.Name = nameT.Text;
            newTeam.Put();
            Controller.ActiveParticipant = newTeam.Id;
            Response.Redirect(Request.RawUrl);
        }

        protected void tourButton_Command(object sender, CommandEventArgs e)
        {
            tblTournament.DataSource = Controller.Tournaments.FindAll(x => x.Type == Convert.ToInt32(ddlTour2.SelectedValue));
            tblTournament.DataBind();
        }
    }
}