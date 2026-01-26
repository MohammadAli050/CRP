<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="PreRequisiteSetup.aspx.cs" Inherits="EMS.BasicSetup.PreRequisiteSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Pre Requisite Course Setup
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
            <label>Pre Requisite Course Setup</label>
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
                                        <asp:DropDownList runat="server" ID="ddlCourse"  Style="width: 350px;" />
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style1">
                                        <label><b>Course Code : </b></label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:TextBox ID="txtCourseCode" Placeholder="Course Code" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style1">
                                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" Width="90px" />
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td>
                                        <asp:Button runat="server" ID="btnReLoadPreRequisite" Text="Page Re-load" OnClick="btnReLoadPreRequisite_Click" Width="100px" />
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style1">
                                        <asp:Button ID="btnAddPreRequisiteSet" runat="server" OnClick="btnAddPreRequisiteSet_Click" Text="Add Set" Width="100px"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div>
                            <asp:GridView ID="GvPreRequisiteSet" runat="server" AutoGenerateColumns="false"  Width="90%" AllowSorting="true"
                                CssClass="gridCss" CellPadding="4" AllowPaging="true" PageSize="50" OnPageIndexChanging="GvPreRequisiteSet_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNodeId" Text='<%#Eval("NodeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Main Course">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNodeName" Text='<%#Eval("NodeName")+" "+ Eval("VersionCode")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Program" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblProgramName" Text='<%#Eval("ProgramName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pre Requisite Course" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPreRequisiteCourse" Text='<%#Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="350px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:LinkButton runat="server" ToolTip="Add Course" ID="lnkAddPreRequisiteSetCourse" Text="Edit" CommandArgument='<%#Eval("PreRequisiteMasterId") %>'  OnClick="lnkAddPreRequisiteSetCourse_Click">
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <div style="text-align: center">
                                                <asp:LinkButton runat="server" ToolTip="Remove Group" ID="lnkRemovePreRequisiteSet" Text="Remove" CommandArgument='<%#Eval("PreRequisiteMasterId") %>' OnClientClick="return confirm('Do you really want to remove this pre requisite group?');" OnClick="lnkRemovePreRequisiteSet_Click">
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
                <asp:Button ID="btnPreReqCourseShowPopup" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="ModalPreReqCourseExtender" runat="server" TargetControlID="btnPreReqCourseShowPopup" PopupControlID="pnlPreReqCoursePopup"
                     BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlPreReqCoursePopup" runat="server" BackColor="#ffffff" Width="900px" Style="display: none; border-radius: 3px;">
                        <div style="padding: 5px;">
                            <fieldset style="padding: 5px; border: 2px solid #ff9933;">
                                <legend style="font-weight: 100; font-size: small; font-variant: small-caps; color: blue; text-align: center">Pre Requisite Course</legend>
                                <div style="padding: 5px;">
                                    <div class="Message-Area">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                    <asp:Label ID="lblPreReqNew" runat="server" Text="Message : "></asp:Label>
                                                    <asp:Label ID="lblPreReqMessage" runat="server" ForeColor="#CC0000"></asp:Label>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <table style="padding: 1px;" border="0">
                                        
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProgram" runat="server" Width="100px" Text="Program"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProgram" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPreRequisiteMasterId" Visible="false" runat="server" Width="150px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMainCourse" runat="server" Width="100px" Text="Main Course"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlMainCourse" Width="350px" runat="server"></asp:DropDownList>
                                                <asp:Label ID="lblMainCourse2" runat="server" ></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPreReqCourse" runat="server" Width="150px" Text="Pre-Requisite Course"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlPreReqCourse" Width="350px" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAddPreReqCourse" runat="server" Text=" Save " OnClick="btnAddPreReqCourse_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelPreReqCourse" runat="server" Text=" Close " OnClick="btnCancelPreReqCourse_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GvPreRequisiteCourseDetails" runat="server" AutoGenerateColumns="false"  
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

                                            <%--<asp:TemplateField HeaderText="Program">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblProgramName"  Text='<%#Eval("ProgramName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateField>--%>
                                        
                                            <asp:TemplateField HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCredit"  Text='<%#Eval("Credits") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:LinkButton runat="server" ToolTip="Remove Course" ID="lnkDeletePreRequisiteCourse" Text="Remove" CommandArgument='<%#Eval("PreRequisiteDetailId") %>'  OnClick="lnkDeletePreRequisiteCourse_Click">
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
