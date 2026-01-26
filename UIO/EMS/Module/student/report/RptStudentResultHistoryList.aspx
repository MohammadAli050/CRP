<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptStudentResultHistoryList" Codebehind="RptStudentResultHistoryList.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        body .main-container .BodyContent-wrapper
        {
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    
    <asp:Label ID="Label1" runat="server" Text="Enter Student ID :"></asp:Label>
    <asp:TextBox ID="txtStudentID" runat="server" Style="margin-left:5px;"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Load" style=" cursor: pointer; margin-left: 30px;" OnClick="btnStudentCGPAList"/>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width:500px; height:20px; margin-top: 10px;">
                <asp:Label ID="lblMessage" runat="server" Text="" style ="font:bold 13px arial; margin-left:50px;"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" sizetoreportcontent="true">
                    <LocalReport ReportPath="miu/student/report/RptStudentResultHistoryList.rdlc">
                    </LocalReport>
    </rsweb:reportviewer>
        
</asp:Content>

