<%@ Page Language="C#" AutoEventWireup="true" Title="Login" CodeBehind="Login.aspx.cs" Inherits="Tournament_Management.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid email format.
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    void OnLoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
    {
        if (!IsValidEmail(Login1.UserName))
        {
            Login1.InstructionText = "Enter a valid email address.";
            Login1.InstructionTextStyle.ForeColor = System.Drawing.Color.RosyBrown;
            e.Cancel = true;
        }
        else
        {
            Login1.InstructionText = String.Empty;
        }
    }

    void OnLoginError(object sender, EventArgs e)
    {
        Login1.HelpPageText = "Help with logging in...";
        Login1.PasswordRecoveryText = "Forgot your password?";
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - Tournament Management</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <div class="container-fluid">

        <form id="loginForm" runat="server">
            <table style="text-align: center; border: 1">
                <tr>
                    <td align="center">
                        <asp:Login ID="Login1" runat="server"
                            BorderPadding="20"
                            CssClass="form-group"
                            Orientation="Vertical"
                            TextLayout="TextOnTop"
                            HelpPageUrl="help.aspx"
                            PasswordRecoveryUrl="help.aspx"
                            UserNameLabelText="Email address:&nbsp;&nbsp;"
                            OnLoggingIn="OnLoggingIn"
                            OnAuthenticate="OnAuth"
                            OnLoginError="OnLoginError">
                            <TextBoxStyle CssClass="form-control" />
                            <LabelStyle CssClass="text-left" />
                            <FailureTextStyle CssClass="form-control" />
                            <HyperLinkStyle CssClass="link-info" />
                            <LoginButtonStyle CssClass="btn btn-info" />
                        </asp:Login>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>