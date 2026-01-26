using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.Registration.Approval
{
    public partial class CourseRegistrationApprovedByCOE : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.CourseRegistrationApprovedByCOE);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.CourseRegistrationApprovedByCOE));

        BussinessObject.UIUMSUser UserObj = null;
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

                base.CheckPage_Load();
                if (!IsPostBack)
                {
                    divTimeline.Visible = false;
                    divGridView.Visible = false;
                    LoadStatus();
                    LoadInstitute(UserObj);
                    LoadProgram(UserObj, 0);
                    ucSession.LoadDropDownList(0);
                }
            }
            catch (Exception Ex)
            {
            }
        }

        #region On Load Methods
        private void LoadStatus()
        {
            try
            {
                var statusList = ucamContext.CourseRegistrationApprovalStatus.ToList();

                ddlStatus.Items.Clear();
                ddlStatus.AppendDataBoundItems = true;
                ddlStatus.Items.Add(new ListItem("Select", "0"));

                if (statusList != null && statusList.Any())
                {
                    ddlStatus.DataTextField = "StatusName";
                    ddlStatus.DataValueField = "StatusId";
                    ddlStatus.DataSource = statusList.OrderBy(x => x.StatusId);
                    ddlStatus.DataBind();

                    ddlStatus.SelectedValue = "1";
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void LoadInstitute(UIUMSUser userObj)
        {
            try
            {
                var InstituteList = new List<UCAMDAL.Institution>();
                var InstList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();
                ddlInstitute.Items.Clear();
                ddlInstitute.AppendDataBoundItems = true;
                ddlInstitute.Items.Add(new ListItem("Select", "0"));

                InstituteList = MisscellaneousCommonMethods.GetInstitutionsByUserId(userObj.Id);

                if (InstituteList != null && InstituteList.Any())
                {
                    ddlInstitute.DataTextField = "InstituteName";
                    ddlInstitute.DataValueField = "InstituteId";
                    ddlInstitute.DataSource = InstituteList.OrderBy(x => x.InstituteName);
                    ddlInstitute.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadProgram(UIUMSUser userObj, int InstituteId)
        {
            try
            {
                var InstituteList = new List<UCAMDAL.Institution>();
                var ProgramList = new List<UCAMDAL.Program>();

                ddlProgram.Items.Clear();
                ddlProgram.AppendDataBoundItems = true;
                ddlProgram.Items.Add(new ListItem("Select", "0"));

                ProgramList = MisscellaneousCommonMethods.GetProgramByUserIdAndInstituteId(userObj.Id, InstituteId);

                if (ProgramList != null && ProgramList.Any())
                {
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";
                    ddlProgram.DataSource = ProgramList.OrderBy(x => x.ShortName);
                    ddlProgram.DataBind();
                }


            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region On Change Methods

        protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProgram(UserObj, Convert.ToInt32(ddlInstitute.SelectedValue));
            ucSession.LoadDropDownList(0);
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            ucBatch.LoadDropDownList(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }

        #endregion

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearAll();

            try
            {
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int BatchId = Convert.ToInt32(ucBatch.selectedValue);
                int InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int StudentId = 0;

                int AcacalId = Convert.ToInt32(ucSession.selectedValue);

                #region Get Student ID

                if (!string.IsNullOrWhiteSpace(txtStudent.Text.Trim()))
                {
                    var student = ucamContext.Students.Where(x => x.Roll == txtStudent.Text.Trim()).FirstOrDefault();
                    if (student != null)
                    {
                        StudentId = student.StudentID;
                    }
                }

                #endregion

                if (ProgramId == 0 && StudentId == 0)
                {
                    showAlert("Please select a program or enter a student ID");
                    return;
                }
                if (AcacalId == 0)
                {
                    showAlert("Please select a session");
                    return;
                }

                int TreeMasterId = BindingStudentList(ProgramId, BatchId, StudentId, AcacalId);

                CheckTimeLine();
            }
            catch (Exception ex)
            {
            }

        }


        private void CheckTimeLine()
        {
            try
            {
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int AcacalId = Convert.ToInt32(ucSession.selectedValue);
                int ActivityTypeId = Convert.ToInt32(CommonEnum.ActivityType.CourseRegistrationApproveByInstitute);
                lblTimelinemsg.Text = "";
                if (ProgramId > 0 && AcacalId > 0)
                {
                    divTimeline.Visible = true;
                    btnCoeApproved.Visible = false;
                    if (DateTimeCheckingMethod.CheckDateTimeRange(AcacalId, ProgramId, ActivityTypeId) == false)
                    {
                        btnCoeApproved.Visible = true;
                    }

                    #region Show the date and time

                    List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(AcacalId, ProgramId, ActivityTypeId);
                    if (setUpDateForProgramList != null && setUpDateForProgramList.Count > 0)
                    {
                        SetUpDateForProgram setUpDateForProgram = setUpDateForProgramList.FirstOrDefault();
                        if (setUpDateForProgram != null)
                        {
                            string Status = setUpDateForProgram.IsActive == true ? "" : " And Status is Inactive";
                            lblTimelinemsg.Text = "Course registration approve by COE timeline is " + "From " + setUpDateForProgram.StartDate.ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(setUpDateForProgram.StartTime).ToString("hh:mm tt")
                                + " To " + setUpDateForProgram.EndDate.ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(setUpDateForProgram.EndTime).ToString("hh:mm tt") + Status;
                        }
                    }

                    #endregion

                }
                else
                {
                    divTimeline.Visible = false;
                }

            }
            catch (Exception ex)
            {
            }
        }
        private int BindingStudentList(int ProgramId, int BatchId, int StudentId, int acacalId)
        {
            int treeMasterId = 0;
            int StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            try
            {
                #region Already Forwarded Course List


                var studentCourseList = (from aph in ucamContext.CourseRegistrationApprovalHistories
                                         join crs in ucamContext.Courses
                                         on new { CourseId = aph.CourseId ?? 0, VersionId = aph.VersionId ?? 0 }
                                         equals new { CourseId = crs.CourseID, VersionId = crs.VersionID }
                                         where (aph.ProgramId == ProgramId && aph.AcacalId == acacalId
                                         && (aph.StudentId == StudentId || StudentId == 0))
                                         select new
                                         {
                                             aph.StudentId,
                                             crs.FormalCode,
                                             crs.VersionCode,
                                             crs.Credits,
                                             InstituteForwared = aph.InstituteForwardDate != null ? 1 : 0,
                                             COEApproved = aph.COEApprovedDate != null ? 1 : 0,
                                         }).ToList();
                #region Concat all formal code by studentId and store in a list

                var InstituteForwaredCourses = studentCourseList
                    .Where(x => x.InstituteForwared == 1 && x.COEApproved == 0)
                .GroupBy(x => x.StudentId)
                .Select(g => new
                {
                    StudentId = g.Key,
                    FormalCodeList = string.Join(", ", g.Select(x => x.FormalCode))
                })
                .ToList();


                var COEApprovedCourses = studentCourseList
                    .Where(x => x.COEApproved == 1)
                .GroupBy(x => x.StudentId)
                .Select(g => new
                {
                    StudentId = g.Key,
                    FormalCodeList = string.Join(", ", g.Select(x => x.FormalCode))
                })
                .ToList();


                #endregion


                #endregion

                #region Student List

                var rawStudentList = (from student in ucamContext.Students
                                      join per in ucamContext.People on student.PersonID equals per.PersonID
                                      join prog in ucamContext.Programs on student.ProgramID equals prog.ProgramID
                                      // Left join with student year semester history
                                      join aph in ucamContext.CourseRegistrationApprovalHistories
                                        on student.StudentID equals aph.StudentId
                                      where (student.ProgramID == ProgramId || ProgramId == 0)
                                         && (student.BatchId == BatchId || BatchId == 0)
                                         && (student.StudentID == StudentId || StudentId == 0)

                                         && (aph.AcacalId == acacalId)
                                         && (aph.ProgramId == ProgramId)
                                         && (aph.StudentId == StudentId || StudentId == 0)

                                         && (aph.ApplicationStatus == StatusId || StatusId == 0)

                                      select new
                                      {
                                          student.StudentID,
                                          student.Roll,
                                          per.FullName,
                                          prog.ShortName,
                                          student.TreeMasterID
                                      }).Distinct().ToList();
                #endregion

                #region Student With Course

                var studentList = (from student in rawStudentList
                                   join courseGroup in InstituteForwaredCourses
                                   on student.StudentID equals courseGroup.StudentId into courseGroupJoin
                                   from courseGroup in courseGroupJoin.DefaultIfEmpty()

                                   join COEcourseGroup in COEApprovedCourses
                                   on student.StudentID equals COEcourseGroup.StudentId into COEcourseGroupJoin
                                   from COEcourseGroup in COEcourseGroupJoin.DefaultIfEmpty()

                                   select new
                                   {
                                       student.StudentID,
                                       student.Roll,
                                       student.FullName,
                                       student.ShortName,
                                       student.TreeMasterID,
                                       FormalCodeList = courseGroup != null ? courseGroup.FormalCodeList : string.Empty,
                                       COEFormalCodeList = COEcourseGroup != null ? COEcourseGroup.FormalCodeList : string.Empty

                                   }).ToList();
                #endregion

                if (studentList != null && studentList.Count > 0)
                {
                    divGridView.Visible = true;
                    GvStudent.DataSource = studentList;
                    GvStudent.DataBind();
                }
                else
                {
                    showAlert("No student found!");
                }
            }
            catch (Exception ex)
            {
            }
            return treeMasterId;
        }

        private void ClearAll()
        {
            divTimeline.Visible = false;
            divGridView.Visible = false;
            GvStudent.DataSource = null;
            GvStudent.DataBind();
        }

        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }


        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int AcacalId = Convert.ToInt32(ucSession.selectedValue);
                int InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int UserId = UserObj.Id;



            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCoeApproved_Click(object sender, EventArgs e)
        {
            try
            {
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int AcacalId = Convert.ToInt32(ucSession.selectedValue);
                int InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int UserId = UserObj.Id;

                DataTable dtSelectedCourse = new DataTable();
                dtSelectedCourse.Columns.Add("CourseID", typeof(int));
                dtSelectedCourse.Columns.Add("VersionID", typeof(int));
                dtSelectedCourse.Columns.Add("YearNo", typeof(int));
                dtSelectedCourse.Columns.Add("SemesterNo", typeof(int));

                DataTable dtSelectedStudent = new DataTable();
                dtSelectedStudent.Columns.Add("StudentID", typeof(int));

                List<string> stdList = new List<string>();

                #region Selected Student List

                foreach (GridViewRow stdrow in GvStudent.Rows)
                {
                    CheckBox chkSelect = (CheckBox)stdrow.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        Label lblRoll = (Label)stdrow.FindControl("lblRoll");
                        int studentId = Convert.ToInt32(GvStudent.DataKeys[stdrow.RowIndex].Values["StudentID"]);
                        dtSelectedStudent.Rows.Add(studentId);

                        if (lblRoll != null && lblRoll.Text != "")
                            stdList.Add(lblRoll.Text);
                    }

                }


                #endregion


                if (dtSelectedStudent.Rows.Count == 0)
                {
                    showAlert("Please select at least one student");
                    return;
                }

                string studentList = "";
                studentList = string.Join(", ", stdList);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                parameters.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                parameters.Add(new SqlParameter { ParameterName = "@InstituteId", SqlDbType = System.Data.SqlDbType.Int, Value = InstituteId });
                parameters.Add(new SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.Int, Value = UserId });
                parameters.Add(new SqlParameter { ParameterName = "@SelectedStudentList", SqlDbType = System.Data.SqlDbType.Structured, Value = dtSelectedStudent, TypeName = "dbo.SelectedStudentType" });

                DataTable dtUpdatedList = DataTableManager.GetDataFromQuery("SelectedStudentListApprovedByCOE", parameters);

                if (dtUpdatedList != null && dtUpdatedList.Rows.Count > 0)
                {
                    showAlert("Student with courses approved by COE successfully");
                    btnLoad_Click(null, null);

                    #region Log Insert
                    MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Course Registration approved By COE"
                        , UserObj.LogInID + " approved course registration for the student : " + studentList +
                        " for session : " + ucSession.selectedText.ToString(), "", "", _pageId, _pageName, _pageUrl);

                    #endregion

                    return;
                }
                showAlert("Failed to approve course by COE");

            }
            catch (Exception ex)
            {
            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

            lblAlertMessage.Text = "If you reject now, the courses will be unregistered and any marks entered will be deleted. The institute status will stay as 'Forwarded'. Are you sure you want to go ahead?";

            ModalPopupConfirmationAlert.Show();
        }

        protected void btnRequestConfirm_Click(object sender, EventArgs e)
        {

            try
            {
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int AcacalId = Convert.ToInt32(ucSession.selectedValue);
                int InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int UserId = UserObj.Id;

                DataTable dtSelectedStudent = new DataTable();
                dtSelectedStudent.Columns.Add("StudentID", typeof(int));

                List<string> stdList = new List<string>();

                #region Selected Student List

                foreach (GridViewRow stdrow in GvStudent.Rows)
                {
                    CheckBox chkSelect = (CheckBox)stdrow.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        Label lblRoll = (Label)stdrow.FindControl("lblRoll");
                        int studentId = Convert.ToInt32(GvStudent.DataKeys[stdrow.RowIndex].Values["StudentID"]);
                        dtSelectedStudent.Rows.Add(studentId);

                        if (lblRoll != null && lblRoll.Text != "")
                            stdList.Add(lblRoll.Text);
                    }

                }


                #endregion


                if (dtSelectedStudent.Rows.Count == 0)
                {
                    showAlert("Please select at least one student");
                    return;
                }

                string studentList = "";
                studentList = string.Join(", ", stdList);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                parameters.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                parameters.Add(new SqlParameter { ParameterName = "@InstituteId", SqlDbType = System.Data.SqlDbType.Int, Value = InstituteId });
                parameters.Add(new SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.Int, Value = UserId });
                parameters.Add(new SqlParameter { ParameterName = "@SelectedStudentList", SqlDbType = System.Data.SqlDbType.Structured, Value = dtSelectedStudent, TypeName = "dbo.SelectedStudentType" });

                DataTable dtUpdatedList = DataTableManager.GetDataFromQuery("SelectedStudentListRejectedByCOE", parameters);

                if (dtUpdatedList != null && dtUpdatedList.Rows.Count > 0)
                {
                    showAlert("Student with courses Rejected by COE successfully");
                    btnLoad_Click(null, null);

                    #region Log Insert
                    MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Course Registration Rejected By COE"
                        , UserObj.LogInID + " rejected course registration for the student : " + studentList +
                        " for session : " + ucSession.selectedText.ToString(), "", "", _pageId, _pageName, _pageUrl);

                    #endregion

                    return;
                }
                showAlert("Failed to Reject course by COE");

            }
            catch (Exception ex)
            {
            }
        }
    }
}