<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptClassAttendanceDatewise.aspx.cs" Inherits="EMS.miu.ClassAttendance.Reports.RptClassAttendanceDatewise" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Date wise Attendace Report
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="well">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h3>Attendance Report Datewise</h3>

                <div class="form-horizontal">

                    <asp:Label ID="lblMsg" Style="font: 18;" runat="server" Text=""></asp:Label><br />

                  <div class="Message-Area">
                    <table id="Table1" style="width: 50%; height: 25px;" runat="server">
                        <tr>
                            <td class="auto-style8"><b>Program :</b></td>
                            <td class="auto-style7"> <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>                                                                                                                     
                              
                            <td class="auto-style9"><b>Session :</b></td>
                            <td class="auto-style10"><uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/></td>
                            
                            
                            
                            
                            <%--<td class="auto-style8"><asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Total Mark : " Style="width: 60px; display: inline-block;" Width="65px"></asp:Label></td>
                            <td class="auto-style7"><asp:TextBox runat="server" ID="txtTotalMark" text="10" Width="23px"  /></td>--%>
                            
                        </tr>  
                               </table>        
                    <table id="Table3" style="width: 72%; height: 25px;" runat="server"> 
                        <tr>
                            <td class="auto-style5"><asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Course : " Style="width: 60px; display: inline-block;" Width="65px"></asp:Label></td>
                            <td class="auto-style6"><asp:DropDownList ID="ddlAcaCalSection" runat="server" Style="width: 450px;" /></td>
                        </tr>
                        </table>     
                    <table id="Table2" style="width: 68%; height: 25px;" runat="server">                                
                        <tr>
                            <td class="auto-style8"><b>From Date :</b></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAttendanceFromDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAttendanceFromDate" Format="dd/MM/yyyy" />
                            </td>
                            <td class="auto-style8"><b>To Date :</b></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAttendanceToDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAttendanceToDate" Format="dd/MM/yyyy" />
                            </td>
                            <td class="auto-style8"><asp:Button ID="btnLoad" runat="server" Text="Load Student" OnClick="btnLoad_Click" /></td>
                        </tr>    
                     </table> 
                </div>
                <div id="div1" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/working.gif" Height="150px" Width="150px" />
        </div>
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
        <center>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ClassAttendance" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="60%" SizeToReportContent="true" Height="33px">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
