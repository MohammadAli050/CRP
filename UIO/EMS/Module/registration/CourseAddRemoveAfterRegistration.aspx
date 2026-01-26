<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.Master" CodeBehind="CourseAddRemoveAfterRegistration.aspx.cs" Inherits="EMS.miu.registration.CourseAddRemoveAfterRegistration" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student General Bill
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
    </style>
     <style type="text/css">
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
    <div style="height: auto; width: 1100px">

        <%-- <div class="PageTitle">
            <label>Student General Bill</label>
        </div>--%>

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
                             <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" class="margin-zero dropDownList" />
                        </td>
                        <td>
                             <uc1:SessionUserControl runat="server" ID="ucSession" class="margin-zero dropDownList"/>
                        </td>
                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                        <td></td>
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="#ffffff" Width="765px" Style="display: none; border-radius: 3px;">
                    <div style="padding: 5px; ">
                        <fieldset style="padding: 5px; border: 2px solid green;">
                            <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Academic Calender Schedule</legend>
                            <div style="padding: 5px;">
                                                           
                            </div>                                                                                                           
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td class="font">Admission Start</td>
                                        <td class="font">Admission End</td>
                                        <td style="width: 50px;"></td>                                                
                                        <td class="font">Registration Start</td>
                                        <td class="font">Registration End</td>
                                    </tr>
                                    <tr>
                                        <td class="td">

                                            <asp:TextBox runat="server" ID="clrAdmissionStart" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="reqtxtDate" runat="server" TargetControlID="clrAdmissionStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrAdmissionEnd" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="clrAdmissionEnd" Format="dd/MM/yyyy" />
                                        </td>
                                        <td style="width: 50px;"></td>
                                        <td class="td">
                                                <asp:TextBox runat="server" ID="clrRegistrationStart" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="clrRegistrationStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrRegistrationEnd" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="clrRegistrationEnd" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td class="font">Reg Payment Without Fine</td>
                                        <td class="font">Reg Payment With Fine</td>
                                        <td style="width: 50px;"></td>                                                
                                        <td class="font">Orientation Date</td>
                                        <td class="font">Result Publicaion Date</td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrRegPaymentWithoutFine" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="clrRegPaymentWithoutFine" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrRegPaymentWithFine" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="clrRegPaymentWithFine" Format="dd/MM/yyyy" />
                                        </td>
                                        <td style="width: 50px;"></td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrOrientationDate" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="clrOrientationDate" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrResultPublicaionDate" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="clrResultPublicaionDate" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td class="font">Class Start</td>
                                        <td class="font">Class End</td>
                                        <td style="width: 50px;"></td>                                                
                                        <td class="font">Advising Start</td>
                                        <td class="font">Advising End</td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrClassStart" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="clrClassStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrClassEnd" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="clrClassEnd" Format="dd/MM/yyyy" />
                                        </td>
                                        <td style="width: 50px;"></td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrAdvisingStart" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="clrAdvisingStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrAdvisingEnd" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="clrAdvisingEnd" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td class="font">MidExam Start</td>
                                        <td class="font">MidExam End</td>
                                        <td style="width: 50px;"></td>                                                
                                        <td class="font">FinalExam Start</td>
                                        <td class="font">FinalExamend</td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrMidExamStart" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="clrMidExamStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrMidExamEnd" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="clrMidExamEnd" Format="dd/MM/yyyy" />
                                        </td>
                                        <td style="width: 50px;"></td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrFinalExamStart" class="margin-zero input-Size"/>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="clrFinalExamStart" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrFinalExamend" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="clrFinalExamend" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td class="font">Mark Sheeet Submission</td>
                                        <td class="font">Answer Script Submission</td>
                                        <td style="width: 50px;"></td>                                                
                                        <td class="font">Session Vacation Start</td>
                                        <td class="font">Session Vacation End</td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrMarkSheeetSubmission" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="clrMarkSheeetSubmission" Format="dd/mm/yyyy" />
                                            <%--<dxe:ASPxDateEdit ID="clrMarkSheeetSubmission" runat="server" Width = "100%">
                                            </dxe:ASPxDateEdit>--%>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrAnswerScriptSubmission" class="margin-zero input-Size" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="clrAnswerScriptSubmission" Format="dd/mm/yyyy" />
                                            <%-- <dxe:ASPxDateEdit ID="clrAnswerScriptSubmission" runat="server" Width = "100%">
                                            </dxe:ASPxDateEdit>--%>
                                        </td>
                                        <td style="width: 50px;"></td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrSessionVacationStart" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender18" runat="server" TargetControlID="clrSessionVacationStart" Format="dd/MM/yyyy" />
                                            <%--<dxe:ASPxDateEdit ID="clrSessionVacationStart" runat="server" Width = "100%">
                                            </dxe:ASPxDateEdit>--%>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox runat="server" ID="clrSessionVacationEnd" class="margin-zero input-Size"  />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender19" runat="server" TargetControlID="clrSessionVacationEnd" Format="dd/MM/yyyy" />
                                            <%--<dxe:ASPxDateEdit ID="clrSessionVacationEnd" runat="server" Width = "100%">
                                            </dxe:ASPxDateEdit>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <div style="margin-left: 5px; float: right;">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="cursor"  Text="Cancel" Height="30"></asp:Button>
                                </div>
                                <div style="margin-left: 5px; float: right;">
                                    <asp:Button ID="btnAddAndNext" runat="server" OnClick="btnAdd_Click" CssClass="cursor"  Text="Add New Course" Height="30"></asp:Button>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="GridViewTable">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div>
                        <div style="margin-left: 5px; float: left;">
                            <asp:Button ID="btnAdd" Visible="true" runat="server" OnClick="btnAdd_Click" Text="Add New"></asp:Button>
                        </div>

                        <div style="margin-left: 5px; float: left;">
                            <asp:Button ID="btnRefresh" runat="server"  Text="Refresh"></asp:Button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
             <div style="clear: both;"></div>
            <asp:GridView runat="server" ID="gvBillView" AutoGenerateColumns="False"
                AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                PageSize="20" CssClass="gridCss" OnRowCommand="gvBillView_RowCommand" >
                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFFFCC" />

                <Columns>
                    <asp:TemplateField HeaderText="Code" Visible="false"  HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStudentCourseHisId" Text='<%#Eval("StudentCourseHistoryId") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblFormalCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurpose" Text='<%#Eval("CourseTitle") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("Fees") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit"
                                ToolTip="Item Edit" CommandName="EditBillHistory" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                                OnClientClick="return confirm('Are you sure to Delete this ?')"
                                ToolTip="Item Delete" CommandName="DeleteBillHistory" CommandArgument='<%#Eval("BillHistoryId") %>'>                                                
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <%--<RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />>--%>
                <EmptyDataTemplate>
                    No data found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
