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
            List<Game> games = cTournament.Games;

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
                t1 = Controller.Participants.First(y => y.Id == g.Scores[0].Team) as Team;
                t2 = Controller.Participants.First(y => y.Id == g.Scores[1].Team) as Team;

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
                newCell.ID = "cellS1G" + g.Id;
                newCell.Text = g.Scores[1].Points.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellWin" + g.Id;
                newCell.Text = Controller.Participants.First(elem => elem.Id == findWinner(g.Scores[0], g.Scores[1])).Name ?? "draw";
                newRow.Cells.Add(newCell);
                /*
                                string currentRowIndexTemp = (tblTournaments.Rows.Count + 1).ToString();
                                newCell = new TableCell();
                                newCell.ID = "cellEditButton" + t.Id;
                                Button editButton = new Button();
                                editButton.ID = "editButton_" + t.Id;
                                editButton.Text = "Edit";
                                editButton.CssClass = "btn btn-warning";
                                editButton.CommandArgument = currentRowIndexTemp;
                                editButton.CommandName = t.Id.ToString();
                                editButton.Command += this.btnToggleInputs_Click;
                                newCell.Controls.Add(editButton);
                                newRow.Cells.Add(newCell);
                */
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

                //TODO add edit row
                /*
                                TableRow row = new TableRow();
                                row.Style.Add("visibility", "collapse");
                                row.ID = $"editRow{t.Id}";

                                newCell = new TableCell();
                                TextBox txt = new TextBox();
                                txt.ID = $"edittxtName{t.Id}";
                                txt.Text = t.Name;
                                txt.CssClass = "form-control";
                                newCell.Controls.Add(txt);
                                newCell.ID = $"editName{t.Id}";
                                row.Cells.Add(newCell);

                                newCell = new TableCell();
                                txt = new TextBox();
                                txt.ID = $"edittxtActive{t.Id}";
                                txt.Text = t.Active.GetYesNoString();
                                txt.CssClass = "form-control";
                                form1.Controls.Add(txt);
                                form1.Controls.Add(txt);
                                newCell.Controls.Add(txt);
                                newCell.ID = $"editActive{t.Id}";
                                row.Cells.Add(newCell);

                                newCell = new TableCell();
                                DropDownList dropdown = new DropDownList();
                                dropdown.ID = $"edittxtType{t.Id}";
                                dropdown.CssClass = "form-control";
                                dropdown.DataSource = Controller.TypeList;
                                dropdown.DataTextField = "Value";
                                dropdown.DataValueField = "Key";
                                dropdown.DataBind();
                                newCell.Controls.Add(dropdown);
                                newCell.ID = $"editType{t.Id}";
                                row.Cells.Add(newCell);

                                newCell = new TableCell();
                                newCell.ID = $"cellSaveButton{t.Id}";
                                Button saveButton = new Button();
                                saveButton.ID = $"saveButton{t.Id}";
                                saveButton.CommandName = "Edit";
                                saveButton.Text = "Save";
                                saveButton.CssClass = "btn btn-success";
                                saveButton.CommandArgument = t.Id.ToString();
                                saveButton.Command += this.btnEdit_Click;
                                newCell.Controls.Add(saveButton);
                                row.Cells.Add(newCell);

                                newCell = new TableCell();
                                newCell.ID = $"cellCancelButton{t.Id}";
                                Button cancelButton = new Button();
                                cancelButton.ID = $"cancelButton{t.Id}";
                                cancelButton.Text = "Cancel";
                                cancelButton.CssClass = "btn btn-secondary";
                                cancelButton.CommandArgument = t.Id.ToString();
                                cancelButton.Command += this.btnCancel_Click;
                                newCell.Controls.Add(cancelButton);
                                row.Cells.Add(newCell);

                                tblTournaments.Rows.Add(row);
                                */
                t1 = new Team();
                t2 = new Team();
            }
        }

        private int findWinner(Score s1, Score s2)
        {
            int result;

            result = s1.Points > s2.Points ? s1.Team : s2.Team;
            if (s1.Points == s2.Points) result = -1;
            return result;
        }

        private void btnDelete_Click(object sender, CommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}