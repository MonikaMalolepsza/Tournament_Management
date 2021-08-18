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
            Controller = Global.Controller;
            Controller.GetAllTournaments();
            if (!IsPostBack) initialize_Dropdown();
            btnShow.CommandArgument = tournamentList.SelectedValue;
            btnShow.Command += btn_show_games;
            Controller.GetAllTeams();
            if (Controller.ActiveParticipant != -1) Show_Games(Controller.ActiveParticipant);
            //if (tournamentList.SelectedValue != null)
            //{
            //    Controller.ActiveParticipant = Convert.ToInt32(tournamentList.SelectedValue);
            //}

            //  Show_Games(Controller.ActiveParticipant);
            // transfer the tournament id on click with redirect and set it as active if not null.
        }

        protected void initialize_Dropdown()
        {
            tournamentList.DataSource = Controller.Tournaments;
            tournamentList.DataTextField = "Name";
            tournamentList.DataValueField = "Id";
            tournamentList.DataBind();

            // btnShow.Text = "Show Games";
            // btnShow.CssClass = "btn btn-info my-auto";
            // btnShow.CommandArgument = tournamentList.SelectedValue;
            // btnShow.Command += btn_show_games;
        }

        protected void btn_show_games(object sender, CommandEventArgs e)
        {
            Show_Games(Convert.ToInt32(e.CommandArgument));
        }

        protected void Show_Games(int tournamentId)
        {
            tblGames.Visible = true;
            tblGames.Rows.Clear();

            Tournament cTournament = Controller.Tournaments.First(x => x.Id == tournamentId);
            Controller.ActiveParticipant = tournamentId;

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thrG";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerT1";
            newHeaderCell.Text = "Team 1";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerS1";
            newHeaderCell.Text = "Score Team 1";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerT2";
            newHeaderCell.Text = "Team 2";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerS2";
            newHeaderCell.Text = "Score Team 2";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "winner";
            newHeaderCell.Text = "Winner";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerEdit";
            newHeaderCell.Text = "Edit";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerGDel";
            newHeaderCell.Text = "Delete";
            thr.Cells.Add(newHeaderCell);

            tblGames.Rows.Add(thr);

            //Data
            Team t1 = new Team();
            Team t2 = new Team();

            foreach (Game g in cTournament.Games)
            {
                // TODO: ArgumentOutOfRangeException with tournamentid = 10, 9  second game is with only 0 for every attribute initialized!??
                t1 = Controller.Participants.FirstOrDefault(y => y.Id == g.Scores[0].Team) as Team;
                t2 = Controller.Participants.FirstOrDefault(y => y.Id == g.Scores[1].Team) as Team;

                TableRow newRow = new TableRow();
                newRow.ID = "tableRowG" + g.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellT1G" + g.Id;
                newCell.Text = t1.Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellS1G" + g.Id;
                newCell.Text = g.Scores[0].Points.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellT2G" + g.Id;
                newCell.Text = t2.Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellS2G" + g.Id;
                newCell.Text = g.Scores[1].Points.ToString();
                newRow.Cells.Add(newCell);

                string txtW;
                int winner = findWinner(g.Scores[0], g.Scores[1]);
                if (winner == -1) txtW = "draw";
                else txtW = Controller.Participants.FirstOrDefault(elem => elem.Id == winner).Name;
                newCell = new TableCell();
                newCell.ID = "cellWin" + g.Id;
                newCell.Text = txtW;
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblGames.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + g.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + g.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = g.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + g.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + g.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = g.Id.ToString();
                delButton.Command += this.btnDelete_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblGames.Rows.Add(newRow);

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{g.Id}";

                newCell = new TableCell();
                DropDownList ddlist = new DropDownList();
                ddlist.ID = $"ddlistT1G{g.Id}";
                ddlist.DataSource = Controller.Teams;
                ddlist.SelectedValue = g.Scores[0].Team.ToString();
                ddlist.DataValueField = "Id";
                ddlist.DataTextField = "Name";
                ddlist.DataBind();
                ddlist.CssClass = "form-control";
                newCell.Controls.Add(ddlist);
                newCell.ID = $"cellT1Gedit{g.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"txtS1Gedit{g.Id}";
                txt.Text = g.Scores[0].Points.ToString();
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"cellS1Gedit{g.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                ddlist = new DropDownList();
                ddlist.ID = $"ddlistT2G{g.Id}";
                ddlist.DataSource = Controller.Teams;
                ddlist.SelectedValue = g.Scores[1].Team.ToString();
                ddlist.DataValueField = "Id";
                ddlist.DataTextField = "Name";
                ddlist.DataBind();
                ddlist.CssClass = "form-control";
                newCell.Controls.Add(ddlist);
                newCell.ID = $"cellT2Gedit{g.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"txtS2Gedit{g.Id}";
                txt.Text = g.Scores[1].Points.ToString();
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"cellS2Gedit{g.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{g.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{g.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = g.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{g.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{g.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = g.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblGames.Rows.Add(row);
                //t1 = new Team();
                //t2 = new Team();
            }
        }

        private void btnCancel_Click(object sender, CommandEventArgs e)
        {
            this.hideInputs();
        }

        private void hideInputs()
        {
            foreach (TableRow tr in tblGames.Rows)
            {
                if (tr.Style.Count > 0)
                {
                    tr.Style.Clear();
                    tr.Style.Add("visibility", "collapse");
                }
            }
        }

        private void btnEdit_Click(object sender, CommandEventArgs e)
        {
            //update the game in a tournament ??
            string index = e.CommandArgument.ToString();
            Game tmp = Controller.Tournaments.First(x => x.Id == Controller.ActiveParticipant).Games.First(item => item.Id == Convert.ToInt32(e.CommandArgument));
            tmp.Scores[0].Points = Convert.ToInt32(Request.Form[$"ctl00$Games$txtS1Gedit{index}"]);
            tmp.Scores[0].Team = Convert.ToInt32(Request.Form[$"ctl00$Games$ddlistT1G{index}"]);
            tmp.Scores[1].Team = Convert.ToInt32(Request.Form[$"ctl00$Games$ddlistT2G{index}"]);
            tmp.Scores[1].Points = Convert.ToInt32(Request.Form[$"ctl00$Games$txtS2Gedit{index}"]);
            tmp.TournamentId = Controller.ActiveParticipant;
            tmp.Update();
            this.hideInputs();

            Response.Redirect(Request.RawUrl);
        }

        private int findWinner(Score s1, Score s2)
        {
            int result;

            result = s1.Points > s2.Points ? s1.Team : s2.Team;
            if (s1.Points == s2.Points) result = -1;
            return result;
        }

        protected void btnToggleInputs_Click(object sender, CommandEventArgs e)
        {
            //update style to visibility: visible on row for index!
            tblGames.Rows[Convert.ToInt32(e.CommandArgument)].Style.Clear();
            tblGames.Rows[Convert.ToInt32(e.CommandArgument)].Style.Add("visibility", "visible");
        }

        private void btnDelete_Click(object sender, CommandEventArgs e)
        {
            Controller.Tournaments.First(x => x.Id == Controller.ActiveParticipant).Games.First(g => g.Id == Convert.ToInt32(e.CommandArgument)).Delete();
            Controller.GetAllTournaments();

            Response.Redirect(Request.RawUrl);
        }
    }
}