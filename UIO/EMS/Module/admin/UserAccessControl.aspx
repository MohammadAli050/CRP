<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_UserAccessControl" Codebehind="UserAccessControl.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">User Access Control</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            //Initial
            $('#personLoading').hide();
            $('#userLoading').hide();
            var optionString = "<option value='0'>Select</option>";
            $('#ddlUser').html(optionString);//Load Dropdown
            $('#ddlPersonStudent').html(optionString);//Load Dropdown
            //Initial
            //Role Dropdown Load
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/LoadRoleDropDown",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var roleList = msg.d;
                    var role = roleList.split(";");
                    var optionString = "<option value='-1'>Select</option>";
                    for (var i = 0; i < role.length - 1; i++) {
                        roleField =role[i].split(",");
                        optionString += "<option value='" + roleField[0] + "'>" + roleField[1] + "</option>";
                    }
                    $('#ddlRole').html(optionString);//Load Dropdown
                },
                error: function () {
                    alert("Error: Role");
                }
            });
            //Role Dropdown Load

            //Menu Dropdown Load
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/LoadMenuDropDown",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var menuList = msg.d;
                    var menu = menuList.split(";");
                    var optionString = "<option value='0'>Select</option>";
                    for (var i = 0; i < menu.length - 1; i++) {
                        menuField = menu[i].split(",");
                        optionString += "<option value='" + menuField[0] + "'>" + menuField[1] + "</option>";
                    }
                    $('#ddlMenu').html(optionString);//Load Dropdown
                },
                error: function () {
                    alert("Error: Menu");
                }
            });
            //Menu Dropdown Load

            //Menu Dropdown Load
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/LoadButtonListDropDown",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var buttonList = msg.d;
                    var button = buttonList.split(";");
                    var optionString = "<option value='0'>Select</option>";
                    for (var i = 0; i < button.length - 1; i++) {
                        buttonField = button[i].split(",");
                        optionString += "<option value='" + buttonField[0] + "'>" + buttonField[1] + "</option>";
                    }
                    $('#ddlButton').html(optionString);//Load Dropdown
                },
                error: function () {
                    alert("Error: Button");
                }
            });
            //Menu Dropdown Load
        });

        function DeleteConfirm() {
            if (confirm("Are you sure want to delete record?"))
                return true;
            else
                return false;
        }

        function btnPersonSearch_Click(userId) {
            $('#MainContainer_lblMsg').text('');

            $('#hdUserId').val('');
            $('#ddlRole').val('-1');
            $('#MainContainer_txtUserValidFrom').val('');
            $('#MainContainer_txtUserValidTo').val('');
            $('#lblRoleMsg').text('');

            var optionString = "<option value='0'>Select</option>";
            
            $('#txtUserSearch').val('');
            $('#ddlUser').html(optionString);//Load Dropdown
            $('#lblUserMsg').text('');

            $('#lblPersonMsg').text('');
            $('#personLoading').show();
            var personKey = $('#txtPersonSearch').val();
            if (personKey == '') {
                $('#personLoading').hide();
                $('#MainContainer_lblMsg').text('Person Name Please');
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "UserAccessControl.aspx/LoadPersonDropDown",
                    data: "{SearchKey: '" + personKey + "', UserId: '" + userId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#personLoading').hide();

                        var personList = msg.d;
                        var person = personList.split(";");
                        var optionString = "<option value='0'>Select</option>";
                        for (var i = 0; i < person.length - 1; i++) {
                            personField = person[i].split(",");
                            optionString += "<option value='" + personField[0] + "'>" + personField[1] + "</option>";
                        }
                        $('#ddlPersonStudent').html(optionString);//Load Dropdown
                    },
                    error: function () {
                        $('#personLoading').hide();
                        alert("Error: Person");
                    }
                });
            }
        }

        function btnUserSearch_Click(personId) {
            $('#MainContainer_lblMsg').text('');

            $('#hdUserId').val('');
            $('#ddlRole').val('-1');
            $('#MainContainer_txtUserValidFrom').val('');
            $('#MainContainer_txtUserValidTo').val('');
            $('#lblRoleMsg').text('');

            var optionString = "<option value='0'>Select</option>";

            $('#ddlPersonStudent').html(optionString);//Load Dropdown
            $('#txtPersonSearch').val('');
            $('#lblPersonMsg').text('');

            $('#lblUserMsg').text('');
            $('#userLoading').show();
            var userKey = $('#txtUserSearch').val();
            if (userKey == '') {
                $('#MainContainer_lblMsg').text('User Id Please');
                $('#userLoading').hide();
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "UserAccessControl.aspx/LoadUserDropDown",
                    data: "{SearchKey: '" + userKey + "', PersonId: '" + personId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#userLoading').hide();

                        var userList = msg.d;
                        var user = userList.split(";");
                        var optionString = "<option value='0'>Select</option>";
                        for (var i = 0; i < user.length - 1; i++) {
                            userField = user[i].split(",");
                            optionString += "<option value='" + userField[0] + "'>" + userField[1] + "</option>";
                        }
                        $('#ddlUser').html(optionString);//Load Dropdown
                    },
                    error: function () {
                        $('#userLoading').hide();
                        alert("Error: Person");
                    }
                });
            }
        }

        function DeleteMenuRow(rowNo) {
            var flag = DeleteConfirm();
            if (flag == true) {
                $.ajax({
                    type: "POST",
                    url: "UserAccessControl.aspx/DeleteUserMenu",
                    data: "{Id: '" + rowNo + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d;
                        if (result == 'True') {
                            $('#lblMenuMsg').text('Delete Successfully');
                            $('#' + rowNo).remove();
                        }
                        else {
                            $('#lblMenuMsg').text('Error 402');
                        }
                    },
                    error: function () {
                        alert("Error: Menu Delete");
                    }
                });
            }
        }

        function DeleteUserMenuRow(rowNo) {
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/DeleteUserMenu",
                data: "{Id: '" + rowNo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;
                    if (result == 'True') {
                        $('#' + rowNo).remove();
                    }
                },
                error: function () {
                    alert("Error: Role Menu Delete");
                }
            });
        }

        function AddMenuRow() {
            $('#lblMenuMsg').text('');

            //var rowCount = $('#tblMenu tr').length;
            var menu = $('#ddlMenu option:selected').text();
            var validFrom = document.getElementById('<%=txtMenuValidFrom.ClientID%>').value;
            var validTo = document.getElementById('<%=txtMenuValidTo.ClientID%>').value;
            var userId = $('#ddlUser').val();
            var menuId = $('#ddlMenu').val();
            if (userId == '0') { $('#lblMenuMsg').text('Please Select User'); return; }
            if (menuId == '0') { $('#lblMenuMsg').text('Please Select Menu'); return; }
            if (validFrom == '') { $('#lblMenuMsg').text('Please Select Valid-From Date'); return; }
            if (validTo == '') { $('#lblMenuMsg').text('Please Select Valid-To Date'); return; }

            var flagDuplicate = 0;
            $('#tblMenu tr').find('input[type="text"]').each(function () {
                if ($(this).val() == menu) {
                    $('#lblMenuMsg').text('This menu already added');
                    flagDuplicate = 1;
                }
            });
            $('#tblRoleMenu tr').find('input[type="text"]').each(function () {
                if ($(this).val() == menu) {
                    $('#lblMenuMsg').text('This menu already added');
                    flagDuplicate = 1;
                }
            });
            if (flagDuplicate == 1)
                return;
            else
                var flag = SaveUserMenu(userId, menuId, validFrom, validTo, menu);
        }

        function SaveUserMenu(userId, menuId, validFrom, validTo, menu) {
            $('#lblMenuMsg').val('');

            var flagInsertOperation = '';

            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/SaveUserMenuWithDateRange",
                data: "{UserId: '" + userId + "', MenuId: '" + menuId + "', ValidFrom: '" + validFrom + "', ValidTo: '" + validTo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;
                    if (result == 'Error') {
                        $('#lblMenuMsg').text('Error 401');
                        return '';
                    }
                    else {
                        $('#lblMenuMsg').text('Saved');
                        var rowCount = result;
                        var rowData =   "<tr id='" + rowCount + "'>"
                                        + "<td><input type='text' value='" + menu + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + validFrom + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + validTo + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='button' value='Delete' onclick='DeleteMenuRow(" + rowCount + ");' class='btn-size' /></td>"
                                        + "</tr>";
                        $("#tblMenu tr:last").after(rowData);
                    }
                },
                error: function () {
                    alert("Error: Menu Insert");
                    $('#lblMenuMsg').text('Error 402');
                }
            });
        }

        function chkRoleMenu_Click(menuId) {
            var isChecked = $('#' + menuId).is(':Checked');
            var userMenuId = $('#' + menuId).val();
            var userId = $('#ddlUser').val();
            if (isChecked) {
                //Delete From UserMenu
                DeleteUserMenuRow(userMenuId);
                $('#' + menuId).val('');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "UserAccessControl.aspx/SaveUserMenu",
                    data: "{UserId: '" + userId + "', MenuId: '" + menuId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d;
                        if(result != 'Error')
                            $('#' + menuId).val(result);
                    },
                    error: function () {
                        $('#lblMenuMsg').text('Error 1101');
                    }
                });

            }
        }

        function DeleteButtonRow(rowNo) {
            var flag = DeleteConfirm();
            if (flag == true) {
                $.ajax({
                    type: "POST",
                    url: "UserAccessControl.aspx/DeleteUserObjectControl",
                    data: "{Id: '" + rowNo + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d;
                        if (result == 'True') {
                            $('#lblButtonMsg').text('Delete Successfully');
                            $('#' + rowNo).remove();
                        }
                        else {
                            $('#lblButtonMsg').text('Error 402');
                        }
                    },
                    error: function () {
                        alert("Error: Button Delete");
                    }
                });
            }
        }

        function AddButtonRow() {
            $('#lblButtonMsg').text('');

            //var rowCount = $('#tblButton tr').length;

            var button = $('#ddlButton option:selected').text();
            var validFrom = $('#MainContainer_txtButtonValidFrom').val();
            var validTo = $('#MainContainer_txtButtonValidTo').val();
            var userId = $('#ddlUser').val();
            var buttonId = $('#ddlButton').val();

            if (userId == '0') { $('#lblButtonMsg').text('Please Select User'); return; }
            if (buttonId == '0') { $('#lblButtonMsg').text('Please Select Menu'); return; }
            if (validFrom == '') { $('#lblButtonMsg').text('Please Select Valid-From Date'); return; }
            if (validTo == '') { $('#lblButtonMsg').text('Please Select Valid-To Date'); return; }

            var flagDuplicate = 0;
            $('#tblButton tr').find('input[type="text"]').each(function () {
                if ($(this).val() == button) {
                    $('#lblButtonMsg').text('This button already added');
                    flagDuplicate = 1;
                }
            });

            if (flagDuplicate == 1)
                return;
            else
                var flag = SaveUserObjectControl(userId, buttonId, validFrom, validTo, button);
        }

        function SaveUserObjectControl(userId, buttonId, validFrom, validTo, button) {
            $('#lblButtonMsg').val('');

            var flagInsertOperation = '';

            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/SaveUserObjectControl",
                data: "{UserId: '" + userId + "', ButtonId: '" + buttonId + "', ValidFrom: '" + validFrom + "', ValidTo: '" + validTo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;
                    if (result == 'Error') {
                        $('#lblButtonMsg').text('Error 501');
                        return '';
                    }
                    else {
                        $('#lblButtonMsg').text('Saved');
                        var rowCount = result;
                        var rowData =   "<tr id='" + rowCount + "'>"
                                        + "<td><input type='text' value='" + button + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + validFrom + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + validTo + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='button' value='Delete' onclick='DeleteButtonRow(" + rowCount + ");' class='btn-size' ></td>"
                                        + "</tr>";
                        $("#tblButton tr:last").after(rowData);
                    }
                },
                error: function () {
                    alert("Error: Button Insert");
                    $('#lblButtonMsg').text('Error 502');
                }
            });
        }

        function ddlPersonStudent_Change() {
            $('#personLoading').show();

            $('#MainContainer_lblMsg').text('');
            $('#lblPersonMsg').text('');
            $('#lblUserMsg').text('');

            var userKey = '';
            var personId = $('#ddlPersonStudent').val();
            if (personId == '0') {
                $('#personLoading').hide();

                $('#tblMenu tr:not(:first)').remove();
                $('#tblButton tr:not(:first)').remove();
                $('#tblRoleMenu tr:not(:first)').remove();
                $('#lblMenuMsg').text('');
                $('#lblButtonMsg').text('');
                $('#ddlRole').val('-1');
                $('#MainContainer_txtUserValidFrom').val('');
                $('#MainContainer_txtUserValidTo').val('');

                var optionString = "<option value='0'>Select</option>";
                $('#ddlUser').html(optionString);//Load Dropdown
                return;
            }
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/LoadUserDropDown",
                data: "{SearchKey: '" + userKey + "', PersonId: '" + personId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#personLoading').hide();

                    var userList = msg.d;
                    var user = userList.split(";");
                    var singleIndex;
                    var optionString = "<option value='0'>Select</option>";
                    for (var i = 0; i < user.length - 1; i++) {
                        userField = user[i].split(",");
                        optionString += "<option value='" + userField[0] + "'>" + userField[1] + "</option>";
                        singleIndex = userField[0];
                    }
                    $('#ddlUser').html(optionString);//Load Dropdown

                    if (user.length - 1 == 0) {
                        $('#lblPersonMsg').text('No User');
                        $('#tblMenu tr:not(:first)').remove();
                        $('#tblButton tr:not(:first)').remove();
                        $('#lblMenuMsg').text('');
                        $('#lblButtonMsg').text('');
                    }
                    else if (user.length - 1 == 1) {
                        $('#lblPersonMsg').text('Single User');
                        userField = user[0].split(",");
                        $('#ddlUser').val(singleIndex);
                        SelectRole(singleIndex);

                        $('#tblMenu tr:not(:first)').remove();
                        $('#tblButton tr:not(:first)').remove();
                        $('#lblMenuMsg').text('');
                        $('#lblButtonMsg').text('');
                        RetrieveMenu(singleIndex);
                        RetrieveButton(singleIndex);
                    }
                    else {
                        $('#tblMenu tr:not(:first)').remove();
                        $('#tblButton tr:not(:first)').remove();
                        $('#lblMenuMsg').text('');
                        $('#lblButtonMsg').text('');
                        $('#lblPersonMsg').text('Multi User');
                    }
                },
                error: function () {
                    $('#personLoading').hide();
                    alert("Error: Person");
                }
            });
        }

        function ddlUser_Change() {
            $('#userLoading').show();

            $('#MainContainer_lblMsg').text('');
            $('#lblUserMsg').text('');
            $('#lblPersonMsg').text('');

            $('#txtUserSearch').val('');
            var personKey = '';
            var userId = $('#ddlUser').val();
            if (userId == '0') {
                $('#userLoading').hide();

                var optionString = "<option value='0'>Select</option>";
                $('#ddlPersonStudent').html(optionString);//Load Dropdown
                $('#tblMenu tr:not(:first)').remove();
                $('#tblButton tr:not(:first)').remove();
                $('#tblRoleMenu tr:not(:first)').remove();
                $('#lblMenuMsg').text('');
                $('#lblButtonMsg').text('');
                $('#ddlRole').val('-1');
                $('#MainContainer_txtUserValidFrom').val('');
                $('#MainContainer_txtUserValidTo').val('');

                return;
            }
            else {
                SelectRole(userId);
                $('#tblMenu tr:not(:first)').remove();
                $('#tblButton tr:not(:first)').remove();
                $('#lblMenuMsg').text('');
                $('#lblButtonMsg').text('');
                RetrieveMenu(userId);
                RetrieveButton(userId);
            }

            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/LoadPersonDropDown",
                data: "{SearchKey: '" + personKey + "', UserId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#userLoading').hide();

                    var personList = msg.d;
                    var person = personList.split(";");
                    var singleIndex;
                    var optionString = "<option value='0'>Select</option>";
                    for (var i = 0; i < person.length - 1; i++) {
                        personField = person[i].split(",");
                        optionString += "<option value='" + personField[0] + "'>" + personField[1] + "</option>";
                        singleIndex = personField[0];
                    }
                    $('#ddlPersonStudent').html(optionString);//Load Dropdown

                    if (person.length - 1 == 0) {
                        $('#lblUserMsg').text('No Person');
                    }
                    else if (person.length - 1 == 1) {
                        $('#lblUserMsg').text('Single Person');
                        personField = person[0].split(",");
                        $('#ddlPersonStudent').val(singleIndex);
                    }
                    //else {
                    //    $('#lblUserMsg').text('Multi Person');
                    //}
                },
                error: function () {
                    $('#userLoading').hide();
                    alert("Error: Person");
                }
            });
        }

        function ddlRole_Change() {
            $('#tblRoleMenu tr:not(:first)').remove();
        }

        function SelectRole(userId) {
            $('#MainContainer_lblMsg').text('');
            $('#lblRoleMsg').text('');
            $('#MainContainer_txtUserValidFrom').val('');
            $('#MainContainer_txtUserValidTo').val('');
            $('#ddlRole').val('-1');
            $('#hdUserId').val('');

            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/RetrieveRoleInfo",
                data: "{UserId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var roleInfo = msg.d;
                    if (roleInfo != '' && roleInfo != 'Error') {
                        var info = roleInfo.split(",");

                        $('#hdUserId').val(info[0]);
                        $('#ddlRole').val(info[1]);
                        $('#MainContainer_txtUserValidFrom').val(info[2]);
                        $('#MainContainer_txtUserValidTo').val(info[3]);

                        RetrieveRoleMenu(info[1], userId);
                    }
                },
                error: function () {
                    alert("Error: Role");
                }
            });
        }

        function btnSaveRole_Click() {
            $('#MainContainer_lblMsg').text('');
            $('#lblRoleMsg').text('');

            var userId = $('#hdUserId').val();
            var roleId = $('#ddlRole').val();
            var validFrom = $('#MainContainer_txtUserValidFrom').val();
            var validTo = $('#MainContainer_txtUserValidTo').val();

            if (userId == '') { $('#MainContainer_lblMsg').text('Error: 301'); return;}
            if (roleId == '-1') { $('#MainContainer_lblMsg').text('Please Select Role'); return;}
            if (validFrom == '') { $('#MainContainer_lblMsg').text('From Date Please'); return;}
            if (validTo == '') { $('#MainContainer_lblMsg').text('To Date Please'); return;}

            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/SaveRoleInfo",
                data: "{UserId: '" + userId + "', RoleId: '" + roleId + "', ValidFrom: '" + validFrom + "', ValidTo: '" + validTo + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;
                    if(result == 'Success')
                        $('#lblRoleMsg').text('Saved');
                    else
                        $('#lblRoleMsg').text('Error: 303');
                },
                error: function () {
                    alert("Error: Role Update");
                }
            });
        }

        function RetrieveMenu(userId) {
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/RetrieveMenuList",
                data: "{UserId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var menuList = msg.d;
                    var menu = menuList.split(";");
                    var rowString = '';
                    for (var i = 0; i < menu.length - 1; i++) {
                        menuField = menu[i].split(",");
                        rowString += "<tr id='" + menuField[0] + "'><td><input type='text' value='" + menuField[1] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + menuField[2] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + menuField[3] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='button' value='Delete' onclick='DeleteMenuRow(" + menuField[0] + ");' class='btn-size' ></td>"
                                        + "</tr>";
                    }
                    $("#tblMenu tr:last").after(rowString);//Load Menu Table
                },
                error: function () {
                    alert("Error: Retrieve Menu");
                }
            });
        }

        function RetrieveButton(userId) {
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/RetrieveButtonList",
                data: "{UserId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var buttonList = msg.d;
                    var button = buttonList.split(";");
                    var rowString = '';
                    for (var i = 0; i < button.length - 1; i++) {
                        buttonField = button[i].split(",");
                        rowString += "<tr id='" + buttonField[0] + "'><td><input type='text' value='" + buttonField[1] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + buttonField[2] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='text' value='" + buttonField[3] + "' class='margin-zero input-Size' disabled='disabled' /></td>"
                                        + "<td><input type='button' value='Delete' onclick='DeleteButtonRow(" + buttonField[0] + ");' class='btn-size' ></td>"
                                        + "</tr>";
                    }
                    $("#tblButton tr:last").after(rowString);//Load Button Table
                },
                error: function () {
                    alert("Error: Retrieve Button");
                }
            });
        }

        function RetrieveRoleMenu(roleId, userId) {
            $.ajax({
                type: "POST",
                url: "UserAccessControl.aspx/RetrieveRoleMenuList",
                data: "{RoleId: '" + roleId + "', UserId: '" + userId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var roleMenuList = msg.d;
                    var roleMenu = roleMenuList.split(";");
                    var rowString = '';
                    
                    for (var i = 0; i < roleMenu.length - 1; i++) {
                        roleMenuField = roleMenu[i].split(",");
                        var statusChecked = '';
                        if (roleMenuField[2] == '0')
                            statusChecked = "checked='checked'";
                        rowString += "<tr><td><input type='checkbox' id='" + roleMenuField[0] + "' value='" + roleMenuField[2] + "' " + statusChecked + " onclick='chkRoleMenu_Click(" + roleMenuField[0] + ");' /></td>"
                                        + "<td colspan='3'><input type='text' value='" + roleMenuField[1] + "' class='margin-zero input-Size5' disabled='disabled' /></td>"
                                        + "</tr>";
                    }
                    $('#tblRoleMenu tr:not(:first)').remove();
                    $("#tblRoleMenu tr:last").after(rowString);//Load Role Menu Table
                },
                error: function () {
                    alert("Error: Retrieve Role Menu");
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>User Access Control</label>
        </div>
        
        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>
        <div class="UserAccessControl-container">

            <div class="floatLeft CommonPanel">
                <div class="div-margin">
                    <label class="display-inline field-Title"><b>Person</b></label>
                </div>
                <hr />
                <div class="div-margin">
                    <label class="display-inline field-Title">Search</label>
                    <input type="text" id="txtPersonSearch" class="margin-zero input-Size2" placeholder="Person Name" />
                    <input type="button" id="btnPersonSearch" class="button-margin SearchKey" value="Search" onclick="btnPersonSearch_Click(0);" />
                </div>
                <div class="div-margin">
                    <label class="display-inline field-Title">Person</label>
                    <select id="ddlPersonStudent" class="margin-zero dropDownList1" onchange="ddlPersonStudent_Change();"></select>
                </div>
                <div class="div-margin loadingPanel">
                    <label class="display-inline field-Title"></label>
                    <img id="personLoading" src="../Images/Loading.gif" />
                    <label id="lblPersonMsg"></label>
                </div>
            </div>

            <div class="floatLeft">
                <div class="CommonPanel">
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>User</b></label>
                    </div>
                    <hr />
                    <div class="div-margin">
                        <label class="display-inline field-Title">Search</label>
                        <input type="text" id="txtUserSearch" class="margin-zero input-Size" placeholder="User ID" />
                        <input type="button" id="btnUserSearch" class="button-margin SearchKey" value="Search" onclick="btnUserSearch_Click(0);" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title">User ID</label>
                        <select id="ddlUser" class="margin-zero dropDownList" onchange="ddlUser_Change();"></select>
                    </div>
                    <div class="div-margin loadingPanel">
                        <label class="display-inline field-Title"></label>
                        <img id="userLoading" src="../Images/Loading.gif" />
                        <label id="lblUsersMsg"></label>
                    </div>
                </div>
            </div>

            <div class="cleaner"></div>

            <div class="div-margin">
                <div class="CommonPanel RolePanel" style="display:none;">
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Role</b></label>
                    </div>
                    <hr />
                    <div class="div-margin">
                        <label class="display-inline field-Title">Role</label>
                        <select id="ddlRole" class="margin-zero dropDownList" onchange="ddlRole_Change();"></select>
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title">Valid From :</label>
                        <asp:TextBox runat="server" ID="txtUserValidFrom" class="margin-zero input-Size" placeholder="Valid From" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtUserValidFrom" Format="dd/MM/yyyy" />

                        <label class="display-inline field-Title2">Valid To :</label>
                        <asp:TextBox runat="server" ID="txtUserValidTo" class="margin-zero input-Size" placeholder="Valid To" />
                        <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtUserValidTo" Format="dd/MM/yyyy" />
                    </div>
                    <div class="div-margin">
                        <label class="display-inline field-Title"></label>
                        <input type="button" id="btnSave" class="margin-zero btn-size" value="Save" onclick="btnSaveRole_Click();" />
                        <input type="hidden" id="hdUserId" value="" />
                    </div>
                    <div class="div-margin loadingPanel">
                        <label class="display-inline field-Title"></label>
                        <label id="lblRoleMsg"></label>
                    </div>
                </div>
            </div>

            <div class="div-margin">
                <div class="CommonPanel RolePanel">
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Menu</b></label>
                        <label class="lblMsg" id="lblMenuMsg"></label>
                    </div>
                    <hr />
                    <div class="div-margin">
                        <table class="tableStyle">
                            <tr>
                                <th>Menu</th>
                                <th>Valid From</th>
                                <th>Valid To</th>
                                <th>Action</th>
                            </tr>
                            <tr>
                                <td>
                                    <select id="ddlMenu" class="margin-zero dropDownList"></select>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMenuValidFrom" class="margin-zero input-Size" placeholder="Valid From" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMenuValidFrom" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMenuValidTo" class="margin-zero input-Size" placeholder="Valid To" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtMenuValidTo" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <input type="button" id="btnMenuAdd" value="Add" class="btn-size" onclick="return AddMenuRow();" />
                                </td>
                            </tr>
                        </table>
                        <table class="tableStyle" id="tblMenu">
                            <tr></tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="div-margin">
                <div class="CommonPanel RolePanel">
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Allowed Menu Options</b></label>
                        <label class="lblMsg" id="Label1"></label>
                    </div>
                    <hr />
                    <div class="div-margin">
                        <table class="tableStyle" id="tblRoleMenu" >
                            <tr></tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="div-margin"  style="display:none;">
                <div class="CommonPanel ButtonPanel">
                    <div class="div-margin">
                        <label class="display-inline field-Title"><b>Button</b></label>
                        <label class="lblMsg" id="lblButtonMsg"></label>
                    </div>
                    <hr />
                    <div class="div-margin">
                        <table class="tableStyle">
                            <tr>
                                <th>Button</th>
                                <th>Valid From</th>
                                <th>Valid To</th>
                                <th>Action</th>
                            </tr>
                            <tr>
                                <td>
                                    <select id="ddlButton" class="margin-zero dropDownList">
                                        <%--<option value="0">Select0</option>
                                        <option value="1">Select1</option>
                                        <option value="2">Select2</option>
                                        <option value="3">Select3</option>
                                        <option value="4">Select4</option>--%>
                                    </select>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtButtonValidFrom" class="margin-zero input-Size" placeholder="Valid From" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtButtonValidFrom" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtButtonValidTo" class="margin-zero input-Size" placeholder="Valid To" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtButtonValidTo" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <input type="button" id="btnButtonAdd" value="Add" class="btn-size" onclick="return AddButtonRow();" />
                                </td>
                            </tr>
                        </table>
                        
                        <table class="tableStyle" id="tblButton">
                            <tr></tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

