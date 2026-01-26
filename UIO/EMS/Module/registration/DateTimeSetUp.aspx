<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DateTimeSetUp.aspx.cs" Inherits="DateTimeSetUp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Set-Up</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline';
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
            <label>Set-Up</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="DateTimeSetUp-container">
                    <div class="div-margin">
                        <div class="loadArea">

                            <div>
                                <div style="float: left">
                                    <asp:Label Text="Program" ID="lblProgram" runat="server"></asp:Label>

                                    <uc1:ProgramUserControl ID="ucProgram" runat="server" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />

                                </div>
                                <div style="float: left">
                                     <asp:Label Text="Semester" ID="lblSem" runat="server"></asp:Label>
                                    <uc1:SessionUserControl runat="server" ID="ucSession" />
                                </div>
                                <div style="float: left">
                                    <label class="display-inline field-Title1">Type(Registration)</label>
                                    <br />
                                    <asp:DropDownList runat="server" ID="ddlActivityType" class="margin-zero dropDownList" DataValueField="ValueID" DataTextField="ValueName" AutoPostBack="true" OnSelectedIndexChanged="ActivityType_Change" />

                                </div>
                                <div style="float:left">
                                       <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="Load_Click" class="button-margin btn-size" />
                                </div>
                            </div>

                         

                            <div style="clear: both"></div>
                            <div style="margin-top:20px">
                                
                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                <label class="display-inline field-Title3">
                                    IsActive</label>

                                <label>From</label>
                                <asp:TextBox ID="txtStartDate" runat="server" class="margin-zero input-Size datepicker" DataFormatString="{0:dd/MM/yyyy}" placeholder="Date" Width="150px" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate" />

                                <label>To</label>
                                <asp:TextBox ID="txtEndDate" runat="server" class="margin-zero input-Size datepicker" DataFormatString="{0:dd/MM/yyyy}" placeholder="Date" Width="150px" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate" />

                                <asp:Button ID="btnSaveUpdate" runat="server" class="button-margin btn-size" OnClick="SaveUpdate_Click" Text="Save" />
                                <asp:Button ID="btnCancel" runat="server" class="button-margin btn-size" OnClick="Cancel_Click" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel03">
            <ContentTemplate>
                <div class="DateTimeSetUp-container">
                    <asp:Panel ID="pnlDateTimeSetUp" runat="server" Width="800px" Wrap="False">
                        <asp:GridView ID="gvDateTimeSetUp" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("AcaCalName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblProgram" Font-Bold="False" Text='<%#Eval("ProgramName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblType" Font-Bold="True" Text='<%#Eval("TypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="txtStartDt" Font-Bold="True" Text='<%#Eval("StartDate", "{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="txtFromDt" Font-Bold="True" Text='<%#Eval("EndDate", "{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IsActive" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIsActive" Font-Bold="True" Text='<%#Eval("IsActive") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%#Eval("Id") %>' runat="server" ToolTip="Update" ID="lbSave" OnClick="lbUpdate_Click">
                                            <span class="action-container"><img alt="Update" src="/Images/update.png" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("Id") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <span class="action-container"><img alt="Delete" src="/Images/delete.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <b>No Data Found !</b>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
