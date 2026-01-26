using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using OfficeOpenXml;
using CommonUtility;

public partial class Admin_GradeSheet : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            SetUserInfoInSession();
            LoadCamboBox();
            fuGradeSheet.Enabled = true;
            btnImport.Enabled = true;
            btnViewGradeSheet.Enabled = true;

            Panel1.Enabled = true;
        }
    }

    void LoadCamboBox()
    {
        FillProgramComboBox();
        FillAcademicCalenderCombo();
        FillAcaCalSectionCombo();
    }

    void SetUserInfoInSession()
    {
        try
        {
            int employeeId = 0;
            HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];

            string uid = aCookie["UserName"];
            string pwd = aCookie["UserPassword"];

            User user = UserManager.GetByLogInId(uid);
            if (user != null)
            {
                Role role = RoleManager.GetById(user.RoleID);
                if (role != null)
                {
                    Session["Role"] = role.RoleName;
                }
                if (user.Person != null)
                {
                    if (user.Person.Employee != null)
                        employeeId = user.Person.Employee.EmployeeID;
                }
            }
        }
        catch { }
    }

    void FillProgramComboBox()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();


            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    void FillAcademicCalenderCombo()
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

    void FillAcaCalSectionCombo()
    {
        ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
    }

    void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
    {
        try
        {
            int employeeId = 0;
            HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];

            string uid = aCookie["UserName"];
            string pwd = aCookie["UserPassword"];

            User user = UserManager.GetByLogInId(uid);
            if (user != null)
            {
                if (user.Person != null)
                {
                    if (user.Person.Employee != null)
                        employeeId = user.Person.Employee.EmployeeID;
                }
            }

            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

                if (acaCalId != 0 && programId != 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId)).ToList();
                else if (acaCalId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId).ToList();
                else if (programId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

                if (Session["Role"].ToString().Contains("Faculty") || Session["Role"].ToString().Contains("KIUA"))
                {
                    if (employeeId != 0)
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == employeeId || x.TeacherTwoID == employeeId).ToList();
                    else
                        acaCalSectionList = null;
                }
                else if (Session["Role"].ToString() != "MainAdmin" && Session["Role"].ToString() != "DepartmentExecutive")
                {
                    acaCalSectionList = null;
                }

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.Title + ":" + course.FormalCode);

                    //acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ThenBy(x => x.VersionID).ToList();
                    Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                    foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                    {
                        string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                        //ddlAcaCalSection.Items.Add(new ListItem(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()));
                        try
                        {
                            dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString());
                        }
                        catch { }
                    }
                    var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                    foreach (var temp in acaCalSecList)
                        ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                }
            }
        }
        catch { }
    }

    void ReadExcelFile(string filePath)
    {
        string resultNotAssign = string.Empty;
        try
        {
            //Fetch Dropdown Value
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            //Get Object From Database
            AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSectionId);
            List<GradeSheet> gradeSheetList = GradeSheetManager.GetAllByAcaCalSectionId(acaCalSectionId);

            GradeSheet gradeSheet = new GradeSheet();
            gradeSheet.ProgramID = programId;
            gradeSheet.AcademicCalenderID = acaCalId;
            gradeSheet.CourseID = acaCalSection.CourseID;
            gradeSheet.VersionID = acaCalSection.VersionID;
            gradeSheet.AcaCal_SectionID = acaCalSectionId;
            gradeSheet.TeacherID = acaCalSection.TeacherOneID;

            FileInfo newFile = new FileInfo(@filePath);
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                string versionCode = worksheet.Cells[4, 1].Value.ToString();
                string code = string.Empty;
                for (int i = 13; versionCode[i] != ','; i++)
                    code += versionCode[i];

                string sectionCode = worksheet.Cells[5, 1].Value.ToString();
                string section = string.Empty;
                for (int i = 9; sectionCode[i] != ','; i++)
                    code += sectionCode[i];

                AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(acaCalSectionId);
                if (academicCalenderSection != null)
                {
                    Course course = CourseManager.GetByCourseIdVersionId(academicCalenderSection.CourseID, academicCalenderSection.VersionID);
                    if (course != null)
                    {
                        string sectionTemp = string.Empty;
                        sectionTemp = course.VersionCode + acaCalSection.SectionName;
                        if (sectionTemp != code)
                        {
                            lblMsg.Text = "<b>You Choose the wrong File OR Select wrong Course.</b>";
                            return;
                        }
                    }
                }

                int iRow = 8;
                //Variable result
                //Used For Store Result which upload from Excel Sheet
                string[,] result = new string[500, 3];

                int indexX = 0, flagResultNotAssign = 0;

                while (true)
                {
                    string studentRoll, courseGrade, courseMark;
                    studentRoll = worksheet.Cells[iRow, 3].Value.ToString();
                    courseGrade = worksheet.Cells[iRow, 4].Value.ToString() == "#N/A" ? "" : worksheet.Cells[iRow, 4].Value.ToString();
                    courseMark = worksheet.Cells[iRow, 5].Value.ToString() == "-1" ? "" : worksheet.Cells[iRow, 5].Value.ToString();
                    iRow++;

                    if (studentRoll != "" && courseGrade != "" && courseMark != "")
                    {
                        result[indexX, 0] = studentRoll;
                        result[indexX, 1] = courseGrade;
                        result[indexX, 2] = courseMark;
                        indexX++;
                    }
                    else if (studentRoll == "")
                    {
                        break;
                    }
                    else
                    {
                        if (flagResultNotAssign == 1)
                        {
                            resultNotAssign += ", " + worksheet.Cells[iRow, 3].Value.ToString();
                        }
                        else
                        {
                            flagResultNotAssign = 1;
                            resultNotAssign += worksheet.Cells[iRow, 3].Value.ToString();
                        }
                    }
                }

                #region Read From Result Array and Insert/Update GradeSheet Database Table

                List<StudentCourseHistory> studentCourseHistory = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSectionId);
                lblMsg.Text = "";
                string studentNotFound = string.Empty, studentNotThisCourse = string.Empty;
                int flagNotFound = 0, flagNotThisCourse = 0, gradeSheetInsert = 0, gradeSheetUpdate = 0;

                for (int i = 0; i < indexX; i++)
                {
                    StudentCourseHistory tempStudentCourseHistory = null;

                    Student student = StudentManager.GetByRoll(result[i, 0]);
                    if (student != null)
                    {
                        tempStudentCourseHistory = studentCourseHistory.Where(x => x.StudentID == student.StudentID).SingleOrDefault();
                        if (tempStudentCourseHistory != null)
                        {
                            gradeSheet.StudentID = student.StudentID;
                            gradeSheet.ObtainGrade = result[i, 1];
                            gradeSheet.ObtainMarks = Convert.ToDecimal(result[i, 2]);
                            gradeSheet.CreatedBy = 99;
                            gradeSheet.CreatedDate = DateTime.Now;
                            gradeSheet.ModifiedBy = 99;
                            gradeSheet.ModifiedDate = DateTime.Now;

                            int resultInsert = 0;
                            bool resultUpdate = false;
                            GradeSheet gradeSheetTemp = gradeSheetList.Where(x => x.StudentID == student.StudentID && x.CourseID == gradeSheet.CourseID && x.VersionID == gradeSheet.VersionID).FirstOrDefault();
                            if (gradeSheetTemp == null)
                            {
                                resultInsert = GradeSheetManager.Insert(gradeSheet);
                                if (resultInsert > 0)
                                    gradeSheetInsert++;
                            }
                            else
                            {
                                gradeSheet.GradeSheetId = gradeSheetTemp.GradeSheetId;
                                resultUpdate = GradeSheetManager.Update(gradeSheet);
                                if (resultUpdate)
                                    gradeSheetUpdate++;
                            }
                        }
                        else
                        {
                            if (flagNotThisCourse == 1)
                            {
                                studentNotThisCourse += ", " + result[i, 0];
                            }
                            else
                            {
                                flagNotThisCourse = 1;
                                studentNotThisCourse += result[i, 0];
                            }
                        }
                    }
                    else
                    {
                        if (flagNotFound == 1)
                        {
                            studentNotFound += ", " + result[i, 0];
                        }
                        else
                        {
                            flagNotFound = 1;
                            studentNotFound += result[i, 0];
                        }
                    }

                    lblMsg.Text = "<br />New Data Insert : " + gradeSheetInsert + "; Data Updated : " + gradeSheetUpdate + ";";
                    if (studentNotFound.Length > 0)
                        lblMsg.Text += "<br /> Student Not Fount: " + studentNotFound;
                    if (studentNotThisCourse.Length > 0)
                        lblMsg.Text += "<br /> Student Not In This Course: " + studentNotThisCourse;
                    if (resultNotAssign.Length > 0)
                        lblMsg.Text += "<br /> Marks are not assign: " + resultNotAssign;
                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Your selected file is back dated. Please Use Office Excel 2010.";
        }
        finally { }
    }

    void GenerateGradeSheet(int acaCalId, int programId, int acaCalSectionId)
    {
        int flagExcept = 0;
        string fileName = string.Empty;
        FileInfo newFile = null;
        try
        {
            //int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSectionId);
            if (acaCalSection != null)
            {
                Program program = ProgramManager.GetById(acaCalSection.ProgramID);
                AcademicCalender acaCal = AcademicCalenderManager.GetById(acaCalSection.AcademicCalenderID);
                Course course = CourseManager.GetByCourseIdVersionId(acaCalSection.CourseID, acaCalSection.VersionID);
                Employee emp1 = EmployeeManager.GetById(acaCalSection.TeacherOneID);
                Employee emp2 = null;
                Person person1 = null;
                Person person2 = null;
                if (emp1 != null)
                    person1 = PersonManager.GetById(emp1.PersonId);
                GradeSheetTemplate gradeSheetTemplate = GradeSheetTemplateManager.GetById(acaCalSection.BasicExamTemplateId);
                //Employee employee = EmployeeManager.GetByPersonId(person.PersonID);

                //string fileName = acaCal.CalendarUnitType_TypeName + "_" + acaCal.Year + "_" + acaCal.AcademicCalenderID + "_" + program.ShortName;
                fileName = course.VersionCode.Replace(' ', '_') + "_" + acaCalSection.SectionName + "_" + emp1.Code;

                #region 2nd Faculty Is Exist
                if (acaCalSection.TeacherTwoID != 0)
                {
                    emp2 = EmployeeManager.GetById(acaCalSection.TeacherTwoID);
                    if (emp2 != null)
                    {
                        person2 = PersonManager.GetById(emp2.PersonId);
                        if (person2 != null)
                            fileName += "_" + emp2.Code;
                    }
                }
                #endregion

                List<StudentCourseHistory> stdCourseHistoryList = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSectionId);
                int numberOfStudent = Convert.ToInt32(gradeSheetTemplate.Path) + 1;
                string originalFileName = string.Empty;
                if (numberOfStudent > stdCourseHistoryList.Count)
                    originalFileName = gradeSheetTemplate.Code + ".xlsx";
                else
                    originalFileName = gradeSheetTemplate.Code + "2.xlsx";

                char flag = gradeSheetTemplate.Code[gradeSheetTemplate.Code.Length - 1];

                FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/ExcelFiles/" + originalFileName));
                //FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/ExcelFiles/Demo.xlsx"));
                string path = @"D:\Grade_Sheet_Templete\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                newFile = new FileInfo(@"D:\Grade_Sheet_Templete\" + fileName + ".xlsx");

                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;

                    ExcelWorksheet gradeSheet = myWorkbook.Worksheets["Course"];
                    if (stdCourseHistoryList.Count > 0 && stdCourseHistoryList != null)
                    {
                        CourseStatus courseStatusDp = CourseStatusManager.GetByCode("Dp");
                        if (courseStatusDp != null)
                            stdCourseHistoryList = stdCourseHistoryList.Where(x => x.CourseStatusID != courseStatusDp.CourseStatusID).ToList();
                    }
                    //List<StudentCourseHistory> stdCourseHistoryList = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSectionId);
                    if (stdCourseHistoryList.Count > 0 && stdCourseHistoryList != null)
                    {
                        Dictionary<string, string> dicStudentInfo = new Dictionary<string, string>();
                        Dictionary<string, int> dicStudentInfoWgrade = new Dictionary<string, int>();

                        foreach (StudentCourseHistory stdCourseHistory in stdCourseHistoryList)
                        {
                            Student tempStudent = StudentManager.GetById(stdCourseHistory.StudentID);
                            Person tempPerson = PersonManager.GetById(tempStudent.PersonID);

                            dicStudentInfo.Add(tempStudent.Roll, tempPerson.FullName);
                            dicStudentInfoWgrade.Add(tempStudent.Roll, Convert.ToInt32(stdCourseHistory.CourseStatusID));
                        }

                        int courseStatusId = 0;
                        CourseStatus courseStatus = CourseStatusManager.GetByCode("W");
                        if (courseStatus != null)
                            courseStatusId = courseStatus.CourseStatusID;

                        int columnNo = 0;
                        int columnW = 0;
                        int noOfStudent = 0;
                        if (flag == 'N')
                        {
                            columnNo = 14;
                            columnW = 11;
                            noOfStudent = 17;
                        }
                        else if (flag == 'O')
                        {
                            columnNo = 15;
                            columnW = 12;
                            noOfStudent = 18;
                        }

                        var stdList = dicStudentInfo.OrderBy(x => x.Key).ToList();
                        int index = 0;
                        foreach (var temp in stdList)
                        {
                            index++;
                            //gradeSheet.Cell(7 + index, 1).Value = index.ToString();
                            gradeSheet.Cells[7 + index, 2].Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(temp.Value.ToString().ToLower());
                            gradeSheet.Cells[7 + index, 3].Value = temp.Key.ToString() + " ";
                            int wGrade = dicStudentInfoWgrade[temp.Key.ToString()];
                            if (wGrade == courseStatusId || wGrade == courseStatusId)
                            {
                                gradeSheet.Cells[7 + index, columnW].Value = "W";
                            }
                            gradeSheet.Cells[13, noOfStudent].Value = stdList.Count.ToString();
                        }

                        //Set Basic Setup
                        //gradeSheet.Cell(7, 15).Value = program.ShortName;
                        //if(program.DeptID == 12)
                        //    gradeSheet.Cell(2, 3).Value = "School of Business and Economics";
                        //else
                        //    gradeSheet.Cell(2, 3).Value = "School of Science and Engineering ";
                        string semester = string.Empty;
                        if (Convert.ToInt32(acaCal.Code) % 10 == 1)
                            semester += "Spring " + acaCal.Year;
                        else if (Convert.ToInt32(acaCal.Code) % 10 == 2)
                            semester += "Summer " + acaCal.Year;
                        else if (Convert.ToInt32(acaCal.Code) % 10 == 3)
                            semester += "Fall " + acaCal.Year;

                        //R//gradeSheet.Cell(4, columnNo).Value = semester;
                        gradeSheet.Cells[3, 1].Value = "Semester: " + semester;
                        //gradeSheet.Cell(3, 1).Value = "Semester: " + semester;

                        //gradeSheet.Cell(3, 3).Value = "Grade Sheet";
                        //R//gradeSheet.Cell(7, columnNo).Value = course.VersionCode; //Course Code
                        //R//gradeSheet.Cell(8, columnNo).Value = course.Title; //Course Title
                        gradeSheet.Cells[4, 1].Value = "Course Code: " + course.VersionCode + ", Course Title: " + course.Title;

                        //R//gradeSheet.Cell(9, columnNo).Value = acaCalSection.SectionName == null ? "" : acaCalSection.SectionName; //Section

                        string Faculty = person1 == null ? "" : (person1.FullName == null ? "" : person1.FullName);
                        if (acaCalSection.TeacherTwoID != 0)
                            Faculty += " / " + (person2 == null ? "" : (person2.FullName == null ? "" : person2.FullName));
                        //R//gradeSheet.Cell(10, columnNo).Value = Faculty;

                        string FacultyCode = emp1 == null ? "" : (emp1.Code == null ? "" : emp1.Code);
                        if (acaCalSection.TeacherTwoID != 0)
                            FacultyCode += " / " + (emp2 == null ? "" : (emp2.Code == null ? "" : emp2.Code));
                        //R//gradeSheet.Cell(11, columnNo).Value = FacultyCode;

                        string SectionFaculty = "Section: " + (acaCalSection.SectionName == null ? "" : acaCalSection.SectionName) + ", Faculty: " + (person1 == null ? "" : (person1.FullName == null ? "" : person1.FullName)) + " [" + (emp1 == null ? "" : (emp1.Code == null ? "" : emp1.Code)) + "]";
                        if (acaCalSection.TeacherTwoID != 0)
                            SectionFaculty += " / " + (person2 == null ? "" : (person2.FullName == null ? "" : person2.FullName)) + " [" + (emp2 == null ? "" : (emp2.Code == null ? "" : emp2.Code)) + "]";
                        gradeSheet.Cells[5, 1].Value = SectionFaculty;
                        //gradeSheet.Cell(5, 1).Value = "Section: " + (acaCalSection.SectionName == null ? "" : acaCalSection.SectionName) + ", Faculty: " + (person == null ? "" : (person.FirstName == null ? "" : person.FirstName)) + " [ " + "GetByPersonId" + " ]";

                        excelPackage.Save();
                        //lblMsg.Text = "Check D Drive. File Name- " + fileName;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            flagExcept = 1;
            lblMsg.Text = "Error: 101" + ex.Message;
        }
        finally
        {
            //if()
            if (flagExcept == 0)
            {
                DownloadFile(newFile);
                //lblMsg.Text = "Check D Drive. File Name- " + fileName;
            }
        }
    }

    void DownloadFile(FileInfo file)
    {
        if (file.Name.Length > 0)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
    }

    #endregion

    #region Event

    protected void ddlAcaCal_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    protected void ddlProgram_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    protected void btnGenerateGradeSheet_Click(Object sender, EventArgs e)
    {
        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        {
            lblMsg.Text = "Generate Grade Sheet: Please Select Dropdown Value First.";
            return;
        }
        else
        {
            AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(Convert.ToInt32(ddlAcaCalSection.SelectedValue));
            if (acaCalSec != null)
            {
                if (acaCalSec.TeacherOneID > 0 && acaCalSec.BasicExamTemplateId > 0)
                    GenerateGradeSheet(Convert.ToInt32(ddlAcaCalBatch.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlAcaCalSection.SelectedValue));
                else
                    lblMsg.Text = "First Complete Class Routine. Faculty or Templete Field are empty.";
            }
            else
                lblMsg.Text = "Not found: This Class Routine.";

        }
    }

    //protected void btnGenerateAllGradeSheet_Click(object sender, EventArgs e)
    //{
    //    ddlAcaCalSection.SelectedValue = "0";
    //    lblMsg.Text = "";

    //    if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0")
    //    {
    //        lblMsg.Text = "Generate Grade Sheet: Please Select Dropdown Value First.";
    //        return;
    //    }
    //    else
    //    {
    //        string pendingCourse = string.Empty, completeCourse = string.Empty;
    //        int batchId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
    //        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
    //        int acaCalSectionId = 0;
    //        int flag1 = 0;
    //        int flag2 = 0;
    //        for (int i = 0; i < ddlAcaCalSection.Items.Count; i++)
    //        {
    //            acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.Items[i].Value);

    //            if (acaCalSectionId != 0)
    //            {
    //                AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(acaCalSectionId);
    //                if (acaCalSec != null)
    //                {
    //                    if (acaCalSec.TeacherOneID > 0 && acaCalSec.GradeSheetTemplateID > 0)
    //                    {
    //                        string[] tempCourseCode1 = ddlAcaCalSection.Items[i].Text.ToString().Split(':');

    //                        GenerateGradeSheet(batchId, programId, acaCalSectionId);
    //                        if (lblMsg.Text != "" && lblMsg.Text != "Error: 101")
    //                        {
    //                            if (flag1 == 1)
    //                                completeCourse += ", " + tempCourseCode1[1];
    //                            else
    //                                completeCourse = tempCourseCode1[1];

    //                            flag1 = 1;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string[] tempCourseCode2 = ddlAcaCalSection.Items[i].Text.ToString().Split(':');
    //                        if (flag2 == 1)
    //                            pendingCourse += ", " + tempCourseCode2[1];
    //                        else
    //                            pendingCourse = tempCourseCode2[1];

    //                        flag2 = 1;
    //                    }

    //                }
    //                else
    //                    lblMsg.Text = "Error 102: Not found: This Class Routine.";
    //            }
    //        }
    //        if (flag1 == 1)
    //            lblMsg.Text = "<b>Check D Drive For Templete: </b>" + completeCourse + "<br />";
    //        if (flag2 == 1)
    //            lblMsg.Text += "<b>Faculty or Templete Field are empty in This Courses: </b>" + pendingCourse;
    //    }
    //}

    protected void btnImport_Click(Object sender, EventArgs e)
    {
        int flagExcept = 0;

        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        {
            lblMsg.Text = " <b>Import File -</b> Please Select Dropdown Value First.";
            return;
        }

        var path = "";
        try
        {
            if (fuGradeSheet.HasFile)
            {
                //List<GradeSheet> gradeSheetList = GradeSheetManager.GetAllByAcaCalSectionId(Convert.ToInt32(ddlAcaCalSection.SelectedValue));
                //if (gradeSheetList.Count > 0 && gradeSheetList != null)
                //{
                //    gradeSheetList = gradeSheetList.Where(x => x.AcademicCalenderID == Convert.ToInt32(ddlAcaCalBatch.SelectedValue)).ToList();
                //    if (gradeSheetList.Count > 0 && gradeSheetList != null)
                //    {
                //        lblMsg.Text = "<b><i>" + ddlAcaCalSection.SelectedItem.Text + "</i> is already Uploaded.</b>";
                //        return;
                //    }
                //}
                string[] fileNameExtention = fuGradeSheet.FileName.Split('.');
                if ((fileNameExtention[fileNameExtention.Length - 1]).ToLower() == "xlsx")
                {
                    path = Path.Combine(Server.MapPath("~/ExcelFiles"), fuGradeSheet.FileName);
                    flagExcept = 1;
                    fuGradeSheet.SaveAs(path);

                    string filePath = Server.MapPath("~/ExcelFiles/") + fuGradeSheet.FileName;

                    ReadExcelFile(filePath);
                }
                else
                {
                    lblMsg.Text = "<b>This System Support only Office 2007/2010 Excel Files Which Extension Is 'xlsx'</b>";
                }
            }
            else
            {
                lblMsg.Text = "Please <b>Choose File</b>.";
            }
        }
        catch { if (flagExcept == 1) File.Delete(path); }
        finally { if (flagExcept == 1) File.Delete(path); }
    }

    protected void btnViewGradeSheet_Click(object sender, EventArgs e)
    {
        //if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        //{
        //    lblMsg.Text = "View Grade Sheet: Please Select Dropdown Value First....";
        //    return;
        //}

        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            List<GradeSheet> gradeSheetList = GradeSheetManager.GetAllByAcaCalSectionId(acaCalSectionId);
            if (gradeSheetList.Count > 0 && gradeSheetList != null)
            {
                gradeSheetList = gradeSheetList.Where(x => x.IsFinalSubmit == false).ToList();
                if (gradeSheetList.Count > 0 && gradeSheetList != null)
                {
                    for (int i = 0; i < gradeSheetList.Count; i++)
                    {
                        Student student = StudentManager.GetById(gradeSheetList[i].StudentID);
                        Person person = PersonManager.GetById(student.PersonID);

                        gradeSheetList[i].StudentRoll = student.Roll;
                        gradeSheetList[i].StudentName = person.FullName;
                    }
                }
            }

            gvGradeSheet.DataSource = gradeSheetList;
            gvGradeSheet.DataBind();
        }
        catch { }
        finally { }
    }

    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int counter = 0;
            foreach (GridViewRow gridRow in gvGradeSheet.Rows)
            {
                HiddenField hField = (HiddenField)gridRow.FindControl("hfGradeSheetId");
                int gradeSheetId = Convert.ToInt32(hField.Value);
                GradeSheet gradeSheet = GradeSheetManager.GetById(gradeSheetId);
                gradeSheet.IsFinalSubmit = true;
                bool result = GradeSheetManager.Update(gradeSheet);
                if (result)
                    counter++;
            }
            lblMsg.Text = "Transferred To Register Office: " + counter;
            gvGradeSheet.DataSource = null;
            gvGradeSheet.DataBind();
        }
        catch { }
        finally { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        string searchKey = txtSearch.Text;

        if (acaCalId == 0 || programId == 0)
        {
            lblMsg.Text = "Please Select Batch and Program.";
            return;
        }

        FillAcaCalSectionCombo(acaCalId, programId, searchKey);
    }

    #endregion
}