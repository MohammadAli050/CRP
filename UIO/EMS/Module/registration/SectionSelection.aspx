<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SectionSelection" Codebehind="SectionSelection.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Section Selection 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
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
    <div style="height: auto; width: 1300px">
        <div style="margin-top: 20px;">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td>Batch :</td>
                            <td>
                                <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                            </td>
                            <td>Program :</td>
                            <td>
                                <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblStudentId" runat="server" Text="Student ID :"></asp:Label>
                                <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divProgress" style="display: none; width: 240px; float: right; z-index: 1000; margin-top: -44px">
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                <br />
                Processing your request, please wait...
            </div>
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel1"
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
            TargetControlID="UpdatePanel1"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="lBtnRef" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="lBtnRef" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <div style="padding: 5px; margin: 5px; width: 900px;">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnMessage" CssClass="common-css">
                        <div>
                            <fieldset>
                                <legend>Message</legend>
                                <div class="education-info-body error-message-purchaseForm">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both"></div>

        <div style="padding: 5px; margin: 5px;">
            <fieldset>
                <legend>Course Registration</legend>
                <div class="education-info-body">
                    <div style="float: right; padding: 5px; margin: 5px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lBtnRefresh" runat="server" Text="Refresh" OnClick="lBtnRefresh_Click"></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="clear: both"></div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="GridViewTable">
                                    <asp:GridView runat="server" ID="gvCourseRegistration" AutoGenerateColumns="False"
                                        AllowPaging="true" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="20" CssClass="gridCss">
                                        <HeaderStyle BackColor="SeaGreen" />

                                        <AlternatingRowStyle BackColor="#FFFFCC" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits","{0:00}") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section" HeaderStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSectionAdd" runat="server" OnClick="btnSectionAdd_Click" Text=" ● Add"
                                                        ToolTip="Add section." CommandArgument='<%#Eval("ID") %>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnRemoveSection" runat="server" OnClick="btnRemoveSection_Click" Text=" ● Remove"
                                                        ToolTip="Remove section from course." CommandArgument='<%#Eval("ID") %>'
                                                        OnClientClick="return confirm('Are you sure to remove section ?')">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section Name & Time" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSection" Text='<%#Eval("SectionName") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conflict" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFormSL" Text='<%#Eval("ConflictedCourse")%>'>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Registration" HeaderStyle-Width="9%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRegistration" runat="server" OnClick="btnRegistration_Click"
                                                        OnClientClick='<%# Eval("CourseTitle","return confirm(\"Are you sure to register course: {0}\")") %>'
                                                        ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? "● Done" : "● Yes" %>                                                     
                                                </div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Direct Admission" HeaderStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnGPA5Student" runat="server"
                                                ToolTip="Consider as a Direct Admission" CommandArgument='<%#Eval("CandidateId") %>'
                                                OnClientClick="return confirm('consider as a DIRECT admission ?')" OnClick="GPA5Student_OnClick">  
                                                <div align="center">
                                                    ● Yes
                                                </div>                                             
                                                                                               
                                            </asp:LinkButton>
                                          
                                            <asp:LinkButton ID="btnGeneralStudent" runat="server"
                                                ToolTip="Consider as a General student" CommandArgument='<%#Eval("CandidateId") %>'
                                                OnClientClick="return confirm('consider as a GENERAL student ?')" OnClick="GeneralStudent_OnClick">    
                                                         <div align="center">
                                                    ● No
                                                </div>                                     
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Approve" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnApproval" runat="server"
                                                OnClientClick='<%# Eval("FormSL","return confirm(\"Are you sure to approve SL NO - {0}\")") %>'
                                                OnClick="Approval_OnClick" ToolTip="Approve application" CommandArgument='<%#Eval("CandidateId") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsApproved").ToString())) ? "● Done" : "● Yes" %>                                                     
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Roll" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAdmissionTestRoll" Font-Bold="True"
                                                Text='<%#  string.Equals(Eval("AdmissionTestRoll"),"-0") ? "N/A" : Eval("AdmissionTestRoll") %>' ></asp:Label>
                                            <asp:Label runat="server" ID="lblFormSubmitDate"
                                                Text='<%#  Eval("FormSubmitDate") == null ? "" : "<br />" + Eval("FormSubmitDate","{0:dd/MM/yyyy}") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Admit Card" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPrintAdmit" runat="server" OnClick="PrintAdmit_OnClick" Text="● Print" ToolTip="Print Admit Card"
                                                CommandArgument='<%#Eval("CandidateId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <EmptyDataTemplate>
                                            No data found!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

