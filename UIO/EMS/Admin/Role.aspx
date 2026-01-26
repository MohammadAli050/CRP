<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_Role" Codebehind="Role.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    Role
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function Client_OnTreeNodeChecked()
        {
            javascript:__doPostBack('tvwMenu','');
        }
    </script>
    
    <style type="text/css">
        .style1
        {
            border: 1px solid #DF5900;
            font: 11px Arial, Helvetica, sans-serif;
            color: #FF6600;
            vertical-align: Middle;
            width: 369px;
        }
    
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
        .style2
        {
            border: 1px solid #DF5900;
            font: 11px Arial, Helvetica, sans-serif;
            color: #FF6600;
            vertical-align: Middle;
            width: 298px;
        }
        .style3
        {
            border: 1px solid #DF5900;
            font: 11px Arial, Helvetica, sans-serif;
            color: #FF6600;
            vertical-align: Middle;
            height: 16px;
            width: 84px;
        }
        </style>
    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
            <label>Role Editor</label>
        </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:900px;">
                <tr>
                    <td colspan="2" align="center" class="td" style="width: 890px">
                                    
                        <asp:Label ID="lblErr" runat="server"></asp:Label>
                                    
                    </td>
                </tr>
                <tr>
                    <td class="style2" style="vertical-align: top; text-align: left;" valign="top">
                        <asp:Panel ID="pnlMaster" runat="server" Width="390px" Height="356px">
                            <table style="width: 390px; height: 30px;" >
                                <tr>
                                    <td class="td">
                                        
                                        <asp:TextBox ID="txtSearchParam" runat="server" Width="289px"></asp:TextBox>
                                        
                                    </td>
                                    <td class="td" style="width: 80px" align="center">
                                        <asp:Button ID="btnFind" runat="server" Text="Find" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnFind_Click" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 301px; height: 326px">
                                <tr>
                                    <td class="td" style="width: 79px; height: 16px" align="center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnAdd_Click" CssClass="button" />
                                    </td>
                                    <td class="td" style="width: 79px; height: 16px" align="center">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnEdit_Click" CssClass="button" />
                                    </td>
                                    <td class="style3" align="center" style="visibility:hidden;">
                                        <asp:Button ID="btnCopy" runat="server" Text="Copy" 
                                            BackColor="#FF6600" BorderColor="#FF9933" CssClass="button" 
                                            onclick="btnCopy_Click" />
                                    </td>
                                    <td class="td" style="width: 79px; height: 16px" align="center"  style="visibility:hidden;">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnDelete_Click" CssClass="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="left" class="td" valign="top" style="vertical-align: top; text-align: left">
                                        <asp:GridView ID="gdvRoles" runat="server" BackColor="LightGoldenrodYellow" 
                                            BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" 
                                            AutoGenerateColumns="False" Width="319px" Height="73px">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                                </asp:CommandField>
                                                <asp:BoundField HeaderText="Role Name" DataField="RoleName" 
                                                    HeaderStyle-Width="250px" ItemStyle-Width="90px">
                                                    <HeaderStyle Width="250px" />
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" 
                                                    HeaderStyle-Width="10px" ItemStyle-Width="10px" >
                                                    <HeaderStyle Width="10px" />
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="Tan" />
                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                                HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#FF9999" ForeColor="GhostWhite" />
                                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="style2" align="left" style="vertical-align: top; text-align: left" valign="top">
                        <asp:Panel ID="pnlDetails" runat="server">
                            <table style="width:400px;">
                                <tr>
                                    <td class="style1" align="left">
                                        Role Name</td>
                                    <td align="left" class="td" style="width: 300px">
                                        <asp:TextBox ID="txtRole" runat="server" MaxLength="50" Width="235px" 
                                            Height="18px"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="revRoleName" runat="server" 
                                            ControlToValidate="txtRole" 
                                            ErrorMessage="Please enter only alphabates or alpha numeric values" 
                                            ToolTip="Please enter only alphabates or alpha numeric values" 
                                            ValidationExpression="[0-9]*[a-z, A-Z]+[0-9]*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr class="td">
                                    <td class="style1" align="left">
                                        Session Timeout when user is inactive</td>
                                    <td align="left" class="td" style="width: 300px">
                                        <asp:TextBox ID="txtSessionTime" runat="server" MaxLength="50" Width="78px" 
                                            Height="16px"></asp:TextBox>
                                        &nbsp;sec&nbsp;
                                        <asp:RegularExpressionValidator ID="revSessionTime" runat="server" 
                                            ControlToValidate="txtSessionTime" ErrorMessage="Please enter only numbers" 
                                            ValidationExpression="[1-9]+[0-9]*" 
                                            ToolTip="Please enter only numbers like 1, 10, 20 etc">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>                                
                                
                                <tr>
                                    <td class="td" colspan="2" align="left" style="width: 400px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" class="td" style="width: 400px">
                                        <asp:TreeView ID="tvwMenu" runat="server" ShowCheckBoxes="All" 
                                            ImageSet="BulletedList" ShowExpandCollapse="False" onclick="Client_OnTreeNodeChecked();"
                                            ontreenodecheckchanged="tvwMenu_TreeNodeCheckChanged" ShowLines="True" LineImagesFolder="~/TreeLineImages">
                                            <ParentNodeStyle Font-Bold="False" />
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <SelectedNodeStyle Font-Underline="True" ForeColor="Lime" 
                                                HorizontalPadding="0px" VerticalPadding="0px" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                                HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                        </asp:TreeView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="td">
                                        <asp:Panel ID="pnlSvClos" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnSave" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Save" 
                                                            onclick="btnSave_Click" CssClass="button" />
                                                    </td>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnCancel" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Cancel" 
                                                            onclick="btnCancel_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
