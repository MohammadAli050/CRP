<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="True" CodeBehind="AccountTypeAndStudentWiseBill.aspx.cs" Inherits="AccountTypeAndStudentWiseBill" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Account Wise Bill Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <%-- <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />--%>

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
    <style>
        .center {
            margin: 0 auto 0 0px;
            padding: 10px;
        }
        .auto-style1 {
            width: 201px;
        }
        .auto-style2 {
            width: 204px;
        }
        .auto-style5 {
            width: 168px;
        }
        .auto-style6 {
            width: 145px;
        }
        .auto-style7 {
            width: 148px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Bill Report : : Account and Date Wise</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <panel id="pnlMessage" runat="server" visible="true" cssclass="msgPanel"> 
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMessage" Text="" ForeColor="Red"/>
                </div>
            </panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ExamResultPrint-container">
                <div class="div-margin">
                    <div class="Message-Area">
                        <table>
                            <tr>
                                <td class="auto-style5">
                                    <asp:Label ID="Label5" runat="server" Text="Account Type" Style="width: 90px; display: inline-block;" Width="89px"></asp:Label>
                                    <asp:DropDownList ID="ddlTypeDefinition" runat="server" DataValueField="TypeDefinitionID" DataTextField="Definition" Style="width: 150px;" AutoPostBack="true">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style6">
                                    <asp:Label ID="Label1" runat="server" Text="Program" Style="width: 80px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" Style="width: 110px;" AutoPostBack="true" OnSelectedIndexChanged="OnProgramSelectedIndexChanged">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style7">
                                    <asp:Label ID="Label6" runat="server" Text="Batch" Style="width: 80px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlBatch" runat="server" Style="width: 110px;" AutoPostBack="true" Width="114px">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style7">
                                    <asp:Label ID="Label2" runat="server" Text="Session" Style="width: 80px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlSession" runat="server" Style="width: 110px;" AutoPostBack="true" Width="114px">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label3" runat="server" Text="From Date" Style="width: 80px; display: inline-block;"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtFromDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label4" runat="server" Text="To Date" Style="width: 80px; display: inline-block;"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtToDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="btnLoadBill" runat="server" Text="Load" Width="150" Height="40" OnClick="GetBillHistory_Click"></asp:Button>

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoadBill" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoadBill" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <div>&nbsp;</div>
    <center>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="99%" SizeToReportContent="true">
            <LocalReport ReportPath="miu\bill\report\AccountTypeAndStudentWiseBill.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </center>
</asp:Content>

