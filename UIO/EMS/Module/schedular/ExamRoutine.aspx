<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.Master" AutoEventWireup="true" Inherits="Admin_ExamRoutine" Codebehind="ExamRoutine.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Routine : Excel File</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        .displayDiv {
            display: inline-block;
        }
        .titleLabelL {
            text-align: right;
            padding-right: 10px;
            width: 80px;
            display: inline-block;
        }
        .titleLabelR {
            text-align: right;
            padding-right: 10px;
            width: 95px;
            display: inline-block;
        }
        .calenderText {
            width: 175px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
     <div>
        <div style="float: left; width: 350px;">
            <fieldset>
                <legend>Export Class Routine</legend>
                <div>
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblAcaCal" runat="server" Text="Trimester"></asp:Label></div>
                    <div class="displayDiv"><asp:DropDownList runat="server" class="dropdownList-size" ID="ddlAcaCalBatch" /></div>
                </div>
                <div>
                    <div class="displayDiv" style="vertical-align: top;"><asp:Label CssClass="titleLabelL" ID="lblProgram1" runat="server" Text="Program"></asp:Label></div>
                    <div class="displayDiv">
                        <asp:CheckBoxList runat="server" ID="cblProgram">

                        </asp:CheckBoxList>
                    </div>
                </div>
                <div style="margin-top: 4px;">
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblGenerateAndSave" runat="server" Text=""></asp:Label></div>
                    <div class="displayDiv"><asp:Button runat="server" ID="btnGenerateExcel" Text="Generate Excel File" Style="cursor: pointer; margin-left: 0px;" OnClick="btn_GenerateExcel" /></div>
                </div>
                <div style="height: 15px; padding: 0 2px;">
                    <asp:Label runat="server" ID="lblNotification"></asp:Label>
                </div>
            </fieldset>
        </div>

        <div style="float: left; width: 340px; margin-left: 25px;">
            <fieldset style="padding: 17px;">
                <legend>Import Class Routine</legend>
                <div>
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblSelectFile" runat="server" Text="Select File"></asp:Label></div>
                    <div class="displayDiv"><asp:FileUpload runat="server" class="dropdownList-size" ID="fuUploadFile" /></div>
                </div>
                <div style="margin-top: 30px;">
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblUploadFile" runat="server" Text=""></asp:Label></div>
                    <div class="displayDiv"><asp:Button runat="server" ID="btnUpload" Text="Upload" Style="cursor: pointer; margin-left: 0px;" OnClick="btn_UploadExcel" /></div>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblAlert" Text="" style="color: red;"></asp:Label>
                </div>
            </fieldset>
        </div>
        <div style="clear: both;"></div>
    </div>        
</asp:Content>

