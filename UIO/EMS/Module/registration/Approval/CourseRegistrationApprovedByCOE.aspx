<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="CourseRegistrationApprovedByCOE.aspx.cs" Inherits="EMS.Module.Registration.Approval.CourseRegistrationApprovedByCOE" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Course Registration Forward By Institute
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1">


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

        function confirmApproval() {
            return confirm("Are you sure you want to approve selected students?");
        }

        function confirmReject() {
            return confirm("Are you sure you want to reject selected students?");
        }

        function toggleAllCourse(source) {
            var checkboxes = document.querySelectorAll('[id$=chkCourseSelect]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
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

    <%--Modal CSS--%>
    <style type="text/css">
        /* Modal Background Overlay */
        .modalBackground {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.6);
            backdrop-filter: blur(4px);
            z-index: 9999;
            animation: fadeIn 0.3s ease-in-out;
        }

        /* Modal Panel Container */
        .modern-modal-panel {
            position: fixed !important;
            top: 50% !important;
            left: 50% !important;
            transform: translate(-50%, -50%) !important;
            z-index: 1000000 !important;
            animation: slideDown 0.3s ease-out !important;
        }

        /* Main Modal Container */
        .modern-modal-container {
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
            padding: 40px;
            min-width: 450px;
            max-width: 550px;
            text-align: center;
        }

        /* Icon Wrapper */
        .modal-icon-wrapper {
            margin-bottom: 25px;
            display: flex;
            justify-content: center;
        }

        /* Modal Icon Styles */
        .modal-icon {
            width: 80px;
            height: 80px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            animation: scaleIn 0.4s ease-out 0.1s backwards;
        }

        .modal-icon-warning {
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            box-shadow: 0 8px 24px rgba(245, 87, 108, 0.4);
        }

        .modal-icon-success {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
            box-shadow: 0 8px 24px rgba(56, 239, 125, 0.4);
        }

        .modal-icon-error {
            background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);
            box-shadow: 0 8px 24px rgba(244, 92, 67, 0.4);
        }

        .icon-symbol {
            color: white;
            font-size: 48px;
            font-weight: 700;
            line-height: 1;
        }

        /* Content Wrapper */
        .modal-content-wrapper {
            margin-bottom: 30px;
        }

        /* Modal Title */
        .modal-title {
            font-size: 28px;
            font-weight: 700;
            color: #2d3748;
            margin-bottom: 15px;
            letter-spacing: -0.5px;
        }

        /* Modal Message */
        .modal-message {
            margin-bottom: 10px;
        }

        .alert-message-text {
            font-size: 16px;
            color: #4a5568;
            line-height: 1.6;
            display: block;
        }

        /* Modal Actions (Buttons) */
        .modal-actions {
            display: flex;
            gap: 12px;
            justify-content: center;
            flex-wrap: wrap;
        }

        /* Button Base Styles */
        .btn-modal {
            padding: 14px 32px;
            font-size: 16px;
            font-weight: 600;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            min-width: 140px;
            outline: none;
        }

            .btn-modal:hover {
                transform: translateY(-2px);
                box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
            }

            .btn-modal:active {
                transform: translateY(0);
            }

        /* Confirm Button */
        .btn-confirm {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(102, 126, 234, 0.4);
        }

            .btn-confirm:hover {
                background: linear-gradient(135deg, #5568d3 0%, #6a3f8f 100%);
                box-shadow: 0 8px 25px rgba(102, 126, 234, 0.5);
            }

        /* Cancel Button */
        .btn-cancel {
            background: #e2e8f0;
            color: #4a5568;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
        }

            .btn-cancel:hover {
                background: #cbd5e0;
                color: #2d3748;
            }

        /* Alternative Danger Style for Confirm Button (if needed) */
        .btn-confirm-danger {
            background: linear-gradient(135deg, #eb3349 0%, #f45c43 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(235, 51, 73, 0.4);
        }

            .btn-confirm-danger:hover {
                background: linear-gradient(135deg, #d42a40 0%, #e04a38 100%);
                box-shadow: 0 8px 25px rgba(235, 51, 73, 0.5);
            }

        /* Alternative Success Style for Confirm Button (if needed) */
        .btn-confirm-success {
            background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(56, 239, 125, 0.4);
        }

            .btn-confirm-success:hover {
                background: linear-gradient(135deg, #0e8174 0%, #2fd66d 100%);
                box-shadow: 0 8px 25px rgba(56, 239, 125, 0.5);
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">


    <div class="PageTitle">
        <h4>Course Registration Forward By Institute </h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 1000000;">
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

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program<span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Student ID</b>
                            <asp:TextBox ID="txtStudent" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Status </b>
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2" style="margin-top: 5px">
                            <br />
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" CssClass="btn btn-info form-control" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-3" runat="server" id="divTimeline">
                <div class="alert alert-warning text-center">
                    <asp:Label runat="server" ID="lblTimelinemsg" Font-Bold="true" Font-Italic="true" Text=""></asp:Label>
                </div>
            </div>

            <div class="card mt-3" runat="server" id="divGridView">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnCoeApproved" runat="server" Text="Approved by COE" OnClientClick="return confirmApproval();" OnClick="btnCoeApproved_Click" CssClass="btn btn-success form-control" />
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <asp:Button ID="btnReject" runat="server" Text="Reject by COE/Back to Institute/Unregistration"
                                OnClick="btnReject_Click" CssClass="btn btn-danger form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12" style="position: static">
                            <b>Student List</b>
                            <br />
                            <asp:GridView ID="GvStudent" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentID"
                                CssClass="gridview-container table-striped table-bordered" CellPadding="4" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="toggleAll(this)" Text="All" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Program
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblShortName" Text='<%#Eval("ShortName")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Student ID
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <div style="text-align: left">
                                                Student Name
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblName" Text='<%#Eval("FullName")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <div style="text-align: left">
                                                Institute Forwarded Course List
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormalCodeList" Text='<%#Eval("FormalCodeList")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <div style="text-align: left">
                                                COE Approved Course List
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCOEFormalCodeList" Text='<%#Eval("COEFormalCodeList")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    
    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>

            <!-- ASP.NET Modal Popup -->
            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupConfirmationAlert" runat="server"
                TargetControlID="Button1"
                PopupControlID="Panel2"
                BackgroundCssClass="modalBackground"
                CancelControlID="Button2">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="Panel2" CssClass="modern-modal-panel" Style="display: none;">
                <div class="modern-modal-container">
                    <!-- Icon Section -->
                    <div class="modal-icon-wrapper">
                        <div class="modal-icon modal-icon-warning">
                            <span class="icon-symbol">!</span>
                        </div>
                    </div>

                    <!-- Content Section -->
                    <div class="modal-content-wrapper">
                        <h3 class="modal-title">Confirmation Required</h3>
                        <div class="modal-message">
                            <asp:Label ID="lblAlertMessage" runat="server" Text="" CssClass="alert-message-text"></asp:Label>
                        </div>
                    </div>

                    <!-- Buttons Section -->
                    <div class="modal-actions">
                        <asp:Button runat="server" ID="Button2" Text="Cancel" CssClass="btn-modal btn-cancel" />
                        <asp:Button runat="server" ID="btnRequestConfirm" Text="Yes, Confirm" OnClick="btnRequestConfirm_Click" CssClass="btn-modal btn-confirm" OnClientClick="this.value = 'Please wait....'; this.disabled = true;" UseSubmitBehavior="false" />
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnInsCancel" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="btnInsCancel" Enabled="true" />
                    </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>
