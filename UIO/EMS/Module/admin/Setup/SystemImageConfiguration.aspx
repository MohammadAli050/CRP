<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="SystemImageConfiguration.aspx.cs" Inherits="EMS.Module.admin.Setup.SystemImageConfiguration" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">System Image Configuration</asp:Content>
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

        function previewImage(input) {
            var preview = document.getElementById('<%= imgPreview.ClientID %>');
            var previewContainer = document.getElementById('previewContainer');

            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    preview.src = e.target.result;
                    previewContainer.style.display = 'block';
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

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


        /* #ctl00\$MainContainer\$TimeSelector2_imgUp {
            position: absolute;
            margin-top: 16px;
            margin-left: -9px;
        }*/
        #ctl00_MainContainer_chkIsActive {
            height: 17px;
            width: 22px;
        }


        input#ctl00_MainContainer_chkIsActive {
            height: 30px;
            width: 30px;
        }
    </style>

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
        }

        .upload-container {
            background: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 30px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .btn-upload {
            background-color: #4CAF50;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-upload:hover {
                background-color: #45a049;
            }

        .preview-container {
            margin-top: 20px;
            text-align: center;
        }

        .preview-image {
            max-width: 100%;
            max-height: 300px;
            border: 1px solid #ddd;
            border-radius: 4px;
            margin-top: 10px;
        }

        .gridview-container {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .grid-image {
            width: 100px;
            height: 100px;
            object-fit: cover;
            border-radius: 4px;
        }

        h2 {
            color: #333;
            margin-bottom: 20px;
        }
    </style>

    <style type="text/css">
        /* Base styling for file input */
        .file-input {
            display: inline-block;
            padding: 10px 20px;
            font-size: 14px;
            font-family: Arial, sans-serif;
            color: #fff;
            background-color: #007bff; /* Bootstrap blue */
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            width: 100% !important;
        }

            /* Hover effect */
            .file-input:hover {
                background-color: #0056b3;
            }

            /* Remove ugly default styles */
            .file-input::-webkit-file-upload-button {
                visibility: hidden;
            }

            .file-input::before {
                content: 'Choose File';
                display: inline-block;
                background: #007bff;
                color: #fff;
                padding: 10px 20px;
                border-radius: 5px;
                cursor: pointer;
            }

            /* Hover effect for custom button */
            .file-input:hover::before {
                background: #0056b3;
            }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">


    <div class="PageTitle">
        <h4>System Image Configuration</h4>
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
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Batch</b>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="ucBatch_BatchSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label5" runat="server" Text="Type" Font-Bold="true" Font-Size="Large" Width="100%"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlActivityType" Style="width: 100%;" CssClass="form-control" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="Load_Click" CssClass="btn btn-info" Width="100%" Style="margin-top: 25px" />
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="card mt-5">
        <div class="card-body">
            <h2>Upload Image</h2>

            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <asp:FileUpload ID="FileUpload1" runat="server"
                        CssClass="file-input"
                        onchange="previewImage(this);"
                        accept="image/*" />

                    <br />
                    <br />
                    <div id="previewContainer" class="preview-container" style="display: none;">
                        <h3>Preview:</h3>
                        <asp:Image ID="imgPreview" runat="server" CssClass="preview-image" />
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <asp:Button ID="btnUpload" runat="server"
                        Text="Upload Image"
                        CssClass="btn-upload"
                        OnClick="btnUpload_Click" />
                </div>
            </div>

            <asp:Label ID="lblMessage" runat="server"
                ForeColor="Green"
                Font-Bold="true"></asp:Label>
        </div>
    </div>



    <asp:UpdatePanel runat="server" ID="UpdatePanel03">
        <ContentTemplate>

            <div class="card mt-5">
                <div class="card-body">
                    <h2>Uploaded Images</h2>
                    <asp:GridView ID="gvUploadedImage" runat="server" OnRowDataBound="gvUploadedImage_RowDataBound"
                        AutoGenerateColumns="False"
                        CellPadding="10"
                        GridLines="None"
                        Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Program">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProgram" Text='<%#Eval("ShortName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Batch">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBatch" Text='<%#Eval("BatchNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Logo">
                                <ItemTemplate>
                                    <asp:Image ID="lblLogo" runat="server" Width="70px" Height="70px" />
                                    <asp:Label runat="server" ID="lblImageBytes" Text='<%#Eval("ImageBytes") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="#4CAF50" ForeColor="White" Font-Bold="true" />
                        <RowStyle BackColor="#f9f9f9" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </div>
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


