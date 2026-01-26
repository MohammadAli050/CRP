<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="EMS.BasicSetup.BillableCourse" Codebehind="BillableCourse.aspx.cs" %>
<%--<%@ Register src="../UserControls/Uc_AcademicCalender.ascx" tagname="Uc_AcademicCalender" tagprefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:980px; height:300px" >
                <tr>
                    <td align="left" style="border: 1px solid Gray; height:25px">

                        <table style="width: 980px">
							<tr>
								<td class="td"style="width: 180px;height: 25px">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="208%">Billable Course Info</asp:label>
								</td>
								<td class="td" style="height: 25px; width:800px">
								    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="100%" 
                                        Height="16px"></asp:label>
								</td>
							</tr>
							</table>
					   </td>					   						
                </tr>
                <tr>
                    <td style="height: 25px">
                        <table>
                            <tr>
                                <td style="width: 180px;height: 25px">
                                    <asp:Label ID="Label3" runat="server" Text="Academic Calender" Width = "160%"></asp:Label>
                                </td>
                                <td  style="height: 25px; width:171px">
                                   <asp:DropDownList ID="cboAcaCalender" runat="server" Height="25px" 
                                          Width="171px" AutoPostBack="True" 
                                        onselectedindexchanged="cboAcaCalender_SelectedIndexChanged">
                                    </asp:DropDownList>     
                                </td>
                                <td style="width: 80px;height: 25px">
                                    <asp:Label ID="Label1" runat="server" Text="Program" Width = "100%"></asp:Label>
                                </td>
                                <td  style="height: 25px; width:171px">
                                   <asp:DropDownList ID="cboProgram" runat="server" Height="25px" 
                                          Width="171px" AutoPostBack="True" 
                                        onselectedindexchanged="cboProgram_SelectedIndexChanged">
                                    </asp:DropDownList>     
                                </td>
                                <td style = "width:100px;height:25px;">
                                    <lable>Non Credit Courses</lable>
                                </td>
                                <td style = "height:25px;width:170px;">
                                    <asp:DropDownList runat = "server" ID = "DDLNonCreditCorses"
                                         AutoPostBack = "true" 
                                        OnSelectedIndexChanged = "DDLNonCreditCourses_SelectedIndexChanged" 
                                        Height="25px" Width="170px">
                                    </asp:DropDownList>
                                </td>
                            </tr>                       
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                        <table>
                            <tr>                                            
                                <td style="width: 180px;height: 25px">
                                    <asp:Button ID="btnAdd" runat="server" Height="25px" Text="Add" Width="105px" 
                                        onclick="btnAdd_Click" />
                                </td>
                                <td style="width: 180px;height: 25px">
                                    <asp:Button ID="btnEdit" runat="server" Height="25px" Text="Edit" 
                                        Width="105px" onclick="btnEdit_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Height="25px" Text="View" 
                                        Width="105px" onclick="btnView_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 25px">
                        <asp:Panel ID="pnlDataArea" runat="server">
                        <table>
                            <tr>
                                <td style="width: 180px;height: 25px">
                                    <asp:Label ID="Label2" runat="server" Text="Bill Start From Retake No"></asp:Label>
                                </td>
                                <td style="width: 180px;height: 25px">
                                    <asp:TextBox ID="txtBillStartFrom" runat="server" Height="25px"></asp:TextBox>
                                    <asp:TextBox ID="txtID1" runat="server" Visible="False" Width="16px">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsCredit" runat="server" Height="25px" 
                                        Text="For NON-Credit Course" Enabled="False" />
                                </td>
                            </tr>                                                    
                            <tr>
                                <td style="width: 180px;height: 25px"><asp:Label ID="Label" runat="server" Text="Bill Start From Retake No"></asp:Label></td>
                                            <td style="width: 180px;height: 25px">
                                                <asp:TextBox ID="txtBillStart2" runat="server" Height="25px"></asp:TextBox>
                                                <asp:TextBox ID="txtID2" runat="server" Visible="False" Width="16px">0</asp:TextBox>
                                </td>                                        
                                            <td>
                                                <asp:CheckBox ID="chkIsCredit2" Text = "For Credit Course" runat="server" 
                                                        Height="25px" Enabled="False" /></td>
                                        
                                
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="25px" 
                                            Width="105px" onclick="btnSave_Click" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="25px" 
                                            Width="105px" onclick="btnCancel_Click" /></td>
                                </tr>
                        </table>
                        </asp:Panel> 
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px"></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:ValidationSummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>  
			</table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>