<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeDetails.aspx.cs" Inherits="EMS.BasicSetup.GradeDetails"
    MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        .dxeButtonEdit .dxeEditArea
        {
            background-color: white;
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
        .dxeEditArea
        {
            font-family: Tahoma;
            font-size: 9pt;
            border: 1px solid #A0A0A0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <div class="Message-Area" style="width:1100px; height:25px;">
                    <div style="float: left; width: 100px">
                        <asp:Label ID="lbl" runat="server" Text="Message:" Font-Bold="True" /></div>
                    <div style="float: left; width: 1000px">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
            <div id="divMaster" runat="server">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                         <div class="Message-Area" style="width:1100px; height:100px;">
                            <div id="divSelection" runat="server" style="padding: 5px; height: 20px">
                                <div style="float: left; width: 150px; height: 20px">
                                    <asp:Label ID="Label3" runat="server" Text="Academic Calender" /></div>
                                <div style="float: left; width: 180px; height: 20px">
                                    <asp:DropDownList ID="cboAcaCalender" runat="server" Height="25px" TabIndex="1" Width="171px">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 100px; height: 20px">
                                    <asp:Label ID="Label1" runat="server" Text="Program" /></div>
                                <div style="float: left; width: 180px; height: 20px">
                                    <asp:DropDownList ID="cboProgram" runat="server" Height="25px" TabIndex="2" Width="171px">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 70px; height: 20px; padding-left: 10px;">
                                    <asp:Button ID="btnLoad" runat="server" Text="Load..." Height="20px" Width="70px"
                                        OnClick="btnLoad_Click" /></div>
                            </div>
                            <hr />
                            <div id="divInput" runat="server" style="padding: 5px; height: 25px;">
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 40px;">
                                    <asp:Label ID="Label4" runat="server" Text="Grade"></asp:Label>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 70px;">
                                    <asp:TextBox ID="txtGrade" runat="server" Width="70px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 2px; padding-right: 2px; width: 5px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="Required Field" ControlToValidate="txtGrade" 
                                        ValidationGroup="vgAdd">*</asp:RequiredFieldValidator></div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 60px;">
                                    <asp:Label ID="Label5" runat="server" Text="Grade Point"></asp:Label>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 70px;">
                                    <asp:TextBox ID="txtGradePoint" runat="server" Width="70px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 2px; padding-right: 2px; width: 5px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtGradePoint" ErrorMessage="Required Field" 
                                        ValidationGroup="vgAdd">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 60px;">
                                    <asp:Label ID="Label6" runat="server" Text="Min Marks"></asp:Label>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 70px;">
                                    <asp:TextBox ID="txtMinMarks" runat="server" Width="70px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 2px; padding-right: 2px; width: 5px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtMinMarks" ErrorMessage="Required Field" 
                                        ValidationGroup="vgAdd">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 60px;">
                                    <asp:Label ID="Label2" runat="server" Text="Max Marks"></asp:Label>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 70px;">
                                    <asp:TextBox ID="txtMaxMarks" runat="server" Width="70px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 2px; padding-right: 2px; width: 5px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="txtMaxMarks" ErrorMessage="Required Field" 
                                        ValidationGroup="vgAdd">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 80px;">
                                    <asp:Label ID="Label7" runat="server" Text="Retake Discount"></asp:Label>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 5px; padding-right: 5px; width: 70px;">
                                    <asp:TextBox ID="txtRetakeDiscount" runat="server" Width="70px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 20px; padding-left: 2px; padding-right: 2px; width: 5px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtRetakeDiscount" ErrorMessage="Required Field" 
                                        ValidationGroup="vgAdd">*</asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; width: 80px; height: 20px; padding-left: 5px; padding-right: 5px;">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Height="20px" Width="70px" 
                                        OnClick="btnAdd_Click" ValidationGroup="vgAdd" /></div>
                            </div>
                        </div>
                        <div >
                            <div style="padding:5px; margin:5px">
                            <asp:GridView ID="gvGradeDetails" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvGradeDetails_RowCancelingEdit"
                                OnRowDataBound="gvGradeDetails_RowDataBound" OnRowDeleting="gvGradeDetails_RowDeleting"
                                OnRowEditing="gvGradeDetails_RowEditing" OnRowUpdated="gvGradeDetails_RowUpdated"
                                OnRowUpdating="gvGradeDetails_RowUpdating"  
                                HeaderStyle-BackColor="#a3afbc" HeaderStyle-ForeColor="White" HeaderStyle-Height="30" CellPadding="5" Width="1000">
                                <AlternatingRowStyle BackColor="#F9F9F9" />
                                <Columns>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                    <asp:BoundField DataField="grade" HeaderText="Grade" />
                                    <asp:BoundField DataField="gradePoint" HeaderText="Grade point" />
                                    <asp:BoundField DataField="minMarks" HeaderText="Min Marks" />
                                    <asp:BoundField DataField="maxMarks" HeaderText="Max Marks" />
                                    <asp:BoundField DataField="retakeDiscount" HeaderText="Retake Discount" />
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                </Columns>
                                <HeaderStyle BackColor="#99CCFF" />
                            </asp:GridView>
                            </div>
                            <div style="height: 20px; padding-left: 5px; padding-right: 5px;">
                                <div style="float: left; width: 80px; height: 20px; padding-left: 5px; padding-right: 5px;">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="20px" Width="70px" OnClick="btnSave_Click" /></div>
                                <div style="float: left; width: 80px; height: 20px; padding-left: 5px; padding-right: 5px;">
                                    <asp:Button ID="btnCancle" runat="server" Text="Cancel" Height="20px" Width="70px"
                                        OnClick="btnCancle_Click" /></div>
                            </div>
                        </div>
                        <div class="cleaner"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
          
             
       
       </div>
    </div>
     
</asp:Content>
