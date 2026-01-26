<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="RptEmployeeInfo.aspx.cs" Inherits="EMS.miu.admin.RptEmployeeInfo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Teacher Info : Dept Wise</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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
    <div>
        <div class="PageTitle">
            <label>Teacher Info : Dept Wise</label>
        </div>


        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="Message-Area">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="auto-style8"><b>Department</b></td>
                            <td colspan="2" class="auto-style2">
                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="101px"></asp:DropDownList>
                            </td>
                            <td class="auto-style22">Gender : </td>
                            <td class="auto-style21">
                                <asp:DropDownList runat="server" ID="ddlGender" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" Width="96px" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                            <asp:CheckBox ID="chkDateOfBirth" runat="server" Checked="true" Text="Date Of Birth" /></td>
                            <td class="auto-style20">
                                <asp:CheckBox ID="chkGender" runat="server" Checked="true" Text="Gender" /></td>
                            <td class="auto-style21">
                                <asp:CheckBox ID="chkFullName" runat="server" Checked="true" Text="FullName" /></td>
                            <td class="auto-style7">
                                <asp:CheckBox ID="chkPhone" runat="server" Checked="true" Text="Phone" /></td>
                            <td class="auto-style8">
                                <asp:CheckBox ID="chkEmail" runat="server" Checked="true" Text="Email" /></td>
                            <td class="auto-style8">
                                <asp:CheckBox ID="chkPhoto" runat="server" Checked="true" Text="Photos" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
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


        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">
                </rsweb:ReportViewer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

