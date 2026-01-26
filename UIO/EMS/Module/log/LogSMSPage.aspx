<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" CodeBehind="LogSMSPage.aspx.cs" Inherits="EMS.miu.log.LogSMSPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    LOG General
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <link href="../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
        });

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
            <label>Log General</label>
        </div>

        <div class="ClassSchedule-container">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="div-margin">
                        </br>
                        <div class="loadedArea">
                             <b>
                                 <label class="display-inline field-Title" style="color:red;">Remaining Credits: </label>
                                 <asp:Label runat="server" ID="lblCredits" style="color:red;"></asp:Label>
                             </b>
                        </div>
                        </br>
                        <div class="loadedArea">

                            <label class="display-inline field-Title">From Date :</label>
                            <asp:TextBox runat="server" ID="DateFromTextBox" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateFromTextBox" Format="dd/MM/yyyy" />
                            <label class="display-inline field-Title">To Date :</label>
                            <asp:TextBox runat="server" ID="DateToTextBox" Width="170px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="DateToTextBox" Format="dd/MM/yyyy" />
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size" Width="61px" />
                        </div>

                        <div class="loadedArea" style="margin-top:10px">
                            <label class="display-inline field-Title">Sender :</label>
                            <asp:TextBox runat="server" ID="txtFilterSender" Width="100" class="margin-zero label-width" />

                            <label class="display-inline field-Title">Recipient :</label>
                            <asp:TextBox runat="server" ID="txtFilterRecipient" Width="100" class="margin-zero label-width" />

                            <label class="display-inline field-Title">Message :</label>
                            <asp:TextBox runat="server" ID="txtFilterMsg" Width="100" class="margin-zero label-width" />

                             <label class="display-inline field-Title">Status :</label>
                            <asp:TextBox runat="server" ID="txtFilterStatus" Width="100" class="margin-zero label-width" />

                            <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="150px" Width="150px" />
        </div>
        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel4"
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
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div>
                    </br>
                    <asp:GridView ID="gvLogGeneral" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%" 
                        AllowPaging="true" PageSize="50" OnPageIndexChanging="gvLogGeneral_PageIndexChanging" PagerStyle-HorizontalAlign="Right"
                        PagerStyle-CssClass="GridPager" OnSelectedIndexChanged="gvLogGeneral_SelectedIndexChanged">
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="First" LastPageText="Last" Position="TopAndBottom" />
                        <HeaderStyle BackColor="#CC9966" ForeColor="White" Height="30" />
                        <AlternatingRowStyle BackColor="#FFFFCC" />
                        <RowStyle Height="24px" />

                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDate" Text='<%#Eval("Date") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sender" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSender" Text='<%#Eval("Sender") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recipient" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblReceipient" Text='<%#Eval("Receipient") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMsg" Text='<%#Eval("Message") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("Status") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <label>Data Not Found</label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="rowCss" />
                        <HeaderStyle CssClass="tableHead" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
