<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="MajorMinorDepartmentSetup" CodeBehind="MajorMinorDepartmentSetup.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Students Major Assign</asp:Content>
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
        <h4>Students Major Assign</h4>
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
                            <asp:Button ID="btnView" runat="server" OnClick="btnLoad_Click" Text="Load Student" CssClass="btn btn-info" Style="margin-top: 25px;" Width="100%" />
                        </div>
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



    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <div class="card mt-4">
                <div class="card-body">

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                        ShowHeader="true" CssClass="gridview-container table-striped table-bordered" CellPadding="4" Width="100%" DataKeyNames="StudentID">

                        <Columns>

                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkActive"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lbl_1" runat="server" Text="Completed Cr."></asp:Label>
                                    <br />
                                    <hr />
                                    <asp:Label ID="Label2" runat="server" Text="C + R = T"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCompletedCr" Text='<%#Eval("CompletedCr") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <div style="text-align: center">

                                        <asp:DropDownList ID="ddlMajor1" runat="server" Width="90px"
                                            DataTextField="Name" DataValueField="NodeID">
                                        </asp:DropDownList>
                                        <br />
                                        <hr />
                                        <asp:Button ID="btnMajor1" runat="server" Width="90px" Text="Update" OnClick="btnMajor1_Click"
                                            OnClientClick="return confirm('Are you sure to done this process?');" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">

                                        <asp:Label runat="server" ID="lblMajor1NodeID" Text='<%#Eval("Major1NodeName") %>'>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <div style="text-align: center">

                                        <asp:DropDownList ID="ddlMajor2" runat="server" Width="90px"
                                            DataTextField="Name" DataValueField="NodeID">
                                        </asp:DropDownList>
                                        <br />
                                        <hr />
                                        <asp:Button ID="btnMajor2" runat="server" Width="90px" Text="Update" OnClick="btnMajor2_Click"
                                            OnClientClick="return confirm('Are you sure to done this process?');" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">

                                        <asp:Label runat="server" ID="lblMajor2NodeID" Text='<%#Eval("Major2NodeName") %>'>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

