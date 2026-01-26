<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptTotalStudentByCourseWise" Codebehind="RptTotalStudentByCourseWise.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Course Wise</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
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
    <style type="text/css">
        .RptTotalStudentByCourseWise-container .field-Title{
            width: 100px;
        }
        .RptTotalStudentByCourseWise-container .field-Title2 {
            width: 115px;
            margin-left: 15px;
        }
        .RptTotalStudentByCourseWise-container .dropDownList {
            width: 135px;
        }
        .RptTotalStudentByCourseWise-container .div-margin {
            margin: 10px 0;
        }
        .RptTotalStudentByCourseWise-container .btn-size {
            margin-left: 15px;
            width: 100px;
            cursor: pointer;
        }
        .RptTotalStudentByCourseWise-container .loadArea {
            margin-top: 20px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
            padding: 5px;
        }
        .RptTotalStudentByCourseWise-container .img-Loading {
            vertical-align: middle;
            padding-bottom: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Student Number :: Course Wise</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="RptTotalStudentByCourseWise-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Program</label>
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="ddlProgram_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" />

                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />

                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="RptTotalStudentByCourseWise-container">
                    <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" Height="100%" sizetoreportcontent="true">
                        <LocalReport ReportPath="miu/schedular/report/RptTotalStudentByCourseWise.rdlc"></LocalReport>
                    </rsweb:reportviewer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>