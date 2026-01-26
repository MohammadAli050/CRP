<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="DegreeCompletedViewer.aspx.cs" Inherits="EMS.miu.admin.report.DegreeCompletedViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Degree Completed Viewer</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .chkList-table
        {
            margin-left: 130px;
        }

        .btn-margin
        {
            margin-left: 10px;
        }
        .div-margin
        {
            height: 44px;
        }
        .loadArea
        {
            height: 19px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
        });

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
            <label>Degree Completed Viewer</label>
        </div>

        <asp:Panel runat="server" ID="panelContainer">
            <asp:UpdatePanel runat="server" ID="UpClassSchedule">
                <ContentTemplate>
                    <div style="clear:both"></div>
                    <table>
                        <tr>
                            <td>
                                <b>Trimester System</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="display-inline field-Title">Program :</label>
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlTriProgram" runat="server" ></asp:DropDownList>
                            </td>
                            <td>
                                 <label class="display-inline field-Title">
                                        From Session :</label>
                            </td>
                             <td>
                                 <asp:DropDownList ID="ddlTriFromSession" runat="server" />
                            </td>
                            <td>
                                <label class="display-inline field-Title">
                                        To Session :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTriToSession" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Semester System</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="display-inline field-Title">Program :</label>
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlSemProgram" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                 <label class="display-inline field-Title">
                                        From Session :</label>
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlSemFromSession" runat="server" />
                            </td>
                             <td>
                                <label class="display-inline field-Title">
                                        To Session :</label>
                            </td>
                            <td>
                                 <asp:DropDownList  ID="ddlSemToSession" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnLoad" runat="server" class="button-margin btn-size" OnClick="btnLoad_Click" Text="Load" />
                    <div class="ClassRoutine-container">
                        <div class="div-margin">
                            <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -106px">
                                <div style="float: left">
                                    <asp:Image ID="LoadingImage" runat="server" Height="35px" ImageUrl="~/Images/Img/Waiting.gif" Width="35px" />
                                </div>
                            </div>
                           
                        </div>
                    </div>
                 
                </ContentTemplate>
            </asp:UpdatePanel>

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

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="Message-Area">
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                     <rsweb:ReportViewer ID="DegreeCompletionRptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="97.8%" SizeToReportContent="true">      
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>

