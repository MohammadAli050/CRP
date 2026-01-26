
var programChart, cgpaChart;

$(document).ready(function () {
    loadDashboardData();

    // Initialize DataTable
    $('#tblCourseEnrollment').DataTable({
        "pageLength": 10,
        "order": [[3, "desc"]]
    });
});

function applyFilter() {
    var calendarType = 0;// $('#<%= ddlCalendarType.ClientID %>').val();
    var academicCalendar = $('#ctl00_MainContainer_ddlAcademicCalendar').val();
    loadDashboardData(calendarType, academicCalendar);
}

function loadDashboardData(calendarType, academicCalendar) {
    // Show loading
    $('#totalStudents, #totalCourses, #totalPrograms, #totalInst').text('-');

    $.ajax({
        type: "POST",
        url: "SemesterDashboard.aspx/GetDashboardData",
        data: JSON.stringify({
            calendarTypeId: calendarType || '0',
            academicCalendarId: academicCalendar || '0'
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            updateDashboard(data);
        },
        error: function (error) {
            console.error("Error loading dashboard data:", error);
        }
    });
}

function updateDashboard(data) {
    // Update stats cards
    $('#totalStudents').text(data.TotalStudents);
    $('#totalCourses').text(data.TotalCourses);
    $('#totalPrograms').text(data.TotalPrograms);
    $('#totalInst').text(data.TotalInst);

    // Update Program Chart
    updateProgramChart(data.ProgramData);

    // Update CGPA Chart
    updateCGPAChart(data.CGPAData);

    // Update Course Table
    updateCourseTable(data.CourseData);
}

function updateProgramChart(programData) {
    var ctx = document.getElementById('programChart').getContext('2d');

    if (programChart) {
        programChart.destroy();
    }
    if (programData.Count > 0) {

        programChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: programData.map(p => p.ProgramName),
                datasets: [{
                    label: 'Number of Students',
                    data: programData.map(p => p.StudentCount),
                    backgroundColor: [
                        'rgba(102, 126, 234, 0.8)',
                        'rgba(118, 75, 162, 0.8)',
                        'rgba(240, 147, 251, 0.8)',
                        'rgba(79, 172, 254, 0.8)',
                        'rgba(67, 233, 123, 0.8)'
                    ],
                    borderColor: [
                        'rgba(102, 126, 234, 1)',
                        'rgba(118, 75, 162, 1)',
                        'rgba(240, 147, 251, 1)',
                        'rgba(79, 172, 254, 1)',
                        'rgba(67, 233, 123, 1)'
                    ],
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            precision: 0
                        }
                    }
                }
            }
        });
    }
}

function updateCGPAChart(cgpaData) {
    var ctx = document.getElementById('cgpaChart').getContext('2d');

    if (cgpaChart) {
        cgpaChart.destroy();
    }

    cgpaChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: cgpaData.map(c => c.Range),
            datasets: [{
                data: cgpaData.map(c => c.Count),
                backgroundColor: [
                    'rgba(102, 126, 234, 0.8)',
                    'rgba(118, 75, 162, 0.8)',
                    'rgba(240, 147, 251, 0.8)',
                    'rgba(79, 172, 254, 0.8)',
                    'rgba(67, 233, 123, 0.8)'
                ],
                borderColor: '#fff',
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'right'
                }
            }
        }
    });
}

function updateCourseTable(courseData) {
   
    /// if there is data, populate the table
    if (courseData.length > 0) {
        var table = $('#tblCourseEnrollment').DataTable();
        table.clear();
        courseData.forEach(function (course) {
            var status = course.IsActive ?
                '<span class="badge badge-active">Active</span>' :
                '<span class="badge badge-inactive">Inactive</span>';

            table.row.add([
                course.CourseCode,
                course.CourseTitle,
                course.ProgramName,
                course.EnrolledStudents,
                course.Credits,
                status
            ]);
        });

        table.draw();
    }
}
