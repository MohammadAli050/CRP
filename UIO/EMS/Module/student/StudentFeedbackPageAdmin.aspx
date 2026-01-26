<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentFeedbackPageAdmin.aspx.cs" Inherits="StudentFeedbackPageAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Feedback</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />


    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'inline-block';
        }

        function onComplete() {
            var panelProg = $get('MainContainer_PnProcess');
            panelProg.style.display = 'none';
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div>
        <div class="PageTitle">
            <label>Student Feedback</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>
         
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="GridViewTable">
                        <asp:GridView runat="server" ID="gvPreviousFeedback" AutoGenerateColumns="False"
                            AllowPaging="false" PagerSettings-Mode="NumericFirstLast"
                            PageSize="20" CssClass="gridCss">
                            <HeaderStyle BackColor="SeaGreen" ForeColor="White" Height="35" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />

                            <Columns>

                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Messages" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMessage" Text='<%#Eval("Message") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Roll" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRoll" Text='<%#Eval("Roll") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RegistrationNo" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegistrationNo" Text='<%#Eval("RegistrationNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Phone Number" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPhoneNumber" Text='<%#Eval("Phone") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Submit Date" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSubmitDate" Text='<%#Eval("CreatedDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <RowStyle Height="25px" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <EmptyDataTemplate>
                                No data found!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>




    </div>
</asp:Content>
