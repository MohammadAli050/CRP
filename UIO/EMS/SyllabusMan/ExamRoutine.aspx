<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Sylabus/Sylabus.master" AutoEventWireup="true" Inherits="SyllabusMan_ExamRoutine" Codebehind="ExamRoutine.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxScheduler.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler.Controls" tagprefix="dxwschsc" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            border: 1px solid Gray;
            width: 50%;
            height: 451px;
        }
    .style6
    {
        width: 140px;
        height: 25px;
    }
    .style7
    {
        width: 300px;
        height: 25px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolMas" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border: 1px solid Gray; width:900; height:550px" >
                <tr>
                    <td align="left" style="border-style:solid; border-color:Gray; border-width:1px;">
                        <table style="width: 900px; height:25px;">
                            <tr>
                                <td class="td" style="height: 25px; width: 300px">
                                    <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="180px">Exam Routine</asp:label>
                                </td>
                                <td class="td" style="height: 25px; width:600px">
                                    <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="500px"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid Gray; " class="style6">
                                                Program
                                </td>
                                <td style="border: 1px solid Gray; " class="style7">
                                     <asp:DropDownList ID="Program" runat="server" Height="25px" 
                                                Width="300px">
                                     </asp:DropDownList>
                                </td>                               
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table style="width: 700px; height:25px;">
                                        <tr>                                            
                                            <td style="border: 1px solid Gray; height: 25px; width: 140px">
                                                Unity Type</td>
                                            <td style="border: 1px solid Gray; height: 25px; width: 162px">
                                                <asp:DropDownList ID="ddlCalUnitType" runat="server" Height="25px" 
                                                    Width="162px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="border: 1px solid Gray; height: 25px; width: 140px">
                                                Year</td>
                                            <td style="border: 1px solid Gray; height: 25px; width: 162px">
                                                <asp:DropDownList ID="ddlYear" runat="server" Height="25px" Width="162px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="border: 1px solid Gray; height: 25px; width: 80px">
                                                <asp:Button ID="btnFind" runat="server" Text="Find" Height="25px" Width="80px" />
                                            </td>
                                        </tr>                                        
                                    </td>                    
                             </tr>                                                                                
                        </table> 
                    </td>                                        
               </tr>
               <tr style="border: 1px solid Gray;">
                <td align="left" style="width: 300px; height: 550px">
                   <table>
                        <tr>
                            <td></td>                          
                        </tr>
                        <tr style="border: 1px solid Gray;">
                            <td style="border: 1px solid Gray;">
                                
                                <asp:TextBox ID="txtSearch" runat="server" Width="164px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="60px" />                                                           
                        </tr>
                        <tr style="border: 1px solid Gray;">
                            <td align="left" style="border: 1px solid Gray; width: 300px; height: 530px">
                            
                                <asp:Panel ID="Panel1" runat="server" Height="530px" Width="300px" 
                                    ScrollBars="Vertical">
                                    <asp:GridView ID="gvCourseList" runat="server" Width="300px" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                                GridLines="Horizontal">
                                        <RowStyle BackColor="White" ForeColor="#333333" Height="25px" Width="300px" />
                                              <Columns>
                                                  <asp:commandfield ShowSelectButton="True">
                                                      <ControlStyle Width="5px" />
                                                      <FooterStyle Width="5px" />
                                                      <HeaderStyle Width="5px" />
                                                      <ItemStyle Width="5px" />
                                                  </asp:commandfield>
                                                  <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                                  <asp:BoundField DataField="FormalCode" HeaderText="Formal Code" 
                                                      ReadOnly="True">
                                                      <ControlStyle Width="10px" />
                                                      <FooterStyle Width="10px" />
                                                      <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                                      <ItemStyle Width="10px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="Version Code" HeaderText="Version Code" ReadOnly="True">
                                                      <ControlStyle Width="10px" />
                                                      <FooterStyle Width="10px" />
                                                      <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                                      <ItemStyle Width="10px" />
                                                  </asp:BoundField>
                                                  <asp:boundfield DataField="Title" HeaderText="Title" ReadOnly="True">
                                                      <ControlStyle Width="170px" />
                                                      <FooterStyle Width="170px" />
                                                      <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                      <ItemStyle Width="170px" />
                                                  </asp:boundfield>
                                                  <asp:BoundField DataField="Credits" HeaderText="Credits">
                                                      <ControlStyle Width="5px" />
                                                      <FooterStyle Width="5px" />
                                                      <HeaderStyle HorizontalAlign="Left" Width="5px" />
                                                      <ItemStyle Width="5px" />
                                                  </asp:BoundField>
                                              </Columns>
                                              <FooterStyle BackColor="White" ForeColor="#333333" />
                                              <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                              <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                                                  Height="25px" />
                                              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                                  Height="25px" />
                                    </asp:GridView>
                                </asp:Panel>                            
                            </td>
                        </tr>
                   </table>
                </td> 
                <td>
                <table style="border: 1px solid Gray; width: 600px; height:25px;">
                    <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray; width: 75px; height: 25px">
                                        Formal Code</td>
                                    <td style="border: 1px solid Gray; Width: 100px; height: 25px">
                                        <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                         <td style="width: 200px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Section</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="140px"></asp:TextBox>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray; width: 75px; height: 25px">
                                        Version Code</td>
                                    <td style="border: 1px solid Gray; Width: 100px; height: 25px">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                         <td style="width: 200px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Day</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <asp:DropDownList ID="ddlDay" runat="server" Height="25px" Width="140px">
                                            <asp:ListItem>SAT</asp:ListItem>
                                            <asp:ListItem>SUN</asp:ListItem>
                                            <asp:ListItem>MON</asp:ListItem>
                                            <asp:ListItem>TUES</asp:ListItem>
                                            <asp:ListItem>WED</asp:ListItem>
                                            <asp:ListItem>THURS</asp:ListItem>
                                            <asp:ListItem>FRI</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray; width: 75px; height: 25px">
                                        Title</td>
                                    <td style="border: 1px solid Gray; Width: 300px; height: 25px">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                         <td style="width: 200px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Time</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <asp:DropDownList ID="ddlTime" runat="server" Height="25px" Width="140px">
                                        </asp:DropDownList>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray; width: 75px; height: 25px">
                                        Credits</td>
                                    <td style="border: 1px solid Gray; Width: 100px; height: 25px">
                                        <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                         <td style="width: 200px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Room</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <asp:DropDownList ID="ddlRoom" runat="server" Height="25px" Width="140px">
                                        </asp:DropDownList>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px;">                            
                         </td>
                         <td style="width: 200px; height:25px;border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Faculty</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="140px">
                                        </asp:DropDownList>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>                         
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td style="width: 400px; height:25px;">                            
                            &nbsp;</td>
                         <td style="width: 200px; height:25px; border: 1px solid Gray;">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="border: 1px solid Gray;width: 60px; height: 25px">
                                        Date</td>
                                    <td style="border: 1px solid Gray; Width: 140px; height: 25px">
                                        <dxe:ASPxDateEdit ID="txtDate" runat="server">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                 </tr>                                                            
                            </table>
                         </td>                         
                      </tr>
                      <tr>
                        <td colspan="2">                            
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="width: 200px; height: 25px; border: 1px solid Gray;" align="center">                                    
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="80px" />
                                    </td>
                                    <td style="width: 200px; height: 25px; border: 1px solid Gray;" align="center">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" />
                                    </td>
                                    <td style="width: 200px; height: 25px; border: 1px solid Gray;" align="center">
                                        <asp:Button ID="btnDelect" runat="server" Text="Delete" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                            <table>
                                <tr style="border: 1px solid Gray;">
                                    <td style="width: 300px; height: 25px; border: 1px solid Gray;" align="center">                                    
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" />
                                    </td>
                                    <td style="width: 300px; height: 25px; border: 1px solid Gray;" align="center">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" />
                                    </td>
                                 </tr>
                            </table>
                        </td>
                      </tr>
                      <tr style="border: 1px solid Gray;">
                        <td colspan="2" style="width: 600px;border: 1px solid Gray;">
                            <asp:Panel ID="Panel2" runat="server" Height="280px" ScrollBars="Vertical" 
                                Width="616px">
                                <asp:GridView ID="gvRoutine" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                    GridLines="Horizontal" Width="600px">
                                    <RowStyle BackColor="White" ForeColor="#333333" Height="25px" Width="600px" />
                                    <Columns>
                                        <asp:commandfield ShowSelectButton="True">
                                            <ControlStyle Width="5px" />
                                            <FooterStyle Width="5px" />
                                            <HeaderStyle Width="5px" />
                                            <ItemStyle Width="5px" />
                                        </asp:commandfield>
                                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                        <asp:BoundField DataField="FormalCode" HeaderText="Formal Code" ReadOnly="True">
                                            <ControlStyle Width="10px" />
                                            <FooterStyle Width="10px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Version Code" HeaderText="Version Code" 
                                            ReadOnly="True">
                                            <ControlStyle Width="10px" />
                                            <FooterStyle Width="10px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:boundfield DataField="Title" HeaderText="Title" ReadOnly="True">
                                            <ControlStyle Width="170px" />
                                            <FooterStyle Width="170px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                            <ItemStyle Width="170px" />
                                        </asp:boundfield>
                                        <asp:BoundField DataField="Credits" HeaderText="Credits">
                                            <ControlStyle Width="5px" />
                                            <FooterStyle Width="5px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="5px" />
                                            <ItemStyle Width="5px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Section" HeaderText="Section">
                                            <ControlStyle Width="4px" />
                                            <FooterStyle Width="4px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="4px" />
                                            <ItemStyle Width="4px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Room" HeaderText="Room">
                                            <ControlStyle Width="10px" />
                                            <FooterStyle Width="10px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Day" HeaderText="Day">
                                            <ControlStyle Width="10px" />
                                            <FooterStyle Width="10px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Time" HeaderText="Time">
                                            <ControlStyle Width="50px" />
                                            <FooterStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Faculty" HeaderText="Faculty">
                                            <ControlStyle Width="50px" />
                                            <FooterStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                                        Height="25px" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                                        Height="25px" />
                                </asp:GridView>
                                </asp:Panel>
                        </td>
                      </tr>                                                     
                  </table>
               </td>
             </tr>                                                                                                                      
           </table>
       </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


