<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="RelationDiscountPage.aspx.cs" Inherits="EMS.BasicSetup.RelationDiscountPage" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Relation Discount Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
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

        .Filed-with-large
        {
            margin: 2px;
            width: 250px;
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
            <label>Relation Discount Setup</label>
        </div>

        <div class="Message-Area" style="height: 35px;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div style="float: left;">
                        <div style="float: left; margin-right: 5px;">
                            <label>Program :</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />

                        </div>
                        <div style="float: left; margin-right: 5px;">
                            <label>Batch :</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                        </div>
                        <div style="float: left; margin-right: 5px;">
                            <label>Relation Type :</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <asp:DropDownList ID="ddlRelationDiscountType" runat="server"></asp:DropDownList>
                        </div>
                        <div style="float: left; margin-right: 5px;">
                            <label>Student ID :</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <asp:TextBox runat="server" ID="txtStudentId" Style="width: 150px;" />
                        </div>
                        <div style="float: left; margin-right: 5px;">
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" />
                        </div>
                    </div>

                    <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -23px">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                        </div>
                    </div>
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

        <div>
            <asp:UpdatePanel runat="server" ID="UpClassSchedule">
                <ContentTemplate>
                    <div class="Message-Area">
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <div>
                            <div style="margin-bottom: 5px; margin-top: 5px; float: left; width: 100%;">
                                <div style="float: left;">
                                    <asp:Button ID="btnAddNew" runat="server" BackColor="LightSkyBlue" OnClick="btnAddNew_Click" Text="Add New"></asp:Button>
                                </div>
                                <div style="float: right;">
                                    <asp:Label ID="lbl1" Text="Count :" runat="server"></asp:Label>
                                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="ClassSchedule-container">
                                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                <ajaxToolkit:ModalPopupExtender
                                    ID="ModalPopupExtender1"
                                    runat="server"
                                    TargetControlID="btnShowPopup"
                                    PopupControlID="pnPopUp"
                                    CancelControlID="btnClose"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel runat="server" ID="pnPopUp" Style="display: none;">
                                    <div style="width: 800px; padding: 5px; margin: 5px; background-color: Window;">
                                        <fieldset style="padding: 10px; margin: 5px; border-color: lightgreen;">
                                            <legend>Relation Discount Setup</legend>
                                            <div style="padding: 5px; width: 100%">

                                                <div style="float: left; margin-bottom: 10px;">
                                                    <asp:Label ID="lbl2" runat="server" ForeColor="BlueViolet" Font-Bold="true" Text="Message: "></asp:Label>
                                                    <asp:Label ID="lblMsgPopUp" ForeColor="BlueViolet" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div style="float: left; width: 100%">
                                                    <div style="float: left; margin-right: 10px;">
                                                        <label class="label-width">1st Applicant ID</label>
                                                    </div>
                                                    <div style="float: left; margin-right: 5px;">
                                                        <asp:TextBox CssClass="field-width" runat="server" ID="txtStudent1st" />
                                                        <asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1"
                                                            runat="server"
                                                            ErrorMessage="*"
                                                            ControlToValidate="txtStudent1st"
                                                            ValidationGroup="val_1"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div style="float: left;">
                                                        <asp:ImageButton CssClass="marginTop" runat="server" ID="btnSearchName1st" OnClick="btnSearchName1st_Click"
                                                            Height="30" Width="30" ImageUrl="~/Images/search.png" ValidationGroup="val_1" />
                                                    </div>
                                                    <div style="float: left; margin-left: 10px">
                                                        <label class="label-width">Name</label>
                                                        <asp:TextBox CssClass="Filed-with-large" runat="server" ID="txtName1st" />
                                                    </div>
                                                </div>

                                                <div style="clear: both;"></div>
                                                <hr />
                                                <div style="float: left; margin-right: 20px;">
                                                    <label class="label-width">Relation Type</label>
                                                </div>
                                                <div style="float: left;">
                                                    <asp:DropDownList ID="ddlRelationDiscountTypePopUp" runat="server"></asp:DropDownList>
                                                </div>
                                                <div style="clear: both;"></div>
                                                <hr />
                                                <div style="float: left; width: 100%">
                                                    <div style="float: left; width: 100%">
                                                        <div style="float: left; margin-right: 5px;">
                                                            <label class="label-width">2nd Applicant ID</label>
                                                        </div>
                                                        <div style="float: left; margin-right: 5px;">
                                                            <asp:TextBox CssClass="field-width" runat="server" ID="txtStudent2nd" />

                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator2"
                                                                runat="server"
                                                                ErrorMessage="*"
                                                                ControlToValidate="txtStudent2nd"
                                                                ValidationGroup="val_2"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:ImageButton CssClass="marginTop" runat="server" ID="btnSearchName2nd" OnClick="btnSearchName2nd_Click"
                                                                Height="30" Width="30" ImageUrl="~/Images/search.png" ValidationGroup="val_2" />
                                                        </div>
                                                        <div style="float: left; margin-left: 10px">
                                                            <label class="label-width">Name</label>
                                                            <asp:TextBox CssClass="Filed-with-large" runat="server" ID="txtName2nd" />
                                                        </div>

                                                    </div>
                                                </div>
                                                <div style="clear: both;"></div>
                                                <hr />
                                                <div style="clear: both;"></div>
                                                <div style="margin-top: 10px">
                                                    <div style="float: left; margin-left: 10px">
                                                        <asp:Button runat="server" ID="btnAddSibling" Text="Add" OnClick="btnAddSibling_Click" />
                                                    </div>
                                                    <div style="float: left; margin-left: 10px">
                                                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                                                    </div>
                                                    <asp:Button runat="server" ID="btnClose" Text="Cancel" Style="width: 150px; height: 30px;" Visible="false" Enabled="false" />
                                                </div>
                                                <div style="clear: both;"></div>
                                                <hr />
                                                <div style="float: left; Width: 100%;">
                                                    <asp:GridView ID="gvSiblingList" runat="server" AutoGenerateColumns="false" Width="100%">
                                                        <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
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
                                                                <HeaderStyle Width="300px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Program">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("Program") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <div style="text-align: center">
                                                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lnkSiblingDelete" Text="Delete"
                                                                            CommandArgument='<%#Eval("StudentID") %>'
                                                                            OnClick="lnkSiblingDelete_Click">                                                                         
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No data found!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="clear: both;"></div>

                            <div>
                                <asp:GridView ID="gvStudentList" runat="server" AutoGenerateColumns="false" ShowFooter="true" Width="100%"
                                    OnSorting="gvStudentList_Sorting" AllowSorting="true">
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
                                            <HeaderStyle Width="300px" />
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

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:LinkButton runat="server" ToolTip="Edit" ID="lnkEdit"
                                                        CommandArgument='<%#Eval("StudentID") %>'
                                                        OnClick="lnkEdit_Click">
                                                <span class="action-container"><img alt="Save" src="../Images/2.edit.png" / style="width:20px; height:20px;"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
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
