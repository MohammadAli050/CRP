<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_SetupMakeDuplicate" Codebehind="SetupMakeDuplicate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Make Duplicate</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup Duplicate</label>
        </div>

        <asp:Panel runat="server" ID="pnMessage">
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="InitialPanel">
            <div class="TotalAmountOfCredit-container">
                <div class="div-margin">
                    <div class="loadArea">
                        <label class="display-inline field-Title3">From Semester</label>
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Batch</label>
                        <asp:DropDownList runat="server" ID="ddlBatchAcaCalFrom" class="margin-zero dropDownList1" />

                        <label class="display-inline field-Title2">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgramFrom" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title3">To Semester</label>
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Batch</label>
                        <asp:DropDownList runat="server" ID="ddlBatchAcaCalTo" class="margin-zero dropDownList1" />

                        <label class="display-inline field-Title2">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgramTo" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title3">Setup Type</label>
                    </div>
                    <div class="loadedArea">
                        <div>
                            <asp:CheckBox runat="server" ID="chkDisCountSetup" Text="Discount Continuation Setup" OnCheckedChanged="chkDisCountSetup_Change" /><asp:Label runat="server" ID="lblDiscountSetup" Text="" class="display-inline field-Title4"></asp:Label>
                        </div>
                        <div>
                            <asp:CheckBox runat="server" ID="chkDemo" Text="Demo Setup" /><asp:Label runat="server" ID="lblDemo" Text="" class="display-inline field-Title4"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

