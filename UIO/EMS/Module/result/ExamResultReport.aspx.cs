using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_ExamResultReport : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            LoadCourse(0, 0);
            LoadAcaCalSection(0, 0, 0);
            lblMsg.Text = "";
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int sessionId = Convert.ToInt32(ucSession.selectedValue);
        LoadCourse(programId, sessionId);
    }

    protected void LoadCourse(int programId, int acaCalId)
    {
        try
        {
            lblMsg.Text = string.Empty;
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("-Select Course-", "0"));
            ddlCourse.AppendDataBoundItems = true;

            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalProgram(acaCalId, programId);

            User user = UserManager.GetByLogInId(userObj.LogInID);
            Role userRole = RoleManager.GetById(user.RoleID);
            if (user.Person != null)
            {
                Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                if (empObj != null && empObj.EmployeeTypeId == 1 && userRole.RoleName != "Admin" && userRole.RoleName != "DepControMarkEntry")
                {
                    acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                }
            }
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                var courseList = acaCalSectionList.Select(m => new { m.CourseID, m.VersionID }).Distinct().ToList();
                foreach (var c in courseList)
                {
                    Course courseObj = CourseManager.GetByCourseIdVersionId(c.CourseID, c.VersionID);
                    ddlCourse.Items.Add(new ListItem(courseObj.FormalCode + " " + courseObj.Title, c.CourseID + "_" + c.VersionID));
                }
            }

            //List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);

            //if (offeredCourseList.Count > 0 && offeredCourseList != null)
            //{
            //    foreach (OfferedCourse offeredCourse in offeredCourseList)
            //        ddlCourse.Items.Add(new ListItem(offeredCourse.CourseCode + " " + offeredCourse.CourseTitle, offeredCourse.CourseID + "_" + offeredCourse.VersionID));
            //}
        }
        catch { }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        string course = ddlCourse.SelectedValue;
        string[] courseVersion = course.Split('_');

        int courseId = Convert.ToInt32(courseVersion[0]);
        int versionId = Convert.ToInt32(courseVersion[1]);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        LoadAcaCalSection(courseId, versionId, acaCalId);
    }

    protected void LoadAcaCalSection(int courseId, int versionId, int acaCalId)
    {
        try
        {
            lblMsg.Text = string.Empty;
            ddlAcaCalSection.Items.Clear();
            ddlAcaCalSection.Items.Add(new ListItem("-Select Section-", "0"));
            ddlAcaCalSection.AppendDataBoundItems = true;

            List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
            if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
            {
                foreach (AcademicCalenderSection academicCalenderSection in academicCalenderSectionList)
                    ddlAcaCalSection.Items.Add(new ListItem(academicCalenderSection.SectionName, Convert.ToString(academicCalenderSection.AcaCal_SectionID)));
            }
        }
        catch { }
        finally { }
    }

    protected void ddlAcaCalSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
    }


    protected void ResultLoadButton_Click(object sender, EventArgs e)
    {
        string course = ddlCourse.SelectedValue;
        string[] courseVersion = course.Split('_');
        int courseId = Convert.ToInt32(courseVersion[0]);
        int versionId = Convert.ToInt32(courseVersion[1]);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);

        int acaCaSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
        if (acaCaSectionId> 0)
        {
            AcademicCalenderSection acaCalSectionObj = AcademicCalenderSectionManager.GetById(acaCaSectionId);
            if (acaCalSectionObj!= null) 
            {
                List<ExamTemplateBasicCalculativeItemDTO> examTemplateBasicCalculativeItemList = ExamTemplateMasterManager.ExamTemplateItemGetByAcaCalSectionId(acaCalSectionObj.AcaCal_SectionID).ToList();

                List<ExamMarkColumnWiseDTO> examMarkColumnWiseList = ExamTemplateMasterManager.GetStudentExamMarkColumnWise(courseId, versionId, acaCalId, acaCaSectionId);

                List<int> studentIdList = examMarkColumnWiseList.Select(d => d.StudentCourseHistoryId).Distinct().ToList();

                List<int> examSequenceList = examTemplateBasicCalculativeItemList.Select(d => d.ColumnSequence).Distinct().ToList();

                ExamMetaType examMetaTypeTypeObj = ExamMetaTypeManager.GetAll().Where(d => d.ExamMetaTypeName == "Final Exam").FirstOrDefault();

                DataTable table = new DataTable();
                table.Columns.Add("Student Name", typeof(string));
                table.Columns.Add("Roll", typeof(string));
                decimal final = 0;
                for (int j = 0; j < examTemplateBasicCalculativeItemList.Count; j++)
                {
                    if (examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName == "Final")
                    {
                        ExamTemplateBasicItemDetails examTemplateDetails = ExamTemplateBasicItemDetailsManager.GetById(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemId);
                        table.Columns.Add(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName + " " + examTemplateDetails.ExamTemplateBasicItemMark.ToString("#") + "%", typeof(string));
                        final = examTemplateDetails.ExamTemplateBasicItemMark;
                    }
                    else
                        table.Columns.Add(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName, typeof(string));
                }
                decimal incourseMark = 100 - final;
                if(final == 0)
                    table.Columns.Add("Total 100%", typeof(string));
                else
                    table.Columns.Add("Total ("+final.ToString("#")+"+"+incourseMark.ToString("#")+")%", typeof(string));
                table.Columns.Add("Grade Point", typeof(string));
                table.Columns.Add("Letter Grade", typeof(string));
                table.Columns.Add("Student Roll", typeof(string));
                table.Columns.Add("Incourse " + incourseMark.ToString("#")+"%", typeof(string));
                table.Columns.Add("Final Mark", typeof(string));

                for (int i = 0; i < studentIdList.Count; i++)
                {
                    int studentCourseHistoryId = Convert.ToInt32(studentIdList[i]);
                    int studentId = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId).Select(d => d.StudentId).FirstOrDefault();
                    LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                    List<GradeDetails> gradeDetailList = new List<GradeDetails>();
                    DataRow newRow;
                    if (studentObj != null)
                    {
                        gradeDetailList = GradeDetailsManager.GetByGradeMasterId(Convert.ToInt32(studentObj.GradeMasterId));
                    }

                    object[] rowArray = new object[examTemplateBasicCalculativeItemList.Count+8];
                    int newRowCounter = 0;
                    rowArray[0] = studentObj.Name;
                    rowArray[1] = studentObj.Roll;
                    newRowCounter = 1;
                    decimal totalMark = 0;
                    decimal basicExamMark = 0;
                    decimal finalWithoutConvertMark = 0;

                    for (int j = 0; j < examSequenceList.Count; j++)
                    {
                        decimal examMarks = 0;
                        int sequenceNo = Convert.ToInt32(examSequenceList[j]);
                        ExamTemplateBasicCalculativeItemDTO examItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ColumnSequence == sequenceNo).FirstOrDefault();
                        if (examItemObj != null)
                        {
                            if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Basic) //(int)CommonUtility.CommonEnum.ExamTemplateItemColumnType.Basic)
                            {
                                decimal studentExamMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.ConvertedMark).FirstOrDefault());
                                examMarks = studentExamMark;
                                examMarks = Math.Round(examMarks, 2);

                                ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType != 0).FirstOrDefault();
                                if (examCalculativeItemObj== null)
                                {
                                    totalMark = totalMark + examMarks;
                                    if (examItemObj.ExamMetaTypeId != 8 || !examItemObj.ExamTemplateBasicItemName.Contains("Final"))
                                    {
                                        basicExamMark = basicExamMark + examMarks;
                                    }
                                    else 
                                    {
                                        finalWithoutConvertMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.Marks).FirstOrDefault());
                                    }
                                }
                            }
                            else if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Calculative) 
                            {
                                if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Average) 
                                {
                                    decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark));
                                    int itemCount = Convert.ToInt32(examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 0).ToList().Count);
                                    examMarks = marks / itemCount;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 1).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne)
                                {
                                    decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Max(d => d.ConvertedMark));

                                    examMarks = marks ;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo)
                                {
                                    decimal[] markArray = new decimal []{};
                                    markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                    var maxArrayObj = markArray;
                                    var maxNumber = maxArrayObj.Max(z => z);
                                    var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();

                                    examMarks = (maxNumber + secondMax)/2;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree)
                                {
                                    decimal[] markArray = new decimal[] { };
                                    markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                    var maxArrayObj = markArray;
                                    var maxNumber = maxArrayObj.Max(z => z);
                                    var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();
                                    var thirdMax = maxArrayObj.OrderByDescending(z => z).Skip(2).First();

                                    examMarks = (maxNumber + secondMax + thirdMax) / 3;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Sum)
                                {
                                    decimal mark = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark);

                                    examMarks = mark;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                            }
                        }
                        rowArray[newRowCounter + 1] = examMarks;
                        newRowCounter = newRowCounter + 1;
                    }
                    totalMark = Math.Ceiling(totalMark);
                    rowArray[newRowCounter + 1] = totalMark;
                    newRowCounter = newRowCounter + 1;

                    decimal gradePoint = 0;
                    string gradeLetter = "Grading System Not Assigned";

                    if (gradeDetailList != null && gradeDetailList.Count > 0)
                    {
                        gradePoint = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradePoint;
                        gradeLetter = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().Grade;
                        //gradeId = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradeId;
                    }
                    if (examMetaTypeTypeObj != null)
                    {
                        ExamMarkColumnWiseDTO examMarkColumnWiseObj = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examMetaTypeTypeObj.ExamMetaTypeId).FirstOrDefault();
                        if (examMarkColumnWiseObj != null)
                        {
                            if (examMarkColumnWiseObj.ExamMarkTypeId == 2)
                            {
                                gradePoint = Convert.ToDecimal(0.00);
                                gradeLetter = "Absent";
                            }
                        }
                    }
                    
                    //if(){}

                    rowArray[newRowCounter + 1] = gradePoint;
                    newRowCounter = newRowCounter + 1;
                    rowArray[newRowCounter + 1] = gradeLetter;
                    newRowCounter = newRowCounter + 1;
                    rowArray[newRowCounter + 1] = studentObj.Roll;
                    newRowCounter = newRowCounter + 1;
                    rowArray[newRowCounter + 1] = basicExamMark;
                    newRowCounter = newRowCounter + 1;
                    rowArray[newRowCounter + 1] = finalWithoutConvertMark;
                    newRowCounter = newRowCounter + 1;
                    

                    newRow = table.NewRow();
                    newRow.ItemArray = rowArray;
                    table.Rows.Add(newRow);
                }
                GetResultFromTable(table);
                GridViewResultReport.DataSource = table;
                GridViewResultReport.DataBind();
                GridRebind();
            }
        }
    }

    private void GetResultFromTable(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++) 
        {
            for (int j = 2; j < dt.Columns.Count; j++)
            {
                ExamResultDTO examResultObj = new ExamResultDTO();
                examResultObj.StudentName = dt.Rows[i].ItemArray[0].ToString();
                examResultObj.Roll = dt.Rows[i].ItemArray[1].ToString();
                examResultObj.MarksOrGrade = dt.Rows[i].ItemArray[j].ToString();
            }
        }
    }

    private void GridRebind()
    {
        for (int i = 0; i < GridViewResultReport.Rows.Count; i++)
        {
            int rowCell = GridViewResultReport.Rows[0].Cells.Count;
            GridViewRow row = GridViewResultReport.Rows[i];
            string grade = GridViewResultReport.Rows[i].Cells[rowCell - 4].Text.ToString();

            if (grade == Convert.ToString("F") || grade == Convert.ToString("Absent"))
            {
                row.ForeColor = Color.White;
                row.BackColor = Color.Tomato;
            }
        }
    }

    //private bool CheckSequenceNo(int templateItemId)
    //{
    //    List<ExamMarkEquationColumnOrder> sequence = ExamMarkEquationColumnOrderManager.GetByTemplateItemId(templateItemId).ToList();
    //    if (sequence.Count == 0) { return false; }
    //    else { return true; }
    //}

    //private string GetStudentRoll(int studentId)
    //{
    //    try
    //    {
    //        string studentRoll = Convert.ToString(StudentManager.GetById(studentId).Roll);
    //        return studentRoll;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}