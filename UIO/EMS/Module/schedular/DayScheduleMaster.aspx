<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="DayScheduleMaster" CodeBehind="DayScheduleMaster.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Day Schedule Master</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .chkList-table {
            margin-left: 130px;
        }

        .btn-margin {
            margin-left: 10px;
        }
    </style>

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Day Schedule Master</label>
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
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="auto-style2">&nbsp</td>
                            <td class="auto-style3"><b>Session</b></td>
                            <td>
                                <uc1:SessionUserControl runat="server" ID="ucSession" />
                            </td>
                            <td class="auto-style2">&nbsp</td>
                            <td style="vertical-align: middle">
                                <asp:Button ID="btnLoad" runat="server" CssClass="pointer" Width="100px" Height="25px" Text="LOAD" OnClick="btnLoad_Click" />
                            </td>
                            <td class="auto-style2">&nbsp</td>
                            <td style="vertical-align: middle">
                                <asp:Button ID="btnGenerate" runat="server" CssClass="pointer" BackColor="Green" Width="100px" Height="25px" Text="Generate" OnClick="btnGenerate_Click" />
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
            <div class="LoadStudentCourseHistory">
                <asp:Panel ID="PnlDayScheduleMaster" runat="server" Width="80%" Wrap="False">
                    <asp:GridView ID="gvDayScheduleMaster" AllowSorting="true" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <RowStyle Height="25px" />

                        <Columns>

                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                    <asp:Label runat="server" ID="lblScheduleDate" Font-Bold="True" Text='<%#Eval("ScheduleDate", "{0:dd/MMM/yyyy}")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Default Day" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList Enabled="false" ID="ddlDefaultDay" runat="server" SelectedValue='<%#(Eval("DefaultDayId")==null ? "0" : Eval("DefaultDayId"))%>' Style="width: 100px;">
                                        <asp:ListItem Value="1">Sat</asp:ListItem>
                                        <asp:ListItem Value="2">Sun</asp:ListItem>
                                        <asp:ListItem Value="3">Mon</asp:ListItem>
                                        <asp:ListItem Value="4">Tue</asp:ListItem>
                                        <asp:ListItem Value="5">Wed</asp:ListItem>
                                        <asp:ListItem Value="6">Thu</asp:ListItem>
                                        <asp:ListItem Value="7">Fri</asp:ListItem>
                                        <asp:ListItem Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MakeUp Day" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlMakeUpDay" runat="server" SelectedValue='<%#(Eval("MakeUpDayId")==null ? "0" : Eval("MakeUpDayId"))%>' Style="width: 100px;">
                                        <asp:ListItem Value="1">Sat</asp:ListItem>
                                        <asp:ListItem Value="2">Sun</asp:ListItem>
                                        <asp:ListItem Value="3">Mon</asp:ListItem>
                                        <asp:ListItem Value="4">Tue</asp:ListItem>
                                        <asp:ListItem Value="5">Wed</asp:ListItem>
                                        <asp:ListItem Value="6">Thu</asp:ListItem>
                                        <asp:ListItem Value="7">Fri</asp:ListItem>
                                        <asp:ListItem Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="WeekNo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate> 
                                    <asp:TextBox TextMode="Number" ID="txtWeekNo" Text='<%#Eval("WeekNo") %>' Width="50px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField> 
                                <ItemTemplate>
                                    <asp:Button runat="server" ToolTip="Edit" Text="Edit" ID="btnEdit" OnClick="btnEdit_Click"></asp:Button>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderTemplate>
                                    <asp:Button runat="server" ToolTip="Save All" Text="Save All" ID="btnSaveAll"
                                        OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                        OnClick="btnSaveAll_Click"></asp:Button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ToolTip="Save" Text="Save" ID="btnSave" OnClick="btnSave_Click"></asp:Button>
                                </ItemTemplate>

                                <HeaderStyle Width="100px" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />

                            </asp:TemplateField>
                            
                        </Columns>
                        <EmptyDataTemplate>
                            <b>No Data Found !</b>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnPopUp"
                CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel runat="server" ID="pnPopUp" Style="display: none;">
                <div>
                    <div style="height: 400px; width: 1100px; padding: 5px; margin: 5px; background-color: Window; overflow: scroll">

                        <fieldset style="padding: 5px; margin: 5px;">
                            <legend style="font-weight: bold; font-size: large; text-align: center">Day Schedule Details</legend>
                            <div>
                                <asp:Label runat="server" ID="lblPopUpMessageTitle" Text="Message" CssClass="label-width-popUp"></asp:Label>
                                <asp:Label runat="server" ID="lblPopUpMessage" ></asp:Label>

                                <asp:GridView ID="gvDayScheduleDetails" AllowSorting="true" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="25px" />
                                    <AlternatingRowStyle BackColor="#FFFFCC" />
                                    <RowStyle Height="25px" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Course" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                                <asp:Label runat="server" ID="lblCourse" Font-Bold="True" Text='<%#Eval("FullCourseCode") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblSection" Font-Bold="True" Text='<%#Eval("SectionName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTime" Font-Bold="True" Text='<%#Eval("TimeSlotInfoOne") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <label>Is Active</label>
                                                <hr />
                                                <asp:CheckBox ID="chkSelectAllIsActive" runat="server" Text="All"
                                                    AutoPostBack="true" OnCheckedChanged="chkSelectAllIsActive_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ChkIsActive" Checked='<%# Eval("IsActive") == null ? false : Eval("IsActive") %>'></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                <asp:Button runat="server" ToolTip="Save All" Text="Save All" ID="btnSaveDetailAll"
                                                    OnClientClick=" return confirm('Are you sure, you want to Save?')" OnClick="btnSaveDetailAll_Click"></asp:Button>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ToolTip="Save" Text="Save" ID="btnSaveDetail" OnClick="btnSaveDetail_Click"></asp:Button>
                                            </ItemTemplate>

                                            <HeaderStyle Width="100px" />
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>
                                        <b>No Data Found !</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" Style="width: 150px; height: 30px;" OnClick="btnCancel_Click" />
                            </div>
                        </fieldset>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

