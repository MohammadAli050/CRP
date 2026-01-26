<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_StudentProfile" CodeBehind="StudentProfile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style type="text/css">
        .right {
            text-align: right
        }

        .margin_left {
            margin-left: 20px;
        }

        .auto-style1 {
            width: 213px;
            font-weight: bold;
        }

        .auto-style3 {
            text-align: left;
            padding-left: 8px;
            width: 104px;
            font-weight: bold;
        }

        .auto-style5 {
            text-align: left;
            padding-left: 8px;
            width: 92px;
            font-weight: bold;
        }

        .auto-style6 {
            width: 134px;
        }

        .auto-style7 {
            width: 92px;
        }

        .auto-style8 {
            text-align: left;
            padding-left: 8px;
            width: 127px;
            font-weight: bold;
        }

        .auto-style9 {
            width: 127px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="PageTitle" style="font-size: 10px;">
        <label>Student Profile Information</label>
    </div>
    <div>
        <div style="width: 50%; float: left; border: 0px solid gray;">
            <table style="margin: 0 auto; width: 80%;">
                <tr>
                    <td colspan="2" class="auto-style6">
                        <h3 style="text-align: left; width: 343px;">Basic Information

                    <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <asp:Image runat="server" ID="imgPhoto" ImageUrl="../../Images/photoBoy.png" />
                    </td>
                    <td colspan="2" class="auto-style1">
                        <asp:FileUpload ID="ImageUpload" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                </tr>

                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label1" runat="server" Text="Label">Full Name: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label5" runat="server" Text="Label">Program ID: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label23" runat="server" Text="Label">Registration No: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblRegNo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label2" runat="server" Text="Label">Roll: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblRoll" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label11" runat="server" Text="Label">CGPA: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblCgpa" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label22" runat="server" Text="Label">Guardian Name: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblGuardianName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label3" runat="server" Text="Label">Father Name: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label6" runat="server" Text="Label">Mother Name: </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblMotherName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label4" runat="server" Text="Label">Gender:  </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label7" runat="server" Text="Label">Email:  </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label8" runat="server" Text="Label">Phone:  </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label9" runat="server" Text="Label">Blood Group:  </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblBloodGroup" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #F8F9FC">
                    <td class="auto-style5">
                        <asp:Label ID="Label10" runat="server" Text="Label">Religion:  </asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                </tr>
                <%--<tr>
                    <td class="auto-style7"></td>
                    <td style="text-align: left;"> 
                        <asp:Button ID="btnSave" runat="server" Text="UPDATE" OnClick="btnSave_Click" />
                    </td>
                </tr>--%>
            </table>
        </div>
        <div style="width: 40%; float: left;">
            <div style="float: left; width: 100%; border: 0px solid gray;">
                <h3 style="text-align: left">Address</h3>
                <table style="width: 80%;">


                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style3">
                            <asp:Label ID="Label12" runat="server" Text="Label">House: </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblHouse" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style3">
                            <asp:Label ID="Label18" runat="server" Text="Label">Road:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRoad" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style3">
                            <asp:Label ID="Label19" runat="server" Text="Label">Thana:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblThana" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style3">
                            <asp:Label ID="Label20" runat="server" Text="Label">District:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style3">
                            <asp:Label ID="Label21" runat="server" Text="Label">Post Code:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPostCode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                </table>

            </div>
            <div>&nbsp;</div>
            <div>&nbsp;</div>
            <div style="float: left; width: 100%; border: 0px solid gray;">
                <h3 style="text-align: left">Other Information</h3>
                <table style="width: 80%;">


                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style8">
                            <asp:Label ID="Label13" runat="server" Text="Label">Nick Name: </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNickName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style8">
                            <asp:Label ID="Label14" runat="server" Text="Label">Matrial Status:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMaritalStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style8">
                            <asp:Label ID="Label15" runat="server" Text="Label">Father Profession:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFatherProfession" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style8">
                            <asp:Label ID="Label16" runat="server" Text="Label">Mother Profession:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMotherProfession" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #F8F9FC">
                        <td class="auto-style8">
                            <asp:Label ID="Label17" runat="server" Text="Label">Country:  </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9">&nbsp;</td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
    <div class="cleaner"></div>


</asp:Content>

