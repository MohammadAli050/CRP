<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="ClassAttendanceDelete.aspx.cs" Inherits="EMS.miu.ClassAttendance.ClassAttendanceDelete" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Attendace Delete
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="well">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h3>Attendance Delete</h3>

                <div class="form-horizontal">

                    <asp:Label ID="lblMsg" Style="font: 18;" runat="server" Text=""></asp:Label><br />

                    <div class="Message-Area">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table id="Table1" style="width: 50%; height: 25px;" runat="server">
                                        <tr>
                                            <td class="auto-style8"><b>Program :</b></td>
                                            <td class="auto-style7">
                                                <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                                            </td>

                                            <td class="auto-style9"><b>Session :</b></td>
                                            <td class="auto-style10">
                                                <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                                            </td>

                                            <td></td>

                                        </tr>
                                    </table>
                                    <table id="Table2" style="width: 72%; height: 25px;" runat="server">
                                        <tr>
                                            <td class="auto-style5">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Course : " Style="width: 60px; display: inline-block;" Width="65px"></asp:Label></td>

                                            <td class="auto-style6">
                                                <asp:DropDownList ID="ddlAcaCalSection" runat="server" Style="width: 450px;" /></td>
                                        </tr>
                                    </table>
                                    <table id="Table3" style="width: 40%; height: 25px;" runat="server">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Entry Date" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtAttendanceDate" Width="170px" AutoPostBack="true" class="margin-zero input-Size datepicker" OnTextChanged="txtAttendanceDate_TextChanged" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAttendanceDate" Format="dd/MM/yyyy" />
                                                    &nbsp;&nbsp;&nbsp;
                            
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnLoad" runat="server" Text="Load Students" OnClick="btnLoad_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div1" style="display: none; width: 195px; float: right; margin: -30px -35px 0 0;">
                                    <div style="float: left">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="30px" Width="30px" />
                                    </div>
                                    <div id="divIconTxt" style="float: left; margin: 8px 0 0 10px;">
                                        Please wait...
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="display-inline">
                    </div>


                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/working.gif" Height="150px" Width="150px" />
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
        <center>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div style="width:54%; float:right">
                        <asp:Button ID="btnDeleteAll" runat="server"  Text="Delete All" OnClientClick=" return confirm('Are you sure want to Delete all Attendance?')" OnClick="btnDelete_Click" Width="190px" Height="30px" Visible="false"/>
                    </div>
                    <asp:GridView ID="gvStudentlists" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="60%">
                        <HeaderStyle BackColor="green" ForeColor="White" Height="30" />
                        <FooterStyle BackColor="green" ForeColor="White" Height="30" />
                        <AlternatingRowStyle BackColor="lightgreen" />
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible ="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblId" Width="75px" Font-Bold="True" Text='<%#Eval("ID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="75px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StudentID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStudentRoll" Width="75px" Font-Bold="True" Text='<%#Eval("Roll") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="75px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFullName" Width="200px" Font-Bold="True" Text='<%#Eval("Name") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" Width="100px" OnClientClick=" return confirm('Are you sure want to Delete?')" OnClick="btnDeleteSingle_Click" CommandArgument='<%#Eval("ID") %>'/>
                                    
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Comment" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" runat="server" Text='<%#Eval("Comment") %>' Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>--%>
                        </Columns>
                        <RowStyle Height="35px" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <EmptyDataTemplate>
                            No data found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <div style="width:54%; float:right">
                        <asp:Button ID="Button1" runat="server"  Text="Delete All" OnClientClick=" return confirm('Are you sure want to Delete all Attendance?')" OnClick="btnDelete_Click" Width="190px" Height="30px" Visible="false"/>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
