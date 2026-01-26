<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Transcript" 
    MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" Codebehind="Transcript.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Auto Assign Course</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        Enter Roll:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="rollBox" runat="server"></asp:TextBox>
        <asp:Button ID="loadButton" runat="server" Text="Load" OnClick="loadButton_Click" />
    </div>
    <div style="width: 600px">
        <br />
        <br />
        <rsweb:ReportViewer ID="Transcript" runat="server" Width="100%">
            <LocalReport ReportPath="miu/registration/report/Transcript.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
