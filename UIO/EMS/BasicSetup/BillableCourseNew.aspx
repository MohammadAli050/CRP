<%@ Page Title="Billable Course Setup" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="BillableCourseNew.aspx.cs" Inherits="EMS.BasicSetup_BillableCourseNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/BasicSetUp/BasicSetUp.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Bill/Bill.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            border: 1px solid Gray;
            width: 485px;
         }
        .style3
        {
            height: 10px;
        }
        .style6
        {
            height: 300px;
        }
        
        </style>
        <script type="text/javascript">
            function isNumber(e) {
                var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode;
                status = charCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                    alert("Please make sure entries are numbers only");

                    return false;
                }

                return true;
            }     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--This Div is the parrent div for the clild page(i.e. content part of the Master Page)--%>
            <div id = "containter">
                <div id = "containerHeader">
                    <div id = "contentHeader"><label>Billable Courses</label></div>
                    <div id = "errorMessageHolder">
                        <asp:Label runat= "server" ID = "lblErrorMessage" CssClass = "errorText" Text = "Error Creation test" Visible="false"></asp:Label>
                    </div>
                </div>
                <div id = "containerBody">
                   <%--This div will hold the crud buttons and the gird to hold information--%>
                   <div id = "contentBodyLeft">
                        <div class = "verticalSpace">
                        </div>
                        
                        <%--This Div will hold a textbox and a find button to search--%>
                        <div class = "search">
                            <asp:TextBox runat = "server" ID = "txtSearch" Width="220px" Height = "20px"></asp:TextBox>
                            <asp:Button runat = "server" ID = "btnFind" Text = "Find" CssClass = "buttonUserControl"
                             ToolTip="Enter a search parameter (course code or title) and click this button to find that course."
                             TabIndex = "1" onclick="btnFind_Click" />
                            <asp:Button runat="server" ID="btnNotSetBillableCourses" Text="Courses" 
                                Height="22px" Width="80px" OnClick="btnNotSetBillableCourses_Click"/>
                        </div>
                            
                        <div class = "buttons">
                            <asp:Button runat = "server" ID = "btnAdd" Text = "Add New" 
                                CssClass = "buttonUserControl" TabIndex = "2"/>
                            
                            <asp:Button runat = "server" ID = "btnEdit" Text = "Edit" 
                                CssClass = "buttonUserControl" TabIndex = "3" OnClick = "btnEdit_Click"/>
                            
                            <asp:Button runat = "server" ID = "btnDelete" Text = "Delete" 
                                CssClass = "buttonUserControl" TabIndex = "4"/>
                        </div>
                        
                        <div class = "verticalSpace"></div>
                        <%--This div will hold the gridview --%>
                        <div class = "gridContainer">
                            <%--This Div wil hold the Grid Header--%>
                            <div class = "gridHeader">
                                <label class = "LabeForGridHeader" style = "width:38px;"></label>
                                <label class = "LabeForGridHeader">Code</label>
                                <label class = "LabeForGridHeader" style = "width:180px;">Title</label>
                                <label class = "LabeForGridHeader" style = "width:60px;">Credits</label>
                                <label class="LabeForGridHeader" style="width:70px;">IsCredit?</label>
                            </div>
                            
                            <%--This Div will hold the Grid Body--%>
                            <div class = "gridBody">
                                <asp:GridView runat = "server" ID = "gridViewCourses"
                                 AutoGenerateColumns="False" BackColor="White" BorderColor="#006666"  
                                 RowStyleForeColor="#000066" Height="41px" AutoGenerateSelectButton="True" 
                                    Width="459px">
                                 <Columns>
                                    <asp:TemplateField HeaderText = "CourseID" Visible = "false">
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblID" Text = '<%#Eval("ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "CourseID" Visible = "false">
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblCourseID" Text = '<%#Eval("CourseID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText = "VersionID" Visible = "false">
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblVersionID" Text = '<%#Eval("VersionID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText = "Course Code" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblCourseCode" Text = '<%#Eval("FormalCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText = "Course Title" ItemStyle-Width="209px">
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblCourseTitle" Text = '<%#Eval("Title")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText = "Credits" >
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblCourseCredit" Text = '<%#Eval("Credits")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText = "IsCredit" >
                                        <ItemTemplate>
                                            <asp:Label runat = "server" ID = "lblIsCreditCourse" Text = '<%#Eval("IsCreditCourse")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                 <FooterStyle BackColor="White" ForeColor="#000066" />
                                 <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                 <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" Height="24px" />
                                </asp:GridView>
                            </div>
                            
                        </div>
                   </div>
                   <%--This Div will hold the UI for --%>
                   <div id = "contentBodyRight" runat = "server">
                        <div class = "verticalSpace"></div>
                        <div class = "labelsContainer">
                            <lable class = "labelForUI" for = "txtCourseName">Name</lable>
                            <div class = "verticalSpace"></div>
                            <lable class = "labelForUI" for = "txtCourseCode">Code</lable>
                            <div class = "verticalSpace"></div>  
                            <lable class = "labelForUI" for = "txtCourseVersion">Version</lable><br /><br />
                            <lable class = "labelForUI" for = "DDLProgram">Program</lable><br /><br />
                            <lable class = "labelForUI" for = "DDLBatch">Academic Calendar</lable><br /><br />
                            <lable class = "labelForUI" for = "txtRetakeNumber">Bill Start from ReTake#</lable><br />  
                            
                        </div>
                        
                        <div class = "inputControlsContainer">
                            <asp:TextBox runat = "server" ID = "txtCourseName" Enabled ="false" Width="180px"  ></asp:TextBox><br />
                            <div class = "verticalSpace"></div>
                            <asp:TextBox ID="txtCourseCode" runat = "server" Enabled ="false" ></asp:TextBox><br />
                            <div class = "verticalSpace"></div>
                            <asp:TextBox ID="txtCourseVersion" runat = "server" Enabled ="false" ></asp:TextBox><br />
                            <div class = "tinyVerticalSpace"></div>
                            <asp:DropDownList runat ="server" ID = "DDLProgram" Width ="170px" ></asp:DropDownList><br /><br />
                            <asp:DropDownList runat = "server" ID = "ddlAcaCalender" Width ="170px" ></asp:DropDownList><br />
                            <div class = "verticalSpace"></div>
                            <asp:TextBox ID="txtRetakeNumber" runat = "server"></asp:TextBox>
                        </div>
                        <div class ="validationMessageContainer">
                            <%--<asp:RequiredFieldValidator ID="RequiredRetakeNumber" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class = "buttons" style = "float:left;">
                            <asp:Button ID = "btnSave" runat = "server" CssClass = "buttonUserControl" Text = "Save"
                             OnClick = "btnSave_Click"/>
                            <asp:Button ID = "btnCancel" runat = "server" CssClass = "buttonUserControl" Text = "Cancel"
                             OnClick = "btnCancel_Click"/>
                        </div>
                        
                   </div>
                </div>
                
                
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>