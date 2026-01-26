<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_GradeWiseRetakeDiscount" Codebehind="GradeWiseRetakeDiscount.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Grade Wise Retake Discount
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <style>
        .ContainWrapper .Grid-Button {
            margin-top: 5px;
            margin-bottom: 2px;
            border: 1px solid #aaa;
            background-color: #f9f9f9;
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
        <div class="Message-Area" style="height: 45px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="padding: 1px; width: 1000px;">
                        <tr>
                            <td>Program :</td>
                            <td>
                                <asp:DropDownList ID="ddlProgram" runat="server" Width="150px"
                                    DataTextField="ShortName" DataValueField="ProgramID">
                                </asp:DropDownList>
                            </td>
                            <td>Session :</td>
                            <td>
                                <asp:DropDownList ID="ddlAcaCalSession" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -33px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                        </div>
                        <div id="divIconTxt" style="float: left; margin: 16px 0 0 10px;">
                            Please wait...
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

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

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 1100px; margin-top: 20px;">

                    <div class="Grid-Button" style="width: 890px; height: 30px">
                        <div style="float: left;">
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                            <asp:Label ID="lblCourseCount" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div style="float: right;">
                            <asp:Button ID="btnUpdate" runat="server" Font-Bold="true" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                        </div>
                    </div>
                    <asp:GridView runat="server" ID="gvGradeWiseRetakeDiscount" AutoGenerateColumns="False" Width="900"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Grade">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnGradeWiseRetakeDiscountId" runat="server" Value='<%#Eval("GradeWiseRetakeDiscountId") %>' />
                                    <asp:HiddenField ID="hdnGradeId" runat="server" Value='<%#Eval("GradeId") %>' />
                                    <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("Grade.Grade") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade Point">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGradePoint" Text='<%#Eval("Grade.GradePoint") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retake Discount">                               
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRetakeDiscount" Text='<%#Eval("RetakeDiscount") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retake Discount Tr & Wv">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRetakeDiscountOnTrOrWav" Text='<%#Eval("RetakeDiscountOnTrOrWav") %>'>
                                    </asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both"></div>
        <div style="height: 30px; width: 900px; padding: 15px;"></div>
    </div>
</asp:Content>

