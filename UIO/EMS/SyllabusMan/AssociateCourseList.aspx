<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="AssociateCourseList.aspx.cs" Inherits="EMS.SyllabusMan.AssociateCourseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <div style="margin:10px;">
        <fieldset>
            <legend style="font-weight:bold; font-size:medium;">All Associate Course 
            </legend>

            <div style="margin:10px; width:900px; font-size:medium">
                <asp:GridView runat="server" ID="gvAssociateCourseList" AutoGenerateColumns="False"
                    ShowHeader="true" CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>

                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Credits">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCredits" Text='<%#Eval("Credits") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Associate Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAssociateCode" Text='<%#Eval("AssociateCourse.FormalCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Associate Title">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAssociateTitle" Text='<%#Eval("AssociateCourse.Title") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Associate Credits">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAssociateCredits" Text='<%#Eval("AssociateCourse.Credits") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>

                    </Columns>
                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>
