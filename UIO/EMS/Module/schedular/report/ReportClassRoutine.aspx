<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Report_RptClassRoutine" Codebehind="ReportClassRoutine.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .displayDiv
        {
            display: inline-block;
        }

        .displayDiv2
        {
            display: inline-block;
            width: 100%;
        }

            .displayDiv2 table
            {
                width: 100%;
            }

                .displayDiv2 table tr
                {
                    display: inline-block;
                }

        .titleLabelL
        {
            text-align: right;
            padding-right: 10px;
            width: 80px;
            display: inline-block;
        }

        .titleLabelR
        {
            text-align: right;
            padding-right: 10px;
            width: 95px;
            display: inline-block;
        }

        .calenderText
        {
            width: 175px;
        }

        body .main-container .BodyContent-wrapper
        {
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="background-color: #C0C0C0;">
        <div style="width: 880px;">
            <fieldset>
                <legend>Class Routine</legend>
                <div>
                    <div class="displayDiv">
                        <asp:Label CssClass="titleLabelL" ID="lblAcaCal" runat="server" Style="width: 90px; font: bold 14px arial;" Text="Trimester"></asp:Label>
                    </div>
                    <div class="displayDiv">
                        <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlAcaCalBatch" />
                    </div>
                </div>
                <div>
                    <div class="displayDiv">
                        <asp:Label CssClass="titleLabelL" ID="lblProgram1" runat="server" Style="width: 150px; font: bold 14px arial;" Text="Choose Program :"></asp:Label>
                    </div>
                    <div class="displayDiv2" style="margin-left: 17px;">
                        <asp:CheckBoxList runat="server" ID="cblProgram">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div>
                    <div class="displayDiv">
                        <asp:Label CssClass="titleLabelL" ID="Label1" runat="server" Style="width: 195px; font: bold 14px arial;" Text="Choose Required Field :"></asp:Label>
                    </div>
                    <div class="displayDiv2" style="margin-left:23px; margin-top: 5px;">
                        <asp:CheckBox ID="chkCoursecode" runat="server" Text="Course Code" />
                        <asp:CheckBox ID="chkTitle" runat="server" Text="Title" />
                        <asp:CheckBox ID="chkSection" runat="server" Text="Section" />
                        <asp:CheckBox ID="chkCredit" runat="server" Text="Credit" />
                        <asp:CheckBox ID="chkDay" runat="server" Text="Day" />
                        <asp:CheckBox ID="chkTime" runat="server" Text="Time" />
                        <asp:CheckBox ID="chkRoomNo" runat="server" Text="Room No" />
                        <asp:CheckBox ID="chkFaculty" runat="server" Text="Faculty" />
                        <asp:CheckBox ID="chkProgram" runat="server" Text="Program" />
                        <asp:CheckBox ID="chkSharedPrograms" runat="server" Text="Shared Programs" />
                    </div>
                </div>
                <%--<div>
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblProgram" runat="server" Text="Program"></asp:Label></div>
                    <div class="displayDiv"><asp:DropDownList runat="server" class="dropdownList-size" ID="ddlProgram" DataValueField="ProgramId" DataTextField="ShortName" /></div>
                </div>--%>
                <div style="margin-top: 4px;">
                    <div class="displayDiv">
                        <asp:Label CssClass="titleLabelL" ID="lblGenerateAndSave" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="displayDiv">
                        <asp:Button runat="server" ID="btnGenerateClassRoutine" Text="Generate Class Routine" Style="cursor: pointer; margin-left: 0px;" OnClick="btnClassRoutine" />
                    </div>
                </div>
                <div style="height: 15px; padding: 0 2px;">
                    <asp:Label runat="server" ID="lblNotification"></asp:Label>
                </div>

                
            </fieldset>
        </div>
    </div>
        <rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" asynrendering="true" width="100%" sizetoreportcontent="true">
                    <LocalReport ReportPath="miu/schedular/report/ReportClassRoutine.rdlc">
                    </LocalReport>
        </rsweb:reportviewer>


</asp:Content>

