<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_CalenderUnitCreator" Codebehind="CalendarUnitCreator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            border: 0px solid Gray;
            width: 50%;
            height: auto;
        }
        .style2
        {
            height: 24px;
        }
        .style3
        {
            height: 25px;
        }
        .style4
        {
            border: 0px solid Gray;
            height: 7px;
        }
        .style5
        {
            border: 0px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 328px;
        }
        .style7
        {
            width: 40%;
            height: 27px;
        }
        .style8
        {
            height: 27px;
        }
        .style9
        {
            border: 0px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 24px;
            width: 30%;
        }
        .style10
        {
            border: 0px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 24px;
        }
        .auto-style1 {
            height: 14px;
            width: 39%;
        }
        .auto-style2 {
            width: 39%;
            height: 27px;
        }
    </style>
    <script type="text/javascript">
        function ShowText() {
            document.getElementById('ctl00_cpHolMas_lblDetName').value = document.getElementById('ctl00_cpHolMas_txtCalenderName').value + " Name:";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 900px; height: auto; border: 1px solid Gray; margin-top:10px; margin-left:auto; margin-right:auto">
		        <tr>
			        <td class="style4" colspan="2" valign="middle" style="border: 1px solid gray;">
			            <table style="width: 899px; height: 30px;">
							<tr>
								<td class="td" style="width: 163px; font-variant: small-caps; font-size: 16px;">
								    <asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="#4f4a46" Text="Calendar"></asp:label>
								</td>
								<td class="td">
								    <asp:label id="lblMsg" runat="server" ForeColor="Red" Width="100%"></asp:label>
								</td>
							</tr>
						</table>
			        </td>
		        </tr>
		        <tr>
			        <td class="style1">
			            <asp:Panel id="pnlControl" runat="server" Width="445px" Height="448px" BackColor="#ebfbe2" BorderStyle="solid" BorderWidth="1px">
                        <table style="border: 0px solid Gray; width: 445px; ">
                            <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                                <td colspan="2" style="border: 0px solid Gray;" class="style3">
                                    <asp:TextBox ID="txtSrch" runat="server" Width="98%" TabIndex="1"></asp:TextBox>
                                </td>
                                <td align="center" style="border-style:solid; border-color:Gray; border-width:1px;" >
                                    <asp:ImageButton ID="butFind" runat="server" ImageUrl="~/ButtonImages/BtnFind.png" onclick="butFind_Click" 
                                       style="margin-top: 3px;" AlternateText="Find"  /> 
                                </td>      
                                                                                                                                                                                            
                            </tr>
                            <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                <td style="border-style:solid; border-color:Gray; border-width:1px;" 
                                    align="center" class="style2">
                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/ButtonImages/BtnAdd.png" 
                                        onclick="btnAdd_Click" AlternateText="Add" style="margin-top: 3px;" />
                                </td>
                                <td style="border-style:solid; border-color:Gray; border-width:1px;" 
                                    align="center" class="style2">
                                    <asp:ImageButton ID="btnEdit" runat="server" 
                                        ImageUrl="~/ButtonImages/BtnEdit.png" onclick="btnEdit_Click" 
                                        AlternateText="Edit" style="margin-top: 3px;" />
                                </td>
                                <td style="border-style:solid; border-color:Gray; border-width:1px;"
                                    align="center" class="style2">
                                    <asp:ImageButton ID="btnDelete" runat="server" 
                                        ImageUrl="~/ButtonImages/BtnDelete.png" onclick="btnDelete_Click" 
                                        AlternateText="Delete" style="margin-top: 3px;"/>
                                </td>
                             </tr>
                            <tr style="border-style:solid; border-color:Gray; border-width:0px;">
                                <td colspan="3" 
                                    style="border-style:solid; border-color:Gray; border-width:0px;vertical-align:top;"> 
                                    
                                    <asp:Panel ID="pnlGrid" runat="server" Height="100%" ScrollBars="Vertical" 
                                        Width="100%">
                                        <asp:GridView ID="gvwCalenderMaster" runat="server" AutoGenerateColumns="False" 
                                            AutoGenerateSelectButton="True" BackColor="White" BorderColor="#006666" 
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Height="120px" 
                                            Width="411px" TabIndex="6">
                                            <RowStyle ForeColor="#000066" Height="24px" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Calender Name" DataField="Name" />
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" 
                                                Height="24px" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                                Height="24px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
			            </asp:Panel>
			        </td>
			        <td class="style1" align="center">
			            <asp:Panel id="pnlEntry" runat="server" Width="445px"  Enabled="False">
				        <fieldset style="border: 1px solid #0033CC; width: 425px; height: 435px;" id="gbxMaster">
				        <legend>Sequence of Course Offering</legend>
				        <table style="border: 0px solid #808080; width: 423px; ">
					        <tr>
						        <td align="center" valign="middle" class="auto-style1">
						            <asp:RequiredFieldValidator ID="rfvMaster" runat="server" BorderStyle="None" 
                                        ControlToValidate="txtCalenderName" Display="Dynamic" 
                                        ErrorMessage="Calendar Name can not be empty." SetFocusOnError="True" 
                                        ValidationGroup="ValidateMaster" Width="10px">*</asp:RequiredFieldValidator>
						            <asp:Label ID="lblcalName" runat="server" Text="Sequence Name:" Font-Bold="true"></asp:Label>
						        </td>
						        <td class="td" style="height: 14px;" align="left">
                                    <asp:TextBox ID="txtCalenderName" runat="server" Width="85%" 
                                        style="margin-left: 0px" TabIndex="7" onkeyup="ShowText()"></asp:TextBox>
                                    
                                    </td>
					        </tr>
					        <tr>
						        <td class="style5" colspan="2">
						        <asp:panel id="pnlDetails" runat="server" Width="420px" Height="340px" 
                                        style="margin-top: 0px">
							        <fieldset id="gbxDetails" style="border: 1px solid #0066CC; background-color:#ebfbe2; width: 390px; height: 320px auto;">
							        <legend>Sequence Distribution</legend>
							        <table style="width: 99%; ">
								        <tr>
                                            <td align="right" class="style10" style="width: 1%">
                                                <asp:RequiredFieldValidator ID="rfvDetailName" runat="server" 
                                                    ControlToValidate="txtDetailName" ErrorMessage="Detail Name can not be empty." 
                                                    SetFocusOnError="True" ValidationGroup="ValidateDetail">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" class="style9">
                                                <asp:TextBox ID="lblDetName" runat="server" BorderStyle="None" Enabled="False" 
                                                    Height="18px" Width="138px"></asp:TextBox>
                                            </td>
                                            <td class="td" style="height: 24px">
                                                <asp:TextBox ID="txtDetailName" runat="server" Width="95%" TabIndex="8"></asp:TextBox>
                                            </td>
                                        </tr>
								        <tr>
									        <td class="td" style="height: 24px" align="left" colspan="3">
									        <asp:imagebutton ID="btnAddDet" runat="server" AlternateText="Add" 
                                                    ImageUrl="~/ButtonImages/BtnAdd.png" onclick="btnAddDet_Click" 
                                                    ValidationGroup="ValidateDetail" style="margin-left: 9px" />
									        </td>
								        </tr>
								        <tr>
									        <td align="left" colspan="3" class="td" valign="top">
                                                <asp:Panel ID="pnlCalendarDet" runat="server"  ScrollBars="Vertical" Height="230" Width="99%">
                                                    <asp:GridView ID="gdvCalDet" runat="server" AutoGenerateColumns="False" 
                                                        BackColor="White" BorderColor="#CCCCCC" 
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="59%" 
                                                        onrowdeleting="gdvCalDet_RowDeleting" onrowediting="gdvCalDet_RowEditing" 
                                                        Width="99%" onrowcancelingedit="gdvCalDet_RowCancelingEdit" 
                                                        onrowupdating="gdvCalDet_RowUpdating" onrowupdated="gdvCalDet_RowUpdated" 
                                                        TabIndex="10">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:CommandField ShowEditButton="True">
                                                                <ControlStyle Width="130px" />
                                                                <FooterStyle Width="130px" />
                                                                <HeaderStyle Width="130px" />
                                                                <ItemStyle Width="130px" />
                                                            </asp:CommandField>
                                                            <asp:CommandField ShowDeleteButton="True">
                                                                <ControlStyle Width="50px" />
                                                                <FooterStyle Width="50px" />
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="Name" HeaderText="Detail Name">
                                                                <HeaderStyle BorderColor="#003399" ForeColor="#99CCFF" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                </asp:Panel>
											</td>
								        </tr>
							        </table>
							        </fieldset></asp:panel>
						        </td>
					        </tr>
					        <tr>
						        <td align="left" class="auto-style2">
                                    <asp:ImageButton ID="butSave" runat="server" AlternateText="Save" 
                                        ImageUrl="~/ButtonImages/BtnSave.png" onclick="butSave_Click" 
                                        ValidationGroup="ValidateMaster" style="margin-left: 2px;"/>
                                </td>
						        <td align="right" class="style8">
                                    <asp:ImageButton ID="btnClose" runat="server" AlternateText="Cancel" 
                                        ImageUrl="~/ButtonImages/BtnCancel.png" onclick="btnClose_Click" style="margin-right: 11px;"/>
                                </td>
					        </tr>
				        </table>
				        </fieldset></asp:Panel>
			        </td>
		        </tr>
		        <tr style="height: 28px">
			        <td class="style4" colspan="2" valign="middle">
                        <asp:ValidationSummary ID="vsDetail" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="ValidateDetail" />
                        <asp:ValidationSummary ID="vsMaster" runat="server" ShowMessageBox="True" 
                            ShowSummary="False" ValidationGroup="ValidateMaster" />
			        </td>
		        </tr>
	        </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>

