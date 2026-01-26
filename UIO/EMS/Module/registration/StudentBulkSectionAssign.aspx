<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master"
    AutoEventWireup="true" Inherits="Admin_StudentBulkSectionAssign" CodeBehind="StudentBulkSectionAssign.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Bulk Section Assign 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style>
        .msgPanel
        {
            margin-top: 20px;
            margin-bottom: 25px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
            padding: 5px;
        }
    </style>

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
    <div style="padding: 5px; width: 100%;">
        <div class="PageTitle">
            <label>Student Bulk Section Assign</label>
        </div>

        <div class="Message-Area" style="width: 100%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                <td>Program</td>
                                <td>
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                                <td>Session</td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                </td>
                                <td>Batch</td>
                                <td>
                                    <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                </td>
                                <td>Course</td>

                                <td>
                                    <asp:DropDownList ID="ddlCourse" runat="server" Width="300" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>Roll</td>
                                <td>
                                    <asp:DropDownList ID="ddlGender" runat="server" Width="150" Visible="false" Height="0">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlRoll" runat="server" Width="150">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Even" Value="Even"></asp:ListItem>
                                        <asp:ListItem Text="Odd" Value="Odd"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" Text="Load Student" OnClick="btnLoad_Click" />
                                </td>

                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>

                                <td>Section for Assign</td>
                                <td>
                                    <asp:DropDownList ID="ddlSection" runat="server" Width="150"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div id="divProgress" style="display: none; width: 195px; float: right; margin: -95px -140px 0 0;">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="50px" Width="50px" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both;"></div>

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

        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Student : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" AllowPaging="false" PageSize="20"
                        PagerSettings-Mode="NumericFirstLast" Width="100%"
                        PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                        ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                        <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#F0F8FF" />
                        <Columns>
                           
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGender" Text='<%#Eval("Gender") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSection" Font-Bold="true" Text='<%#Eval("SectionName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Button ID="btnSectionUpdate" runat="server" Text="Update Section" OnClick="btnSectionUpdate_Click" />
                                    <hr />
                                    <asp:CheckBox ID="chkSelect" runat="server"
                                        AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:CheckBox runat="server" ID="ChkSelect"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                        <%-- <HeaderStyle CssClass="tableHead" />--%>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
</asp:Content>

