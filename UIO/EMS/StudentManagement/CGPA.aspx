<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentManagement_CGPA" Codebehind="CGPA.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <link href="../CSS/UIUMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StudentManagement/StdManagement.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function isNumber(e) {
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode;
            var status = charCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                alert("Please make sure entries are numbers only");

                return false;
            }

            return true;
        }     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="GPAFormContainter">
                <div id = "containerHeader">
                    <div id = "contentHeader"><label>GPA Calculation</label></div>
                    <div id = "errorMessageHolder">
                        <asp:Label runat= "server" ID = "lblErrorMessage" CssClass = "errorText" Text = "Error Creation test" Visible="false"></asp:Label>
                    </div>
                </div>
                <div id="inputControlsContainer">
                    <div class="DDLHolder">
                        <label>Select Batch..</label>
                        <asp:DropDownList runat = "server" ID = "ddlAcaCalenderBatch" Width ="170px" ></asp:DropDownList><br />
                    </div>
                    <div class="DDLHolder">
                    <label>Select Registration Trimester..</label>
                        <asp:DropDownList runat = "server" ID = "ddlAcaCalenderRegistration" Width ="170px" ></asp:DropDownList><br />
                    </div>
                    <div class="DDLHolder">
                    <label>Select Program</label>
                        <asp:DropDownList runat = "server" ID = "DDLProgram" Width ="170px" ></asp:DropDownList><br />
                    </div>
                    <div style="height:15px;"></div>
                    <div class="labelHolder">
                        <label for="txtstudentID">Student ID</label>
                    </div>
                    <div class="labelHolder">
                        <asp:TextBox runat = "server" ID = "txtstudentID"></asp:TextBox>
                    </div>
                    <asp:Button runat ="server" ID = "btnLoad" Text ="Load" CssClass="labelHolder" 
                        onclick="btnLoad_Click"/>
                </div>
                <%--<div>
                    
                    <asp:Button runat ="server" ID = "btnShowGPA" Text ="Show GPA"/>
                    <asp:Button runat ="server" ID = "btnShowCGPA" Text ="Show CGPA"/>
                </div>--%>
                <div id="StudentListForSelectionContainer">
                    <div class="SelectionListCaption">
                        <h3>Select Student to Calculate GPA and CGPA</h3>
                    </div>
                   <div class="selectionGridBody">
                    <asp:GridView ID="GridViewStudentList" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                         OnRowDataBound="GridStudentListBind" >
                        <Columns>
                            <asp:TemplateField ItemStyle-Width = "60px" HeaderText="Select">
	                                    <ItemTemplate>
	                                        <asp:CheckBox ID="chkSelected" runat="server"/>
	                                    </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width = "60px" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID = "lblStudentID" runat ="server" Text = '<%#Eval("StudentID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width = "60px" HeaderText="Student ID(Roll)">
                                <ItemTemplate>
                                    <asp:Label ID = "lblStudentRoll" runat ="server" Text = '<%#Eval("StudentRoll")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width = "160px" HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStudentName" Text = '<%#Eval("StudentName")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button runat="server" ID="btnShowGPA" Text="Show" OnClick="btnShowGPA_Click"/>
                   </div>
                    
                </div>
                <div class="gpaGridContainer">
                    <div class="GPAGridCaption">
                        <h3>GPA and CGPA of Students</h3>
                        
                    </div>
                    <div class = "gpaGridHeaderContainer">
                                <label class = "gpaGridHeader" style="width:70px;">Student ID</label>
                                <label class = "gpaGridHeader" style="width:180px;">Name</label>
                                <label class = "gpaGridHeader" style = "width:50px;">GPA</label>
                                <label class = "gpaGridHeader" style = "">Credits trimester</label>
                                <label class="gpaGridHeader" style="width:50px;">CGPA</label>
                                <label class = "gpaGridHeader" style = "">Total Credits</label>
                    </div>
                    <div class="gpaGridBody">
                        <asp:GridView ID="GridViewGPA" runat="server" AutoGenerateColumns = "false" ShowHeader ="false"
                                OnRowDataBound ="Grid_Student_GPA_Bind" Height="564px" Width="897px" >
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width = "60px">
	                                    <ItemTemplate><%#Eval("")%></ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            
                        </asp:GridView>
                    </div>
                </div>
                
            </div>
            
            
            
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>