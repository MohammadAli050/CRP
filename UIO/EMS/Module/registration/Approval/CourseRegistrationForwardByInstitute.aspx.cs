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
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Data;

namespace EMS.Module.Registration.Approval
{
    public partial class CourseRegistrationForwardByInstitute : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.CourseRegistrationForwardByInstitute);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.CourseRegistrationForwardByInstitute));

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
                    Session["maincourseList"] = null;
                    divGridView.Visible = false;
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

                if (TreeMasterId > 0)
                {
                    BindCourseList(ProgramId, AcacalId, TreeMasterId);
                }

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
                int ActivityTypeId = Convert.ToInt32(CommonEnum.ActivityType.CourseRegistrationForwardByInstitute);
                lblTimelinemsg.Text = "";
                if (ProgramId > 0 && AcacalId > 0)
                {
                    divTimeline.Visible = true;
                    btnRegistration.Visible = false;
                    if (DateTimeCheckingMethod.CheckDateTimeRange(AcacalId, ProgramId, ActivityTypeId) == false)
                    {
                        btnRegistration.Visible = true;
                    }

                    #region Show the date and time

                    List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(AcacalId, ProgramId, ActivityTypeId);
                    if (setUpDateForProgramList != null && setUpDateForProgramList.Count > 0)
                    {
                        SetUpDateForProgram setUpDateForProgram = setUpDateForProgramList.FirstOrDefault();
                        if (setUpDateForProgram != null)
                        {
                            string Status = setUpDateForProgram.IsActive == true ? "" : " And Status is Inactive";
                            lblTimelinemsg.Text = "Course registration forward by institute timeline is " + "From " + setUpDateForProgram.StartDate.ToString("dd-MMM-yyyy") + " " + Convert.ToDateTime(setUpDateForProgram.StartTime).ToString("hh:mm tt")
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

        private void BindCourseList(int programId, int acacalId, int TreeMasterId)
        {
            try
            {
                List<CourseListWithNode> courseListWithNodesList = new List<CourseListWithNode>();

                List<SqlParameter> parameters1 = new List<SqlParameter>();
                parameters1.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = programId });
                parameters1.Add(new SqlParameter { ParameterName = "@TreeMasterId", SqlDbType = System.Data.SqlDbType.Int, Value = TreeMasterId });

                DataTable dtCourselist = DataTableManager.GetDataFromQuery("GetCourseListFromAllSyllabusByProgram", parameters1);

                if (dtCourselist != null && dtCourselist.Rows.Count > 0)
                {
                    courseListWithNodesList = DataTableMethods.ConvertDataTable<CourseListWithNode>(dtCourselist);

                    gvCourseList.DataSource = courseListWithNodesList;
                    gvCourseList.DataBind();

                    Session["maincourseList"] = courseListWithNodesList;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private int BindingStudentList(int ProgramId, int BatchId, int StudentId, int acacalId)
        {
            int treeMasterId = 0;

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
                                             crs.Credits
                                         }).ToList();
                #region Concat all formal code by studentId and store in a list

                var groupedStudentCourses = studentCourseList
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

                                      where (student.ProgramID == ProgramId || ProgramId == 0)
                                         && (student.BatchId == BatchId || BatchId == 0)
                                         && (student.StudentID == StudentId || StudentId == 0)
                                      select new
                                      {
                                          student.StudentID,
                                          student.Roll,
                                          per.FullName,
                                          prog.ShortName,
                                          student.TreeMasterID
                                      }).ToList();
                #endregion

                #region Student With Course

                var studentList = (from student in rawStudentList
                                   join courseGroup in groupedStudentCourses
                                   on student.StudentID equals courseGroup.StudentId into courseGroupJoin
                                   from courseGroup in courseGroupJoin.DefaultIfEmpty()
                                   select new
                                   {
                                       student.StudentID,
                                       student.Roll,
                                       student.FullName,
                                       student.ShortName,
                                       student.TreeMasterID,
                                       FormalCodeList = courseGroup != null ? courseGroup.FormalCodeList : string.Empty
                                   }).ToList();
                #endregion

                if (studentList != null && studentList.Count > 0)
                {
                    divGridView.Visible = true;
                    GvStudent.DataSource = studentList;
                    GvStudent.DataBind();
                    treeMasterId = (int)studentList.Where(x => x.TreeMasterID > 0).FirstOrDefault().TreeMasterID;
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
            gvCourseList.DataSource = null;
            gvCourseList.DataBind();
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

                DataTable dtSelectedCourse = new DataTable();
                dtSelectedCourse.Columns.Add("CourseID", typeof(int));
                dtSelectedCourse.Columns.Add("VersionID", typeof(int));
                dtSelectedCourse.Columns.Add("YearNo", typeof(int));
                dtSelectedCourse.Columns.Add("SemesterNo", typeof(int));

                DataTable dtSelectedStudent = new DataTable();
                dtSelectedStudent.Columns.Add("StudentID", typeof(int));

                List<string> stdList = new List<string>();
                List<string> crsList = new List<string>();


                #region Selected Course List

                foreach (GridViewRow row in gvCourseList.Rows)
                {
                    try
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkCourseSelect");
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            Label lblCourseID = (Label)row.FindControl("lblCourseID");
                            Label lblVersionID = (Label)row.FindControl("lblVersionID");
                            TextBox txtYearNo = (TextBox)row.FindControl("txtYearNo");
                            TextBox txtSemesterNo = (TextBox)row.FindControl("txtSemesterNo");

                            int courseId = Convert.ToInt32(lblCourseID.Text);
                            int versionId = Convert.ToInt32(lblVersionID.Text);
                            int yearNo = Convert.ToInt32(txtYearNo.Text);
                            int semesterNo = Convert.ToInt32(txtSemesterNo.Text);

                            dtSelectedCourse.Rows.Add(courseId, versionId, yearNo, semesterNo);

                            Label lblFormalCode = (Label)row.FindControl("lblFormalCode");
                            if (lblFormalCode != null && lblFormalCode.Text != "")
                                crsList.Add(lblFormalCode.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        showAlert(ex.Message);
                        dtSelectedCourse = new DataTable();
                    }

                }
                #endregion

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



                if (dtSelectedCourse.Rows.Count == 0)
                {
                    showAlert("Please select at least one course");
                    return;
                }
                if (dtSelectedStudent.Rows.Count == 0)
                {
                    showAlert("Please select at least one student");
                    return;
                }

                string studentList = "", courseList = "";
                studentList = string.Join(", ", stdList);
                courseList = string.Join(", ", crsList);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                parameters.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                parameters.Add(new SqlParameter { ParameterName = "@InstituteId", SqlDbType = System.Data.SqlDbType.Int, Value = InstituteId });
                parameters.Add(new SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.Int, Value = UserId });
                parameters.Add(new SqlParameter { ParameterName = "@SelectedCourseList", SqlDbType = System.Data.SqlDbType.Structured, Value = dtSelectedCourse, TypeName = "dbo.SelectedCourseType" });
                parameters.Add(new SqlParameter { ParameterName = "@SelectedStudentList", SqlDbType = System.Data.SqlDbType.Structured, Value = dtSelectedStudent, TypeName = "dbo.SelectedStudentType" });

                DataTable dtUpdatedList = DataTableManager.GetDataFromQuery("SelectedCourseListForwardByInstitute", parameters);

                if (dtUpdatedList != null && dtUpdatedList.Rows.Count > 0)
                {
                    showAlert("Course forwarded by institute successfully");
                    btnLoad_Click(null, null);

                    #region Log Insert
                    MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Course Registration Forwarded By Institute"
                        , UserObj.LogInID + " forwarded course registration for the student : " + studentList + " with the courses : " + courseList +
                        " for session : " + ucSession.selectedText.ToString(), "", "", _pageId, _pageName, _pageUrl);

                    #endregion

                    return;
                }
                showAlert("Failed to forward course by institute");

            }
            catch (Exception ex)
            {
            }
        }

        public class CourseListWithNode
        {
            public int? ParentNodeID { get; set; }      // Nullable for leaf nodes
            public int? ChildNodeID { get; set; }       // Nullable for leaf nodes
            public int NodeID { get; set; }
            public string NodeName { get; set; }
            public int CourseID { get; set; }
            public int VersionID { get; set; }
            public int NodeCourseID { get; set; }
            public string FormalCode { get; set; }
            public string VersionCode { get; set; }
            public string CourseTitle { get; set; }
            public decimal Credit { get; set; }
            public int Priority { get; set; }
            public int SemesterNo { get; set; }
            public int YearNo { get; set; }
        }

    }
}