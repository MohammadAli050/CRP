<%@ Page Title="Exam Result Publish" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultPublishIndividualStudent.aspx.cs" Inherits="EMS.miu.result.ExamResultPublishIndividualStudent" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Title" Runat="Server">
    Exam Result Publish
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Head" Runat="Server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Exam Result Publish</label>
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
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" Width="450px" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div1" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                <div style="float: left">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                </div>
                <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                    Please wait...
                </div>
            </div>

            <br />

            <asp:GridView ID="GvResultPublish" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found." CellPadding="4" BackColor="White" BorderColor="#336666" BorderStyle="Double" 
            BorderWidth="3px" GridLines="Horizontal" OnRowCommand="GvResultPublish_RowCommand">
                <Columns>
                    <asp:TemplateField Visible ="false"  HeaderText="Student Id">
                        <ItemTemplate >
                            <asp:Label ID="lblCourseHistoryId"  runat="server" Text='<%# Bind("CourseHistoryId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Student Roll">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentRoll" runat="server" Text='<%# Bind("StudentRoll") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Student Name">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Marks">
                        <ItemTemplate >
                            <asp:Label ID="lblObtainedTotalMarks" runat="server" Text='<%# Bind("ObtainedTotalMarks") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="GPA">
                        <ItemTemplate >
                            <asp:Label ID="lblObtainedGPA" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Grade">
                        <ItemTemplate >
                            <asp:Label ID="lblObtainedGrade" runat="server" Text='<%# Bind("ObtainedGrade") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>

                   <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btLnkPublish" CommandName="PublishResult" Text="Publish" CommandArgument='<%# Bind("CourseHistoryId") %>' runat="server"></asp:LinkButton>
                        <%--<asp:LinkButton ID="DeleteButton" CommandName="TestSetDelete" Text="Delete" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>--%>
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

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
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
</asp:Content>
