<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ActiveInActiveForRegStudent" Codebehind="ActiveInActiveForRegStudent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Active
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <style>
        /*table {
            border-collapse: collapse;
        }*/

        /*table, tr, th {
            border: 1px solid #008080;
        }*/

        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            /*border: 1px solid #aaa;*/
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
    <div style="padding: 5px; width: 1100px;">
        <%-- <div class="PageTitle">
            <label>Student Active</label>
        </div>--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="studentProbationList-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Batch</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalBatch" DataValueField="AcademicCalenderID" DataTextField="FullCode" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title">Program</label>
                            <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ShortName" DataValueField="ProgramID" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title">Registration Session</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalSession" DataValueField="AcademicCalenderID" DataTextField="FullCode" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title">Student ID</label>
                            <asp:TextBox ID="txtRoll" runat="server" class="display-inline field-Title">  </asp:TextBox>
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Is Registered</label>
                            <asp:DropDownList ID="ddlRegistered" runat="server" Width="110px">
                                    <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Registered"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Not Registered"></asp:ListItem>
                                </asp:DropDownList>

                            <label class="display-inline field-Title">Is Active</label>
                            <asp:DropDownList ID="ddlActive" runat="server" Width="110px">
                                    <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Inactive"></asp:ListItem>
                                </asp:DropDownList>

                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                        </div>
                    </div>
                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -49px -150px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                    <%-- <div class="Message-Area">--%>
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <%--</div>--%>
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

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender2"
            TargetControlID="UpdatePanel2"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script = "InProgress();" />
                    <EnableAction AnimationTarget = "btnGenerate" 
                                  Enabled = "false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnGenerate" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 1100px; margin-top: 20px;">

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>

                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Registered">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsReg" runat="server" 
                                        ForeColor='<%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? System.Drawing.Color.Blue : System.Drawing.Color.Red %>'>
                                        <div align="center">
                                            <%# (Boolean.Parse(Eval("IsRegistered").ToString())) ? "● Yes" : "● No" %>
                                        </div>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Button ID="btnActive" OnClick="btnActive_Click" runat="server"
                                        Text="Active" />
                                    <br />
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkActive" Checked='<%#Eval("IsActive") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRemarks" Font-Bold="true" Text='<%#Eval("Remarks") %>' Width="440"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="450px" />
                            </asp:TemplateField>

                        </Columns>

                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both"></div>
        <div style="height: 30px; width: 900px; padding: 15px;"></div>
    </div>
</asp:Content>

