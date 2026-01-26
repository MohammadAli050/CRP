<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Registration_CourseRegistration" Codebehind="CourseRegistration.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="985px" class = "tbcolor">
                <tr>                    
                    <td style="width: 248px; height: 25px;" class="td">
                        <asp:label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Width="245px">Course Registration</asp:label>
                    </td>
                    <td style="width: 730px; height: 25px;" class="td">
                        <asp:label ID="lblMsg" runat="server" ForeColor="#CC0000" Width="727px"></asp:label>
                    </td>
                </tr>               
                <tr>
                    <td colspan = "2" class="td" style="width: 978px;">
                        <asp:Panel ID="pnlStudentInfo" runat="server">
                            <table>
                                <tr>
                                    <td style="width:85px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;">                                        
                                        Student ID
                                    </td>
                                    <td style="width: 250px">
                                        <dxe:ASPxTextBox ID="txtStudent" runat="server" 
                                            ontextchanged="txtStudent_TextChanged" Width="170px">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Admission Session
                                    </td>
                                    <td style="height: 25px; width: 164px">
                                        <asp:Label ID="lblAdmissionSession" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        First Major</td>
                                    <td style="height: 25px; width: 230px">
                                        <asp:Label ID="lblFirstMajor" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:85px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Student Name</td>
                                    <td style="width: 250px" >
                                        <asp:Label ID="lblStdName" runat="server" Height="25px" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Current Session</td>
                                    <td style="height: 25px; width: 164px">
                                        <asp:Label ID="lblCurrentSession" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        First Minor</td>
                                    <td style="height: 25px; width: 230px">
                                        <asp:Label ID="lblForstMinor" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:85px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Department</td>
                                    <td style="width: 250px">
                                        <asp:Label ID="lblDept" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Completed Credits</td>
                                    <td style="height: 25px; width: 164px">
                                        <asp:Label ID="lblCredits" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Second Major</td>
                                    <td style="height: 25px; width: 230px">
                                        <asp:Label ID="lblSecondMajor" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:85px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Program</td>
                                    <td style="width: 250px">
                                        <asp:Label ID="lblProg" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        CGPA</td>
                                    <td style="height: 25px; width: 164px">
                                        <asp:Label ID="lblCGPA" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                    <td style="width:120px; font-size: 11px; font-weight: normal; font-style: normal; color: #000000; font-family: Arial, Helvetica, sans-serif;" 
                                        >                                        
                                        Second Minor</td>
                                    <td style="height: 25px; width: 230px">
                                        <asp:Label ID="lblSecondMinor" runat="server" Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="td" colspan="2" style="width: 978px;">
                        Legends:
                        <asp:Label ID="Label1" runat="server" BackColor="GreenYellow" Text="Completed" 
                            ForeColor="Black"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label2" runat="server" BackColor="DarkKhaki" 
                            Text="Auto Assigned" ForeColor="Black"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label3" runat="server" BackColor="White" 
                            Text="Yet to be completed" ForeColor="Black"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label4" runat="server" BackColor="Thistle" Text="Open" 
                            ForeColor="Black"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label5" runat="server" BackColor="Tomato" ForeColor="Black" 
                            Text="Requisitioned"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan = "2" style="height: 25px;width: 978px;" class="td">
                        <asp:GridView ID="gvRoutine" runat="server" Width="970px" 
                            AutoGenerateColumns="False" 
                            onrowdatabound="gvRoutine_RowDataBound" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Trimester-Wise Distribution of Courses" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HighestGrade" HeaderText="Grade" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Retakes" HeaderText="Retakes" >
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField  HeaderText="Requisition">
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chk" runat="server" Visible = "false"  OnCheckedChanged = "chk_CheckedChanged" AutoPostBack="true"/>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                               </asp:TemplateField>                                        
                                <asp:TemplateField HeaderText="Select Section">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSection" runat="server" DataTextField = "SectionTime">
                                        </asp:DropDownList>                                 
                                    </ItemTemplate>                            
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                
						        <asp:BoundField DataField="DataObjTypeName" HeaderText="DataObjTypeName" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID" HeaderText="DataObjID" Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID2" HeaderText="DataObjID2" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataObjID3" HeaderText="DataObjID3(Node_CourseID)" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CanRetake" HeaderText="CanRetake" Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsOffered" HeaderText="IsOffered" Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsEligible" HeaderText="IsEligible" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasCompleted" HeaderText="HasCompleted" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PreRequisitDone" HeaderText="PreRequisitDone" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HasMultispan" HeaderText="HasMultispan" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MultiSpanMasID" HeaderText="MultiSpanMasID" 
                                    Visible="False" >
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan = "2" style="height: 25px;width: 978px;" class="td">
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan = "2">
                        <asp:Panel ID="pnlCourse" runat="server">

                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

