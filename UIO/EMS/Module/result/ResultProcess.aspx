<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ResultProcess.aspx.cs" Inherits="EMS.miu.result.ResultProcess" %>

<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Result Process
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            var maincontainer = $('#MasterBody').height();
            var bodycontainer = $(document).height();
        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 20px;
        }

        .auto-style2 {
            width: 101px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle floatLeft">
            <label>Result Process</label>
        </div>
        <div style="height: 30px;">
            <div id="divProgress" class="floatRight" style="padding-top: 7px; display: none;">
                <div class="floatRight">
                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                    <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                    <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                </div>
            </div>
        </div>
        <div class="cleaner"></div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="Message-Area">
            <asp:UpdatePanel runat="server" ID="UpdatePanel03">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Program</b></td>
                            <td class="auto-style4">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <%--<td class="auto-style1">&nbsp</td>
                            <td class="auto-style8"><b>Semester</b></td>--%>
                            <td hidden="hidden" class="auto-style2">
                                <uc1:SessionUserControl runat="server" ID="ucSession" />
                            </td>
                            <td class="auto-style1">&nbsp</td>
                            <td class="auto-style10"><b>Batch</b></td>
                            <td class="auto-style14">
                                <uc2:BatchUserControl runat="server" ID="ucBatch" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnProcessGroup" class="margin-zero btn-size" Text="Process" OnClick="ProcessGroup_Click" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnViewGroup" class="margin-zero btn-size" Text="View" OnClick="ViewGroup_Click" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style9"><b>Student ID</b></td>
                            <td class="auto-style4">
                                <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" Width="117" />
                            </td>
                            <%--<td class="auto-style1">&nbsp</td>
                            <td><b>Semester</b></td>--%>
                            <td hidden="hidden" class="auto-style2">
                                <asp:DropDownList runat="server" ID="ddlAcaCalStudentSemester" class="margin-zero dropDownList" />
                            </td>
                            <td class="auto-style1">&nbsp</td>
                            <td>
                                <asp:Button runat="server" ID="btnProcessStudent" class="margin-zero btn-size" Text="Process" OnClick="ProcessStudent_Click" /></td>
                            <td>
                                <asp:Button runat="server" ID="btnViewStudent" class="margin-zero btn-size" Text="View" OnClick="ViewStudent_Click" /></td>
                        </tr>

                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="ResultProcess-container">
                    <div class="div-margin">
                        <asp:Label runat="server" ID="lblResult" class="tableBanner display-inline">Trimester wise GPA, CGPA, Transcript GPA and Transcript CGPA</asp:Label>
                    </div>
                    <asp:Panel ID="pnlResult" runat="server" Width="85%" Wrap="False">
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Font-Bold="True" Text='<%#Eval("Name") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="False" Text='<%#Eval("Roll") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblYear" Font-Bold="True" Text='<%#Eval("StdAcademicCalenderID") %>' /></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Enroll Cr." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCredit" Font-Bold="False" Text='<%#Eval("Credit") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGPA" Font-Bold="True" Text='<%#Eval("GPA") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CGPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCGPA" Font-Bold="False" Text='<%#Eval("CGPA") %>' /></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Earned Cr." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCredit" Font-Bold="True" Text='<%#Eval("TranscriptCredit") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="T GPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGPA" Font-Bold="False" Text='<%#Eval("TranscriptGPA") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="T CGPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCGPA" Font-Bold="True" Text='<%#Eval("TranscriptCGPA") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsAllGradeSubmitted" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCGPA" Font-Bold="True" Text='<%#Eval("IsAllGradeSubmitted") %>' /></ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <b>No Data Found !</b>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel01" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnProcessGroup" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnProcessGroup" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel01" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnProcessStudent" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnProcessStudent" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender3" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnViewGroup" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnViewGroup" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender4" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnViewStudent" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnViewStudent" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
