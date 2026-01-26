<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_TreeMaster" CodeBehind="TreeMaster.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Src="~/UserControls/CoursePicker/CoursePickerCtl.ascx" TagName="CoursePicker" TagPrefix="uctl" %>
<%@ Register Src="~/UserControls/CourseSelector.ascx" TagName="CourseSelector" TagPrefix="uctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Tree Master
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .button_delete {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 26px;
            background-color: #d7393b;
            color: white;
        }

        .button_all {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 15px;
            background-color: #368445;
            color: white;
        }

        .button_edit {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 35px;
            background-color: #368445;
            color: white;
        }

        .button_course {
            height: 38px;
            width: 100px;
            border-radius: 5px;
            padding-left: 10px;
            background-color: #368445;
            color: white;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        td input {
            height: 25px;
            width: 100px;
        }
    </style>
    <script type="text/javascript">

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
    <div class="well" style="margin-top: 20px;">
        <div>
            <div class="PageTitle">
                <label>Program Tree Setup</label>
            </div>

                <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="Message-Area" style="height: 36px;">
                        <asp:Label ID="Label3" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <b>Program</b>
                                    <asp:DropDownList ID="ddlPrograms" AutoPostBack="true" runat="server" Width="100%" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <b>Tree</b>
                                    <asp:DropDownList ID="ddlTree" AutoPostBack="true" runat="server" Width="100%" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlTree_SelectedIndexChanged" Enabled="False"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Panel ID="pnlUpperCont" runat="server" Width="100%" Height="98%">
                        <div class="card" style="margin-top: 5px">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="butAddRoot" runat="server" Text="Add Root" OnClick="butAddRoot_Click"
                                            ToolTip="Click this button to add the root node of a tree." AlternateText="Add Root" Class="button_all"
                                            TabIndex="3" />
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="butAddNode" runat="server" Text="Add Node" OnClick="butAddNode_Click" Class="button_all"
                                            ToolTip="Click this button to add a node under a selected root or node." AlternateText="Add Node"
                                            TabIndex="4" />
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="butAddSet" runat="server" Text="Add Set" OnClick="butAddSet_Click" Class="button_all"
                                            ToolTip="Click this button to add a virtual node set detail under a selected node." AlternateText="Add Set"
                                            TabIndex="5" />
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="butAddCourse" runat="server" Text="Add Course" OnClick="butAddCourse_Click" Class="button_course"
                                            ToolTip="Click this button to add a course under a selected node." AlternateText="Add Course" OnClientClick="this.style.display = 'none'"
                                            TabIndex="6" />
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" Class="button_edit" Style="text-align: left"
                                            ToolTip="Click this button to edit a selected node or course. Sets are not editable." AlternateText="Edit Course"
                                            TabIndex="6" />
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button ID="btnDel" runat="server" Text="Delete" OnClick="btnDel_Click" Class="button_delete"
                                            ToolTip="Click this button to delete a selected node, course, root or set." AlternateText="Add Course"
                                            TabIndex="7" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <div class="Message-Area">
                        <table style="width: 944px;">

                            <tr>
                                <td style="width: 50%; border-style: solid; border-color: Gray; border-width: 1px; height: 39px; vertical-align: middle;" colspan="2">
                                    <asp:Label ID="lblCaption" runat="server" Width="100%" Height="98%"
                                        Font-Names="Verdana" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid Gray;" colspan="2" class="style3" align="left">
                                    <asp:Panel ID="pnlTree" runat="server" Width="550" Height="99%" ScrollBars="Vertical">
                                        <asp:TreeView ID="tvwMaster" runat="server" ShowLines="True"
                                            Height="98%" OnSelectedNodeChanged="tvwMaster_SelectedNodeChanged"
                                            LineImagesFolder="~/TreeLineImages" TabIndex="9">
                                            <ParentNodeStyle ForeColor="#660066" />
                                            <HoverNodeStyle ForeColor="#FF6600" />
                                            <SelectedNodeStyle ForeColor="Lime" Font-Bold="True" Font-Underline="True" />
                                            <RootNodeStyle ForeColor="#000066" />
                                            <LeafNodeStyle ForeColor="#006699" />
                                        </asp:TreeView>
                                    </asp:Panel>
                                </td>
                                <td style="border: 1px solid Gray;" colspan="2" class="style3">
                                    <dxtc:ASPxPageControl ID="pcTabControl" runat="server" ActiveTabIndex="0"
                                        Height="356px" Width="470px">
                                        <TabPages>
                                            <dxtc:TabPage Text="General Information" Name="GI">
                                                <ActiveTabStyle HorizontalAlign="Center">
                                                </ActiveTabStyle>
                                                <TabStyle HorizontalAlign="Center">
                                                </TabStyle>
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <asp:Panel ID="pnlControl" runat="server" Height="99%" Visible="False"
                                                            HorizontalAlign="Left" ScrollBars="Vertical">
                                                            <table style="width: 99%;">
                                                                <%--height: 421px;--%>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlReg" runat="server" Height="116%" Visible="False"
                                                                            Style="margin-bottom: 6px" BorderStyle="None">
                                                                            <table style="width: 100%; height: 187px;">
                                                                                <tr>
                                                                                    <td align="left" class="style12">
                                                                                        <asp:RequiredFieldValidator ID="rfvNodeName" runat="server"
                                                                                            ControlToValidate="txtName" ErrorMessage="Node Name can not be empty"
                                                                                            ValidationGroup="ValidateTree">*</asp:RequiredFieldValidator>
                                                                                        <asp:Label ID="lblName" runat="server" Text="Name" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" colspan="3">
                                                                                        <asp:TextBox ID="txtName" runat="server" Width="200px" TabIndex="10"
                                                                                            MaxLength="150" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="style12">
                                                                                        <asp:Label ID="lblRequiredUnits" runat="server" Text="Required Credits"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <dxe:ASPxSpinEdit ID="spnRequUnits" runat="server" DecimalPlaces="2"
                                                                                            Height="21px" Increment="0.1" LargeIncrement="1" Number="0" Visible="False"
                                                                                            Width="100px" TabIndex="11">
                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                            </SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:Label ID="lblPass" runat="server" Text="Passing GPA" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <dxe:ASPxSpinEdit ID="spnPass" runat="server" Height="21px" Increment="0.1"
                                                                                            LargeIncrement="1" Number="0" Visible="False" Width="90px" TabIndex="12">
                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                            </SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style12" align="left">
                                                                                        <asp:Label ID="lblOperator" runat="server" Text="Select Operator"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <asp:DropDownList ID="ddlOperators" runat="server" AutoPostBack="true"
                                                                                            Width="100px" TabIndex="13" Visible="False">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style12" align="left" colspan="4">
                                                                                        <uctl:CourseSelector ID="ctlCourseSelect" runat="server"
                                                                                            DropDownListTabIndex="16" FindButtonTabIndex="15" TextCodeTabIndex="14"
                                                                                            Visible="False" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="style12">
                                                                                        <asp:Label ID="lblPriority" runat="server" Text="Priority" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <dxe:ASPxSpinEdit ID="spnPriority" runat="server" Height="21px" Number="0"
                                                                                            Visible="False" Width="100px" TabIndex="17">
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style12" align="left">
                                                                                        <asp:Label ID="lblNode" runat="server" Text="Select Node" Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <asp:DropDownList ID="ddlNodes" runat="server" AutoPostBack="true" Width="270px"
                                                                                            TabIndex="18" Visible="False">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td align="left" class="style12">
                                                                                        <asp:Label ID="lblMinCOurse" runat="server" Text="Minimum Courses"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit ID="spnMinCourse" runat="server" Height="21px" Number="0"
                                                                                            Visible="False" Width="90px" TabIndex="19">
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:Label ID="lblMaxCoure" runat="server" Text="Maximum Courses"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit ID="spnMaxCourse" runat="server" Height="21px" Number="0"
                                                                                            Visible="False" Width="90px" TabIndex="20">
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="style10">
                                                                                        <asp:Label ID="lblMinCredits" runat="server" Text="Minimum Credits"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        <dxe:ASPxSpinEdit ID="spnMinCredits" runat="server" Height="21px"
                                                                                            Increment="0.1" LargeIncrement="1" Number="0" Visible="False" Width="90px" TabIndex="21">
                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                            </SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td align="left" class="style11">
                                                                                        <asp:Label ID="lblMaxCredits" runat="server" Text="Maximum Credits"
                                                                                            Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        <dxe:ASPxSpinEdit ID="spnMaxCreds" runat="server" Height="21px" Increment="0.1"
                                                                                            LargeIncrement="1" Number="0" Visible="False" Width="90px" TabIndex="22">
                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                            </SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="style10">
                                                                                        <asp:CheckBox ID="chkIsBundle" runat="server" Text="Is Bundle"
                                                                                            Visible="False" TabIndex="23" />
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="Is Active"
                                                                                            Visible="False" TabIndex="24" />
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        <asp:CheckBox ID="chkIsAssoc" runat="server" Checked="false" Text="Is Accosiated"
                                                                                            Visible="False" TabIndex="25" />
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        <asp:CheckBox ID="chkIsMajor" runat="server" Checked="false" Text="Is Major"
                                                                                            Visible="False" TabIndex="25" />
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlSet" runat="server" Height="99%" Visible="False">
                                                                            <table style="width: 100%; height: 231px;">
                                                                                <tr>
                                                                                    <td class="style9" align="left">
                                                                                        <asp:CompareValidator ID="cvNetNo" runat="server" ErrorMessage="Set No must be numeric"
                                                                                            Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidateTree"
                                                                                            ControlToValidate="txtVNodeSet">*</asp:CompareValidator>
                                                                                        <asp:RequiredFieldValidator ID="rfvSetNo" runat="server"
                                                                                            ControlToValidate="txtVNodeSet" ErrorMessage="Set number can not be empty."
                                                                                            ValidationGroup="ValidateTree">*</asp:RequiredFieldValidator>
                                                                                        <asp:Label ID="lblSet" runat="server" Text="Node Set No"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtVNodeSet" runat="server" Width="100px"
                                                                                            onkeypress="return isNumber(event)" TabIndex="25" MaxLength="10"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRequiredUnitsSet" runat="server" Text="Required Credits"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit ID="spnRequUnitsSet" runat="server" DecimalPlaces="2"
                                                                                            Height="21px" Increment="0.1" LargeIncrement="1" Number="0" Visible="False"
                                                                                            Width="100px" TabIndex="26">
                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                            </SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style9" align="left">
                                                                                        <asp:Label ID="lblSetOperator" runat="server" Text="Select Set Operator"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <asp:DropDownList ID="ddlSetOptrs" runat="server" AutoPostBack="true"
                                                                                            Width="100px" TabIndex="27">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="4">
                                                                                        <asp:CheckBox ID="chkStudSpec" runat="server" AutoPostBack="True"
                                                                                            OnCheckedChanged="chkStudSpec_CheckedChanged" TabIndex="28"
                                                                                            Text="Student Specific Major" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" class="style9">
                                                                                        <asp:RadioButton ID="rbtSetCourse" runat="server" ForeColor="#000066"
                                                                                            Text="Operand Course" AutoPostBack="True"
                                                                                            OnCheckedChanged="rbtSetCourse_CheckedChanged" TabIndex="29" />
                                                                                    </td>
                                                                                    <td align="center" colspan="3">
                                                                                        <asp:RadioButton ID="rbtSetNode" runat="server" ForeColor="#000066"
                                                                                            Text="Operand Node" AutoPostBack="True"
                                                                                            OnCheckedChanged="rbtSetNode_CheckedChanged" TabIndex="30" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="style9">
                                                                                        <asp:Label ID="lblOpCour" runat="server" Text="Select Operand Course"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <dxe:ASPxComboBox ID="ddlSetCrs" runat="server" AutoPostBack="True"
                                                                                            DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                                                            OnSelectedIndexChanged="ddlSetCrs_SelectedIndexChanged"
                                                                                            ValueType="System.String" Width="270px" TabIndex="31">
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style9" align="left">
                                                                                        <asp:Label ID="lblOpNode" runat="server" Text="Select Operand Node"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <dxe:ASPxComboBox ID="ddlSetNode" runat="server" AutoPostBack="True"
                                                                                            DropDownStyle="DropDown" EnableIncrementalFiltering="True" Height="20px"
                                                                                            ValueType="System.String" Width="270px" TabIndex="32">
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td align="left" class="style9">
                                                                                        <asp:Label ID="lblWild" runat="server" Text="Wild Card"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="3">
                                                                                        <asp:TextBox ID="txtWildCard" runat="server" Width="100px" TabIndex="33"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                            <dxtc:TabPage Text="Set Prerequisites" Name="PREREQ">
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <asp:Panel ID="pnlPREREQ" runat="server" Height="98%" ScrollBars="Vertical" Enabled="true">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="width: 25%;">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Prerequisit Name " Width="100%"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%;">
                                                                                    <dxe:ASPxComboBox ID="cboPreReqName" runat="server" AutoPostBack="True"
                                                                                        DropDownStyle="DropDown" EnableIncrementalFiltering="True"
                                                                                        ValueType="System.String" Width="100%"
                                                                                        OnSelectedIndexChanged="cboPreReqName_SelectedIndexChanged" TabIndex="34">
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 10%;" align="right">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Name " Width="100%"></asp:Label>
                                                                                </td>
                                                                                <td>

                                                                                    <asp:TextBox ID="txtPreReqName" runat="server" Width="89%" TabIndex="35"></asp:TextBox>

                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 30%;">
                                                                        <asp:Button ID="btnPreReqCourse" runat="server" Text="Set Course"
                                                                            OnClick="btnPreReqCourse_Click" TabIndex="36" />
                                                                    </td>
                                                                    <td style="width: 70%;">
                                                                        <asp:Button ID="btnPreReqNode" runat="server" Text="Set Node"
                                                                            OnClick="btnPreReqNode_Click" TabIndex="37" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pnlPreReqArea" runat="server">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gdvPreReq" runat="server"
                                                                                            AutoGenerateColumns="False" Width="408px" KeyFieldName="Id"
                                                                                            OnCellEditorInitialize="gdvPreReq_CellEditorInitialize" TabIndex="38"
                                                                                            OnRowInserted="gdvPreReq_RowInserted" OnRowInserting="gdvPreReq_RowInserting"
                                                                                            OnRowUpdated="gdvPreReq_RowUpdated" OnRowUpdating="gdvPreReq_RowUpdating"
                                                                                            OnCancelRowEditing="gdvPreReq_CancelRowEditing"
                                                                                            OnInitNewRow="gdvPreReq_InitNewRow"
                                                                                            OnStartRowEditing="gdvPreReq_StartRowEditing"
                                                                                            OnRowDeleted="gdvPreReq_RowDeleted" OnRowDeleting="gdvPreReq_RowDeleting" OnRowValidating="gdvPreReq_RowValidating">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Button" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="True">
                                                                                                    <EditButton Visible="True">
                                                                                                    </EditButton>
                                                                                                    <NewButton Text="Add" Visible="True">
                                                                                                    </NewButton>
                                                                                                    <DeleteButton Visible="True">
                                                                                                    </DeleteButton>
                                                                                                    <ClearFilterButton Visible="True">
                                                                                                    </ClearFilterButton>
                                                                                                    <CellStyle HorizontalAlign="Left" Wrap="True">
                                                                                                    </CellStyle>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Course" FieldName="NodeCourse.ChildCourse.FullCodeAndCourse"
                                                                                                    VisibleIndex="1">
                                                                                                    <PropertiesComboBox ValueType="System.Int32" EnableSynchronization="False"
                                                                                                        EnableIncrementalFiltering="True">
                                                                                                    </PropertiesComboBox>
                                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Node" FieldName="Node.Name"
                                                                                                    VisibleIndex="2">
                                                                                                    <PropertiesComboBox ValueType="System.Int32" EnableSynchronization="False"
                                                                                                        EnableIncrementalFiltering="True">
                                                                                                    </PropertiesComboBox>
                                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Required Units"
                                                                                                    VisibleIndex="3" FieldName="ReqCredits" PropertiesSpinEdit-NullText="0.00" PropertiesSpinEdit-Increment=".25" PropertiesSpinEdit-LargeIncrement="1">
                                                                                                    <PropertiesSpinEdit DisplayFormatString="g" SpinButtons-ShowLargeIncrementButtons="true">
                                                                                                        <SpinButtons ShowLargeIncrementButtons="True"></SpinButtons>
                                                                                                    </PropertiesSpinEdit>
                                                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Operator" FieldName="Operator.Name"
                                                                                                    VisibleIndex="4">
                                                                                                    <PropertiesComboBox ValueType="System.String" EnableSynchronization="False" EnableIncrementalFiltering="True"></PropertiesComboBox>
                                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing Mode="Inline" />
                                                                                            <Settings ShowVerticalScrollBar="true" />
                                                                                        </dxwgv:ASPxGridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td style="width: 60%;">
                                                                                                    <asp:Label ID="lblOccurance" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="width: 20%;">

                                                                                                    <dxe:ASPxSpinEdit ID="speOperatorcourseOccurance" runat="server" AutoPostBack="True"
                                                                                                        Height="21px" Number="0" Width="100%" TabIndex="39">
                                                                                                    </dxe:ASPxSpinEdit>

                                                                                                </td>
                                                                                                <td style="width: 20%;">
                                                                                                    <dxe:ASPxSpinEdit ID="speOperatorNodeOccurance" runat="server" AutoPostBack="True"
                                                                                                        Height="21px" Number="0" Width="100%" TabIndex="40">
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                        </TabPages>
                                    </dxtc:ASPxPageControl>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%; border: 1px solid Gray;">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlSavCan" runat="server" Height="99%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%;"></td>
                                                            <td align="center">

                                                                <asp:ImageButton ID="butSave" runat="server"
                                                                    AlternateText="Click this button to save the data on the interface to database"
                                                                    ImageUrl="~/ButtonImages/Save.jpg" OnClick="butSave_Click" TabIndex="41"
                                                                    ValidationGroup="ValidateTree" Style="height: 20px" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="butCancel" runat="server" AlternateText="Cancel"
                                                                    ImageUrl="~/ButtonImages/Cancel.jpg" OnClick="butCancel_Click"
                                                                    TabIndex="42" Style="height: 20px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid Gray; vertical-align: middle;" colspan="4" class="style4">
                                    <asp:ValidationSummary ID="vsTreeMaster" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="ValidateTree" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="modalPopupCourseList" runat="server" TargetControlID="Button1" PopupControlID="Panel2"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="Panel2" Style="display: none; padding: 5px; overflow-y: scroll" BackColor="White" Width="50%" Height="600px">


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
                                        <b>Program</b>
                                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1"></div>
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <br />
                                        <input type="text" class="form-control" id="myInput" onkeyup="javascript: Search( this.value );"
                                            placeholder="Type Program Name/Course Code/Title/Credit" title="Type Program Name/Course Code/Title/Credit" style="color: red; width: 100%">
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnSave" Text="SAVE" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Style="display: inline-block; width: 100%; text-align: center;" />

                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="Button4" Text="CLOSE" OnClick="Button2_Click" CssClass="btn btn-danger btn-sm" Style="display: inline-block; width: 100%; text-align: center;" />
                                    </div>
                                </div>
                                <br />

                                <asp:GridView runat="server" ID="gvCourseList" AllowSorting="True" CssClass="gridview-container table-striped table-bordered"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <HeaderStyle BackColor="#3f2c7b" ForeColor="White" Font-Bold="True" />
                                    <FooterStyle BackColor="#3f2c7b" ForeColor="White" Font-Bold="True" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <RowStyle Height="10px" />

                                    <Columns>


                                        <asp:TemplateField HeaderText="Program">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("ProgramShortName") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Formal Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Title") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredit" Text='<%#Eval("Credits") %>' ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="header-center">

                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:HiddenField ID="hdnCourseID" runat="server" Value='<%#Eval("CourseID") %>' />
                                                    <asp:HiddenField ID="hdnVersionID" runat="server" Value='<%#Eval("VersionID") %>' />

                                                    <asp:CheckBox runat="server" ID="ChkChecked"></asp:CheckBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnSave2" Text="SAVE" CssClass="btn btn-info btn-sm" OnClick="btnSave_Click" Style="display: inline-block; width: 100%; text-align: center;" />

                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="Button2" Text="CLOSE" OnClick="Button2_Click" CssClass="btn btn-danger btn-sm" Style="display: inline-block; width: 100%; text-align: center;" />
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
