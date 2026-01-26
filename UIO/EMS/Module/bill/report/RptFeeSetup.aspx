<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptFeeSetup.aspx.cs" Inherits="EMS.miu.bill.report.RptFeeSetup" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControlAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    FeeSetup
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
        .auto-style2 {
            width: 202px;
        }
        .auto-style3 {
            width: 60px;
            font-weight: bold;
        }
        .auto-style4 {
            width: 82px;
            font-weight: bold;
        }
        .auto-style5 {
            width: 301px;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>FeeSetup :: Program and Batch Wise</label>
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
                            <td class="auto-style4">Program : </td>
                            <td class="auto-style2"><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" /></td>                                                                                                                     
                                
                            <td class="auto-style3">Batch : </td>
                            <td class="auto-style5"><uc2:BatchUserControlAll runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged"/></td>
                            
                            <td><asp:Button ID="btnLoad" CssClass="pointer" runat="server" OnClick="btnLoad_Click" Text="Load" Width="100px" /></td>
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

    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="width: 1100px; margin-top: 20px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <div>&nbsp;</div>
    <div> 
        <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="38.2%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true">
            <%--<LocalReport ReportPath="miu/bill/report/RptFeeSetup.rdlc">
            </LocalReport>--%>
        </rsweb:reportviewer>
    </div>    
</asp:Content>
