<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptStudentRoadmap" Codebehind="RptStudentRoadmap.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Student Roadmap
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
    <div style="padding: 5px; width: 1200px;">
        <div class="PageTitle">
            <label>Student Roadmap</label>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label7" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" style ="font:bold 13px arial; color:red; margin-left:50px;" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Enter Student ID : "></asp:Label>
                                <asp:TextBox ID="txtStudentID" runat="server" Style="margin-left:5px;"></asp:TextBox>                           
                                <asp:Button ID="Button1" runat="server" Text="Load" style=" cursor: pointer; margin-left: 30px;" OnClick="btnStudentRoadmap"/>
                            </td >
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label2" runat="server" Text="Stdent Name : "></asp:Label> 
                                <b><asp:Label ID="lblName" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Program : "></asp:Label> 
                                <b><asp:Label ID="lblProgram" runat="server" Text=""></asp:Label></b>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Batch:"></asp:Label> 
                                <b><asp:Label ID="lblBatch" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>&nbsp;</div>
        <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="65%" Height="100%" sizetoreportcontent="true" >
            <LocalReport ReportPath="miu/student/report/RptStudentRoadmap.rdlc">
            </LocalReport>
        </rsweb:reportviewer>
    </div>
</asp:Content>

