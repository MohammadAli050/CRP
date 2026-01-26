<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_GenerateWorksheet" Codebehind="GenerateWorksheet.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Generate Worksheet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style>
        .msgPanel {
            margin-top: 10px;
            border: 1px solid #008080;
            padding: 5px;
            width: 888px;
        }

        .tbl-width-lbl-Small {
            width: 70px;
            padding: 5px;
        }

        .tbl-width-lbl-Large {
            width: 150px;
            padding: 5px;
        }

        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
    </style>

    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 10px; width: 1200px;">
        <div class="PageTitle">
            <label>Generate Worksheet</label>
        </div>

        <div class="Message-Area" style="height: 35px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 1100px;">
                        <tr>
                            <td class="tbl-width-lbl"><b>Program</b></td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="tbl-width-lbl"><b>Batch</b></td>
                            <td>
                                <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                            </td>

                            <td class="tbl-width-lbl-Small"><b>Block</b></td>
                            <td>
                                <asp:DropDownList ID="ddlBlock" runat="server" Width="90px">
                                    <asp:ListItem Value="0" Text="Not Blocked"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Blocked"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="All"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tbl-width-lbl-Small"><b>Major</b></td>
                            <td>
                                <asp:DropDownList ID="ddlMajor" runat="server" Width="110px">
                                    <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Major Defined"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Major Not Defined"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                            <td class="tbl-width-lbl-Large"><b>Student ID</b></td>
                            <td>
                                <asp:TextBox ID="txtStudent" runat="server" Text="" Width="150"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                            </td>
                        </tr>

                    </table>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>
                        <%--<div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">                            
                        </div>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

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

        <div style="clear: both;"></div>
        <div>
            <div style="padding: 1px; margin-top: 20px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div style="float: left; width: 100%;">
                            <div style="float: left;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Student : "></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div style="float: right; padding: 5px;">
                                <div style="float: left; color: blue;">
                                    <b>Generate Type</b>
                                </div>
                                
                                <div style="float: left; margin-left: 5px;">
                                    <asp:DropDownList ID="ddlGenerationType" runat="server" Width="110px" ForeColor="Red">
                                        <asp:ListItem Value="1" Text="Close Cr."></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Fixed Cr."></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Full Cr."></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; margin-left: 5px;">
                                <asp:DropDownList ID="ddlSemesterNumber" runat="server" Width="110px">
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" AllowPaging="false" PageSize="20"
                            PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvStudentList_PageIndexChanging" Width="100%"
                            PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                            ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                            <asp:CheckBox runat="server" ID="ChkActive"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="130px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FullName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Button ID="btnGenetare" runat="server" Height="45px" Text="Generate"
                                            OnClick="btnGenetareWorksheet_Click" Font-Bold="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div align="center">
                                            <%# (Boolean.Parse(Eval("IsGeneratedWorksheet").ToString())) ? "● Yes" : "● No" %>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registered">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label runat="server" ID="lblRegStatus" Text='<%#(Boolean.Parse(Eval("IsRegistared").ToString())) ? "Yes" : "" %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                            <%-- <HeaderStyle CssClass="tableHead" />--%>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

