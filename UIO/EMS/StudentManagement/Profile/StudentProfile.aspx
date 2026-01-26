<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="EMS.bup.StudentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../../CSS/StudentManagement/bup.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="pageNav">
        <ul>
            <li><a class="active" href="StudentProfile.aspx">Basic Information</a></li>
            <li><a href="FamilyInfo.aspx">Family Information</a></li>
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
        <h3>Basic Information
            
                    <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>
        </h3>

        <div class="col2">
            <label>Name</label>
            <asp:TextBox runat="server" ID="txtStudentName" class="inputText" />

            <label>Date of Birth</label>
            <asp:TextBox runat="server" ID="txtDOB" class="inputText" />
            <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy" />

            <label>Blood Group</label>
            <asp:DropDownList runat="server" ID="ddlBloodGroup">
                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                <asp:ListItem Value="A+" Text="A+"></asp:ListItem>
                <asp:ListItem Value="A-" Text="A-"></asp:ListItem>
                <asp:ListItem Value="B+" Text="B+"></asp:ListItem>
                <asp:ListItem Value="B-" Text="B-"></asp:ListItem>
                <asp:ListItem Value="AB+" Text="AB+"></asp:ListItem>
                <asp:ListItem Value="AB-" Text="AB-"></asp:ListItem>
                <asp:ListItem Value="O+" Text="O+"></asp:ListItem>
                <asp:ListItem Value="O-" Text="O-"></asp:ListItem>
            </asp:DropDownList>

            <label>Gender</label>
            <asp:DropDownList runat="server" ID="ddlGender">
                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
            </asp:DropDownList>

           
        </div>
        <div class="col2">
            <label>Maritial Status</label>
            <asp:DropDownList runat="server" ID="ddlMaritialStatus">
                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                <asp:ListItem Value="Unmarried" Text="Unmarried"></asp:ListItem>
            </asp:DropDownList>

            <label>Religion</label>
            <asp:DropDownList runat="server" ID="ddlReligion">
                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                <asp:ListItem Value="1" Text="Islam"></asp:ListItem>
                <asp:ListItem Value="2" Text="Hinduism"></asp:ListItem>
                <asp:ListItem Value="3" Text="Christianity"></asp:ListItem>
                <asp:ListItem Value="4" Text="Buddhism"></asp:ListItem>
                <asp:ListItem Value="5" Text="Other"></asp:ListItem>
            </asp:DropDownList>

            <label>Nationality</label>
            <asp:TextBox runat="server" ID="txtNationality" class="inputText" />

             <%--<label>National ID</label>
            <asp:TextBox runat="server" ID="txtNID" class="inputText" />--%>
            <label></label>
            <asp:Button style="display: block; margin-top: 20px; width: 250px;" runat="server" ID="btnSubmit" class="btnSubmit" Text="Save" OnClick="btnSubmit_Click" />
        </div>            
    </div>
    <div style="clear:both;"></div>
</asp:Content>
