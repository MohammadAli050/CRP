<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentEnrollment.aspx.cs" Inherits="EMS.miu.admin.StudentEnrollment" %>
 
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Student Enrollment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
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
    <style type="text/css">
        .auto-style4 {
            width: 60px;
        }

        .auto-style5 {
            width: 280px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Student Enrollment</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">
                <div class="Message-Area">
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <div style="padding: 5px; margin: 5px; width: 900px;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Width="73px" Text="Test Roll"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdmissionTestRoll" Placeholder="Admission Test Roll" runat="server"></asp:TextBox>
                                <asp:Button ID="btnLoad" runat="server" Width="80px" Text="Load" OnClick="btnLoad_Click" />
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnLoad" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
                    </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnEnrollStudent" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="btnEnrollStudent" Enabled="true" />
                    </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlInfo" runat="server">
                <div class="Message-Area">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblStudentRoll" runat="server" Width="150px" Text="Student Roll"></asp:Label>
                            </td>
                            <td class="auto-style5">
                                <asp:TextBox ID="txtStudentRoll" Placeholder="Student Roll" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnEnrollStudent" runat="server" Text="Enroll" OnClick="btnEnrollStudent_Click" Height="44px" Width="200px" />
                            </td>

                        </tr>
                    </table>
                </div>
                <div class="Message-Area">
                    <div style="padding: 5px; margin: 5px; width: 900px;">
                        <table>

                            <tr>
                                <td>
                                    <asp:Label ID="lblStudentName" runat="server" Width="150px" Text="Student Name"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtStudentName" Placeholder="Student Name" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGender" runat="server" Width="150px" Text="Gender"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem Text="Male" Value="0" />
                                        <asp:ListItem Text="Female" Value="1" />
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContactAddress" runat="server" Width="150px" Text="Present Address"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtPresentAddress" TextMode="MultiLine" runat="server" Width="602px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Width="150px" Text="Permanent Address"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtPermanentAddress" TextMode="MultiLine" runat="server" Width="601px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Width="150px" Text="Father Name"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Width="150px" Text="Occupation"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtFatherOccupation" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Width="150px" Text="Official Address"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtFatherOfficialAddress" TextMode="MultiLine" runat="server" Width="602px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Width="150px" Text="Telephone"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtFathersTel" runat="server" Width="213px"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label8" runat="server" Text="Mobile"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFathersMob" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Width="150px" Text="Mother Name"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtMotherName" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Width="150px" Text="Occupation"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtMotherOccupation" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Width="150px" Text="Official Address"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMotherOfficialAddress" TextMode="MultiLine" runat="server" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Width="150px" Text="Telephone"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtMothersTel" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label13" runat="server" Text="Mobile"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMotherMob" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Width="150px" Text="Local Guardian Name"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtLocalGuardianName" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Width="150px" Text="Relation"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtRelation" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Width="150px" Text="Telephone"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtLocalTele" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label18" runat="server" Text="Mobile"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLocalMob" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Width="150px" Text="Email Address"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtLocalEmailAddress" runat="server" Width="235px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Width="150px" Text="Blood Group"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtBloodGroup" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Width="150px" Text="Marital Status"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtMeritalStutus" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" Width="150px" Text="Student Email Address"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtStudentEmail" runat="server" Width="239px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Width="150px" Text="Student Mobile"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtStudentMobile" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style4"></td>
                                <td></td>

                            </tr>

                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="gvExamList" runat="server" AutoGenerateColumns="false" ShowFooter="true" Width="80%">
                            <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                            <FooterStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                            <AlternatingRowStyle BackColor=" #F9F99F" />

                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Examination">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblExaination" Text='<%#Eval("TypeName ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="School/College/Institute">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Font-Bold="true" Text='<%#Eval("InstituteName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%#Eval("GPA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Passing Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text='<%#Eval("PassingYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Area of Study">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPhone" Text='<%#Eval("GroupOrSubject") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>

                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel> 

</asp:Content>
