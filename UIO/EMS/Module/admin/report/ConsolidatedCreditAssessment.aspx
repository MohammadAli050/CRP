<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master"  CodeBehind="ConsolidatedCreditAssessment.aspx.cs" Inherits="EMS.miu.admin.ConsolidatedCreditAssessment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Degree Completed Viewer</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .chkList-table
        {
            margin-left: 130px;
        }

        .btn-margin
        {
            margin-left: 10px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
        });

        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please make sure entries are numbers only")
                return false;
            }
            return true;
        }

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Degree Completed Viewer</label>
        </div>

        <asp:Panel runat="server" ID="panelContainer">
            <asp:UpdatePanel runat="server" ID="UpClassSchedule">
                <ContentTemplate>
                    <div style="clear:both"></div>
                    <br>
                    <br></br>
                    <div class="ClassRoutine-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <div style="float: left; height: 20px;">
                                    <div style="float: left;">
                                        <label class="display-inline field-Title">
                                        Program :</label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:ProgramUserControl ID="ucProgram" runat="server" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div style="float: left; height: 21px;">
                                    <div style="float: left;">
                                        <label class="display-inline field-Title">
                                        From Session :</label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:SessionUserControl ID="ucFromSession" runat="server" />
                                    </div>
                                </div>
                                <div style="float: left; height: 21px;">
                                    <div style="float: left;">
                                        <label class="display-inline field-Title">
                                        To Session :</label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:SessionUserControl ID="ucToSession" runat="server" />
                                    </div>
                                </div>
                                <div>
                                    <div style="float: left;">
                                        <label class="display-inline field-Title">
                                        Credit :</label>
                                    </div>
                                    <div style="float: left;">
                                    <asp:TextBox ID="txtCredit" runat="server" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnLoad" runat="server" class="button-margin btn-size" OnClick="btnLoad_Click" Text="Load" />
                            </div>
                            <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -106px">
                                <div style="float: left">
                                    <asp:Image ID="LoadingImage" runat="server" Height="35px" ImageUrl="~/Images/Img/Waiting.gif" Width="35px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </br>
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
                     <rsweb:ReportViewer ID="MeritListRptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">      
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>

