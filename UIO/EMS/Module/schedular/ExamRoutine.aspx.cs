using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ExamRoutine : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            FillAcademicCalenderCombo();
            FillProgramCheckBoxList();
        }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillProgramCheckBoxList()
    {
        try
        {
            cblProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            if (programList != null)
            {
                cblProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                cblProgram.DataValueField = "ProgramID";
                cblProgram.DataTextField = "ShortName";
                cblProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void btn_GenerateExcel(object sender, EventArgs e)
    {
        int[] programIdList = new int[50], programIdArray = new int[50];
        string[] programNameArray = new string[50];
        int index = 0, index2 = 0;

        for (int j = 0; j < cblProgram.Items.Count; j++)
        {
            if (cblProgram.Items[j].Selected)
            {
                programIdArray[index] = Convert.ToInt32(cblProgram.Items[j].Value);
                programNameArray[index] = cblProgram.Items[j].Text;
                index++;
            }
            else
            {
                programIdList[index2] = Convert.ToInt32(cblProgram.Items[j].Value);
                index2++;
            }
        }

        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        AcademicCalender acaCal = AcademicCalenderManager.GetById(acaCalId);
        //Program program = ProgramManager.GetById(programId);
        List<Program> programList = ProgramManager.GetAll();

        string fileName = acaCal.CalendarUnitType_TypeName + "_" + acaCal.Year + "_" + acaCal.AcademicCalenderID;
        for (int i = 0; i < index; i++)
            fileName += "_" + programNameArray[i] + "_" + programIdArray[i];

        List<Course> courseList = CourseManager.GetAll();

        List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAll();
        offeredCourseList = offeredCourseList.Where(x => x.AcademicCalenderID == acaCalId).ToList();
        for (int i = 0; i < index2; i++)
            offeredCourseList = offeredCourseList.Where(x => x.ProgramID != programIdList[i]).ToList();


        FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/AppData/SampleExcel.xlsx"));
        FileInfo newFile = new FileInfo(@"D:\" + fileName + ".xlsx");
        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
            ExcelWorkbook myWorkbook = excelPackage.Workbook;

            ExcelWorksheet classRoutine = myWorkbook.Worksheets["ClassRoutine"];
            classRoutine.Cell(1, 1).Value = "Program";
            classRoutine.Cell(1, 2).Value = "Code";//Formal Code
            classRoutine.Cell(1, 3).Value = "Version Code";//Version Code
            classRoutine.Cell(1, 4).Value = "Version";//Version Id
            classRoutine.Cell(1, 5).Value = "Title";//Course Name
            classRoutine.Cell(1, 6).Value = "Section";
            classRoutine.Cell(1, 7).Value = "Capacity";
            classRoutine.Cell(1, 8).Value = "Day1";
            classRoutine.Cell(1, 9).Value = "Day2";
            classRoutine.Cell(1, 10).Value = "Time1";
            classRoutine.Cell(1, 11).Value = "Time2";
            classRoutine.Cell(1, 12).Value = "Room1";
            classRoutine.Cell(1, 13).Value = "Room2";
            classRoutine.Cell(1, 14).Value = "Faculty1";
            classRoutine.Cell(1, 15).Value = "Faculty2";

            Course tempCourse;
            Program tempProgram;
            int row = 2;
            int column = 1;
            foreach (OfferedCourse offeredCourse in offeredCourseList)
            {
                tempProgram = programList.Where(x => x.ProgramID == offeredCourse.ProgramID).SingleOrDefault();
                tempCourse = courseList.Where(x => x.CourseID == offeredCourse.CourseID && x.VersionID == offeredCourse.VersionID).SingleOrDefault();

                if (tempCourse != null && tempProgram != null)
                {
                    classRoutine.Cell(row, column).Value = tempProgram.ShortName;
                    classRoutine.Cell(row, column + 1).Value = tempCourse.FormalCode;
                    classRoutine.Cell(row, column + 2).Value = tempCourse.VersionCode;
                    classRoutine.Cell(row, column + 3).Value = tempCourse.VersionID.ToString();
                    classRoutine.Cell(row, column + 4).Value = tempCourse.Title;
                    row++;
                }

            }

            ExcelWorksheet setup = myWorkbook.Worksheets["Setup"];
            //Days
            setup.Cell(1, 1).Value = "Days";
            setup.Cell(2, 1).Value = "Sat";
            setup.Cell(3, 1).Value = "Sun";
            setup.Cell(4, 1).Value = "Mon";
            setup.Cell(5, 1).Value = "Tue";
            setup.Cell(6, 1).Value = "Wed";
            setup.Cell(7, 1).Value = "Thu";
            setup.Cell(8, 1).Value = "Fri";

            List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
            List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
            List<Employee> employeeList = EmployeeManager.GetAll();

            if (timeSlotPlanList.Count > 0)
            {
                setup.Cell(1, 3).Value = "Time Slot";
                row = 2;
                column = 3;
                foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                {
                    setup.Cell(row, column).Value = timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM");

                    row++;
                }
            }

            if (roomInfoList.Count > 0)
            {
                setup.Cell(1, 5).Value = "Room";
                setup.Cell(1, 6).Value = "Capacity";
                row = 2;
                column = 5;
                foreach (RoomInformation roomInfo in roomInfoList)
                {
                    setup.Cell(row, column).Value = roomInfo.RoomNumber;
                    setup.Cell(row, column + 1).Value = roomInfo.Capacity.ToString();

                    row++;
                }
            }

            if (employeeList.Count > 0)
            {
                setup.Cell(1, 8).Value = "Faculty Code";
                setup.Cell(1, 9).Value = "Faculty Name";
                row = 2;
                column = 8;
                foreach (Employee employee in employeeList)
                {
                    setup.Cell(row, column).Value = employee.Code;
                    setup.Cell(row, column + 1).Value = employee.EmployeeName;

                    row++;
                }
            }

            excelPackage.Save();
            lblNotification.Text = "Check C Drive. File Name- " + fileName;
        }
    }

    protected void btn_UploadExcel(object sender, EventArgs e)
    {
        if (fuUploadFile.HasFile)
        {


            var path = Path.Combine(Server.MapPath("~/AppData"), fuUploadFile.FileName);
            fuUploadFile.SaveAs(path);

            //string fileName = Server.HtmlEncode(fuUploadFile.FileName);
            //string filePath = Server.MapPath(fuUploadFile.FileName);
            //filePath = "~/AppData/" + fileName;

            string filePath = Server.MapPath("~/AppData/") + fuUploadFile.FileName;

            _readExcelFile2(filePath);

            File.Delete(path);
        }
    }

    private void _readExcelFile2(string filePath)
    {
        Microsoft.Office.Interop.Excel.Application appExl;
        Microsoft.Office.Interop.Excel.Workbook workbook;
        Microsoft.Office.Interop.Excel.Worksheet NwSheet;
        Microsoft.Office.Interop.Excel.Range ShtRange;
        appExl = new Microsoft.Office.Interop.Excel.ApplicationClass();

        string[] pathAllPart = filePath.Split('\\');
        string[] fileName = pathAllPart[pathAllPart.Length - 1].Split('_');

        try
        {
            workbook = appExl.Workbooks.Open(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            NwSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets.get_Item(1);

            int Cnum = 0;
            int Rnum = 0;

            ShtRange = NwSheet.UsedRange;

            #region Hashing List
            Hashtable dayHash = new Hashtable();
            dayHash.Add("Sun", 1);
            dayHash.Add("Mon", 2);
            dayHash.Add("Tue", 3);
            dayHash.Add("Wed", 4);
            dayHash.Add("Thu", 5);
            dayHash.Add("Fri", 6);
            dayHash.Add("Sat", 7);

            List<Course> courseList = CourseManager.GetAll();
            Hashtable courseHash = new Hashtable();
            foreach (Course course in courseList)
                courseHash.Add(course.VersionCode, course.CourseID + "_" + course.VersionID);

            List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
            Hashtable roomInfoHash = new Hashtable();
            foreach (RoomInformation roomInfo in roomInfoList)
                roomInfoHash.Add(roomInfo.RoomNumber, roomInfo.RoomInfoID);

            List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
            Hashtable timeSlotPlanHash = new Hashtable();
            foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                timeSlotPlanHash.Add(timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"), timeSlotPlan.TimeSlotPlanID);

            List<Employee> employeeList = EmployeeManager.GetAll();
            Hashtable employeeHash = new Hashtable();
            foreach (Employee employee in employeeList)
                employeeHash.Add(employee.Code, employee.EmployeeID);

            List<Program> programList = ProgramManager.GetAll();
            Hashtable programHash = new Hashtable();
            foreach (Program program in programList)
                programHash.Add(program.ShortName, program.ProgramID);

            int acaCalID = Convert.ToInt32(fileName[2]);
            #endregion

            for (Rnum = 2; Rnum <= ShtRange.Rows.Count; Rnum++)
            {
                string programName, FormalCode, VersionCode, VersonId, Title, Section, Capacity, Day1, Day2, Time1, Time2, Room1, Room2, Faculty1, Faculty2;
                programName = (ShtRange.Cells[Rnum, 1] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 1] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                FormalCode = (ShtRange.Cells[Rnum, 2] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 2] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                VersionCode = (ShtRange.Cells[Rnum, 3] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 3] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                VersonId = (ShtRange.Cells[Rnum, 4] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 4] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Title = (ShtRange.Cells[Rnum, 5] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 5] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Section = (ShtRange.Cells[Rnum, 6] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 6] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Capacity = (ShtRange.Cells[Rnum, 7] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 7] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Day1 = (ShtRange.Cells[Rnum, 8] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 8] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Day2 = (ShtRange.Cells[Rnum, 9] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 9] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Time1 = (ShtRange.Cells[Rnum, 10] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 10] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Time2 = (ShtRange.Cells[Rnum, 11] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 11] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Room1 = (ShtRange.Cells[Rnum, 12] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 12] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Room2 = (ShtRange.Cells[Rnum, 13] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 13] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Faculty1 = (ShtRange.Cells[Rnum, 14] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 14] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";
                Faculty2 = (ShtRange.Cells[Rnum, 15] as Microsoft.Office.Interop.Excel.Range).Value2 != null ? (ShtRange.Cells[Rnum, 15] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() : "";

                AcademicCalenderSection acaCalSection = new AcademicCalenderSection();
                acaCalSection.AcademicCalenderID = acaCalID;

                string[] courseVersion = courseHash[VersionCode].ToString().Split('_');
                acaCalSection.CourseID = Convert.ToInt32(courseVersion[0]);//courseList.Where(x => x.VersionCode == VersionCode).SingleOrDefault().CourseID;
                acaCalSection.VersionID = Convert.ToInt32(courseVersion[1]); //courseList.Where(x => x.VersionCode == VersionCode).SingleOrDefault().VersionID;

                acaCalSection.SectionName = Section;
                acaCalSection.Capacity = Convert.ToInt32(Capacity);

                acaCalSection.RoomInfoOneID = Convert.ToInt32(roomInfoHash[Room1]); //roomInfoList.Where(x => x.RoomNumber == Room1).SingleOrDefault().RoomInfoID;
                acaCalSection.RoomInfoTwoID = Convert.ToInt32(roomInfoHash[Room2]); //roomInfoList.Where(x => x.RoomNumber == Room2).SingleOrDefault().RoomInfoID;

                acaCalSection.DayOne = Convert.ToInt32(dayHash[Day1]);
                acaCalSection.DayTwo = Convert.ToInt32(dayHash[Day2]);

                acaCalSection.TimeSlotPlanOneID = Convert.ToInt32(timeSlotPlanHash[Time1]);
                acaCalSection.TimeSlotPlanTwoID = Convert.ToInt32(timeSlotPlanHash[Time2]);

                acaCalSection.TeacherOneID = Convert.ToInt32(employeeHash[Faculty1]); //employeeList.Where(x => x.Code == Faculty1).SingleOrDefault().TeacherID;
                acaCalSection.TeacherTwoID = Convert.ToInt32(employeeHash[Faculty2]); //employeeList.Where(x => x.Code == Faculty2).SingleOrDefault().TeacherID;

                acaCalSection.ProgramID = Convert.ToInt32(programHash[programName]);

                acaCalSection.CreatedBy = -1;
                acaCalSection.CreatedDate = DateTime.Now;
                acaCalSection.ModifiedBy = -1;
                acaCalSection.ModifiedDate = DateTime.Now;

                int result = AcademicCalenderSectionManager.Insert(acaCalSection);
            }
            appExl.Workbooks.Close();
        }
        catch
        {

        }
    }
}