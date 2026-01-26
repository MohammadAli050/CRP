<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentCourseHistoryEdit" CodeBehind="StudentCourseHistoryEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course History Edit</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {

        });

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
    <div>
        <div class="PageTitle">
            <label>Student Course History Edit</label>
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
                <div class="StudentCourseHistory-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Student ID :</label>
                            <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="Student ID" />
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin SearchKey" />
                             
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Student Name :</label>
                            <asp:Label runat="server" ID="lblStudentName" class="display-inline field-Title-Fix" Text="..........................................." />

                            <label class="display-inline field-Title1">Batch :</label>
                            <asp:Label runat="server" ID="lblStudentBatch" class="display-inline field-Title-Fix" Text="..............................................." />

                            <label class="display-inline field-Title1">Program :</label>
                            <asp:Label runat="server" ID="lblStudentProgram" class="display-inline field-Title-Fix" Text="..............................................." />

                            <label class="display-inline field-Title1">Major :</label>
                            <asp:Label runat="server" ID="lblStudentMajor" class="display-inline field-Title-Fix" Text="..............................................." />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.Gif"  Height="150px" Width="150px" />
    </div>

        <div class="LoadStudentCourseHistory">
            <div class="div-margin">
                <asp:Label runat="server" ID="lblResult" class="tableBanner display-inline">Trimester wise GPA and CGPA</asp:Label>
            </div>
            <%--<asp:Panel ID="pnlResult" runat="server" Width="800px" Wrap="False">
                <asp:gridview ID="gvResult" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                    <RowStyle Height="24px" />
                    <AlternatingRowStyle BackColor="#f5fbfb" />
                    <Columns>

                        <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("Semester") %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Credit Hour" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblCredit" Font-Bold="True" Text='<%#Eval("Credit") %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="GPA" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblGPA" Font-Bold="True" Text='<%#Eval("GPA") %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CGPA" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblCGPA" Font-Bold="True" Text='<%#Eval("CGPA") %>' /></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <b>No Data Found !</b>
                    </EmptyDataTemplate>
                    <RowStyle CssClass="rowCss" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:gridview>
            </asp:Panel>--%>
        </div>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="LoadStudentCourseHistory">
                    <div class="div-margin">
                        <asp:Label runat="server" ID="lblRegistered" class="tableBanner display-inline">Result of completed/registered courses</asp:Label>
                    </div>
                    <asp:Panel ID="PnlRegisteredCourse" runat="server" Width="100%" Wrap="False">
                        <asp:GridView ID="gvRegisteredCourse" OnSorting="gvRegisteredCourse_Sorting" AllowSorting="true" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <RowStyle Height="25px" />

                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Term" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:Label runat="server" ID="lblTerm" Font-Bold="True" Text='<%#Eval("Attribute3") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("Semester") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkCourseCode" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="CourseCode">Course ID</asp:LinkButton>
                                    </HeaderTemplate> 
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("CourseCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkCourseName" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="CourseName">Course Name</asp:LinkButton>
                                    </HeaderTemplate>          
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseName" Font-Bold="True" Text='<%#Eval("CourseName") %>' />
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

                                <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkYearNo" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="YearNo">Year</asp:LinkButton>
                                    </HeaderTemplate>                                     
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlYearNo" runat="server" SelectedValue='<%#(Eval("YearNo")== null? "0" : Eval("YearNo"))%>' Style="width: 40px;"> 
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField Visible="false"  HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkSemesterNo" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="SemesterNo">Semester</asp:LinkButton>
                                    </HeaderTemplate> 
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
                            <%-- <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />--%>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="LoadStudentCourseHistory">
            <div class="div-margin">
                <asp:Label runat="server" ID="lblWaiver" class="tableBanner display-inline">Transferred/Waived courses</asp:Label>
            </div>
            <%--<asp:Panel ID="PnlWaivedCourse" runat="server" Width="800px" Wrap="False">
                <asp:gridview ID="gvWaiVeredCourse" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                    <RowStyle Height="24px" />
                    <AlternatingRowStyle BackColor="#f5fbfb" />
                    <Columns>

                        <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("CourseCode") %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate><asp:Label runat="server" ID="lblCourseName" Font-Bold="True" Text='<%#Eval("CourseName") %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' /></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <b>No Data Found !</b>
                    </EmptyDataTemplate>
                    <RowStyle CssClass="rowCss" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:gridview>
            </asp:Panel>--%>
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

