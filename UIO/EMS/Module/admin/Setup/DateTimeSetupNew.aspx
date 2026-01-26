<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="DateTimeSetupNew.aspx.cs" Inherits="EMS.Module.admin.Setup.DateTimeSetupNew" %>


<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Date Time Set-Up</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript" src="../JavaScript/jquery-1.7.1.js"></script>
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


        $(document).ready(function () {

            $('#ctl00_MainContainer_TimeSelector1 table tr td')[0].children[1].style.width = '10px';
            $('#ctl00_MainContainer_TimeSelector1 table tr td')[0].children[3].style.width = '10px';
            $('#ctl00_MainContainer_TimeSelector1 table tr td')[0].children[5].style.width = '10px';

            $('#ctl00_MainContainer_TimeSelector2 table tr td')[0].children[1].style.width = '10px';
            $('#ctl00_MainContainer_TimeSelector2 table tr td')[0].children[3].style.width = '10px';
            $('#ctl00_MainContainer_TimeSelector2 table tr td')[0].children[5].style.width = '10px';
        });


    </script>

    <style type="text/css">
        .blink {
            animation: blinker 0.6s linear infinite;
            color: #1c87c9;
            font-size: 30px;
            font-weight: bold;
            font-family: sans-serif;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        #ctl00_MainContainer_ucAccessableProgram_ddlProgram {
            width: 100% !important;
            height: 38px !important;
            border: 1px solid #ccc;
        }

        #ctl00_MainContainer_TimeSelector1, #ctl00_MainContainer_TimeSelector2 {
            margin-top: 15px;
        }


            #ctl00_MainContainer_TimeSelector1 input, #ctl00_MainContainer_TimeSelector2 input {
                border: none !important;
            }

            #ctl00_MainContainer_TimeSelector1 table tr, #ctl00_MainContainer_TimeSelector2 table tr {
                border: 1px solid #ccc;
            }

        #ctl00\$MainContainer\$TimeSelector1_imgUp, #ctl00\$MainContainer\$TimeSelector2_imgUp {
            position: absolute;
            margin-top: 10px;
            margin-left: -13px;
            width: 25px;
        }

        #ctl00\$MainContainer\$TimeSelector1_imgDown, #ctl00\$MainContainer\$TimeSelector2_imgDown {
            width: 25px;
        }
        /* #ctl00\$MainContainer\$TimeSelector2_imgUp {
            position: absolute;
            margin-top: 16px;
            margin-left: -9px;
        }*/
        #ctl00_MainContainer_chkIsActive {
            height: 17px;
            width: 22px;
        }

        #ctl00\$MainContainer\$TimeSelector1_txtHour, #ctl00\$MainContainer\$TimeSelector2_txtHour {
            height: 30px !important;
            width: 21px !important;
            font-size: 12pt !important;
        }

        #ctl00\$MainContainer\$TimeSelector1_txtMinute, #ctl00\$MainContainer\$TimeSelector2_txtMinute {
            height: 30px !important;
            width: 21px !important;
            font-size: 12pt !important;
        }

        #ctl00\$MainContainer\$TimeSelector1_txtSecond, #ctl00\$MainContainer\$TimeSelector2_txtSecond {
            height: 30px !important;
            width: 21px !important;
            font-size: 12pt !important;
        }

        #ctl00\$MainContainer\$TimeSelector1_txtAmPm, #ctl00\$MainContainer\$TimeSelector2_txtAmPm {
            height: 30px !important;
            width: 30px !important;
            font-size: 12pt !important;
        }

        img#ctl00\$MainContainer\$TimeSelector1_imgUp, img#ctl00\$MainContainer\$TimeSelector2_imgUp {
            margin-left: 0px;
        }

        input#ctl00_MainContainer_chkIsActive {
            height: 30px;
            width: 30px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">


    <div class="PageTitle">
        <h4>Date Time Set-Up</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>




    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>

            <div class="card">
                <div class="card-body">

                    <div class="form-group row">

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label8" runat="server" Text="Calender Type" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <span class="text-danger">*</span>
                            <asp:DropDownList runat="server" ID="ddlCalenderType" Style="width: 100%;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCalenderType_SelectedIndexChanged" />

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label1" runat="server" Text="Semester/Trimester" Font-Bold="true" Font-Size="Large"></asp:Label><span class="text-danger"> *</span>
                            <asp:DropDownList runat="server" ID="ddlAcaCalBatch" Style="width: 100%;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="AcaCal_Change" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label4" runat="server" Text="Program" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <uc1:ProgramUserControl runat="server" ID="ucAccessableProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged"></uc1:ProgramUserControl>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="Type" Font-Bold="true" Font-Size="Large" Width="100%"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlActivityType" Style="width: 100%;" CssClass="form-control" DataValueField="ValueID" DataTextField="ValueName" AutoPostBack="true" OnSelectedIndexChanged="ActivityType_Change" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="Load_Click" CssClass="btn btn-info" Width="100%" Style="margin-top: 25px" />
                        </div>
                    </div>

                    <div class="form-group row">

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label2" runat="server" Text="Access Valid From :" Font-Bold="true" Font-Size="Large" Width="100%"></asp:Label>
                            <asp:TextBox runat="server" ID="DateFromTextBox" Style="width: 100%; height: 39px;" CssClass="form-control" placeholder="dd/MM/yyyy" DataFormatString="{0:dd/MM/yyyy}" />
                            <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateFromTextBox" Format="dd/MM/yyyy" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label7" runat="server" Text="Start Time:" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <cc1:TimeSelector ID="TimeSelector1" runat="server"></cc1:TimeSelector>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label3" runat="server" Text="Access Valid To :" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <asp:TextBox runat="server" ID="DateToTextBox" Style="width: 100%; height: 39px;" CssClass="form-control" placeholder="dd/MM/yyyy" DataFormatString="{0:dd/MM/yyyy}" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="DateToTextBox" Format="dd/MM/yyyy" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label6" runat="server" Text="End Time:" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <cc1:TimeSelector ID="TimeSelector2" runat="server"></cc1:TimeSelector>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-1" style="text-align: center">
                            <label style="color: green; font-size: larger; margin-bottom: 0px;">Open</label>
                            <div>
                                <asp:CheckBox ID="chkIsActive" Text="" ForeColor="Green" Style="" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3" runat="server" id="divSaveUpdateButton">
                            <asp:Button runat="server" ID="btnSaveUpdate" Text="Save" OnClick="SaveUpdate_Click" CssClass="btn btn-info" Style="width: 50%; margin-top: 25px" />

                            <asp:Button runat="server" ID="btnUpdateSelected" Text="Update" OnClick="btnUpdateSelected_Click" CssClass="btn btn-info" Style="width: 50%; margin-top: 25px" />


                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="Cancel_Click" CssClass="btn btn-danger" Style="width: 140px; margin-top: 25px" />

                        </div>
                    </div>


                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="UpdatePanel03">
        <ContentTemplate>

            <asp:Panel ID="pnlDateTimeSetUp" CssClass="panel-body" runat="server" Width="100%" Wrap="False">

                <div class="card" style="margin-top: 5px">
                    <div class="card-body">

                        <asp:GridView ID="gvDateTimeSetUp" runat="server" AutoGenerateColumns="False" TabIndex="6" Width="100%" CssClass="gridview-container table-striped table-bordered">
                            <RowStyle CssClass="hoverable" />
                            <AlternatingRowStyle BackColor=" #E9FBBB" />
                            <Columns>

                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                        <asp:Label runat="server" ID="lblSemester" Font-Bold="True" Text='<%#Eval("AcaCalName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox runat="server" ID="ChkActive" AutoPostBack="true" OnCheckedChanged="ChkActive_CheckedChanged"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Program" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblProgram" Font-Bold="False" Text='<%#Eval("ProgramName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblType" Font-Bold="True" Text='<%#Eval("TypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStartDate" Font-Bold="False" Text='<%#Eval("StartDate", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStartTime" Font-Bold="True" Text='<%#Eval("StartTime", "{0:hh:mm tt}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEndDate" Font-Bold="False" Text='<%#Eval("EndDate", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEndTime" Font-Bold="True" Text='<%#Eval("EndTime", "{0:hh:mm tt}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIsActive" Font-Bold="True"
                                            ForeColor='<%# (Boolean.Parse(Eval("IsActive").ToString())) ? System.Drawing.Color.Blue : System.Drawing.Color.Red %>'
                                            Text='<%# (Boolean.Parse(Eval("IsActive").ToString())) ? "Open" : "Close" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%#Eval("Id") %>' runat="server" ToolTip="Update" ID="lbSave" OnClick="lbUpdate_Click" CssClass="btn btn-info">
                                            <i class="fa fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CssClass="btn btn-danger" CommandArgument='<%#Eval("Id") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <i class="fa fa-trash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

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


</asp:Content>


