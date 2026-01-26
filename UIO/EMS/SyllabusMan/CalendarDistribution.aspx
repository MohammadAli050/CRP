<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="CalenderDistribution" Codebehind="CalendarDistribution.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
    <style type="text/css">
        .table {
            border: 1px solid #008080;
        }

       
        .style10
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            line-height: 24px;
            color: #333333;
            vertical-align: Middle;
            width: 100%;
            height: 100px;
        }
        .style11
        {
            height: 28px;
        }
            
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
                
        .dxeButtonEdit
        {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
        .style12
        {
            border: 1px solid Blue;
            font: 11px Arial, Helvetica, sans-serif;
            color: #666666;
            vertical-align: Middle;
            width: 27%;
        }
        .auto-style2 {
            width: 35%;
        }
        .auto-style3 {
            width: 19%;
        }
        </style>
    <script type="text/javascript">
        function isNumber(e) {
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode
            status = charCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
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
            <table class="table" style="width:983px; height: 665px;">
		        <tr>
			        <td class="tr" style="width: 100%; height:7px;" colspan="2" valign="middle">
			            <table style="width: 100%;">
                            <tr>
                                <td class="td" style="width: 20%">
                                    <asp:Label ID="lblPageName" runat="server" Font-Bold="True" ForeColor="#000066" 
                                        Text="Course distribution"></asp:Label>
                                </td>
                                <td class="td" style="width: 80%">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
			        </td>
		        </tr>
		        <tr>
			        <td class="td" style="height: 105px;vertical-align:top; width: 100%;" colspan="2">

			            <asp:Panel ID="pnlSrchCriteria" runat="server" Height="100px" Width="100%">
                            <table  class="table" style="width: 100%; height: 99px;">
                                <tr>
                                    <td align="right" class="td" style="width: 112px;height: 30px;vertical-align: top;">
                                        <asp:Label ID="lblPrograms" runat="server" Text="Programs:"></asp:Label>
                                    </td>
                                    <td class="td" colspan="6" style="width: 112px;height: 30px; text-align: left;" 
                                        align="left">
                                        <asp:DropDownList ID="ddlPrograms" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlPrograms_SelectedIndexChanged" TabIndex="1" 
                                            Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="tr">
                                    <td align="right" class="td" style="width: 112px;height: 30px;vertical-align: top;">
                                        <asp:Label ID="lblTrees" runat="server" Text="Trees:"></asp:Label>
                                    </td>
                                    <td class="td" colspan="6" style="height: 30px;" align="left">
                                        <asp:DropDownList ID="ddlTreeMasters" runat="server" AutoPostBack="True" 
                                            Enabled="False" onselectedindexchanged="ddlTreeMasters_SelectedIndexChanged" 
                                            TabIndex="2" Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="td" style="width: 112px;height: 30px;vertical-align: top;">
                                        <asp:Label ID="lblCalender" runat="server" Text="Linked Calendars:"></asp:Label>
                                    </td>
                                    <td class="td" style="width: 272px;height: 30px;vertical-align: middle;">
                                        <asp:DropDownList ID="ddlLinkedCalendars" runat="server" AutoPostBack="True" 
                                            Enabled="False" 
                                            onselectedindexchanged="ddlLinkedCalendars_SelectedIndexChanged" TabIndex="3" 
                                            Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnAddCalender" runat="server" 
                                            ImageUrl="~/ButtonImages/AddCalender.jpg" onclick="btnAddCalender_Click" 
                                            TabIndex="5" ToolTip="Link a new calendar with the selected tree." />
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnAddNode" runat="server" 
                                            ImageUrl="~/ButtonImages/AddNode.jpg" onclick="btnAddNode_Click" TabIndex="6" 
                                            ToolTip="Add a new node under the selected calendar detail." />
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnAddCourse" runat="server" 
                                            ImageUrl="~/ButtonImages/AddCourse.jpg" onclick="btnAddCourse_Click" 
                                            TabIndex="7" ToolTip="Add a new course under the selected calendar detail." />
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/ButtonImages/Edit.jpg" 
                                            onclick="btnEdit_Click" TabIndex="8" 
                                            ToolTip="Select a linked Node or Course and click this button to edit it." />
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnDelete" runat="server" 
                                            ImageUrl="~/ButtonImages/Delete.jpg" onclick="btnDelete_Click" TabIndex="9" 
                                            ToolTip="Click this button to delete a selected calendar link, node link, course link." />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

			        </td>
		        </tr>
		        <tr>
			        <td class="td" style="width: 462px;height: 371px;vertical-align:top;" valign="top">
			            <asp:Panel ID="pnlCalendar" runat="server" Height="462px" ScrollBars="Vertical" 
                            Width="496px">
                            <asp:TreeView ID="tvwCalendar" runat="server" Width="100%" ShowLines="True"
                                Height="98%" onselectednodechanged="tvwCalendar_SelectedNodeChanged"
                                LineImagesFolder="~/TreeLineImages" TabIndex="4">
                                <ParentNodeStyle ForeColor="#660066" />
                                <HoverNodeStyle ForeColor="#FF6600" />
                                <SelectedNodeStyle ForeColor="Lime" Font-Bold="True" Font-Underline="True"/>
                                <RootNodeStyle ForeColor="#000066" />
                                <LeafNodeStyle ForeColor="#006699" />
                            </asp:TreeView>
                        </asp:Panel>
			        </td>
			        <td class="td" align="center">
			                <asp:Panel ID="pnlControl" runat="server" Height="366px" Width="460px" 
                                Visible="False">                            
                                    <asp:Panel ID="pnlLinkCal" runat="server" Height="120px" Width="98%" 
                                        Visible="False" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                                        <table class="table" style="width:100%; height: 100%;">
                                            <tr>
                                                <td class="style10" valign ="middle" >
                                	                <fieldset id="gbxLinkcal" style="border: 1px solid #0000FF; height: 90px; width:400px;">
									                <legend style="color: #000080">Link calendar</legend>
									                    <table style="width: 100%">
										                    <tr>
											                    <td class="td" style="width: 20%" align="right">
											                        <asp:RequiredFieldValidator ID="rvCalLinkName" runat="server" 
                                                                        ErrorMessage="Calendar Link Name can not be empty" 
                                                                        ControlToValidate="txtCalName" ValidationGroup="ValidateDistrib">*</asp:RequiredFieldValidator>
											                        <asp:Label ID="lblCalLinkName" runat="server" Text="Name:"></asp:Label>
                                                                </td>
											                    <td class="td" style="width: 60%" align="left">
											                        <asp:TextBox ID="txtCalName" runat="server" TabIndex="10" Width="186px"></asp:TextBox>
                                                                </td>
										                    </tr>
										                    <tr>
											                    <td align="right" class="td" style="width: 20%">
											                    <asp:label ID="lblCalLinker" runat="server" Text="Calendars:"></asp:label>
											                    </td>
											                    <td class="td" style="width: 60%" align="left">
											                    <asp:dropdownlist ID="ddlCalendars" runat="server" AutoPostBack="True" Width="95%" 
                                                                        TabIndex="11">
											                    </asp:dropdownlist>
											                    </td>
										                    </tr>
									                    </table>
									                </fieldset>
									            </td>
									        </tr>
									     </table>
								    </asp:Panel>

                                    <asp:Panel ID="pnlNodes" runat="server" Height="150px" Width="98%" 
                                        Visible="False" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                                         <table class="table" style="width:100%; height: 100%;">
                                            <tr>
                                                <td valign ="middle">
                                	                <fieldset id="gbxAddNode" style="border: 1px solid #0000FF; height: 130px; width:400px;">
									                <legend style="color: #000080">Link node</legend>
									                    <table style="width: 100%; height: 125px;">
										                    <tr>
											                    <td class="td" style="width: 20%" align="right">
											                        <asp:RequiredFieldValidator ID="rvNodLinkName" runat="server" 
                                                                        ErrorMessage="Node Link Name can not be empty" 
                                                                        ControlToValidate="txtNodeLinkName" ValidationGroup="ValidateDistrib">*</asp:RequiredFieldValidator>
											                        <asp:Label ID="lblNodeLinkName" runat="server" Text="Name:"></asp:Label>
                                                                </td>
											                    <td class="td" style="width: 60%" align="left">
											                        <asp:TextBox ID="txtNodeLinkName" runat="server" TabIndex="12" Width="186px">
											                        </asp:TextBox>
                                                                </td>
										                    </tr>
									                        <tr>
                                                                <td align="right" class="td" style="width: 20%">
                                                                    <asp:Label ID="lblLinkNode" runat="server" Text="Nodes:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 60%">
                                                                    <asp:DropDownList ID="ddlNodes" runat="server" AutoPostBack="True" 
                                                                        TabIndex="13" Width="95%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="td" style="width: 20%">
                                                                    <asp:Label ID="lblPriorityNode" runat="server" Text="Priority:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 60%">                                                                
                                                                    <asp:TextBox ID="txtNodePriority" runat="server" MaxLength="6" 
                                                                            onkeypress="return isNumber(event)" TabIndex="14"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="right" class="auto-style3">
                                                                                <asp:Label ID="Label1" runat="server" Text="Cr. to Complete:"></asp:Label>
                                                                            </td>
                                                                            <td align="left" class="auto-style2">                                                                
                                                                                <asp:TextBox ID="txtNodeCredits" runat="server" MaxLength="6" 
                                                                                        onkeypress="return isNumber(event)" TabIndex="15"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                <asp:CheckBox ID="chkMajorRelated" Text = "Major Type" runat="server" 
                                                                                    TabIndex="16" />
                                                                                <asp:CheckBox ID="chkMinorRelated" Text = "Minor Type" runat="server" 
                                                                                    TabIndex="16" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
									                    </table>
									                </fieldset>
											    </td>
									        </tr>
									     </table>
								    </asp:Panel>

                                    <asp:Panel ID="pnlCourse" runat="server" Height="150px" Width="98%" 
                                        Visible="False" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                                          <table class="table" style="width:100%; height: 100%;">
                                            <tr>
                                                <td valign ="middle">
                                	                <fieldset id="gbxAddCourse" style="border: 1px solid #0000FF; height: 100px; width:400px;">
									                <legend style="color: #000080">Link course</legend>
									                    <table class="table" style="width: 100%; height: 90px;">
										                    <tr>
											                    <td class="td" style="width: 25%" align="right">
											                        <asp:Label ID="lblAddCourse" runat="server" Text="Courses:"></asp:Label>
                                                                </td>
											                    <td class="td" style="width: 60%" align="left">
											                        <asp:DropDownList ID="ddlCourses" runat="server" Width="95%" 
                                                                        AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlCourses_SelectedIndexChanged" TabIndex="17">
                                                                    </asp:DropDownList>
                                                                </td>
										                    </tr>
										                    <tr>
                                                                <td style="width: 25%" align="right">
                                                                    <asp:Label ID="lblPriorityCourse" runat="server" Text="Priority:"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCoursePriority" runat="server" MaxLength="6" 
                                                                            onkeypress="return isNumber(event)" TabIndex="18"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="td" style="width: 25%" align="right">
                                                                    <asp:Label ID="Label2" runat="server" Text="Cr. to Complete:"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 60%">                                                                
                                                                    <asp:TextBox ID="txtCourseCredits" ReadOnly = "true" runat="server" 
                                                                        MaxLength="6" TabIndex="19"></asp:TextBox>
                                                                </td>
                                                            </tr>         										                    
									                    </table>
									                </fieldset>								
                                  			    </td>
									        </tr>
									     </table>
								    </asp:Panel>
                                    <table class="table" style="border: 1px solid #808080; width:99%; height: 10px;">  
                                    <tr>
                                        <td align="left" class="style11">
                                            <asp:ImageButton ID="btnSave" runat="server" 
                                                ImageUrl="~/ButtonImages/Save.jpg" onclick="btnSave_Click" 
                                                ToolTip="Click this button save the information displayed on the interface" 
                                                TabIndex="30" ValidationGroup="ValidateDistrib" />
                                        </td>
                                        <td class="style11">
                                            </td>
                                        <td align="right" class="style11">
                                            <asp:ImageButton ID="btnClose" runat="server" 
                                                ImageUrl="~/ButtonImages/Cancel.jpg" onclick="btnClose_Click" 
                                                ToolTip="Click this button to clear the interface." TabIndex="33" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
			        </td>
		        </tr>
		        <tr>
			        <td class="tr" style="width: 100%; height:7px;" colspan="2" valign="middle">			            
			            <asp:ValidationSummary ID="vsDistribution" runat="server" 
                            ValidationGroup="ValidateDistrib" />			            
			        </td>
		        </tr>
	        </table>
	    </ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

