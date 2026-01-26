<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Admin_StudentCourseHistory" CodeBehind="StudentCourseHistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Student Course History</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {

        });

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

        function viewCourseDetails(ID) {
            event.preventDefault();
            var hiddenField = document.getElementById('<%= hdnUserId.ClientID %>').value;

            $.ajax({
                type: "POST",
                url: "StudentCourseHistory.aspx/GetMarksDetails",
                data: JSON.stringify({
                    studentCourseHistoryId: ID,
                    userId: hiddenField
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#courseDetailsModalBody').html(response.d);
                    $('#courseDetailsModal').modal('show');
                },
                failure: function (response) {
                    alert("Error: " + response.d);
                }
            });

        }

        function CloseModal() {
            $('#courseDetailsModal').modal('hide');
        }

    </script>

    <style type="text/css">
       
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <h4>Student Course History</h4>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <div>


        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Student ID</b>
                                <asp:TextBox runat="server" ID="txtStudentId" MaxLength="12" CssClass="form-control" placeholder="Student ID" />
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <br />
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" CssClass="btn btn-info form-control" />

                            </div>
                        </div>
                    </div>
                </div>


                <div class="card mt-3">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Student Name</b>
                                <asp:Label runat="server" ID="lblStudentName" CssClass="form-control" Text="..........................................." />

                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Program</b>
                                <asp:Label runat="server" ID="lblStudentProgram" CssClass="form-control" Text="..............................................." />

                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Batch</b>
                                <asp:Label runat="server" ID="lblStudentBatch" CssClass="form-control" Text="..............................................." />

                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <b>Major</b>
                                <asp:Label runat="server" ID="lblStudentMajor" CssClass="form-control" Text="..............................................." />

                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel03" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnUserId" runat="server" />
                <div class="card mt-4">
                    <div class="card-header" style="background-color: lightgray; font-weight: bold">Semester wise result summary </div>
                    <div class="card-body">
                        <asp:Panel runat="server" ID="panelResultSummary"></asp:Panel>
                    </div>
                </div>

                <div class="card" style="margin-top: 10px">
                    <div class="card-header" style="background-color: lightgray; font-weight: bold">Semester wise result breakdown </div>
                    <div class="card-body">
                        <asp:Panel runat="server" ID="pnStudentResultHistory"></asp:Panel>

                    </div>
                </div>


                <!-- Modal -->
                <div class="modal fade" id="courseDetailsModal" tabindex="-1" role="dialog" aria-labelledby="courseDetailsModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header bg-info text-white">
                                <h5 class="modal-title" id="courseDetailsModalLabel">Marks Breakdown</h5>
                                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close" onclick="CloseModal()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body" id="courseDetailsModalBody">
                                <!-- AJAX response will be injected here -->
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="CloseModal()">Close</button>
                            </div>
                        </div>
                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>


        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender01" TargetControlID="UpdatePanel03" runat="server">
            <Animations>
                <OnUpdating> <Parallel duration="0"> <ScriptAction Script="InProgress()();" /> <EnableAction AnimationTarget="btnLoad" Enabled="false" /> </Parallel> </OnUpdating>
                <OnUpdated> <Parallel duration="0"> <ScriptAction Script="onComplete();" /> <EnableAction AnimationTarget="btnLoad" Enabled="true" /> </Parallel> </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

