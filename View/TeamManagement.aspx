<%@ Page Language="C#" AutoEventWireup="true" Title="Team" MasterPageFile="~/Base.Master" CodeBehind="TeamManagement.aspx.cs" Inherits="Tournament_Management.View.WebForm1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="TeamManagement" runat="server">
        <div class="jumbotron">
          <h1 class="display-4">Team Management</h1>
          <p class="lead">Under construction</p>
          <hr class="my-4">
          <p>...</p>
        </div>
               <div id="form1" runat="server">
                <div class="container-fluid">
                </div>
                   <br />
                   <br />
                <div class="panel panel-info">
                     <div class="panel-heading">Teams</div>
<%--<asp:Table ID="tblPeople" CssClass="table table-striped" runat="server"></asp:Table>--%>
                </div>
                <div>
                    <div class="container">
                     <asp:Placeholder id="editPerson" runat="server"></asp:Placeholder>
                    </div>
                </div>
            </div>

    </asp:Content>

