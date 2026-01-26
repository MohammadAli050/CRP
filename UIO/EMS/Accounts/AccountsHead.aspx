<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Accounts_AccountsHead" Codebehind="AccountsHead.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">Account Head</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
	<style type="text/css">
        .style2
        {
        }
        .style3
        {
            border: 1px solid Green;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            width: 56px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
	        <table style="width:100%; height: 577px;">
                <tr>
                    <td colspan="2" class="td">
                        <asp:Label ID="lblMenu" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" class="td">
                        <asp:Label ID="lblErr" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" style="vertical-align: top; text-align: left" valign="top">
                        <asp:Panel ID="pnlMaster" runat="server" Width="487px" BorderColor="Orange" BorderWidth="1px">
                            <table style="width:100%; height: 357px;">
                                <tr>
                                    <td class="td">
                                        <asp:Button ID="btnAddRoot" runat="server" BackColor="#FF6600" 
                                            BorderColor="#FF9933" onclick="btnAddRoot_Click" 
                                            Text="Add Root" Width="77px" CssClass="button" />
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnAdd" runat="server" BackColor="#FF6600" 
                                            BorderColor="#FF9933" onclick="btnAdd_Click" Text="Add Node" 
                                            Width="77px" CssClass="button" />
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnEdit_Click"  CssClass="button"/>
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnDelete_Click" CssClass="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="left" class="td" style="vertical-align: top; text-align: left" valign="top">
                                        <asp:Panel ID="pnlGRd" runat="server" Height="487px" ScrollBars="Vertical">
                                            <asp:TreeView ID="tvwAccountsHead" runat="server" ImageSet="BulletedList" 
                                                ShowExpandCollapse="False" 
                                                onselectednodechanged="tvwAccountsHead_SelectedNodeChanged" 
                                                ShowLines="True" LineImagesFolder="~/TreeLineImages">
                                                <ParentNodeStyle Font-Bold="False" />
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <SelectedNodeStyle Font-Underline="True" ForeColor="Lime" 
                                                    HorizontalPadding="0px" VerticalPadding="0px" />
                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                                    HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                            </asp:TreeView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td rowspan="2" class="td" style="vertical-align: top; text-align: left">
                        <asp:Panel ID="pnlDetail" runat="server" Height="241px" BorderWidth="1px" BorderColor="Orange">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style3" align="left">
                                        Account&nbsp; Name</td>
                                    <td align="left" class="td">
                                        <asp:TextBox ID="txtName" runat="server" ValidationGroup="vgMenu" Width="345px"></asp:TextBox>
                                    
                                    </td>
                                    <td align="left" class="td"> 
                                        <asp:RequiredFieldValidator ID="rvAcName" runat="server" 
                                            ControlToValidate="txtName" ErrorMessage="Account Name can not be left blank" 
                                            ValidationGroup="vgSave">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3" align="left">
                                        Account&nbsp; Code</td>
                                    <td align="left" class="td">
                                        &nbsp;</td>
                                    <td align="left" class="td"> </td>
                                </tr>
                                <tr>
                                    <td class="style3" align="left">
                                        Account Tag</td>
                                    <td align="left" class="td">
                                        <asp:DropDownList ID="ddlTag" runat="server" Width="257px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="td"> </td>
                                </tr>
                                <tr>
                                    <td class="style3" align="left">
                                        Remarks</td>
                                    <td align="left" class="td">
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="68px" Width="348px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="td"> </td>
                                </tr>
                                <tr>
                                    <td class="td" colspan="2" align="left">
                                        <asp:CheckBox ID="chkIsLeaf" runat="server" Text="Is Leaf" />
                                        &nbsp;<asp:CheckBox ID="chkSysMandatory" runat="server" Text="Sys Mandatory" />
                                    </td>
                                        <td align="left" class="td"> </td>
                                </tr>
                                
                                <tr>
                                    <td class="style2" colspan="2">
                                        <asp:Panel ID="pnlSvClos" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnSave" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Save" Width="77px" 
                                                            onclick="btnSave_Click" ValidationGroup="vgSave" CssClass="button" />
                                                    </td>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnCancel" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Cancel" Width="77px" 
                                                            onclick="btnCancel_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:ValidationSummary ID="vsAcount" runat="server" Height="64px" 
                                          ShowMessageBox="false"  ValidationGroup="vgSave" />
                                    </td>
                                    <td align="left" class="td"> </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


