<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="StudentFreshBill.aspx.cs" Inherits="EMS.miu.registration.StudentFreshBill" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Registration Menual Entry
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
     <style type="text/css">
        .modalBackground {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .font {
            font-size: 12px;
            
        }

        .cursor {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 1100px">

        <%-- <div class="PageTitle">
            <label>Student General Bill</label>
        </div>--%>

        <div class="Message-Area">
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table style="padding: 1px; width: 900px;">
                    <tr>
                        <td class="tbl-width-lbl">
                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Student ID :"></asp:Label></td>
                        <td class="tbl-width">
                            <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                        </td>
                         <td>
                             <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" class="margin-zero dropDownList" />
                        </td>
                        <td>
                             <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                        </td>
                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
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
       
        <div id="GridViewTable">
           
             <div style="clear: both;"></div>
            <asp:GridView runat="server" ID="gvBillView" AutoGenerateColumns="False"
                AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                PageSize="20" CssClass="gridCss" OnRowCommand="gvBillView_RowCommand" >
                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" />

                <Columns>
                    <asp:TemplateField HeaderText="Code" Visible="false"  HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStudentCourseHisId" Text='<%#Eval("StudentCourseHistoryId") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurpose" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("Fees") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                   <%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">--%>
                        <%--<ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit"
                                ToolTip="Item Edit" CommandName="EditBillHistory" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                                OnClientClick="return confirm('Are you sure to Delete this ?')"
                                ToolTip="Item Delete" CommandName="DeleteBillHistory" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                            </asp:LinkButton>
                        </ItemTemplate>--%>
                   <%-- </asp:TemplateField>--%>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <%--<RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />>--%>
                <EmptyDataTemplate>
                    No data found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
         <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Visible="true">
                        <asp:Label ID="lblTotalPayble" runat="server" Text="Your total amount : "></asp:Label>
                        <asp:Label ID="lblTotalPaybleAmount" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
