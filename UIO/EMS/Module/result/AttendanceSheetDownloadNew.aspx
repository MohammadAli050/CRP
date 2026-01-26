<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="AttendanceSheetDownloadNew.aspx.cs" Inherits="EMS.Module.result.AttendanceSheetDownloadNew" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Attendance Sheet :: Download</asp:Content>

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

        function CheckDropdown() {
            $('#ctl00_MainContainer_lblMsg').text('');

            if ($('#ctl00_MainContainer_ddlAcaCalBatch').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Semester');
                return false;
            }
            if ($('#ctl00_MainContainer_ddlProgram').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Program');
                return false;
            }
            if ($('#ctl00_MainContainer_ddlAcaCalSection').val() == '0') {
                $('#ctl00_MainContainer_lblMsg').text('Please select Course');
                return false;
            }

            return true;
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
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="PageTitle">
        <label>Attendance Sheet :: Download </label>
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
                    <script type="text/javascript">
                        Sys.Application.add_load(initdropdown);
                    </script>
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
                            <asp:DropDownList ID="ddlCourse" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <br />
                            <asp:Button runat="server" ID="btnGenerateAttendanceSheet" Text="Attendance Sheet Generate" OnClick="btnGenerateAttendanceSheet_Click" OnClientClick="jsShowHideProgress();" CssClass="btn btn-info form-control" />

                        </div>
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
