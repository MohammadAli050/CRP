using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CourseDropApprove : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    #region Function
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
        }
    }
    
    protected decimal RegistrationCredit(int studentId, int acaCalId)
    {
        try
        {
            List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(studentId);
            if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
            {
                CourseStatus courseStatusR = CourseStatusManager.GetByCode("R");
                CourseStatus courseStatusDR = CourseStatusManager.GetByCode("DR");
                studentCourseHistoryList = studentCourseHistoryList.Where(x => x.AcaCalID == acaCalId && (x.CourseStatusID == courseStatusR.CourseStatusID || x.CourseStatusID == courseStatusDR.CourseStatusID)).ToList();
                if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                {
                    decimal totalCredit = 0;
                    foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                    {
                        totalCredit += studentCourseHistory.CourseCredit;
                    }
                    return totalCredit;
                }
            }
            return 0;
        }
        catch { return 0; }
    }

    protected string LastCGPA(int studentId)
    {
        try
        {
            StudentACUDetail studentACUDetail = StudentACUDetailManager.GetLatestCGPAByStudentId(studentId);
            if (studentACUDetail != null)
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetById(studentACUDetail.StdAcademicCalenderID);
                if (academicCalender != null)
                    return studentACUDetail.CGPA.ToString() + " [" + academicCalender.Code + "]";
                else
                    return studentACUDetail.CGPA.ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    private void ClearGrid()
    {
        gvCourseDrop.DataSource = null;
        gvCourseDrop.DataBind();
    }
    #endregion

    #region Event
    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    public void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            gvCourseDrop.DataSource = null;
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32( ucProgram.selectedValue);

            if (programId == 0)
            {
                lblMsg.Text = "Please select Program.";
                return;
            }
            else if (sessionId == 0)
            {
                lblMsg.Text = "Please select Session.";
                return;
            }

            List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByProgramSession(programId, sessionId);

            if (programId != 0)
            {
                studentCourseHistoryList = studentCourseHistoryList.Where(o => o.ProgramId == programId).ToList();
            }

            if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
            {
                CourseStatus courseStatus = CourseStatusManager.GetByCode(CommonEnum.CourseStatus.DR.ToString());
                studentCourseHistoryList = studentCourseHistoryList.Where(x => x.CourseStatusID == courseStatus.CourseStatusID).ToList();
                if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                {
                    foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                    {
                        Course course = CourseManager.GetByCourseIdVersionId(studentCourseHistory.CourseID, studentCourseHistory.VersionID);
                        Student student = StudentManager.GetById(studentCourseHistory.StudentID);
                        if (course != null)
                        {
                            studentCourseHistory.CourseCode = course.FormalCode;
                            studentCourseHistory.CourseName = course.Title;
                        }
                        if (student != null)
                        {
                            studentCourseHistory.StudentRoll = student.Roll;
                            Person person = PersonManager.GetById(student.PersonID);
                            if (person != null)
                                studentCourseHistory.StudentName = person.FullName;
                        }
                        studentCourseHistory.RegCredit = RegistrationCredit(studentCourseHistory.StudentID, sessionId).ToString();
                        studentCourseHistory.LastCGPA = LastCGPA(studentCourseHistory.StudentID);
                    }
                }
                gvCourseDrop.DataSource = studentCourseHistoryList;
                gvCourseDrop.DataBind();
            }
        }
        catch { }        
    }

    public void lbApproved_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            string remark = txtRemark.Text;

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(id);
            Course _Course = CourseManager.GetByCourseIdVersionId(studentCourseHistory.CourseID, studentCourseHistory.VersionID);
            string course = _Course.FormalCode + " " + _Course.Title;
            Student _student = StudentManager.GetById(studentCourseHistory.StudentID);
            CourseStatus courseStatus = CourseStatusManager.GetByCode(CommonEnum.CourseStatus.Dp.ToString());
            if (studentCourseHistory != null)
            {
                studentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                studentCourseHistory.AcaCalSectionID = 0;
                studentCourseHistory.ModifiedBy = 100;
                studentCourseHistory.ModifiedDate = DateTime.Now;
                studentCourseHistory.Remark = remark;

                bool updateResult = StudentCourseHistoryManager.Update(studentCourseHistory);

                if (updateResult)
                {
                    BillHistory studentBillHistoryObj = null; //BillHistoryManager.GetByStudentCourseHistoryId(studentCourseHistory.ID);
                    studentBillHistoryObj.Fees = 0;
                    studentBillHistoryObj.BillingDate = DateTime.Now;
                    studentBillHistoryObj.Remark = "Course dropped";
                    studentBillHistoryObj.ModifiedBy = userObj.Id;
                    studentBillHistoryObj.ModifiedDate = DateTime.Now;
                    bool updateBillHistoryResult = BillHistoryManager.Update(studentBillHistoryObj);
                    lblMsg.Text = "Approved Successfully";
                    //BillHistory studentBillHistoryNewObj= new BillHistory();
                    //studentBillHistoryNewObj.StudentCourseHistoryId = studentBillHistoryObj.StudentCourseHistoryId;
                    //studentBillHistoryNewObj.StudentId = studentBillHistoryObj.StudentId;
                    //studentBillHistoryNewObj.AcaCalId = studentBillHistoryObj.AcaCalId;
                    //studentBillHistoryNewObj.TypeDefinationId = studentBillHistoryObj.TypeDefinationId;
                    //studentBillHistoryNewObj.Fees = -(studentBillHistoryObj.Fees);
                    //studentBillHistoryNewObj.CreatedBy = -1;
                    //studentBillHistoryNewObj.CreatedDate = DateTime.Now;
                    //studentBillHistoryNewObj.ModifiedBy = -1;
                    //studentBillHistoryNewObj.ModifiedDate = DateTime.Now;

                    //int insertBillHistoryResult = BillHistoryManager.Insert(studentBillHistoryObj);
                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucSession.selectedText.ToString(),
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Course Drop Request Approve",
                                                         userObj.LogInID + " Approved Course Drop Request for Course " + course + " for semester " + ucSession.selectedText.ToString() + " Of " + ucProgram.selectedText.ToString() + " Program",
                                                         userObj.LogInID + " is Approved Course Drop Request",
                                                          ((int)CommonEnum.PageName.Admin_CourseDropApprove).ToString(),
                                                         CommonEnum.PageName.Admin_CourseDropApprove.ToString(),
                                                         _pageUrl,
                                                         _student.Roll);
                    if (updateBillHistoryResult)
                    {
                        btnLoad_Click(null, null);
                    }
                }
            }
        }
        catch { }
    }

    public void lbRejected_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            string remark = txtRemark.Text;

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(id);
            CourseStatus courseStatus = CourseStatusManager.GetByCode(CommonEnum.CourseStatus.Rn.ToString());
            Course _Course = CourseManager.GetByCourseIdVersionId(studentCourseHistory.CourseID, studentCourseHistory.VersionID);
            string course = _Course.FormalCode + " " + _Course.Title;
            Student _student = StudentManager.GetById(studentCourseHistory.StudentID);

            if (studentCourseHistory != null)
            {
                studentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                studentCourseHistory.ModifiedBy = 100;
                studentCourseHistory.ModifiedDate = DateTime.Now;
                studentCourseHistory.Remark = remark;

                bool updateResult = StudentCourseHistoryManager.Update(studentCourseHistory);

                if (updateResult)
                {
                    lblMsg.Text = "Reject Successfully";
                    LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        ucSession.selectedText.ToString(),
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        "Course Drop Request Reject",
                                                        userObj.LogInID + " Rejected Course Drop Request for Course " + course + " for semester " + ucSession.selectedText.ToString() + " Of " + ucProgram.selectedText.ToString() + " Program",
                                                        userObj.LogInID + " is Rejected Course Drop Request",
                                                         ((int)CommonEnum.PageName.Admin_CourseDropApprove).ToString(),
                                                        CommonEnum.PageName.Admin_CourseDropApprove.ToString(),
                                                        _pageUrl,
                                                        _student.Roll);
                    btnLoad_Click(null, null);
                }
            }
        }
        catch { }
    }
    #endregion
}