<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiblingSetup.aspx.cs" Inherits="EMS.SiblingSetup"
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
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="Message-Area" style="height:25px;">
                    <div style="float: left; width: 100px">
                        <asp:Label ID="lbl" runat="server" Text="Message:" Font-Bold="True" /></div>
                    <div style="float: left; width: 1000px">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="width: 1200px; height: 400px; float: left;">
            <asp:Panel ID="pnlControl" runat="server">
                <div style="width: 400px; float: left; ">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div style="border: thin groove #0000FF">
                                <div style="padding: 5px; height: 20px">
                                    <div style="float: left; width: 100px; height: 20px">
                                        <asp:Label ID="Label3" runat="server" Text="Applicant's Roll:" /></div>
                                    <div style="float: left; width: 180px; height: 20px">
                                        <asp:TextBox ID="txtApplicantRoll" runat="server" Width="100%" /></div>
                                    <div style="float: left; width: 10px; height: 20px; padding-left: 5px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ErrorMessage="Required Field" ControlToValidate="txtApplicantRoll" 
                                            ValidationGroup="LD">*</asp:RequiredFieldValidator></div>
                                    <div style="float: left; width: 70px; height: 20px; padding-left: 10px;">
                                        <asp:Button ID="btnLoad" runat="server" Text="Load..." Height="20px" OnClick="btnLoad_Click"
                                            Width="70px" ValidationGroup="LD" /></div>
                                </div>
                                <div style="padding: 5px; height: 20px;">
                                    <div style="float: left; width: 80px; height: 20px; padding-left: 10px; padding-right: 10px;">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" Height="20px" Width="70px"
                                            OnClick="btnAddNew_Click" /></div>
                                    <div style="float: left; width: 80px; height: 20px; padding-left: 10px; padding-right: 10px;">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Height="20px" Width="70px" OnClick="btnAdd_Click" /></div>
                                    <div style="float: left; width: 80px; height: 20px; padding-left: 10px; padding-right: 10px;">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Height="20px" Width="70px"
                                            OnClick="btnDelete_Click" /></div>
                                </div>
                            </div>
                            <div style=" height: 200px;  border: thin groove #0000FF">
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" 
                                    AutoGenerateSelectButton="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SiblingSetupId" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SiblingSetupId") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSiblingSetupId" runat="server" Text='<%# Bind("SiblingSetupId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GroupID" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GroupID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupID" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ApplicantId" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ApplicantId") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplicantId" runat="server" Text='<%# Bind("ApplicantId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Roll") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoll" runat="server" Text='<%# Bind("Roll") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#CCFFFF" />
                                    <HeaderStyle BackColor="#9999FF" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <div style="float: left; width: 795px; ">
                <asp:Panel ID="pnlInput" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="border: thin groove #0000FF; height:85px;">
                                <div style="padding: 5px; height: 20px">
                                    <div style="float: left; width: 130px; height: 20px">
                                        <asp:Label ID="lblName" runat="server" Text="1st Applicant's Roll:" /></div>
                                    <div style="float: left; width: 200px; height: 20px">
                                        <asp:TextBox ID="txtRoll1Applicant" runat="server" Width="100%" /></div>
                                    <div style="float: left;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field"
                                            ControlToValidate="txtRoll1Applicant" ValidationGroup="SV">*</asp:RequiredFieldValidator></div>
                                    <div style="float: left; width: 100px; height: 20px; padding-left: 10px; padding-right: 10px;">
                                        <asp:Button ID="btnShowName1" runat="server" Text="Show Name" Height="20px" OnClick="btnShowName1_Click" /></div>
                                    <div style="float: left; width: 50px; height: 20px">
                                        <asp:Label ID="lblRoll" runat="server" Text="Name:" /></div>
                                    <div style="float: left; width: 250px; height: 20px">
                                        <asp:TextBox ID="txtName1Applicant" runat="server" Width="100%" /></div>
                                </div>
                                <div style="padding: 5px; height: 20px">
                                    <div style="float: left; width: 130px; height: 20px">
                                        <asp:Label ID="Label1" runat="server" Text="2nd Applicant's Roll:" /></div>
                                    <div style="float: left; width: 200px; height: 20px">
                                        <asp:TextBox ID="txtRoll2Applicant" runat="server" Width="100%" /></div>
                                    <div style="float: left;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field"
                                            ControlToValidate="txtRoll2Applicant" ValidationGroup="SV">*</asp:RequiredFieldValidator></div>
                                    <div style="float: left; width: 100px; height: 20px; padding-left: 10px; padding-right: 10px;">
                                        <asp:Button ID="btnShowName2" runat="server" Text="Show Name" Height="20px" OnClick="btnShowName2_Click" /></div>
                                    <div style="float: left; width: 50px; height: 20px">
                                        <asp:Label ID="Label2" runat="server" Text="Name:" /></div>
                                    <div style="float: left; width: 250px; height: 20px">
                                        <asp:TextBox ID="txtName2Applicant" runat="server" Width="100%" /></div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                           <div style="border: thin groove #0000FF; height:40px;">
                                <div style="padding: 5px 5px 5px 5px; float: left">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="85px" OnClick="btnSave_Click"
                                        ValidationGroup="SV" />
                                </div>
                                <div style="padding: 5px 5px 5px 5px">
                                    <asp:Button ID="btnCancle" runat="server" Text="Cancle" Width="85px" OnClick="btnCancle_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
        <div style="width: 1200px; clear: both; border-right-style: groove; border-bottom-style: groove;
            border-left-style: groove; border-top-style: groove">
        </div>
    </div>
</asp:Content>
