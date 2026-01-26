<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="True" CodeBehind="ExamResultPrint.aspx.cs" Inherits="ExamResultPrint" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Exam Result Print
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
   <%-- <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />--%>

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
    <style>
        .center {
            margin: 0 auto;
            padding: 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="PageTitle">
        <label>Exam Result Print</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel"> 
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ExamResultPrint-container">
                <div class="div-margin">
                    <div class="Message-Area">

                        <label class="display-inline field-Title">Calender Type</label>
                        <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                        <label class="display-inline field-Title1">Session</label>
                        <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                        <label class="display-inline field-Title1">Course Name</label>
                        <asp:DropDownList runat="server" ID="ddlAcaCalSection" class="margin-zero dropDownList" />

                        <asp:Button ID="btnLoadResult" runat="server" Text="LOAD" Width="100" class="button-margin btn-size" OnClick="LoadResult_Click" />

                        <div id="divProgress" style="display: none; float: right; z-index: 1000;">
                            <div style="float: left">
                                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                            </div>
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
    <rsweb:ReportViewer ID="ExamResultViewPrint" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="62%" SizeToReportContent="true">      
    </rsweb:ReportViewer>

</asp:Content>