<%@ Page EnableEventValidation="false" Async="true" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="Registration.aspx.cs" Inherits="EMS.miu.registration.Registration" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Form Fill-Up
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <style>
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .button-margin {
            margin: 0px 0px 8px 0px;
        }

        .center-text {
            text-align: center;
        }


        .btn {
            border: none;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }

        .success {
            background-color: #9acd32;
        }

            .success:hover {
                background-color: #8ab92d;
            }

        .info {
            background-color: #2196F3;
        }

            .info:hover {
                background: #0b7dda;
            }

        .auto-style4 {
            width: 233px;
        }

        .auto-style5 {
            width: 93px;
        }

        .auto-style6 {
            width: 26px;
        }

        .auto-style7 {
            width: 20px;
        }

        .auto-style8 {
            width: 61px;
        }

        .auto-style9 {
            width: 31px;
        }

        .auto-style10 {
            width: 30px;
        }

        .auto-style11 {
            width: 230px;
        }

        .auto-style12 {
            width: 80px;
        }

        .auto-style13 {
            width: 110px;
        }

        .glow_text {
            animation: glow 1s infinite alternate;
        }

        @keyframes glow {
            to {
                text-shadow: 0 0 2px red;
            }
        }

        .glow_text {
            font-family: sans-serif;
            font-weight: bold;
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

        //function openPopup(strOpen) {
        //    open(strOpen, "Info",
        //         "status=1, width=300, height=200, top=100, left=300");
        //}

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 100%">

        <div class="PageTitle">
            <label>Form Fill-Up</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div class="Message-Area">
                        <asp:Label ID="Label2" runat="server" Text="Message : " Font-Bold="false"></asp:Label>
                        <asp:Label class="glow_text" ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="TeacherManagement-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <table>
                                <tr>
                                    <td class="glow_text">
                                        <asp:Label ID="Label8" Font-Bold="true" ForeColor="Red" Font-Size="Medium" runat="server" Text="After finalizing Form-Fill-Up or after Dead-Line examinee will not be able to make any changes."></asp:Label></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Registration No :"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtStudent" Enabled="false" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLoad" CssClass="btn info" runat="server" Visible="false" Text="Load" OnClick="btnLoad_Click" Height="25px" Width="97px" />
                                    </td>
                                    <td class="auto-style8"></td>
                                    <td>
                                        <asp:Label ID="lbl121" runat="server" Font-Bold="true" Text="Reg No: "></asp:Label></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblRegistrationNo" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td class="auto-style9"></td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Roll: "></asp:Label></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblRoll" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <table>
                                <tr>
                                    <td><b>Name :</b></td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td><b>Batch :</b></td>
                                    <td class="auto-style5">
                                        <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td><b>Program :</b></td>
                                    <td>
                                        <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                                        &nbsp; ||&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRegistrationOpenMsg" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="auto-style6"></td>
                                    <td>
                                        <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Registration Session : "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRegistrationSession" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="auto-style7"></td>
                                    <td>
                                        <asp:Label ID="lblSectionCount" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="auto-style7"></td>
                                    <td>
                                        <asp:Label ID="lblCreditCount" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td></td>

                                </tr>
                            </table>
                        </div>
                        <div class="loadedArea">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Institute : "></asp:Label></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblInstitute" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td class="auto-style10"></td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Exem Center: "></asp:Label></td>

                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlExamCenter" class="margin-zero dropDownList">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveExamCenter" Visible="false" runat="server" CssClass="btn info" Height="25px" Width="180px" Text="Save Exam Center" OnClick="btnSaveExamCenter_Click" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Phone : "></asp:Label></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblPhone" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td> 
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlAddCourse" runat="server" Visible="false">
                    <div class="TeacherManagement-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <table style="width: 80%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" Font-Bold="true" ForeColor="Blue" Font-Size="Medium" runat="server" Text="If you wish to add courses for (RETAKE/IRREGULAR/IMPROVEMENT). Please take course from below and take print-out again !"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="loadedArea">
                                <table style="width: 739px">
                                    <tr>
                                        <td class="auto-style12">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Course Type "></asp:Label></td>
                                        <td class="auto-style13">
                                            <asp:DropDownList runat="server" ID="ddlCourseType" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged" Width="130px">
                                                <asp:ListItem Value="1" Selected="True">Regular</asp:ListItem>
                                                <asp:ListItem Value="2">Retake/Irregular</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlCourse" runat="server" Width="222px"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddCourse" CssClass="btn info" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" Height="25px" Width="135px" />
                                        </td>
                                        <td class="auto-style8"></td>


                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel1"
            runat="server">
            <Animations>
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
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender2"
            TargetControlID="UpdatePanel1"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnRegistration" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnRegistration" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <div style="clear: both"></div>

        <div style="padding: 5px; margin: 5px;">
            <fieldset>
                <legend>
                    <p style="font-size: large;"><b>Regular Courses</b></p>
                </legend>
                <div class="education-info-body">
                    <div style="float: right; padding: 5px; margin: 5px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lBtnRefresh" runat="server" Text="Refresh" OnClick="lBtnRefresh_Click"></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="clear: both"></div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="GridViewTable">
                                    <asp:GridView runat="server" ID="gvCourseRegistration" AutoGenerateColumns="False"
                                        AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="20" CssClass="gridCss">
                                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="35" />
                                        <AlternatingRowStyle BackColor="#FFFFCC" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Version Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField Visible="false" HeaderText="Session" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSession" Text='<%#Eval("CalendarDetailName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Submit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblForward" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsForward").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsForward").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Form-Fill-Up" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblReg" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Taken Status" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblIsAutoOpen" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? "Taken" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnFixedCourseTake" runat="server" OnClick="btnFixedCourseTake_Click"
                                                        ForeColor='<%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'
                                                        ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? "(Taken) Click to Remove" : "Click to take" %>                                                     
                                                </div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <EmptyDataTemplate>
                                            No data found!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </fieldset>

            <asp:Panel ID="pnlReg" runat="server">
                <fieldset>
                    <legend></legend>
                    <div class="education-info-body">
                        <div id="divTerms2" runat="server" visible="false" style="float: left; margin-top: 5px">
                            <asp:Label ID="Label9" Font-Bold="true" Font-Italic="true" Font-Underline="true" runat="server" ForeColor="Red" Text="I have checked that Examinee has signed the printed form & Courses mentioned in print-out are similar to the courses shown above."></asp:Label>
                        </div>
                        <div class="cleaner" style="width: 10px; height: 10px;"></div>
                        <div style="float: left; margin-right: 15px;">
                            <asp:Button ID="btnPrint" runat="server" Visible="false" Enabled="true" CssClass="btn success" Height="30px" Width="400px" OnClick="btnForwardPrintOn_Click" Text="PRINT FORM (REGULAR COURSES)" OnClientClick=" return confirm('Are you sure you want to Print Form?');" />
                        </div>

                        <div style="float: left;">
                            <asp:Button ID="btnRegistration" runat="server" Visible="false" CssClass="btn success" Text="CONFIRM FORM-FILL-UP" Height="30" Enabled="true" OnClick="btnRegistration_Click" OnClientClick=" return confirm('Are you sure you want to complete Form-Fill-Up?');" />
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
        </div>


        <div style="padding: 5px; margin: 5px;">
            <fieldset>
                <legend>
                    <p style="font-size: large;"><b>Retake/Improvement Course</b></p>
                </legend>
                <div class="education-info-body">
                    <div style="float: right; padding: 5px; margin: 5px;">
                    </div>
                    <div style="float: right; padding: 5px; margin: 5px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Refresh" OnClick="lBtnRefresh_Click"></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="clear: both"></div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div id="Div1">
                                    <asp:GridView runat="server" ID="gvCourseHistory" AutoGenerateColumns="False"
                                        AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="20" CssClass="gridCss">
                                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="35" />
                                        <AlternatingRowStyle BackColor="#FFFFCC" />

                                        <Columns>

                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="2%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Course Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("CourseCredit") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Grade" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("ObtainedGrade") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Session" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSession" Text='<%#Eval("SessionFullCode") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Submit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblForward" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsForward").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsForward").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Form-Fill-Up" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblIsCourseRegistered" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsCourseRegistered").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsCourseRegistered").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Taken Status" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblReg" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsRetakeTaken").ToString())) ? "Taken" : "Not Taken" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsRetakeTaken").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPreRegistrationRetake" runat="server" OnClick="btnPreRegistrationRetake_Click"
                                                        ForeColor='<%# (Boolean.Parse(Eval("IsRetakeTaken").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'
                                                        ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsRetakeTaken").ToString())) ? "(Taken) Click to Remove" : "Click to take" %>                                                     
                                                </div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <EmptyDataTemplate>
                                            No data found!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </fieldset>

            <asp:Panel ID="Panel2" runat="server">
                <fieldset>
                    <legend></legend>
                    <div class="education-info-body">
                        <div id="divTerms" runat="server" visible="false" style="float: left; margin-top: 5px;">
                            <asp:Label ID="Label10" Font-Bold="true" Font-Italic="true" Font-Underline="true" runat="server" ForeColor="Red" Text="I have checked that Examinee has signed the printed form & Courses mentioned in print-out are similar to the courses shown above."></asp:Label>
                        </div>
                        <div class="cleaner" style="width: 10px; height: 10px;"></div>
                        <div style="float: left; margin-right: 15px;">
                            <asp:Button ID="btnRetakeForwardPrint" runat="server" Visible="false" Enabled="true" CssClass="btn success" Height="30px" Width="400px" OnClick="btnRetakeForwardPrint_Click" Text="PRINT FORM (RETAKE/IRREGULAR COURSES)" OnClientClick=" return confirm('Are you sure you want to Print Form?');" />
                        </div>

                        <div style="float: left;">
                            <asp:Button ID="btnRetakeRegistration" runat="server" Visible="false" CssClass="btn success" Text="CONFIRM FORM-FILL-UP" Height="30" Enabled="true" OnClick="btnRegistration_Click" OnClientClick=" return confirm('Are you sure you want to complete Form-Fill-Up?');" />
                        </div>

                    </div>
                </fieldset>
            </asp:Panel>
        </div>

        <div style="padding: 5px; margin: 5px;">
            <fieldset>
                <legend>
                    <p style="font-size: large;"><b>Irregular Courses</b></p>
                </legend>
                <div class="education-info-body">
                    <div style="float: right; padding: 5px; margin: 5px;">
                    </div>
                    <div style="clear: both"></div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div id="Div2">
                                    <asp:GridView runat="server" ID="gvIrregularCourses" AutoGenerateColumns="False"
                                        AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                                        PageSize="20" CssClass="gridCss">
                                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="35" />
                                        <AlternatingRowStyle BackColor="#FFFFCC" />

                                        <Columns>

                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="2%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Version Code" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField Visible="false" HeaderText="Session" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSession" Text='<%#Eval("CalendarDetailName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="4%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Submit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblForward" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsForward").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsForward").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Form-Fill-Up" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblReg" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? "Done" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Taken Status" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div style="text-align: center;">
                                                        <asp:Label ID="lblIsAutoOpen" runat="server" Font-Bold="true"
                                                            Text=' <%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? "Taken" : "--" %>'
                                                            ForeColor='<%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnIrregularCourseTake" runat="server" OnClick="btnIrregularCourseTake_Click"
                                                        ForeColor='<%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'
                                                        ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsAutoOpen").ToString())) ? "(Taken) Click to Remove" : "Click to take" %>                                                     
                                                </div>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="6%" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <EmptyDataTemplate>
                                            No data found!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </fieldset>
        </div>


    </div>
    <div>

        <rsweb:ReportViewer Visible="false" ID="ReportViewer" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="57%" Height="100%" SizeToReportContent="true">
        </rsweb:ReportViewer>
    </div>

</asp:Content>

