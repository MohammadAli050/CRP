<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RelationBetweenDiscountRetake.aspx.cs" Inherits="EMS.BasicSetup.RelationBetweenDiscountRetake" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 980px; height: 580px" id="tblHeader">
                <tr>
                    <td style="width: 250px; height: 25px;" class="td">
                        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099"
                            Width="300px">Relation Between Discount and Retake No</asp:Label>
                    </td>
                    <td style="width: 100px; height: 25px;" class="td">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="735px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style1">
                        <table>
                            <tr>
                                <td align="left">Academic Calender                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboAcaCalender" runat="server" Width="150px"
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                        TabIndex="1" OnSelectedIndexChanged="cboAcaCalender_SelectedIndexChanged1"
                                        AutoPostBack="True">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="left">Program                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboProgram" runat="server" Width="150px"
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                        AutoPostBack="True" ReadOnlyStyle-Font-Italic="true"
                                        TabIndex="3" OnSelectedIndexChanged="cboProgram_SelectedIndexChanged1">
                                        <ReadOnlyStyle Font-Italic="True">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" CssClass="button" Height="25px"
                                        Text="Load" Width="124px" OnClick="btnLoad_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 25px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Height="25px"
                                        Text="Save" Width="124px" OnClick="btnSave_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Height="25px"
                                        Text="Cancel" Width="124px" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" >
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:GridView ID="gvDiscount" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                                            GridLines="Horizontal" HeaderStyle-BackColor="#a3afbc" HeaderStyle-ForeColor="White"
                                             HeaderStyle-Height="30" CellPadding="5" Width="276px">
                                            <AlternatingRowStyle BackColor="#F9F9F9" />
                                            <RowStyle VerticalAlign="Middle" HorizontalAlign="Center" BackColor="White"
                                                ForeColor="#333333" />
                                            <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center"
                                                BackColor="White" ForeColor="#333333" />
                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
