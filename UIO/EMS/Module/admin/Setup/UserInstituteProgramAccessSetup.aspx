<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="UserInstituteProgramAccessSetup" CodeBehind="UserInstituteProgramAccessSetup.aspx.cs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Institute Program Access Setup</asp:Content>

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


        function initdropdown() {
            $('#ctl00_MainContainer_ddlUser').select2({
                allowClear: true
            });

            $('#ctl00_MainContainer_ddlInstitute').select2({
                allowClear: true
            });


            //$('#ctl00_MainContainer_ddlprgInst').select2({

            //    allowClear: true,
            //    dropdownParent: $('#ctl00_MainContainer_Panel2')
            //});

        }

    </script>

    <style>
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

        .large-checkbox {
            transform: scale(1.5); /* Increase size by 1.5x */
            transform-origin: top left;
            margin: 5px;
        }

        #ctl00_MainContainer_gvProgramList input {
            height: 25px !important;
            width: 25px !important;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <h4>Institute Program Access Setup</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <asp:UpdatePanel ID="UpdatePanel03" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(initdropdown);
            </script>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <b>User</b>
                    <asp:DropDownList ID="ddlUser" CssClass="form-control" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

            <div class="card mt-3">
                <div class="card-body">
                    <div class="row">

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Name</b>
                            <asp:Label ID="lblName" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:Label>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Role</b>
                            <asp:Label ID="lblRole" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Email</b>
                            <asp:Label ID="lblEmail" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Mobile</b>
                            <asp:Label ID="lblMobile" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card mt-3" runat="server" id="divGridView">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-4">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2" runat="server" id="divUpdateAll">
                            <br />
                            <asp:Button ID="btnUpdateAll" runat="server" Text="Update All" CssClass="btn btn-sm btn-success" OnClick="btnUpdateAll_Click" />

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-lg-7 col-md-7 col-sm-7" style="position: static">
                            <asp:GridView ID="gvInstitute" runat="server" AutoGenerateColumns="False"
                                AllowPaging="false" AllowSorting="True" CssClass="gridview-container table-striped table-bordered" EmptyDataText="No data">
                                <RowStyle Height="10px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSL" Text='<%# Container.DataItemIndex + 1 %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Institute Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblName" Text='<%#Eval("InstituteName") %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Program">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTP" Text='<%#Eval("Attribute1") %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Access Given">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAG" Text='<%#Eval("Attribute2") %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="text-align: center">

                                                <asp:Button ID="btnSelect" runat="server" Text="View Programs" CssClass="btn btn-sm btn-primary" OnClick="btnSelect_Click" CommandArgument='<%# Eval("InstituteId") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5" style="position: static">

                            <asp:GridView ID="gvProgramList" runat="server" AutoGenerateColumns="False" DataKeyNames="ProgramID"
                                AllowPaging="false" AllowSorting="True" CssClass="gridview-container table-striped table-bordered" EmptyDataText="Click View Programs to show">
                                <RowStyle Height="10px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSL" Text='<%# Container.DataItemIndex + 1 %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Program Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPrgName" Text='<%#Eval("ShortName") %>' ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Access Given">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGiven" Text='<%# Convert.ToInt32(Eval("Attribute1"))==1 ? "Yes" : "No" %>' ForeColor='<%# Convert.ToInt32(Eval("Attribute1"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                <b>All</b>
                                                <br />
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Convert.ToInt32(Eval("Attribute1"))==1 ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                <asp:HiddenField ID="hdnProgramId" runat="server" Value='<%#Eval("ProgramID") %>' />
                                            </div>
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


    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel03" runat="server">
        <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnProcessGroup" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnProcessGroup" Enabled="true" /></Parallel></OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

</asp:Content>

