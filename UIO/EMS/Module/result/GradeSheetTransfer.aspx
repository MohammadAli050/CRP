<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_GradeSheetTransfer" Codebehind="GradeSheetTransfer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Grade Transfer</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Transfer Grade</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="gradeSheetTransfer-container">
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <div>
                        <div style="width: 230px; display: inline-block;">
                            <asp:Label ID="lblAcaCal" runat="server" Text="Semester" style="width: 75px; display: inline-block;"></asp:Label>
                            <asp:DropDownList ID="ddlAcaCalBatch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcaCal_Change" style="width: 150px;"/>
                        </div>
                        <div style="width: 220px; display: inline-block; margin-left: 27px;">
                            <asp:Label ID="Label5" runat="server" Text="Program" style="width: 65px; display: inline-block;"></asp:Label>
                            <asp:DropDownList ID="ddlProgram" runat="server" DataValueField="ProgramID" DataTextField="NameWithCode" style="width: 150px;" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_Change" >
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div><br />
                        <div style="display: inline-block;">
                            <asp:Label ID="Label6" runat="server" Text="Course" style="width: 75px; display: inline-block;"></asp:Label>
                            <asp:DropDownList ID="ddlAcaCalSection" runat="server" style="width: 400px;" />
                        </div>
                        <div>
                            <asp:Label ID="lblSearch" runat="server" Text="Search Key" style="width: 75px; display: inline-block;"></asp:Label>
                            <asp:TextBox ID="txtSearch" runat="server" style="width: 145px;" placeHolder="Search Key" />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-left: 10px; height: 25px; width: 100px;" OnClick="btnSearch_Click" />
                        </div>
                        <div style="padding-left: 77px; margin-top: 5px;">
                            <asp:Button ID="btnLoadGradeSheet" runat="server" Text="Load Grade Sheet" Width="150" Height="40" OnClick="btnLoadGradeSheet_Click"></asp:Button>
                            <asp:Button ID="btnShowGradeSheet" runat="server" Text="Show Grade Sheet" Width="150" Height="40" OnClick="btnShowGradeSheet_Click" style="margin-left: 10px;"></asp:Button>
                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Width="150" Height="40" OnClick="btnTransfer_Click" style="margin-left: 257px;"></asp:Button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                <ContentTemplate>
                    <div class="margin-bottom" style="margin-top: 5px;">
                        <asp:Panel ID="PnlGradeSheet" runat="server" Width="800px" Height="397px" ScrollBars="Vertical" Wrap="False">
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

                                    <asp:TemplateField HeaderText="Current Grade" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkCurrentGrade" Checked='<%#Eval("IsCurrentGrade") %>' Enabled="true" style="margin: 0px;" AutoPostBack="true" OnCheckedChanged="chkCurrentGrade_Change" />
                                            <asp:Label runat="server" ID="lblObtainGrade" Font-Bold="True" Text='<%#Eval("ObtainGrade") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Previous Grade" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkPreviousGrade" Checked='<%#Eval("IsPreviousGrade") %>' Enabled="true" style="margin: 0px;" AutoPostBack="true" OnCheckedChanged="chkPreviousGrade_Change" />
                                            <asp:Label runat="server" ID="lblCourseHistoryGrade" Font-Bold="True" Text='<%#Eval("CourseHistoryGrade") %>' />
                                            <asp:HiddenField runat="server" ID="hfCourseHistoryId" Value='<%#Eval("CourseHistoryId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Record" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRecord" Font-Bold="True" Text='<%#Eval("PreviousRecord") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <b>No Data Found !</b>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="rowCss" />
                                <HeaderStyle CssClass="tableHead" />
                            </asp:gridview>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                <ContentTemplate>
                    <div class="margin-bottom" style="margin-top: 5px;">
                        <asp:Panel ID="pnlResultShow" runat="server" Width="800px" Height="397px" ScrollBars="Vertical" Wrap="False">
                            <asp:gridview ID="gvResultShow" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
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
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><asp:Label runat="server" ID="lblName" Font-Bold="True" Text='<%#Eval("StudentName") %>' /></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><asp:Label runat="server" ID="lblGrade" Font-Bold="True" Text='<%#Eval("ObtainedGrade") %>' /></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><asp:Label runat="server" ID="lblStatus" Font-Bold="True" Text='<%#Eval("CourseStatus") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <b>No Data Found !</b>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="rowCss" />
                                <HeaderStyle CssClass="tableHead" />
                            </asp:gridview>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

