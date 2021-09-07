﻿<%@ Page Language="C#" AutoEventWireup="true" Title="Team" MasterPageFile="~/Base.Master" CodeBehind="TeamManagement.aspx.cs" Inherits="Tournament_Management.View.TeamManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TeamManagement" runat="server">
    <div class="jumbotron">
        <span>
            <a runat="server" href="~/#">
                <img class="img-responsive"
                    src="../Static/Images/baseball.png"
                    height="20"
                    style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 20%;"
                    width="20" alt="people" />
            </a>
        </span>
        <h1 class="display-4">Team Management</h1>
        <p class="lead">Manage your favourite teams</p>
        <hr class="my-4">
        <p>...</p>
    </div>
    <div class="container-fluid">
    </div>
    <br />
    <br />
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">Teams</div>
        <br />
        <div class="panel-body">
            <asp:GridView
                CssClass="table"
                runat="server"
                ID="teamGrid"
                AllowPaging="true"
                PagerSettings-Mode="NextPrevious"
                PageSize="5"
                OnPageIndexChanging="teamGrid_PageIndexChanging"
                PagerSettings-PageButtonCount="2"
                DataKeyNames="id"
                OnRowUpdating="teamGrid_RowUpdating"
                OnRowCancelingEdit="teamGrid_RowCancelingEdit"
                OnRowDeleting="teamGrid_RowDeleting"
                AutoGenerateDeleteButton="true"
                AutoGenerateEditButton="true"
                OnRowEditing="teamGrid_RowEditing"
                AutoGenerateColumns="true">
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
</asp:Content>