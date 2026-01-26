<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptSemesterResultSummary.aspx.cs" Inherits="EMS.miu.result.report.RptSemesterResultSummary" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Semester Result Summary
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
    <style>
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Exam Result Print</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel"> 
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMessage" Text="" />
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="label1" runat="server" Text="Calender:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCalenderType"  AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" Width="100px" />
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Label ID="lblTrees" runat="server" Text="Session:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAcademicCalender"  AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" Width="130px" />
                            </td>
                            <td>&nbsp;</td>         
                            <td><asp:Button runat="server" ID="Button2" Text="LOAD" OnClick="btnLoad_Click" Width="100px" Height="25px"  /> </td>                                
                    </table>

                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
</asp:Content>
