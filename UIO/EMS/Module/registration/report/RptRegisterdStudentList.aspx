<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptRegisterdStudentList" Codebehind="RptRegisterdStudentList.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Registered Courses
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
        .pointer {
            cursor: pointer;
        }
        
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        
        .auto-style14 {
            width: 80px;
            height: 30px;
        }
        
        .auto-style15 {
            width: 44px;
            height: 30px;
        }
        
        .auto-style16 {
            height: 30px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="PageTitle">
        <label>Registered CourseList :: Program Session And Batch Wise</label>
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

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Program</b></td>
                            <td class="auto-style4"><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>                                                                                                                                                   
                            <td class="auto-style8"><b>Session</b></td>
                            <td class="auto-style7"><uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/></td>
                            <td class="auto-style10" style="visibility:hidden;">Batch</td>
                            <td class="auto-style14" style="visibility:hidden;"><uc1:BatchUserControl runat="server" ID="ucBatch"/></td>
                            <td class="auto-style16"><asp:Button ID="Button1" runat="server" CssClass="pointer" Width="100px" Height="25px" Text="LOAD" OnClick="buttonView_Click" /></td>
                            <td class="auto-style15"></td>
                            <td class="auto-style16"><asp:Button Visible="false" ID="Button2" runat="server" CssClass="pointer" Width="120px" Height="25px" Text="OPEN  PDF" OnClick="buttonPrint_Click" /></td>    
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
            <Triggers>
                <asp:PostBackTrigger ControlID="Button2" />
            </Triggers>
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
    <div> 
        <rsweb:reportviewer ID="RegisteredCourses" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="50.6%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true" >
        </rsweb:reportviewer>
    </div>    
        
</asp:Content>

