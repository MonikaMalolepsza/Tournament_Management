<%@ Page Language="C#" AutoEventWireup="true"  Title="Home" MasterPageFile="~/Base.Master" CodeBehind="Home.aspx.cs" Inherits="Tournament_Management.Home" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="Home" runat="server">
        <div class="jumbotron">
          <h1 class="display-4">Hello,</h1>
          <p class="lead">Welcome to your tournament manager! Currently you can manage the players, referees and other staff members. Team manager is under construction.</p>
          <hr class="my-4">
            <p></p>
            <br />
                <div class="btn-group" role="group">
                    <a type="button" class="btn btn-info btn-lg" runat="server" href="~/View/TeamManagement">I want to manage my team!</a>
                </div>
                <div class="btn-group" role="group">
                    <a type="button" class="btn btn-info btn-lg" runat="server" href="~/View/PersonalManagement">I want to add new members!</a>
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
