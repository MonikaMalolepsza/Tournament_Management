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
            LoadTeams();
        }

        private void LoadInputTeam()
        {

            Label lbl = new Label();
            lbl.Text = "Name";
            editPerson.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtName";
            txt.Text = "";
            txt.CssClass = "form-control";
            editPerson.Controls.Add(txt);

            DropDownList dropdown = new DropDownList();
            dropdown.ID = $"txtType";
            dropdown.CssClass = "form-control";
            dropdown.DataSource = Controller.TypeList;
            dropdown.DataTextField = "Value";
            dropdown.DataValueField = "Key";
            dropdown.DataBind();
            editPerson.Controls.Add(dropdown);

        }

        private void LoadTeams()
        {
            tblTeam.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
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


            tblTeam.Rows.Add(thr);

            //Data
            foreach (Participant team in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + team.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + team.Id;
                newCell.Text = (team as Team).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + team.Id;
                newCell.Text = Controller.TypeList[(team as Team).Type];
                newRow.Cells.Add(newCell);


                string currentRowIndexTemp = (tblTeam.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + team.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + team.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = team.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + team.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + team.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = team.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblTeam.Rows.Add(newRow);

                //add edit row 

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{team.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{team.Id}";
                txt.Text = "";
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{team.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                DropDownList dropdown = new DropDownList();
                dropdown.ID = $"edittxtType{team.Id}";
                dropdown.CssClass = "form-control";
                dropdown.DataSource = Controller.TypeList;
                dropdown.DataTextField = "Value";
                dropdown.DataValueField = "Key";
                dropdown.DataBind();
                newCell.Controls.Add(dropdown);
                newCell.ID = $"editType{team.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{team.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{team.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = team.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);


                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{team.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{team.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = team.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblTeam.Rows.Add(row);
            }
        }
        protected void btnToggleInputs_Click(object sender, CommandEventArgs e)
        {
            //update style to visibility: visible on row for index!
            tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Style.Clear();
            tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Style.Add("visibility", "visible");

        }
        protected void btnDeleteEdit_Click(object sender, CommandEventArgs e)
        {

            Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)).Delete();

            Response.Redirect(Request.RawUrl);

        }
        protected void btnEdit_Click(object sender, CommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            Team tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Team;
            tmp.Name = Request.Form[$"ctl00$PersonalManagement$edittxtName{index}"];
            tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtType{index}"]);
            tmp.Update();

        }
        private void hideInputs()
        {
            foreach (TableRow tr in tblTeam.Rows)
            {
                if (tr.Style.Count > 0)
                {
                    tr.Style.Clear();
                    tr.Style.Add("visibility", "collapse");
                }
            }
        }
        protected void btnCancel_Click(object sender, CommandEventArgs e)
        {
            this.hideInputs();
        }
    }
}