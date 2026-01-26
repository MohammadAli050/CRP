<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="StudentFeedbackPage.aspx.cs" Inherits="StudentFeedbackPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Feedback</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <style>
        .btn {
            border: none;
            color: white;
            font-weight: bold;
            cursor: pointer;
        }

        .success {
            background-color: #9acd32;
        }

            .success:hover {
                background-color: #8ab92d;
            }

        .info {
            background-color: #2196F3;
        }

            .info:hover {
                background: #0b7dda;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'inline-block';
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
            <label>Student Feedback/Request</label>
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
                <div class="TeacherManagement-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <table style="width: 373px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Name : "></asp:Label></td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div class="loadArea">
                            <table style="width: 739px">
                                <tr>
                                    <td class="auto-style12">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Feedback"></asp:Label></td>

                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server" Width="458px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmitFeedback" CssClass="btn success" Height="45px" Width="130px" runat="server" Text="Submit" OnClick="btnSubmitFeedback_Click" />
                                    </td>
                                    <td class="auto-style8"></td>

                                </tr>
                            </table>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

        <ajaxToolkit:UpdatePanelAnimationExtender
            ID="UpdatePanelAnimationExtender1"
            TargetControlID="UpdatePanel4"
            runat="server">
            <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnSubmitFeedback" 
                                  Enabled="false" />                   
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnSubmitFeedback" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>


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
                                    <HeaderStyle Width="90%" />
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
