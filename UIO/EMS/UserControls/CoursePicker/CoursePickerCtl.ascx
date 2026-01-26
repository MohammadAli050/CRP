<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_CoursePicker_CoursePickerCtl" Codebehind="CoursePickerCtl.ascx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<style type="text/css">
    .style1
    {
    	height: 16px;
        width: 200px;
    }
    .style2
    {
    	height: 16px;
        }
    .style3
    {
    	height: 16px;
        Width: 50px;
    }
</style>


    <script type="text/javascript">
	    function OpenModal()
	    {
	        window.showModalDialog("../UserControls/CoursePicker/CoursePickerBox.aspx", "", "");
	        //window.showModalDialog("~/UserControls/CoursePicker/CoursePickerBox.aspx", "", "");
		}
    </script>

<table class="style1" style="border: 1px solid #808080" >
    <tr>
        <td class="style2" colspan="2">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
            </asp:ScriptManagerProxy>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="lblCouseTitle" runat="server" Width="150px" Height="16px"></asp:Label>
        </td>
        <td class="style3">

            <asp:Button ID="btnPick" runat="server" onclick="btnPick_Click" Text="Pick" 
                Width="50px" Height="16px"/>

        </td>
    </tr>
</table>




