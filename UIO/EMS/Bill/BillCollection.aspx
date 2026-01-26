
<%@ Page Title="Bill Collection" Language="C#" MasterPageFile="~/MasterPage/Bill/Bill.master" AutoEventWireup="true" CodeBehind="BillCollection.aspx.cs" Inherits="EMS.Bill.BillCollection" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    
.dxeButtonEdit
{
    background-color: white;
    border: solid 1px #9F9F9F;
    width: 170px;
}


.dxeButtonEdit
{
    background-color: white;
    border: solid 1px #9F9F9F;
    width: 170px;
}
.dxeButtonEdit .dxeEditArea {
    background-color: white;
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

.dxeEditArea 
{
	font-family: Tahoma;
	font-size: 9pt;
	border: 1px solid #A0A0A0;
}
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <div style="background-color: #C4ECFF">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                              <div style="border-style: groove; height:20px">
                                <div style="float: left; width: 15%"> <asp:Label ID="lbl" runat="server" 
                                        Text="Message:" Font-Bold="True" /></div>
                                  <div style="float: left; width: 80%"> 
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label> 
                                  </div>
                              </div>  
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="padding-top: 5px; padding-bottom: 5px; clear:both; border-right-style: groove; border-left-style: groove;">
                              <div style="height:20px ">
                                <div style="float:left; width:200px; height:20px " > <asp:Label ID="lblName" runat="server" Text="Roll:" /></div>
                                <div style="float:left; width:300px ; height:20px"><asp:TextBox ID="txtRoll" runat="server" Width="100%"/></div><div style="float:left;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="Required Field" ControlToValidate="txtRoll" ValidationGroup="SV">*</asp:RequiredFieldValidator></div>
                                <div style="float:left; width:100px; height:20px; padding-left: 10px; padding-right: 10px;">
                                    <asp:Button ID="btnShowName" runat="server" Text="Show Name" Height="20px" 
                                        onclick="btnShowName_Click" /></div>
                                <div style="float:left; width:50px; height:20px"><asp:Label ID="lblRoll" runat="server" Text="Name:" /></div>
                                <div style="float:left; width:300px; height:20px"><asp:TextBox ID="txtName" runat="server" Width="100%"/></div>
                              </div> 
                              
                    <asp:Panel ID="pnlContainer" runat="server">
                              <div style="height:20px ">
                                <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label1" runat="server" Text="Reference No:" /></div>
                                <div style="float:left; width:300px ; height:20px"><asp:TextBox ID="txtReferenceNo" runat="server" Width="100%"/></div>                                
                              </div>
                              <div style="height:20px ">
                                <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label3" runat="server" Text="Amount:" /></div>
                                <div style="float:left; width:300px ; height:20px"><asp:TextBox ID="txtAmount" runat="server" Width="100%"/></div> 
                                <div style="float:left;" >
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ErrorMessage="Required Field" ControlToValidate="txtAmount" 
                                        ValidationGroup="SV">*</asp:RequiredFieldValidator></div>                               
                              </div>
                              <div style="height:20px ">
                                <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label5" runat="server" Text="Bank Account:" /></div>
                                <div style="float:left; width:300px ; height:20px"><asp:DropDownList ID="ddlBankAcc" runat="server" Width="250px" Height="20px"/></div>                                
                              </div>
                             <div style="padding: 5px 0px 5px 0px; margin-top: 5px; border-top-style: groove; border-bottom-style: groove;">
                                  <div style="height:20px ">
                                    <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label2" runat="server" Text="Cheque No:" /></div>
                                    <div style="float:left; width:300px ; height:20px"><asp:TextBox ID="txtChequeNo" runat="server" Width="100%"/></div>                                
                                  </div>
                                  <div style="height:20px ">
                                    <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label4" runat="server" Text="Bank Name:" /></div>
                                    <div style="float:left; width:300px ; height:20px"><asp:TextBox ID="txtBankName" runat="server" Width="100%"/></div>                                
                                  </div>
                                  <div style="height:20px ">
                                    <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label6" runat="server" Text="Cheque Date:" /></div>
                                    <div style="float:left; width:300px ; height:20px">
                                        <dxe:ASPxDateEdit ID="dtpCheque" runat="server" Width="250px" Height="18px"></dxe:ASPxDateEdit>
                                      </div>                                
                                  </div>
                                  <div style="height:20px ">
                                    <div style="float:left; width:200px; height:20px " > <asp:Label ID="Label7" runat="server" Text="Remarks:" /></div>
                                    <div style="float:left; width:300px; height:20px">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="300px" />
                                    </div> 
                                  </div>
                              </div>
                       </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="border-right-style: groove; border-bottom-style: groove; border-left-style: groove">
                    <div style="padding:5px 5px 5px 5px; float:left">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="85px" 
                            onclick="btnSave_Click" ValidationGroup="SV" />
                    </div>
                    <div style="padding:5px 5px 5px 5px">
                        <asp:Button ID="btnCancle" runat="server" Text="Cancle" Width="85px" onclick="btnCancle_Click" 
                             />
                    </div>
                </div>                                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
