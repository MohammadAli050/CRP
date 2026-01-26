<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="SmsSetup.aspx.cs" Inherits="EMS.miu.admin.SmsSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    SMS Setup
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="well">
        <h3>SMS Setup</h3>
        <asp:Label ID="lblMsg" Style="color: red; font: 18;" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <div class="form-horizontal">

            <div style="float: left">
                <div style="float: Right">
                
                </div>
                <div style="float: left">
                </div>
                <div style="float: left">
                </div>
                <div style="float: right">
                </div>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Height="22px" />
                <br />
            </div>
            <div style="clear: both"></div>
            <br />
            <asp:GridView ID="gvSetup" runat="server" AutoGenerateColumns="False" Visible="true"
                EmptyDataText="No data found."
                GridLines="None" CellSpacing="-1"
                CssClass="table table-bordered table-striped"
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField HeaderText="Registration Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRegStat" runat="server" Checked='<%#Eval("RegistrationStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bill Collection Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBillCollection" runat="server" Checked='<%#Eval("BillCollectionStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Late Fine Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkLateFine" runat="server" Checked='<%#Eval("LateFineStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Waiver Posting Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkWaiverPosting" runat="server" Checked='<%#Eval("WaiverPostingStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Admit Card Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAdmitCardStatus" runat="server" Checked='<%#Eval("AdmitCardStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Result Pub Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkResultPubStatus" runat="server" Checked='<%#Eval("ResultPubStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Result Correction Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkResultCorrectionStatus" runat="server" Checked='<%#Eval("ResultCorrectionStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Custom SMS Status">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCustomSmsStatus" runat="server" Checked='<%#Eval("CustomSmsStatus") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
