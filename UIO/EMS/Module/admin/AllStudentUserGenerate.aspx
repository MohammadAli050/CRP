<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_AllStudentUserGenerate" Codebehind="AllStudentUserGenerate.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">User Generate For Student</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>

    <style type="text/css">
        .UserGenerateForStudent-container label,  .UserGenerateForStudent-container  select, .UserGenerateForStudent-container input{
            font-size: 11px;
        }
        .UserGenerateForStudent-container .div-margin {
            margin: 5px 0;
        }
        .UserGenerateForStudent-container .loadArea {
            margin-top: 20px;
            border-top: 1px solid #b5e79e;
            border-bottom: 1px solid #b5e79e;
            background-color: #f9f9f9;
            padding: 5px;
        }
        .UserGenerateForStudent-container .dropDownList {
            padding: 0px 5px;
        }
        .UserGenerateForStudent-container .btn-size {
            padding: 0px 5px;
            cursor: pointer;
            margin-left: 5px;
        }
        .UserGenerateForStudent-container .img-Loading {
            vertical-align: middle;
            padding-bottom: 3px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>User Generate For Student</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="UserGenerateForStudent-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Program</label>
                            <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Program_Change" />

                            <label class="display-inline field-Title1">Batch</label>
                            <asp:DropDownList runat="server" ID="ddlBatch" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Batch_Change" />

                            <asp:Button runat="server" ID="btnGenerate" Text="Generate" OnClick="Generate_Click" class="button-margin btn-size" />
                            <asp:Button runat="server" ID="btnView" Text="View" OnClick="View_Click" class="button-margin btn-size" />
                             
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="RptTotalStudentByCourseWise-container">
                    <rsweb:reportviewer id="ReportViewer01" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" Height="100%" sizetoreportcontent="true">
                        <LocalReport ReportPath="Module/Admin/UserAndPassword.rdlc"></LocalReport>
                    </rsweb:reportviewer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress();" /> <EnableAction AnimationTarget="btnView" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnView" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>