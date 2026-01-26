<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PasswordChangeByAdmin" Codebehind="PasswordChangeByAdmin.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Password Change By Admin</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function btnGenerateValidation() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlUser option:selected').val() == '-1') {
                $('#MainContainer_lblMsg').text('Please select User ID');
                return false;
            }
        }

        function btnMailValidation(){
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlUser option:selected').val() == '-1') {
                $('#MainContainer_lblMsg').text('Please select User ID');
                return false;
            }
            else if ($('#MainContainer_lblPassword').val() == '') {
                $('#MainContainer_lblMsg').text('Please Generate Password');
                return false;
            }
            else if ($('#MainContainer_lblEamil').val() == '') {
                $('#MainContainer_lblMsg').text('This User Profile has no email address in his data. Please update his profile with E-mail');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Password Change (Admin)</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="PasswordChangeByAdmin-container">
            <div class="div-margin">
                <label class="display-inline field-Title">Search</label>
                <asp:TextBox runat="server" ID="txtUserSearch" class="margin-zero input-Size" placeholder="Search Key" />
                <asp:Button runat="server" ID="btnUserSearch" class="button-margin SearchKey" Text="Search" OnClick="btnUserSearch_Click" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">User</label>
                <asp:DropDownList runat="server" ID="ddlUser" class="margin-zero dropDownList" DataTextField="LogInID" DataValueField="User_ID" OnSelectedIndexChanged="ddlUser_Selected" AutoPostBack="true" >
                </asp:DropDownList>
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Name</label>
                <asp:Label runat="server" ID="lblName" class="display-inline" Text=".............................." />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Email</label>
                <asp:Label runat="server" ID="lblEamil" class="display-inline" Text=".............................." />
            </div>
            <div class="div-margin">
                <asp:Button runat="server" ID="btnGenerate" class="margin-zero btn-size" Text="Generate Password" OnClick="btnGenerate_Click" OnClientClick="return btnGenerateValidation();" />
                <asp:Label runat="server" ID="lblPassword" class="display-inline" Text="" />
            </div>
            <div class="div-margin">
                <asp:Button runat="server" ID="btnMail" class="margin-zero btn-size" Text="Password Mail" OnClick="btnMail_Click" OnClientClick="return btnMailValidation();" />
                <asp:Label runat="server" ID="lblMailStatus" class="display-inline" Text="" />
            </div>
        </div>
    </div>
</asp:Content>

