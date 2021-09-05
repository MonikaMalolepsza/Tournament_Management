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
                addNewURole.DataBind();
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
            DropDownList cur = (DropDownList)row.FindControl("ddlist");
            temp.Role = Convert.ToInt32(cur.SelectedValue);
            TextBox tb = (TextBox)row.FindControl("passwordOU");
            if (tb.Text != "")
            {
                temp.Password = tb.Text;
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

        public string roleConverter(object role_id)
        {
            return UserController.Roles[Convert.ToInt32(role_id)];
        }

        protected void btnAdd_SubmitUser(object sender, CommandEventArgs e)
        {
            User newUser = new User();
            newUser.Role = Convert.ToInt32(addNewURole.SelectedValue);
            newUser.Name = nameU.Text;
            newUser.Surname = surnameU.Text;
            newUser.Email = emailU.Text;
            newUser.Password = passU.Text;
            newUser.Put();
            Response.Redirect(Request.RawUrl);
        }
    }
}