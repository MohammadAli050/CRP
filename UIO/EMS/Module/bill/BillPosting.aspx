<%@ Page Title="Bill Posting" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="BillPosting.aspx.cs" Inherits="EMS.miu.bill.BillPosting" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bill Posting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
    <div class="PageTitle">
        <label>Bill Posting</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td >
                            <table>
                                <tr>
						            <%--<td class="auto-style4">
							            Choose a Type </td>--%>
						            <td class="auto-style41">
                                        <div class="control-group radio">
                                            <div class="checkbox inline">   
                                        <asp:RadioButtonList ID="studentTypeList" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studentTypeList_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True" Text="Single Student" />
                                            <asp:ListItem Value="2" Text="Multiple Student" />
                                        </asp:RadioButtonList>
                                                </div>
                                            </div>
						            </td>
					            </tr>
                            </table>
                            <asp:Panel ID="multiStudentDDLPanel" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td class="auto-style4">
                                            <b>Program :</b>
                                        </td>
                                        <td class="auto-style6">
                                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                        </td>
                                    
                                        <td class="auto-style4">
                                            <b>Session :</b>
                                        </td>
                                        <td class="auto-style6">
                                            <uc1:SessionUserControl runat="server" ID="ucSession" />
                                        </td>
                                    
                                        <td class="auto-style4">
                                            <b>Batch :</b>
                                        </td>
                                        <td class="auto-style6">
                                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                        </td>

                                        <td class="auto-style4">
                                            <asp:Label ID="lblFessType" runat="server" Font-Bold="true"  Text="Fee Group :"></asp:Label> 
                                        </td>
                                        <td class="auto-style6">        
                                            <asp:DropDownList ID="ddlFeeGroup" Width="150px" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:Button ID="btnLoadFeeGroup" Visible="false" runat="server" Text="Add" OnClick="btnLoadFeeGroup_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblAmount1" runat="server" Text="Fee Amount :"></asp:Label> 
                                        </td>
                                        <td class="auto-style6"> 
                                            <asp:Label ID="lblAmount2" runat="server"></asp:Label>
                                        </td>
                                    </tr>  
                                
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblFee" runat="server" Visible="false" Text="Fee Item"></asp:Label> 
                                        </td>
                                        <td class="auto-style6">       
                                            <asp:DropDownList ID="ddlFeeItem" Visible="false" Width="150px" runat="server"> </asp:DropDownList>
                                        </td>
                                        <td class="auto-style4">
                                       
                                            <%--<asp:Button ID="btnLoadFeeItem" runat="server" Text="Add" OnClick="btnLoadFeeItem_Click" />--%>
                                        </td>
                                    </tr>                                  
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="singleStudentPanel" Visible="true" runat="server">
                                <table> 
                                <tr>
                                    <td class="auto-style4" >
                                        <asp:Label ID="lblStudentRoll" runat="server"  Text="Student Roll"></asp:Label> 
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtStudentRoll" Placeholder="Student Roll"  runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button ID="LoadStudent" runat="server" Text="Load Student" OnClick="LoadStudent_Click" />
                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td valign="top" class="auto-style3" >
                        <asp:GridView ID="GridViewStudent"  runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
                        EmptyDataText="No data found." AllowPaging="false" CellPadding="4"  PageSize="20" Width="480px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                            <asp:TemplateField Visible ="false"  HeaderText="Id">
                                <ItemTemplate >
                                    <asp:Label ID="lblStudentId"  runat="server" Text='<%# Bind("StudentID") %>'></asp:Label> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField Visible ="True">
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="chkAllStudentHeader" OnCheckedChanged="chkAllStudentHeader_CheckedChanged" Text="" AutoPostBack="true"></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <asp:CheckBox ID="CheckBox"  runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="30px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Roll"  HeaderText="Student Roll">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                                
                            <asp:BoundField DataField="StudentName"  HeaderText="Student Name">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="350px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TrimesterBill"  HeaderText="Bill">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TheoryCourseCount"  HeaderText="Theory">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LabCourseCount"  HeaderText="Lab">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
                        <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                    <td valign="top" class="auto-style3" >
                        <table>
                            <asp:GridView ID="GvFeeAmount" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
                            EmptyDataText="No data found." AllowPaging="false" CellPadding="4"  PageSize="20" Width="450px" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns> 
                                <asp:TemplateField Visible ="True">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkAllFee" OnCheckedChanged="chkAllFee_CheckedChanged" Text="" AutoPostBack="true"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate >
                                        <asp:CheckBox ID="feeCheckBox"  runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>

                                <asp:TemplateField Visible ="false"  HeaderText="FundTypeId">
                                <ItemTemplate >
                                    <asp:Label ID="lblFundTypeId"  runat="server" Text='<%# Bind("FundTypeId") %>'></asp:Label> 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="FeeGroupDetailId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFeeGroupDetailId" Text='<%#Eval("FeeGroupDetailId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="FeeTypeId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFeeTypeId" Text='<%#Eval("FeeTypeId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>  

                                <asp:TemplateField HeaderText="Fee Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFeeName" Text='<%#Eval("FeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="220px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <HeaderTemplate>
                                        <asp:Button ID="btnCalculateFee" runat="server" Text="Calculate Fee" OnClick="btnCalculateFee_Click" />
                                        <hr />
                                        <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                                    </HeaderTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" Width="80px" ID="txtAmount" Text='<%#Eval("Amount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<HeaderTemplate>
                                        <asp:Button ID="btnCalculateBillAmount" runat="server" Text="Calculate Amount" OnClick=" />
                                    </HeaderTemplate>--%>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField> 
                                    
                                <asp:TemplateField HeaderText="Comment">
                                    <HeaderTemplate>
                                        <asp:Button ID="btnBillPosting" runat="server" Text="Post Bill" OnClientClick="return confirm('Do you really want to post this bill?');" OnClick="btnBillPosting_Click" />
                                        <hr />
                                        <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate >
                                        <asp:TextBox ID="txtComment" width="150px" placeholder="Comment" runat="server" Text=""/>
                                    </ItemTemplate> 
                                </asp:TemplateField>                                    
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
                            <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
</asp:Content>
