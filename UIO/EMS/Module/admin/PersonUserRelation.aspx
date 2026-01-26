<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PersonUserRelation" Codebehind="PersonUserRelation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Relaton: Person & User</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function BindEvents() {

            function InProgress01() { alert("Work11"); var panelProg = $get('MainContainer_PnProcess01'); panelProg.style.display = 'inline'; }
            function onComplete01() { alert("Work12"); var panelProg = $get('MainContainer_PnProcess01'); panelProg.style.display = 'none'; }

            function InProgress02() { alert("Work21"); var panelProg = $get('MainContainer_PnProcess02'); panelProg.style.display = 'inline'; }
            function onComplete02() { alert("Work22"); var panelProg = $get('MainContainer_PnProcess02'); panelProg.style.display = 'none'; }

            function InProgress03() { alert("Work31"); var panelProg = $get('MainContainer_PnProcess03'); panelProg.style.display = 'inline'; }
            function onComplete03() { alert("Work32"); var panelProg = $get('MainContainer_PnProcess03'); panelProg.style.display = 'none'; }
        }

        function btnRelateValidation() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlUser option:selected').val() == '-1' || $('#MainContainer_ddlPersonStudent option:selected').val() == '0') {
                $('#MainContainer_lblMsg').text('Please RELATE Person and User');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>

        <div class="PageTitle">
            <label>Relation Between Person and User</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(BindEvents);
                </script>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div class="PersonUserRelation-container" style="margin-top: 20px;">

            <div class="floatLeft CommonPanel">
                <asp:UpdatePanel ID="UpdatePanel02" runat="server">
                    <ContentTemplate>

                        <div class="div-margin1">
                            <label class="display-inline field-Title"><b>Person</b></label>
                            <%--<span runat="server" id="PnProcess01" style="display: none;">
                                <img src="../Images/loading01.gif" class="img-Loading" />
                            </span>--%>
                        </div>

                        <hr />

                        <%--<div class="div-margin">
                            <label class="display-inline field-Title">Person Type</label>
                            <asp:DropDownList runat="server" ID="ddlPersonType" class="margin-zero dropDownList" DataTextField="ValueName" DataValueField="ValueID" OnSelectedIndexChanged="ddlPersonType_Selected" AutoPostBack="true" />
                        </div>--%>

                        <div class="div-margin">
                            <label class="display-inline field-Title">Search(By Name)</label>
                            <asp:TextBox runat="server" ID="txtPersonSearch" class="margin-zero input-Size2" placeholder="Name" />
                        </div>

                        <div class="div-margin">
                            <label class="display-inline field-Title">Search(By Initial)</label>
                            <asp:TextBox runat="server" ID="txtInitialSearch" class="margin-zero input-Size2" placeholder="Initial" />
                        </div>

                        <div class="div-margin">
                            <label class="display-inline field-Title2"></label>
                            <asp:Button runat="server" ID="btnSearch" class="button-margin SearchKey" Text="Search" OnClick="btnSearch_Click" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel03" runat="server">
                    <ContentTemplate>

                        <hr />

                        <div class="div-margin">
                            <label class="display-inline field-Title">Person</label>
                            <asp:DropDownList runat="server" ID="ddlPersonStudent" class="margin-zero dropDownList" OnSelectedIndexChanged="ddlPersonStudent_Selected" AutoPostBack="true" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <%--<asp:Panel runat="server" ID="pnlSearch">--%>

                    <hr />

                    <div class="div-margin">
                        <label class="display-inline field-Title">Father Name</label>
                        <label class="display-inline field-Title">
                            <asp:Label runat="server" ID="lblFatherName"></asp:Label>
                        </label>
                    </div>

                    <div class="div-margin">
                        <label class="display-inline field-Title">Date of Birth</label>
                        <label class="display-inline field-Title">
                            <asp:Label runat="server" ID="lblDOB"></asp:Label>
                        </label>
                    </div>

                    <div class="div-margin">
                        <label class="display-inline field-Title">Code</label>
                        <label class="display-inline field-Title">
                            <asp:Label runat="server" ID="lblCode"></asp:Label></label>
                    </div>
                <%--</asp:Panel>--%>
            </div>

            <div class="floatLeft">
                <div class="CommonPanel">
                    <asp:UpdatePanel ID="UpdatePanel04" runat="server">
                        <ContentTemplate>
                        
                            <div class="div-margin1">
                                <label class="display-inline field-Title"><b>User</b></label>
                                <%--<span runat="server" id="PnProcess02" style="display: none;">
                                    <img src="../Images/loading01.gif" class="img-Loading" />
                                </span>--%>
                            </div>
                            <hr />
                            <div class="div-margin">
                                <label class="display-inline field-Title">Search</label>
                                <asp:TextBox runat="server" ID="txtUserSearch" class="margin-zero input-Size" placeholder="User ID" />
                                <asp:Button runat="server" ID="btnUserSearch" class="button-margin SearchKey" Text="Search" OnClick="btnUserSearch_Click" />
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel05" runat="server">
                        <ContentTemplate>
                            <div class="div-margin">
                                <label class="display-inline field-Title">User ID</label>
                                <asp:DropDownList runat="server" ID="ddlUser" class="margin-zero dropDownList" DataTextField="LogInID" DataValueField="User_ID" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="div-margin">
                    <asp:Button runat="server" ID="btnRelate" class="margin-zero btn-size" Text="Relation" OnClick="btnRelate_Click" OnClientClick="return btnRelateValidation();" />

                    <%--<span runat="server" id="PnProcess03" style="display: none;">
                        <img src="../Images/loading01.gif" class="img-Loading" />
                    </span>--%>
                </div>
                <div class="div-margin">
                </div>
            </div>

            <div class="cleaner"></div>
        </div>

        <%--<ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress01()();" /> <EnableAction AnimationTarget="btnSearch" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete01();" /> <EnableAction AnimationTarget="btnSearch" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender02" TargetControlID="UpdatePanel05" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress02()();" /> <EnableAction AnimationTarget="btnUserSearch" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete02();" /> <EnableAction AnimationTarget="btnUserSearch" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>--%>
    </div>
</asp:Content>

