<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="FeeSetup.aspx.cs" Inherits="EMS.BasicSetup.FeeSetup" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 10px; width: 1250px;">

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both;"></div>
        <div class="Message-Area" style="height: 100px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 980px;" id="tblHeader">                        
                        <tr>
                            <td colspan="2" class="style1">
                                <table>
                                    <tr>
                                        <td align="left">Academic Calender                                         
                                        </td>
                                        <td style="width: 150px; height: 25px">
                                            <dxe:ASPxComboBox ID="cboAcaCalender" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                TabIndex="1" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboAcaCalender_SelectedIndexChanged">
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td align="left">Program                                         
                                        </td>
                                        <td style="width: 150px; height: 25px">
                                            <dxe:ASPxComboBox ID="cboProgram" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                AutoPostBack="True" ReadOnlyStyle-Font-Italic="true"
                                                TabIndex="3" OnSelectedIndexChanged="cboProgram_SelectedIndexChanged">
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
                                                Text="Cancel" Width="124px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div style="clear: both;"></div>
        <div>
            <div style="padding: 1px; margin-top: 20px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:GridView ID="gvDiscount" runat="server" AutoGenerateColumns="False">
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

