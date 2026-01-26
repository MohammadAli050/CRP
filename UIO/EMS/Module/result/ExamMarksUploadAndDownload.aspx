<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="ExamMarksUploadAndDownload.aspx.cs" Inherits="EMS.Module.result.ExamMarksUploadAndDownload" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Exam Marks Upload And Download</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

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

        function toggleAll(source) {
            var checkboxes = document.querySelectorAll('[id$=chkSelect]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
        }

        function jsShowHideProgress() {
            setTimeout(function () {
                document.getElementById('divProgress').style.display = 'block';
                document.getElementById("blurOverlay").style.display = "block";
            }, 200);
            deleteCookie();

            var timeInterval = 500; // milliseconds (checks the cookie for every half second )

            var loop = setInterval(function () {
                if (IsCookieValid()) {
                    document.getElementById('divProgress').style.display =
                        'none';
                    document.getElementById("blurOverlay").style.display = "none";
                    clearInterval(loop)
                }

            }, timeInterval);
        }
        // cookies
        function deleteCookie() {
            var cook = getCookie('ExcelDownloadFlag');
            if (cook != "") {
                document.cookie = "ExcelDownloadFlag=;Path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC";
            }
        }

        function IsCookieValid() {
            var cook = getCookie('ExcelDownloadFlag');
            return cook != '';
        }

        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        function initdropdown() {
            $('#ctl00_MainContainer_ddlInstitute').select2({
                allowClear: true
            });

            $('#ctl00_MainContainer_ddlCourse').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlExamTemplateName').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlUploaderOne').select2({
                allowClear: true
            });
            $('#ctl00_MainContainer_ddlUploaderTwo').select2({
                allowClear: true
            });


        }

        /// Excel File Upload Validation
        function validateExcelFileUpload(input) {
            var file = input.files[0];

            var lbl = document.getElementById('<%= lblUploadMessage.ClientID %>');
            if (lbl) {
                lbl.innerText = '';
            }

            if (!file) return;

            var allowedExtensions = [".xlsx", ".xls", ".csv"];
            var fileName = file.name;
            var fileSize = file.size;
            var ext = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();

            if (allowedExtensions.indexOf(ext) === -1) {
                alert("Invalid file type. Only .xlsx, .xls, .csv files are allowed.");
                input.value = "";
                return false;
            }
            if (fileSize > 5 * 1024 * 1024) {
                alert("File size exceeds 5 MB limit.");
                input.value = "";
                return false;
            }
            return true;
        }
        function showFileName(input) {
            var fileName = input.files && input.files.length > 0 ? input.files[0].name : '';
            document.getElementById('fuExcelFileName').textContent = fileName;
            document.getElementById('fuExcelLabelText').textContent = fileName ? 'Selected:' : 'Choose Excel file...';
        }

        function confirmSubmit(courseId, versionId) {
            event.preventDefault();
            var hiddenField = document.getElementById('<%= hdnFinalSubmitValue.ClientID %>');
            hiddenField.value = "0_0_0";

            var selectedValue = $("#ctl00_MainContainer_ucSession_ddlSession").val();

            var acacalId = document.getElementById
            hiddenField.value = courseId + "_" + versionId + "_" + selectedValue;

            var modal = document.getElementById('customModal');
            modal.style.display = 'flex';
        }

    </script>

    <style type="text/css">
        .auto-style3 {
            width: 100px;
        }

        table {
            border-collapse: collapse;
        }

        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }


        @media (max-width: 576px) {
            .PageTitle h4 {
                font-size: 1.2rem;
            }

            .card-body {
                padding: 0.5rem;
            }

            .alert {
                font-size: 0.95rem;
            }
        }
    </style>

    <%--File Uplaod Css--%>
    <style type="text/css">
        /* Professional style for file upload */
        .file-upload-wrapper {
            position: relative;
            width: 100%;
            /*max-width: 350px;*/
            margin: 10px 0;
        }

        .file-upload-input {
            width: 100%;
            height: 45px;
            opacity: 0;
            position: absolute;
            left: 0;
            top: 0;
            cursor: pointer;
            z-index: 2;
        }

        .file-upload-label {
            display: flex;
            align-items: center;
            justify-content: left;
            background: #f5f5f5;
            border: 2px solid #3498db;
            color: #333;
            border-radius: 6px;
            padding: 10px 16px;
            font-size: 16px;
            font-weight: 500;
            cursor: pointer;
            transition: border-color 0.2s, background 0.2s;
            min-height: 45px;
            z-index: 1;
        }

            .file-upload-label:hover {
                border-color: #217dbb;
                background: #eaf6ff;
            }

            .file-upload-label i {
                color: #3498db;
                margin-right: 10px;
                font-size: 20px;
            }

        .file-upload-filename {
            margin-left: 10px;
            color: #555;
            font-size: 15px;
            font-style: italic;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            max-width: 180px;
        }

        .sweet-alert {
            z-index: 1000000 !important;
            background-color: lightgray !important;
        }
    </style>


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="PageTitle">
        <label>Exam Marks Upload And Download</label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(initdropdown);
            </script>
            <div class="card">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Course <span class="text-danger">*</span></b>
                            <asp:DropDownList ID="ddlCourse" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Grid Section -->
            <div class="card mt-5">
                <div class="card-body">
                    <asp:GridView ID="gvCourseList" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="gridview-container table-striped table-bordered" ShowHeader="false"
                        EmptyDataText="No data found." GridLines="None">

                        <AlternatingRowStyle BackColor="#f9f9f9" />
                        <Columns>

                            <asp:TemplateField>
                                <ItemTemplate>

                                    <div class="card">
                                        <div class="card-body">
                                            <div class="row align-items-center">
                                                <div class="col-lg-1 col-md-1 col-sm-1" style="vertical-align: middle">
                                                    <b><%# Container.DataItemIndex + 1 %></b>
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-sm-4">
                                                    <asp:Label runat="server" ID="lblCourse"
                                                        Text='<%#Eval("FormalCode")+"<br />"+Eval("CourseTitle")+"<br />"+Eval("Credit") %>'></asp:Label>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-2">
                                                    <asp:Label runat="server" ID="lblStatus"
                                                        Text='<%# "Student : "+Eval("StudentCount")+"<br />"+"Submitted : "+Eval("FinalSubmitted") +"<br />"+"Published : "+Eval("Published") %>'></asp:Label>
                                                </div>

                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                    <asp:Label runat="server" ID="lblTemplate"
                                                        Text='<%#Eval("TemplateName") %>'></asp:Label>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-2">

                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="btnDownload" Text="Download" CommandArgument='<%#Eval("CourseId")+"_"+Eval("VersionId") %>'
                                                                OnClick="btnDownloadExcel_Click" OnClientClick="jsShowHideProgress();" CssClass="btn btn-info form-control btn-sm" />
                                                            <asp:Button runat="server" ID="btnUpload" Style="margin-top: 2px" Text="Upload" OnClick="btnUpload_Click" Visible='<%#Eval("FinalSubmitted").ToString()=="Yes" ? false : true %>'
                                                                CommandArgument='<%#Eval("CourseId")+"_"+Eval("VersionId") %>' CssClass="btn btn-success form-control btn-sm" />
                                                            <asp:Button runat="server" ID="btnSubmit" Style="margin-top: 2px" Text="Final Submit" Visible='<%#Eval("FinalSubmitted").ToString()=="Yes" ? false : true %>'
                                                                OnClientClick='<%# "return confirmSubmit(\"" + Eval("CourseId") + "\", \"" + Eval("VersionId") + "\"); return false;" %>'
                                                                CssClass="btn btn-danger form-control btn-sm" />

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="modalPopupUploadExcel" runat="server" TargetControlID="Button1" PopupControlID="Panel2"
                BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="Panel2" runat="server" CssClass="card shadow p-4" Style="display: none; width: 100%; max-width: 80%; height: 500px; margin: auto; overflow: scroll">
                <h5 class="mb-3 text-primary fw-bold">Exam Marks Excel File Upload</h5>
                <div>
                    <asp:Label runat="server" ID="lblCourseInformation" ForeColor="Crimson" CssClass="ms-2" />
                    <asp:HiddenField ID="hdnCourseInformation" runat="server" />
                </div>

                <div class="row" style="margin-left: 2px">
                    <div class="file-upload-wrapper">
                        <asp:FileUpload ID="fuExcel" runat="server" CssClass="file-upload-input" accept=".xlsx,.xls,.csv" onchange="validateExcelFileUpload(this); showFileName(this);" />
                        <label for="fuExcel" class="file-upload-label">
                            <i class="fa fa-upload"></i>
                            <span id="fuExcelLabelText">Choose Excel file (Max 5 MB)</span>
                            <span class="file-upload-filename" id="fuExcelFileName"></span>
                        </label>
                    </div>


                </div>

                <div class="row mt-2">
                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <asp:Button ID="btnExcelUpload" runat="server" Text="Upload Excel" OnClick="btnExcelUpload_Click" CssClass="btn btn-primary" OnClientClick="this.value = 'Processing....'; this.disabled = true;" UseSubmitBehavior="false" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    </div>
                </div>
                <asp:Label ID="lblUploadMessage" runat="server" ForeColor="Red" />

                <div class="card mt-5" runat="server" id="divGrid">
                    <div class="card-body">

                        <div class="row" style="padding: 2px">
                            <div class="col-lg-10 col-md-10 col-sm-10">
                                <asp:Label ID="lblStudentSummary" runat="server" Font-Size="12px" Font-Italic="true" Font-Bold="true" ForeColor="Crimson"></asp:Label>

                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2" runat="server" id="divSave">
                                <asp:Button ID="btnSaveUploadedMark" runat="server" Text="Save Marks" CssClass="btn btn-info" OnClick="btnSaveUploadedMark_Click" OnClientClick="jsShowHideProgress();" />
                                <%--OnClick="btnSaveUploadedMark_Click"--%>
                            </div>
                        </div>


                        <!-- Add this GridView below the existing GvExamMarkSubmit GridView in the marks entry student list section -->
                        <div class="table-container" style="width: 100%; margin-top: 10px;">
                            <asp:GridView ID="gvStudentList" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="SL,HistoryId,TemplateId,TemplateName,TemplateMarks,StudentName,StudentRoll,Marks,PresentStatus,Status,ValidMarks" ShowFooter="false" OnRowDataBound="gvStudentList_RowDataBound"
                                CssClass="gridview-container table-striped table-bordered" ShowHeader="true" Width="100%">
                                <HeaderStyle BackColor="#0D2D62" ForeColor="White" Height="25px" />
                                <RowStyle BackColor="#ecf0f0" />
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Student ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUploadRoll" runat="server" Text='<%# Eval("StudentRoll") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Template">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTemplate" runat="server" Text='<%# Eval("TemplateName")+" (" +Eval("TemplateMarks")+")" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Marks">
                                        <ItemTemplate>
                                            <div style="text-align: center">

                                                <asp:Label ID="lblHistoryId" Visible="false" runat="server" Text='<%# Eval("HistoryId") %>'></asp:Label>
                                                <asp:Label ID="lblUploadMarks" runat="server" Text='<%# Eval("Marks") %>'
                                                    ForeColor='<%# Eval("Marks").ToString()!="" ? Eval("Marks").ToString().ToLower()=="ab" || Eval("Marks").ToString().ToLower()=="abs" ? System.Drawing.Color.Red : System.Drawing.Color.Black : System.Drawing.Color.Black  %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Present/Absent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUploadStatus" runat="server" Text='<%# Convert.ToInt32(Eval("PresentStatus"))==1 ? "Present" : Convert.ToInt32(Eval("PresentStatus"))==2 ? "Absent" : ""  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Student Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStudentStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="text-align: center; color: gray;">No students uploaded.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>


                        <div class="row" style="padding: 2px">
                            <div class="col-lg-10 col-md-10 col-sm-10">
                                <asp:Label ID="lblStudentSummary2" runat="server" Font-Size="12px" Font-Italic="true" Font-Bold="true" ForeColor="Crimson"></asp:Label>

                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2" runat="server" id="divSave2">
                                <asp:Button ID="btnSaveUploadedMarkFooter" runat="server" Text="Save Marks" CssClass="btn btn-info" OnClick="btnSaveUploadedMark_Click" OnClientClick="jsShowHideProgress();" />
                            </div>
                        </div>



                        <div class="row" style="margin-top: 10px">
                            <div class="col-lg-4 col-md-4 col-sm-6">
                                <asp:Button ID="Button11" runat="server" Text="Close" CssClass="btn btn-danger" Visible="false" />
                                <asp:Button ID="Button13" runat="server" Text="Close" CssClass="btn btn-danger" />
                                <%--OnClick="Button12_Click"--%>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6">
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6">
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelUpload" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <!-- Add this HTML to your page (hidden by default) -->
            <div id="customModal" class="custom-modal-overlay" style="display: none; position: fixed; top: 0; left: 0; right: 0; bottom: 0; background-color: rgba(0, 0, 0, 0.4); align-items: center; justify-content: center; z-index: 9999;">
                <div class="custom-modal" style="background: #fff; border-radius: 8px; box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2); padding: 40px 30px 30px; max-width: 500px; width: 90%; text-align: center;">
                    <asp:HiddenField ID="hdnFinalSubmitValue" runat="server" />
                    <!-- Warning Icon -->
                    <div style="width: 80px; height: 80px; margin: 0 auto 20px; border: 4px solid #f8bb86; border-radius: 50%; display: flex; align-items: center; justify-content: center;">
                        <span style="font-size: 50px; color: #f8bb86; font-weight: bold;">!</span>
                    </div>

                    <!-- Title -->
                    <h2 style="font-size: 28px; font-weight: 600; color: #595959; margin: 0 0 15px;">Are you sure?</h2>

                    <!-- Message -->
                    <p style="font-size: 16px; color: #545454;">Do you want to submit the marks?</p>
                    <p style="font-size: 16px; color: red;">After the final submission you can not change the marks</p>
                    <!-- Buttons -->
                    <div style="display: flex; gap: 10px; justify-content: center;">
                        <asp:Button ID="btnsubmitConfirm" runat="server" OnClick="btnsubmitConfirm_Click" Style="background-color: #3085d6; color: #fff; border: none; padding: 12px 30px; font-size: 16px; font-weight: 500; border-radius: 4px; cursor: pointer; min-width: 120px;" Text="Yes, submit it!"></asp:Button>
                        <button id="btnCancelconfirmation" style="background-color: #d33; color: #fff; border: none; padding: 12px 30px; font-size: 16px; font-weight: 500; border-radius: 4px; cursor: pointer; min-width: 120px;">
                            No, cancel
                        </button>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnSearch" Enabled="false" />                   
                    </Parallel>
                </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnSearch" Enabled="true" />
                        </Parallel>
                </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>
