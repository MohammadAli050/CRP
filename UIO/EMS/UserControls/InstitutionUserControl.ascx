<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_InstitutionUserControl" Codebehind="InstitutionUserControl.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div >            
            <div> 
                <asp:DropDownList ID="ddlInstitution" Width="100%" Height="38px" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlInstitution_SelectedIndexChanged" AutoPostBack="true">                    
                </asp:DropDownList>
            </div>
        </div>       
    </ContentTemplate>
</asp:UpdatePanel>
