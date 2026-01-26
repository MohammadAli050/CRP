<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ExamRoutine_ExamScheduleTimeSlotUpdate" Codebehind="ExamScheduleTimeSlotUpdate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Time Slot::Update</asp:Content>
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

            if ($('#MainContainer_ddlStartHour').val() == '0' || $('#MainContainer_ddlStartMin').val() == '0' || $('#MainContainer_ddlStartAmPm').val() == '0') {
                $('#MainContainer_lblMsg').text('Please start time');
                return false;
            }
            if ($('#MainContainer_ddlEndHour').val() == '0' || $('#MainContainer_ddlEndMin').val() == '0' || $('#MainContainer_ddlEndAmPm').val() == '0') {
                $('#MainContainer_lblMsg').text('Please end time');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup :: Exam Schedule Day :: Update</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="ExamScheduleTimeSlotUpdate-container">
            <asp:Button runat="server" ID="btnBackLink" Text="Back" OnClick="btnBackLink_Click" class="button-margin btn-size1" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleTimeSlotUpdate-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Exam Set(s)</label>
                            <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Slot</label>
                            <asp:DropDownList runat="server" ID="ddlTimeSlot" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title2">Start Time</label>
                            <asp:DropDownList runat="server" ID="ddlStartHour" class="margin-zero dropDownList3" />
                            <asp:DropDownList runat="server" ID="ddlStartMin" class="margin-zero dropDownList3" />
                            <asp:DropDownList runat="server" ID="ddlStartAmPm" class="margin-zero dropDownList4" />

                            <label class="display-inline field-Title2">End Time</label>
                            <asp:DropDownList runat="server" ID="ddlEndHour" class="margin-zero dropDownList3" />
                            <asp:DropDownList runat="server" ID="ddlEndMin" class="margin-zero dropDownList3" />
                            <asp:DropDownList runat="server" ID="ddlEndAmPm" class="margin-zero dropDownList4" />
                        </div>
                        <div class="loadedArea">
                            <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" class="button-margin btn-size" OnClientClick="return CheckField();" />

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
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnUpdate" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnUpdate" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

