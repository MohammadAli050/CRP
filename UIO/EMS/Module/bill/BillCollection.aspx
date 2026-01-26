<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="BillCollection.aspx.cs" Inherits="EMS.miu.bill.BillCollection" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Bill Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }


        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
        .auto-style1 {
            width: 220px;
        }
        .auto-style2 {
            width: 262px;
        }
        .auto-style3 {
            width: 23px;
        }
        .auto-style4 {
            width: 110px;
        }
        </style>
    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle">
        <label>Student Bill Collection</label>
    </div>
    <asp:Panel runat="server" ID="pnMessage">
        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divProgress" style="display: none; float: right; z-index: 1000;">
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel2"
        runat="server">
        <Animations>
        <OnUpdating>
            <Parallel duration="0">
                <ScriptAction Script="InProgress();" />
                <EnableAction AnimationTarget="btnLoad" 
                                Enabled="false" />                   
            </Parallel>
        </OnUpdating>
        <OnUpdated>
            <Parallel duration="0">
                <ScriptAction Script="onComplete();" />
                <EnableAction   AnimationTarget="btnLoad" 
                                Enabled="true" />
            </Parallel>
        </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
    <div class="Message-Area">
       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="padding: 5px; margin: 5px; width: 900px;">
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Font-Underline="true" Font-Bold="true" Text="DEPOSIT ENTRY FORM"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td >
                                <asp:Label ID="lblAccountNo" runat="server"  Text="A/C-SND NO :"></asp:Label>
                                <asp:Label ID="lblAccountNumber" runat="server" Font-Bold="true" Text="20501770900003518"></asp:Label>
                            </td>
                       
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBankName" runat="server"  Text="ISLAMI BANK BANGLADESH LIMITED"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBankBranch" runat="server"  Text="GULSHAN BRANCH"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                    <div style="clear: both;"></div>
                    <div style="clear: both;"></div>

                    <table style="padding: 1px; width: 769px;">
                        <tr>
                            <td class="auto-style1" >
                                <asp:Label ID="lblSlipNo" runat="server"  Text="SI. No.  "></asp:Label>
                                <asp:TextBox  ID="txtSlipNo" runat="server" PlaceHolder ="Serial No"></asp:TextBox>
                            </td>
                            <td >
                                <asp:Label ID="lblDate" runat="server"  Text="Collection Date  "></asp:Label>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="164px"  class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateTextBox" Format="dd/MM/yyyy" />
                            </td>
                            <td>
                                <asp:Label ID="lblPostingDate" runat="server"  Text="Posting Date  "></asp:Label>
                                <asp:TextBox ID="PostingDateTextBox" runat="server" class="margin-zero input-Size datepicker" DataFormatString="{0:dd/MM/yyyy}" />
                            </td>
                        </tr>
                   
                    </table>
                    <div style="clear: both;"></div>
                    <div style="clear: both;"></div>

                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="lblStudentRoll" runat="server" Width="130px" Text="Student's ID : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentRoll" PlaceHolder ="Student's ID" runat="server" Width="190px"></asp:TextBox>
                                <asp:Button ID="LoadStudentButton" runat="server" Text="Load" OnClick="LoadStudentButton_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="lblStudentName" runat="server" Width="130px" Text="Student Name : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentName" runat="server" PlaceHolder ="Student Name" Enabled="false" Width="190px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="lblSemester" runat="server" Width="130px" Text="Semester : "></asp:Label>
                            </td>
                            <td>
                                <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="Label2" runat="server" Width="130px" Text="Phone No : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhoneNo" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                    </table>

                    <div style="clear: both;"></div>
                    <div style="clear: both;"></div>
                    <br/>
                    <table style="padding: 1px; border: 1px solid black; width: 524px;">
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSiNo" runat="server" Font-Bold="true" Width="50px" Text="SI No"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                               <b>Remark Particulars</b> 
                            </td>
                            <td style="border: 1px solid black;">
                                <asp:Label ID="lblTaka" runat="server" Font-Bold="true" Width="100px" Text="Taka"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi1" runat="server" Width="50px" Text="1"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox1" Text="Admission Fee" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                       
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtAdmissionFee" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi2" runat="server" Width="50px" Text="2"></asp:Label>
                            </td>

                              <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox2" Text="Tution Fee" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                          
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtTutionFee" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi3" runat="server" Width="50px" Text="3"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox3" Text="Semester Charge" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                            
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtSemesterCharge" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi4" runat="server" Width="50px" Text="4"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox4" Text="Late Fee" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                           
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtLateFee" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi5" runat="server" Width="50px" Text="5"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox5" Text="Re-Admission Fee" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                           
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtReAdmissionFee" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi6" runat="server" Width="50px" Text="6"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox6" Text="Previous Due" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtPreviousDue" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="lblSi7" runat="server" Width="50px" Text="7"></asp:Label>
                            </td>
                             <td style="border: 1px solid black;" class="auto-style3">
                                <asp:CheckBox ID="chkbox7" Text="Others" runat="server" AutoPostBack="true" OnCheckedChanged="Remarks_CheckedChanged"/>
                            </td>
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtOthers" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;" class="auto-style3">
                                <asp:Label ID="Label16" runat="server" Width="50px" Text=""></asp:Label>
                            </td>
                            <td style="border: 1px solid black;" class="auto-style2">
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Width="300px" Text="Total Amount"></asp:Label>
                            </td>
                            <td style="border: 1px solid black;">
                                <asp:TextBox ID="txtTotal" PlaceHolder ="Total Amount"  runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="clear: both;"></div>
                    <div style="clear: both;"></div>
                    <br/>
                    <table style="padding: 1px; width: 900px;">
                        <tr>
                            <td class="auto-style4">
                                Payment Type
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdlPaymentType" runat="server">
                                    <asp:ListItem Value="1" Text="Cash"  />
                                    <asp:ListItem Value="2" Text="Bank" Selected="True" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="70px" Width="282px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="auto-style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
                    <asp:Button ID="PrintButton" runat="server" Text="Print" OnClick="PrintButton_Click"/>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnCollectionHistoryId" runat="server" />

    <div>&nbsp;</div>
    <rsweb:ReportViewer ID="StudentBillCollection" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" Height="100%" Width="57.2%" SizeToReportContent="true">      
    </rsweb:ReportViewer>
</asp:Content>