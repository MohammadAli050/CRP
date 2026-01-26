<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="PreAdvisingRetake" Codebehind="PreAdvisingRetake.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
   Retake Course
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }

        /*table, tr, th {
            border: 1px solid #008080;
        }*/

        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
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
    <div style="padding: 5px; width: 1200px;">
        <div class="PageTitle">
            <label>Retake Course</label>
        </div>
        
        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 1px; width: 900px;">
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
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divProgress" style="display:none ; float: right; z-index: 1000; margin-top: -55px">
                <div style="float: left">
                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                </div>
                <div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">
                    Please wait...
                </div>
            </div>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <div style="border: 0px solid blue; padding: 5px; width: 890px;">
                            <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
                <div style="padding-top: 10px; width: 900px;">
                    <fieldset>
                        <legend>Course For retake</legend>
                        <div id="Div1">
                            <asp:GridView runat="server" ID="gvCoursePreRegistrationRetake" AutoGenerateColumns="False"
                                AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                                PageSize="20" CssClass="gridCss">
                                  <HeaderStyle BackColor="SeaGreen" ForeColor="White"  Height="35"/>
                                        <AlternatingRowStyle BackColor="#FFFFCC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <div align="center">
                                                <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits","{0:00}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <div align="center">
                                                <asp:Label runat="server" ID="lblObtainedGrade" Text='<%#Eval("ObtainedGrade") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <div align="center">
                                                <asp:Label runat="server" ID="lblGroup" Text='<%#Eval("NodeGroup") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered trimester" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <div align="center">
                                                <asp:Label runat="server" ID="lblCalendarDetailName" Text='<%#Eval("CalendarDetailName") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPreRegistrationRetake" runat="server" OnClick="btnPreRegistrationRetake_Click"
                                                ForeColor='<%# (Boolean.Parse(Eval("IsAutoAssign").ToString())) ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>'
                                                OnClientClick='<%# Eval("CourseTitle","return confirm(\"Are you sure to Change the Status for course: {0}\")") %>'
                                                ToolTip="Course  Registration" CommandArgument='<%#Eval("ID") %>'>
                                                <div align="center">                                                  
                                                   <%# (Boolean.Parse(Eval("IsAutoAssign").ToString())) ? "× Undo" : "√ Select" %>                                                     
                                                </div>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="6%" />
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

