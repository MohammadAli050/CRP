<%@ Page Title="Exam Basic Template Item" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="EMS.miu.Result.ExamTemplateItemSetup" CodeBehind="ExamTemplateItemSetup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

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

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Exam Template Item Setup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Exam Template Item Setup</label>
    </div>

    <!-- Loading Spinner -->
    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;"></div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" CssClass="rounded-circle shadow" />
    </div>

    <!-- Form Section -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                Sys.Application.add_load(initdropdown);
            </script>

            <div class="card shadow-sm border-0 mb-4">
                <div class="card-body">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label for="ddlExamTemplateName" class="form-label fw-semibold">
                                Exam Template Name <span class="text-danger">*</span>
                            </label>
                            <asp:DropDownList ID="ddlExamTemplateName" runat="server" Width="100%" AutoPostBack="True"
                                CssClass="select2" OnSelectedIndexChanged="ddlTemplateName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-6">
                            <label for="ddlExamMetaType" class="form-label fw-semibold">
                                Exam Meta Type <span class="text-danger">*</span>
                            </label>
                            <asp:DropDownList ID="ddlExamMetaType" runat="server" Width="100%" AutoPostBack="True"
                                CssClass="select2" OnSelectedIndexChanged="ddlExamMetaType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-6">
                            <label for="ddlExamType" class="form-label fw-semibold">
                                Exam Type <span class="text-danger">*</span>
                            </label>
                            <asp:DropDownList ID="ddlExamType" runat="server" Width="100%" AutoPostBack="True"
                                CssClass="select2">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-6">
                            <label for="txtTemplateMark" class="form-label fw-semibold">Exam Template Mark</label>
                            <asp:TextBox ID="txtTemplateMark" runat="server" CssClass="form-control" Enabled="false"
                                Placeholder="Exam Template Mark"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="txtExamName" class="form-label fw-semibold">
                                Exam Template Item Name <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtExamName" runat="server" CssClass="form-control"
                                Placeholder="Exam Template Item Name"></asp:TextBox>
                            <asp:Label ID="lblExamTemplateBasicItemId" Visible="false" runat="server"></asp:Label>
                        </div>

                        <div class="col-md-6">
                            <label for="txtSequence" class="form-label fw-semibold">Exam Template Item Sequence</label>
                            <asp:TextBox ID="txtSequence" runat="server" CssClass="form-control" TextMode="Number"
                                Placeholder="Exam Template Item Sequence"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="txtExamMark" class="form-label fw-semibold">
                                Exam Template Item Marks <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtExamMark" runat="server" CssClass="form-control"
                                Placeholder="Exam Template Item Mark"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="txtConvertTo" class="form-label fw-semibold">
                                Convert To Marks <span class="text-danger">*</span>
                            </label>
                            <asp:TextBox ID="txtConvertTo" runat="server" CssClass="form-control"
                                Placeholder="Convert To Marks"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label for="ddlIsVisible" class="form-label fw-semibold">
                                Visible Status for Student Panel
                            </label>
                            <asp:DropDownList ID="ddlIsVisible" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Invisible" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Visible" Value="1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-12 text-end mt-3">
                            <asp:Button ID="AddButton" runat="server" Text="Add Template Item" CssClass="btn btn-success me-2" OnClick="AddButton_Click" />
                            <asp:Button ID="UpdateButton" runat="server" Visible="false" Text="Update Template Item" CssClass="btn btn-primary me-2" OnClick="UpdateButton_Click" />
                            <asp:Button ID="CancelButton" runat="server" Visible="false" Text="Cancel Edit" CssClass="btn btn-secondary" OnClick="CancelButton_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Grid Section -->
            <div class="card">
                <div class="card-header bg-dark text-white fw-bold">
                    Exam Template Item List
                </div>
                <div class="card-body">
                    <asp:GridView ID="GvExamTemplateItem" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="gridview-container table-striped table-bordered"
                        EmptyDataText="No data found." CellPadding="4" OnRowCommand="GvExamTemplateItem_RowCommand"
                        ShowFooter="True" OnRowDataBound="GvExamTemplateItem_RowDataBound"
                        ForeColor="#333333" GridLines="None">

                        <AlternatingRowStyle BackColor="#f9f9f9" />
                        <Columns>
                            <asp:BoundField DataField="ExamTemplateBasicItemId" Visible="false" HeaderText="Id" />
                            <asp:TemplateField HeaderText="Exam Template Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblExamTemplateName"
                                        Text='<%#Eval("ExamTemplateBasicItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                        CommandName="ExamTemplateItemEdit" ToolTip="Edit Exam Template Item"
                                        CommandArgument='<%# Bind("ExamTemplateBasicItemId") %>' Text="Edit"></asp:LinkButton>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <strong>Total Marks:</strong>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ExamTemplateBasicItemMark" HeaderText="Exam Mark">
                                <FooterStyle Font-Bold="True" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Attribute2" HeaderText="Convert to Marks">
                                <FooterStyle Font-Bold="True" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ColumnSequence" HeaderText="Column Sequence" />

                            <asp:TemplateField HeaderText="Visible Status for Student Panel">
                                <ItemTemplate>
                                    <span class='<%# (string)Eval("Attribute1") == "1" ? "badge bg-success" : "badge bg-secondary" %>'>
                                        <%# (string)Eval("Attribute1") == "1" ? "Visible" : "Invisible" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                        CommandName="ExamTemplateItemDelete"
                                        CommandArgument='<%# Bind("ExamTemplateBasicItemId") %>'
                                        Text="Delete" ToolTip="Delete Exam Template Item"
                                        OnClientClick="return confirm('Do you really want to delete this exam template item?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle CssClass="table-dark" />
                        <FooterStyle BackColor="#e9ecef" Font-Bold="True" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" /> <EnableAction AnimationTarget="AddButton" Enabled="false" />
                <ScriptAction Script="InProgress();" /> <EnableAction AnimationTarget="UpdateButton" Enabled="false" />
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="AddButton" Enabled="true" />
                <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="UpdateButton" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

</asp:Content>

