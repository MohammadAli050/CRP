<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="GenerateStudentIdCardInfo.aspx.cs" Inherits="EMS.miu.admin.GenerateStudentIdCardInfo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    StudentId Card Generate
</asp:Content>
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
        .auto-style5 {
            width: 280px;
        }

        .auto-style10 {
            width: 99px;
        }

        .auto-style11 {
            height: 29px;
        }

        .auto-style12 {
            width: 280px;
            height: 29px;
        }

        .auto-style13 {
            width: 99px;
            height: 29px;
        }

        .auto-style14 {
            width: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>StudentId Card Generate</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <div style="padding: 5px; margin: 5px; width: 900px;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Width="73px" Text="Student Roll"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentRoll" Placeholder="Student Roll" runat="server"></asp:TextBox>


                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Width="127px" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td class="auto-style14"></td>
                            <td>
                                <asp:Button ID="btnGenerateIDCard" runat="server" Text="Generate" OnClick="btnGenerateIDCard_Click" />
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
    </div>

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


    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlInfo" runat="server">
                <div class="Message-Area">
                    <div style="padding: 5px; margin: 5px; width: 900px;">
                        <table>

                            <tr>
                                <td>
                                    <asp:Label ID="lblStudentName" runat="server" Width="150px" Text="Student Name"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtStudentName" Placeholder="Student Name" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td class="auto-style10">
                                    <asp:Label ID="lblBirthDate" runat="server" Text="Validation Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValidationDate" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">
                                    <asp:Label ID="Label3" runat="server" Width="150px" Text="Student ID"></asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="txtStudentId" Placeholder="Student ID" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td class="auto-style10">
                                    <asp:Label ID="Label4" runat="server" Text="Session"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtSession" runat="server" Width="156px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGender" runat="server" Width="150px" Text="Gender"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem Text="Male" Value="0" />
                                        <asp:ListItem Text="Female" Value="1" />
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style10">
                                    <asp:Label ID="Label19" runat="server" Text="Blood Group"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtBloodGroup" runat="server" Width="78px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <rsweb:reportviewer id="ReportViewer" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" backcolor="Wheat" bordercolor="WhiteSmoke" borderstyle="Solid" borderwidth="1" cssclass="center" height="100%" width="97.8%" sizetoreportcontent="true">
                </rsweb:reportviewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
