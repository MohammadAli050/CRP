<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PreRegistration" Codebehind="PreRegistration.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Pre Registration Report</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <p>
        Pre Registration Status Report
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Program Name"></asp:Label>
        :
            <asp:DropDownList ID="programListCombo" runat="server" Width="160px"
                DataTextField="NameWithCode" DataValueField="ProgramID">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Batch No."></asp:Label>
        :<asp:DropDownList ID="batchListCombo" DataValueField="AcademicCalenderID" DataTextField="FullCode" runat="server" Width="140px">
        </asp:DropDownList>
        &nbsp;&nbsp; Search:<asp:TextBox ID="searchBox" runat="server" Width="100px" Style="margin-left: 4px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="loadButton0" runat="server" OnClick="loadButton_Click" Text="Load" Width="55px" />

    </p>
    <p>

        <asp:GridView ID="ResultView" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found !!">
            <Columns>
                <asp:BoundField DataField="Roll" HeaderText="Student ID" />
                <asp:BoundField DataField="FullName" HeaderText="Name" />
            </Columns>
        </asp:GridView>

    </p>
    <p>
    </p>


</asp:Content>
