﻿<%@ Page Language="C#" AutoEventWireup="true" Title="People" MasterPageFile="~/Base.Master" CodeBehind="PersonalManagement.aspx.cs" Inherits="Tournament_Management.View.PersonalManagement" %>

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
    <div class="container-fluid">
        <div id="rdbtnList1_container" runat="server">
            <asp:DropDownList
                CssClass="form-control"
                ID="ddlistPpl"
                runat="server">
                <asp:ListItem Value="Person">All People</asp:ListItem>
                <asp:ListItem Value="FootballPlayer">Footballplayer</asp:ListItem>
                <asp:ListItem Value="BasketballPlayer">Basketballplayer</asp:ListItem>
                <asp:ListItem Value="HandballPlayer">Handballplayer</asp:ListItem>
                <asp:ListItem Value="Physio">Physio</asp:ListItem>
                <asp:ListItem Value="Trainer">Trainer</asp:ListItem>
                <asp:ListItem Value="Referee">Referee</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button runat="server" Text="OK" OnCommand="ddlistPplBn_Ok" CssClass="btn btn-info" ID="ddlistPplBn"></asp:Button>
    </div>
    <br />
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">Games</div>
        <br />
        <div class="panel-body">
            <asp:GridView ID="tblPpl"
                CssClass="table"
                runat="server"
                AllowPaging="true"
                PagerSettings-Mode="NextPrevious"
                PageSize="5"
                PagerSettings-PageButtonCount="2"
                DataKeyNames="id"
                OnPageIndexChanging="tblPpl_PageIndexChanging"
                OnRowUpdating="tblPpl_RowUpdating"
                OnRowCancelingEdit="tblPpl_RowCancelingEdit"
                OnRowDeleting="tblPpl_RowDeleting"
                AutoGenerateDeleteButton="true"
                AutoGenerateEditButton="true"
                OnRowEditing="tblPpl_RowEditing"
                AutoGenerateColumns="true">
                <Columns>
                </Columns>
                <EditRowStyle CssClass="GridRowEditStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
            <div runat="server" class="container" id="editTourn" visible="true">
                <div id="addNewGame" visible="true" runat="server">
                    <div class="h4">
                        Add new Player
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="newPrsnTypeListDD" Text="Type">
                        </asp:Label>
                        <asp:DropDownList
                            CssClass="form-control"
                            ID="newPrsnTypeListDD"
                            runat="server">
                            <asp:ListItem Value="FootballPlayer">Footballplayer</asp:ListItem>
                            <asp:ListItem Value="BasketballPlayer">Basketballplayer</asp:ListItem>
                            <asp:ListItem Value="HandballPlayer">Handballplayer</asp:ListItem>
                            <asp:ListItem Value="Physio">Physio</asp:ListItem>
                            <asp:ListItem Value="Trainer">Trainer</asp:ListItem>
                            <asp:ListItem Value="Referee">Referee</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="nameU" Text="Name"></asp:Label>
                        <asp:TextBox runat="server" ID="nameU" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="surnameU" Text="Surname"></asp:Label>
                        <asp:TextBox runat="server" ID="surnameU" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="activeU" Text="Active"></asp:Label>
                        <asp:CheckBox runat="server" ID="activeU" CssClass="form-check-input"></asp:CheckBox>
                    </div>
                    <div class="form-group">
                        <br />
                        <asp:Label runat="server" AssociatedControlID="ageU" Text="Age"></asp:Label>
                        <asp:TextBox TextMode="Number" runat="server" ID="ageU" CssClass="form-control"></asp:TextBox>
                    </div>
                    <br />
                    <asp:Button ID="btnAdd" OnCommand="btnAdd_Command" runat="server" Text="Save" CssClass="btn btn-info" />
                    <div id="exportButtons" runat="server">
                        <hr class="my-4">
                        <asp:Button ID="exportTeamXML" OnCommand="Export_CommandXML" CssClass="btn btn-secondary" Text='export XML' runat="server"></asp:Button>
                        <asp:Button ID="exportTeamJSON" OnCommand="Export_CommandJSON" CssClass="btn btn-secondary" Text='export JSON' runat="server"></asp:Button>
                    </div>
                </div>
            </div>
            <br />
        </div>
</asp:Content>
