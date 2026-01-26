<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentRegistration_PrepareStudentWorksheet" Codebehind="PrepareStudentWorksheet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Work sheet</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="WorkSheet">
        <div class="margin-bottom">
            <div class="float-left border1px">
                <span style="font-weight: bold;">Worksheet</span>
            </div>
            <div class="float-left border1px" style="margin-left: 10px; width: 800px; height: 15px;">
                <asp:Label runat="server" ID="lblMessage">.....</asp:Label>
            </div>
            <div class="cleaner"></div>
        </div>
        <div class="margin-bottom" style="width: 1100px;">
            <fieldset>
                <legend>Search</legend>
                <div class="float-left margin-right">
                    <label class="labelDisplay" for="ddlAcaCalBatch" style="width: 160px;">Academic Calender / Batch</label>
                    <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlAcaCalBatch" />
                </div>
                <div class="float-left margin-right">
                    <label class="labelDisplay" for="ddlProgram" style="width: 105px;">Program</label>
                    <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlProgram" DataValueField="ProgramId" DataTextField="ShortName" />
                </div>
                <div class="cleaner"></div>
                <div style="margin-top: 10px;">
                    <div class="float-left margin-right">
                        <label class="labelDisplay" for="txtGpaFrom" style="width: 160px;">GPA From</label>
                        <asp:TextBox runat="server" class="dropdownList-size" ID="txtGpaFrom" />
                    </div>
                    <div class="float-left margin-right">
                        <label class="labelDisplay" for="ddlProgram" style="width: 105px;">GPA To</label>
                        <asp:TextBox runat="server" class="dropdownList-size" ID="DropDownList2" />
                    </div>
                    <div class="float-left margin-right">
                        <asp:Button ID="btnView" runat="server" CssClass="button" Height="25px" OnClick="btnView_Click" Text="Load" Width="124px" />
                    </div>
                </div>
                <div class="cleaner"></div>
            </fieldset>
        </div>

        <div class="margin-bottom">
            <div style="font-weight: bold;">
                <div style="width: 1100px;">
                    <fieldset>
                        <legend>Provisioned Student
                        </legend>
                        <div class="float-left" style="width: 150px; margin-left: 15px;">
                            <asp:Label ID="Label2" runat="server" Text="Auto Open : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtProAutoOpen" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 200px; margin-left: 15px;">
                            <asp:Label ID="lblAutoPreReg" runat="server" Text="Auto Pre-Registration : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtProAutoPreReg" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 150px; margin-left: 15px;">
                            <asp:Label ID="Label1" runat="server" Text="Auto Mandatory : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtProAutoMandatory" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 200px; margin-left: 15px;">
                            <asp:Button runat="server" ID="btnFillAllText" OnClick="btnFillAll_Click" Text="Fill All" />
                        </div>
                    </fieldset>
                </div>
                <div style="clear: both"></div>
                <div style="width: 1100px;">
                    <fieldset>
                        <legend>Non-Provisioned Student
                        </legend>
                        <div class="float-left" style="width: 150px; margin-left: 15px;">
                            <asp:Label ID="Label3" runat="server" Text="Auto Open : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtAutoOpen" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 200px; margin-left: 15px;">
                            <asp:Label ID="Label4" runat="server" Text="Auto Pre-Registration : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtAutoPreReg" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 150px; margin-left: 15px;">
                            <asp:Label ID="Label5" runat="server" Text="Auto Mandatory : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtAutoMandatory" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                        </div>
                        <div class="float-left" style="width: 200px; margin-left: 15px;">
                            <asp:Button runat="server" ID="Button1" OnClick="btnFillAll_Click" Text="Fill All" />
                        </div>
                    </fieldset>
                </div>
                <div style="clear: both"></div>
                <br />
                <div class="float-left">
                    <asp:CheckBox ID="chkAll" Text="Select All" Width="120px" runat="server" CssClass="selectAllCheckBox" Font-Bold="true" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                </div>
                <div class="float-left" style="width: 129px; margin-left: 15px;">Student (ID)</div>
                <div class="float-left" style="width: 253px; margin-left: 15px;">Student (Name)</div>
                <div class="float-left" style="width: 267px; margin-left: 15px;">Course Tree → Linked Calendars</div>
                <br />
                <br />

                <div class="cleaner"></div>
            </div>
            <asp:Panel ID="pnlCourseOfferGrid" runat="server" Width="980px" Height="397px" ScrollBars="Vertical" Wrap="False">
                <asp:GridView ID="gvwCollection" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="960px" ShowHeader="False">
                    <RowStyle Height="24px" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <%--<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Text = " Select All " AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                                </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" Width="120px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="StudentID" HeaderText="ID" ItemStyle-Width="50px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <%--<ItemStyle Width="50px" />--%>
                        </asp:BoundField>

                        <asp:BoundField DataField="Roll" HeaderText="Student Roll" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="129px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FullName" HeaderText="Student name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="253px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TreeMasterID" HeaderText="tree id" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <%--<ItemStyle Width="100px" />--%>
                        </asp:BoundField>

                        <asp:BoundField DataField="Prefix" HeaderText="link Canlanders" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <%--<ItemStyle Width="100px" />--%>
                        </asp:BoundField>

                        <asp:BoundField DataField="CourseTreeLinkCalendars" HeaderText="tree name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="267px" />
                        </asp:BoundField>

                        <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txt" AutoPostBack="true" Style="width: 40px; text-align: center;" MaxLength="2" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" Width="130px" />
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle Height="24px" />
                    <HeaderStyle Height="24px" />
                </asp:GridView>
                <br />
                <div class="margin-bottom">
                    <div class="float-left margin-right">
                        <asp:Button ID="btnGenerateWorkSheet" runat="server" CssClass="button" Height="25px" OnClick="btnGenerateWorkSheet_Click"
                            Text="Generate WorkSheet" Width="160px" />
                    </div>
                    <div class="cleaner"></div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

