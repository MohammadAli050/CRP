<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_ForceOperationForCourse" Codebehind="ForceOperationForCourse.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Force Operation</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
     
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 148px;
        }
        .auto-style2 {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Force Operation</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="ForceOperation-container">
            <div class="div-margin">
                <div class="loadArea">
                    <table>
                    <tr>
                    <td>
                        <label class="display-inline field-Title">Program :</label>
                    </td>
                    <td>
                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" class="margin-zero dropDownList" />
                        <%--<asp:DropDownList runat="server" Width="160px" DataTextField="NameWithCode" DataValueField="ProgramID" ID="ddlProgram" class="margin-zero dropDownList" />--%>
                    </td>
                    <td>
                    <label class="display-inline field-Title">Batch :</label>
                        </td>
                    <td>
                    <%--<asp:DropDownList runat="server" ID="ddlBatch" DataTextField="FullCode" class="margin-zero dropDownList" />--%>
                    <uc1:BatchUserControl runat="server" ID="ucBatch"  class="margin-zero dropDownList"/>
                    </td>
                    <td>
                    <label class="display-inline field-Title">Semester :</label>
                        </td>
                    <td>
                    <%--<asp:DropDownList runat="server" ID="ddlSemester" class="margin-zero dropDownList" />--%>
                    <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                    </td>
                    <td>
                    <label class="display-inline field-Title">Course</label>
                        </td>
                    <td>
                    <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList"/>
                    </td>
                            </tr>
                        </table>
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Student ID :</label>
                    <asp:TextBox runat="server" ID="txtStudentId" MaxLength="15" class="margin-zero input-Size" placeholder="Student ID" />

                    <label class="display-inline field-Title2">Name :</label>
                    <asp:TextBox runat="server" ID="txtStudentName" MaxLength="20" class="margin-zero input-Size2" placeholder="Student Name" ReadOnly="true" />
                    <%--<asp:Button runat="server" ID="btnPickStudentName" Text="..." OnClick="btnPickStudentName_Click" />--%>
                </div>
                <div class="loadedArea">
                    <div style="display: inline-block;">
                        <label class="display-inline">Auto-Open :</label>
                        <asp:DropDownList runat="server" ID="ddlAutoOpen" class="margin-zero dropDownList2">
                            <asp:ListItem Value="0" Text="X" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="0"></asp:ListItem>
                            <asp:ListItem Value="2" Text="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display: inline-block;">
                        <label class="display-inline">Pre-Registration :</label>
                        <asp:DropDownList runat="server" ID="ddlPreRegistration" class="margin-zero dropDownList2">
                            <asp:ListItem Value="0" Text="X" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="0"></asp:ListItem>
                            <asp:ListItem Value="2" Text="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="display: inline-block;">
                        <label class="display-inline">Mandatory :</label>
                        <asp:DropDownList runat="server" ID="ddlMandatory" class="margin-zero dropDownList2">
                            <asp:ListItem Value="0" Text="X" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="0"></asp:ListItem>
                            <asp:ListItem Value="2" Text="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div style="display: inline-block;">
                        <asp:TextBox runat="server" ID="txtCourseCode" placeholder="CourseCode" class="margin-zero input-Size3" />
                    </div>
                    <div style="display: inline-block;">
                        <asp:TextBox runat="server" ID="txtCourseTitle" placeholder="CourseTitle" class="margin-zero input-Size" />
                    </div>
                    <div style="display: inline-block;">
                        <label class="display-inline field-Title3">Seq No.</label>
                        <asp:TextBox runat="server" ID="txtLow" placeholder="LOW" class="margin-zero input-Size4" />
                        <asp:TextBox runat="server" ID="txtHigh" placeholder="HIGH" class="margin-zero input-Size4" />
                    </div>
                </div>
                <div class="loadedArea">
                    <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                    <label class="display-inline field-Title4"><asp:CheckBox runat="server" Checked="true" ID="chkPriority" /> Priority Wise Short</label>
                    <div class="cleaner"></div>
                </div>
            </div>
        </div>

        <div class="ForceOperationWorkSheetGenerate">
            <asp:Panel ID="pnWorkSheetGenerate" runat="server" Width="100%" Wrap="False"><%--ScrollBars="Vertical" Wrap="False"--%>
            <%--<asp:Panel ID="pnWorkSheetGenerate" runat="server" Width="980px" Height="400px" style="display: inline-block;">--%>
                <%--<asp:gridview ID="gvWorkSheetGenerate" runat="server" AutoGenerateColumns="False" Height="80px" TabIndex="6" Width="960px" ShowHeader="True" style="border: 1px solid gray;">--%>
                <asp:gridview ID="gvWorkSheetGenerate" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
				    <RowStyle Height="24px" />
                    <AlternatingRowStyle BackColor="#f5fbfb" />
				    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <ItemStyle Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btnAutoOpen" OnClick="btnAutoOpen_Click" runat="server" Text="Auto-Open" /><hr />
                                <asp:CheckBox ID="chkAutoOpenAll" runat="server" Text="Select" AutoPostBack="true" OnCheckedChanged="chkAutoOpenAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox runat="server" ID="chkIsAutoOpen" Checked='<%#Eval("IsAutoOpen") %>'></asp:CheckBox>
                                    <asp:HiddenField ID="hdIsAutoOpen" runat="server" Value='<%#Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>

                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btnIsAutoAssign" OnClick="btnIsAutoAssign_Click" runat="server" Text="Pre-Reg" /><hr />
                                <asp:CheckBox ID="chkIsAutoAssignAll" runat="server" Text="Select" AutoPostBack="true" OnCheckedChanged="chkIsAutoAssignAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox ID="chkIsAutoAssign" Checked='<%# Eval("IsAutoAssign")%>' runat="server" />
                                    <asp:HiddenField ID="hdIsAutoAssign" runat="server" Value='<%#Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>--%>

                       <%-- <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Button ID="btnMandatory" OnClick="btnMandatory_Click" runat="server" Text="Mandatory" /><hr />
                                <asp:CheckBox ID="chkMandatoryAll" runat="server" Text="Select" AutoPostBack="true" OnCheckedChanged="chkMandatoryAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox ID="chkIsMandatory" Checked='<%# Eval("IsMandatory")%>' runat="server" />
                                    <asp:HiddenField ID="hdIsMandatory" runat="server" Value='<%#Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>--%>

                        <%--<asp:TemplateField ItemStyle-Width="70px" HeaderText="Auto-Open" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsAutoOpenSingle" Checked='<%# Eval("IsAutoOpen")%>' runat="server" />
                                <asp:HiddenField ID="hdIsAutoOpen" runat="server" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <%--<asp:TemplateField ItemStyle-Width="70px" HeaderText="Pre-Reg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPreRegistrationSingle" Checked='<%# Eval("IsAutoAssign")%>' runat="server" />
                                <asp:HiddenField ID="hdIsMandatory" runat="server" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <%--<asp:TemplateField ItemStyle-Width="70px" HeaderText="Mandatory" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMandatorySingle" Checked='<%# Eval("IsMandatory")%>' runat="server" />
                                <asp:HiddenField ID="hdIsMandatory" runat="server" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:boundfield DataField="StudentID" HeaderText="Student ID" Visible="true" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>

                        <asp:boundfield DataField="StudentName" HeaderText="Student Name" Visible="True" ItemStyle-Width="160" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>

                        <asp:boundfield DataField="CourseCode" HeaderText="Course Code" Visible="True" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        <asp:boundfield DataField="CourseStatus" HeaderText="Course Status" Visible="True" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        <asp:boundfield ItemStyle-Width="190px" DataField="CourseName" HeaderText="Course Title" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        <asp:boundfield ItemStyle-Width="190px" DataField="PreRequisiteCourseName" HeaderText="Pre-Requisite" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        
                        <asp:boundfield ItemStyle-Width="40px" DataField="CourseCredit" HeaderText="Credit" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        <asp:boundfield DataField="Grade" HeaderText="Grade" Visible="True" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
                        <asp:boundfield ItemStyle-Width="50px" DataField="Semester" HeaderText="Semester" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>

                        <asp:boundfield ItemStyle-Width="50px" DataField="Priority" HeaderText="Priority" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>

                        <asp:boundfield ItemStyle-Width="50px" DataField="SequenceNo" HeaderText="Seq. No" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:boundfield>
				    </Columns>
				    <SelectedRowStyle Height="24px" />
				    <HeaderStyle CssClass="tableHead" Height="24px" />
			    </asp:gridview>
            </asp:Panel>
        </div>
    </div>
</asp:Content>