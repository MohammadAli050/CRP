<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_Menu" Codebehind="Menu.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    Menu
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
	<style type="text/css">
	    .style1 {
	        width: 481px;
	    }

	    .style2 {
	    }

	    .style3 {
	    }

	    .style4 {
	        width: 82px;
	    }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
     <div class="PageTitle">
            <label>Menu Editor</label>
        </div>
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
                    <td class="td" align="left" style="vertical-align: top; text-align: left; border: 1px solid black" valign="top">
                        <asp:Panel ID="pnlMaster" runat="server" Width="487px" >
                            <table style="width:100%; height: 357px;">   
                                                                  
                                <tr >
                                    <td class="td">
                                        <asp:Button ID="btnAddRoot" runat="server" BackColor="#FF6600" 
                                            BorderColor="#FF9933" onclick="btnAddRoot_Click" 
                                            Text="Add Root" Width="77px" CssClass="button" />
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnAdd" runat="server" BackColor="#FF6600" 
                                            BorderColor="#FF9933" onclick="btnAdd_Click" Text="Add" 
                                            Width="77px" CssClass="button" />
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnEdit_Click" style="height: 26px" CssClass="button" 
                                            Height="16px" />
                                    </td>
                                    <td class="td">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                            BackColor="#FF6600" BorderColor="#FF9933" 
                                            onclick="btnDelete_Click" CssClass="button" />
                                    </td>
                                </tr>   
                                     
                                        
                                            
                                                 <tr>
                                                     <td align="left" class="td" colspan="4" style="vertical-align: top; text-align: left" valign="top">
                                                         <asp:Panel ID="pnlGRd" runat="server" Height="487px" ScrollBars="Vertical">
                                                             <asp:TreeView ID="tvwMenus" runat="server" ImageSet="BulletedList" LineImagesFolder="~/TreeLineImages" onselectednodechanged="tvwMenus_SelectedNodeChanged" ShowExpandCollapse="False" ShowLines="True">
                                                                 <ParentNodeStyle Font-Bold="False" />
                                                                 <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                                 <SelectedNodeStyle Font-Underline="True" ForeColor="Lime" HorizontalPadding="0px" VerticalPadding="0px" />
                                                                 <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                                             </asp:TreeView>
                                                         </asp:Panel>
                                                     </td>
                                                 </tr>
                                
                                
                                
                            </table>
                        </asp:Panel>
                    </td>
                    <td rowspan="2" class="td" style="vertical-align: top; text-align: left ; border: 1px solid black"">
                        <asp:Panel ID="pnlDetail" runat="server" Height="241px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="td" align="left" style="width: 82px">
                                        <asp:Label ID="lblName" runat="server" Text="Name" ForeColor="#FF6600"></asp:Label>
                                    </td>
                                    <td align="left" class="td">
                                        <asp:TextBox ID="txtName" runat="server" ValidationGroup="vgMenu" Width="345px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                                            ControlToValidate="txtName" ErrorMessage="Name can not be empty" 
                                            ToolTip="Name can not be empty" ValidationGroup="vgMenu">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" align="left" style="width: 82px">
                                        <asp:Label ID="lblURL" runat="server" Text="URL" ForeColor="#FF6600"></asp:Label>
                                    </td>
                                    <td align="left" class="td">
                                        <asp:TextBox ID="txtURL" runat="server" Height="68px" Width="348px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" colspan="2" align="left">
                                        <asp:CheckBox ID="chkSysAdmnAcs" runat="server" ForeColor="#FF6600" 
                                            Text="System Admin Accessible" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" colspan="2">
                                        <asp:Panel ID="pnlSvClos" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnSave" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Save" Width="77px" 
                                                            onclick="btnSave_Click" ValidationGroup="vgMenu" CssClass="button" />
                                                    </td>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnCancel" runat="server" BackColor="#FF6600" 
                                                            BorderColor="#FF9933" Text="Cancel" Width="77px" 
                                                            onclick="btnCancel_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:ValidationSummary ID="vsMenu" runat="server" Height="64px" 
                                            ValidationGroup="vgMenu" />
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

