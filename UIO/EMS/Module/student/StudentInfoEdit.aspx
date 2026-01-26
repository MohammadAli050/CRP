<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_StudentInfoEdit" Codebehind="StudentInfoEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Student Info :: Edit</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../Content/CSSFiles/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Student Information :: Edit</label>
        </div>

        <div class="Message-Area">
            <label class="msgTitle">Message: </label>
            <asp:Label runat="server" ID="lblMsg" Text="" />
        </div>

        <div class="StudentInformationEdit-container">
            <div class="div-margin">
                <div class="loadArea">
                    <label class="display-inline field-Title">Student ID</label>
                    <asp:TextBox runat="server" ID="txtStudentID" class="margin-zero input-Size" placeholder="Student ID" />
                    <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" class="button-margin btn-size" OnClientClick="return CheckField();" />
                </div>
                <div class="loadedArea"></div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Name</label>
                    <asp:TextBox runat="server" ID="txtName" class="margin-zero input-Size" placeholder="Name" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Father's Name</label>
                    <asp:TextBox runat="server" ID="txtFatherName" class="margin-zero input-Size" placeholder="Father's Name" />

                    <label class="display-inline field-Title2">Mother's Name</label>
                    <asp:TextBox runat="server" ID="txtMotherName" class="margin-zero input-Size" placeholder="Mother's Name" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Date Of Birth</label>
                    <asp:TextBox runat="server" ID="txtDOB" class="margin-zero input-Size" placeholder="Date Of Birth" />
                    <ajaxToolkit:CalendarExtender ID="reqDOB" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy" />

                    <label class="display-inline field-Title2">Gender</label>
                   <%-- <asp:DropDownList runat="server" ID="ddlGender" class="margin-zero dropDownList" />--%>
                    <asp:TextBox runat="server" ID="txtGender" class="margin-zero input-Size" placeholder="Gender" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title">Contact No</label>
                    <asp:TextBox runat="server" ID="txtContact" class="margin-zero input-Size" placeholder="Mobile/Phone" />

                    <label class="display-inline field-Title2">Email</label>
                    <asp:TextBox runat="server" ID="txtEmail" class="margin-zero input-Size" placeholder="Email Address" />
                </div>
                <div class="loadedArea">
                    <label class="display-inline field-Title5">Address</label>
                </div>
                <div class="loadedArea">
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:GridView ID="GridView1" runat="server" OnRowEditing="GridView1_RowEditing">
                            <Columns>
                            
                            <asp:CommandField ShowEditButton="True" />
                                                        
                            </Columns>
                        </asp:GridView>
                         
                    </asp:Panel>
                </div>
                <%--<div class="loadedArea">
                    <label class="display-inline field-Title3">Guardian</label>
                    <asp:TextBox runat="server" ID="txtGuardianaddress" class="margin-zero input-Size1" TextMode="MultiLine" Rows="3" />

                    <label class="display-inline field-Title4">Mailing</label>
                    <asp:TextBox runat="server" ID="txtMailingAddress" class="margin-zero input-Size1" TextMode="MultiLine" Rows="3" />
                </div>--%>
                <div class="loadedArea">
                    <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" class="button-margin btn-size1" OnClientClick="return CheckField();" />
                </div>
            </div>
        </div>
    </div>

    
    
</asp:Content>

