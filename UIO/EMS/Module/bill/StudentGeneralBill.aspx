<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/Site.Master" AutoEventWireup="true" 
    Inherits="Student_StudentGeneralBill" Codebehind="StudentGeneralBill.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student General Bill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }


        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 1200px">

        <%-- <div class="PageTitle">
            <label>Student General Bill</label>
        </div>--%>

        <div class="Message-Area">
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table style="padding: 1px; width: 900px;">
                    <tr>
                        <td class="tbl-width-lbl">
                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Student ID :"></asp:Label></td>
                        <td class="tbl-width">
                            <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="tbl-width-lbl" style="width:150px;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Registration Session :"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlAcaCalSession" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click"
                                OnClientClick="return confirm('Are you sure, you want to Generate');" />
                        </td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="tbl-width-lbl"><b>Name :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tbl-width-lbl"><b>Batch :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tbl-width-lbl"><b>Program :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                        </td>

                        <td></td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label2" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="GridViewTable">
            <asp:GridView runat="server" ID="gvBillView" AutoGenerateColumns="False" OnRowDataBound="gvBillView_RowDataBound"
                AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                PageSize="20" CssClass="gridCss" ShowFooter="True">
                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" />

                <Columns>
                    <asp:TemplateField HeaderText="Bill Session" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblBillSession" Text='<%#Eval("AcademicCalender.FullCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurpose" Text='<%#Eval("Purpose") %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                             <asp:Label runat="server" ID="lblTotal" Text="Total :"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:Label runat="server" ID="lblTotalAmount"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="After Retake Discount" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmountByCollectiveDiscount" Text='<%#Eval("AmountByCollectiveDiscount") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:Label runat="server" ID="lblTotalAmountByCollectiveDiscount"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="After Tution Waiver" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmountByIterativeDiscount" Text='<%#Eval("AmountByIterativeDiscount") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:Label runat="server" ID="lblTotalAmountByIterativeDiscount"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Course" HeaderStyle-Width="50%"> 
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseInfo.CourseFullInfo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="50%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCourseCredits" Text='<%#Eval("CourseInfo.Credits") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50%" />
                    </asp:TemplateField>

                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                <EmptyDataTemplate>
                    No data found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

