<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="MarksTemplateAndPersonAssign.aspx.cs" Inherits="EMS.Module.result.MarksTemplateAndPersonAssign" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Marks Template & Marks Uploader User Assign</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

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

        function jsShowHideProgress() {
            setTimeout(function () {
                document.getElementById('divProgress').style.display = 'block';
                document.getElementById("blurOverlay").style.display = "block";
            }, 200);
            deleteCookie();

            var timeInterval = 500; // milliseconds (checks the cookie for every half second )

            var loop = setInterval(function () {
                if (IsCookieValid()) {
                    document.getElementById('divProgress').style.display =
                        'none';
                    document.getElementById("blurOverlay").style.display = "none";
                    clearInterval(loop)
                }

            }, timeInterval);
        }
        // cookies
        function deleteCookie() {
            var cook = getCookie('ExcelDownloadFlag');
            if (cook != "") {
                document.cookie = "ExcelDownloadFlag=;Path=/; expires=Thu, 01 Jan 1970 00:00:00 UTC";
            }
        }

        function IsCookieValid() {
            var cook = getCookie('ExcelDownloadFlag');
            return cook != '';
        }

        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
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


<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="PageTitle">
        <label>Marks Template & Marks Uploader User Assign</label>
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
                    <script type="text/javascript">
                        Sys.Application.add_load(initdropdown);
                    </script>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Course <span class="text-danger">*</span></b>
                            <asp:DropDownList ID="ddlCourse" CssClass="form-control select2" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-4" runat="server" id="divUpdatePanel">

                <asp:HiddenField ID="hdnSetupId" runat="server" />

                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Template</b>
                            <asp:DropDownList ID="ddlExamTemplateName" runat="server" Width="100%"
                                CssClass="select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button runat="server" ID="btnUpdateTemplate" Text="Update" OnClick="btnUpdateTemplate_Click" CssClass="btn btn-info form-control" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Uploader One</b>
                            <asp:DropDownList ID="ddlUploaderOne" runat="server" Width="100%"
                                CssClass="select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Uploader Two</b>
                            <asp:DropDownList ID="ddlUploaderTwo" runat="server" Width="100%"
                                CssClass="select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button runat="server" ID="btnUpdatePerson" Text="Update" OnClick="btnUpdatePerson_Click" CssClass="btn btn-info form-control" />
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-1">
                            <br />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger form-control" />
                        </div>

                    </div>
                </div>
            </div>

            <!-- Grid Section -->
            <div class="card mt-5">
                <div class="card-body">
                    <asp:GridView ID="gvCourseList" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="gridview-container table-striped table-bordered"
                        EmptyDataText="No data found." CellPadding="4"
                        ShowFooter="True"
                        ForeColor="#333333" GridLines="None">

                        <AlternatingRowStyle BackColor="#f9f9f9" />
                        <Columns>

                            <asp:TemplateField HeaderText="Sl." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourse"
                                        Text='<%#Eval("FormalCode")+"<br />"+Eval("CourseTitle")+"<br />"+Eval("Credit") %>'></asp:Label>
                                </ItemTemplate>
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
                                        <asp:Label runat="server" ID="lblCourseId" Visible="false" Text='<%#Eval("CourseId") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblVersionId" Visible="false" Text='<%#Eval("VersionId") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblSetupId" Visible="false" Text='<%#Eval("SetupId") %>'></asp:Label>

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Student">
                                <ItemTemplate>
                                    <div style="text-align: center">

                                        <asp:Label runat="server" ID="lblStudent"
                                            Text='<%#Eval("StudentCount") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus"
                                        Text='<%# "Submitted : "+Eval("FinalSubmitted") +"<br />"+"Published : "+Eval("Published") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Marks Template">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTemplate"
                                        Text='<%#Eval("TemplateName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Marks Uploaded By">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUploadedBy"
                                        Text='<%# "One : "+ Eval("AssignedPersonOne") +"<br />"+ "Two : "+ Eval("AssignedPersonTwo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                        ToolTip="Edit Exam Template Item" OnClick="EditButton_Click"
                                        CommandArgument='<%# Bind("SetupId") %>' Text="Edit"></asp:LinkButton>
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

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnSearch" Enabled="false" />                   
                    </Parallel>
                </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnSearch" Enabled="true" />
                        </Parallel>
                </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>
