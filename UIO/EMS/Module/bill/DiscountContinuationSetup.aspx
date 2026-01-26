<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_DiscountContinuationSetup" Codebehind="DiscountContinuationSetup.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Discount Continuation Setup</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            
        });
        function jScript() {
            $('#MainContainer_gvDiscountContinuationSetup, input[type=text]').addClass('margin-zero input-Size1');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Discount Continuation Setup</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <script type="text/javascript">Sys.Application.add_load(jScript);</script>
                <asp:Panel runat="server" ID="pnMessage">
                    <div class="Message-Area">
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <asp:Panel runat="server" ID="InitialPanel">
                    <div class="DiscountContinuationSetup-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <table>
                                    <tr>
                                    <td>
                                        <label class="display-inline field-Title">Program :</label>
                                    </td>
                                    <td>
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" class="margin-zero dropDownList" />
                                    </td>
                                    <td>
                                        <label class="display-inline field-Title">Batch :</label>
                                    </td>
                                    <td>
                                        <uc1:BatchUserControl runat="server" ID="ucBatch"  class="margin-zero dropDownList"/>
                                    </td>
                                </tr>
                                </table>
                            </div>
                            <div class="loadedArea">
                                <label class="display-inline field-Title"></label>
                                <asp:Button runat="server" ID="btnAdd" class="margin-zero btn-size" Text="Add" OnClick="btnAdd_Click" />

                                <label class="display-inline field-Title2"></label>
                                <asp:Button runat="server" ID="btnView" class="margin-zero btn-size" Text="View" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel03">
            <ContentTemplate>
                <asp:Panel runat="server" ID="DiscountPanel">
                    <div class="DiscountContinuationSetup-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <label class="display-inline field-Title"></label>
                                <asp:Button runat="server" ID="Cancel" class="margin-zero btn-size" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div class="loadedArea">
                                <label class="display-inline field-Title">Discount</label>
                                <asp:DropDownList runat="server" ID="ddlDiscountType" class="margin-zero dropDownList" />

                                <label class="display-inline field-Title2">Min Credits</label>
                                <asp:TextBox runat="server" ID="txtMinCredits" class="margin-zero input-Size" placeholder="Min" />

                                <label class="display-inline field-Title2">Max Credits</label>
                                <asp:TextBox runat="server" ID="txtMaxCredits" class="margin-zero input-Size" placeholder="Max" />

                                <label class="display-inline field-Title2">Min CGPA</label>
                                <asp:TextBox runat="server" ID="txtMinCGPA" class="margin-zero input-Size" placeholder="CGPA" />

                                <label class="display-inline field-Title2">Percent Min</label>
                                <asp:TextBox runat="server" ID="txtPercentMin" class="margin-zero input-Size" placeholder="Min" />

                                <label class="display-inline field-Title2">Percent Max</label>
                                <asp:TextBox runat="server" ID="txtPercentMax" class="margin-zero input-Size" placeholder="Max" />

                                <asp:Button runat="server" ID="btnRowAdd" class="margin-zero btn-size2" Text="Add" OnClick="btnbtnRowAdd_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel04">
            <ContentTemplate>
                <div class="DiscountContinuationSetup-container">
                    <asp:Panel ID="PnlDiscountContinuationSetup" runat="server" Width="940px" Wrap="False">
                        <asp:gridview ID="gvDiscountContinuationSetup" runat="server" AutoGenerateColumns="False" 
                            Width="100%"
                            onrowediting="gvDiscountContinuationSetup_RowEditing"
                            onrowupdating="gvDiscountContinuationSetup_RowUpdating" 
                            onrowcancelingedit="gvDiscountContinuationSetup_RowCancelingEdit"
                            onrowdeleting="gvDiscountContinuationSetup_RowDeleting" >
                            <RowStyle Height="30px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>

                                <asp:CommandField InsertText="" SelectText="" ShowDeleteButton="True" ShowEditButton="True" NewText="" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="150px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Discount" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDiscount" Font-Bold="False" Text='<%#Eval("DiscountType") %>' />
                                        <asp:HiddenField runat="server" ID="hfId" Value='<%#Eval("DiscountContinuationID") %>' />
                                        <asp:HiddenField runat="server" ID="hfTypeDefId" Value='<%#Eval("TypeDefinitionID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MinCredits" HeaderText="MinCredits">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MaxCredits" HeaderText="Max Credits">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MinCGPA" HeaderText="Min CGPA">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PercentMin" HeaderText="PercentMin">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PercentMax" HeaderText="Percentage Max">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>