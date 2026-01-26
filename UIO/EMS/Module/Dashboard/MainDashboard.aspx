<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="MainDashboard.aspx.cs" Inherits="EMS.Module.Dashboard.MainDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Dashboard</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />

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

    <script type="text/javascript">
        function scrollToSection(sectionId) {
            var section = document.getElementById(sectionId);
            if (section) {
                // Smooth scroll to section
                section.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });

                // Add highlight effect
                section.classList.add('highlight-section');
                setTimeout(function () {
                    section.classList.remove('highlight-section');
                }, 2000);
            }
        }
    </script>

    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
        }

        .dashboard-container {
            max-width: 1400px;
            margin: 0 auto;
        }

        .dashboard-header {
            background: white;
            padding: 25px 30px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            margin-bottom: 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .dashboard-header h1 {
                color: #333;
                font-size: 28px;
                font-weight: 600;
            }

        .refresh-btn {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            padding: 12px 25px;
            border-radius: 8px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 600;
            transition: all 0.3s;
            display: flex;
            align-items: center;
            gap: 8px;
        }

            .refresh-btn:hover {
                transform: translateY(-2px);
                box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
            }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 25px;
            margin-bottom: 30px;
        }

        .stat-card {
            background: white;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            transition: all 0.3s;
            position: relative;
            overflow: hidden;
        }

            .stat-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 4px;
                background: linear-gradient(90deg, #667eea 0%, #764ba2 100%);
            }

            .stat-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 15px 40px rgba(0, 0, 0, 0.15);
            }

        .stat-icon {
            width: 60px;
            height: 60px;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 28px;
            margin-bottom: 15px;
        }

            .stat-icon.purple {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
            }

            .stat-icon.blue {
                background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
                color: white;
            }

            .stat-icon.green {
                background: linear-gradient(135deg, #43e97b 0%, #38f9d7 100%);
                color: white;
            }

        .stat-label {
            color: #666;
            font-size: 14px;
            margin-bottom: 8px;
            font-weight: 500;
        }

        .stat-value {
            color: #333;
            font-size: 32px;
            font-weight: 700;
        }

        .data-section {
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            margin-bottom: 30px;
        }

        .section-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #f0f0f0;
        }

        .section-title {
            font-size: 22px;
            font-weight: 600;
            color: #333;
        }

        .filter-group {
            display: flex;
            gap: 15px;
            align-items: center;
        }

            .filter-group select, .filter-group input {
                padding: 10px 15px;
                border: 2px solid #e0e0e0;
                border-radius: 8px;
                font-size: 14px;
                transition: all 0.3s;
            }

                .filter-group select:focus, .filter-group input:focus {
                    outline: none;
                    border-color: #667eea;
                }

        .data-table {
            width: 100%;
            border-collapse: collapse;
            overflow: hidden;
        }

            .data-table thead {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            }

            .data-table th {
                padding: 15px;
                text-align: left;
                /*color: white;*/
                font-weight: 600;
                font-size: 14px;
            }

            .data-table td {
                padding: 15px;
                border-bottom: 1px solid #f0f0f0;
                color: #555;
                font-size: 14px;
            }

            .data-table tbody tr {
                transition: all 0.2s;
            }

                .data-table tbody tr:hover {
                    background: #f8f9ff;
                }

        .status-badge {
            padding: 6px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            display: inline-block;
        }

        .status-active {
            background: #d4edda;
            color: #155724;
        }

        .status-inactive {
            background: #f8d7da;
            color: #721c24;
        }

        .chart-container {
            height: 300px;
            margin-top: 20px;
        }

        .loader {
            display: none;
            text-align: center;
            padding: 40px;
        }

            .loader.active {
                display: block;
            }

        .spinner {
            border: 4px solid #f3f3f3;
            border-top: 4px solid #667eea;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1s linear infinite;
            margin: 0 auto;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        @media (max-width: 768px) {
            .dashboard-header {
                flex-direction: column;
                gap: 15px;
                text-align: center;
            }

            .stats-grid {
                grid-template-columns: 1fr;
            }

            .filter-group {
                flex-direction: column;
                width: 100%;
            }

                .filter-group select, .filter-group input {
                    width: 100%;
                }

            .data-table {
                font-size: 12px;
            }

                .data-table th, .data-table td {
                    padding: 10px 8px;
                }
        }

        .clickable-card {
            cursor: pointer;
            user-select: none;
        }

            .clickable-card:hover {
                transform: translateY(-8px);
                box-shadow: 0 20px 50px rgba(102, 126, 234, 0.3);
            }

            .clickable-card:active {
                transform: translateY(-3px);
                box-shadow: 0 10px 30px rgba(102, 126, 234, 0.2);
            }
    </style>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="card">
                <div class="card-body">

                    <!-- Header -->
                    <div class="dashboard-header">
                        <h1><i class="fas fa-graduation-cap"></i>Academic Dashboard</h1>
                        <asp:LinkButton ID="btnRefresh" runat="server" CssClass="refresh-btn"
                            Text="Refresh Data" OnClick="btnRefresh_Click">
                    <i class="fas fa-sync-alt"></i>
                        </asp:LinkButton>
                    </div>

                    <!-- Statistics Cards -->
                    <div class="stats-grid">
                        <div class="stat-card clickable-card" onclick="scrollToSection('institutionsSection')">
                            <div class="stat-icon purple">
                                <i class="fas fa-university"></i>
                            </div>
                            <div class="stat-label">Total Institutions</div>
                            <div class="stat-value">
                                <asp:Label ID="lblTotalInstitutions" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="stat-card clickable-card" onclick="scrollToSection('programsSection')">
                            <div class="stat-icon blue">
                                <i class="fas fa-book"></i>
                            </div>
                            <div class="stat-label">Total Programs</div>
                            <div class="stat-value">
                                <asp:Label ID="lblTotalPrograms" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="stat-card clickable-card" onclick="scrollToSection('studentsSection')">
                            <div class="stat-icon green">
                                <i class="fas fa-user-graduate"></i>
                            </div>
                            <div class="stat-label">Total Students</div>
                            <div class="stat-value">
                                <asp:Label ID="lblTotalStudents" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="stat-card clickable-card" onclick="scrollToSection('studentsSection')">
                            <div class="stat-icon purple">
                                <i class="fas fa-user-check"></i>
                            </div>
                            <div class="stat-label">Active Students</div>
                            <div class="stat-value">
                                <asp:Label ID="lblActiveStudents" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Institutions Section -->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="data-section" id="institutionsSection">
                                <div class="section-header">
                                    <h2 class="section-title"><i class="fas fa-university"></i>Institutions</h2>
                                    <div class="filter-group">
                                        <asp:TextBox ID="txtSearchInstitution" runat="server"
                                            placeholder="Search institutions..."
                                            AutoPostBack="true"
                                            OnTextChanged="txtSearchInstitution_TextChanged">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="loader" id="loaderInstitutions">
                                    <div class="spinner"></div>
                                </div>
                                <asp:GridView ID="gvInstitutions" runat="server"
                                    CssClass="data-table"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    PageSize="10"
                                    OnPageIndexChanging="gvInstitutions_PageIndexChanging">
                                    <Columns>
                                        <%--add a serial number column here to show serial number of each row--%>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <b><%# Container.DataItemIndex + 1 %></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InstituteName" HeaderText="Institution Name" />
                                        <asp:BoundField DataField="InstituteCode" HeaderText="Code" />
                                        <asp:BoundField DataField="InstituteAddress" HeaderText="Address" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <span class='<%# Convert.ToInt32(Eval("ActiveStatus")) == 1 ? "status-badge status-active" : "status-badge status-inactive" %>'>
                                                    <%# Convert.ToInt32(Eval("ActiveStatus")) == 1 ? "Active" : "Inactive" %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <!-- Programs Section -->
                            <div class="data-section" id="programsSection">
                                <div class="section-header">
                                    <h2 class="section-title"><i class="fas fa-book"></i>Programs</h2>
                                    <div class="filter-group">
                                        <asp:DropDownList ID="ddlFilterInstitute" runat="server"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlFilterInstitute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSearchProgram" runat="server"
                                            placeholder="Search programs..."
                                            AutoPostBack="true"
                                            OnTextChanged="txtSearchProgram_TextChanged">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <asp:GridView ID="gvPrograms" runat="server"
                                    CssClass="data-table"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    PageSize="10"
                                    OnPageIndexChanging="gvPrograms_PageIndexChanging">
                                    <Columns>
                                        <%--add a serial number column here to show serial number of each row--%>

                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <b><%# Container.DataItemIndex + 1 %></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Code" HeaderText="Code" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Program Name" />
                                        <asp:BoundField DataField="DegreeName" HeaderText="Degree" />
                                        <asp:BoundField DataField="InstituteName" HeaderText="Institute Name" />
                                        <asp:BoundField DataField="ProgramType" HeaderText="Program Type" />

                                        
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <!-- Students Section -->
                            <div class="data-section" id="studentsSection">
                                <div class="section-header">
                                    <h2 class="section-title"><i class="fas fa-user-graduate"></i>Students</h2>
                                    <div class="filter-group">
                                        <asp:DropDownList ID="ddlFilterProgram" runat="server"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlFilterProgram_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSearchStudent" runat="server"
                                            placeholder="Search students..."
                                            AutoPostBack="true"
                                            OnTextChanged="txtSearchStudent_TextChanged">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <asp:GridView ID="gvStudents" runat="server"
                                    CssClass="data-table"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    PageSize="10"
                                    OnPageIndexChanging="gvStudents_PageIndexChanging">
                                    <Columns>

                                        <%--add a serial number column here to show serial number of each row--%>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <b><%# Container.DataItemIndex + 1 %></b>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="ProgramName" HeaderText="Program" />
                                        <asp:BoundField DataField="BatchId" HeaderText="Batch" />
                                        <asp:BoundField DataField="Roll" HeaderText="Roll No" />
                                        <asp:BoundField DataField="FullName" HeaderText="Name" />

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-badge status-active" : "status-badge status-inactive" %>'>
                                                    <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Completed">
                                            <ItemTemplate>
                                                <%# Convert.ToBoolean(Eval("IsCompleted")) ? "Yes" : "No" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel2" runat="server">
        <Animations>
                <OnUpdating>
                    <Parallel duration="0">
                        <ScriptAction Script="InProgress();" />
                        <EnableAction AnimationTarget="btnSearch" Enabled="false" />                   
                    </Parallel>
                </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <ScriptAction Script="onComplete();" />
                            <EnableAction   AnimationTarget="btnSearch" Enabled="true" />
                        </Parallel>
                </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>


</asp:Content>
