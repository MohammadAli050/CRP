<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    Inherits="ExamScheduleCreate" Codebehind="ExamScheduleCreate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Routine :: Create</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup :: Exam Schedule :: Create</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--<div class="ExamScheduleCreate-container">
            <asp:Button runat="server" ID="btnBackLink" Text="Back" OnClick="btnBackLink_Click" class="button-margin btn-size" />
        </div>--%>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleCreate-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                            <label class="display-inline field-Title3">Exam Set(s)</label>
                            <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Day</label>
                            <asp:DropDownList runat="server" ID="ddlDay" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title2">Slot</label>
                            <asp:DropDownList runat="server" ID="ddlTimeSlot" class="margin-zero dropDownList" />

                            <asp:Button runat="server" ID="btnConflictCheck" Text="Conflict Check" OnClick="btnConflictCheck_Click" class="button-margin btn-size1" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Program</label>
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack="true"   OnSelectedIndexChanged="Program_Changed" />

                            <label class="display-inline field-Title2">Course</label>
                            <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList3" AutoPostBack="true"   OnSelectedIndexChanged="Course_Changed" />
                        </div>
                        <div class="loadedArea1">
                            <asp:CheckBoxList runat="server" ID="cblSection">
                            </asp:CheckBoxList>
                            <div class="cleaner"></div>
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title"></label>
                            <asp:Button runat="server" ID="btnCreate" Text="Create" OnClick="btnCreate_Click" class="button-margin btn-size" />

                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleCreate-container">
                    <asp:Panel ID="PnlExamScheduleCreate" runat="server" Wrap="False"><%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamScheduleList" runat="server" AutoGenerateColumns="False" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Program" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblProgram" Font-Bold="False" Text='<%#Eval("ProgramName") %>' /></ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCourse" Font-Bold="False" Text='<%#Eval("CourseInfo") %>' /></ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblDay" Font-Bold="False" Text='<%#Eval("Day") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Slot" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblTimeSlot" Font-Bold="False" Text='<%#Eval("TimeSlot") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblSection" Font-Bold="False" Text='<%#Eval("SectionList") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Edit" ID="lbEdit" CommandArgument='<%#Eval("Id") %>' OnClick="lbEdit_Click">
                                            <span class="action-container"><img src="../../Images/2.edit.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("Id") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <span class="action-container"><img src="../../Images/3.delete.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
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
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

