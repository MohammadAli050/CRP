<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="CertificateGeneration.aspx.cs" Inherits="EMS.miu.admin.report.CertificateGeneration" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Certificate Generation
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
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
            width: 100px;
        }

        .auto-style3 {
            width: 143px;
        }

        .auto-style5 {
            width: 109px;
        }

        .auto-style6 {
            width: 82px;
        }

        .auto-style8 {
            width: 99px;
            height: 27px;
        }

        .auto-style9 {
            height: 27px;
        }

        .auto-style10 {
            height: 27px;
            width: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Student Certificates</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <panel id="pnlMessage" runat="server" visible="true" cssclass="msgPanel"> 
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMessage" Text="" />
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ExamResultPrint-container">
                <div class="div-margin">
                    <div class="Message-Area">
                        <table>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Label ID="Label2" runat="server" Text="Type "></asp:Label>
                                    <asp:DropDownList ID="ddlType" runat="server" Width="100px" />
                                </td>
                                <td></td>
                                <td class="auto-style5">
                                    <asp:Label ID="Label1" runat="server" Text="Student ID "></asp:Label>
                                    <asp:TextBox ID="txtStudentRoll" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td class="auto-style5">
                                    <asp:Button ID="btnLoad" runat="server" Text="Load" Width="101px" Height="37px" OnClick="btnLoad_Click"></asp:Button>
                                </td>
                                <td></td>
                                <td class="auto-style3">
                                    <asp:Label ID="Label3" runat="server" Text="Student Name "></asp:Label>
                                    <asp:TextBox ID="txtStudentName" ReadOnly="true" runat="server" Width="201px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td class="auto-style6">
                                    <asp:Label ID="Label5" runat="server" Text="Serial No "></asp:Label>
                                    <asp:TextBox ID="txtSerial" ReadOnly="true" runat="server" Width="72px"></asp:TextBox>
                                </td>
                                <%--<td></td>
                                <td class="auto-style6">
                                    <asp:Button ID="btnCheckValidity" runat="server" Text="Check Validity" Width="101px" Height="37px" OnClick="btnCheckValidity_Click" Visible="false" Enabled="false"></asp:Button>
                                </td>--%>
                                <td></td>
                                <td class="auto-style6">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" Width="101px" Height="37px" OnClick="btnGenerate_Click" Enabled="false"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Button ID="btnSaveCancel" runat="server" Text="Save/Cancel" Enabled="false" Width="145px" Height="24px" OnClick="btnSaveCancel_Click"></asp:Button>
                                </td>
                                <td></td>
                                <td class="auto-style6">
                                    <asp:Button ID="btnPreview" runat="server" Enabled="false" Text="Preview" Width="103px" Height="24px" OnClick="btnPreview_Click"></asp:Button>
                                </td>
                                <td></td>
                                <td class="auto-style6">
                                    <asp:Button ID="btnPrint" runat="server" Enabled="false" Text="Print" Width="103px" Height="24px" OnClick="btnPrint_Click"></asp:Button>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <panel id="Panel1" runat="server" visible="true" cssclass="msgPanel"> 
                <div class="Message-Area">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


