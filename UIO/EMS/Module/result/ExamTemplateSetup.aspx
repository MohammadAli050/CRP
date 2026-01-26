<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="EMS.miu.Result.ExamTemplateSetup" CodeBehind="ExamTemplateSetup.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Exam Template
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

    <style type="text/css">
        @media (max-width: 576px) {
            .modalPopup {
                height: 300px;
                width: 400px;
                position: fixed;
                z-index: 100001 !important;
                left: 288.5px !important;
                top: 207px !important;
            }
        }

        .modalBackground {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }

        .font {
            font-size: 12px;
        }

        .cursor {
            cursor: pointer;
        }

        .auto-style4 {
            width: 212px;
        }
    </style>

    <script>
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

        function initdropdown() {
            $('#ctl00_MainContainer_ddlProgram').select2({
                allowClear: true,
                dropdownParent: $('#ctl00_MainContainer_Panel1')
            });

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Exam Template Name Setup</label>
    </div>

    <div class="container-fluid mt-4">

        <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;"></div>
        <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" CssClass="rounded-circle shadow" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="card shadow-sm mb-4">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button ID="addButton" runat="server" Text="Add New Template" CssClass="btn btn-primary w-100" OnClick="addButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card mt-4">
                    <div class="card-body">


                        <asp:GridView ID="GvExamTemplate" runat="server" AutoGenerateColumns="False" CssClass="gridview-container table-striped table-bordered"
                            EmptyDataText="No data found." AllowPaging="True" PageSize="20" Width="100%"
                            OnRowCommand="GvExamTemplate_RowCommand" OnPageIndexChanging="GvExamTemplate_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ExamTemplateMasterId" Visible="false" HeaderText="Id" />
                                <asp:BoundField DataField="ShortName" HeaderText="Program Name" />
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ExamTemplateEditButton" CommandName="ExamTemplateEdit" Text="Edit" ToolTip="Edit Exam Template" CommandArgument='<%# Bind("ExamTemplateMasterId") %>' runat="server" CssClass="btn btn-sm btn-warning"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExamTemplateMasterName" HeaderText="Template Name" />
                                <asp:BoundField DataField="ExamTemplateMasterTotalMark" HeaderText="Template Mark" />
                                <asp:TemplateField HeaderText="Active Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReg" runat="server" Font-Bold="true"
                                            Text='<%# (string)Eval("Attribute1") == "1" ? "Active" : "In Active" %>'
                                            ForeColor='<%# (string)Eval("Attribute1") == "0" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ExamTemplateDeleteButton" CommandName="ExamTemplateDelete" Text="Delete" ToolTip="Delete Exam Template" OnClientClick="return confirm('Are you sure to delete exam template?')" CommandArgument='<%# Bind("ExamTemplateMasterId") %>' runat="server" CssClass="btn btn-sm btn-danger"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="addButton" BackgroundCssClass="modalBackground" />

                <asp:Panel ID="Panel1" runat="server" CssClass="card shadow p-4" Style="display: none; width: 100%; max-width: 500px; margin: auto;">
                    <h5 class="mb-3 text-primary fw-bold">Exam Template</h5>
                    <div class="mb-3">
                        <asp:Label ID="lblMessage" runat="server" CssClass="form-label fw-bold" Text="Message :" />
                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red" CssClass="ms-2" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblExamTemplate" runat="server" CssClass="form-label" Text="Exam Template Name" />
                        <asp:TextBox ID="txtExamTemplateName" runat="server" CssClass="form-control" Placeholder="Exam Template Name" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblExamTemplateMark" runat="server" CssClass="form-label" Text="Exam Template Mark" />
                        <asp:TextBox ID="txtExamTemplateMark" runat="server" CssClass="form-control" Placeholder="Exam Template Mark" />
                        <asp:Label ID="lblExamTemplateMasterId" Visible="false" runat="server" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblExamTemplateType" runat="server" CssClass="form-label" Text="Exam Template Type" />
                        <asp:DropDownList ID="ddlExamTemplateType" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Basic" Value="1" />
                            <%--<asp:ListItem Text="Calculative" Value="2" />--%>
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <script type="text/javascript">
                            Sys.Application.add_load(initdropdown);
                        </script>
                        <asp:Label ID="lblProgramName" runat="server" CssClass="form-label" Text="Program Name" />
                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-select" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblIsActive" runat="server" CssClass="form-label" Text="Active Status" />
                        <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-select">
                            <asp:ListItem Text="InActive" Selected="True" Value="0" />
                            <asp:ListItem Text="Active" Value="1" />
                        </asp:DropDownList>
                    </div>
                    <div class="d-flex justify-content-end gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" Visible="false" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClose_Click" />
                    </div>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
