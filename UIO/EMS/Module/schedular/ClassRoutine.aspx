<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ClassRoutine" CodeBehind="ClassRoutine.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Class Routine</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .chkList-table {
            margin-left: 130px;
        }

        .btn-margin {
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

        function MandatoryFieldCheck() {
            if ($('#ctl00_MainContainer_ddlCourse option:selected').val() == "0" || $('#ctl00_MainContainer_txtSection').val() == "" || $('#ctl00_MainContainer_txtCapacity').val() == "") {
                $('#ctl00_MainContainer_lblPopUpMessage').text('Course, Sectoin and Capacity are mandatory');
                return false;
            }
            else {
                $('#ctl00_MainContainer_lblPopUpMessage').text('');
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div style="height: 35px; margin-top: 5px;">
            <div class="PageTitle" style="float: left;">
                <label>Class Routine [edit] → [update] → [delete]</label>
            </div>
            <div id="divProgress" style="display: none; float: right;">
                <div style="float: left">
                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    <asp:Image ID="LoadingImage3" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>


        <asp:Panel runat="server" ID="panelContainer">
            <asp:UpdatePanel runat="server" ID="UpClassSchedule">
                <ContentTemplate>
                    <div class="ClassRoutine-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <div style="float: left;">
                                    <div style="float: left;">
                                        <label class="display-inline field-Title">Program :</label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div style="float: left;">
                                    <div style="float: left;">
                                        <label class="display-inline field-Title" style="margin-left: 5px;">Session :</label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:SessionUserControl runat="server" ID="ucSession" />
                                    </div>
                                </div>
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                            </div>
                            <%--<div class="loadArea">
                                <table style="border:none;">
                                    <tr>
                                        <td>
                                            <label class="display-inline field-Title">Copy To :</label>
                                        </td>
                                        <td>
                                            <uc1:SessionUserControl runat="server" Width="20" ID="ucCopySession" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnCopyTo" Text="Make Duplicate" OnClick="btnCopyTo_Click" class="button-margin btn-size" />
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <div class="loadedArea">
                                <label class="display-inline field-Title-First">Course</label>
                                <asp:TextBox runat="server" ID="txtFilterCourse" class="margin-zero field-width-size-L" />
                                <label class="display-inline field-Title-Others">Room</label>
                                <asp:TextBox runat="server" ID="txtFilterRoom" class="margin-zero field-width-size-Ss" />
                                <label class="display-inline field-Title-Others">Day</label>
                                <asp:TextBox runat="server" ID="txtFilterDay" class="margin-zero field-width-size-Ss" />
                                <label class="display-inline field-Title-Others">Time Slot</label>
                                <asp:TextBox runat="server" ID="txtFilteTimeSlotn" class="margin-zero field-width-size-S" />
                                <label class="display-inline field-Title-Others">Faculty</label>
                                <asp:TextBox runat="server" ID="txtFilterFaculty" class="margin-zero field-width-size-M" />

                                <asp:Button runat="server" ID="btnSearch" Text="Filter" OnClick="btnSearch_Click" class="button-margin btn-size" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <ajaxToolkit:UpdatePanelAnimationExtender
                ID="UpdatePanelAnimationExtender1"
                TargetControlID="UpdatePanel3"
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

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="Message-Area">
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="ClassRoutine-container">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div style="margin: 5px; float: left;">
                                        <asp:Button ID="btnAddNew" runat="server" BackColor="LightSkyBlue" OnClick="btnAddNew_Click" Text="Add New"></asp:Button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="clear: both;"></div>
                        <asp:GridView ID="gvClassSchedule" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="30px" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <RowStyle Height="25" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Update" Text="Edit" ID="lbEdit" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lbEdit_Click"> 
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Conflict" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Room 1" Text="R1" ID="lblR1" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lblR1_Click">                                               
                                        </asp:LinkButton>
                                        &nbsp
                                        <asp:LinkButton runat="server" ToolTip="Room 2" Text="R2" ID="lblR2" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lblR2_Click">                                                
                                        </asp:LinkButton>&nbsp
                                        <asp:LinkButton runat="server" ToolTip="Faculty 1" Text="F1" ID="lblT1" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lblT1_Click">                                                
                                        </asp:LinkButton>&nbsp
                                        <asp:LinkButton runat="server" ToolTip="Faculty 2" Text="F2" ID="lblT2" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lblT2_Click">                                                
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Prog" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("ProgramName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseName" Text='<%#Eval("CourseInfo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSection" Text='<%#Eval("SectionName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Occupied" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblOccupied" Text='<%#Eval("Occupied") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="Sec Defi" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSectionDefi" Text='<%#Eval("SectionDefination") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="Sec Gendeer" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSectionGender" Text='<%#Eval("SectionGender.ValueName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- Blocked By Sajib --%>
                                <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCapacity" Text='<%# Eval("Capacity").ToString() == "0" ? "" : Eval("Capacity") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="Reg-Count" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegisteredCount" Text='<%# Eval("RegisteredCount").ToString() == "0" ? "" : Eval("RegisteredCount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Room (1)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoom1" Text='<%#Eval("RoomInfoOne") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Room (2)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoom2" Text='<%#Eval("RoomInfoTwo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day (1)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDay1" Text='<%#Eval("DayInfoOne") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day (2)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDay2" Text='<%#Eval("DayInfoTwo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time (1)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTimeSlot1" Text='<%#Eval("TimeSlotPlanInfoOne") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time (2)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTimeSlot2" Text='<%#Eval("TimeSlotPlanInfoTwo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Faculty (1)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFaculty1" Text='<%#Eval("TeacherInfoOne") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Faculty (2)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFaculty2" Text='<%#Eval("TeacherInfoTwo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="Share Prog" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblShareProg1" Text='<%#Eval("AllShareProgram") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Template" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGradeSheetTemplate" Text='<%#Eval("ExamTemplateMaster.ExamTemplateMasterName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="Online Template" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblOnlineGradeSheetTemplateID" Text='<%#Eval("ExamTemplate.TemplateName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="R Conflict" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoomConflict" Text='<%#Eval("RoomConflict") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- Blocked By Sajib --%>
                                <%--<asp:TemplateField HeaderText="F Conflict" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFacultyConflict" Text='<%#Eval("FacultyConflict") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Calculative Template" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCalculativeTemplate" Text='<%#Eval("CalculativeExamTemplateMasterName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <%-- <asp:TemplateField HeaderText="Remark" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRemarks" Text='<%#Eval("Remarks") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" Text="Delete" ID="lbDelete" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')"> 
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <label>Data Not Found</label>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div class="ClassRoutine-container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnPopUp"
                    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel runat="server" ID="pnPopUp" Style="display: none;" Width="900">
                    <div>
                        <div class="updatePanel-Wrapper">
                            <fieldset style="padding: 5px; margin: 5px;">
                                <legend style="font-weight: bold; font-size: large; text-align: center">Section</legend>
                                <div>
                                    <div>
                                        <asp:Label runat="server" ID="lblPopUpMessageTitle" Text="Message" CssClass="label-width-popUp"></asp:Label>
                                        <asp:Label runat="server" ID="lblPopUpMessage" Style="color: red;"></asp:Label>
                                    </div>

                                    <hr />

                                    <div runat="server" id="divCourses" style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Course<sup style="color: red;">*</sup></label>
                                        <asp:DropDownList CssClass="combo-width" AutoPostBack="false" EnableViewState="true" runat="server" ID="ddlCourse" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Section<sup style="color: red;">*</sup></label>
                                        <asp:TextBox CssClass="field-width-popUp" runat="server" ID="txtSection" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Capacity<sup style="color: red;">*</sup></label>
                                        <asp:TextBox CssClass="field-width-popUp" runat="server" ID="txtCapacity" />
                                    </div>
                                    <%-- (Visibility Off) Block By Sajib --%>
                                    <div style="float: left; margin-right: 15px; visibility: hidden; height: 0px;">
                                        <label class="label-width">Section Definition</label>
                                        <asp:TextBox CssClass="field-width" runat="server" ID="txtSectionDefination" />
                                    </div>
                                    <div style="clear: both; height: 1px;"></div>

                                    <%-- (Visibility Off) Block By Sajib --%>
                                    <div style="float: left; margin-right: 15px; visibility: hidden; height: 0px;">
                                        <label class="label-width">Section Gender</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlSectionGender" />
                                    </div>
                                </div>
                                <div style="clear: both; margin: 2px"></div>

                                <hr />

                                <div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Room-1 </label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlRoomInfo1" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Room-2 </label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlRoomInfo2" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Room-3 </label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlRoomInfo3" />
                                    </div>
                                    <div style="clear: both;"></div>

                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Day-1</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlDay1" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Day-2</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlDay2" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Day-3</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlDay3" />
                                    </div>
                                    <div style="clear: both;"></div>

                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Time-1</sup></label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlTimeSlot1" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Time-2</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlTimeSlot2" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Time-3</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlTimeSlot3" />
                                    </div>
                                    <div style="clear: both;"></div>

                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Faculty-1</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlFaculty1" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Faculty-2</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlFaculty2" />
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Faculty-3</label>
                                        <asp:DropDownList CssClass="combo-width" runat="server" ID="ddlFaculty3" />
                                    </div>
                                    <div style="clear: both; margin: 2px"></div>

                                    <hr />

                                    <%-- (Visibility Off) Block By Sajib --%>
                                    <div style="float: left; margin-right: 15px; visibility: hidden; height: 0px;">
                                        <div style="float: left;">
                                            <label class="label-width">Add Share Batch</label>
                                        </div>
                                        <div style="float: left;" class="combo-width">
                                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:Button ID="btnBatchAdd" runat="server" Text="Add" CssClass="btn-margin" OnClick="btnBatchAdd_Click" />
                                            <asp:Button ID="btnBatchRemove" runat="server" Text="Remove" CssClass="btn-margin" OnClick="btnBatchRemove_Click" />
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <%-- (Visibility Off) Block By Sajib --%>
                                    <div style="float: left; margin: 5px 15px 5px 0px; visibility: hidden; height: 0px;">
                                        <label class="label-width">Share Batch :</label>
                                        <asp:Label ID="lblShareBatch" runat="server"></asp:Label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <%-- (Visibility Off) Block By Sajib --%>
                                    <div style="float: left; visibility: hidden; height: 0px;">
                                        <div style="float: left">
                                            <label class="label-width">Share Program </label>
                                        </div>
                                        <div style="float: left; margin-left: 5px;">
                                            <asp:CheckBoxList ID="chkListShareProgram" runat="server" RepeatColumns="3" Width="500px">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div style="clear: both;"></div>

                                    <%-- <div style="float: left; margin-right: 15px;">
                                        <label class="label-width">Grade Tem.</label>
                                        <asp:DropDownList runat="server" CssClass="combo-width" ID="ddlGradeSheetTemplate" />
                                    </div>--%>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Basic Grade Template</label><%-- Online Grade Tem. --%><asp:DropDownList runat="server" CssClass="combo-width-sp" ID="ddlOnlineGeadeSheet"></asp:DropDownList>
                                    </div>
                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp">Calculative Grade Template</label><%-- Online Grade Tem. --%><asp:DropDownList runat="server" CssClass="combo-width-sp" ID="ddlCalculativeTemplate"></asp:DropDownList>
                                    </div>
                                    <div style="clear: both;"></div>

                                    <div style="float: left; margin-right: 15px;">
                                        <label class="label-width-popUp" style="vertical-align: top;">Remark</label>
                                        <asp:TextBox CssClass="field-width-popUp-textArea" runat="server" ID="txtRemark" TextMode="MultiLine" Height="50px" />
                                    </div>
                                    <div style="clear: both; margin-top: 15px;"></div>
                                    <div>
                                        <div style="padding: 15px; margin-left: 100px;">
                                            <asp:Button runat="server" ID="btnUpdateInsert" Text="" Style="width: 156px; height: 30px;" OnClick="btnUpdateInsert_Click" OnClientClick="return MandatoryFieldCheck();" />

                                            <asp:Button runat="server" ID="btnAddAndNext" Text="Insert & Next" Style="width: 156px; height: 30px;" OnClick="btnAddAndNext_Click" OnClientClick="return MandatoryFieldCheck();" />

                                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" Style="width: 150px; height: 30px;" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

