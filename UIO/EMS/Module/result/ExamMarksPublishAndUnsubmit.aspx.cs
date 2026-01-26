using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.result
{
    public partial class ExamMarksPublishAndUnsubmit : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamMarksPublishAndUnsubmit);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamMarksPublishAndUnsubmit));
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
                    lblSummary.Text = "";
                    hdnFinalSubmitValue.Value = "0_0_0";
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
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
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

                List<LogicLayer.BusinessObjects.Course> courseList = new List<LogicLayer.BusinessObjects.Course>();

                List<LogicLayer.BusinessObjects.Course> allcourseList = CourseManager.GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(programId, sessionId, batchId, ExamCenterId).OrderBy(d => d.FormalCode).ToList();

                if (UserObj.RoleID == 1 || UserObj.RoleID == 2) // Admin or Controller    
                {
                    courseList = allcourseList;
                }
                else
                {
                    int EmployeeID = MisscellaneousCommonMethods.GetEmployeeId(UserObj.LogInID);

                    var assignedCourseList = ucamContext.MarksTemplateAndPersonAssigns
                        .Where(x => x.AcacalId == sessionId && (x.MarksUploadByOne == EmployeeID || x.MarksUploadByTwo == EmployeeID)).ToList();

                    if (assignedCourseList != null && assignedCourseList.Any())
                    {
                        foreach (var assignCourse in assignedCourseList)
                        {
                            var courseObj = allcourseList.FirstOrDefault(x => x.CourseID == assignCourse.CourseId && x.VersionID == assignCourse.VersionId);
                            if (courseObj != null)
                            {
                                courseList.Add(courseObj);
                            }
                        }
                    }
                    else
                    {
                        showAlert("No course assigned for you.");
                        return;
                    }

                }

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
                hdnFinalSubmitValue.Value = "0_0_0";
                int CourseId = 0, VersionId = 0;
                string CourseNameNew = ddlCourse.SelectedValue;

                CourseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                VersionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                int StatusId = Convert.ToInt32(ddlStatus.SelectedValue);

                List<tempcoursemodel> tempcoursemodelList = new List<tempcoursemodel>();

                var emplist = ucamContext.Employees.ToList();
                var templist = ExamTemplateMasterManager.GetAll();
                var coursehistorylist = ucamContext.StudentCourseHistories.Where(x => x.AcaCalID == sessionId).ToList();

                if (CourseId != 0)
                {
                    var courseobj = ucamContext.Courses.FirstOrDefault(x => x.CourseID == CourseId && x.VersionID == VersionId);
                    if (courseobj != null)
                    {
                        var ExamMarksMasterList = ucamContext.ExamMarkMasters.Where(x => x.CourseId == CourseId && x.VersionId == VersionId && x.AcaCalId == sessionId).ToList();

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
                                string TemplateName = templateobj.ExamTemplateMasterName;
                                var templateItemList = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(ExamTempateMasterId);

                                if (templateItemList != null && templateItemList.Any())
                                {
                                    foreach (var item in templateItemList)
                                    {
                                        TemplateName = TemplateName + "<br />" + item.ExamTemplateBasicItemName + "-" + item.ExamTemplateBasicItemMark + "-Converted : " + item.Attribute2;

                                        string MarksEntered = "No";
                                        if (ExamMarksMasterList != null && ExamMarksMasterList.Any())
                                        {
                                            var marksMaster = ExamMarksMasterList.Where(x => x.ExamTemplateBasicItemId == item.ExamTemplateBasicItemId).FirstOrDefault();
                                            if (marksMaster != null)
                                            {
                                                MarksEntered = "Yes";
                                            }
                                        }

                                        TemplateName = TemplateName + " (Marks Entered: " + MarksEntered + ") ";

                                    }
                                }

                                tempmodel.TemplateName = TemplateName;

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

                            if (tempassignobj.FinalSubmitted != null && tempassignobj.FinalSubmitted == 1)
                            {
                                tempmodel.FinalSubmitted = "Yes";
                            }
                            if (tempassignobj.Published != null && tempassignobj.Published == 1)
                            {
                                tempmodel.Published = "Yes";
                            }

                            #endregion

                        }

                        #region Status Wise Filtering

                        if (StatusId == -1)
                            tempcoursemodelList.Add(tempmodel);
                        else if (StatusId == 1 && tempmodel.FinalSubmitted == "Yes")
                            tempcoursemodelList.Add(tempmodel);
                        else if (StatusId == 2 && tempmodel.FinalSubmitted == "No")
                            tempcoursemodelList.Add(tempmodel);
                        else if (StatusId == 3 && tempmodel.Published == "Yes")
                            tempcoursemodelList.Add(tempmodel);
                        else if (StatusId == 4 && tempmodel.Published == "No")
                            tempcoursemodelList.Add(tempmodel);
                        #endregion

                        lblSummary.Text = "Total Course : " + tempcoursemodelList.Count() + ", Submitted : " + tempcoursemodelList.Where(x => x.FinalSubmitted == "Yes" && x.Published == "No").Count()
                            + ", Not Submitted : " + tempcoursemodelList.Where(x => x.FinalSubmitted == "No").Count()
                            + ", Published : " + tempcoursemodelList.Where(x => x.Published == "Yes").Count()
                            + ", Not Published : " + tempcoursemodelList.Where(x => x.Published == "No").Count();

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
                                var ExamMarksMasterList = ucamContext.ExamMarkMasters.Where(x => x.CourseId == CourseId && x.VersionId == VersionId && x.AcaCalId == sessionId).ToList();

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

                                        string TemplateName = templateobj.ExamTemplateMasterName;
                                        var templateItemList = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(ExamTempateMasterId);

                                        if (templateItemList != null && templateItemList.Any())
                                        {
                                            foreach (var itemdetails in templateItemList)
                                            {
                                                TemplateName = TemplateName + "<br />" + itemdetails.ExamTemplateBasicItemName + ":" + itemdetails.ExamTemplateBasicItemMark + ", Convert to : " + itemdetails.Attribute2;

                                                string MarksEntered = "No";
                                                if (ExamMarksMasterList != null && ExamMarksMasterList.Any())
                                                {
                                                    var marksMaster = ExamMarksMasterList.Where(x => x.ExamTemplateBasicItemId == itemdetails.ExamTemplateBasicItemId).FirstOrDefault();
                                                    if (marksMaster != null)
                                                    {
                                                        MarksEntered = "Yes";
                                                    }
                                                }

                                                TemplateName = TemplateName + " (Marks Entered: " + MarksEntered + ") ";
                                            }
                                        }

                                        tempmodel.TemplateName = TemplateName;
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

                                    if (tempassignobj.FinalSubmitted != null && tempassignobj.FinalSubmitted == 1)
                                    {
                                        tempmodel.FinalSubmitted = "Yes";
                                    }
                                    if (tempassignobj.Published != null && tempassignobj.Published == 1)
                                    {
                                        tempmodel.Published = "Yes";
                                    }

                                    #endregion
                                }
                                #region Status Wise Filtering

                                if (StatusId == -1)
                                    tempcoursemodelList.Add(tempmodel);
                                else if (StatusId == 1 && tempmodel.FinalSubmitted == "Yes")
                                    tempcoursemodelList.Add(tempmodel);
                                else if (StatusId == 2 && tempmodel.FinalSubmitted == "No")
                                    tempcoursemodelList.Add(tempmodel);
                                else if (StatusId == 3 && tempmodel.Published == "Yes")
                                    tempcoursemodelList.Add(tempmodel);
                                else if (StatusId == 4 && tempmodel.Published == "No")
                                    tempcoursemodelList.Add(tempmodel);
                                #endregion

                                lblSummary.Text = "Total Course : " + tempcoursemodelList.Count() + ", Submitted : " + tempcoursemodelList.Where(x => x.FinalSubmitted == "Yes" && x.Published == "No").Count()
                           + ", Not Submitted : " + tempcoursemodelList.Where(x => x.FinalSubmitted == "No").Count()
                           + ", Published : " + tempcoursemodelList.Where(x => x.Published == "Yes").Count()
                           + ", Not Published : " + tempcoursemodelList.Where(x => x.Published == "No").Count();

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

        #region Final Submit Course

        protected void btnsubmitConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                #region Getting Variable

                string HiddenValue = hdnFinalSubmitValue.Value;
                int CourseId = 0, VersionId = 0, AcacalId = 0;
                if (!string.IsNullOrEmpty(HiddenValue))
                {
                    string[] ids = HiddenValue.Split('_');
                    if (ids.Length == 3)
                    {
                        CourseId = Convert.ToInt32(ids[0]);
                        VersionId = Convert.ToInt32(ids[1]);
                        AcacalId = Convert.ToInt32(ids[2]);
                    }
                }

                if (CourseId == 0 || VersionId == 0 || AcacalId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }

                #endregion


                hdnFinalSubmitValue.Value = "0_0_0";

                var SubmitObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                    && x.VersionId == VersionId && x.AcacalId == AcacalId);

                if (SubmitObj != null)
                {
                    if (SubmitObj.FinalSubmitted == null || SubmitObj.FinalSubmitted == 0)
                    {
                        var course = ucamContext.Courses.Where(x => x.CourseID == CourseId && x.VersionID == VersionId).FirstOrDefault();

                        #region Final Submit Process


                        var ExamMarkMasterList = ucamContext.ExamMarkMasters.Where(x => x.CourseId == CourseId
                        && x.VersionId == VersionId && x.AcaCalId == AcacalId).ToList();

                        int FinalSubmitCount = 0;

                        var ExamtemlplateItemList = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId((int)SubmitObj.ExamTemplateMasterId);

                        foreach (var item in ExamtemlplateItemList)
                        {
                            try
                            {
                                var MarkmasterObj = ExamMarkMasterList.Where(x => x.ExamTemplateBasicItemId == item.ExamTemplateBasicItemId).FirstOrDefault();

                                if (MarkmasterObj != null)
                                {
                                    List<SqlParameter> parameters1 = new List<SqlParameter>();
                                    parameters1.Add(new SqlParameter { ParameterName = "@ExamMarksMasterId", SqlDbType = System.Data.SqlDbType.Int, Value = MarkmasterObj.ExamMarkMasterId });
                                    parameters1.Add(new SqlParameter { ParameterName = "@ModifiedBy", SqlDbType = System.Data.SqlDbType.Int, Value = UserObj.Id });

                                    DataTable dtSubmittedMarks = DataTableManager.GetDataFromQuery("ExamMarksFinalSubmittedByMarksMasterId", parameters1);

                                    if (dtSubmittedMarks != null && dtSubmittedMarks.Rows.Count > 0)
                                        FinalSubmitCount++;
                                    else
                                    {
                                        showAlert("You need to enter " + item.ExamTemplateBasicItemName + " marks before final submission.");
                                        return;
                                    }

                                }
                                else
                                {
                                    showAlert("You need to enter " + item.ExamTemplateBasicItemName + " marks before final submission.");
                                    return;
                                }



                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        if (FinalSubmitCount > 0)
                        {

                            SubmitObj.FinalSubmitted = 1;
                            SubmitObj.FinalSubmittedDate = DateTime.Now;
                            SubmitObj.ModifiedBy = UserObj.Id;
                            SubmitObj.ModifiedDate = DateTime.Now;
                            ucamContext.Entry(SubmitObj).State = EntityState.Modified;
                            ucamContext.SaveChanges();
                            showAlert("Marks for the course have been successfully finalized and submitted.");
                            LoadGridView();

                            try
                            {
                                MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Exam marks submit", UserObj.LogInID + " submitted a course marks" +
                                    ".Course information : " + course.FormalCode + "_" + course.Title, "", course.FormalCode, _pageId, _pageName, _pageUrl);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            showAlert("No marks were finalized. Please ensure marks have been uploaded before final submission.");
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        showAlert("Marks for this course have already been submitted.");
                        return;
                    }
                }
                else
                {
                    showAlert("Marks template not assigned for this course.");
                    return;
                }

            }
            catch (Exception ex)
            {
            }

        }

        #endregion

        #region Temp Model
        public class StudentMarkUploadModel
        {
            public int SL { get; set; }
            public int HistoryId { get; set; }
            public int TemplateId { get; set; }
            public string TemplateName { get; set; }
            public string TemplateMarks { get; set; }
            public string StudentName { get; set; }
            public string StudentRoll { get; set; }
            public string Marks { get; set; }
            public int PresentStatus { get; set; }
            public string Status { get; set; }
            public int ValidMarks { get; set; }

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
        public class TemplateItemModel
        {
            public int TemplateId { get; set; }
            public string TemplateName { get; set; }
            public string TemplateMarks { get; set; }
            public int ColumnNo { get; set; }
        }



        #endregion

        protected void gvCourseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var dataItem = (tempcoursemodel)e.Row.DataItem;
                    if (dataItem != null && dataItem.FinalSubmitted == "Yes" && dataItem.Published != "Yes")
                    {
                        e.Row.BackColor = System.Drawing.Color.Blue; // Or any color you prefer
                    }
                    if (dataItem != null && dataItem.Published == "Yes")
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen; // Or any color you prefer
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        protected void btnResultUnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region Getting Variable

                string HiddenValue = hdnFinalSubmitValue.Value;
                int CourseId = 0, VersionId = 0, AcacalId = 0;
                if (!string.IsNullOrEmpty(HiddenValue))
                {
                    string[] ids = HiddenValue.Split('_');
                    if (ids.Length == 3)
                    {
                        CourseId = Convert.ToInt32(ids[0]);
                        VersionId = Convert.ToInt32(ids[1]);
                        AcacalId = Convert.ToInt32(ids[2]);
                    }
                }

                if (CourseId == 0 || VersionId == 0 || AcacalId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }

                #endregion


                hdnFinalSubmitValue.Value = "0_0_0";

                var SubmitObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                    && x.VersionId == VersionId && x.AcacalId == AcacalId);

                if (SubmitObj != null)
                {
                    if (SubmitObj.FinalSubmitted != null && SubmitObj.FinalSubmitted == 1)
                    {
                        var course = ucamContext.Courses.Where(x => x.CourseID == CourseId && x.VersionID == VersionId).FirstOrDefault();

                        #region Un Submit Process

                        int FinalUnSubmitCount = 0;

                        List<SqlParameter> parameters1 = new List<SqlParameter>();
                        parameters1.Add(new SqlParameter { ParameterName = "@CourseId", SqlDbType = System.Data.SqlDbType.Int, Value = CourseId });
                        parameters1.Add(new SqlParameter { ParameterName = "@VersionId", SqlDbType = System.Data.SqlDbType.Int, Value = VersionId });
                        parameters1.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                        parameters1.Add(new SqlParameter { ParameterName = "@ModifiedBy", SqlDbType = System.Data.SqlDbType.Int, Value = UserObj.Id });

                        DataTable dtunSubmittedMarks = DataTableManager.GetDataFromQuery("ExamMarksUnSubmittedByCourseVersionSession", parameters1);

                        if (dtunSubmittedMarks != null && dtunSubmittedMarks.Rows.Count > 0)
                            FinalUnSubmitCount++;
                        else
                        {
                            showAlert("unsubmit failed for this course.");
                            return;
                        }

                        if (FinalUnSubmitCount > 0)
                        {

                            SubmitObj.FinalSubmitted = 0;
                            SubmitObj.Published = 0;
                            SubmitObj.ModifiedBy = UserObj.Id;
                            SubmitObj.ModifiedDate = DateTime.Now;
                            ucamContext.Entry(SubmitObj).State = EntityState.Modified;
                            ucamContext.SaveChanges();
                            showAlert("Marks for the course have been un-submitted.");
                            LoadGridView();

                            try
                            {
                                MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Exam marks Un-submit", UserObj.LogInID + " Un-submitted a course marks" +
                                    ".Course information : " + course.FormalCode + "_" + course.Title, "", course.FormalCode, _pageId, _pageName, _pageUrl);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            showAlert("Marks un-submit failed.");
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        showAlert("Marks for this course have already been Un-submitted.");
                        return;
                    }
                }
                else
                {
                    showAlert("Marks template not assigned for this course.");
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnResultPublish_Click(object sender, EventArgs e)
        {
            try
            {
                #region Getting Variable

                int CourseId = 0, VersionId = 0, AcacalId = 0;

                AcacalId = Convert.ToInt32(ucSession.selectedValue);

                Button btn = sender as Button;

                if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
                {
                    string[] ids = btn.CommandArgument.Split('_');

                    if (ids.Length == 2)
                    {
                        CourseId = Convert.ToInt32(ids[0]);
                        VersionId = Convert.ToInt32(ids[1]);
                    }
                }




                if (CourseId == 0 || VersionId == 0 || AcacalId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }

                #endregion



                var SubmitObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                    && x.VersionId == VersionId && x.AcacalId == AcacalId);

                if (SubmitObj != null)
                {
                    if (SubmitObj.FinalSubmitted != null && SubmitObj.FinalSubmitted == 1)
                    {
                        var course = ucamContext.Courses.Where(x => x.CourseID == CourseId && x.VersionID == VersionId).FirstOrDefault();

                        #region Publish Process

                        int PublishCount = 0;

                        List<SqlParameter> parameters1 = new List<SqlParameter>();
                        parameters1.Add(new SqlParameter { ParameterName = "@CourseId", SqlDbType = System.Data.SqlDbType.Int, Value = CourseId });
                        parameters1.Add(new SqlParameter { ParameterName = "@VersionId", SqlDbType = System.Data.SqlDbType.Int, Value = VersionId });
                        parameters1.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                        parameters1.Add(new SqlParameter { ParameterName = "@ModifiedBy", SqlDbType = System.Data.SqlDbType.Int, Value = UserObj.Id });

                        DataTable dtPublished = DataTableManager.GetDataFromQuery("ExamMarksPublishedByCourseVersionSession", parameters1);

                        if (dtPublished != null && dtPublished.Rows.Count > 0)
                            PublishCount++;
                        else
                        {
                            showAlert("publish failed for this course.");
                            return;
                        }

                        if (PublishCount > 0)
                        {
                            SubmitObj.Published = 1;
                            SubmitObj.PublishedDate = DateTime.Now;
                            SubmitObj.ModifiedBy = UserObj.Id;
                            SubmitObj.ModifiedDate = DateTime.Now;
                            ucamContext.Entry(SubmitObj).State = EntityState.Modified;
                            ucamContext.SaveChanges();
                            showAlert("Marks for the course have been Published.");
                            LoadGridView();

                            try
                            {
                                MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Exam marks Publish", UserObj.LogInID + " Publish a course marks" +
                                    ".Course information : " + course.FormalCode + "_" + course.Title, "", course.FormalCode, _pageId, _pageName, _pageUrl);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            showAlert("Marks publish failed.");
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        showAlert("Marks for this course not yet submitted.");
                        return;
                    }
                }
                else
                {
                    showAlert("Marks template not assigned for this course.");
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}