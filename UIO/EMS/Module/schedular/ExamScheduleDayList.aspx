<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
     Inherits="ExamScheduleDayList" Codebehind="ExamScheduleDayList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Day::List</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });
        function InProgress() {
            var panelProg = $get('ctl00_MainContainer_PnProcess');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('ctl00_MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup :: Exam Schedule Day :: List</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="ExamScheduleDayList-container">
            <asp:Button runat="server" ID="btnCreateLink" Text="Create" OnClick="btnCreateLink_Click" class="button-margin btn-size1" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleDayList-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title2">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed"/>

                            <label class="display-inline field-Title3">Exam Set(s)</label>
                            <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" />

                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size"  />
                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleDayList-container">
                    <asp:Panel ID="PnlExamScheduleDayList" runat="server" Wrap="False"><%--Height="397px" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamScheduleDayList" runat="server" AutoGenerateColumns="False" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Exam Set" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblExamSet" Font-Bold="False" Text='<%#Eval("ExamScheduleSetName") %>' /></ItemTemplate>
                                    <HeaderStyle Width="400px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblTotalDay" Font-Bold="False" Text='<%#Eval("DayNo") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblDOB" Font-Bold="False" Text='<%#Eval("DayDate","{0:dd-MMM-yy}") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Edit" ID="lbEdit" CommandArgument='<%#Eval("Id") %>' OnClick="lbEdit_Click">
                                            <span class="action-container"><img src="../../Images/2.edit.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("Id") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <span class="action-container"><img src="../../Images/3.delete.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

