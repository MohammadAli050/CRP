<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CourseWiseStudentListRW" CodeBehind="CourseWiseStudentListRW.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course Wise Students</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        
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

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>

        <div class="PageTitle">
            <label>Course Wise Students</label>
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
                <div class="CourseWiseStudentListRW-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title1">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                            <label class="display-inline field-Title1">Program</label>
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Program_Changed" />

                            <label class="display-inline field-Title1">Course</label>
                            <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Course_Changed" />

                            <label class="display-inline field-Title1">Section</label>
                            <asp:DropDownList runat="server" ID="ddlSection" class="margin-zero dropDownList" />

                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />

                            <asp:Panel runat="server" ID="PnProcess" style="display: none;">
                                <img src="../../Images/loading01.gif" class="img-Loading" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="CourseWiseStudentListRW-container">
                    <asp:Panel ID="PnlCourseWiseStudentListRW" runat="server" Wrap="False">
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" PagerSettings-Mode="NumericFirstLast" PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger" ShowHeader="true" CssClass="gridCss">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Batch">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBatch" Font-Bold="true" Text='<%#Eval("BatchNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pre-Registered">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsWorkSheet" Enabled="false" Checked='<%#Eval("IsWorkSheet") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regisered">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsCourseHistory" Enabled="false" Checked='<%#Eval("IsCourseHistory") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    
    </div>
</asp:Content>

