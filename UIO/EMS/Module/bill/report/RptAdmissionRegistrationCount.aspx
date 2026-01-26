<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptAdmissionRegistrationCount.aspx.cs" Inherits="EMS.miu.bill.report.RptAdmissionRegistrationCount" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Trimester/Semester Wise Admission and Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            width: auto;
            height: auto;
            text-align: center;
            }

        .box {border: 1px solid green ;
            }

    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Trimester/Semester Wise Admission and Registration</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                <asp:Label ID="Label2" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>                  
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div style="text-align: left">
        <div class="Message-Area">
        Choose Program: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="true"></asp:DropDownList> &nbsp;&nbsp;&nbsp;
        Year From: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddList2" runat="server">
        </asp:DropDownList> &nbsp;&nbsp;&nbsp;
        Year To: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddList3" runat="server">
        </asp:DropDownList>
        <asp:Button ID="RunReport" runat="server" style="margin-left: 10px" OnClick="RunReport_Click" Text="Load" Width="100px" />
         &nbsp;&nbsp;&nbsp<input id="btnprint" type="button" onclick="PrintDiv()" value="Print" />
            </div>
        <div id="printarea">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img alt="UIU Logo" class="auto-style1" src='<%= string.Format("{0}/Images/coverImage.png", CommonUtility.AppPath.ApplicationPath) %>'/>
                <br />
                <b><asp:Label ID="Label1" style="text-align:center;" runat="server"></asp:Label>
                </b>&nbsp;<br />
                <asp:GridView ID="GridView1" runat="server"  style=" position:relative;margin: 10px; top: 0px; left: -1px; height: 172px; width: 207px;" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"  GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
        </div>

    </div>
</asp:Content>
