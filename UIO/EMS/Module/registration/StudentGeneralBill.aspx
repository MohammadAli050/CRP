<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Student_StudentGeneralBill" CodeBehind="StudentGeneralBill.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student General Bill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .auto-style3 {
            width: 100px;
        }

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
    </style>
    <style>
        .modalBackground {
            background-color: #2a2d2a;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .font {
            font-size: 12px;
        }

        .cursor {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 100%">

        <div class="Message-Area">
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table style="padding: 1px; width: 900px;">
                    <tr>
                        <td class="tbl-width-lbl">
                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Student ID :"></asp:Label></td>
                        <td class="tbl-width">
                            <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                        </td>                       
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="150px">
                                <asp:ListItem Value="0" Text="All" />
                                <asp:ListItem Value="1" Text="Bill History" />
                                <asp:ListItem Value="2" Text="Collection History" />
                            </asp:DropDownList>
                        </td>
                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                        <td class="auto-style8">Session</td>
                        <td>
                            <asp:DropDownList ID="ddlBillPostingSession" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillPostingSession_SelectedIndexChanged" Width="158px"></asp:DropDownList>
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="tbl-width-lbl"><b>Name :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tbl-width-lbl"><b>Batch :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tbl-width-lbl"><b>Program :</b></td>
                        <td class="tbl-width">
                            <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                        </td>

                        <td></td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label2" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Visible="true">
                        <asp:Label ID="lblTotalBillMsg" runat="server" Text="Your total bill amount : "></asp:Label>
                        <asp:Label ID="lblTotalBill" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTotalPaidMsg" runat="server" Text="Your total paid amount : "></asp:Label>
                        <asp:Label ID="lblTotalPaid" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTotalDiscountMsg" runat="server" Text="Your total discount amount : "></asp:Label>
                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblTotalPayble" runat="server" Text="Your total payble amount : "></asp:Label>
                        <asp:Label ID="lblTotalPaybleAmount" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <br />

        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvBillView" AutoGenerateColumns="False"
                        AllowPaging="false" PageSize="20" CssClass="gridCss" CellPadding="4" DataKeyNames="BillHistoryId">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />

                        <Columns>
                            <asp:BoundField DataField="BillHistoryId" Visible="false" HeaderText="">
                                <HeaderStyle Width="75px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Code/Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Title/Fee/Discount Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurpose" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Credit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourse" Text='<%#Eval("Credits") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount/Dues">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("Fees") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Discount/Advance">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDiscount" Text='<%#Eval("DiscountAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPayment" Text='<%#Eval("Payment") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Current Due">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCurrentDue"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trimester" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPSemester" Text='<%#Eval("SemesterName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="BillingDate" HeaderText="Billing Date" DataFormatString="{0:dd-MMM-yy}">
                                <HeaderStyle CssClass="center" Width="100px" />
                                <ItemStyle CssClass="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="Posting Date" DataFormatString="{0:dd-MMM-yy}">
                                <HeaderStyle CssClass="center" Width="100px" />
                                <ItemStyle CssClass="center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRemark" Text='<%#Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Visible="false"
                                        ToolTip="Bill History Edit" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible="false" OnClientClick="return confirm('Do you really want to delete this bill?');"
                                        ToolTip="Bill History Delete" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="65px"></HeaderStyle>
                                <ItemStyle CssClass="center" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
                        <%--<RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />>--%>
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                    <div style="padding: 5px;">
                        <fieldset style="padding: 5px; border: 2px solid green;">
                            <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Edit Bill/Collection History</legend>
                            <div style="padding: 5px;">
                                <div class="Message-Area">
                                    <asp:Label ID="labl123" runat="server" Text="Message : "></asp:Label>
                                    <asp:Label ID="lblNewMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                                <table style="padding: 1px;" border="0">
                                    <tr>
                                        <td><b>
                                            <asp:Label ID="lblDate" Visible="false" runat="server" Text="Date No :"></asp:Label></b></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="DateTextBox" Width="164px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateTextBox" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Student Name :</b></td>
                                        <td>
                                            <asp:Label ID="lblNewStudentName" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Student Roll :</b></td>
                                        <td>
                                            <asp:Label ID="lblStudentRoll" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Fee/Due Amount :</b></td>
                                        <td>
                                            <asp:TextBox ID="txtFeeAmount" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>Discount/Advance Amount :</b></td>
                                        <td>
                                            <asp:TextBox ID="txtDiscountAmount" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>Payment Amount :</b></td>
                                        <td>
                                            <asp:TextBox ID="txtPaymentAmount" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>
                                            <asp:Label ID="lblTypeDefinition1" runat="server" Text="Type Definition :"></asp:Label></b></td>
                                        <td><b>
                                            <asp:Label ID="lblTypeDefinition" runat="server" Text=""></asp:Label></b></td>
                                    </tr>
                                    <tr>
                                        <td><b>
                                            <asp:Label ID="lblCourseName1" runat="server" Text="Course :"></asp:Label></b></td>
                                        <td><b>
                                            <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label></b></td>
                                    </tr>
                                    <tr>
                                        <td><b>Trimester :</b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSession" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>
                                                <asp:Label ID="lblPaymentType" Visible="false" runat="server" Text="Payment Type :"></asp:Label></b>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdlPaymentType" Visible="false" Enabled="false" runat="server">
                                                <asp:ListItem Value="1" Text="Cash" />
                                                <asp:ListItem Value="2" Text="Bank" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>
                                            <asp:Label ID="lblMoneyReceiptNo" Visible="false" runat="server" Text="Money Receipt No :"></asp:Label></b></td>
                                        <td>
                                            <asp:TextBox ID="txtMoneyReceiptNo" Visible="false" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>
                                            <asp:Label ID="lblComment" Visible="false" runat="server" Text="Remark :"></asp:Label></b></td>
                                        <td>
                                            <asp:TextBox ID="txtRemark" Visible="false" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Button ID="UpdateButton" runat="server" Text="Update Bill" OnClick="btnUpdate_Click" />
                                            <asp:Button ID="PaymentUpdateButton" runat="server" Text="Update Payment" OnClick="btnUpdatePayment_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="CancelButton" runat="server" Text="Cancel Edit/Close" CssClass="cursor" OnClick="btnCancel_Click" /></td>
                                    </tr>
                                </table>

                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:HiddenField ID="hdnBillHistoryID" runat="server" Value='<%#Eval("BillHistoryID") %>' />
        <asp:HiddenField ID="hdnCollectionHistoryId" runat="server" Value='<%#Eval("CollectionHistoryId") %>' />
    </div>
</asp:Content>

