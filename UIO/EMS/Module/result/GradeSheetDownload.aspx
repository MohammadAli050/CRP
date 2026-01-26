<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="GradeSheetDownload.aspx.cs" Inherits="EMS.Module.Result.GradeSheetDownload" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Grade Sheet :: Download</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

        function CheckDropdown() {
            $('#ctl00_MainContainer_lblMsg').text('');

            if ($('#ctl00_MainContainer_ddlAcaCalBatch').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Semester');
                return false;
            }
            if ($('#ctl00_MainContainer_ddlProgram').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Program');
                return false;
            }
            if ($('#ctl00_MainContainer_ddlAcaCalSection').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Course');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="GradeSheetDownload-Wrapper-Mainbody">

        <div class="PageTitle">
            <label>Grade Sheet :: Download</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="TeacherManagement-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <table>
                                <tr>
                                    <td class="auto-style9"><b>Program</b></td>
                                    <td class="auto-style4">
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                    </td>
                                    <td class="auto-style8"><b>Session</b></td>
                                    <td class="auto-style7">
                                        <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                    </td>
                                    <td class="auto-style10">Batch</td>
                                    <td class="auto-style14">
                                        <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                    </td>

                                    <td class="auto-style17"><b>Exam Center</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlExamCenter" OnSelectedIndexChanged="ddlExamCenter_SelectedIndexChanged" AutoPostBack="true" class="margin-zero dropDownList" Width="226px">
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <label class="display-inline field-Title">Course</label>
                                    </td>

                                    <td class="auto-style14">
                                        <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList1" Width="239px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <table>
                                <tr>
                                    
                                    <td class="auto-style1">&nbsp</td>
                                    <%--<td class="auto-style8"><b>Institution</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlInstitution" class="margin-zero dropDownList" Width="231px">
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td class="auto-style1">
                                        <label class="display-inline field-Title1"></label>
                                        <asp:Button runat="server" ID="btnGenerateGradeSheet" Text="Grade Sheet Generate" OnClick="btnGenerateGradeSheet_Click" OnClientClick="return CheckDropdown();" Width="209px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
            <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnSearch" Enabled="false" />                   
                    </Parallel>
                </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnSearch" Enabled="true" />
                        </Parallel>
                </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

    </div>

</asp:Content>
