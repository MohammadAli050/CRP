<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_CourseOfferAndCount"  Codebehind="CourseOfferAndCount.aspx.cs" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/TreeUserControl.ascx" TagPrefix="uc1" TagName="TreeUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Course Offer & Active</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <style>
        .msgPanel {
            margin-top: 20px;
            margin-bottom: 25px;
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
    <div style="padding: 5px; width: 100%;">
        <div class="PageTitle">
            <label>Course Offer & Active</label>
        </div>

        <div class="Message-Area" style="height: 45px; width: 95%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div >
                        <table style="padding: 5px; width: 100%;">
                            <tr>
                                 <td class="auto-style5">Program</td>
                                <td class="auto-style4">                                   
                                    <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"/>
                                </td>
                                <td>Session</td>
                                <td>
                                    <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="OnSessionSelectedIndexChanged"/>
                                </td>
                               
                                <td>Tree</td>
                                <td>                                 
                                    <uc1:TreeUserControl runat="server" ID="ucTree"  OnTreeSelectedIndexChanged="OnTreeSelectedIndexChanged"/>
                                </td>
                                <td><asp:CheckBox  ID="chkIsOffer" runat="server" Checked="true" Text="Load Only Offered Courses"/></td>
                                <td  >

                                    <asp:Button ID="btnLoad" runat="server" Width="90px" Text="Load" ForeColor="Blue" OnClick="btnLoad_Click" /> &nbsp &nbsp
                                    <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" ForeColor="Red" Text="Generate Course" />
                                </td>
                                <td >

                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div id="divProgress" style="display:none  ; width: 195px; float: right; margin: -43px -140px 0 0;">
                        <div style="float: left">
                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="50px" Width="50px" />
                        </div>                       
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true" CssClass="msgPanel">                    
                    <asp:Label ID="Label1" runat="server" Text="Message : " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <asp:LinkButton ID="lBtnContinueAnyWay" runat="server" Text="Continue Anyway..."
                        OnClick="lBtnContinueAnyWay_Click" Enabled="true" Visible="true"></asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lBtnCancel" runat="server" Text="Cancel" OnClick="lBtnCancel_Click"> </asp:LinkButton>                   
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear: both;"></div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel2"
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

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender2"
            TargetControlID="UpdatePanel2"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script = "InProgress();" />
                    <EnableAction AnimationTarget = "btnGenerate" 
                                  Enabled = "false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnGenerate" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>



        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="width: 95%; margin-top: 20px;">

                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Count : "></asp:Label>
                    <asp:Label ID="lblCourseCount" runat="server" Font-Bold="true"></asp:Label>

                    <asp:GridView runat="server" ID="gvOfferedCourse" AutoGenerateColumns="False" Width="100%"
                        ShowHeader="true" CssClass="gridCss">
                        <HeaderStyle BackColor="SeaGreen" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Course Code">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("OfferID") %>' />
                                    <asp:HiddenField ID="hdnCourseID" runat="server" Value='<%#Eval("CourseID") %>' />
                                    <asp:HiddenField ID="hdnVersionID" runat="server" Value='<%#Eval("VersionID") %>' />
                                    <asp:HiddenField ID="hdnNode_CourseID" runat="server" Value='<%#Eval("Node_CourseID") %>' />
                                    <asp:Label runat="server" ID="lblCourseCode" Text='<%#Eval("FormalCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Version Code">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblVersionCode" Text='<%#Eval("VersionCode") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Title">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCourseTitle" Text='<%#Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit">
                                <ItemTemplate>
                                    <center>
                                    <asp:Label runat="server" ID="lblCourseCredit" Text='<%#Eval("CourseCredit") %>'></asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pre Registration limit">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="lblLimit" Text='<%#Eval("Limit") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="110px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opened">
                                <ItemTemplate>
                                    <center>
                                    <asp:Label runat="server" ID="lblOpened"   Width="90"  Text='<%#Eval("Opened") %>'> </asp:Label>
                                        </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opened All">
                                <ItemTemplate>
                                   <center>
                                    <asp:Label runat="server" ID="lblOpenedAll" Width="90" Text='<%#Eval("OpenedAll") %>'> </asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="Assigned">
                                <ItemTemplate>
                                    <center>
                                    <asp:Label runat="server" ID="lblAssigned" Width="90" Text='<%#Eval("Assigned") %>'> </asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Assigned All">
                                <ItemTemplate>
                                   <center>
                                    <asp:Label runat="server" ID="lblAssignedAll" Width="90" Text='<%#Eval("AssignedAll") %>'> </asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mandatory">
                                <ItemTemplate>
                                  <center>
                                    <asp:Label runat="server" ID="lblMandatory" Width="90" Text='<%#Eval("Mandatory") %>'> </asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mandatory All">
                                <ItemTemplate>
                                   <center>
                                    <asp:Label runat="server" ID="lblMandatoryAll" Width="90" Text='<%#Eval("MandatoryAll") %>'> </asp:Label>
                                    </center>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>--%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Button ID="btnActiveAndOffer" OnClick="btnActiveAndOffer_Click" runat="server"
                                        Text="Save (Active/ Offer)" />
                                    <br />
                                    <hr />
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" ID="ChkActive" Checked='<%#Eval("IsActive") %>'></asp:CheckBox>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" />
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

