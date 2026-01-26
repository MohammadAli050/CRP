<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ForgetPassword" Codebehind="ForgetPassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Forget Password</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function ValidationField() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_txtUserID').val() == '') {
                $('#MainContainer_lblMsg').text('User Id Please');
                $('.warningUserID').css('color', 'red');
                return false;
            }
            else { $('.warningUserID').css('color', 'black'); }

            if ($('#MainContainer_txtEmail').val() == '') {
                $('#MainContainer_lblMsg').text('Email Address Please');
                $('.warningEmail').css('color', 'red');
                return false;
            }
            else {
                var valid = isValidEmailAddress($('#MainContainer_txtEmail').val());
                if (valid)
                    $('.warningEmail').css('color', 'black');
                else {
                    $('#MainContainer_lblMsg').text('Wrong Email Format');
                    $('.warningEmail').css('color', 'red');
                    return valid;
                }
            }
        }

        function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
            return pattern.test(emailAddress);
        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Forget Password</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="ForgetPassword-container">
            <div>
                <img src="../../Images/one.png" class="imgList" />
            </div>
            <div class="div-margin">
                <label class="display-inline title-flag"><b>Please Contact CITS</b></label>
            </div>
            <div>
                <img src="../../Images/two.png" class="imgList" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">User ID</label>
                <sup class="warningUserID">*</sup>
                <asp:TextBox runat="server" ID="txtUserID" class="margin-zero input-Size" placeholder="User ID" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Email</label>
                <sup class="warningEmail">*</sup>
                <asp:TextBox runat="server" ID="txtEmail" class="margin-zero input-Size" placeholder="E-mail" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title"></label>
                <asp:Button runat="server" ID="btnSend" class="margin-zero btn-size" Text="Get New Password" OnClick="btnSend_Click" OnClientClick="return ValidationField();" />
            </div>
        </div>
    </div>
</asp:Content>

