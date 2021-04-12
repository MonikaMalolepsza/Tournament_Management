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

                    tblPeople.Rows.Add(newRow);
                }
            }

        }
    }
}