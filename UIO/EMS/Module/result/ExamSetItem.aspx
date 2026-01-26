<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Test_ExamSetItem" Codebehind="ExamSetItem.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Set Item</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Exam Set Item</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <div class="ExamSetItem-container">
                <div class="div-margin">
                    <div class="loadArea">
                        <asp:HiddenField ID="MicroTestId" runat="server" />

                        <label class="display-inline field-Title">Exam Set</label>
                        <asp:DropDownList runat="server" ID="ddlExamSet" class="margin-zero dropDownList"/>

                        <label class="display-inline field-Title">Exam</label>
                        <asp:DropDownList runat="server" ID="ddlExam" class="margin-zero dropDownList"/>

                        <asp:Button ID="SaveExamSetItemButton" runat="server" Text="Save(Relation)" OnClick="SaveExamSetItemButton_Click" />
                        <asp:Button ID="UpdateExamSetItem" Visible="false" runat="server" Text="Update" class="button-margin btn-size" OnClick="UpdateExamSetItem_Click" />
                        <asp:Button ID="CancelExamSetItem" Visible="false" runat="server" Text="Cencel" class="button-margin btn-size" OnClick="CancelExamSetItem_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoad" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="divProgress" style="display:none ;  margin-top:-35px">
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                <br />
                Processing your request ...
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>                    
            <asp:GridView ID="gvExamType" runat="server"  AutoGenerateColumns="False" CellSpacing="2"
            EmptyDataText="No data found." DataKeyNames="ItemId" 
            RowCommand="gvExamType_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="728px" OnRowCommand="gvExamType_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ItemId" Visible="false" HeaderText="Id">
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                </asp:BoundField>

        
                <asp:BoundField DataField="ExamSetName" HeaderText="Exam Set Name">
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                </asp:BoundField>

                <asp:BoundField DataField="ExamName" HeaderText="Exam Name">
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                </asp:BoundField>


                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="EditButton" CommandName="ExamSetGroupEdit" Text="Edit" CommandArgument='<%# Bind("ItemId") %>' runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" CommandName="ExamSetGroupDelete" Text="Delete" CommandArgument='<%# Bind("ItemId") %>' runat="server" OnClientClick="return confirm('Are you sure to Delete ?')" />
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

