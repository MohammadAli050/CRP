<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_TreeUserControl" Codebehind="TreeUserControl.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="float:left; width:200px; height:25px">           
            <div style="float:left; width:130px;"> 
                <asp:DropDownList ID="ddlTree" Width="100%" Height="38px" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlTree_SelectedIndexChanged" AutoPostBack="true">                    
                </asp:DropDownList>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
