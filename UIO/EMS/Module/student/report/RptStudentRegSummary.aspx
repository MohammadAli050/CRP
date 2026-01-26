<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentRegSummary.aspx.cs" Inherits="EMS.miu.student.report.RptStudentRegSummary" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Registration Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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
        .auto-style1 {
            width: 90px;
        }
        .auto-style2 {
            width: 160px;
        }
        .auto-style4 {
            width: 70px;
        }
        .auto-style5 {
            width: 285px;
        }
        .auto-style6 {
            width: 97px;
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        .auto-style7 {
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Course Registration Summary :: Student Wise</label>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>            
                <div>
                    <table>
                        <tr>
                            <td class="auto-style1">
                                <b>Student Roll : </b>
                            </td>
                            <td class="auto-style2">
                                <asp:TextBox runat="server" ID="txtRoll"></asp:TextBox>
                            </td>                                                                                                                                           
                            <td class="auto-style4">
                                <b>Session : </b>
                            </td>
                            <td class="auto-style5">
                                <asp:DropDownList ID="ddlSession" runat="server" Width="120px" />
                            </td>
                                                                                                                                                                            
                            <td class="auto-style6"><asp:Button ID="Button1" runat="server" CssClass="pointer" Width="101px" Text="LOAD" OnClick="buttonView_Click" /></td>
                            <td class="auto-style7">&nbsp;</td>
                            <td><asp:Button ID="Button2" runat="server" CssClass="pointer" Width="120px" Text="OPEN  PDF" OnClick="buttonPrint_Click" /></td>    
                        </tr>                                                 
                            
                    </table>
                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                    </div>
                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                        Please wait...
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="Button2" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1" runat="server">
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
    
    <div>&nbsp;</div>
    <rsweb:reportviewer ID="StudentRegSummary" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" asynrendering="true" Width="54.6%" Height="100%" sizetoreportcontent="true" >
    </rsweb:reportviewer>
            
</asp:Content>
