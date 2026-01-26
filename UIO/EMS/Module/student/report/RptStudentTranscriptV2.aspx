<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentTranscriptV2.aspx.cs" Inherits="EMS.Module.student.report.RptStudentTranscriptV2" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Transcript
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">


    <link href="../../../Bootstrap/css/fontcss.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../JavaScript/jquery_dip.min.js"></script>
    <script type="text/javascript" src="../../../Bootstrap/js/popper.min.js"></script>
    <script type="text/javascript" src="../../../Bootstrap/js/bootstrap4.min.js"></script>
    <script type="text/javascript" src="../../../Bootstrap/js/moment.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/simplePagination.js/1.6/jquery.simplePagination.js"></script>


    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Course/1.1.0/Course.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/Course/1.1.0/Course.min.css" rel="stylesheet" type="text/css" />

    <%--<link href="../../Bootstrap/css/mdb.min.css" rel="stylesheet" />--%>


    <style>
        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            background-color: #f9f9f9;
            padding: 5px;
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

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">


    <div class="PageTitle">
        <label>Student Transcript</label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
        <div>
            <asp:Label ID="Label2" runat="server" Text="Processing your request.........." ForeColor="Red" Font-Bold="true" Font-Italic="true" Font-Size="30px"></asp:Label>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div class="card ">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Roll</b>
                            <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnLoadStdInfo" runat="server" CssClass="btn btn-info" Text="Load Info" Style="height: 38px; width: 100%; margin-top: 24px;" OnClick="btnLoadStdInfo_Click" />

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label4" runat="server" Text="Roll "></asp:Label>
                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="................." CssClass="form-control"></asp:Label>

                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="Name  "></asp:Label>
                            <asp:Label ID="lblName" runat="server" Font-Bold="true" Text="................." CssClass="form-control"></asp:Label>

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label5" runat="server" Text="Program "></asp:Label>
                            <asp:Label ID="lblProg" runat="server" Font-Bold="true" Text="................." CssClass="form-control"></asp:Label>

                        </div>


                    </div>


                    <div class="row" style="margin-top: 10px">

                        <div class="col-lg-2 col-md-2 col-sm-2">

                            <div class="controls" style="display: none">
                                <asp:TextBox runat="server" ID="txtResultPublishDate" Visible="false" Width="113px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtResultPublishDate" Format="dd/MM/yyyy" />
                            </div>

                            <b>Prepared Date</b>
                            <asp:TextBox runat="server" ID="txtIssuedDate" CssClass="form-control" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIssuedDate" Format="dd/MM/yyyy" />
                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Total Credits</b>
                            <asp:TextBox ID="txtRequiredCredit" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Remarks</b>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program Duration</b>
                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnLoad" runat="server" CssClass="btn btn-info" Style="height: 38px; width: 100%; margin-top: 24px;" Text="Load Transcript" OnClick="btnLoad_Click" />

                        </div>

                    </div>



                    <div class="row" style="margin-top: 30px">


                        <div class="col-lg-10 col-md-10 col-sm-10">
                            <label id="lblDegreeName" runat="server" visible="false">Degree Name</label>

                            <asp:DropDownList runat="server" ID="ddlMISSDegreeName" Visible="false" CssClass="form-control">
                                <asp:ListItem Value="1">MSc/M Engg in Information Systems Security (MISS)</asp:ListItem>
                                <asp:ListItem Value="2">Master of Engineering in Information Systems Security (M Engg in ISS)</asp:ListItem>
                                <asp:ListItem Value="3">Master of Science in Engineering in Information Systems Security (MSc Engg in ISS)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlMICTDegreeName" Visible="false" CssClass="form-control">
                                <asp:ListItem Value="1">MSc/Masters in Information and Communication Technology (MICT)</asp:ListItem>
                                <asp:ListItem Value="2">Masters in Information and Communication Technology (MICT)</asp:ListItem>
                                <asp:ListItem Value="3">Master of Science in Information and Communication Technology (MSc in ICT)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlMICEDegreeName" Visible="false" CssClass="form-control">
                                <asp:ListItem Value="1">MSc/Masters in Information and Communication Engineering (MICE)</asp:ListItem>
                                <asp:ListItem Value="2">Master of Science in Information and Communication Engineering (MSc in ICE)</asp:ListItem>
                                <asp:ListItem Value="3">Masters in Information and Communication Engineering (MICE)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlMESDegreeName" Visible="false" CssClass="form-control">
                                <asp:ListItem Value="1">M.Sc. in Environmental Science</asp:ListItem>
                                <asp:ListItem Value="2">Masters in Environmental Science</asp:ListItem>
                                <asp:ListItem Value="3">Master of Environmental Science (MES)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlMESMDegreeName" Visible="false" CssClass="form-control">
                                <asp:ListItem Value="1">M.Sc. in Environmental Science and Management</asp:ListItem>
                                <asp:ListItem Value="2">Masters in Environmental Science and Management</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="StudentGradeReport" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="57%" Height="100%" SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel5" runat="server">
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

</asp:Content>
