<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master"  CodeBehind="BillManualEntry.aspx.cs" Inherits="EMS.miu.registration.BillManualEntry" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Registration Menual Entry
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
         .auto-style2 {
             width: 108px;
         }
         .auto-style3 {
             width: 97px;
         }
         .auto-style4 {
             width: 130px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div style="padding: 10px; width: 1250px;">
        <div class="PageTitle">
            <label>Student Manual Bill Entry</label>
        </div>
        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both;"></div>
        <div class="Message-Area" style="height: auto">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width:auto;" id="tblHeader">
                        <tr valign="top">
                            <td class="auto-style7">
                                <table >
					                <tr>
						                <td  class="auto-style4" align="left" >
							                Date &nbsp;</td>
						                <td align="left" class="auto-style9">
							                <asp:TextBox runat="server" ID="DateTextBox" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateTextBox" Format="dd/MM/yyyy" />
                                            <sup class="warningDOB">*</sup>
						                </td>
					                </tr>
                                    <%--<tr>
                                        <td class="auto-style4">Posting Date &nbsp;</td>
                                        <td align="left" class="auto-style9">
                                            <asp:TextBox ID="PostingDateTextBox" runat="server" class="margin-zero input-Size datepicker" DataFormatString="{0:dd/MM/yyyy}" />
                                            <sup class="warningDOB">*</sup> </td>
                                    </tr>--%>
				                    <tr>
                                        <td align="left" class="auto-style4">Choose a Type </td>
                                        <td align="left" class="auto-style9">
                                            <asp:RadioButtonList ID="studentTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studentTypeList_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="Single Student" Value="1" />
                                                <asp:ListItem Text="Multiple Student" Value="2" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="singleStudentPanel" runat="server">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="width:130px;">
                                                            <asp:Label ID="lblStudentId" runat="server" Text="Student Id"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:TextBox ID="txtStudentId" runat="server" placeholder="Student Id" style="margin-left: 0px" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="LoadCourseButton" runat="server" OnClick="LoadCourseButton_Click" Text="Load" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="multiStudentDDLPanel" runat="server">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="width:130px;">
                                                            <asp:Label ID="lblProgram" runat="server" Text="Program"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width:150px;">
                                                            <uc1:ProgramUserControl ID="ucProgram" runat="server" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width:130px;">
                                                            <asp:Label ID="lblBatch" runat="server" Text="Batch"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width:150px;">
                                                            <uc1:BatchUserControl ID="ucBatch" runat="server" class="margin-zero dropDownList" />
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:Button ID="LoadButton" runat="server" OnClick="LoadButton_Click" Text="Load Student" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label>
                                        </td>
                                        <td class="auto-style9">
                                            <uc1:SessionUserControl ID="ucSession" runat="server" class="margin-zero dropDownList" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblFeeType" runat="server" Text="Fee"></asp:Label>
                                        </td>
                                        <td class="auto-style9">
                                            <asp:DropDownList ID="ddlFeeType" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label>
                                        </td>
                                        <td class="auto-style6">
                                            <asp:DropDownList ID="ddlStudentCourse" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                                        </td>
                                        <td class="auto-style9">
                                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                        </td>
                                        <td class="auto-style9">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="70px" placeholder="Remark" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Button ID="btnSave" runat="server" Height="25px" onclick="btnSave_Click" Text="Save" Width="124px" />
                                        </td>
                                    </tr>
				                </table>

                            </td>
                            <td align="left" class="auto-style2">
                                <asp:Button ID="CheckedAllButton" runat="server" OnClick="CheckedButton_Click" Text="Select All" Visible="false" />
                                <asp:Button ID="UncheckedAllButton" runat="server" OnClick="UncheckedButton_Click" Text="Unchecked All" Visible="false" />
                                <asp:Panel ID="StudentGridPanel" runat="server">
                                    <asp:GridView ID="gvStudentList" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="gridCss" EmptyDataText="No data found." Width="469px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="True">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student Roll">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentRoll" runat="server" Text='<%# Bind("Roll") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Student Name">
                                            <HeaderStyle Width="150px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="#FFFFCC" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                        <SortedDescendingHeaderStyle BackColor="#820000" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="height: auto; width: 1100px">

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
                            <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Add New Bill</legend>
                            <div style="padding: 5px;"></div>                                                                                                           
                            <div >
                                 <table>
                                    <tr>
                                        <td><asp:Label ID="lblStudentRoll" runat="server" Text="Student Roll"></asp:Label></td>
                                        <td><asp:TextBox Enabled="false" ID="txtStudentRoll" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                    <td><asp:Label ID="lblSemester" runat="server" Text="Semester/Trimester"></asp:Label></td>
                                    <td>
                                        <uc1:ProgramUserControl runat="server" ID="ProgramUserControl1" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged2" class="margin-zero dropDownList" />
                                        <uc1:SessionUserControl runat="server" ID="SessionUserControl1" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged2" class="margin-zero dropDownList"/>
                                    </td>
                                    </tr>
                                     <tr>
                                        <td><asp:Label ID="lblFeesAmount" runat="server" Text="Amount"></asp:Label></td>
                                        <td><asp:TextBox ID="txtFeesAmount" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblTypeDefination" runat="server" Text="Type"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeDefination" runat="server"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </div>

                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <div style="margin-left: 5px; float: right;">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="cursor"  Text="Cancel" Height="30"></asp:Button>
                                </div>
                                <div style="margin-left: 5px; float: right;">
                                    <asp:Button ID="btnAddNewBillHistory" runat="server" OnClick="btnAddNewBillHistory_Click" CssClass="cursor"  Text="Add New Bill" Height="30"></asp:Button>
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
                <EmptyDataTemplate>
                    No data found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>--%>
