<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentGradeCertificateSemesterWise.aspx.cs" Inherits="EMS.Module.student.report.RptStudentGradeCertificateSemesterWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Grade Certificate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Student Grade Certificate :: Year Wise</label>
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="TeacherManagement-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <table>
                                <tr>
                                    <td class="auto-style8"><b>Institution</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlInstitution" class="margin-zero dropDownList" Width="231px" OnSelectedIndexChanged="ddlInstitution_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style9"><b>Program</b></td>
                                    <td class="auto-style4">
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                    </td>
                                    <td class="auto-style1">&nbsp</td>
                                    <td class="auto-style10"><b>Batch</b></td>
                                    <td class="auto-style14">
                                        <uc2:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                                    </td>
                                    <td class="auto-style1">&nbsp</td>
                                    <td class="auto-style8"><b>Year</b></td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="ddlSemester" runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" Width="80px" AutoPostBack="true" />
                                    </td>
                                    <td class="auto-style1">&nbsp</td>

                                </tr>

                            </table>
                        </div>
                        <div class="loadedArea">
                            <table style="height: 80px">
                                <tr>
                                    <td class="auto-style8"><b>Student List</b></td>
                                    <td class="auto-style8">
                                        <div style="height: 80px; width: 150px; overflow-y: auto">
                                            <asp:CheckBoxList ID="ddlStudentList" runat="server" OnSelectedIndexChanged="ddlStudentList_SelectedIndexChanged" Height="50px"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <%-- <td>
                                <asp:Label ID="Label2" runat="server" Text="Publication Date" Width="69px"></asp:Label>
                            </td>
                            <td>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="txtResultPublishDate" Width="113px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtResultPublishDate" Format="dd/MM/yyyy" />
                                    &nbsp;&nbsp;&nbsp;
                            
                                </div>
                            </td>--%>
                                    <td class="auto-style5"></td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Issued on Date" Width="101px"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="controls">
                                            <asp:TextBox runat="server" ID="txtIssuedDate" Width="113px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIssuedDate" Format="dd/MM/yyyy" />
                                            &nbsp;&nbsp;&nbsp;
                            
                                        </div>
                                    </td>
                                    <td class="auto-style5"></td>
                                    <td colspan="2" style="vertical-align: middle" class="auto-style8">
                                        <asp:Button ID="btnLoad" runat="server" CssClass="pointer" Width="100px" Height="25px" Text="LOAD" OnClick="btnLoad_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <table>
                                <tr>
                                    <td>
                                        <label id="lblDegreeName" runat="server" visible="false" class="display-inline field-Title">Degree Name</label>
                                        <asp:DropDownList runat="server" Visible="false" ID="ddlMISSDegreeName" class="margin-zero dropDownList">
                                            <asp:ListItem Value="1">MSc/M Engg in Information System Security (MISS)</asp:ListItem>
                                            <asp:ListItem Value="2">Master of Engineering in Information System Security (M Engg in ISS)</asp:ListItem>
                                            <asp:ListItem Value="3">Master of Science in Engineering in Information System Security (MSc Engg in ISS)</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList runat="server" Visible="false" ID="ddlMICTDegreeName" class="margin-zero dropDownList">
                                            <asp:ListItem Value="1">MSc/Masters in Information and Communication Technology (MICT)</asp:ListItem>
                                            <asp:ListItem Value="2">Masters in Information and Communication Technology (MICT)</asp:ListItem>
                                            <asp:ListItem Value="3">Master of Science in Information and Communication Technology (MSc in ICT)</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnLoad" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.Gif" Height="150px" Width="150px" />
    </div>


    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="StudentGradeReport" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="57%" Height="100%" SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
