<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptStudentClassRoutine" Codebehind="RptStudentClassRoutine.aspx.cs" %>
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
    <asp:Label ID="Label1" runat="server" Text="Student ID :"></asp:Label>
    <asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="Trimester"></asp:Label>
    <asp:DropDownList class="dropdownList-size" ID="ddlAcaCalBatch" runat="server" />
    <asp:Button ID="Button1" runat="server" style="margin-top:5px; margin-left: 65px;" Text="Generate Class Routine" Height="30px" OnClick="btnGenerateStudentClassRoutine" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width:500px; height:20px; margin-top: 10px;">
                <asp:Label ID="lblMessage" runat="server" Text="" style ="font:bold 13px arial; margin-left:50px; "></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" sizetoreportcontent="true">
                    <LocalReport ReportPath="miu/student/report/RptStudentClassRoutine.rdlc">
                    </LocalReport>
    </rsweb:reportviewer>
</asp:Content>

