<%@ Page Title="Exam Calculative Template Item" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamTemplateCalculativeItemSetup.aspx.cs" Inherits="EMS.miu.result.ExamTemplateCalculativeItemSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Exam Calculative Template Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
         <h4>Exam Calculative Template Item</h4>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" style="color:red; font:18;" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblTemplateName" runat="server" CssClass="control-newlabel" Text="Exam Template Name"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExamTemplateName" AutoPostBack="True" Width="170px" runat="server" OnSelectedIndexChanged="ddlTemplateName_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblExamMetaType" runat="server" CssClass="control-newlabel" Text="Exam Meta Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExamMetaType" AutoPostBack="True"  Width="170px" runat="server" ></asp:DropDownList>
                        <asp:Label ID="lblExamTemplateCalculativeFormulaId" Visible="false" runat="server" ></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblExamCalculationType" runat="server" CssClass="control-newlabel" Text="Exam Calculation Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExamCalculationType" AutoPostBack="True"  Width="170px" runat="server" ></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Button ID="AddButton" runat="server" Text="Add Template Item" OnClick="AddButton_Click" />
                        <asp:Button ID="UpdateButton" Visible="false" runat="server" Text="Update Template Item" OnClick="UpdateButton_Click" />
                        <asp:Button ID="CancelButton" Visible="false" runat="server" Text="Cancel Edit" OnClick="CancelButton_Click" />
                    </td>
                </tr>
            </table>

            <asp:GridView ID="GvExamTemplateItem" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found." CellPadding="4" OnRowCommand="GvExamTemplateItem_RowCommand" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" HeaderText="Id">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Exam Template Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExamTemplateName" Text='<%#Eval("ExamTemplateMaster.ExamTemplateMasterName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="250px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Exam Meta Type Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExamMetaTypeName" Text='<%#Eval("ExamMetaTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="250px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Exam Calculation Type Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExamCalculationTypeName" Text='<%#Eval("ExamCalculationTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="250px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" CommandName="ExamTemplateItemEdit" Text="Edit" ToolTip="Edit Exam Template Item" CommandArgument='<%# Bind("Id") %>' runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" CommandName="ExamTemplateItemDelete" Text="Delete" OnClientClick="return confirm('Do you really want to delete this exam template item?');" ToolTip="Delete Exam Template Item" CommandArgument='<%# Bind("Id") %>' runat="server"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>
                    </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>