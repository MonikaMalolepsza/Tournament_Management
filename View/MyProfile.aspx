<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Title="My Profile" MasterPageFile="~/Base.Master" Inherits="Tournament_Management.View.My" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ranking" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="~/#">
                <img class="img-responsive"
                    src="../Static/Images/sport-bag.png"
                    height="20"
                    style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 20%;"
                    width="20" alt="people" />
            </a>
        </span>
        <h1 class="display-4">My Profile</h1>
        <hr class="my-4">
    </div>
    <div id="form1" runat="server">
        <div class="container-fluid">
        </div>
        <br />
        <div class="panel panel-default">
            <div class="panel-heading">Details</div>
            <div class="panel-body">
                <asp:GridView ID="tblMy"
                    CssClass="table"
                    runat="server"
                    OnRowUpdating="tblMy_RowUpdating"
                    OnRowCancelingEdit="tblMy_RowCancelingEdit"
                    AutoGenerateEditButton="true"
                    OnRowEditing="tblMy_RowEditing"
                    AutoGenerateColumns="false"
                    OnRowDataBound="tblMy_DataBound">
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
                        <asp:BoundField DataField="role"
                            ReadOnly="true"
                            HeaderText="Role" />
                        <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                                <asp:Label ID="labelPass" Text='hidden' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox TextMode="Password" ID="passwordMy" runat="server" CssClass="form-control"></asp:TextBox>
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
            </div>
            <br />
        </div>
        <br />
    </div>
</asp:Content>
