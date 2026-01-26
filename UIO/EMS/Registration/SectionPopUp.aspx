<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectionPopUp.aspx.cs" Inherits="EMS.Registration.SectionPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    <script language="javascript" type="text/javascript">
        function CloseForm()
        {
            window.close();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                    <div style="height: 250px;">
                        <asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                <asp:BoundField DataField="Occupied" HeaderText="Occupied" />
                                <asp:BoundField DataField="TimeSlotPlanOneID" HeaderText="Time Slot One" />
                                <asp:BoundField DataField="TimeSlotPlanTwoID" HeaderText="Time Slot Two" />
                                <asp:BoundField DataField="TeacherIDOne" HeaderText="Teacher One" />
                                <asp:BoundField DataField="TeacherIDTwo" HeaderText="Teacher Two" />
                                <asp:BoundField DataField="RoomInfoOneID" HeaderText="Room Info One" />
                                <asp:BoundField DataField="RoomInfoTwoID" HeaderText="Room Info Two" />
                            </Columns>
                        </asp:GridView>   
                    </div>
                    <div>
                        <asp:Button ID="btnOk" runat="server" Text="OK" OnClientClick="CloseForm()" 
                            onclick="btnOk_Click"/>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
