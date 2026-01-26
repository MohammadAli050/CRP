<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="ExamStudentPresent.aspx.cs" Inherits="ExamStudentPresent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Attendance</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div>

        <div class="PageTitle">
            <label>Student Attendance</label>
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
                <div class="ExamStudentPresent-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title1">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList1" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList1" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                            <label class="display-inline field-Title3">Exam Set(s)</label>
                            <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title1">Day</label>
                            <asp:DropDownList runat="server" ID="ddlDay" class="margin-zero dropDownList1" AutoPostBack="true" OnSelectedIndexChanged="Day_Changed" />

                            <label class="display-inline field-Title2">Slot</label>
                            <asp:DropDownList runat="server" ID="ddlTimeSlot" class="margin-zero dropDownList1"  AutoPostBack="true" OnSelectedIndexChanged="TimeSlot_Changed"/>

                            <label class="display-inline field-Title3">Room(s)</label>
                            <asp:DropDownList runat="server" ID="ddlRoom" class="margin-zero dropDownList2" />
                        </div>
                        <div class="loadedArea">
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                        </div>
                        <asp:Panel runat="server" ID="pnSubmitStudentAttendanceTop">
                        <div class="loadedArea">
                            <asp:Button ID="btnSubmitAllAttendanceTop" runat="server" Text="Save" class="button-margin btn-size" OnClick="SubmitAllAttendance_Click" />
                        </div>
                    </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="ExamStudentPresent-container">
                    <asp:Panel ID="PnlExamStudentPresent" runat="server" Wrap="False"><%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamStudentPresent" runat="server" AutoGenerateColumns="False" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblID" Font-Bold="False" Text='<%#Eval("Roll") %>' />
                                        <asp:HiddenField runat="server" ID="hfId" Value='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblName" Font-Bold="False" Text='<%#Eval("Attribute1") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCourseCode" Font-Bold="False" Text='<%#Eval("CourseCode") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblSection" Font-Bold="False" Text='<%#Eval("Attribute2") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsPresent" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsPresent" Checked='<%#Eval("IsPresent") %>'></asp:CheckBox>
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

        <asp:UpdatePanel ID="UpdatePanel04" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnSubmitStudentAttendanceBottom">
                    <div class="ExamStudentPresent-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <asp:Button ID="btnSubmitAllAttendanceTopButtom" runat="server" Text="Save" class="button-margin btn-size" OnClick="SubmitAllAttendance_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>