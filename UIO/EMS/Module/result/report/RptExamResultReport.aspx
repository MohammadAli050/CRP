<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptExamResultReport.aspx.cs" Inherits="EMS.miu.result.report.RptExamResultReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Exam Result Report Print
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
            document.getElementById("blurOverlay").style.display = "block";
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
            document.getElementById("blurOverlay").style.display = "none";
        }


        function initdropdown() {
            $('#ctl00_MainContainer_ddlInstitute').select2({
                allowClear: true
            });

            $('#ctl00_MainContainer_ddlCourse').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlExamTemplateName').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlUploaderOne').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlUploaderTwo').select2({
                allowClear: true
            });


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Exam Result Report Print</label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>



    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                Sys.Application.add_load(initdropdown);
            </script>
            <div class="card">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Course <span class="text-danger">*</span></b>
                            <asp:DropDownList ID="ddlCourse" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button runat="server" ID="ResultLoadButton"
                                Text="View Report"
                                OnClick="ResultLoadButton_Click" Height="38px" CssClass="btn btn-info form-control btn-sm" />
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer
                ID="ExamResultReport"
                runat="server"
                Font-Names="Verdana"
                Font-Size="8pt"
                WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt"
                asynrendering="true"
                Width="80%"
                SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="ResultLoadButton" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="ResultLoadButton" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
