<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_SearchPersonProfile" Codebehind="SearchPersonProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Search Profile</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Search Profile For Update</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="SearchPerson-container">
            <div class="div-margin">
                <label class="display-inline field-Title">Person Type</label>
                <asp:DropDownList runat="server" ID="ddlPersonType" class="margin-zero dropDownList" DataTextField="ValueName" DataValueField="ValueID" />
            </div>
            <div>
                <label class="display-inline field-Title">Keyword</label>
                <asp:TextBox runat="server" ID="txtSearch" class="margin-zero input-Size" placeholder="Name" />
                <asp:Button runat="server" ID="btnSearch" class="button-margin SearchKey" Text="Search" OnClick="btnSearch_OnClick" />
            </div>
        </div>

        <div class="SearchPerson">
            <asp:Panel ID="PnlPerson" runat="server" Wrap="False"><%--Height="397px" ScrollBars="Vertical"--%>
                <asp:gridview ID="gvPerson" runat="server" AutoGenerateColumns="False" class="mainTable">
                    <RowStyle Height="24px" />
                    <AlternatingRowStyle BackColor="#f5fbfb" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="45px" />
                        </asp:TemplateField>                    

                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><asp:Label runat="server" ID="lblName" Font-Bold="False" Text='<%#Eval("FirstName") %>' /></ItemTemplate>
                            <HeaderStyle Width="230px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><asp:Label runat="server" ID="lblFatherName" Font-Bold="False" Text='<%#Eval("FatherName") %>' /></ItemTemplate>
                            <HeaderStyle Width="230px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Birth" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblDOB" Font-Bold="False" Text='<%#Eval("DOB","{0:dd-MMM-yy}") %>' /></ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ToolTip="Edit" ID="lbEdit" CommandArgument='<%#Eval("PersonId") %>' OnClick="lbEdit_Click">
                                    <span class="action-container"><img src="../Images/edit.png" /></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="45px" />
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Data Found !!
                    </EmptyDataTemplate>
                    <RowStyle CssClass="rowCss" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:gridview>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

