<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentDiscountInitialPage.aspx.cs" Inherits="EMS.miu.bill.StudentDiscountInitialPage" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Discount Initial
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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
        <div class="Message-Area" style="height: 115px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 100%;">
                        <tr>
                            <td class="tbl-width-lbl">Program</td>
                            <td>
                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/>
                            </td>
                            <td class="tbl-width-lbl">Batch</td>
                            <td >
                                <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                            </td>
                            <td>Student ID
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="txt1" runat="server" ForeColor="Red" Text="Filter"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFilterFrom" MaxLength="3" Width="40" runat="server" ForeColor="#003300"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtFilterTo" MaxLength="3" Width="40" runat="server" Text="" ForeColor="#003300"></asp:TextBox>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    ControlToValidate="txtFilterFrom"
                                    ValidationExpression="\d+"
                                    Display="Static"
                                    EnableClientScript="true"
                                    ErrorMessage="*"
                                    runat="server"
                                    ValidationGroup="VG" />

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                    ControlToValidate="txtFilterTo"
                                    ValidationExpression="\d+"
                                    Display="Static"
                                    EnableClientScript="true"
                                    ErrorMessage="*"
                                    runat="server"
                                    ValidationGroup="VG" />
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" ValidationGroup="VG" />
                            </td>
                            <td style="width: 40px; height: 40px;">
                                <div id="divProgress" style="display: none; float: right; z-index: 1000;">
                                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="12">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10">
                                <div style="float: left;">
                                    <div style="float: left; margin-right: 10px;">
                                        <asp:CheckBox ID="chkAll" runat="server" Text="All" ForeColor="Red" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:CheckBoxList ID="chkTyprDefinition" runat="server" RepeatDirection="Horizontal" RepeatColumns="10"
                                            ForeColor="#003300">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                        <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


          <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <div style="margin-bottom: 5px; margin-top: 5px;">
                            <asp:Label ID="lbl1" Text="Count :" runat="server"></asp:Label>
                            <asp:Label ID="lblCount" runat="server"></asp:Label>
                        </div>
                        <div>
                            <asp:GridView ID="gvStudentDiscountInitial" runat="server" AutoGenerateColumns="false" ShowFooter="true" Width="100%"
                                OnSorting="gvStudentDiscountInitial_Sorting" AllowSorting="true">
                                <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="30" />
                                <FooterStyle BackColor="SeaGreen" ForeColor="White" Height="30" />
                                <AlternatingRowStyle BackColor=" #F9F99F" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roll">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkRoll" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="Roll">Roll</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnStudentID" Value='<%#Eval("StudentID") %>'></asp:HiddenField>
                                            <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblName" Font-Bold="true" Text='<%#Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="250px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Program">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkProgram" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="Program">Program</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>                                            
                                            <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("Program") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Discount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkDiscountType" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="DiscountType">Discount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnTypeDefinitionId" Value='<%#Eval("TypeDefinitionId") %>'></asp:HiddenField>
                                            <asp:Label runat="server" ID="txtDiscountType" Font-Bold="false" Text='<%#Eval("DiscountType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkTypePercentage" runat="server" ForeColor="White" CommandName="Sort" CommandArgument="TypePercentage">Percentage</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPercentage" Font-Bold="true" Text='<%#Eval("TypePercentage") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:TextBox Width="99%" runat="server" ID="txtComments" Font-Bold="true" Text='<%#Eval("Comments") %>'></asp:TextBox>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            <asp:Button runat="server" ToolTip="Save/ Edit All" Text="Save All" ID="lBtnSaveAllHeader"
                                                OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                                OnClick="lBtnSaveAll_Click"></asp:Button>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnStudentDiscountInitialDetailsId" Value='<%#Eval("StudentDiscountInitialDetailsId") %>'></asp:HiddenField>
                                            <asp:LinkButton runat="server" ToolTip="Save / Edit" ID="lbSave"
                                                CommandArgument='<%#Eval("StudentDiscountInitialDetailsId") %>'
                                                OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                                OnClick="lBtnSave_Click">
                                                <span class="action-container"><img alt="Save" src="../../Images/1.add.png" /></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button runat="server" ToolTip="Save/ Edit All" Text="Save All" ID="lbSaveAllFooter"
                                                OnClientClick=" return confirm('Are you sure, you want to Save?')"
                                                OnClick="lBtnSaveAll_Click"></asp:Button>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    No data found!
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
