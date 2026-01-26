<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="SyllabusMan_CourseCreator" Codebehind="CourseCreator.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register assembly="DevExpress.Web.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
         table {
            border-collapse: collapse;
        }
        .style1
        {
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
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode
            status = charCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                alert("Please make sure entries are numbers only")
                
                return false
            }

            return true;
        }     
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:1100px; height:510px;">
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px;width: 100%; height:25px;">
                        <table style="width:100%; height:24px;">
							<tr>
								<td class="td" style="width: 85px">
								<asp:label ID="lblHeader0" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="120px">Courses</asp:label>
								</td>
								<td class="td" style="width: 500px;">
								<asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="741px"></asp:label>
								</td>
							</tr>
						</table>
                    </td>
                </tr>
                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                    <td  align="left" valign="top">
                        <asp:Panel ID="pnlCourses" runat="server" BorderColor="White" BorderStyle="None"
                                    BorderWidth="1px" Height="98%" Width="100%">                                  
                            <table style="border: 1px solid Gray; width: 100%; height: 100%; ">
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                    <td colspan ="4" align="left" valign="top" style="height:35px;">
                                        <table>
                                            <tr>
                                                <td >
                                                    <asp:TextBox ID="txtSrch" runat="server" Width="350px" TabIndex="1"></asp:TextBox>
                                                </td>
                                                <td align="center" >
                                                    <asp:Button ID="butFind" runat="server" CssClass="button" 
                                                        onclick="butFind_Click" TabIndex="2" Text="Find" 
                                                        ToolTip="Enter a search parameter (course code or title) and click this button to find that course." />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                    <td align="center" valign="top" class="style3">
                                        <asp:Button ID="btnAdd" runat="server"  
                                            onclick="btnAdd_Click" Text="Add Course" CssClass="button" 
                                            ToolTip="Click this button if you want to add a new course." 
                                            TabIndex="3" />
                                    </td>
                                    <td align="center" valign="top" class="style3">
                                        <asp:Button ID="btnVersion" runat="server" Text="Add Version" 
                                            CssClass="button" onclick="btnVersion_Click" />
                                    </td>
                                    <td align="center" valign="top" class="style3">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" 
                                            CssClass="button" onclick="btnEdit_Click" TabIndex="4" 
                                            ToolTip="Please select a course from the grid by clicking the select cloumn and then click this button to edit it." />
                                    </td>
                                    <td align="center" valign="top" class="style3">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                            CssClass="button" onclick="btnDelete_Click" TabIndex="5" 
                                            ToolTip="Please select a course from the grid by clicking the select cloumn and then click this button to delete it." />
                                    </td>
                                 </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                    <td colspan="4">
                                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                    <td colspan="4" style="border-style:solid; border-color:Gray; border-width:1px;vertical-align:top;" 
                                        class="style6">
                                        <asp:Panel ID="pnlGrid" runat="server" Height="290px" ScrollBars="Vertical">
                                            <asp:GridView ID="gvwCourses" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#006666" 
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Height="120px" 
                                                Width="100%" onsorting="gvwCourses_Sorting" TabIndex="6">
                                                <RowStyle ForeColor="#000066" Height="24px" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ControlStyle Font-Names="Arial Narrow" Width="40px" />
                                                        <FooterStyle Font-Names="Arial Narrow" Width="40px" />
                                                        <HeaderStyle Font-Names="Arial Narrow" Width="40px" />
                                                        <ItemStyle Font-Names="Arial Narrow" Width="40px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="FullCode" HeaderText="Code">
                                                        <ControlStyle Width="70px" />
                                                        <FooterStyle Width="70px" />
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Title" HeaderText="Title" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Credits" HeaderText="Credits">
                                                        <ControlStyle Width="20px" Font-Names="Arial Narrow" />
                                                        <FooterStyle Font-Names="Arial Narrow" Width="20px" />
                                                        <HeaderStyle Font-Names="Arial Narrow" Width="20px" />
                                                        <ItemStyle Font-Names="Arial Narrow" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OwnerProgram.ShortName" HeaderText="Program" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Id" HeaderText="CourseID" Visible="False" >
                                                        <ControlStyle Width="20px" />
                                                        <FooterStyle Width="20px" />
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VersionID" HeaderText="VersionID" Visible="False" />
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                                    Height="24px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>                            
                        </asp:Panel>
                    </td>
                    <td valign="top" style="border-style:solid; border-color:Gray; border-width:1px;">                                  
                            <table style="width: 100%; height:30%;">
                                <tr>
                                    <td>
                                        <dxtc:ASPxPageControl ID="pcTabControl" runat="server" ActiveTabIndex="2" 
                                            Height="356px">
                                            <TabPages>
                                                <dxtc:TabPage Text="General Information" Name="GI">
                                                    <ActiveTabStyle HorizontalAlign="Center">
                                                    </ActiveTabStyle>
                                                    <TabStyle HorizontalAlign="Center">
                                                    </TabStyle>
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <asp:Panel ID="pnlGI" runat="server" Height="90%" ScrollBars="Vertical" 
                                                                Enabled="false" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px">
                                                            <table>
                                                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                                                    <td align="left">
                                                                        Formal Code :<asp:RequiredFieldValidator ID="rfvCode" runat="server" 
                                                                            ControlToValidate="txtCode" ErrorMessage="Code can not be empty" 
                                                                            Font-Names="Arial Narrow" SetFocusOnError="True" 
                                                                            ValidationGroup="ValidateCourse">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtCode" runat="server" TabIndex="7" 
                                                                            ValidationGroup="ValidateCourse" Width="152px"></asp:TextBox>
                                                                        <asp:Label ID="lblCourseID" runat="server" Visible="False"></asp:Label>
                                                                        <asp:Label ID="lblVersionID" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblCode" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                                                    <td align="left">
                                                                        Version Code :
                                                                        <asp:RequiredFieldValidator ID="rfvVCode" runat="server" 
                                                                            ControlToValidate="txtVcode" Enabled="False" 
                                                                            ErrorMessage="Version Code can not be empty" Font-Names="Arial Narrow" 
                                                                            SetFocusOnError="True" ValidationGroup="ValidateCourse">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtVcode" runat="server" TabIndex="8" 
                                                                            ValidationGroup="ValidateCourse" Width="155px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblVCode" runat="server" ForeColor="#CC0000" Text="*" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="border-style:solid; border-color:Gray; border-width:1px;">
                                                                    <td align="left" valign="top">
                                                                        Course Name :<asp:RequiredFieldValidator ID="rfvName" runat="server" 
                                                                            ControlToValidate="txtName" ErrorMessage="Name can not be empty" 
                                                                            Font-Names="Arial Narrow" SetFocusOnError="True" 
                                                                            ValidationGroup="ValidateCourse">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtName" runat="server" Height="60px" TabIndex="9" 
                                                                            ValidationGroup="ValidateCourse" Width="267px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lbName" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Course Credits :<asp:CompareValidator ID="cvMinvalue" runat="server" 
                                                                            ControlToValidate="txtCredit" ErrorMessage="Credit can not be less than zero" 
                                                                            Font-Names="Arial Narrow" Operator="GreaterThanEqual" Type="Double" 
                                                                            ValidationGroup="ValidateCourse" ValueToCompare="0.0">*</asp:CompareValidator>
                                                                        <asp:RequiredFieldValidator ID="rfvCredits" runat="server" 
                                                                            ControlToValidate="txtCredit" ErrorMessage="Credits can not be empty" 
                                                                            Font-Names="Arial Narrow" SetFocusOnError="True" 
                                                                            ValidationGroup="ValidateCourse">*</asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="cvCredits" runat="server" 
                                                                            ControlToValidate="txtCredit" ErrorMessage="Only numeric values are allowed" 
                                                                            Font-Names="Arial Narrow" Operator="DataTypeCheck" Type="Double" 
                                                                            ValidationGroup="ValidateCourse">*</asp:CompareValidator>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtCredit" runat="server" MaxLength="6" 
                                                                            onkeypress="return isNumber(event)" TabIndex="10" 
                                                                            ValidationGroup="ValidateCourse" Width="27%"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblCredit" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td align="left" colspan = "3">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="Is Active" />                                                                    
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkCreditCourse" runat="server" Checked="True" 
                                                                                        Text="Is Credit Course" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkSecMan" runat="server" Checked="True" 
                                                                                        Text="Is Section Mandatory" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Owner Program</td>
                                                                    <td align="left" colspan="2">
                                                                        <dxe:ASPxComboBox ID="cboPrograms" runat="server" ValueType="System.String" 
                                                                            Width="147px" AutoPostBack="True" DropDownStyle="DropDown" 
                                                                            EnableIncrementalFiltering="True">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Associated Course</td>
                                                                    <td align="left" 
                                                                        colspan="2">
                                                                        <dxe:ASPxComboBox ID="cboAssociateCourse" runat="server" ValueType="System.String" 
                                                                            Width="147px" AutoPostBack="True" DropDownStyle="DropDown" 
                                                                            EnableIncrementalFiltering="True">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Start ACU</td>
                                                                    <td align="left" 
                                                                        colspan="2">
                                                                        <dxe:ASPxComboBox ID="cboStarACU" runat="server" ValueType="System.String" 
                                                                            Width="147px" AutoPostBack="True" DropDownStyle="DropDown" 
                                                                            EnableIncrementalFiltering="True">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Course Type</td>
                                                                    <td colspan = "2">
                                                                        <dxe:ASPxComboBox ID="cboCourseType" runat="server" AutoPostBack="True" 
                                                                            DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                                                            ValueType="System.String" Width="147px">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td align="left">
                                                                        Course Group</td>
                                                                    <td align="left" colspan="2">
                                                                        <dxe:ASPxComboBox ID="cboCourseGroup" runat="server" ValueType="System.String" 
                                                                            Width="147px" AutoPostBack="True" DropDownStyle="DropDown" 
                                                                            EnableIncrementalFiltering="True">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Text="Multispan ACU Info" Name="MACU">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <asp:Panel ID="pnlMACU" runat="server" Height="98%" ScrollBars="Vertical" Enabled="false">
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <dxe:ASPxCheckBox ID="chkHasMultiSCUSpan" runat="server" 
                                                                                Text="This course can be taken accross multiple Academic Calender Unit" 
                                                                                OnCheckedChanged="chkHasMultiSCUSpan_CheckedChanged" AutoPostBack="True">
                                                                            </dxe:ASPxCheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="pnlACUS" runat="server" Enabled="False">
                                                                                <table style="width:100%;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            Max Allowed ACU</td>
                                                                                        <td align="left">
                                                                                            <dxe:ASPxSpinEdit ID="spnEditMaxACU" runat="server" Height="21px" Number="0" 
                                                                                                Width="75px" NumberType="Integer">
                                                                                            </dxe:ASPxSpinEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Min Allowed ACU</td>
                                                                                        <td align="left">
                                                                                            <dxe:ASPxSpinEdit ID="spnEditMinACU" runat="server" Height="20px" Number="0" 
                                                                                                Width="75px" NumberType="Integer">
                                                                                            </dxe:ASPxSpinEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <dxwgv:ASPxGridView ID="gdvAllowablelUnits" runat="server" 
                                                                                                AutoGenerateColumns="False" Width="386px" KeyFieldName="Id" 
                                                                                                OnCellEditorInitialize="gdvAllowablelUnits_CellEditorInitialize" 
                                                                                                OnRowDeleted="gdvAllowablelUnits_RowDeleted" 
                                                                                                OnRowDeleting="gdvAllowablelUnits_RowDeleting" 
                                                                                                OnRowInserted="gdvAllowablelUnits_RowInserted" 
                                                                                                OnRowInserting="gdvAllowablelUnits_RowInserting" 
                                                                                                OnRowUpdated="gdvAllowablelUnits_RowUpdated" 
                                                                                                OnRowUpdating="gdvAllowablelUnits_RowUpdating" 
                                                                                                OnRowValidating="gdvAllowablelUnits_RowValidating" 
                                                                                                OnDataBinding="gdvAllowablelUnits_DataBinding">
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0">
                                                                                                        <EditButton Visible="True">
                                                                                                        </EditButton>
                                                                                                        <NewButton Text="Add" Visible="True">
                                                                                                        </NewButton>
                                                                                                        <DeleteButton Visible="True">
                                                                                                        </DeleteButton>
                                                                                                        <ClearFilterButton Visible="True">
                                                                                                        </ClearFilterButton>
                                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Allowed Credits per ACU"   
                                                                                                        VisibleIndex="1" FieldName="CreditUnits"  PropertiesSpinEdit-NullText="0.00" PropertiesSpinEdit-Increment=".25" PropertiesSpinEdit-LargeIncrement="1" >
                                                                                                        <PropertiesSpinEdit DisplayFormatString="{0:00}" DecimalPlaces="2" NumberFormat="Custom" SpinButtons-ShowLargeIncrementButtons="true"  >
                                                                                                            <SpinButtons ShowLargeIncrementButtons="True">
                                                                                                            </SpinButtons>
                                                                                                        </PropertiesSpinEdit>
                                                                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                                                                </Columns>
                                                                                                <SettingsBehavior ConfirmDelete="True" />
                                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="Inline" />
                                                                                                <Settings ShowVerticalScrollBar="true" />
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel> 
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Text="Equivalent Courses" Name="EQUI">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <asp:Panel ID="pnlEQUI" runat="server" Height="98%" ScrollBars="Vertical" Enabled="false">
                                                                <table style="width:103%; height: 302px;">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:CheckBox ID="chkHasEqui" runat="server" 
                                                                                Text="This course has equivalent courses"
                                                                                OnCheckedChanged="chkHasEqui_CheckedChanged" AutoPostBack="True" 
                                                                                CausesValidation="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>                                                                                                                                        
                                                                            <dxwgv:ASPxGridView ID="gdvEquiCourses" runat="server" 
                                                                                AutoGenerateColumns="False" Width="416px"  
                                                                                OnCellEditorInitialize="gdvEquiCourses_CellEditorInitialize"                                                                                 
                                                                                KeyFieldName="Id" OnRowInserting="gdvEquiCourses_RowInserting" 
                                                                                OnRowDeleting="gdvEquiCourses_RowDeleting" 
                                                                                OnRowUpdating="gdvEquiCourses_RowUpdating" 
                                                                                OnRowDeleted="gdvEquiCourses_RowDeleted" 
                                                                                OnRowValidating="gdvEquiCourses_RowValidating" >
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                                                                                        <EditButton Visible="True">
                                                                                        </EditButton>
                                                                                        <NewButton Text="Add" Visible="True">
                                                                                        </NewButton>
                                                                                        <DeleteButton Visible="True">
                                                                                        </DeleteButton>
                                                                                        <CellStyle HorizontalAlign="Left" Wrap="True">
                                                                                        </CellStyle>
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Equivalent Course" 
                                                                                        FieldName = "FullCodeAndCourse" VisibleIndex="1">
                                                                                        <PropertiesComboBox ValueType="System.String"  DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior ConfirmDelete="True" />
                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                </SettingsPager>
                                                                                <SettingsEditing Mode="Inline" />
                                                                                <Settings ShowVerticalScrollBar="true" />
                                                                            </dxwgv:ASPxGridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Text="Course Content" Name="CONT">
                                                    <TabStyle HorizontalAlign="Center">
                                                    </TabStyle>
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <asp:Panel ID="pnlCont" runat="server" Height="98%" ScrollBars="Vertical" Enabled="false">
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td align="left">
                                                                            Course contents</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtContents" runat="server" Height="227px" 
                                                                                TextMode="MultiLine" Width="384px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                            </TabPages>
                                        </dxtc:ASPxPageControl>                                                                               
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlCourseCtl" runat="server" BorderColor="ActiveBorder" BorderStyle="Ridge" BorderWidth="2px" Width="99%" Enabled="false">
                                            <table>
                                                <tr>
                                                
                                                    <td style="width: 127px;" align="left">
                                                        <asp:Button ID="butSave" runat="server" CssClass="button" onclick="butSave_Click" 
                                                            ValidationGroup="ValidateCourse" Text="Save"
                                                            ToolTip="Click this button to save course information." TabIndex="11" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnClose" runat="server" Text="Cancel"
                                                            CssClass="button" onclick="btnClose_Click" 
                                                            ToolTip="Click this button to clear the interface." TabIndex="12" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px";>
                        &nbsp;
                        <asp:ImageButton ID="btnImport" runat="server" AlternateText="Import" 
                            BackColor="Blue" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" 
                            ImageUrl="~/ButtonImages/Import.jpg" onclick="btnImport_Click" 
                            Visible="False" /> &nbsp&nbsp
                            

                        <asp:ImageButton ID="btnExport" runat="server" AlternateText="Export" 
                            BackColor="Blue" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" 
                            ImageUrl="~/ButtonImages/Export.jpg" onclick="btnExport_Click" 
                            Visible="False" /> 
                            
                    </td>
               </tr>--%>
                <tr>
                    <td colspan="2" align="left" style="border-style:solid; border-color:Gray; border-width:1px";>

                        <asp:validationsummary ID="vsCourse" runat="server" BorderStyle="None" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ValidateCourse" />

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

