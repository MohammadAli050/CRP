<%@ Page Language="C#" AutoEventWireup="true" Inherits="OfferedCourseCount" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" Codebehind="OfferedCourseCount.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Offered Course Count</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
     <div style="float: left; margin-top:10px;" >        
            <div style="float: left; padding:5px;" >
                <div style="float: left">
                    <div style="float: left">
                        <label style="margin-right:5px;">Program</label>
                    </div>
                    <div style="float: left">
                        <uc1:ProgramUserControl class="display-inline field-Title" runat="server" ID="ucProgram" />
                    </div>
                    <asp:Button ID="loadButton" runat="server" Text="Load" OnClick="loadButton_Click"  Height="30px" Width="70px" BackColor="#edd366"/>
                </div>
                <br />
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        
        <div style="width: 900px; margin-top:15px;">
            <br />
            <br />
            <rsweb:ReportViewer ID="rvOfferedCourseCount" runat="server" Width="100%">
                <LocalReport ReportPath="miu/registration/report/OfferedCourseCount.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
         <div style="clear: both"></div>
    </div>
    <div style="clear: both"></div>
</asp:Content>
