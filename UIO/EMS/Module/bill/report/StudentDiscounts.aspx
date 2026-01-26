<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_StudentDiscounts" Codebehind="StudentDiscounts.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
     <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="studentProbationList-container">
        <div class="div-margin">
            <div class="loadArea">
                <label class="display-inline field-Title">Semester</label>                
                <asp:DropDownList ID="ddlAcaCalSession" runat="server" Style="width: 150px;" />

                <label class="display-inline field-Title">Program</label>                 
                <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" Style="width: 150px;">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>

                 <label class="display-inline field-Title">Batch</label>                
                <asp:DropDownList ID="ddlBatch" runat="server" Style="width: 150px;" />

                 <asp:Button ID="btnViewBill" runat="server" Text="Load" Width="150" Height="40" OnClick="btnViewBill_Click" BackColor="#edd366"/>
            </div>            
        </div>
    </div>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
        <LocalReport ReportPath="miu/bill/report/StudentDiscounts.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>

