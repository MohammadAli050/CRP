<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Sylabus/Sylabus.master" AutoEventWireup="true" Inherits="SyllabusMan_CourseOffering" Codebehind="CourseOffering.aspx.cs" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #tblHeader
        {
            height: 381px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:985px; height:580px" id="tblHeader">
                <tr >                    
                    <td style="width: 250px; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="250px">Course Offering</asp:label>
                    </td>
                    <td style="width: 100px; height: 25px;" class="td">
                        <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="735px"></asp:label>
                    </td>
                </tr>
                <tr>                    
                    <td colspan="2"> 
                        <asp:Panel ID="pnlCalender" runat="server" Width="985px" Height="97px">                                        
                        <table>
                            <tr >                                            
                                <td align ="left">
                                    Academic Calender                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboAcaCalender" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        onselectedindexchanged="cboAcaCalender_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align ="left">
                                    Department                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboDept" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        AutoPostBack="True" onselectedindexchanged="cboDept_SelectedIndexChanged" 
                                        TabIndex="2">
                                    </dxe:ASPxComboBox>
                                </td>                                            
                                <td align ="left">Program                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboProgram" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        AutoPostBack="True" ReadOnlyStyle-Font-Italic="true" 
                                        onselectedindexchanged="cboProgram_SelectedIndexChanged" TabIndex="3">
                                        <ReadOnlyStyle Font-Italic="True">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align ="left">Course Tree                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboCourseTree" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        AutoPostBack="True" ReadOnlyStyle-Font-Italic="true" 
                                        onselectedindexchanged="cboCourseTree_SelectedIndexChanged" TabIndex="3">
                                        <ReadOnlyStyle Font-Italic="True">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxComboBox>
                                </td>                                                               
                            </tr>
                            <tr>
                                <td colspan = "8">  
                                                                      
                                </td>
                            </tr>
                            <tr>
                                <td colspan = "6">                                        
                                    <asp:Label ID="Label1" runat="server" Text="Total Appeared Courses :"></asp:Label>
                                    <asp:Label ID="lblTotalCourses" runat="server"></asp:Label>
                                </td>
                                <td colspan = "2">                                        
                                    <asp:Label ID="Label2" runat="server" Text="Total Selected Courses :"></asp:Label>
                                    <asp:Label ID="lblSelectedCourses" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan = "8">
                                    <table>
                                        <tr>                                                
                                            <td >                                    
                                                <asp:Button ID="btnView" runat="server" CssClass="button" Height="25px" 
                                                    onclick="btnView_Click" Text="View" Width="124px" />
                                            </td>
                                            <td>                                                            
                                                <asp:Button ID="btnSave" runat="server" Height="25px" onclick="btnSave_Click" 
                                                    Text="Save" Width="124px" CssClass="button" />                                                                                                            
                                            </td>
                                            <td>                                                    
                                                <asp:Button ID="btnCancel" runat="server" Height="25px" 
                                                    Text="Cancel" Width="124px" onclick="btnCancel_Click" CssClass="button" />                                                    
                                            </td>
                                        </tr>                                                                                                                                                               
                                    </table>                                                                         
                                </td>
                             </tr>
                         </table>                              
                        </asp:Panel>                         
                    </td>                     
                 </tr>                     
                 <tr>
                    <td  colspan="2">
                        <asp:Panel ID="pnlCourseOfferGrid" runat="server" Width="985px" Height="397px" 
                            ScrollBars="Vertical">
                            <asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                Height="120px" TabIndex="6" Width="983px">
						        <RowStyle Height="24px" />
								    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                         <asp:boundfield DataField="FormalCode" HeaderText="Formal Course Code" 
                                            ItemStyle-Width="150px">
                                            <ItemStyle Width="150px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="VersionCode" HeaderText="Version Course Code" 
                                            ItemStyle-Width="150px">
                                            <ItemStyle Width="150px" />
                                        </asp:boundfield>
									    <asp:boundfield DataField="Title" HeaderText="Course Description" 
                                            ItemStyle-Width="500px" >
                                            <ItemStyle Width="500px" />
                                        </asp:boundfield>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text = " Select All " AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged = "chk_CheckedChanged"/>
                                            </ItemTemplate>
                                       </asp:TemplateField>
								    </Columns>
							    <SelectedRowStyle 
                                        Height="24px" />
							    <HeaderStyle 
                                        Height="24px" />
						    </asp:gridview>
                        </asp:Panel>
                    </td>
                 </tr>                              
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

