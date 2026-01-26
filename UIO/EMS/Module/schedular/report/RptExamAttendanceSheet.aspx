<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptExamAttendanceSheet.aspx.cs" Inherits="EMS.miu.schedular.report.RptExamAttendanceSheet" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Exam Attendance Sheet
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
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        .auto-style1 {
            width: 126px;
        }
        .auto-style2 {
            width: 166px;
        }
        .auto-style3 {
            width: 331px;
        }
        .auto-style4 {
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Exam Attendance Sheet :: Room Wise</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>                  
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrograms" Font-Bold="true" runat="server" Text="Calendar:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList runat="server" ID="ddlCalenderType" Width="100px" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />
                            </td>
                                    
                            <td>
                                <asp:Label ID="lblTrees" Font-Bold="true" runat="server" Text="Session:"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList runat="server" ID="ddlAcademicCalender" Width="140px"  AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />
                            </td>
                            <td>
                                <asp:Label ID="lblCalender" Font-Bold="true" runat="server" Text="Exam:"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:DropDownList runat="server" Width="280" ID="ddlExamScheduleSet" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Day:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList runat="server" ID="ddlDay" Width="100px"  />
                            </td>
                            <td >
                                <asp:Label ID="Label4"  Font-Bold="true" runat="server" Text="Slot:"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList runat="server" ID="ddlTimeSlot" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="TimeSlot_Changed" />
                            </td>                   
                            <td>
                                <asp:Label ID="lblRoom" Font-Bold="true" runat="server" Text="Room : "></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:DropDownList runat="server" ID="ddlRoom" Width="280"></asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="Button1"  runat="server" Text="LOAD" Width="100" OnClick="buttonView_Click"/></td>
                            <td class="auto-style4">&nbsp;</td>                                            
                            <td><asp:Button ID="Button2" runat="server" Text="OPEN  PDF" Width="120" OnClick="buttonPrint_Click"></asp:Button></td>                   
                        </tr>                                                                   
                    </table>
                </div>
           
                <div id="divProgress" style="display: none; width: 165px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="Button2" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

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
    <rsweb:ReportViewer ID="ExamAttandanceSheet" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="51.3%" SizeToReportContent="true">      
        <%--<LocalReport ReportPath="miu/schedular/report/RptExamAttendanceSheet.rdlc">
        </LocalReport>--%>
    </rsweb:ReportViewer>
</asp:Content>
