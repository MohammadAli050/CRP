<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserControls_CoursePicker_CoursePickerBox" Codebehind="CoursePickerBox.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Picker</title>
    <base target="_self" />
	<script type="text/javascript">
	function CloseModal()
	{
		window.close();
	}
	
	</script>
	
	<style type="text/css">
	    .style1 
	    {
		    color: #0000FF;
	    }
	    .style2
        {
            width: 109px;
        }
        .tr
        {
        	border: 1px solid #808080;
        }
        
	    .style3
        {
            border: 1px solid #808080;
            height: 258px;
        }
        
	    .style4
        {
            height: 26px;
            width: 513px;
        }
        
	    .style5
        {
            height: 123px;
            width: 513px;
        }
        .style6
        {
            width: 513px;
            height: 328px;
        }
        
	</style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<table style="width: 47%; height: 520px; border: 1px solid #0000FF;">
			<tr>
				<td style="border: 1px solid #808080; " class="style4">
				    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                </td>
			</tr>
			<tr>
				<td class="style5">
				<fieldset id="gbxSrchparam" 
                        style="border: 1px solid #FF0000; height: 136px; width: 469px;">
				<legend style="color: #0000FF">Search Criteria</legend>
				<table style="border-left: 1px none #FF0000; border-right: 1px none #FF0000; border-top: 1px solid #FF0000; border-bottom: 1px none #FF0000; width: 99%; height: 63%; ">
					<tr>
						<td style="border: 1px solid #808080; " class="style2">
							<label id="lblFormal">Formal Code</label>
						</td>
						<td style="border: 1px solid #808080;">
						<asp:TextBox id="txtFormalCode" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td style="border: 1px solid #808080; " class="style2">							
							<label id="lblVersion">Version Code</label>
						</td>
						<td style="border: 1px solid #808080;">
						<asp:TextBox id="txtVersionCode" runat="server"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td style="border: 1px solid #808080; " class="style2">
							<label id="lblTitle">Title</label>
						</td>
						<td style="border: 1px solid #808080;">
						<asp:TextBox id="txtTitle" runat="server" Width="333px"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td align="left" class="style2">
                            <asp:ImageButton ID="btnFind" runat="server" AlternateText="Find" 
                                BackColor="#0033CC" ImageUrl="~/ButtonImages/Find.jpg" 
                                onclick="btnFind_Click" />
                        </td>
						<td align="left">
                            <asp:ImageButton ID="btnAll" runat="server" AlternateText="Show All" 
                                BackColor="#0033CC" ImageUrl="~/ButtonImages/ShowAll.jpg" 
                                onclick="btnAll_Click" />
                        </td>
					</tr>
				</table>
				</fieldset></td>
			</tr>
			<tr>
				<td class="style6">
				<fieldset id="gbxResults" style="border: 1px solid #FF0000; height: 327px;">
				<legend class="style1">Results</legend>
				<table style="border-left: 1px none #FF0000; border-right: 1px none #FF0000; border-top: 1px solid #FF0000; border-bottom: 1px none #FF0000; width: 100%; height: 68%;">
					<tr>
						<td class="style3" colspan="2">
                            <asp:Panel ID="pnlResult" runat="server" Height="97%" ScrollBars="Horizontal" 
                                Width="100%">
                                <asp:DataGrid ID="grdResult" runat="server" 
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="152px" 
                                    onselectedindexchanged="grdResult_SelectedIndexChanged">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                                    <ItemStyle BackColor="White" ForeColor="#003399" />
                                    <Columns>
                                        <asp:ButtonColumn CommandName="Select" HeaderText="Pick" Text="Select"></asp:ButtonColumn>
                                        <asp:BoundColumn HeaderText="Formal Code"  DataField="FormalCode" ReadOnly="True"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Version Code" DataField="VersionCode" ReadOnly="True"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Title" DataField="Title" ReadOnly="True">
                                            <HeaderStyle Width="275px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VersionID" Visible="false"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                </asp:DataGrid>
                            </asp:Panel>
                        </td>
					</tr>
					<tr>
						<td align="left" style="width: 249px">
                            <asp:Button ID="btnOK" runat="server" Text="Ok" onclick="btnOK_Click" 
                                onclientclick="CloseModal()" />
                        </td>
						<td align="right">
                            <asp:ImageButton ID="btnClose" runat="server" AlternateText="Close" 
                                BackColor="#0033CC" ImageUrl="~/ButtonImages/Cancel.jpg" 
                                onclick="btnClose_Click" />
                        </td>
					</tr>
				</table>
				</fieldset></td>
			</tr>
		</table>
    
    </div>
    </form>
</body>
</html>
