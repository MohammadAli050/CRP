<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="DayLectureEntry" CodeBehind="DayLectureEntry.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Day Lecture Entry</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
        });

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
 
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Day Lecture Entry</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td><b>Program</b></td>
                            <td class="auto-style1">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="auto-style5"><b>Session</b></td>
                            <td class="auto-style4">
                                <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                            </td>
                            <td class="auto-style3"><b>Course</b></td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlCourse" runat="server" Width="380px" />
                            </td>
                            <td class="auto-style7"><b>Lecture No</b></td>
                            <td>
                                <asp:DropDownList ID="ddlLectureNo" runat="server" Style="width: 100px;">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" Width="70px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel03" runat="server">
        <ContentTemplate>
            <div>
                <table> 
                    <tr>
                        <td><b>Topic </b></td>
                        <td>
                            <asp:TextBox ID="txtTopics" TextMode="MultiLine" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Remarks1 </b></td>
                        <td>
                            <asp:TextBox ID="txtRemarks1" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Remarks2 </b></td>
                        <td>
                            <asp:TextBox ID="txtRemarks2" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Remarks3 </b></td>
                        <td>
                            <asp:TextBox ID="txtRemarks3" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Remarks4 </b></td>
                        <td>
                            <asp:TextBox ID="txtRemarks4" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </td>
                        <td></td>
                    </tr>

                </table>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
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

</asp:Content>

