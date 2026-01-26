<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master"
    AutoEventWireup="true" Inherits="StudentBulkRegistrationNew" CodeBehind="StudentBulkRegistration.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Bulk Registration
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
        .auto-style1 {
            width: 193px;
        }
        .auto-style2 {
            width: 62px;
        }
        .auto-style3 {
            width: 154px;
        }
        .auto-style4 {
            width: 136px;
        }
        .auto-style5 {
            width: 59px;
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
            <label>Student Bulk Registration</label>
        </div>

        <div class="Message-Area" style="width: 100%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                <td class="auto-style2">Program</td>
                                <td class="auto-style1">
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                                <td class="auto-style3">Session</td>
                                <td class="auto-style4">
                                    <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                </td>
                                <td class="auto-style5">Batch</td>
                                <td>
                                    <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                                </td> 
                            </tr>
                            <tr>
                                <td class="auto-style2">Gender</td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlGender" runat="server" Width="150">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style3">
                                    <asp:Button ID="btnLoad" runat="server" Text="Load Student" OnClick="btnLoad_Click" />
                                </td>

                                <td class="auto-style4"></td>
                                <td class="auto-style5"></td>
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
                            <asp:TemplateField HeaderText="SL." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="30px"/>
                        </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' /> 
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("FullName") %>'></asp:Label>
                                </ItemTemplate>                                 
                                <HeaderStyle Width="200px" />
                                <HeaderStyle />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGender" Text='<%#Eval("Gender") %>'></asp:Label>
                                </ItemTemplate> 
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>   

                              <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSection" Font-Bold="true" Text='<%#Eval("CourseAndSection") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Registered Section">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRegisteredSection" Font-Bold="true" Text='<%#Eval("RegCourseAndSection") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClientClick=" return confirm('Are you sure, you want to Save?')" OnClick="btnRegister_Click" />
                                    <hr />
                                    <asp:CheckBox ID="chkSelect" runat="server"
                                        AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center"> 
                                        <asp:CheckBox runat="server" ID="ChkSelect"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
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

