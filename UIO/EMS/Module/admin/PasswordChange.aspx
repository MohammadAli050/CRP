<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PasswordChange" CodeBehind="PasswordChange.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Password Change</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function ValidationField() {
            document.getElementById('<%=lblMsg.ClientID%>').innerHTML = '';

            var mobileNo = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            var passVal = document.getElementById('<%=txtPassword.ClientID%>').value;
            var confPassVal = document.getElementById('<%=txtRetypePassword.ClientID%>').value;
             
            if (passVal == '') {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Password Please !';
                document.getElementById('<%=lblPassWarning.ClientID%>').style.color = 'Red';

                return false;
            }
            else {
                document.getElementById('<%=lblPassWarning.ClientID%>').style.color = 'Green';
            }

            if (confPassVal == '') {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Retype Password Please !';
                document.getElementById('<%=lblConfPassWarning.ClientID%>').style.color = 'Red';

                return false;
            }
            else {
                document.getElementById('<%=lblConfPassWarning.ClientID%>').style.color = 'Green';

            }

            if (passVal != confPassVal) {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Password Does not MATCH !';
                document.getElementById('<%=lblConfPassWarning.ClientID%>').style.color = 'Red';
                return false;
            }

            if (passVal.length != 6) {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Password Length MUST be 6 !';
                document.getElementById('<%=lblPassWarning.ClientID%>').style.color = 'Red'; 
                return false;
            }
            else {
                document.getElementById('<%=lblPassWarning.ClientID%>').style.color = 'Green';
                document.getElementById('<%=lblConfPassWarning.ClientID%>').style.color = 'Green';
            }

            var re  = /^(\+88)01[1-9]\d{8}$/;
            if (re.test(mobileNo)) {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = '';
            }
            else {
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Mobile No Format is Wrong!';
                return false;
            }

        } 

    </script>
    <style>
        .glow_text {
            animation: glow .5s infinite alternate;
        }

        @keyframes glow {
            to {
                text-shadow: 0 0 1px Red;
            }
        }

        .glow_text {
            font-family: sans-serif;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Password Change</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label class="glow_text" runat="server" ID="lblMsg" Text="" Font-Bold="true" ForeColor="Red" />
        </div>

        <div class="PasswordChange-container">
            <div class="div-margin">
                <label class="display-inline field-Title">Name</label>
                <asp:Label runat="server" ID="lblName" class="display-inline field-Title-Fix" Text="..............................................." />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Registration No</label>
                <asp:Label runat="server" ID="lblRegistration" class="display-inline field-Title-Fix" Text="..............................................." />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">ID</label>
                <asp:Label runat="server" ID="lblID" class="display-inline field-Title-Fix" Text="..............................................." />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Mobile No(Format: +8801XXXXXXXXX)</label>
                <asp:TextBox runat="server" ID="txtMobileNo" class="margin-zero input-Size" placeholder="Mobile Number" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtMobileNo" ErrorMessage="Wrong Format"                    
                    ValidationExpression="^(\+88)01[1-9]\d{8}$" ValidationGroup="check">
                 
                </asp:RegularExpressionValidator>
            </div>

            <div class="div-margin">
                <label class="display-inline field-Title">New Password</label>
                <asp:TextBox runat="server" ID="txtPassword" class="margin-zero input-Size" placeholder="Password" TextMode="Password" />
                <asp:Label class="glow_text" ID="lblPassWarning" runat="server" Font-Bold="true" Font-Size="Medium" Text="*"></asp:Label>
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Confirm Password</label>
                <asp:TextBox runat="server" ID="txtRetypePassword" class="margin-zero input-Size" placeholder="Confirm Password" TextMode="Password" />
                <asp:Label class="glow_text" ID="lblConfPassWarning" runat="server" Font-Bold="true" Font-Size="Medium" Text="*"></asp:Label>
            </div>

            <div class="div-margin">
                <label class="display-inline field-Title"></label>
                <asp:Button runat="server" ID="btnUpdate" validationgroup="check" class="margin-zero btn-size" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return ValidationField(this);" />
            </div>
        </div>
    </div>
</asp:Content>

