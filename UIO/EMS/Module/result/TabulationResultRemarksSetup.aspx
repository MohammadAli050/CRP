<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="TabulationResultRemarksSetup.aspx.cs" Inherits="EMS.miu.result.TabulationResultRemarksSetup" %>

<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Tabulation & Result Remarks Update
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            var maincontainer = $('#MasterBody').height();
            var bodycontainer = $(document).height();
        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 20px;
        }

        .auto-style3 {
            width: 57px;
        }

        .auto-style4 {
            width: 18px;
        }

        .auto-style5 {
            width: 8px;
        }

        .auto-style6 {
            width: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle floatLeft">
            <label>Tabulation & Result Remarks Update</label>
        </div>
        <div style="height: 30px;">
            <div id="divProgress" class="floatRight" style="padding-top: 7px; display: none;">
                <div class="floatRight">
                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                    <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                    <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                </div>
            </div>
        </div>
        <div class="cleaner"></div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="Message-Area">
            <asp:UpdatePanel runat="server" ID="UpdatePanel03">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="auto-style9"><b>Program</b></td>
                            <td class="auto-style4">
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                            </td>
                            <td class="auto-style6">&nbsp</td>
                            <td class="auto-style8"><b>Semester</b></td>
                            <td class="auto-style2">
                                <uc1:SessionUserControl runat="server" ID="ucSession" />
                            </td>
                            <td class="auto-style5">&nbsp</td>
                            <td class="auto-style10"><b>Batch</b></td>
                            <td class="auto-style14">
                                <uc2:BatchUserControl runat="server" ID="ucBatch" />
                            </td>
                            <td class="auto-style5">&nbsp</td>
                            <td class="auto-style9"><b>Student ID</b></td>
                            <td class="auto-style4">
                                <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" Width="102" />
                            </td>
                            <td class="auto-style6">&nbsp</td>
                            <td>
                                <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" />
                            </td>

                        </tr>

                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="ResultProcess-container"> 
                    <div id="RemarksDiv">
                        <table>
                            <tr>
                                <td class="auto-style9"><b>Remarks : </b></td>
                                <td>
                                    <asp:DropDownList ID="ddlRemarksText" runat="server" Style="width: 150px;" Width="195px">
                                        <asp:ListItem Value="0">Others</asp:ListItem>
                                        <asp:ListItem Value="1">Passed</asp:ListItem>
                                        <asp:ListItem Value="2">Promoted (Conditions Applicable)</asp:ListItem>
                                        <asp:ListItem Value="3">Withdraw (Dropped out from the program)</asp:ListItem>
                                        <asp:ListItem Value="4">Reported</asp:ListItem>
                                        <asp:ListItem Value="5">Degree Awarded</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOtherRemarks" placeholder="Others Remarks" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnTabulationApply" runat="server" Text="Apply in Tabulation" AutoPostBack="true" OnClick="btnTabulationApply_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnResultApply" runat="server" Text="Apply in Result" AutoPostBack="true" OnClick="btnResultApply_Click" />
                                </td>

                            </tr>
                        </table>

                    </div>
                    <asp:Panel ID="pnlRemarks" runat="server" Width="85%" Wrap="False">
                        <asp:GridView ID="gvRemarks" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                        <asp:HiddenField ID="hdnStudentId" runat="server" Value='<%#Eval("StudentId") %>' />
                                        <asp:HiddenField ID="hdnAcaCalId" runat="server" Value='<%#Eval("AcaCalId") %>' />
                                        <asp:Label runat="server" ID="lblName" Font-Bold="True" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField Visible="false" HeaderText="CGPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCGPA" Font-Bold="False" Text='<%#Eval("CGPA") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Roll" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="False" Text='<%#Eval("Roll") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Session Name" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSessionName" Font-Bold="False" Text='<%#Eval("SessionName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TabulationRemarks" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTabulationRemarks" Width="270px" runat="server" Text='<%#Eval("TabulationRemarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                    <ItemStyle HorizontalAlign="Center"  Width="300px"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ResultRemarks" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtResultRemarks" Width="270px" runat="server" Text='<%#Eval("ResultRemarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                    <ItemStyle HorizontalAlign="Center"  Width="300px"/>
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
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel01" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnProcessGroup" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnProcessGroup" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel01" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnProcessStudent" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnProcessStudent" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender3" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnViewGroup" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnViewGroup" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender4" TargetControlID="UpdatePanel02" runat="server">
            <Animations>
                <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnViewStudent" Enabled="false" /></Parallel></OnUpdating>
                <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction   AnimationTarget="btnViewStudent" Enabled="true" /></Parallel></OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
