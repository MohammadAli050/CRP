<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptDeptWiseRegisteredStudent.aspx.cs" Inherits="EMS.miu.registration.report.RptDeptWiseRegisteredStudent" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Registered Student Count
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
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Registered Student Count :: Department Wise</label>
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
                                <b>Year</b>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlYear" Width="100"  AutoPostBack ="true" />
                            </td>
                            <td>&nbsp</td>        
                            <td><asp:Button ID="Button1" runat="server" Text="LOAD" Width="100" OnClick="buttonView_Click"></asp:Button></td>
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
    <rsweb:ReportViewer ID="DeptWiseRegisteredStudent" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="57%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
</asp:Content>
