<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Test_Exam" Codebehind="Exam.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">ExamManagement</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>

        <div class="PageTitle">
            <label>Exam Management</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ExamManagement-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <asp:HiddenField ID="hfExamId" runat="server" Value="0" />

                            <label class="display-inline field-Title">Exam Type</label>
                            <asp:TextBox runat="server" ID="txtExamName" class="margin-zero input-Size" />

                            <label class="display-inline field-Title">Marks</label>
                            <asp:TextBox runat="server" ID="txtMarks" class="margin-zero input-Size" />

                            <asp:Button ID="SaveExamButton" runat="server" Text="Save" OnClick="SaveExamButton_Click" />
                            <asp:Button ID="UpdateExamButton" Visible="false" runat="server" Text="Update" OnClick="UpdateExamButton_Click" />
                            <asp:Button ID="CancelButton" Visible="false" runat="server" Text="Cencel" OnClick="CancelButton_Click" />
                            <div id="divProgress" style="display:none ;">
                                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/loading01.gif" class="img-Loading" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

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
                
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>                    
            <asp:GridView ID="GridViewExam" runat="server"  AutoGenerateColumns="False" CellSpacing="2"
            EmptyDataText="No data found." DataKeyNames="ExamId" 
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            OnRowCommand="GridViewExam_RowCommand" Width="728px" OnDataBound="GridViewExam_DataBound">
            <Columns>
                <asp:BoundField DataField="ExamId" Visible="false" HeaderText="Id">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="150px" />
            </asp:BoundField>

            <asp:BoundField DataField="ExamName"  HeaderText="Exam Name">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="150px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Marks" HeaderText="Marks">
                <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="150px" />
            </asp:BoundField>
       
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="EditButton" CommandName="ExamEdit" Text="Edit" CommandArgument='<%# Bind("ExamId") %>' runat="server" />
                    <asp:LinkButton ID="DeleteButton" CommandName="ExamDelete" Text="Delete" CommandArgument='<%# Bind("ExamId") %>' runat="server" OnClientClick="return confirm('Are you sure to Delete ?')" />
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