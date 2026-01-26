<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Scholarship_StudentList" Codebehind="StudentList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Scholarship List</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function checkView() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlAcaCal').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Semester');
                return false;
            }

            if ($('#MainContainer_ddlProgram').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Program');
                return false;
            }
        }
        function checkDownlaod() {
            $('#MainContainer_lblMsg').text('');

            if ($('#MainContainer_ddlAcaCal').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Semester');
                return false;
            }

            if ($('#MainContainer_ddlProgram').val() == '0') {
                $('#MainContainer_lblMsg').text('Please select Program');
                return false;
            }

            //if ($('#MainContainer_ddlFromBatchAcaCal').val() == '0') {
            //    $('#MainContainer_lblMsg').text('Please select From Batch');
            //    return false;
            //}

            //if ($('#MainContainer_ddlToBatchAcaCal').val() == '0') {
            //    $('#MainContainer_lblMsg').text('Please select To Batch');
            //    return false;
            //}

            var acaCal = parseInt($('#MainContainer_ddlAcaCal').val());
            var fromBatch = parseInt($('#MainContainer_ddlFromBatchAcaCal').val());
            var toBatch = parseInt($('#MainContainer_ddlToBatchAcaCal').val());

            //if (acaCal > toBatch) {
            //    $('#MainContainer_lblMsg').text('Please provide Correct Semester which related (To Batch)');
            //    return false;
            //}

            if (toBatch < fromBatch) {
                $('#MainContainer_lblMsg').text('Please provide Correct (From - To) Batch');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Downlaod Student List</label>
        </div>

        <asp:Panel runat="server" ID="pnMessage">
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="InitialPanel">
            <div class="StudentList-container">
                <div class="div-margin">
                    <div class="loadArea">
                        <label class="display-inline field-Title">Semester</label>
                        <asp:DropDownList runat="server" ID="ddlAcaCal" class="margin-zero dropDownList" />

                        <label class="display-inline field-Title2">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgram" DataTextField="NameWithCode" DataValueField="Code" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Scheme</label>
                        <asp:DropDownList runat="server" ID="ddlScheme" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_Select" />
                    </div>
                    <div class="loadedArea disabledrowload1">
                        <label class="display-inline field-Title">From Batch</label>
                        <asp:DropDownList runat="server" ID="ddlFromBatchAcaCal" class="margin-zero dropDownList"  />

                        <label class="display-inline field-Title2">To Batch</label>
                        <asp:DropDownList runat="server" ID="ddlToBatchAcaCal" class="margin-zero dropDownList" />
                    </div>
                    <div class="loadedArea disabledrowload2">
                        <label class="display-inline field-Title">Percentage</label>
                        <label class="display-inline field-Title3">100%</label>
                        <asp:DropDownList runat="server" ID="ddl100" class="margin-zero dropDownList1" />
                        <label class="display-inline field-Title4">50%</label>
                        <asp:DropDownList runat="server" ID="ddl50" class="margin-zero dropDownList1" />
                        <label class="display-inline field-Title4">25%</label>
                        <asp:DropDownList runat="server" ID="ddl25" class="margin-zero dropDownList1" />
                    </div>
                    <asp:Panel runat="server" ID="Panel01">
                        <div class="loadedArea disabledrowload2">
                            <label class="display-inline field-Title">Total</label>
                            <asp:Label runat="server" ID="lblTotalCredit" class="display-inline field-Title6" Text=""></asp:Label>
                        </div>
                        <div class="loadedArea disabledrowload2">
                            <label class="display-inline field-Title">Credit (s)</label>
                            <label class="display-inline field-Title3">100%</label>
                            <asp:Label runat="server" ID="lblNoOfStudent100" class="display-inline field-Title5" Text=""></asp:Label>
                            <label class="display-inline field-Title4">50%</label>
                            <asp:Label runat="server" ID="lblNoOfStudent50" class="display-inline field-Title5" Text=""></asp:Label>
                            <label class="display-inline field-Title4">25%</label>
                            <asp:Label runat="server" ID="lblNoOfStudent25" class="display-inline field-Title5" Text=""></asp:Label>
                        </div>
                        <div class="loadedArea disabledrowload2">
                            <label class="display-inline field-Title">Apply (s)</label>
                            <label class="display-inline field-Title3">100%</label>
                            <asp:Label runat="server" ID="lblApply100" class="display-inline field-Title5" Text=""></asp:Label>
                            <label class="display-inline field-Title4">50%</label>
                            <asp:Label runat="server" ID="lblApply50" class="display-inline field-Title5" Text=""></asp:Label>
                            <label class="display-inline field-Title4">25%</label>
                            <asp:Label runat="server" ID="lblApply25" class="display-inline field-Title5" Text=""></asp:Label>
                        </div>
                        <div class="loadedArea"></div>
                    </asp:Panel>
                    <div class="loadedArea">
                        <label class="display-inline field-Title"></label>
                        <asp:Button runat="server" ID="btnDownload" class="margin-zero btn-size" Text="Generate Scholarship" OnClick="btnGenerate_Click" OnClientClick="return checkDownlaod();" />
                        <asp:Button runat="server" ID="btnView" class="margin-zero btn-size" Text="View Scholarship List" OnClick="btnView_Click" OnClientClick="return checkView();" />
                        <asp:Button runat="server" ID="btnScholarshipBillPosting"  class="margin-zero btn-size" Width="200px"  Text="Scholarship Discount Posting" OnClick="btnScholarshipBillPosting_Click" OnClientClick="return checkView();" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="StudentList-container">
                    <asp:Panel ID="PnlScholarship" runat="server" Wrap="False"><%--Height="397px" ScrollBars="Vertical"--%>
                        <asp:gridview ID="gvScholarship" runat="server" 
                            onrowediting="gvDiscountContinuationSetup_RowEditing"
                            onrowcancelingedit="gvDiscountContinuationSetup_RowCancelingEdit"
                            onrowupdating="gvDiscountContinuationSetup_RowUpdating"
                            AutoGenerateColumns="False" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                </asp:TemplateField>                    
                                <asp:TemplateField HeaderText="Student ID" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Font-Bold="False" Text='<%#Eval("Roll") %>' />
                                        <asp:HiddenField runat="server" ID="hfId" Value='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblName" Font-Bold="False" Text='<%#Eval("Name") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GPA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblGPA" Font-Bold="False" Text='<%#Eval("GPA") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCredit" Font-Bold="False" Text='<%#Eval("Credit") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PassCredit" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><asp:Label runat="server" ID="lblPassCredit" Font-Bold="False" Text='<%#Eval("PassCredit") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RegisterCredit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblRegisterCredit" Font-Bold="False" Text='<%#Eval("RegisterCredit") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scholarship(%)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblScholarship" Font-Bold="False" Text='<%#Eval("CalculateScholarship") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ManualScholarship" HeaderText="Change(%)">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField InsertText="" SelectText="Action" ShowCancelButton="true" ShowDeleteButton="false" ShowEditButton="True" NewText="" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="150px" />
                                </asp:CommandField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>