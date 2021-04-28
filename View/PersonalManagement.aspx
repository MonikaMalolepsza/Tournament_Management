<%@ Page Language="C#" AutoEventWireup="true" Title="People" MasterPageFile="~/Base.Master" CodeBehind="PersonalManagement.aspx.cs" Inherits="Tournament_Management.View.PersonalManagement" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="PersonalManagement" runat="server">
        <div class="jumbotron">
          <h1 class="display-4">Personal Management</h1>
          <p class="lead"> Can't find your favourite Player? Say no more!</p>
          <hr class="my-4">
          <p>Here you can add new players to your team, update the details and delete the inactive members.</p>
        </div>
               <div runat="server">
                <div class="container-fluid">

                    <asp:RadioButtonList 
                        CssClass="form-check vertical-center" 
                        ID="rdbtnList1"  
                        runat="server">
                        <asp:ListItem>&nbsp;All People</asp:ListItem>
                        <asp:ListItem>&nbsp;Footballplayer</asp:ListItem>
                        <asp:ListItem>&nbsp;Basketballplayer</asp:ListItem>
                        <asp:ListItem>&nbsp;Handballplayer</asp:ListItem> 
                        <asp:ListItem>&nbsp;Physio</asp:ListItem>
                        <asp:ListItem>&nbsp;Trainer</asp:ListItem>
                        <asp:ListItem>&nbsp;Referee</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Button ID="btnConfirm" runat="server" Text="Show me the players!" CssClass="btn btn-info" />
                </div>
                   <br />
                   <br />
                <div class="panel panel-info">
                     <div class="panel-heading">Members</div>
                        <asp:Table ID="tblPeople" CssClass="table table-striped" runat="server"></asp:Table>
                </div>
                <div>
                    <div class="container">
                         <asp:Button ID="btnAdd" OnCommand="btnAdd_Click" Visible="false" runat="server" Text="Add New Member" CssClass="btn btn-secondary" />
                        <br />
                         <div id="form1" runat="server"></div>
                        <br />
                        <asp:Button ID="btnSubmit" OnCommand="btnSubmit_Click" Visible="false" runat="server" Text="Save" CssClass="btn btn-secondary" />
                    </div>
                </div>
            </div>

    </asp:Content>

