<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ProfileView.aspx.cs" Inherits="EMS.StudentManagement.ProfileView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Student Profile
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style3
        {
            width: 100px;
        }

        table
        {
            border-collapse: collapse;
        }

        .tbl-width-lbl
        {
            width: 100px;
            padding: 5px;
        }

        .tbl-width
        {
            width: 150px;
            padding: 5px;
        }
        div.text {
            margin: 0;
            padding: 0;
            padding-bottom: .45em;
            font-size: 14px;
            color:black;
        }

        div.text label {
        margin: 0;
        padding: 0;
        display: block;
        font-size: 100%;
        /*padding-top: .1em;*/
        padding-right: .95em;
        width: 16em;
        text-align: right;
        float: left;
        font-weight:bold;
        } 
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 100%">
        <div class="Message-Area">
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table style="padding: 1px; width: 900px;">
                    <tr>
                        <td class="tbl-width-lbl"><b>Program</b></td>
                        <td>
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="tbl-width-lbl"><b>Batch</b></td>
                        <td>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                        </td>
                        <td class="tbl-width-lbl">
                            <label><b>Student ID :</b></label></td>
                        <td class="tbl-width">
                            <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                        </td>

                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>

                    </tr>
                </table>
            </div>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label2" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel ID="pnlStudentBasicInfo" Visible="false" runat="server">
        <div>
            <div style="background-color: #339966">
                <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="18px" Text="Student Basic Information"></asp:Label><br />
            </div>
            <br />
            <div class="text">
                <label>ID: </label>
                <asp:Label ID="lblStudentRoll" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Name: </label>
                <asp:Label ID="lblStudentName" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Phone: </label>
                <asp:Label ID="lblPhone" Text="N/A" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Email: </label>
                <asp:Label ID="lblEmail" Text="N/A" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Father's Name: </label>
                <asp:Label ID="lblFatherName" Text ="N/A" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Mother's Name: </label>
                <asp:Label ID="lblMotherName" Text="N/A" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Date of Birth: </label>
                <asp:Label ID="lblDOB" Text="N/A" runat="server"></asp:Label>
            </div>
            
            <div class="text">
                <label>Present Address: </label>
                <asp:Label ID="lblPresentAddress" Text="N/A" runat="server"></asp:Label>
            </div>
            <div class="text">
                <label>Permanent Address: </label>
                <asp:Label ID="lblPermanentAddress" Text="N/A" runat="server"></asp:Label>
            </div>
            
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlStudentLoad" Visible="true" runat="server">
        <asp:GridView ID="GvStudent" runat="server" AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found." CellPadding="4"
            OnRowCommand="GvStudent_RowCommand">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="StudentId" Visible="false" HeaderText="Id">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Student Name">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle Width="150px" />
                </asp:BoundField>

                <asp:BoundField DataField="Roll" HeaderText="Student Roll">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="150px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <asp:LinkButton ID="DetailButton" CommandName="StudentDetails" Text="Details" ToolTip="Details" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteButton" CommandName="StudentDelete" Enabled="false" Visible="false" Text="Delete" ToolTip="Delete Student" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemStyle CssClass="center" />
                            </asp:TemplateField>--%>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
            <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
