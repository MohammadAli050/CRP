<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="EMS.SyllabusMan.CourseExplorer" CodeBehind="CourseExplorer.aspx.cs" %>


<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Course List Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

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

        .table {
            border: 1px solid #008080;
        }


        .style10 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            width: 100%;
            height: 100px;
        }

        .style11 {
            height: 28px;
        }

        .dxeButtonEdit {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }

        .dxeButtonEdit {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }

        .style12 {
            border: 1px solid Blue;
            font: 11px Arial, Helvetica, sans-serif;
            color: #666666;
            vertical-align: Middle;
            width: 27%;
        }

        .button_load_SaveOrUpdate {
            height: 38px;
            width: 90px;
            border-radius: 5px;
            padding-left: 23px;
            background-color: blue;
            color: white;
        }

        .button_Add {
            height: 38px;
            width: 175px;
            border-radius: 5px;
            padding-left: 23px;
            background-color: #368445;
            color: white;
        }

        .button_close {
            height: 38px;
            width: 90px;
            border-radius: 5px;
            padding-left: 23px;
            background-color: #d7393b;
            color: white;
        }

        #ctl00_MainContainer_btnMigrateCourse, #ctl00_MainContainer_btnClear, #ExcelUpload, #btnSampleExcel, #ctl00_MainContainer_btnCourseMigrateButton, #btnExcelUpload, #ctl00_MainContainer_ucDepartment_ddlDepartment, #ctl00_MainContainer_ucProgram_ddlProgram, #ctl00_MainContainer_btnExportExcel, #ctl00_MainContainer_searchFormalCode, #ctl00_MainContainer_searchTitle, #ctl00_MainContainer_btnLoad, #ctl00_MainContainer_btnAddNew {
            height: 40px !important;
            width: 100% !important;
            font-size: 15px !important;
            border-radius: 10px;
        }
    </style>
    <script type="text/javascript">
        function isNumber(e) {
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode
            status = charCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please make sure entries are numbers only")

                return false
            }

            return true;
        }

        function check() {

            var txtFoCode = document.getElementById("<%=txtFormalCode.ClientID%>");
            var txtVeCode = document.getElementById("<%=txtVersionCode.ClientID%>");
            var txtTtle = document.getElementById("<%=txtTitle.ClientID%>");
            var txtCdits = document.getElementById("<%=txtCredits.ClientID%>");

            if (txtFoCode.value == "" || txtVeCode.value == "" || txtTtle.value == "" || txtCdits.value == "") {

                document.getElementById('<%= btnSaveOrUpdate.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%= btnSaveOrUpdate.ClientID %>').disabled = false;
            }
        }

        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please make sure entries are numbers only")
                return false;
            }
            return true;
        }
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
        <h4>Course List Management</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="card">
                <div class="card-body">


                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Choose Program</b>
                            <br />
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" class="margin-zero dropDownList" OnProgramSelectedIndexChanged="ucProgram_ProgramSelectedIndexChanged" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Course Code</b>
                            <br />

                            <asp:TextBox ID="searchFormalCode" runat="server" Width="350px"></asp:TextBox>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Course Name</b>
                            <br />
                            <asp:TextBox ID="searchTitle" runat="server" Width="350px"></asp:TextBox>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button ID="btnLoad" runat="server" Text="Load Courses" OnClick="btnLoad_Click" CssClass="btn btn-primary form-control" />

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-dark form-control" />

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button ID="btnMigrateCourse" runat="server" CssClass="btn btn-danger form-control" Text="Migrate Course" OnClick="btnMigrateCourse_Click" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px">

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnAddNew" runat="server" Text="Add New Course" CssClass="btn btn-info form-control" OnClick="btnAddNewCourse_Click" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-success form-control" Text="Download Course" OnClick="btnExportExcel_Click" />
                        </div>
                    </div>
                </div>
            </div>



            <div class="card" style="margin-top: 10px" runat="server" id="divUpload">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:Label ID="Label2" runat="server" Text="Choose Excel File" Font-Bold="true"></asp:Label>
                            <br />
                            <asp:FileUpload ID="ExcelUpload" runat="server" accept=".xlsx,.xls" CssClass="w-100" BackColor="#cccccc" ClientIDMode="Static" Height="35px" />
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <br />
                            <asp:Button ID="btnExcelUpload" runat="server" CssClass="btn btn-info form-control w-100" Font-Size="18px" Text="Load Excel File To View Before Upload" OnClick="btnExcelUpload_Click"
                                ClientIDMode="Static" CausesValidation="false"></asp:Button>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <br />
                            <asp:Button ID="btnCourseMigrateButton" runat="server" CssClass="btn btn-danger form-control w-100" Font-Size="18px" Text="Migrate Course Information" OnClick="btnCourseMigrateButton_Click" OnClientClick="jsShowHideProgress();"></asp:Button>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <br />
                            <asp:Button ID="btnSampleExcel" runat="server" CssClass="btn btn-dark form-control w-100" Font-Size="18px" Text="Download Sample Excel File To View Format " OnClick="btnSampleExcel_Click"
                                ClientIDMode="Static" CausesValidation="false"></asp:Button>
                        </div>
                    </div>



                    <div class="card" style="margin-top: 10px">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6" runat="server" id="DivTotalCourse" style="text-align: center;">

                                    <div class="card">
                                        <div class="card-body">

                                            <div class="row">
                                                <asp:Label ID="lblTotalCourse" runat="server" Text="" Font-Bold="true" Font-Size="18"></asp:Label>
                                            </div>
                                            <br />
                                            <asp:GridView ID="GVTotalCourseList" runat="server" Width="100%">
                                                <HeaderStyle BackColor="#3f2c7b" ForeColor="White" Height="10px" Font-Bold="True" />
                                                <FooterStyle BackColor="#3f2c7b" ForeColor="White" Height="10px" Font-Bold="True" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>

                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-6" runat="server" id="DivNotUploadedCourse" style="text-align: center;">

                                    <div class="card">
                                        <div class="card-body">

                                            <div class="row">
                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                    <asp:Label ID="lblNotMigratedCourse" runat="server" Text="" Font-Bold="true" Font-Size="18px" ForeColor="Red"></asp:Label></b>
                                                </div>
                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                    <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="btn-info btn-sm" Style="display: inline-block; width: 100%; text-align: center; font-size: 20px;" Font-Bold="true" Text="Download Excel File"
                                                        OnClick="lnkDownloadExcel_Click" ClientIDMode="Static" CausesValidation="false" OnClientClick="jsShowHideProgress();"></asp:LinkButton>
                                                </div>
                                            </div>

                                            <br />
                                            <asp:GridView ID="GVNotUploadedCourseList" runat="server" Width="100%">
                                                <HeaderStyle BackColor="#3f2c7b" ForeColor="White" Height="10px" Font-Bold="True" />
                                                <FooterStyle BackColor="#3f2c7b" ForeColor="White" Height="10px" Font-Bold="True" />
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
            </div>




        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
            <asp:PostBackTrigger ControlID="btnExcelUpload" />
            <asp:PostBackTrigger ControlID="btnSampleExcel" />
            <asp:PostBackTrigger ControlID="lnkDownloadExcel" />

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
        TargetControlID="UpdatePanel3"
        runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnSaveOrUpdate" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnSaveOrUpdate" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div>
                <div>
                    <div style="margin-bottom: 5px; margin-top: 5px; float: left; width: 100%;">
                    </div>

                    <div class="Teacher-container">
                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnPopUp" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnPopUp" Style="display: none;" Width="50%">

                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div style="height: auto; width: auto; padding: 5px; margin: 5px; background-color: Window;">

                                        <fieldset style="padding: 10px; margin: 5px; border-color: lightgreen;">
                                            <legend>Course Info</legend>
                                            <div class="well" style="padding: 5px; width: 100%">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <div class="Message-Area">
                                                                <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="lblPopUpMassege" ForeColor="Red" runat="server"></asp:Label>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="card" style="margin-top: 10px">
                                                    <div class="card-body">

                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Formal Code <span style="color: red">*</span></b>
                                                                <asp:HiddenField ID="hdnCourseId" runat="server" />
                                                                <asp:HiddenField ID="hdnVersionId" runat="server" />
                                                                <asp:TextBox runat="server" ID="txtFormalCode" CssClass="form-control" Width="100%" onblur="check();" onkeyup="check();" AutoPostBack="false" />
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Version Code <span style="color: red">*</span></b>
                                                                <asp:TextBox runat="server" ID="txtVersionCode" CssClass="form-control" Width="100%" onblur="check();" onkeyup="check();" />
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Transcript Code</b>
                                                                <asp:TextBox runat="server" ID="txtTranscriptCode" CssClass="form-control" Width="100%" />
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-top: 10px">
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Program<span style="color: red">*</span></b>
                                                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Title<span style="color: red">*</span></b>
                                                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" Width="100%" onblur="check();" onkeyup="check();" />

                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Credit <span style="color: red">*</span></b>
                                                                <asp:TextBox runat="server" ID="txtCredits" CssClass="form-control" Width="100%" onkeyup="check();" onkeypress="return onlyDotsAndNumbers(event)" />

                                                            </div>
                                                        </div>


                                                        <div class="row" style="margin-top: 10px">

                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Thesis/Project</b>
                                                                <asp:DropDownList ID="ddlMultiple" runat="server" AutoPostBack="false" EnableViewState="true" Width="100%" CssClass="form-control">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Type</b>
                                                                <asp:DropDownList ID="ddlCourseType" runat="server" AutoPostBack="false" EnableViewState="true" CssClass="form-control" Width="100%">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Is Active</b>
                                                                <asp:DropDownList ID="ddlIsActive" runat="server" AutoPostBack="false" EnableViewState="true" CssClass="form-control" Width="100%">
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                        <div class="row" style="margin-top: 10px">

                                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                                                <asp:Button ID="btnSaveOrUpdate" runat="server" Text="SaveOrUpdate" Style="border-radius: 5px" CssClass="btn btn-primary" Width="100%" OnClick="btnSaveOrUpdate_Click" />

                                                            </div>
                                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" Style="border-radius: 5px" CssClass="btn btn-danger" Width="100%" />

                                                            </div>
                                                            <div class="col-lg-5 col-md-5 col-sm-5">
                                                                <div style="text-align: center; font-size: 15px">
                                                                    <asp:CheckBox ID="chkCheckDuplicate" runat="server" OnCheckedChanged="chkCheckDuplicate_CheckedChanged" ForeColor="Crimson" Font-Bold="true" AutoPostBack="true" Text="Check Version Code Is Exists" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-top: 10px; display: none">
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Course Content</b>
                                                                <asp:TextBox runat="server" ID="txtCourseContent" CssClass="form-control" Width="100%" />

                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                <b>Course Group</b>
                                                                <asp:TextBox runat="server" ID="txtCourseGroup" CssClass="form-control" Width="100%" />

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                        </fieldset>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div style="clear: both;"></div>

                    <%-- Main Course List GridView --%>
                    <div class="card" runat="server" id="divGrid">
                        <div class="card-header">
                            <h3 class="card-title">Course List</h3>
                        </div>
                        <div class="card-body table-responsive p-0">
                            <%-- table-responsive prevents overflow --%>
                            <asp:GridView ID="gvCourselists" runat="server"
                                OnSorting="gvStudentBillView_Sorting"
                                AllowSorting="True"
                                AutoGenerateColumns="False"
                                ShowFooter="false"
                                Width="100%"
                                GridLines="None"
                                CssClass="table table-hover table-bordered table-striped mb-0">


                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <span class="badge badge-secondary"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Course Code" SortExpression="FormalCode">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkFormalCode" runat="server" CommandName="Sort" CommandArgument="FormalCode" Text="Course Code" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span class="text-bold"><%#Eval("FormalCode") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Version Code" SortExpression="VersionCode">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkVersionCode" runat="server" CommandName="Sort" CommandArgument="VersionCode" Text="Version Code" />
                                        </HeaderTemplate>
                                        <ItemTemplate><%#Eval("VersionCode") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transcript Code">
                                        <ItemTemplate><%#Eval("TranscriptCode") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkTitle" runat="server" CommandName="Sort" CommandArgument="Title" Text="Title" />
                                        </HeaderTemplate>
                                        <ItemTemplate><%#Eval("Title") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credits" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="badge badge-info"><%#Eval("Credits") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Thesis/Project" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#  Eval("HasMultipleACUSpan") == null ? "" : (Boolean.Parse(Eval("HasMultipleACUSpan").ToString())) ? "<span class='badge badge-warning'>Yes</span>" : "No" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Active" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#  Eval("IsActive") == null ? "" : (Boolean.Parse(Eval("IsActive").ToString())) ? "<span class='text-success'><i class='fas fa-check-circle'></i> Yes</span>" : "<span class='text-danger'><i class='fas fa-times-circle'></i> No</span>" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate><%#Eval("CourseType") %></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ToolTip="Edit" ID="lnkEdit"
                                                CssClass="btn btn-sm btn-outline-primary"
                                                CommandArgument='<%#Eval("CourseID")+","+ Eval("VersionID")%>' OnClick="lnkEdit_Click">
                            <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
