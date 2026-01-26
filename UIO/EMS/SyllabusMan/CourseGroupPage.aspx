<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="CourseGroupPage.aspx.cs" Inherits="EMS.SyllabusMan.CourseGroupPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server"> 
    <div style="margin: 10px;">
        <fieldset style="width:410px">
            <legend style="font-weight: bold; font-size: medium;">Course Group</legend>

            <div style="clear: both;"></div>
            <div style="margin: 10px; width: 100%;">
                <div style="clear: both;"></div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                            CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="400px" Style="display: none">
                            <div style="padding: 5px;">
                                <fieldset style="padding: 5px">
                                    <legend style="font-weight: bold; font-size: large; text-align: center">Course Group</legend>
                                    <div style="padding: 5px;">
                                        <div style="width: 100px; float: left;">Group Name</div>
                                        <div style="width: 155px; float: left;">
                                            <asp:TextBox ID="txtGroupName" runat="server" Width="120"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtGroupName"
                                                ValidationGroup="VG"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div style="padding: 5px;">
                                        <div style="width: 100px; float: left;">Type Definition</div>
                                        <div style="width: 120px; float: left;">
                                            <asp:DropDownList ID="ddlTypeDefinition" runat="server" Width="120">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div style="padding: 5px;">
                                        <div style="width: 100px; float: left;">Remarks</div>
                                        <div style="width: 400px; float: left;">
                                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Height="40" Width="350">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div style="padding: 5px;">
                                        <div style="margin-left: 5px; float: left;">
                                            <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" ValidationGroup="VG"></asp:Button>
                                        </div>
                                        <div style="margin-left: 5px; float: left;">
                                            <asp:Button ID="btnAddAndNext" runat="server" OnClick="btnAddAndNext_Click" Text="Insert & Next" ValidationGroup="VG"></asp:Button>
                                        </div>

                                        <div style="margin-left: 5px; float: left;">
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="clear: both;"></div>
                <div id="GridViewTable" style="padding-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div>
                                <div style="margin-left: 5px; float: left;">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New"></asp:Button>
                                </div>

                                <div style="margin-left: 5px; float: left;">
                                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"></asp:Button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="clear: both;"></div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="margin-top: 15px;">
                                <asp:GridView runat="server" ID="gvCourseGroup" AutoGenerateColumns="False" Width="50%"
                                    CssClass="gridCss">
                                    <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#F0F8FF" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Name" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGroupName" Text='<%#Eval("GroupName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblType" Text='<%#Eval("Type.Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRemarks" Text='<%#Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"
                                                    ToolTip="Item Edit" CommandArgument='<%#Eval("CourseGroupId") %>'>                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                                    OnClientClick="return confirm('Are you sure to Delete this ?')"
                                                    ToolTip="Item Delete" CommandArgument='<%#Eval("CourseGroupId") %>'>                                                
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
