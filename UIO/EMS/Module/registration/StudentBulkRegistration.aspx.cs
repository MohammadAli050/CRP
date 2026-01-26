using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentBulkRegistrationNew : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;
        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            if (ucProgram.selectedValue == "0")
            {
                lblMessage.Text = "Please select Program.";
                return;
            }
            else if (ucSession.selectedValue == "0")
            {
                lblMessage.Text = "Please select Session.";
                return;
            }
            else if (ucBatch.selectedValue == "0")
            {
                lblMessage.Text = "Please select batch.";
                return;
            }

            LoadGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadGrid()
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int sessionId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);

        List<StudentBulkRegistration> StudentCourseSectionList = RegistrationWorksheetManager.GetAllStudentWithAllCourseSectionByProgramSessionBatch(programId, sessionId, batchId);

        if (ddlGender.SelectedValue == "Male")
        {
            StudentCourseSectionList = StudentCourseSectionList.Where(r => r.Gender.ToLower() == "male").ToList();
        }
        else if (ddlGender.SelectedValue == "Female")
        {
            StudentCourseSectionList = StudentCourseSectionList.Where(r => r.Gender.ToLower() == "female").ToList();
        }


        if (StudentCourseSectionList != null)
        {
            gvStudentList.DataSource = StudentCourseSectionList.OrderBy(s => s.Roll).ToList();
            gvStudentList.DataBind();
        }
        else
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        lblCount.Text = StudentCourseSectionList.Count().ToString();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                if (ckBox.Checked)
                {

                    HiddenField HdnId = (HiddenField)row.FindControl("hdnId");
                    int StudentId = Convert.ToInt32(HdnId.Value);
                    Label lblRoll = (Label)row.FindControl("lblRoll");
                    string Roll = lblRoll.Text;

                    List<RegistrationWorksheet> list = RegistrationWorksheetManager.GetAllCourseByProgramSessionBatchStudentId(sessionId, StudentId);
                    StudentCourseHistory studentCourseHistory = new StudentCourseHistory();

                    if (list != null && list.Count > 0)
                    {
                        foreach (RegistrationWorksheet item in list)
                        {
                            studentCourseHistory = null;

                            if (!item.IsRegistered)
                            {
                                List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(StudentId, sessionId);

                                studentCourseHistory = studentCourseHistoryList.Find(o => o.CourseStatusID == (int)CommonEnum.CourseStatus.Rn &&
                                                                                          o.CourseID == item.CourseID &&
                                                                                          o.VersionID == item.VersionID);
                                if (studentCourseHistory == null)
                                {
                                    studentCourseHistory = new StudentCourseHistory();

                                    studentCourseHistory.StudentID = item.StudentID;
                                    studentCourseHistory.CourseStatusID = (int)CommonEnum.CourseStatus.Rn;
                                    studentCourseHistory.AcaCalID = item.OriginalCalID;
                                    studentCourseHistory.CourseID = item.CourseID;
                                    studentCourseHistory.VersionID = item.VersionID;
                                    studentCourseHistory.CourseCredit = item.Credits;
                                    studentCourseHistory.AcaCalSectionID = item.AcaCal_SectionID;
                                    studentCourseHistory.CourseStatusDate = DateTime.Now;
                                    studentCourseHistory.CreatedBy = userObj.Id;
                                    studentCourseHistory.CreatedDate = DateTime.Now;
                                    studentCourseHistory.ModifiedBy = userObj.Id;
                                    studentCourseHistory.ModifiedDate = DateTime.Now;

                                    int i = StudentCourseHistoryManager.Insert(studentCourseHistory);
                                    if (i > 0)
                                    {
                                        #region Log Insert

                                        LogGeneralManager.Insert(
                                                    DateTime.Now,
                                                    "",
                                                    "",
                                                    userObj.LogInID,
                                                    "",
                                                    "",
                                                    " Student course registration ",
                                                    userObj.LogInID + " course history added for " + item.CourseTitle + ", " + item.FormalCode + ", " + Roll,
                                                    userObj.LogInID + " course registration ",
                                                    ((int)CommonEnum.PageName.Registration).ToString(),
                                                    CommonEnum.PageName.Registration.ToString(),
                                                    _pageUrl,
                                                    Roll);
                                        #endregion
                                        studentCourseHistory.ID = i;

                                        //newStudentBillableCourseList.Add(studentCourseHistory);

                                        if (i > 0)
                                        {
                                            item.IsRegistered = true;
                                            bool update = RegistrationWorksheetManager.Update(item);
                                            if (update)
                                            {
                                                #region Log Insert

                                                LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            "",
                                                            "",
                                                            userObj.LogInID,
                                                            "",
                                                            "",
                                                            " Student course registration ",
                                                            userObj.LogInID + " course worksheet updated for " + item.CourseTitle + ", " + item.FormalCode + ", " + Roll,
                                                            userObj.LogInID + " course registration ",
                                                            ((int)CommonEnum.PageName.Registration).ToString(),
                                                            CommonEnum.PageName.Registration.ToString(),
                                                            _pageUrl,
                                                            Roll);
                                                #endregion
                                            }
                                            else
                                            {
                                                //ShowAlertMessage("Registration not completed! Please do it again.");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }


                }
            }



            LoadGrid();
        }
        catch (Exception)
        {
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("chkSelect");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }
}