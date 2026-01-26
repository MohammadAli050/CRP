<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ServayByStudent_EvaluationSummary" Codebehind="EvaluationSummary.aspx.cs" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Evaluation :: Summary</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 102px;
        }
        .auto-style2 {
            width: 73px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
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
                <div>
                    <div>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                         <label>Program</label>
                                    </td>
                                    <td>
                                          <asp:DropDownList runat="server" ID="ddlProgram" DataValueField="ProgramId" DataTextField="ShortName" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_Change" />
                                    </td>
                                    <td class="auto-style2">
                                        <label class="display-inline field-Title"style="width:100px">Session :</label>
                                    </td>
                                    <td>
                                        <uc1:SessionUserControl runat="server" ID="uclAcaCal"  OnSessionSelectedIndexChanged="uclBatch_Change"/>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <label class="display-inline field-Title">Course</label>
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList runat="server" ID="ddlAcaCalSection" Height="21px" Width="287px"/>
                                    </td>
                                    <td >
                                         <asp:TextBox runat="server" ID="txtSearchKey" class="margin-zero input-Size" placeholder="Course Code / Title" />
                                    </td>
                                    <td>
                                         <asp:Button runat="server" ID="btnSearch" class="button-margin SearchKey" Text="Search"  OnClick="btnSearch_Click" />
                                    </td>
                                    <td>

                                    </td>
                                </tr>
                            </table>
                            <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="EvaluationSummary-container">
                    <asp:Panel ID="PnlEvaluationSummary" runat="server" Width="800px" Wrap="False">
                        <asp:gridview ID="gvEvaluationSummary" runat="server" AutoGenerateColumns="False" Width="100%">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Course" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCourse" Font-Bold="False" Text='<%#Eval("CourseInfo") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Student" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblTotalStudent" Font-Bold="False" Text='<%#Eval("TotalStudent") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblComplete" Font-Bold="False" Text='<%#Eval("EvaluationCompleteStudent") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblPending" Font-Bold="False" Text='<%# Convert.ToInt32(Eval("TotalStudent")) - Convert.ToInt32(Eval("EvaluationCompleteStudent")) %>' /></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>