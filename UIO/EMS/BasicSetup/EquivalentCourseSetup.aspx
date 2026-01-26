<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="EquivalentCourseSetup.aspx.cs" Inherits="EMS.BasicSetup.EquivalentCourseSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Equivalent Course Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .marginTop
        {
            margin-top: -5px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle">
            <label>Equivalent Course Setup</label>
        </div>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel3"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoad" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnLoad" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>

        <div class="Message-Area">
            <asp:UpdatePanel runat="server" ID="UpClassSchedule">
                <ContentTemplate>
                    <div >
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <div>
                            <div>
                                <table style="padding: 1px;" border="0">
                                    
                                </table>
                            </div>
                            <div>
                                <%--<label>Program : </label>--%>
                                <%--<asp:DropDownList runat="server" ID="ddlProgram" visible ="false" Style="width: 150px;" />--%>
                                
                                <table style="padding: 1px;" border="0">
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true"  Font-Underline="true" Text="Filter Option"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <label><b>Course : </b></label>
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList runat="server" ID="ddlCourseForFilter"  Style="width: 350px;" />
                                        </td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style1">
                                            <label><b>Course Code : </b></label>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="txtCourseCodeForFilter" Placeholder="Course Code" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style1">
                                            <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" Width="90px" />
                                        </td>
                                         
                                        <td class="auto-style1">
                                            <asp:Button runat="server" ID="btnReLoad" Text="Page Re-Load" ImageUrl="~/Images/refresh-icon.png" OnClick="btnReLoad_Click" Width="100px" />
                                        </td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style1">
                                            <asp:Button ID="btnAddEquivalentGroup" runat="server" OnClick="btnAddEquivalentGroup_Click" Text=" Add Equivalent " Width="120px"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div>
                            <asp:GridView ID="GvEquivalentCourse" runat="server" AutoGenerateColumns="false"  Width="90%" AllowSorting="true"
                                CssClass="gridCss" CellPadding="4" AllowPaging="true" PageSize="50" OnPageIndexChanging="GvEquivalentCourse_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGroupName" Text='<%#Eval("GroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Course Code">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="300px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:LinkButton runat="server" ToolTip="Add Course" ID="lnkAddEquivalentCourse" Text="Edit" CommandArgument='<%#Eval("EquiCourseMasterId") %>'  OnClick="lnkAddEquivalentCourse_Click">
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:LinkButton runat="server" ToolTip="Delete Group" ID="lnkRemoveEquivalentGroup" Text="Remove" CommandArgument='<%#Eval("EquiCourseMasterId") %>' OnClientClick="return confirm('Do you really want to remove this equivalent course group?');" OnClick="lnkRemoveEquivalentGroup_Click">
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                       <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No data found!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#ff9933" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#FFFFCC" />
                                <AlternatingRowStyle BackColor="White" />
                                <PagerStyle BackColor="#ff9933" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="#ffffff" Width="900px" Style="display: none; border-radius: 3px;">
                    <div style="padding: 5px;">
                        <fieldset style="padding: 5px; border: 2px solid #ff9933;">
                            <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Equivalent Course List</legend>
                            <div style="padding: 5px;">
                                <div class="Message-Area">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                <asp:Label ID="lblNew" runat="server" Text="Message : "></asp:Label>
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <table style="padding: 1px;" border="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEquiValanceMasterId" runat="server" Width="100px" Visible="false"></asp:Label>
                                            <asp:Label ID="lblEquivalanceGroupName" runat="server" Width="100px" Text="Group Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEquivalanceGroupName" Placeholder="Equivalent Group Name" Width="250px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEquivalanceRemark" runat="server" Width="100px" Text="Remark"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEquivalanceRemark" TextMode="MultiLine" Placeholder="Equivalent Remark" Width="250px" Height="30px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCourseAddEquivalance" runat="server" Width="100px" Text="Course"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlCourseAddEquivalance" Width="350px" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="AddButton" runat="server" Text="Save" OnClick="btnAdd_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="CancelButton" runat="server" Text="Close" CssClass="cursor" OnClick="btnCancel_Click"/></td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GvEquivalentCourseDetails" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowSorting="true" CssClass="gridCss" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Course Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTitle"  Text='<%#Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="400px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCredit"  Text='<%#Eval("Credits") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:LinkButton runat="server" ToolTip="Remove Course" ID="lnkDeleteEquivalentCourse" Text="Remove" CommandArgument='<%#Eval("EquiCourseDetailId") %>'  OnClick="lnkDeleteEquivalentCourse_Click">
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data found!
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#ff9933" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFFFCC" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnEquivalentCourseMasterId" runat="server" />
</asp:Content>
