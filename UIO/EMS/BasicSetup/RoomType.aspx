<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_RoomType" Codebehind="RoomType.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
            .style18
            {
                border: 0px solid Gray;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 16px;
                font-variant: small-caps;
                line-height: 24px;
                color: #333333;
                vertical-align: Middle;
                height: 20px;
                                
            }
        .style19
        {
            width: 531px;
        }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:860px; height:528px; margin-top:10px; margin-left:auto; margin-right:auto" >
               <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px">
                        <table style="width: 610px; height:25px;">
                            <tr>
                                <td class="style18">
                                    
                                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46" 
                                        Text="Room Type Infprmation" Width="225px"></asp:Label>                                   
                                </td>
                                <td style="height: 25px; width: 610px">
                                    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="610px"></asp:label>
                                </td>
                            </tr>
                        </table> 
                    </td>                                        
               </tr>
               <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                   <td valign="top" align="left" style="vertical-align:top; width: 420px">
                      <asp:Panel ID="pnlCollection" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="450px" 
                            Width="430px">
                            <table style="border: 0px solid Gray; width: 420px; height: 420px; ">
                                <tr style="border: 0px solid Gray;" >
                                    <td colspan="2" style="border: 0px solid Gray; height: 25px; width: 280px">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="280px" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="center" valign="middle" style="border: 1px solid Gray; width: 140px; height: 25px">
                                        <asp:Button ID="btnFind" runat="server" Text="Find" onclick="btnFind_Click" Width="80"
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid Gray;" >
                                    <td align="center"  valign="middle"
                                        style="border: 1px solid Gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid Gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" onclick="btnEdit_Click" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid Gray;width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnDelete" runat="server"  Width="80"
                                            Text="Delete" onclick="btnDelete_Click" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                                    <td colspan="3" style="border: 0px solid Gray; vertical-align:top; height: 362px; width: 420px;">
                                        <asp:panel ID="pnlGrid" runat="server" Height="300px" Width="420px" 
                                            ScrollBars="Vertical">
											<asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="solid" BorderWidth="1px" 
                                                CellPadding="4" Height="120px" TabIndex="6" Width="405px" 
                                                GridLines="Horizontal" 
                                                onselectedindexchanged="gvwCollection_SelectedIndexChanged">
												<RowStyle ForeColor="#333333" Height="24px" BackColor="White" />
												<Columns>
													<asp:commandfield ShowSelectButton="True">
														<ControlStyle Width="100px" />
														<FooterStyle Width="100px" />
														<HeaderStyle Width="100px" />
														<ItemStyle Width="100px" />
													</asp:commandfield>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />												    
													<asp:boundfield DataField="Roomtypename" HeaderText="Room Type" ReadOnly="True" >
														<ControlStyle Width="300px" />
														<FooterStyle Width="300px" />
														<HeaderStyle Width="300px" HorizontalAlign="Left" ForeColor="#ffffff"/>
														<ItemStyle Width="300px" />
													</asp:boundfield>													
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
                   <td class="style19" valign="top" align="left" style="vertical-align:top;">
                      <asp:Panel ID="pnlRoomType" runat="server" BackColor="#ebfbe2" 
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="450px" 
                            Width="414px">
                                <table style="border: 0px solid Gray; width: 376px; "> 
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>                               
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                       
                                        <td style="border: 0px solid Gray; font-weight: bold; font-size: 13px; " valign="top" align="left">
                                                                                    Room Type</td>
                                        <td style="border: 0px solid Gray; " valign="top" align="left">
                                           
                                            <asp:TextBox ID="txtRoomType" runat="server" Width="218px" 
                                                ontextchanged="txtRoomType_TextChanged" MaxLength="50"></asp:TextBox>
                                           
                                            </td>
                                        <td style="border: 0px solid Gray; " valign="top" 
                                            align="center">
                                            
                                            <asp:RequiredFieldValidator ID="rvRoomTypeName" runat="server" 
                                                ControlToValidate="txtRoomType" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>                                                                                                                                               
                                    <tr>
                                        <td colspan="3" style="border-style:none; height: 25px; width: 540px;" 
                                            valign="top" align="left" class="td">
                                            <asp:TextBox ID="txtRoomTypeID" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="border-style:solid; border-color:Gray; border-width:1px;" class="tr">
                                        <td style="border: 1px solid Gray; height: 25px; width: 400px;" valign="middle" align="center" class="td">                                            
                                            <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" CssClass="button" Width="80"
                                                />&nbsp;&nbsp;                                            
                                        </td>
                                        <td style="border: 1px solid Gray; height: 25px; width: 540px;" valign="middle" 
                                            align="center" class="td" colspan="2">                                             
                                            &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80"
                                                onclick="btnCancel_Click" CssClass="button" 
                                                />
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

