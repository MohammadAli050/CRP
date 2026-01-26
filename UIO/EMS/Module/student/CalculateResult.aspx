<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CalculateResult" Codebehind="CalculateResult.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Result Processing</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function checkView() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlSemester').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Semester');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Calculate Student's GPA & CGPA</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="CalculateResult-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">Semester</label>
                    <asp:DropDownList runat="server" ID="ddlSemester" class="margin-zero dropDownList"  />

                    <label class="display-inline field-Title2">Batch</label>
                    <asp:DropDownList runat="server" ID="ddlBatch" class="margin-zero dropDownList"  />

                    <label class="display-inline field-Title2">Program</label>
                    <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" DataTextField="NameWithCode" DataValueField="ProgramID" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Student(ID)</label>
                    <asp:TextBox runat="server" ID="txtStudentId" MaxLength="9" class="margin-zero input-Size" placeholder="Student ID" />
                </div>
                <div class="loadedArea">
                    <asp:Button runat="server" ID="btnExecute" Text="Execute" class="button-margin btn-size" OnClick="btnExecute_Click" OnClientClick="return checkView();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

