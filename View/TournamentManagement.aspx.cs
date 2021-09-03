﻿using System;
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

        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("~/ULogin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            generateShowBtn();
            LoadInput();
            if (Controller.ActiveParticipant != -1) LoadTournaments();
            typeList.SelectedValue = Controller.ActiveParticipant.ToString();
        }

        /* not a part of evaluation sheet!
                public List<Team> Participants
                {
                    get
                    {
                        if (ViewState["Participants"] != null)
                            return (List<Team>)ViewState["Participants"];
                        return null;
                    }
                    set => ViewState["Participants"] = value;
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
        */

        private void LoadInput()
        {
            Label lbl = new Label();
            lbl.Text = "Name";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtName";
            txt.Text = "";
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Discipline";
            form1.Controls.Add(lbl);

            DropDownList dropdown = new DropDownList();
            dropdown.ID = "txtType";
            dropdown.CssClass = "form-control";
            dropdown.DataSource = Controller.TypeList;
            dropdown.DataTextField = "Value";
            dropdown.DataValueField = "Key";
            dropdown.DataBind();
            form1.Controls.Add(dropdown);

            lbl = new Label();
            lbl.Text = "Active";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtActive";
            txt.Text = "";
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        private void LoadTournaments()
        {
            tblTournaments.Rows.Clear();

            //header rows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerType";
            newHeaderCell.Text = "Discipline";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerEdit";
            newHeaderCell.Text = "Edit";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerDel";
            newHeaderCell.Text = "Delete";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerGames";
            newHeaderCell.Text = "Games";
            thr.Cells.Add(newHeaderCell);

            tblTournaments.Rows.Add(thr);

            //Data
            foreach (Tournament t in Controller.Tournaments)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tournamentTableRow" + t.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + t.Id;
                newCell.Text = t.Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + t.Id;
                newCell.Text = t.Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + t.Id;
                newCell.Text = Controller.TypeList[t.Type];
                newRow.Cells.Add(newCell);

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

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + t.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + t.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = t.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDetailsButton" + t.Id;
                Button detailsButton = new Button();
                detailsButton.ID = "detButton_" + t.Id;
                detailsButton.CssClass = "btn btn-secondary";
                detailsButton.CommandName = "Details";
                detailsButton.Text = "Show Games";
                detailsButton.CommandName = currentRowIndexTemp;
                detailsButton.CommandArgument = t.Id.ToString();
                detailsButton.Command += this.btnOverview_Click;
                newCell.Controls.Add(detailsButton);
                newRow.Cells.Add(newCell);

                tblTournaments.Rows.Add(newRow);

                //add edit row

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
                saveButton.Command += this.btnSave_Click;
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
            }
        }

        protected void btnCancel_Click(object sender, CommandEventArgs e)
        {
            this.hideInputs();
        }

        private void hideInputs()
        {
            foreach (TableRow tr in tblTournaments.Rows)
            {
                if (tr.Style.Count > 0)
                {
                    tr.Style.Clear();
                    tr.Style.Add("visibility", "collapse");
                }
            }
        }

        protected void generateShowBtn()
        {
            Button save = new Button();
            save.ID = "btnConfirm";
            save.Command += btnShow_Click;
            ListControl ctrl = typeList;
            save.CommandArgument = ctrl.SelectedValue;
            save.Text = "Show me the Tournaments!";
            save.CssClass = "btn btn-secondary";
            btnShow.Controls.Add(save);
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
            this.hideInputs();

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

        protected void btnToggleInputs_Click(object sender, CommandEventArgs e)
        {
            //   Participants = Controller.Tournaments.First(x => x.Id == Convert.ToInt32(e.CommandName)).GetAllTeams();
            //  Candidates = Controller.GetAllCandidates(Convert.ToInt32(e.CommandName));

            //update style to visibility: visible on row for index!
            tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Style.Clear();
            tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Style.Add("visibility", "visible");
            //    tblTournaments.Rows[Convert.ToInt32(e.CommandArgument) + 1].Style.Clear();
            //    tblTournaments.Rows[Convert.ToInt32(e.CommandArgument) + 1].Style.Add("visibility", "visible");
        }

        /*
                protected void btnParticipant_Click(object sender, CommandEventArgs e)
                {
                    string value = Request.Form[$"ctl00$TeamManagement$edittxtParticipants{e.CommandArgument}"];
                    Team mmbr = Participants.Find(x => x.Name.Contains(value));
                    if (mmbr != null)
                    {
                        // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
                        Participants.Remove(mmbr);
                        Candidates.Add(mmbr);
                    }
                    else
                    {
                    }
                    (tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtParticipants{ e.CommandArgument}") as ListBox).DataBind();
                    (tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();

                    //    Response.Redirect(Request.RawUrl);
                }

                protected void btnCandidate_Click(object sender, CommandEventArgs e)
                {
                    string value = Request.Form[$"ctl00$TeamManagement$edittxtCandidates{e.CommandArgument}"];
                    Team mmbrNew = null;
                    if (value != null) mmbrNew = Candidates.Find(x => x.Name.Contains(value));
                    if (mmbrNew != null)
                    {
                        Candidates.Remove(mmbrNew);
                        Participants.Add(mmbrNew);
                        //(Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Add(mmbrNew);
                    }
                    else
                    {
                    }
                    //    Response.Redirect(Request.RawUrl);
                    (tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtParticipants{ e.CommandArgument}") as ListBox).DataBind();
                    (tblTournaments.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();
                }
        */

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
    }
}