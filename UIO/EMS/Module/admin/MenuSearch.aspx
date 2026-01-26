<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="MenuSearch.aspx.cs" Inherits="EMS.miu.admin.MenuSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Menu Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style>
        .msgPanel {
            margin-top: 10px;
            border: 1px solid #008080;
            padding: 5px;
            width: 888px;
        }

        .tbl-width-lbl-Small {
            width: 70px;
            padding: 5px;
        }

        .tbl-width-lbl-Large {
            width: 150px;
            padding: 5px;
        }

        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
    </style>

    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div style="padding: 10px; width: 1200px;">
        <div class="PageTitle">
            <label>Menu Search</label>
        </div>

        <div class="Message-Area" style="height: 35px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 70%;">
                        <tr>
                            <td class="tbl-width-lbl"><b>Menu</b></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMenuName" Width="80%"></asp:TextBox>
                            </td>
                            
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Height="30px" Width="70px" BackColor="#edd366" />
                            </td>
                        </tr>

                    </table>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>
                        <%--<div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">                            
                        </div>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <%--<div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnSearch" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnSearch" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <div style="clear: both;"></div>
        <div>
            <div style="padding: 1px; margin-top: 20px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        
                        <div style="clear: both;"></div>
                        <asp:GridView ID="gvMenuList" runat="server" AutoGenerateColumns="false" ShowFooter="true" Width="100%">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                                        <AlternatingRowStyle BackColor=" #F9F99F" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="40px" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblName" Font-Bold="true" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="300px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="URL">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%#Eval("URL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="300px" />
                                            </asp:TemplateField>

                                         
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:LinkButton runat="server" ToolTip="Go" Text="Go to Page" ID="lnkGo">
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <RowStyle Height="30px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <EmptyDataTemplate>
                                            No data found!
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>