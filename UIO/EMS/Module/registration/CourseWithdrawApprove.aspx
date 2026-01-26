<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_TestCourseWithDrawApprove" Codebehind="CourseWithdrawApprove.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/TreeUserControl.ascx" TagPrefix="uc1" TagName="TreeUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Course Withdraw Approved</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
        });       
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Course Withdraw Approved/Rejected</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="CourseDrop-container">
                    <div class="div-margin">
                        <div class="loadArea"  style="height: 20px;">
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
                            <div style="float: left; margin-left: 10px;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="CourseDropApproved-container">
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

                                <asp:boundfield DataField="RegCredit" HeaderText="Reg Credit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:boundfield>

                                <asp:boundfield DataField="LastCGPA" HeaderText="Last CGPA" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:boundfield>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemark" CssClass="input-Size4" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Approve" ID="lbApproved" CommandArgument='<%#Eval("ID") %>' OnClick="lbApproved_Click" >
                                           Approve
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Reject" ID="lbReject" CommandArgument='<%#Eval("ID") %>' OnClick="lbRejected_Click" >
                                           Reject
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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

