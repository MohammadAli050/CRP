<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ExamRoutine_ExamScheduleUpdate" Codebehind="ExamScheduleUpdate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Routine :: Update</asp:Content>
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
            <label>Setup :: Exam Schedule :: Update</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="ExamScheduleUpdate-container">
            <asp:Button runat="server" ID="btnBackLink" Text="Back" OnClick="btnBackLink_Click" class="button-margin btn-size" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleUpdate-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Exam Set(s)</label>
                            <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Day</label>
                            <asp:DropDownList runat="server" ID="ddlDay" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title2">Slot</label>
                            <asp:DropDownList runat="server" ID="ddlTimeSlot" class="margin-zero dropDownList" />
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
                            <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" class="button-margin btn-size" OnClientClick="return CheckField();" />

                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

