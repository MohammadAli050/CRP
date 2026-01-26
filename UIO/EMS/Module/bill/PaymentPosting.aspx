<%@ Page Title="Payment Posting" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="PaymentPosting.aspx.cs" Inherits="EMS.miu.bill.PaymentPosting" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Payment Posting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Payment Posting</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>

                        <td class="auto-style4">
                            <b>Batch :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" />
                        </td>

                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" />
                        </td>

                        <td class="auto-style4">
                            <b>Student Roll :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtStudentRoll" PlaceHolder="Student Roll" runat="server" Width="150px"></asp:TextBox>
                        </td>

                        <td class="auto-style4">
                            <asp:Button ID="btnLoad" runat="server"  Text="Load" OnClick="btnLoad_Click" /> 
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="btnPostPayment" runat="server"  Text="Post Payment" OnClick="btnPostPayment_Click" /> 
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:GridView ID="GvBillPosting"  runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
                    EmptyDataText="No data found." AllowPaging="false" CellPadding="4"  >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible ="false"  HeaderText="Id">
                            <ItemTemplate >
                                <asp:Label ID="lblBillHistoryMasterId"  runat="server" Text='<%# Bind("BillHistoryMasterId") %>'></asp:Label> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField Visible ="false"  HeaderText="StudentId">
                            <ItemTemplate >
                                <asp:Label ID="lblStudentId"  runat="server" Text='<%# Bind("StudentId") %>'></asp:Label> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Roll"  HeaderText="Student Roll">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Name"  HeaderText="Student Name">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="350px" />
                        </asp:BoundField>
                
                        <asp:TemplateField Visible ="True">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="chkAllStudentHeader" OnCheckedChanged="chkAllStudent_CheckedChanged" Text="" AutoPostBack="true"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate >
                                <asp:CheckBox ID="CheckBox"  runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="ReferenceNo"  HeaderText="Reference No">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:TemplateField Visible ="true"  HeaderText="Bank Serial No">
                            <ItemTemplate >
                                <asp:TextBox runat="server" ID="txtBankSerialNo"></asp:TextBox> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Amount"  HeaderText="Amount">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="BillingDate"  HeaderText="Bill Date" DataFormatString="{0:dd-MMM-yy}">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
