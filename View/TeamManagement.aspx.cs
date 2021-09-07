using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tournament_Management.ControllerNS;
using Tournament_Management.Model;
using Tournament_Management.Helper;

namespace Tournament_Management.View
{
    public partial class TeamManagement : System.Web.UI.Page
    {
        private Controller _controller;

        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            Controller.GetAllTeams();

            if (!IsPostBack)
            {
                teamGrid.DataSource = Controller.Teams;
                teamGrid.DataBind();
                MembersFront.DataBind();
                CandidatesFront.DataBind();
                addNewT.DataBind();
            }
        }

        public List<Person> Members
        {
            get
            {
                if (ViewState["Members"] != null)
                    return (List<Person>)ViewState["Members"];
                return null;
            }
            set => ViewState["Members"] = value;
        }

        public List<Person> Candidates
        {
            get
            {
                if (ViewState["Candidates"] != null)
                    return (List<Person>)ViewState["Candidates"];
                return null;
            }
            set => ViewState["Candidates"] = value;
        }

        protected void teamGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            teamGrid.EditIndex = e.NewEditIndex;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
            int activeId = (int)teamGrid.DataKeys[e.NewEditIndex].Value;
            Members = Controller.Teams.First(x => x.Id == activeId).List;
            Candidates = Controller.GetAllCandidates(activeId);
            MembersFront.DataBind();
            CandidatesFront.DataBind();
            editMembersTeam.Visible = true;
        }

        protected void teamGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = teamGrid.Rows[e.RowIndex];
            Team temp = new Team();
            temp = Controller.Teams.First(team => team.Id == (int)e.Keys["id"]);
            temp.Delete();
            teamGrid.EditIndex = -1;
            teamGrid.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void teamGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            teamGrid.EditIndex = -1;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
        }

        protected void teamGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        public string typeConverter(object type_id)
        {
            return Controller.TypeList[Convert.ToInt32(type_id)];
        }

        protected void teamGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            teamGrid.PageIndex = e.NewPageIndex;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (CandidatesFront.SelectedIndex != -1)
            {
                Person mmbrNew = null;
                mmbrNew = Candidates.Find(x => (x.Name + " " + x.Surname).Contains(MembersFront.SelectedValue));
                if (mmbrNew != null)
                {
                    Candidates.Remove(mmbrNew);
                    Members.Add(mmbrNew);
                }
            }
            CandidatesFront.DataBind();
            MembersFront.DataBind();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            if (MembersFront.SelectedIndex != -1)
            {
                Person mmbr = Members.Find(x => (x.Name + " " + x.Surname).Contains(MembersFront.SelectedValue));
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

        protected void btnAdd_Submit(object sender, CommandEventArgs e)
        {
            Team newTeam = new Team();
            newTeam.Type = Convert.ToInt32(addNewT.SelectedValue);
            newTeam.List = Members;
            newTeam.Name = (nameT.Text !="" ? nameT.Text : newTeam.Name);
            newTeam.Update();
            Response.Redirect(Request.RawUrl);
        }

        protected void SaveNewI_Command(object sender, CommandEventArgs e)
        {
            Team newTeam = new Team();
            newTeam.Type = Convert.ToInt32(addNewT.SelectedValue);
            newTeam.List = Members;
            newTeam.Name = nameT.Text;
            newTeam.Put();
            Controller.ActiveParticipant = newTeam.Id;
            Response.Redirect(Request.RawUrl);
        }
    }
}