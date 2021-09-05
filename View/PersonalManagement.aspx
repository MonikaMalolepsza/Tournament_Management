<%@ Page Language="C#" AutoEventWireup="true" Title="People" MasterPageFile="~/Base.Master" CodeBehind="PersonalManagement.aspx.cs" Inherits="Tournament_Management.View.PersonalManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PersonalManagement" runat="server">
    <div class="jumbotron">
        <div class="row">
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/referee1.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/basketball.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/football-player.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/volleyball.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/bicycle.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
            <div class="col-md-2">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/athlete.png"
                        height="20"
                        style="display: block; margin-bottom: 25px; width: 80%;"
                        width="20" alt="people" />
                </a>
            </div>
        </div>

        <h1 class="display-4">Personal Management</h1>
        <p class="lead">Can't find your favourite Athlete? Say no more!</p>
        <hr class="my-4">
        <p>Here you can add new players to your team, update the details and delete the inactive members.</p>
    </div>
    <div runat="server">
        <div class="container-fluid">
            <div id="rdbtnList1_container" runat="server">
                <asp:RadioButtonList
                    CssClass="form-check vertical-center"
                    ID="rdbtnList1"
                    runat="server">
                    <asp:ListItem Value="0">&nbsp;All People</asp:ListItem>
                    <asp:ListItem Value="1">&nbsp;Footballplayer</asp:ListItem>
                    <asp:ListItem Value="2">&nbsp;Basketballplayer</asp:ListItem>
                    <asp:ListItem Value="3">&nbsp;Handballplayer</asp:ListItem>
                    <asp:ListItem Value="4">&nbsp;Physio</asp:ListItem>
                    <asp:ListItem Value="5">&nbsp;Trainer</asp:ListItem>
                    <asp:ListItem Value="6">&nbsp;Referee</asp:ListItem>
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
            <div class="panel-heading">Members</div>
            <asp:Table ID="tblPeople" CssClass="table" runat="server"></asp:Table>
        </div>
        <br />
    </div>
</asp:Content>