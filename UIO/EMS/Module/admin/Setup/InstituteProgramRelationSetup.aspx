<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="InstituteProgramRelationSetup.aspx.cs" Inherits="InstituteProgramRelationSetup" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Institute, Faculty, Department and Program Setup
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
            document.getElementById("blurOverlay").style.display = "block";
        }
        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
            var overlay = document.getElementById("blurOverlay");
            if (overlay) overlay.style.display = "none";
        }
        function initdropdown() {
            $('.use-select2').select2({ allowClear: true });
        }
    </script>
    <style>
        @media (max-width: 576px) { .PageTitle h4 { font-size: 1.2rem; } .card-body { padding: 0.5rem; } }
        .card { margin-top: 5px; }
        .modal-row { margin-top: 10px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <h4>Institute, Faculty, Department and Program Setup</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;"></div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script type="text/javascript">Sys.Application.add_load(initdropdown);</script>
            
            <%-- INSTITUTE SECTION --%>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control use-select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <br /><asp:Button ID="btnInstituteAdd" Width="100%" CssClass="btn btn-success" runat="server" Text="Add Institute" OnClick="btnInstituteAdd_Click" />
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 250px; margin-top: 5px">
                        <asp:GridView ID="gvInstitute" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInstitute_RowDataBound" CssClass="table table-custom table-hover align-middle shadow-sm" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="SL"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Institute">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center">
                                            <asp:Image ID="lblLogo" runat="server" CssClass="rounded-circle border" Width="45px" Height="45px" />
                                            <div class="ms-2">
                                                <div class="fw-bold text-dark"><%# Eval("InstituteName") %></div>
                                                <small class="text-primary fw-semibold"><%# Eval("InstituteCode") %></small>
                                            </div>
                                        </div>
                                        <asp:Label runat="server" ID="lblLogoByte" Text='<%#Eval("LogoBytes") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" CommandArgument='<%# Eval("InstituteId") %>' CssClass="btn btn-light btn-sm text-primary border"><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("InstituteId") %>' OnClientClick="return confirm('Delete?');" CssClass="btn btn-light btn-sm text-danger border"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%-- FACULTY & DEPT SECTIONS (Combined for space) --%>
            <div class="row">
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                            <b>Faculty</b>
                            <div class="d-flex gap-2">
                                <asp:DropDownList ID="ddlFaculty" CssClass="form-control use-select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Button ID="btnAddFaculty" CssClass="btn btn-success" runat="server" Text="Add" OnClick="btnAddFaculty_Click" />
                            </div>
                            <div style="overflow-y: scroll; height: 150px; margin-top: 5px">
                                <asp:GridView ID="gvFaculty" runat="server" AutoGenerateColumns="false" CssClass="table table-sm table-hover" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEditFaculty" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="lbEditFaculty_Click"><i class="fas fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                            <b>Department</b>
                            <div class="d-flex gap-2">
                                <asp:DropDownList ID="ddlDeptSearch" CssClass="form-control use-select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptSearch_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Button ID="btnAddDept" CssClass="btn btn-success" runat="server" Text="Add" OnClick="btnAddDept_Click" />
                            </div>
                            <div style="overflow-y: scroll; height: 150px; margin-top: 5px">
                                <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false" CssClass="table table-sm table-hover" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Dept Name" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEditDept" runat="server" CommandArgument='<%#Eval("DeptID")%>' OnClick="lbEditDept_Click"><i class="fas fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- PROGRAM SECTION --%>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <b>Program</b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control use-select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <br /><asp:Button ID="btnAddProgram" Width="100%" CssClass="btn btn-success" runat="server" Text="Add Program" OnClick="btnAddProgram_Click" />
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 300px; margin-top: 5px">
                        <asp:GridView ID="gvProgram" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-custom shadow-sm" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Institute & Info">
                                    <ItemTemplate>
                                        <div class="fw-bold text-dark">Faculty: <%# Eval("Attribute1") %> <br />  Dept: <%# Eval("Attribute2") %></div> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program Details">
                                    <ItemTemplate>
                                        <div class="fw-bold text-primary"><%# Eval("ShortName") %> (Code: <%# Eval("Code") %>)</div>
                                        <div class="small text-secondary"><%# Eval("DegreeName") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%#Eval("ProgramID")%>' OnClick="lbEdit_Click" CssClass="btn btn-outline-primary btn-sm"><i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%#Eval("ProgramID")%>' OnClick="lbDelete_Click" OnClientClick="return confirm('Delete?');" CssClass="btn btn-outline-danger btn-sm"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- MODALS --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Program Modal Updated --%>
            <asp:Button ID="Button2" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupProgramAdd" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="btnPrgCancel" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="Panel2" runat="server" BackColor="White" Width="60%" Style="display: none; padding:20px; border-radius:10px; max-height: 90vh; overflow-y: auto;">
                <asp:HiddenField ID="hdnProgramId" runat="server" />
                <h5 class="mb-3">Program Setup</h5>
                <div class="row">
                    <div class="col-md-6">
                        <b>Institute</b>
                        <asp:DropDownList runat="server" ID="ddlprgInst" CssClass="form-control use-select2" />
                    </div>
                    <div class="col-md-6">
                        <b>Program Type*</b>
                        <asp:DropDownList runat="server" ID="ddlProgramType" CssClass="form-control use-select2" />
                    </div>
                </div>
                <div class="row modal-row">
                    <div class="col-md-6">
                        <b>Faculty*</b>
                        <asp:DropDownList runat="server" ID="ddlPrgFaculty" CssClass="form-control use-select2" />
                    </div>
                    <div class="col-md-6">
                        <b>Department*</b>
                        <asp:DropDownList runat="server" ID="ddlPrgDept" CssClass="form-control use-select2" />
                    </div>
                </div>
                <div class="row modal-row">
                    <div class="col-md-6">
                        <b>Calendar Type*</b>
                        <asp:DropDownList runat="server" ID="ddlCalender" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <b>Program Name*</b>
                        <asp:TextBox runat="server" ID="TextBoxProgramName" CssClass="form-control" />
                    </div>
                </div>
                <div class="row modal-row">
                    <div class="col-md-12">
                        <b>Detailed Name*</b>
                        <asp:TextBox runat="server" ID="TextBoxDetailedName" CssClass="form-control" />
                    </div>
                </div>
                <div class="row modal-row">
                    <div class="col-md-6">
                        <b>Code*</b>
                        <asp:TextBox runat="server" ID="TextBoxCode" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <b>Short Name*</b>
                        <asp:TextBox runat="server" ID="TextBoxShortName" CssClass="form-control" />
                    </div>
                </div>
                <div class="row modal-row">
                    <div class="col-md-6">
                        <b>Total Credit</b>
                        <asp:TextBox runat="server" ID="TextBoxTotalCredit" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-6">
                        <b>Duration</b>
                        <asp:TextBox runat="server" ID="TextBoxDuration" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-3"><asp:Button ID="btnProgramAdd" runat="server" OnClick="btnProgramAdd_Click" Text="Save" CssClass="btn btn-success w-100" /></div>
                    <div class="col-md-3"><asp:Button ID="btnPrgCancel" runat="server" Text="Cancel" CssClass="btn btn-danger w-100" /></div>
                </div>
            </asp:Panel>

            <%-- Other Modals (Institute, Faculty, Dept) remain same as your previous code --%>
             <asp:Button ID="Button1" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupInstituteAdd" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="btnInsCancel" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="Panel1" runat="server" BackColor="White" Width="50%" Style="display: none; padding:15px; border-radius:10px;">
                <asp:HiddenField ID="hdnInstituteId" runat="server" />
                <div class="row">
                    <div class="col-12"><b>Name*</b><asp:TextBox ID="txtInstituteName" CssClass="form-control" runat="server"></asp:TextBox></div>
                    <div class="col-12"><b>Code*</b><asp:TextBox ID="txtInstituteCode" CssClass="form-control" runat="server"></asp:TextBox></div>
                    <div class="col-12"><b>Address*</b><asp:TextBox ID="txtInstituteAddress" CssClass="form-control" runat="server"></asp:TextBox></div>
                    <div class="col-12"><b>Logo</b><asp:FileUpload ID="fuLogo" runat="server" CssClass="form-control" accept=".jpg,.png" /></div>
                    <div class="col-12"><b>Active</b><br /><asp:CheckBox ID="chkIsActive" runat="server" Checked="true" /></div>
                </div>
                <div class="row mt-3">
                    <div class="col-2"><asp:Button ID="btnInstituteAddNext" runat="server" CssClass="btn btn-success w-100" OnClick="btnInstituteAddNext_Click" Text="Save" /></div>
                    <div class="col-2"><asp:Button ID="btnInsCancel" runat="server" CssClass="btn btn-danger w-100" Text="Cancel" /></div>
                </div>
            </asp:Panel>

            <asp:Button ID="Button3" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupFacultyAdd" runat="server" TargetControlID="Button3" PopupControlID="PanelFaculty" CancelControlID="btnFacultyCancel" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="PanelFaculty" runat="server" BackColor="White" Width="50%" Style="display: none; padding:15px; border-radius:10px;">
                <asp:HiddenField ID="hdnFacultyId" runat="server" />
                <b>Faculty Name*</b><asp:TextBox ID="txtFacultyName" CssClass="form-control" runat="server"></asp:TextBox>
                <div class="row mt-3">
                    <div class="col-2"><asp:Button ID="btnFacultySave" runat="server" CssClass="btn btn-success w-100" OnClick="btnFacultySave_Click" Text="Save" /></div>
                    <div class="col-2"><asp:Button ID="btnFacultyCancel" runat="server" CssClass="btn btn-danger w-100" Text="Cancel" /></div>
                </div>
            </asp:Panel>

            <asp:Button ID="btnDeptModal" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupDept" runat="server" TargetControlID="btnDeptModal" PopupControlID="PanelDept" CancelControlID="btnDeptCancel" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="PanelDept" runat="server" BackColor="White" Width="50%" Style="display: none; padding:15px; border-radius:10px;">
                <asp:HiddenField ID="hdnDeptId" runat="server" />
                <div class="row">
                    <div class="col-12"><b>Code*</b><asp:TextBox ID="txtDeptCode" CssClass="form-control" runat="server"></asp:TextBox></div>
                    <div class="col-12"><b>Name*</b><asp:TextBox ID="txtDeptName" CssClass="form-control" runat="server"></asp:TextBox></div>
                    <div class="col-12"><b>Detailed Name*</b><asp:TextBox ID="txtDeptDetailed" CssClass="form-control" runat="server"></asp:TextBox></div>
                </div>
                <div class="row mt-3">
                    <div class="col-2"><asp:Button ID="btnDeptSave" runat="server" CssClass="btn btn-success w-100" OnClick="btnDeptSave_Click" Text="Save" /></div>
                    <div class="col-2"><asp:Button ID="btnDeptCancel" runat="server" CssClass="btn btn-danger w-100" Text="Cancel" /></div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnInstituteAddNext" />
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdAni" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating><ScriptAction Script="InProgress();" /></OnUpdating>
            <OnUpdated><ScriptAction Script="onComplete();" /></OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>