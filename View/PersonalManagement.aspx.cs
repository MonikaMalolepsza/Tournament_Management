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
    public partial class PersonalManagement : System.Web.UI.Page
    {
        private Controller _controller;
        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
            //  this.generateBtnList();
            this.generateShowBtn();
            //persist the old value after refresh
            rdbtnList1.SelectedValue = Controller.ActiveParticipant.ToString();
            switch (Controller.ActiveParticipant)
            {
                case 0:
                    {
                        Controller.GetAllPeople();
                        LoadPeople();
                        break;
                    }
                case 1:
                    {
                        Controller.GetAllFootballPlayers();
                        LoadFootballPlayers();
                        btnAdd.Visible = true;
                        break;
                    }
                case 2:
                    {
                        Controller.GetAllBasketballPlayers();
                        LoadBasketballPlayers();
                        btnAdd.Visible = true;
                        break;
                    }
                case 3:
                    {
                        Controller.GetAllHndballPlayers();
                        LoadHandballPlayers();

                        break;
                    }
                case 4:
                    {
                        Controller.GetAllPhysio();
                        LoadPhysio();
                        break;
                    }
                case 5:
                    {
                        Controller.GetAllTrainers();
                        LoadTrainer();
                        break;
                    }
                case 6:
                    {
                        Controller.GetAllReferees();
                        LoadReferee();
                        break;
                    }
                default:
                    break;
            }
            if (Controller.ActiveParticipant != 0)
            {
                btnAdd.Visible = true;
            }

            this.hideInputs();
        }

        private void generateShowBtn()
        {
            Button save = new Button();
            save.ID = "btnConfirm";
            save.Command += btnShow_Click;
            ListControl ctrl = rdbtnList1;
            save.CommandArgument = ctrl.SelectedValue;
            save.Text = "Show me the players!";
            save.CssClass = "btn btn-info";
            btnShow.Controls.Add(save);
        }

        // Load tabn
        private void LoadInputBasic()
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
            lbl.Text = "Surname";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtSurname";
            txt.Text = "";
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Age";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtAge";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Active";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtActive";
            txt.Text = "";
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        private void LoadInputFP()
        {
            Controller.NewParticipant = new FootballPlayer();

            //All
            this.LoadInputBasic();

            //Special fields
            Label lbl = new Label();
            lbl.Text = "Goal";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtGoal";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Speed";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtSpeed";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        private void LoadInputBP()
        {
            Controller.NewParticipant = new BasketballPlayer();

            //All
            this.LoadInputBasic();

            //Special fields
            Label lbl = new Label();
            lbl.Text = "Goal";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtGoal";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Speed";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtSpeed";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Height";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtHeight";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        //HandballPlayer
        private void LoadInputHP()
        {
            Controller.NewParticipant = new HandballPlayer();

            //All
            this.LoadInputBasic();

            //Special fields

            Label lbl = new Label();
            lbl.Text = "Goal";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtGoal";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Speed";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtSpeed";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);

            lbl = new Label();
            lbl.Text = "Position";
            form1.Controls.Add(lbl);

            txt = new TextBox();
            txt.ID = "txtPosition";
            txt.Text = "";
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        //Physio
        private void LoadInputP()
        {
            Controller.NewParticipant = new Physio();

            //All
            this.LoadInputBasic();

            //Special fields

            Label lbl = new Label();
            lbl.Text = "Experience";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtExperience";
            txt.Text = "";
            txt.TextMode = TextBoxMode.Number;
            txt.CssClass = "form-control";
            form1.Controls.Add(txt);
        }

        //Trainer
        private void LoadInputT()
        {
            Controller.NewParticipant = new Trainer();

            //All
            this.LoadInputBasic();

            Label lbl = new Label();
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
        }

        //Referee
        private void LoadInputR()
        {
            Controller.NewParticipant = new Referee();
            //All
            this.LoadInputBasic();

            //Special fields

            Label lbl = new Label();
            lbl.Text = "Certificate";
            form1.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.ID = "txtCert";
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
        }

        private void LoadPeople()
        {
            tblPeople.Rows.Clear();
            //All
            //headerrows
            TableHeaderRow thr = new TableHeaderRow();

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();

                TableCell newCell = new TableCell();
                newCell.Text = (pers as Person).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = (pers as Person).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = (pers as Person).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.Text = (pers as Person).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
            }
        }

        //Footballplayer
        private void LoadFootballPlayers()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerGoals";
            newHeaderCell.Text = "Goals";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSpeed";
            newHeaderCell.Text = "Speed";
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

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellGoals" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Goals.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSpeed" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Speed.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + pers.Id;
                newCell.Text = Controller.TypeList[(pers as FootballPlayer).Type];
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                //create hidden row for edit

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as FootballPlayer).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as FootballPlayer).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.TextMode = TextBoxMode.Number;
                txt.Text = (pers as FootballPlayer).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as FootballPlayer).Active.GetYesNoString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.TextMode = TextBoxMode.Number;
                txt.ID = $"edittxtGoal{pers.Id}";
                txt.Text = (pers as FootballPlayer).Goals.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editGoal{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSpeed{pers.Id}";
                txt.TextMode = TextBoxMode.Number;
                txt.Text = (pers as FootballPlayer).Speed.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editSpeed{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtType{pers.Id}";
                txt.Text = "Football";
                txt.Enabled = false;
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editType{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);
            }
        }

        //Basketballplayer
        private void LoadBasketballPlayers()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerGoals";
            newHeaderCell.Text = "Goals";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerHeight";
            newHeaderCell.Text = "Height";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSpeed";
            newHeaderCell.Text = "Speed";
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

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellGoals" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Goals.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellHeight" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Height.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSpeed" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Speed.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + pers.Id;
                newCell.Text = Controller.TypeList[(pers as BasketballPlayer).Type];
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = pers.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.Text = "X";
                delButton.CommandName = "Delete";
                delButton.CssClass = "btn btn-danger";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                // add input fields

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Active.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtGoal{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Goals.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editGoal{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtHeight{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Height.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editHeight{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSpeed{pers.Id}";
                txt.Text = (pers as BasketballPlayer).Speed.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editSpeed{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtType{pers.Id}";
                txt.Text = "Basketball";
                txt.Enabled = false;
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editType{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);

                tblPeople.Rows.Add(row);
            }
        }

        //HandballPlayer
        private void LoadHandballPlayers()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerGoals";
            newHeaderCell.Text = "Goals";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerPosition";
            newHeaderCell.Text = "Position";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSpeed";
            newHeaderCell.Text = "Speed";
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

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellGoals" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Goals.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellPosition" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Position.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSpeed" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Speed.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + pers.Id;
                newCell.Text = Controller.TypeList[(pers as HandballPlayer).Type];
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = pers.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                //add edit row

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as HandballPlayer).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as HandballPlayer).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.Text = (pers as HandballPlayer).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as HandballPlayer).Active.GetYesNoString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtGoal{pers.Id}";
                txt.Text = (pers as HandballPlayer).Goals.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editGoal{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtPosition{pers.Id}";
                txt.Text = (pers as HandballPlayer).Position;
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editPosition{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSpeed{pers.Id}";
                txt.Text = (pers as HandballPlayer).Speed.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editSpeed{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtType{pers.Id}";
                txt.Text = "Handball";
                txt.Enabled = false;
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editType{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);

                tblPeople.Rows.Add(row);
            }
        }

        //Physio
        private void LoadPhysio()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerExperience";
            newHeaderCell.Text = "Experience";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerEdit";
            newHeaderCell.Text = "Edit";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerDel";
            newHeaderCell.Text = "Delete";
            thr.Cells.Add(newHeaderCell);

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as Physio).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as Physio).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as Physio).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as Physio).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellExperience" + pers.Id;
                newCell.Text = (pers as Physio).Experience.ToString();
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = pers.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                // add edit row

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as Physio).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as Physio).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.Text = (pers as Physio).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as Physio).Active.GetYesNoString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtExperience{pers.Id}";
                txt.Text = (pers as Physio).Experience.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editExperience{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);

                tblPeople.Rows.Add(row);
            }
        }

        //Trainer
        private void LoadTrainer()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
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

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as Trainer).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as Trainer).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as Trainer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as Trainer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + pers.Id;
                newCell.Text = Controller.TypeList[(pers as Trainer).Type];
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = pers.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                //add edit row

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as Trainer).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as Trainer).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.Text = (pers as Trainer).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as Trainer).Active.GetYesNoString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                DropDownList dropdown = new DropDownList();
                dropdown.ID = $"edittxtType{pers.Id}";
                dropdown.CssClass = "form-control";
                dropdown.DataSource = Controller.TypeList;
                dropdown.DataTextField = "Value";
                dropdown.DataValueField = "Key";
                dropdown.DataBind();
                newCell.Controls.Add(dropdown);
                newCell.ID = $"editType{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);

                tblPeople.Rows.Add(row);
            }
        }

        //Referee
        private void LoadReferee()
        {
            tblPeople.Rows.Clear();

            //headerrows
            TableHeaderRow thr = new TableHeaderRow();
            thr.ID = "thr";

            TableHeaderCell newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerName";
            newHeaderCell.Text = "Name";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSurname";
            newHeaderCell.Text = "Surname";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerCert";
            newHeaderCell.Text = "Certificate";
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

            tblPeople.Rows.Add(thr);

            //Data
            foreach (Participant pers in Controller.Participants)
            {
                TableRow newRow = new TableRow();
                newRow.ID = "tableRow" + pers.Id;

                TableCell newCell = new TableCell();
                newCell.ID = "cellName" + pers.Id;
                newCell.Text = (pers as Referee).Name;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSurname" + pers.Id;
                newCell.Text = (pers as Referee).Surname;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as Referee).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as Referee).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellCert" + pers.Id;
                newCell.Text = (pers as Referee).Certificate;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellType" + pers.Id;
                newCell.Text = Controller.TypeList[(pers as Referee).Type];
                newRow.Cells.Add(newCell);

                string currentRowIndexTemp = (tblPeople.Rows.Count + 1).ToString();
                newCell = new TableCell();
                newCell.ID = "cellEditButton" + pers.Id;
                Button editButton = new Button();
                editButton.ID = "editButton_" + pers.Id;
                editButton.Text = "Edit";
                editButton.CssClass = "btn btn-warning";
                editButton.CommandArgument = currentRowIndexTemp;
                editButton.CommandName = pers.Id.ToString();
                editButton.Command += this.btnToggleInputs_Click;
                newCell.Controls.Add(editButton);
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellDelButton" + pers.Id;
                Button delButton = new Button();
                delButton.ID = "delButton_" + pers.Id;
                delButton.CssClass = "btn btn-danger";
                delButton.CommandName = "Delete";
                delButton.Text = "X";
                delButton.CommandArgument = pers.Id.ToString();
                delButton.Command += this.btnDeleteEdit_Click;
                newCell.Controls.Add(delButton);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);

                //add edit row

                TableRow row = new TableRow();
                row.Style.Add("visibility", "collapse");
                row.ID = $"editRow{pers.Id}";

                newCell = new TableCell();
                TextBox txt = new TextBox();
                txt.ID = $"edittxtName{pers.Id}";
                txt.Text = (pers as Referee).Name;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editName{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtSurname{pers.Id}";
                txt.Text = (pers as Referee).Surname;
                txt.CssClass = "form-control";
                newCell.Controls.Add(txt);
                newCell.ID = $"editSurname{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtAge{pers.Id}";
                txt.Text = (pers as Referee).Age.ToString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editAge{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtActive{pers.Id}";
                txt.Text = (pers as Referee).Active.GetYesNoString();
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editActive{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                txt = new TextBox();
                txt.ID = $"edittxtCert{pers.Id}";
                txt.Text = (pers as Referee).Certificate;
                txt.CssClass = "form-control";
                form1.Controls.Add(txt);
                newCell.Controls.Add(txt);
                newCell.ID = $"editCert{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                DropDownList dropdown = new DropDownList();
                dropdown.ID = $"edittxtType{pers.Id}";
                dropdown.CssClass = "form-control";
                dropdown.DataSource = Controller.TypeList;
                dropdown.DataTextField = "Value";
                dropdown.DataValueField = "Key";
                dropdown.DataBind();
                newCell.Controls.Add(dropdown);
                newCell.ID = $"editType{pers.Id}";
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellSaveButton{pers.Id}";
                Button saveButton = new Button();
                saveButton.ID = $"saveButton{pers.Id}";
                saveButton.CommandName = "Edit";
                saveButton.Text = "Save";
                saveButton.CssClass = "btn btn-success";
                saveButton.CommandArgument = pers.Id.ToString();
                saveButton.Command += this.btnEdit_Click;
                newCell.Controls.Add(saveButton);
                row.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = $"cellCancelButton{pers.Id}";
                Button cancelButton = new Button();
                cancelButton.ID = $"cancelButton{pers.Id}";
                cancelButton.Text = "Cancel";
                cancelButton.CssClass = "btn btn-secondary";
                cancelButton.CommandArgument = pers.Id.ToString();
                cancelButton.Command += this.btnCancel_Click;
                newCell.Controls.Add(cancelButton);
                row.Cells.Add(newCell);

                tblPeople.Rows.Add(row);
            }
        }

        // style
        private void hideInputs()
        {
            foreach (TableRow tr in tblPeople.Rows)
            {
                if (tr.Style.Count > 0)
                {
                    tr.Style.Clear();
                    tr.Style.Add("visibility", "collapse");
                }
            }
        }

        //helper methods

        protected void setBaseNew(Person InOutPerson)
        {
            InOutPerson.Name = Request.Form["ctl00$PersonalManagement$txtName"];
            InOutPerson.Surname = Request.Form["ctl00$PersonalManagement$txtSurname"];
            InOutPerson.Age = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtAge"]);
            InOutPerson.Active = Request.Form["ctl00$PersonalManagement$txtActive"].GetTrueFalseString();
        }

        protected void setBaseEdit(Person InOutPerson, string index)
        {
            InOutPerson.Name = Request.Form[$"ctl00$PersonalManagement$edittxtName{index}"];
            InOutPerson.Surname = Request.Form[$"ctl00$PersonalManagement$edittxtSurname{index}"];
            InOutPerson.Age = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtAge{index}"]);
            InOutPerson.Active = Request.Form[$"ctl00$PersonalManagement$edittxtActive{index}"].GetTrueFalseString();
        }

        // event handlers
        protected void btnToggleInputs_Click(object sender, CommandEventArgs e)
        {
            //update style to visibility: visible on row for index!
            tblPeople.Rows[Convert.ToInt32(e.CommandArgument)].Style.Clear();
            tblPeople.Rows[Convert.ToInt32(e.CommandArgument)].Style.Add("visibility", "visible");
        }

        protected void btnAdd_Click(object sender, CommandEventArgs e)
        {
            switch (Controller.ActiveParticipant)
            {
                case 1:
                    {
                        LoadInputFP();
                        break;
                    }
                case 2:
                    {
                        LoadInputBP();
                        break;
                    }
                case 3:
                    {
                        LoadInputHP();
                        break;
                    }
                case 4:
                    {
                        LoadInputP();
                        break;
                    }
                case 5:
                    {
                        LoadInputT();
                        break;
                    }
                case 6:
                    {
                        LoadInputR();
                        break;
                    }
                default:
                    break;
            }

            btnSubmit.Visible = true;
        }

        protected void btnShow_Click(object sender, CommandEventArgs e)
        {
            Controller.ActiveParticipant = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnSubmit_Click(object sender, CommandEventArgs e)
        {
            switch (Controller.ActiveParticipant)
            {
                case 1:
                    {
                        FootballPlayer tmp = Controller.NewParticipant as FootballPlayer;
                        this.setBaseNew(tmp);
                        tmp.Goals = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtGoals"]);
                        tmp.Speed = Convert.ToDouble(Request.Form["ctl00$PersonalManagement$txtSpeed"]);
                        tmp.Put(); break;
                    }
                case 2:
                    {
                        BasketballPlayer tmp = Controller.NewParticipant as BasketballPlayer;
                        this.setBaseNew(tmp);
                        tmp.Goals = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtGoals"]);
                        tmp.Height = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtHeight"]);
                        tmp.Speed = Convert.ToDouble(Request.Form["ctl00$PersonalManagement$txtSpeed"]);
                        tmp.Put();
                        break;
                    }
                case 3:
                    {
                        HandballPlayer tmp = Controller.NewParticipant as HandballPlayer;
                        this.setBaseNew(tmp);
                        tmp.Goals = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtGoals"]);
                        tmp.Position = Request.Form["ctl00$PersonalManagement$txtPosition"];
                        tmp.Speed = Convert.ToDouble(Request.Form["ctl00$PersonalManagement$txtSpeed"]);
                        tmp.Put();
                        break;
                    }
                case 4:
                    {
                        Physio tmp = Controller.NewParticipant as Physio;
                        this.setBaseNew(tmp);
                        tmp.Experience = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtExperience"]);
                        tmp.Put();
                        break;
                    }
                case 5:
                    {
                        Trainer tmp = Controller.NewParticipant as Trainer;
                        this.setBaseNew(tmp);
                        tmp.Age = Convert.ToInt32(Request.Form["ctl00$PersonalManagement$txtAge"]);
                        tmp.Put();
                        break;
                    }
                case 6:
                    {
                        Referee tmp = Controller.NewParticipant as Referee;
                        this.setBaseNew(tmp);
                        tmp.Certificate = Request.Form["ctl00$PersonalManagement$txtCert"];
                        tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$txtType"]);
                        tmp.Put();
                        break;
                    }
                default:
                    break;
            }
            btnSubmit.Visible = false;
        }

        protected void btnCancel_Click(object sender, CommandEventArgs e)
        {
            this.hideInputs();
        }

        protected void btnDeleteEdit_Click(object sender, CommandEventArgs e)
        {
            Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)).Delete();

            Response.Redirect(Request.RawUrl);
        }

        protected void btnEdit_Click(object sender, CommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            switch (Controller.ActiveParticipant)
            {
                case 1:
                    {
                        FootballPlayer tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as FootballPlayer;
                        this.setBaseEdit(tmp, index);
                        tmp.Goals = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtGoals{index}"]);
                        tmp.Speed = Convert.ToDouble(Request.Form[$"ctl00$PersonalManagement$edittxtSpeed{index}"]);
                        tmp.Update();
                        break;
                    }
                case 2:
                    {
                        BasketballPlayer tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as BasketballPlayer;
                        this.setBaseEdit(tmp, index);
                        tmp.Goals = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtGoals{index}"]);
                        tmp.Height = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtHeight{index}"]);
                        tmp.Speed = Convert.ToDouble(Request.Form[$"ctl00$PersonalManagement$edittxtSpeed{index}"]);
                        tmp.Update();
                        break;
                    }
                case 3:
                    {
                        HandballPlayer tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as HandballPlayer;
                        this.setBaseEdit(tmp, index);
                        tmp.Goals = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtGoals{index}"]);
                        tmp.Position = Request.Form[$"ctl00$PersonalManagement$edittxtPosition{index}"];
                        tmp.Speed = Convert.ToDouble(Request.Form[$"ctl00$PersonalManagement$edittxtSpeed{index}"]);
                        tmp.Update();
                        break;
                    }
                case 4:
                    {
                        Physio tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Physio;
                        this.setBaseEdit(tmp, index);
                        tmp.Experience = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtCert{index}"]);
                        tmp.Update();
                        break;
                    }
                case 5:
                    {
                        Trainer tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Trainer;
                        this.setBaseEdit(tmp, index);
                        tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtType{index}"]);
                        tmp.Update();
                        break;
                    }
                case 6:
                    {
                        Referee tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Referee;
                        this.setBaseEdit(tmp, index);
                        tmp.Certificate = Request.Form[$"ctl00$PersonalManagement$edittxtCert{index}"];
                        tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$PersonalManagement$edittxtType{index}"]);
                        tmp.Update();
                        break;
                    }
                default:
                    break;
            }
            btnSubmit.Visible = false;

            this.hideInputs();

            Response.Redirect(Request.RawUrl);
        }
    }
}