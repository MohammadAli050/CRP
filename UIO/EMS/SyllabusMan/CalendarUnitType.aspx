<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_CalendarUnitType" Codebehind="CalendarUnitType.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
            .style8
            {
                width: 424px;
                height: 27px;
            }
            .style9
            {
                height: 27px;
            }
            .style10
            {
                border: 0px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 16px;
                font-variant: small-caps;
                line-height: 24px;
                color: #333333;
                vertical-align: Middle;
                height: 24px;
                width: 150px;
            }
            .style12
            {
                border: 1px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                line-height: 24px;
                color: #333333;
                vertical-align: Middle;
                height: 27px;
                width: 424px;
            }
            .style13
            {
                border: 0px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 13px;
                font-weight: bold;
                line-height: 24px;
                color: #4f4a46;
                vertical-align: Middle;
                height: 27px;
                width: 120px;
            }
        .style14
        {
            height: 27px;
            width: 139px;
        }
        .style15
        {
            height: 27px;
            width: 140px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:965px; height:480px; margin-top:10px; margin-left:auto; margin-right:auto" >
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px";>

                        <table style="width: 965px; height:30px;">
							<tr>
								<td class="style10">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46" 
                                        Width="93%">Calendar Type</asp:label>
								</td>
								<td class="td" style="height: 24px">
								    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="100%"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>
                <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                    <td class="td" valign="top" align="left" style="vertical-align:top;">
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="400px" 
                            Width="483px">
                            <table style="border: 0px solid Gray; width: 483px; ">
                                <tr style="border: 0px solid Gray;">
                                    <td class="td" colspan="2" style="border: 0px solid Gray; height: 27px;">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="94%"></asp:TextBox>
                                    </td>
                                    <td align="center" valign="middle" class="td" style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" Width="80"
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 0px solid Gray;">
                                    <td align="center" valign="middle"
                                        style="border: 1px solid Gray; " class="style15">
                                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid Gray; " class="style14">
                                        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="Edit" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" Width="80"
                                            Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:0px;" class="tr">
                                    <td class="td" colspan="3" style="border: 0px solid Gray; vertical-align:top; height: 362px;">
                                        <asp:panel ID="pnlGrid" runat="server"  ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="457px" BorderColor="#336666" BorderStyle="solid" BorderWidth="1px"
                                                GridLines="Horizontal" ForeColor="#333333">
												<RowStyle Height="24px" ForeColor="#333333" BackColor="White" />
												<Columns>
													<asp:commandfield ShowSelectButton="True">
														<ControlStyle Width="40px" />
														<FooterStyle Width="40px" />
														<HeaderStyle Width="40px" />
														<ItemStyle Width="40px" />
													</asp:commandfield>
													<asp:boundfield DataField="TypeName" HeaderText="Name" />
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
												</Columns>
												<FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
												<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
												<SelectedRowStyle BackColor="#57b846" Font-Bold="True" ForeColor="#333333" 
                                                    Height="24px" />
												<HeaderStyle BackColor="#57b846" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
											    <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
											</asp:gridview>
										</asp:panel>
									</td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </td>
                	<td style="border: 0px solid Gray;" valign="top" class="td">
					    <asp:panel ID="pnlClass" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" 
                            Enabled="False" Height="400px" Width="476px">
						    <table style="width: 476px; ">
							    <tr style="border-style:solid; border-color:Gray; border-width:0px;">
								    <td align="left" class="style13">
								        Calendar Name &nbsp;</td>
								    <td align="left" 
                                        style="border: 0px solid Gray; " 
                                        class="style12">
								        <asp:DropDownList ID="ddlParent" runat="server" Height="20px" Width="162px">
                                        </asp:DropDownList>
								    </td>
								    <td align="center" style="border: 0px solid Gray; height: 27px;" class="td">
								        &nbsp;</td>
							    </tr>
                                
							    <tr>
								    <td align="left" class="td" style="width: 93px; height: 27px;font-weight: bold; color: #4f4a46; font-size: 13px;">
								        Type Name</td>
								    <td align="left" style="border: 0px solid Gray; " class="style8">
								        <asp:TextBox ID="txtName" runat="server" TabIndex="7" Width="55%"></asp:TextBox>
								    </td>
								    <td align="right" 
                                        style="border: 0px solid Gray; " class="style9">
								        <asp:RequiredFieldValidator ID="rvName" runat="server" 
                                            ControlToValidate="txtName" ErrorMessage="Name can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								    </td>
							    </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
							    <tr>
								    <td class="style13" style="border: 1px solid gray" valign="middle" align="center">
								        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="80"
                                            ValidationGroup="vdSave" CssClass="button" />
                                    </td>
								    <td style="height: 27px; border: 1px solid gray;" align="center" class="td" colspan="2" valign="middle">
								        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Width="80"
                                            Text="Cancel" CssClass="button" />
								    </td>
							    </tr>
							    
						    </table>
					    </asp:panel>
					</td>
                </tr>            

                <tr>
                    <td ;="" align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:0px">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

