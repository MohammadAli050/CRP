<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DiagonsticPage.aspx.cs" Inherits="EMS.miu.registration.DiagonsticPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
     <div style="padding: 5px; width: 1200px;">
        <div class="PageTitle">
            <label>Problem Diagonstic</label>
        </div>
        
        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td >
                                <label >Type</label>
                            </td >
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCalenderType"  AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />
                            </td>
                            <td >
                                <label >Session</label>
                            </td >
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSemester" />
                            </td>
                            <td >
                            </td>
                          
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label1" runat="server" Text="Running course but bill is missing. "></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnQueryExecute1" runat="server" CommandArgument="1"   Text="Execute" OnClick="btnQueryExecute_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label2" runat="server" Text="Exam mark, Bill, Result"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnQueryExecute2" runat="server" CommandArgument="2"   Text="Execute" OnClick="btnQueryExecute_Click" />
                            </td>
                        </tr>
                    </table>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvDiagonsticData" runat="server" AllowPaging="false" CssClass="gridCss" CellPadding="4" >
                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" />
                <PagerSettings Mode="NumericFirstLast" />
                <%--<RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />>--%>
                <EmptyDataTemplate>
                    No data found!
                </EmptyDataTemplate>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
