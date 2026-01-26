<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentBulkForceRegistration.aspx.cs" Inherits="EMS.miu.admin.StudentBulkForceRegistration" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Student Bulk Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 170px;
        }
        .auto-style3 {
            width: 273px;
        }
        .lblCss {
            width: 170px;
        }
        .auto-style4 {
            width: 668px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
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
    <%--<asp:Panel runat="server" ID="pnMessage">
        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" />
        </div>
    </asp:Panel>--%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <div style="padding: 5px; margin: 5px;  width: 900px;">
                    <table>
                        <tr>
                            <td class="tbl-width-lbl"><b>Program</b></td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="tbl-width-lbl"><b>Batch</b></td>
                            <td>
                                <uc1:BatchUserControl runat="server" ID="ucBatch"/>
                            </td>
                            
                            <td class="auto-style4">        
                                <asp:Button ID="btnStudentButton" runat="server" Width="80px" Text="Load" OnClick="btnStudentButton_Click"  />
                            </td>
                        </tr>
                     </Table>
                    <table>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblSemester" runat="server" Width="130px" Text="Semester"></asp:Label>
                            </td>
                            <td class="auto-style4">   
                                <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                               
                            </td>
                        </tr>

                        <tr>
                            <td>
                                 <asp:Button ID="btnLoadAllCourse" runat="server" Text="Load Course" OnClick="btnLoadAllCourse_Click" />
                                
                            </td>
                            <td>
                                <asp:Button ID="btnLoadOfferedCourse" runat="server" Text="Load Offer Course" OnClick="btnLoadOfferedCourse_Click" />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblCourse" runat="server" Width="130px" Text="Course"></asp:Label>
                                
                            </td>
                            <td class="auto-style4">   
                                <asp:DropDownList ID="ddlCourse" runat="server" Width="200px"></asp:DropDownList>
                                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                
                            </td>
                            <td class="auto-style4">   
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:GridView ID="gvStudentCourse" runat="server" Width="600px" DataKeyNames="CourseID" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="gridCss" CellPadding="4" >
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

                                <ItemTemplate >
                                    <asp:CheckBox ID="CheckBox"  runat="server" />
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
                            <asp:TemplateField HeaderText="Credits" >
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

                <div>
                    <asp:GridView ID="gvStudentList" runat="server" Width="600px" DataKeyNames="StudentID" AutoGenerateColumns="False"
                        AllowPaging="false" CssClass="gridCss" CellPadding="4" >
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
                                <%--<ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("CourseID") %>' />
                                        <asp:CheckBox runat="server" ID="ChkActive"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />--%>

                                <ItemTemplate >
                                    <asp:CheckBox ID="CheckBox"  runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStudnetName" Font-Bold="true" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStudentRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            
				        </Columns>
				        <SelectedRowStyle Height="24px" />
				        <HeaderStyle CssClass="tableHead" Height="24px" />
                    </asp:GridView>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>