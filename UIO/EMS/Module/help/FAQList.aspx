<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="FAQList" Codebehind="FAQList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>

