<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultApprove.aspx.cs" Inherits="ExamResultApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Result Approve</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">

    <div>

        <div class="PageTitle">
            <label>Result Approve</label>
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
                <div class="ExamResultApprove-container">
                    <div class="div-margin">
                        <div class="loadArea">

                            <label class="display-inline field-Title">Calender Type</label>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="CalenderType_Changed" />

                            <label class="display-inline field-Title1">Academic Calender</label>
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                            <label class="display-inline field-Title1">Course Name</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalSection" class="margin-zero dropDownList" />

                            <asp:Button ID="btnLoadResult" runat="server" Text="Load Result" class="button-margin btn-size" OnClick="LoadResult_Click" />

                            <div id="divProgress" style="display: none; float: right; z-index: 1000;">
                                <div style="float: left">
                                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                    <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                    <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnSubmitStudentMarkTop">
                            <div class="loadedArea">
                                <asp:Button ID="btnFinalSubmitTop" runat="server" Text="Result Approve" class="button-margin btn-size1" OnClick="FinalApprove_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <div class="ExamResultApprove-container">
                    <asp:Panel ID="PnlFinalExamResult" runat="server" Wrap="False">
                        <%-- Height="100%" ScrollBars="Vertical"--%>
                        <asp:GridView ID="gvFinalExamResultApprove" runat="server" AutoGenerateColumns="true" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel04" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnSubmitStudentMarkButtom">
                    <div class="ExamResultApprove-container">
                        <div class="div-margin">
                            <div class="loadArea">
                                <asp:Button ID="btnFinalSubmitBottom" runat="server" Text="Result Approve" class="button-margin btn-size1" OnClick="FinalApprove_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnLoadResult" Enabled="false" />                   
                    </Parallel>
                </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnLoadResult" Enabled="true" />
                        </Parallel>
                </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

    </div>

</asp:Content>
