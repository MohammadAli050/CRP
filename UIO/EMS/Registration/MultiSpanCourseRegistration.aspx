<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Registration/Registration.master" AutoEventWireup="true" Inherits="Registration_MultiSpanCourseRegistration" Codebehind="MultiSpanCourseRegistration.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" class = "tbcolor">
                <tr>                    
                    <td style="width: 40%; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="245px">Multispan Course Registration</asp:label>
                    </td>
                    <td style="width: 60%; height: 25px;" class="td">
                        <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="727px"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td colspan = "2"></td>
                </tr>  
                <tr>
                    <td colspan = "2">
                    <asp:Label ID="lblRequisition" runat="server" Text="Requisition Area"></asp:Label>                        
                    </td>
                </tr>              
                <tr>
                    <td colspan = "2">
                        <asp:GridView ID="gvRequisition" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" Width="100%">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="Name" 
                                    HeaderText="Course Code">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HighestGrade" HeaderText="Grade">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Span Definitions">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSpan" runat="server" DataTextField="SectionTime">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select Section">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSection" runat="server" DataTextField="SectionTime">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="chk_CheckedChanged" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                </asp:TemplateField>                                
                                <asp:BoundField DataField="DataObjTypeName" HeaderText="DataObjTypeName" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID" HeaderText="DataObjID" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID2" HeaderText="DataObjID2" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID3" HeaderText="DataObjID3" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CanRetake" HeaderText="CanRetake" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsOffered" HeaderText="IsOffered" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsEligible" HeaderText="IsEligible" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasCompleted" HeaderText="HasCompleted" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PreRequisitDone" HeaderText="PreRequisitDone" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasMultispan" HeaderText="HasMultiSpan" 
                                    Visible="False" />
                                <asp:BoundField DataField="MultiSpanMasID" HeaderText="MultiSpanMasID" 
                                    Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr> 
                <tr>
                    <td colspan = "2"></td>
                </tr> 
                <tr>
                    <td colspan = "2"></td>
                </tr>    
                <tr>
                    <td colspan = "2">
                        <asp:Label ID="lblAlreadyTaken" runat="server" 
                            Text="Previous Registration History" Visible="False"></asp:Label>
                    </td>
                </tr> 
                <tr>
                    <td colspan = "2">
                        <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" Width="100%" 
                            Visible="False">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="Name" 
                                    HeaderText="Course Code">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HighestGrade" HeaderText="Grade">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Span Definitions">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSpan" runat="server" DataTextField="SectionTime">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select Section">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSection" runat="server" DataTextField="SectionTime" Enabled = "false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DataObjTypeName" HeaderText="DataObjTypeName" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID" HeaderText="DataObjID" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID2" HeaderText="DataObjID2" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID3" HeaderText="DataObjID3" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CanRetake" HeaderText="CanRetake" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsOffered" HeaderText="IsOffered" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsEligible" HeaderText="IsEligible" Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasCompleted" HeaderText="HasCompleted" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PreRequisitDone" HeaderText="PreRequisitDone" 
                                    Visible="False">
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasMultispan" HeaderText="HasMultiSpan" 
                                    Visible="False" />
                                <asp:BoundField DataField="MultiSpanMasID" HeaderText="MultiSpanMasID" 
                                    Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>                     
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>