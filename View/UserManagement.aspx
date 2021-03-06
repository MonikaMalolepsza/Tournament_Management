<%@ Page Language="C#" Title="Users" MasterPageFile="~/Base.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="Tournament_Management.View.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Users" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="#">
                <img class="img-responsive"
                    src="../Static/Images/users2.png"
                    height="20"
                    style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 20%;"
                    width="20" alt="home" />
            </a>
        </span>
        <h1 class="display-4">User Management</h1>
        <p class="lead">Here you can add edit and delete users.</p>
        <hr class="my-4">
        <p></p>
        <br />
    </div>
    <div class="panel panel-default">
        <div class="panel-heading lead">Users</div>
        <div class="panel-body">
            <asp:GridView
                ID="tblUsers"
                CssClass="table"
                runat="server"
                DataKeyNames="id"
                OnRowUpdating="tblUsers_RowUpdating"
                OnRowCancelingEdit="tblUsers_RowCancelingEdit"
                OnRowDeleting="tblUsers_RowDeleting"
                AutoGenerateDeleteButton="true"
                AutoGenerateEditButton="true"
                OnRowEditing="tblUsers_RowEditing"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="name"
                        ReadOnly="false"
                        HeaderText="Name" />
                    <asp:BoundField DataField="surname"
                        ReadOnly="false"
                        HeaderText="Surname" />
                    <asp:BoundField DataField="email"
                        ReadOnly="false"
                        HeaderText="Email" />
                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate>
                            <asp:Label ID="labelPass" Text='hidden' runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox TextMode="Password" ID="passwordOU" runat="server" CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <asp:Label ID="labelDD" Text='<%# roleConverter(Eval("Role"))%>' runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlist" DataSource='<%# UserController.Roles %>' DataTextField="Value" DataValueField="Key" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRowEditStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
            <br />
            <div class="h4">
                Add new User
            </div>
            <asp:Label runat="server" AssociatedControlID="addNewURole" Text="Role">
            </asp:Label>
            <asp:DropDownList runat="server" DataTextField="Value" DataValueField="Key" CssClass="form-control" d DataSource='<%# UserController.Roles %>' ID="addNewURole">
            </asp:DropDownList>
            <asp:Label runat="server" AssociatedControlID="nameU" Text="Name"></asp:Label>
            <asp:TextBox runat="server" ID="nameU" CssClass="form-control"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="surnameU" Text="Surname"></asp:Label>
            <asp:TextBox runat="server" ID="surnameU" CssClass="form-control"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="emailU" Text="Email"></asp:Label>
            <asp:TextBox runat="server" ID="emailU" CssClass="form-control"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="passU" Text="Password"></asp:Label>
            <asp:TextBox TextMode="Password" runat="server" ID="passU" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btnAdd" OnCommand="btnAdd_SubmitUser" runat="server" Text="Save" CssClass="btn btn-info" />
        </div>
        <br />
    </div>
</asp:Content>
