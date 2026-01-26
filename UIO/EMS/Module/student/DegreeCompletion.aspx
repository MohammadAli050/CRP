<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DegreeCompletion.aspx.cs" Inherits="EMS.Module.student.DegreeCompletion" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Degree Completion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">


    <link href="../../Bootstrap/css/bootstrap4.min.css" rel="stylesheet" />


    <link href="../../Bootstrap/css/fontcss.css" rel="stylesheet" />
    <script type="text/javascript" src="../../JavaScript/jquery_dip.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/popper.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/bootstrap4.min.js"></script>
    <script type="text/javascript" src="../../Bootstrap/js/moment.min.js"></script>
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

        #ctl00_MainContainer_ucProgram_ddlProgram, #ctl00_MainContainer_ucBatch_ddlBatch {
            height: 35px;
            width: 100% !important;
            border-color: silver;
        }

        input[type=checkbox] {
            height: 20px;
            width: 20px;
        }

        table#ctl00_MainContainer_gvStudentList span {
            margin-left: 2px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">


    <div class="container-fluid">

        <div class="row">

            <div class="col-sm-12 col-md-12 col-sm-12">
                <label><b style="color: black; font-size: 26px">Degree Completion</b></label>

            </div>
        </div>


        <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="300px" Width="300px" />
            <div>
                <asp:Label ID="Label2" runat="server" Text="Processing your request.........." ForeColor="Red" Font-Bold="true" Font-Italic="true" Font-Size="30px"></asp:Label>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>

                <div class="card ">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <asp:Label ID="Label88" runat="server" Text="Program" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <br />
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />

                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <asp:Label ID="Label5" runat="server" Text="Batch" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <br />
                                <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />

                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <asp:Label ID="Label6" runat="server" Text="Student ID" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" Height="35px">  </asp:TextBox></td>

                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <br />
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" CssClass="btn btn-info" Width="100%" Height="35px" Style="margin-top: 1px" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 2px">
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Publication Date</b>
                                <br />
                                <asp:TextBox runat="server" ID="txtApplyPublicationDate" CssClass="form-control" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtApplyPublicationDate" Format="dd/MM/yyyy" />
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <br />
                                <asp:Button ID="btnApply" runat="server" Text="Apply to the Selected Students" AutoPostBack="true" OnClick="btnApply_Click" CssClass="btn btn-info" Width="100%" Height="35px" Style="margin-top: 1px" />

                            </div>
                        </div>

                        <br />
                        <div class="row alert alert-dark" style="text-align: center">
                            <asp:Label runat="server" Font-Bold="True" Text="* You don't need to select(select Check) the students to save Degree Completed. Just check & click save button."></asp:Label>
                        </div>
                    </div>
                </div>

                <br />
                <div class="card ">
                    <div class="card-body">

                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                            ShowHeader="true" CssClass="gridCss">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL.">
                                    <ItemTemplate>
                                        <b><%# Container.DataItemIndex + 1 %><span>.</span></b>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button runat="server" ToolTip="Save All" Text="Save" class=" btn-sm btn-outline-dark" ID="btnSave" OnClientClick="if ( !confirm('Are you sure you want to Save ?')) return false;" OnClick="btnSave_Click1"></asp:Button>
                                            <hr />
                                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="All"
                                                AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />

                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox runat="server" ID="ChkSelect"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentId") %>' />
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("Attribute1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Roll">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Attribute2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transcript">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnGenerateTranscript" OnClick="btnGenerateTranscript_Click" runat="server" Text="Genereate" class=" btn-sm btn-outline-primary" />
                                            <asp:Button ID="btnTrasncriptSave" OnClick="btnTrasncriptSave_Click" runat="server" Text="Save" class=" btn-sm btn-outline-dark" />
                                        </div>
                                        <hr />
                                        <div style="text-align: center">
                                            <label>Transcript</label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTrasncript" runat="server" CssClass="form-control" Width="95%" Style="margin-bottom: 2px; margin-top: 2px" Text='<%# Eval("DegreeTranscriptNumber") %>'></asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Certificate">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnGenerateCertificate" OnClick="btnGenerateCertificate_Click" runat="server" Text="Genereate" class=" btn-sm btn-outline-primary" />
                                            <asp:Button ID="btnCertificateSave" OnClick="btnCertificateSave_Click" runat="server" Text="Save" class=" btn-sm btn-outline-dark" />
                                        </div>
                                        <hr />
                                        <div style="text-align: center">
                                            <label>Certificate</label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:TextBox ID="txtCertificate" runat="server" CssClass="form-control" Width="95%" Style="margin-bottom: 2px; margin-top: 2px; margin-left: 3px" Text='<%# Eval("DegreeCertificateNumber") %>'></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Degree Compeleted">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnDegreeCompleteSave" OnClick="btnDegreeCompleteSave_Click" runat="server" Text="Save" class=" btn-sm btn-outline-dark" />
                                        </div>
                                        <hr />
                                        <div style="text-align: center">
                                            <label>Degree Completed</label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox runat="server" ID="chkDegreeComplete" Checked='<%#Eval("IsDegreeComplete") %>'></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Publication Date">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnPublicationDateSave" OnClick="btnSave_Click" runat="server" Text="Save/Update" class=" btn-sm btn-outline-dark" />
                                        </div>
                                        <hr />
                                        <div style="text-align: center">
                                            <label>Publication Date</label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnTranscriptInfoId" runat="server" Value='<%#Eval("StudentTranscriptInfoId") %>' />
                                        <asp:TextBox ID="txtPublicationDate" runat="server" CssClass="form-control" Width="95%" Style="margin-bottom: 2px; margin-top: 2px"
                                            Text='<%#(Eval("PublicationDate") != null ? Eval("PublicationDate","{0:dd/MM/yyyy}") : "")%>'></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPublicationDate" Format="dd/MM/yyyy" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Examination Month">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnExaminationMonth" OnClick="btnSaveExaminationMonth_Click" runat="server" Text="Save/Update" class=" btn-sm btn-outline-dark" />
                                        </div>
                                        <hr />
                                        <div style="text-align: center">
                                            <label>Examination Month</label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:TextBox ID="txtExaminationMonth" runat="server" CssClass="form-control" Width="95%" Style="margin-bottom: 2px; margin-top: 2px; margin-left: 3px" Text='<%# Eval("ExaminationMonth") %>'></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                                </asp:TemplateField>

                            </Columns>

                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>



    </div>





    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel5"
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
        TargetControlID="UpdatePanel5"
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

                <%--<asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both"></div>
    <div style="height: 30px; width: 900px; padding: 15px;"></div>
</asp:Content>
