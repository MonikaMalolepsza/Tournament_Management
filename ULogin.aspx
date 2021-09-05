<%@ Page Language="C#" AutoEventWireup="true" Title="Login" CodeBehind="ULogin.aspx.cs" Inherits="Tournament_Management.ULogin" %>

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

<body style="margin: 0; padding: 0; background-color: #17a2b8; height: 100vh;">

    <div class="container" style="margin-top: 300px">

        <div>
            <div class="col-md-4 col-md-offset-4">
                <div class="" style="margin-left: 20px; margin-right: 20px;">
                    <div style="align-content: center">
                        <a href="#">
                            <img src="Static/Images/login.png"
                                height="50%"
                                style="display: block; margin-left: auto; margin-right: auto; margin-bottom: 25px; width: 50%;"
                                width="50%" alt="login" />
                        </a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="panel panel-default" style="margin-left: 20px; margin-right: 20px;">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center lead">WELCOME</h3>
                        </div>
                        <div class="panel-body">
                            <form id="loginForm" runat="server">
                                <div align="center">
                                    <asp:Login ID="Login1" runat="server"
                                        TextBoxStyle-Width="100%"
                                        CssClass="form-group"
                                        Orientation="Vertical"
                                        TitleText=""
                                        TextLayout="TextOnTop"
                                        DisplayRememberMe="false"
                                        HelpPageUrl="help.aspx"
                                        PasswordRecoveryUrl="help.aspx"
                                        UserNameLabelText="Email address:"
                                        OnLoggingIn="OnLoggingIn"
                                        OnAuthenticate="OnAuth"
                                        DestinationPageUrl="~/View/Home.aspx"
                                        OnLoginError="OnLoginError">
                                        <TextBoxStyle CssClass="form-control" Width="100%" />
                                        <LabelStyle CssClass="text-center" Width="100%" />
                                        <FailureTextStyle CssClass="form-control" />
                                        <HyperLinkStyle CssClass="link-info" />
                                        <LoginButtonStyle CssClass="btn btn-info" Width="100%" />
                                    </asp:Login>
                                </div>
                            </form>
                            <hr />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>