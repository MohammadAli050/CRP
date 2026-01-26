using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;


namespace EMS.miu.registration
{
    public partial class BulkSectionAssignAfterReg : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        int userId = 0;
        User user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            user = UserManager.GetByLogInId(loginID);
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
                //else if (ucBatch.selectedValue == "0")
                //{
                //    lblMessage.Text = "Please select batch.";
                //    return;
                //}
                else if (ddlCourse.SelectedValue == "0_0")
                {
                    lblMessage.Text = "Please select Course.";
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
            string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            List<StudentCourseHistory> studentList = StudentCourseHistoryManager.GetAllRegisteredStudentByProgramSessionBatchCourse(programId, sessionId, Convert.ToInt32(ucBatch.selectedValue), Convert.ToInt32(courseAndVersion[0]), Convert.ToInt32(courseAndVersion[1]));
            if (ddlRoll.SelectedValue == "Odd")
            {
                studentList = studentList.Where(r => (Convert.ToInt32(r.Roll.Substring(r.Roll.Length - 1, 1)) % 2) == 1).ToList();
            }
            else if (ddlRoll.SelectedValue == "Even")
            {
                studentList = studentList.Where(r => (Convert.ToInt32(r.Roll.Substring(r.Roll.Length - 1, 1)) % 2) == 0).ToList();
            }


            if (studentList != null)
            {
                gvStudentList.DataSource = studentList.OrderBy(s => s.Roll).ToList();
                gvStudentList.DataBind();
            }
            else
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
            }

            lblCount.Text = studentList.Count().ToString();
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
                LoadCourseCombo();
                ClearGrid();
                ClearSectionDropdown();
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearSectionDropdown()
        {
            ddlSection.Items.Clear();
        }

        private void LoadCourseCombo()
        {
            List<Course> courseList = CourseManager.GetOfferedCourseByProgramSession(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucSession.selectedValue));
            if (courseList != null)
                courseList = courseList.OrderBy(x => x.FormalCode).ToList();
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("-Select-", "0_0"));
            ddlCourse.AppendDataBoundItems = true;
            if (courseList != null)
            {
                foreach (Course course in courseList)
                {
                    ddlCourse.Items.Add(new ListItem(course.CourseFullInfo.ToString(), course.CourseID + "_" + course.VersionID));
                }
            }
        }

        private void ClearGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        protected void btnSectionUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                if (ddlSection.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Section.";
                    return;
                }
                else if (ddlSection.SelectedValue == "-1")
                {
                    foreach (GridViewRow row in gvStudentList.Rows)
                    {
                        try
                        {
                            CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                            if (ckBox.Checked)
                            {
                                Label lblCourseHistoryId = (Label)row.FindControl("lblCourseHistoryId");
                                int courseHistoryId = Convert.ToInt32(lblCourseHistoryId.Text);

                                StudentCourseHistory courseHistoryObj = StudentCourseHistoryManager.GetById(courseHistoryId);
                                int sectionId = 0;
                                if(courseHistoryObj.AcaCalSectionID != null)
                                    sectionId = (int)courseHistoryObj.AcaCalSectionID;

                                courseHistoryObj.AcaCalSectionID = 0;


                                bool isTrue = StudentCourseHistoryManager.Update(courseHistoryObj);
                                try
                                {
                                    List<RegistrationWorksheet> rwList = RegistrationWorksheetManager.GetByStudentID(courseHistoryObj.StudentID);
                                    RegistrationWorksheet rw = rwList.Where(x => x.CourseID == courseHistoryObj.CourseID).FirstOrDefault();
                                    rw.AcaCal_SectionID = 0;
                                    RegistrationWorksheetManager.Update(rw);
                                }
                                catch { }
                                if (isTrue)
                                {
                                    AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(sectionId);
                                    if (academicCalenderSection.Occupied > 0)
                                    {
                                        academicCalenderSection.Occupied = (academicCalenderSection.Occupied - 1);
                                        academicCalenderSection.ModifiedDate = DateTime.Now;
                                        academicCalenderSection.ModifiedBy = -2;
                                        AcademicCalenderSectionManager.Update(academicCalenderSection);
                                    }
                                    try
                                    {
                                        #region Log Insert
                                        LogGeneralManager.Insert(
                                                    DateTime.Now,
                                                    "",
                                                    "",
                                                    user.LogInID,
                                                    "",
                                                    "",
                                                    " Student Section Change",
                                                    user.LogInID + " removed section in " + courseHistoryObj.CourseName + ", " + courseHistoryObj.FormalCode + ", " + " for "+  courseHistoryObj.Roll,
                                                    user.LogInID + " Student Section Change",
                                                    ((int)CommonEnum.PageName.SectionChangeAfterReg).ToString(),
                                                    CommonEnum.PageName.SectionChangeAfterReg.ToString(),
                                                    _pageUrl,
                                                    courseHistoryObj.Roll);
                                        #endregion
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(Convert.ToInt32(ddlSection.SelectedValue));

                    string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

                    foreach (GridViewRow row in gvStudentList.Rows)
                    {
                        try
                        {
                            CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                            if (ckBox.Checked)
                            {
                                Label lblCourseHistoryId = (Label)row.FindControl("lblCourseHistoryId");
                                int courseHistoryId = Convert.ToInt32(lblCourseHistoryId.Text);
                                //RegistrationWorksheet ws = RegistrationWorksheetManager.get
                                StudentCourseHistory courseHistoryObj = StudentCourseHistoryManager.GetById(courseHistoryId);
                                int sectionId = 0;
                                if (courseHistoryObj.AcaCalSectionID != null)
                                    sectionId = (int)courseHistoryObj.AcaCalSectionID;
                                courseHistoryObj.AcaCalSectionID = Convert.ToInt32(ddlSection.SelectedValue);

                                bool isTrue = StudentCourseHistoryManager.Update(courseHistoryObj);
                                try
                                {
                                    List<RegistrationWorksheet> rwList = RegistrationWorksheetManager.GetByStudentID(courseHistoryObj.StudentID);
                                    RegistrationWorksheet rw = rwList.Where(x => x.CourseID == courseHistoryObj.CourseID).FirstOrDefault();
                                    rw.AcaCal_SectionID = Convert.ToInt32(ddlSection.SelectedValue);
                                    RegistrationWorksheetManager.Update(rw);
                                }
                                catch { }
                                if (isTrue)
                                {
                                    academicCalenderSection.Occupied = (academicCalenderSection.Occupied + 1);
                                    academicCalenderSection.ModifiedDate = DateTime.Now;
                                    academicCalenderSection.ModifiedBy = -2;
                                    AcademicCalenderSectionManager.Update(academicCalenderSection);
                                    try
                                    {
                                        #region Log Insert
                                        LogGeneralManager.Insert(
                                                    DateTime.Now,
                                                    "",
                                                    "",
                                                    user.LogInID,
                                                    "",
                                                    "",
                                                    " Student Section Change",
                                                    user.LogInID + " changed section in " + courseHistoryObj.CourseName + ", " + courseHistoryObj.FormalCode + ", " + " for " + courseHistoryObj.Roll,
                                                    user.LogInID + " Student Section Change",
                                                    ((int)CommonEnum.PageName.SectionChangeAfterReg).ToString(),
                                                    CommonEnum.PageName.SectionChangeAfterReg.ToString(),
                                                    _pageUrl,
                                                    courseHistoryObj.Roll);
                                        #endregion
                                    }
                                    catch { }
                                }
                                if (sectionId > 0)
                                {
                                    AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(sectionId);
                                    if (acaCalSec.Occupied > 0)
                                    {
                                        acaCalSec.Occupied = (acaCalSec.Occupied - 1);
                                        acaCalSec.ModifiedDate = DateTime.Now;
                                        acaCalSec.ModifiedBy = -2;
                                        AcademicCalenderSectionManager.Update(acaCalSec);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }


                LoadGrid();
            }
            catch (Exception)
            {
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();
                LoadSectionCombo();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadSectionCombo()
        {
            // DropDownList ddlSection = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlSection");

            string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

            List<AcademicCalenderSection> list = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(Convert.ToInt32(ucSession.selectedValue),
                                                                                                         Convert.ToInt32(courseAndVersion[0]),
                                                                                                         Convert.ToInt32(courseAndVersion[1]));

            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("-Select-", "0"));
            ddlSection.AppendDataBoundItems = true;
            if (list != null)
            {
                foreach (AcademicCalenderSection item in list)
                {
                    ddlSection.Items.Add(new ListItem(item.SectionName.ToString(), item.AcaCal_SectionID.ToString()));
                }
            }
            ddlSection.Items.Add(new ListItem("-Remove Section-", "-1"));
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
}