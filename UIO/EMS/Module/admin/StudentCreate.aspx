<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentCreate.aspx.cs" Inherits="EMS.miu.admin.StudentCreate" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    Student Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 150px;
        }
        .auto-style3 {
            width: 472px;
        }
        .lblCss {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <%--<div style="height: auto; width: 100%">--%>
        <div class="PageTitle">
            <label>Student Create</label>
        </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>                  
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
       <%-- <asp:Panel runat="server" ID="pnMessage">
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" />
            </div>
        </asp:Panel>--%>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <div style="padding: 5px; margin: 5px; width: 900px;">
                        <table>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblStudentRoll" runat="server" width="150px" Text="Student Roll"></asp:Label>   
                                </td>
                                <td class="auto-style3">        
                                    <asp:TextBox ID="txtStudentRoll" Placeholder="Student Roll" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnStudentCheck" runat="server" Width="80px" Text="Check" OnClick="btnStudentCheck_Click" />
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblStudentName" runat="server" width="150px" Text="Student Name"></asp:Label>   
                                </td>
                                <td class="auto-style3">   
                                    <asp:TextBox ID="txtStudentName" Placeholder="Student Name" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblPhoneNo" runat="server" width="150px" Text="Phone No"></asp:Label>  
                                </td>
                                <td class="auto-style3">      
                                    <asp:TextBox ID="txtPhoneNo" Placeholder="Phone No" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblEmailAddress" runat="server" width="150px" Text="Email Address"></asp:Label>   
                                </td>
                                <td class="auto-style3">     
                                    <asp:TextBox ID="txtEmailAddress" Placeholder="Email Address" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblDate" runat="server" width="150px" Text="DOB"></asp:Label>
                                </td>
                                <td class="auto-style3">  
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="164px"  class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateTextBox" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblProgram" width="150px" runat="server" Text="Program"></asp:Label>
                                </td>
                                <td class="auto-style3">  
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" class="margin-zero dropDownList" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="lblBatch" width="150px" runat="server" Text="Batch"></asp:Label>
                                </td>
                                <td class="auto-style3">  
                                    <uc1:BatchUserControl runat="server" ID="ucBatch" class="margin-zero dropDownList"  />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblGender" runat="server" width="150px"  Text="Gender"></asp:Label> 
                                </td>
                                <td class="auto-style3">       
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem Text="Male" Value="0" />
                                        <asp:ListItem Text="Female" Value="1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Button ID="btnSubmit" Enabled="false" runat="server" Width="120px" Text="Add Student" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    <%--</div>--%>
    <asp:HiddenField ID="HiddenStudentId" runat="server" />
</asp:Content>