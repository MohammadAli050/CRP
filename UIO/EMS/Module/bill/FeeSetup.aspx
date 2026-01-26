<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_FeeSetup" Codebehind="FeeSetup.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Fee Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="margin: 10px;">
<%--        <fieldset>
            <legend style="font-weight: bold; font-size: medium;">Late File</legend>--%>
            <div class="PageTitle">
            <label>Fee Setup :: Program And Batch Wise</label>
        </div>
        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both;"></div>
            <div style="clear: both;"></div>
            <div style="margin: 10px; width: 100%;">
                <div style="float: left;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--<div style="clear: both;"></div>--%>
                            <div style="float: left; width: 900px">
                                <div style="padding: 5px; float: left;">
                                    <div style="width: 100px; float: left;">Program</div>
                                    <div style="width: 140px; float: left;">
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                    </div>
                                </div>

                                <div style="padding: 5px; float: left;">
                                    <div style="width: 100px; float: left;">Batch</div>
                                    <div style="width: 140px; float: left;">
                                        <uc1:BatchUserControl runat="server" ID="ucBatch"  class="margin-zero dropDownList"/>
                                    </div>
                                </div>

                                <div style="padding: 5px; float: left;">
                                    <div style="width: 100px; float: left;"></div>
                                    <div style="width: 140px; float: left;">
                                        <uc1:SessionUserControl Visible ="false" runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                    </div>
                                </div>
                                 <div style="padding: 5px; float: left;">
                                      <div style="width: 100px; float: left;">
                                    <asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" Text="Load" ValidationGroup="VG"></asp:Button>
                                </div>
                                 </div>
                            </div>
                            <div style="clear: both;"></div>                             
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="clear: both;"></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="Grid-Button" style="height: 30px; margin-top:10px; width:390px;">
                                <div style="float: left;">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                                    <asp:Label ID="lblCourseCount" runat="server" Font-Bold="true"></asp:Label>
                                </div> 
                                <div style="width: 100px; float: right;">
                                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"></asp:Button>
                                </div>                                
                            </div>
                            <asp:GridView runat="server" ID="gvFeeSetup" AutoGenerateColumns="False" 
                                ShowHeader="true" >
                                <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#FFFFCC" />
                                <Columns>                                   
                                     <asp:TemplateField HeaderText="Fee Type">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFeeSetupID" runat="server" Value='<%#Eval("FeeSetupID") %>' />
                                            <asp:HiddenField ID="hdmAcaCalID" runat="server" Value='<%#Eval("AcaCalID") %>' />
                                            <asp:HiddenField ID="hdmBatchID" runat="server" Value='<%#Eval("BatchID") %>' />
                                            <asp:HiddenField ID="hdnProgramID" runat="server" Value='<%#Eval("ProgramID") %>' />
                                            <asp:HiddenField ID="hdnTypeDefID" runat="server" Value='<%#Eval("TypeDefID") %>' />

                                            <asp:Label runat="server" ID="lblType" Text='<%#Eval("Type.Definition") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Height="30px" Text="Update"
                                            OnClick="btnUpdate_Click" Font-Bold="True" />
                                            <hr />
                                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                                    </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtAmount" Text='<%#Eval("Amount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>                                    
                                </Columns>
                                <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    No data found!
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        <%--</fieldset>--%>
    </div>
</asp:Content>

