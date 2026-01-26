<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentActive" CodeBehind="StudentActive.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Active
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
   <%-- <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />
    <link href="../Content/CssStyle/StudentManagement.css" rel="stylesheet" />--%>

    <style>
        .msgPanel
        {
            margin-top: 20px;
            margin-bottom: 25px;
            background-color: #f9f9f9;
            padding: 5px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 5px; width: 100%;">
        <div class="Message-Area" style="height: 35px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="float: left; padding: 5px;">
                        <div style="float: left;">
                            <asp:Label ID="lbl1" runat="server" Text="Program"></asp:Label>
                        </div>
                        <div style="float: left; margin-left:5px;">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </div>
                    </div>
                    <div style="float: left; margin-left:5px;">
                            <asp:Label ID="Label3" runat="server" Text="Batch"></asp:Label>
                        </div>
                    <div style="float: left; margin-left:5px;">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                        </div>
                    <div style="float: left; padding: 5px;">
                        <div style="float: left; margin-left:5px;">
                            <asp:Label ID="Label4" runat="server" Text="Student ID"></asp:Label>
                        </div>

                        <div style="float: left; margin-left:5px;">
                            <asp:TextBox ID="txtRoll" runat="server" class="display-inline field-Title">  </asp:TextBox>
                        </div>
                    </div>
                    <div style="float: left; padding: 5px;">
                        <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                    </div>
                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -15px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both;"></div>

    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel2"
        runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoad" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoad" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender2"
        TargetControlID="UpdatePanel2"
        runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script = "InProgress();" />
                    <EnableAction AnimationTarget = "btnGenerate" 
                                  Enabled = "false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnGenerate" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="width: 1100px; margin-top: 20px;">

                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

                <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                    ShowHeader="true" CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>

                        <asp:TemplateField HeaderText="FullName">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roll">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btnActive" OnClick="btnActive_Click" runat="server"
                                    Text="Update" />
                                <br />
                                <hr />
                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Active"
                                    AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox runat="server" ID="ChkActive" Checked='<%#Eval("IsActive") %>'></asp:CheckBox>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="140px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtRemarks" Font-Bold="true" Text='<%#Eval("Remarks") %>' Width="440"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="450px" />
                        </asp:TemplateField>
                    </Columns>

                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both"></div>
    <div style="height: 30px; width: 900px; padding: 15px;"></div>

</asp:Content>

