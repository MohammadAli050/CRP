<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="CurriculumDistribution.aspx.cs" Inherits="EMS.SyllabusMan.CurriculumDistribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Curriculum Distribution
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .font {
            font-size: 12px;
        }

        .cursor {
            cursor: pointer;
        }

        .auto-style3 {
            width: 100px;
        }

        .button_Page_Re_load {
            height: 38px;
            width: 167px;
            border-radius: 5px;
            padding-left: 23px;
            background-color: #368445;
            color: white;
        }

        .button_Clear_save {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 23px;
            background-color: #368445;
            color: white;
        }

        .button_delete {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 17px;
            background-color: #d7393b;
            color: white;
        }

        #ctl00_MainContainer_Button1 {
            height: 40px !important;
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

        .sweet-alert {
            z-index: 1000000;
            background-color: #cbcbcb;
        }

        /*#ctl00_MainContainer_gvCourseList td input {
            height: 25px;
            width: 25px;
        }*/
    </style>

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

        function Search(x) {


            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("ctl00_MainContainer_gvCourseList");
            tr = table.getElementsByTagName("tr");

            for (i = 0; i < tr.length; i++) {
                if (tr) {
                    txtValue = tr[i].innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                    tr[0].style.display = "";


                }

            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div class="PageTitle">
        <label>Curriculum Distribution</label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000" Width="50%"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program</b>
                            <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lblProgramId" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Tree</b>
                            <asp:DropDownList ID="ddlTree" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlTree_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lblTreeId" runat="server" Visible="false"></asp:Label>

                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Distribution</b>
                            <asp:DropDownList ID="ddlCalenderDistribution" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlCalenderDistribution_SelectedIndexChanged"></asp:DropDownList>
                            <asp:Label ID="lblCalenderDistributionId" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <br />
                            <asp:Button ID="btnAddNewDistName" runat="server" Text="Add New Distribution" CssClass="btn btn-info" Height="38px" Visible="false" OnClick="btnAddNewDistName_Click" />

                        </div>
                    </div>
                </div>

            </div>

            <div class="row mt-5">
                <div class="col-lg-2 col-md-2 col-sm-2">

                    <asp:Button ID="btnReloadTree" runat="server" Text="Page Re-load " Visible="false" Class="button_Page_Re_load" OnClick="btnReloadTree_Click" /></td>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">

                    <asp:Button ID="btnClearFields" runat="server" Text=" Clear " Visible="false" Class="button_Clear_save" OnClick="btnClearFields_Click" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td valign="top">
                            <div style="height: 540px; width: 427px; overflow-y: auto; float: left; border-style: solid; border-width: 2px; border-color: #FFA500; padding: 10px;">
                                <asp:Label ID="lblTreeView" runat="server" Font-Bold="true" Text="Tree View"></asp:Label>
                                <asp:Panel ID="pnlTreeView" runat="server">
                                    <asp:TreeView ID="tvwCalendar" runat="server" Width="100%" ShowLines="True"
                                        Height="98%" OnSelectedNodeChanged="tvwCalendar_SelectedNodeChanged"
                                        LineImagesFolder="~/TreeLineImages" TabIndex="4">
                                        <ParentNodeStyle ForeColor="#660066" />
                                        <HoverNodeStyle ForeColor="#FF6600" />
                                        <SelectedNodeStyle ForeColor="Lime" Font-Bold="True" Font-Underline="True" />
                                        <RootNodeStyle ForeColor="#000066" />
                                        <LeafNodeStyle ForeColor="#006699" />
                                    </asp:TreeView>
                                </asp:Panel>
                            </div>
                        </td>
                        <td class="auto-style5">&nbsp;</td>
                        <td valign="top">
                            <div style="height: 540px; width: 400px; overflow-y: auto; float: left; border-style: solid; border-width: 2px; border-color: #FFA500; padding: 10px;">
                                <asp:Panel ID="pnlAddTreeItem" runat="server">
                                    <asp:Label ID="lblAddTreeItem" runat="server" Font-Bold="true" Text="Add Tree Item"></asp:Label>
                                    <asp:RadioButtonList ID="rbAddTreeItem" RepeatDirection="Horizontal" Style="display: inline" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rbAddTreeItem_SelectedIndexChanged">
                                        <asp:ListItem Text="Course" Value="1" Selected="True" />
                                        <asp:ListItem Text="Node" Value="2" />
                                    </asp:RadioButtonList>
                                    <asp:Panel ID="pnlAddCourse" Visible="true" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblAddCourseTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddCourseTrimester" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddCourse" runat="server" Text="Course"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddCourse" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddCourseCrComplete" runat="server" Text="Cr. To Complete"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddCourseCrComplete" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddCoursePriority" runat="server" Text="Priority"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddCoursePriority" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddCourseParentNode" runat="server" Text="Parent Node"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddCourseParentNode" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="Add Multiple" CssClass="btn btn-info form-control" OnClick="Button1_Click" OnClientClick="this.style.display = 'none'" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddCourseSave" runat="server" Text=" Save " Class="button_Clear_save" OnClick="btnAddCourseSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAddNode" Visible="false" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblAddNodeTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddNodeTrimester" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodeName" runat="server" Text="Node Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddNodeName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodeNodes" runat="server" Text="Nodes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddNodeNodes" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodeCrComplete" runat="server" Text="Cr. To Complete"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddNodeCrComplete" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodePriority" runat="server" Text="Priority"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddNodePriority" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodeIsMajor" runat="server" Text="Is Major Related"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkAddNodeIsMajor" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddNodeParentNode" runat="server" Text="Parent Node"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAddNodeParentNode" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnAddNodeSave" runat="server" Text=" Save " Class="button_Clear_save" OnClick="btnAddNodeSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlEditTreeItem" runat="server">
                                    <asp:Label ID="lblEditTreeItem" runat="server" Font-Bold="true" Text="Edit Tree Item"></asp:Label>
                                    <asp:RadioButtonList ID="rbEditTreeItem" RepeatDirection="Horizontal" Style="display: inline" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rbEditTreeItem_SelectedIndexChanged">
                                        <asp:ListItem Text="Course" Value="1" Selected="True" />
                                        <asp:ListItem Text="Node" Value="2" />
                                    </asp:RadioButtonList>
                                    <asp:Panel ID="pnlEditCourse" Visible="true" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblEditCourseTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditCourseTrimester" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEditCourseTrimester_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditCourseOld" runat="server" Text="Current Courses"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditCourseOld" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEditCourseOld_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditCourseNew" runat="server" Text="New Courses"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditCourseNew" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditCourseCrComplete" runat="server" Text="Cr. To Complete"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEditCourseCrComplete" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditCoursePriority" runat="server" Text="Priority"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEditCoursePriority" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditCourseParentNode" runat="server" Text="Parent Node"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditCourseParentNode" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnEditCourseSave" runat="server" Text=" Save " Class="button_Clear_save" OnClick="btnEditCourseSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEditNode" Visible="false" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblEditNodeTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditNodeTrimester" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEditNodeTrimester_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeOld" runat="server" Text="Current Nodes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditNodeOld" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEditNodeOld_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeNew" runat="server" Text="New Nodes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditNodeNew" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeName" runat="server" Text="Node Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEditNodeName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeCrComplete" runat="server" Text="Cr. To Complete"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEditNodeCrComplete" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodePriority" runat="server" Text="Priority"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEditNodePriority" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeIsMajor" runat="server" Text="Is Major Related"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEditNodeIsMajor" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEditNodeParentNode" runat="server" Text="Parent Node"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEditNodeParentNode" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnEditNodeSave" runat="server" Text=" Save " Class="button_Clear_save" OnClick="btnEditNodeSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlDeleteTreeItem" runat="server">
                                    <asp:Label ID="lblDeleteTreeItem" runat="server" Font-Bold="true" Text="Remove Tree Item"></asp:Label>
                                    <asp:RadioButtonList ID="rbDeleteTreeItem" RepeatDirection="Horizontal" Style="display: inline" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rbDeleteTreeItem_SelectedIndexChanged">
                                        <asp:ListItem Text="Course" Value="1" Selected="True" />
                                        <asp:ListItem Text="Node" Value="2" />
                                    </asp:RadioButtonList>
                                    <asp:Panel ID="pnlDeleteCourse" Visible="true" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblDeleteCourseTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeleteCourseTrimester" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeleteCourseTrimester_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDeleteCourse" runat="server" Text="Courses"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeleteCourse" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnDeleteCourse" runat="server" Text=" Remove " Class="button_delete" OnClick="btnDeleteCourse_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDeleteNode" Visible="false" runat="server">
                                        <table class="table" style="width: 100%; height: auto;">
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblDeleteNodeTrimester" runat="server" Text="Trimester"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeleteNodeTrimester" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeleteNodeTrimester_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDeleteNode" runat="server" Text="Nodes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeleteNode" Width="180px" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnDeleteNode" runat="server" Text=" Remove " Class="button_delete" OnClick="btnDeleteNode_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                <div style="padding: 5px;">
                    <fieldset style="padding: 5px; border: 2px solid #ff9933;">
                        <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Message</legend>
                        <div style="padding: 5px;">
                            <div class="Message-Area">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel2" runat="server" Visible="true">
                                            <asp:Label ID="lblNew" runat="server" Text="Message : "></asp:Label>
                                            <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000"></asp:Label>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:Button ID="CancelButton" runat="server" Text="Close" CssClass="cursor" /></td>
                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanelAddTreeDistribution" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnAddTreeDistributionPopUp" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderAddTreeDistribution" runat="server" TargetControlID="btnAddTreeDistributionPopUp" PopupControlID="pnlAddTreeDistribution"
                CancelControlID="btnCancelAddTreeDistributionPopUp" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlAddTreeDistribution" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                <div style="padding: 5px;">
                    <fieldset style="padding: 5px; border: 2px solid #ff9933;">
                        <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Message</legend>
                        <div style="padding: 5px;">
                            <div class="Message-Area">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel4" runat="server" Visible="true">
                                            <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                                            <asp:Label ID="lblMsgAddTreeDistributionPopUp" runat="server" ForeColor="#CC0000"></asp:Label>
                                            <br />

                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                                            <b>Syllabus Name</b>
                                                            <asp:TextBox runat="server" ID="txtDistribution" Placeholder="New Syllabus Name" Width="100%" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                                            <b>Semester Type</b>
                                                            <asp:DropDownList ID="ddlCalendarType" runat="server" Width="100%" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 10px">
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                                            <asp:Button ID="btnSaveDistName" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSaveDistName_Clicked" />

                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                                            <asp:Button ID="btnCancelAddTreeDistributionPopUp" runat="server" Text="Close" OnClick="btnCancelAddTreeDistributionPopUp_Click" CssClass="btn btn-danger" />

                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button2" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="modalPopupCourseList" runat="server" TargetControlID="Button2" PopupControlID="Panel3"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="Panel3" Style="display: none; padding: 5px; overflow-y: scroll" BackColor="White" Width="65%" Height="600px">


                <div class="card">
                    <div class="card-body">

                        <div class="card">
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: center">
                                        <b style="color: blue">Please check the courses you want to add</b>
                                    </div>

                                </div>

                                <div class="row" style="margin-top: 10px">
                                    <div class="col-lg-5 col-md-5 col-sm-5">
                                        <b>Trimester</b>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server"
                                            ControlToValidate="ddlSemesterNo" ErrorMessage="Required" Font-Size="15pt" Font-Bold="true"
                                            ForeColor="Red" Display="Dynamic" ValueToCompare="0" Operator="NotEqual" CssClass="blink"
                                            ValidationGroup="VG1"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlSemesterNo" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1"></div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <br />
                                        <input type="text" class="form-control" id="myInput" onkeyup="javascript: Search( this.value );"
                                            placeholder="Type Course Code/Title/Credit" title="Type Course Code/Title/Credit" style="color: red; width: 100%">
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnSave" ValidationGroup="VG1" Text="SAVE" CssClass="btn btn-info btn-sm" OnClick="btnSave2_Click" Style="display: inline-block; width: 100%; text-align: center;" />

                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="Button4" Text="CLOSE" OnClick="Button3_Click" CssClass="btn btn-danger btn-sm" Style="display: inline-block; width: 100%; text-align: center;" />
                                    </div>
                                </div>
                                <br />

                                <asp:GridView runat="server" ID="gvCourseList" AllowSorting="True" CssClass="table-bordered" OnRowDataBound="gvCourseList_RowDataBound"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <HeaderStyle BackColor="#3f2c7b" ForeColor="White" Font-Bold="True" />
                                    <FooterStyle BackColor="#3f2c7b" ForeColor="White" Font-Bold="True" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <RowStyle Height="10px" />

                                    <Columns>


                                        <asp:TemplateField HeaderText="Formal Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("CourseInfo")==null ? "" : Eval("CourseInfo.FormalCode")  %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("CourseInfo")==null ? "" : Eval("CourseInfo.VersionCode")  %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("CourseInfo")==null ? "" : Eval("CourseInfo.Title")  %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredit" Text='<%#Eval("CourseInfo")==null ? "" : Eval("CourseInfo.Credits") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center">

                                            <ItemTemplate>
                                                <div style="text-align: center; width: 40px">
                                                    <asp:HiddenField ID="hdnCourseID" runat="server" Value='<%#Eval("CourseId") %>' />
                                                    <asp:HiddenField ID="hdnVersionID" runat="server" Value='<%#Eval("VersionId") %>' />
                                                    <asp:HiddenField ID="hdnNodeCourseId" runat="server" Value='<%#Eval("NodeCourseId") %>' />

                                                    <asp:CheckBox runat="server" ID="ChkChecked"></asp:CheckBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPriority" Text="" placeholder="1111" TextMode="Number" CssClass="form-control" ForeColor="Black"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Parent Node">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlParentNode" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                                <br />
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnSave2" ValidationGroup="VG1" Text="SAVE" CssClass="btn btn-info btn-sm" OnClick="btnSave2_Click" Style="display: inline-block; width: 100%; text-align: center;" />

                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="Button3" Text="CLOSE" OnClick="Button3_Click" CssClass="btn btn-danger btn-sm" Style="display: inline-block; width: 100%; text-align: center;" />
                                    </div>
                                </div>

                            </div>
                        </div>



                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
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

</asp:Content>
