using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServayByStudent_EvaluationVarify : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            if (!IsPostBack)
            {
               
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            List<Student> studentList = new List<Student>();

            int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
            string batchCode = uclBatch.selectedValue.ToString();
            string programCode = uclProgram.selectedValue;

            //Start Course
            List<Course> courseList = CourseManager.GetAll();
            Hashtable hashCourse = new Hashtable();
            foreach (Course course in courseList)
                hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode);
            //End Course

            if (txtStudent.Text.Length == 12 && acaCalId != 0)
            {
                Student student = StudentManager.GetByRoll(txtStudent.Text);
                if (student != null)
                {
                    List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);
                    if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                        studentCourseHistoryList = studentCourseHistoryList.Where(x => x.AcaCalID == acaCalId).ToList();

                    if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                    {
                        List<EvaluationForm> evaluationFormList = EvaluationFormManager.GetAllByPersonId(Convert.ToInt32(student.StudentID));
                        if (evaluationFormList.Count > 0 && evaluationFormList != null)
                            evaluationFormList = evaluationFormList.Where(x => x.AcaCalId == acaCalId).ToList();

                        if (evaluationFormList.Count > 0 && evaluationFormList != null)
                        {
                            string completeEvaluation = string.Empty;
                            string pendingEvaluation = string.Empty;
                            foreach (EvaluationForm evaluation in evaluationFormList)
                            {
                                studentCourseHistoryList = studentCourseHistoryList.Where(x => x.AcaCalSectionID != evaluation.AcaCalSecId).ToList();
                                try
                                {
                                    string courseVersion = evaluation.AcaCalSec.CourseID.ToString() + "_" + evaluation.AcaCalSec.VersionID.ToString();
                                    completeEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                            {
                                try
                                {
                                    string courseVersion = studentCourseHistory.CourseID.ToString() + "_" + studentCourseHistory.VersionID.ToString();
                                    pendingEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            student.Attribute1 = completeEvaluation;
                            student.Attribute2 = pendingEvaluation;
                        }
                        else
                        {
                            string pendingEvaluation = string.Empty;
                            foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                            {
                                try
                                {
                                    string courseVersion = studentCourseHistory.CourseID.ToString() + "_" + studentCourseHistory.VersionID.ToString();
                                    pendingEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            student.Attribute2 = pendingEvaluation;
                        }
                    }

                    Person person = PersonManager.GetById(student.PersonID);
                    if (person != null)
                        student.FullName = person.FullName;

                    studentList.Add(student);
                    gvEvaluationVarify.DataSource = studentList;
                    gvEvaluationVarify.DataBind();
                }
            }
            else if (txtStudent.Text.Length > 0 && txtStudent.Text.Length < 9)
            {
                lblMsg.Text = "Invalid Student ID";
            }
            else if ((acaCalId == 0 || batchCode == "0" || programCode == "0") && txtStudent.Text.Length == 0)
            {
                string warning = string.Empty;
                if (acaCalId == 0)
                    warning += " - Semester";
                if (batchCode == "0")
                    warning += " - Batch";
                if (programCode == "0")
                    warning += "Program";

                lblMsg.Text = "Please select " + warning + " Dropdown List";
            }
            else if (txtStudent.Text.Length == 12 && (acaCalId == 0 || programCode == "0"))
            {
                lblMsg.Text = "Please select program & semester Dropdown List";
            }
            else
            {
                studentList = StudentManager.GetAllByBatchProgram(batchCode, programCode);
                studentList = studentList.OrderBy(x => x.Roll).ToList();
                foreach (Student student in studentList)
                {
                    List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);
                    if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                        studentCourseHistoryList = studentCourseHistoryList.Where(x => x.AcaCalID == acaCalId).ToList();

                    if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                    {
                        List<EvaluationForm> evaluationFormList = EvaluationFormManager.GetAllByPersonId(Convert.ToInt32(student.StudentID));
                        if (evaluationFormList.Count > 0 && evaluationFormList != null)
                            evaluationFormList = evaluationFormList.Where(x => x.AcaCalId == acaCalId).ToList();

                        if (evaluationFormList.Count > 0 && evaluationFormList != null)
                        {
                            string completeEvaluation = string.Empty;
                            string pendingEvaluation = string.Empty;
                            int flag = 0;
                            foreach (EvaluationForm evaluation in evaluationFormList)
                            {
                                studentCourseHistoryList = studentCourseHistoryList.Where(x => x.AcaCalSectionID != evaluation.AcaCalSecId).ToList();
                                try
                                {
                                    string courseVersion = evaluation.AcaCalSec.CourseID.ToString() + "_" + evaluation.AcaCalSec.VersionID.ToString();
                                    completeEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                            {
                                try
                                {
                                    string courseVersion = studentCourseHistory.CourseID.ToString() + "_" + studentCourseHistory.VersionID.ToString();
                                    pendingEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            student.Attribute1 = completeEvaluation;
                            student.Attribute2 = pendingEvaluation;
                        }
                        else
                        {
                            string pendingEvaluation = string.Empty;
                            foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                            {
                                try
                                {
                                    string courseVersion = studentCourseHistory.CourseID.ToString() + "_" + studentCourseHistory.VersionID.ToString();
                                    pendingEvaluation += "[" + hashCourse[courseVersion] + "] ";
                                }
                                catch { }
                            }
                            student.Attribute2 = pendingEvaluation;
                        }
                    }

                    Person person = PersonManager.GetById(student.PersonID);
                    if (person != null)
                        student.FullName = person.FullName;
                }
                gvEvaluationVarify.DataSource = studentList;
                gvEvaluationVarify.DataBind();
            }
        }
        catch { }
    }


    protected void uclProgram_Changed(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
        int programId = Convert.ToInt32(uclProgram.selectedValue);
        uclAcaCal.LoadDropDownList(programId);
        uclBatch.LoadDropDownList(programId);
    }
    #endregion
}