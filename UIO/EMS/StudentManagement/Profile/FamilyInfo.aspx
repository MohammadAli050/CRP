<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="FamilyInfo.aspx.cs" Inherits="EMS.bup.FamilyInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../../CSS/StudentManagement/bup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="pageNav">
        <ul>
            <li><a href="StudentProfile.aspx">Basic Information</a></li>
            <li><a class="active" href="FamilyInfo.aspx">Family Information</a></li>
            <%--<li><a href="AddressInfo.aspx">Address Information</a></li>--%>
            <li><a href="ContactInfo.aspx">Contact Information</a></li>
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

        <h3>Family Information
             <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>
        </h3>

        <div class="col2">
            <label>Mother's Name</label>
            <asp:TextBox runat="server" ID="txtMotherName" class="inputText" />

            <label>Mother's Profession</label>
            <asp:TextBox runat="server" ID="txtMotherProfession" class="inputText" />

           <%-- <label>Spouse Name</label>
            <asp:TextBox runat="server" ID="txtSpouseName" class="inputText" />--%>

            <label>Legal/Local Guardian's Name</label>
            <asp:TextBox runat="server" ID="txtGuardianName" class="inputText" />
           
        </div>
        <div class="col2">
            <label>Father's Name</label>
            <asp:TextBox runat="server" ID="txtFatherName" class="inputText" />

            <label>Father's Profession</label>
            <asp:TextBox runat="server" ID="txtFatherProfession" class="inputText" />

            <%--<label>Total Monthly Income of Parents/Guardian/Yourself in Taka</label>
            <asp:TextBox runat="server" ID="txtMonthlyIncome" class="inputText" />--%>

             <asp:Button style="display: block; margin-top: 20px; width: 250px;" runat="server" ID="btnSubmit" class="btnSubmit" Text="Save" OnClick="btnSubmit_Click" />
        </div>
        <div style="clear:both;"></div>            
    </div>
    <div style="clear:both;"></div>
</asp:Content>
