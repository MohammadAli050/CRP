<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="PreAdvising" Codebehind="PreAdvising.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Pre Advising (New)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
         
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
    <div style="padding: 5px; width: 1200px;">
         <div class="PageTitle">
            <label>Pre Advising (New)</label>
        </div>
        
        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 1px; width: 100%;">
                            <tr>
                                <td class="tbl-width-lbl">
                                    <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Student ID :"></asp:Label></td>
                                <td class="tbl-width">

                                    <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                                </td>
                                <td class="tbl-width-lbl">
                                    <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="tbl-width-lbl"><b>Name :</b></td>
                                <td class="tbl-width">
                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="tbl-width-lbl"><b>Batch :</b></td>
                                <td class="tbl-width">
                                    <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="tbl-width-lbl"><b>Program :</b></td>
                                <td class="tbl-width">
                                    <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="tbl-width-lbl" style="width: 300px"><b>Pre-Advising Cr. Limit :</b></td>
                                <td class="tbl-width">
                                    <asp:Label ID="lblPreAdvisingCrLimit" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                <div style="float: left">
                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                </div>
            </div>
        </div>

         <div class="Message-Area">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                            <div style="border: 0px solid blue; padding: 5px; width: 890px;">
                                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Tomato"></asp:Label>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        <div style="clear: both;"></div>

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

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="padding-top: 10px; width: 100%;">
                    <fieldset>
                        <legend>Auto Open Course</legend>
                        <div style="float: right; padding: 5px; margin: 5px;">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lBtnRefresh" runat="server" Text="Refresh" OnClick="lBtnRefresh_Click"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div style="clear: both"></div>

                        <div id="GridViewTable">
                            <asp:GridView runat="server" ID="gvCoursePreRegistration" AutoGenerateColumns="False" Width="100%"
                                CssClass="gridCss">
                                <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#F0F8FF" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle  />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credits" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits","{0:00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group" HeaderStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGroup" Text='<%#Eval("NodeGroup") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="130px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered trimester" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCalendarDetailName" Text='<%#Eval("CalendarDetailName") %>'>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnRegistration" runat="server" OnClick="btnPreRegistration_Click"
                                                ForeColor='<%# (Boolean.Parse(Eval("IsAutoAssign").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'
                                                OnClientClick='<%# Eval("CourseTitle","return confirm(\"Are you sure to Change the Status for course: {0}\")") %>'
                                                ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsAutoAssign").ToString())) ? "× Undo" : "√ Take" %>                                                     
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    No data found!
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

