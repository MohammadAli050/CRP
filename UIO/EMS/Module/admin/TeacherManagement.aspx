<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits=" EMS.Module.admin.TeacherManagement" CodeBehind="TeacherManagement.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Teacher Profile Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .marginTop {
            margin-top: -5px;
        }

        .LoadStudentCourseHistory table, tr, th, td {
            /* border: 1px solid #008080; */
            padding: 0px !important;
        }

        .sweet-alert {
            z-index: 1000000 !important;
        }
    </style>

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
        $(document).ready(function () {

            var index1 = $('#ContentPlaceHolder_ddlExamTypeSecondary option:selected').val();
            if (index1 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPASecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearSecondary').attr('disabled', 'disabled');
            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPASecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearSecondary').removeAttr('disabled');
            }

            var index2 = $('#ContentPlaceHolder_ddlExamTypeHigherSecondary option:selected').val();
            if (index2 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearHigherSecondary').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearHigherSecondary').removeAttr('disabled');
            }

            var index3 = $('#ContentPlaceHolder_ddlExamTypeUndergraduate option:selected').val();
            if (index3 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearUndergraduate').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearUndergraduate').removeAttr('disabled');
            }

            var index4 = $('#ContentPlaceHolder_ddlExamTypeGraduate option:selected').val();
            if (index4 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearGraduate').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearGraduate').removeAttr('disabled');
            }
        });

        function ExamTypeSecondary() {
            var index1 = $('#ContentPlaceHolder_ddlExamTypeSecondary option:selected').val();
            if (index1 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPASecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearSecondary').attr('disabled', 'disabled');
            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPASecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearSecondary').removeAttr('disabled');
            }
        }

        function ExamTypeHigherSecondary() {
            var index2 = $('#ContentPlaceHolder_ddlExamTypeHigherSecondary option:selected').val();
            if (index2 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearHigherSecondary').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearHigherSecondary').removeAttr('disabled');
            }
        }

        function ExamTypeUndergraduate() {
            var index3 = $('#ContentPlaceHolder_ddlExamTypeUndergraduate option:selected').val();
            if (index3 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteUndergraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearUndergraduate').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteUndergraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearUndergraduate').removeAttr('disabled');
            }
        }

        function ExamTypeGraduate() {
            var index4 = $('#ContentPlaceHolder_ddlExamTypeGraduate option:selected').val();
            if (index4 == '0') {
                $('#ContentPlaceHolder_ddlResultTypeGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGW4SGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtInstituteGraduate').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtPassingYearGraduate').attr('disabled', 'disabled');

            }
            else {
                $('#ContentPlaceHolder_ddlResultTypeGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGW4SGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtInstituteGraduate').removeAttr('disabled');
                $('#ContentPlaceHolder_txtPassingYearGraduate').removeAttr('disabled');
            }
        }

        function ResultTypeSecondary() {
            var index = $('#ContentPlaceHolder_ddlResultTypeSecondary option:selected').val();
            if (index == '0') {
                $('#ContentPlaceHolder_txtGW4SSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPASecondary').removeAttr('disabled');
            }
            else {
                $('#ContentPlaceHolder_txtGW4SSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPASecondary').attr('disabled', 'disabled');
            }
        }
        function ResultTypeHigherSecondary() {
            var index = $('#ContentPlaceHolder_ddlResultTypeHigherSecondary option:selected').val();
            if (index == '0') {
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').removeAttr('disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').removeAttr('disabled');
            }
            else {
                $('#ContentPlaceHolder_txtGW4SHigherSecondary').attr('disabled', 'disabled');
                $('#ContentPlaceHolder_txtGPAHigherSecondary').attr('disabled', 'disabled');
            }
        }
        function ResultTypeUndergraduate() {
            var index = $('#ContentPlaceHolder_ddlResultTypeUndergraduate option:selected').val();
            if (index == '0') {
                $('#ContentPlaceHolder_txtGPAUndergraduate').removeAttr('disabled');
            }
            else {
                $('#ContentPlaceHolder_txtGPAUndergraduate').attr('disabled', 'disabled');
            }
        }
        function ResultTypeGraduate() {
            var index = $('#ContentPlaceHolder_ddlResultTypeGraduate option:selected').val();
            if (index == '0') {
                $('#ContentPlaceHolder_txtGPAGraduate').removeAttr('disabled');
            }
            else {
                $('#ContentPlaceHolder_txtGPAGraduate').attr('disabled', 'disabled');
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="PageTitle">
        <h4>Teacher Profile Management</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

    <div>


        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Teacher ID</b>
                                <asp:TextBox ID="txtSearchCode" runat="server" CssClass="form-control" Width="100%" />

                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <b>Teacher Name</b>
                                <asp:TextBox ID="txtSearchTeacherName" runat="server" CssClass="form-control" Width="100%" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button ID="btnLoad" runat="server" Text="Load" CssClass="btn btn-info form-control" OnClick="btnLoad_Click" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:Button ID="btnAddNew" Visible="false" runat="server" CssClass="btn btn-primary form-control" OnClick="btnAddNew_Click" Text="Add New"></asp:Button>

                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>


        <div class="card mt-5">
            <div class="card-body">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTeacherList" runat="server" AutoGenerateColumns="false" ShowFooter="false" Width="100%"
                            CssClass="gridview-container table-striped table-bordered" OnRowDataBound="gvTeacherList_RowDataBound">
                            <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                            <FooterStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <RowStyle Height="20px" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Photo">
                                    <ItemTemplate>
                                        <asp:Image ID="imgPhotoGrid" runat="server" Width="60px" Height="60px" Style="border: solid; border-color: black;border-width:.2px" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Roll">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lnkRoll" runat="server" ForeColor="White">Code</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnStudentID" Value='<%#Eval("EmployeeID") %>'></asp:HiddenField>
                                        <asp:HiddenField runat="server" ID="hdnPersonId" Value='<%#Eval("PersonId") %>'></asp:HiddenField>
                                        <asp:Label runat="server" ID="lblTeacherId" Text='<%#Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Font-Bold="true" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Institute">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInst" Font-Bold="true" Text='<%#Eval("institutionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Program">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%#Eval("programName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text='<%#Eval("StatusDetails") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Phone">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPhone" Text='<%#Eval("BasicInfo.SMSContactSelf") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("ContactDetails.EmailOffice") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Login ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblUserLogInId" Text='<%#Eval("UserLogInId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Signature">
                                    <ItemTemplate>
                                        <asp:Image ID="imgSignGrid" runat="server" Width="80px" Height="60px" Style="border: solid; border-color: black;border-width:.2px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton runat="server" ToolTip="Edit" Text="Edit" ID="lnkEdit"
                                                CommandArgument='<%#Eval("EmployeeID") %>'
                                                OnClick="lnkEdit_Click">
                                                    </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>

                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />

                        </asp:GridView>




                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none;" />
                        <ajaxToolkit:ModalPopupExtender
                            ID="ModalPopupExtender1"
                            runat="server"
                            TargetControlID="btnShowPopup"
                            PopupControlID="pnPopUp"
                            CancelControlID="btnClose"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel runat="server" ID="pnPopUp" Style="display: none;">
                            <div style="height: 550px; width: 1050px; margin: 5px; background-color: Window; overflow: scroll">
                                <fieldset style="padding: 0px 10px; margin: 5px; border-color: lightgreen;">
                                    <legend>Teacher Info</legend>
                                    <asp:HiddenField runat="server" ID="hfPersonID" Value="" />

                                    <div class="card" runat="server" id="divPhotoSignature">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6" style="text-align: center">
                                                    <b>Photo</b>
                                                    <br />
                                                    <asp:Image runat="server" ID="imgPhoto" Height="100" Width="100" />
                                                    <asp:HiddenField runat="server" ID="hfPhotoPath" Value="" />
                                                    <br />
                                                    <label class="text-danger">***Less than 200KB</label>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-8">
                                                            <asp:FileUpload ID="FileUploadPhoto" runat="server" accept=".jpeg,.jpg,.png" CssClass="btn btn-info  form-control" Width="100%" />
                                                        </div>
                                                        <div class="col-lg-4 col-sm-4 col-md-4">
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-success btn-sm  form-control" OnClick="btnUploadPhoto_Click" />
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6" style="border-left: solid; border-color: black; text-align: center">
                                                    <b>Signature</b>
                                                    <br />
                                                    <asp:Image runat="server" ID="imgSig" Height="100" Width="120" />
                                                    <asp:HiddenField runat="server" ID="hfSigPath" Value="" />
                                                    <br />
                                                    <label class="text-danger">***Less than 100KB</label>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-8 col-md-8 col-sm-8">
                                                            <asp:FileUpload ID="FileUploadSignature" runat="server" accept=".jpeg,.jpg,.png" CssClass="btn btn-info  form-control" Width="100%" />

                                                        </div>
                                                        <div class="col-lg-4 col-sm-4 col-md-4">
                                                            <asp:Button ID="btnUploadSig" runat="server" Text="Upload" CssClass="btn btn-success btn-sm form-control" OnClick="btnUploadSig_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card mt-1">
                                        <div class="card-body">

                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Full Name</b>
                                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Father's Name</b>
                                                    <asp:TextBox runat="server" ID="txtFatherName" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Mother's Name</b>
                                                    <asp:TextBox runat="server" ID="txtMotherName" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Program</b>
                                                    <asp:DropDownList runat="server" ID="ddlProgram" AutoPostBack="false" EnableViewState="true" CssClass="form-control" Width="100%"></asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>DOB</b>
                                                    <asp:TextBox runat="server" ID="txtDob" CssClass="form-control" Width="100%" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDob" Format="dd/MM/yyyy" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Date of Joining</b>
                                                    <asp:TextBox runat="server" ID="txtDoj" CssClass="form-control" Width="100%" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDoj" Format="dd/MM/yyyy" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Nationality</b>
                                                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" EnableViewState="true">
                                                        <asp:ListItem Value="0">Not Available</asp:ListItem>
                                                        <asp:ListItem Value="1">Bangladeshi</asp:ListItem>
                                                        <asp:ListItem Value="2">Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Gender</b>
                                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" EnableViewState="true"></asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Marital Status</b>
                                                    <asp:DropDownList ID="ddlMaritalStat" CssClass="form-control" Width="100%" runat="server" AutoPostBack="false" EnableViewState="true"></asp:DropDownList>

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Religion</b>
                                                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" EnableViewState="true"></asp:DropDownList>

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Employee Type</b>
                                                    <asp:DropDownList runat="server" ID="ddlEmployeeType" Enabled="false" CssClass="form-control" Width="100%"></asp:DropDownList>

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Status</b>
                                                    <asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="false" EnableViewState="true" CssClass="form-control" Width="100%"></asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Remarks</b>
                                                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Library Card No</b>
                                                    <asp:TextBox runat="server" ID="txtLibCard" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Teacher Code</b>
                                                    <asp:TextBox runat="server" ID="txtTeacherCode" Enabled="false" CssClass="form-control" Width="100%" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <br />
                                                    <asp:Button runat="server" ID="btnValidate" Enabled="false" Text="Validate" CssClass="btn btn-danger form-control" OnClick="btnValidate_Click" />
                                                    <asp:Label runat="server" ID="lblValidationStat" class="margin-zero" />
                                                    <asp:HiddenField ID="hfTeacherCodeChanged" Value="0_0" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row mt-2">

                                                <div class="col-lg-9 col-md-9 col-sm-9">
                                                    <b>Institute</b>
                                                    <asp:DropDownList runat="server" ID="ddlInstitute" AutoPostBack="false" EnableViewState="true" CssClass="form-control" Width="100%"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="card mt-1">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Mailing Address</b>
                                                    <asp:TextBox ID="txtMailingAddress" runat="server" CssClass="form-control" Width="100%" />
                                                    <asp:HiddenField ID="hdnMailing" Value="0_0" runat="server" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Mobile 1</b>
                                                    <asp:TextBox ID="txtMobile1" runat="server" CssClass="form-control" Width="100%" />
                                                    <asp:HiddenField ID="hdnContact" Value="0_0" runat="server" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Mobile2</b>
                                                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>SMS Contact No.</b>
                                                    <asp:TextBox ID="txtSMSContact" runat="server" CssClass="form-control" Width="100%" placeholder="+880XXXXXXXXXX" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                        ControlToValidate="txtSMSContact" ErrorMessage="Wrong Format"
                                                        Style="z-index: 101; left: 424px; position: absolute; top: 285px"
                                                        ValidationExpression="^(?:\+?88)?01[15-9]\d{8}$" ValidationGroup="check">
                                                        </asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Phone Residential</b>
                                                    <asp:TextBox ID="txtPhnRes" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Phone Office</b>
                                                    <asp:TextBox ID="txtPhnOff" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Phone Emergency</b>
                                                    <asp:TextBox ID="txtPhnEmergency" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Designation/Rank</b>
                                                    <asp:TextBox runat="server" ID="txtDesignationRank" CssClass="form-control" Width="100%" />

                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Email Personal</b>
                                                    <asp:TextBox ID="txtEmailPersonal" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Email Official</b>
                                                    <asp:TextBox ID="txtEmailOfficial" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <b>Email Other</b>
                                                    <asp:TextBox ID="txtEmailOther" runat="server" CssClass="form-control" Width="100%" />
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                </div>
                                            </div>

                                            <div class="row mt-2">
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <asp:Button runat="server" CssClass="form-control btn btn-success" Width="100%" ID="btnPopUpSave" Text="Save" ValidationGroup="check" OnClick="btnSave_Click" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <asp:Button runat="server" CssClass="form-control btn btn-danger" Width="100%" ID="btnCancel" Text="Cancel" />

                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div style="margin-top: 10px">
                                <asp:Button runat="server" ID="btnClose" Text="Cancel" Style="width: 150px; height: 30px;" Visible="false" Enabled="false" />
                            </div>
                        </asp:Panel>


                    </ContentTemplate>
                    <Triggers>

                        <asp:PostBackTrigger ControlID="btnUpload" />
                        <asp:PostBackTrigger ControlID="btnUploadSig" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>

        </div>
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

</asp:Content>
