<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="SemesterDashboard.aspx.cs" Inherits="EMS.Module.Dashboard.SemesterDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Dashboard</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.9.1/chart.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.21/js/jquery.dataTables.min.js"></script>
    <script src="SemesterDashboard.js"></script>

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
            background: #f5f7fa;
            color: #333;
        }

        .dashboard-container {
            max-width: 1400px;
            margin: 0 auto;
            padding: 20px;
        }

        .header {
            background: white;
            padding: 25px 30px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            margin-bottom: 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .header h1 {
                color: #333;
                font-size: 28px;
                font-weight: 600;
            }

        .filter-section {
            background: white;
            padding: 25px;
            border-radius: 10px;
            margin-bottom: 30px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .filter-row {
            display: flex;
            gap: 20px;
            align-items: flex-end;
            flex-wrap: wrap;
        }

        .filter-group {
            flex: 1;
            min-width: 250px;
        }

            .filter-group label {
                display: block;
                margin-bottom: 8px;
                font-weight: 600;
                color: #555;
                font-size: 14px;
            }

            .filter-group select {
                width: 100%;
                padding: 12px;
                border: 2px solid #e0e0e0;
                border-radius: 6px;
                font-size: 14px;
                transition: all 0.3s;
            }

                .filter-group select:focus {
                    outline: none;
                    border-color: #667eea;
                }

        .btn-filter {
            padding: 12px 30px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 600;
            transition: transform 0.2s;
        }

            .btn-filter:hover {
                transform: translateY(-2px);
                box-shadow: 0 4px 8px rgba(102, 126, 234, 0.3);
            }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-bottom: 30px;
        }

        .stat-card {
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            transition: transform 0.3s;
        }

            .stat-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 4px 12px rgba(0,0,0,0.15);
            }

        .stat-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
        }

        .stat-icon {
            width: 50px;
            height: 50px;
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            color: white;
        }

            .stat-icon.blue {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            }

            .stat-icon.green {
                background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            }

            .stat-icon.orange {
                background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
            }

            .stat-icon.purple {
                background: linear-gradient(135deg, #43e97b 0%, #38f9d7 100%);
            }

        .stat-title {
            font-size: 14px;
            color: #888;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .stat-value {
            font-size: 32px;
            font-weight: bold;
            color: #333;
        }

        .chart-section {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(500px, 1fr));
            gap: 20px;
            margin-bottom: 30px;
        }

        .chart-card {
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

            .chart-card h3 {
                margin-bottom: 20px;
                color: #333;
                font-size: 18px;
            }

        .chart-container {
            min-height: 300px;
            position: relative;
        }

        .table-section {
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 30px;
        }

            .table-section h3 {
                margin-bottom: 20px;
                color: #333;
                font-size: 18px;
            }

        .data-table {
            width: 100%;
            border-collapse: collapse;
        }

            .data-table thead {
                background: #667eea;
                color: white;
            }

            .data-table th {
                padding: 12px;
                text-align: left;
                font-weight: 600;
            }

            .data-table td {
                padding: 12px;
                border-bottom: 1px solid #f0f0f0;
            }

            .data-table tbody tr:hover {
                background: #f8f9fa;
            }

        .badge {
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
        }

        .badge-active {
            background: #d4edda;
            color: #155724;
        }

        .badge-inactive {
            background: #f8d7da;
            color: #721c24;
        }

        .badge-completed {
            background: #d1ecf1;
            color: #0c5460;
        }

        .loading {
            text-align: center;
            padding: 40px;
            color: #667eea;
        }

            .loading i {
                font-size: 48px;
                animation: spin 2s linear infinite;
            }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
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
                    <div class="header">
                        <h1><i class="fas fa-chart-line"></i>Academic Dashboard</h1>
                        <p>Student Performance & Enrollment Analytics</p>
                    </div>

                    <!-- Filter Section -->
                    <div class="filter-section">
                        <div class="filter-row">
                            <div class="filter-group" runat="server" visible="false">
                                <label><i class="fas fa-calendar-alt"></i>Calendar Unit Type</label>
                                <asp:DropDownList ID="ddlCalendarType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="filter-group">
                                <label><i class="fas fa-calendar"></i>Academic Calendar</label>
                                <asp:DropDownList ID="ddlAcademicCalendar" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicCalendar_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="filter-group">
                                <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn-filter" OnClientClick="applyFilter()" />
                            </div>
                        </div>
                    </div>

                    <!-- Stats Cards -->
                    <div class="stats-grid">

                        <div class="stat-card">
                            <div class="stat-header">
                                <div>
                                    <div class="stat-title">Institue</div>
                                    <div class="stat-value" id="totalInst">-</div>
                                </div>
                                <div class="stat-icon purple">
                                    <i class="fas fa-chart-line"></i>
                                </div>
                            </div>
                        </div>

                        <div class="stat-card">
                            <div class="stat-header">
                                <div>
                                    <div class="stat-title">Programs</div>
                                    <div class="stat-value" id="totalPrograms">-</div>
                                </div>
                                <div class="stat-icon orange">
                                    <i class="fas fa-graduation-cap"></i>
                                </div>
                            </div>
                        </div>

                        <div class="stat-card">
                            <div class="stat-header">
                                <div>
                                    <div class="stat-title">Total Students</div>
                                    <div class="stat-value" id="totalStudents">-</div>
                                </div>
                                <div class="stat-icon blue">
                                    <i class="fas fa-users"></i>
                                </div>
                            </div>
                        </div>
                        <div class="stat-card">
                            <div class="stat-header">
                                <div>
                                    <div class="stat-title">Active Courses</div>
                                    <div class="stat-value" id="totalCourses">-</div>
                                </div>
                                <div class="stat-icon green">
                                    <i class="fas fa-book"></i>
                                </div>
                            </div>
                        </div>

                    </div>

                    <!-- Charts Section -->
                    <div class="chart-section">
                        <div class="chart-card">
                            <h3><i class="fas fa-chart-bar"></i>Student Enrollment by Program</h3>
                            <div class="chart-container">
                                <canvas id="programChart"></canvas>
                            </div>
                        </div>
                        <div class="chart-card">
                            <h3><i class="fas fa-chart-pie"></i>GPA Distribution</h3>
                            <div class="chart-container">
                                <canvas id="cgpaChart"></canvas>
                            </div>
                        </div>
                    </div>

                    
                    <!-- Course Enrollment Table -->
                    <div class="table-section">
                        <h3><i class="fas fa-list"></i>Course Enrollment Details</h3>
                        <table id="tblCourseEnrollment" class="data-table display">
                            <thead>
                                <tr>
                                    <th>Course Code</th>
                                    <th>Course Title</th>
                                    <th>Program</th>
                                    <th>Enrolled Students</th>
                                    <th>Credits</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <%--<tbody id="courseTableBody">
                                <tr>
                                    <td colspan="6" class="loading">
                                        <i class="fas fa-spinner"></i>
                                        <br />
                                        Loading data...
                                    </td>
                                </tr>
                            </tbody>--%>
                        </table>
                    </div>

                    <!-- Student Data Table -->
                    <div class="table-section">
                        <h3><i class="fas fa-table"></i>Student Performance Data</h3>
                        <asp:GridView ID="gvStudents" runat="server" CssClass="data-table" AutoGenerateColumns="false"
                            AllowPaging="false" PageSize="10" OnPageIndexChanging="gvStudents_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Roll" HeaderText="Roll No" />
                                <asp:BoundField DataField="FullName" HeaderText="Student Name" />
                                <asp:BoundField DataField="ProgramName" HeaderText="Program" />
                                <asp:BoundField DataField="BatchName" HeaderText="Batch" />
                                <%--<asp:BoundField DataField="CGPA" HeaderText="CGPA" DataFormatString="{0:F2}" />--%>
                                <%--<asp:BoundField DataField="TotalCredit" HeaderText="Credits" />--%>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <span class='badge <%# Convert.ToBoolean(Eval("IsActive")) ? "badge-active" : "badge-inactive" %>'>
                                            <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pager" />
                        </asp:GridView>
                    </div>

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
