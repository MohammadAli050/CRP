<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ActiveStudentSummary" Codebehind="ActiveStudentSummary.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <p>Total Active Student</p>
     
    <asp:GridView ID="Active_Student_View" runat="server" Height="68px"  Width="264px" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Program" HeaderText="Program Name" />
            <asp:BoundField DataField="TotalStudent" HeaderText="Total Students" ReadOnly="True" SortExpression="TotalStudent" />
         
        </Columns>
    </asp:GridView>

     
    <br />
    Total = <asp:Label ID="totalCount" runat="server" Text=""></asp:Label>

     
</asp:Content>

