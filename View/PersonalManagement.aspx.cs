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

            if (rdbtnList1.Items[1].Selected)
            {
                LoadFootballPlayers();
                LoadInputFields();
            }
            // TODO implement the rest of the people
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            tblInput.Enabled = true;
            btnSubmit.Enabled = true;
            //All
            if (rdbtnList1.Items[0].Selected)
            {
                Controller.GetAllPeople();
                LoadPeople();
            }
            //Footballplayer
            else if (rdbtnList1.Items[1].Selected)
            {
                Controller.GetAllFootballPlayers();
                LoadFootballPlayers();
            }
            //BasketballPlayer
            else if (rdbtnList1.Items[2].Selected)
            {
                Controller.GetAllBasketballPlayers();
                LoadBasketballPlayers();
            }
            //HandballPlayer
            else if (rdbtnList1.Items[3].Selected)
            {
                Controller.GetAllHndballPlayers();
                LoadHandballPlayers();
            }
            //Physio
            else if (rdbtnList1.Items[4].Selected)
            {
                //todo
                //  Controller.GetAllPhysio();
                //  LoadPhysio();
            }
            //Trainer
            else if (rdbtnList1.Items[5].Selected)
            {
                //todo
                //  Controller.GetAllTrainers();
                //  LoadTrainer();
            }
            //Referee
            else if (rdbtnList1.Items[6].Selected)
            {
                //todo in controller
                //  Controller.GetAllReferees();
                //  LoadReferee();
            }

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
            //BasketballPlayer
            else if (rdbtnList1.Items[2].Selected)
            {
            // Implement put request
            }
            //HandballPlayer
            else if (rdbtnList1.Items[3].Selected)
            {
                // Implement put request
            }
            //Physio
            else if (rdbtnList1.Items[4].Selected)
            {
                // Implement put request

            }
            //Trainer
            else if (rdbtnList1.Items[5].Selected)
            {
                // Implement put request
            }
            //Referee
            else if (rdbtnList1.Items[6].Selected)
            {
                // Implement put request
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

            //TODO: NOT TESTED YET, lust prepared, not sure if ok!

            //BasketballPlayer
            else if (rdbtnList1.Items[2].Selected)
            {
                Controller.NewParticipant = new BasketballPlayer();
            }          
            //HandballPlayer
            else if (rdbtnList1.Items[3].Selected)
            {
                Controller.NewParticipant = new HandballPlayer();
            }           
            //Physio
            else if (rdbtnList1.Items[4].Selected)
            {
                Controller.NewParticipant = new Physio();
            }            
            //Trainer
            else if (rdbtnList1.Items[5].Selected)
            {
                Controller.NewParticipant = new Trainer();
            }           
            //Referee
            else if (rdbtnList1.Items[6].Selected)
            {
                Controller.NewParticipant = new Referee();
            }
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
            newHeaderCell.ID = "headerGoals";
            newHeaderCell.Text = "Goals";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerSpeed";
            newHeaderCell.Text = "Speed";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
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
                newCell.ID = "cellGoals" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Goals.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellSpeed" + pers.Id;
                newCell.Text = (pers as FootballPlayer).Speed.ToString();
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
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
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
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
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
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as BasketballPlayer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
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
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
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
                newCell.ID = "cellAge" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Age.ToString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as HandballPlayer).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
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
            newHeaderCell.ID = "headerExperience";
            newHeaderCell.Text = "Experience";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerAge";
            newHeaderCell.Text = "Age";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
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
                newCell.ID = "cellExperience" + pers.Id;
                newCell.Text = (pers as Physio).Experience.ToString();
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
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
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
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
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
            newHeaderCell.ID = "headerCert";
            newHeaderCell.Text = "Certificate";
            thr.Cells.Add(newHeaderCell);

            newHeaderCell = new TableHeaderCell();
            newHeaderCell.ID = "headerActive";
            newHeaderCell.Text = "Active";
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
                newCell.ID = "cellCert" + pers.Id;
                newCell.Text = (pers as Referee).Certificate;
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellActive" + pers.Id;
                newCell.Text = (pers as Referee).Active.GetYesNoString();
                newRow.Cells.Add(newCell);

                newCell = new TableCell();
                newCell.ID = "cellCheckbox" + pers.Id;
                CheckBox cbChoose = new CheckBox();
                cbChoose.ID = "cbChoose!" + pers.Id;
                newCell.Controls.Add(cbChoose);
                newRow.Cells.Add(newCell);

                tblPeople.Rows.Add(newRow);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for(int i = 1; i < tblPeople.Rows.Count; i++)
            {
                //this.Page.Request.Form["cbChoose$1"]
                if (tblPeople.Rows[i].Cells[tblPeople.Rows[i].Cells.Count - 1].Controls[0] is CheckBox)
                    if ((tblPeople.Rows[i].Cells[tblPeople.Rows[i].Cells.Count - 1].Controls[0] as CheckBox).Checked)
                        Controller.Participants.First(x => x.Id == Convert.ToInt32((tblPeople.Rows[i].Cells[tblPeople.Rows[i].Cells.Count - 1].Controls[0] as CheckBox).ID.Split('!')[1])).Delete();
            }
            Response.Redirect(Request.RawUrl);
            //foreach(TableRow tr in tblPeople.Rows)
            //{
            //    if (tr.Cells[tr.Cells.Count - 1].Controls[0] is CheckBox)
            //        if ((tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox).Checked)
            //            Controller.Participants.First(x => x.Id == Convert.ToInt32((tr.Cells[tr.Cells.Count - 1].Controls[0] as CheckBox).ID.Split('$')[1])).Delete();
            //}
        }
    }
}