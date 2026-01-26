<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="StudentGradeChange.aspx.cs" Inherits="EMS.miu.result.StudentGradeChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Grade Change Only</asp:Content>

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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>

        <div class="PageTitle floatLeft">
        <label>Old Grade Change</label>
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
                <div class="StudentGradeChange-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title1">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                            <label class="display-inline field-Title1">Student ID</label>
                            <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="Student ID"/>

                            <asp:Button ID="btnLoadCourse" runat="server" Text="Load Course" class="button-margin btn-size" OnClick="LoadCourse_Click" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Course</label>
                            <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList1" AutoPostBack="true" OnSelectedIndexChanged="Course_Changed" />
                            <asp:Button ID="btnLoad" runat="server" Text="Load" class="button-margin btn-size" OnClick="Load_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
        <ContentTemplate>
            <div class="StudentGradeChange-container">
                <asp:Panel ID="PnlgvGradeChange" runat="server" Wrap="False">
                    <asp:gridview ID="gvGradeChange" runat="server" AutoGenerateColumns="False" class="mainTable" onrowdatabound="gvGradeChange_RowDataBound" >
                        <RowStyle Height="24px" />
                        <AlternatingRowStyle BackColor="#f5fbfb" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <HeaderStyle Width="45px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <ItemTemplate><asp:Label runat="server" ID="lblCourseCode" Font-Bold="False" Text='<%#Eval("CourseCode") %>' /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:Label runat="server" ID="lblCourseName" Font-Bold="False" Text='<%#Eval("CourseName")%>' /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Previous Result" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:TextBox runat="server" ID="txtObtainedGrade" Enabled="false" Text='<%#Eval("ObtainedGrade")%>' class="margin-zero input-Size" /></ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:DropDownList runat="server" ID="ddlGrade" class="margin-zero dropDownList3" AutoPostBack ="true" OnSelectedIndexChanged="Grade_Changed" /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:DropDownList runat="server" ID="ddlStatus" class="margin-zero dropDownList3" AutoPostBack ="true" OnSelectedIndexChanged="Status_Changed" /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Update" ID="lbUpdate" CommandArgument='<%#Eval("ID") %>' OnClick="lbUpdate_Click">
                                        <input type="button" value="Update" id="btnUpdate" />
                                    </asp:LinkButton>
                                </ItemTemplate>
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

    </div>
</asp:Content>
