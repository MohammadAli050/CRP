<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentInformationViewUpdateAndMigrateNewStudent.aspx.cs" Inherits="EMS.Module.student.StudentInformationViewUpdateAndMigrateNewStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Information View/Update And Migrate New Student
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../Bootstrap/css/bootstrap4.min.css" rel="stylesheet" />

    <link href="../../Bootstrap/css/fontcss.css" rel="stylesheet" />
    <script type="text/javascript" src="../../JavaScript/jquery_dip.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/popper.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/bootstrap4.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/moment.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/simplePagination.js/1.6/jquery.simplePagination.js"></script>


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
        #MasterBody .navMenu #ctl00_menuMain tr td table tr td a {
            padding: 0 10px;
            cursor: pointer;
            height: 40px !important;
            padding-top: 12px;
            color: #5d5d5d;
            display: inline-block;
            text-decoration: none;
            font-family: Arial, Helvetica, sans-serif;
        }

        #MasterBody .navMenu > div tr td a {
            padding: 0 20px;
            padding-top: 0px;
            cursor: pointer;
            height: 40px !important;
            padding-top: 8px;
            color: #5d5d5d;
            display: inline-block;
            width: 100%;
            background: #f2f2f2;
            text-decoration: none;
        }

        ol, ul {
            margin-top: 12px;
            margin-bottom: 0;
        }

        #MasterBody .navMenu {
            background-color: #F2F2F2;
            height: 42 px !important;
            border-bottom: 2 px solid #2c2cff;
            margin-top: 5 px;
        }

        .new_card {
            border: none;
            border-radius: 8px;
            box-shadow: 1px 1px 10px 0 rgb(32,33,36, 0.1);
            padding: 15px 19px;
        }

        table.table thead th {
            border-top: none;
            color: white;
            text-align: center;
            background: #6a75ff;
            font-weight: bold;
            padding: 10px 0px;
        }

        table.table td, table.table th {
            padding: 5px 10px;
        }

        #MasterBody .navMenu {
            background-color: #F2F2F2;
            height: 43px;
            border-bottom: none;
            margin-top: 0px;
        }

        .blink {
            animation: blinker 0.6s linear infinite;
            color: #1c87c9;
            font-size: 30px;
            font-weight: bold;
            font-family: sans-serif;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">



        <div class="PageTitle">
            <h4>Student Information View/Update And Migrate New Student</h4>
        </div>

        <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
        </div>
        <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
        </div>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="card ">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-4">
                                <asp:Label ID="Label88" runat="server" Text="Program" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" Style="border-radius: 8px" Height="38px" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                    ControlToValidate="ddlProgram" ErrorMessage="Required" Font-Size="10pt" Font-Bold="true"
                                    ForeColor="Red" Display="Dynamic" ValueToCompare="0" Operator="NotEqual" CssClass="blink"
                                    ValidationGroup="gr3"></asp:CompareValidator>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4">
                                <asp:Label ID="Label1" runat="server" Text="Batch" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" Style="border-radius: 8px" Height="38px" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server"
                                    ControlToValidate="ddlBatch" ErrorMessage="Required" Font-Size="10pt" Font-Bold="true"
                                    ForeColor="Red" Display="Dynamic" ValueToCompare="0" Operator="NotEqual" CssClass="blink"
                                    ValidationGroup="gr3"></asp:CompareValidator>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <asp:LinkButton ID="lnkViewInformation" runat="server" ValidationGroup="gr3" CssClass="btn-info btn-sm" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px; margin-top: 25px" Font-Bold="true" OnClick="lnkViewInformation_Click">View Student</asp:LinkButton>

                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <asp:LinkButton ID="lnkStudentMigrate" runat="server" ValidationGroup="gr3" CssClass="btn-info btn-sm" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px; margin-top: 25px" Font-Bold="true" OnClick="lnkStudentMigrate_Click">Student Migration</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="card w-100" style="margin-top: 10px;" runat="server" id="DivGridview">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                                <br />

                                <asp:GridView ID="gvStudentList" runat="server" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="false" CssClass="gridview-container table-striped table-bordered" CellPadding="4">
                                    <RowStyle BackColor="#ecf0f0" />
                                    <AlternatingRowStyle BackColor="#ffffff" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <ItemStyle Width="35px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Student ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblStudentId" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Student Name">
                                            <ItemTemplate>
                                                <div style="text-align: left">
                                                    <asp:Label runat="server" ID="lblName" Font-Bold="true" Text='<%#Eval("Name") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGender" Font-Bold="true" Text='<%#Eval("BasicInfo.Gender") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Student Registration No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRegistration" Font-Bold="true" Text='<%#Eval("StudentRegistrationNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <SelectedRowStyle Height="24px" />
                                    <HeaderStyle CssClass="tableHead" Height="24px" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card w-100" style="margin-top: 10px" runat="server" id="DivExcelUpload">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <asp:Label ID="Label2" runat="server" Text="Select Excel File" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:FileUpload ID="ExcelUpload" runat="server" accept=".xlsx,.xls" CssClass="btn btn-primary" Width="100%" Style="margin-bottom: 5px" ClientIDMode="Static" Height="58%" />
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:LinkButton ID="lnkExcelUpload" runat="server" CssClass="btn-info btn-sm" Height="45px" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px; margin-top: 1px" Font-Bold="true" Text="Load Excel Data" OnClick="btnExcelUpload_Click"
                                    ClientIDMode="Static" CausesValidation="false"></asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:LinkButton ID="lnkSampleExcel" runat="server" CssClass="btn-info btn-sm" Height="45px" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px; margin-top: 1px" Font-Bold="true" Text="Sample Excel" OnClick="lnkSampleExcel_Click"
                                    ClientIDMode="Static" CausesValidation="false"></asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <br />
                                <asp:LinkButton ID="lnkStudentMigrateButton" runat="server" CssClass="btn-primary btn-sm" Height="45px" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px; margin-top: 1px" Font-Bold="true" Text="Migrate Student" OnClick="lnkStudentMigrateButton_Click"></asp:LinkButton>
                            </div>


                        </div>
                    </div>
                </div>



                <br />

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6" runat="server" id="DivTotalStudent" style="text-align: center">

                                <div class="card">
                                    <div class="card-body">

                                        <div class="row">
                                            <asp:Label ID="lblTotalStudent" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                                        </div>
                                        <div class="row" style="margin-top: 10px">
                                            <asp:GridView ID="GVTotalStudentList" runat="server">
                                                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                                                <RowStyle BackColor="#ecf0f0" />
                                                <AlternatingRowStyle BackColor="#ffffff" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6" runat="server" id="DivNotUploadedStudent" style="text-align: center">

                                <div class="card">
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:Label ID="lblNotMigratedStudent" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label></b>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="btn-info btn-sm" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px;" Font-Bold="true" Text="Download Excel" OnClick="lnkDownloadExcel_Click"
                                                    ClientIDMode="Static" CausesValidation="false"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px">
                                            <asp:GridView ID="GVNotUploadedStudentList" runat="server">
                                                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                                                <RowStyle BackColor="#ecf0f0" />
                                                <AlternatingRowStyle BackColor="#ffffff" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="lnkExcelUpload" />

            </Triggers>
        </asp:UpdatePanel>



        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel1"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="lnkViewInformation" Enabled="false" />
                    <EnableAction AnimationTarget="lnkStudentMigrate" Enabled="false" />                   
                    <EnableAction AnimationTarget="btnExcelUpload" Enabled="false" />                   
                    <EnableAction AnimationTarget="btnStudentMigrate" Enabled="false" />                   

                                      
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="lnkViewInformation" Enabled="true" />
                    <EnableAction   AnimationTarget="lnkStudentMigrate" Enabled="true" />
                    <EnableAction   AnimationTarget="btnExcelUpload" Enabled="true" />
                    <EnableAction   AnimationTarget="btnStudentMigrate" Enabled="true" />

                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    

</asp:Content>
