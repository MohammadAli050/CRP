<%@ Page Title="Exam Result Submit" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" 
    CodeBehind="ExamResultSubmit.aspx.cs" Inherits="EMS.miu.result.ExamResultSubmit" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Result Submit</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
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

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Exam Result Submit</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="divProgress" style="display:none ;  margin-top:-35px">
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/working.gif" Height="50px" Width="50px" />
                <br />
                Processing your request ...
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged" />
                        </td>
                        
                    </tr>
                             
                </table>
                <table>
                    <tr>
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="450px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <%--<td class="auto-style4">
                            <b>Section :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlAcaCalSection" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlAcaCalSection_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>--%>
                        <td class="auto-style4">
                            <b>Exam :</b>
                        </td> 
                        <td>
                            <asp:DropDownList ID="ddlExam" Width="120px" runat="server" ></asp:DropDownList>
                            <asp:Label ID="lblExamTemplateBasicItemId" Visible="false" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td> 
                    </tr>         
                </table>
            </div>
            <br />
            <asp:Panel runat="server" ID="pnlTotalMark" Visible="false">
                <label><b>Exam Date : </b></label>
                <asp:TextBox runat="server" ID="txtExamDate" Width="170px" class="margin-zero input-Size datepicker" placeholder="Exam Date" DataFormatString="{0:dd/MM/yyyy}" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExamDate" Format="dd/MM/yyyy" />
                
                <label><b>Exam taken in <asp:TextBox runat="server" ID="txtTotalMark" Width="50px"></asp:TextBox> Mark will be converted to <asp:Label runat="server" ID="lblTemplateMark"></asp:Label> mark.</b></label>
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSaveTotalMark_click"/>
                <br />
                <table>
                    <tr>
                        <td class="auto-style4">
                            <asp:Button runat="server" ID="btnFinalSubmit" Text="Final Submit Selected Exam" OnClick="btnFinalSubmit_Clicked" OnClientClick="return confirm('Are you sure? You will not be able to change mark after submission!');"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnFinalSubmitAll" Text="Final Submit All Exam" OnClick="btnFinalSubmitAll_Clicked" OnClientClick="return confirm('Are you sure? You will not be able to change mark after submission!');"/>
                        </td>
                    </tr>
                </table>
                
    <%--<asp:Button runat="server" ID="btnFinalSubmitAll" Text="Final Submit All Exam" OnClick="btnFinalSubmitAll_Clicked" OnClientClick="return confirm('Are you sure? You will not be able to change mark after submission!');"/>--%>

            </asp:Panel>
            <asp:GridView ID="GvExamMarkSubmit" runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
            EmptyDataText="No data found." CellPadding="4" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal" OnRowCommand="GvExamMarkSubmit_RowCommand" >
                <Columns>
                    <asp:TemplateField Visible ="false"  HeaderText="Student Id">
                        <ItemTemplate >
                            <asp:Label ID="lblCourseHistoryId"  runat="server" Text='<%# Bind("CourseHistoryId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="ExamMarkDetailsID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblExamMarkDetailId" runat="server" Text='<%# Bind("ExamMarkDetailId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Student Roll">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentRoll" runat="server" Text='<%# Bind("StudentRoll") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Student Name">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle  />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mark">
                        <ItemTemplate >
                            <asp:TextBox ID="txtMark" width="70px" Enabled='<%#(Eval("ExamMarkTypeId")).ToString() == "2" ? false : true %>' runat="server" Text='<%# Bind("Mark") %>'/>
                        </ItemTemplate>
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate >
                            <asp:CheckBox ID="chkStatus" runat="server" Font-Bold="true" Checked='<%#(Eval("ExamMarkTypeId")).ToString() == "2" ? true : false %>' Text="Absent" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged"/>
                        </ItemTemplate>
                        <ItemStyle CssClass="center" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button ID="SubmitAllMarkButton" runat="server"  Text="Submit All" OnClick="SubmitAllMarkButton_Click" />
                        </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="SubmitButton" CommandName="ResultSubmit" Text="Submit" CommandArgument='<%# Bind("CourseHistoryId") %>' runat="server"></asp:LinkButton>
                        <%--<asp:LinkButton ID="DeleteButton" CommandName="TestSetDelete" Text="Delete" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>--%>
                    </ItemTemplate>
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle CssClass="center" />
                    </asp:TemplateField>
                </Columns>
            
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnFinalSubmitAll" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnFinalSubmitAll" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>