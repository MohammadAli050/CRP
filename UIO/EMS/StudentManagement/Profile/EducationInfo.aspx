<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="EducationInfo.aspx.cs" Inherits="EMS.bup.EducationInfo" %>
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
            <li><a href="AddressInfo.aspx">Address Information</a></li>
            <li><a href="ContactInfo.aspx">Contact Information</a></li>
            <li><a href="PhotoAndSignature.aspx">Photo & Signature</a></li>
            <li><a class="active" href="EducationInfo.aspx">Education Information</a></li>
            <li><a href="../StudentManagementHome.aspx">Back to Management Home</a></li>
        </ul>
    </div>
    <div class="examContainer">
        <h3>SSC/Equivalent</h3>
        <table style="width: 100%">
            <tr>
                <td><b>Name of Examination</b></td>
                <td><b>Group</b></td>
                <td><b>Division/Grade</b></td>
                <td><b>GPA</b></td>
                <td><b>Out of</b></td>
                <td><b>Board</b></td>
                <td><b>Year of Passing</b></td>
                <td><b>Roll</b></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSecondaryExamName">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">SSC</asp:ListItem>
                    <asp:ListItem Value="">O Level</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSecondaryGroup">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">Science</asp:ListItem>
                    <asp:ListItem Value="">Commerce</asp:ListItem>
                    <asp:ListItem Value="">Humanities</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSecondaryGrade">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">First Division</asp:ListItem>
                    <asp:ListItem Value="">Second Division</asp:ListItem>
                    <asp:ListItem Value="">Third Division</asp:ListItem>
                    <asp:ListItem Value="">GPA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSecondaryGPA" class="inputText" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSecondaryOutOfGPA" class="inputText"/>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSecondaryBoard">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">Dhaka</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSecondaryYear">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSecondaryRoll" class="inputText" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtSecondaryExamName" class="inputText"/>
                </td>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtSecondaryExamGroup" class="inputText" />
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtSecondaryBoard" class="inputText" />
                </td>
            </tr>
          
            
        </table>
    </div>
    <div class="examContainer">
        <h3>HSC/Equivalent</h3>
        <table style="width: 100%">
            <tr>
                <td><b>Name of Examination</b></td>
                <td><b>Group</b></td>
                <td><b>Division/Grade</b></td>
                <td><b>GPA</b></td>
                <td><b>Out of</b></td>
                <td><b>Board</b></td>
                <td><b>Year of Passing</b></td>
                <td><b>Roll</b></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlHigherSecondaryExamName">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">HSC</asp:ListItem>
                    <asp:ListItem Value="">A Level</asp:ListItem>
                    <asp:ListItem Value="">Diploma</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlHigherSecondaryGroup">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">Science</asp:ListItem>
                    <asp:ListItem Value="">Commerce</asp:ListItem>
                    <asp:ListItem Value="">Humanities</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlHigherSecondaryGrade">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">First Division</asp:ListItem>
                    <asp:ListItem Value="">Second Division</asp:ListItem>
                    <asp:ListItem Value="">Third Division</asp:ListItem>
                    <asp:ListItem Value="">GPA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHigherSecondaryGPA" class="inputText" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHigherSecondaryOutOfGPA" class="inputText"/>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlHigherSecondaryBoard">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    <asp:ListItem Value="">Dhaka</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlHigherSecondaryYear">
                    <asp:ListItem Value="">Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHigherSecondaryRoll" class="inputText" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtHigherSecondaryExamName" class="inputText"/>
                </td>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtHigherSecondaryExamGroup" class="inputText" />
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <label>If other</label><br />
                    <asp:TextBox runat="server" ID="txtHigherSecondaryBoard" class="inputText" />
                </td>
            </tr>
        </table>
    </div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="panel1">
                <div class="examContainer">
                    <h3>Higher Education</h3>
                    <table style="width: 100%">
                        <tr>
                            <td><b>Name of Examination</b></td>
                            <td><b>Subject</b></td>
                            <td><b>Division/Class/Grade</b></td>
                            <td><b>CGPA</b></td>
                            <td><b>Out of</b></td>
                            <td><b>University</b></td>
                            <td><b>Year of Passing</b></td>
                            <td><b>Roll</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlGraduationExamName">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="">Undergraduate</asp:ListItem>
                                <asp:ListItem Value="">Graduate</asp:ListItem>
                                <asp:ListItem Value="">Post Graduate</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlGraduationSubject">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlGraduationGrade">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="">First Division</asp:ListItem>
                                <asp:ListItem Value="">Second Division</asp:ListItem>
                                <asp:ListItem Value="">Third Division</asp:ListItem>
                                <asp:ListItem Value="">CGPA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtCGPA" class="inputText" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtOutOfCGPA" class="inputText"/>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlUniversity">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="">Dhaka University</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlGraduationYear">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtGraduationRoll" class="inputText" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>If other</label><br />
                                <asp:TextBox runat="server" ID="txtGraduationExamName" class="inputText"/>
                            </td>
                            <td>
                                <label>If other</label><br />
                                <asp:TextBox runat="server" ID="txtGraduationSubject" class="inputText" />
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <label>If other</label><br />
                                <asp:TextBox runat="server" ID="txtUniversity" class="inputText" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAddMore1" runat="server" Text="Add More" OnClick="btnAddMore1_Click" />
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        

            <asp:Panel runat="server" ID="panel2">
                <div class="examContainer">
        
            <h3>Higher Education 2</h3>
            <table style="width: 100%">
                <tr>
                    <td><b>Name of Examination</b></td>
                    <td><b>Subject</b></td>
                    <td><b>Division/Class/Grade</b></td>
                    <td><b>CGPA</b></td>
                    <td><b>Out of</b></td>
                    <td><b>University</b></td>
                    <td><b>Year of Passing</b></td>
                    <td><b>Roll</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationExamName2">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">Undergraduate</asp:ListItem>
                        <asp:ListItem Value="">Graduate</asp:ListItem>
                        <asp:ListItem Value="">Post Graduate</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationSubject2">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationGrade2">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">First Division</asp:ListItem>
                        <asp:ListItem Value="">Second Division</asp:ListItem>
                        <asp:ListItem Value="">Third Division</asp:ListItem>
                        <asp:ListItem Value="">CGPA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCGPA2" class="inputText" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOutOfCGPA2" class="inputText"/>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlUniversity2">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">Dhaka University</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationYear2">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGraduationRoll2" class="inputText" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtGraduationExamName2" class="inputText"/>
                    </td>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtGraduationSubject2" class="inputText" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtUniversity2" class="inputText" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddMore2" runat="server" Text="Add More" OnClick="btnAddMore2_Click"/>
                        <asp:Button ID="btnCancel2" runat="server" Text="Cancel" OnClick="btnCancel2_Click" />

                    </td>
                </tr>
            </table>
        </div>
            </asp:Panel>
    
            <asp:Panel runat="server" ID="panel3">
                <div class="examContainer">
        
            <h3>Higher Education 3</h3>
            <table style="width: 100%">
                <tr>
                    <td><b>Name of Examination</b></td>
                    <td><b>Subject</b></td>
                    <td><b>Division/Class/Grade</b></td>
                    <td><b>CGPA</b></td>
                    <td><b>Out of</b></td>
                    <td><b>University</b></td>
                    <td><b>Year of Passing</b></td>
                    <td><b>Roll</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationExamName3">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">Undergraduate</asp:ListItem>
                        <asp:ListItem Value="">Graduate</asp:ListItem>
                        <asp:ListItem Value="">Post Graduate</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationSubject3">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationGrade3">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">First Division</asp:ListItem>
                        <asp:ListItem Value="">Second Division</asp:ListItem>
                        <asp:ListItem Value="">Third Division</asp:ListItem>
                        <asp:ListItem Value="">CGPA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCGPA3" class="inputText" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOutOfCGPA3" class="inputText"/>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlUniversity3">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        <asp:ListItem Value="">Dhaka University</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGraduationYear3">
                        <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGraduationRoll3" class="inputText" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtGraduationExamName3" class="inputText"/>
                    </td>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtGraduationSubject3" class="inputText" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <label>If other</label><br />
                        <asp:TextBox runat="server" ID="txtUniversity3" class="inputText" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddMore3" runat="server" Text="Add More" />
                        <asp:Button ID="btnCancel3" runat="server" Text="Cancel" OnClick="btnCancel3_Click" />

                    </td>
                </tr>
            </table>
        </div>
            </asp:Panel>
        </ContentTemplate>
        
    </asp:UpdatePanel>
    <asp:Button runat="server" ID="btnSubmit" class="btnSubmit" Text="Save All" OnClick="btnSubmit_Click" />
    
</asp:Content>
