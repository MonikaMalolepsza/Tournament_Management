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
        <div class="panel panel-info">
            <div class="panel-heading">Teams</div>
            <br />
            <asp:Table ID="tblTeam" CssClass="table table-striped" runat="server"></asp:Table>
            <br />
        </div>
        <div>
            <div class="container">
                <asp:PlaceHolder ID="editPerson" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>

</asp:Content>

