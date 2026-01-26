<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="ExamMarksPublishAndUnsubmit.aspx.cs" Inherits="EMS.Module.result.ExamMarksPublishAndUnsubmit" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Exam Marks Publish And Un-submit</asp:Content>

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




        function confirmUnSubmit(courseId, versionId) {
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
        <label>Exam Marks Publish And Un-submit</label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

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
                            <asp:DropDownList ID="ddlCourse" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Status</b>
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="-1">All</asp:ListItem>
                                <asp:ListItem Value="1">Submitted</asp:ListItem>
                                <asp:ListItem Value="2">Not Submit</asp:ListItem>
                                <asp:ListItem Value="3">Published</asp:ListItem>
                                <asp:ListItem Value="4">Not Publish</asp:ListItem>

                            </asp:DropDownList>

                        </div>
                    </div>
                </div>
            </div>


            <!-- Grid Section -->
            <div class="card mt-5">
                <div class="card-body">

                    <h5 class="mb-3 text-primary fw-bold">
                        <asp:Label runat="server" ID="lblSummary" CssClass="ms-2" /></h5>

                    <asp:GridView ID="gvCourseList" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="gridview-container table-striped table-bordered" ShowHeader="false" OnRowDataBound="gvCourseList_RowDataBound"
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

                                                    <asp:Button runat="server" ID="btnResultPublish"
                                                        Text='<%# Eval("FinalSubmitted").ToString()=="Yes" && Eval("Published").ToString()=="Yes" ? "Re-Publish" : Eval("FinalSubmitted").ToString()=="Yes" && Eval("Published").ToString()=="No" ? "Publish":"" %>'
                                                        OnClick="btnResultPublish_Click"
                                                        Visible='<%# Eval("FinalSubmitted").ToString()=="Yes" ? true : false %>'
                                                        CommandArgument='<%#Eval("CourseId")+"_"+Eval("VersionId") %>'
                                                        OnClientClick="jsShowHideProgress();" CssClass="btn btn-info form-control btn-sm" />

                                                    <asp:Button runat="server" ID="btnUnSubmit" Style="margin-top: 2px" Text="Marks Un-Submit" Visible='<%#Eval("FinalSubmitted").ToString()=="Yes" ? true : false %>'
                                                        OnClientClick='<%# "return confirmUnSubmit(\"" + Eval("CourseId") + "\", \"" + Eval("VersionId") + "\"); return false;" %>'
                                                        CssClass="btn btn-danger form-control btn-sm" />

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



        </ContentTemplate>
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
                    <p style="font-size: 16px; color: #545454;">Do you want to un-submit the marks?</p>
                    <p style="font-size: 16px; color: red;">After the un-submission you can enter the marks</p>
                    <!-- Buttons -->
                    <div style="display: flex; gap: 10px; justify-content: center;">
                        <asp:Button ID="btnsubmitConfirm" runat="server" OnClick="btnResultUnsubmit_Click" Style="background-color: #3085d6; color: #fff; border: none; padding: 12px 30px; font-size: 16px; font-weight: 500; border-radius: 4px; cursor: pointer; min-width: 120px;" Text="Yes, Un-submit it!"></asp:Button>
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
