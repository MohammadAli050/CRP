<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_UserProfile" Codebehind="UserProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">User Profile</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            if($('#ctl00_MainContainer_btnCreate').val() == 'Update')
                $('#txtAvailable').hide();
        });

        function Available_Click() {
            $('#ctl00_MainContainer_lblMsg').text('');

            var code = $('#ctl00_MainContainer_txtCode').val();
            if (code != '') {
                $.ajax({
                    type: "POST",
                    url: "UserProfile.aspx/AvailabilityCheck",
                    data: "{Code: '" + code + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d;
                        if (result == 'found') {
                            $('#ctl00_MainContainer_lblMsg').text('Already Exist');
                        }
                        else if (result == 'null') {
                            $('#ctl00_MainContainer_lblMsg').text('Availabe');
                        }
                        else if (result == 'error') {
                            $('#ctl00_MainContainer_lblMsg').text('Availabe');
                        }
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

        function ValidationField() {
            $('#ctl00_MainContainer_lblMsg').text('');

            if ($('#ctl00_MainContainer_txtFullName').val() == '') {
                $('.warningFullName').css('color', 'red');
                return false;
            }
            else { $('.warningFullName').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtCode').val() != 'undefined') {
                if ($('#ctl00_MainContainer_txtCode').val() == '') {
                    $('.warningCode').css('color', 'red');
                    return false;
                }
                else { $('.warningCode').css('color', 'black'); }
            }

            if ($('#ctl00_MainContainer_txtDOB').val() == '') {
                $('.warningDOB').css('color', 'red');
                return false;
            }
            else { $('.warningDOB').css('color', 'black'); }

            if ($('#ctl00_MainContainer_rbMale').is(':checked') == false && $('#ctl00_MainContainer_rbFemale').is(':checked') == false) {
                $('.warningSex').css('color', 'red');
                return false;
            }
            else { $('.warningSex').css('color', 'black'); }

            if ($('#ctl00_MainContainer_rbSingle').is(':checked') == false && $('#ctl00_MainContainer_rbMarried').is(':checked') == false) {
                $('.warningStatus').css('color', 'red');
                return false;
            }
            else { $('.warningStatus').css('color', 'black'); }

            if ($('#ctl00_MainContainer_ddlBloodGroup option:selected').val() == '0') {
                $('.warningBloodGroup').css('color', 'red');
                return false;
            }
            else { $('.warningBloodGroup').css('color', 'black'); }

            if ($('#ctl00_MainContainer_ddlReligion option:selected').val() == '0') {
                $('.warningReligion').css('color', 'red');
                return false;
            }
            else { $('.warningReligion').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtNationality').val() == '') {
                $('.warningNationality').css('color', 'red');
                return false;
            }
            else { $('.warningNationality').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtPhone').val() == '') {
                $('.warningPhone').css('color', 'red');
                return false;
            }
            else { $('.warningPhone').css('color', 'black'); }

            if ($('#ctl00_MainContainer_txtEmail').val() == '') {
                $('.warningEmail').css('color', 'red');
                return false;
            }
            else { $('.warningEmail').css('color', 'black'); }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Create/Update Faculty/Employee/Staff</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="UserProfile-container">
            <div class="div-margin">
                <label class="display-inline field-Title">Full Name</label>
                <sup class="warningFullName">*</sup>
                <asp:TextBox runat="server" ID="txtFullName" class="margin-zero input-Size" placeholder="Full Name" />
            </div>
            <asp:Panel runat="server" ID="pnlCode">
                <div class="div-margin">
                    <label class="display-inline field-Title">Code</label>
                    <sup class="warningCode">*</sup>
                    <asp:TextBox runat="server" ID="txtCode" class="margin-zero" placeholder="Code" />
                    <input type="button" id="txtAvailable" class="AvailableKey" value="Available" onclick="Available_Click();" />
                </div>
            </asp:Panel>
            <div class="div-margin">
                <label class="display-inline field-Title">Date Of Birth</label>
                <sup class="warningDOB">*</sup>
                <asp:TextBox runat="server" ID="txtDOB" class="margin-zero input-Size datepicker" placeholder="Date Of Birth" DataFormatString="{0:dd/MM/yyyy}" />
                <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Gender</label>
                <sup class="warningSex">*</sup>
                <asp:RadioButton runat="server" ID="rbMale" class="radioButtonGroup" GroupName="sex" Text="Male" />
                <asp:RadioButton runat="server" ID="rbFemale" class="radioButtonFlag" GroupName="sex" Text="Female" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Maritul Status</label>
                <sup class="warningStatus">*</sup>
                <asp:RadioButton runat="server" ID="rbSingle" class="radioButtonGroup" GroupName="MaritulStatus" Text="Single" />
                <asp:RadioButton runat="server" ID="rbMarried" GroupName="MaritulStatus" Text="Married" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Blood Group</label>
                <sup class="warningBloodGroup">*</sup>
                <asp:DropDownList runat="server" ID="ddlBloodGroup" class="margin-zero dropDownList">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">A+</asp:ListItem>
                    <asp:ListItem Value="2">A-</asp:ListItem>
                    <asp:ListItem Value="3">AB+</asp:ListItem>
                    <asp:ListItem Value="4">AB-</asp:ListItem>
                    <asp:ListItem Value="5">B+</asp:ListItem>
                    <asp:ListItem Value="6">B-</asp:ListItem>
                    <asp:ListItem Value="7">O+</asp:ListItem>
                    <asp:ListItem Value="8">O-</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Religion</label>
                <sup class="warningReligion">*</sup>
                <asp:DropDownList runat="server" ID="ddlReligion" class="margin-zero dropDownList">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Islam</asp:ListItem>
                    <asp:ListItem Value="2">Hinduism</asp:ListItem>
                    <asp:ListItem Value="3">Buddhism</asp:ListItem>
                    <asp:ListItem Value="4">Christianity</asp:ListItem>
                    <asp:ListItem Value="5">Animists</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Nationality</label>
                <sup class="warningNationality">*</sup>
                <asp:TextBox runat="server" ID="txtNationality" class="margin-zero input-Size" placeholder="Nationality" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Phone</label>
                <sup class="warningPhone">*</sup>
                <asp:TextBox runat="server" ID="txtPhone" class="margin-zero input-Size" placeholder="Phone Number" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title">Email</label>
                <sup class="warningEmail">*</sup>
                <asp:TextBox runat="server" ID="txtEmail" class="margin-zero input-Size" placeholder="Email Address" />
            </div>
            <div class="div-margin">
                <label class="display-inline field-Title"></label>
                <asp:Button runat="server" ID="btnCreate" class="margin-zero btn-size" Text="Create" OnClick="btnCreate_Click" OnClientClick="return ValidationField();" />
            </div>
        </div>
    </div>
</asp:Content>

