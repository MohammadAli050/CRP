<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptAccountStatement.aspx.cs" Inherits="EMS.miu.bill.report.RptAccountStatement" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Acount Statement
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
        .pointer {
            cursor: pointer;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        
        .auto-style3 {
            width: 59px;
        }
        .auto-style5 {
            width: 34px;
        }
        .auto-style6 {
            width: 14px;
        }
        .auto-style7 {
            width: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Student Account Statement :: Student Wise</label>
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
                            <td><b>Program</b></td>
                            <td><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>
                            <td class="auto-style5">&nbsp</td>                                                                                                                                                   
                            <td class="auto-style3"><b>Session</b></td>
                            <td><uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/></td>
                            <td>&nbsp</td>
                            <td class="auto-style7"><b>Batch</b></td>
                            <td><uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged"/></td>
                           <td class="auto-style6">&nbsp</td>
                            <td><b>Roll<b></td>
                            <td>
                                <asp:DropDownList ID="ddlRunningStudent" runat="server" Width="120px" OnSelectedIndexChanged="ddlRunningStudent_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                           </tr>
                        <tr>
                            
                            <td><b>From :</b></td>
                            <td>
                                <asp:TextBox runat="server" ID="dateTextBox1" Width="164px"  class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="DateTextBox1" Format="MM/dd/yyyy" />
                            </td>
                            <td class="auto-style5"><b>To :</b></td>  
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="dateTextBox2" Width="164px"  class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="DateTextBox2" Format="MM/dd/yyyy" />
                            </td>
                            <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                <asp:Button ID="Button1" runat="server" CssClass="pointer" Width="100px" Height="25px"  Text="LOAD" OnClick="buttonView_Click" />
                            </td>
                            <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                <asp:Button ID="Button2" runat="server" CssClass="pointer" Width="120px" Height="25px" Text="OPEN  PDF" OnClick="buttonPrint_Click" />
                            </td>                                                                                 
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
    <rsweb:reportviewer ID="StudentAccountStatement" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="53%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true" >
    </rsweb:reportviewer>
    </div>
</asp:Content>
