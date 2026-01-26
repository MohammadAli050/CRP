<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" Inherits="Report_RptMajorMinorDeptSetup" Codebehind="RptMajorMinorDeptSetup.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Report Major
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    
    <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="studentProbationList-container">
        <div class="div-margin">
            <div class="loadArea">
                <label class="display-inline field-Title">Program</label>
                <asp:DropDownList ID="ddlProgram" runat="server" class="display-inline field-Title" DataTextField="NameWithCode" DataValueField="ProgramID">
                </asp:DropDownList>

                <label class="display-inline field-Title">Batch</label>
                <asp:DropDownList ID="ddlAcaCalBatch" DataValueField="AcademicCalenderID" DataTextField="FullCode" runat="server" class="display-inline field-Title">
                </asp:DropDownList>

                <label class="display-inline field-Title">Student ID</label>
                <asp:TextBox ID="txtRoll" runat="server" class="display-inline field-Title">  </asp:TextBox>

                <asp:CheckBox ID="chkMajor1" runat="server" Text="Major1" class="display-inline field-Title" Checked="true" />
                <asp:CheckBox ID="chkMajor2" runat="server" Text="Major2" class="display-inline field-Title" />

                 <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
            </div>
        </div>



        <div class="loadArea">
            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
            <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
        </div>

         <div class="loadArea">
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ></asp:Label>
            
        </div>

        <div class="loadArea">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
                <LocalReport ReportPath="miu/registration/report/RptMajorMinor.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
        <div style="clear: both"></div>
    </div>
    <div style="clear: both"></div>
</asp:Content>
