<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentCourseHistoryEditBulk" CodeBehind="StudentCourseHistoryEditBulk.aspx.cs" %>

<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course History Bulk Edit</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">


        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

        function InIEvent() {
            //$('#hdFlagSingleClick').val('0');

            var columnValue = $('#ctl00_MainContainer_gvRegisteredCourse > tbody > tr').not(':first').find('td:first').text();

            columnValue = columnValue.replace(/\s+/g, '');

            var rowColor = 'other';
            var previousStatus = '000'.toString();
            var columnRow = new Array(1000);
            index = 0;

            for (var i = 0; i < columnValue.length; i = i + 3) {
                columnRow[index] = columnValue.slice(i, i + 3);

                if (columnRow[index] != previousStatus) {
                    previousStatus = columnRow[index];
                    if (rowColor == 'white') {
                        rowColor = 'other';
                        $('#ctl00_MainContainer_gvRegisteredCourse > tbody > tr:nth-child(' + (index + 2) + ') > td').css("background-color", "#d9eaf7");
                    }
                    else
                        rowColor = 'white';
                }
                else {
                    if (rowColor == 'other')
                        $('#ctl00_MainContainer_gvRegisteredCourse > tbody > tr:nth-child(' + (index + 2) + ') > td').css("background-color", "#d9eaf7");
                }
                index = index + 1;
            }
        }


    </script>
    <style type="text/css">
        .auto-style1 {
            width: 12px;
        }

        .auto-style2 {
            width: 14px;
        }

        .auto-style3 {
            width: 126px;
        }

        .auto-style4 {
            width: 13px;
        }

        .auto-style5 {
            width: 18px;
        }

        .auto-style6 {
            width: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Course History Bulk Edit</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Program</b></td>
                            <td class="auto-style4">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="auto-style1">&nbsp</td>
                            <td class="auto-style8"><b>Semester</b></td>
                            <td class="auto-style2">
                                <uc1:SessionUserControl runat="server" ID="ucSession" />
                            </td>
                            <td class="auto-style1">&nbsp</td>
                            <td class="auto-style10"><b>Batch</b></td>
                            <td class="auto-style14">
                                <uc2:BatchUserControl runat="server" ID="ucBatch" />
                            </td>
                            <td class="auto-style5"></td>
                            <td class="auto-style10"><b>Range(Student)</b></td>
                            <td>
                                <asp:DropDownList ID="ddlRange" runat="server" Width="110px">
                                    <asp:ListItem Value="1">1  -  250</asp:ListItem>
                                    <asp:ListItem Value="2">251  -  500</asp:ListItem>
                                    <asp:ListItem Value="3">501  -  750</asp:ListItem>
                                    <asp:ListItem Value="4">751  -  1000</asp:ListItem>
                                    <asp:ListItem Value="5">1001  -  1250</asp:ListItem>
                                    <asp:ListItem Value="6">1251  -  1500</asp:ListItem>
                                    <asp:ListItem Value="3">1501  -  1750</asp:ListItem>
                                    <asp:ListItem Value="4">1751  -  2000</asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style6"></td>
                            <td>
                                <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" Width="102px" />
                            </td>
                            <td class="auto-style3"></td>
                            <td hidden="hidden">
                                <asp:DropDownList ID="ddlSemesterNoAll" runat="server" Width="58px">
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style10"><b>Year</b></td>
                            <td>
                                <asp:DropDownList ID="ddlYearAll" runat="server" Width="58px">
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <div class="LoadStudentCourseHistory">
            <div class="div-margin">
                <asp:Label runat="server" ID="lblResult" class="tableBanner display-inline">Course History</asp:Label>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="LoadStudentCourseHistory">
                    <asp:Panel ID="PnlRegisteredCourse" runat="server" Width="100%" Wrap="False">
                        <asp:GridView ID="gvCourseHistry" OnSorting="gvRegisteredCourse_Sorting" AllowSorting="true" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <RowStyle Height="25px" />

                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student ID" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="True" Text='<%#Eval("Roll") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student's Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Font-Bold="True" Text='<%#Eval("FullName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFormalCode" Font-Bold="True" Text='<%#Eval("FormalCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseTitle" Font-Bold="True" Text='<%#Eval("CourseTitle") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblObtainedGrade" Font-Bold="True" Text='<%#Eval("ObtainedGrade") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseStatus" Font-Bold="True" Text='<%#Eval("CourseStatus.Description") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <label>SemesterNo</label>
                                        <hr />
                                        <asp:CheckBox ID="chkSelectAllchkSemesterNo" runat="server" Text="All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllchkSemesterNo_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSemesterNo"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <label>Year No</label>
                                        <hr />
                                        <asp:CheckBox ID="chkSelectAllchkYearNo" runat="server" Text="All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllchkYearNo_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkYearNo"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlYearNo" runat="server" SelectedValue='<%#(Eval("YearNo")== null? "0" : Eval("YearNo"))%>' Style="width: 40px;">
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false" HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSemesterNo" runat="server" SelectedValue='<%#(Eval("SemesterNo")==null ? "0" : Eval("SemesterNo"))%>' Style="width: 40px;">
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="0"></asp:ListItem>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <label>IsConsiderGPA</label>
                                        <hr />
                                        <asp:CheckBox ID="chkSelectAllIsConsiderGPA" runat="server" Text="All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllIsConsiderGPA_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="ChkIsConsiderGPA" Checked='<%# Eval("IsConsiderGPA") == null ? false : Eval("IsConsiderGPA") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Action">
                                    <HeaderTemplate>
                                        <asp:Button runat="server" ToolTip="Save All" Text="Save All" ID="btnSaveAll"
                                            OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                            OnClick="btnSaveAll_Click"></asp:Button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ToolTip="Save" Text="Save" ID="btnSave" OnClick="btnSave_Click"></asp:Button>
                                    </ItemTemplate>

                                    <HeaderStyle Width="100px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />

                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <b>No Data Found !</b>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
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

    </div>
</asp:Content>

