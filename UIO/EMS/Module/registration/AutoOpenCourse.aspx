<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="AutoOpenCourse" Codebehind="AutoOpenCourse.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Src="~/UserControl/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControl/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>--%>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Auto Open Course
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
           .msgPanel {
            margin-top: 10px;
            border: 0px solid #008080;
            padding: 5px;
            width: 988px;
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
            <label>Auto Open Course</label>
        </div>

        <div class="Message-Area" style="height: 40px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 1070px;">
                        <tr>
                             <td class="tbl-width-lbl"><b>Program</b></td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="tbl-width-lbl"><b>Batch</b></td>
                            <td>
                                <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                            </td>
                           
                            <asp:Panel ID="hdnPnl" runat="server" Visible="false">
                                <td>
                                    <asp:Label ID="lblCGPASession" runat="server" Text="CGPA Session" Enabled="false"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCGPASession" DataValueField="AcademicCalenderID" DataTextField="FullCode" runat="server" Width="140px" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkCurrentCgpa" runat="server" Text="Current CGPA" Checked="true"
                                        OnCheckedChanged="chkCurrentCgpa_CheckedChanged" AutoPostBack="true" />
                                </td>
                            </asp:Panel>
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
                             <td>
                                <asp:Label ID="lblStudentId" runat="server" Text="Student ID"></asp:Label>
                                <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Height="30px" Width="70px" BackColor="#edd366" />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
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

                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total Student : "></asp:Label>
                        <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" AllowPaging="false" PageSize="20"
                            PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvStudentList_PageIndexChanging" Width="100%"
                            PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                            ShowHeader="true" CssClass="gridCss" DataKeyNames="StudentID">
                            <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#F0F8FF" />
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
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CGPA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCGPA" Text='<%#Eval("CGPA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lable_2" runat="server" Width="100" Text="Auto Open Cr.">
                                        </asp:Label>
                                        <br />
                                        <hr style="width: 100px" />
                                        <asp:TextBox ID="txtAutoOpenCr" runat="server" Width="50">
                                        </asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtAutoOpenCr"
                                            ValidationExpression="^[0-9,]*$"
                                            ErrorMessage="*"
                                            ForeColor="Red"
                                            runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label runat="server" ID="lblAutoOpenCr" Text='<%#Eval("AutoOpenCr") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Button ID="btnAutoOpen" runat="server" Height="45px" Text="Auto Open"
                                            OnClick="btnAutoOpen_Click" Font-Bold="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label runat="server" ID="lblAutoOpen" Font-Bold="true" Text='<%#Eval("AutoOpen") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>                               
                            </Columns>
                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

