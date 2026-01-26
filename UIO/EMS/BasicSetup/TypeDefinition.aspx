<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_TypeDefinition" CodeBehind="TypeDefinition.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2 {
            height: 26px;
        }

        .style3 {
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 27px;
            width: 120px;
        }

        .style5 {
            height: 26px;
            width: 317px;
        }

        .style6 {
            width: 317px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <table style="border: 1px solid Gray; width: 980px; height: 580px">
                <tr>
                    <td colspan="2" align="left" style="border-style: solid; border-color: Gray; border-width: 1px">

                        <table style="width: 980px; height: 30px;">
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099"
                                        Width="93%">Info</asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                    <td class="td" valign="top" align="left" style="vertical-align: top;">
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#CCFFCC"
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px" Height="549px"
                            Width="483px">
                            <table style="border: 1px solid Gray; width: 483px; height: 550px;">
                                <tr style="border: 1px solid Gray;">
                                    <td class="td" colspan="2" style="height: 27px;">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="94%"></asp:TextBox>
                                    </td>
                                    <td align="center" class="td" style="height: 27px;">
                                        <asp:Button ID="btnFind" runat="server" Text="Find"
                                            CssClass="button" OnClick="btnFind_Click" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid Gray;">
                                    <td align="center"
                                        style="border: 1px solid Gray;">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                            CssClass="button" OnClick="btnAdd_Click" />
                                    </td>
                                    <td align="center"
                                        style="border: 1px solid Gray;">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit"
                                            CssClass="button" OnClick="btnEdit_Click" />
                                    </td>
                                    <td align="center"
                                        style="border: 1px solid Gray; height: 27px;">
                                        <asp:Button ID="btnDelete" runat="server"
                                            Text="Delete" CssClass="button" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;" class="tr">
                                    <td class="td" colspan="3" style="border: 1px solid Gray; vertical-align: top; height: 362px;">
                                        <asp:Panel ID="pnlGrid" runat="server" Height="433px" ScrollBars="Vertical">
                                            <asp:GridView ID="gvwCollection" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                                                CellPadding="4" Height="120px" TabIndex="6" Width="422px"
                                                GridLines="Horizontal">
                                                <RowStyle ForeColor="#333333" Height="24px" BackColor="White" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ControlStyle Width="40px" />
                                                        <FooterStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle Width="40px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Type" HeaderText="Type">
                                                        <ControlStyle Width="60px" />
                                                        <FooterStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Definition"
                                                        HeaderText="Discount Definition" />
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"
                                                    Height="24px" />
                                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White"
                                                    Height="24px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="border: 1px solid Gray;" valign="top" class="td">
                        <asp:Panel ID="pnlTypeDefinition" runat="server" BackColor="#CCFFCC"
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px"
                            Enabled="False" Height="550px" Width="476px">
                            <table style="width: 476px; height: 550px;">
                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                                    <td class="style3" align="left">Type &nbsp;</td>
                                    <td align="left"
                                        style="border: 1px solid Gray;" class="style6">
                                        <asp:DropDownList ID="ddlType" runat="server" Height="20px" Width="258px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Section</asp:ListItem>
                                            <asp:ListItem>Course</asp:ListItem>
                                            <asp:ListItem>Discount</asp:ListItem>
                                            <asp:ListItem>Program</asp:ListItem>
                                            <asp:ListItem>Fee</asp:ListItem>
                                            <asp:ListItem>Fee_PCA</asp:ListItem>
                                            <asp:ListItem>Fee_PCA_GED</asp:ListItem>
                                            <asp:ListItem>Fee_PCA_CORE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="border: 1px solid Gray; height: 27px;" class="td">

                                        <asp:RequiredFieldValidator ID="rvType" runat="server"
                                            ControlToValidate="ddlType" ErrorMessage="Type can not be empty"
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                                    <td align="left" class="style3">Definition</td>
                                    <td align="left"
                                        style="border: 1px solid Gray;" class="style6">
                                        <asp:TextBox ID="txtDefinition" runat="server" Width="81%" TabIndex="8"
                                            MaxLength="48" OnTextChanged="txtDefinition_TextChanged"></asp:TextBox>
                                    </td>
                                    <td align="right" style="border: 1px solid Gray;" class="style2">
                                        <%--<asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtDefinition"
                                            ErrorMessage="Definition can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ControlToValidate="txtDefinition"
                                            ErrorMessage="No space in defination."
                                            ValidationExpression="^[\S]*$" ValidationGroup="vdSave">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>

                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                                    <td align="left" class="style3">Accounts Head</td>
                                    <td align="left"
                                        style="border: 1px solid Gray;" class="style5">
                                        <asp:DropDownList ID="ddlAccHead" runat="server" Height="20px" Width="258px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="border: 1px solid Gray;" class="style2">
                                        <%--<asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>

                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                                    <td align="left" class="style3">Priority</td>
                                    <td align="left" style="border: 1px solid Gray;" class="style6">
                                        <asp:TextBox ID="txtPriority" runat="server" Width="41%" TabIndex="8"
                                            MaxLength="48"></asp:TextBox>
                                    </td>
                                    <td align="right" style="border: 1px solid Gray;" class="style2">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtPriority" ErrorMessage="Only numbers in Priority"
                                            ValidationExpression="^[0-9]+$" ValidationGroup="vdSave">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;">
                                    <td align="left" class="style3">&nbsp;</td>
                                    <td align="left"
                                        style="border: 1px solid Gray;" class="style5">
                                        <asp:CheckBox ID="chkIsCourseSpecificBilling" runat="server"
                                            Text="Is Course Specific Billing" Enabled="False" />
                                        <br />
                                        <asp:CheckBox ID="chkIsLifetimeOnceBilling" runat="server"
                                            Text="Is Lifetime Once Billing" /><br />
                                        <asp:CheckBox ID="chkIsPerAcaCalBilling" runat="server"
                                            Text="Is Per AcaCal Billing" />
                                    </td>
                                    <td align="right" style="border: 1px solid Gray;" class="style2">
                                        <%--<asp:RequiredFieldValidator ID="rvCode" runat="server" 
                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                            ValidationGroup="vdSave">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>


                                <tr>
                                    <td class="style3">
                                        <asp:Button ID="btnSave" runat="server" Text="Save"
                                            ValidationGroup="vdSave" CssClass="button" OnClick="btnSave_Click" />
                                    </td>
                                    <td style="height: 27px;" class="td" colspan="2">
                                        <asp:Button ID="btnCancel" runat="server"
                                            Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-style: solid; border-color: Gray; border-width: 1px;">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="2" style="border-style: solid; border-color: Gray; border-width: 1px">
                        <asp:ValidationSummary ID="vsTypeDefinition" runat="server" BorderStyle="None"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

