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
            LoadInputTeam();
            LoadInputMembers();
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

        private void LoadInputTeam()
        {
            Label lbl = new Label();
            lbl.Text = "Name";
            addTeam.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtName";
            txt.Text = "";
            txt.CssClass = "form-control";
            addTeam.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Discipline";
            addTeam.Controls.Add(lbl);

            DropDownList dropdown = new DropDownList();
            dropdown.ID = $"txtType";
            dropdown.CssClass = "form-control";
            dropdown.DataSource = Controller.TypeList;
            dropdown.DataTextField = "Value";
            dropdown.DataValueField = "Key";
            dropdown.DataBind();
            addTeam.Controls.Add(dropdown);
        }

        private void LoadInputMembers()
        {
            TableRow row = new TableRow();
            row.ID = "addMembers";

            TableCell newCell = new TableCell();

            Label lbl = new Label();
            lbl.Text = "Members";
            newCell.Controls.Add(lbl);
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
                txt.Text = (team as Team).Name;
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

                row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editSwapRow{team.Id}";

                newCell = new TableCell();

                Label lbl = new Label();
                lbl.Text = "Members";
                newCell.Controls.Add(lbl);

                ListBox memberList = new ListBox();
                memberList.ID = $"edittxtMembers{team.Id}";
                memberList.DataSource = Members;
                memberList.DataBind();
                memberList.CssClass = "form-control";
                newCell.Controls.Add(memberList);
                newCell.ID = $"editMembers{team.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();

                newCell.ID = $"cellButton{team.Id}";
                ImageButton mButton = new ImageButton();
                mButton.ImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAWCAYAAAAfD8YZAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA+5pVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1wTU06T3JpZ2luYWxEb2N1bWVudElEPSJ1dWlkOjY1RTYzOTA2ODZDRjExREJBNkUyRDg4N0NFQUNCNDA3IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjA1OUQzQzAxMDZBODExRTI5OUZEQTZGODg4RDc1ODdCIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjA1OUQzQzAwMDZBODExRTI5OUZEQTZGODg4RDc1ODdCIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKE1hY2ludG9zaCkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDowMTgwMTE3NDA3MjA2ODExODA4M0ZFMkJBM0M1RUU2NSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDowNjgwMTE3NDA3MjA2ODExODA4M0U3NkRBMDNEMDVDMSIvPiA8ZGM6dGl0bGU+IDxyZGY6QWx0PiA8cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiPmdseXBoaWNvbnM8L3JkZjpsaT4gPC9yZGY6QWx0PiA8L2RjOnRpdGxlPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PjJSsWEAAAB3SURBVHjaYvj//z8DMRgIBIC4AEWMBI0XgBjEWUC0ZjSN/5ENIEcj3AByNf4Hy5GtEaSGXI0YfiZFI4pmUjXCNZOjEaqPPI0gzMRACaDI2RQHGMVRRXEioTh5kmIAE55Y+ACkHID4Ig4lF2hXGFBcDOErAAECDAApfSISSEStFwAAAABJRU5ErkJggg==";
                mButton.ID = $"mButton{team.Id}";
                mButton.CommandName = memberList.SelectedIndex.ToString();
                mButton.CssClass = "btn btn-secondary";
                mButton.CommandArgument = team.Id.ToString();
                mButton.Command += this.btnCandidate_Click;
                newCell.Controls.Add(mButton);

                ImageButton cButton = new ImageButton();
                cButton.ID = $"cButton{team.Id}";
                cButton.ImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAWCAYAAAAfD8YZAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA+5pVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1wTU06T3JpZ2luYWxEb2N1bWVudElEPSJ1dWlkOjY1RTYzOTA2ODZDRjExREJBNkUyRDg4N0NFQUNCNDA3IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjA1OUQzQkZEMDZBODExRTI5OUZEQTZGODg4RDc1ODdCIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjA1OUQzQkZDMDZBODExRTI5OUZEQTZGODg4RDc1ODdCIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKE1hY2ludG9zaCkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDowMTgwMTE3NDA3MjA2ODExODA4M0ZFMkJBM0M1RUU2NSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDowNjgwMTE3NDA3MjA2ODExODA4M0U3NkRBMDNEMDVDMSIvPiA8ZGM6dGl0bGU+IDxyZGY6QWx0PiA8cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiPmdseXBoaWNvbnM8L3JkZjpsaT4gPC9yZGY6QWx0PiA8L2RjOnRpdGxlPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Po6G6sQAAAB/SURBVHjaYvj//z8DDANBARALIIvhw8gaFwAxiHGBWAPQNf4nxQBsGok2gAGq6D85BoCAALkGwEwgywBkJ5BsALofSDIAWyAQbQCuUCTKACYGSgDVnE12gJEdVWQnEoqSJyUZgwmqABu4CMQOQEUf8EYV2YUBxcUQuQUgQIABAENaIhLMSm8LAAAAAElFTkSuQmCC";
                cButton.CssClass = "btn btn-secondary";
                cButton.CommandArgument = team.Id.ToString();
                cButton.Command += this.btnMember_Click;
                newCell.Controls.Add(cButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();

                lbl = new Label();
                lbl.Text = "Candidates";
                newCell.Controls.Add(lbl);

                ListBox candidatesList = new ListBox();
                candidatesList.ID = $"edittxtCandidates{team.Id}";
                candidatesList.DataSource = Candidates;
                candidatesList.DataBind();
                candidatesList.CssClass = "form-control";
                newCell.Controls.Add(candidatesList);
                newCell.ID = $"editCandidates{team.Id}";
                row.Cells.Add(newCell);

                cButton.CommandName = candidatesList.SelectedIndex.ToString();

                newCell = new TableCell();
                row.Cells.Add(newCell);
                //newCell = new TableCell();
                //row.Cells.Add(newCell);

                tblTeam.Rows.Add(row);
            }
        }

        protected void btnToggleInputs_Click(object sender, CommandEventArgs e)
        {
            //update style to visibility: visible on row for index!

            Members = (Controller.Participants[Convert.ToInt32(e.CommandName)-1] as Team).List;
            Candidates = Controller.GetAllCandidates(Convert.ToInt32(e.CommandName));

            LoadTeams();

            tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Style.Clear();
            tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Style.Add("visibility", "visible");
            tblTeam.Rows[Convert.ToInt32(e.CommandArgument) + 1].Style.Clear();
            tblTeam.Rows[Convert.ToInt32(e.CommandArgument) + 1].Style.Add("visibility", "visible");
        }

        protected void btnDeleteEdit_Click(object sender, CommandEventArgs e)
        {
            Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)).Delete();
            Response.Redirect(Request.RawUrl);
        }

        protected void btnSubmit_Click(object sender, CommandEventArgs e)
        {
            Team tmp = new Team();
            //Controller.NewParticipant as Team;
            /*
            List<Person> _list;
            int _type;
            string _name;
             */
            tmp.Name = Request.Form["ctl00$TeamManagement$txtName"];
            tmp.Type = Convert.ToInt32(Request.Form["ctl00$TeamManagement$txtType"]);
            tmp.List = Members;
            tmp.Put();
        }

        protected void btnMember_Click(object sender, CommandEventArgs e)
        {
            string value = Request.Form[$"ctl00$TeamManagement$edittxtMembers{e.CommandArgument}"];
            Person mmbr = Members.Find(x => (x.Name + " " + x.Surname).Contains(value));
            if (mmbr != null)
            {
                // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
                Members.Remove(mmbr);
                Candidates.Add(mmbr);
            }
            else
            {
            }
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();

            //    Response.Redirect(Request.RawUrl);
        }

        protected void btnCandidate_Click(object sender, CommandEventArgs e)
        {
            string value = Request.Form[$"ctl00$TeamManagement$edittxtCandidates{e.CommandArgument}"];
            Person mmbrNew = Candidates.Find(x => (x.Name + " " + x.Surname).Contains(value));
            if (mmbrNew != null)
            {
                Candidates.Remove(mmbrNew);
                Members.Add(mmbrNew);
                //(Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Add(mmbrNew);
            }
            else
            {
            }
            //    Response.Redirect(Request.RawUrl);
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();
        }

        protected void btnNewMember_Click(object sender, CommandEventArgs e)
        {
            string value = Request.Form[$"ctl00$TeamManagement$edittxtMembers{e.CommandArgument}"];
            Person mmbr = Members.Find(x => (x.Name + " " + x.Surname).Contains(value));
            if (mmbr != null)
            {
                // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
                Members.Remove(mmbr);
                Candidates.Add(mmbr);
            }
            else
            {
            }
    (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();

            //    Response.Redirect(Request.RawUrl);
        }

        protected void btnNewCandidate_Click(object sender, CommandEventArgs e)
        {
            string value = Request.Form[$"ctl00$TeamManagement$edittxtCandidates{e.CommandArgument}"];
            Person mmbrNew = Candidates.Find(x => (x.Name + " " + x.Surname).Contains(value));
            if (mmbrNew != null)
            {
                Candidates.Remove(mmbrNew);
                Members.Add(mmbrNew);
                //(Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Add(mmbrNew);
            }
            else
            {
            }
            //    Response.Redirect(Request.RawUrl);
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
            (tblTeam.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();
        }

        protected void btnEdit_Click(object sender, CommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            Team tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Team;
            tmp.Name = Request.Form[$"ctl00$TeamManagement$edittxtName{index}"];
            tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$TeamManagement$edittxtType{index}"]);
            tmp.List = Members;
            tmp.Update();

            Response.Redirect(Request.RawUrl);
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