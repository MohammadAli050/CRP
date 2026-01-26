<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    Inherits="ClassRoutineManage" Codebehind="ClassRoutineManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Routine: Class</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        .displayDiv {
            display: inline-block;
        }
        .titleLabelL {
            text-align: right;
            /*padding-right: 10px;*/
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContainer_ImgWaiting').hide();
        });

        function btnGenerateExcel() {
            $('#MainContainer_ImgWaiting').show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
     <div>
        <asp:UpdatePanel runat="server" ID="UpdatePanelOne">
            <ContentTemplate>
                <div style="margin: 10px 0;">
                    <label>Message: </label>
                    <asp:Label ID="lblMsg" runat="server" Text="" style="color: #f00;"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="float: left; width: 350px;">
            <fieldset>
                <legend>Export Class Routine</legend>
                <div style="float: left; width: 240px;">
                <div>
                    <div class="displayDiv"><asp:Label CssClass="titleLabelL" ID="lblAcaCal" runat="server" Text="Semester" style="font-weight: bold;"></asp:Label></div>
                    <div class="displayDiv" style="margin-left: 7px;"><asp:DropDownList runat="server" ID="ddlAcaCalBatch" style="width: 130px;" /></div>
                </div>
                <div>
                    <div class="displayDiv" style="vertical-align: top;"><asp:Label CssClass="titleLabelL" ID="lblProgram1" runat="server" Text="Program" style="font-weight: bold;"></asp:Label></div>
                    <div class="displayDiv">
                        <asp:CheckBoxList runat="server" ID="cblProgram" />
                    </div>
                </div>
                <div style="margin-top: 4px; padding-left: 89px;">
                    <div>
                        <asp:Button runat="server" ID="btnGenerateExcel" Text="Generate Excel" Style="margin-bottom: 10px; width: 130px; height: 30px; cursor: pointer; margin-left: 0px;" OnClick="btnGenerateExcel_GenerateExcel" /> <%--OnClientClick="btnGenerateExcel();" --%>
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnDownloadExcel" Text="Download Excel" Style="margin-bottom: 10px; width: 130px; height: 30px; cursor: pointer; margin-left: 0px;" OnClick="btnDownloadExcel_GenerateExcel" />
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnPrintFormatExcel" Text="Print Format Excel" Style="width: 130px; height: 30px; cursor: pointer; margin-left: 0px;" OnClick="btnPrintFormatExcel_GenerateExcel" />
                    </div>
                </div>
                </div>
                <div style="float: right; width: 70px;">
                    <asp:Image ID="ImgWaiting" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Width="70px" />
                </div>
                <div style="clear: both;"></div>
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
                    <div class="displayDiv">
                        <asp:Button runat="server" ID="btnUpload" Text="Upload" Style="cursor: pointer; margin-left: 0px;" OnClick="btn_UploadExcel" />
                        <asp:Label runat="server" ID="lblAlert" Text="" style="color: red;"></asp:Label>
                    </div>
                </div>
            </fieldset>
        </div>
        <div style="clear: both;"></div>
    </div>
</asp:Content>