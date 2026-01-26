using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;

public partial class ClassRoutineManage : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            FillAcademicCalenderCombo();
            //FillProgramCombo();
            FillProgramCheckBoxList();
            btnPrintFormatExcel.Enabled = false;
        }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
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

    private void DownloadGeneratedSchedule(FileInfo file)
    {
        if (file.Name.Length > 0)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
    }

    private void UploadClassSchedule(string filePath)
    {
        Hashtable courseHash = null;
        string programName, FormalCode, VersionCode, VersionId, Title, Section, Day1, Day2, Time1, Time2, Room1, Room2, Faculty1, Faculty2;
        string[,] routine = new string[500, 14];
        string courseNotFound = string.Empty;
        int flagError = 0, insertRow = 0, updateRow = 0;
        string sectionNotAssign = string.Empty;
        string versionCodeNotFound = string.Empty;

        try
        {
            string[] pathAllPart = filePath.Split('\\');
            string[] fileName = pathAllPart[pathAllPart.Length - 1].Split('_');
            int acaCalID = Convert.ToInt32(fileName[2]);

            AcademicCalender academicCalender = AcademicCalenderManager.GetById(acaCalID);
            if (academicCalender != null)
            {

                FileInfo newFile = new FileInfo(@filePath);
                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    

                    #region Hashing List
                    Hashtable dayHash = new Hashtable();
                    dayHash.Add("Sat", 1);
                    dayHash.Add("Sun", 2);
                    dayHash.Add("Mon", 3);
                    dayHash.Add("Tue", 4);
                    dayHash.Add("Wed", 5);
                    dayHash.Add("Thu", 6);
                    dayHash.Add("Fri", 7);

                    List<Course> courseList = CourseManager.GetAll();
                    courseHash = new Hashtable();
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

                    #endregion

                    #region Read Excel Routine

                    int countInsert = 0, countUpdate = 0, numberOfRow = 0;
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    int iRow = 2, index = 0;
                    while (true)
                    {
                        programName = worksheet.Cells[iRow, 1].Value.ToString();
                        FormalCode = worksheet.Cells[iRow, 2].Value.ToString();
                        VersionCode = worksheet.Cells[iRow, 3].Value.ToString();
                        VersionId = worksheet.Cells[iRow, 4].Value.ToString();
                        Title = worksheet.Cells[iRow, 5].Value.ToString();
                        Section = worksheet.Cells[iRow, 6].Value.ToString();
                        Day1 = worksheet.Cells[iRow, 7].Value.ToString();
                        Day2 = worksheet.Cells[iRow, 8].Value.ToString();
                        Time1 = worksheet.Cells[iRow, 9].Value.ToString();
                        Time2 = worksheet.Cells[iRow, 10].Value.ToString();
                        Room1 = worksheet.Cells[iRow, 11].Value.ToString();
                        Room2 = worksheet.Cells[iRow, 12].Value.ToString();
                        Faculty1 = worksheet.Cells[iRow, 13].Value.ToString();
                        Faculty2 = worksheet.Cells[iRow, 14].Value.ToString();

                        if (programName == "" || FormalCode == "" || VersionCode == "" || VersionId == "" || Title == "")
                            break;

                        routine[index, 0] = programName;
                        routine[index, 1] = FormalCode;
                        routine[index, 2] = VersionCode;
                        routine[index, 3] = VersionId;
                        routine[index, 4] = Title;
                        routine[index, 5] = Section;
                        routine[index, 6] = Day1;
                        routine[index, 7] = Day2;
                        routine[index, 8] = Time1;
                        routine[index, 9] = Time2;
                        routine[index, 10] = Room1;
                        routine[index, 11] = Room2;
                        routine[index, 12] = Faculty1;
                        routine[index, 13] = Faculty2;

                        index++;
                        numberOfRow++;


                        //Off
                        #region Off
                        //AcademicCalenderSection acaCalSection = new AcademicCalenderSection();
                        //acaCalSection.AcademicCalenderID = acaCalID;

                        //string[] courseVersion = courseHash[VersionCode].ToString().Split('_');
                        //acaCalSection.CourseID = Convert.ToInt32(courseVersion[0]);//courseList.Where(x => x.VersionCode == VersionCode).SingleOrDefault().CourseID;
                        //acaCalSection.VersionID = Convert.ToInt32(courseVersion[1]); //courseList.Where(x => x.VersionCode == VersionCode).SingleOrDefault().VersionID;

                        //acaCalSection.SectionName = Section;
                        ////acaCalSection.Capacity = Convert.ToInt32(Capacity);

                        //if (Room1 != "")
                        //    acaCalSection.RoomInfoOneID = Convert.ToInt32(roomInfoHash[Room1]); //roomInfoList.Where(x => x.RoomNumber == Room1).SingleOrDefault().RoomInfoID;
                        //if (Room2 != "")
                        //    acaCalSection.RoomInfoTwoID = Convert.ToInt32(roomInfoHash[Room2]); //roomInfoList.Where(x => x.RoomNumber == Room2).SingleOrDefault().RoomInfoID;

                        //if (Day1 != "")
                        //    acaCalSection.DayOne = Convert.ToInt32(dayHash[Day1]);
                        //if (Day2 != "")
                        //    acaCalSection.DayTwo = Convert.ToInt32(dayHash[Day2]);

                        //if (Time1 != "")
                        //    acaCalSection.TimeSlotPlanOneID = Convert.ToInt32(timeSlotPlanHash[Time1]);
                        //if (Time2 != "")
                        //    acaCalSection.TimeSlotPlanTwoID = Convert.ToInt32(timeSlotPlanHash[Time2]);

                        //if (Faculty1 != "")
                        //    acaCalSection.TeacherOneID = Convert.ToInt32(employeeHash[Faculty1]); //employeeList.Where(x => x.Code == Faculty1).SingleOrDefault().TeacherID;
                        //if (Faculty2 != "")
                        //    acaCalSection.TeacherTwoID = Convert.ToInt32(employeeHash[Faculty2]); //employeeList.Where(x => x.Code == Faculty2).SingleOrDefault().TeacherID;

                        //acaCalSection.ProgramID = Convert.ToInt32(programHash[programName]);

                        //acaCalSection.CreatedBy = -1;
                        //acaCalSection.CreatedDate = DateTime.Now;
                        //acaCalSection.ModifiedBy = -1;
                        //acaCalSection.ModifiedDate = DateTime.Now;
                        //acaCalSection.ShareSection = 0;

                        //List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetAllByRoomDayTime(acaCalSection.RoomInfoOneID, acaCalSection.RoomInfoTwoID, acaCalSection.DayOne, acaCalSection.DayTwo, acaCalSection.TimeSlotPlanOneID, acaCalSection.TimeSlotPlanTwoID);

                        //if (acaCalSecList.Count > 0)
                        //{
                        //    acaCalSecList = acaCalSecList.OrderBy(x => x.AcaCal_SectionID).ToList();
                        //    int acaCalSecID = acaCalSecList[0].AcaCal_SectionID;
                        //    acaCalSection.ShareSection = acaCalSecID;

                        //    //foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
                        //    //{
                        //    //    acaCalSec.ShareSection = acaCalSecID;
                        //    //    bool resultUpdate = AcademicCalenderSectionManager.Update(acaCalSec);
                        //    //}
                        //    AcademicCalenderSection tempAcademicCalenderSection = AcademicCalenderSectionManager.GetByCourseVersionSecFac(acaCalSection.CourseID, acaCalSection.VersionID, acaCalSection.SectionName, acaCalSection.TeacherOneID);
                        //    if (tempAcademicCalenderSection == null)
                        //    {
                        //        int resultInsert = AcademicCalenderSectionManager.Insert(acaCalSection);
                        //        if (resultInsert > 0)
                        //            countInsert++;
                        //    }
                        //    else
                        //    {
                        //        acaCalSection.AcaCal_SectionID = tempAcademicCalenderSection.AcaCal_SectionID;
                        //        bool resultUpdate = AcademicCalenderSectionManager.Update(acaCalSection);
                        //        if (resultUpdate)
                        //            countUpdate++;
                        //    }
                        //}
                        //else
                        //{
                        //    int resultInsert = AcademicCalenderSectionManager.Insert(acaCalSection);

                        //    if (resultInsert > 0)
                        //        countInsert++;
                        //}
                        ////End OFF
                        #endregion

                        iRow++;
                    }

                    #endregion

                    #region Insert/Update Routine

                    for (int i = 0; i < index; i++)
                    {
                        AcademicCalenderSection acaCalSection = new AcademicCalenderSection();
                        acaCalSection.AcademicCalenderID = acaCalID;

                        if (routine[i, 5] == "")
                        {
                            sectionNotAssign += routine[i, 2];
                            continue;
                        }

                        if (courseHash.ContainsKey(routine[i, 2]))
                        {
                            string[] courseVersion = courseHash[routine[i, 2]].ToString().Split('_');
                            acaCalSection.CourseID = Convert.ToInt32(courseVersion[0]);
                            acaCalSection.VersionID = Convert.ToInt32(courseVersion[1]);
                            acaCalSection.SectionName = routine[i, 5];

                            AcademicCalenderSection tempAcademicCalenderSection = AcademicCalenderSectionManager.GetByAcaCalCourseVersionSection(acaCalSection.AcademicCalenderID, acaCalSection.CourseID, acaCalSection.VersionID, acaCalSection.SectionName);
                            if (tempAcademicCalenderSection != null)
                            {
                                //Update
                                if (routine[i, 0] != "" && programHash.ContainsKey(routine[i, 0]))
                                    tempAcademicCalenderSection.ProgramID = Convert.ToInt32(programHash[routine[i, 0]]);

                                if (routine[i, 6] != "" && dayHash.ContainsKey(routine[i, 6]))  tempAcademicCalenderSection.DayOne = Convert.ToInt32(dayHash[routine[i, 6]]);
                                else    tempAcademicCalenderSection.DayOne = 0;
                                if (routine[i, 7] != "" && dayHash.ContainsKey(routine[i, 7]))  tempAcademicCalenderSection.DayTwo = Convert.ToInt32(dayHash[routine[i, 7]]);
                                else    tempAcademicCalenderSection.DayTwo = 0;

                                if(routine[i, 8] != "" && timeSlotPlanHash.ContainsKey(routine[i, 8]))  tempAcademicCalenderSection.TimeSlotPlanOneID = Convert.ToInt32(timeSlotPlanHash[routine[i, 8]]);
                                else    tempAcademicCalenderSection.TimeSlotPlanOneID = 0;
                                if (routine[i, 9] != "" && timeSlotPlanHash.ContainsKey(routine[i, 9])) tempAcademicCalenderSection.TimeSlotPlanTwoID = Convert.ToInt32(timeSlotPlanHash[routine[i, 9]]);
                                else    tempAcademicCalenderSection.TimeSlotPlanTwoID = 0;

                                if (routine[i, 10] != "" && roomInfoHash.ContainsKey(routine[i, 10])) tempAcademicCalenderSection.RoomInfoOneID = Convert.ToInt32(roomInfoHash[routine[i, 10]]);
                                else    tempAcademicCalenderSection.RoomInfoOneID = 0;
                                if (routine[i, 11] != "" && roomInfoHash.ContainsKey(routine[i, 11])) tempAcademicCalenderSection.RoomInfoTwoID = Convert.ToInt32(roomInfoHash[routine[i, 11]]);
                                else    tempAcademicCalenderSection.RoomInfoTwoID = 0;

                                if (routine[i, 12] != "" && employeeHash.ContainsKey(routine[i, 12])) tempAcademicCalenderSection.TeacherOneID = Convert.ToInt32(employeeHash[routine[i, 12]]);
                                else    tempAcademicCalenderSection.TeacherOneID = 0;
                                if (routine[i, 13] != "" && employeeHash.ContainsKey(routine[i, 13])) tempAcademicCalenderSection.TeacherTwoID = Convert.ToInt32(employeeHash[routine[i, 13]]);
                                else    tempAcademicCalenderSection.TeacherTwoID = 0;

                                tempAcademicCalenderSection.ModifiedBy = 100;
                                tempAcademicCalenderSection.ModifiedDate = DateTime.Now;

                                bool updateResult = AcademicCalenderSectionManager.Update(tempAcademicCalenderSection);
                                if (updateResult)
                                    updateRow++;
                            }
                            else
                            {
                                //Insert
                                acaCalSection.Capacity = 0;

                                if (routine[i, 6] != "" && dayHash.ContainsKey(routine[i, 6])) acaCalSection.DayOne = Convert.ToInt32(dayHash[routine[i, 6]]);
                                else    acaCalSection.DayOne = 0;
                                if (routine[i, 7] != "" && dayHash.ContainsKey(routine[i, 7])) acaCalSection.DayTwo = Convert.ToInt32(dayHash[routine[i, 7]]);
                                else    acaCalSection.DayTwo = 0;

                                if (routine[i, 8] != "" && timeSlotPlanHash.ContainsKey(routine[i, 8])) acaCalSection.TimeSlotPlanOneID = Convert.ToInt32(timeSlotPlanHash[routine[i, 8]]);
                                else    acaCalSection.TimeSlotPlanOneID = 0;
                                if (routine[i, 9] != "" && timeSlotPlanHash.ContainsKey(routine[i, 9])) acaCalSection.TimeSlotPlanTwoID = Convert.ToInt32(timeSlotPlanHash[routine[i, 9]]);
                                else    acaCalSection.TimeSlotPlanTwoID = 0;

                                if (routine[i, 10] != "" && roomInfoHash.ContainsKey(routine[i, 10])) acaCalSection.RoomInfoOneID = Convert.ToInt32(roomInfoHash[routine[i, 10]]);
                                else    acaCalSection.RoomInfoOneID = 0;
                                if (routine[i, 11] != "" && roomInfoHash.ContainsKey(routine[i, 11])) acaCalSection.RoomInfoTwoID = Convert.ToInt32(roomInfoHash[routine[i, 11]]);
                                else acaCalSection.RoomInfoTwoID = 0;

                                if (routine[i, 12] != "" && employeeHash.ContainsKey(routine[i, 12])) acaCalSection.TeacherOneID = Convert.ToInt32(employeeHash[routine[i, 12]]);
                                else    acaCalSection.TeacherOneID = 0;
                                if (routine[i, 13] != "" && employeeHash.ContainsKey(routine[i, 13])) acaCalSection.TeacherTwoID = Convert.ToInt32(employeeHash[routine[i, 13]]);
                                else acaCalSection.TeacherTwoID = 0;

                                acaCalSection.DeptID = 0;
                                
                                if (routine[i, 0] != "" && programHash.ContainsKey(routine[i, 0]))  acaCalSection.ProgramID = Convert.ToInt32(programHash[routine[i, 0]]);

                                
                                acaCalSection.CreatedBy = 99;
                                acaCalSection.CreatedDate = DateTime.Now;
                                acaCalSection.ModifiedBy = 99;
                                acaCalSection.ModifiedDate = DateTime.Now;
                                acaCalSection.TypeDefinitionID = 0;
                                acaCalSection.Occupied = 0;
                                acaCalSection.ShareSection = 0;
                                acaCalSection.BasicExamTemplateId = 0;

                                List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetAllByRoomDayTime(acaCalSection.RoomInfoOneID, acaCalSection.RoomInfoTwoID, acaCalSection.DayOne, acaCalSection.DayTwo, acaCalSection.TimeSlotPlanOneID, acaCalSection.TimeSlotPlanTwoID);
                                if (acaCalSecList.Count > 0 && acaCalSecList != null)
                                {
                                    acaCalSecList = acaCalSecList.OrderBy(x => x.AcaCal_SectionID).ToList();
                                    int acaCalSecID = acaCalSecList[0].AcaCal_SectionID;

                                    acaCalSection.ShareSection = acaCalSecID;
                                    int insertResult = AcademicCalenderSectionManager.Insert(acaCalSection);
                                    if (insertResult > 0)
                                        insertRow++;
                                }
                                else
                                {
                                    int insertResult = AcademicCalenderSectionManager.Insert(acaCalSection);
                                    if (insertResult > 0)
                                        insertRow++;
                                }
                            }
                        }
                        else
                        {
                            versionCodeNotFound += routine[i, 2];
                        }
                    }

                    #endregion
                }
                //lblAlert.Text = "Upload Complete";
            }
            else { lblMsg.Text = "<b>May you Change the file NAME</b>"; }
        }
        catch { lblAlert.Text = "Error 101"; flagError = 1; }
        finally
        {
            if (flagError == 0)
            {
                lblMsg.Text = "<br /><b>Insert : </b>" + insertRow;
                lblMsg.Text += "<br /><b>Update : </b>" + updateRow;
                lblMsg.Text += "<br /><b>Section Not Assign : </b>" + sectionNotAssign;
                lblMsg.Text += "<br /><b>Course Not Found : </b>" + courseNotFound;
            }
        }
    }

    #endregion

    #region Event

    protected void btnGenerateExcel_GenerateExcel(object sender, EventArgs e)
    {
        int selectedFlag = 0;
        for (int i = 0; i < cblProgram.Items.Count; i++)
            if (cblProgram.Items[i].Selected)
                selectedFlag++;

        if (ddlAcaCalBatch.SelectedValue == "0" || selectedFlag == 0)
        {
            lblMsg.Text = "<b>Please Select Semester AND Program FIRST</b>";
            return;
        }

        int flagExcept = 0;
        FileInfo newFile = null;
        try
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
                offeredCourseList = offeredCourseList.Where(x => x.ProgramID != programIdList[i] && x.IsActive == true).ToList();


            FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/ExcelFiles/SampleExcel.xlsx"));

            string path = @"D:\Routine_Templete\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            newFile = new FileInfo(@"D:\Routine_Templete\" + fileName + ".xlsx");

            using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
            {
                ExcelWorkbook myWorkbook = excelPackage.Workbook;

                ExcelWorksheet classRoutine = myWorkbook.Worksheets["ClassRoutine"];
                classRoutine.Cells[1, 1].Value = "Program";
                classRoutine.Cells[1, 2].Value = "Code";//Formal Code
                classRoutine.Cells[1, 3].Value = "Version Code";//Version Code
                classRoutine.Cells[1, 4].Value = "Version";//Version Id
                classRoutine.Cells[1, 5].Value = "Title";//Course Name
                classRoutine.Cells[1, 6].Value = "Section";
                //classRoutine.Cell(1, 7).Value = "Capacity";
                classRoutine.Cells[1, 7].Value = "Day1";
                classRoutine.Cells[1, 8].Value = "Day2";
                classRoutine.Cells[1, 9].Value = "Time1";
                classRoutine.Cells[1, 10].Value = "Time2";
                classRoutine.Cells[1, 11].Value = "Room1";
                classRoutine.Cells[1, 12].Value = "Room2";
                classRoutine.Cells[1, 13].Value = "Faculty1";
                classRoutine.Cells[1, 14].Value = "Faculty2";

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
                        classRoutine.Cells[row, column].Value = tempProgram.ShortName;
                        classRoutine.Cells[row, column + 1].Value = tempCourse.FormalCode;
                        classRoutine.Cells[row, column + 2].Value = tempCourse.VersionCode;
                        classRoutine.Cells[row, column + 3].Value = tempCourse.VersionID.ToString() + " ";
                        classRoutine.Cells[row, column + 4].Value = tempCourse.Title;
                        row++;
                    }

                }

                ExcelWorksheet setup = myWorkbook.Worksheets["Setup"];
                //Days
                setup.Cells[1, 1].Value = "Days";
                setup.Cells[2, 1].Value = "Sat";
                setup.Cells[3, 1].Value = "Sun";
                setup.Cells[4, 1].Value = "Mon";
                setup.Cells[5, 1].Value = "Tue";
                setup.Cells[6, 1].Value = "Wed";
                setup.Cells[7, 1].Value = "Thu";
                setup.Cells[8, 1].Value = "Fri";

                List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
                List<RoomInformation> roomInfoList = RoomInformationManager.GetAll().OrderBy(x => x.RoomNumber).ToList();
                List<Employee> employeeList = EmployeeManager.GetAll();
                if (employeeList.Count > 0 && employeeList != null) employeeList = employeeList.OrderBy(x => x.Code).ToList();

                if (timeSlotPlanList.Count > 0)
                {
                    setup.Cells[1, 3].Value = "Time Slot";
                    row = 2;
                    column = 3;
                    foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                    {
                        setup.Cells[row, column].Value = timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM");

                        row++;
                    }
                }

                if (roomInfoList.Count > 0)
                {
                    setup.Cells[1, 5].Value = "Room";
                    setup.Cells[1, 6].Value = "Capacity";
                    row = 2;
                    column = 5;
                    foreach (RoomInformation roomInfo in roomInfoList)
                    {
                        setup.Cells[row, column].Value = roomInfo.RoomNumber + " ";
                        setup.Cells[row, column + 1].Value = roomInfo.Capacity.ToString() + " ";

                        row++;
                    }
                }

                if (employeeList.Count > 0)
                {
                    setup.Cells[1, 8].Value = "Faculty Code";
                    setup.Cells[1, 9].Value = "Faculty Name";
                    row = 2;
                    column = 8;
                    foreach (Employee employee in employeeList)
                    {
                        setup.Cells[row, column].Value = employee.Code;
                        setup.Cells[row, column + 1].Value = employee.EmployeeName;

                        row++;
                    }
                }

                excelPackage.Save();
                //lblNotification.Text = "Check D Drive. File Name- " + fileName;

                ImgWaiting.Visible = false;
            }
        }
        catch (Exception ex){ lblNotification.Text = "Error: 202" + ex; flagExcept = 1; }
        finally
        {
            if (flagExcept == 0)
            {
                DownloadGeneratedSchedule(newFile);
            }
        }
    }
    
    protected void btn_UploadExcel(object sender, EventArgs e)
    {
        int flagExcept = 0;
        var path = Path.Combine(Server.MapPath("~/ExcelFiles"), fuUploadFile.FileName);
        try
        {
            if (fuUploadFile.HasFile)
            {
                fuUploadFile.SaveAs(path);

                //string fileName = Server.HtmlEncode(fuUploadFile.FileName);
                //string filePath = Server.MapPath(fuUploadFile.FileName);
                //filePath = "~/AppData/" + fileName;

                string filePath = Server.MapPath("~/ExcelFiles/") + fuUploadFile.FileName;

                UploadClassSchedule(filePath);
            }
            else
            {
                flagExcept = 1;
                lblAlert.Text = "Select The File.";
            }
        }
        catch { File.Delete(path); }
        finally { if (flagExcept == 0)  File.Delete(path); }
    }

    protected void btnDownloadExcel_GenerateExcel(object sender, EventArgs e)
    {
        Hashtable dayHash = null, roomInfoHash = null, timeSlotPlanHash = null, employeeHash = null, programHash = null;
        int selectedFlag = 0;
        for (int i = 0; i < cblProgram.Items.Count; i++)
            if (cblProgram.Items[i].Selected)
                selectedFlag++;

        if (ddlAcaCalBatch.SelectedValue == "0" || selectedFlag == 0)
        {
            lblMsg.Text = "<b>Please Select Semester AND Program FIRST</b>";
            return;
        }

        int flagExcept = 0;
        FileInfo newFile = null;
        try
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

            string fileName = acaCal.CalendarUnitType_TypeName + "_" + acaCal.Year + "_" + acaCal.AcademicCalenderID;
            for (int i = 0; i < index; i++)
                fileName += "_" + programNameArray[i] + "_" + programIdArray[i];

            #region Get Academic Section

            List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(acaCalId);

            if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
            {
                for (int i = 0; i < index2; i++)
                    academicCalenderSectionList = academicCalenderSectionList.Where(x => x.ProgramID != programIdList[i]).ToList();

                if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
                {
                    //Hash Table Generate
                    #region Hashing List
                    dayHash = new Hashtable();
                    dayHash.Add("1", "Sat");
                    dayHash.Add("2", "Sun");
                    dayHash.Add("3", "Mon");
                    dayHash.Add("4", "Tue");
                    dayHash.Add("5", "Wed");
                    dayHash.Add("6", "Thu");
                    dayHash.Add("7", "Fri");                    

                    List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
                    roomInfoHash = new Hashtable();
                    foreach (RoomInformation roomInfo in roomInfoList)
                        roomInfoHash.Add(roomInfo.RoomInfoID, roomInfo.RoomNumber);

                    List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
                    timeSlotPlanHash = new Hashtable();
                    foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                        timeSlotPlanHash.Add(timeSlotPlan.TimeSlotPlanID, timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"));

                    List<Employee> employeeList = EmployeeManager.GetAll();
                    employeeHash = new Hashtable();
                    foreach (Employee employee in employeeList)
                        employeeHash.Add(employee.EmployeeID, employee.Code);

                    List<Program> programList = ProgramManager.GetAll();
                    programHash = new Hashtable();
                    foreach (Program program in programList)
                        programHash.Add(program.ProgramID, program.ShortName);

                    #endregion


                    FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/ExcelFiles/SampleExcel.xlsx"));
                    newFile = new FileInfo(@"D:\Routine_Templete\" + fileName + ".xlsx");
                    using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                    {
                        ExcelWorkbook myWorkbook = excelPackage.Workbook;

                        ExcelWorksheet classRoutine = myWorkbook.Worksheets["ClassRoutine"];
                        classRoutine.Cells[1, 1].Value = "Program";
                        classRoutine.Cells[1, 2].Value = "Code";//Formal Code
                        classRoutine.Cells[1, 3].Value = "Version Code";//Version Code
                        classRoutine.Cells[1, 4].Value = "Version";//Version Id
                        classRoutine.Cells[1, 5].Value = "Title";//Course Name
                        classRoutine.Cells[1, 6].Value = "Section";
                        //classRoutine.Cell(1, 7).Value = "Capacity";
                        classRoutine.Cells[1, 7].Value = "Day1";
                        classRoutine.Cells[1, 8].Value = "Day2";
                        classRoutine.Cells[1, 9].Value = "Time1";
                        classRoutine.Cells[1, 10].Value = "Time2";
                        classRoutine.Cells[1, 11].Value = "Room1";
                        classRoutine.Cells[1, 12].Value = "Room2";
                        classRoutine.Cells[1, 13].Value = "Faculty1";
                        classRoutine.Cells[1, 14].Value = "Faculty2";

                        Course tempCourse;
                        Program tempProgram;
                        int row = 2;
                        int column = 1;
                        foreach (AcademicCalenderSection acaCalSec in academicCalenderSectionList)
                        {
                            Course course = CourseManager.GetByCourseIdVersionId(acaCalSec.CourseID, acaCalSec.VersionID);
                            if (course != null)
                            {
                                if (programHash.ContainsKey(acaCalSec.ProgramID))
                                    classRoutine.Cells[row, column].Value = programHash[acaCalSec.ProgramID].ToString();

                                classRoutine.Cells[row, column + 1].Value = course.FormalCode;
                                classRoutine.Cells[row, column + 2].Value = course.VersionCode;
                                classRoutine.Cells[row, column + 3].Value = course.VersionID.ToString() + " ";
                                classRoutine.Cells[row, column + 4].Value = course.Title;
                                classRoutine.Cells[row, column + 5].Value = acaCalSec.SectionName;

                                if(dayHash.ContainsKey(acaCalSec.DayOne.ToString()))
                                    classRoutine.Cells[row, column + 6].Value = dayHash[acaCalSec.DayOne.ToString()].ToString();
                                if (dayHash.ContainsKey(acaCalSec.DayTwo.ToString()))
                                    classRoutine.Cells[row, column + 7].Value = dayHash[acaCalSec.DayTwo.ToString()].ToString();

                                if(timeSlotPlanHash.ContainsKey(acaCalSec.TimeSlotPlanOneID))
                                    classRoutine.Cells[row, column + 8].Value = timeSlotPlanHash[acaCalSec.TimeSlotPlanOneID].ToString();
                                if (timeSlotPlanHash.ContainsKey(acaCalSec.TimeSlotPlanTwoID))
                                    classRoutine.Cells[row, column + 9].Value = timeSlotPlanHash[acaCalSec.TimeSlotPlanTwoID].ToString();

                                if(roomInfoHash.ContainsKey(acaCalSec.RoomInfoOneID))
                                    classRoutine.Cells[row, column + 10].Value = roomInfoHash[acaCalSec.RoomInfoOneID].ToString();
                                if (roomInfoHash.ContainsKey(acaCalSec.RoomInfoTwoID))
                                    classRoutine.Cells[row, column + 11].Value = roomInfoHash[acaCalSec.RoomInfoTwoID].ToString();

                                if(employeeHash.ContainsKey(acaCalSec.TeacherOneID))
                                    classRoutine.Cells[row, column + 12].Value = employeeHash[acaCalSec.TeacherOneID].ToString();
                                if (employeeHash.ContainsKey(acaCalSec.TeacherTwoID))
                                    classRoutine.Cells[row, column + 13].Value = employeeHash[acaCalSec.TeacherTwoID].ToString();
                            }
                            row++;
                        }

                        ExcelWorksheet setup = myWorkbook.Worksheets["Setup"];
                        //Days
                        setup.Cells[1, 1].Value = "Days";
                        setup.Cells[2, 1].Value = "Sat";
                        setup.Cells[3, 1].Value = "Sun";
                        setup.Cells[4, 1].Value = "Mon";
                        setup.Cells[5, 1].Value = "Tue";
                        setup.Cells[6, 1].Value = "Wed";
                        setup.Cells[7, 1].Value = "Thu";
                        setup.Cells[8, 1].Value = "Fri";

                        timeSlotPlanList = TimeSlotPlanManager.GetAll();
                        roomInfoList = RoomInformationManager.GetAll().OrderBy(x => x.RoomNumber).ToList();
                        employeeList = EmployeeManager.GetAll();
                        if (employeeList.Count > 0 && employeeList != null) employeeList = employeeList.OrderBy(x => x.Code).ToList();

                        if (timeSlotPlanList.Count > 0)
                        {
                            setup.Cells[1, 3].Value = "Time Slot";
                            row = 2;
                            column = 3;
                            foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                            {
                                setup.Cells[row, column].Value = timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM");

                                row++;
                            }
                        }

                        if (roomInfoList.Count > 0)
                        {
                            setup.Cells[1, 5].Value = "Room";
                            setup.Cells[1, 6].Value = "Capacity";
                            row = 2;
                            column = 5;
                            foreach (RoomInformation roomInfo in roomInfoList)
                            {
                                setup.Cells[row, column].Value = roomInfo.RoomNumber + " ";
                                setup.Cells[row, column + 1].Value = roomInfo.Capacity.ToString() + " ";

                                row++;
                            }
                        }

                        if (employeeList.Count > 0)
                        {
                            setup.Cells[1, 8].Value = "Faculty Code";
                            setup.Cells[1, 9].Value = "Faculty Name";
                            row = 2;
                            column = 8;
                            foreach (Employee employee in employeeList)
                            {
                                setup.Cells[row, column].Value = employee.Code;
                                setup.Cells[row, column + 1].Value = employee.EmployeeName;

                                row++;
                            }
                        }

                        excelPackage.Save();
                        //lblNotification.Text = "Check D Drive. File Name- " + fileName;
                    }
                }
            }

            #endregion
        }
        catch { lblNotification.Text = "Error: 202"; flagExcept = 1; }
        finally
        {
            if (flagExcept == 0)
            {
                DownloadGeneratedSchedule(newFile);
            }
        }
    }

    protected void btnPrintFormatExcel_GenerateExcel(object sender, EventArgs e)
    {
    }

    #endregion
}