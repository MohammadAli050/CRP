<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentUnBlockByProgram" Codebehind="StudentUnBlockByProgram.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Block Bulk Process
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <%--<link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />--%>
    <style>
        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
            padding: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 5px; width: 1100px;">
        <%-- <div class="PageTitle">
            <label>Student Active</label>
        </div>--%>

        <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
            <%-- <div class="Message-Area">--%>
            <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <%--</div>--%>
        </asp:Panel>

        <div style="clear: both;"></div>

        <div style="width: 1100px; margin-top: 20px;">
            <fieldset>
                <legend>Un-Block Batch Process</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="gvBlockStudentList" AutoGenerateColumns="False" Width="100%"
                            ShowHeader="true" CssClass="gridCss">
                            <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#F0F8FF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Detail Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDetailName" Text='<%#Eval("DetailName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblShortNameWithCode" Font-Bold="true" Text='<%#Eval("ShortNameWithCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Blocked Student Count">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:Label runat="server" ID="lblStudentCount" Font-Bold="true" Text='<%#Eval("StudentCount") %>' Width="80"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:LinkButton runat="server" ID="lnkBtnUnBlock" Text="UnBlock All" OnClick="lnkBtnUnBlock_Click" ToolTip="Unblock all student by program "
                                                CommandArgument='<%#Eval("ProgramID") %>'></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </asp:TemplateField>
                            </Columns>

                            <RowStyle Height="25px" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>

        <div style="clear: both;"></div>

        <div style="width: 1100px; margin-top: 20px;">
            <fieldset>
                <legend>Active Batch Process</legend>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="gvInActiveStudentList" AutoGenerateColumns="False" Width="100%"
                            ShowHeader="true" CssClass="gridCss">
                            <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#F0F8FF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Detail Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDetailName" Text='<%#Eval("DetailName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblShortNameWithCode" Font-Bold="true" Text='<%#Eval("ShortNameWithCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In-Active Student Count">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:Label runat="server" ID="lblStudentCount" Font-Bold="true" Text='<%#Eval("StudentCount") %>' Width="80"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:LinkButton runat="server" ID="lnkBtnActive" Text="Active All" OnClick="lnkBtnActive_Click" ToolTip="Active all student by program "
                                                CommandArgument='<%#Eval("ProgramID") %>'></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle Height="25px" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>
    </div>
</asp:Content>

