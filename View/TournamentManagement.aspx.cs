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
        private UserController _userController;
        public Controller Controller { get => _controller; set => _controller = value; }
        public UserController UserController { get => _userController; set => _userController = value; }

        public List<Team> Members
        {
            get
            {
                if (ViewState["MembersTournament"] != null)
                    return (List<Team>)ViewState["MembersTournament"];
                return null;
            }
            set => ViewState["MembersTournament"] = value;
        }

        public List<Team> Candidates
        {
            get
            {
                if (ViewState["CandidatesTournament"] != null)
                    return (List<Team>)ViewState["CandidatesTournament"];
                return null;
            }
            set => ViewState["CandidatesTournament"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            UserController = Global.UserController;

            if (!IsPostBack)
            {
                Controller.GetAllTournaments();
                tblTournament.DataSource = Controller.Tournaments;
                tblTournament.DataBind();
                MembersFront.DataBind();
                CandidatesFront.DataBind();
                addNewT.DataBind();
            }
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (CandidatesFront.SelectedIndex != -1)
            {
                Team mmbrNew = null;
                mmbrNew = Candidates.Find(x => x.Name == CandidatesFront.SelectedValue);
                if (mmbrNew != null)
                {
                    Candidates.Remove(mmbrNew);
                    Members.Add(mmbrNew);
                }
            }
            CandidatesFront.DataBind();
            MembersFront.DataBind();
        }

        protected void tblTournament_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tblTournament.EditIndex = -1;
            tblTournament.PageIndex = e.NewPageIndex;
            tblTournament.DataSource = Controller.Tournaments;
            Controller.ActiveParticipant = -1;
            tblTournament.DataBind();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            if (MembersFront.SelectedIndex != -1)
            {
                Team mmbr = Members.Find(x => x.Name == MembersFront.SelectedValue);
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

        public string typeConverter(object type_id)
        {
            return Controller.TypeList[Convert.ToInt32(type_id)];
        }

        protected void SaveNewT_Command(object sender, CommandEventArgs e)
        {
            Tournament newTournament = new Tournament();
            newTournament.Type = Convert.ToInt32(addNewT.SelectedValue);
            newTournament.Teams = Members;
            newTournament.Name = nameT.Text;
            newTournament.Put();
            Controller.ActiveParticipant = newTournament.Id;
            Response.Redirect(Request.RawUrl);
        }

        protected void gotoGames_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("~/View/GameManagement?TournamentId={0}", tblTournament.DataKeys[((sender as Button).NamingContainer as GridViewRow).RowIndex]));
        }

        protected void tblTournament_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = tblTournament.Rows[e.RowIndex];
            Tournament newT = new Tournament();
            newT.Get((int)e.Keys["id"]);
            DropDownList cur = (DropDownList)row.FindControl("tournamentTypes");
            newT.Type = Convert.ToInt32(cur.SelectedValue);
            newT.Teams = Members;
            newT.Name = ((TextBox)(row.Cells[1].Controls[0])).Text;
            newT.Update();
            newT = null;
            Controller.GetAllTournaments();
            tblTournament.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void tblTournament_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tblTournament.EditIndex = -1;
            tblTournament.DataSource = Controller.Tournaments;
            Controller.ActiveParticipant = -1;
            tblTournament.DataBind();
            addNewTournament.Visible = true;
            editMembersTournaments.Visible = false;
        }

        protected void tblTournament_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = tblTournament.Rows[e.RowIndex];
            Tournament temp = new Tournament();
            temp = Controller.Tournaments.First(team => team.Id == (int)e.Keys["id"]);
            temp.Delete();
            tblTournament.EditIndex = -1;
            tblTournament.DataBind();
            Response.Redirect(Request.RawUrl);
            editMembersTournaments.Visible = true;
            addNewTournament.Visible = false;
        }

        protected void tblTournament_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tblTournament.EditIndex = e.NewEditIndex;
            tblTournament.DataSource = Controller.Tournaments;
            tblTournament.DataBind();
            int activeId = (int)tblTournament.DataKeys[e.NewEditIndex].Value;
            Controller.ActiveParticipant = activeId;
            Members = Controller.Tournaments.First(x => x.Id == activeId).Teams;
            Candidates = Controller.GetAllTournamentCandidates(activeId);
            MembersFront.DataBind();
            CandidatesFront.DataBind();
            editMembersTournaments.Visible = true;
            addNewTournament.Visible = false;
        }

        protected void Export_CommandXML(object sender, CommandEventArgs e)
        {
            string xml = Controller.SerializeFromGrid(Controller.Tournaments, 1);
            string fileName = "xmlDump.xml";
            HttpResponse response = HttpContext.Current.Response;
            response.StatusCode = 200;
            response.ContentType = "application-download";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.Write(xml);
            response.Flush();
            response.End();
        }

        protected void Export_CommandJSON(object sender, CommandEventArgs e)
        {
            string json = Controller.SerializeFromGrid(Controller.Tournaments, 2);
            string fileName = "jsonDump.json";
            HttpResponse response = HttpContext.Current.Response;
            response.StatusCode = 200;
            response.ContentType = "application-download";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.Write(json);
            response.Flush();
            response.End();
        }
    }
}