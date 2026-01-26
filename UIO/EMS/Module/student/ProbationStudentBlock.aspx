<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ProbationStudentBlock" Codebehind="ProbationStudentBlock.aspx.cs" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Active
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <style>
        /*table {
            border-collapse: collapse;
        }*/

        /*table, tr, th {
            border: 1px solid #008080;
        }*/

        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            /*border: 1px solid #aaa;*/
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
    <div style="padding: 5px; width: 1200px;">
        <%-- <div class="PageTitle">
            <label>Student Active</label>
        </div>--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="StudentList-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <table>
                                <tr>
                                    <td>
                                        <label class="display-inline field-Title">Program</label>
                                    </td>
                                    <td>
                                         <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ShortName" DataValueField="ProgramID" class="margin-zero dropDownList" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true"/>

                                    </td>
                                    <td>
                                        <label class="display-inline field-Title">Batch</label>
                                    </td>
                                    <td>
                                         <uc1:BatchUserControl runat="server" ID="ucBatch" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Min Probation</label>
                            <asp:TextBox ID="txtMinProbation" runat="server" class="display-inline field-Title">  </asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtMinProbation"
                                            ValidationExpression="^[0-9,]*$"
                                            ErrorMessage="*"
                                            ForeColor="Red"
                                            runat="server" />

                            <label class="display-inline field-Title">Max Probation</label>
                            <asp:TextBox ID="txtMaxProbation" runat="server" class="display-inline field-Title">  </asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            ControlToValidate="txtMaxProbation"
                                            ValidationExpression="^[0-9,]*$"
                                            ErrorMessage="*"
                                            ForeColor="Red"
                                            runat="server" />
                        </div>
                        <div class="loadedArea">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                        </div>
                        <div class="loadedArea">
                            <asp:Button ID="btnBlock" runat="server" Text="Block" OnClick="btnBlock_Click" Height="30px" Width="80px" BackColor="#edd366" />
                            <asp:Button ID="btnUnBlock" runat="server" Text="Un Block" OnClick="btnUnBlock_Click" Height="30px" Width="80px" BackColor="#edd366" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Height="30px" Width="80px" BackColor="#edd366" />


                        </div>
                    </div>
                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -49px -150px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel3"
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
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnBlock" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnBlock" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender3"
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnUnBlock" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnUnBlock" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender4"
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnSave" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnSave" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 1200px; margin-top: 20px;">

                   <%-- <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>--%>

                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SL No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server"
                                        Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkSelect" ></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                    <asp:Label runat="server" ID="lblName" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch Code">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBatchCode" Font-Bold="true" Text='<%#Eval("BatchCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPA">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGPA" Font-Bold="true" Text='<%#Eval("GPA") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGPA">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCGPA" Font-Bold="true" Text='<%#Eval("CGPA") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Probation Count">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProbationCount" Font-Bold="true" Text='<%#Eval("ProbationCount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Block">
                                <%--<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>--%>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkIsBlock" Enabled="false" Checked='<%#Eval("IsBlock") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRemarks" Font-Bold="true" Text='<%#Eval("Remarks") %>' Width="440"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="350px" />
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

