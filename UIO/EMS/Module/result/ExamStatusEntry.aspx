<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamStatusEntry.aspx.cs" Inherits="EMS.miu.result.ExamStatusEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Exam Status Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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

    <style>
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle floatLeft">
        <label>Exam Status Entry</label>
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
            <div class="ExamResultEntry-container">
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

                        <div class="display-inline">
                            <label class="display-inline field-Title"></label><br />
                            <asp:Button ID="btnLoadResult" runat="server" Text="Get All Student" class="button-margin btn-size" OnClick="LoadResult_Click" />
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnSubmitStudentMarkTop">
                        <div class="loadedArea">
                            <div class="SpecialForButton">
                                <asp:Button ID="btnSubmitAllStudentMarkTop" runat="server" Text="Save All Student Exam Status" class="button-margin btn-size1 floatLeft" OnClick="SubmitAllStudentMark_Click" />
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
            <div class="ExamResultEntry-container">
                    <asp:Panel ID="PnlExamResultSubmit" runat="server" Wrap="False"><%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamResultSubmit" runat="server" AutoGenerateColumns="False" class="mainTable"  >
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

                               
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hfId1" Visible="false" Text='<%#Eval("Id1") %>' Value='<%#Eval("Id1") %>' />
                                        <asp:Label runat="server" ID="hfCourseHistoryId1" Visible="false" Text='<%#Eval("CourseHistoryId1") %>'  Value='<%#Eval("CourseHistoryId1") %>' />
                                        <asp:Label runat="server" ID="hfExamId1" Visible="false" Text='<%#Eval("ExamId1") %>'  Value='<%#Eval("ExamId1") %>' />
                                        <asp:Label runat="server" ID="hfStatus1"  Visible="false" Text='<%#Eval("Status1") %>' Value='<%#Eval("Status1") %>' />
                                        <%--<asp:Label runat="server" ID="'<%#Eval("Status1") %>'" Text='<%#Eval("Id1")+" "+ Eval("CourseHistoryId1")+" "+ Eval("ExamId1")+" "+ Eval("Status1") %>' Value='<%#Eval("Id1")+" "+ Eval("CourseHistoryId1")+" "+ Eval("ExamId1")+" "+ Eval("Status1") %>' ></asp:Label>--%>
                                        <asp:DropDownList runat="server" ID="ddlStatus1" class="margin-zero dropDownList4" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hfId2" Visible="false" Text='<%#Eval("Id2") %>'  Value='<%#Eval("Id2") %>' />
                                        <asp:Label runat="server" ID="hfCourseHistoryId2" Visible="false" Text='<%#Eval("CourseHistoryId2") %>' Value='<%#Eval("CourseHistoryId2") %>' />
                                        <asp:Label runat="server" ID="hfExamId2" Visible="false" Text='<%#Eval("ExamId2") %>'  Value='<%#Eval("ExamId2") %>' />
                                        <asp:Label runat="server" ID="hfStatus2" Visible="false" Text='<%#Eval("Status2") %>' Value='<%#Eval("Status2") %>' />
                                        <asp:DropDownList runat="server" ID="ddlStatus2" class="margin-zero dropDownList4" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hfId3" Visible="false" Text='<%#Eval("Id3") %>'  Value='<%#Eval("Id3") %>' />
                                        <asp:Label runat="server" ID="hfCourseHistoryId3" Visible="false" Text='<%#Eval("CourseHistoryId3") %>'  Value='<%#Eval("CourseHistoryId3") %>' />
                                        <asp:Label runat="server" ID="hfExamId3" Visible="false" Text='<%#Eval("ExamId3") %>'  Value='<%#Eval("ExamId3") %>' />
                                        <asp:Label runat="server" ID="hfStatus3" Visible="false" Text='<%#Eval("Status3") %>' Value='<%#Eval("Status3") %>' />
                                        <asp:DropDownList runat="server" ID="ddlStatus3" class="margin-zero dropDownList4" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:DropDownList runat="server" ID="ddlStatus4" class="margin-zero dropDownList4"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:DropDownList runat="server" ID="ddlStatus5" class="margin-zero dropDownList4" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:DropDownList runat="server" ID="ddlStatus6" class="margin-zero dropDownList4" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:DropDownList runat="server" ID="ddlStatus7" class="margin-zero dropDownList4" />
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

                                <asp:TemplateField HeaderText="Comment" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtComment" Width="250px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Save" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Save" ID="lbSave" OnClick="lbSave_Click">
                                            <span class="action-container"><img src="../../Images/update.png" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>

                               <%-- <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
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
                                <asp:Button ID="btnSubmitAllStudentMarkButtom" runat="server" Text="Save All Student Exam Status" class="button-margin btn-size1 floatLeft" OnClick="SubmitAllStudentMark_Click" />
                                
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
