<%@ Page Language="C#" AutoEventWireup="true" Title="Ranking" MasterPageFile="~/Base.Master" CodeBehind="Ranking.aspx.cs" Inherits="Tournament_Management.View.Ranking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ranking" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Ranking</h1>
        <p class="lead">Overview page</p>
        <hr class="my-4">
    </div>
    <div id="form1" runat="server">
        <div class="container-fluid">
        </div>
        <br />
        <br />
        <br />
        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTour"></asp:DropDownList>
        <br />
        <asp:Button runat="server" Text="OK" CssClass="btn btn-success" ID="tourButton"></asp:Button>
        <br />
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">Tournaments</div>
            <br />
            <asp:GridView ID="tblRanking" CssClass="table" runat="server"></asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>