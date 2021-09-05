<%@ Page Language="C#" AutoEventWireup="true" Title="Tournaments" MasterPageFile="~/Base.Master" CodeBehind="TournamentManagement.aspx.cs" Inherits="Tournament_Management.View.TournamentManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TournamentManagement" runat="server">
    <div class="jumbotron">
         <div class="row">
               <div class="col-md-4">
                    <a runat="server" href="~/#">
                        <img class="img-responsive"
                            src="../Static/Images/sport.png"
                            style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width:  50%;"
                             alt="people" />
                    </a>
                </div>
             <div class="col-md-4">
                    <a runat="server" href="~/#">
                        <img class="img-responsive"
                            src="../Static/Images/team.png"
                            style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width:  50%;"
                            alt="people" />
                    </a>
                </div> 
             <div class="col-md-4">
                    <a runat="server" href="~/#">
                        <img class="img-responsive"
                            src="../Static/Images/trophy.png"
                            style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 50%;"
                            alt="people" />
                    </a>
                </div> 
            </div>
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
                    <asp:ListItem Value="3">&nbsp;Basketball</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;Handball</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div id="btnShow" runat="server"></div>
        </div>
        <br />
        <br />
        <div class="container">
            <br />
            <div id="form1" runat="server"></div>
            <br />
            <asp:Button ID="btnSubmit" OnCommand="btnSubmit_Click" Visible="true" runat="server" Text="Save" CssClass="btn btn-success" />
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