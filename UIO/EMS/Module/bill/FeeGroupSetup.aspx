<%@ Page Title="Fee Group Setup" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="FeeGroupSetup.aspx.cs" Inherits="EMS.miu.bill.FeeGroupSetup" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fee Group Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .modalBackground
        {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .font
        {
            font-size: 12px;
        }

        .cursor
        {
            cursor: pointer;
        }

    </style>
    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Fee Group Setup</label>
    </div>

    <asp:Panel runat="server" ID="pnMessage">
        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>
    </asp:Panel>

    <div style="clear: both;"></div>
    
    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="padding: 1px; width: auto;">
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Batch :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" />
                        </td>

                        <td class="auto-style1">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                        <td colspan="2">
                            <asp:Button ID="btnAddFeeGroup" runat="server" Text="Create Fee Group" OnClick="btnAddFeeGroup_Click" />
                        </td>
                    </tr>
                </table>
                <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    </div>
                </div>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GvFeeGroup" runat="server" AutoGenerateColumns="False"  EmptyDataText="No Data Found"
                    AllowSorting="true" PageSize="20" CssClass="gridCss" CellPadding="4" >
                    <Columns>
                        <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="30px"/>
                        </asp:TemplateField> 

                        <asp:BoundField DataField="FeeGroupMasterId" Visible="false" HeaderText="Id">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ProgramName"  HeaderText="Program">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="BatchNO"  HeaderText="Batch">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FeeGroupName"  HeaderText="Fee Group">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="FundName"  HeaderText="Fund Name">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="250px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"
                                    ToolTip="Edit Fee Group" CommandArgument='<%#Eval("FeeGroupMasterId") %>'>                                                
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle CssClass="center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                           
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="green" ForeColor="#5D7B9D" HorizontalAlign="left" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnShowFeeGroupPopup" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalFeeGroupPopupExtender" runat="server" TargetControlID="btnShowFeeGroupPopup" PopupControlID="pnlShowFeeGroupPopup"
                CancelControlID="btnShowFeeGroupCancel" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlShowFeeGroupPopup" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                <div style="padding: 5px;">
                    <fieldset style="padding: 5px; border: 2px solid green;">
                        <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: brown; text-align: center">Fee Group Setup</legend>
                        <div style="padding: 5px;">
                            <div class="Message-Area">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel2" runat="server" Visible="true">
                                            <asp:Label ID="lblNew" runat="server" Text="Message : "></asp:Label>
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000"></asp:Label>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <table style="padding: 1px; width: 400px;">
                                <tr>
                                    <td>
                                        <b>Program :</b>
                                    </td>
                                    <td >
                                        <uc1:ProgramUserControl runat="server" ID="ucProgramFeeGroup" OnProgramSelectedIndexChanged="ucProgramFeeGroup_ProgramSelectedIndexChanged"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Batch :</b>
                                    </td>
                                    <td>
                                        <uc1:BatchUserControl runat="server" ID="ucBatchFeeGroup" OnBatchSelectedIndexChanged="ucBatchFeeGroup_BatchSelectedIndexChanged"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Fund Type :</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFundType" runat="server" Width="120" AutoPostBack="true" OnSelectedIndexChanged="ddlFundType_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <b>Fee Group Name :</b>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtFeeGroupName" Width="250" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSaveFeeGroup" runat="server" Text="Save" OnClick="btnSaveFeeGroup_Click"/>
                                        <%--<asp:Button ID="btnPrint" runat="server" TabIndex="9" Text="Print" Width="100" OnClick="btnPrint_Click" />--%>
                                        <asp:Button ID="btnShowFeeGroupCancel" runat="server" TabIndex="10" Text="Cancel/Close" Width="100" OnClick="btnShowFeeGroupCancel_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnShowFeeGroupTypePopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalShowFeeGroupTypePopupExtender" runat="server" TargetControlID="btnShowFeeGroupTypePopup" PopupControlID="pnlShowFeeGroupTypePopup"
                    CancelControlID="btnFeeGroupTypeCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlShowFeeGroupTypePopup" runat="server" BackColor="#ffffff" Width="900px" Style="display: none; border-radius: 3px;">
                    <div style="padding: 5px;">
                        <fieldset style="padding: 5px; border: 2px solid green;">
                            <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: brown; text-align: center">Fee Group Type</legend>
                            <div style="padding: 5px;">
                                <div class="Message-Area">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                                                <asp:Label ID="lblShowFeeGroupTypeMsg" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <table style="padding: 1px; width: 400px;" border="0" >
                                    <tr>
                                        <td>
                                            <b>Fee Group Name :</b>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblFeeGroupName" runat="server" ></asp:Label>
                                            <asp:Label ID="lblFeeGroupMasterId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Fee Type :</b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFeeType" runat="server" AutoPostBack="true" Width="200" OnSelectedIndexChanged="ddlFeeType_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Amount :</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFeeGroupTypeAmount" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnFeeGroupTypeSave" runat="server" Text="Save" OnClick="btnFeeGroupTypeSave_Click"/>
                                            <%--<asp:Button ID="btnPrint" runat="server" TabIndex="9" Text="Print" Width="100" OnClick="btnPrint_Click" />--%>
                                            <asp:Button ID="btnFeeGroupTypeCancel" runat="server" TabIndex="10" Text="Cancel/Close" Width="100" OnClick="btnFeeGroupTypeCancel_Click" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GvFeeGroupType" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowSorting="true" CssClass="gridCss" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <HeaderStyle Width="30px"/>
                                        </asp:TemplateField> 

                                        <asp:BoundField DataField="FeeGroupDetailId" Visible="false" HeaderText="Id">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="150px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FeeName"  HeaderText="Fee Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Amount"  HeaderText="Amount">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRemoveFeeGroupType" runat="server" OnClick="btnRemoveFeeGroupType_Click" Text="Remove"
                                                    ToolTip="Remove Fee Type" CommandArgument='<%#Eval("FeeGroupDetailId") %>'>                                                
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px"></HeaderStyle>
                                            <ItemStyle CssClass="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>

                                    <HeaderStyle BackColor="#ff9933" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="green" ForeColor="#5D7B9D" HorizontalAlign="left" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel2"
        runat="server">
        
        <Animations>
         <OnUpdating>
            <Parallel duration="0">
            <ScriptAction Script="InProgress();" />
            <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel>
         </OnUpdating>
         <OnUpdated>
            <Parallel duration="0">
            <ScriptAction Script="onComplete();" />
            <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
         </Parallel>
         </OnUpdated>
        </Animations>
 </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
