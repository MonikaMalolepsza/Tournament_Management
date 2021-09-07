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

            if (!IsPostBack)
            {
                teamGrid.DataSource = Controller.Teams;
                teamGrid.DataBind();
            }
            //LoadInputTeam();
            //LoadInputMembers();
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

        //    private void LoadInputTeam()
        //    {
        //        Label lbl = new Label();
        //        lbl.Text = "Name";
        //        addTeam.Controls.Add(lbl);

        //        TextBox txt = new TextBox();
        //        txt.ID = "txtName";
        //        txt.Text = "";
        //        txt.CssClass = "form-control";
        //        addTeam.Controls.Add(txt);

        //        lbl = new Label();
        //        lbl.Text = "Discipline";
        //        addTeam.Controls.Add(lbl);

        //        DropDownList dropdown = new DropDownList();
        //        dropdown.ID = $"txtType";
        //        dropdown.CssClass = "form-control";
        //        dropdown.DataSource = Controller.TypeList;
        //        dropdown.DataTextField = "Value";
        //        dropdown.DataValueField = "Key";
        //        dropdown.DataBind();
        //        addTeam.Controls.Add(dropdown);
        //    }

        //    private void LoadInputMembers()
        //    {
        //        TableRow row = new TableRow();
        //        row.ID = "addMembers";

        //        TableCell newCell = new TableCell();

        //        Label lbl = new Label();
        //        lbl.Text = "Members";
        //        newCell.Controls.Add(lbl);
        //    }

        //    protected void btnMember_Click(object sender, CommandEventArgs e)
        //    {
        //        string value = Request.Form[$"ctl00$TeamManagement$edittxtMembers{e.CommandArgument}"];
        //        Person mmbr = Members.Find(x => (x.Name + " " + x.Surname).Contains(value));
        //        if (mmbr != null)
        //        {
        //            // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
        //            Members.Remove(mmbr);
        //            Candidates.Add(mmbr);
        //        }
        //        else
        //        {
        //        }
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();

        //        //    Response.Redirect(Request.RawUrl);
        //    }

        //    protected void btnCandidate_Click(object sender, CommandEventArgs e)
        //    {
        //        string value = Request.Form[$"ctl00$TeamManagement$edittxtCandidates{e.CommandArgument}"];
        //        Person mmbrNew = null;
        //        if (value != null) mmbrNew = Candidates.Find(x => (x.Name + " " + x.Surname).Contains(value));
        //        if (mmbrNew != null)
        //        {
        //            Candidates.Remove(mmbrNew);
        //            Members.Add(mmbrNew);
        //            //(Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Add(mmbrNew);
        //        }
        //        else
        //        {
        //        }
        //        //    Response.Redirect(Request.RawUrl);
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();
        //    }

        //    protected void btnNewMember_Click(object sender, CommandEventArgs e)
        //    {
        //        string value = Request.Form[$"ctl00$TeamManagement$edittxtMembers{e.CommandArgument}"];
        //        Person mmbr = Members.Find(x => (x.Name + " " + x.Surname).Contains(value));
        //        if (mmbr != null)
        //        {
        //            // (Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Remove(mmbr);
        //            Members.Remove(mmbr);
        //            Candidates.Add(mmbr);
        //        }
        //        else
        //        {
        //        }
        //(teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();

        //        //    Response.Redirect(Request.RawUrl);
        //    }

        //    protected void btnNewCandidate_Click(object sender, CommandEventArgs e)
        //    {
        //        string value = Request.Form[$"ctl00$TeamManagement$edittxtCandidates{e.CommandArgument}"];
        //        Person mmbrNew = Candidates.Find(x => (x.Name + " " + x.Surname).Contains(value));
        //        if (mmbrNew != null)
        //        {
        //            Candidates.Remove(mmbrNew);
        //            Members.Add(mmbrNew);
        //            //(Controller.Participants[Convert.ToInt32(e.CommandArgument)] as Team).List.Add(mmbrNew);
        //        }
        //        else
        //        {
        //        }
        //        //    Response.Redirect(Request.RawUrl);
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].FindControl($"edittxtMembers{ e.CommandArgument}") as ListBox).DataBind();
        //        (teamGrid.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].FindControl($"edittxtCandidates{ e.CommandArgument}") as ListBox).DataBind();
        //    }

        //    protected void btnEdit_Click(object sender, CommandEventArgs e)
        //    {
        //        string index = e.CommandArgument.ToString();
        //        Team tmp = Controller.Participants.First(x => x.Id == Convert.ToInt32(e.CommandArgument)) as Team;
        //        tmp.Name = Request.Form[$"ctl00$TeamManagement$edittxtName{index}"];
        //        tmp.Type = Convert.ToInt32(Request.Form[$"ctl00$TeamManagement$edittxtType{index}"]);
        //        tmp.List = Members;
        //        tmp.Update();

        //        Response.Redirect(Request.RawUrl);
        //    }

        protected void teamGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            teamGrid.EditIndex = e.NewEditIndex;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
        }

        protected void teamGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = teamGrid.Rows[e.RowIndex];
            Team temp = new Team();
            temp = Controller.Teams.First(team => team.Id == (int)e.Keys["id"]);
            temp.Delete();
            teamGrid.EditIndex = -1;
            teamGrid.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void teamGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            teamGrid.EditIndex = -1;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
        }

        protected void teamGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        public string typeConverter(object type_id)
        {
            return Controller.TypeList[Convert.ToInt32(type_id)];
        }

        protected void teamGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            teamGrid.PageIndex = e.NewPageIndex;
            teamGrid.DataSource = Controller.Teams;
            teamGrid.DataBind();
        }
    }
}