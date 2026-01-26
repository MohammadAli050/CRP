<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="LogGeneralPage.aspx.cs" Inherits="EMS.miu.log.LogGeneralPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Log General
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
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
    <style>
        /* Specific tweaks for this page */
        .card-title { font-weight: bold; }
        .filter-label { font-weight: 600; margin-bottom: 2px; display: block; }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h4 class="m-0 text-dark"><i class="fas fa-history mr-2"></i>Log General</h4>
                </div>
            </div>
        </div>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(3px); background-color: rgba(255, 255, 255, 0.3); z-index: 9999;"></div>
    <div id="divProgress" style="display: none; z-index: 10000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/Images/Img/Waiting.gif" Height="120px" Width="120px" Style="border-radius: 50%; box-shadow: 0 0 15px rgba(0,0,0,0.2);" />
    </div>

    <section class="content">
        <div class="container-fluid">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="card card-outline card-info">
                        <div class="card-header">
                            <h3 class="card-title">Search & Filters</h3>
                            <div class="card-tools">
                                <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row border-bottom pb-3 mb-3">
                                <div class="col-md-3">
                                    <span class="filter-label">From Date</span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                        </div>
                                        <asp:TextBox runat="server" ID="DateFromTextBox" CssClass="form-control datepicker" placeholder="dd/mm/yyyy" />
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="reqSubmissionStart" runat="server" TargetControlID="DateFromTextBox" Format="dd/MM/yyyy" />
                                </div>
                                <div class="col-md-3">
                                    <span class="filter-label">To Date</span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                        </div>
                                        <asp:TextBox runat="server" ID="DateToTextBox" CssClass="form-control datepicker" placeholder="dd/mm/yyyy" />
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="DateToTextBox" Format="dd/MM/yyyy" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">&nbsp;</span>
                                    <asp:LinkButton runat="server" ID="btnLoad" OnClick="btnLoad_Click" CssClass="btn btn-info btn-block">
                                        <i class="fas fa-sync mr-1"></i> Load
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <span class="filter-label">User Login</span>
                                    <asp:TextBox runat="server" ID="txtFilterUser" CssClass="form-control" placeholder="User ID" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">Events</span>
                                    <asp:TextBox runat="server" ID="txtFilterEvent" CssClass="form-control" placeholder="Event Name" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">Message</span>
                                    <asp:TextBox runat="server" ID="txtFilterMsg" CssClass="form-control" placeholder="Keyword" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">Student Roll</span>
                                    <asp:TextBox runat="server" ID="txtFilterRoll" CssClass="form-control" placeholder="Roll No" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">Course Code</span>
                                    <asp:TextBox runat="server" ID="txtFilteCourse" CssClass="form-control" placeholder="Course Code" />
                                </div>
                                <div class="col-md-2">
                                    <span class="filter-label">&nbsp;</span>
                                    <asp:LinkButton runat="server" ID="btnFilter" OnClick="btnFilter_Click" CssClass="btn btn-secondary btn-block">
                                        <i class="fas fa-filter mr-1"></i> Filter
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="card mt-3">
                <div class="card-body p-0">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="gvLogGeneral" runat="server" AutoGenerateColumns="False" Width="100%" 
                                    CssClass="table table-hover table-head-fixed text-nowrap mb-0"
                                    AllowPaging="true" PageSize="50" OnPageIndexChanging="gvLogGeneral_PageIndexChanging" 
                                    OnSelectedIndexChanged="gvLogGeneral_SelectedIndexChanged" GridLines="None" ShowHeaderWhenEmpty="true">
                                    
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="First" LastPageText="Last" Position="Bottom" />
                                    <PagerStyle CssClass="pagination-container p-2 bg-light border-top text-right" />
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Date/Time">
                                            <ItemTemplate>
                                                <i class="far fa-clock text-muted mr-1 small"></i>
                                                <asp:Label runat="server" ID="lblDate" Text='<%#Eval("DateTime") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="User Login">
                                            <ItemTemplate>
                                                <span class="badge badge-secondary p-1 px-2"><%#Eval("UserLoginId") %></span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Event">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEvent" Text='<%#Eval("EventName") %>' CssClass="text-primary font-weight-bold" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Message">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblMsg" Text='<%#Eval("Message") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="300px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Student Roll">
                                            <ItemTemplate><%#Eval("StudentRoll") %></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Course Code">
                                            <ItemTemplate><%#Eval("CourseFormalCode") %></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                    <EmptyDataTemplate>
                                        <div class="text-center p-4">
                                            <i class="fas fa-folder-open fa-3x text-muted mb-2"></i>
                                            <p class="mb-0">No Log Records Found</p>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </section>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel4" runat="server">
        <Animations>
            <OnUpdating><Parallel duration="0"><ScriptAction Script="InProgress();" /><EnableAction AnimationTarget="btnLoad" Enabled="false" /></Parallel></OnUpdating>
            <OnUpdated><Parallel duration="0"><ScriptAction Script="onComplete();" /><EnableAction AnimationTarget="btnLoad" Enabled="true" /></Parallel></OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>