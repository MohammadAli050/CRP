<%@ Page Language="C#" AutoEventWireup="true" Inherits="Security_Default" CodeBehind="LogIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BHPI Login Portal</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="shortcut icon" href="../Images/coverImage.jpg" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600;700&display=swap" rel="stylesheet">

    <script type="text/javascript" src="../JavaScript/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-62797653-1', 'auto');
        ga('send', 'pageview');
    </script>

    <script type="text/javascript">
        function disableBtn(btn) {
            btn.disabled = true;
            btn.value = "Authenticating...";
            btn.style.backgroundColor = "#6c757d";
            btn.style.borderColor = "#6c757d";
            btn.style.cursor = "not-allowed";
            __doPostBack(btn.name, '');
        }
    </script>

    <style>
        :root {
            /* BHPI Brand Colors based on their website */
            --bhpi-green: #006a4e; /* Deep Green */
            --bhpi-light-green: #e8f5e9;
            --text-dark: #333333;
            --text-muted: #666666;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Open Sans', 'Segoe UI', Tahoma, sans-serif;
            background-color: #f4f6f9;
            background-image: url('https://www.transparenttextures.com/patterns/cubes.png'); /* Subtle texture */
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login-wrapper {
            width: 100%;
            max-width: 900px; /* Wider for potential side-by-side layout if needed, but keeping centered box */
            padding: 15px;
            display: flex;
            justify-content: center;
        }

        .login-box {
            width: 100%;
            max-width: 450px;
            background: white;
            border-radius: 8px;
            box-shadow: 0 4px 25px rgba(0, 0, 0, 0.08);
            overflow: hidden;
            border-top: 5px solid var(--bhpi-green); /* Top Green Border like BHPI site */
        }

        /* Header Styling */
        .login-header {
            background: white;
            padding: 30px 30px 10px 30px;
            text-align: center;
            border-bottom: 1px solid #eee;
        }

        .logo-container img {
            max-width: 120px;
            height: auto;
            margin-bottom: 15px;
        }

        .login-header h1 {
            font-size: 1.25rem;
            font-weight: 700;
            color: var(--bhpi-green);
            margin-bottom: 5px;
            text-transform: uppercase;
            line-height: 1.4;
        }

        .login-header p {
            font-size: 0.9rem;
            color: var(--text-muted);
            font-weight: 600;
        }

        /* Body Styling */
        .login-body {
            padding: 30px 40px;
        }

        .login-title {
            text-align: center;
            margin-bottom: 25px;
        }

        .login-title h2 {
            font-size: 1.2rem;
            color: var(--text-dark);
            font-weight: 600;
        }

        /* Form Controls */
        .form-group {
            margin-bottom: 20px;
            position: relative;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            color: var(--text-dark);
            font-weight: 600;
            font-size: 0.9rem;
        }

        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            font-size: 0.95rem;
            transition: border-color 0.2s, box-shadow 0.2s;
            background-color: #fcfcfc;
        }

        .form-control:focus {
            outline: none;
            border-color: var(--bhpi-green);
            box-shadow: 0 0 0 3px rgba(0, 106, 78, 0.15);
            background-color: white;
        }

        /* Checkbox */
        .form-check {
            display: flex;
            align-items: center;
            margin-bottom: 25px;
        }

        .form-check input[type="checkbox"] {
            width: 16px;
            height: 16px;
            margin-right: 8px;
            accent-color: var(--bhpi-green);
            cursor: pointer;
        }

        .form-check-label {
            color: var(--text-muted);
            font-size: 0.9rem;
            cursor: pointer;
        }

        /* Button */
        .btn-login {
            width: 100%;
            padding: 12px;
            background: var(--bhpi-green);
            color: white;
            border: 1px solid var(--bhpi-green);
            border-radius: 4px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .btn-login:hover {
            background: #00523c; /* Darker Green */
            border-color: #00523c;
            box-shadow: 0 4px 12px rgba(0, 106, 78, 0.2);
        }

        /* Links */
        .forgot-password {
            text-align: center;
            margin-top: 15px;
        }

        .forgot-password a {
            color: var(--bhpi-green);
            text-decoration: none;
            font-size: 0.9rem;
            font-weight: 500;
        }

        .forgot-password a:hover {
            text-decoration: underline;
        }

        /* Alerts */
        .alert {
            margin-top: 20px;
            padding: 12px;
            border-radius: 4px;
            font-size: 0.9rem;
            text-align: center;
        }
        
        .alert-danger {
            background-color: #fdf2f2;
            color: #d93025;
            border: 1px solid #f8d7da;
        }

        /* Footer */
        .login-footer {
            padding: 15px 30px;
            background: #f9f9f9;
            text-align: center;
            border-top: 1px solid #eee;
        }

        .login-footer p {
            margin: 0;
            font-size: 0.8rem;
            color: #888;
        }

        .login-footer a {
            color: var(--text-dark);
            text-decoration: none;
            font-weight: 600;
        }

        @media (max-width: 480px) {
            .login-header h1 { font-size: 1.1rem; }
            .login-body { padding: 20px; }
        }
    </style>
</head>

<body>
    <form id="frmLogIn" runat="server">
        <div class="login-wrapper">
            <div class="login-box">
                <div class="login-header">
                    <div class="logo-container">
                        <img src="../Images/coverimage.jpg" alt="BHPI Logo" onerror="this.style.display='none'">
                    </div>
                    <h1>Bangladesh Health Professions Institute</h1>
                    <p>(BHPI) Portal</p>
                </div>

                <div class="login-body">
                    <asp:ScriptManager ID="scMgtMas" runat="server" />

                    <asp:UpdatePanel ID="upMain" runat="server">
                        <ContentTemplate>
                            <div class="login-title">
                                <h2>Login</h2>
                            </div>

                            <asp:Login ID="logMain" runat="server" OnAuthenticate="logMain_Authenticate"
                                DisplayRememberMe="True" RememberMeText="Remember Me"
                                RenderOuterTable="False">
                                <LayoutTemplate>
                                    <div class="form-group">
                                        <label for="UserName"><i class="bi bi-person-fill"></i> User ID</label>
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control"
                                            placeholder="Enter your User ID" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="User ID is required." 
                                            ValidationGroup="logMain" Display="Dynamic" ForeColor="#d93025" Font-Size="0.85em">* Required</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="Password"><i class="bi bi-lock-fill"></i> Password</label>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"
                                            CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" ErrorMessage="Password is required." 
                                            ValidationGroup="logMain" Display="Dynamic" ForeColor="#d93025" Font-Size="0.85em">* Required</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-check">
                                        <asp:CheckBox ID="chkRememberMe" runat="server" />
                                        <label class="form-check-label" for="chkRememberMe">Keep me signed in</label>
                                    </div>

                                    <asp:Button ID="Button1" runat="server" CommandName="Login"
                                        Text="Login" CssClass="btn-login" ValidationGroup="logMain" 
                                        OnClientClick="disableBtn(this);" />

                                    <div class="forgot-password">
                                        <a href="#">Forgot Password?</a>
                                    </div>

                                    <div class="alert alert-danger" role="alert" id="FailureText" runat="server"
                                        visible="false" enableviewstate="False">
                                        <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
                                    </div>
                                </LayoutTemplate>
                            </asp:Login>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="login-footer">
                    <p>
                        Copyright &copy; 2026 BHPI. All rights reserved.<br />
                        Developed by <a href="https://www.edusoftconsultants.com/" target="_blank">Edusoft Consultants Ltd</a>
                    </p>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>