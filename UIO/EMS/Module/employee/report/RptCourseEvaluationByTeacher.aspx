<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptCourseEvaluationByTeacher" CodeBehind="RptCourseEvaluationByTeacher.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Course Evaluation Result
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style type="text/css">
        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
            padding: 5px;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .auto-style2 {
            width: 173px;
        }

        .auto-style3 {
            width: 153px;
        }

        .auto-style4 {
            width: 149px;
        }
        .auto-style5 {
            width: 103px;
        }
    </style>

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
    <div style="padding: 5px; width: 100%;">
        <div class="PageTitle">
            <label>Course Evaluation Generation : : Semester,Program,Teacher and Course With Section Wise</label>
        </div>

        <div class="Message-Area" style="height: 60px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                <td class="auto-style5">
                                    <label class="display-inline field-Title">Calender Type</label>
                                    <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="CalenderType_Changed" />
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="Label2" runat="server" Text="Semester : "></asp:Label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" Width="147px"></asp:DropDownList>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label3" runat="server" Text="Program : "></asp:Label>
                                    <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" Width="151px"></asp:DropDownList>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label5" runat="server" Text="Course With Section"></asp:Label>
                                    <asp:DropDownList ID="ddlCourseWithSection" runat="server" Style="width: 330px;" />
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" BackColor="Orange" Text="Load" Width="150" Height="40" OnClick="GetEvaluationResult_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
        </div>


        <div style="clear: both;"></div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="Message-Area" style="height: 20px;">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both;"></div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel2"
            runat="server">
            <animations>
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
            </animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <center>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="clear: both"></div>
            <div style="height: 30px; width: 900px; padding: 15px;"></div>
        </center>
    </div>
</asp:Content>
