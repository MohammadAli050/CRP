<%@ Page Language="C#" AutoEventWireup="true"   EnableEventValidation = "false" CodeBehind="Registration.aspx.cs" 
Inherits="EMS.Registration.Registration" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
     <style type="text/css">
        .collapsePanel {
              width: 640px;
              height:0px;
              background-color:white;
              overflow:hidden;
        }
 
.collapsePanelHeader{   
      width:640px;            
      height:20px;
      color: Blue;
      background-color: #9966FF;
      font-weight:bold;
      float:none;
      padding:5px; 
      cursor: pointer; 
      vertical-align: middle;
}

  </style> 
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContainer" Runat="Server">
 

        <asp:UpdatePanel ID="UpdatePanelHeader" runat="server">
            <ContentTemplate>
                 <table width="100%" class = "tbcolor">
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
                                        
                                        <asp:TextBox ID="txtSearchStu" runat="server" Width="170px"></asp:TextBox>
                                        
                                        <asp:Button ID="btnSearchStu" runat="server" onclick="btnSearchStu_Click" 
                                            Text="Search" />
                                        
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
                                        <asp:Label ID="lblStdName" runat="server"  Font-Italic="True"></asp:Label>
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
                
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div style="height: 18px"></div>  
              
         <%--<asp:UpdatePanel ID="errorMessage" runat="server">
             <ContentTemplate>
                <div style="height: 18px"></div>        
                    <div style="height: 18px">
                        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </div>
                 <div style="height: 18px"></div>
             </ContentTemplate>
         </asp:UpdatePanel> --%>    
              
        
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                
                   <asp:Button ID="btnLoadSection" runat="server" Text="Load Section" 
                        onclick="btnLoadSection_Click"  />  
                        
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLoadCourse" runat="server" Text="Load Course" 
                        onclick="btnLoadCourse_Click" />
                        
                        &nbsp; &nbsp;
                        <asp:Button ID="btnUndoSection" runat="server" Text="Undo Section" 
                        onclick="btnUndoSection_Click" />
                        
                        <br />
                         <div>
                            <asp:GridView ID="GridViewSection" runat="server" 
                        AutoGenerateColumns="False" onrowdatabound="GridViewSection_RowDataBound" 
                        onselectedindexchanged="GridViewSection_SelectedIndexChanged" BackColor="#CCCCFF">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                                <asp:BoundField DataField="AcaCal_SectionID" HeaderText="AcaCal_SectionID" 
                                    Visible="False" />
                                <asp:BoundField DataField="TimeSlot_1" HeaderText="Time Slot (1)" />
                                <asp:BoundField DataField="DayOne" HeaderText="Day One" />
                                <asp:BoundField DataField="TimeSlot_2" HeaderText="Time Slot (2)" />
                                <asp:BoundField DataField="DayTwo" HeaderText="DayTwo" />
                                <asp:BoundField DataField="Faculty_1" HeaderText="Faculty (1)" />
                                <asp:BoundField DataField="Faculty_2" HeaderText="Faculty (2)" />
                                <asp:BoundField DataField="RoomNo_1" HeaderText="RoomNo (1)" />
                                <asp:BoundField DataField="RoomNo_2" HeaderText="RoomNo (2)" />
                                <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                                <asp:BoundField DataField="Occupied" HeaderText="Occupied" />
                            </Columns>
                                <HeaderStyle BackColor="#CC99FF" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" />
                        </asp:GridView>
                        </div>                       
                        <div>
                        
                            <asp:GridView ID="GridViewCourse" runat="server" AutoGenerateColumns="False" 
                                onrowdatabound="GridViewCourse_RowDataBound" 
                                onselectedindexchanged="GridViewCourse_SelectedIndexChanged" 
                                BackColor="#CCCCFF">
                                <Columns>
                                    <asp:TemplateField HeaderText="CourseID" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseID" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VersionID" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVersionID" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNode_CourseID" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formal Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormalCode" runat="server" Text='<%# Bind("FormalCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course Title">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#CC99FF" />
                            </asp:GridView>
                        
                        </div>
                        
                        
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1st" runat ="server">
            <ContentTemplate>               
                                      <div ></div>
                                      <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image1" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label1" runat="server" Text="Show Details (1st Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView1st" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView1st_RowDataBound" 
                                              onselectedindexchanged="GridView1st_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                             TargetControlID="Panel2"  
                                             CollapsedSize="0"  
                                              
                                             Collapsed="True"  
                                             ExpandControlID="Panel1"  
                                             CollapseControlID="Panel1"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label1"  
                                             CollapsedText="Show Details (1st Trimester)"  
                                             ExpandedText="Hide Details (1st Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
       <asp:UpdatePanel ID="UpdatePanel2nd" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel3" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image2" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label2" runat="server" Text="Show Details (2nd Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel4" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView2nd" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView2nd_RowDataBound" 
                                              onselectedindexchanged="GridView2nd_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Width="120px" Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                                             TargetControlID="Panel4"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel3"  
                                             CollapseControlID="Panel3"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label2"  
                                             CollapsedText="Show Details (2nd Trimester)"  
                                             ExpandedText="Hide Details (2nd Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel> 

        <asp:UpdatePanel ID="UpdatePanel3rd" runat ="server">
            <ContentTemplate>               
                                     
                                      <asp:Panel ID="Panel5" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image3" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label3" runat="server" Text="Show Details (3rd Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel6" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView3rd" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView3rd_RowDataBound" 
                                              onselectedindexchanged="GridView3rd_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"
                                             TargetControlID="Panel6"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel5"  
                                             CollapseControlID="Panel5"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label3"  
                                             CollapsedText="Show Details (3rd Trimester)"  
                                             ExpandedText="Hide Details (3rd Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel4th" runat ="server">
            <ContentTemplate>               
                                     
                                      <asp:Panel ID="Panel7" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image4" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label4" runat="server" Text="Show Details (4th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel8" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView4th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView4th_RowDataBound" 
                                              onselectedindexchanged="GridView4th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                                             TargetControlID="Panel8"  
                                             CollapsedSize="0"  
                                              
                                             Collapsed="True"  
                                             ExpandControlID="Panel7"  
                                             CollapseControlID="Panel7"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label4"  
                                             CollapsedText="Show Details (4th Trimester)"  
                                             ExpandedText="Hide Details (4th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel5th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel9" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image5" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label5" runat="server" Text="Show Details (5th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel10" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView5th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView5th_RowDataBound" 
                                              onselectedindexchanged="GridView5th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server" Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server" Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server"
                                             TargetControlID="Panel10"  
                                             CollapsedSize="0"  
                                              
                                             Collapsed="True"  
                                             ExpandControlID="Panel9"  
                                             CollapseControlID="Panel9"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label5"  
                                             CollapsedText="Show Details (5th Trimester)"  
                                             ExpandedText="Hide Details (5th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel6th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel11" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image6" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label6" runat="server" Text="Show Details (6th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel12" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView6th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView6th_RowDataBound" 
                                              onselectedindexchanged="GridView6th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server"
                                             TargetControlID="Panel12"  
                                             CollapsedSize="0"  
                                             
                                             Collapsed="True"  
                                             ExpandControlID="Panel11"  
                                             CollapseControlID="Panel11"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label6"  
                                             CollapsedText="Show Details (6th Trimester)"  
                                             ExpandedText="Hide Details (6th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel7th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel13" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image7" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label7" runat="server" Text="Show Details (7th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel14" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView7th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView7th_RowDataBound" 
                                              onselectedindexchanged="GridView7th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server"
                                             TargetControlID="Panel14"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel13"  
                                             CollapseControlID="Panel13"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label7"  
                                             CollapsedText="Show Details (7th Trimester)"  
                                             ExpandedText="Hide Details (7th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel8th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel15" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image8" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label8" runat="server" Text="Show Details (8th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel16" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView8th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView8th_RowDataBound" 
                                              onselectedindexchanged="GridView8th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"   Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" runat="server"
                                             TargetControlID="Panel16"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel15"  
                                             CollapseControlID="Panel15"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label8"  
                                             CollapsedText="Show Details (8th Trimester)"  
                                             ExpandedText="Hide Details (8th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel9th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel17" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image9" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label9" runat="server" Text="Show Details (9th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel18" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView9th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView9th_RowDataBound" 
                                              onselectedindexchanged="GridView9th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="server"
                                             TargetControlID="Panel18"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel17"  
                                             CollapseControlID="Panel17"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label9"  
                                             CollapsedText="Show Details (9th Trimester)"  
                                             ExpandedText="Hide Details (9th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel10th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel19" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image10" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label10" runat="server" Text="Show Details (10th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel20" runat="server" CssClass="collapsePanel" Width="100%">
                                            <asp:GridView ID="GridView10th" 
            runat="server" AutoGenerateColumns="False" onrowdatabound="GridView10th_RowDataBound" 
                                                onselectedindexchanged="GridView10th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" 
                                                              Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" 
                                                              Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" 
                                                              Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" 
                                                              Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"   
                                                              Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" 
                                                              Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  
                                                              Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" 
                                                              Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" 
                                                              Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                                <SelectedRowStyle BackColor="#C6D5FB" />
                                                <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender10" runat="server"
                                             TargetControlID="Panel20"  
                                             CollapsedSize="0"  
                                              
                                             Collapsed="True"  
                                             ExpandControlID="Panel19"  
                                             CollapseControlID="Panel19"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label10"  
                                             CollapsedText="Show Details (10th Trimester)"  
                                             ExpandedText="Hide Details (10th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel11th" runat ="server">
            <ContentTemplate>               
                                      
                                      <asp:Panel ID="Panel21" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image11" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label11" runat="server" Text="Show Details (11th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel22" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView11th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView11th_RowDataBound" 
                                              onselectedindexchanged="GridView11th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"   Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender11" runat="server"
                                             TargetControlID="Panel22"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel21"  
                                             CollapseControlID="Panel21"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label11"  
                                             CollapsedText="Show Details (11th Trimester)"  
                                             ExpandedText="Hide Details (11th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
         <asp:UpdatePanel ID="UpdatePanel12th" runat ="server">
            <ContentTemplate>               
                                     
                                      <asp:Panel ID="Panel23" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image12" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label12" runat="server" Text="Show Details (12th Trimester)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel24" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridView12th" runat="server" AutoGenerateColumns="False" 
                                              onrowdatabound="GridView12th_RowDataBound" 
                                              onselectedindexchanged="GridView12th_SelectedIndexChanged">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server" Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server"  Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender12" runat="server"
                                             TargetControlID="Panel24"  
                                             CollapsedSize="0"  
                                               
                                             Collapsed="True"  
                                             ExpandControlID="Panel23"  
                                             CollapseControlID="Panel23"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label12"  
                                             CollapsedText="Show Details (12th Trimester)"  
                                             ExpandedText="Hide Details (12th Trimester)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                      
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
         <asp:UpdatePanel ID="UpdatePanelMultySpan" runat ="server">
            <ContentTemplate>               
                                      <div ></div>
                                      <asp:Panel ID="Panel25" runat="server" CssClass="collapsePanelHeader" 
                                          Width="100%">
                                           <asp:Image ID="Image13" runat="server" ImageUrl="../Images/expand_blue.jpg" ></asp:Image>                                        
                                            <asp:Label ID="Label13" runat="server" Text="Show Details (Multy Span)" ></asp:Label>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="Panel26" runat="server" CssClass="collapsePanel" Width="100%">
        
                                          <asp:GridView ID="GridViewMultySpan" runat="server" AutoGenerateColumns="False">
                                              <Columns>
                                                  <asp:TemplateField HeaderText="ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblSccpnId" runat="server" 
                                                              Text='<%# Bind("ID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Student ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("StudentID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalCourseProgNodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox20" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCcpnID" runat="server" 
                                                              Text='<%# Bind("CalCourseProgNodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Completed" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("IsCompleted") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsComplete" runat="server" 
                                                              Checked='<%# Bind("IsCompleted") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Original Cal ID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblOriginalCalId" runat="server" Text='<%# Bind("OriginalCalID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Assign" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("IsAutoAssign") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoAssign" runat="server" 
                                                              Checked='<%# Bind("IsAutoAssign") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Auto Open" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IsAutoOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsAutoOpen" runat="server" 
                                                              Checked='<%# Bind("IsAutoOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Requisitioned">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox21" runat="server" 
                                                              Text='<%# Bind("Isrequisitioned") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsRequisition" runat="server" 
                                                              Checked='<%# Bind("Isrequisitioned") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsMandatori" runat="server" 
                                                              Checked='<%# Bind("IsMandatory") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IsManualOpen" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("IsManualOpen") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:CheckBox ID="chkIsManualOpen" runat="server" 
                                                              Checked='<%# Bind("IsManualOpen") %>' />
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarDetailID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox24" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderDetailId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarDetailID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeCalendarMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox25" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeCalenderMasterId" runat="server" 
                                                              Text='<%# Bind("TreeCalendarMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TreeMasterID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblTreeMasterId" runat="server" Text='<%# Bind("TreeMasterID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarMasterName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox27" runat="server" 
                                                              Text='<%# Bind("CalendarMasterName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalMasterName" runat="server" Text='<%# Bind("CalendarMasterName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CalendarDetailName" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox28" runat="server" 
                                                              Text='<%# Bind("CalendarDetailName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCalDetailName" runat="server" Text='<%# Bind("CalendarDetailName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("VersionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionId" runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node CourseID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeCourseId" runat="server" Text='<%# Bind("Node_CourseID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="NodeID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("NodeID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodeId" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Node Link Name"  Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblNodelinkName" runat="server" Text='<%# Bind("NodeLinkName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Code">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCode" runat="server"  Text='<%# Bind("FormalCode") %>'></asp:TextBox>
                                                          <br />                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="VersionCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("VersionCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblVersionCode" runat="server" Text='<%# Bind("VersionCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Title">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseTitle" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal SectionID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox10" runat="server" 
                                                              Text='<%# Bind("AcaCal_SectionID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalSecId" runat="server" Text='<%# Bind("AcaCal_SectionID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Section Name" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtSectionName" runat="server" 
                                                              Text='<%# Bind("SectionName") %>'></asp:TextBox>
                                                          <br />
                                                          
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ProgramID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("ProgramID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblProgramId" runat="server" Text='<%# Bind("ProgramID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DeptID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("DeptID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Priority" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Retake No">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtRetakeNo" runat="server" Text='<%# Bind("RetakeNo") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Obtained GPA">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblObtainGpa" runat="server" Text='<%# Bind("ObtainedGPA") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtGrade" runat="server" Width="60px" Text='<%# Bind("ObtainedGrade") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcademicCalenderID" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox18" runat="server" 
                                                              Text='<%# Bind("AcademicCalenderID") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalId" runat="server" Text='<%# Bind("AcademicCalenderID") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCalYear">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalYear" runat="server" Text='<%# Bind("AcaCalYear") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="BatchCode" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("BatchCode") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblBatchCode" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="AcaCal Unit Type Name">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="lblAcaCalUnitTypName" runat="server" Text='<%# Bind("AcaCalTypeName") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Completed Credit">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox36" runat="server" Text='<%# Bind("CompletedCredit") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCompletedCredit" runat="server" Text='<%# Bind("CompletedCredit") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Course Credit">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox37" runat="server" Text='<%# Bind("CourseCredit") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:TextBox ID="txtCourseCredit" runat="server" Text='<%# Bind("CourseCredit") %>'></asp:TextBox>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                              </Columns>
                                              <SelectedRowStyle BackColor="#C6D5FB" />
                                              <HeaderStyle BackColor="#CCCCCC" />
                                          </asp:GridView>
                                          
                                          <div style="padding: 10px 0px 0px 0px">
                                             <asp:Button ID="btnSaveMultySpan" runat="server" Text="Save Project/Thesis" onclick="btnSaveMultySpan_Click" />
                                         </div>
                                      </asp:Panel>
                                      
                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender13" runat="server"
                                             TargetControlID="Panel26"  
                                             CollapsedSize="0"  
                                              
                                             Collapsed="True"  
                                             ExpandControlID="Panel25"  
                                             CollapseControlID="Panel25"  
                                             AutoCollapse="False"  
                                             AutoExpand="False"  
                                             ScrollContents="True"  
                                             TextLabelID="Label13"  
                                             CollapsedText="Show Details (Multy Span)"  
                                             ExpandedText="Hide Details (Multy Span)"   
                                             ImageControlID="Image1"
                                             ExpandedImage="../Images/collapse_blue.jpg"
                                             CollapsedImage="../Images/expand_blue.jpg"                                            
                                             ExpandDirection="Vertical" /> 
                                        
            
            </ContentTemplate>
        </asp:UpdatePanel>

          <div> </div> 
         
         
         
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
                <div style="float: left">
                     <asp:Button ID="btnSaveRequisition" runat="server" Text="Save Requisition" 
                         onclick="btnSaveRequisition_Click" />
                 </div> 
                 <div style="float: right">
                     <asp:Button ID="btnRegistration" runat="server" Text="Registration" onclick="btnRegistration_Click" 
                          />
                 </div>
         </div>
    </ContentTemplate>
    </asp:UpdatePanel>                               
                                      
</asp:Content>
