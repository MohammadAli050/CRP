<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="AutoMandatoryCourse" Codebehind="AutoMandatoryCourse.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Auto Mandatory Course</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">    
    
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
    <style type="text/css">
        .auto-style1
        {
            width: 151px;
        }
        .auto-style4
        {
            width: 49px;
        }
        .auto-style8
        {
            width: 127px;
        }
        .auto-style9
        {
            width: 239px;
        }
        .auto-style10
        {
            width: 28px;
        }
        .auto-style11
        {
            width: 155px;
        }
        .auto-style12
        {
            width: 41px;
        }
        .auto-style13
        {
            width: 70px;

        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 0px; width: 100%;">
        <div class="PageTitle">
            <label>Auto Mandatory Course</label>
        </div>

        <div class="Message-Area" style="height: 45px;" >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 100%;  border: 0px solid red;">
                        <tr>
                            <td class="auto-style10"><b>Batch</b></td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlAcaCalBatch" DataValueField="AcademicCalenderID" DataTextField="FullCode" runat="server" Width="139px" style="margin-left: 0px">
                                </asp:DropDownList></td>
                            <td class="auto-style4"><b>Program</b></td>
                            <td class="auto-style11">
                                <asp:DropDownList ID="ddlProgram" runat="server" Width="125px"
                                    DataTextField="NameWithCode" DataValueField="ProgramID" style="margin-left: 8px">
                                </asp:DropDownList></td>
                           
                            <td class="auto-style12"><b>Block</b></td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlBlock" runat="server" Width="105px"> 
                                    <asp:ListItem Value="0" Text="Not Blocked"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Blocked"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="All"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style12"><b>Major</b></td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlMajor" runat="server" Width="110px">
                                    <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Major Defined"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Major Not Defined"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td class="auto-style13">
                                <asp:Label ID="lblStudentId" runat="server" Text="Student ID" Font-Bold="true"></asp:Label>
                             </td>
                            <td class="auto-style9"> 
                                <asp:TextBox ID="txtStudent" runat="server" Text="" style="margin-left: 13px"></asp:TextBox>
                            </td>
                            <asp:Panel ID="hdnPnl" runat="server" Visible="false" Width="16px">
                                <td>
                                    <asp:Label ID="lblCGPASession" runat="server" Text="CGPA Session" Enabled="false"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCGPASession" DataValueField="AcademicCalenderID" DataTextField="FullCode" runat="server" Width="140px" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkCurrentCgpa" runat="server" Text="Current CGPA" Checked="true"
                                        OnCheckedChanged="chkCurrentCgpa_CheckedChanged" AutoPostBack="true" />
                                </td>
                            </asp:Panel>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" CssClass="button_style" Width="84px" />
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
                        <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" PageSize="20" HorizontalAlign="Center"
                            PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvStudentList_PageIndexChanging" 
                            PagerStyle-Font-Bold="true" PagerStyle-Font-Size="Larger"
                             ForeColor="#1A1A1A" HeaderStyle-BorderColor="Yellow" CssClass="gridCss" DataKeyNames="StudentID" OnSelectedIndexChanged="gvStudentList_SelectedIndexChanged">
                            <HeaderStyle BackColor="#C9CACB"  ForeColor="#87090D" />
                            <AlternatingRowStyle BackColor="#F4F4F4" />
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
                                    <HeaderStyle Width="130px" BorderColor="#C9CACB" />
                                    <ItemStyle BorderColor="#C9CACB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FullName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BorderColor="#C9CACB" />
                                    <ItemStyle BorderColor="#C9CACB" width="45%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" BorderColor="#C9CACB" />
                                    <ItemStyle BorderColor="#C9CACB" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CGPA">
                                    <ItemTemplate>
                                        <div style="text-align: center"> 
                                        <asp:Label runat="server" ID="lblCGPA" Text='<%#Eval("CGPA") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" BorderColor="#C9CACB" />
                                    <ItemStyle BorderColor="#C9CACB" />
                                </asp:TemplateField>

                                 <asp:TemplateField>
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
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Button ID="btnAutoMandatory" runat="server"  Text="Auto Mandatory" 
                                            OnClick="btnAutoMandatory_Click" Font-Bold="true" Font-Size="Small" Height="35" Width="130"  CssClass="button_style" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label runat="server" ID="lblAutoMandaotry" Font-Bold="false" Text='<%#Eval("AutoMandaotry") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" BorderColor="#C9CACB" />
                                    <ItemStyle BorderColor="#C9CACB" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle Font-Bold="True" Font-Size="Larger" />
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

