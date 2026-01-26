<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_TestCourseWithdraw" Codebehind="CourseWithdrawRequest.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/TreeUserControl.ascx" TagPrefix="uc1" TagName="TreeUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Course WithDraw Request</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function CheckedField() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlSemester').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Semester');
                return false;
            }
            if ($('#MainContainer_txtStudentId').val() == '') {
                $('#MainContainer_lblMsg').text('Please enter Student ID');
                return false;
            }
            if ($('#MainContainer_txtStudentId').val().length < 9) {
                $('#MainContainer_lblMsg').text('Incorrect Student ID');
                return false;
            }

            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
     <div>
        <div class="PageTitle">
            <label>Course WithDraw Request</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="CourseDrop-container">
                    <div class="div-margin">
                        <div class="loadArea" style="height: 20px;">
                            <div style="float: left;">
                                <label class="display-inline field-Title">Program</label>
                            </div>
                            <div style="float: left;">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </div>
                            <div style="float: left;">
                                <label class="display-inline field-Title">Session</label>
                            </div>
                            <div style="float: left;">
                                <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                            </div>
                            <div style="float: left;">
                                <label class="display-inline field-Title">Student ID</label>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox runat="server" ID="txtStudentId" class="margin-zero input-Size" placeholder="Student ID" />
                            </div>
                            <div style="float: left; margin-left: 10px;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                            </div>
                        </div>
                            
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Name</label>
                            <div class="display-inline input-Size2"><asp:TextBox runat="server" ID="txtStudentName" class="margin-zero input-Size2" placeholder="Student Name" /></div>

                            <label class="display-inline field-Title2">Last CGPA</label>
                            <div class="display-inline input-Size3 text-format"><asp:TextBox runat="server" ID="txtLastCGPA" class="margin-zero input-Size3" placeholder="CGPA" /></div>

                            <label class="display-inline field-Title2">Reg. Credit</label>
                            <div class="display-inline input-Size3 text-format"><asp:TextBox runat="server" ID="txtRegCredit" class="margin-zero input-Size3" placeholder="Credit" /></div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="CourseDrop-container">
                    <asp:Panel ID="pnCourseDrop" runat="server" Width="100%" Wrap="False">
                        <asp:gridview ID="gvCourseDrop" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
				            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
				            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>

                                 <asp:BoundField DataField="StudentInfo.Roll" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FormalCode" HeaderText="Course Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Course.Credits" HeaderText="Credit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemark" CssClass="input-Size4" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="CourseStatus.Code" HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                 <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbRequest" OnClick="lbRequest_Click"
                                            ForeColor='<%# (Eval("CourseStatus.Code").ToString() == "Rn" ?  System.Drawing.Color.Blue : System.Drawing.Color.Red)  %>'
                                            OnClientClick='<%# Eval("CourseTitle","return confirm(\"Are you sure to Change the Status for course: {0}\")") %>'
                                            ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                  <%# (Eval("CourseStatus.Code").ToString() == "Rn" ? "Request" : (Eval("CourseStatus.Code").ToString() == "WR" ? "Undo" : ""))  %>                                                    
                                                </div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                              <%--  <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbRequest" CommandArgument='<%#Eval("ID") %>' OnClick="lbRequest_Click">
                                            <span class="action-container">
                                                <input class="requestButton" type="button" <%# (Eval("CourseStatus").ToString() == "R" ? "" : (Eval("CourseStatus").ToString() == "WR" ? "" : "hidden='hidden'"))  %> value='<%# (Eval("CourseStatus").ToString() == "R" ? "Request" : (Eval("CourseStatus").ToString() == "WR" ? "Undo" : ""))  %>' />
                                            </span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
				            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !
                            </EmptyDataTemplate>
				            <SelectedRowStyle Height="24px" />
				            <HeaderStyle CssClass="tableHead" Height="24px" />
			            </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

