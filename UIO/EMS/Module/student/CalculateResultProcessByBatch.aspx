<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CalculateResultProcessByBatch" Codebehind="CalculateResultProcessByBatch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Calculate GPA and CGPA</asp:Content>
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
            <label>Calculate Student's GPA & CGPA</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="calculateGpaCgpa-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">Batch :</label>
                    <asp:DropDownList runat="server" ID="ddlBatch" class="margin-zero dropDownList"  />
                    <asp:Button runat="server" ID="btnProcess" Text="Process" OnClick="btnProcess_Click" class="button-margin SearchKey" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

