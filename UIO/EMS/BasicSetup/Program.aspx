<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_Program" Codebehind="Program.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
            .style7
            {
                border: 0px solid Gray;
                width: 120px;
                height: 27px;
                font-weight: bold;
                color: #4f4a46;
            }
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
                font-weight: bold;
                line-height: 24px;
                color: #333333;
                vertical-align: Middle;
                height: 24px;
                width: 150px;
                font-variant: small-caps;
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
            <table style="border: 1px solid Gray; width:965px; height:530px; margin-top:10px; margin-left:auto; margin-right:auto" >
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px">

                        <table style="width: 965px; height:30px;">
							<tr>
								<td class="style10">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46" Width="93%">Program Info</asp:label>
                                        
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
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="1px" Height="450"
                            Width="483px">
                            <table style="border: 0px solid Gray; width: 483px; ">
                                <tr style="border: 1px solid Gray;">
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
                                                BackColor="White" BorderColor="#336666" BorderStyle="solid" BorderWidth="1px" 
                                                CellPadding="4"  TabIndex="6" Width="457px" 
                                                GridLines="Horizontal">
												<RowStyle ForeColor="#333333" Height="24px" BackColor="White" />
												<Columns>
													<asp:commandfield ShowSelectButton="True">
														<ControlStyle Width="40px" />
														<FooterStyle Width="40px" />
														<HeaderStyle Width="40px" />
														<ItemStyle Width="40px" />
													</asp:commandfield>
													<asp:boundfield DataField="Code" HeaderText="Code" >
														<ControlStyle Width="60px" />
														<FooterStyle Width="60px" />
														<HeaderStyle Width="60px" />
														<ItemStyle Width="60px" />
													</asp:boundfield>
													<asp:boundfield DataField="ShortName" HeaderText="Name" />
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
												</Columns>
												<FooterStyle BackColor="White" ForeColor="#333333" />
												<PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
												<SelectedRowStyle BackColor="#57b846" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
												<HeaderStyle BackColor="#57b846" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
											</asp:gridview>
										</asp:panel>
									</td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </td>
                	<td style="border: 0x solid Gray;" valign="top" class="td">
					    <asp:panel ID="pnlClass" runat="server" BackColor="#ebfbe2" Height="450"
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" 
                            Enabled="False"  Width="476px">
						    <table style="width: 476px; ">
							    <tr style="border-style:solid; border-color:Gray; border-width:0px;">
								    <td align="left" class="style13">
								        Name &nbsp;</td>
								    <td align="left" style="border: 0px solid Gray; " class="style12">
								    <asp:textbox ID="txtName" runat="server" Width="55%" TabIndex="7" MaxLength="90"></asp:textbox>
								    </td>
								    <td align="center" style="border: 0px solid Gray; height: 27px;" class="td">
								        <asp:RequiredFieldValidator ID="rvName" runat="server" 
                                            ControlToValidate="txtName" ErrorMessage="Name can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                    </td>
							    </tr>
							    <tr style="border-style:solid; border-color:Gray; border-width:1px;">
								    <td align="left" class="style7">
								        Code</td>
								    <td align="left" style="border: 0px solid Gray; " class="style8">
								    <asp:textbox ID="txtCode" runat="server" Width="25%" TabIndex="8" MaxLength="48"></asp:textbox>
								    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								        <asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								    </td>
							    </tr>
							    <tr>
								    <td align="left" class="td" style="width: 93px; height: 27px; font-weight: bold; color: #4f4a46;">
								        Detail Name</td>
								    <td align="left" style="border: 0px solid Gray; width: 347px; height: 27px;">
								    <asp:textbox ID="txtDetailName" runat="server" Width="96%" TabIndex="9" 
                                            Height="27px" TextMode="MultiLine" MaxLength="98"></asp:textbox>
								    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								    </td>
							    </tr>
							    <tr>
								    <td align="left" class="td" style="width: 93px; height: 27px; font-weight: bold; color: #4f4a46;">
								        Total Credit</td>
								    <td align="left" style="border: 0px solid Gray; width: 347px; height: 27px;">
								        <asp:TextBox ID="txtCredits" runat="server" Width="68px"></asp:TextBox>
								    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								        <asp:CompareValidator ID="cvCredits" runat="server" 
                                            ControlToValidate="txtCredits" ErrorMessage="Credits can only be positive numbers" Operator="DataTypeCheck" 
                                            Type="Double" ValidationGroup="vdSave">*</asp:CompareValidator>
                                        <asp:RangeValidator ID="rngvCredits" runat="server" 
                                            ControlToValidate="txtCredits" ErrorMessage="Credits can only positive numbers" 
                                            MinimumValue="0" Type="Double" ValidationGroup="vdSave">*</asp:RangeValidator>
                                    </td>
							    </tr>
							    <tr>
                                    <td align="left" class="td" style="width: 93px; height: 27px; font-weight: bold; color: #4f4a46;">
                                        Department</td>
                                    <td align="left" 
                                        style="border: 0px solid Gray; " class="style8">
                                        <asp:DropDownList ID="ddlParent" runat="server" Height="20px" Width="162px">
                                        </asp:DropDownList>
                                    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								    </td>
                                </tr>
							    <tr>
                                    <td align="left" class="td" style="width: 93px; height: 27px; font-weight: bold; color: #4f4a46;">
                                        Type</td>
                                    <td align="left" 
                                        style="border: 0px solid Gray; " class="style8">
                                        <asp:DropDownList ID="ddlType" runat="server" Height="20px" Width="162px">
                                        </asp:DropDownList>
                                    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
							    <tr>
								    <td class="style13" valign="middle" align="center" style="border: 1px solid gray">
								        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="80"
                                            ValidationGroup="vdSave" CssClass="button" />
                                    </td>
								    <td style="height: 27px; border: 1px solid gray" class="td" colspan="2" align="center" valign="middle">
								        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Width="80"
                                            Text="Cancel" CssClass="button" />
								    </td>
							    </tr>
							    
						    </table>
					    </asp:panel>
					</td>
                </tr>            

                <tr>
                    <td align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:0px">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

