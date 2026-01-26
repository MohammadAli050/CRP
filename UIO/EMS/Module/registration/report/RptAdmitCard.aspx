<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptAdmitCard" CodeBehind="RptAdmitCard.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

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
        }

    </script>
    <style type="text/css">
        .auto-style4 {
            width: 120px;
        }

        .auto-style7 {
            width: 145px;
        }

        .auto-style8 {
            width: 51px;
        }

        .auto-style9 {
            width: 60px;
        }

        .auto-style10 {
            width: 39px;
            font-weight: bold;
        }

        .auto-style11 {
            width: 33px;
        }

        .auto-style12 {
            width: 100px;
        }

        .pointer {
            cursor: pointer;
        }

        .center {
            margin: 0 auto;
            padding: 10px;
        }

        .auto-style14 {
            width: 80px;
        }

        .auto-style16 {
            width: 12px;
        }

        .auto-style17 {
            width: 84px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Admit Card :: Program Session And Batch Wise </label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card">
                <div class="card-body">
                    <script type="text/javascript">
                        Sys.Application.add_load(initdropdown);
                    </script>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Student List</b>
                            <div style="height: 80px; width: 150px; overflow-y: auto">
                                <asp:CheckBoxList ID="ddlStudentList" runat="server" CssClass="form-control"></asp:CheckBoxList>
                            </div>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-info form-control" Text="LOAD" OnClick="buttonView_Click" /></td>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" />
                <EnableAction AnimationTarget="btnLoad" Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <div>&nbsp;</div>
    <div>
        <rsweb:ReportViewer ID="AdmitCard" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="50.6%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>

</asp:Content>

