<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ExamResultUnSubmit.aspx.cs" Inherits="EMS.miu.result.ExamResultUnSubmit" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Result Un-Submit</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Exam Result Un-Submit</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/>
                        </td>
                        <td class="auto-style4">
                            <b>Student Roll :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox runat="server" ID="txtStudentRoll"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnLoad" runat="server" Text="Load Course" OnClick="btnLoad_Click" />
                        </td>                          
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblName" Visible="false" Text="Student Name : " Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblStudentName" Visible="false"></asp:Label>
                        </td>
                    </tr>               
                </table>
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>

                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="450px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>

                    </tr>
                </table>
                
            </div>
            <br />
            <asp:Panel runat="server" ID="pnlExamUnSubmit" Visible="false">
            <table>
                <tr>
                    
                    
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label runat="server" ID="lblExam" Font-Bold="true" Text="Exam List:"></asp:Label>
                    </td>
                    
                </tr>
                <td>
                    <asp:CheckBox runat="server" ID="cbxAllSelect" Text="Select All" OnCheckedChanged="cbxSelectAll_Checked" AutoPostBack="true" />
                    <asp:CheckBoxList runat="server" ID="cblExamList"></asp:CheckBoxList>
                    <asp:Label ID="lblExamTemplateBasicItemId" Visible="false" runat="server"></asp:Label>
                </td>
                
            </table>
            <asp:Button runat="server" ID="btnUnSubmit" Text="Un-Submit Mark" Height="35px" OnClick="btnUnSubmit_Clicked"/>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>&nbsp;</div>
    
</asp:Content>