<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="UserControl.aspx.cs" Inherits="EMS.miu.admin.UserControl" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/InstitutionUserControl.ascx" TagPrefix="uc1" TagName="InstitutionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">User Control</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
            document.getElementById("blurOverlay").style.display = "block";
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
            document.getElementById("blurOverlay").style.display = "none";
        }

    </script>

    <style type="text/css">
        #ctl00_MainContainer_ucAccessableProgram_ddlProgram {
            width: 100% !important;
            height: 38px !important;
            display: block;
            width: 100%;
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: var(--bs-body-color);
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            background-color: var(--bs-body-bg);
            background-clip: padding-box;
            border: var(--bs-border-width) solid var(--bs-border-color);
            border-radius: var(--bs-border-radius);
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">


    <div class="PageTitle">
        <h4>User Control</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>

                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>User Type</b>
                                <asp:DropDownList runat="server" ID="ddlUserType" CssClass="form-control" Width="100%" OnSelectedIndexChanged="onUserTypeSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4">
                                <b>User Name</b>
                                <asp:DropDownList runat="server" ID="ddlTeacher" CssClass="form-control" Width="100%" OnSelectedIndexChanged="onTeacherSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnLoadTeacher" CssClass="btn btn-info" Width="100%" OnClick="btnLoadTeacherClick" Text="Load" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Search Text</b>
                                <asp:TextBox runat="server" ID="txtNameSearch" Width="100%" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-dark" Width="100%" OnClick="btnSearchClick" />
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Role</b>
                                <asp:DropDownList runat="server" ID="ddlRole" Width="100%" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Login ID</b>
                                <asp:TextBox runat="server" ID="txtUserId" Width="100%" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnValidate" Text="Validate" Width="100%" CssClass="btn btn-primary form-control" OnClick="btnValidateClick" />
                                <asp:Label runat="server" ID="lblValidate"></asp:Label>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Password</b>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" Width="100%" CssClass="form-control" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnSendPassword" Text="Send by Email" OnClick="btnSendPassword_Click" Width="100%" CssClass="btn btn-warning form-control" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />

                                <asp:CheckBox runat="server" ID="chkAllCourse" Text="Access All Course" />
                                <br />
                                <asp:CheckBox runat="server" ID="chkIsActive" Text="Active User"></asp:CheckBox>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Valid From</b>
                                <asp:TextBox runat="server" ID="txtStartDate" Width="100%" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="validfrom" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Valid Till</b>
                                <asp:TextBox runat="server" ID="txtEndDate" Width="100%" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="validto" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Program Access</b>
                                <br />
                                <uc1:ProgramUserControl runat="server" ID="ucAccessableProgram" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAddClicked" Width="100%" CssClass="btn btn-dark form-control" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnAllProg" Text="All Program" OnClick="btnAllProgClicked" Width="100%" CssClass="btn btn-dark form-control" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button runat="server" ID="btnClearProg" Text="Clear All" OnClick="btnClearProgClicked" Width="100%" CssClass="form-control" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2" runat="server" id="divInst" visible="false">
                                <b>Exam Center Access</b>
                                <asp:DropDownList runat="server" ID="ddlExamCenter"></asp:DropDownList>
                                <asp:Button runat="server" ID="btnAddInstitution" Text="Add" OnClick="btnAddInstitutionClicked" />
                                <asp:GridView ID="gvUserInstitution" AllowSorting="true" runat="server" AutoGenerateColumns="false" Width="500px">
                                    <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                                    <FooterStyle BackColor="#CC9966" ForeColor="White" Height="30" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor=" #F9F99F" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Exam Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblExamCenter" Font-Bold="True" Text='<%#Eval("ExamCenterName") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:LinkButton runat="server" ToolTip="Delete" ID="lnkDelete" CommandArgument='<%#Eval("Id")%>' OnClick="lnkDelete_Click">
                                                    <span class="action-container">Delete</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px">
                            <div class="col-lg-5 col-md-5 col-sm-5">
                                <br />
                                <asp:CheckBoxList runat="server" ID="chkAccessList">
                                </asp:CheckBoxList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100%" CssClass="form-control btn btn-success" OnClick="btnSaveClicked" OnClientClick="return confirm('Are you sure want save this user information?');" />

                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>

                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>


    </div>


    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
         <OnUpdating>
             <Parallel duration="0">
                 <ScriptAction Script="InProgress();" />
                 <EnableAction AnimationTarget="btnInsCancel" Enabled="false" />                   
             </Parallel>
         </OnUpdating>
             <OnUpdated>
                 <Parallel duration="0">
                     <ScriptAction Script="onComplete();" />
                     <EnableAction   AnimationTarget="btnInsCancel" Enabled="true" />
                 </Parallel>
         </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>

