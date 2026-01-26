<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DiscountContinuationSetup.aspx.cs" Inherits="EMS.BasicSetup.DiscountContinuationSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:980px; height:580px" >
                <tr>
                    <td align="left" style="border: 1px solid Gray; height:25px">

                        <table style="width: 980px">
							<tr>
								<td class="td"style="width: 180px;height: 25px">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="208%">Discount Continuation Setup Info</asp:label>
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
                        <asp:Panel ID="pnlSelectArea" runat="server">
                            <table>
                                <tr>
                                    <td style="height: 25px">
                                        <table>
                                            <tr>
                                                <td style="width: 180px;height: 25px">
                                                    <asp:Label ID="Label1" runat="server" Text="Academic Calender" Height = "25px" Width="100%"></asp:Label>
                                                </td>
                                                <td  style="height: 25px; width:171px">
                                                   <asp:DropDownList ID="cboAcaCalender" runat="server" Height="25px" 
                                                          Width="171px" TabIndex="1">
                                                    </asp:DropDownList>     
                                                </td>
                                                <td style="width: 100px;height: 25px"> 
                                                    <asp:Label ID="Label3" runat="server" Text="Program" Width = "100%" height="25px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboProgram" runat="server" Height="25px" 
                                                          Width="171px" TabIndex="2">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                                <td style="width: 180px;height: 25px">
                                                    <asp:Button ID="btnAdd" runat="server" Height="25px" Text="Add" Width="105px" 
                                                        onclick="btnAdd_Click" TabIndex="3" />
                                                </td>
                                                <td style="width: 180px;height: 25px">
                                                    <asp:Button ID="btnView" runat="server" Height="25px" onclick="btnView_Click" 
                                                        TabIndex="5" Text="View" Width="105px" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                        <asp:Panel ID="pnlDataArea" runat="server" Height="400px">
                        <table>                            
                            <tr>
                                <td style="height: 25px">
                                    <table>
                                        <tr>
                                            <td style="height: 25px">
                                                <asp:Label ID="Label2" runat="server" Text="Discount Type" Height="25px"></asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="ddlTypeDefinition" runat="server" Width = "100px" 
                                                    Height="25px" TabIndex="6">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:Label ID="Label4" runat="server" Text="Minimum Credits" Height="25px"></asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtMinCredits" runat="server" Height="25px" TabIndex="7"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:Label ID="Label5" runat="server" Text="Maximum Credits" Height="25px"></asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtMaxCredits" runat="server" Height="25px" TabIndex="8"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:Label ID="Label6" runat="server" Text="Minimum CGPA" Height="25px"></asp:Label></td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtCGPA" runat="server" Height="25px" TabIndex="9"></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:Button ID="btnAddNew" runat="server" Height="25px" Text="Add" 
                                                    Width="105px" onclick="btnAddNew_Click" TabIndex="10" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSelect" runat="server" AutoGenerateColumns="False" 
                                        Width="100%" onrowdatabound="gvSelect_RowDataBound" TabIndex="11" 
                                        onrowediting="gvSelect_RowEditing" 
                                        onrowupdating="gvSelect_RowUpdating" 
                                        onrowcancelingedit="gvSelect_RowCancelingEdit" 
                                        onrowdeleting="gvSelect_RowDeleting">
                                        <Columns>
                                            <asp:CommandField InsertText="" SelectText="" ShowDeleteButton="True" 
                                                ShowEditButton="True" NewText="" >
                                                <ControlStyle Width="50px" />
                                                <FooterStyle Width="50px" />
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" >
                                                <HeaderStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Typedefinitionid" HeaderText="TypeDefID" 
                                                Visible="False" >
                                                <HeaderStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Discount Name">
                                                <ControlStyle Width="60px" />
												<FooterStyle Width="60px" />
												<HeaderStyle Width="60px" />
												<ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MinCredits" HeaderText="Minimum Credits" >
                                                <ControlStyle Width="60px" />
												<FooterStyle Width="60px" />
												<HeaderStyle Width="60px" />
												<ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MaxCredits" HeaderText="Maximum Credits" >
                                                <ControlStyle Width="60px" />
												<FooterStyle Width="60px" />
												<HeaderStyle Width="60px" />
												<ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MinCGPA" HeaderText="Minimum CGPA" >
                                                <ControlStyle Width="60px" />
												<FooterStyle Width="60px" />
												<HeaderStyle Width="60px" />
												<ItemStyle Width="60px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" Height="25px" 
                                                    Width="105px" onclick="btnSave_Click" TabIndex="12" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="25px" 
                                                    Width="105px" TabIndex="13" onclick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                
                            </tr>
                        </table>
                        </asp:Panel> 
                    </td>
                </tr>
                <tr>
                    <td style="border-style: solid; border-color: Gray; border-width: 1px; height:25px;" 
                        align="left">
                        <asp:ValidationSummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>  
			</table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>