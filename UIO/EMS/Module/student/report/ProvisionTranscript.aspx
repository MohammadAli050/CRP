<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_ProvisionTranscript" Codebehind="ProvisionTranscript.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Transcript</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Transcript</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="transcript-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">Student ID :</label>
                    <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" class="margin-zero input-Size" placeholder="Student ID"/>
                    <asp:Button runat="server" ID="Load" Text="Process" OnClick="btnLoad_Click" class="button-margin" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

