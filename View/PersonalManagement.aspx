<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalManagement.aspx.cs" Inherits="Tournament_Management.View.PersonenVerwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>People Management</h1>
            <asp:RadioButtonList ID="rdbtnList1" runat="server">
                <asp:ListItem>All People</asp:ListItem>
                <asp:ListItem>Footballplayer</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Font-Bold="True" Font-Size="Medium" OnClick="btnConfirm_Click"/>
            <asp:Table ID="tblPeople" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>