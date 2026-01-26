<%@ Page Title="Exam Result Publish" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultPublish.aspx.cs" Inherits="EMS.miu.result.ExamResultPublish" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Exam Result Publish
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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
                            <asp:Button ID="btnLoadSection" runat="server" Text="Load Result" OnClick="btnLoadSection_Click" />   
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <b>Total section :</b>
                            <asp:Label ID="lblTotalSection" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <b>Total final submitted section :</b>
                            <asp:Label ID="lblTotalFinalSubmitSection" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <b>Total published section :</b>
                            <asp:Label ID="lblTotalPublishedSection" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Panel ID="Panel1" Visible="false" runat="server">
                                <asp:Button ID="btnPublishAll" runat="server" Visible="false" Text="Publish All" OnClick="btnPublishAll_Click"/>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvExamMarkPublish" runat="server" AutoGenerateColumns="False" CssClass="table-bordered"
                EmptyDataText="No data found." CellPadding="4" BackColor="White" BorderColor="#336666" 
                BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal" OnRowCommand="gvExamMarkPublish_RowCommand" >
                <Columns>
                    
                    <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible ="false"  HeaderText="Student Id">
                        <ItemTemplate >
                            <asp:Label ID="lblAcacalSectionId"  runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="AcademicCalenderID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblAcademicCalenderId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="CourseID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="VersionID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="CourseName">
                        <ItemTemplate >
                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Bind("CourseName") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="SectionName">
                        <ItemTemplate >
                            <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                        
                    <asp:TemplateField  HeaderText="StudentCount">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentCount" runat="server" Text='<%# Bind("StudentCount") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Final Submit">
                        <ItemTemplate >
                            <asp:Label ID="lblIsFinalSubmit" runat="server"  Text='<%# Bind("IsFinalSubmit") %>'></asp:Label> 
                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Published" >
                        <ItemTemplate >
                            <asp:Label ID="lblIsPublished" runat="server" Visible="false" Text='<%# Bind("IsPublished") %>'></asp:Label> 
                            <asp:Button ID="btnResultPublish" runat="server"  Text="Publish" CommandName="ResultPublish" CommandArgument='<%#Eval("AcaCal_SectionID")+","+Eval("AcademicCalenderID")+","+Eval("CourseID")+","+Eval("VersionID")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Re Published" >
                        <ItemTemplate >
                            <asp:Button ID="btnResultRePublish" runat="server"  Text="Re Publish" CommandName="ResultRePublish" CommandArgument='<%#Eval("AcaCal_SectionID")+","+Eval("AcademicCalenderID")+","+Eval("CourseID")+","+Eval("VersionID")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
                <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoadSection" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoadSection" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
