<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DiscountWorksheet.aspx.cs" Inherits="EMS.StudentManagement.DiscountWorksheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">


.dxeButtonEdit
{
    background-color: white;
    border: solid 1px #9F9F9F;
    width: 170px;
}
.dxeButtonEdit .dxeEditArea {
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">    

<div></div>
<div>
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
                    <asp:Label ID="Label1" runat="server" Text="Academic Calender" Height="20px" 
                        Width="150px" Font-Size="Small"></asp:Label> <br />
                    <asp:DropDownList ID="ddlAcaCalender" runat="server" Height="20px" 
                        Width="150px"></asp:DropDownList>
                </div>
            </div>
            <div style="padding: 5px; margin: 5px; float: left">
                <div style="text-align: left">    
                    <asp:Label ID="Label2" runat="server" Text="Program" Height="20px" 
                        Width="150px" Font-Size="Small"></asp:Label> <br />
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="20px" 
                        Width="150px" AutoPostBack="True" 
                        onselectedindexchanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div style="padding: 5px; margin: 5px; float: left">
                <div style="text-align: left">    
                    <asp:Label ID="Label3" runat="server" Text="Student" Height="20px" 
                        Width="150px" Font-Size="Small"></asp:Label> <br />
                    <asp:DropDownList ID="ddlStudent" runat="server" Height="20px" 
                        Width="150px" AutoPostBack="True" 
                        onselectedindexchanged="ddlStudent_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div style="padding: 5px; margin: 5px; float: left">
                <div style="text-align: left">    
                    <asp:Label ID="Label4" runat="server" Text="Course" Height="20px" Width="100px" Font-Size="Small"></asp:Label> <br />
                    <asp:DropDownList ID="ddlCourse" runat="server" Height="20px" Width="250px" 
                        AutoPostBack="True" onselectedindexchanged="ddlCourse_SelectedIndexChanged"  
                        ></asp:DropDownList> &nbsp;
                      <asp:Button ID="btnLoad" runat="server"  Text="Load ..." onclick="btnLoad_Click" 
                         />
                </div>
            </div>             
        </ContentTemplate>
    </asp:UpdatePanel>
</div>  

<div style="border-style: groove; ">
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
    <ContentTemplate>
    
        `
    </ContentTemplate>
    </asp:UpdatePanel>
</div>  
    
<div style="clear:both" >
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div style="float:left;  height:300px">
            <asp:GridView ID="gvDiscountWorksheet" runat="server" AutoGenerateColumns="False" >               
                
                <SelectedRowStyle BackColor="#00CC99" />
                <HeaderStyle BackColor="#669999" />
            </asp:GridView>
        </div>
         </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div style="border-style: groove; clear:both">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save"  
                Height="20px" Width="90px" onclick="btnSave_Click" /> &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"  
                Height="20px" Width="90px" onclick="btnCancel_Click" />
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>   
</div>

<div></div>

</asp:Content>