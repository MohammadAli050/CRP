<%@ Page Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Codebehind="CourseAutoAssignOpen.aspx.cs" Inherits="StudentManagement_CourseAutoAssignOpen" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<script runat="server">

</script>

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
            <table style="width:980px; height:580px" id="tblHeader">
                <tr>
                    <td style="width: 250px; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="250px">Course Auto assign and auto open</asp:label>
                    </td>
                    <td style="width: 728px; height: 25px;" class="td">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" 
                            Width="728px" style="margin-left: 0px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                        <asp:Label ID="Label1" runat="server" Text="Program"></asp:Label>        
                    </td>
                    <td style="height: 25px">
                        <dxe:ASPxComboBox ID="cboProgram" runat="server" Width="150px" 
                                        Height="25px" DropDownStyle="DropDown" EnableIncrementalFiltering="True" 
                                        AutoPostBack="True" ReadOnlyStyle-Font-Italic="true" 
                            onselectedindexchanged="cboProgram_SelectedIndexChanged">                                                                    
                       </dxe:ASPxComboBox>                                
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px">
                        <asp:Button ID="btnAutoAssignandOpen" runat="server" 
                            Text="Auto Assign and Open" onclick="btnAutoAssignandOpen_Click" 
                            TabIndex="1" />
                    </td>
                    <td style="height: 25px">
                        <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td colspan = "2"></td>                    
                </tr>
                <tr style="height: 25px">
                    <td colspan = "2"></td>                    
                </tr>
                <tr style="height: 500px">
                    <td colspan = "2">
                        <asp:gridview ID="gvwCollection" runat="server" AutoGenerateColumns="False" 
                                Height="120px" TabIndex="6" Width="960px">
						        <RowStyle Height="24px" />
								    <Columns>
                                        <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text = " Select All " AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged = "chk_CheckedChanged"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                       </asp:TemplateField>                                       
                                        <asp:boundfield DataField="Id" HeaderText="ID" ItemStyle-Width="50px" 
                                            Visible="true">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="50px" />                                            
                                        </asp:boundfield>
                                        <asp:boundfield DataField="Roll" HeaderText="Student Roll" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="100px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="100px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="StdName" HeaderText="Student name" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="300px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="300px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="TreeMasterID" HeaderText="tree id" Visible="true" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="100px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="100px" />
                                        </asp:boundfield>
                                        <asp:boundfield DataField="TreeName" HeaderText="Course Tree Name" HeaderStyle-HorizontalAlign="Left" 
                                            ItemStyle-Width="300px">
                                             <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="300px" />
                                        </asp:boundfield>                                        
								    </Columns>
							    <SelectedRowStyle 
                                        Height="24px" />
							    <HeaderStyle Height="24px"/>
						    </asp:gridview>
                    </td>                    
                </tr>
            </table>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
