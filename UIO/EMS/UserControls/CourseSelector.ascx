<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_CourseSelector" Codebehind="CourseSelector.ascx.cs" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<style type="text/css">
    .style1
    {
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:400px;">
            <tr>
                <td class="style1" colspan="3">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
                    </asp:ScriptManagerProxy>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right">
                    <asp:Label ID="lblCode" runat="server" Text="Code"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCode" runat="server" TabIndex="0" Height="24px" OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" 
                TabIndex="1" Height="24px"/>
                </td>
            </tr>
           
            <tr>
                <td class="style1" align="right">
                    <asp:Label ID="lblSelect" runat="server" Text="Select Course"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" 
                Height="24px" onselectedindexchanged="ddlCourse_SelectedIndexChanged" 
                TabIndex="2" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

