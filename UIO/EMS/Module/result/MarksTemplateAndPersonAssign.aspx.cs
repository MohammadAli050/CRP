using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.result
{
    public partial class MarksTemplateAndPersonAssign : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.MarksTemplateAndPersonAssign);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.MarksTemplateAndPersonAssign));
        int userId = 0;

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
                    btnCancel.Visible = false;
                    divUpdatePanel.Visible = false;
                    hdnSetupId.Value = "0";
                    LoadInstitute(UserObj);
                    LoadProgram(UserObj, 0);
                    ucSession.LoadDropDownList(0);
                    LoadExamTemplateDDL();
                    LoadEmployeeDDL();
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
        private void LoadExamTemplateDDL()
        {
            ddlExamTemplateName.DataSource = ExamTemplateMasterManager.GetAll().Where(d => d.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Basic).ToList();
            ddlExamTemplateName.DataTextField = "ExamTemplateMasterName";
            ddlExamTemplateName.DataValueField = "ExamTemplateMasterId";
            ddlExamTemplateName.DataBind();
            ListItem item = new ListItem("-Select Exam Template-", "0");
            ddlExamTemplateName.Items.Insert(0, item);
        }
        private void LoadEmployeeDDL()
        {
            try
            {
                var employeeList = EmployeeManager.GetAll();

                ddlUploaderOne.Items.Clear();
                ddlUploaderOne.AppendDataBoundItems = true;
                ddlUploaderOne.Items.Add(new ListItem("Select", "0"));

                ddlUploaderTwo.Items.Clear();
                ddlUploaderTwo.AppendDataBoundItems = true;
                ddlUploaderTwo.Items.Add(new ListItem("Select", "0"));

                if (employeeList != null && employeeList.Any())
                {
                    ddlUploaderOne.DataTextField = "CodeAndName";
                    ddlUploaderOne.DataValueField = "EmployeeID";
                    ddlUploaderOne.DataSource = employeeList.OrderBy(x => x.CodeAndName);
                    ddlUploaderOne.DataBind();

                    ddlUploaderTwo.DataTextField = "CodeAndName";
                    ddlUploaderTwo.DataValueField = "EmployeeID";
                    ddlUploaderTwo.DataSource = employeeList.OrderBy(x => x.CodeAndName);
                    ddlUploaderTwo.DataBind();
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
            LoadGridView();
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
            LoadAllCourse();
            LoadGridView();
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
            LoadGridView();
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
            LoadGridView();
        }
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }



        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        #region Function
        private void LoadAllCourse()
        {
            try
            {
                int programId = Convert.ToInt32(ddlProgram.SelectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int ExamCenterId = 0;

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("All Course", "0_0"));

                List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(programId, sessionId, batchId, ExamCenterId).OrderBy(d => d.FormalCode).ToList();

                if (courseList != null && courseList.Any())
                {
                    foreach (LogicLayer.BusinessObjects.Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.FormalCode + "-" + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }
        private void LoadGridView()
        {
            try
            {
                int CourseId = 0, VersionId = 0;
                btnCancel.Visible = false;
                string CourseNameNew = ddlCourse.SelectedValue;

                CourseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                VersionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                List<tempcoursemodel> tempcoursemodelList = new List<tempcoursemodel>();

                var emplist = ucamContext.Employees.ToList();
                var templist = ExamTemplateMasterManager.GetAll();
                var coursehistorylist = ucamContext.StudentCourseHistories.Where(x => x.AcaCalID == sessionId).ToList();

                if (CourseId != 0)
                {
                    var courseobj = ucamContext.Courses.FirstOrDefault(x => x.CourseID == CourseId && x.VersionID == VersionId);
                    if (courseobj != null)
                    {
                        #region Bind List

                        var tempmodel = new tempcoursemodel();
                        tempmodel.CourseId = courseobj.CourseID;
                        tempmodel.VersionId = courseobj.VersionID;
                        tempmodel.FormalCode = courseobj.FormalCode;
                        tempmodel.CourseTitle = courseobj.Title;
                        tempmodel.Credit = Convert.ToString(courseobj.Credits);

                        tempmodel.ExamTemplateMasterId = 0;
                        tempmodel.PersonOneId = 0;
                        tempmodel.PersonTwoId = 0;
                        tempmodel.SetupId = 0;
                        tempmodel.FinalSubmitted = "No";
                        tempmodel.Published = "No";
                        tempmodel.StudentCount = coursehistorylist.Count(x => x.CourseID == CourseId && x.VersionID == VersionId);

                        var tempassignobj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                        && x.VersionId == VersionId
                        && x.AcacalId == sessionId);
                        if (tempassignobj != null)
                        {
                            #region Template Info


                            int ExamTempateMasterId = tempassignobj.ExamTemplateMasterId.HasValue ? tempassignobj.ExamTemplateMasterId.Value : 0;

                            int persononeId = tempassignobj.MarksUploadByOne.HasValue ? tempassignobj.MarksUploadByOne.Value : 0;
                            int persontwoId = tempassignobj.MarksUploadByTwo.HasValue ? tempassignobj.MarksUploadByTwo.Value : 0;

                            tempmodel.SetupId = tempassignobj.Id;
                            tempmodel.ExamTemplateMasterId = ExamTempateMasterId;
                            var templateobj = templist.FirstOrDefault(x => x.ExamTemplateMasterId == ExamTempateMasterId);
                            if (templateobj != null)
                            {
                                tempmodel.TemplateName = templateobj.ExamTemplateMasterName;
                            }
                            var persononeobj = emplist.FirstOrDefault(x => x.EmployeeID == persononeId);
                            if (persononeobj != null)
                            {
                                tempmodel.AssignedPersonOne = persononeobj.Code;
                                tempmodel.PersonOneId = persononeobj.EmployeeID;
                            }
                            var persontwoobj = emplist.FirstOrDefault(x => x.EmployeeID == persontwoId);
                            if (persontwoobj != null)
                            {
                                tempmodel.AssignedPersonTwo = persontwoobj.Code;
                                tempmodel.PersonTwoId = persontwoobj.EmployeeID;
                            }

                            if (tempassignobj.FinalSubmitted != null && tempassignobj.FinalSubmitted != 1)
                            {
                                tempmodel.FinalSubmitted = "Yes";
                            }
                            if (tempassignobj.Published != null && tempassignobj.Published != 1)
                            {
                                tempmodel.Published = "Yes";
                            }

                            #endregion

                        }

                        tempcoursemodelList.Add(tempmodel);

                        #endregion

                    }
                }
                else
                {
                    foreach (ListItem item in ddlCourse.Items)
                    {
                        try
                        {
                            string courseInfo = item.Value;
                            CourseId = Convert.ToInt32(courseInfo.ToString().Split('_').First());
                            VersionId = Convert.ToInt32(courseInfo.ToString().Split('_').Last());
                            if (CourseId > 0)
                            {
                                var courseobj = ucamContext.Courses.FirstOrDefault(x => x.CourseID == CourseId && x.VersionID == VersionId);

                                #region Bind List

                                var tempmodel = new tempcoursemodel();
                                tempmodel.CourseId = courseobj.CourseID;
                                tempmodel.VersionId = courseobj.VersionID;
                                tempmodel.FormalCode = courseobj.FormalCode;
                                tempmodel.CourseTitle = courseobj.Title;
                                tempmodel.Credit = Convert.ToString(courseobj.Credits);

                                tempmodel.ExamTemplateMasterId = 0;
                                tempmodel.PersonOneId = 0;
                                tempmodel.PersonTwoId = 0;
                                tempmodel.SetupId = 0;
                                tempmodel.FinalSubmitted = "No";
                                tempmodel.Published = "No";
                                tempmodel.StudentCount = coursehistorylist.Count(x => x.CourseID == CourseId && x.VersionID == VersionId);


                                var tempassignobj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                                && x.VersionId == VersionId
                                && x.AcacalId == sessionId);
                                if (tempassignobj != null)
                                {
                                    #region Template Info

                                    int ExamTempateMasterId = tempassignobj.ExamTemplateMasterId.HasValue ? tempassignobj.ExamTemplateMasterId.Value : 0;

                                    int persononeId = tempassignobj.MarksUploadByOne.HasValue ? tempassignobj.MarksUploadByOne.Value : 0;
                                    int persontwoId = tempassignobj.MarksUploadByTwo.HasValue ? tempassignobj.MarksUploadByTwo.Value : 0;

                                    tempmodel.SetupId = tempassignobj.Id;
                                    tempmodel.ExamTemplateMasterId = ExamTempateMasterId;
                                    var templateobj = templist.FirstOrDefault(x => x.ExamTemplateMasterId == ExamTempateMasterId);
                                    if (templateobj != null)
                                    {
                                        tempmodel.TemplateName = templateobj.ExamTemplateMasterName;
                                    }
                                    var persononeobj = emplist.FirstOrDefault(x => x.EmployeeID == persononeId);
                                    if (persononeobj != null)
                                    {
                                        tempmodel.AssignedPersonOne = persononeobj.Code;
                                        tempmodel.PersonOneId = persononeobj.EmployeeID;
                                    }
                                    var persontwoobj = emplist.FirstOrDefault(x => x.EmployeeID == persontwoId);
                                    if (persontwoobj != null)
                                    {
                                        tempmodel.AssignedPersonTwo = persontwoobj.Code;
                                        tempmodel.PersonTwoId = persontwoobj.EmployeeID;
                                    }

                                    if (tempassignobj.FinalSubmitted != null && tempassignobj.FinalSubmitted != 1)
                                    {
                                        tempmodel.FinalSubmitted = "Yes";
                                    }
                                    if (tempassignobj.Published != null && tempassignobj.Published != 1)
                                    {
                                        tempmodel.Published = "Yes";
                                    }

                                    #endregion
                                }

                                tempcoursemodelList.Add(tempmodel);

                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                if (tempcoursemodelList != null && tempcoursemodelList.Any())
                {
                    gvCourseList.DataSource = tempcoursemodelList;
                    gvCourseList.DataBind();
                    divUpdatePanel.Visible = true;
                }
                else
                {
                    gvCourseList.DataSource = null;
                    gvCourseList.DataBind();
                }



            }
            catch (Exception)
            {
            }
        }

        #endregion

        protected void btnUpdateTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                int SetupId = Convert.ToInt32(hdnSetupId.Value);
                int ExamTemplateMasterId = Convert.ToInt32(ddlExamTemplateName.SelectedValue);

                hdnSetupId.Value = "0";

                if (SetupId > 0)
                {
                    var ExistingObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.Id == SetupId);
                    if (ExistingObj != null)
                    {
                        int UpdatedCount = UpdateTemplate(ExistingObj, ExamTemplateMasterId);
                        if (UpdatedCount > 0)
                        {
                            showAlert("Template updated successfully.");
                            LoadGridView();
                        }
                    }
                }
                else
                {
                    int courseSelectedCount = CountGridStudent();
                    if (courseSelectedCount == 0)
                    {
                        showAlert("Please select at least one course to update template.");
                    }
                    else
                    {
                        int InsertUpdateCount = 0;
                        for (int i = 0; i < gvCourseList.Rows.Count; i++)
                        {
                            GridViewRow row = gvCourseList.Rows[i];
                            CheckBox coursecheck = (CheckBox)row.FindControl("chkSelect");
                            if (coursecheck.Checked == true)
                            {
                                Label lblCourseId = (Label)row.FindControl("lblCourseId");
                                Label lblVersionId = (Label)row.FindControl("lblVersionId");

                                int courseId = lblCourseId != null ? Convert.ToInt32(lblCourseId.Text) : 0;
                                int versionId = lblVersionId != null ? Convert.ToInt32(lblVersionId.Text) : 0;

                                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                                var ExistingObj = ucamContext.MarksTemplateAndPersonAssigns.Where(x => x.CourseId == courseId
                                && x.VersionId == versionId && x.AcacalId == sessionId).FirstOrDefault();
                                if (ExistingObj != null)
                                {
                                    UpdateTemplate(ExistingObj, ExamTemplateMasterId);
                                    InsertUpdateCount++;
                                    continue;
                                }
                                else
                                {
                                    int insertId = InsertTemplate(sessionId, courseId, versionId, ExamTemplateMasterId);
                                    if (insertId > 0)
                                    {
                                        InsertUpdateCount++;
                                    }
                                }

                            }
                        }
                        if (InsertUpdateCount > 0)
                        {
                            ddlExamTemplateName.SelectedValue = "0";
                            LoadGridView();
                            showAlert("Template assigned successfully.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private int InsertTemplate(int sessionId, int courseId, int versionId, int examTemplateMasterId)
        {
            int Result = 0;
            try
            {
                var newObj = new UCAMDAL.MarksTemplateAndPersonAssign();
                newObj.AcacalId = sessionId;
                newObj.CourseId = courseId;
                newObj.VersionId = versionId;
                newObj.ExamTemplateMasterId = examTemplateMasterId;
                newObj.CreatedBy = UserObj.Id;
                newObj.CreatedDate = DateTime.Now;
                ucamContext.MarksTemplateAndPersonAssigns.Add(newObj);
                ucamContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Result;
        }
        private int UpdateTemplate(UCAMDAL.MarksTemplateAndPersonAssign existingObj, int examTemplateMasterId)
        {
            int result = 0;
            try
            {
                existingObj.ExamTemplateMasterId = examTemplateMasterId;
                existingObj.ModifiedBy = UserObj.Id;
                existingObj.ModifiedDate = DateTime.Now;
                result = 1;
                ucamContext.Entry(existingObj).State = EntityState.Modified;
                ucamContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return result;

        }

        protected void btnUpdatePerson_Click(object sender, EventArgs e)
        {
            try
            {
                int SetupId = Convert.ToInt32(hdnSetupId.Value);
                int uploaderOneId = Convert.ToInt32(ddlUploaderOne.SelectedValue);
                int uploaderTwoId = Convert.ToInt32(ddlUploaderTwo.SelectedValue);

                hdnSetupId.Value = "0";

                if (SetupId > 0)
                {
                    var ExistingObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.Id == SetupId);
                    if (ExistingObj != null)
                    {
                        int UpdatedCount = UpdateUploader(ExistingObj, uploaderOneId, uploaderTwoId);
                        if (UpdatedCount > 0)
                        {
                            showAlert("Marks uploader updated successfully.");
                            LoadGridView();
                        }
                    }
                }
                else
                {
                    int courseSelectedCount = CountGridStudent();
                    if (courseSelectedCount == 0)
                    {
                        showAlert("Please select at least one course to update marks uploader.");
                    }
                    else
                    {
                        int InsertUpdateCount = 0;
                        for (int i = 0; i < gvCourseList.Rows.Count; i++)
                        {
                            GridViewRow row = gvCourseList.Rows[i];
                            CheckBox coursecheck = (CheckBox)row.FindControl("chkSelect");
                            if (coursecheck.Checked == true)
                            {
                                Label lblCourseId = (Label)row.FindControl("lblCourseId");
                                Label lblVersionId = (Label)row.FindControl("lblVersionId");

                                int courseId = lblCourseId != null ? Convert.ToInt32(lblCourseId.Text) : 0;
                                int versionId = lblVersionId != null ? Convert.ToInt32(lblVersionId.Text) : 0;
                                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                                var ExistingObj = ucamContext.MarksTemplateAndPersonAssigns.Where(x => x.CourseId == courseId
                                && x.VersionId == versionId && x.AcacalId == sessionId).FirstOrDefault();
                                if (ExistingObj != null)
                                {
                                    UpdateUploader(ExistingObj, uploaderOneId, uploaderTwoId);
                                    InsertUpdateCount++;
                                    continue;
                                }
                                else
                                {
                                    int insertId = InsertTemplate(sessionId, courseId, versionId, uploaderOneId, uploaderTwoId);
                                    if (insertId > 0)
                                    {
                                        InsertUpdateCount++;
                                    }
                                }

                            }
                        }
                        if (InsertUpdateCount > 0)
                        {
                            ddlUploaderOne.SelectedValue = "0";
                            ddlUploaderTwo.SelectedValue = "0";
                            showAlert("Marks uploader assigned successfully.");
                            LoadGridView();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private int InsertTemplate(int sessionId, int courseId, int versionId, int uploaderOneId, int uploaderTwoId)
        {
            int Result = 0;
            try
            {
                var newObj = new UCAMDAL.MarksTemplateAndPersonAssign();
                newObj.AcacalId = sessionId;
                newObj.CourseId = courseId;
                newObj.VersionId = versionId;
                newObj.MarksUploadByOne = uploaderOneId;
                newObj.MarksUploadByTwo = uploaderTwoId;
                newObj.CreatedBy = UserObj.Id;
                newObj.CreatedDate = DateTime.Now;
                ucamContext.MarksTemplateAndPersonAssigns.Add(newObj);
                ucamContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return Result;
        }
        private int UpdateUploader(UCAMDAL.MarksTemplateAndPersonAssign existingObj, int uploaderOneId, int uploaderTwoId)
        {
            int result = 0;
            try
            {
                existingObj.MarksUploadByOne = uploaderOneId;
                existingObj.MarksUploadByTwo = uploaderTwoId;
                existingObj.ModifiedBy = UserObj.Id;
                existingObj.ModifiedDate = DateTime.Now;
                result = 1;
                ucamContext.Entry(existingObj).State = EntityState.Modified;
                ucamContext.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)btn.NamingContainer;
                int rowIndex = gvRow.RowIndex;
                Label lblSetupId = (Label)gvRow.FindControl("lblSetupId");

                int SetupId = lblSetupId != null ? Convert.ToInt32(lblSetupId.Text) : 0;
                if (SetupId > 0)
                {
                    btnCancel.Visible = true;
                    var existingObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.Id == SetupId);
                    if (existingObj != null)
                    {
                        hdnSetupId.Value = SetupId.ToString();
                        int examTemplateMasterId = existingObj.ExamTemplateMasterId.HasValue ? existingObj.ExamTemplateMasterId.Value : 0;
                        ddlExamTemplateName.SelectedValue = examTemplateMasterId.ToString();
                        int persononeId = existingObj.MarksUploadByOne.HasValue ? existingObj.MarksUploadByOne.Value : 0;
                        int persontwoId = existingObj.MarksUploadByTwo.HasValue ? existingObj.MarksUploadByTwo.Value : 0;
                        ddlUploaderOne.SelectedValue = persononeId.ToString();
                        ddlUploaderTwo.SelectedValue = persontwoId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlExamTemplateName.SelectedValue = "0";
            ddlUploaderOne.SelectedValue = "0";
            ddlUploaderTwo.SelectedValue = "0";
            btnCancel.Visible = false;

        }
        public class tempcoursemodel
        {
            public int SetupId { get; set; }
            public int CourseId { get; set; }
            public int VersionId { get; set; }
            public Nullable<int> ExamTemplateMasterId { get; set; }
            public string FormalCode { get; set; }
            public string CourseTitle { get; set; }
            public string Credit { get; set; }
            public string TemplateName { get; set; }
            public string AssignedPersonOne { get; set; }
            public string AssignedPersonTwo { get; set; }

            public int PersonOneId { get; set; }
            public int PersonTwoId { get; set; }

            public int StudentCount { get; set; }

            public string FinalSubmitted { get; set; }
            public string Published { get; set; }

        }
        private int CountGridStudent()
        {
            int courseCoutner = 0;
            for (int i = 0; i < gvCourseList.Rows.Count; i++)
            {
                GridViewRow row = gvCourseList.Rows[i];

                CheckBox coursecheck = (CheckBox)row.FindControl("chkSelect");


                if (coursecheck.Checked == true)
                {
                    courseCoutner++;
                }
            }
            return courseCoutner;
        }

    }
}