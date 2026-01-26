<%@ Page Language="C#" AutoEventWireup="true" Inherits="StudentManagement_PreCourseAdd" Codebehind="PreCourseAdd.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <script type="text/javascript">
     window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <div style="float: left; margin: 5px;">
                    <asp:Label ID="lblPreCourse" runat="server" Text="Pre Course" Width="150px"> </asp:Label>
                    <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlPreCourse" Width="350px" />
                </div>
                <div style="clear: both"></div>
                <div style="float: left; margin: 5px;">
                    <asp:Label ID="lblMainCourse" runat="server" Text="Main Course" Width="150px"> </asp:Label>
                    <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlMainCourse" Width="350px" />
                </div>
                <div style="clear: both"></div>
                 <div style="float: left; margin: 5px;">
                    <asp:Label ID="Label1" runat="server" Text="Is Bundle Course" Width="150px"> </asp:Label>
                    <asp:CheckBox runat="server" class="dropdownList-size" ID="chkIsbundle" Width="350px" />
                </div>
                <div style="clear: both"></div>
                <div style="float: left; margin: 5px;">
                    <asp:Button runat="server" ID="btnAdd" Text="Add Pre Course" OnClick="btnAdd_Click" />
                </div>
            </div>
            <div style="clear: both"></div>
            <div>
                <asp:GridView runat="server" ID="gvPreCourse" AutoGenerateColumns="False"
                    AllowPaging="true" PagerSettings-Mode="NumericFirstLast"
                    PageSize="20" CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen" />
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Pre Course" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPreCourse" Text='<%#Eval("PreCourse") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Main Course" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMainCourse" Text='<%#Eval("MainCourse") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Is Bundle" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblisBundle" Text='<%#Eval("IsBundle") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text=" ● Delete"
                                    ToolTip="Add section." CommandArgument='<%#Eval("StudentPreCourseId") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="7%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
