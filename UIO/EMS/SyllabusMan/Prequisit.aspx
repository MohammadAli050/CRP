<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_Prequisit" Codebehind="Prequisit.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dxp" %>

<%@ Register assembly="DevExpress.Web.ASPxTreeList.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dxwtl" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 139px;
            border-style:solid; 
	        border-color:Gray; 
	        border-width:1px;
        }
        .style3
        {
            width: 50%;
            height: 340px;
        }
        .style4
        {
            width: 100%;
            height: 23px;
        }
        .style7
        {
            width: 35%;
        }
    </style>
    <script type="text/javascript">
            function isNumber(e) {
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode
            status = charCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please make sure entries are numbers only")
                
                return false
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:944px; ">
                <tr>
                    <td style="border: 1px solid Gray;vertical-align:middle;" colspan="4" class="style4">
                        <table style="width: 100%">
							<tr>
								<td class="td" style="width: 136px">
								<asp:label ID="lblHeader0" runat="server" Font-Bold="True" ForeColor="#000099" Width="64%">Trees</asp:label>
								</td>
								<td class="td">
								<asp:label ID="lblMsg" runat="server" ForeColor="Red" Width="100%"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>            
                <tr>
                    <td style="width:20%;border-style:solid; border-color:Gray; border-width:1px;" 
                        align="right">
                        &nbsp; Select a program:
                    </td>
                    <td style="border: 1px solid Gray;" class="style7">
                        <asp:DropDownList ID="ddlPrograms" AutoPostBack="true" runat="server" Width="98%" 
                            onselectedindexchanged="ddlPrograms_SelectedIndexChanged" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                    <td style="width:20%;border-style:solid; border-color:Gray; border-width:1px;" 
                        align="right">
                        &nbsp; Select a tree:
                    </td>
                    <td style="width:30%;border-style:solid; border-color:Gray; border-width:1px;">
                        <asp:DropDownList ID="ddlTree" AutoPostBack="true" runat="server" Width="98%" 
                            onselectedindexchanged="ddlTree_SelectedIndexChanged" Enabled="False" 
                            TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid Gray; vertical-align:middle; height: 39px;" 
                        colspan="2">
                        <asp:Panel ID="pnlUpperCont" runat="server" Width="100%" Height="98%" 
                            HorizontalAlign="Left">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="width:50%;border-style:solid; border-color:Gray; border-width:1px; height: 39px;vertical-align:middle;" colspan="2">
                        <asp:Label ID="lblCaption" runat="server" Width="100%" Height="98%" 
                            Font-Names="Verdana" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid Gray;" colspan="2" class="style3">
                        <asp:Panel ID="pnlTree" runat="server" Height="99%" ScrollBars="Vertical">
                            <dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server">
                            </dxwtl:ASPxTreeList>
                        </asp:Panel>
                    </td>
                    <td style="border: 1px solid Gray;" colspan="2" class="style3">
                        <asp:Panel ID="pnlControl" runat="server" Height="99%" Visible="False">
                            <table style="width:100%; height: 175px;">                                            
                                <tr>
                                    <td>
                                         <asp:Panel ID="pnlOperator" runat="server" Height="99%" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="style1" align="right">
                                                        <asp:Label ID="lblOperator" runat="server" Text="Select Operator"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <asp:DropDownList ID="ddlOperators" runat="server" AutoPostBack="true" 
                                                            Width="98%" TabIndex="11">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                             </table>
                                         </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlDecide" runat="server" Height="33px">
                                             <table style="width:100%;">
                                                <tr>
                                                    <td align="center" class="style6">
                                                        <asp:RadioButton ID="rbtSetCourse" runat="server" ForeColor="#000066" 
                                                            Text="Operand Course" AutoPostBack="True" 
                                                            oncheckedchanged="rbtSetCourse_CheckedChanged" TabIndex="18" />
                                                    </td>
                                                    <td class="td" align="center">
                                                        <asp:RadioButton ID="rbtSetNode" runat="server" ForeColor="#000066" 
                                                            Text="Operand Node" AutoPostBack="True" 
                                                            oncheckedchanged="rbtSetNode_CheckedChanged" TabIndex="17" />
                                                    </td>
                                                </tr>
                                             </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlCourse" runat="server" Height="33px">
                                             <table style="width:100%;">
                                                <tr>
                                                    <td class="style1" align="right">
                                                        <asp:Label ID="lblCourse" runat="server" Text="Select Course"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="true" 
                                                            Width="98%" 
                                                            TabIndex="12">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                             </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlNode" runat="server">
                                             <table style="width:100%;">
                                                <tr>
                                                    <td class="style1" align="right">
                                                        <asp:Label ID="lblNode" runat="server" Text="Select Node"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <asp:DropDownList ID="ddlNodes" runat="server" AutoPostBack="true" Width="98%" 
                                                            TabIndex="13">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlSavCan" runat="server" Height="99%" Visible="False">
                                            <table style="width:100%;border: 1px solid Gray;">
                                                <tr>
                                                    <td style="width: 139px;">
                                                        <asp:ImageButton ID="butSave" runat="server" ImageUrl="~/ButtonImages/Save.jpg" 
                                                            onclick="butSave_Click" AlternateText="Click this button to save the data on the interface to database" 
                                                            ValidationGroup="ValidateTree" TabIndex="22" />
                                                    </td>
                                                    <td align="right">
                                                        <asp:ImageButton ID="butCancel" runat="server" 
                                                            ImageUrl="~/ButtonImages/Cancel.jpg" onclick="butCancel_Click" 
                                                            AlternateText="Cancel" TabIndex="23" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlRequisiteGrid" runat="server">
                                            <asp:GridView ID="grdRequisites" runat="server" BackColor="White" 
                                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                GridLines="Horizontal" AutoGenerateColumns="False" Width="380px">
                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ItemStyle Width="50px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField HeaderText="Course">
                                                        <HeaderStyle ForeColor="#66CCFF" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Node">
                                                        <HeaderStyle ForeColor="#66CCFF" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Opearator">
                                                        <HeaderStyle ForeColor="#66CCFF" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Id" Visible="False">
                                                        <HeaderStyle ForeColor="#66CCFF" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="Azure" />
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid Gray;vertical-align:middle;" colspan="4" class="style4">
                    
                        <asp:ValidationSummary ID="vsTreeMaster" runat="server" ShowMessageBox="True" 
                            ShowSummary="False" ValidationGroup="ValidateTree" />
                    
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

