<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_SessionUserControl" Codebehind="SessionUserControl.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>           
            <div> 
                <asp:DropDownList ID="ddlSession" Width="100%" Height="38px" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">                    
                </asp:DropDownList>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
