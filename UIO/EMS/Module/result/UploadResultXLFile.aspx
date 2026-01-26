<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="UploadResultXLFile.aspx.cs" Inherits="EMS.Module.result.UploadResultXLFile" %>




<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Exam Result Upload 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>    Exam Result Upload By Excel(Session Wise only for Registered Student)</label>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel01">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="EvaluationVarify-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <div>
                                <table>
                                    <tr>
                                        <td class="auto-style9">
                                            <b>
                                                <asp:Label Text="Program" ID="lblProgram" runat="server"></asp:Label></b>
                                        </td>
                                        <td class="auto-style4">
                                            <uc1:ProgramUserControl ID="ucProgram" runat="server" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                        </td>
                                        <td class="auto-style8">
                                            <b>
                                                <asp:Label Text="Session" ID="lblSem" runat="server"></asp:Label></b>
                                        </td>
                                        <td class="auto-style7">
                                            <uc1:SessionUserControl runat="server" ID="ucSession" />
                                        </td>
                                        <td class="auto-style16">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsave" runat="server" Text="SaveData" OnClick="btnsave_Click" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>  
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnLoad" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <br />
        <table >
            <tr>
                <td>
                    <b><asp:Label ID="Label2" runat="server" Text="StudentsList With Marks" Font-Bold="true" Font-Size="X-Large" Visible="false"></asp:Label></b>
                     <asp:GridView runat="server" ID="gvStudentList" AutoGenerateColumns="False"
                    ShowHeader="true" CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CourseID" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <b>
                                    <asp:Label runat="server" ID="lblCourseID" Text='<%#Eval("CourseCode") %>'></asp:Label></b>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" Height="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RollNumber" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b>
                                <asp:Label runat="server" ID="lblRollNumber" Text='<%#Eval("Roll") %>'></asp:Label></b></b></ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marks" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><b>
                                 <asp:Label runat="server" ID="lblMarks"  Text='<%#Eval("Marks").ToString()=="-1"? "Absent": Eval("Marks")%>' ForeColor='<%# Eval("Marks").ToString()=="-1"?System.Drawing.Color.Red:System.Drawing.Color.DarkGreen %>'></asp:Label></b>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                    </Columns>

                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
                </td>
            </tr>
        </table>
</asp:Content>
