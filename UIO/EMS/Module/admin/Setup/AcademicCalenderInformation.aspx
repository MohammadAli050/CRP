<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="AcademicCalenderInformation" CodeBehind="AcademicCalenderInformation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Academic Calender Management
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
            z-index: 1000;
            left: -62.5px;
            top: 27px;
        }

        .swal-modal {
            z-index: 100000 !important;
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
        <h4>Academic Calender Management</h4>
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

                            <b>Calender Type</b>
                            <asp:DropDownList ID="ddlCalenderMaster" runat="server" DataTextField="CalenderTypeName" DataValueField="CalenderTypeID" AutoPostBack="true" OnSelectedIndexChanged="ddlCalenderType_SelectedIndexChanged" CssClass="form-control" Height="38px" Width="100%"></asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-success" Text="Add New" OnClick="btnAddNew_Click" Style="display: inline-block; width: 100%; margin-top: 20px; height: 38px; text-align: center; font-size: 17px;" />
                        </div>
                    </div>
                </div>

            </div>



            <div class="card" style="margin-top: 10px">
                <div class="card-body">


                    <asp:GridView ID="gvSessionList" AllowSorting="false" runat="server" CssClass="gridview-container table-striped table-bordered"
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

                            <asp:TemplateField HeaderText="Calender Master" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCalenderMaster" Font-Bold="True" Text='<%#Eval("calenderUnitMaster.Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit Type" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitType" Font-Bold="True" Text='<%#Eval("CalendarUnitType_TypeName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblYear" Font-Bold="True" Text='<%#Eval("Year") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Session Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCode" Font-Bold="True" Text='<%#Eval("Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStartDate" Font-Bold="True" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEndDate" Font-Bold="True" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sequence" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSequence" Font-Bold="True" Text='<%#Eval("Sequence") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ToolTip="Update" Text="Edit" ID="lbEdit" CommandArgument='<%#Eval("AcademicCalenderID")%>' OnClick="lbEdit_Click" CssClass="btn btn-info btn-sm"> 
        <i class="fa fa-edit"></i>
                                    </asp:LinkButton>

                                    &nbsp;

         <asp:LinkButton runat="server" ToolTip="Delete" Text="Delete" ID="lbDelete" CommandArgument='<%#Eval("AcademicCalenderID") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')" CssClass="btn btn-danger btn-sm"> 
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
                                <legend style="font-weight: bold; font-size: large; text-align: center">Academic Calender Insert/Update</legend>

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <hr />
                                        <asp:HiddenField ID="hdnValue" runat="server" />

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Calender Unit</b>
                                                <asp:DropDownList ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" runat="server" CssClass="form-control" Height="38px" Width="100%" />
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Calender Unit Type</b>
                                                <asp:DropDownList ID="ddlCalenderUnitType" runat="server" CssClass="form-control" Height="38px" Width="100%" />

                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Year</b>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Height="38px" Width="100%" />
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Session Code</b>
                                                <asp:TextBox ID="txtSessionCode" runat="server" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Start Date</b>
                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                    Format="dd-MM-yyyy" />
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>End Date</b>
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate"
                                                    Format="dd-MM-yyyy" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <b>Sequence</b>
                                                <asp:TextBox ID="txtSequence" runat="server" TextMode="Number" CssClass="form-control" Height="38px" Width="100%"></asp:TextBox>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                            </div>

                                        </div>



                                        <div class="row" style="margin-top: 10px">

                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                            </div>

                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <asp:Button ID="btnInsert" runat="server" Text="AddNew" OnClick="btnInsert_Click" OnClientClick="this.value = 'Updating Data....'; this.disabled = true;" UseSubmitBehavior="false" CssClass="btn btn-info btn-sm" Style="display: inline-block; width: 100%; margin-top: 20px; height: 38px; text-align: center; font-size: 17px;"></asp:Button>

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
