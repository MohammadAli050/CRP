<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultViewConverted.aspx.cs" Inherits="EMS.miu.result.ExamResultViewConverted" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Result View (Converted Mark)</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Exam Result View (Converted Mark)</label>
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
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="450px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <%--<td class="auto-style4">
                            <b>Section :</b>
                        </td>--%>
                        <%--<td class="auto-style6">
                            <asp:DropDownList ID="ddlAcaCalSection" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlAcaCalSection_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>--%>
                    </tr>               
                </table>
                <table>
                    <td class="auto-style4">
                        <asp:Label runat="server" ID="lblExam" Font-Bold="true" Text="Exam : " Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbxAllSelect" Text="Select All" OnCheckedChanged="cbxSelectAll_Checked" AutoPostBack="true" Visible="false"/>
                        <asp:CheckBoxList runat="server" ID="cblExamList" RepeatDirection="Horizontal" RepeatColumns="6" Width="900px"></asp:CheckBoxList>
                        <asp:Label ID="lblExamTemplateBasicItemId" Visible="false" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Visible="false" />
                    </td>
                </table>
                
            </div>
            <br />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>&nbsp;</div>
    <rsweb:ReportViewer ID="ResultView" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="47%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
</asp:Content>