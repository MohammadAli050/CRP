<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CourseDropApprove" CodeBehind="CourseDropApprove.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/TreeUserControl.ascx" TagPrefix="uc1" TagName="TreeUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course Drop Approved</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
        });

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
    <div>
        <div class="PageTitle">
            <label>Course Drop Approved/Rejected</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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
                            <div style="float: left; margin-left: 10px;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" />
                            </div>
                        </div>
                         <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>                        
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

         <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel2"
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

        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="CourseDropApproved-container">
                    <asp:Panel ID="pnCourseDrop" runat="server" Width="100%" Wrap="False">
                        <asp:GridView ID="gvCourseDrop" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
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

                                <asp:BoundField DataField="RegCredit" HeaderText="Reg Credit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="LastCGPA" HeaderText="Last CGPA" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                    <ItemStyle Font-Bold="true" HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemark" CssClass="input-Size4" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Approve" ID="lbApproved" CommandArgument='<%#Eval("ID") %>' OnClick="lbApproved_Click">
                                           Approve
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Reject" ID="lbReject" CommandArgument='<%#Eval("ID") %>' OnClick="lbRejected_Click">
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
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

