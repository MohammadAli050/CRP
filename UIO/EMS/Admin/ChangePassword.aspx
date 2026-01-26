<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ChangePassword" Codebehind="ChangePassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td  valign="middle" align="center" style="width:100%; height:500px;">
                <asp:ChangePassword ID="chpswdAdmin" runat="server" BackColor="#FFFBD6" 
                    BorderColor="#FFDFAD" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
                    Font-Names="Verdana" Font-Size="Medium" 
                    oncancelbuttonclick="chpswdAdmin_CancelButtonClick" 
                    onchangingpassword="chpswdAdmin_ChangingPassword" 
                    ChangePasswordFailureText="" 
                    
                    ConfirmPasswordCompareErrorMessage="The Confirmed New Password must match the New Password entry." 
                    Height="206px" Width="394px" 
                    onchangedpassword="chpswdAdmin_ChangedPassword">
                    <CancelButtonStyle BackColor="White" BorderColor="#CC9966" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" />
                    <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
                    <ContinueButtonStyle BackColor="White" BorderColor="#CC9966" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#990000" />
                    <ChangePasswordButtonStyle BackColor="White" BorderColor="#CC9966" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#990000" />
                    <TitleTextStyle BackColor="#990000" Font-Bold="True" Font-Size="0.9em" 
                        ForeColor="White" />
                    <TextBoxStyle Font-Size="0.8em" />
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                </asp:ChangePassword>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblChngPwd" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

