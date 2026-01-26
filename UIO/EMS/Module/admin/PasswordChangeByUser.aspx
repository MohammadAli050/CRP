<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PasswordChangeByUser" Codebehind="PasswordChangeByUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Password Change By User</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function ValidationField() {
            $('#ctl00_MainContainer_lblMsg').text('');

            if ($('#ctl00_MainContainer_txtCurrentPassword').val() == '') {
                $('#ctl00_MainContainer_lblMsg').text('Current Password Please');
                $('.warningCurrent').css('color', 'red');
                return false;
            }
            else { $('.warningCurrent').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtPassword').val() == '') {
                $('#ctl00_MainContainer_lblMsg').text('New Password Please');
                $('.warningPassword').css('color', 'red');
                return false;
            }
            else { $('.warningPassword').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtRetypePassword').val() == '') {
                $('#ctl00_MainContainer_lblMsg').text('Retype New Password Please');
                $('.warningRetypePassword').css('color', 'red');
                return false;
            }
            else { $('.warningRetypePassword').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtPassword').val() != $('#ctl00_MainContainer_txtRetypePassword').val()) {
                $('#ctl00_MainContainer_lblMsg').text('Password Does not MATCH');
                return false;
            }
            if ($('#ctl00_MainContainer_txtPassword').val().length < 8) {
                $('#ctl00_MainContainer_lblMsg').text('Password Length MUST be 8');
                return false;
            }
        }

        function AfterUpdatePanelWork() {
            
            function ValidationField() {
                $('#ctl00_MainContainer_lblMsg').text('');

                if ($('#ctl00_MainContainer_txtCurrentPassword').val() == '') {
                    $('#ctl00_MainContainer_lblMsg').text('Current Password Please');
                    $('.warningCurrent').css('color', 'red');
                    return false;
                }
                else { $('.warningCurrent').css('color', 'black'); }

                if ($('#ctl00_MainContainer_txtPassword').val() == '') {
                    $('#ctl00_MainContainer_lblMsg').text('New Password Please');
                    $('.warningPassword').css('color', 'red');
                    return false;
                }
                else { $('.warningPassword').css('color', 'black'); }

                if ($('#ctl00_MainContainer_txtRetypePassword').val() == '') {
                    $('#ctl00_MainContainer_lblMsg').text('Retype New Password Please');
                    $('.warningRetypePassword').css('color', 'red');
                    return false;
                }
                else { $('.warningRetypePassword').css('color', 'black'); }

                if ($('#ctl00_MainContainer_txtPassword').val() != $('#ctl00_MainContainer_txtRetypePassword').val()) {
                    $('#ctl00_MainContainer_lblMsg').text('Password Does not MATCH');
                    return false;
                }
                if ($('#ctl00_MainContainer_txtPassword').val().length < 6) {
                    $('#ctl00_MainContainer_lblMsg').text('Password Length MUST be 6');
                    return false;
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Password Change (User)</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(AfterUpdatePanelWork);
                </script>

                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="PasswordChangeByUser-container">
                    <div class="div-margin">
                        <label class="display-inline field-Title">Current(Password)</label>
                        <sup class="warningCurrent">*</sup>
                        <asp:TextBox runat="server" ID="txtCurrentPassword" class="margin-zero input-Size" placeholder="Current Password" TextMode="Password" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title">Type(New Password)</label>
                        <sup class="warningPassword">*</sup>
                        <asp:TextBox runat="server" ID="txtPassword" class="margin-zero input-Size" placeholder="New Password" TextMode="Password" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title">Retype(Password)</label>
                        <sup class="warningRetypePassword">*</sup>
                        <asp:TextBox runat="server" ID="txtRetypePassword" class="margin-zero input-Size" placeholder="Retype Password" TextMode="Password" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title"></label>
                        <asp:Button runat="server" ID="btnUpdate" Text="Update" class="margin-zero btn-size" OnClick="btnUpdate_Click" OnClientClick="return ValidationField();" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

