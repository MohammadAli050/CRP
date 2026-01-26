<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    Inherits="AssignCourseTree" CodeBehind="AssignCourseTree.aspx.cs" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Curriculum Distribution Assign Students</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

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

        function initdropdown() {
            $('#ctl00_MainContainer_ddlInstitute').select2({
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">


    <div class="PageTitle">
        <h4>Curriculum Distribution Assign Students</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>





    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program</b>
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Student ID</b>
                            <asp:TextBox ID="txtStudent" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="Load Student" ValidationGroup="gr3" CssClass="btn btn-info" Style="margin-top: 25px;" Width="100%" />

                        </div>

                    </div>


                    <div class="row mt-3" runat="server" id="DivDistribution">

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label2" runat="server" Text="Curriculum Tree " Font-Bold="true" Font-Size="Large"></asp:Label>
                            <asp:DropDownList ID="ddlCourseTree" DataValueField="TreeMasterID" DataTextField="Node_Name" AutoPostBack="true" OnSelectedIndexChanged="CourseTree_SelectedIndexChanged" runat="server" CssClass="form-control" Style="border-radius: 8px"></asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server"
                                ControlToValidate="ddlCourseTree" ErrorMessage="Required" Font-Size="10pt" Font-Bold="true"
                                ForeColor="Red" Display="Dynamic" ValueToCompare="0" Operator="NotEqual" CssClass="blink"
                                ValidationGroup="gr4"></asp:CompareValidator>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label5" runat="server" Text="Curriculum Distribution" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <asp:DropDownList runat="server" CssClass="form-control" Style="border-radius: 8px" ID="ddlLinkedCalendars" DataValueField="TreeCalendarMasterID" DataTextField="Name" />
                            <asp:CompareValidator ID="CompareValidator4" runat="server"
                                ControlToValidate="ddlLinkedCalendars" ErrorMessage="Required" Font-Size="10pt" Font-Bold="true"
                                ForeColor="Red" Display="Dynamic" ValueToCompare="0" Operator="NotEqual" CssClass="blink"
                                ValidationGroup="gr4"></asp:CompareValidator>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save Distribution" ValidationGroup="gr4" CssClass="btn btn-info" Style="margin-top: 25px;" Width="100%" />

                        </div>
                    </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <div class="card mt-3">
                <div class="card-body">
                    <div class="row">
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Style="color: #FF5722; font-size: 20px"></asp:Label>
                    </div>
                    <br />
                    <div class="row">

                        <asp:GridView ID="gvwCollection" runat="server" AutoGenerateColumns="False" CssClass="gridview-container table-striped table-bordered" CellPadding="4" Width="100%">

                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox ID="chkAll" Text="Select All" Width="100px" runat="server" CssClass="selectAllCheckBox" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Width="60px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudentID" runat="server" Text='<%# Eval("StudentID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Student ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>


                                <asp:BoundField DataField="Name" HeaderText="Student Name" HeaderStyle-CssClass="header-left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TreeMasterID" HeaderText="Tree Master" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <%--<ItemStyle Width="100px" />--%>
                                                        </asp:BoundField>
                                <asp:BoundField DataField="TreeCalendarMasterID" HeaderText="Tree Calendar Master" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <%--<ItemStyle Width="100px" />--%>
                                                        </asp:BoundField>

                                <asp:BoundField DataField="CourseTreeLinkCalendars" HeaderText="Curriculum Tree → Curriculum Distribution" HeaderStyle-CssClass="header-left"
                                    ItemStyle-Width="200px">

                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>

                            </Columns>
                            <SelectedRowStyle Height="24px" />
                            <HeaderStyle Height="24px" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>





    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel3"
        runat="server">
        <Animations>
                    <OnUpdating>
                        <Parallel duration="0">
                            <ScriptAction Script="InProgress();" />
                            <EnableAction AnimationTarget="btnView" 
                                            Enabled="false" />                   
                        </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnView" 
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
                            <EnableAction AnimationTarget="btnSave" 
                                            Enabled="false" />                   
                        </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnSave" 
                                            Enabled="true" />
                        </Parallel>
                    </OnUpdated>
            </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>





    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button2" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ConfirmModalPopUp" runat="server" TargetControlID="Button2" PopupControlID="Panel2"
                CancelControlID="Button3" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="Panel2" Style="display: none; padding: 5px;" BackColor="White" Width="500px" Height="180px">
                <fieldset style="padding: 5px; margin: 5px;">
                    <legend style="font-weight: bold; font-size: 20px; text-align: center">Confirmation</legend>

                    <div class="row">
                        <div class="card">
                            <div class="card-body" style="text-align: center">
                                <asp:Label runat="server" ID="lblMsgConfirm" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <asp:Button runat="server" ID="Button3" Text="Cancel" CssClass="btn btn-danger" Width="100%" Font-Bold="true" Font-Size="18px" />

                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <asp:Button runat="server" ID="btnConfirmApply" Text="Confirm" CssClass="btn btn-info" Width="100%" Font-Bold="true" Font-Size="18px" OnClick="btnConfirmApply_Click" />

                        </div>
                    </div>
                    <br />


                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

