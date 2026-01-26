<%@ Page Title="Bill Payment Due History" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" 
    AutoEventWireup="true" CodeBehind="RptStudentBillPaymentDue.aspx.cs" Inherits="EMS.miu.bill.report.RptStudentBillPaymentDue" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bill Payment Due History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Bill Payment Due History</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Batch :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" />
                        </td>
                                    
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" />
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="btnLoad" runat="server"  Text="Load" OnClick="btnLoad_Click" /> 
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
    </asp:UpdatePanel>

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
    <rsweb:reportviewer ID="StudentBillPaymentDue" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="53%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true" >
    </rsweb:reportviewer>
    </div>
</asp:Content>
