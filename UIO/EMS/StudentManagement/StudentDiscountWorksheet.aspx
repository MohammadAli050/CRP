<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master"
    CodeBehind="StudentDiscountWorksheet.aspx.cs" Inherits="EMS.StudentManagement.StudentDiscountWorksheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
        .dxeButtonEdit .dxeEditArea
        {
            background-color: white;
        }
        .dxeEditArea
        {
            font-family: Tahoma;
            font-size: 9pt;
            border: 1px solid #A0A0A0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Text="Message: "></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove; height: 65px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="Program" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="20px" Width="150px" 
                            AutoPostBack="True" onselectedindexchanged="ddlProgram_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Batch" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlBatch" runat="server" Height="20px" Width="150px" 
                            AutoPostBack="True" onselectedindexchanged="ddlBatch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left; width: 300px;">
                    <div style="text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Student" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlStudent" runat="server" Height="20px" Width="295px" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-right-style: groove; border-left-style: groove;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnGenerateSaveLoad" runat="server" Text="Generate" ToolTip="New generate and load"
                    Width="80px" onclick="btnGenerateSaveLoad_Click" />
                &nbsp;
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" 
                    onclick="btnEdit_Click" />
                &nbsp;
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="float: left; height: 300px">
                    <asp:GridView ID="gvStudentDiscountWorksheet" runat="server" AutoGenerateColumns="False">
                        <SelectedRowStyle BackColor="#00CC99" />
                        <HeaderStyle BackColor="#669999" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove; clear: both">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="20px" Width="90px" OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="20px" Width="90px"
                        OnClick="btnCancel_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
    </div>
</asp:Content>
