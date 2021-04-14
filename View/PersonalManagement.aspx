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
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Font-Bold="True" Font-Size="Medium" OnClick="btnConfirm_Click" />
            <asp:Table ID="tblPeople" runat="server"></asp:Table>
            <asp:Button ID="btnDelete" runat="server" Text="Delete selected" OnClick="btnDelete_Click"/>
        </div>
        <div>
            <asp:Table ID="tblInput" runat="server" Enabled="false">
                <asp:TableRow>
                    <asp:TableCell Text="Name"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Text="Surname"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Text="Goals"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtGoals" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Text="Speed"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtSpeed" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Text="Age"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Text="Active"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtActive" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Enabled="false" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
