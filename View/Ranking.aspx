<%@ Page Language="C#" AutoEventWireup="true" Title="Ranking" MasterPageFile="~/Base.Master" CodeBehind="Ranking.aspx.cs" Inherits="Tournament_Management.View.Ranking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ranking" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="~/#">
                <img class="img-responsive"
                    src="../Static/Images/podium1.png"
                    height="20"
                    style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 20%;"
                    width="20" alt="people" />
            </a>
        </span>
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
        <div class="panel panel-default">
            <div class="panel-heading">Tournaments</div>
            <br />
            <div class="panel-body">
                <asp:GridView ID="tblRanking" CssClass="table" runat="server">
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
        </div>
    </div>
</asp:Content>
