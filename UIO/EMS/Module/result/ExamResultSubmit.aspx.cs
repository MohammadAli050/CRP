using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class ExamResultSubmit : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamMarkSubmit);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamMarkSubmit));

        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                LoadCourse(0, 0);
                //LoadAcaCalSection(0, 0, 0);
                LoadExam(0);
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

                    if (empObj != null && userRole.RoleName != "Admin" && userRole.RoleName!="DepControMarkEntry")
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
                }

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ToList();
                    foreach (AcademicCalenderSection acaCalSec in acaCalSectionList)
                    {
                        ddlCourse.Items.Add(new ListItem(acaCalSec.Course.Title + ":" + acaCalSec.Course.FormalCode + "(" + acaCalSec.SectionName + ")", acaCalSec.Course.CourseID + "_" + acaCalSec.Course.VersionID + "_" + acaCalSec.AcaCal_SectionID));
                    }
                }


                //List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);
                //offeredCourseList = offeredCourseList.Where(x => x.IsActive == true).ToList();
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
            int acaCalSection = Convert.ToInt32(courseVersion[2]);

            LoadExam(acaCalSection);
            //LoadAcaCalSection(courseId, versionId, acaCalId);
        }

        //protected void LoadAcaCalSection(int courseId, int versionId, int acaCalId)
        //{
        //    try
        //    {
        //        lblMsg.Text = string.Empty;
        //        ddlAcaCalSection.Items.Clear();
        //        ddlAcaCalSection.Items.Add(new ListItem("-Select Section-", "0"));
        //        ddlAcaCalSection.AppendDataBoundItems = true;

        //        List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
        //        if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
        //        {
        //            foreach (AcademicCalenderSection academicCalenderSection in academicCalenderSectionList)
        //                ddlAcaCalSection.Items.Add(new ListItem(academicCalenderSection.SectionName, Convert.ToString(academicCalenderSection.AcaCal_SectionID)));
        //        }
        //    }
        //    catch { }
        //    finally { }
        //}

        //protected void ddlAcaCalSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GvExamMarkSubmit.DataSource = null;
        //    GvExamMarkSubmit.DataBind();
        //    GvExamMarkSubmit.Visible = false;
        //    pnlTotalMark.Visible = false;
        //    int acaCalSection = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
        //    LoadExam(acaCalSection);
        //}

        protected void LoadExam(int acaCalSection)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("-Select Exam-", "0"));
                ddlExam.AppendDataBoundItems = true;
                AcademicCalenderSection acacalSectionObj = AcademicCalenderSectionManager.GetById(acaCalSection);
                if (acacalSectionObj != null)
                {
                    List<ExamTemplateBasicItemDetails> examList = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(acacalSectionObj.BasicExamTemplateId).ToList();
                    ddlExam.DataSource = examList;
                    ddlExam.DataValueField = "ExamTemplateBasicItemId";
                    ddlExam.DataTextField = "ExamTemplateBasicItemName";
                    ddlExam.DataBind();
                }
            }
            catch { }
            finally { }
        }

        protected void btnFinalSubmit_Clicked(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                string course = ddlCourse.SelectedValue;
                string[] courseVersion = course.Split('_');

                int courseId = Convert.ToInt32(courseVersion[0]);
                int versionId = Convert.ToInt32(courseVersion[1]);
                int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
                int examTemplateItemId = Convert.ToInt32(ddlExam.SelectedValue);
                List<ExamMarkNewDTO> studentList = ExamMarkDetailsManager.GetByExamMarkDtoByParameter(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateItemId);

                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                if (examTemplateItemId > 0)
                {
                    examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
                }
                
                foreach (ExamMarkNewDTO obj in studentList)
                {
                    string mark = GetStudentMark(obj.CourseHistoryId);
                    if (obj.ExamMarkDetailId > 0)
                    {
                        ExamMarkDetails examMarkDetailsObj = ExamMarkDetailsManager.GetById(obj.ExamMarkDetailId);
                        ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetById(examMarkDetailsObj.ExamMarkMasterId);
                        examMarkDetailsObj.IsFinalSubmit = true;
                        examMarkDetailsObj.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMasterObj.ExamMark) * (decimal)examMarkDetailsObj.Marks;

                        ExamMarkDetailsManager.Update(examMarkDetailsObj);
                    }
                    else
                    {
                        int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, Convert.ToDecimal(txtTotalMark.Text));
                        ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetById(examMarkMasterId);
                        ExamMarkDetails examMarkDetails = new ExamMarkDetails();
                        examMarkDetails.ExamMarkMasterId = examMarkMasterId;
                        examMarkDetails.CourseHistoryId = obj.CourseHistoryId;
                        if (!string.IsNullOrEmpty(mark))
                        {
                            examMarkDetails.Marks = Convert.ToDecimal(mark);
                            examMarkDetails.ExamMarkTypeId = 1;
                        }
                        else
                        {
                            examMarkDetails.ExamMarkTypeId = 2;
                            examMarkDetails.Marks = 0;
                        }
                        examMarkDetails.IsFinalSubmit = true;
                        examMarkDetails.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMasterObj.ExamMark) * (decimal)examMarkDetails.Marks;
                        examMarkDetails.ExamTemplateBasicItemId = examTemplateBasicItemDetailsObj.ExamTemplateBasicItemId;
                        examMarkDetails.CreatedBy = userObj.Id;
                        examMarkDetails.CreatedDate = DateTime.Now;
                        examMarkDetails.ModifiedBy = userObj.Id;
                        examMarkDetails.ModifiedDate = DateTime.Now;

                        ExamMarkDetailsManager.Insert(examMarkDetails);
                    }
                }
                lblMsg.Text = "Saved!";
                try
                {
                    Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                    AcademicCalenderSection acaCalSecObj = AcademicCalenderSectionManager.GetById(acaCalSectionId);

                    #region Log Insert
                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         BaseAcaCalCurrent.Code,
                                                         BaseAcaCalCurrent.FullCode,
                                                         BaseCurrentUserObj.LogInID,
                                                         "",
                                                         "",
                                                         "Mark Final Submit",
                                                         BaseCurrentUserObj.LogInID + " final sumbitted mark for Course : " + courseObj.Title + ", Section: " + acaCalSecObj.SectionName + ", Exam Name : " +ddlExam.SelectedItem.Text,
                                                         "normal",
                                                          ((int)CommonEnum.PageName.ExamMarkSubmit).ToString(),
                                                         CommonEnum.PageName.ExamMarkSubmit.ToString(),
                                                         _pageUrl,
                                                         "");
                    #endregion
                }
                catch { }
            }
            catch { }
        }

        protected void btnFinalSubmitAll_Clicked(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                string course = ddlCourse.SelectedValue;
                string[] courseVersion = course.Split('_');
                int courseId = Convert.ToInt32(courseVersion[0]);
                int versionId = Convert.ToInt32(courseVersion[1]);
                int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
                foreach (ListItem item in ddlExam.Items)
                {
                    int examTemplateItemId = Convert.ToInt32(item.Value);
                    if (examTemplateItemId > 0)
                    {
                        List<ExamMarkNewDTO> studentList = ExamMarkDetailsManager.GetByExamMarkDtoByParameter(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateItemId);

                        ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                        if (examTemplateItemId > 0)
                        {
                            examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
                        }

                        foreach (ExamMarkNewDTO obj in studentList)
                        {
                            if (obj.ExamMarkDetailId > 0)
                            {
                                ExamMarkDetails examMarkDetailsObj = ExamMarkDetailsManager.GetById(obj.ExamMarkDetailId);
                                int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark);
                                ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetById(examMarkMasterId);
                                examMarkDetailsObj.IsFinalSubmit = true;
                                examMarkDetailsObj.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMasterObj.ExamMark) * (decimal)examMarkDetailsObj.Marks;

                                ExamMarkDetailsManager.Update(examMarkDetailsObj);
                            }
                            else
                            {
                                int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark);
                                ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetById(examMarkMasterId);
                                ExamMarkDetails examMarkDetails = new ExamMarkDetails();
                                examMarkDetails.ExamMarkMasterId = examMarkMasterId;
                                examMarkDetails.CourseHistoryId = obj.CourseHistoryId;
                                examMarkDetails.Marks = 0;
                                examMarkDetails.ExamMarkTypeId = 2;
                                examMarkDetails.IsFinalSubmit = true;
                                examMarkDetails.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMasterObj.ExamMark) * (decimal)examMarkDetails.Marks;
                                examMarkDetails.ExamTemplateBasicItemId = examTemplateBasicItemDetailsObj.ExamTemplateBasicItemId;
                                examMarkDetails.CreatedBy = userObj.Id;
                                examMarkDetails.CreatedDate = DateTime.Now;
                                examMarkDetails.ModifiedBy = userObj.Id;
                                examMarkDetails.ModifiedDate = DateTime.Now;

                                ExamMarkDetailsManager.Insert(examMarkDetails);
                            }
                        }
                    }
                }
                ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = new ExamMarkPublishAcaCalSectionRelation();
                examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(acaCalSectionId);
                if (examMarkPublishObj != null)
                {
                    examMarkPublishObj.IsFinalSubmit = true;
                    examMarkPublishObj.FinalSubmitDate = DateTime.Now;
                    examMarkPublishObj.FinalSubmitBy = userObj.Id;
                    ExamMarkPublishAcaCalSectionRelationManager.Update(examMarkPublishObj);
                }
                else
                {
                    ExamMarkPublishAcaCalSectionRelation obj = new ExamMarkPublishAcaCalSectionRelation();
                    obj.AcacalSectionId = acaCalSectionId;
                    obj.IsFinalSubmit = true;
                    obj.FinalSubmitDate = DateTime.Now;
                    obj.FinalSubmitBy = userObj.Id;
                    ExamMarkPublishAcaCalSectionRelationManager.Insert(obj);
                }
                lblMsg.Text = "Saved!";
                try
                {
                    Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                    AcademicCalenderSection acaCalSecObj = AcademicCalenderSectionManager.GetById(acaCalSectionId);

                    #region Log Insert
                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         BaseAcaCalCurrent.Code,
                                                         BaseAcaCalCurrent.FullCode,
                                                         BaseCurrentUserObj.LogInID,
                                                         "",
                                                         "",
                                                         "Mark Final Submit All",
                                                         BaseCurrentUserObj.LogInID + " final sumbitted mark for Course : " + courseObj.Title + ", Section: " + acaCalSecObj.SectionName + " for all exams",
                                                         "normal",
                                                          ((int)CommonEnum.PageName.ExamMarkSubmit).ToString(),
                                                         CommonEnum.PageName.ExamMarkSubmit.ToString(),
                                                         _pageUrl,
                                                         "");
                    #endregion
                }
                catch { }
            }
            catch { }
        }
        protected void btnSaveTotalMark_click(object sender, EventArgs e)
        {
            int examTemplateItemId = Convert.ToInt32(lblExamTemplateBasicItemId.Text);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');
            int acaCalSecId = Convert.ToInt32(courseVersion[2]);
            ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(acaCalId, acaCalSecId, examTemplateItemId);

            if (examMarkMasterObj != null)
            {
                examMarkMasterObj.ExamMark = Convert.ToDecimal(txtTotalMark.Text);
                examMarkMasterObj.ExamDate = DateTime.ParseExact(txtExamDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                bool isUpdated = ExamMarkMasterManager.Update(examMarkMasterObj);
                if (isUpdated)
                    lblMsg.Text = "Saved successfully";
            }
            else
            {
                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                if (examTemplateItemId > 0)
                {
                    examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
                }
                int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, Convert.ToDecimal(txtTotalMark.Text));
                if(examMarkMasterId > 0)
                    lblMsg.Text = "Saved successfully";
            }

        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            GvExamMarkSubmit.DataSource = null;
            GvExamMarkSubmit.DataBind();
            pnlTotalMark.Visible = false;
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
            int examTemplateItemId = Convert.ToInt32(ddlExam.SelectedValue);

            ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
            if (examTemplateBasicItemDetailsObj != null)
            {
                lblTemplateMark.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark);
                ExamMarkMaster examMarkMasterObj = ExamMarkMasterManager.GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(sessionId, acaCalSectionId, examTemplateItemId);
                if (examMarkMasterObj != null)
                {
                    if (examMarkMasterObj.ExamDate != null)
                    {
                        DateTime dt = (DateTime)examMarkMasterObj.ExamDate;
                        txtExamDate.Text = dt.ToString("dd/MM/yyyy");
                    }
                    else txtExamDate.Text = "";
                    
                    txtTotalMark.Text = Convert.ToString(examMarkMasterObj.ExamMark);
                }
                else
                {
                    txtExamDate.Text = "";
                    txtTotalMark.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark);
                }
                lblExamTemplateBasicItemId.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemId);
                List<ExamMarkNewDTO> studentList = ExamMarkDetailsManager.GetByExamMarkDtoByParameter(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateItemId);
                if (studentList.Count > 0)
                {
                    studentList = studentList.Where(x => x.IsFinalSubmit != true).ToList();
                    if (studentList.Count > 0)
                    {
                        GvExamMarkSubmit.DataSource = studentList;
                        GvExamMarkSubmit.DataBind();
                        GvExamMarkSubmit.Visible = true;
                        pnlTotalMark.Visible = true;
                    }
                    else
                        lblMsg.Text = "Final submission done";
                }
                else
                    lblMsg.Text = "No data found";
            }
        }

        protected void GvExamMarkSubmit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ResultSubmit")
                {
                    lblMsg.Text = string.Empty;
                    int examTemplateItemId = Convert.ToInt32(lblExamTemplateBasicItemId.Text);
                    int studentCourseHistoryId = Convert.ToInt32(e.CommandArgument);
                    ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                    if (examTemplateItemId > 0)
                    {
                        examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
                    }

                    int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, Convert.ToDecimal(txtTotalMark.Text));
                    if (examMarkMasterId > 0)
                    {
                        InsertEditExamMarkDetails(studentCourseHistoryId, examTemplateBasicItemDetailsObj, examMarkMasterId);
                        try
                        {
                            StudentCourseHistory courseHistoryObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                                 DateTime.Now,
                                                                 BaseAcaCalCurrent.Code,
                                                                 BaseAcaCalCurrent.FullCode,
                                                                 BaseCurrentUserObj.LogInID,
                                                                 "",
                                                                 "",
                                                                 "Mark Submit",
                                                                 BaseCurrentUserObj.LogInID + " sumbitted mark for Roll: " + courseHistoryObj.Roll + ", Course : "+ courseHistoryObj.CourseTitle + ", Section: " + courseHistoryObj.SectionName,
                                                                 "normal",
                                                                  ((int)CommonEnum.PageName.ExamMarkSubmit).ToString(),
                                                                 CommonEnum.PageName.ExamMarkSubmit.ToString(),
                                                                 _pageUrl,
                                                                 courseHistoryObj.Roll);
                            #endregion
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private int InsertExamMarkMaster(ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj, decimal examMasterMark)
        {
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');
            if (examTemplateBasicItemDetailsObj != null && Convert.ToInt32(ucSession.selectedValue) > 0 && Convert.ToInt32(courseVersion[2]) > 0)
            {
                ExamMarkMaster examMarkMasterObj = new ExamMarkMaster();
                examMarkMasterObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                examMarkMasterObj.ExamMarkEntryDate = DateTime.Now;
                examMarkMasterObj.ExamMark = examMasterMark;
                if(string.IsNullOrEmpty(txtExamDate.Text))
                    examMarkMasterObj.ExamDate = DateTime.Now;
                else
                    examMarkMasterObj.ExamDate = DateTime.ParseExact(txtExamDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                examMarkMasterObj.ExamTemplateBasicItemId = examTemplateBasicItemDetailsObj.ExamTemplateBasicItemId;
                examMarkMasterObj.AcaCalSectionId = Convert.ToInt32(courseVersion[2]);
                examMarkMasterObj.CreatedBy = userObj.Id;
                examMarkMasterObj.CreatedDate = DateTime.Now;
                examMarkMasterObj.ModifiedBy = userObj.Id;
                examMarkMasterObj.ModifiedDate = DateTime.Now;
                ExamMarkMaster examMarkMasterNewObj = ExamMarkMasterManager.GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(examMarkMasterObj.AcaCalId, examMarkMasterObj.AcaCalSectionId, examMarkMasterObj.ExamTemplateBasicItemId);
                if (examMarkMasterNewObj == null)
                {
                    int result = ExamMarkMasterManager.Insert(examMarkMasterObj);
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return examMarkMasterNewObj.ExamMarkMasterId;
                }
            }
            else
            {
                return 0;
            }
        }
        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            TextBox txtMark = (TextBox)GvExamMarkSubmit.Rows[index].FindControl("txtMark");
            CheckBox cb1 = (CheckBox)GvExamMarkSubmit.Rows[index].FindControl("chkStatus");

            if (cb1.Checked)
            {
                txtMark.Text = "";
                txtMark.Enabled = false;
            }
            else
                txtMark.Enabled = true;

        }
        private bool InsertEditExamMarkDetails(int studentCourseHistoryId, ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj, int examMarkMasterId)
        {
            //bool result = false;
            ExamMarkMaster examMarkMaserObj = ExamMarkMasterManager.GetById(examMarkMasterId);
            string mark = GetStudentMark(studentCourseHistoryId);
            ExamMarkDetails examMarkDetails = new ExamMarkDetails();
            examMarkDetails.ExamMarkMasterId = examMarkMasterId;
            examMarkDetails.CourseHistoryId = studentCourseHistoryId;
            //examMarkDetails.ExamMarkTypeId = Convert.ToInt32(GetStudentStatus(studentCourseHistoryId));
            if (!string.IsNullOrEmpty(mark))
            {
                examMarkDetails.Marks = Convert.ToDecimal(mark);
                examMarkDetails.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMaserObj.ExamMark) * (decimal)examMarkDetails.Marks;
                examMarkDetails.ExamMarkTypeId = 1;
            }
            else
            {
                examMarkDetails.ExamMarkTypeId = 2;
                examMarkDetails.Marks = 0;
                examMarkDetails.ConvertedMark = 0;
            }
            examMarkDetails.ExamTemplateBasicItemId = Convert.ToInt32(ddlExam.SelectedValue);
            examMarkDetails.CreatedBy = userObj.Id;
            examMarkDetails.CreatedDate = DateTime.Now;
            examMarkDetails.ModifiedBy = userObj.Id;
            examMarkDetails.ModifiedDate = DateTime.Now;

            ExamMarkDetails examMarkDetailsObj = ExamMarkDetailsManager.GetByCourseHistoryIdExamTemplateItemId(examMarkDetails.CourseHistoryId, examMarkDetails.ExamTemplateBasicItemId);
            
                if (CheckMark(Convert.ToDecimal(examMarkDetails.Marks), Convert.ToDecimal(examMarkMaserObj.ExamMark)))
                {
                    if (examMarkDetailsObj != null)
                    {
                        if (examMarkDetailsObj.IsFinalSubmit == true)
                        {
                            lblMsg.Text = "You can't submit mark now. Final submit complete.";
                            return false;
                        }
                        examMarkDetailsObj.ExamMarkMasterId = examMarkMasterId;
                        if (!string.IsNullOrEmpty(mark))
                        {
                            examMarkDetailsObj.Marks = Convert.ToDecimal(mark);
                            examMarkDetailsObj.ConvertedMark = (examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark / examMarkMaserObj.ExamMark) * (decimal)examMarkDetailsObj.Marks;
                            examMarkDetailsObj.ExamMarkTypeId = 1;
                        }
                        else
                        {
                            examMarkDetailsObj.ExamMarkTypeId = 2;
                            examMarkDetailsObj.Marks = 0;
                        }
                        //examMarkDetailsObj.ExamMarkTypeId = Convert.ToInt32(GetStudentStatus(studentCourseHistoryId));
                        examMarkDetailsObj.ModifiedBy = userObj.Id;
                        examMarkDetailsObj.ModifiedDate = DateTime.Now;
                        bool result = ExamMarkDetailsManager.Update(examMarkDetailsObj);
                        if (result)
                        {
                            lblMsg.Text = "Student marks edited successfully.";
                            return true;
                        }
                        else
                        {
                            lblMsg.Text = "Student marks could not edited.";
                            return false;
                        }
                    }
                    else
                    {
                        int result = ExamMarkDetailsManager.Insert(examMarkDetails);
                        if (result > 0)
                        {
                            lblMsg.Text = "Student marks inserted successfully.";
                            return true;
                        }
                        else
                        {
                            lblMsg.Text = "Student marks could not inserted.";
                            return false;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Student marks could not be more then exam mark.";
                    return false;
                }
        }
        //private void GridRebind()
        //{
        //    //List<AttendanceStatus> attendanceStatusList = AttendanceDetailManager.GetAllAttendanceStatus();

        //    for (int i = 0; i < GvExamMarkSubmit.Rows.Count; i++)
        //    {
        //        GridViewRow row = GvExamMarkSubmit.Rows[i];
        //        Label lblExamMarkTypeId = (Label)row.FindControl("lblExamMarkTypeId");
        //        CheckBoxList chkExamSatausTypeList = (CheckBoxList)row.FindControl("chkExamSatausType");
        //        TextBox textBox = (TextBox)row.FindControl("txtMark");
        //        int status = Convert.ToInt32(lblExamMarkTypeId.Text);
        //        if (status == 1)
        //        {
        //            //textBox.Text = Convert.ToString(0);
        //            textBox.Enabled = true;
        //        }
        //        else if (status == 2)
        //        {
        //            chkExamSatausTypeList.SelectedValue = Convert.ToString(lblExamMarkTypeId.Text);
        //            textBox.Text = Convert.ToString(0);
        //            textBox.Enabled = false;
        //        }
        //    }
        //}

        private string GetStudentMark(int lblCourseHistoryId)
        {
            string studentMark = string.Empty;
            for (int i = 0; i < GvExamMarkSubmit.Rows.Count; i++)
            {
                GridViewRow row = GvExamMarkSubmit.Rows[i];
                Label courseHistoryId = (Label)row.FindControl("lblCourseHistoryId");
                TextBox mark = (TextBox)row.FindControl("txtMark");
                CheckBox cbStatus = (CheckBox)row.FindControl("chkStatus");
                if (Convert.ToString(mark) != string.Empty)
                {
                    if (Convert.ToInt32(courseHistoryId.Text) == lblCourseHistoryId)
                    {
                        if (cbStatus.Checked)
                            studentMark = "";
                        else
                            studentMark = mark.Text;
                        break;
                    }
                }

            }
            return studentMark;
        }

        //private int GetStudentStatus(int lblCourseHistoryId)
        //{
        //    int examMarkStatusId = 0;
        //    for (int i = 0; i < GvExamMarkSubmit.Rows.Count; i++)
        //    {
        //        GridViewRow row = GvExamMarkSubmit.Rows[i];
        //        Label courseHistoryId = (Label)row.FindControl("lblCourseHistoryId");
        //        CheckBox cbStatus = (CheckBox)row.FindControl("chkStatus");
        //        if (Convert.ToInt32(courseHistoryId.Text) == lblCourseHistoryId)
        //        {
        //            if (cbStatus.Checked)
        //                examMarkStatusId = 2;
        //            else
        //                examMarkStatusId = 1;
        //            break;
        //        }
        //    }
        //    return examMarkStatusId;
        //}

        private bool CheckMark(decimal studentMark, decimal examMark)
        {
            double stuMark = Convert.ToDouble(studentMark);
            double exmMark = Convert.ToDouble(examMark);

            if (stuMark > exmMark)
            {
                return false;
            }
            if (stuMark >= 0 && stuMark <= exmMark)
            {
                return true;
            }
            else { return false; }
        }

        protected void SubmitAllMarkButton_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                int examTemplateItemId = Convert.ToInt32(lblExamTemplateBasicItemId.Text);
                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                if (examTemplateItemId > 0)
                {
                    examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateItemId);
                }
                int examMarkMasterId = InsertExamMarkMaster(examTemplateBasicItemDetailsObj, Convert.ToDecimal(txtTotalMark.Text));
                
                foreach (GridViewRow row in GvExamMarkSubmit.Rows)
                {
                    Label courseHistoryId = (Label)row.FindControl("lblCourseHistoryId");
                    int studentCourseHistoryId = Convert.ToInt32(courseHistoryId.Text);
                    if (examMarkMasterId > 0)
                    {
                        InsertEditExamMarkDetails(studentCourseHistoryId, examTemplateBasicItemDetailsObj, examMarkMasterId);
                    }
                }
                try
                {
                    string course = ddlCourse.SelectedValue;
                    string[] courseVersion = course.Split('_');
                    int courseId = Convert.ToInt32(courseVersion[0]);
                    int versionId = Convert.ToInt32(courseVersion[1]);
                    int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
                    Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                    AcademicCalenderSection acaCalSecObj = AcademicCalenderSectionManager.GetById(acaCalSectionId);
                    #region Log Insert
                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         BaseAcaCalCurrent.Code,
                                                         BaseAcaCalCurrent.FullCode,
                                                         BaseCurrentUserObj.LogInID,
                                                         "",
                                                         "",
                                                         "Mark All Submit",
                                                         BaseCurrentUserObj.LogInID + " sumbitted all marks for Course : " + courseObj.Title + ", Section: " + acaCalSecObj.SectionName + ", Exam Name : " + ddlExam.SelectedItem.Text,
                                                         "normal",
                                                          ((int)CommonEnum.PageName.ExamMarkSubmit).ToString(),
                                                         CommonEnum.PageName.ExamMarkSubmit.ToString(),
                                                         _pageUrl,
                                                         "");
                    #endregion
                }
                catch { }
            }
            catch { }
        }
    }
}