<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptTabulationSheet.aspx.cs" Inherits="EMS.miu.result.report.RptTabulationSheet" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Tabulation Sheet
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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
        .pointer {
            cursor: pointer;
        }

        .center {
            margin: 0 auto;
            padding: 10px;
        }

        .scrollingControlContainer {
            overflow-x: auto;
            overflow-y: scroll;
        }

        .scrollingCheckBoxList {
            border: 1px #808080 solid;
            margin: 10px 10px 10px 10px;
            height: 50px;
            width: 150px;
        }

        .auto-style2 {
            width: 11px;
            height: 77px;
        }

        .auto-style3 {
            width: 66px;
            height: 77px;
        }

        .auto-style5 {
            width: 30px;
            height: 77px;
        }

        .auto-style6 {
            width: 55px;
            visibility: hidden;
            height: 77px;
        }
        .auto-style8 {
            height: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Tabulation Sheet :: Program And Session Wise</label>
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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label2" runat="server" Text="Note : " Font-Bold="true" Font-Size="Medium"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="Before generating tabulation sheet, please process student result first." Font-Size="Medium" ForeColor="Red"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table style="height: 80px">
                        <tr>
                            <td class="auto-style8"><b>Program</b></td>
                            <td class="auto-style8">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="auto-style2"></td>
                            <td class="auto-style3"><b>Session</b></td>
                            <td class="auto-style8">
                                <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                            </td>
                            <td class="auto-style8"></td>
                            <td class="auto-style8"><b>Batch</b></td>
                            <td class="auto-style8">
                                <div style="height: 80px; width: 140px; overflow-y: auto">
                                    <asp:CheckBoxList ID="ddlBatchList" runat="server" Height="62px" ></asp:CheckBoxList>
                                </div>
                            </td>
                            <td class="auto-style8"></td>
                            <td colspan="2" style="vertical-align: middle" class="auto-style8">
                                <asp:Button ID="btnLoadStudentRoll" runat="server" CssClass="pointer" Width="114px" Height="25px" Text="Load Student" OnClick="btnLoadStudentRoll_Click" />
                            </td>
                            <td class="auto-style8"><b>Student List</b></td>
                            <td class="auto-style8">
                                <div style="height: 80px; width: 150px; overflow-y: auto">
                                    <asp:CheckBoxList ID="ddlStudentList" runat="server" Height="50px"></asp:CheckBoxList>
                                </div>
                            </td>
                            <td class="auto-style5"></td>
                            <td class="auto-style6"><b>Major</b></td>
                            <td class="auto-style8">
                                <asp:DropDownList Visible="false" ID="ddlMajor" runat="server" AutoPostBack="true" Width="251"></asp:DropDownList>
                            </td>
                            <td colspan="2" style="vertical-align: middle" class="auto-style8">
                                <asp:Button ID="btnLoad" runat="server" CssClass="pointer" Width="100px" Height="25px" Text="LOAD" OnClick="buttonView_Click" />
                            </td>
                        </tr>

                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
