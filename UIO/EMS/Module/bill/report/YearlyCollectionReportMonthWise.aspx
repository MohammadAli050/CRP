<%@ Page Language="C#" MasterPageFile="~/MIUReports.master" AutoEventWireup="true" CodeFile="YearlyCollectionReportMonthWise.aspx.cs" Inherits="TEST_MIUReports_YearlyCollectionReportMonthWise" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Yearly Collection Report (Month wise)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">

     
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="Server">
   <h2> Yearly Collection Report (Month wise) </h2>
     

    <br />Choose Program: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddListProgramID" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddListProgramID_SelectedIndexChanged">
    </asp:DropDownList>&nbsp;&nbsp;Year:&nbsp;
    <asp:DropDownList ID="ddListYear" runat="server">
    </asp:DropDownList>
&nbsp;&nbsp; Semester/Trimester:&nbsp;&nbsp;
    <asp:DropDownList ID="ddListSemester" runat="server" Enabled="False">
    </asp:DropDownList>
&nbsp; &nbsp;<asp:Button ID="RunReport" runat="server" OnClick="RunReport_Click" Text="Run Report" Width="86px" />
    <br />
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" style="margin-right: 4px" Width="792px">
    </rsweb:ReportViewer>
    <br />
&nbsp;
   

    </asp:Content>