<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_Department" Codebehind="Department.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
            .style7
            {
                border: 0px solid Gray;
                width: 120px;
                height: 27px;
                font-size: 13px;
                font-weight: bold;
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
                line-height: 2px;
                color: #333333;
                vertical-align: Middle;
                height: 24px;
                width: 150px;
                font-variant:small-caps;
            }
            .style12
            {
                border: 0px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                line-height: 2px;
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
                line-height: 2px;
                color: #333333;
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
        .auto-style1 {
            border: 0px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            line-height: 2px;
            color: #333333;
            vertical-align: Middle;
            height: 24px;
            width: 249px;
            font-variant: small-caps;
        }
        .auto-style2 {
            height: 8px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:970px; height:480px; margin-top:10px; margin-left:auto; margin-right:auto" >
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px">

                        <table style="width: 965px; height:30px;">
							<tr>
								<td class="auto-style1">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46">Department Information</asp:label>
								</td>
								<td class="td" style="height: 24px; float:right;" >
								    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="80%"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>
                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                    <td valign="top" align="left" >
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#ebfbe2" BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="1px"  Width="483px">
                            <table style="border: 0px solid Gray; width: 483px;  ">
                                <tr style="border: 1px solid Gray;">
                                    <td colspan="2" style="height: 27px;">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="94%"></asp:TextBox>
                                    </td>
                                    <td align="center"  style="height: 27px; border: 1px solid gray;" valign="middle">
                                        <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" Width="80" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 0px solid Gray">
                                    <td align="center" valign="middle" style="border: 1px solid Gray; " >
                                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add"  Width="80" CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle" style="border: 1px solid Gray; " >
                                        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="Edit" Width="80" CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle" style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" Width="80" Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:0px;" class="tr">
                                    <td class="td" colspan="3" style="border: 0px solid Gray; vertical-align:top; height: 328px ">
                                        <asp:panel ID="pnlGrid" runat="server"  ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="solid" BorderWidth="1px" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="457px" 
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
                	<td style="border: 0px solid Gray;" valign="top">
					    <asp:panel ID="pnlClass" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="1px" 
                            Enabled="False"  Width="476px">
						    <table style="width: 476px; height: 400px ">
							    <tr style="border-style:solid; border-color:Gray; border-width:1px; font-weight: bold;">
								    <td align="left" class="style13">
								        Name &nbsp;</td>
								    <td align="left" 
                                        style="border: 0px solid Gray; " 
                                        class="style12">
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
								    <td align="left" 
                                        style="border: 0px solid Gray; " class="style8">
								    <asp:textbox ID="txtCode" runat="server" Width="25%" TabIndex="8" MaxLength="48"></asp:textbox>
								    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								        <asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								    </td>
							    </tr>
							    <tr>
								    <td align="left" class="td" style="width: 93px; height: 27px; font-size: 13px; font-weight: bold;">
								        Detail Name</td>
								    <td align="left" style="border: 0px solid Gray; width: 347px; height: 27px;">
								    <asp:textbox ID="txtDetailName" runat="server" Width="96%" TabIndex="9" 
                                            Height="27px" TextMode="MultiLine" MaxLength="98"></asp:textbox>
								    </td>
								    <td align="right" style="border: 0px solid Gray; " class="style9">
								        <asp:RequiredFieldValidator ID="rvTxtDetName" runat="server" 
                                            ControlToValidate="txtDetailName" ErrorMessage="Detail name can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								    </td>
							    </tr>
							    <%--<tr>
                                    <td align="left" class="td" style="width: 93px; height: 27px">
                                        Department</td>
                                    <td align="left" 
                                        style="border: 1px solid Gray; " class="style8">
                                        <asp:DropDownList ID="ddlParent" Visible="false" Enabled="false" runat="server" Height="20px" Width="162px">
                                        </asp:DropDownList>
                                    </td>
								    <td align="right" style="border: 1px solid Gray; " class="style9">
								    </td>
                                </tr>--%>
							    <tr>
                                    <td align="right" class="auto-style2" colspan="2">
                                        <table class="style6">
                                            <tr>
                                                <td align ="center" class="td">
                                                    <asp:CheckBox ID="chkIsOpen" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="chkIsOpen_CheckedChanged" Text="Opening Date" />
                                                </td>
                                                <td align ="center" class="td">
                                                    <asp:CheckBox ID="chkIsClosed" runat="server" 
                                                        oncheckedchanged="chkIsClosed_CheckedChanged" Text="End Date" 
                                                        AutoPostBack="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:Calendar ID="clrStartDate" runat="server" BackColor="#FFFFCC" 
                                                        BorderColor="#FFCC66" BorderWidth="1px" 
                                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                                        ForeColor="#663399" Height="200px" Width="220px" ShowGridLines="True" 
                                                        Enabled="False">
                                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                        <SelectorStyle BackColor="#FFCC66" />
                                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <DayHeaderStyle BackColor="#FFCC66" Height="1px" Font-Bold="True" />
                                                        <TitleStyle BackColor="#990000" 
                                                            Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    </asp:Calendar>
                                                </td>
                                                <td class="td">
                                                    <asp:Calendar ID="clrEndDate" runat="server" BackColor="#FFFFCC" 
                                                        BorderColor="#FFCC66" BorderWidth="1px" 
                                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                                        ForeColor="#663399" Height="200px" Width="220px" Enabled="False" 
                                                        ShowGridLines="True">
                                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                        <SelectorStyle BackColor="#FFCC66" />
                                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <DayHeaderStyle BackColor="#FFCC66" Height="1px" Font-Bold="True" />
                                                        <TitleStyle BackColor="#990000" 
                                                            Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    </asp:Calendar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>    
							    <tr>
								    <td class="style13" valign="middle" align="center">
								        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Width="80"
                                            ValidationGroup="vdSave" CssClass="button" />
                                    </td>
								    <td class="style13"  colspan="2" valign="middle" align="center">
								        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Width="80"
                                            Text="Cancel" CssClass="button" />
								    </td>
							    </tr>
							    
						    </table>
					    </asp:panel>
					</td>
                </tr>            
                <tr>
                    <td  align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:0px">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

