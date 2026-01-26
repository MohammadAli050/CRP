<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="True" CodeBehind="RptDailyBillHistory.aspx.cs" Inherits="RptDailyBillHistory" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Daily Collection Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <%-- <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />--%>

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
    <style>
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Daily Collection Report : : Program and Date Wise</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <panel id="pnlMessage" runat="server" visible="true" cssclass="msgPanel"> 
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMessage" Text="" ForeColor="Red"/>
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ExamResultPrint-container">
                <div class="div-margin">
                    <div class="Message-Area">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Program" Style="width: 60px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" Style="width: 150px;" AutoPostBack="true">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style2">
                                    <b>From Date</b>
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox runat="server" ID="txtFromDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                                </td>
                                <td class="auto-style2">
                                    <b>To Date</b>
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox runat="server" ID="txtToDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                                </td>
                                <td class="auto-style9">&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnDailyBillHistory" runat="server" Text="Load" Width="150" OnClick="GetDailyBillHistory_Click"></asp:Button>

                                </td>
                                <td>
                                    <div id="divProgress" style="display: none; float: right; z-index: 1000;">
                                        <div style="float: left">
                                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                        </div>
                                    </div>
                                </td>

                            </tr>


                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnDailyBillHistory" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnDailyBillHistory" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <div>&nbsp;</div>
    <center>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="98%" SizeToReportContent="true">
            <LocalReport ReportPath="miu\bill\report\RptDailyBillHistory.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </center>
</asp:Content>

