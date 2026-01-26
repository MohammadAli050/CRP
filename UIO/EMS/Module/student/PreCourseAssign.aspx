<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PreCourseAssign" Codebehind="PreCourseAssign.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Pre-Course Assign</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Pre Mendatory Course</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="preMandatoryCourse-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">Batch</label>
                    <asp:DropDownList runat="server" ID="ddlBatch" class="margin-zero dropDownList" />

                    <label class="display-inline field-Title2">Program</label>
                    <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Pre Course Name</label>
                    <asp:DropDownList runat="server" ID="ddlPreMendatoryCourse" class="margin-zero dropDownList" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Pre Course</label>
                    <asp:DropDownList runat="server" ID="ddlPreCourse" class="margin-zero dropDownList" />

                    <label class="display-inline field-Title2">Main Course</label>
                    <asp:DropDownList runat="server" ID="ddlMainCourse" class="margin-zero dropDownList" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title"></label>
                    <asp:Button runat="server" ID="btnTransfer" class="margin-zero btn-size" Text="Transfer" OnClick="btnTransfer_Click" />

                    <label class="display-inline field-Title3">Admission Solution To U-Cam Solution(Data Transfer)</label>
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title"></label>
                    <asp:Button runat="server" ID="btnView" class="margin-zero btn-size" Text="View" OnClick="btnView_Click" />

                    <label class="display-inline field-Title3">U-Cam Sulution</label>
                </div>
                <%--<div class="loadedArea">
                    <label class="display-inline field-Title"></label>
                    <asp:Button runat="server" ID="btnSave" class="margin-zero btn-size2" Text="Save" OnClick="btnSave_Click" />
                </div>--%>
            </div>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel04">
            <ContentTemplate>
                <div class="preMandatoryCourse-container">
                    <asp:Panel ID="PnlPreMandatoryCourse" runat="server" Width="600px" Wrap="False">
                        <asp:gridview ID="gvPreMandatoryCourse" runat="server" AutoGenerateColumns="False" Width="100%" >
                            <RowStyle Height="30px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="StudentName" HeaderText="Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentRoll" HeaderText="Id">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <%--<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkPreCourse" Checked="true"></asp:CheckBox>
                                            <asp:HiddenField ID="hdStudentPreCourseId" runat="server" Value='<%#Eval("StudentPreCourseId") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>--%>
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