<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentBlock" CodeBehind="StudentBlock.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Block
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style>
        table
        {
            border-collapse: collapse;
        }

        .msgPanel
        {
            margin-top: 20px;
            margin-bottom: 25px;
            border: 1px solid #aaa;
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
        <div class="PageTitle">
            <label>Student Block</label>
        </div>

        <div class="Message-Area" style="height: 45px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                <td class="tbl-width-lbl"><b>Program</b></td>
                                <td>
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                                <td class="tbl-width-lbl"><b>Batch</b></td>
                                <td>
                                    <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                </td>
                                <td>Student Id</td>
                                <td>
                                    <asp:TextBox ID="txtRoll" runat="server" Width="150px">
                                    </asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="tbl-width-lbl"><b>Registration Session</b></td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucRegistrationSession" />
                                </td>
                                <td class="tbl-width-lbl"><b>Session Upto</b></td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucSessionUpto" />
                                </td>
                                <td>Due Amount </td>
                                <td>&nbsp <b style="color: red;">( </b>From &nbsp
                                    <asp:TextBox ID="txtAmountFrom" runat="server" Width="100px">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAmountFrom" runat="server"
                                        ValidationExpression="[+-]?\d+\.?\d*" ErrorMessage="*" ValidationGroup="VG"></asp:RegularExpressionValidator>
                                    &nbsp to  &nbsp
                                     <asp:TextBox ID="txtAmountTo" runat="server" Width="100px">
                                     </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtAmountTo" runat="server"
                                        ValidationExpression="[+-]?\d+\.?\d*" ErrorMessage="*" ValidationGroup="VG"></asp:RegularExpressionValidator>
                                    <b style="color: red;"> )</b>
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" Width="90px" Text="Load" OnClick="btnLoad_Click" ValidationGroup="VG" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <%-- <div id="divProgress" style="display: none; width: 195px; float: right; margin: -32px -35px 0 0;">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                        </div>
                        <div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">
                            Please wait...
                        </div>
                    </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                    <%-- <div class="Message-Area">--%>
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <%--</div>--%>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both;"></div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
        </div>

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
                <div style="width: 100%; margin-top: 20px;">

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" Width="100%"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <%--<HeaderStyle Width="40px" />--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGPA">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="txtCGPA" Font-Bold="true" Text='<%#Eval("CGPA") %>' Width="80"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dues">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="txtDues" Font-Bold="true" Text='<%#Eval("Dues") %>' Width="80"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRemarks" Font-Bold="true" Text='<%#Eval("Remarks") %>' Width="440"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="450px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Admit Card Block">
                                <HeaderTemplate>
                                    <label>Admit Card Block</label>
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAllAdmitCardBlock" runat="server" Text="All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAllAdmitCardBlock_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkIsAdmitCardBlock" Checked='<%#Eval("IsAdmitCardBlock") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Registration Block">
                                <HeaderTemplate>
                                    <label>Registration Block</label>
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAllRegistrationBlock" runat="server" Text="All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAllRegistrationBlock_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkIsRegistrationBlock" Checked='<%#Eval("IsRegistrationBlock") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Result Block">
                                <HeaderTemplate>
                                    <label>Result Block</label>
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAllResultBlock" runat="server" Text="All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAllResultBlock_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkIsResultBlock" Checked='<%#Eval("IsResultBlock") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Master Block">
                                <HeaderTemplate>
                                    <label>Master Block</label>
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAllMasterBlock" runat="server" Text="All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAllMasterBlock_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkIsMasterBlock" Checked='<%#Eval("IsMasterBlock") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <HeaderTemplate>
                                    <asp:Button runat="server" ToolTip="Save/ Edit All" Text="Save All" ID="btnSaveAllHeader"
                                        OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                        OnClick="btnSaveAllHeader_Click"></asp:Button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Save / Edit" ID="lBtnSave"
                                        CommandArgument='<%#Eval("StudentID") %>'
                                        OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                        OnClick="lBtnSave_Click">
                                                <span class="action-container"><img alt="Save" src="../../Images/1.add.png" /></span>
                                    </asp:LinkButton>
                                </ItemTemplate>

                                <HeaderStyle Width="100px" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />

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
    </div>
</asp:Content>

