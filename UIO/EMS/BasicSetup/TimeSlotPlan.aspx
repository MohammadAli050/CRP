<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="BasicSetup_TimeSlotPlan" CodeBehind="TimeSlotPlan.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style18 {
            border: 1px solid #339933;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 20px;
            width: 180px;
        }

        .style19 {
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 30px;
            width: 188px;
        }

        .style20 {
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 25px;
            width: 188px;
        }

        .style21 {
            border: 1px solid Green;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #336600;
            vertical-align: Middle;
            width: 540px;
            height: 25px;
        }

        .style23 {
            border: 1px solid Green;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #336600;
            vertical-align: Middle;
            width: 310px;
            height: 29px;
        }

        .style24 {
            border: 1px solid Green;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #336600;
            vertical-align: Middle;
            width: 30px;
            height: 29px;
        }

        .style25 {
            border: 1px solid Gray;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            height: 29px;
            width: 188px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width: 950px; height: 500px; margin-top: 10px; margin-left: auto; margin-right: auto">
                <tr>
                    <td colspan="2" align="left" style="border: 1px solid #009900; height: 30px; width: 950px;">
                        <table style="width: 945px; height: 20px;">
                            <tr>
                                <td class="td" style="border: 0px solid #339933; font-family: Arial, Helvetica, sans-serif; font-size: 16px; font-variant: small-caps; line-height: 24px; color: #333333; vertical-align: Middle; height: 20px; width: 180px;">
                                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#4f4a46"
                                        Width="180px">Time Plan</asp:Label>
                                </td>
                                <td class="td" style="height: 25px; width: 751px">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="750px" Height="24px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="border-style: solid; border-color: gray; border-width: 1px;">
                    <td class="td" valign="top" align="left" style="vertical-align: top;">
                        <asp:Panel ID="pnlCollection" runat="server" BackColor="#ebfbe2"
                            BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="1px" Height="450px"
                            Width="460px">
                            <table style="border: 0px solid gray; width: 459px; height: 450px;">
                                <tr style="border: 1px solid gray;">
                                    <td colspan="2" style="border: 0px solid green; height: 25px; width: 280px" class="td">
                                        <asp:TextBox ID="txtSrch" runat="server" TabIndex="1" Width="270px"></asp:TextBox>
                                    </td>
                                    <td align="center" style="border: 1px solid gray; width: 140px; height: 25px" class="td">
                                        <asp:Button ID="btnFind" runat="server" OnClick="btnFind_Click" Text="Find" Width="80"
                                            CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border: 1px solid Gray;">
                                    <td align="center"
                                        style="border: 1px solid gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center"
                                        style="border: 1px solid gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Width="80"
                                            CssClass="button" />
                                    </td>
                                    <td align="center"
                                        style="border: 1px solid gray; width: 140px; height: 25px;" class="td">
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Width="80"
                                            Text="Delete" CssClass="button" />
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: green; border-width: 0px;" class="tr">
                                    <td class="td" colspan="3" style="border: 0px solid green; vertical-align: top; height: 362px;">
                                        <asp:Panel ID="pnlGrid" runat="server" Width="450px" ScrollBars="Vertical">
                                            <asp:GridView ID="gvwCollection" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#336666" BorderWidth="1px"
                                                CellPadding="4" Height="120px" TabIndex="6" Width="430px"
                                                GridLines="Horizontal" BorderStyle="Double">
                                                <RowStyle Height="24px" BackColor="White" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ControlStyle Width="35px" />
                                                        <FooterStyle Width="35px" />
                                                        <HeaderStyle Width="35px" />
                                                        <ItemStyle Width="35px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" ReadOnly="True">
                                                        <ControlStyle Width="152px" />
                                                        <FooterStyle Width="152px" />
                                                        <HeaderStyle Width="152px" HorizontalAlign="Left" ForeColor="#ffffff" />
                                                        <ItemStyle Width="152px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EndTime" HeaderText="End Time" ReadOnly="True">
                                                        <ControlStyle Width="145px" />
                                                        <FooterStyle Width="145px" />
                                                        <HeaderStyle Width="145px" HorizontalAlign="Left" ForeColor="#ffffff" />
                                                        <ItemStyle Width="145px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <PagerStyle BackColor="#336666" ForeColor="White"
                                                    HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#57b846" ForeColor="White"
                                                    Height="24px" Font-Bold="True" />
                                                <HeaderStyle BackColor="#57b846" Font-Bold="True"
                                                    Height="24px" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="td" valign="top" align="left" style="vertical-align: top;">
                        <asp:Panel ID="pnlClass" runat="server" BackColor="#ebfbe2"
                            BorderColor="ActiveBorder" BorderStyle="solid" BorderWidth="1px" Height="450px" Width="498px">

                            <table style="border: 0px solid green; width: 459px;">
                                <tr style="border-style: solid; border-color: green; border-width: 1px;" class="tr">
                                    <td style="border: 0px solid #009900; font-family: Arial, Helvetica, sans-serif; font-size: 13px; line-height: 24px; color: #333333; vertical-align: Middle; height: 30px; width: 188px; font-weight: bold;"
                                        valign="top" align="left" class="td">Start Time
                                    </td>
                                    <td style="border: 0px solid #009900; height: 30px; width: 238px;" valign="top" align="left" class="td">

                                        <asp:TextBox ID="txtStHr" runat="server" Width="60px" MaxLength="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label1" runat="server" Text="hr"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtStMin" runat="server" Width="60px" MaxLength="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label2" runat="server" Text="min"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="ddlSt" runat="server" Height="25px" Width="66px">
                                        </asp:DropDownList>

                                    </td>
                                    <td style="border: 0px solid #009900; height: 30px; width: 30px;" valign="top"
                                        align="center" class="td">
                                        <asp:RequiredFieldValidator ID="rvFirstName2" runat="server"
                                            ValidationGroup="vdSave" ControlToValidate="ddlSt">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: Gray; border-width: 1px;" class="tr">
                                    <td style="border: 0px solid #009900; font-size: 13px; font-weight: bold;" valign="top" align="left" class="style20">End Time</td>
                                    <td style="border: 0px solid #009900; height: 30px; width: 238px;" valign="top" align="left" class="td">

                                        <asp:TextBox ID="txtEtHr" runat="server" Width="60px" MaxLength="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label4" runat="server" Text="hr"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtEtMin" runat="server" Width="60px" MaxLength="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label5" runat="server" Text="min"></asp:Label>&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlEt" runat="server" Height="25px" Width="66px">
                                            </asp:DropDownList>

                                    </td>
                                    <td style="border: 0px solid #009900; height: 30px; width: 30px;" valign="top"
                                        align="center" class="td">
                                        <asp:RequiredFieldValidator ID="rvFirstName3" runat="server"
                                            ValidationGroup="vdSave" ControlToValidate="ddlEt">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="border-style: solid; border-color: #009900; border-width: 1px;" class="tr">
                                    <td style="border: 0px solid #009900; font-size: 13px; font-weight: bold;" valign="top" align="left"
                                        class="style25">Type</td>
                                    <td style="border: 0px solid #009900;" valign="top" align="left"
                                        class="style23">

                                        <asp:DropDownList ID="ddlType" runat="server" Height="30px" Width="162px">
                                        </asp:DropDownList>

                                    </td>
                                    <td style="border: 0px solid #009900;" valign="top"
                                        align="center" class="style24">

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="ddlType" ValidationGroup="vdSave">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border: thin none #009900;" valign="top" align="center"
                                        class="style21">
                                        <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblDuration" runat="server" BorderColor="#009933"
                                            BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="tr">
                                    <td style="border: 1px solid #009900;" valign="middle" align="center"
                                        class="style20">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="80"
                                            OnClick="btnSave_Click" CssClass="button" />&nbsp;&nbsp;                                            
                                    </td>
                                    <td style="border: 1px solid #009900;" valign="middle" align="center" class="td" colspan="2">&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80"
                                        OnClick="btnCancel_Click" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
