<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="EMS.Admin.UserPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">User Password</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
     
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>User Password</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>

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
                    
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Login ID</b></label>
                        <asp:TextBox runat="server" ID="txtUserId"></asp:TextBox>
                        <asp:Button runat="server" ID="btnGetPassword" Text="Get Password" OnClick="btnGetPassword_Click" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Password</b></label>
                        <asp:TextBox runat="server" ID="txtPassword"/>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
