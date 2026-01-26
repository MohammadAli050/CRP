<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentCourseWaiver" CodeBehind="StudentCourseWaiver.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Course Waiver</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle floatLeft">
            <label>Student Course Waiver</label>
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

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="StudentCourseWaiver-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Student</label>
                            <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="ID" />
                            <asp:Button runat="server" ID="btnSearchName" class="margin-zero btn-size" Text="Search" OnClick="Search_Click" />
                            <asp:DropDownList runat="server" ID="ddlStudentNameID" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="StudentNameID_Changed" Height="20px" Width="252px" />

                            <%--<label class="display-inline field-Title1">Search</label>--%>
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Course</label>
                            <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList1" AutoPostBack="true" OnSelectedIndexChanged="Course_Changed" />

                            <%--<label class="display-inline field-Title1">Search</label>--%>
                            <asp:TextBox runat="server" ID="txtSearchCourse" class="margin-zero input-Size1" placeHolder="Code / Name" />
                            <asp:Button runat="server" ID="btnSearchCourse" class="margin-zero btn-size" Text="Search" OnClick="SearchCourse_Click" />

                            <label class="display-inline field-Title2">Credit(s)</label>
                            <asp:TextBox runat="server" ID="txtCredit" class="margin-zero input-Size2" Enabled="false" />
                            <label class="display-inline field-Title2">Grade</label>
                            <asp:DropDownList runat="server" ID="ddlGrade" class="margin-zero dropDownList2" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Semester</label>
                            <asp:DropDownList runat="server" ID="ddlSemester" class="margin-zero dropDownList3" />

                            <label class="display-inline field-Title2">Type</label>
                            <asp:DropDownList runat="server" ID="ddlWaiverType" class="margin-zero dropDownList4" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">University</label>
                            <asp:TextBox runat="server" ID="txtUniversity" class="margin-zero input-Size3" placeHolder="University Name" />

                            <asp:DropDownList runat="server" ID="ddlUniversity" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="University_Chenged" />

                            <%--<label class="display-inline field-Title1">Search</label>--%>
                            <asp:TextBox runat="server" ID="txtSearchUniversity" class="margin-zero input-Size1" placeHolder="Name" />
                            <asp:Button runat="server" ID="btnSearchUniversity" class="margin-zero btn-size" Text="Search" OnClick="SearchUniversity_Click" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title"></label>
                            <asp:Button runat="server" ID="btnSave" class="margin-zero btn-size" Text="Add" OnClick="Save_Click" />
                            <asp:Button runat="server" ID="btnClean" class="margin-zero btn-size" Text="Clean" OnClick="Clean_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel03">
            <ContentTemplate>
                <div class="StudentCourseWaiver-container" style="width: 100%">
                    <asp:GridView ID="gvCourseWaiver" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                        <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <RowStyle Height="25px"/>

                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("CourseCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Title" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourseName" Font-Bold="True" Text='<%#Eval("CourseName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblObtainedGrade" Font-Bold="True" Text='<%#Eval("ObtainedGrade") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade Point" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblObtainedGPA" Font-Bold="True" Text='<%#Eval("ObtainedGPA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="University" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStudentName" Font-Bold="True" Text='<%#Eval("StudentName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsWaiver" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsWaiver" Enabled="false" Checked='<%#Eval("IsConsiderGPA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsTransfer" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsTransfer" Enabled="false" Checked='<%#Eval("IsMultipleACUSpan") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton CommandArgument='<%#Eval("ID") %>' runat="server" ToolTip="Update" ID="lbSave" OnClick="lbUpdate_Click">
                                        <span class="action-container"><img alt="Update" src="../Images/update.png" /></span>
                                    </asp:LinkButton>                                        
                                </ItemTemplate>
                                <HeaderStyle Width="45px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("ID") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                        <span class="action-container"><img alt="Delete" src="../../Images/delete.png" class="img-action" /></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <b>No Data Found !</b>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="rowCss" />
                        <HeaderStyle CssClass="tableHead" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnView" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnView" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
