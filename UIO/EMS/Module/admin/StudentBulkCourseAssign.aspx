<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentBulkCourseAssign.aspx.cs" Inherits="EMS.Module.admin.StudentBulkCourseAssign" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Bulk Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style type="text/css">
 

        .auto-style1 {
            width: 20px;
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Student Bulk Registration</label>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Program"></asp:Label>
                                </td>
                                <td>
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Batch"></asp:Label>
                                </td>
                                <td>
                                    <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:Label ID="lblSemester" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSemester" runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" Width="80px" AutoPostBack="true" />
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Course"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCourse" runat="server" Width="200px"></asp:DropDownList>
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Comp Cr."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompCredit" placeholder="0" Text="0" Width="50px" TextMode="Number" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style1"></td> 
                                <td>
                                    <asp:Button ID="btnLoadStudent" runat="server" Text="Load Student" OnClick="btnStudentButton_Click" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div class="loadedArea">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnLoadOfferedCourse" runat="server" Text="Load Course" OnClick="btnLoadAllCourse_Click" />
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:DropDownList ID="ddlAssignCourse" runat="server" Width="200px"></asp:DropDownList>
                                </td>
                                <td class="auto-style1"></td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div class="loadedArea">
                        <table style="height:40px">
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div>
                <asp:GridView ID="gvStudentCourse" runat="server" Width="600px" DataKeyNames="CourseID" AutoGenerateColumns="False"
                    AllowPaging="false" CssClass="gridCss" CellPadding="4">
                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Course Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCourseId" Font-Bold="true" Text='<%#Eval("CourseID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Version Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblVersionId" Font-Bold="true" Text='<%#Eval("VersionID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate>
                            <%--<ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("CourseID") %>' />
                                        <asp:CheckBox runat="server" ID="ChkActive"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />--%>

                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCourseCode" Font-Bold="true" Text='<%#Eval("FormalCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCourseName" Font-Bold="true" Text='<%#Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credits">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="true" Text='<%#Eval("Credits") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                    </Columns>
                    <SelectedRowStyle Height="24px" />
                    <HeaderStyle CssClass="tableHead" Height="24px" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table>
                    <tr>
                        <td class="auto-style2">
                            <asp:Button ID="btnWorkSheet" runat="server" Visible="false" Text="Open For Registartion" OnClick="btnWorkSheet_Click" />
                        </td>
                        <td class="auto-style3">
                            <asp:Button ID="btnRegistrationAndBilling" runat="server" Visible="false" Text="Registration + Bill Generate" OnClick="btnRegistrationAndBilling_Click" />
                        </td>
                        <td class="auto-style2">
                            <asp:Button ID="btnRegistration" runat="server" Visible="false" Text="Registration" OnClick="btnRegistration_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div>
                <asp:GridView ID="gvStudentList" runat="server" Width="600px" DataKeyNames="StudentID" AutoGenerateColumns="False"
                    AllowPaging="false" CssClass="gridCss" CellPadding="4">
                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Student Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStudentId" Font-Bold="true" Text='<%#Eval("StudentID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAllStudent" runat="server" Text="All" OnCheckedChanged="chkSelectAllStudent_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate> 
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStudnetName" Font-Bold="true" Text='<%#Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roll">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStudentRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First CompCredit">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstCompCredit" Font-Bold="true" Text='<%#Eval("FirstCompCredit") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Second CompCredit">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSecondCompCredit" Font-Bold="true" Text='<%#Eval("SecondCompCredit") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Third CompCredit">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblThirdCompCredit" Font-Bold="true" Text='<%#Eval("ThirdCompCredit") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField> 

                    </Columns>
                    <SelectedRowStyle Height="24px" />
                    <HeaderStyle CssClass="tableHead" Height="24px" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
