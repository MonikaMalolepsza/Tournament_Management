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
    public partial class My : System.Web.UI.Page
    {
        private UserController userController;

        public UserController UserController { get => userController; set => userController = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserController = Global.UserController;
            List<User> UsrGrid = new List<User>();
            UsrGrid.Add(UserController.User);
            tblMy.DataSource = UsrGrid;
            tblMy.DataBind();
        }

        protected void tblMy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tblMy.EditIndex = e.NewEditIndex;
            //tblMy.SelectRow(3);

            List<User> UsrGrid = new List<User>();
            UsrGrid.Add(UserController.User);
            tblMy.DataSource = UsrGrid;
            tblMy.DataBind();
        }

        protected void tblMy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tblMy.EditIndex = -1;
            List<User> UsrGrid = new List<User>();
            UsrGrid.Add(UserController.User);
            tblMy.DataSource = UsrGrid;
            tblMy.DataBind();
        }

        protected void tblMy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = tblMy.Rows[e.RowIndex];
            UserController.User.Name = ((TextBox)(row.Cells[2].Controls[0])).Text;
            UserController.User.Surname = ((TextBox)(row.Cells[3].Controls[0])).Text;
            UserController.User.Email = ((TextBox)(row.Cells[4].Controls[0])).Text;

            if (((TextBox)(row.Cells[5].Controls[0])).Text != "")
            {
                UserController.UpdatePassword(UserController.User.Id, ((TextBox)(row.Cells[5].Controls[0])).Text);
            }
            UserController.User.Update();
            tblMy.EditIndex = -1;
        }
        protected void tblMy_DataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the roles instead of ids
                e.Row.Cells[4].Text = UserController.Roles[Convert.ToInt32(e.Row.Cells[4].Text)];

            }

        }


    }
}