<%@ Page Language="C#" AutoEventWireup="true" Title="Home" MasterPageFile="~/Base.Master" CodeBehind="Home.aspx.cs" Inherits="Tournament_Management.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Home" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="~/Home">
                <img class="img-responsive" src="Static/Images/Board-Background.jpg" />
            </a>
        </span>
        <h1 class="display-4">Hello,</h1>
        <p class="lead">Welcome to your tournament manager! You can manage the Teams, add the players, referees and other staff members. View played Games, see statistics and more!</p>
        <hr class="my-4">
        <p></p>
        <br />
        <div class="btn-group" role="group">
            <a type="button" class="btn btn-info btn-lg" runat="server" href="~/View/TeamManagement">manage my team</a>
        </div>
        <div class="btn-group" role="group">
            <a type="button" class="btn btn-info btn-lg" runat="server" href="~/View/PersonalManagement">add new members</a>
        </div> 
        <div class="btn-group" role="group"> 
            <a type="button" class="btn btn-info btn-lg" runat="server" href="~/View/TournamentManagement">go to tournaments</a>
        </div>
    </div>
    <div id="form1" runat="server">
        <div class="container-fluid">
        </div>
        <br />
        <br />
        <div>
        </div>
    </div>
</asp:Content>