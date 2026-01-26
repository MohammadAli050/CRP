<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="PhotoAndSignature.aspx.cs" Inherits="EMS.bup.PhotoAndSignature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../../CSS/StudentManagement/bup.css" rel="stylesheet" />
    <style>
        .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding: 15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
        }

        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image: url('success.png');
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="pageNav">
        <ul>
            <li><a href="StudentProfile.aspx">Basic Information</a></li>
            <li><a href="FamilyInfo.aspx">Family Information</a></li>
            <%--<li><a href="AddressInfo.aspx">Address Information</a></li>--%>
            <li><a href="ContactInfo.aspx">Contact Information</a></li>
            <li><a class="active" href="PhotoAndSignature.aspx">Photo & Signature</a></li>
            <%--<li><a href="EducationInfo.aspx">Education Information</a></li>
            <li><a href="../StudentManagementHome.aspx">Back to Management Home</a></li>--%>
        </ul>
    </div>

    <div class="card">
        <div class="card-body">

            <h3 style="text-align: left">Student Photo & Signature
                <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>
            </h3>

            <div class="row">

                <asp:HiddenField ID="hdnPersonId" runat="server" />

                <div class="col-lg-6 col-md-6 col-sm-6" style="text-align: center">
                    <b>Photo</b>
                    <br />
                    <asp:Image runat="server" ID="imgPhoto" Height="100" Width="100" />
                    <br />
                    <label class="text-danger">***Less than 200KB</label>
                    <br />
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <asp:FileUpload ID="FileUploadPhoto" runat="server" accept=".jpeg,.jpg,.png" CssClass="btn btn-info  form-control" Width="100%" />
                        </div>
                        <div class="col-lg-4 col-sm-4 col-md-4">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" Height="38px" CssClass="btn btn-success btn-sm form-control" OnClick="btnUploadPhoto_Click" />
                        </div>
                    </div>

                </div>
                <div class="col-lg-6 col-md-6 col-sm-6" style="border-left: solid; border-color: black; text-align: center">
                    <b>Signature</b>
                    <br />
                    <asp:Image runat="server" ID="imgSig" Height="100" Width="120" />
                    <br />
                    <label class="text-danger">***Less than 100KB</label>
                    <br />
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <asp:FileUpload ID="FileUploadSignature" runat="server" accept=".jpeg,.jpg,.png" CssClass="btn btn-info  form-control" Width="100%" />

                        </div>
                        <div class="col-lg-4 col-sm-4 col-md-4">
                            <asp:Button ID="btnUploadSig" runat="server" Text="Upload" Height="38px" CssClass="btn btn-success btn-sm form-control" OnClick="btnUploadSig_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
