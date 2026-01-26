<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="LateRegistrationFinePost.aspx.cs" Inherits="EMS.miu.bill.LateRegistrationFinePost" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Late Registration Fine Post
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style>
        table {
            border-collapse: collapse;
        }

        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
            padding: 5px;
        }

        .auto-style8 {
            width: 127px;
        }
        .auto-style10 {
            width: 136px;
        }
        .auto-style11 {
            width: 208px;
        }
        .auto-style12 {
            width: 680px;
        }
        .auto-style13 {
            width: 135px;
        }
        .auto-style14 {
            width: 36px;
        }
        .auto-style15 {
            width: 201px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 5px; width: 100%;">
        <div class="PageTitle">
            <label>Late Registration Fine Post</label>
        </div>
        <div class="Message-Area" style="height: 50px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div>
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                <td class="auto-style8">Program</td>
                                <td class="auto-style15">
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>                                
                                <td class="auto-style8">Registration Session</td>
                                <td class="auto-style15">
                                    <uc1:SessionUserControl runat="server" ID="ucRegistrationSession" OnSessionSelectedIndexChanged="ucRegistrationSession_SessionSelectedIndexChanged" />
                                </td>
                                
                                <td class="auto-style10">Registration Last Date</td>
                                <td class="auto-style11">
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="164px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="uptoDate" runat="server" TargetControlID="DateTextBox" Format="dd/MM/yyyy" />
                                </td>
                                <td class="auto-style10">Student Id</td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtRoll" runat="server" Width="163px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Width="90px" Text="Load" OnClick="btnLoad_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div class="Message-Area" style="height: 20px;">
                    <asp:Label ID="Label3" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="Message-Area" style="height: 25px;">
                    <table style="padding: 5px; width: 100%;">
                        <tr>
                            <td class="auto-style12"></td>
                            <td class="auto-style13">Fine Posting Date</td>
                            <td style="width: 170px">
                                <asp:TextBox ID="txtPostingDate" runat="server" Width="160"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPostingDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="auto-style14"></td>
                            <td>
                                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="POST" ForeColor="Blue" Width="90" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="clear: both;"></div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <strong>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
            </strong>
        </div>

        <strong>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel4"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoad" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoad" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        * Please note that *Already posted fines are not going to be updated from this page (even if you can select them)<br />
        * If Posted Fine column shows any value greater than 0.00 that means, there is a late reg. fine post of that amount in that student&#39;s account</strong>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 100%; margin-top: 30px;">

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Inserted : "></asp:Label>
                    <asp:Label ID="lblInserted" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Updated : "></asp:Label>
                    <asp:Label ID="lblUpdated" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="NotInsertd : "></asp:Label>
                    <asp:Label ID="lblNotInserted" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="NotUpdated : "></asp:Label>
                    <asp:Label ID="lblNotUpdated" runat="server" Font-Bold="true"></asp:Label>
                    <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False" Width="60%"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" CssClass="margin-zero"/>
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentID") %>' />
                                    <asp:HiddenField ID="hdnBillId" runat="server" Value='<%#Eval("BillHistoryId") %>' />
                                    <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FullName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Font-Bold="true" Text='<%#Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"  />
                                <HeaderStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calculative Fine">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCalculativeFine" Font-Bold="true" Text='<%#Eval("CalculativeFine") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Posted Fine">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPostedFine" ForeColor="Green" Font-Bold="true" Text='<%#Eval("PostedFine") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MisMatch">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMisMatch" ForeColor="Blue" Font-Bold="true" Text='<%#Eval("MisMatch") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkStudent"></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both"></div>
        <div style="height: 30px; width: 900px; padding: 15px;"></div>
    </div>
</asp:Content>

