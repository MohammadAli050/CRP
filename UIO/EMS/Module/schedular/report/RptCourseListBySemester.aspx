<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptCourseListBySemester" Codebehind="RptCourseListBySemester.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Course Distribution
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
        .auto-style1 {
            width: 170px;
        }
        .pointer {
            cursor: pointer;        
        }
        .center {
            margin: 0 auto;
            padding: 10px;
        }
        .auto-style2 {
            width: 41px;
        }
        .auto-style3 {
            width: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">   
    <div class="PageTitle">
        <label>Course Distribution :: Semester/Trimester Tree Wise</label>
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
                            <td style="width: 70px; font-weight: bold;">
                                <asp:Label ID="lblPrograms" runat="server" Text="Programs:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlPrograms" runat="server" AutoPostBack="True" onselectedindexchanged="ddlPrograms_SelectedIndexChanged" TabIndex="1" Width="130px">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 50px; font-weight: bold;">
                                <asp:Label ID="lblTrees" runat="server" Text="Trees:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlTreeMasters" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="ddlTreeMasters_SelectedIndexChanged" TabIndex="2" Width="170px">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style2">&nbsp;</td>
                            <td style="width: 80px; font-weight: bold;">
                                <asp:Label ID="lblCalender" runat="server" Text="Distribution:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlLinkedCalendars" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="ddlLinkedCalendars_SelectedIndexChanged" TabIndex="3" Width="190px">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style3">&nbsp;</td>
                            <td><asp:Button ID="Button1" runat="server" OnClick="btnLoad_Click"  Text="LOAD" Width="100"  CssClass="pointer" Height="25px"/> </td>                                
                    </table>

                </div>
                <div id="divProgress" style="display: none; width: 195px; float: right; margin: -32px -35px 0 0;">
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
                <EnableAction AnimationTarget = "btnGenerate" Enabled = "false" />                   
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
            <div style="width: 1100px; margin-top: 10px;">
                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <div>&nbsp;</div>    
    <div> 
        <rsweb:reportviewer id="ReportViewer1" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="44%" Height="100%" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" sizetoreportcontent="true" >
            <%--<LocalReport ReportPath="miu/schedular/report/RptCourseListBySemester.rdlc">
            </LocalReport>--%>
        </rsweb:reportviewer>
    </div>    
  
</asp:Content>

