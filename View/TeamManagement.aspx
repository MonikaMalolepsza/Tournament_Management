<%@ Page Language="C#" AutoEventWireup="true" Title="Team" MasterPageFile="~/Base.Master" CodeBehind="TeamManagement.aspx.cs" Inherits="Tournament_Management.View.TeamManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TeamManagement" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Team Management</h1>
        <p class="lead">Under construction</p>
        <hr class="my-4">
        <p>...</p>
    </div>
    <div id="form1" runat="server">
        <div class="container-fluid">
        </div>
        <br />
        <br />
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="lead">Initialize New Team</div>
                <div class="my-4">Specify the name of your team and the sport discipline and save it. You can then add the members of your team in edit options below.</div>
                <br />
                <div class="container">
                    <asp:PlaceHolder ID="addTeam" runat="server"></asp:PlaceHolder>
                    <br />
                    <asp:Button ID="btnSubmit" OnCommand="btnSubmit_Click" runat="server" Text="Save" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>

        <br />
        <div class="panel panel-info">
            <div class="panel-heading">Teams</div>
            <br />
            <asp:Table ID="tblTeam" CssClass="table" runat="server"></asp:Table>
            <br />
        </div>
    </div>
</asp:Content>