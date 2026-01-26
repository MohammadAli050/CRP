<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Student_ThesisAndProjectRegistration" Codebehind="ThesisAndProjectRegistration.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Thesis / Progect Reg.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }
              
        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
        <div class="PageTitle">
            <label>Generate Worksheet</label>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label3" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

         <div class="Message-Area">
            <table>
                <tr style="text-align: left;">
                    <td>
                        <asp:Label ID="lblStudentID" runat="server" Text="Student ID:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtRoll" runat="server" Text=""></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click"></asp:Button></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="tbl-width-lbl"><b>Name :</b></td>
                    <td class="tbl-width">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tbl-width-lbl"><b>Batch :</b></td>
                    <td class="tbl-width">
                        <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tbl-width-lbl"><b>Program :</b></td>
                    <td class="tbl-width">
                        <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>

        </div>
     
        <div style="clear: both;"></div>
       <div class="Message-Area">
            <table>
                <tr style="text-align: left;">

                    <th>
                        <asp:Label ID="Label2" runat="server" Text="Thesis/Project Course:"></asp:Label></th>
                    <th style="width: 100px;">
                        <asp:DropDownList ID="ddlMultipleACUSCourse" runat="server" ReadOnly="true" OnSelectedIndexChanged="ddlMultipleACUSCourse_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList></th>

                    <th>
                        <asp:Label ID="lblTotalCr" runat="server" Text="Total Cr:"></asp:Label></th>
                    <th>
                        <asp:TextBox ID="txtTotalCr" runat="server" ReadOnly="true" Text=""></asp:TextBox></th>
                </tr>
                <tr style="text-align: left;">
                    <th></th>
                    <th></th>
                    <th>
                        <asp:Label ID="lblRemainCr" runat="server" Text="Remain Cr:"></asp:Label></th>
                    <th>
                        <asp:TextBox ID="txtRemainCr" runat="server" ReadOnly="true" Text=""></asp:TextBox></th>
                </tr>
            </table>

        </div>
        <div style="clear: both;"></div>
        <div id="GridViewTable" style="margin: 10px;">
            <fieldset>
                <legend>Result History</legend>

                <asp:GridView runat="server" ID="gvThesisProjectReg" AutoGenerateColumns="False"
                    CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Course Code" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("CourseCredit","{0:00}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("ObtainedGrade") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trimester" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTrimester" Text='<%#Eval("Trimester") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </fieldset>

        </div>
        <div style="clear: both;"></div>

        <div class="Message-Area">
            <table>
                <tr style="text-align: left;">
                    <th>
                        <asp:Label ID="Label1" runat="server" Text=" Cr. for Registration:"></asp:Label></th>
                    <th>
                        <asp:DropDownList ID="ddlMultiSpanDtl" runat="server" ReadOnly="true" DataTextFormatString="{0:F}">
                        </asp:DropDownList></th>
                    <th>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button></th>
                    <th></th>
                </tr>
            </table>
        </div>

        <div style="clear: both;"></div>
        <div id="Div1" class="Message-Area">
            <fieldset>
                <legend>For Registration</legend>
                <table>
                    <tr style="text-align: left;">
                        <th>FormalCode</th>
                        <th>CourseTitle</th>
                        <th>CourseCredit</th>
                        <th> </th>
                    </tr>
                    <tr style="text-align: left;">

                        <td>
                            <asp:HiddenField runat="server" ID="hdnlblIdRW"></asp:HiddenField>
                            <asp:Label runat="server" ID="lblFormalCodeRW"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblCourseTitleRW"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblCreditsRW"></asp:Label></td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDeleteRW" OnClick="btnDeleteRW_Click" Text="Delete"></asp:LinkButton></td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="clear: both;"></div>

        <div class="Message-Area">
            <table>
                <tr style="text-align: left;">
                    <th>
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click"></asp:Button></th>
                </tr>
            </table>
        </div>
        <div style="clear: both;"></div>

    </div>
</asp:Content>

