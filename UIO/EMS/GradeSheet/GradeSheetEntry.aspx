<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeSheetEntry.aspx.cs"
    Inherits="EMS.GradeSheet.GradeSheetEntry" MasterPageFile="~/MasterPage/GradeSheet/GradeSheet.Master" %>

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
                            OnSelectedIndexChanged="ddlAcaCalender_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left;">
                    <div style="text-align: left;">
                        <asp:Label ID="Label2" runat="server" Text="Program" Height="20px" Width="150px"
                            Font-Size="Small"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="20px" Width="150px" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label4" runat="server" Text="Course & Section" Height="20px" Width="246px"
                            Font-Size="Small"></asp:Label>
                        &nbsp;<br />
                        <asp:DropDownList ID="ddlCourse" runat="server" Height="20px" Width="250px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnLoad" runat="server" Text="Load..." OnClick="btnLoad_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImport" />
            </Triggers>
            <ContentTemplate>
                <div style="padding: 5px; margin: 5px; float: left">
                    <div style="text-align: left">
                        <asp:Label ID="Label3" runat="server" Text="Please select a Excel sheet to import"
                            Height="20px" Width="251px" Font-Size="Small"></asp:Label>
                        &nbsp;<br />
                        <asp:FileUpload ID="FileUploadExcel" runat="server" />
                        &nbsp;
                        <asp:Button ID="btnImport" runat="server" Text="Import..." OnClick="btnImport_Click" />
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
                    <div style="float: left; width: 48%; height: 300px; overflow: auto; border-right-style: groove;">
                        <asp:GridView ID="gvShowGradeSheet" runat="server" Width="545px" 
                            AutoGenerateColumns="False" >
                            <Columns>
                                <asp:BoundField DataField="StudentID" HeaderText="StudentId" />
                                <asp:BoundField DataField="Roll" HeaderText="Roll">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Name">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalMarks" HeaderText="Total Marks">
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Grade" Visible="true" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Grade") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblObtainGrade" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:DropDownList Width="80" runat="server" id="ddlGrade" AutoPostBack="true" Enabled="False"
                                            onselectedindexchanged="ddlGrade_SelectedIndexChanged"></asp:DropDownList> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle BackColor="#669999" />
                            <AlternatingRowStyle BackColor="#60CA95" />
                        </asp:GridView>
                    </div>
                    <div style="float: right; width: 48%; height: 300px; overflow: auto;">
                        <asp:GridView ID="gvImportGradesheet" runat="server" 
                            AutoGenerateColumns="False" >
                            <Columns>
                                <asp:TemplateField HeaderText="StudentId" Visible="False">
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
                                <asp:TemplateField HeaderText="Grade" Visible="true">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Grade") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server" Text='<%# Bind("Grade") %>' Enabled="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Grade">                                    
                                    <ItemTemplate>
                                        <asp:DropDownList Width="80" runat="server" id="ddlGradeImp" AutoPostBack="true"  Enabled="False"
                                            onselectedindexchanged="ddlGradeImp_SelectedIndexChanged"></asp:DropDownList> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <SelectedRowStyle BackColor="#00CC99" />
                            <HeaderStyle BackColor="#669999" />
                            <AlternatingRowStyle BackColor="#60CA95" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="border-style: groove; clear: both">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="20px" Width="90px" OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Height="20px" Width="90px" OnClick="btnEdit_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
    </div>
</asp:Content>
