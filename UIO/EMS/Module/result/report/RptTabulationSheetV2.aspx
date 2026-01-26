<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    CodeBehind="RptTabulationSheetV2.aspx.cs" Inherits="EMS.miu.result.report.RptTabulationSheetV2" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>
<%@ Register Src="~/UserControls/SessionUserControl.ascx" TagPrefix="uc1" TagName="SessionUserControl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Tabulation Sheet Report 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

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


        function initdropdown() {
            $('#ctl00_MainContainer_ddlInstitute').select2({
                allowClear: true
            });

        }

    </script>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/exceljs/4.3.0/exceljs.min.js"></script>

    <script type="text/javascript">
        function addGradingTableHeader(ws) {
            const bdr = { top: {style:'thin'}, left: {style:'thin'}, bottom: {style:'thin'}, right: {style:'thin'} };
    
            // Row 1: University Name (center, columns 10-40)
            ws.addRow([]);
            ws.getCell(1, 10).value = 'Bangladesh Institute of Governance and Management';
            ws.getCell(1, 10).font = { bold: true, size: 16, name: 'Calibri' };
            ws.getCell(1, 10).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(1, 10, 1, 40);
            ws.getRow(1).height = 30;

            // Row 2: Tabulation Sheet Title (center, columns 10-40)
            ws.addRow([]);
            ws.getCell(2, 10).value = 'TABULATION SHEET';
            ws.getCell(2, 10).font = { bold: true, size: 14, name: 'Calibri' };
            ws.getCell(2, 10).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(2, 10, 2, 40);
            ws.getRow(2).height = 25;

            // Row 3: Program/Session info (center, columns 10-40)
            //ws.addRow([]);
            //ws.getCell(3, 10).value = 'Program: B.Sc. in Computer Science & Engineering | Session: Spring 2025';
            //ws.getCell(3, 10).font = { size: 11, name: 'Calibri' };
            //ws.getCell(3, 10).alignment = { horizontal: 'center', vertical: 'middle' };
            //ws.mergeCells(3, 10, 3, 40);
            //ws.getRow(3).height = 20;

            // Row 4: Empty spacing
            ws.addRow([]);
            ws.getRow(4).height = 10;

            // Row 5: GRADING SYSTEM header (left side, columns 1-8)
            ws.addRow([]);
            ws.getCell(5, 1).value = 'GRADING SYSTEM';
            ws.getCell(5, 1).font = { bold: true, size: 12, name: 'Calibri' };
            ws.getCell(5, 1).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.getCell(5, 1).fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FFD3D3D3' } };
            ws.mergeCells(5, 1, 5, 8);
            ws.getRow(5).height = 25;

            // Row 6: Grading table headers (columns 1-8)
            ws.addRow(['Marks', '', 'Letter Grade', '', 'Grade Point', '', '', '']);
            ws.mergeCells(6, 1, 6, 2);  // Marks
            ws.mergeCells(6, 3, 6, 4);  // Letter Grade
            ws.mergeCells(6, 5, 6, 8);  // Grade Point
            ws.getRow(6).height = 20;
            ws.getRow(6).eachCell((cell, colNum) => {
                if (colNum <= 8) {
                    cell.font = { bold: true, size: 10, name: 'Calibri' };
                    cell.alignment = { horizontal: 'center', vertical: 'middle' };
                    cell.border = bdr;
                    cell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FFE7E6E6' } };
                }
            });

            // Rows 7-16: Grading data (10 rows)
            const gradingData = [
                ['≥ 80%', 'A+', '4.00'],
                ['75% to <80%', 'A', '3.75'],
                ['70% to <75%', 'A-', '3.50'],
                ['65% to <70%', 'B+', '3.25'],
                ['60% to <65%', 'B', '3.00'],
                ['55% to <60%', 'B-', '2.75'],
                ['50% to <55%', 'C+', '2.50'],
                ['45% to <50%', 'C', '2.25'],
                ['40% to <45%', 'D', '2.00'],
                ['<40%', 'F', '0.00']
            ];

            gradingData.forEach((row, index) => {
                const rowNum = 7 + index;
                ws.addRow([row[0], '', row[1], '', row[2], '', '', '']);
                ws.mergeCells(rowNum, 1, rowNum, 2);  // Marks column
                ws.mergeCells(rowNum, 3, rowNum, 4);  // Letter Grade column
                ws.mergeCells(rowNum, 5, rowNum, 8);  // Grade Point column
        
                ws.getRow(rowNum).height = 18;
                ws.getRow(rowNum).eachCell((cell, colNum) => {
                    if (colNum <= 8) {
                        cell.font = { size: 10, name: 'Calibri' };
                        cell.alignment = { horizontal: 'center', vertical: 'middle' };
                        cell.border = bdr;
                    }
                });
            });

            // Row 17: I (Incomplete) - columns 1-8
            ws.addRow(['-', '', 'I', '', 'Incomplete', '', '', '']);
            ws.mergeCells(17, 1, 17, 2);
            ws.mergeCells(17, 3, 17, 4);
            ws.mergeCells(17, 5, 17, 8);
            ws.getRow(17).height = 18;
            ws.getRow(17).eachCell((cell, colNum) => {
                if (colNum <= 8) {
                    cell.font = { size: 10, name: 'Calibri' };
                    cell.alignment = { horizontal: 'center', vertical: 'middle' };
                    cell.border = bdr;
                }
            });

            // Row 18: W (Withdrawn) - columns 1-8
            ws.addRow(['-', '', 'W', '', 'Withdrawn', '', '', '']);
            ws.mergeCells(18, 1, 18, 2);
            ws.mergeCells(18, 3, 18, 4);
            ws.mergeCells(18, 5, 18, 8);
            ws.getRow(18).height = 18;
            ws.getRow(18).eachCell((cell, colNum) => {
                if (colNum <= 8) {
                    cell.font = { size: 10, name: 'Calibri' };
                    cell.alignment = { horizontal: 'center', vertical: 'middle' };
                    cell.border = bdr;
                }
            });

            // Row 19: Empty spacing
            ws.addRow([]);
            ws.getRow(19).height = 15;

            return 19; // Return the last row number of header
        }

        // Function to add signature footer with left/right layout
        function addSignatureFooter(ws, startRow) {
            const bdr = { top: {style:'thin'}, left: {style:'thin'}, bottom: {style:'thin'}, right: {style:'thin'} };
    
            // Add spacing before footer
            ws.addRow([]);
            ws.getRow(startRow).height = 30;

            // Row 1: Result Published line (centered across all columns)
            const publishRow = startRow + 1;
            ws.addRow([]);
            ws.getCell(publishRow, 1).value = 'Result Published on 30 Oct 2019';
            ws.getCell(publishRow, 1).font = { bold: true, size: 11, name: 'Calibri' };
            ws.getCell(publishRow, 1).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(publishRow, 1, publishRow, 50);
            ws.getRow(publishRow).height = 25;

            // Add spacing
            ws.addRow([]);
            ws.getRow(publishRow + 1).height = 20;

            // LEFT SIDE - Tabulators (detailed)
            const leftStartRow = publishRow + 2;
    
            // Tabulators header
            ws.getCell(leftStartRow, 1).value = 'Tabulators';
            ws.getCell(leftStartRow, 1).font = { bold: true, size: 11, name: 'Calibri', underline: true };
            ws.getCell(leftStartRow, 1).alignment = { horizontal: 'left', vertical: 'middle' };
            ws.mergeCells(leftStartRow, 1, leftStartRow, 15);

            // Signature column header
            ws.getCell(leftStartRow, 16).value = 'Signature';
            ws.getCell(leftStartRow, 16).font = { bold: true, size: 11, name: 'Calibri', underline: true };
            ws.getCell(leftStartRow, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(leftStartRow, 16, leftStartRow, 20);

            // Tabulator 1
            ws.addRow([]);
            const tab1Row = leftStartRow + 1;
            ws.getCell(tab1Row, 1).value = 'Tabulator-I: ';
            ws.getCell(tab1Row, 1).font = { size: 10, name: 'Calibri' };
            ws.getCell(tab1Row, 1).alignment = { horizontal: 'left', vertical: 'top', wrapText: true };
            ws.mergeCells(tab1Row, 1, tab1Row, 15);
    
            ws.getCell(tab1Row, 16).value = '____________________';
            ws.getCell(tab1Row, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(tab1Row, 16, tab1Row, 20);
            ws.getRow(tab1Row).height = 35;

            // Tabulator 2
            ws.addRow([]);
            const tab2Row = leftStartRow + 2;
            ws.getCell(tab2Row, 1).value = 'Tabulator-II: ';
            ws.getCell(tab2Row, 1).font = { size: 10, name: 'Calibri' };
            ws.getCell(tab2Row, 1).alignment = { horizontal: 'left', vertical: 'top', wrapText: true };
            ws.mergeCells(tab2Row, 1, tab2Row, 15);
    
            ws.getCell(tab2Row, 16).value = '____________________';
            ws.getCell(tab2Row, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(tab2Row, 16, tab2Row, 20);
            ws.getRow(tab2Row).height = 35;

            // Add spacing between left and right sections
            ws.addRow([]);

            // RIGHT SIDE - Simple signatures (Controller & Vice Chancellor)
            const rightStartRow = leftStartRow;
    
            // Controller of Examinations (top right)
            ws.getCell(rightStartRow, 30).value = 'Controller of Examinations';
            ws.getCell(rightStartRow, 30).font = { bold: true, size: 11, name: 'Calibri' };
            ws.getCell(rightStartRow, 30).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(rightStartRow, 30, rightStartRow, 40);

            // Date line for Controller
            const controllerDateRow = rightStartRow + 1;
            ws.getCell(controllerDateRow, 30).value = 'Date: ____________________';
            ws.getCell(controllerDateRow, 30).font = { size: 10, name: 'Calibri' };
            ws.getCell(controllerDateRow, 30).alignment = { horizontal: 'left', vertical: 'middle' };
            ws.mergeCells(controllerDateRow, 30, controllerDateRow, 40);

            // Vice Chancellor (bottom right)
            const vcRow = rightStartRow + 2;
            ws.getCell(vcRow, 30).value = 'Vice Chancellor';
            ws.getCell(vcRow, 30).font = { bold: true, size: 11, name: 'Calibri' };
            ws.getCell(vcRow, 30).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(vcRow, 30, vcRow, 40);

            // Date line for VC
            const vcDateRow = vcRow + 1;
            ws.getCell(vcDateRow, 30).value = 'Date: ____________________';
            ws.getCell(vcDateRow, 30).font = { size: 10, name: 'Calibri' };
            ws.getCell(vcDateRow, 30).alignment = { horizontal: 'left', vertical: 'middle' };
            ws.mergeCells(vcDateRow, 30, vcDateRow, 40);

            // Examination Committee section (below left section)
            ws.addRow([]);
            const examCommRow = tab2Row + 2;
    
            ws.getCell(examCommRow, 1).value = 'Examination Committee';
            ws.getCell(examCommRow, 1).font = { bold: true, size: 11, name: 'Calibri', underline: true };
            ws.getCell(examCommRow, 1).alignment = { horizontal: 'left', vertical: 'middle' };
            ws.mergeCells(examCommRow, 1, examCommRow, 15);

            ws.getCell(examCommRow, 16).value = 'Signature';
            ws.getCell(examCommRow, 16).font = { bold: true, size: 11, name: 'Calibri', underline: true };
            ws.getCell(examCommRow, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(examCommRow, 16, examCommRow, 20);

            // Chairman
            ws.addRow([]);
            const chairRow = examCommRow + 1;
            ws.getCell(chairRow, 1).value = 'Chairman:';
            ws.getCell(chairRow, 1).font = { size: 10, name: 'Calibri' };
            ws.getCell(chairRow, 1).alignment = { horizontal: 'left', vertical: 'top', wrapText: true };
            ws.mergeCells(chairRow, 1, chairRow, 15);
    
            ws.getCell(chairRow, 16).value = '____________________';
            ws.getCell(chairRow, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(chairRow, 16, chairRow, 20);
            ws.getRow(chairRow).height = 45;

            // Members
            ws.addRow([]);
            const memRow = chairRow + 1;
            ws.getCell(memRow, 1).value = 'Members:';
            ws.getCell(memRow, 1).font = { size: 10, name: 'Calibri' };
            ws.getCell(memRow, 1).alignment = { horizontal: 'left', vertical: 'top', wrapText: true };
            ws.mergeCells(memRow, 1, memRow, 15);
    
            ws.getCell(memRow, 16).value = '____________________';
            ws.getCell(memRow, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(memRow, 16, memRow, 20);
            ws.getRow(memRow).height = 45;

            // Member 2
            ws.addRow([]);
            const mem2Row = memRow + 1;
            ws.getCell(mem2Row, 1).value = '';
            ws.getCell(mem2Row, 1).font = { size: 10, name: 'Calibri' };
            ws.getCell(mem2Row, 1).alignment = { horizontal: 'left', vertical: 'top', wrapText: true };
            ws.mergeCells(mem2Row, 1, mem2Row, 15);
    
            ws.getCell(mem2Row, 16).value = '____________________';
            ws.getCell(mem2Row, 16).alignment = { horizontal: 'center', vertical: 'middle' };
            ws.mergeCells(mem2Row, 16, mem2Row, 20);
            ws.getRow(mem2Row).height = 35;

            // Final info row
            ws.addRow([]);
            ws.addRow([]);
            const finalRow = mem2Row + 3;
            ws.getCell(finalRow, 1).value = 'Generated on: ' + new Date().toLocaleString();
            ws.getCell(finalRow, 1).font = { italic: true, size: 9 };
            ws.getCell(finalRow, 1).alignment = { horizontal: 'center' };
            ws.mergeCells(finalRow, 1, finalRow, 50);
        }

        async function exportToExcel(courseData, rsltSummary) {
            try {
                console.log('=== Debug Info ===');
                let dataArray = courseData;

                // Handle string input
                if (typeof courseData === 'string') {
                    try {
                        dataArray = JSON.parse(courseData);
                    } catch (e) {
                        console.error('Failed to parse courseData:', e);
                        alert('Invalid data format received');
                        return false;
                    }
                }

                // Handle object input
                if (dataArray && typeof dataArray === 'object' && !Array.isArray(dataArray)) {
                    const possibleKeys = ['data', 'Data', 'resultData', 'ResultData', 'results', 'Results', 'items', 'Items'];
                    let found = false;
    
                    for (let key of possibleKeys) {
                        if (dataArray[key] && Array.isArray(dataArray[key])) {
                            dataArray = dataArray[key];
                            found = true;
                            break;
                        }
                    }
    
                    if (!found) {
                        const keys = Object.keys(dataArray);
                        if (keys.every(k => !isNaN(k))) {
                            dataArray = Object.values(dataArray);
                        } else {
                            alert('Invalid data format: No array found');
                            return false;
                        }
                    }
                }

                if (!Array.isArray(dataArray) || dataArray.length === 0) {
                    alert('No data available to export');
                    return false;
                }

                console.log('Processing', dataArray.length, 'records');

                // Process rsltSummary data
                let summaryArray = rsltSummary;
                if (typeof rsltSummary === 'string') {
                    try {
                        summaryArray = JSON.parse(rsltSummary);
                    } catch (e) {
                        console.error('Failed to parse rsltSummary:', e);
                        summaryArray = [];
                    }
                }

                // Group summary data by StudentID
                const studentSummaryMap = {};
                if (Array.isArray(summaryArray)) {
                    summaryArray.forEach(item => {
                        const studentId = item.StudentID;
                        if (!studentSummaryMap[studentId]) {
                            studentSummaryMap[studentId] = [];
                        }
                        studentSummaryMap[studentId].push({
                            semesterId: item.StdAcademicCalenderID,
                            enrolled: item.Credit || 0,
                            earned: item.TranscriptCredit || 0,
                            gradePoint: item.TotalGradePoint || 0,
                            gpa: item.GPA || 0,
                            cgpa: item.CGPA || 0
                        });
                    });

                    // Sort each student's semesters by semesterId
                    Object.keys(studentSummaryMap).forEach(studentId => {
                        studentSummaryMap[studentId].sort((a, b) => a.semesterId - b.semesterId);
                    });
                }

                // Find maximum number of semesters across all students
                const maxSemesters = Math.max(
                    ...Object.values(studentSummaryMap).map(sems => sems.length),
                    1
                );
                console.log('Maximum semesters:', maxSemesters);

                // Extract unique exam types with their properties
                const examTypesMap = {};
                dataArray.forEach(item => {
                    const examName = item.ExamTemplateBasicItemName || '';
                    const examKey = examName.toLowerCase();
                    if (examName && !examTypesMap[examKey]) {
                        examTypesMap[examKey] = {
                            name: examName,
                            fullMark: parseFloat(item.ExamTemplateBasicItemMark) || 0,
                            convertedTo: parseFloat(item.ConvertedToMarks) || 0,
                            sequence: parseInt(item.ColumnSequence) || 0
                        };
                    }
                });

                // Sort exam types by sequence
                const examTypes = Object.values(examTypesMap).sort((a, b) => a.sequence - b.sequence);
                console.log('Exam Types:', examTypes);

                // Calculate columns needed
                const examColumnsCount = examTypes.reduce((total, exam) => {
                    return total + (exam.name.toLowerCase() === 'final' ? 2 : 1);
                }, 0);

                // Group data by student
                const studentMap = {};
                dataArray.forEach(item => {
                    const studentId = item.StudentID;
                    if (!studentMap[studentId]) {
                        studentMap[studentId] = {
                            info: {
                                Roll: item.Roll || '',
                                RegistrationNo: item.RegistrationNo || '',
                                StudentName: item.FullName || '',
                                Sex: item.Gender === 'Male' ? 'M' : (item.Gender === 'Female' ? 'F' : ''),
                                Batch: item.BatchNo || '',
                                RegistrationSession: item.RegistrationSession || '',
                                Enroll: item.Enroll || 0,
                                Earned: item.Earned || 0,
                                TranscriptGPA: item.TranscriptGPA || 0,
                                SGPA: item.TranscriptCGPA || 0,
                                CGPA: item.TranscriptCGPA || 0,
                                TotalGradePoint: 0,
                                ExamRoll: item.Roll || ''
                            },
                            courses: {}
                        };
                    }
    
                    const courseCode = item.FormalCode || item.VersionCode;
                    if (!courseCode) return;
    
                    if (!studentMap[studentId].courses[courseCode]) {
                        studentMap[studentId].courses[courseCode] = {
                            code: courseCode,
                            title: item.Title || '',
                            credits: item.Credits || 0,
                            examMarks: {},
                            total: 0,
                            grade: item.Grade || '',
                            gradePoint: item.GradePoint || 0
                        };
                    }
    
                    const examType = item.ExamTemplateBasicItemName || '';
                    const examKey = examType.toLowerCase();
                    const convertedMark = parseFloat(item.ConvertedMark) || 0;
                    const inputtedMark = parseFloat(item.InputtedMarks) || 0;
    
                    if (examType) {
                        studentMap[studentId].courses[courseCode].examMarks[examKey] = {
                            inputted: inputtedMark,
                            converted: convertedMark
                        };
                    }
                });

                // Calculate totals
                Object.values(studentMap).forEach(student => {
                    let totalGP = 0;
                    Object.values(student.courses).forEach(course => {
                        course.total = Object.values(course.examMarks).reduce((sum, marks) => sum + marks.converted, 0);
                        totalGP += (course.gradePoint * course.credits);
                    });
                    student.info.TotalGradePoint = totalGP;
                });

                // Get unique courses
                const courseCodesSet = new Set();
                dataArray.forEach(item => {
                    const code = item.FormalCode || item.VersionCode;
                    if (code) courseCodesSet.add(code);
                });
                const courseCodes = Array.from(courseCodesSet);

                console.log('Courses:', courseCodes.length, 'Students:', Object.keys(studentMap).length);

                // Create workbook
                const wb = new ExcelJS.Workbook();
                const ws = wb.addWorksheet('Tabulation Sheet');

                // ========== ADD HEADER ==========
                const headerEndRow = addGradingTableHeader(ws);
                const headerOffset = headerEndRow;
        
                // ========== MAIN TABLE ==========
                const bdr = { top: {style:'thin'}, left: {style:'thin'}, bottom: {style:'thin'}, right: {style:'thin'} };
                const hStyle = {
                    font: {bold: true, size: 11, name: 'Calibri'},
                    alignment: {horizontal: 'center', vertical: 'middle', wrapText: true},
                    fill: {type: 'pattern', pattern: 'solid', fgColor: {argb: 'FFD3D3D3'}},
                    border: bdr
                };
                const vStyle = {...hStyle, alignment: {horizontal: 'center', vertical: 'middle', textRotation: 90, wrapText: true}};
        const dStyle = {font: {size: 10, name: 'Calibri'}, alignment: {horizontal: 'center', vertical: 'middle'}, border: bdr};
        const dLeft = {...dStyle, alignment: {horizontal: 'left', vertical: 'middle'}};

        const MAIN_TABLE_START_ROW = headerOffset + 1;
        
        // Column positions
        const fixedCols = 8;
        const subjectCol = 9;
        const enrolledNumCol = subjectCol + courseCodes.length;
        const courseStartCol = enrolledNumCol + 1;
        const creditCol = courseStartCol + (courseCodes.length * (examColumnsCount + 3));
        const gpCol = creditCol + 2;
        const sgpaCol = gpCol + 1;
        const retakeCol = sgpaCol + 1;
        const examRollCol = retakeCol + courseCodes.length;

        // Right side columns
        const colsPerSemester = 4;
        const summaryCol = examRollCol + 1;
        const semesterCols = [];
        let currentCol = summaryCol;
        
        for (let i = 0; i < maxSemesters; i++) {
            semesterCols.push({
                enrolledCol: currentCol,
                earnedCol: currentCol + 1,
                gradePtCol: currentCol + 2,
                gpaCol: currentCol + 3
            });
            currentCol += colsPerSemester;
        }
        
        const cumStartCol = currentCol;
        const cumEnrolledCol = cumStartCol;
        const cumEarnedCol = cumEnrolledCol + 1;
        const cumGradePtCol = cumEarnedCol + 1;
        const cumCGPACol = cumGradePtCol + 1;
        const resultCol = cumCGPACol + 1;
        const remarksCol = resultCol + 1;
        const finalExamRollCol = remarksCol + 1;
        const serialNumCol = finalExamRollCol + 1;

        // CREATE HEADER ROWS (same as before, just offset by headerOffset)
        const r1 = [];
        r1.push('Serial Number', 'Registration', '', 'Student No', 'Exam Roll', "Student's Name", 'Sex', 'Batch');
        r1.push('Subject(s) Taken');
        for (let i = 1; i < courseCodes.length; i++) r1.push('');
        r1.push('Enrolled Subject(s) No');
        const examSpan = (courseCodes.length * (examColumnsCount + 3)) + 2 + 1 + 1 + courseCodes.length;
        r1.push('Examination of 2025');
        for (let i = 1; i < examSpan; i++) r1.push('');
        r1.push('Exam Roll');
        r1.push('Summary of Result');
        for (let i = 1; i < maxSemesters * colsPerSemester; i++) r1.push('');
        r1.push('Cumulative Result');
        for (let i = 1; i < 4; i++) r1.push('');
        r1.push('Result');
        r1.push('Remarks');
        r1.push('Exam\nRoll');
        r1.push('Serial Number');

        const row1 = ws.getRow(MAIN_TABLE_START_ROW);
        row1.values = r1;
        row1.eachCell((c, n) => {
            if (n === 1 || n === enrolledNumCol || n === examRollCol || n === finalExamRollCol || n === serialNumCol) {
                c.style = vStyle;
            } else {
                c.style = hStyle;
            }
        });

        // ROW 2
        const r2 = ['', 'Number', 'Session', '', '', '', '', ''];
        courseCodes.forEach(code => r2.push(code));
        r2.push('');
        courseCodes.forEach(code => {
            const crs = Object.values(studentMap)[0].courses[code];
            const title = crs ? crs.title : '';
            r2.push(`${code}\n${title}`);
            for (let i = 0; i < examColumnsCount + 2; i++) r2.push('');
        });
        r2.push('Credit Hour', '');
        r2.push('Grade Point');
        r2.push('SGPA');
        r2.push('Course(s) to be Retaken/Taken');
        for (let i = 1; i < courseCodes.length; i++) r2.push('');
        r2.push('');
        for (let i = 0; i < maxSemesters; i++) {
            r2.push(`Semester-${i + 1}`);
            for (let j = 1; j < colsPerSemester; j++) r2.push('');
        }
        r2.push('', '', '', '');
        r2.push('', '', '', '');

        const row2 = ws.getRow(MAIN_TABLE_START_ROW + 1);
        row2.values = r2;
        row2.eachCell(c => c.style = hStyle);

        // ROW 3
        const r3 = ['', '', '', '', '', '', '', ''];
        for (let i = 0; i < courseCodes.length; i++) r3.push('');
        r3.push('');
        courseCodes.forEach(code => {
            const crs = Object.values(studentMap)[0].courses[code];
            const cr = crs ? crs.credits : 3;
            r3.push(`Cr Hrs ${cr}`);
            for (let i = 0; i < examColumnsCount + 2; i++) r3.push('');
        });
        r3.push('Enrolled', 'Earned');
        r3.push('', '');
        courseCodes.forEach(code => r3.push(code));
        r3.push('');
        for (let i = 0; i < maxSemesters; i++) {
            r3.push('Credit Hour');
            r3.push('');
            r3.push('Grade Pt Earned');
            r3.push(i === maxSemesters - 1 ? 'SGPA' : 'GPA');
        }
        r3.push('Credit Hour');
        r3.push('');
        r3.push('Grade Pt Earned');
        r3.push('CGPA');
        r3.push('', '', '', '');

        const row3 = ws.getRow(MAIN_TABLE_START_ROW + 2);
        row3.values = r3;
        row3.eachCell((c, n) => {
            let isGradePtCol = false;
            for (let i = 0; i < maxSemesters; i++) {
                if (n === semesterCols[i].gradePtCol) {
                    isGradePtCol = true;
                    break;
                }
            }
            if (n === cumGradePtCol) isGradePtCol = true;
            c.style = isGradePtCol ? vStyle : hStyle;
        });

        // ROW 4
        const r4 = ['', '', '', '', '', '', '', ''];
        for (let i = 0; i < courseCodes.length; i++) r4.push('');
        r4.push('');
        courseCodes.forEach(() => {
            examTypes.forEach(exam => {
                const isFinal = exam.name.toLowerCase() === 'final';
                let headerText = '';
                if (exam.convertedTo === exam.fullMark) {
                    headerText = `${exam.name}\n(${exam.convertedTo}%)`;
                } else {
                    headerText = `${exam.name}\n(${exam.convertedTo}% of ${exam.fullMark})`;
                }
                r4.push(headerText);
                if (isFinal) r4.push('');
            });
            r4.push('Total', 'Letter\nGrade', 'Grade\nPoint');
        });
        r4.push('Out of 15', 'Out of 15');
        r4.push('', '');
        for (let i = 0; i < courseCodes.length; i++) r4.push('');
        r4.push('');
        for (let i = 0; i < maxSemesters; i++) {
            r4.push('Enrolled', 'Earned');
            r4.push('', '');
        }
        r4.push('Enrolled', 'Earned');
        r4.push('', '');
        r4.push('', '', '', '');

        const row4 = ws.getRow(MAIN_TABLE_START_ROW + 3);
        row4.values = r4;
        row4.eachCell(c => c.style = hStyle);

        // ROW 5
        const r5 = ['', '', '', '', '', '', '', ''];
        for (let i = 0; i < courseCodes.length; i++) r5.push('');
        r5.push('');
        courseCodes.forEach(() => {
            examTypes.forEach(exam => {
                const isFinal = exam.name.toLowerCase() === 'final';
                if (isFinal) {
                    r5.push('', '');
                } else {
                    r5.push('');
                }
            });
            r5.push('', '', '');
        });
        r5.push('', '');
        r5.push('', '');
        for (let i = 0; i < courseCodes.length; i++) r5.push('');
        r5.push('');
        for (let i = 0; i < maxSemesters; i++) {
            r5.push('Out of 15', 'Out of 15');
            r5.push('', '');
        }
        r5.push('Out of 15', 'Out of 15');
        r5.push('', '');
        r5.push('', '', '', '');

        const row5 = ws.getRow(MAIN_TABLE_START_ROW + 4);
        row5.values = r5;
        row5.eachCell(c => c.style = hStyle);

        // MERGE CELLS (same as before)
        ws.mergeCells(MAIN_TABLE_START_ROW, 1, MAIN_TABLE_START_ROW + 4, 1);
        ws.mergeCells(MAIN_TABLE_START_ROW, 2, MAIN_TABLE_START_ROW, 3);
        ws.mergeCells(MAIN_TABLE_START_ROW + 1, 2, MAIN_TABLE_START_ROW + 4, 2);
        ws.mergeCells(MAIN_TABLE_START_ROW + 1, 3, MAIN_TABLE_START_ROW + 4, 3);
        ws.mergeCells(MAIN_TABLE_START_ROW, 4, MAIN_TABLE_START_ROW + 4, 4);
        ws.mergeCells(MAIN_TABLE_START_ROW, 5, MAIN_TABLE_START_ROW + 4, 5);
        ws.mergeCells(MAIN_TABLE_START_ROW, 6, MAIN_TABLE_START_ROW + 4, 6);
        ws.mergeCells(MAIN_TABLE_START_ROW, 7, MAIN_TABLE_START_ROW + 4, 7);
        ws.mergeCells(MAIN_TABLE_START_ROW, 8, MAIN_TABLE_START_ROW + 4, 8);
        ws.mergeCells(MAIN_TABLE_START_ROW, subjectCol, MAIN_TABLE_START_ROW, enrolledNumCol-1);
        
        for (let i = 0; i < courseCodes.length; i++) {
            ws.mergeCells(MAIN_TABLE_START_ROW + 1, subjectCol+i, MAIN_TABLE_START_ROW + 4, subjectCol+i);
        }
        
        ws.mergeCells(MAIN_TABLE_START_ROW, enrolledNumCol, MAIN_TABLE_START_ROW + 4, enrolledNumCol);
        ws.mergeCells(MAIN_TABLE_START_ROW, courseStartCol, MAIN_TABLE_START_ROW, examRollCol-1);

        let col = courseStartCol;
        courseCodes.forEach(() => {
            const courseSpan = examColumnsCount + 3;
            ws.mergeCells(MAIN_TABLE_START_ROW + 1, col, MAIN_TABLE_START_ROW + 1, col + courseSpan - 1);
            ws.mergeCells(MAIN_TABLE_START_ROW + 2, col, MAIN_TABLE_START_ROW + 2, col + courseSpan - 1);
            
            let examCol = col;
            examTypes.forEach(exam => {
                const isFinal = exam.name.toLowerCase() === 'final';
                if (isFinal) {
                    ws.mergeCells(MAIN_TABLE_START_ROW + 3, examCol, MAIN_TABLE_START_ROW + 4, examCol + 1);
                    examCol += 2;
                } else {
                    ws.mergeCells(MAIN_TABLE_START_ROW + 3, examCol, MAIN_TABLE_START_ROW + 4, examCol);
                    examCol += 1;
                }
            });
            
            ws.mergeCells(MAIN_TABLE_START_ROW + 3, examCol, MAIN_TABLE_START_ROW + 4, examCol);
            ws.mergeCells(MAIN_TABLE_START_ROW + 3, examCol + 1, MAIN_TABLE_START_ROW + 4, examCol + 1);
            ws.mergeCells(MAIN_TABLE_START_ROW + 3, examCol + 2, MAIN_TABLE_START_ROW + 4, examCol + 2);
            
            col += courseSpan;
        });

        ws.mergeCells(MAIN_TABLE_START_ROW + 1, creditCol, MAIN_TABLE_START_ROW + 1, creditCol+1);
        ws.mergeCells(MAIN_TABLE_START_ROW + 2, creditCol, MAIN_TABLE_START_ROW + 2, creditCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 2, creditCol+1, MAIN_TABLE_START_ROW + 2, creditCol+1);
        ws.mergeCells(MAIN_TABLE_START_ROW + 3, creditCol, MAIN_TABLE_START_ROW + 4, creditCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 3, creditCol+1, MAIN_TABLE_START_ROW + 4, creditCol+1);

        ws.mergeCells(MAIN_TABLE_START_ROW + 1, gpCol, MAIN_TABLE_START_ROW + 4, gpCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 1, sgpaCol, MAIN_TABLE_START_ROW + 4, sgpaCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 1, retakeCol, MAIN_TABLE_START_ROW + 1, examRollCol-1);
        
        for (let i = 0; i < courseCodes.length; i++) {
            ws.mergeCells(MAIN_TABLE_START_ROW + 2, retakeCol+i, MAIN_TABLE_START_ROW + 4, retakeCol+i);
        }
        
        ws.mergeCells(MAIN_TABLE_START_ROW, examRollCol, MAIN_TABLE_START_ROW + 4, examRollCol);

        const lastSemesterCol = semesterCols[maxSemesters - 1].gpaCol;
        ws.mergeCells(MAIN_TABLE_START_ROW, summaryCol, MAIN_TABLE_START_ROW, lastSemesterCol);
        
        for (let i = 0; i < maxSemesters; i++) {
            const sem = semesterCols[i];
            ws.mergeCells(MAIN_TABLE_START_ROW + 1, sem.enrolledCol, MAIN_TABLE_START_ROW + 1, sem.gpaCol);
            ws.mergeCells(MAIN_TABLE_START_ROW + 2, sem.enrolledCol, MAIN_TABLE_START_ROW + 2, sem.earnedCol);
            ws.mergeCells(MAIN_TABLE_START_ROW + 2, sem.gradePtCol, MAIN_TABLE_START_ROW + 4, sem.gradePtCol);
            ws.mergeCells(MAIN_TABLE_START_ROW + 2, sem.gpaCol, MAIN_TABLE_START_ROW + 4, sem.gpaCol);
        }
        
        ws.mergeCells(MAIN_TABLE_START_ROW, cumEnrolledCol, MAIN_TABLE_START_ROW + 1, cumCGPACol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 2, cumEnrolledCol, MAIN_TABLE_START_ROW + 2, cumEarnedCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 2, cumGradePtCol, MAIN_TABLE_START_ROW + 4, cumGradePtCol);
        ws.mergeCells(MAIN_TABLE_START_ROW + 2, cumCGPACol, MAIN_TABLE_START_ROW + 4, cumCGPACol);

        ws.mergeCells(MAIN_TABLE_START_ROW, resultCol, MAIN_TABLE_START_ROW + 4, resultCol);
        ws.mergeCells(MAIN_TABLE_START_ROW, remarksCol, MAIN_TABLE_START_ROW + 4, remarksCol);
        ws.mergeCells(MAIN_TABLE_START_ROW, finalExamRollCol, MAIN_TABLE_START_ROW + 4, finalExamRollCol);
        ws.mergeCells(MAIN_TABLE_START_ROW, serialNumCol, MAIN_TABLE_START_ROW + 4, serialNumCol);

        // DATA ROWS
        let dataStartRow = MAIN_TABLE_START_ROW + 5;
        let sn = 1;
        
        Object.entries(studentMap).forEach(([id, st]) => {
            const rd = [
                sn,
                st.info.RegistrationNo,
                st.info.RegistrationSession,
                st.info.Roll,
                '',
                st.info.StudentName,
                st.info.Sex,
                st.info.Batch
            ];
    
            courseCodes.forEach(c => rd.push(c));
            rd.push(Object.keys(st.courses).length);
    
            courseCodes.forEach(c => {
                const crs = st.courses[c];
                if (crs) {
                    examTypes.forEach(exam => {
                        const examKey = exam.name.toLowerCase();
                        const marks = crs.examMarks[examKey];
                        const isFinal = examKey === 'final';
                        
                        if (marks) {
                            if (isFinal) {
                                rd.push(marks.inputted || '', marks.converted || '');
                            } else {
                                rd.push(marks.converted || '');
                            }
                        } else {
                            if (isFinal) {
                                rd.push('', '');
                            } else {
                                rd.push('');
                            }
                        }
                    });
                    
                    rd.push(crs.total || '', crs.grade || '', crs.gradePoint || '');
                } else {
                    examTypes.forEach(exam => {
                        const isFinal = exam.name.toLowerCase() === 'final';
                        if (isFinal) {
                            rd.push('', '');
                        } else {
                            rd.push('');
                        }
                    });
                    rd.push('', '', '');
                }
            });
    
            rd.push(st.info.Enroll, st.info.Earned);
            rd.push(st.info.TotalGradePoint || '');
            rd.push(st.info.SGPA || '');
    
            courseCodes.forEach(() => rd.push(''));
            rd.push('');

            const studentId = parseInt(id);
            const studentSemesters = studentSummaryMap[studentId] || [];
            
            for (let i = 0; i < maxSemesters; i++) {
                if (i < studentSemesters.length) {
                    const sem = studentSemesters[i];
                    rd.push(sem.enrolled || 0);
                    rd.push(sem.earned || 0);
                    rd.push(sem.gradePoint || 0);
                    rd.push(sem.gpa || 0);
                } else {
                    rd.push('', '', '', '');
                }
            }
            
            if (studentSemesters.length > 0) {
                const cumEnrolled = studentSemesters.reduce((sum, s) => sum + (s.enrolled || 0), 0);
                const cumEarned = studentSemesters.reduce((sum, s) => sum + (s.earned || 0), 0);
                const cumGradePoint = studentSemesters.reduce((sum, s) => sum + (s.gradePoint || 0), 0);
                const lastSemester = studentSemesters[studentSemesters.length - 1];
                
                rd.push(cumEnrolled);
                rd.push(cumEarned);
                rd.push(cumGradePoint);
                rd.push(lastSemester.cgpa || 0);
            } else {
                rd.push(st.info.Enroll || 0);
                rd.push(st.info.Earned || 0);
                rd.push(st.info.TotalGradePoint || 0);
                rd.push(st.info.CGPA || 0);
            }
            
            rd.push('Passed');
            rd.push('');
            rd.push(st.info.ExamRoll);
            rd.push(sn);
    
            sn++;
    
            const dr = ws.getRow(dataStartRow);
            dr.values = rd;
            dr.eachCell((c, n) => {
                c.style = (n === 6) ? dLeft : dStyle;
            });
            
            dataStartRow++;
        });

        // ========== ADD FOOTER ==========
        const footerStartRow = dataStartRow + 2;
        addSignatureFooter(ws, footerStartRow);

        // ========== SET COLUMN WIDTHS ==========
        ws.getColumn(1).width = 8;
        ws.getColumn(2).width = 16;
        ws.getColumn(3).width = 14;
        ws.getColumn(4).width = 15;
        ws.getColumn(5).width = 12;
        ws.getColumn(6).width = 30;
        ws.getColumn(7).width = 6;
        ws.getColumn(8).width = 8;

        for (let i = 0; i < courseCodes.length; i++) {
            ws.getColumn(subjectCol + i).width = 12;
        }
        ws.getColumn(enrolledNumCol).width = 8;

        let ci = courseStartCol;
        courseCodes.forEach(() => {
            examTypes.forEach(exam => {
                const isFinal = exam.name.toLowerCase() === 'final';
                if (isFinal) {
                    ws.getColumn(ci++).width = 10;
                    ws.getColumn(ci++).width = 10;
                } else {
                    ws.getColumn(ci++).width = 10;
                }
            });
            ws.getColumn(ci++).width = 10;
            ws.getColumn(ci++).width = 8;
            ws.getColumn(ci++).width = 8;
        });

        ws.getColumn(creditCol).width = 10;
        ws.getColumn(creditCol + 1).width = 10;
        ws.getColumn(gpCol).width = 12;
        ws.getColumn(sgpaCol).width = 8;

        for (let i = 0; i < courseCodes.length; i++) {
            ws.getColumn(retakeCol + i).width = 12;
        }
        ws.getColumn(examRollCol).width = 8;

        for (let i = 0; i < maxSemesters; i++) {
            const sem = semesterCols[i];
            ws.getColumn(sem.enrolledCol).width = 10;
            ws.getColumn(sem.earnedCol).width = 10;
            ws.getColumn(sem.gradePtCol).width = 8;
            ws.getColumn(sem.gpaCol).width = 8;
        }
        ws.getColumn(cumEnrolledCol).width = 10;
        ws.getColumn(cumEarnedCol).width = 10;
        ws.getColumn(cumGradePtCol).width = 8;
        ws.getColumn(cumCGPACol).width = 8;
        ws.getColumn(resultCol).width = 12;
        ws.getColumn(remarksCol).width = 15;
        ws.getColumn(finalExamRollCol).width = 12;
        ws.getColumn(serialNumCol).width = 8;

        ws.getRow(MAIN_TABLE_START_ROW).height = 60;
        ws.getRow(MAIN_TABLE_START_ROW + 1).height = 40;
        ws.getRow(MAIN_TABLE_START_ROW + 2).height = 30;
        ws.getRow(MAIN_TABLE_START_ROW + 3).height = 35;

        ws.pageSetup = {
            printArea: `A1:Z${ws.lastRow.number}`,
            fitToPage: true,
            fitToWidth: 1,
            fitToHeight: 0,
            orientation: 'landscape',
            margins: {
                left: 0.4, right: 0.4,
                top: 0.75, bottom: 0.75,
                header: 0.3, footer: 0.3
            }
        };

        const buf = await wb.xlsx.writeBuffer();
        const blob = new Blob([buf], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
        const ts = new Date().toISOString().replace(/[:.]/g, '-').slice(0, -5);
        const fn = `Tabulation_Sheet_${ts}.xlsx`;

        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = fn;
        link.click();
        window.URL.revokeObjectURL(link.href);

        console.log('Excel exported successfully!');
        alert('Excel file downloaded!');
        return true;

        } catch (err) {
            console.error('Export error:', err);
            alert('Error: ' + err.message);
            return false;
        }
        }

        function LoadReport() {
            event.preventDefault();
            try {
                var programId = $("#ctl00_MainContainer_ddlProgram").val();
                var acacalId = $("#ctl00_MainContainer_ucSession_ddlSession").val();

                if (programId > 0 || acacalId > 0) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "RptTabulationSheetV2.aspx/GetTabulationSheetData",
                        data: "{'programId':'" + programId + "','acacalId':'" + acacalId + "'}",
                        dataType: "json",
                        success: function (res) {
                            onComplete();
                            var parsed = JSON.parse(res.d);
                            console.log('Parsed:', parsed);
            
                            if (parsed.status == "success") {
                                exportToExcel(parsed.resultData, parsed.resultSummary);
                            } else {
                                alert('Failed: ' + (parsed.message || 'Unknown error'));
                            }
                        },
                        error: function (res) {
                            onComplete();
                            console.error('AJAX error:', res);
                            alert('Failed to retrieve data');
                        }
                    });
                } else {
                    alert("Select both Program and Session");
                }
            } catch (err) {
                onComplete();
                console.error('Error:', err);
                alert('Error: ' + err.message);
            }
            return true;
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>
            Tabulation Sheet Report
        </label>
    </div>

    <div id="blurOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; backdrop-filter: blur(5px); background-color: rgba(255, 255, 255, 0.3); z-index: 999999;">
    </div>
    <div id="divProgress" style="display: none; z-index: 1000000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Waiting.gif" Height="200px" Width="200px" Style="border-radius: 500px" />
    </div>



    <asp:UpdatePanel ID="UpdatePanel02" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                Sys.Application.add_load(initdropdown);
            </script>
            <div class="card">
                <div class="card-body">

                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <b>Institute</b>
                            <asp:DropDownList ID="ddlInstitute" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <b>Program <span class="text-danger">*</span> </b>
                            <asp:DropDownList ID="ddlProgram" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <b>Registration Session<span class="text-danger">*</span></b>
                            <uc1:SessionUserControl runat="server" ID="ucSession" OnSessionSelectedIndexChanged="ucSession_SessionSelectedIndexChanged" />
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <br />
                            <asp:Button ID="ResultLoadButton" CssClass="btn btn-primary" runat="server" Text="Load Report" OnClientClick="LoadReport()" OnClick="ResultLoadButton_Click1" />

                        </div>


                    </div>
                </div>
            </div>


            <div class="card" style="margin-top: 10px">
                <div class="card-body">

                    <asp:Panel runat="server" ID="pnlTabulationSheet"></asp:Panel>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer
                ID="ReportViewer1"
                runat="server"
                Font-Names="Verdana"
                Font-Size="8pt"
                WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt"
                asynrendering="true"
                Width="80%"
                SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel02" runat="server">
        <Animations>
            <OnUpdating>
                <Parallel duration="0">
                    <ScriptAction Script="InProgress();" />
                    <EnableAction AnimationTarget="ResultLoadButton" Enabled="false" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="onComplete();" />
                    <EnableAction   AnimationTarget="ResultLoadButton" Enabled="true" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </ajaxToolkit:UpdatePanelAnimationExtender>
</asp:Content>
