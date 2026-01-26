<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptRegistrationSummary" CodeBehind="RptRegistrationSummary.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Registration Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
    <style type="text/css">
        .auto-style4 {
            width: 120px;
            height: 30px;
        }

        .auto-style7 {
            width: 145px;
            height: 30px;
        }

        .auto-style8 {
            width: 51px;
            height: 30px;
        }

        .auto-style9 {
            width: 60px;
            height: 30px;
        }

        .auto-style10 {
            width: 39px;
            font-weight: bold;
            height: 30px;
        }

        .auto-style11 {
            width: 33px;
            height: 30px;
        }

        .pointer {
            cursor: pointer;
        }

        .auto-style13 {
            width: 140px;
            height: 30px;
        }

        .center {
            margin: 0 auto;
            padding: 10px;
        }

        .auto-style14 {
            width: 80px;
            height: 30px;
        }

        .auto-style17 {
            width: 84px;
            height: 30px;
        }

        .auto-style18 {
            width: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Registration Summary :: Program Session And Batch Wise</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                <td class="auto-style11"><b>Roll<b></td>
                                <td class="auto-style13">
                                    <asp:DropDownList ID="ddlStudent" runat="server" Width="120px" OnSelectedIndexChanged="ddlRunningStudent_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="loadedArea">
                        <table>
                            <tr>
                                <td class="auto-style17"><b>Exam Center</b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlExamCenter" class="margin-zero dropDownList" Width="226px">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style1">&nbsp</td>
                                <td class="auto-style8"><b>Institution</b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlInstitution" class="margin-zero dropDownList" Width="231px">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style18">&nbsp</td>
                                <td>
                                    <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="buttonView_Click" Width="144px" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <animations>
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
        </animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <div>&nbsp;</div>
    <div>
        <rsweb:ReportViewer ID="RegistrationSummary" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="50.6%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>

</asp:Content>

