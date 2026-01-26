<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ServayByStudent_EvaluationVarify" CodeBehind="EvaluationVarify.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc1" TagName="BatchUserControlAll" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Evaluation :: Verify</asp:Content>

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
            <label>Course Evaluation Form :: Student Verify</label>
        </div>

        <asp:Panel runat="server" ID="pnMessage">
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </asp:Panel>

        <div>
            <div>
                    <table>
                        <tr>
                            <td>
                                <label class="display-inline field-Title2">Program</label>
                            </td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="uclProgram" class="margin-zero dropDownList" DataValueField="Code" DataTextField="ShortName" OnProgramSelectedIndexChanged="uclProgram_Changed" />
                            </td>
                            <td>
                                <label class="display-inline field-Title">Semester</label>
                            </td>
                            <td>
                                <uc1:SessionUserControl runat="server" ID="uclAcaCal" class="margin-zero dropDownList" />
                            </td>
                            <td>
                                <label class="display-inline field-Title2">Batch :</label>
                            </td>
                            <td>
                                <uc1:BatchUserControl runat="server" ID="uclBatch" class="margin-zero dropDownList" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <label class="display-inline field-Title">Student ID</label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtStudent" class="margin-zero input-Size" placeholder="Student ID" Width="109px"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loadedArea">
                    <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" />
                </div>
        </div>

        <div class="EvaluationVarify-container">
            <asp:Panel ID="PnlEvaluationVarify" runat="server" Width="940px" Wrap="False">
                <asp:GridView ID="gvEvaluationVarify" runat="server" AutoGenerateColumns="False" Width="100%">
                    <RowStyle Height="24px" />
                    <AlternatingRowStyle BackColor="#f5fbfb" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="45px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Font-Bold="False" Text='<%#Eval("Roll") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" Font-Bold="False" Text='<%#Eval("FullName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Evaluation Complete" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblComplete" Font-Bold="False" Text='<%#Eval("Attribute1") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Evaluation Pending" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPending" Font-Bold="False" Text='<%#Eval("Attribute2") %>' /></ItemTemplate>
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
    </div>
</asp:Content>

