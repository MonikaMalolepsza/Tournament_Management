<%@ Page Language="C#" AutoEventWireup="true" Title="Home" MasterPageFile="~/Base.Master" CodeBehind="Home.aspx.cs" Inherits="Tournament_Management.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Home" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="~/Home">
                <img class="img-responsive"
                    src="../Static/Images/playoff.png"
                    height="20"
                    style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 20%;"
                    width="20" alt="home" />
            </a>
        </span>
        <h1 class="display-4">Hello,</h1>
        <p class="lead">Welcome to your tournament manager! You can manage the Teams, add the players, referees and other staff members. View played Games, see statistics and more!</p>
        <hr class="my-4">
        <p></p>
        <br />

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