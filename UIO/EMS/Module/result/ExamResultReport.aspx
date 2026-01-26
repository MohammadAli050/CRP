<%@ Page Title="Exam Result Report" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" 
    Inherits="Test_ExamResultReport" Codebehind="ExamResultReport.aspx.cs" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    Exam Result Report
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
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

    <%--<script type="text/javascript">
        function printPartOfPage(elementId) {
            var reportHeader = "";
            var hiddenDate = '';
            var uperPart = document.getElementById('PrintAreaVid').innerHTML;
            var windowUrl = '';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=70000,height=60000');
            //printWindow.document.write("<div align='center'>" + "<h2>" + reportHeader + "</h2>" + "</div>" + uperPart + "<div align='center'>" + "<h3>" + hiddenDate + "</h3>" + "</div>");
            printWindow.document.write("<div style='Height:12; Width:20'>" + "</div>");
            printWindow.document.write(PrintAreaVid.outerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            //printWindow.close();
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div class="PageTitle">
        <label>Exam Result Report</label>
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
                        <td class="auto-style4">
                            <b>Course :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlCourse" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td class="auto-style4">
                            <b>Section :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlAcaCalSection" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlAcaCalSection_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="ResultLoadButton" runat="server" Text="Load Result" OnClick="ResultLoadButton_Click" />   
                        </td>
                        <%--<td class="auto-style4">
                            <input type="button" value="Print" class="btn btn-primary" onclick="JavaScript: printPartOfPage('PrintAreaVid');"/>
                        </td>--%>
                    </tr>
                </table>
                
            </div>          
            <br />

            <asp:GridView ID="GridViewResultReport" runat="server"
                CssClass="table-bordered" EmptyDataText="No data found."  CellPadding="4"  Width="80%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
                <PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                        
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="ResultLoadButton" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="ResultLoadButton" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>    
</asp:Content>