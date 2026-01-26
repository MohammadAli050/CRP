<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    Inherits="ExamScheduleSeatPlanManagement" Codebehind="ExamScheduleSeatPlanManagement.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Seat Plan</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>

        <div class="PageTitle">
            <label>Exam Schedule Seat Plan</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleSeatPlan-container">
                    <div class="div-margin">
                        <div class="information-Zone">
                            <div class="loadArea">
                                <label class="display-inline field-Title">Calender Type</label>
                                <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                                <label class="display-inline field-Title2">Academic Calender</label>
                                <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />
                            </div>
                            <div class="loadedArea">
                                <label class="display-inline field-Title">Exam Set(s)</label>
                                <asp:DropDownList runat="server" ID="ddlExamScheduleSet" class="margin-zero dropDownList2" AutoPostBack ="true" OnSelectedIndexChanged="ExamScheduleSet_Changed" />
                            </div>
                            <div class="loadedArea">
                                <asp:Button runat="server" ID="btnInvigilationScheduleDownload" Text="Load Invigilation Schedule" OnClick="btnInvigilationScheduleDownload_Click" class="button-margin btn-size" />
                            </div>
                            <div class="loadedArea">
                                <label class="display-inline field-Title">Day</label>
                                <asp:DropDownList runat="server" ID="ddlDay" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Day_Changed" />

                                <label class="display-inline field-Title2">Slot</label>
                                <asp:DropDownList runat="server" ID="ddlTimeSlot" class="margin-zero dropDownList"  AutoPostBack="true" OnSelectedIndexChanged="TimeSlot_Changed"/>
                            </div>
                            <div class="loadedArea">
                                <label class="display-inline field-Title"></label>
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />

                                <asp:Panel runat="server" ID="pnMaleFemaleCount">
                                    <label class="display-inline field-Title4">Total Male</label>
                                    <asp:TextBox runat="server" ID="txtMaleCount" class="margin-zero input-Size" Enabled="false" Text="0" />
                                    <label class="display-inline field-Title5">Total Female</label>
                                    <asp:TextBox runat="server" ID="txtFemaleCount" class="margin-zero input-Size" Enabled="false" Text="0" />
                                </asp:Panel>
                            </div>
                            <asp:Panel runat="server" ID="pnGenerateButton">
                                <div class="loadedArea">
                                    <%--<label class="display-inline field-Title4">Total Student(Odd)</label>
                                    <asp:TextBox runat="server" ID="txtOddRow" class="margin-zero input-Size" Enabled="false" Text="0" />
                                    <label class="display-inline field-Title5">Total Student(Even)</label>
                                    <asp:TextBox runat="server" ID="txtEvenRow" class="margin-zero input-Size" Enabled="false" Text="0" />--%>
                                
                                    <label class="display-inline field-Title"></label>
                                    <asp:Button runat="server" ID="btnGenerateSeatPlan" Text="Generate Seat Plan" OnClick="btnGenerateSeatPlan_Click" class="button-margin btn-size" />
                                </div>
                            </asp:Panel>
                        </div>
                        <asp:Panel runat="server" ID="pnSectionAssign">
                            <div class="information-Zone2">
                                <div class="loadArea">
                                    <label class="display-inline field-Title">Campus</label>
                                    <asp:DropDownList runat="server" ID="ddlCampus" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="Campus_Changed" />

                                    <label class="display-inline field-Title2">Building</label>
                                    <asp:DropDownList runat="server" ID="ddlBuilding" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="Building_Changed" />
                                </div>
                                <div class="loadedArea">
                                    <label class="display-inline field-Title">Room</label>
                                    <asp:DropDownList runat="server" ID="ddlRoom" class="margin-zero dropDownList" />

                                    <asp:Button runat="server" ID="btnMaleRoom" Text="Room for Male" OnClick="btnMaleRoom_Click" class="button-margin btn-size2" />
                                    <asp:Button runat="server" ID="btnFemaleRoom" Text="Room for Female" OnClick="btnFemaleRoom_Click" class="button-margin btn-size2" />
                                </div>
                                <div class="loadedArea">
                                    <asp:ListBox runat="server" ID="lbMaleRoomList" CssClass="margin-zero maleListBoxStyle" />
                                    <asp:Button runat="server" ID="btnMaleRoomDelete" Text="Delete" OnClick="btnMaleRoomDelete_Click" class="button-margin btn-size3" />
                                    <asp:ListBox runat="server" ID="lbFemaleRoomList" CssClass="margin-zero maleListBoxStyle" />
                                    <asp:Button runat="server" ID="btnFemaleRoomDelete" Text="Delete" OnClick="btnFemaleRoomDelete_Click" class="button-margin btn-size3" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="cleaner"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnInvigilationScheduleDownload" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="ExamScheduleSeatPlan-container">
                    <asp:Panel ID="PnlExamScheduleSeatPlan" runat="server" Wrap="False"><%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvExamScheduleSeatPlan" runat="server" AutoGenerateColumns="False" class="mainTable" Width="100%">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Program" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblProgram" Font-Bold="False" Text='<%#Eval("ProgramName") %>' /></ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCourse" Font-Bold="False" Text='<%#Eval("CourseInfo") %>' /></ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblDay" Font-Bold="False" Text='<%#Eval("Day") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Slot" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblTimeSlot" Font-Bold="False" Text='<%#Eval("TimeSlot") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblSection" Font-Bold="False" Text='<%#Eval("SectionList") %>' /></ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Male-Female" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblStudentNo" Font-Bold="False" Text='<%#Eval("StudentNo") %>' /></ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlOddEvenRows" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlOddEvenRows_Changed">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Odd</asp:ListItem>
                                            <asp:ListItem Value="2">Even</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hfId" Value='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>--%>

                                <%--<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Edit" ID="lbEdit" CommandArgument='<%#Eval("Id") %>' OnClick="lbEdit_Click">
                                            <span class="action-container"><img src="../Images/2.edit.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("Id") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <span class="action-container"><img src="../Images/3.delete.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>--%>
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

