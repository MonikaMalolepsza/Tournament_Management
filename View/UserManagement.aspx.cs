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
    public partial class UserManagement : System.Web.UI.Page
    {
        private UserController userController;

        public UserController UserController { get => userController; set => userController = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserController = Global.UserController;
            UserController.getAllUsers();

            if (!IsPostBack)
            {
                tblUsers.DataSource = UserController.Users;
                tblUsers.DataBind();
            }
        }

        protected void tblUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = tblUsers.Rows[e.RowIndex];
            User temp = new User();
            temp = UserController.Users.First(user => user.Id == (int)e.Keys["id"]);
            temp.Name = ((TextBox)(row.Cells[1].Controls[0])).Text;
            temp.Surname = ((TextBox)(row.Cells[2].Controls[0])).Text;
            temp.Email = ((TextBox)(row.Cells[3].Controls[0])).Text;
            //TODO: map the roles dictionary here, best would be to display the dropdown on edit
            temp.Role = 1;
            if (((TextBox)(row.Cells[4].Controls[0])).Text != "")
            {
                temp.Password = ((TextBox)(row.Cells[4].Controls[0])).Text;
                temp.UpdatePassword();
            }
            temp.Update();
            temp = null;
            UserController.getAllUsers();
            Response.Redirect(Request.RawUrl);
        }

        protected void tblUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tblUsers.EditIndex = -1;
            tblUsers.DataSource = UserController.Users;
            tblUsers.DataBind();
        }

        protected void tblUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tblUsers.EditIndex = e.NewEditIndex;
            tblUsers.DataSource = UserController.Users;
            tblUsers.DataBind();
        }

        protected void tblUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = tblUsers.Rows[e.RowIndex];
            User temp = new User();
            temp = UserController.Users.First(user => user.Id == (int)e.Keys["id"]);
            temp.Delete();
            tblUsers.EditIndex = -1;
            tblUsers.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void tblUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && tblUsers.EditIndex == -1)
            {
                // Display the roles instead of ids
                e.Row.Cells[5].Text = UserController.Roles[Convert.ToInt32(e.Row.Cells[5].Text)];
            }
        }
    }
}