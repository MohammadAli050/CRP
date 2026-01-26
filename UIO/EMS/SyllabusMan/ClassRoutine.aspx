<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Sylabus/Sylabus.master" AutoEventWireup="true" Inherits="SyllabusMan_ClassRoutine" Codebehind="ClassRoutine.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.2" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <style type="text/css">
        
        .style2
        {
            height: 29px;
            width: 115px;
        }
        .style6
        {
            height: 25px;
            width: 84px;
        }
        .style7
        {
            height: 25px;
            width: 82px;
        }
        .style8
        {
            height: 25px;
            width: 85px;
        }
        
        #tblHeader
        {
            
            margin-right: 18px;
        }
        
        </style>
            <script type="text/javascript">
                function Combo_SelectedIndexChanged(s, e) {
                    Grid.GetValuesOnCustomCallback(s.GetSelectedItem().value, EndCallback)
                }
                function EndCallback(values) {
                    Grid.GetEditor("CategoryName").SetValue(values[0]);
                    Grid.GetEditor("Description").SetValue(values[1]);
                }
         </script>
<%--        <script type="text/javascript"><!--
                        //function is called on changing country
                        function OnCountryChanged(cmbCountry) {
                            document.getElementById('ctl00_cpHolMas_txt').value = cmbCountry.GetValue().ToString();
                            gvRoutine.GetEditor("EquiCourse.FullCodeAndCourse").PerformCallback(cmbCountry.valueOf().ToString());
                        }
    //--></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table id="tblHeader">
                <tr >                    
                    <td style="width: 250px; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="250px">Class Routine </asp:label>
                    </td>
                    <td style="width: 100px; height: 25px;" class="td">
                        <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="735px"></asp:label>
                    </td>
                </tr>                
                <tr>
                    <td colspan="2">                    
                        <asp:Panel ID="pnlCalender" runat="server" Width="985px" Height="41px">
                            <table>
                                <tr >                                            
                                    <td align ="left" class="style2">
                                        Academic Calender                                         
                                    </td>
                                    <td style="width: 309px; height: 25px">
                                        <dxe:ASPxComboBox ID="cboAcaCalender" runat="server" Width="309px" 
                                            Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                            onselectedindexchanged="cboAcaCalender_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td class="style7">
                                        <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" 
                                            BackColor="#D3DCE6" ImageUrl="~/ButtonImages/Delete.jpg" 
                                            onclick="btnDelete_Click" />
                                    </td>
                                </tr>
                                                                                                                          
                            </table>
                            <%--<table style="height: 31px">
                                            <tr>
                                                <td style="width: 140px; height: 25px">
                                                    &nbsp;</td>
                                                <td style="width: 140px; height: 25px">
                                                    &nbsp;</td>
                                                                                                
                                            </tr>
                                        </table>
                            <table >
                                <tr>
                                    <td class="style4">
                                    </td>                                        
                                    <td style="width: 490px; height: 25px">
                                    </td>
                                </tr>                                
                            </table>--%></asp:Panel>                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2">                    
                        <asp:Panel ID="pnlProgram" runat="server" Width="985px" Height="36px">
                            <table>
                                <tr>                                            
                                    <td align ="left" class="style2">Department                                         
                                    </td>
                                    <td style="width: 309px; height: 25px">
                                        <dxe:ASPxComboBox ID="cboDept" runat="server" Width="309px" 
                                            Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                            AutoPostBack="True" onselectedindexchanged="cboDept_SelectedIndexChanged" 
                                            TabIndex="2">
                                        </dxe:ASPxComboBox>
                                    </td>                                            
                                    <td align ="left" class="style6">Program                                         
                                    </td>
                                    <td style="width: 309px; height: 25px">
                                        <dxe:ASPxComboBox ID="cboProgram" runat="server" Width="309px" 
                                            Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                            AutoPostBack="True" ReadOnlyStyle-Font-Italic="true" 
                                            onselectedindexchanged="cboProgram_SelectedIndexChanged" TabIndex="3">
                                            <ReadOnlyStyle Font-Italic="True">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                                                                                                          
                            </table>
                            <%--<table>
                                <tr>
                                    <td class="style5">

                                    </td>                                        
                                    <td style="width: 490px; height: 25px">
                                        <table>
                                            <tr>
                                            </tr>
                                                                                                                                      
                                        </table>
                                    </td>            
                                </tr>                                
                            </table>--%></asp:Panel>                    
                    </td>
                </tr>  
                <tr>
                    <td colspan="2">                    
                        <asp:Panel ID="pnlSearch" runat="server" Width="985px" Height="37px">
                            <table>
                                <tr>                                            
                                    <td class="style2">
                                        Course</td>
                                    <td style="width: 309px; height: 25px">
                                        <dxe:ASPxComboBox ID="cboSearch" runat="server" Width="309px" 
                                            Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                            AutoPostBack="True" ValueType="System.String" TabIndex="4">
                                            <Items>
                                                <dxe:ListEditItem Text="All" Value="0" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td class="style8">
                                        <asp:ImageButton ID="btnSearch" runat="server" BackColor="#D3DCE6" 
                                            ImageUrl="~/ButtonImages/Find.jpg" onclick="btnSearch_Click" />
                                    </td>
                                </tr>
                                                                                                                          
                            </table>
                        </asp:Panel>                    
                    </td>
                </tr>           
                <tr>
                    <td colspan="2">&nbsp;&nbsp; 
                        </td>
                </tr>
                <tr style="width: 985px; height: 700px">
                    <td colspan="2" valign = "top" style="border-style:solid; border-color:Gray; border-width:1px; height: 700px; width: 985px;" >
                        <dxwgv:ASPxGridView ID="gvRoutine" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvRoutine" oncelleditorinitialize="gvRoutine_CellEditorInitialize" 
                            KeyFieldName="Id" Width="1200px" onrowinserting="gvRoutine_RowInserting" 
                            onrowupdating="gvRoutine_RowUpdating" 
                            onrowdeleting="gvRoutine_RowDeleting" onrowdeleted="gvRoutine_RowDeleted" 
                            onrowinserted="gvRoutine_RowInserted" TabIndex="6" 
                            onrowvalidating="gvRoutine_RowValidating" 
                            oncustomcallback="gvRoutine_CustomCallback">                            
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsEditing Mode="Inline" />                      
                            <Columns>
                                <dxwgv:GridViewCommandColumn Visible = "false" VisibleIndex="0" 
                                    ButtonType="Button" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="True" 
                                    Width="100px">
                                    <NewButton Visible="true" Text="Add">
                                    </NewButton>
                                    <EditButton Visible="true">
                                    </EditButton>
                                    <DeleteButton Visible="true">
                                    </DeleteButton>
                                    <CellStyle HorizontalAlign="Left" Wrap="True">
                                    </CellStyle>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Course" FieldName="ChildCourse.FullCodeAndCourse" VisibleIndex="1" Width="120px">                                                                        
                                    <PropertiesComboBox ValueType="System.Int32"  EnableSynchronization="False"
                                        EnableIncrementalFiltering="True" CallbackPageSize="100" ><%--<ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }"></ClientSideEvents>--%><ClientSideEvents SelectedIndexChanged="function(s, e) { gvRoutine.PerformCallback(s.GetValue()) }" /></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn> 
                                <dxwgv:GridViewDataComboBoxColumn Caption="Equi Course" FieldName="EquiCourse.FullCodeAndCourse" VisibleIndex="2" Width="120px">
                                    <PropertiesComboBox ValueType="System.Int32"></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>                                                         
                                <dxwgv:GridViewDataTextColumn Caption="Section" FieldName="SectionName" VisibleIndex="3" Width="50px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Capacity" FieldName="Capacity" VisibleIndex="4" Width="50px">
                                    <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                </dxwgv:GridViewDataSpinEditColumn> 
                                <dxwgv:GridViewDataComboBoxColumn Caption="Room No (1)" FieldName="RoomInfoOne.RoomNumberAndCapacity" VisibleIndex="5" Width="80px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>                                                               
                                <dxwgv:GridViewDataComboBoxColumn Caption="Day (1)" FieldName="DayOne.Name" VisibleIndex="6" Width="120px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Time Slot (1)" FieldName="TimeSlotPlanOne.Time" VisibleIndex="7" Width="180px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Faculty (1)" FieldName="TeacherOneID.Code" VisibleIndex="8" Width="100px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Room No (2)" FieldName="RoomInfoTwo.RoomNumberAndCapacity" VisibleIndex="9" Width="80px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Day (2)" FieldName="DayTwo.Name" VisibleIndex="10" Width="120px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="Time Slot (2)" FieldName="TimeSlotPlanTwo.Time" VisibleIndex="11" Width="180px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn> 
                                <dxwgv:GridViewDataComboBoxColumn Caption="Faculty (2)" FieldName="TeacherTwoID.Code" VisibleIndex="12" Width="120px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="False" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn> 
                                <dxwgv:GridViewDataComboBoxColumn Caption="Shared Dept (1)" FieldName="ShareDptOne.Code" VisibleIndex="13" Width="50px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>                                                            
                                <dxwgv:GridViewDataComboBoxColumn Caption="Shared Dept (2)" FieldName="ShareDptTwo.Code" VisibleIndex="14" Width="50px">                                    
                                    <PropertiesComboBox ValueType="System.String" DropDownStyle="DropDown" EnableIncrementalFiltering="True" ></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>            
                                <%--<dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="6">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton Text="~/SyllabusMan/ClassRoutine.aspx">                                            
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dxwgv:GridViewCommandColumn> --%><dxwgv:GridViewDataComboBoxColumn 
                                    Caption="Section Type" FieldName="SectionTypeDefinition.Definition" 
                                    VisibleIndex="14" Width="100px">
                                    <PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                            </Columns>
                            <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="600"  />
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
       </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

