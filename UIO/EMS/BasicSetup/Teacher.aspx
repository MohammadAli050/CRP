<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_Teacher" Codebehind="Teacher.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
            .style41
        {
            border: 1px solid Green;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            height: 5px;
        }
        .style42
        {
            border: 1px solid Green;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: middle;
            text-align:center;
            width: 86px;
            height: 5px;
        }
        .style43
        {
            border: 1px solid Green;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            width: 100px;
            height: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid green; width:986px; height:580px" >
                <tr>
                    <td colspan="2" align="center" style="border-style:solid; border-color:Gray; border-width:1px">

                        <table style="width: 984px; height:20px;">
							<tr>
								<td class="td" style="border: 1px solid Green;font-family: Arial, Helvetica, sans-serif;font-size: 12px;
							 		line-height: 24px;color: #333333;vertical-align: Middle;height: 20px;width: 180px;">
								    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#339933" 
                                        Width="178px">Teacher's Basic Information</asp:label>
								</td>
								<td class="td" style="height: 25px; width: 800px">
								    <asp:label ID="lblMsg" runat="server" ForeColor="#FF3300" Width="798px"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>
                <tr style="border-style:solid; border-color:green; border-width:1px;">
                    <td class="td" valign="top" align="left" style="vertical-align:top; width: 550px; height: 684px;" 
                        colspan="1">
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#CCFFCC" 
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px" 
                            Height="750px" Width="530px">
                            <table style="border: 1px solid Gray; width: 528px; height: 550px; " class="td">
                                <tr style="border: 1px solid Gray;">
                                    <td class="td" colspan="2" style="border: 1px solid green;font-family: Arial, Helvetica, sans-serif;font-size: 12px;
 										line-height: 24px;color: #333333;vertical-align: Middle;height: 27px;">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="243px"></asp:TextBox>
                                    </td>
                                    <td align="center" class="td" style="border: 1px solid green; width: 100px;">
                                        <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" 
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid green;">
                                    <td style="border: 1px solid green;" class ="style41">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add" 
                                            CssClass="button" />
                                    </td>
                                    <td align="center" class="td" >
                                        <asp:Button ID="btnEdit" runat="server" CssClass="button" 
                                            onclick="btnEdit_Click" Text="Edit" />
                                    </td>
                                    <td align="center" class="style43">
                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                            Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                    <td class="td" colspan="3" 
                                        style="border: 1px solid Gray; vertical-align:top; ">
                                        <asp:panel ID="pnlGrid" runat="server" Height="677px" ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="492px" 
                                                GridLines="Horizontal">
												<RowStyle ForeColor="#333333" Height="24px" BackColor="White" />
												<Columns>
													<asp:commandfield ShowSelectButton="True">
														<ControlStyle Width="40px" />
														<FooterStyle Width="40px" />
														<HeaderStyle Width="40px" />
														<ItemStyle Width="40px" />
													</asp:commandfield>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
												    <%--<asp:BoundField DataField="DeptName" HeaderText="Department" ReadOnly="True" >
                                                    	<HeaderStyle ForeColor="LawnGreen" />
													</asp:boundfield>--%>
                                                    <%--<asp:BoundField DataField="FullName" HeaderText="Teacher's Name" ReadOnly="True" >
														<HeaderStyle ForeColor="LawnGreen" />
													</asp:boundfield>--%>
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
                	<td style="border: 1px solid Gray; height: 684px;" valign="top" align="left" class="td">					    						    					       						   					    
                	    <asp:panel ID="pnlClass" runat="server" BackColor="#CCFFCC" BorderStyle="None" 
                            Enabled="False" Height="751px" Width="430px" ScrollBars="Vertical" 
                            Wrap="False" style="margin-left: 0px">
                            <table style="width: 389px; height:613px; margin-right: 0px;">                                
                                <tr style="border-style:solid; border-width:1px;">
                                    <td style="border: 1px solid green; " class="td">
                                        <table style="width: 387px; height:613px;">
                                        <tr style="border-style:solid; border-color:Gray; border-width:1px;">
								                <td align="left" 
                                                    style="border: 1px solid Gray; " class="td">
								                Code</td>
								                <td align="left" style="border: 1px solid Gray; " class="td">
								                    <asp:TextBox ID="txtCode" runat="server" TabIndex="8" Width="93px"></asp:TextBox>
                                                </td>
								                <td align="center" style="border: 1px solid Gray; " class="td">								                    
								                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" 
                                                        ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                                        ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
								                </td>								                
							                </tr>
							                <tr>
                                                <td align="left" class="td" >
                                                    Prefix</td>
                                                <td align="left" class="td" >
                                                    <asp:DropDownList ID="ddlPrefix" runat="server" Height="25px" Width="162px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" class="td" >
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="td" >
                                                    First Name</td>
                                                <td align="left" class="td" >
                                                    <asp:TextBox ID="txtFirstName" runat="server" TabIndex="8" Width="270px" 
                                                        MaxLength="98"></asp:TextBox>
                                                </td>
                                                <td align="center" class="td" >
                                                    <asp:RequiredFieldValidator ID="rvFirstName0" runat="server" 
                                                        ControlToValidate="txtFirstName" ErrorMessage="First Name can not be empty" 
                                                        ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="td" >
                                                    Middle Name</td>
                                                <td align="left" class="td" >
                                                    <asp:TextBox ID="txtMiddleName" runat="server" TabIndex="8" Width="270px" 
                                                        MaxLength="98"></asp:TextBox>
                                                </td>
                                                <td align="center" class="td" >
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="td" >
                                                    Last Name</td>
                                                <td align="left" class="td" >
                                                    <asp:TextBox ID="txtLastName" runat="server" TabIndex="8" Width="270px" 
                                                        MaxLength="98"></asp:TextBox>
                                                </td>
                                                <td align="center" class="td" >
                                                    <asp:RequiredFieldValidator ID="rfvLstName" runat="server" 
                                                        ControlToValidate="txtLastName" ErrorMessage="First Name can not be empty" 
                                                        ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="td" >
                                                    Other Name</td>
                                                <td align="left" class="td" >
                                                    <asp:TextBox ID="txtOtherName" runat="server" TabIndex="8" Width="231px" 
                                                        MaxLength="98"></asp:TextBox>
                                                </td>
                                                <td align="center" class="td" >
                                                </td>
                                            </tr>
                                            <tr style="border-style:solid; border-color:green; border-width:1px;">
                                                <td align="left" class="td" >
                                                    Department</td>
                                                <td align="left" class="td" >
                                                    <asp:DropDownList ID="ddlDept" runat="server" Height="25px" Width="235px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" class="td" >
                                                </td>
                                            </tr>
                                            <tr style="border-style:solid; border-color:green; border-width:1px;">
                                                <td align="left" class="td" >
                                                    User ID</td>
                                                <td align="left" class="td" >
                                                    <asp:DropDownList ID="ddlUserId" runat="server" Height="25px" Width="235px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" class="td" >
                                                </td>
                                            </tr>
							                <tr>
                                                <td align="left"  class="td">
                                                    Date of Birth</td>
                                                <td align="left" class="td">
                                                    <asp:Calendar ID="clrDOB" runat="server" BackColor="#FFFFCC" 
                                                        BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" 
                                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" 
                                                        Height="154px" ShowGridLines="True" Width="220px">
                                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                        <SelectorStyle BackColor="#FFCC66" />
                                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" 
                                                            ForeColor="#FFFFCC" />
                                                    </asp:Calendar>
                                                </td>
								                <td align="center" class="td">
								                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="td">
                                                    Gender</td>
                                                <td align="left" class="td">
                                                    <asp:DropDownList ID="ddlGender" runat="server" Height="25px" Width="162px">
                                                    </asp:DropDownList>
                                                </td>
								                <td align="center" class="td">
								                    &nbsp;</td>
                                            </tr>
						                </table>
                                    </td>
                                </tr>  
                                <tr>
                                    <td class="td">
                                        <table style="width: 418px">
                                            <tr>
                                                <td class="td">
                                                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                                                        CssClass="button" />
                                                </td>
                                                <td  align="center" class="td">
                                                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                                        Text="Cancel" CssClass="button" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>   
                            </table>                                                                                 
                        </asp:panel>
					</td>
                </tr>            

                <tr>
                    <td  align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:1px" class="td">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

