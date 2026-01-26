<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_UserCreate" Codebehind="UserCreate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">User Create</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function txtAvailable_Click() {
            var user = $('#ctl00_MainContainer_txtUserName').val();
            if (user != '') {
                $.ajax({
                    type: "POST",
                    url: "UserCreate.aspx/AvailabilityCheck",
                    data: "{UserName: '" + user + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d;
                        if (result == 'found') { $('#ctl00_MainContainer_lblMsg').text('Already Exist'); }
                        else if (result == 'null') { $('#ctl00_MainContainer_lblMsg').text('Availabe'); }
                        else if (result == 'error') { $('#ctl00_MainContainer_lblMsg').text('Availabe'); }
                    },
                    error: function () {
                        alert("error 101: Ajax Fail");
                    }
                });
            }
            else {
                $('#ctl00_MainContainer_lblMsg').text('First Type CODE');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>User Create</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="UserCreate-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">User Name</label>
                    <asp:TextBox runat="server" ID="txtUserName" class="margin-zero input-Size" placeholder="New User Name" />
                    <input type="button" id="txtAvailable" class="margin-zero AvailableKey" value="available" onclick="txtAvailable_Click();" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Password</label>
                    <asp:TextBox runat="server" ID="txtPassword" class="margin-zero input-Size" placeholder="Password" TextMode="Password" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Role</label>
                    <asp:DropDownList runat="server" ID="ddlRole" class="margin-zero dropDownList" DataValueField="ID" DataTextField="RoleName" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">User Active</label>
                    <asp:CheckBox runat="server" ID="chkIsActive" class="ActiveUser" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title"></label>
                <asp:Button runat="server" ID="btnCreate" class="margin-zero btn-size" Text="Create" OnClick="btnCreate_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

