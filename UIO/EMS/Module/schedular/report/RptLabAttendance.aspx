<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptLabAttendance" Codebehind="RptLabAttendance.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Exam Attandance Sheet
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
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
        .auto-style5 {
            width: 67px;
        }
        .auto-style6 {
            width: 365px;
        }
        .center {
            margin: 0 auto;
            padding: 7px;
        }
        .auto-style7 {
            width: 180px;
        }
        .auto-style8 {
            width: 74px;
        }
        .auto-style9 {
            width: 64px;
        }
        .auto-style10 {
            width: 155px;
        }
        .auto-style11 {
            width: 31px;
        }
        .auto-style12 {
            width: 53px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Exam Attandance Sheet :: Section Wise</label>
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
                    <table id="Table1" style="width: 101%; height: 25px;" runat="server">
                        <tr>
                            <td class="auto-style8"><b>Program :</b></td>
                            <td class="auto-style7"> <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>                                                                                                                     
                              
                            <td class="auto-style9"><b>Session :</b></td>
                            <td class="auto-style10"><uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/></td>
                            
                            <td class="auto-style5"><asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Course : " Style="width: 60px; display: inline-block;" Width="65px"></asp:Label></td>
                            <td class="auto-style6"><asp:DropDownList ID="ddlAcaCalSection" runat="server" Style="width: 450px;" /></td>                           
                                                                                  
                            <td><asp:Button ID="Button1" runat="server" Text="Generate" Width="100" OnClick="GetAttendance_Click"></asp:Button></td>
                                
                        </tr>                                                 
                            
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
        
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="width: 1100px; margin-top: 10px;">
                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Count : " Visible="false"></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
</asp:Content>
