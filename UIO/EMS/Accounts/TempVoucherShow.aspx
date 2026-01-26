<%@ Page Language="C#" MasterPageFile="~/MasterPage/Accounts/Accounts.master" AutoEventWireup="true" Inherits="Accounts_Default" Title="Untitled Page" Codebehind="TempVoucherShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSource1" EnableModelValidation="True">
    <Columns>
        <asp:BoundField DataField="Prefix" HeaderText="Prefix" 
            SortExpression="Prefix" />
        <asp:BoundField DataField="SLNO" HeaderText="SLNO" 
            SortExpression="SLNO" />
        <asp:BoundField DataField="DrAccountHeadsID" 
            HeaderText="DrAccountHeadsID" SortExpression="DrAccountHeadsID" />
        <asp:BoundField DataField="CrAccountHeadsID" 
            HeaderText="CrAccountHeadsID" SortExpression="CrAccountHeadsID" />
        <asp:BoundField DataField="Amount" HeaderText="Amount" 
            SortExpression="Amount" />
        <asp:BoundField DataField="CourseID" HeaderText="CourseID" 
            SortExpression="CourseID" />
        <asp:BoundField DataField="VersionID" HeaderText="VersionID" 
            SortExpression="VersionID" />
        <asp:BoundField DataField="Remarks" HeaderText="Remarks" 
            SortExpression="Remarks" />
        <asp:BoundField DataField="ReferenceNo" HeaderText="ReferenceNo" 
            SortExpression="ReferenceNo" />
        <asp:BoundField DataField="ChequeNo" HeaderText="ChequeNo" 
            SortExpression="ChequeNo" />
        <asp:BoundField DataField="ChequeBankName" HeaderText="ChequeBankName" 
            SortExpression="ChequeBankName" />
        <asp:BoundField DataField="ChequeDate" HeaderText="ChequeDate" 
            SortExpression="ChequeDate" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Connection String %>" 
    
    
    
        SelectCommand="SELECT [Prefix], [SLNO], [DrAccountHeadsID], [CrAccountHeadsID], [Amount], [CourseID], [VersionID], [Remarks], [ReferenceNo], [ChequeNo], [ChequeBankName], [ChequeDate] FROM [Voucher]">
</asp:SqlDataSource>
</asp:Content>

