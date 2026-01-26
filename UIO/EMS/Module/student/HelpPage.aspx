<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="HelpPage.aspx.cs" Inherits="HelpPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Help</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style>
        .btn {
            border: none;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }

        .success {
            background-color: #9acd32;
        }

            .success:hover {
                background-color: #8ab92d;
            }

        .info {
            background-color: #2196F3;
        }

            .info:hover {
                background: #0b7dda;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle">
            <label>Help</label>
        </div>

        <center>
            <div>
                <p style="color: #f00; font-size: x-large; text-decoration:solid; background-color:#00ffff">If you have problem in seeing Bangla in your PDF, Please Download SutonyMJ and SutonyMJ-Bold</p>
            </div>

            <div id="divFormFillUpManual" visible="false" runat="server">
                <a style="color: #8ab92d; font-size: x-large; text-decoration: none" target="_blank" href="../../Upload/Student-Form-Fill-Up-Manual.pdf"><u>Student Form Fill Up Manual</u></a>
            </div>
            <div style="height: 57px">
            </div>
            <div id="divNoticeForBaBssExam" visible="false" runat="server">
                <a style="color: #8ab92d; font-size: x-large; text-decoration: none" target="_blank" href="../../Upload/BAPassBSSPass2025.pdf?v=1.0"><u>Notice BA & BSS (Pass) Exam 2025</u></a>
            </div>
            <div style="height: 57px">
            </div>
            <div id="divAdminManual" visible="false" runat="server">
                <a style="color: #8ab92d; font-size: x-large; text-decoration: none" target="_blank" href="../../Upload/AdminManula.pdf"><u>Admin Manual</u></a>
            </div>
            <div style="height: 57px">
            </div>
            <div id="divUpdatedNoticeForBaBssExam" visible="false" runat="server">
                <a hidden="hidden" style="color: #8ab92d; font-size: x-large; text-decoration: none" target="_blank" href="../../Upload/UpdatedNotice-BA-BSS-(Pass)-Exam-2018.pdf"><u>Updated Notice BA & BSS (Pass) Exam 2018</u></a>
            </div>
        </center>

    </div>
</asp:Content>
