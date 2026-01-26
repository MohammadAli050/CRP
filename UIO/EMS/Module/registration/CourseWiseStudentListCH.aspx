<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CourseWiseStudentListCH" Codebehind="CourseWiseStudentListCH.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
      <div style="padding: 10px; width: 1200px;">
        <div class="PageTitle">
            <label>Course Wise Students</label>
        </div>

        <div class="Message-Area" style="height: 35px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 1100px;">
                        <tr>
                            <td class="tbl-width-lbl"><b>Program</b></td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="tbl-width-lbl"><b>Course</b></td>
                            <td>
                                <asp:DropDownList ID="ddlCourse" runat="server" Width="90px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">                                    
                                </asp:DropDownList>
                            </td>

                            <td class="tbl-width-lbl-Small"><b>Section</b></td>
                            <td>
                                <asp:DropDownList ID="ddlBlock" runat="server" Width="90px">                                    
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                            </td>
                        </tr>
                    </table>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>                        
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel3"
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

        <div style="clear: both;"></div>
        <div>
            <div style="padding: 1px; margin-top: 20px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div style="float: left; width: 100%;">
                            <div style="float: left;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Student : "></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                            </div>                           
                        </div>
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" AllowPaging="false" PageSize="20"
                            PagerSettings-Mode="NumericFirstLast"  Width="100%"
                            PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                            ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="FullName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblProgram" Font-Bold="true" Text='<%#Eval("Program") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Batch">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBatch" Font-Bold="true" Text='<%#Eval("Batch") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>                                 
                            </Columns>
                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>                           
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

