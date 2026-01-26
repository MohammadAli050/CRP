<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    Inherits="Setup_AcademicCalenderSchedule" Codebehind="AcademicCalenderSchedule.aspx.cs" %>
<%--<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>--%>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div style="margin: 10px;">
        <fieldset>
            <legend style="font-weight: bold; font-size: medium;">Academic Calender Schedule</legend>
            <%--<asp:Label ID="lblMessage" runat="server" Visible ="false"></asp:Label>--%>
            <div style="clear: both;"></div>
            <div style="margin: 10px; width: 100%;">
                <div style="clear: both;"></div>
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
                                        <table style="padding: 1px;" border="0">
                                            <tr>
                                                <td><b>Program :</b></td>                                                
                                                <td><uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/></td>                                                                                                                                                    
                                               
                                                <td><b>Academic Calender :</b></td>
                                                <td><uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/></td>                                
                                            </tr> 
                                            <tr>
                                                <td colspan="4"><asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible ="false"></asp:Label></td>                                
                                            </tr>                                            
                                        </table>                             
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
                                            <asp:Button ID="btnAddAndNext" runat="server" OnClick="btnAddAndNext_Click" CssClass="cursor"  Text="Insert & Next" Height="30"></asp:Button>
                                        </div>

                                        <div style="margin-left: 5px; float: right;">
                                            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" CssClass="cursor" Text="Insert" Height="30"/>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="clear: both;"></div>
                <div id="GridViewTable" style="padding-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div>
                                <div style="margin-left: 5px; float: left;">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New"></asp:Button>
                                </div>

                                <div style="margin-left: 5px; float: left;">
                                    <asp:Button ID="btnRefresh" runat="server"  Text="Refresh"></asp:Button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="clear: both;"></div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="margin-top: 15px;">
                                <asp:GridView runat="server" ID="gvAcaCalShedule" AutoGenerateColumns="False" Width="50%"
                                    CssClass="gridCss">
                                    <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#F0F8FF" />

                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="Academic Calender Schedule Id" HeaderStyle-Width="120px" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblAcademicCalenderScheduleId" Text='<%#Eval("AcademicCalenderScheduleId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:boundfield DataField="ClassStartDate" HeaderText="Class Start Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="ClassEndDate" HeaderText="Class End Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="MidExamStartDate" HeaderText="Mid Exam Start Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="MidExamEndDate" HeaderText="Mid Exam End Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="AdvisingStartDate" HeaderText="Advising Start Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="AdvisingEnd" HeaderText="Advising End Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="RegistrationStartDate" HeaderText="Registration Start Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="RegistrationEndDate" HeaderText="Registration End Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="AdmissionStartDate" HeaderText="Admission Start Date" DataFormatString="{0:dd-MMM-yy}" />
                                        <asp:boundfield DataField="AdmissionEndDate" HeaderText="Admission End Date" DataFormatString="{0:dd-MMM-yy}" />   
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"
                                                    ToolTip="Item Edit" CommandArgument='<%#Eval("AcademicCalenderScheduleId") %>'>                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                                    OnClientClick="return confirm('Are you sure to Delete this ?')"
                                                    ToolTip="Item Delete" CommandArgument='<%#Eval("AcademicCalenderScheduleId") %>'>                                                
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </fieldset>

    </div>
</asp:Content>

