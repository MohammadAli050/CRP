<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="AdmittedStudentCount.aspx.cs" Inherits="EMS.miu.AdmittedStudentCount" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

<%--<span>From : <asp:DropDownList ID="ddlStartingSession" runat="server"></asp:DropDownList></span>
<span>To : <asp:DropDownList ID="ddlEndingSession" runat="server"></asp:DropDownList></span>--%>
    <p>
        Admitted Student Count Summary 
    </p>
    <span>
        <asp:Button ID="Button1" runat="server" Text="Load Report" OnClick="ButtonLoad_Click" /> </span>

    <div>
        <asp:Label ID="lblMessage" Text="" runat="server"></asp:Label><br />
        <%--<asp:TextBox runat="server" ID="TextBox" Width="700"></asp:TextBox>--%>
        <br />
        <rsweb:ReportViewer ID="RptAdmittedStudentCount" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
