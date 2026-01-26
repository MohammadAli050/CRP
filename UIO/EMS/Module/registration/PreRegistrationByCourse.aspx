<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_PreRegistrationByCourse" Codebehind="PreRegistrationByCourse.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Pre Registration Report by Courses</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <p>            
            Pre Registration by Course Status Report</p>
    <p>            
            <asp:Label ID="Label1" runat="server" Text="Course Code"></asp:Label>
            :
            <asp:DropDownList ID="courseListCombo" runat="server" DataTextField="FormalCode" DataValueField="CourseId" Height="20px" Width="109px">
            </asp:DropDownList>

            &nbsp;Search:<asp:TextBox ID="searchBox" runat="server" Width="100px" style="margin-left: 4px"></asp:TextBox>
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
