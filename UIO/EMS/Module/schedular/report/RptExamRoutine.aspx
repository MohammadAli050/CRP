<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptExamRoutine" Codebehind="RptExamRoutine.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Exam Routine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
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
        .auto-style2 {
            width: 200px;
        }
        .auto-style3 {
            width: 71px;
        }
        .auto-style4 {
            width: 130px;
        }
        .auto-style5 {
            width: 70px;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="PageTitle">
        <label>Exam Routine :: Calender and Session Wise</label>
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

    <div class="Message-Area" style="height: 33px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>            
                <div>
                        <table>
                            <tr>
                                <td style="font-weight: bold;" class="auto-style5">
                                    <asp:Label ID="lblPrograms" runat="server" Text="Calender:"></asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <asp:DropDownList runat="server" ID="ddlCalenderType"  AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" Width="100px" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="font-weight: bold;" class="auto-style5">
                                    <asp:Label ID="lblTrees" runat="server" Text="Session:"></asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <asp:DropDownList runat="server" ID="ddlAcademicCalender"  AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" Width="130px" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="font-weight: bold;" class="auto-style5">
                                    <asp:Label ID="lblCalender" runat="server" Text="Exam Set:"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:DropDownList runat="server" ID="ddlExamScheduleSet" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" Width="301px" />
                                </td>
                                <td>&nbsp;</td>
                                <td><asp:Button runat="server" ID="Button2" Text="Load" OnClick="btnLoad_Click" Width="100px" Height="25px"  /> </td> 
                            </tr>                               
                        </table>

                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -32px -35px 0 0;">
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

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
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
        <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" width="58.3%" Height="100%" sizetoreportcontent="true" >
            <%--<LocalReport ReportPath="miu/schedular/report/RptExamRoutine.rdlc">
            </LocalReport>--%>
        </rsweb:reportviewer>
    </div>    
    
</asp:Content>

