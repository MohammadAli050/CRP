<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" Inherits="Report_RptPreAdvising" Codebehind="RptPreAdvising.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Pre Registration Report</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <p>
        Pre Advising Status Report
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Program Name"></asp:Label>
        :
            <asp:DropDownList ID="programListCombo" runat="server" DataTextField="ShortName" DataValueField="ProgramID" Height="20px" Width="109px">
            </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Batch No."></asp:Label>
        :<asp:DropDownList ID="batchListCombo" runat="server" DataValueField="BatchCode" Height="20px" Width="119px">
        </asp:DropDownList>
       
       <asp:Button ID="loadButton0" runat="server" OnClick="loadButton_Click" Text="Load" Width="55px" />

    </p>
    <p>

        &nbsp;<rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" sizetoreportcontent="true">
                    <LocalReport ReportPath="miu/registration/report/RptPreAdvising.rdlc">
                    </LocalReport>
    </rsweb:reportviewer>

    </p>
    <p>
    </p>


</asp:Content>
