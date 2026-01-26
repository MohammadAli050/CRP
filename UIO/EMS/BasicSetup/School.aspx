<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_School" Codebehind="School.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            border: 1px solid Gray;
            width: 120px;
            height: 27px;
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
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 24px;
            width: 110px;
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
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 27px;
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <table style="border: 1px solid Gray; width:980px; height:580px" >
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px">

                        <table style="width: 980px; height:30px;">
							<tr>
								<td class="style10">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="Green" 
                                        Width="93%">School Info</asp:label>
								</td>
								<td class="td" style="height: 24px">
								    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="100%"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>
                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                    <td class="td" valign="top" align="left" style="vertical-align:top;">
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#CCFFCC" 
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px" Height="549px" 
                            Width="483px">
                            <table style="border: 1px solid Gray; width: 483px; height: 550px; ">
                                <tr style="border: 1px solid Gray;">
                                    <td class="td" colspan="2" style="border: 1px solid Gray; height: 27px;">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="90%"></asp:TextBox>
                                    </td>
                                    <td align="center" class="td" style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" 
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid Gray;">
                                    <td align="center" 
                                        style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add" 
                                            CssClass="button" />
                                    </td>
                                    <td align="center" 
                                        style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="Edit" 
                                            CssClass="button" />
                                    </td>
                                    <td align="center" 
                                        style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                            Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                    <td class="td" colspan="3" style="border: 1px solid Gray; vertical-align:top; height: 362px;">
                                        <asp:panel ID="pnlGrid" runat="server" Height="433px" ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="422px" 
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
													<asp:boundfield DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
												</Columns>
												<FooterStyle BackColor="White" ForeColor="#333333" />
												<PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
												<SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
												<HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
											</asp:gridview>
										</asp:panel>
									</td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </td>
                	<td style="border: 1px solid Gray;" valign="top" class="td">
					    <asp:panel ID="pnlClass" runat="server" BackColor="#CCFFCC" 
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px" 
                            Enabled="False" Height="550px" Width="476px">
						    <table style="width: 476px; height:550px;">
							    <tr style="border-style:solid; border-color:Gray; border-width:1px;">
								    <td align="left" class="style13">
								        Name &nbsp;</td>
								    <td align="left" 
                                        style="border: 1px solid Gray; " 
                                        class="style12">
								    <asp:textbox ID="txtName" runat="server" Width="55%" TabIndex="7" MaxLength="98"></asp:textbox>
								    </td>
								    <td align="center" style="border: 1px solid Gray; height: 27px;" class="td">
								        <asp:RequiredFieldValidator ID="rvName" runat="server" 
                                            ControlToValidate="txtName" ErrorMessage="Name can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                    </td>
							    </tr>
							    <tr style="border-style:solid; border-color:Gray; border-width:1px;">
								    <td align="left" class="style7">
								        Code</td>
								    <td align="left" 
                                        style="border: 1px solid Gray; " class="style8">
								    <asp:textbox ID="txtCode" runat="server" Width="25%" TabIndex="8" MaxLength="48"></asp:textbox>
								    </td>
								    <td align="right" style="border: 1px solid Gray; " class="style9">
								        <asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								    </td>
							    </tr>
							    <tr>
								    <td class="style13">
								        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                                            ValidationGroup="vdSave" CssClass="button" />
                                    </td>
								    <td style="height: 27px;" class="td" colspan="2">
								        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                            Text="Cancel" CssClass="button" />
								    </td>
							    </tr>
							    <tr>
								    <td colspan="3" style="border-style:solid; border-color:Gray; border-width:1px;">
								    &nbsp;</td>
							    </tr>
						    </table>
					    </asp:panel>
					</td>
                </tr>            

                <tr>
                    <td align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:1px">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

