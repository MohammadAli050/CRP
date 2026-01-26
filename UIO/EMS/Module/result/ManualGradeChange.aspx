<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    Inherits="Admin_ManualGradeChange" CodeBehind="ManualGradeChange.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course History</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .GridPager a {
            display: block;
            height: 15px;
            min-width: 15px;
            background-color: #3AC0F2;
            color: #fff;
            font-weight: bold;
            border: 1px solid #3AC0F2;
            text-align: center;
            text-decoration: none;
        }

        .GridPager span {
            display: block;
            height: 15px;
            min-width: 15px;
            background-color: #fff;
            color: #3AC0F2;
            font-weight: bold;
            border: 1px solid #3AC0F2;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a:hover {
            background-color: #1e8d12;
            color: #fff;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Student Course History</label>
        </div>

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
                <div class="StudentCourseHistory-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Student ID :</label>
                            <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="Student ID" />
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin SearchKey" />

                            <%--<asp:Panel runat="server" ID="PnProcess" Style="display: none;">
                                <img src="../../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>--%>
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Student Name :</label>
                            <asp:Label runat="server" ID="lblStudentName" class="display-inline field-Title-Fix" Text="..........................................." />

                            <label class="display-inline field-Title1">Batch :</label>
                            <asp:Label runat="server" ID="lblStudentBatch" class="display-inline field-Title-Fix" Text="..............................................." />

                            <label class="display-inline field-Title1">Program :</label>
                            <asp:Label runat="server" ID="lblStudentProgram" class="display-inline field-Title-Fix" Text="..............................................." />

                            <label class="display-inline field-Title1">Major :</label>
                            <asp:Label runat="server" ID="lblStudentMajor" class="display-inline field-Title-Fix" Text="..............................................." />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <div>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender
                ID="ModalPopupExtender1"
                runat="server"
                TargetControlID="btnShowPopup"
                PopupControlID="pnPopUp"
                CancelControlID="btnCancel"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnPopUp" Style="display: none; background-color: whitesmoke;">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="updatePanel-Wrapper" style="width: 950px; padding: 5px; margin: 5px;">
                            <fieldset style="padding: 10px; margin: 5px; border-color: lightgreen;">
                                <legend>Result History</legend>
                                <div class="StudentCourseHistory-container">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left">
                                            <fieldset style="padding: 10px; margin: 5px; border-color: lightgreen; width: 400px;">
                                                <legend>Edit Panel</legend>
                                                <label class="display-inline field-Title" style="width: 120px;">Semester :</label>
                                                <asp:Label ID="pnlblSemester" runat="server" class="margin-zero input-Size"></asp:Label>
                                                <br />
                                                <br />
                                                <label class="display-inline field-Title" style="width: 120px;">Course ID :</label>
                                                <asp:Label ID="pnlblCourseID" runat="server" class="margin-zero input-Size"></asp:Label>
                                                <br />
                                                <br />
                                                <label class="display-inline field-Title" style="width: 120px;">Course Name :</label>
                                                <asp:Label ID="pnlblCourseName" runat="server" class="margin-zero input-Size"></asp:Label>
                                                <br />
                                                <br />
                                                <label class="display-inline field-Title" style="width: 120px;">Total Mark :</label>
                                                <asp:TextBox ID="pntxtTotalMark" runat="server" class="margin-zero input-Size"></asp:TextBox>
                                                <br />
                                                <br />
                                                <label class="display-inline field-Title1" style="width: 120px;">Grade :</label>
                                                <asp:DropDownList ID="pnddlGrade" runat="server" class="margin-zero input-Size"></asp:DropDownList>
                                                <br />
                                                <br />
                                                <label class="display-inline field-Title1" style="width: 120px;">Status :</label>
                                                <asp:DropDownList ID="pnddlStatus" runat="server" class="margin-zero input-Size"></asp:DropDownList>
                                                <br />
                                                <br />
                                                 <label class="display-inline field-Title" style="width: 120px;">Remarks :</label>
                                                <asp:TextBox ID="pnTxtRemarks" runat="server" class="margin-zero input-Size"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:CheckBox ID="pnchkIsConsiderGPA" runat="server" class="margin-zero input-Size" Text="Is Consider GPA"></asp:CheckBox>
                                                <asp:HiddenField runat="server" ID="hdnId" />
                                            </fieldset>
                                            <div style="clear: both;"></div>
                                            <div style="margin-top: 10px; margin-bottom: 5px;">
                                                <asp:Button runat="server" ID="btnUpdte" Text="Update" Style="width: 150px; height: 30px;" OnClick="btnUpdte_Click" />
                                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" Style="width: 150px; height: 30px;" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                        <div style="float: right; width: 450px;">

                                            <fieldset style="padding: 10px; margin: 5px; border-color: lightgreen;">
                                                <legend>History</legend>
                                                <asp:Panel ID="pnPreviousGrade" runat="server" Wrap="False">
                                                    <asp:GridView ID="gvPreviousGrade" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                                                        <AlternatingRowStyle BackColor="#FFFFCC" />
                                                        <RowStyle Height="25px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                                                <HeaderStyle Width="45px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblModifiedDate" Font-Bold="True" Text='<%#Eval("ModifiedDate", "{0:dd-MMM-yyyy}")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Marks" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblObtainedTotalMarks" Font-Bold="True" Text='<%#Eval("ObtainedTotalMarks") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblObtainedGrade" Font-Bold="True" Text='<%#Eval("ObtainedGrade") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GPA" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblObtainedGPA" Font-Bold="True" Text='<%#Eval("ObtainedGPA") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Course Status" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblCourseStatus" Font-Bold="True" Text='<%#Eval("CourseStatus") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="IsConsiderGPA" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblIsConsiderGPA" Font-Bold="True" Text='<%#Eval("IsConsiderGPA") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblRemark" Font-Bold="True" Text='<%#Eval("Remark") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <b>No Previous Grade</b>
                                                        </EmptyDataTemplate>
                                                        <RowStyle CssClass="rowCss" />
                                                        <HeaderStyle CssClass="tableHead" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="LoadStudentCourseHistory">
                    <div class="div-margin">
                        <asp:Label runat="server" ID="lblRegistered" class="tableBanner display-inline">Result of completed/registered courses</asp:Label>
                    </div>
                    <asp:Panel ID="PnlRegisteredCourse" runat="server" Width="100%" Wrap="False">
                        <asp:GridView ID="gvRegisteredCourse" OnSorting="gvRegisteredCourse_Sorting" AllowSorting="true" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <RowStyle Height="25px" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Term" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAttribute3" Font-Bold="True" Text='<%#Eval("Attribute3") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("Semester") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                                     <HeaderTemplate>
                                        <asp:LinkButton ID="lnkCourseCode" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="CourseCode">Course ID</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("CourseCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseName" Font-Bold="True" Text='<%#Eval("CourseName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Marks" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblObtainedTotalMarks" Font-Bold="True" Text='<%#Eval("ObtainedTotalMarks") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblObtainedGrade" Font-Bold="True" Text='<%#Eval("ObtainedGrade") %>' />
                                        <asp:HiddenField runat="server" ID="hdnGradeId" Value='<%#Eval("GradeId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GradePoint" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblObtainedGPA" Font-Bold="True" Text='<%#Eval("ObtainedGPA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseStatus" Font-Bold="True" Text='<%#Eval("CourseStatus.Description") %>' />
                                        <asp:HiddenField runat="server" ID="hdnCourseStatus" Value='<%#Eval("CourseStatusID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsConsiderGPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIsConsiderGPA" Font-Bold="True" Text='<%#Eval("IsConsiderGPA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRemarks" Font-Bold="True" Text='<%#Eval("Remark") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" Text="Edit" ToolTip="Edit" ID="lnkBtnUpdate" CommandArgument='<%#Eval("ID") %>' OnClick="lnkBtnUpdate_Click">                                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <b>No Data Found !</b>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

