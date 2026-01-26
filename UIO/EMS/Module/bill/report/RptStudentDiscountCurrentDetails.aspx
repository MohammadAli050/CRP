<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptStudentDiscountCurrentDetails" Codebehind="RptStudentDiscountCurrentDetails.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
     <p>Student Current Discount</p>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblAcaCal" runat="server" Text="Semester" Style="width: 60px; display: inline-block;"></asp:Label>
                <asp:DropDownList ID="ddlAcaCalSession" runat="server"    Style="width: 150px;" />

            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Program" Style="width: 60px; display: inline-block;"></asp:Label>
                <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" Style="width: 150px;" 
                     >
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>

            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
               
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnViewBill" runat="server" Text="Load" Width="150" Height="40" OnClick="btnViewBill_Click"></asp:Button>
             
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
        <LocalReport ReportPath="miu/bill/report/StudentDiscounts.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>

