<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" Inherits="Report_RptProbationStudentList" Codebehind="RptProbationStudentList.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Probation</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    
    <link href="/ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript" lang="en">
        function InProgress() {
            var panelProg = $get('MainContainer_PnView');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('MainContainer_PnView');
            panelProg.style.display = 'none';
        }

        function ProcessProgress() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = '';
        }

        function ProcessComplete() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <%--<div>
        <div class="PageTitle">
            <label>Probation Student List</label>
        </div>
    </div>--%>

    <div class="Message-Area">
        <label class="msgTitle">Message: </label>
        <asp:Label runat="server" ID="lblMsg" Text="" />
    </div>

    <div class="studentProbationList-container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="div-margin">
                    <div class="loadArea">
                        <label class="display-inline field-Title">Type</label>
                        <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" OnSelectedIndexChanged="CalenderType_Changed" />
                    </div>
                    <div class="loadArea">
                        <label class="display-inline field-Title">Registration Period (From)</label>
                        <asp:DropDownList runat="server" ID="ddlAcaCalSession" class="margin-zero dropDownList" />

                        <label class="display-inline field-Title2">(To)</label>
                        <asp:DropDownList runat="server" ID="ddlAcaCalSession2" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" DataValueField="ProgramID" DataTextField="NameWithCode" />                    

                        <label class="display-inline field-Title2">CGPA (From)</label>
                        <asp:TextBox runat="server" ID="From" class="margin-zero input-Size" Text="0.0" style="text-align: center;" />

                        <label class="display-inline field-Title3">(To)</label>
                        <asp:TextBox runat="server" ID="To" class="margin-zero input-Size" Text="1.99" style="text-align: center;" />
                    </div>
   
                    <div  class="loadedArea">
                        <label class="display-inline field-Title"></label>
                        <asp:Button runat="server" ID="btnProcess" class="margin-zero btn-size" Text="Process" OnClick="btnProcess_Click" />
                        <label class="display-inline field-Title"></label>
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title4">
                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" />
                            </asp:Panel>
                        </label>
                        <label class="display-inline field-Title4">
                            <asp:Panel runat="server" ID="PnView" style="display: none;">
                                <img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" /><img src="../Images/pulse.gif" />
                            </asp:Panel>
                        </label>
                    </div>
                     <div class="loadedArea">
                        <label class="display-inline field-Title">Show Result (From)</label>
                        <asp:DropDownList runat="server" ID="ddlSemesterFrom" class="margin-zero dropDownList" />

                        <label class="display-inline field-Title2">(To)</label>
                        <asp:DropDownList runat="server" ID="ddlSemesterTo" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgramOrder" class="margin-zero dropDownList" DataValueField="ProgramID" DataTextField="NameWithCode" />

                        <label class="display-inline field-Title2">Order By</label>
                        <asp:DropDownList runat="server" ID="ddlOrderBy" class="margin-zero dropDownList" DataValueField="ProgramID" DataTextField="NameWithCode">
                        </asp:DropDownList>
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title"></label>
                        <asp:Button runat="server" ID="btnView" class="margin-zero btn-size" Text="View" OnClick="btnView_Click" />
                    </div>
                    <%--<div class="loadedArea"></div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Probation No </label>
                        <asp:TextBox runat="server" ID="txtProbationNo" class="margin-zero input-Size" style="text-align: center;" />

                        <asp:Button runat="server" ID="btnBlockStudent" class="margin-zero btn-size" Text="Block Student" OnClick="btnBlockStudent_Click" />
                    </div>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%--<ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" />
                <EnableAction AnimationTarget="btnBlockStudent" Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction AnimationTarget="btnBlockStudent" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>--%>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel3" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="ProcessProgress();" />
                <EnableAction AnimationTarget="btnProcess" Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="ProcessComplete();" />
                <EnableAction AnimationTarget="btnProcess" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" />
                <EnableAction AnimationTarget="btnView" Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction AnimationTarget="btnView" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <div class="studentProbationList-container">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" Width="100%" SizeToReportContent="true">
                    <LocalReport ReportPath="miu/student/report/ProbationReport.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
