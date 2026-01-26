<%@ Page Title="Bill Delete" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="BillDelete.aspx.cs" Inherits="EMS.miu.bill.BillDelete" %>

<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bill Delete
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
<div>
    <div class="PageTitle">
        <label>Bill Delete</label>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="Message-Area">
                <asp:Label ID="Label1" runat="server" Text="Message : "></asp:Label>
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table style="padding: 1px; width: auto;">
                    <tr>
                        <td class="auto-style4">
                            <b>Program :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>

                        <td class="auto-style4">
                            <b>Batch :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:BatchUserControl runat="server" ID="ucBatch" />
                        </td>

                        <td class="auto-style4">
                            <b>Session :</b>
                        </td>
                        <td class="auto-style6">
                            <uc1:SessionUserControl runat="server" ID="ucSession" />
                        </td>

                        <td class="auto-style4">
                            <b>Student Roll :</b>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtStudentRoll" PlaceHolder="Student Roll" runat="server" Width="150px"></asp:TextBox>
                        </td>

                        <td class="auto-style1">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>
                        <td class="auto-style1">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                </table>
                <div id="divProgress" style="display: none; float: right; z-index: 1000; margin-top: -38px">
                    <div style="float: left">
                        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="35px" Width="35px" />
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvBillDelete"  runat="server"  AutoGenerateColumns="False" CssClass="table-bordered"
                EmptyDataText="No data found." AllowPaging="false" CellPadding="4" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:TemplateField Visible ="false"  HeaderText="Id">
                        <ItemTemplate >
                            <asp:Label ID="lblStudentId"  runat="server" Text='<%# Bind("StudentID") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField Visible ="false"  HeaderText="BillHistoryMasterId">
                        <ItemTemplate >
                            <asp:Label ID="lblBillHistoryMasterId"  runat="server" Text='<%# Bind("BillHistoryMasterId") %>'></asp:Label> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="150px" />
                    </asp:TemplateField>

                    <asp:TemplateField Visible ="True">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkAllStudentHeader" OnCheckedChanged="chkAllStudentHeader_CheckedChanged" Text="" AutoPostBack="true"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate >
                            <asp:CheckBox ID="CheckBox"  runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="30px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Roll"  HeaderText="Student Roll">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Name"  HeaderText="Student Name">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="250px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ReferenceNo"  HeaderText="Reference No">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Amount"  HeaderText="Amount">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="BillingDate"  HeaderText="BillingDate" DataFormatString="{0:dd-MMM-yy}">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>

                </Columns>
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

    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel2"
        runat="server">
        
        <Animations>
         <OnUpdating>
            <Parallel duration="0">
            <ScriptAction Script="InProgress();" />
            <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel>
         </OnUpdating>
         <OnUpdated>
            <Parallel duration="0">
            <ScriptAction Script="onComplete();" />
            <EnableAction   AnimationTarget="btnLoad" Enabled="true" />
         </Parallel>
         </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>

</div>
</asp:Content>
