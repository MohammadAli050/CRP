<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="ExamResultChangeGrade.aspx.cs" Inherits="EMS.miu.result.ExamResultChangeGrade" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Grade Change</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            //alert($('#MasterBody').height());
        });
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle floatLeft">
        <label>New Grade Change</label>
    </div>
    <div style="height: 30px;">
        <div id="divProgress" class="floatRight" style="padding-top: 7px; display: none;">
            <div class="floatRight" >
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
            </div>
        </div>
    </div>
    <div class="cleaner"></div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="ExamResultChangeGrade-container">
                <div class="div-margin">
                    <div class="loadArea">

                        <div class="display-inline">
                            <label class="display-inline field-Title">Type</label><br />
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList2" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />
                        </div>
                        
                        <div class="display-inline">
                            <label class="display-inline field-Title">Session</label><br />
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />
                        </div>

                        <div class="display-inline">
                            <label class="display-inline field-Title">Program</label><br />
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="Program_Changed" />
                        </div>

                        <div class="display-inline">
                            <label class="display-inline field-Title">Course</label><br />
                            <asp:DropDownList runat="server" ID="ddlAcaCalSection" class="margin-zero dropDownList1" AutoPostBack ="true" OnSelectedIndexChanged="AcaCalSection_Changed" />
                            <asp:TextBox runat="server" ID="txtSearch" CssClass="margin-zero input-Size1" placeholder="Search"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="button-margin btn-size2" OnClick="Search_Click" />
                        </div>

                        <div class="display-inline">
                            <label class="display-inline field-Title">Exam Type</label><br />
                            <asp:DropDownList runat="server" ID="ddlExam" class="margin-zero dropDownList3" AutoPostBack ="true" OnSelectedIndexChanged="ddlExam_Changed" />
                        </div>

                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Student ID</label>
                        <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size2" placeholder="Student ID"/>

                        <asp:Button ID="btnLoadResult" runat="server" Text="Load" class="button-margin btn-size" OnClick="LoadResult_Click" />
                    </div>

                    <asp:Panel runat="server" ID="pnSubmitStudentMarkTop">
                        <div class="loadedArea">
                            <div class="SpecialForButton">
                                <asp:Button ID="btnGradeChangeTop" runat="server" Text="Change Submit" class="button-margin btn-size1" OnClick="GradeChange_Click" OnClientClick="return confirm('Are you sure to Change Grade ?')" />
                                <div class="cleaner"></div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel03" runat="server">
        <ContentTemplate>
            <div class="ExamResultChangeGrade-container">
                    <asp:Panel ID="PnlExamResultSubmit" runat="server" Wrap="False"><%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamResultSubmit" runat="server" AutoGenerateColumns="False" class="mainTable" onrowdatabound="gvExamResultSubmit_RowDataBound" >
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label runat="server" ID="lblID" Font-Bold="False" Text='<%#Eval("Roll") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblName" Font-Bold="False" Text='<%#Eval("FullName")%>' class="display-inline field-Title2" /></ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark1" Text='<%#Eval("Mark1").ToString() == "0.00" ? "" :  Eval("Mark1") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId1" Value='<%#Eval("Id1") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId1" Value='<%#Eval("CourseHistoryId1") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId1" Value='<%#Eval("ExamId1") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus1" Value='<%#Eval("Status1") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus1" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status1_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark2" Text='<%#Eval("Mark2").ToString() == "0.00" ? "" :  Eval("Mark2") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId2" Value='<%#Eval("Id2") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId2" Value='<%#Eval("CourseHistoryId2") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId2" Value='<%#Eval("ExamId2") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus2" Value='<%#Eval("Status2") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus2" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status2_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark3" Text='<%#Eval("Mark3").ToString() == "0.00" ? "" :  Eval("Mark3") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId3" Value='<%#Eval("Id3") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId3" Value='<%#Eval("CourseHistoryId3") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId3" Value='<%#Eval("ExamId3") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus3" Value='<%#Eval("Status3") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus3" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status3_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark4" Text='<%#Eval("Mark4").ToString() == "0.00" ? "" :  Eval("Mark4") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId4" Value='<%#Eval("Id4") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId4" Value='<%#Eval("CourseHistoryId4") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId4" Value='<%#Eval("ExamId4") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus4" Value='<%#Eval("Status4") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus4" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status4_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark5" Text='<%#Eval("Mark5").ToString() == "0.00" ? "" :  Eval("Mark5") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId5" Value='<%#Eval("Id5") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId5" Value='<%#Eval("CourseHistoryId5") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId5" Value='<%#Eval("ExamId5") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus5" Value='<%#Eval("Status5") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus5" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status5_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark6" Text='<%#Eval("Mark6").ToString() == "0.00" ? "" :  Eval("Mark6") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId6" Value='<%#Eval("Id6") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId6" Value='<%#Eval("CourseHistoryId6") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId6" Value='<%#Eval("ExamId6") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus6" Value='<%#Eval("Status6") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus6" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status6_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:TextBox runat="server" ID="txtMark7" Text='<%#Eval("Mark7").ToString() == "0.00" ? "" :  Eval("Mark7") %>' class="margin-zero input-Size" /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfId7" Value='<%#Eval("Id7") %>' />
                                        <asp:HiddenField runat="server" ID="hfCourseHistoryId7" Value='<%#Eval("CourseHistoryId7") %>' />
                                        <asp:HiddenField runat="server" ID="hfExamId7" Value='<%#Eval("ExamId7") %>' />
                                        <asp:HiddenField runat="server" ID="hfStatus7" Value='<%#Eval("Status7") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hfIsFinalSubmit" Value='<%#Eval("IsFinalSubmit") %>' />--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus7" class="margin-zero dropDownList4" AutoPostBack ="true" OnSelectedIndexChanged="Status7_Changed" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label runat="server" ID="lblTotal" Font-Bold="False" Text='<%#Eval("TotalMark") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblGrade" Font-Bold="False" Text='<%#Eval("Grade")%>' class="display-inline field-Title2" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade Point" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblGradePoint" Font-Bold="False" Text='<%#Eval("GradePoint")%>' class="display-inline field-Title2" /></ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Save" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Save" ID="lbSave" OnClick="lbSave_Click">
                                            <span class="action-container"><img src="../../Images/update.png" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>--%>
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

    <asp:UpdatePanel ID="UpdatePanel04" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnSubmitStudentMarkButtom">
                <div class="ExamResultEntry-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <div class="SpecialForButton">
                                <asp:Button ID="btnGradeChangeBottom" runat="server" Text="Change Submit" class="button-margin btn-size1" OnClick="GradeChange_Click" OnClientClick="return confirm('Are you sure to Change Grade ?')" />
                                <div class="cleaner"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel03" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoadResult" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="btnLoadResult" Enabled="true" />
                    </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

</asp:Content>