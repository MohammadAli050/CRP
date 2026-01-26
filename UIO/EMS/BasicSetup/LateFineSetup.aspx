<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="LateFineSetup.aspx.cs" Inherits="EMS.SyllabusMan.LateFineSetup" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <div style="margin: 10px;">
        <fieldset>
            <legend style="font-weight: bold; font-size: medium;">Late File</legend>

            <div style="clear: both;"></div>
            <div style="margin: 10px; width: 100%;">
                <div style="float: left;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="float: left; width: 300px">
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">Schedule Name</div>
                                    <div style="width: 157px; float: left;">
                                        <asp:TextBox ID="txtScheduleName" runat="server"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 5px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtScheduleName"
                                            ValidationGroup="VG"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="clear: both;"></div>
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">Amount</div>
                                    <div style="width: 157px; float: left;">
                                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 5px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtAmount"
                                            ValidationGroup="VG"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtAmount" ValidationGroup="VG"
                                            ValidationExpression="\d+(\.\d{1,2})?"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <%--<div style="clear: both;"></div>--%>
                            <div style="float: left; width: 300px">
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">Program</div>
                                    <div style="width: 120px; float: left;">
                                        <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                        
                                         </div>
                                </div>
                                <div style="clear: both;"></div>
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">Session</div>
                                    <div style="width: 120px; float: left;">
                                        <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>
                            <%--<div style="clear: both;"></div>--%>
                            <div style="float: left; width: 300px">
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">Start Date</div>
                                    <div style="width: 157px; float: left;">
                                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 5px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtStartDate"
                                            ValidationGroup="VG"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="clear: both;"></div>
                                <div style="padding: 5px;">
                                    <div style="width: 100px; float: left;">End Date</div>
                                    <div style="width: 157px; float: left;">
                                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 5px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtStartDate"
                                            ValidationGroup="VG"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div style="padding: 5px;">
                                <div style="width: 100px; float: left;">
                                    <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" ValidationGroup="VG"></asp:Button>
                                </div>
                                <div style="width: 100px; float: left;">
                                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"></asp:Button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div style="clear: both;"></div>
                <div style="width: 70%;">
                    <hr />
                </div>
                <div id="GridViewTable" style="padding-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="gvLateFineSchedule" AutoGenerateColumns="False" Width="70%"
                                CssClass="gridCss">
                                <HeaderStyle BackColor="#737CA1" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#F0F8FF" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Schedule Name" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblScheduleName" Text='<%#Eval("ScheduleName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Program">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("Program.ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Session">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSession" Text='<%#Eval("Session.FullCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStartDate" Text='<%#Eval("StartDate", "{0:dd MMM,  yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblEndDate" Text='<%#Eval("EndDate", "{0:dd MMM, yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"
                                                ToolTip="Item Edit" CommandArgument='<%#Eval("LateFineScheduleId") %>'>                                                
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                                OnClientClick="return confirm('Are you sure to Delete this ?')"
                                                ToolTip="Item Delete" CommandArgument='<%#Eval("LateFineScheduleId") %>'>                                                
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
