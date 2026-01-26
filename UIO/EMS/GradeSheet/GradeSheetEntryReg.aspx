<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeSheetEntryReg.aspx.cs"
    Inherits="EMS.GradeSheet.GradeSheetEntryReg" MasterPageFile="~/MasterPage/GradeSheet/GradeSheet.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
        .dxeButtonEdit .dxeEditArea
        {
            background-color: white;
        }
        .dxeEditArea
        {
            font-family: Tahoma;
            font-size: 9pt;
            border: 1px solid #A0A0A0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" runat="Server">
    <div>
    </div>
    <div style="height: 25px; border-top-style: groove; border-right-style: groove; border-left-style: groove;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Text="Message: "></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove; height: 80px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Academic Calender" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlAcaCalender" runat="server" Height="20px" Width="150px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left;">
                    <div style="text-align: left;">
                        <asp:Label ID="Label2" runat="server" Text="Program" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="20px" Width="150px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Course & Section" Height="20px" Width="245px"
                            Font-Size="Small"></asp:Label>
                        &nbsp;<br />
                        <asp:DropDownList ID="ddlCourse" runat="server" Height="20px" Width="350px" AutoPostBack="True">
                        </asp:DropDownList>
                        <br />
                        <asp:Button ID="btnLoad" runat="server" Text="Load..." OnClick="btnLoad_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                `
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="float: left; width: 100%; height: 300px; overflow: auto;">
                    <div style="float: left; width: 100%; height: 300px; overflow: auto; border-right-style: groove;">
                        <asp:GridView ID="gvGradesheet" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="gvGradesheet_RowDataBound" 
                            onselectedindexchanged="gvGradesheet_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:TemplateField HeaderText="ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtId" runat="server" Text='<%# Bind("Id") %>' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StudentId">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStudentID" runat="server" Text='<%# Bind("StudentID") %>' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roll">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Roll") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRoll" runat="server" Text='<%# Bind("Roll") %>' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Marks">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("TotalMarks") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalMarks" runat="server" Text='<%# Bind("TotalMarks") %>' Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-----------------------------------------------my---------------------------------------%>
                                <asp:TemplateField HeaderText="Grade 1">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("GradeId1") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade1" runat="server" Text='<%# Bind("GradeId11") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade1" runat="server" Text='<%# Bind("GradeId1") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect1" runat="server" OnCheckedChanged="chkSelect1_CheckedChanged" AutoPostBack="true" BorderColor="Black" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                               <asp:TemplateField HeaderText="Grade 2">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("GradeId2") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade2" runat="server" Text='<%# Bind("GradeId21") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade2" runat="server" Text='<%# Bind("GradeId2") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect2" runat="server"  Checked="false" AutoPostBack="true" BorderColor="Black" OnCheckedChanged="chkSelect2_CheckedChanged"/> 
                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade 3">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("GradeId3") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade3" runat="server" Text='<%# Bind("GradeId31") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade3" runat="server" Text='<%# Bind("GradeId3") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect3" runat="server"  Checked="false" AutoPostBack="true" BorderColor="Black" OnCheckedChanged="chkSelect3_CheckedChanged"/> 
                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade 4">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("GradeId4") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade4" runat="server" Text='<%# Bind("GradeId41") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade4" runat="server" Text='<%# Bind("GradeId4") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox4" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect4" runat="server"  Checked="false" AutoPostBack="true" BorderColor="Black" OnCheckedChanged="chkSelect4_CheckedChanged"/> 
                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade 5">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("GradeId5") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade5" runat="server" Text='<%# Bind("GradeId51") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade5" runat="server" Text='<%# Bind("GradeId5") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox5" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect5" runat="server"  Checked="false" AutoPostBack="true" BorderColor="Black" OnCheckedChanged="chkSelect5_CheckedChanged"/> 
                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade 6">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("GradeId6") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade6" runat="server" Text='<%# Bind("GradeId61") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade6" runat="server" Text='<%# Bind("GradeId6") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox6" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect6" runat="server"  Checked="false" AutoPostBack="true" BorderColor="Black" OnCheckedChanged="chkSelect6_CheckedChanged"/> 
                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <%---------------------------------------------my------------------------------------------%>
                                
                                <asp:TemplateField HeaderText="Grade">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("GradeId1") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrade" runat="server" Text='<%# Bind("GradeId11") %>' Enabled="False" Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblGrade" runat="server" Text='<%# Bind("GradeId1") %>' Enabled="False" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList Width="80" runat="server" ID="ddlGrade" AutoPostBack="true" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#00CC99" />
                            <HeaderStyle BackColor="#669999" />
                        </asp:GridView>
                    </div>
                  <%--  <div style="float: left; width: 35%; height: 300px; overflow: auto;">
                        
                        <div style="padding-bottom: 5px; padding-top: 5px">
                            <asp:Button ID="btnConflictResult" runat="server" Text="Show Conflict Result" 
                                onclick="btnConflictResult_Click" />
                                &nbsp
                                <asp:Button ID="btnConsiderGpa" runat="server" Text="Considered GPA" 
                                onclick="btnConsiderGpa_Click" />
                        </div>
                        
                        <div style="height: 150px">
                            <asp:GridView ID="gvConflictedResult" runat="server" 
                                AutoGenerateColumns="False" 
                                onselectedindexchanged="gvConflictedResult_SelectedIndexChanged" Visible="false">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblConflictId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Session">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSession" runat="server" Text='<%# Bind("Session") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSession" runat="server" Text='<%# Bind("Session") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sectionname") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Sectionname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Marks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Obtainedtotalmarks") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Obtainedtotalmarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Grade") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsConsiderGPA") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" 
                                                Checked='<%# Bind("IsConsiderGPA") %>' Enabled="false" BorderColor="Black"
                                                 />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#009999" />
                            </asp:GridView>
                        </div>
                        <div style="padding-top: 10px">
                            
                        </div>
                      </div>
                  --%>  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove; clear: both">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Height="20px" Width="90px"
                        OnClick="btnUpdate_Click" />&nbsp;
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Height="20px" Width="90px"
                        OnClick="btnTransfer_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="20px" Width="90px"
                        OnClick="btnCancel_Click" />&nbsp;
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
    </div>
</asp:Content>
