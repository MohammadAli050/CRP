<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="RptBatchWiseStudent" Codebehind="RptBatchWiseStudent.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc3" TagName="SessionUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">    
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
        .auto-style7 {
            width: 122px;
        }
        .auto-style8 {
            width: 119px;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        .auto-style12 {
            width: 94px;
        }
        .auto-style15 {
            width: 185px;
        }
        .auto-style16 {
            width: 95px;
        }
        .auto-style20 {
            width: 82px;
        }
        .auto-style21 {
            width: 130px;
        }
        .auto-style22 {
            width: 145px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">   
    <div class="PageTitle">
        <label>Student List :: Program and Batch Wise</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                 <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>                  
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <%--<td class="auto-style20">Year : </td>
                            <td class="auto-style21"><asp:DropDownList runat="server" ID="ddlYear"/></td>

                             <td class="auto-style20">Semester : </td>
                            <td class="auto-style21"><asp:DropDownList runat="server" ID="ddlSemester"/></td>--%>

                            <td class="auto-style12">Program : </td>
                            <td class="auto-style15"><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" /></td>                                                                                                                     
                                
                            <td class="auto-style20">Batch : </td>
                            <td class="auto-style21"><uc2:BatchUserControlAll runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged"/></td>

                            <td class="auto-style22">Gender : </td>
                            <td class="auto-style21"><Asp:DropDownList runat="server" ID="ddlGender" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged"/></td>


                        </tr>
                        <tr>
                            <td class="auto-style12"><asp:CheckBox ID="chkStudentID" runat="server" Text="StudentID" /></td>
                            <td class="auto-style16"><asp:CheckBox ID="chkDateOfBirth" runat="server" Text="Date Of Birth" /></td>
                            <td class="auto-style20"><asp:CheckBox ID="chkGender" runat="server" Text="Gender" /></td>
                            <td class="auto-style21"><asp:CheckBox ID="chkFullName" runat="server" Text="FullName" /></td>                                                     
                            <td class="auto-style7"><asp:CheckBox ID="chkPhone" runat="server" Text="Phone" /></td>
                            <td class="auto-style8"><asp:CheckBox ID="chkEmail" runat="server" Text="Email" /></td>
                            <td class="auto-style8"><asp:CheckBox ID="chkPhoto" runat="server" Text="Photos" /></td>
                            <td class="auto-style8"><asp:CheckBox ID="chkPresentAddress" runat="server" Text="Present Address" /></td>
                            <td class="auto-style22"><asp:CheckBox ID="chkPermanentAddress" runat="server" Text="Permanent Address" /></td>
                            <td><asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" CssClass="pointer" Text="Load" Width="90px" /></td>
                        </tr>
                          
                            
                    </table>
                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -42px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     
    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" />
                <EnableAction AnimationTarget="btnLoad" Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script = "InProgress();" />
                <EnableAction AnimationTarget = "btnGenerate"  Enabled = "false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction   AnimationTarget="btnGenerate" Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="width: 1100px; margin-top: 20px;">

                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <div>&nbsp;</div>
    <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" width="57.3%" Height="100%" sizetoreportcontent="true">
        <%--<LocalReport ReportPath="miu/student/report/RptBatchWiseStudent.rdlc">
        </LocalReport>--%>
    </rsweb:reportviewer>
           
</asp:Content>


