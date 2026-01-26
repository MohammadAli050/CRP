<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    CodeBehind="ExamResultApproveByExamController.aspx.cs" Inherits="ExamResultApproveByExamController" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Exam Result Approve By Exam Controller</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

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

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">

    <div class="PageTitle">
        <label>Result Approve By Exam Controller</label>
    </div>
    <div style="height: 30px;">
        <div id="divProgress" class="floatRight" style="padding-top: 7px; display: none;">
            <div class="floatRight" >
                <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage1" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
                <asp:Image ID="LoadingImage2" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="20px" />
            </div>
        </div>
    </div>
    <div class="cleaner"></div>

    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <label class="msgTitle">Message: </label>
                <asp:Label runat="server" ID="lblMsg" Text="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>
            <div class="ExamResultApproveByExamController-container">
                <div class="div-margin">
                    <div class="loadArea">
                        <label class="display-inline field-Title">Calender Type</label>
                        <asp:DropDownList runat="server" ID="ddlCalenderType" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="CalenderType_Changed" />

                        <label class="display-inline field-Title">Academic Calender</label>
                        <asp:DropDownList runat="server" ID="ddlAcademicCalender" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="AcademicCalender_Changed" />

                        <label class="display-inline field-Title">Program</label>
                        <asp:DropDownList runat="server" ID="ddlProgram" class="margin-zero dropDownList" AutoPostBack ="true" OnSelectedIndexChanged="Program_Changed" />

                        <label class="display-inline field-Title">Course</label>
                        <asp:DropDownList runat="server" ID="ddlCourse" class="margin-zero dropDownList1" AutoPostBack="true" OnSelectedIndexChanged="Course_Changed" />
                    </div>
                    <div class="loadedArea">
                        <asp:Button ID="btnLoad" runat="server" Text="Load" class="button-margin btn-size" OnClick="Load_Click" />
                        <asp:Panel runat="server" ID="pnSummaryReport" style="display: inline-block;">
                            <label class="display-inline field-Title">Total Course</label>
                            <asp:Label runat="server" Font-Bold="true" ID="lblTotalCourse" class="display-inline field-Title-Fix" Text="" />

                            <label class="display-inline field-Title">Mark Submit</label>
                            <asp:Label runat="server" Font-Bold="true" ID="lblMarkSubmit" class="display-inline field-Title-Fix" Text="" />

                            <label class="display-inline field-Title">Final Submit</label>
                            <asp:Label runat="server" Font-Bold="true" ID="lblFinalSubmit" class="display-inline field-Title-Fix" Text="" />

                            <label class="display-inline field-Title">Approved</label>
                            <asp:Label runat="server" Font-Bold="true" ID="lblApproed" class="display-inline field-Title-Fix" Text="" />
                        </asp:Panel>
                    </div>
                    <div class="loadedArea">
                        <asp:Button ID="btnPublish" runat="server" Text="Publish All" class="button-margin btn-size2" OnClick="Publish_Click" OnClientClick="if (!confirm('Are you sure you want to Publish Results of the selected Program?')) return false;" />
                        <asp:Button ID="btnSinglePublish" runat="server" Text="Publish: Individual Course" class="button-margin btn-size2" OnClick="SinglePublish_Click" OnClientClick="if (!confirm('Are you sure you want to publish the Result of the selected Course ?')) return false;" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel03" runat="server">
        <ContentTemplate>
            <div class="ExamResultApproveByExamController-container">
                <asp:Panel ID="PnlExamMarkApprove" runat="server" Wrap="False">
                    <asp:gridview ID="gvExamMarkApprove" runat="server" AutoGenerateColumns="False" class="mainTable" OnSelectedIndexChanged="gvExamMarkApprove_SelectedIndexChanged" >
                        <RowStyle Height="24px" />
                        <AlternatingRowStyle BackColor="#f5fbfb" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <HeaderStyle Width="45px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="AcaCal Section ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <ItemTemplate><asp:Label runat="server" ID="lblAcaCal_SectionID" Font-Bold="False" Text='<%#Eval("AcaCal_SectionID") %>' /></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <ItemTemplate><asp:Label runat="server" ID="lblCode" Font-Bold="False" Text='<%#Eval("FormalCode") %>' /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><asp:Label runat="server" ID="lblTitle" Font-Bold="False" Text='<%#Eval("Title")%>' class="display-inline field-Title2" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><asp:Label runat="server" ID="lblFaculty" Font-Bold="False" Text='<%#Eval("Teacher")%>' class="display-inline field-Title2" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#Student(s)" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><asp:Label runat="server" ID="lblTotalStudent" Font-Bold="False" Text='<%#Eval("TotalStudent")%>' class="display-inline field-Title2" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><asp:Label runat="server" ID="lblSection" Font-Bold="False" Text='<%#Eval("SectionName")%>' class="display-inline field-Title2" /></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Submitted" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsFinalSubmit" Enabled="false" Checked='<%#Eval("IsFinalSubmit") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Published" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsPublish" Enabled="false" Checked='<%#Eval("IsPublish") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Is Transfer" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsTransfer" Enabled="false" Checked='<%#Eval("IsTransfer") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Load Result" ID="lbLoadResult" CommandArgument='<%#Eval("AcaCal_SectionID")+","+ Eval("SectionName") %>' OnClick="lbLoadResult_Click">
                                        <input type="button" id="btn1" value="View Result" />
                                       <%-- <asp:Button runat="server" ID="Button1" Text="View Result"/>--%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Hard Copy Received" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkHardCopyReceived" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Re-Submit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Visible='<%#Eval("IsFinalSubmit").ToString() == "True" ? Eval("IsTransfer").ToString() == "True" ? false : true : false %>' ToolTip="Resubmit" ID="lbResubmit" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="Resubmit_Click" OnClientClick="if (!confirm('Are you sure you want to Re-Submit?')) return false;">
                                        <%--<asp:Button runat="server" ID="btnApprove" Text="Approve" />--%>
                                        <input type="button" value='Re-Sumbit' />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approve/Approved" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Visible='<%#Eval("IsFinalSubmit").ToString() == "True" ? true : false %>' Enabled='<%#Eval("IsTransfer").ToString() == "True" ? false : true %>' ToolTip="Approve" ID="lbApprove" CommandArgument='<%#Eval("AcaCal_SectionID") %>' OnClick="Approve_Click" OnClientClick="if (!confirm('Are you sure you want to Approve ?')) return false;">
                                        <%--<asp:Button runat="server" ID="btnApprove" Text="Approve" />--%>
                                        <input type="button" <%#Eval("IsTransfer").ToString() == "True" ? "disabled" : "" %> value='<%#Eval("IsTransfer").ToString() == "True" ? "Approved" : "Approve" %>' />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                           

                            <%--<asp:TemplateField HeaderText="Section" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><asp:TextBox runat="server" ID="txtMark1" Text='<%#Eval("Mark1").ToString() == "0.00" ? "" :  Eval("Mark1") %>' class="margin-zero input-Size" /></ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>--%>
                            
                        </Columns>
                        <EmptyDataTemplate>
                            No Data Found !!
                        </EmptyDataTemplate>
                        <RowStyle CssClass="rowCss" />
                        <HeaderStyle CssClass="tableHead" />
                    </asp:gridview>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvExamMarkApprove" />
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel03" runat="server">
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

    <%--<ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" TargetControlID="UpdatePanel01" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="lbApprove" Enabled="false" />                   
                </Parallel>
            </OnUpdating>
                <OnUpdated>
                    <Parallel duration="0">
                        <ScriptAction Script="onComplete();" />
                        <EnableAction   AnimationTarget="lbApprove" Enabled="true" />
                    </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>--%>
    <div>&nbsp</div>
    <rsweb:ReportViewer ID="ExamResultViewPrint" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" asynrendering="true" SizeToReportContent="true">      
    </rsweb:ReportViewer>

</asp:Content>
