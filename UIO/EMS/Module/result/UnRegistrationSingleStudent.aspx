<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="UnRegistrationSingleStudent.aspx.cs" Inherits="EMS.Module.result.UnRegistrationSingleStudent" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Student Course Remove</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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
        <label>Student Course Remove</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" Font-Bold="true" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <div class="loadArea">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" Text="Please Read the Following Instruction carefully before Course Un-Register Operation :"></asp:Label></td>
                            <td>
                        </tr> 
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="true" ForeColor="Blue" Text="1. If Grade exist, you cannot Un-Register the Course."></asp:Label></td>
                            <td>
                        </tr> 
                    </table>
                </div>

            </div>
            <div class="Message-Area">

                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Student ID :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="Student ID" />
                        </td> 
                        <td class="auto-style4">
                            <asp:Button runat="server" ID="btnLoadStudents" Text="Load" OnClick="btnLoadStudents_Click" />
                        </td>
                    </tr>
                </table>

            </div>
            <br />

        </ContentTemplate>
    </asp:UpdatePanel>

     <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoadStudents" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoadStudents" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>


    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvStudentCourseHistory" runat="server" AutoGenerateColumns="False" CssClass="table-bordered"
                EmptyDataText="No data found." CellPadding="4" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal" Width="100%" OnRowCommand="GvUnReg_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        <ItemStyle Width="2%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="StudentCourseHistoryId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentCourseHistoryId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("SessionFullCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Course ID" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCourseCode" Font-Bold="True" Text='<%#Eval("FormalCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCourseName" Font-Bold="True" Text='<%#Eval("CourseTitle") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCourseCredit" Font-Bold="True" Text='<%#Eval("CourseCredit") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Obtained Grade">
                        <ItemTemplate>
                            <asp:Label ID="lblObtainedGrade" runat="server" Text='<%# Bind("ObtainedGrade") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Course Status">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseStatus" runat="server" Text='<%# Bind("CourseStatus.Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkUnRegister" runat="server"></asp:CheckBox>

                        </ItemTemplate> 
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button ID="btnUnRegisterAll" runat="server" Text="Un-Register All" OnClick="btnUnRegisterAll_Click" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnUnRegister" CommandName="UnReg" Text="Un-Register" CommandArgument='<%# Bind("ID") %>' OnClientClick=" return confirm('Are you sure you want to Un-Register?');" runat="server"></asp:LinkButton>

                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>
                     
                     
                </Columns>

                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>&nbsp;</div>

</asp:Content>
