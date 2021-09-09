<%@ Page Language="C#" AutoEventWireup="true" Title="Tournaments" MasterPageFile="~/Base.Master" CodeBehind="TournamentManagement.aspx.cs" Inherits="Tournament_Management.View.TournamentManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TournamentManagement" runat="server">
    <div class="jumbotron">
        <div class="row">
            <div class="col-md-4">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/sport.png"
                        style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 50%;"
                        alt="people" />
                </a>
            </div>
            <div class="col-md-4">
                <a runat="server" href="~/#">
                    <img class="img-responsive"
                        src="../Static/Images/team.png"
                        style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 50%;"
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
        <asp:DropDownList ID="ddlTour2" DataSource='<%# Controller.TypeList %>' DataTextField="Value" DataValueField="Key" CssClass="form-control" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Button runat="server" Text="OK" OnCommand="tourButton_Command" CssClass="btn btn-info" ID="tourButton"></asp:Button>
        <br />
        <br />
        <div class="panel panel-default">
            <div class="panel-heading">Tournaments</div>
            <br />
            <div class="panel-body">
                <asp:GridView ID="tblTournament" CssClass="table" runat="server">
                    <EditRowStyle CssClass="GridRowEditStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewPagerStyle" />
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="btnSubmit" OnCommand="btnSubmit_Click" Visible="true" runat="server" Text="Save" CssClass="btn btn-success" />
            <div runat="server" id="editTourn" visible="true">
                <div class="h4">
                    Add new Tournament
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label runat="server" AssociatedControlID="addNewT" Text="Discipline">
                        </asp:Label>
                        <asp:DropDownList runat="server" DataTextField="Value" DataValueField="Key" CssClass="form-control" DataSource='<%# Controller.TypeList %>' ID="addNewT">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label runat="server" AssociatedControlID="nameT" Text="Name"></asp:Label>
                        <asp:TextBox runat="server" ID="nameT" CssClass="form-control"></asp:TextBox>
                        <br />
                        <asp:Button runat="server" ID="SaveNewT" CssClass="btn btn-secondary" Text="Add" OnCommand="SaveNewI_Command" />
                    </div>
                </div>
                <hr class="my-4">
                <div id="editMembersTournaments" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label
                                runat="server"
                                AssociatedControlID="MembersFront"
                                Text="Members"></asp:Label>
                            <asp:ListBox
                                CssClass="form-control"
                                ID="MembersFront"
                                SelectionMode="Single"
                                DataSource='<%# Members %>'
                                runat="server"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <div class="btn-group" role="group">
                                <asp:Button ID="Button2" CssClass="btn btn-secondary" Text="<" OnClick="RemoveBtn_Click" runat="server" />
                                <asp:Button ID="Button3" CssClass="btn btn-secondary" Text=">" OnClick="AddBtn_Click" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-5">
                            <asp:Label
                                runat="server"
                                AssociatedControlID="CandidatesFront"
                                Text="Candidates"></asp:Label>
                            <asp:ListBox
                                SelectionMode="Single"
                                CssClass="form-control"
                                ID="CandidatesFront"
                                DataSource='<%# Candidates %>'
                                runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="btnAdd" OnCommand="btnAdd_Submit" runat="server" Text="Save" CssClass="btn btn-info" />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>