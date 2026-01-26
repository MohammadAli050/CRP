<%@ Page Title="Bill Payment History" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="StudentBillHistory.aspx.cs" Inherits="EMS.miu.bill.StudentBillHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bill Payment History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .modalBackground
        {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .font
        {
            font-size: 12px;
        }

        .cursor
        {
            cursor: pointer;
        }

    </style>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Bill Payment History</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Student Roll :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtStudentRoll" PlaceHolder="Student Roll" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="btnLoad" runat="server"  Text="Load" OnClick="btnLoad_Click" /> 
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <b>Student Name :</b>
                        </td>
                        <td colspan="2" class="auto-style6">
                            <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:Label ID="lblStudentProgram" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <b>Total Bill :</b>
                        </td>
                        <td colspan="2" class="auto-style6">
                            <asp:Label ID="lblTotalBill" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <b>Total Payment :</b>
                        </td>
                        <td colspan="2" class="auto-style6">
                            <asp:Label ID="lblTotalPayment" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br/>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvBillMaster"  runat="server"  AutoGenerateColumns="False" ShowFooter="true"
                EmptyDataText="No data found." AllowPaging="false" CellPadding="4" width="700px" OnRowCommand="GvBillMaster_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:TemplateField Visible ="false"  HeaderText="Id">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentId"  runat="server" Text='<%# Bind("StudentId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField Visible ="false"  HeaderText="BillHistoryMasterId">
                        <ItemTemplate >
                            <asp:Label ID="lblBillHistoryMasterId"  runat="server" Text='<%# Bind("BillHistoryMasterId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Code"  HeaderText="Semester">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ReferenceNo"  HeaderText="Reference No">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="BillAmount"  HeaderText="Bill">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="BillingDate"  HeaderText="Billing Date" DataFormatString="{0:dd-MMM-yy}">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PaymentAmount"  HeaderText="Payment">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CollectionDate"  HeaderText="Collection Date" DataFormatString="{0:dd-MMM-yy}">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:TemplateField  HeaderText="Bill View" >
                        <ItemTemplate >
                            <asp:Button ID="btnViewBillHistory" runat="server"  Text="Bill Details" CommandName="ViewBill" CommandArgument='<%#Eval("BillHistoryMasterId")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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

            <asp:Button ID="btnBillHistoryDetail" runat="server" Style="display: none" />

            <cc1:ModalPopupExtender ID="ModalShowBillHistoryDetailsPopupExtender" runat="server" PopupControlID="pnlShowBillHistoryDetailsPopup" 
                CancelControlID="btnShowBillHistoryDetailsCancel"  TargetControlID="btnBillHistoryDetail" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlShowBillHistoryDetailsPopup" runat="server" CssClass="modalPopup" Width="900px" 
            Height="500px" align="center" style="background-color: Window; overflow: scroll; display:none">
                <br/>
                <asp:GridView runat="server" ID="GvStudentBillPaymentHistory"  AutoGenerateColumns="False" 
                CssClass="table-bordered" EmptyDataText="No data found."  CellPadding="4" ShowFooter="true">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="75px" />
                        </asp:TemplateField> 
                
                        <asp:BoundField DataField="BillHistoryId" Visible="false" HeaderText="Code">
                            <HeaderStyle Width="75px" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Bill Type">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTypeName" Text='<%#Eval("FeesName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Semester" HeaderText="Semester">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle CssClass="center" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Bill">
                            <ItemTemplate>
				                <div style="text-align: right;">
				                    <asp:Label ID="lblFee" runat="server" Text='<%# Eval("BillAmount") %>' />
				                </div>
			                </ItemTemplate>
		                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment" >
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPayment" Text='<%#Eval("PaymentAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="ReferenceNo" HeaderText="Ref. No">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle CssClass="center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="BillingDate" HeaderText="Billing Date" DataFormatString="{0:dd-MMM-yy}">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle CssClass="center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MMM-yy}">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle CssClass="center" />
                        </asp:BoundField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
                <br/>
                <asp:Button ID="btnShowBillHistoryDetailsCancel" runat="server" TabIndex="10" Text="Cancel/Close" Width="100" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
