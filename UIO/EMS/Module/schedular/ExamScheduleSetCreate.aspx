<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ExamRoutine_ExamScheduleSetCreate" Codebehind="ExamScheduleSetCreate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Set::Create</asp:Content>
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

        function CheckField() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlAcademicCalender').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select academic calender');
                return false;
            }
            if ($('#MainContainer_txtExamSetName').val() == '') {
                $('#MainContainer_lblMsg').text('Please exam set name');
                return false;
            }
            if ($('#MainContainer_txtTotalDay').val() == '') {
                $('#MainContainer_lblMsg').text('Please day');
                return false;
            }
            if ($('#MainContainer_txtTodayTimeSlot').val() == '') {
                $('#MainContainer_lblMsg').text('Please time slot');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup :: Exam Schedule Set :: Create</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="ExamScheduleSetCreate-container">
            <asp:Button runat="server" ID="btnBackLink" Text="Back" OnClick="btnBackLink_Click" class="button-margin btn-size1" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleSetCreate-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" />
                        </div>

                        <div class="loadedArea">
                            <label class="display-inline field-Title">Exam Set Name</label>
                            <asp:TextBox runat="server" ID="txtExamSetName" class="margin-zero input-Size2" placeholder="Set Name" />

                            <label class="display-inline field-Title3">Total Day</label>
                            <asp:TextBox runat="server" ID="txtTotalDay" class="margin-zero input-Size" placeholder="# Day" />

                            <label class="display-inline field-Title4">Total Time Slot</label>
                            <asp:TextBox runat="server" ID="txtTodayTimeSlot" class="margin-zero input-Size" placeholder="# Slot Time" DataFormatString="{0:dd/MM/yyyy}" />
                        </div>

                        <div class="loadedArea">
                            <asp:Button runat="server" ID="btnCreate" Text="Create" OnClick="btnCreate_Click" class="button-margin btn-size" OnClientClick="return CheckField();" />

                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel01" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnCreate" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnCreate" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

