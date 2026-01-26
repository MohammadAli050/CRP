<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentWithExcessAmount.aspx.cs" Inherits="EMS.miu.bill.report.RptStudentWithExcessAmount" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student With Excess Amount
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
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Due:: Student With Excess Amount</label>
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
                            <td class="auto-style10">Batch</td>
                            <td class="auto-style14"><uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" /></td>

                            <td><b>Gender</b></td>
                            <td><asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem Text="All" Value="0" />
                                    <asp:ListItem Text="Male" Value="1" />
                                    <asp:ListItem Text ="Female" Value="2" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" /></td>
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

    <div>&nbsp;</div>
    <div>
    <rsweb:reportviewer ID="StudentWithExcessAmount" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="90%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true" >
    </rsweb:reportviewer>
    </div>
</asp:Content>
