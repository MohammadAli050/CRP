<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="SMSService.aspx.cs" Inherits="EMS.miu.admin.SMSService" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Send SMS & Email Notification
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script type="text/javascript">
        function CharacterCount(txt) {
            var count = document.getElementById("<%= lblCnt.ClientID %>");
            count.innerText = txt.value.length + " of 160 characters";
        };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="well">
        <h3>Send Sms</h3>
        <asp:Label ID="lblMsg" Style="color: red; font: 18;" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <div class="form-horizontal">


            <div style="float: left">
                <div style="float: left">
                    <asp:DropDownList id="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"/>
                </div>
                 <div style="float:left">
                    <asp:TextBox ID="txtTeacherCode" runat="server" Visible="true"></asp:TextBox>
                </div>
                <div style="float: left">
                    <uc1:ProgramUserControl ID="ucProgram" runat="server" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" Visible="false"/>
                </div>
                <div style="float: left">
                    <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" Visible="false" />
                </div>
                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="LoadButton_Click" Height="22px" />
                <br />
            </div>
            <div style="clear: both"></div>


            <div class="control-group">
                <asp:Label ID="lblWriteSMS" runat="server">Write Message (Not more than 160 letters!)</asp:Label><br />
                <asp:TextBox ID="txtMSG" TextMode="MultiLine" runat="server" Height="75px" Width="319px"></asp:TextBox>
                <asp:Label ID="lblCnt" runat="server" Width="100px">0</asp:Label><br />
                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click" />
            </div>


            <asp:GridView ID="GridViewStudent" runat="server" AutoGenerateColumns="False" Visible="false"
                EmptyDataText="No data found."
                GridLines="None" CellSpacing="-1"
                CssClass="table table-bordered table-striped"
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px">
                <Columns>

                    <asp:TemplateField Visible="false" HeaderText="Student Id">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField Visible ="True">
                <ItemTemplate >
                    <asp:CheckBox ID="CheckBox"  runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="50px" />
            </asp:TemplateField> --%>

                    <asp:TemplateField Visible="True" HeaderText="All">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkAllStudentHeader" OnCheckedChanged="chkAllStudent_CheckedChanged" Font-Bold="true" AutoPostBack="true"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxStudent" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStudentName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student Roll">
                        <ItemTemplate>
                            <%--                    <asp:Label runat="server" ID="lblStudentRoll" Text='<%#Eval("Student.Roll") %>'></asp:Label>--%>
                            <asp:Label ID="lblClassRoll" runat="server" Width="100" Text='<%#Eval("Roll") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SMS Phone No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStudentPhone" Text='<%#Eval("BasicInfo.SMSContactSelf") %>'></asp:Label>
                            <%--<asp:TextBox ID="txtRoll" runat="server"  Text='<%#Eval("Student.Roll") %>'></asp:TextBox>--%>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
             <asp:GridView ID="GridViewTeacher" runat="server" AutoGenerateColumns="False" Visible="true"
                EmptyDataText="No data found."
                GridLines="None" CellSpacing="-1"
                CssClass="table table-bordered table-striped"
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField Visible ="True">
                <ItemTemplate >
                    <asp:CheckBox ID="CheckBox"  runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="50px" />
            </asp:TemplateField> --%>

                    <asp:TemplateField Visible="True" HeaderText="All">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkALLTeacherHeader" OnCheckedChanged="chkAllStudent_CheckedChanged" Font-Bold="true" AutoPostBack="true"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxTeacher" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTeacherName" Text='<%#Eval("BasicInfo.FullName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTeacherCode" Text='<%#Eval("Code") %>'></asp:Label>
                            <%--<asp:TextBox ID="txtRoll" runat="server"  Text='<%#Eval("Student.Roll") %>'></asp:TextBox>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTeacherPhone" Text='<%#Eval("BasicInfo.SMSContactSelf") %>'></asp:Label>
                            <%--<asp:TextBox ID="txtRoll" runat="server"  Text='<%#Eval("Student.Roll") %>'></asp:TextBox>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>

                </Columns>
                <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#000066" HorizontalAlign="Left" BackColor="White" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
