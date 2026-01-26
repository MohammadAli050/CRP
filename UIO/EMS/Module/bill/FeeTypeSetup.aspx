<%@ Page Title="Fee Type Setup" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="FeeTypeSetup.aspx.cs" Inherits="EMS.miu.bill.FeeTypeSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Fee Type Setup
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
<div>
    <div class="PageTitle">
        <label>Fee Type Setup</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table style="padding: 1px; width: auto;">
                    <tr>
                        <td class="auto-style4">
                            <asp:Button ID="btnAddFeeType" runat="server" Text="Create Fee Type" OnClick="btnAddFeeType_Click" />
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
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvFeeType" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found."  CellPadding="4" DataKeyNames="FeeTypeId" OnRowCommand="GvFeeType_RowCommand" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        <HeaderStyle Width="30px"/>
                    </asp:TemplateField> 

                    <asp:BoundField DataField="FeeTypeId" Visible="false" HeaderText="Id">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FeeName"  HeaderText="Fee Name">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
            
                    <asp:BoundField DataField="FeeDefinition"  HeaderText="Fee Definition">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="250px" />
                    </asp:BoundField>          
      
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" CommandName="feeTypeDefinitionEdit" Text="Edit" ToolTip="Edit Fee Type" CommandArgument='<%# Bind("FeeTypeId") %>' runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" CommandName="feeTypeDefinitionDelete" Text="Remove" ToolTip="Remove Fee Type" CommandArgument='<%# Bind("FeeTypeId") %>' runat="server"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnFeeTypePopup" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalFeeTypePopupExtender" runat="server" TargetControlID="btnFeeTypePopup" PopupControlID="pnlFeeTypePopup"
                CancelControlID="btnFeeTypeCancel" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlFeeTypePopup" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                <div style="padding: 5px;">
                    <fieldset style="padding: 5px; border: 2px solid green;">
                        <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: brown; text-align: center">Fee Type Setup</legend>
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
                                    <td >
                                        <asp:Label ID="lblFeeName" runat="server" Width="180px" Text="Fee Name"></asp:Label>        
                                    </td>
                                    <td >      
                                        <asp:TextBox ID="txtFeeName" Width="180px" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblFeeTypeId" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblFeeDefinition" runat="server" Width="180px" Text="Fee Definition"></asp:Label>        
                                    </td>
                                    <td >      
                                        <asp:TextBox ID="txtFeeDefinition" Width="180px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                       <asp:Label ID="lblCourseSpecific" runat="server" Width="180px" Text="Is Course Specific?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbIsCourseSpecific" AutoPostBack="true" class="radio" Width="180px" Text="Is Course Specific?" OnCheckedChanged="rbIsCourseSpecific_CheckedChanged"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblLifeTimeOnceSpecific" runat="server" Width="180px" Text="Is Life Time Once?"></asp:Label>    
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbIsLifeTimeOnceSpecific" AutoPostBack="true" class="radio" Width="180px" Text="Is Life Time Once?" OnCheckedChanged="rbIsLifeTimeOnceSpecific_CheckedChanged"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblPerSemesterSpecific" runat="server" Width="180px" Text="Is Per Semester Specific?"></asp:Label>   
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbIsPerSemesterSpecific" AutoPostBack="true" Width="180px" class="radio" Text="Is Per Semester Specific?" OnCheckedChanged="rbIsPerSemesterSpecific_CheckedChanged"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnFeeTypeSave" runat="server" Visible="false" Text="Save" OnClick="btnFeeTypeSave_Click"/>
                                        <asp:Button ID="btnFeeTypeUpdate" runat="server" Visible="false" Text="Update" OnClick="btnFeeTypeUpdate_Click"/>
                                        <%--<asp:Button ID="btnPrint" runat="server" TabIndex="9" Text="Print" Width="100" OnClick="btnPrint_Click" />--%>
                                        <asp:Button ID="btnFeeTypeCancel" runat="server" TabIndex="10" Text="Cancel/Close" Width="100" OnClick="btnFeeTypeCancel_Click" />
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
</div>
</asp:Content>
