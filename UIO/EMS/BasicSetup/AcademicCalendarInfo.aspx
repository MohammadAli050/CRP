<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_AcademicCalendarInfo" CodeBehind="AcademicCalendarInfo.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style6 {
            width: 100%;
        }

        .style7 {
            border: 0px solid #009900;
            width: 93px;
            height: 27px;
        }

        .style8 {
            width: 251px;
            height: 27px;
        }

        .style9 {
            height: 27px;
        }

        .style10 {
            border: 0px solid #009900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-variant: small-caps;
            line-height: 24px;
            color: #4f4a46;
            vertical-align: Middle;
            height: 24px;
            width: 200px;
        }

        .style11 {
            border: 0px solid Gray;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            width: 93px;
            height: 32px;
        }

        .style12 {
            border: 0px solid Gray;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            height: 32px;
        }

        .style13 {
            border: 1px solid Green;
            font: 11px Arial, Helvetica, sans-serif;
            color: #336600;
            vertical-align: Middle;
            height: 27px;
            width: 251px;
        }

        .style14 {
            width: 251px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid gray; width: 1000px; height: 580px; margin-top: 10px; margin-left: auto; margin-right: auto">
                <tr>
                    <td colspan="2" align="left" style="border-style: solid; border-color: Gray; border-width: 1px">

                        <table style="width: 965px; height: 30px;">
                            <tr>
                                <td class="style10">
                                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46"
                                        Width="230px">Academic Calendar Info</asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="border-style: solid; border-color: Gray; border-width: 0px;">
                    <td class="td" valign="top" align="left" style="vertical-align: top;">
                        <asp:Panel ID="pnlStudents" runat="server" BackColor="#ebfbe2"
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="549px"
                            Width="483px">
                            <table style="border: 0px solid Gray; width: 483px;">
                                <tr style="border: 0px solid Gray;">
                                    <td class="td" colspan="2" style="border: 1px solid #009900; height: 27px;">
                                        <asp:DropDownList ID="DDListTriSem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDListTriSem_SelectedIndexChanged"
                                            Width="116px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="50%"></asp:TextBox>
                                    </td>
                                    <td align="center" valign="middle" class="td" style="border: 1px solid gray; height: 27px;">
                                        <asp:Button ID="btnFind" runat="server" CssClass="button" Width="80"
                                            OnClick="btnFind_Click" Text="Find" />
                                    </td>
                                </tr>
                                <tr style="border: 0px solid Gray;">
                                    <td align="center" valign="middle"
                                        style="border: 1px solid gray; height: 27px;">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" Width="80"
                                            Text="Add" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid gray; height: 27px;">
                                        <asp:Button ID="btnEdit" runat="server" CssClass="button" Width="80"
                                            OnClick="btnEdit_Click" Text="Edit" />
                                    </td>
                                    <td align="center" valign="middle"
                                        style="border: 1px solid gray; height: 27px;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button" Width="80"
                                            OnClick="btnDelete_Click" Text="Delete" />
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: Gray; border-width: 0px;" class="tr">
                                    <td class="td" colspan="3" style="border: 0px solid #009900; vertical-align: top; height: 362px;">
                                        <asp:Panel ID="pnlGrid" runat="server" Height="463px" ScrollBars="Vertical">
                                            <asp:GridView ID="gvwStudents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#006666" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Height="120px" TabIndex="6" Width="457px">
                                                <RowStyle ForeColor="#000000" Height="24px" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ControlStyle Width="40px" />
                                                        <FooterStyle Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle Width="40px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Name" HeaderText="Session">
                                                        <ControlStyle Width="100px" />
                                                        <FooterStyle Width="100px" />
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd-MMM-yy}">
                                                        <ControlStyle Width="130px" />
                                                        <FooterStyle Width="130px" />
                                                        <HeaderStyle Width="130px" />
                                                        <ItemStyle Width="130px" />
                                                    </asp:BoundField>
                                                    <%--   <asp:BoundField DataField="TypeName" HeaderText="TypeName" />--%>
                                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd-MMM-yy}">
                                                         <ControlStyle Width="150px" />
                                                        <FooterStyle Width="150px" />
                                                        <HeaderStyle Width="150px" />
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsCurrent" HeaderText="Cur - Session">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsActiveAdmi" HeaderText="Adm - Session">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IsActiveRegi" HeaderText="Reg - Session">
                                                       
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#57b846" Font-Bold="True" ForeColor="White" Height="24px" />
                                                <HeaderStyle BackColor="#57b846" Font-Bold="True" ForeColor="White" Height="24px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="border: 0px solid Gray;" valign="top" class="td">
                        <asp:Panel ID="pnlStudent" runat="server" BackColor="#ebfbe2"
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px"
                            Height="549px" Width="476px">
                            <table style="width: 476px;">
                                <tr style="border-style: solid; border-color: #009900; border-width: 0px;">
                                    <td align="left" class="td" style="height: 27px; width: 93px">Year</td>
                                    <td align="left"
                                        style="border: 0px solid #009900;"
                                        class="style13">
                                        <asp:TextBox ID="txtYear" runat="server" TabIndex="9" Width="45%" MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td align="center" style="border: 0px solid #009900; height: 27px;" class="td">
                                        <asp:RequiredFieldValidator ID="rvYear" runat="server"
                                            ControlToValidate="txtYear" ErrorMessage="Year can not be empty"
                                            ToolTip="Year can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revYear" runat="server"
                                            ControlToValidate="txtYear" ErrorMessage="Year must be 4 digit"
                                            ValidationExpression="^\d\d\d\d" ValidationGroup="vdSave"
                                            ToolTip="Year must be 4 digit">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: #009900; border-width: 0px;">
                                    <td align="left" class="style7">Calendar Type</td>
                                    <td align="left"
                                        style="border: 0px solid #009900;" class="style8">
                                        <asp:DropDownList ID="ddlCalType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCalType_SelectedIndexChanged"
                                            Width="116px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="border: 0px solid #009900;" class="style9">
                                        <asp:RequiredFieldValidator ID="rvCal" runat="server"
                                            ControlToValidate="ddlCalType" ErrorMessage="Calender can not be empty"
                                            ToolTip="Calender can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="td" style="width: 93px; height: 27px">Batch Code</td>
                                    <td align="left" style="border: 0px solid #009900;" class="style14">
                                        <asp:TextBox ID="txtBatch" runat="server" MaxLength="4" TabIndex="8" ReadOnly="true"
                                            Width="25%"></asp:TextBox>
                                    </td>
                                    <td align="center" style="border: 0px solid #009900; height: 27px;">
                                        <asp:RequiredFieldValidator ID="rvBatchCode" runat="server"
                                            ControlToValidate="txtBatch" ErrorMessage="Batch Code can not be empty"
                                            ToolTip="Batch Code can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revBatchCode" runat="server"
                                            ControlToValidate="txtBatch" ErrorMessage="Batch code must be 4 digit"
                                            ToolTip="Batch code must be 4 digit" ValidationExpression="^\d\d\d\d"
                                            ValidationGroup="vdSave">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="td" style="width: 93px; height: 7px">&nbsp;</td>
                                    <td align="left" style="border: 0px solid #009900;" class="style14">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="chkIsCUrr" runat="server" Text="IsCurrent"
                                                        GroupName="radioGroup" /></td>
                                                <td>
                                                    <asp:RadioButton ID="ChkIsNext" runat="server" Text="IsNext"
                                                        GroupName="radioGroup" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center" style="border: 0px solid #009900; height: 27px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" class="td" colspan="2" style="height: 7px">
                                        <table class="style6">
                                            <tr>
                                                <td align="center" class="td">StartDate</td>
                                                <td align="center" class="td">EndDate</td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrStartDate" runat="server" Width="100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrEndDate" runat="server" Width="100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center" style="border: 0px solid #009900; height: 27px;">
                                        <table cellspacing="1" style="height: 47px">
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rvStartDt" runat="server"
                                                        ControlToValidate="clrStartDate" ErrorMessage="Start Date can not be empty"
                                                        ToolTip="Start Date can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rvEndDt" runat="server"
                                                        ControlToValidate="clrEndDate" ErrorMessage="End Date can not be empty"
                                                        ToolTip="End Date can not be empty" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td colspan="2" style="height:25px">
                                        <asp:Label ID="Label1" runat="server" Text="Admission Date Range"></asp:Label></td>
                                </tr>--%>
                                <tr>
                                    <td align="right" class="td" colspan="2" style="height: 7px">
                                        <table class="style6" style="height: 47px">
                                            <tr>
                                                <td>Activate Admission</td>
                                                <td>
                                                    <asp:CheckBox ID="chkAdmiIsActive" runat="server" Text="IsActive" />
                                                </td>
                                            </tr>

                                        </table>

                                        <%--<table class="style6">
                                            <tr>
                                                <td align ="center" class="td">
                                                    StartDate</td>
                                                <td align ="center" class="td">
                                                    EndDate</td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrAdmissionStartDate" runat="server" Width = "100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrAdmissionEndDate" runat="server" Width = "100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </td>
                                    <td align="center" style="border: 0px solid #009900; height: 27px; width: 100px"></td>
                                </tr>

                                <%--<tr>
                                    <td colspan="2" style="height:25px">
                                        <asp:Label ID="Label2" runat="server" Text="Registration Date Range"></asp:Label></td>
                                </tr>--%>
                                <tr style="width: 100%">
                                    <td align="right" class="td" colspan="2" style="height: 7px">
                                        <table class="style6" style="height: 47px">
                                            <tr>
                                                <td>Activate Registration</td>
                                                <td>
                                                    <asp:CheckBox ID="chkRegiIsActive" runat="server" Text="IsActive" />
                                                </td>
                                            </tr>

                                        </table>

                                        <%--<table class="style6">
                                            <tr>
                                                <td align ="center" class="td">
                                                    StartDate</td>
                                                <td align ="center" class="td">
                                                    EndDate</td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrRegistrationStartDate" runat="server" Width = "100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrRegistrationEndDate" runat="server" Width = "100%">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </td>
                                    <td style="width: 100px"></td>

                                </tr>

                                <tr>
                                    <td class="style11" valign="middle" align="center">
                                        <asp:Button ID="butSave" runat="server" CssClass="button" Width="80"
                                            OnClick="butSave_Click" Text="Save" ValidationGroup="vdSave" />
                                    </td>
                                    <td colspan="2" class="style12" align="center" valign="middle">
                                        <asp:Button ID="btnClose" runat="server" CssClass="button" Width="80"
                                            OnClick="btnClose_Click" Text="Cancel" />
                                    </td>
                                </tr>

                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="2" style="border-style: solid; height: 15px; border-color: Gray; border-width: 0px">
                        <asp:ValidationSummary ID="vsCourse" runat="server" BorderStyle="None" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="vdSave" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

