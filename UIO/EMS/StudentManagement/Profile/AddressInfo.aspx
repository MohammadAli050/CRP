<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="AddressInfo.aspx.cs" Inherits="EMS.bup.AddressInfo" %>
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
            <li><a class="active" href="AddressInfo.aspx">Address Information</a></li>
            <li><a href="ContactInfo.aspx">Contact Information</a></li>
            <li><a href="PhotoAndSignature.aspx">Photo & Signature</a></li>
            <li><a href="EducationInfo.aspx">Education Information</a></li>
            <li><a href="../StudentManagementHome.aspx">Back to Management Home</a></li>
        </ul>
    </div>
    <div>
        <div class="col2">
            <h3>Present Address</h3>
            <label>Present Address</label>
            <asp:TextBox runat="server" ID="txtPresentAddress" TextMode="MultiLine" class="inputText" />

            <label>District</label>
            <asp:DropDownList runat="server" ID="ddlPresentDistrict">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>

        </div>
        <div class="col2">
            <h3>Permanent Address (<asp:CheckBox ID="chkSameAddress" runat="server" Text="Same as Present" OnCheckedChanged="chkSameAddressChanged" AutoPostBack="true" />)</h3> 
            
            <label>Permanent Address</label>
            <asp:TextBox runat="server" ID="txtPermanentAddress" TextMode="MultiLine" class="inputText" />

            <label>District</label>
            <asp:DropDownList runat="server" ID="ddlPermanentDistrict">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="col2">
            <h3>Guardian Address</h3>
            <label>Guardian Address</label>
            <asp:TextBox runat="server" ID="txtGuardianAddress" TextMode="MultiLine" class="inputText" />

            <label>District</label>
            <asp:DropDownList runat="server" ID="ddlGuardianDistrict">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col2">
            <h3>Mailing Address</h3>
            <label>Mailing Address</label>
            <asp:TextBox runat="server" ID="txtMailingAddress" TextMode="MultiLine" class="inputText" />

            <label>District</label>
            <asp:DropDownList runat="server" ID="ddlMailingDistrict">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
        </div>

         <div style="clear:both;"></div>
            <asp:Button runat="server" ID="btnSubmit" class="btnSubmit" Text="Save" OnClick="btnSubmit_Click" />
    </div>
    <div style="clear:both;"></div>
</asp:Content>
