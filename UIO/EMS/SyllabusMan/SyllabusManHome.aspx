<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_SyllabusManHome" Codebehind="SyllabusManHome.aspx.cs" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
   Curriculum Mgt.
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblErr" runat="server"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

