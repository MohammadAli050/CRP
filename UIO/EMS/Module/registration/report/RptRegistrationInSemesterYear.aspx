<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptRegistrationInSemesterYear" CodeBehind="RptRegistrationInSemesterYear.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <p>Registration Summary :: Session Wise</p>
    <table>

        <tr>
            <td><b>Year</b></td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            <td><b>Session</b></td>
            <td>
                <asp:DropDownList ID="ddlSession" runat="server">
                    <asp:ListItem Text="Spring" Value="0" />
                    <asp:ListItem Text="Summer" Value="1" />
                    <asp:ListItem Text="Fall" Value="2" />
                </asp:DropDownList></td>
            <td class="auto-style9">&nbsp;</td>
            <td>
                <asp:Button ID="btnRegistrationInSemesterYear" runat="server" Text="Load" Width="150" OnClick="GetRegistrationInSemesterYear_Click"></asp:Button>

            </td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <center>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
        <LocalReport ReportPath="miu\registration\report\RptRegistrationInSemesterYear.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
    </center>

</asp:Content>
