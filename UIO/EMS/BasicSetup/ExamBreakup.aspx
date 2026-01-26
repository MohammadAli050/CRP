<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamBreakup.aspx.cs" Inherits="EMS.BasicSetup.ExamBreakup"
     MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">    
        
    
    <style type="text/css">
        .style1
        {
            width: 754px;
        }
        .style2
        {
            width: 337px;
        }
        .style3
        {
            width: 329px;
        }
        .style4
        {
            width: 316px;
        }
        .style15
        {
            height: 22px;
        }
        
        .style17
        {
            width: 100%;
            height: 22px;
        }
        .style18
        {
            height: 20px;
            width: 63px;
        }
        
        .style20
        {
            width: 100%;
            height: 22px;
        }
        
        .style22
        {
            width: 100%;
            height: 22px;
        }
        .style23
        {
            width: 64px;
        }
        .style24
        {
            width: 69px;
        }
        
        .style25
        {
            width: 90px;
        }
        .style26
        {
            width: 46px;
        }
        
        .style29
        {
            width: 122px;
        }
        
        .style30
        {
            width: 57px;
        }
        .style31
        {
            width: 49px;
        }
        .style32
        {
            width: 62px;
        }
        .styletd
        {        	
	        border-style:solid; 
	        border-color:Green; 
	        border-width:1px;
        }
        
        .style33
        {
            border: 1px solid Green;
            height: 15px;
        }
        
        .style34
        {
            border: 1px solid Green;
            height: 23px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <table style="width: 900px; height: 451px;">
		        <tr>
			        <td class="style4" colspan="2" valign="middle">
			            <table style="width: 899px">
							<tr>
								<td class="td" style="width: 163px">
								    <asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="Navy" Text="Exam Breakup"></asp:label>
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
			            <asp:Panel id="pnlControl" runat="server" Width="445px" Height="448px">
                        <table style="border: 1px solid Gray; width: 445px; height: 448px; ">
                            <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                <td colspan="2" style="border: 1px solid Gray;" class="style3">
                                    <asp:TextBox ID="txtSrch" runat="server" Width="91%" TabIndex="1"></asp:TextBox>
                                </td>
                                <td align="center" 
                                    style="border-style:solid; border-color:Gray; border-width:1px;" 
                                    class="style3">
                                    <asp:Button ID="btnFind" runat="server" Text="Find" Width="60px" 
                                        onclick="btnFind_Click" />
                                </td>
                            </tr>
                            <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                <td style="border-style:solid; border-color:Gray; border-width:1px;" 
                                    align="center" class="style2">
                                    <asp:Button ID="brnAdd" runat="server" Text="Add" Width="60px" 
                                        onclick="brnAdd_Click" />
                                </td>
                                <td style="border-style:solid; border-color:Gray; border-width:1px;" 
                                    align="center" class="style2">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="60px" 
                                        onclick="btnEdit_Click" />
                                </td>
                                <td style="border-style:solid; border-color:Gray; border-width:1px;"
                                    align="center" class="style2">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="60px" 
                                        onclick="btnDelete_Click" />
                                </td>
                             </tr>
                            <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                <td colspan="3" 
                                    style="border-style:solid; border-color:Gray; border-width:1px;vertical-align:top;height:373px;"> 
                                    
                                    <asp:Panel ID="pnlGrid" runat="server" Height="100%" ScrollBars="Vertical" 
                                        Width="100%">
                                        <asp:GridView ID="gdvExamTypeName" runat="server" AutoGenerateColumns="False" 
                                            AutoGenerateSelectButton="True" BackColor="White" BorderColor="#006666" 
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Height="120px" 
                                            Width="411px" TabIndex="6">
                                            <RowStyle ForeColor="#000066" Height="24px" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Exam Type Name" DataField="Name" />
                                                <asp:BoundField DataField="TypeDefinitionID" HeaderText="Type Def ID" 
                                                    Visible="False" />
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
			        
			        <td>
			        <asp:Panel id="pnlMaster" runat="server" Width="445px" Height="448px">
			           
                        <table style="padding: 0px; margin: 0px; border: 1px solid #000000; width: 445px; height: 45px; ">
                            <tr >                                                                   
                                <td class="style33" style="height:22px">
                                    <table class="style17" style="height:22px">
                                        <tr>
                                            <td class="style18">
                                                <asp:Label ID="Label2" runat="server" Text="Course Type"></asp:Label>
                                            </td>
                                            <td class="style15">
                                                <asp:DropDownList ID="ddlTypeDef" runat="server" Width="130px" 
                                                    onselectedindexchanged="ddlTypeDef_SelectedIndexChanged" 
                                                    style="margin-left: 0px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                                                   
                            </tr>
                            
                            <tr>                                
                                <td class="styletd" style="height:22px">
                                    <table class="style17" 
                                        style="border-width: thin; padding: 0px; margin: 0px; border-spacing: 0px">
                                        <tr>
                                            <td class="style32">
                                                <asp:Label ID="Label3" runat="server" Text="Exam Type Name"></asp:Label>
                                            </td>
                                            <td class="style25" >
                                                <asp:TextBox ID="txtExamTypeName" runat="server" Width="110px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvExamTypeName" runat="server" 
                                                    ControlToValidate="txtExamTypeName" 
                                                    ErrorMessage="Exam type name can't be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="0" rowspan="0" class="style30">
                                                <asp:Label ID="Label4" runat="server" Text="Total Marks"></asp:Label>
                                            </td>
                                            <td colspan="0" rowspan="0" class="style29">
                                            <asp:TextBox ID="txtTotalMarks" runat="server" Width="110px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvTotalMarks" runat="server" 
                                                    ControlToValidate="txtTotalMarks" ErrorMessage="Total marks can't be empty" 
                                                    ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rgvTotalMarks" runat="server" 
                                                    ControlToValidate="txtTotalMarks" 
                                                    ErrorMessage="Total Marks must be a positive number" 
                                                    ValidationExpression="^\d+$" ValidationGroup="vdSave">*</asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkDefault" runat="server" Text="Default" Checked="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                
                            </tr>
                        </table>
                     
                        <asp:Panel ID="pnlDetails" runat="server" Width="445px" Height="390px">
                        <fieldset id="gbxDetails" style="border: 1px solid #0066CC; width: 440px; height: 375px;">
                        <legend>Exam Breakup</legend>
                         <table style="padding: 0px; margin: 0px; border: 1px solid #000000; width: 438px; height: 340px; ">
                            
                            <tr>
                                <td class="style34">
                                
                                    <table cellspacing="1" class="style20">
                                        <tr>
                                            <td class="style26">
                                                <asp:Label ID="Label8" runat="server" Text="Exam Breakup"></asp:Label>
                                            </td>
                                            <td class="style29">
                                                <asp:TextBox ID="txtExamBreakup" runat="server" Width="110px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvExamBreakup" runat="server" 
                                                    ControlToValidate="txtExamBreakup" ErrorMessage="Exam Breakup can't be empty" 
                                                    ValidationGroup="vdExamBreakup">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="style31">
                                                <asp:Label ID="Label9" runat="server" Text="[Mid-1]"></asp:Label>
                                            </td>
                                            <td class="style31">
                                                <asp:Label ID="Label10" runat="server" Text="Allotted Marks"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAllottedMarks" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rgvAllottedMarks" runat="server" 
                                                    ControlToValidate="txtAllottedMarks" 
                                                    ErrorMessage="Allotted marks must be a positive number" 
                                                    ValidationExpression="^\d+$" ValidationGroup="vdExamBreakup">*</asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rvAllottedMarks" runat="server" 
                                                    ControlToValidate="txtAllottedMarks" 
                                                    ErrorMessage="Allotted marks can't be empty" ValidationGroup="vdExamBreakup">*</asp:RequiredFieldValidator>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="styletd" style="height:22px">
                                    <asp:Button ID="btnAddBreakup" runat="server" Text="Add" Width="60px" 
                                        onclick="btnAddBreakup_Click" ValidationGroup="vdExamBreakup" />
                                    </td>
                            </tr>
                            <tr>
                                <td class="styletd" style="height:100px">
                                    <asp:GridView ID="gdvExamBreakup" runat="server" BackColor="White" 
                                        BorderColor="#006666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                                        GridLines="Horizontal" AutoGenerateColumns="False" Width="394px" 
                                        onrowcancelingedit="gdvExamBreakup_RowCancelingEdit" 
                                        onrowdeleting="gdvExamBreakup_RowDeleting" 
                                        onrowediting="gdvExamBreakup_RowEditing" 
                                        onrowupdated="gdvExamBreakup_RowUpdated" 
                                        onrowupdating="gdvExamBreakup_RowUpdating">
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <Columns>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                            <asp:BoundField HeaderText="Exam Breakup" DataField="ExamName" />
                                            <asp:BoundField HeaderText="Allotted Marks" DataField="AllottedMarks" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                             <tr>
                                <td class="styletd" style="height:22px">
                                    <table class="style22">
                                        <tr>
                                            <td class="style23">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" 
                                                    onclick="btnSave_Click" ValidationGroup="vdSave" />
                                            </td>
                                            <td class="style24">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" 
                                                    onclick="btnCancel_Click" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           
                        </table>
                        </fieldset></asp:Panel>
                        
                      </asp:Panel>			           
                        
			        
			        </td>
			       
		        </tr>
		        <tr>
			        <td class="style4" colspan="2" valign="middle">
                        <asp:ValidationSummary ID="vsDetail" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                        <asp:ValidationSummary ID="vsExamBreakup" runat="server" BorderStyle="None" 
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdExamBreakup" />
			        </td>
		        </tr>
	        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>