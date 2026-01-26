<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_StudentSectionChange" Codebehind="StudentSectionChange.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Section Change
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    
    <style>
        .msgPanel {
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
            <label>Course Offer & Active</label>
        </div>

        <div class="Message-Area" style="width: 1150px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 1050px;">
                            <tr>
                                <td>Program</td>
                                <td>
                                    <uc1:ProgramUserControl  runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                                <td>Session</td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                </td>
                                <td>Course</td>
                                <td>
                                    <asp:DropDownList ID="ddlCourse" runat="server" Width="300" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnLoad" runat="server" Text="Load Student" OnClick="btnLoad_Click" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    </td>
                                <td>
                                   
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div id="divProgress" style="display: none; width: 195px; float: right; margin: -43px -140px 0 0;">
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

        <div >
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
                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("StudentInfo.BasicInfo.FullName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("StudentInfo.Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                              <asp:TemplateField>
                                <HeaderTemplate>
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
                             <asp:TemplateField HeaderText="Section">
                                  <HeaderTemplate>
                                    <asp:DropDownList ID="ddlSection" runat="server" Width="130"></asp:DropDownList>                                     
                                    <br />
                                    <hr />
                                     <asp:Button ID="btnSectionUpdate" runat="server" Text="Update Section" OnClick="btnSectionUpdate_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSection" Font-Bold="true" Text='<%#Eval("Section.SectionName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
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

