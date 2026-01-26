using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DegreeValidationByStudentID : BasePage
{
    List<CourseHistryForDegreeValidation> sessionDegreeValidationList = new List<CourseHistryForDegreeValidation>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMessage, Color.Red, Ex.Message);
        }
    }

    private void LoadStudentResult()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtStudentRoll.Text))
            {
                Student student = StudentManager.GetByRoll(Convert.ToString(txtStudentRoll.Text).Trim());

                if (student != null)
                {
                    List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);

                    lblMessage.Text = "";
                    Person person = PersonManager.GetById(student.PersonID);

                    if (person != null)
                    {
                        lblStudentName.Text = person.FullName;
                        lblStudentBatch.Text = student.Batch.Session.FullCode;
                        lblStudentProgram.Text = student.Program.ShortName;

                        lblCGPA.Text = student.CurrentSeccionResult == null ? "0" : (decimal.Round(student.CurrentSeccionResult.TranscriptCGPA, 2)).ToString();
                        lblMajor1.Text = student.Major1NodeName;
                        lblMajor2.Text = student.Major2NodeName == " -- " ? "" : student.Major2NodeName;

                        lblDegreeReq.Text = (decimal.Round(Convert.ToDecimal(student.Program.TotalCredit), 2)).ToString();

                        lblNewCGPA.Text = student.CurrentSeccionResult == null ? "0" : (decimal.Round(student.CurrentSeccionResult.TranscriptCGPA, 2)).ToString();
                        lblDegreeRequirement.Text = (decimal.Round(Convert.ToDecimal(student.Program.TotalCredit), 2)).ToString();

                        List<StudentCourseHistory> completeCr = studentCourseHistoryList.Where(x => x.IsConsiderGPA == true && x.ObtainedGrade != "F").ToList();

                        decimal completeCredit = StudentCourseHistoryManager.GetCompletedCreditByRoll(student.Roll);
                        lblCompletedCr.Text = completeCredit.ToString();

                        decimal attemptCredit = StudentCourseHistoryManager.GetAttemptedCreditByRoll(student.Roll);

                        lblAttemptedCr.Text = (decimal.Round(attemptCredit, 2)).ToString();

                        lblAttempted.Text = (decimal.Round(attemptCredit, 2)).ToString();
                        lblCreditHourCompleted.Text = completeCredit.ToString();
                    }
                    else
                    {
                        lblStudentName.Text = "-------";
                    }

                    List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByStudentId(student.StudentID);
                    if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                    {
                        studentACUDetailList = studentACUDetailList.OrderBy(x => x.StdAcademicCalenderID).ToList();
                        gvTrimesterWiseGPA.DataSource = studentACUDetailList;
                        gvTrimesterWiseGPA.DataBind();
                    }
                    else
                    {
                        gvTrimesterWiseGPA.DataSource = null;
                        gvTrimesterWiseGPA.DataBind();
                    }

                    #region Waiverred Course
                    List<StudentCourseHistory> waiverStudentCourseHistoryList = null;

                    Hashtable courseHashCourseCode = new Hashtable();
                    Hashtable courseHashCourseName = new Hashtable();

                    waiverStudentCourseHistoryList = studentCourseHistoryList.Where(x => x.CourseWavTransfrID != null && x.CourseWavTransfrID != 0).ToList();
                    lblCreditWaiver.Text = Convert.ToString(waiverStudentCourseHistoryList.Sum(d => d.CourseCredit));
                    if (waiverStudentCourseHistoryList.Count > 0 && waiverStudentCourseHistoryList != null)
                    {
                        foreach (StudentCourseHistory studentCourseHistory in waiverStudentCourseHistoryList)
                        {
                            if (courseHashCourseCode.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                                studentCourseHistory.CourseCode = courseHashCourseCode[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                            if (courseHashCourseName.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                                studentCourseHistory.CourseName = courseHashCourseName[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                        }

                        gvWaiVeredCourse.DataSource = waiverStudentCourseHistoryList;
                        gvWaiVeredCourse.DataBind();
                    }
                    else
                    {
                        gvWaiVeredCourse.DataSource = null;
                        gvWaiVeredCourse.DataBind();
                    }
                    #endregion

                    List<CourseHistryForDegreeValidation> list = StudentCourseHistoryManager.GetCourseHistoryByStudentIdForDegreeValidation(student.Roll);

                    if (list != null && list.Count != 0)
                    {
                        lblCompletdCourseCounter.Text = list.Count.ToString();

                        int currentAcaCalId = AcademicCalenderManager.GetIsCurrent(student.Program.CalenderUnitMasterID).AcademicCalenderID;
                        int CountCurrent = list.Where(d => d.AcaCalId == currentAcaCalId).Count();
                        int CountBefore = list.Count() - CountCurrent;

                        lblCompletedBefore.Text = CountBefore.ToString();
                        lblCompletedTrimester.Text = CountCurrent.ToString();

                        gvCourseHistory.DataSource = list.OrderBy(l => l.AcaCalId).ToList();
                        gvCourseHistory.DataBind();

                        List<String> typeNamelist = list.Select(d => d.CourseType).Distinct().ToList();
                        List<CourseTypeDegreeValidation> dvCourseTypelist = new List<CourseTypeDegreeValidation>();
                        for (int i = 0; i < typeNamelist.Count; i++)
                        {

                            CourseTypeDegreeValidation dvCourseType = new CourseTypeDegreeValidation();
                            dvCourseType.TypeName = list.Where(d => d.CourseType == typeNamelist[i]).FirstOrDefault().CourseType;
                            dvCourseType.Count = list.Where(d => d.CourseType == typeNamelist[i]).ToList().Count;
                            dvCourseType.Credits = list.Where(d => d.CourseType == typeNamelist[i]).ToList().Sum(d=> d.Credits);

                            dvCourseTypelist.Add(dvCourseType);

                        }
                        if (dvCourseTypelist != null)
                        {
                            gvTypeWiseCourse.DataSource = dvCourseTypelist;
                            gvTypeWiseCourse.DataBind();
                        }
                        else
                        {
                            gvTypeWiseCourse.DataSource = null;
                            gvTypeWiseCourse.DataBind();
                        }

                    }
                    else
                    {
                        gvCourseHistory.DataSource = null;
                        gvCourseHistory.DataBind();

                        gvTypeWiseCourse.DataSource = null;
                        gvTypeWiseCourse.DataBind();

                        lblCompletdCourseCounter.Text = "";
                        lblCompletedBefore.Text = "";
                        lblCompletedTrimester.Text = "";
                    }                    

                }
                else
                {
                    lblMessage.Text = "Student not found.";
                }
            }
            else
            {
                lblMessage.Text = "Please provide student ID.";
            }
        }
        catch (Exception)
        {
        }
    }

    private void ShowAlertMessage(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
    }

    protected void btnLoadStudentResult_Click(object sender, EventArgs e)
    {
        LoadStudentResult();
    }

    protected void gvCourseHistory_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            sessionDegreeValidationList = SessionManager.GetListFromSession<CourseHistryForDegreeValidation>("_degreeValidationList");

            string sortdirection = string.Empty;
            if (Session["direction"] != null)
            {
                if (Session["direction"].ToString() == "ASC")
                {
                    sortdirection = "DESC";
                }
                else
                {
                    sortdirection = "ASC";
                }
            }
            else
            {
                sortdirection = "DESC";
            }
            Session["direction"] = sortdirection;
            Sort(sessionDegreeValidationList, e.SortExpression.ToString(), sortdirection);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    private void Sort(List<CourseHistryForDegreeValidation> list, String sortBy, String sortDirection)
    {
        if (sortDirection == "ASC")
        {
            list.Sort(new GenericComparer<CourseHistryForDegreeValidation>(sortBy, (int)SortDirection.Ascending));
        }
        else
        {
            list.Sort(new GenericComparer<CourseHistryForDegreeValidation>(sortBy, (int)SortDirection.Descending));
        }
        gvCourseHistory.DataSource = list;
        gvCourseHistory.DataBind();
    }
}