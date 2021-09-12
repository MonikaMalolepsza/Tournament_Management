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

        public int Tournament_id
        {
            get
            {
                if (ViewState["TournamentId"] != null)
                    return (int)ViewState["TournamentId"];
                return -1;
            }
            set => ViewState["TournamentId"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            Controller.GetAllTournaments();
            Controller.GetAllTeams();

            if (!IsPostBack)
            {
                if (Request.QueryString["TournamentId"] != null)
                {
                    Tournament_id = Convert.ToInt32(Request.QueryString["TournamentId"]);
                    tblGames.DataSource = Controller.Tournaments.First(t => t.Id == Tournament_id).Games;
                    ddlTour.SelectedValue = Tournament_id.ToString();
                }
                else
                {
                    Tournament_id = 1;
                }
            }
            ddlTour.DataBind();
            newGameTournDD.DataBind();
            addNewT1.DataBind();
            addNewT2.DataBind();
        }

        protected void tourButton_Command(object sender, CommandEventArgs e)
        {
            Tournament_id = Convert.ToInt32(ddlTour.SelectedValue);
            tblGames.DataSource = Controller.Tournaments.First(t => t.Id == Tournament_id).Games;
            tblGames.DataBind();
        }

        protected void tblGames_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = tblGames.Rows[e.RowIndex];
            Game temp = new Game();
            temp = Controller.Tournaments.First(t => t.Id == Tournament_id).Games.First(game => game.Id == (int)e.Keys["id"]);
            DropDownList team1 = (DropDownList)row.FindControl("ddlistT1");
            temp.Scores[0].Team = Convert.ToInt32(team1.SelectedValue);
            temp.Scores[0].Points = Convert.ToInt32(((TextBox)(row.Cells[3].Controls[1])).Text);
            DropDownList team2 = (DropDownList)row.FindControl("ddlistT2");
            temp.Scores[1].Team = Convert.ToInt32(team2.SelectedValue);
            temp.Scores[1].Points = Convert.ToInt32(((TextBox)(row.Cells[5].Controls[1])).Text);
            temp.Update();
            temp = null;
            Controller.GetAllTournaments();
            Response.Redirect(Request.RawUrl);
        }

        protected void tblGames_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tblGames.EditIndex = -1;
            tblGames.DataSource = Controller.Tournaments.First(t => t.Id == Tournament_id).Games;
            tblGames.DataBind();
        }

        protected void tblGames_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tblGames.EditIndex = e.NewEditIndex;
            tblGames.DataSource = Controller.Tournaments.First(t => t.Id == Tournament_id).Games;
            tblGames.DataBind();
        }

        protected void tblGames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = tblGames.Rows[e.RowIndex];
            Game temp = new Game();
            temp = Controller.Tournaments.First(t => t.Id == Tournament_id).Games.First(user => user.Id == (int)e.Keys["id"]);
            temp.Delete();
            tblGames.EditIndex = -1;
            tblGames.DataBind();
        }

        public string getTournamentsName(object tournament_id)
        {
            return Controller.Tournaments.First(t => t.Id == (int)tournament_id).Name;
        }

        public string getTeamsName(object team_id)
        {
            return Controller.Teams.First(t => t.Id == (int)team_id).Name;
        }

        protected void Export_CommandXML(object sender, CommandEventArgs e)
        {
            string xml = Controller.SerializeFromGrid(Controller.Tournaments.First(t => t.Id == Tournament_id).Games, 1);
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
            string json = Controller.SerializeFromGrid(Controller.Tournaments.First(t => t.Id == Tournament_id).Games, 2);
            string fileName = "jsonDump.json";
            HttpResponse response = HttpContext.Current.Response;
            response.StatusCode = 200;
            response.ContentType = "application-download";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.Write(json);
            response.Flush();
            response.End();
        }

        protected void btnAdd_SubmitUser(object sender, CommandEventArgs e)
        {
            Game newUser = new Game();
            newUser.TournamentId = Convert.ToInt32(newGameTournDD.SelectedValue);
            newUser.Scores.Add(new Score(Convert.ToInt32(addNewT1.Text), Convert.ToInt32(Score1.Text)));
            newUser.Scores.Add(new Score(Convert.ToInt32(addNewT2.Text), Convert.ToInt32(Score2.Text)));
            newUser.Put();
            Response.Redirect(Request.RawUrl);
        }
    }
}