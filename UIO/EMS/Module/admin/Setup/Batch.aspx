<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Setup_Batch" CodeBehind="Batch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Program Batch Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <style>
        @media (max-width: 576px) {
            #ctl00_MainContainer_pnlpopup {
                background-color: rgb(227, 234, 235);
                width: 333px !important;
                position: fixed !important;
                z-index: 100001 !important;
                left: 25.5px !important;
                top: 175px !important;
            }
        }

        #ctl00_MainContainer_pnlpopup {
            background-color: rgb(227, 234, 235);
            width: 500px;
            position: fixed;
            z-index: 100001;
            left: -62.5px;
            top: 27px;
        }
    </style>
    <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
            document.getElementById("blurOverlay").style.display = "block";
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
            document.getElementById("blurOverlay").style.display = "none";
        }
    </script>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">


    <div class="PageTitle">
        <h4>Program Batch Management</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card" style="margin-top: 5px">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Program</b>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="ShortName" DataValueField="ProgramID" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" Height="38px" Width="100%"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-success" Text="Add Batch" OnClick="btnAddNewBatch_Click" Style="display: inline-block; width: 100%; margin-top: 20px; height: 38px; text-align: center; font-size: 17px;" />
                        </div>
                    </div>
                </div>

            </div>



            <div class="card" style="margin-top: 10px">
                <div class="card-body">


                    <asp:GridView ID="gvBatch" OnSorting="gvBatch_Sorting" AllowSorting="True" runat="server" CssClass="gridview-container table-striped table-bordered"
                        AutoGenerateColumns="False" ShowFooter="false" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">

                        <HeaderStyle BackColor="#0D2D62" ForeColor="White" Height="35px" />
                        <RowStyle BackColor="#ecf0f0" />
                        <AlternatingRowStyle BackColor="#ffffff" />

                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Program Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProgramName" Font-Bold="True" Text='<%#Eval("Program.ShortName") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBatchNO" Font-Bold="True" Text='<%#Eval("BatchNO") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Batch Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBatchName" Font-Bold="True" Text='<%#Eval("Attribute1") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Session" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblWebsite" Font-Bold="True" Text='<%#Eval("Session.FullCode") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Update" Text="Edit" ID="lbEdit" CommandArgument='<%#Eval("BatchId")%>' OnClick="lbEdit_Click" CssClass="btn btn-info btn-sm"> 
                                    <i class="fa fa-edit"></i>
                                    </asp:LinkButton>

                                    &nbsp;

                                     <asp:LinkButton runat="server" ToolTip="Delete" Text="Delete" ID="lbDelete" CommandArgument='<%#Eval("BatchId") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')" CssClass="btn btn-danger btn-sm"> 
                                   <i class="fa fa-trash"></i>
                                         </asp:LinkButton>

                                </ItemTemplate>
                                <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                        </Columns>


                    </asp:GridView>

                </div>
            </div>


        </ContentTemplate>

    </asp:UpdatePanel>


    <div>
        <div style="clear: both;"></div>
        <div style="margin: 10px; width: 100%;">
            <div style="clear: both;"></div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                        CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="#e3eaeb" Width="500px" Style="display: none">
                        <div style="padding: 5px;">
                            <fieldset style="padding: 5px">
                                <legend style="font-weight: bold; font-size: large; text-align: center">Batch Insert / Edit</legend>

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:Label ID="LabelMessage" runat="server" Text="" ForeColor="Red"></asp:Label>


                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <b>Program</b>
                                            </div>

                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" EnableViewState="true" runat="server" ID="ddlProgram" DataTextField="ShortName" DataValueField="ProgramID" CssClass="form-control" Height="38px" Width="100%" />

                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <b>Session</b>
                                            </div>

                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" EnableViewState="true" runat="server" ID="ddlSession" CssClass="form-control" Height="38px" Width="100%" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <b>Batch No</b>
                                            </div>

                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <b>Batch Name</b>
                                            </div>

                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:TextBox ID="txtBatchName" runat="server" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>

                                            </div>
                                        </div>


                                        <div class="row" style="margin-top: 5px">

                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:Button ID="btnInsert" runat="server" Text="AddNew" OnClick="btnInsert_Click" CssClass="btn btn-info btn-sm" Style="display: inline-block; width: 100%; margin-top: 20px; height: 38px; text-align: center; font-size: 17px;"></asp:Button>

                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" Style="display: inline-block; width: 100%; margin-top: 20px; height: 38px; text-align: center; font-size: 17px;" />

                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </fieldset>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

    <ajaxToolkit:UpdatePanelAnimationExtender
        ID="UpdatePanelAnimationExtender1"
        TargetControlID="UpdatePanel1"
        runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="btnAddNew" 
                                  Enabled="false" />  
                                    
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="btnAddNew" 
                                    Enabled="true" />
                </Parallel>
            </OnUpdated>
            </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>
