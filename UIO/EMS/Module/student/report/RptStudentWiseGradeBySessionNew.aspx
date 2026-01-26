<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentWiseGradeBySessionNew.aspx.cs" Inherits="EMS.miu.student.report.RptStudentWiseGradeBySessionNew" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Grade Report
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
        .auto-style1 {
            width: 41px;
        }
        .auto-style2 {
            width: 135px;
        }
        .auto-style5 {
            width: 152px;
        }
        .auto-style6 {
            width: 97px;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        .auto-style8 {
            width: 35px;
        }
        .auto-style10 {
            width: 20px;
        }
        .auto-style11 {
            width: 122px;
        }
        .auto-style12 {
            width: 139px;
        }
        .auto-style13 {
            width: 44px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Student Grade Report :: Session Wise</label>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td class="auto-style1">
                                <b>Roll : </b>
                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtStudentRoll" Enabled="false" Width="150" runat="server"></asp:TextBox>
                            </td>
                             <td class="auto-style12">
                                <b>Session Completed : </b>
                            </td>
                            <td class="auto-style5">
                                <asp:DropDownList ID="ddlSession" runat="server" Width="120px"/>
                            </td>                                                                                                                                                                                                                                                                                                                                                
                            <td class="auto-style6"><asp:Button ID="Button1" runat="server" CssClass="pointer" Width="100px" Text="LOAD" OnClick="buttonView_Click" /></td>
                            <td class="auto-style13">&nbsp;</td>
                            <td><asp:Button ID="Button2" runat="server" CssClass="pointer" Width="120px" Text="OPEN  PDF" OnClick="buttonPrint_Click" /></td> 
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
    <rsweb:reportviewer ID="StudentGradeReport" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="57%" Height="100%" sizetoreportcontent="true" >
    </rsweb:reportviewer>

</asp:Content>
