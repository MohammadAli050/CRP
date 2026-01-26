<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="PersonalProfileFillUpPage.aspx.cs" Inherits="PersonalProfileFillUpPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Information</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>

    <style>
        .glow_text {
            animation: glow .5s infinite alternate;
        }

        @keyframes glow {
            to {                
                text-shadow: 0 0 1px Red;
            }
        }

        .glow_text {
            font-family: sans-serif;
            font-weight: bold;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle">
            <label>Student Information</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label CssClass="glow_text" runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="Message-Area glow_text"> 
                    <asp:Label runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium" ID="Label1" Text="Please Provide Your Basic Information (Father's Name, Mother's Name, Mobile Number(SMS self Contact), Guardian Name ). Otherwise You Won't Be Able To Fill-Up The Form." />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         


        <div class="personalProfile-container">
            <asp:Panel runat="server" ID="pnStudentIDZone">
                <div class="div-margin">
                    <div class="loadedArea">
                        <label class="display-inline field-Title">Student ID</label>
                        <asp:TextBox runat="server" ID="txtStudentID" class="margin-zero input-Size3" />
                        <asp:Button runat="server" ID="btnLoad" class="margin-zero btn-size" Text="Load" OnClick="btnLoad_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnIsVisible">

                <div class="div-margin">
                    <asp:Image runat="server" ID="imgPhoto" ImageUrl="../../Images/photoBoy.png" />
                    <asp:HiddenField runat="server" ID="hfPhotoRoll" Value="" />
                </div>

                <div class="div-margin">
                    <asp:Button runat="server" ID="btnUpload" class="margin-zero btn-size" Text="Upload" OnClick="btnUpload_Click" />
                    <asp:FileUpload runat="server" ID="FileUploadPhoto" />
                    <div>
                        uploaded picture's size must not exceed 200 KB and format must be .jpg, .jpeg or .png
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel02" runat="server">
                    <ContentTemplate>
                        <div class="div-margin">
                            <div class="information-Zone">
                                <div class="loadAreaOdd">
                                    <label class="display-inline field-Title">Name</label>
                                    <asp:TextBox runat="server" ID="txtFullName" class="margin-zero input-Size" Enabled="True" />
                                    <asp:HiddenField runat="server" ID="hfStudent" Value="0" />
                                    <asp:HiddenField runat="server" ID="hfRoll" Value="" />
                                    <asp:HiddenField runat="server" ID="hfPerson" Value="0" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label style="color:red" class="display-inline field-Title">* Gender</label>
                                    <asp:DropDownList runat="server" ID="ddlGender" class="margin-zero dropDownList">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="male">Male</asp:ListItem>
                                        <asp:ListItem Value="female">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div> 
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title">Blood Group</label>
                                    <asp:DropDownList runat="server" ID="ddlBloodGroup" class="margin-zero dropDownList">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="a+">A+</asp:ListItem>
                                        <asp:ListItem Value="a-">A-</asp:ListItem>
                                        <asp:ListItem Value="ab+">AB+</asp:ListItem>
                                        <asp:ListItem Value="ab-">AB-</asp:ListItem>
                                        <asp:ListItem Value="b+">B+</asp:ListItem>
                                        <asp:ListItem Value="b-">B-</asp:ListItem>
                                        <asp:ListItem Value="o+">O+</asp:ListItem>
                                        <asp:ListItem Value="o-">O-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title">Contact No.</label>
                                    <asp:TextBox runat="server" ID="txtPhone" class="margin-zero input-Size" placeholder="Phone / Mobile Number" />
                                </div>

                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title">Nationality</label>
                                    <asp:TextBox runat="server" ID="txtNationality" class="margin-zero input-Size" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label style="color:red" class="display-inline field-Title">* Father Name</label>
                                    <asp:TextBox runat="server" ID="txtFatherName" class="margin-zero input-Size" placeholder="Father's Name" />
                                </div>
                                <div class="loadedAreaEven">
                                    <label style="color:red" class="display-inline field-Title">* Mother Name</label>
                                    <asp:TextBox runat="server" ID="txtMotherName" class="margin-zero input-Size" placeholder="Mother's Name" />
                                </div>
                                <div style="height: 38px"></div>
                                <div class="loadedAreaOdd">
                                    <label style="color:red" ><b>* SMS Contact Self <font color="red">(Format: +8801XXXXXXXXX)</font></b></label>
                                    <asp:TextBox runat="server" ID="txtSMSContactSelf" class="margin-zero input-Size" placeholder="Phone / Mobile Number" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtSMSContactSelf" ErrorMessage="Wrong Format"
                                        Style="z-index: 101; left: 424px; position: absolute; top: 285px"
                                        ValidationExpression="^(\+88)01[15-9]\d{8}$" ValidationGroup="check">
                                    </asp:RegularExpressionValidator>
                                </div>

                                <div class="loadedAreaOdd">
                                    <label>SMS Contact Gaurdian <font color="red">(Format: +8801XXXXXXXXX)</font></label>
                                    <asp:TextBox runat="server" ID="txtSMSContactGuardian" class="margin-zero input-Size" placeholder="Phone / Mobile Number" />
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtSMSContactGuardian" ErrorMessage="Wrong Format"
                                        Style="z-index: 101; left: 424px; position: absolute; top: 285px"
                                        ValidationExpression="^(\+88)01[15-9]\d{8}$" ValidationGroup="check">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="loadedAreaOdd">
                                    <%--<label><font color="red">***Please enter only one number for each SMS Contact field.</font></label>--%>

                                </div>
                            </div>
                            <div class="information-Zone">
                                <div class="loadAreaEven">
                                    <label class="display-inline field-Title">Date of Birth</label>
                                    <asp:TextBox runat="server" ID="txtDOB" class="margin-zero input-Size1 datepicker" DataFormatString="{0:dd/MM/yyyy}" />
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy" />
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title">Marital Status</label>
                                    <asp:DropDownList runat="server" ID="ddlMatrialStatus" class="margin-zero dropDownList">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="married">Married</asp:ListItem>
                                        <asp:ListItem Value="unmarried">Unmarried</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title">Religion</label>
                                    <asp:DropDownList ID="ddlReligion" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Islam"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hinduism"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Christianity"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Buddhism"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Other"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title">Email</label>
                                    <asp:TextBox runat="server" ID="txtEmail" class="margin-zero input-Size" placeholder="Email Address" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label style="color:red" class="display-inline field-Title">* Guardian Name</label>
                                    <asp:TextBox runat="server" ID="txtGuardianName" class="margin-zero input-Size" placeholder="Guardian's Name" />
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title">Profession</label>
                                    <asp:TextBox runat="server" ID="txtFatherProfession" class="margin-zero input-Size" placeholder="Father's Profession" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title">Profession</label>
                                    <asp:TextBox runat="server" ID="txtMotherProfession" class="margin-zero input-Size" placeholder="Mother's Profession" />
                                </div>
                            </div>
                            <div hidden="hidden" class="information-Zone">
                                <asp:HiddenField runat="server" ID="hfPresent" Value="0" />
                                <asp:HiddenField runat="server" ID="hfPermanent" Value="0" />
                                <asp:HiddenField runat="server" ID="hfGuardian" Value="0" />
                                <asp:HiddenField runat="server" ID="hfMailing" Value="0" />

                                <asp:HiddenField runat="server" ID="hfPresentType" Value="0" />
                                <asp:HiddenField runat="server" ID="hfPermanentType" Value="0" />
                                <asp:HiddenField runat="server" ID="hfGuardianType" Value="0" />
                                <asp:HiddenField runat="server" ID="hfMailingType" Value="0" />
                                <div class="loadAreaOdd">
                                    <label class="display-inline field-Title2">Present</label>
                                    <asp:TextBox runat="server" ID="txtPresent" class="margin-zero input-Size2" TextMode="MultiLine" placeholder="Present Address" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title2">Permanent</label>
                                    <asp:TextBox runat="server" ID="txtPermanent" class="margin-zero input-Size2" TextMode="MultiLine" placeholder="Permanent Address" />
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title2">Guardian</label>
                                    <asp:TextBox runat="server" ID="txtGuardian" class="margin-zero input-Size2" TextMode="MultiLine" placeholder="Guardian Address" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title2">Mailing</label>
                                    <asp:TextBox runat="server" ID="txtMailing" class="margin-zero input-Size2" TextMode="MultiLine" placeholder="Mailing Address" />
                                </div>
                            </div>
                            <div class="cleaner"></div>
                        </div>

                        <div class="div-margin">
                            <asp:Button runat="server" ID="btnSave" class="margin-zero btn-size" Text="Save" validationgroup="check" OnClick="btnSave_Click" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>
        </div>

    </div>
</asp:Content>
