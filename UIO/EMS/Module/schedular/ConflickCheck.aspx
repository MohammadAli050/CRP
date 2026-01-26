<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ConflickCheck" Codebehind="ConflickCheck.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Conflicted Check</asp:Content>
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
            <label>Course Conflict Check</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <%--<asp:UpdatePanel runat="server" ID="UpClassSchedule">
            <ContentTemplate>--%>
                <div class="ConflictCheck-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Semester :</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalBatch" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title2">Program :</label>
                            <asp:DropDownList runat="server" class="margin-zero dropDownList" ID="ddlProgram" DataValueField="ProgramId" DataTextField="NameWithCode" />

                            <asp:Button runat="server" ID="btnLoad" Text="Load"  class="button-margin btn-size" OnClick="btnLoad_Click" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Course(s) :</label>
                            <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList" />

                            <asp:Button runat="server" ID="btnAdd" Text="Add" class="button-margin btn-size" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                    <div class="div-margin">
                        <div>
                            <div class="floatLeft checkPanel">
                                <label>Course List</label><asp:Button runat="server" ID="btnMatchCheck" class="button-margin btn-size2" Text="Check" OnClick="btnMatchCheck_Click" />
                                <div>
                                    <asp:CheckBoxList runat="server" ID="chbCourseList" AutoPostBack="true" OnSelectedIndexChanged="chbCourseList_Click" />
                                </div>
                            </div>
                            <div class="floatLeft studentIdPanel">
                                <label>Student List</label>
                                <div>
                                    <asp:ListBox runat="server" ID="lbStudentList" style="width: 150px; height: 425px; overflow-y: scroll;" AutoPostBack="true" OnSelectedIndexChanged="lbStudentList_Change" />
                                </div>
                            </div>
                            <div class="floatLeft coursePanel" style="width: 30%;">
                                <label>Conflict Course(s)</label>
                                <div><asp:ListBox runat="server" ID="lbConflictCourse" style="width: 150px; height: 150px; overflow-y: scroll;" /></div>
                                <label>Student Course(s)</label>
                                <div><asp:ListBox runat="server" ID="lbStudentCourse" style="width: 150px; height: 258px; overflow-y: scroll;" /></div>
                            </div>
                            <div class="cleaner"></div>
                        </div>
                    </div>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>

