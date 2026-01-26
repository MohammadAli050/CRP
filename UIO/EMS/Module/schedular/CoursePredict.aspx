<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CoursePredict" Codebehind="CoursePredict.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Predict :: Course</asp:Content>
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Generate Course(s)</label>
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
                <div class="CoursePredict-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Program</label>
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="ddlProgram_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title"></label>
                            <asp:Button runat="server" ID="btnGenerate" Text="Generate" OnClick="btnGenerate_Click" class="button-margin btn-size" />

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
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnGenerate" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnGenerate" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

