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
    public partial class PersonenVerwaltung : System.Web.UI.Page
    {
        private Controller _controller;

        public Controller Controller { get => _controller; set => _controller = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Controller = Global.Controller;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            tblInput.Enabled = true;
            btnSubmit.Enabled = true;
            //All
            if (rdbtnList1.Items[0].Selected)
            {
                Controller.GetAllPeople();
            }
            //Footballplayer
            else if (rdbtnList1.Items[1].Selected)
            {
                Controller.GetAllFootballPlayers();
            }
            LoadPeople();
            LoadInputFields();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //All
            if (rdbtnList1.Items[0].Selected)
            {

            }
            //Footballplayer
            else if (rdbtnList1.Items[1].Selected)
            {
                FootballPlayer tmp = Controller.NewParticipant as FootballPlayer;
                tmp.Name = Request.Form["txtName"];
                tmp.Surname = Request.Form["txtSurname"];
                tmp.Goals = Convert.ToInt32(Request.Form["txtGoals"]);
                tmp.Speed = Convert.ToDouble(Request.Form["txtSpeed"]);
                tmp.Age = Convert.ToInt32(Request.Form["txtAge"]);
                tmp.Active = Request.Form["txtActive"].GetTrueFalseString();
                tmp.Put();
            }
        }

        private void LoadInputFields()
        {
            //All
            if (rdbtnList1.Items[0].Selected)
            {

            }
            //Footballplayer
            else if (rdbtnList1.Items[1].Selected)
            {
                Controller.NewParticipant = new FootballPlayer();
            }
        }

        private void LoadPeople()
        {
            //All
            if (rdbtnList1.Items[0].Selected)
            {
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
            else if (rdbtnList1.Items[1].Selected)
            {
                //headerrows
                TableHeaderRow thr = new TableHeaderRow();

                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Name";
                thr.Cells.Add(newHeaderCell);

                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Surname";
                thr.Cells.Add(newHeaderCell);

                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Goals";
                thr.Cells.Add(newHeaderCell);

                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Speed";
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
                    newCell.Text = (pers as FootballPlayer).Name;
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = (pers as FootballPlayer).Surname;
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = (pers as FootballPlayer).Goals.ToString();
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = (pers as FootballPlayer).Speed.ToString();
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = (pers as FootballPlayer).Age.ToString();
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    newCell.Text = (pers as FootballPlayer).Active.GetYesNoString();
                    newRow.Cells.Add(newCell);

                    newCell = new TableCell();
                    CheckBox cbChoose = new CheckBox();
                    cbChoose.Checked = false;
                    cbChoose.ID = "cbChoose$" + pers.Id;
                    newCell.Controls.Add(cbChoose);
                    newRow.Cells.Add(newCell);

                    tblPeople.Rows.Add(newRow);
                }
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach(TableRow tr in tblPeople.Rows)
            {
                if (tr.Cells[tr.Cells.Count - 1].Controls is CheckBox)
                    if ((tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox).Checked)
                        Controller.Participants.First(x => x.Id == Convert.ToInt32((tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox).ID.Split('$')[1])).Delete();
            }
        }
    }
}