<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ContactInfo.aspx.cs" Inherits="EMS.bup.ContactInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../../CSS/StudentManagement/bup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="pageNav">
        <ul>
            <li><a href="StudentProfile.aspx">Basic Information</a></li>
            <li><a href="FamilyInfo.aspx">Family Information</a></li>
            <%--<li><a href="AddressInfo.aspx">Address Information</a></li>--%>
            <li><a class="active" href="ContactInfo.aspx">Contact Information</a></li>
            <li><a href="PhotoAndSignature.aspx">Photo & Signature</a></li>
            <%--<li><a href="EducationInfo.aspx">Education Information</a></li>
            <li><a href="../StudentManagementHome.aspx">Back to Management Home</a></li>--%>
        </ul>
    </div>
    <div>
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
        <h3>Contact Information
             <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>

        </h3>

        <div class="col2">
            <label>Mobile</label>
            <asp:TextBox runat="server" ID="txtMobile" class="inputText" placeholder="+880XXXXXXXXXX" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                    ControlToValidate="txtMobile" ErrorMessage="Wrong Format"
                                                                    Style="z-index: 101; left: 424px; position: absolute; top: 285px"
                                                                    ValidationExpression="^(?:\+?88)?01[15-9]\d{8}$" ValidationGroup="check">
                                                                </asp:RegularExpressionValidator>
            <label>Email</label>
            <asp:TextBox runat="server" ID="txtEmail" class="inputText" />
            
            <asp:Button style="display: block; margin-top: 20px; width: 250px;" runat="server" ID="btnSubmit" class="btnSubmit" Text="Save" ValidationGroup="check" OnClick="btnSubmit_Click" />
        </div>
        <%--<div class="col2">
            <label>Parent Mobile/Telephone</label>
            <asp:TextBox runat="server" ID="txtParentMobile" class="inputText" />
            <label>Guardian Mobile/Telephone</label>
            <asp:TextBox runat="server" ID="txtGuardianMobile" class="inputText" />
        </div>--%>
        <div style="clear:both;"></div>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
