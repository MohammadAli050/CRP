<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ViewStudentCourse" Codebehind="ViewStudentCourse.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
   View Course
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }

        /*table, tr, th {
            border: 1px solid #008080;
        }*/

        .msgPanel {
            margin-top: 10px;
            border: 0px solid #008080;
            padding: 5px;
            width: 988px;
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
    <div style="padding: 10px; width: 1250px;">
        <div class="PageTitle">
            <label>View Course</label>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area" style="height: 45px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 1000px;">
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="ddlAcaCalBatch" runat="server" Width="120px">
                                </asp:DropDownList></td>
                            <td>Program</td>
                            <td>
                                <asp:DropDownList ID="ddlProgram" runat="server" Width="130px"
                                    DataTextField="ShortName" DataValueField="ProgramID">
                                </asp:DropDownList></td>
                            <td>
                                <asp:Label ID="lblStudentId" runat="server" Text="Student ID"></asp:Label>
                                <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                            </td>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -33px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                        </div>
                        <div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">
                            Please wait...
                        </div>
                    </div>
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

                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Student : "></asp:Label>
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" AllowPaging="false" PageSize="20"
                            PagerSettings-Mode="NumericFirstLast" PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                            ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White"/>
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <Columns>

                                <asp:TemplateField HeaderText="FullName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opened Course">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblOpenedCourse" Text='<%#Eval("OpenedCourse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="350px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Assigned Course">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAssignedCourse" Text='<%#Eval("AssignedCourse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Mandatory Course">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMandatoryCourse" Text='<%#Eval("MandatoryCourse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
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

