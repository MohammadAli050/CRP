<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="DegreeValidationByStudentID" Codebehind="DegreeValidationByStudentID.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Result Verification</asp:Content>
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div style="height: auto; width: 100%">
        <div class="PageTitle">
            <label>Student Result Verification</label>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label2" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000" Width="50%"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="loadArea">
                        <asp:Label ID="Label1" runat="server" Width="100px" Text="Student Id : "></asp:Label>
                        <asp:TextBox ID="txtStudentRoll" runat="server"></asp:TextBox>
                        <asp:Button ID="btnLoadStudentResult" runat="server" Text="Load" OnClick="btnLoadStudentResult_Click" />
                    </div>
                    <div class="loadArea">
                        <asp:Label ID="Label3" runat="server" Width="100px" Text="Student Name : " />
                        <asp:Label runat="server" ID="lblStudentName" Width="200px" Text="..........................................." style="font-weight: 700" />

                        <asp:Label ID="Label4" runat="server" Width="100px" Text="Batch : " />
                        <asp:Label runat="server" ID="lblStudentBatch" Width="200px" Text="..............................................." style="font-weight: 700" />

                        <asp:Label ID="Label5" runat="server" Width="100px" Text="Program : " />
                        <asp:Label runat="server" ID="lblStudentProgram" Width="200px" Text="..............................................." style="font-weight: 700" />
                    </div>
                    <div class="loadArea">
                        <asp:Label ID="Label6" runat="server" Width="100px" Text="CGPA : " />
                        <asp:Label runat="server" ID="lblCGPA" Width="200px" Text="..........................................." style="font-weight: 700" />

                        <asp:Label ID="Label7" runat="server" Width="100px" Text="Major1 : " />
                        <asp:Label runat="server" ID="lblMajor1" Width="200px" Text="..............................................." style="font-weight: 700" />

                        <asp:Label ID="Label8" runat="server" Width="100px" Text="Major2 : " />
                        <asp:Label runat="server" ID="lblMajor2" Width="200px" Text="..............................................." style="font-weight: 700" />
                    </div>
                    <div class="loadArea">
                        <asp:Label ID="Label9" runat="server" Width="100px" Text="Degree Req. : " />
                        <asp:Label runat="server" ID="lblDegreeReq" Width="200px" Text="..........................................." style="font-weight: 700" />

                        <asp:Label ID="Label10" runat="server" Width="100px" Text="Com. Credit(s) : " />
                        <asp:Label runat="server" ID="lblCompletedCr" Width="200px" Text="..............................................." style="font-weight: 700" />

                        <asp:Label ID="Label11" runat="server" Width="100px" Text="Att. Credit(s) : " Visible="False" />
                        <asp:Label runat="server" ID="lblAttemptedCr" Width="200px" Text="..............................................." style="font-weight: 700" Visible="False" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblGPA" runat="server" Text="Trimester-Wise GPA"></asp:Label>
                                <asp:GridView ID="gvTrimesterWiseGPA" AutoGenerateColumns="False" EmptyDataText="No Data Found" runat="server"
                                    CssClass="gridCss" CellPadding="4" Width="395px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trimester" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("AcademicCalender.FullCode") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attempted" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredit" Text='<%#Eval("Credit") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Completed" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredit" Font-Bold="True" Text='<%#Eval("TranscriptCredit") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GPA" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGPA" Text='<%#Eval("TranscriptGPA") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGPA" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCGPA" Text='<%#Eval("TranscriptCGPA","{0:f2}")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>

                                    <HeaderStyle BackColor="Green" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFFFCC" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="lightGreen" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                </asp:GridView>
                            </td>
                            <td valign="top" >
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblWaiverCourse" runat="server" Text="Transferred/waived course"></asp:Label>
                                            <asp:GridView ID="gvWaiVeredCourse" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%" CellPadding="3">
                                                <HeaderStyle BackColor="Green" ForeColor="White" Height="30" />
                                                <AlternatingRowStyle BackColor="Green" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("FormalCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCourseName" Text='<%#Eval("CourseTitle") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <b>No Data Found !</b>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="Green" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#FFFFCC" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                <RowStyle BackColor="lightGreen" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                <SortedDescendingHeaderStyle BackColor="#820000" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br />
                                            <asp:Label ID="Label12" runat="server" Text="Type wise no of courses"></asp:Label>
                                            <asp:GridView ID="gvTypeWiseCourse" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="196px">

                                                <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                                                <AlternatingRowStyle BackColor="#FFFFCC" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTypeWiseCourseTypeName" Font-Bold="True" Text='<%#Eval("TypeName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTypeWiseCourseTypeCount" Text='<%#Eval("Count") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTypeWiseCourseTypeCount" Text='<%#Eval("Credits") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <b>No Data Found !</b>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="Green" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#FFFFCC" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                <RowStyle BackColor="lightGreen" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                <SortedDescendingHeaderStyle BackColor="#820000" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label13" runat="server" Text="Total number of courses completed : " />
                                    <asp:Label runat="server" ID="lblCompletdCourseCounter" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label14" runat="server" Text="Number of courses completed before this trimester : " />
                                    <asp:Label runat="server" ID="lblCompletedBefore" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label15" runat="server" Text="No of courses completed in this trimester : " Visible="False" />
                                    <asp:Label runat="server" ID="lblCompletedTrimester" Width="100px" Text="........" style="font-weight: 700" Visible="False" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label16" runat="server" Text="CGPA : " />
                                    <asp:Label runat="server" ID="lblNewCGPA" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label17" runat="server" Text="Degree Requirement : " />
                                    <asp:Label runat="server" ID="lblDegreeRequirement" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label18" runat="server" Text="Credit Attempted : " Visible="False" />
                                    <asp:Label runat="server" ID="lblAttempted" Width="100px" Text="........" style="font-weight: 700" Visible="False" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label19" runat="server" Text="Total Credit Hour Completed : " />
                                    <asp:Label runat="server" ID="lblCreditHourCompleted" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                                <div class="loadedArea" style="padding:4px;">
                                    <asp:Label ID="Label20" runat="server" Text="Credit Waiver : " />
                                    <asp:Label runat="server" ID="lblCreditWaiver" Width="100px" Text="........" style="font-weight: 700" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Result of completed/registered course"></asp:Label>
                                <asp:GridView ID="gvCourseHistory" AutoGenerateColumns="False" EmptyDataText="No Data Found" runat="server"
                                    OnSorting="gvCourseHistory_Sorting" AllowSorting="true" CssClass="gridCss" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CourseID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCourseID" Text='<%#Eval("CourseID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="VersionID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVersionID" Text='<%#Eval("VersionID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Trimester">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAcaCalCode" Text='<%#Eval("AcaCalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="center" Width="120px" />
                                            <ItemStyle  VerticalAlign="Middle" HorizontalAlign="left"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Course Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="center" Width="120px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Course Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="center" Width="350" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTopNodeName" Text='<%#Eval("CourseType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="center" Width="120" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credits">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblObtainedGrade" Text='<%#Eval("ObtainedGrade") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblPriority" Text='<%#Eval("Priority") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Course Group">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCourseGroup" Text='<%#Eval("CourseGroup") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                        <asp:TemplateField HeaderText="Calendar Name">                                           
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCalendarDetailName" Text='<%#Eval("CalendarDetailName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="NodeLink Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNodeLinkName" Text='<%#Eval("NodeLinkName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>

                                    <HeaderStyle BackColor="Green" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFFFCC" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="lightGreen" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />

                                </asp:GridView>
                            </td>
                        </tr>

                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="div1" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel2"
            runat="server">

            <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnLoad"  Enabled="false" /> 
                    </Parallel>
                </OnUpdating>

                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="btnLoad"  Enabled="true" />
                    </Parallel>
                </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>


