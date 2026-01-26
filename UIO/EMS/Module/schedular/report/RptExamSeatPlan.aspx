<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptExamSeatPlan" Codebehind="RptExamSeatPlan.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
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
        .auto-style4 {
            width: 194px;
        }
        .auto-style5 {
            width: 78px;
        }
        .auto-style6 {
            width: 100px;
        }
        .auto-style7 {
            width: 43px;
            font-weight: bold;
        }
        .auto-style9 {
            width: 27px;
            font-weight: bold;
        }
        .auto-style10 {
            width: 76px;
        }
        .auto-style11 {
            width: 102px;
        }
        .auto-style12 {
            width: 107px;
        }
        .auto-style13 {
            width: 52px;
        }
        .auto-style14 {
            width: 90px;
        }
        .auto-style15 {
            width: 87px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Exam Seat Plan :: Calendar And Session Wise </label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                    <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>                  
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both;"></div>

        <div class="Message-Area" style="height: 30px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>            
                    <div>
                         <table  class="table" style="width: 100%; height: 30px;">
                                <tr>
                                    <td style="font-weight: bold;" class="auto-style5">
                                        <asp:Label ID="lblPrograms" runat="server" Text="Calendar:"></asp:Label>
                                    </td>
                                    <td class="auto-style15">
                                        <asp:DropDownList runat="server" ID="ddlCalenderType"  AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />
                                    </td>
                                    
                                    <td style="font-weight: bold;" class="auto-style10">
                                        <asp:Label ID="lblTrees" runat="server" Text="Session:"></asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                        <asp:DropDownList runat="server" ID="ddlAcademicCalender"  AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="width: 80px; font-weight: bold;">
                                        <asp:Label ID="lblCalender" runat="server" Text="Exam Set:"></asp:Label>
                                    </td>
                                    <td class="auto-style12">
                                       <asp:DropDownList runat="server" ID="ddlExamScheduleSet" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" />
                                    </td>
                                    
                                                             
                                        <td style="font-weight: bold;" class="auto-style13">
                                            <asp:Label ID="Label3" runat="server" Text="Day:"></asp:Label>

                                        </td>
                                        <td class="auto-style6"> <asp:DropDownList runat="server" ID="ddlDay" Width="111px"  /></td>

                                        <td class="auto-style7">
                                            <asp:Label ID="Label4" runat="server" Text="Slot:"></asp:Label>

                                        </td>
                                        <td class="auto-style6"> <asp:DropDownList runat="server" ID="ddlTimeSlot" Width="112px"  /></td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style4"><asp:Button runat="server" ID="Button2" Width="80" Text="Load" OnClick="btnLoad_Click"  /> </td> 
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

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script = "InProgress();" />
                    <EnableAction AnimationTarget = "btnGenerate" Enabled = "false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnGenerate" Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="width: 1100px; margin-top: 10px;">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="border: 0px solid gray; padding: 0 0 0 130px;"> 
            <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" Height="100%" sizetoreportcontent="true" >
                  <LocalReport ReportPath="miu/schedular/report/RptExamSeatPlan.rdlc">
                  </LocalReport>
            </rsweb:reportviewer>
        </div>    
        <div style="clear: both"></div>

    </div>
</asp:Content>

