<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="AssignPreCourse" Codebehind="AssignPreCourse.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Assign Pre-Course</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="AssignCourseTree">
         <div class="PageTitle">
            <label>Assign Pre-Course</label>
        </div>

       <div class="Message-Area">
            <div >
                <span style="font-weight: bold; float:left;">Message :</span>
            </div>
            <div  style="margin-left: 10px; float:left; width: 800px; height: 15px;">
                <asp:Label runat="server" ID="lblMessage">.....</asp:Label>
            </div>
            <div class="cleaner"></div>
        </div>

         <div class="Message-Area" style="height: 45px;">
            <div class="float-left margin-right">
                <label class="labelDisplay" for="ddlAcaCalBatch" style="width: 60px;">Batch</label>
                <asp:DropDownList runat="server" ID="ddlAcaCalBatch" DataValueField="AcademicCalenderID" DataTextField="FullCode" Width="140px" class="dropdownList-size" />
            </div>
            <div class="float-left margin-right">
                <label class="labelDisplay" for="ddlProgram" style="width: 83px;">Program</label>
                <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlProgram" Width="160px"
                                    DataTextField="NameWithCode" DataValueField="ProgramID"/>
            </div>
            <div class="float-left margin-right">
                <asp:Button ID="btnView" runat="server" CssClass="button" Height="25px" OnClick="btnView_Click" Text="Load" Width="124px" />
            </div>
            <div class="cleaner"></div>
        </div>
        <div style="width: 1000px;">
            <div style="float: right; margin: 0px;">
                <asp:Button ID="btnRefresh" runat="server" CssClass="button" Height="25px" OnClick="btnRefresh_Click" Text="Refresh" Width="124px" />
            </div>
        </div>
        <div class="cleaner"></div>
        <div class="margin-bottom">

            <asp:Panel ID="pnlStudentGrid" runat="server" Width="980px">
                <asp:GridView runat="server" ID="gvStudent" AutoGenerateColumns="False"
                    CssClass="gridCss">
                    <HeaderStyle BackColor="SeaGreen"  ForeColor="White"/>
                    <AlternatingRowStyle BackColor="#FFFFCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Roll" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFullName" Text='<%#Eval("BasicInfo.FullName") %>'>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pre Course" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPreCourse" Text='<%#Eval("PreCourseCount") %>'>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pre-Course" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPreCourseAdd" runat="server" OnClick="btnPreCourseAdd_Click" Text=" ● Add/Edit"
                                    ToolTip="Add section." CommandArgument='<%#Eval("StudentID") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="7%" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

