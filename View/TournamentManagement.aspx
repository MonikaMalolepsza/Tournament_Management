<%@ Page Language="C#" AutoEventWireup="true" Title="Tournaments" MasterPageFile="~/Base.Master" CodeBehind="TournamentManagement.aspx.cs" Inherits="Tournament_Management.View.tournament" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PersonalManagement" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Tournament Management</h1>
        <hr class="my-4">
        <p>Here you can initialize Tournaments, update the details and add the games.</p>
    </div>
    <div runat="server">
        <div class="container-fluid">
            <div id="rdbtnList1_container" runat="server">
                <p class="lead">Choose the discipline</p>
                <br />
                <asp:RadioButtonList
                    CssClass="form-check vertical-center"
                    ID="typeList"
                    runat="server">
                    <asp:ListItem Value="0">&nbsp;Show All</asp:ListItem>
                    <asp:ListItem Value="1">&nbsp;Football</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;Basketball</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;Handball</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div id="btnShow" runat="server"></div>
        </div>
        <br />
        <br />
        <div class="container">
            <asp:Button ID="btnAdd" OnCommand="btnAdd_Click" Visible="false" runat="server" Text="Add New Member" CssClass="btn btn-secondary" />
            <br />
            <div id="form1" runat="server"></div>
            <br />
            <asp:Button ID="btnSubmit" OnCommand="btnSubmit_Click" Visible="false" runat="server" Text="Save" CssClass="btn btn-secondary" />
            <br />
        </div>
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">Tournaments</div>
            <asp:Table ID="tblTournaments" CssClass="table" runat="server"></asp:Table>
        </div>
        <br />
    </div>
</asp:Content>