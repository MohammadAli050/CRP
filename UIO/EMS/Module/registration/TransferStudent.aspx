<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentRegistration_TransferStudent" Codebehind="TransferStudent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Transfer Student</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="TransferStudent">
        <div class="margin-bottom">
            <div class="float-left border1px">
                <span style="font-weight: bold;">Transfer Student</span>
            </div>
            <div class="float-left border1px" style="margin-left: 10px; width: 800px; height: 15px;">
                <asp:Label runat="server" ID="lblMessage">.....</asp:Label>
            </div>
            <div class="cleaner"></div>
        </div>
        <div class="margin-bottom">
            <div class="float-left margin-right">
                <label class="labelDisplay" for="ddlAcaCalBatch" style="width: 160px;">Academic Calender / Batch</label>
                <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlAcaCalBatch"/>
            </div>
            <div class="float-left margin-right">
                <asp:Button ID="btnView" runat="server" CssClass="button" Height="25px" OnClick="TranserStudent" Text="Transfer" Width="124px" />
            </div>
            <div class="cleaner"></div>
        </div>
    </div>
</asp:Content>

