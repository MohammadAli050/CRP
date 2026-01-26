<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="CourseRegistrationForwardByInstitute.aspx.cs" Inherits="EMS.Module.Registration.Approval.CourseRegistrationForwardByInstitute" %>

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
            return confirm("Are you sure you want to forward selected students for selected courses?");
        }

        function toggleAllCourse(source) {
            var checkboxes = document.querySelectorAll('[id$=chkCourseSelect]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
        }

        //function initdropdown() {
        //    $('#ctl00_MainContainer_ddlInstitute').select2({
        //        allowClear: true
        //    });
        //}

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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">


    <div class="PageTitle">
        <h4>Course Registration Forward By Institute </h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                   <%-- <script type="text/javascript">
                        Sys.Application.add_load(initdropdown);
                    </script>--%>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program <span class="text-danger">*</span> </b>
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

                        <div class="col-lg-2 col-md-2 col-sm-2" style="margin-top: 5px">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" CssClass="btn btn-info form-control" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-3" runat="server" id="divTimeline">
                <div class="alert alert-warning text-center">
                    <asp:Label runat="server" ID="lblTimelinemsg" Font-Bold="true" Font-Italic="true" Text="" ></asp:Label>
                </div>
            </div>

            <div class="card mt-3" runat="server" id="divGridView">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnRegistration" runat="server" Text="Forward to COE" OnClientClick="return confirmApproval();" OnClick="btnRegistration_Click" CssClass="btn btn-danger form-control" />
                        </div>
                    </div>

                    <div class="card-body table-responsive p-0">

                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6" style="position: static">
                            <b>Student List</b>
                            <br />
                            <asp:GridView ID="GvStudent" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentID"
                                CssClass="table table-hover table-bordered table-striped mb-0" CellPadding="4" Width="100%">
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
                                                Forwarded Course List
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormalCodeList" Text='<%#Eval("FormalCodeList")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6" style="position: static">
                            <b>Course List</b>

                            <br />
                            <asp:GridView ID="gvCourseList" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseID"
                                CssClass="gridview-container table-striped table-bordered" CellPadding="4" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                <asp:CheckBox ID="chkCourseHeader" runat="server" onclick="toggleAllCourse(this)" Text="All" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:CheckBox ID="chkCourseSelect" runat="server" />
                                                <asp:Label runat="server" ID="lblCourseID" Text='<%#Eval("CourseID")  %>' Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lblVersionID" Text='<%#Eval("VersionID")  %>' Visible="false"></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Course Code">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Course Title">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Credit
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCredit" Text='<%#Eval("Credit")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Priority
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPriority" Text='<%#Eval("Priority")  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Year No
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtYearNo" Text='<%#Eval("YearNo")  %>' CssClass="form-control text-center"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                Semester No
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtSemesterNo" Text='<%#Eval("SemesterNo")  %>' CssClass="form-control text-center"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    </div>

                </div>
            </div>

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
