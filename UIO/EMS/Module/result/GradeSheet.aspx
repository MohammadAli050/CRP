<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_GradeSheet" Codebehind="GradeSheet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Grade Sheet</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        function CheckDropdown() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlAcaCalBatch').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Semester');
                return false;
            }
            if ($('#MainContainer_ddlProgram').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Program');
                return false;
            }
            if ($('#MainContainer_ddlAcaCalSection').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Course');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server"> 
    <div>
        <div class="PageTitle">
            <label>Grade Sheet [generate] → [uplaod] → [view] → [submit to department for approval]</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="gradeSheet-container">
            <div class="floatLeft gradeSheetBoard" style="margin-right: 10px;">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <table> 
                            <tr> 
                                <td> 
                                    <asp:Label ID="lblAcaCal" runat="server" Text="Semester" style="width: 60px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlAcaCalBatch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcaCal_Change" style="width: 150px;"/>

                                    <asp:Label ID="lblSearch" runat="server" Text="Search" style="margin-left: 25px; width: 65px; display: inline-block;"></asp:Label>
                                    <asp:TextBox ID="txtSearch" runat="server" style="width: 150px;" placeHolder="Course Title / Code" />
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Program" style="width: 60px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" style="width: 150px;" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_Change" >
                                        
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-left: 93px;" OnClick="btnSearch_Click" />
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Course" style="width: 60px; display: inline-block;"></asp:Label>
                                    <asp:DropDownList ID="ddlAcaCalSection" runat="server" style="width: 400px;" />
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnGenerateGradeSheet" runat="server" Text="Generate" Width="150" Height="40" OnClick="btnGenerateGradeSheet_Click" OnClientClick="return CheckDropdown();"></asp:Button>
                            <%--<asp:Button ID="btnAllGenerateGradeSheet" runat="server" Text="Generate All" Width="150" Height="40" OnClick="btnGenerateAllGradeSheet_Click"></asp:Button>--%>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>

                <asp:Panel runat="server" ID="Panel1">
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fuGradeSheet" runat="server" style="margin-left: 0px;" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnImport" runat="server" Text="Import" Width="150" Height="40" OnClick="btnImport_Click"></asp:Button>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnViewGradeSheet" runat="server" Text="View Grade Sheet" Width="150" Height="40" OnClick="btnViewGradeSheet_Click"></asp:Button>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Submit" Width="150" Height="40" OnClick="btnFinalSubmit_Click"></asp:Button>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div class="margin-bottom">
                <asp:Panel ID="PnlGradeSheet" runat="server" Width="600px" Height="397px" ScrollBars="Vertical" Wrap="False">
                    <asp:gridview ID="gvGradeSheet" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                        <AlternatingRowStyle BackColor="#f5fbfb" />
                        <RowStyle Height="24px" />
                        <Columns>
                             <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle Width="2%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblId" Font-Bold="True" Text='<%#Eval("StudentRoll") %>' />
                                    <asp:HiddenField runat="server" ID="hfGradeSheetId" Value='<%#Eval("GradeSheetId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:Label runat="server" ID="lblShareProg2" Font-Bold="True" Text='<%#Eval("StudentName") %>' /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:Label runat="server" ID="lblShareProg3" Font-Bold="True" Text='<%#Eval("ObtainGrade") %>' /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mark" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:Label runat="server" ID="lblGradeSheetTemplate" Font-Bold="True" Text='<%#Eval("ObtainMarks") %>' /></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Found !
                        </EmptyDataTemplate>
                        <RowStyle CssClass="rowCss" />
                        <HeaderStyle CssClass="tableHead" />
                    </asp:gridview>
                </asp:Panel>
            </div>
            <%-- End margin-bottom Grid View --%>
            <div class="cleaner"></div>
        </div>
    </div>
</asp:Content>

