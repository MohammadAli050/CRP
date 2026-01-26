<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ServayByStudent_EvaluationGraph" CodeBehind="EvaluationGraph.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Evaluation :: Graph</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Course Evaluation Form :: Summary</label>
        </div>

        <asp:Panel runat="server" ID="pnMessage">
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </asp:Panel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="EvaluationSummary-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <div style="float: left;">
                                <div style="float: left;">
                                    <label>Program</label>
                                </div>
                                <div style="float: left;">
                                    <asp:DropDownList runat="server" ID="ddlProgram" DataValueField="ProgramId" DataTextField="ShortName" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_Change" />

                                </div>
                            </div>

                            <div style="float: left; height: 16px;">
                                <div style="float: left;">
                                    <label class="display-inline field-Title">Session :</label>
                                </div>
                                <div style="float: left;">
                                    <uc1:SessionUserControl runat="server" ID="uclAcaCal"  OnSessionSelectedIndexChanged="uclBatch_Change"/>
                                </div>
                            </div>

                        </div>

                        <div class="loadedArea">
                            <label class="display-inline field-Title">Course</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalSection" class="margin-zero dropDownList2" />

                            <asp:TextBox runat="server" ID="txtSearchKey" class="margin-zero input-Size" placeholder="Course Code / Title" />
                            <asp:Button runat="server" ID="btnSearch" class="button-margin SearchKey" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title"></label>
                            <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

