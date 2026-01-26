<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="UnRegistration.aspx.cs" Inherits="EMS.miu.registration.UnRegistration" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Student Bulk Un-Registration</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Student Bulk Un-Registration</label>
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
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="350px"  runat="server" OnSelectedIndexChanged="OnCourseSelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td class="auto-style4">
                            <asp:Button runat="server" ID="btnLoadStudents" Text="Load" OnClick="btnLoadStudents_Click"/>
                        </td>
                    </tr>               
                </table>
                
            </div>
            <br />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvStudentCourseHistory" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found." CellPadding="4" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal" Width="700px" OnRowCommand="GvUnReg_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <ItemStyle Width="2%" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="StudentCourseHistoryId" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentCourseHistoryId" runat="server" Text='<%# Bind("StudentCourseHistoryId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                            AutoPostBack="true" />
                        </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkUnRegister" runat="server"></asp:CheckBox>
                    
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button ID="btnUnRegisterAll" runat="server"  Text="Un-Register All" OnClick="btnUnRegisterAll_Click" />
                        </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUnRegister" CommandName="UnReg" Text="Un-Register" CommandArgument='<%# Bind("StudentCourseHistoryId") %>' runat="server"></asp:LinkButton>
                    
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDeleteMarks" CommandName="DelMarks" Text="Delete Marks" OnClientClick="return confirm('Are you sure want to delete exam marks for this student?');" CommandArgument='<%# Bind("StudentCourseHistoryId") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDeleteAttendance" CommandName="DelAttendance" Text="Delete Attendance" OnClientClick="return confirm('Are you sure want to delete class attendance for this student?');" CommandArgument='<%# Bind("StudentCourseHistoryId") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <%--<asp:TemplateField  HeaderText="Message">
                        <ItemTemplate >
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>--%>
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