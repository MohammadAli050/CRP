<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultUnSubmitBySection.aspx.cs" Inherits="EMS.miu.result.ExamResultUnSubmitBySection" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    Exam Result Un-Submit by Section
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .marginTop {
            margin-top: -5px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="PageTitle">
        <label>Exam Result Un-Submit by Section</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="divProgress" style="display:none ;  margin-top:-35px">
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                <br />
                Processing your request ...
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td class="auto-style4">
                            <b>Section :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlAcaCalSection" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlAcaCalSection_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        
                    </tr>
                </table>
                
            </div>          
            <br />
            <asp:Panel runat="server" ID="pnlExamUnSubmit" Visible="false">
            <table>
                <tr>
                    
                    
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label runat="server" ID="lblExam" Font-Bold="true" Text="Exam List:"></asp:Label>
                    </td>
                    
                </tr>
                <td>
                    <asp:CheckBox runat="server" ID="cbxAllSelect" Text="Select All" OnCheckedChanged="cbxSelectAll_Checked" AutoPostBack="true" />
                    <asp:CheckBoxList runat="server" ID="cblExamList"></asp:CheckBoxList>
                    <asp:Label ID="lblExamTemplateBasicItemId" Visible="false" runat="server"></asp:Label>
                </td>
                
            </table>
            <asp:Button runat="server" ID="btnUnSubmit" Text="Un-Submit Mark" Height="35px" OnClick="btnUnSubmit_Clicked"/>
            </asp:Panel>

            <div>
                                    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender
                                        ID="ModalPopupExtender1"
                                        runat="server"
                                        TargetControlID="btnShowPopup"
                                        PopupControlID="pnPopUp"
                                        CancelControlID="btnClose"
                                        BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel runat="server" ID="pnPopUp" Style="display: none;">
                                        <div style="height: 120px; width: 500px; margin: 5px; background-color: Window;">
                                            <fieldset style="padding: 0px 10px; margin: 5px; border-color: lightgreen;">
                                                <legend>Warning!</legend>
                                                <div style="padding: 20px; width: 100%">
                                                    Result already Published. Do you want to continue un-submit mark?
                                                </div>
                                                <div style="margin-top: 10px; margin-left:50px">
                                                    <asp:Button runat="server" ID="btnConfirm" Text="Confirm" Style="width: 150px; height: 30px;" OnClick="btnConfirm_Clicked"/>
                                                <asp:Button runat="server" ID="btnClose" Text="Cancel" Style="width: 150px; height: 30px;"/>
                                            </div>
                                            </fieldset>
                                            
                                        </div>
                                    </asp:Panel>

                                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnUnSubmit" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnUnSubmit" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>    
</asp:Content>