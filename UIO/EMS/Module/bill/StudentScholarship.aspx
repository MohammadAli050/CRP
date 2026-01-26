<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentScholarship.aspx.cs" Inherits="EMS.miu.bill.StudentScholarship" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/BatchUserControlAll.ascx" TagPrefix="uc2" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Scholarship
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <%--<link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />--%>

    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 26px;
        }
        .auto-style2 {
            width: 25px;
        }
        .cursor {
            cursor: pointer
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="PageTitle">
        <label>Student Scholarship Merit List :: SGPA And CGPA Wise </label>
    </div>
    <div>
        <div id="divProgress" class="floatRight" style="padding-top: 7px; display: none;">
            <div class="floatRight" >
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
            </div>
        </div>
    </div>
    <div class="cleaner"></div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area"> 
    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>     
            <div>
                <table>
                    <tr>
                        <td class="auto-style9"><b>Calender :</b></td>
                        <td class="auto-style4">
                            <asp:DropDownList runat="server" ID="ddlCalenderType" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />
                        </td>
                        <td class="auto-style1">&nbsp</td>                                                                                                                                                   
                        <td class="auto-style8"><b> Result Session :</b></td>
                        <td class="auto-style7">
                            <asp:DropDownList runat="server" ID="ddlAcademicCalender" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />
                        </td>
                        <td class="auto-style2">&nbsp</td>
                        <td class="auto-style10"><b>Program :</b></td>
                        <td class="auto-style14">
                            <asp:DropDownList runat="server" ID="ddlProgram" AutoPostBack ="true" OnSelectedIndexChanged="Program_Changed" />
                        </td>
                        <td class="auto-style2">&nbsp</td>
                        <td class="auto-style11"><b>Batch :<b></td>
                        <td class="auto-style13">
                            <uc2:BatchUserControl runat="server" ID="ucBatch" />
                        </td>        
                    </tr>
                    <tr>
                        <td class="auto-style9"><b>Scholarship Criteria :</b></td>
                        <td><b> 
                            <asp:CheckBox ID="chkSGPA" runat="server" Text="SGPA" />
                            <asp:CheckBox ID="chkCGPA" runat="server" Text="CGPA" />
                        </b></td>
                        <td>&nbsp</td>
                        <td colspan="3">
                            <asp:Button runat="server" ID="Button1" Text="LOAD MERIT LIST" Width="165" CssClass="cursor" OnClick="OnClick_LoadMeritList"/>
                        </td>
                    </tr>                                                 
                            
                </table>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
    </div>

    <div>&nbsp;</div>
    <rsweb:reportviewer id="StudentMeritListForScholarship" runat="server"  waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" BackColor="Wheat" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1" CssClass="center" width="57.3%" Height="100%" sizetoreportcontent="true">
    </rsweb:reportviewer>
    
</asp:Content>
