<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrepareStudentWorksheet.aspx.cs" Inherits="EMS.Registration.PrepareStudentWorksheet" MasterPageFile="~/MasterPage/Registration/Registration.master" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
     <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:980px; height:580px" id="tblHeader">
                <tr >                    
                    <td style="width: 250px; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="250px">Worksheet</asp:label>
                    </td>
                    <td style="width: 100px; height: 25px;" class="td">
                        <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="728px"></asp:label>
                    </td>
                </tr>
                <tr>                    
                    <td colspan="2"; class="td" > 
                        <asp:Panel ID="pnlCalender" runat="server" Width="980px" Height="97px">                                        
                        <table>
                            <tr >                                            
                                <td align ="left">
                                    Academic Calender / Batch                                         
                                </td>
                                <td style="width: 150px; height: 25px">
                                    <dxe:ASPxComboBox ID="cboAcaCalender" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        onselectedindexchanged="cboAcaCalender_SelectedIndexChanged" 
                                        AutoPostBack="True">
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
                                <td >                                    
                                    <asp:Button ID="btnView" runat="server" CssClass="button" Height="25px" 
                                        onclick="btnView_Click" Text="Load" Width="124px" />
                                </td>                                                            
                            </tr>
                            <tr>
                                <td colspan = "5">  
                                                                      
                                </td>
                            </tr>
                            <tr>
                                <td colspan = "5">
                                    <table>
                                        <tr>  
                                            <td align ="left">                                        
                                            </td>
                                            
                                            <td>                                                            
                                                <asp:Button ID="btnSave" runat="server" Height="25px" onclick="btnSave_Click" 
                                                    Text="Generate WorkSheet" Width="124px" CssClass="button" />                                                                                                            
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
                    <td  colspan="2"; class="td" >
                        <table style="height: 24px; width: 960px">
                                <tr style=" color:Aqua">
                                    <td style=" width:100px">
                                        <asp:CheckBox ID="chkAll" Text="Select All" Width="100px" runat="server" 
                                            Font-Bold="True" AutoPostBack="True" 
                                            oncheckedchanged="chkAll_CheckedChanged1" />
                                    </td>
                                    <td style=" width:100px">
                                        <asp:Label ID="Label1" runat="server" Width = "100px" Text="Student Roll" 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style=" width:250px">
                                        <asp:Label ID="Label2" runat="server" Width = "250px" Text="Name" 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style=" width:150px">
                                        <asp:Label ID="Label3" runat="server" Width = "150px" Text="Course Tree Name" 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>          
                    </td>
                 </tr>
                 <tr>
                    <td colspan = "2"; class="td">
                        <asp:Panel ID="pnlCourseOfferGrid" runat="server" Width="100%" Height="397px" 
                            ScrollBars="Vertical" Wrap="False">                                              
                            <asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                Height="120px" TabIndex="6" Width="960px" ShowHeader="False">
						        <RowStyle Height="24px" />
								    <Columns>
                                        <asp:TemplateField  ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text = " Select All " AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged = "chk_CheckedChanged"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                       </asp:TemplateField>
                                        <asp:boundfield DataField="Id" HeaderText="ID" ItemStyle-Width="50px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="50px" />                                            
                                        </asp:boundfield>
                                        <asp:boundfield DataField="Roll" HeaderText="Student Roll" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="100px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="100px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="StdName" HeaderText="Student name" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="250px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="250px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="TreeMasterID" HeaderText="tree id" Visible="false" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="150px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="TreeName" HeaderText="tree name" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="150px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:boundfield>                                        
								    </Columns>
							    <SelectedRowStyle 
                                        Height="24px" />
							    <HeaderStyle Height="24px"/>
						    </asp:gridview>
                        </asp:Panel>
                    </td>
                 </tr>                              
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>