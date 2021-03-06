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
    <br />
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">Tournaments</div>
        <br />
        <div class="panel-body">
            <asp:GridView ID="tblTournament"
                CssClass="table"
                runat="server"
                AllowPaging="true"
                PagerSettings-Mode="NextPrevious"
                PageSize="5"
                OnPageIndexChanging="tblTournament_PageIndexChanging"
                PagerSettings-PageButtonCount="2"
                DataKeyNames="id"
                OnRowUpdating="tblTournament_RowUpdating"
                OnRowCancelingEdit="tblTournament_RowCancelingEdit"
                OnRowDeleting="tblTournament_RowDeleting"
                AutoGenerateDeleteButton="true"
                AutoGenerateEditButton="true"
                OnRowEditing="tblTournament_RowEditing"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="name"
                        ReadOnly="false"
                        HeaderText="Name" />
                    <asp:TemplateField HeaderText="Teams">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlist" DataSource='<%# Bind("Teams") %>' DataTextField="Name" DataValueField="Id" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="labelAdd" Text='Add tournaments below' runat="server">
                            </asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discipline">
                        <ItemTemplate>
                            <asp:Label ID="tourType" Text='<%# typeConverter(Eval("Type"))%>' runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="tournamentTypes" DataSource='<%# Controller.TypeList %>' DataTextField="Value" DataValueField="Key" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:CheckBox ID="tourActive" runat="server" CssClass="form-check-input" Checked='<%# Eval("Active") %>' Enabled="false"></asp:CheckBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="tourActive" runat="server" CssClass="form-check-input" Checked='<%# Bind("Active") %>' Enabled="true"></asp:CheckBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Games">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="gotoGames" CssClass="btn btn-light" Text="Games" OnCommand="gotoGames_Command" CommandArgument="" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRowEditStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
            <div runat="server" id="editTourn" visible="true">
                <div class="row" id="addNewTournament" visible="true" runat="server">
                    <div class="panel-body">
                        <div class="h4">
                            Add new Tournament
                        </div>
                    </div>
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
                        <asp:Button runat="server" ID="SaveNewT" CssClass="btn btn-secondary" Text="Add" OnCommand="SaveNewT_Command" />
                    </div>
                </div>
                <div id="editMembersTournaments" runat="server" visible="false">
                    <hr class="my-4">

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
                    <div class="lead">To save your changes click update.</div>
                    <br />
                </div>
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
