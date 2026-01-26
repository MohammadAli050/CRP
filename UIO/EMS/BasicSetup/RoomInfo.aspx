<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_RoomInfo" Codebehind="RoomInfo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
            .style18
            {
                border: 0px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 16px;
                line-height: 24px;
                color: #4f4a46;
                vertical-align: Middle;
                height: 20px;
                width: 180px;
                text-align: left;
                font-variant:small-caps;
            }
            .style19
        {
            border: 0px solid #339933;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            font-weight: bold;
            line-height: 24px;
            color: #4f4a46;
            vertical-align: Middle;
            width: 200px;
        }
        .style20
        {
            border: 1px solid #339933;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #336600;
            vertical-align: Middle;
            width: 310px;
        }
        .style21
        {
            border: 1px solid #339933;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #336600;
            vertical-align: Middle;
            width: 30px;
        }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:960px; height:580px; margin-top:10px; margin-left:auto; margin-right:auto" >
               <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px">
                        <table style="width: 960px; height:25px;">
                            <tr>
                                <td class="style18">
                                    <asp:label ID="lblHeader" runat="server" Font-Bold="True" Width="180px">Room Information</asp:label> 
                                        
                                </td>
                                <td class="td" style="height: 25px; width: 720px">
                                    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="719px"></asp:label>
                                </td>
                            </tr>
                        </table> 
                    </td>                                        
               </tr>
               <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                   <td class="td" valign="top" align="left" style="vertical-align:top; width: 420px">
                      <asp:Panel ID="pnlCollection" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="550px" 
                            Width="430px">
                            <table style="border: 0px solid Gray;  height: 420px; ">
                                <tr style="border: 0px solid Gray;" >
                                    <td colspan="2" style="border: 0px solid Gray; height: 25px; width: 280px">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="280px" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="center" style="border: 1px solid Gray; width: 140px; height: 25px;" valign="middle">
                                        <asp:Button ID="btnFind" runat="server" onclick="btnFind_Click" Text="Find" width="80" ForeColor="#4f4a46"
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid Gray;" >
                                    <td align="center"  
                                        style="border: 1px solid Gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Add" width="80" ForeColor="#4f4a46"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" 
                                        style="border: 1px solid Gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="Edit" width="80" ForeColor="#4f4a46"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" 
                                        style="border: 1px solid Gray;width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" width="80" ForeColor="#4f4a46"
                                            Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                                    <td colspan="3" style="border: 0px solid Gray; vertical-align:top;  width: 420px;">
                                        <asp:panel ID="pnlGrid" runat="server" Height="476px" Width="420px" 
                                            ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="405px" 
                                                GridLines="Horizontal">
												<RowStyle ForeColor="#333333" Height="24px" BackColor="White" />
												<Columns>
													<asp:commandfield ShowSelectButton="True">
														<ControlStyle Width="40px" />
														<FooterStyle Width="40px" />
														<HeaderStyle Width="40px" />
														<ItemStyle Width="40px" />
													</asp:commandfield>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />												    
													<asp:boundfield DataField="RoomNo" HeaderText="Room Number" ReadOnly="True" >
														<ControlStyle Width="50px" />
														<FooterStyle Width="50px" />
														<HeaderStyle Width="50px" HorizontalAlign="Left" ForeColor="#99FF66"/>
														<ItemStyle Width="50px" />
													</asp:boundfield>
													<asp:boundfield DataField="RoomName" HeaderText="Room Name" ReadOnly="True" >
													    <ControlStyle Width="132px" />
														<FooterStyle Width="132px" />
														<HeaderStyle Width="132px" HorizontalAlign="Left"  ForeColor="#99FF66"/>
														<ItemStyle Width="132px" />
													</asp:boundfield>
													<asp:BoundField DataField="TypeName" HeaderText="Room Type" ReadOnly="True" >
												        <ControlStyle Width="100px" />
														<FooterStyle Width="100px" />
														<HeaderStyle Width="100px" HorizontalAlign="Left" ForeColor="#99FF66" />
														<ItemStyle Width="100px" />
													</asp:boundfield>
												    <asp:BoundField DataField="Capacity" HeaderText="Capacity" ReadOnly="True" >                                                        
												        <ControlStyle Width="32px" />
														<FooterStyle Width="32px" />
														<HeaderStyle Width="32px" HorizontalAlign="Left"  ForeColor="#99FF66"/>
														<ItemStyle Width="32px" />
													</asp:boundfield>
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
                   <td class="td" valign="top" align="left" style="vertical-align:top;">
                      <asp:Panel ID="pnlRoomInfo" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="550px"  Width="540px">
                           
                                <table style="border: 0px solid Gray; width: 540px; ">                                
                                    <tr style="border-style:solid; border-color:Gray; border-width:0px;" class="tr">
                                        <td style="border: 0px solid Gray; " valign="top" align="left" class="style19">
                                                                                    Room Number</td>
                                        <td style="border: 0px solid Gray; " valign="top" align="left" class="style20">
                                           
                                            <asp:TextBox ID="txtRoomNo" runat="server" Width="310px" MaxLength="50"></asp:TextBox>
                                           
                                            </td>
                                        <td style="border: 0px solid Gray; " valign="top" 
                                            align="center" class="style21">
                                            
                                            <asp:RequiredFieldValidator ID="rvRoomNo" runat="server" 
                                                ControlToValidate="txtRoomNo" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                    Room Name</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:TextBox ID="txtRoomName" runat="server" Width="310px" MaxLength="50"></asp:TextBox>
                                           
                                        </td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                    Room Type</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:DropDownList ID="ddlRoomType" runat="server" Height="25px" 
                                                Width="162px">
                                            </asp:DropDownList>
                                           
                                        </td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                    Campus Name</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:DropDownList ID="ddlCampusName" runat="server" Height="25px" 
                                                Width="162px" AutoPostBack="True" OnSelectedIndexChanged="ddlCampusName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           
                                        </td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                    Building Name</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:DropDownList ID="ddlBuildingName" runat="server" Height="25px" 
                                                Width="162px">
                                            </asp:DropDownList>
                                           
                                        </td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                    Capacity</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:TextBox ID="txtCapacity" runat="server" Width="310px" MaxLength="3"></asp:TextBox>
                                           
                                        </td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="txtRoomNo" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 0px solid Gray; height: 25px; width: 200px;" valign="top" align="left" class="style19">
                                                                                   Exam Capacity</td>
                                        <td style="border: 0px solid Gray; height: 25px; width: 310px;" valign="top" align="left" class="td">
                                           
                                            <asp:TextBox ID="txtExamCapacity" runat="server" Width="310px" MaxLength="3"></asp:TextBox>
                                           
                                        </td>
                                        
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td colspan="2" style="border: 0px solid Gray; padding-left: 150px; height: 25px; width: 540px;" valign="top" align="left" class="td">
                                            <asp:Button ID="btnAddress" runat="server" Visible="false" Text="Address Entry" Width="150px" Height="25px" ForeColor="#4f4a46" />
                                            <asp:TextBox ID="txtAddressId" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                            <asp:TextBox ID="txtRoomID" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                        </td>                                        
                                        <td style="border: 0px solid Gray; height: 25px; width: 30px;" valign="top" 
                                            align="center" class="td">
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="txtRoomNo" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 1px solid Gray; height: 25px; width: 400px; " valign="middle" align="right" class="td">                                            
                                            <asp:Button ID="btnSave" runat="server" Text="Save" width="80" ForeColor="#4f4a46"
                                                onclick="btnSave_Click" CssClass="button" />&nbsp;&nbsp;                                            
                                        </td>
                                        <td style="border: 1px solid Gray; height: 25px; width: 540px;" valign="middle" align="left" class="td">                                             
                                            &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" width="80" ForeColor="#4f4a46"
                                                onclick="btnCancel_Click" CssClass="button" />
                                        </td>
                                    </tr>                                                                                                 
                            </table>
                      </asp:Panel>
                  </td>                                                                                                                   
               </tr>
               <tr>
                    <%--<td ;="" align="left" colspan="2" style="border-style:solid; border-color:Gray; border-width:1px">
                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>--%>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
