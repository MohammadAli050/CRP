<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentForceRegistration.aspx.cs" Inherits="EMS.miu.admin.StudentForceRegistration" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Student Registration
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
        <label>Student Registration</label>
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
                            <td class="auto-style2">
                                    <asp:Label ID="lblStudentRoll" runat="server" width="150px" Text="Student Roll"></asp:Label>   
                            </td>
                            <td class="auto-style4">        
                                <asp:TextBox ID="txtStudentRoll" Placeholder="Student Roll" width="200px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnStudentButton" runat="server" Width="80px" Text="Load" OnClick="btnStudentButton_Click"  />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblStudentName" runat="server" width="150px" Text="Student Name"></asp:Label>   
                            </td>
                            <td class="auto-style4">   
                                <asp:TextBox ID="txtStudentName" Placeholder="Student Name" width="200px" Enabled="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>

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
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>