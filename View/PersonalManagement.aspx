<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalManagement.aspx.cs" Inherits="Tournament_Management.View.PersonenVerwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <div class="pricing-header p-3 pb-md-4 mx-auto text-center">
    <h1 class="display-4 fw-normal">People Management</h1>
        <p class="fs-5 text-muted">Quickly build an effective pricing table for your potential customers with this Bootstrap example. It’s built with default Bootstrap components and utilities with little customization.</p>
    </div>
        <form id="form1" runat="server">
        <div class="container">
            <asp:RadioButtonList CssClass="form-check" ID="rdbtnList1" runat="server">
                <asp:ListItem>&nbsp;All People</asp:ListItem>
                <asp:ListItem>&nbsp;Footballplayer</asp:ListItem>
                <asp:ListItem>&nbsp;Basketballplayer</asp:ListItem>
                <asp:ListItem>&nbsp;Handballplayer</asp:ListItem> 
                <asp:ListItem>&nbsp;Physio</asp:ListItem>
                <asp:ListItem>&nbsp;Trainer</asp:ListItem>
                <asp:ListItem>&nbsp;Referee</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-info" OnClick="btnConfirm_Click" />
        </div>
        <div class="container">
            <asp:Table ID="tblPeople" CssClass="table table-striped" runat="server"></asp:Table>
            <asp:Button ID="btnDelete" Visible="false" CssClass="btn btn-secondary" runat="server"  Text="Delete selected" OnClick="btnDelete_Click"/>
        </div>
        <div>
            <div class="container">
             <asp:Placeholder id="tblInput" runat="server"></asp:Placeholder>
             <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CssClass="btn btn-secondary" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
    <script src="../Scripts/jquery-4.3.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
</body>
</html>
