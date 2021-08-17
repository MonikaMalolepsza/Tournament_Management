<%@ Page Language="C#" AutoEventWireup="true" Title="Games" MasterPageFile="~/Base.Master" CodeBehind="GameManagement.aspx.cs" Inherits="Tournament_Management.View.GameManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Games" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Tournament Management - Games</h1>
        <hr class="my-4">
        <p>Here you can manage the games for given tournament.</p>
    </div>
    <div runat="server">
        <div class="container-fluid">
            <div id="rdbtnList1_container" runat="server">
                <p class="lead">Choose the Tournament</p>
                <br />
                <asp:DropDownList runat="server" CssClass="form-control" ID="tournamentList"></asp:DropDownList>
            </div>
            <asp:Button ID="btnShow" Text="Show Games" CssClass="btn btn-info my-auto" runat="server"></asp:Button>
        </div>
        <br />
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">Games</div>
            <asp:Table Visible="false" ID="tblGames" CssClass="table" runat="server"></asp:Table>
        </div>
    </div>
</asp:Content>