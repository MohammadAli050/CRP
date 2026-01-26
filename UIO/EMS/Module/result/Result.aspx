<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_Result" Codebehind="Result.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Result</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }

        table, tr, th {
            border: 1px solid #008080;
        }

        .msgPanel {
            margin-top: 10px;
            border: 1px solid #008080;
            padding: 5px;
            width: 888px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div style="padding: 1px; width: 1100px;">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td>Trimester</td>
                            <td>
                                <asp:DropDownList ID="ddlAcaCalBatch" runat="server" Width="120px">
                                </asp:DropDownList></td>
                            <td>Program</td>
                            <td>
                                <asp:DropDownList ID="ddlProgram" runat="server" Width="130px"
                                    DataTextField="ShortName" DataValueField="ProgramID">
                                </asp:DropDownList></td>
                            <td>
                                <asp:Label ID="lbl" runat="server" Text="Student ID"></asp:Label>
                                <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                     <div id="divProgress" style="display:none ; width: 195px;   float:right; z-index:1000; margin-top:-35px">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                        <br />
                        Processing your request ...
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel3" runat="server">
                <Animations>
                    <OnUpdating>
                       <Parallel duration="0">
                            <ScriptAction Script="InProgress();" />
                            <EnableAction AnimationTarget="btnLoad" Enabled="false" />
                        </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
                        </Parallel>
                    </OnUpdated>
                </Animations>
            </ajaxToolkit:UpdatePanelAnimationExtender>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td>Update GPA</td>
                            <td>
                                <asp:Button runat="server" ID="btnUpdateGPA" Text="Update GPA" OnClick="btnUpdateGPA_Click" />
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div style="padding: 1px; margin-top: 20px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                     <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                        ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                        <HeaderStyle BackColor="SeaGreen" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align:center">
                                        <asp:CheckBox runat="server" ID="ChkActive"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>                          

                            <asp:TemplateField >
                                 <HeaderTemplate>
                                        <asp:Label ID="lbl_1" runat="server" Text="Completed Cr."></asp:Label>
                                        <br />
                                        <hr />
                                        <asp:Label ID="Label2" runat="server" Text="W + C + R = T"></asp:Label>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCompletedCr" Text='<%#Eval("CompletedCr") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Result History">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lBtnResultHistory" Text="View" OnClick="lBtnResultHistory_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
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