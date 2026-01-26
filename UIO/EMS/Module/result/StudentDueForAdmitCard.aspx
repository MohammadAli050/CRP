<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentDueForAdmitCard.aspx.cs" Inherits="EMS.miu.result.StudentDueForAdmitCard" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admit Card Block
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
    <style type="text/css">
        .pointer {
            cursor: pointer;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Admit Card Block :: Based on 50% bill of Selected Session + Previous Due</label>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>                  
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Program</b></td>
                            <td class="auto-style4"><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>                                                                                                                                                   
                            <td class="auto-style8"><b>Session</b></td>
                            <td class="auto-style7"><uc1:SessionUserControl runat="server" ID="ucSession" /></td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            
                        </tr>
                    </table>
                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Min Due Amount</b></td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtAmount" Placeholder ="Due Amount" runat="server"></asp:TextBox> 
                            </td>                                                                                                                                                   
                            <td>
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnAdmitCardBlock" runat="server" Text="Admit Card Block" OnClientClick=" return confirm('Are you sure, you want to block student admit card?')" OnClick="btnAdmitCardBlock_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div1" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="div2" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <asp:GridView runat="server" ID="GvStudentAdmitCardDue" AutoGenerateColumns="False"
                    AllowPaging="false" PageSize="20" CssClass="gridCss" CellPadding="4"  >
                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />

                    <Columns>
                        <asp:TemplateField HeaderText="StudentID" Visible="false" >
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStudentID" Text='<%#Eval("StudentID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Roll" >
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Studant Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Current Trim. Bill(A)" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCurrentTrimesterTotalBill" Text='<%#Eval("CurrentTrimesterTotalBill") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="50% of Current Trim. Bill (B) = (A)/2" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCurrentTrimesterHalfBill" Text='<%#Eval("CurrentTrimesterHalfBill") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="Total Previous Bill (C)" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotalPreviousBill" Text='<%#Eval("TotalPreviousBill") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                    
                        <asp:TemplateField HeaderText="Due (D) = (B)+(C)" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDue" Text='<%#Eval("Due") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>
                    
                        <asp:TemplateField HeaderText="Total Paid (E)" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotalPaid" Text='<%#Eval("TotalPaid") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Final Due (F) = (D)-(E)" HeaderStyle-Width="20%" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFinalDue" Text='<%#Eval("FinalDue") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Admit Card Block">
                            <HeaderTemplate>
                                <label>Admit Card Block</label>
                                <hr />
                                <asp:CheckBox ID="chkSelectAllAdmitCardBlock" runat="server" Text="All"
                                    AutoPostBack="true" OnCheckedChanged="chkSelectAllAdmitCardBlock_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox runat="server" ID="ChkIsAdmitCardBlock" ></asp:CheckBox>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="140px" />
                        </asp:TemplateField>
                        <%--Text='<%#Eval("IsAdmitCardBlock") %>'--%>
                        <%--<asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRemark" Text='<%#Eval("Remark") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Visible ="false"
                                    ToolTip="Bill History Edit" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible ="false" OnClientClick="return confirm('Do you really want to delete this bill?');"
                                    ToolTip="Bill History Delete" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle CssClass="center" />
                        </asp:TemplateField>--%>
                   
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
                    </ContentTemplate>
            </asp:UpdatePanel>
    </div>
</asp:Content>
