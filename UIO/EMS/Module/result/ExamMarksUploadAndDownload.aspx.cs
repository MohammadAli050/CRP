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
    public partial class ExamMarksUploadAndDownload : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamMarksUploadAndDownload);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamMarksUploadAndDownload));
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
                    divGrid.Visible = false;
                    hdnCourseInformation.Value = "0_0_0";
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

        #region Excel Download

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int CourseId = 0, VersionId = 0, AcacalId = 0, InstituteId = 0, ProgramId = 0;

                AcacalId = Convert.ToInt32(ucSession.selectedValue);
                InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

                string instituteName = "", programName = "";

                instituteName = ddlInstitute.SelectedItem.Text;
                programName = ddlProgram.SelectedItem.Text;

                #region Get CommandArgument value

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

                #endregion

                if (CourseId == 0 || VersionId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }

                #region Check Template

                var TemplateObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                && x.VersionId == VersionId
                && x.AcacalId == AcacalId);

                if (TemplateObj == null || TemplateObj.ExamTemplateMasterId == null || TemplateObj.ExamTemplateMasterId == 0)
                {
                    showAlert("No marks template assigned for this course.");
                    return;
                }

                #endregion


                #region Download Excel

                string originalFileName = "MarksEntrySampleExcel.xlsx";
                string fileName = "";

                var course = ucamContext.Courses.FirstOrDefault(x => x.CourseID == CourseId && x.VersionID == VersionId);
                fileName = course.VersionCode;

                FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/result/ExcelFiles/" + originalFileName));
                string path = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Grade_Sheet_Files/")).DirectoryName;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileInfo newFile = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Grade_Sheet_Files/" + fileName + ".xlsx"));

                #region Remove the excel file

                if (File.Exists(newFile.FullName))
                {
                    File.Delete(newFile.FullName);
                }

                #endregion

                var acaCal = AcademicCalenderManager.GetById(AcacalId);
                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet gradeSheet = myWorkbook.Worksheets["Course"];



                    string semester = string.Empty;
                    semester = acaCal.FullCode;

                    #region Course Information

                    string courseCode = string.Empty;
                    courseCode += course.VersionCode + "-" + course.Title + "(" + course.Credits + ")";

                    #endregion

                    #region Teacher 

                    string Faculty = "";

                    #endregion


                    gradeSheet.Protection.IsProtected = true;
                    gradeSheet.Protection.SetPassword("password"); // Optional
                    gradeSheet.Cells.Style.Locked = false;

                    gradeSheet.Cells[2, 1].Value = "Institute : " + instituteName;
                    gradeSheet.Cells[4, 1].Value = "Program : " + programName;
                    gradeSheet.Cells[5, 1].Value = "Session : " + semester;
                    gradeSheet.Cells[6, 1].Value = "Course : " + courseCode;
                    gradeSheet.Cells[7, 1].Value = CourseId + "_" + VersionId + "_" + AcacalId;

                    gradeSheet.Cells[2, 1].Style.Locked = true;
                    gradeSheet.Cells[4, 1].Style.Locked = true;
                    gradeSheet.Cells[5, 1].Style.Locked = true;
                    gradeSheet.Cells[6, 1].Style.Locked = true;
                    gradeSheet.Cells[7, 1].Style.Locked = true;
                    gradeSheet.Cells[7, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);

                    var ExamTemplateMaster = ExamTemplateMasterManager.GetById(TemplateObj.ExamTemplateMasterId);

                    if (ExamTemplateMaster != null)
                    {
                        gradeSheet.Cells[9, 5].Value = ExamTemplateMaster.ExamTemplateMasterTotalMark.ToString();

                        #region Fill Up Template Item

                        var ExamtemplateItemList = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(ExamTemplateMaster.ExamTemplateMasterId).ToList();

                        if (ExamtemplateItemList != null && ExamtemplateItemList.Any())
                        {
                            ExamtemplateItemList = ExamtemplateItemList.OrderBy(x => x.ColumnSequence).ToList();

                            int ItemStartIndex = 5;

                            #region Header Item

                            decimal overallMarks = 0;

                            foreach (var item in ExamtemplateItemList)
                            {
                                gradeSheet.Cells[8, ItemStartIndex].Value = item.ExamTemplateBasicItemName;
                                gradeSheet.Cells[9, ItemStartIndex].Value = item.ExamTemplateBasicItemMark.ToString();

                                gradeSheet.Cells[8, ItemStartIndex].Style.Locked = true;
                                gradeSheet.Cells[9, ItemStartIndex].Style.Locked = true;

                                overallMarks = overallMarks + item.ExamTemplateBasicItemMark;

                                ItemStartIndex++;
                            }

                            //gradeSheet.Cells[8, ItemStartIndex].Value = "Total Marks";
                            //gradeSheet.Cells[8, ItemStartIndex].Style.Locked = true;
                            //gradeSheet.Cells[9, ItemStartIndex].Value = overallMarks.ToString("0.00");
                            //gradeSheet.Cells[9, ItemStartIndex].Style.Locked = true;

                            #endregion

                            #region Student List

                            var StudentList = StudentCourseHistoryManager.GetAllByAcaCalIdCourseId(AcacalId, CourseId).ToList();

                            if (StudentList != null && StudentList.Any())
                            {
                                StudentList = StudentList.OrderBy(x => x.StudentRoll).ToList();

                                int StartRow = 10;
                                int SL = 1;

                                var ExammarkmasterList = ucamContext.ExamMarkMasters
                                    .Where(x => x.CourseId == CourseId
                                    && x.VersionId == VersionId
                                    && x.AcaCalId == AcacalId).ToList();

                                #region using linq query get exammarkdetails with join examMarkMaster

                                var ExamMarksDetailsList = (from emd in ucamContext.ExamMarkDetails
                                                            join emm in ucamContext.ExamMarkMasters
                                                            on emd.ExamMarkMasterId equals emm.ExamMarkMasterId
                                                            where emm.CourseId == CourseId
                                                            && emm.VersionId == VersionId
                                                            && emm.AcaCalId == AcacalId
                                                            select emd).ToList();

                                #endregion

                                int lastStudentRow = StartRow - 1; // Track last row for validation

                                foreach (var stditem in StudentList)
                                {
                                    decimal TotalMarks = 0;
                                    int CourseHistoryId = stditem.StudentCourseHistoryId;

                                    gradeSheet.Cells[StartRow, 1].Value = SL.ToString();
                                    gradeSheet.Cells[StartRow, 2].Value = stditem.StudentName;
                                    gradeSheet.Cells[StartRow, 3].Value = stditem.StudentRoll.ToString();

                                    gradeSheet.Cells[StartRow, 1].Style.Locked = true;
                                    gradeSheet.Cells[StartRow, 2].Style.Locked = true;
                                    gradeSheet.Cells[StartRow, 3].Style.Locked = true;

                                    #region Item Wise Marks

                                    ItemStartIndex = 5;

                                    foreach (var item in ExamtemplateItemList)
                                    {
                                        var ExammarkMasterObj = ExammarkmasterList
                                            .Where(x => x.ExamTemplateBasicItemId == item.ExamTemplateBasicItemId).FirstOrDefault();

                                        if (ExammarkMasterObj != null)
                                        {
                                            var exammarksDetailObj = ExamMarksDetailsList.Where(x => x.ExamMarkMasterId == ExammarkMasterObj.ExamMarkMasterId
                                            && x.CourseHistoryId == stditem.StudentCourseHistoryId).FirstOrDefault();

                                            if (exammarksDetailObj != null)
                                            {
                                                if (exammarksDetailObj.ExamMarkTypeId == 2) // For Absent 
                                                {
                                                    gradeSheet.Cells[StartRow, ItemStartIndex].Value = "";
                                                }
                                                else
                                                {
                                                    if (exammarksDetailObj.Marks != null)
                                                    {
                                                        gradeSheet.Cells[StartRow, ItemStartIndex].Value = Convert.ToDecimal(exammarksDetailObj.Marks).ToString("0.00");
                                                        TotalMarks = TotalMarks + Convert.ToDecimal(exammarksDetailObj.Marks);
                                                    }
                                                    else
                                                        gradeSheet.Cells[StartRow, ItemStartIndex].Value = "";
                                                }
                                            }
                                            else
                                                gradeSheet.Cells[StartRow, ItemStartIndex].Value = "";
                                        }

                                        ItemStartIndex++;
                                    }
                                    #endregion


                                    StartRow++;
                                    SL++;
                                }

                                #region Add Data Validation for Marks Columns (Simpler Approach)

                                // Apply validation to all marks input columns (from column 5 onwards)
                                int validationStartCol = 5;
                                int validationEndCol = 5 + ExamtemplateItemList.Count - 1;

                                for (int col = validationStartCol; col <= validationEndCol; col++)
                                {
                                    // Get max marks for this column
                                    decimal maxMark = Convert.ToDecimal(gradeSheet.Cells[9, col].Value ?? 100);

                                    // Apply validation for each row
                                    for (int row = 10; row <= lastStudentRow; row++)
                                    {
                                        var cell = gradeSheet.Cells[row, col];

                                        // Add decimal validation
                                        var validation = cell.DataValidation.AddDecimalDataValidation();
                                        validation.Operator = OfficeOpenXml.DataValidation.ExcelDataValidationOperator.between;
                                        validation.Formula.Value = -1;
                                        validation.Formula2.Value = (double?)maxMark;
                                        validation.AllowBlank = true;

                                        validation.ShowErrorMessage = true;
                                        validation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
                                        validation.ErrorTitle = "Invalid Marks";
                                        validation.Error = $"Enter a number between 0 and {maxMark}, or blank for absent";

                                        validation.ShowInputMessage = true;
                                        validation.PromptTitle = "Marks Entry";
                                        validation.Prompt = $"Enter marks (0-{maxMark}) or blank for absent";
                                    }
                                }

                                //// Add a note about AB entry
                                //gradeSheet.Cells[8, validationEndCol + 1].Value = "Note: For absent students type -1";
                                //gradeSheet.Cells[8, validationEndCol + 1].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                //gradeSheet.Cells[8, validationEndCol + 1].Style.Font.Size = 9;

                                #endregion

                            }

                            #endregion
                        }

                        #endregion
                    }

                    excelPackage.Save();
                }

                if (newFile.Name.Length > 0)
                {
                    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                    cookie.Value = "Flag";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + newFile.Name);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(newFile.FullName);
                    Response.End();
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                    cookie.Value = "Flag";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(cookie);
                }

                #endregion
            }
            catch (Exception ex)
            {
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
            }
        }

        #endregion

        #region Excel Upload
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                int CourseId = 0, VersionId = 0, AcacalId = 0;

                ClearModalGridView();

                AcacalId = Convert.ToInt32(ucSession.selectedValue);

                #region Get CommandArgument value

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


                #endregion

                if (CourseId == 0 || VersionId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }
                #region Check Template

                var TemplateObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                && x.VersionId == VersionId
                && x.AcacalId == AcacalId);

                if (TemplateObj == null || TemplateObj.ExamTemplateMasterId == null || TemplateObj.ExamTemplateMasterId == 0)
                {
                    showAlert("No marks template assigned for this course.");
                    return;
                }

                #endregion
                #region Check already Submitted or Published

                if (TemplateObj.FinalSubmitted != null && TemplateObj.FinalSubmitted == 1)
                {
                    showAlert("Marks already submitted for this course. Download not possible.");
                    return;
                }
                if (TemplateObj.Published != null && TemplateObj.Published == 1)
                {
                    showAlert("Marks already published for this course. Download not possible.");
                    return;
                }

                #endregion

                var course = ucamContext.Courses.Where(x => x.CourseID == CourseId && x.VersionID == VersionId).FirstOrDefault();
                if (course != null)
                {
                    lblCourseInformation.Text = "You are uploading marks for course : " + course.FormalCode + "_" + course.Title + "(" + course.Credits + ")";
                }
                hdnCourseInformation.Value = CourseId + "_" + VersionId + "_" + ucSession.selectedValue;
                modalPopupUploadExcel.Show();
            }
            catch (Exception ex)
            {
            }
        }
        private void ClearModalGridView()
        {
            try
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
                divGrid.Visible = false;
                hdnCourseInformation.Value = "0_0_0";
                hdnFinalSubmitValue.Value = "0_0_0";
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnExcelUpload_Click(object sender, EventArgs e)
        {
            lblUploadMessage.Text = "";
            lblStudentSummary.Text = "";
            lblStudentSummary2.Text = "";

            divSave.Visible = false;
            divSave2.Visible = false;

            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
            divGrid.Visible = false;

            modalPopupUploadExcel.Show();

            if (!fuExcel.HasFile)
            {
                lblUploadMessage.Text = "Please select a file to upload.";
                return;
            }

            #region File Types And Size Checking


            // Allowed file types
            string[] allowedExtensions = { ".xlsx", ".xls", ".csv" };
            string fileExt = System.IO.Path.GetExtension(fuExcel.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExt))
            {
                lblUploadMessage.Text = "Invalid file type. Only .xlsx, .xls, .csv files are allowed.";
                return;
            }

            // Max file size: 5 MB
            int maxFileSize = 5 * 1024 * 1024; // 5 MB in bytes
            if (fuExcel.PostedFile.ContentLength > maxFileSize)
            {
                lblUploadMessage.Text = "File size exceeds 5 MB limit.";
                return;
            }

            #endregion

            #region Variable 

            string HiddenValue = hdnCourseInformation.Value;
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

            int StudentIdStartRow = 10;


            #endregion


            if (CourseId == 0 || VersionId == 0 || AcacalId == 0)
            {
                showAlert("Invalid Course selection.");
                return;
            }


            #region File Upload Process Start

            var resultList = new List<StudentMarkUploadModel>();


            #region Save the file

            // Get the file name from the uploaded file
            string fileName = Path.GetFileName(fuExcel.PostedFile.FileName);

            // Get the path to the MarksUploadedFile folder in your application root
            string uploadFolder = Server.MapPath("~Module/Result/MarksUploadedFile");

            // Ensure the directory exists
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Combine the folder path and file name
            string savePath = Path.Combine(uploadFolder, fileName);

            // Save the uploaded file to the target folder
            fuExcel.PostedFile.SaveAs(savePath);

            #endregion


            decimal ExamMarks = 0;

            var StudentList = StudentCourseHistoryManager.GetAllByAcaCalIdCourseId(AcacalId, CourseId).ToList();
            List<ExamTemplateBasicItemDetails> examTemplateBasicItems = new List<ExamTemplateBasicItemDetails>();

            #region Section And Template Information

            try
            {
                var TemplateObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                && x.VersionId == VersionId && x.AcacalId == AcacalId);
                if (TemplateObj != null && TemplateObj.ExamTemplateMasterId != null && TemplateObj.ExamTemplateMasterId != 0)
                {
                    var ExamTemplateMaster = ExamTemplateMasterManager.GetById(TemplateObj.ExamTemplateMasterId);

                    if (ExamTemplateMaster != null)
                    {
                        ExamMarks = ExamTemplateMaster.ExamTemplateMasterTotalMark;
                        examTemplateBasicItems = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(ExamTemplateMaster.ExamTemplateMasterId).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
            }

            #endregion

            using (var package = new ExcelPackage(new FileInfo(savePath)))
            {
                // Pseudocode:
                // 1. Check if the package has any worksheets before accessing the first one.
                // 2. If there are no worksheets, handle the error (e.g., show a message or throw a custom exception).
                // 3. If worksheets exist, access the first worksheet safely.

                if (package.Workbook.Worksheets.Count == 0)
                {
                }
                else
                {

                    var worksheet = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[1] : null;

                    string fileHiddenvalue = worksheet.Cells[7, 1].Value.ToString();

                    if (HiddenValue != fileHiddenvalue)
                    {
                        showAlert("The uploaded file seems to correspond to a different course. Please verify and re-upload the correct file.");
                        return;
                    }

                    int row = StudentIdStartRow;

                    int StudentCount = 0, blankStudent = 0, exceedStudent = 0, absentStudent = 0, notfoundStudent = 0, invalidMarks = 0;

                    int headerRow = 8;

                    List<TemplateItemModel> templateItems = new List<TemplateItemModel>();

                    int validationStartCol = 5;
                    int validationEndCol = 5 + examTemplateBasicItems.Count - 1;

                    for (int col = validationStartCol; col <= validationEndCol; col++)
                    {

                        string header = worksheet.Cells[headerRow, col].Text.Trim();
                        if (!string.IsNullOrEmpty(header))
                        {
                            var tempObj = examTemplateBasicItems.FirstOrDefault(x => x.ExamTemplateBasicItemName == header);
                            templateItems.Add(new TemplateItemModel
                            {
                                TemplateId = tempObj == null ? 0 : tempObj.ExamTemplateBasicItemId,
                                TemplateMarks = tempObj == null ? "0" : tempObj.ExamTemplateBasicItemMark.ToString(),
                                TemplateName = header,
                                ColumnNo = col
                            });
                        }
                    }


                    int dataStartRow = 10;

                    while (worksheet.Cells[dataStartRow, 1].Value != null) // Check Sl column
                    {

                        for (int i = 0; i < templateItems.Count; i++)
                        {
                            var marksObj = worksheet.Cells[dataStartRow, templateItems[i].ColumnNo].Text.Trim();

                            var model = new StudentMarkUploadModel();

                            string Roll = worksheet.Cells[dataStartRow, 3].Text.Trim();
                            var studentObj = StudentList.FirstOrDefault(x => x.StudentRoll == Roll);
                            var Exists = resultList.Where(x => x.StudentRoll == Roll).FirstOrDefault();
                            if (Exists == null)
                                StudentCount++;

                            model.SL = Convert.ToInt32(worksheet.Cells[dataStartRow, 1].Text.Trim());
                            model.StudentName = worksheet.Cells[dataStartRow, 2].Text.Trim();
                            model.StudentRoll = Roll;

                            model.TemplateName = templateItems[i].TemplateName;
                            model.TemplateId = templateItems[i].TemplateId;
                            model.TemplateMarks = templateItems[i].TemplateMarks;
                            model.ValidMarks = 0;
                            model.Status = "";
                            if (studentObj != null)
                            {
                                model.HistoryId = studentObj.StudentCourseHistoryId;
                                #region Process Marks

                                if (marksObj != null)
                                {
                                    if (marksObj == "")
                                    {
                                        model.Marks = "AB";
                                        model.PresentStatus = 2;
                                        model.ValidMarks = 1;
                                        if (Exists == null)
                                            absentStudent++;
                                    }
                                    else
                                    {
                                        decimal marksValue = 0;
                                        bool isDecimal = decimal.TryParse(marksObj, out marksValue);
                                        if (isDecimal)
                                        {
                                            if (marksValue >= 0 && marksValue <= ExamMarks)
                                            {
                                                model.Marks = marksValue.ToString("0.00");
                                                model.PresentStatus = 1;
                                                model.ValidMarks = 1;
                                            }
                                            else if (marksValue > ExamMarks)
                                            {
                                                model.Marks = marksValue.ToString("0.00");
                                                model.PresentStatus = 1;
                                                model.Status = "marks exceed";
                                                if (Exists == null)
                                                    exceedStudent++;
                                            }
                                            else
                                            {
                                                model.Marks = marksObj;
                                                model.PresentStatus = 0;
                                                model.Status = "invalid marks";
                                                if (Exists == null)
                                                    invalidMarks++;
                                            }
                                        }
                                        else
                                        {
                                            model.Marks = marksObj;
                                            model.PresentStatus = 0;
                                            model.Status = "invalid marks";
                                            if (Exists == null)
                                                invalidMarks++;
                                        }
                                    }
                                }
                                else
                                {
                                    model.Marks = marksObj;
                                    model.PresentStatus = 0;
                                    model.Status = "invalid marks";
                                    if (Exists == null)
                                        invalidMarks++;
                                }

                                #endregion
                            }
                            else
                            {
                                model.Marks = "";
                                model.PresentStatus = 0;
                                model.Status = "student not found";
                                if (Exists == null)
                                    notfoundStudent++;
                            }

                            resultList.Add(model);
                        }

                        dataStartRow++;
                    }




                    if (resultList != null && resultList.Any())
                    {
                        divGrid.Visible = true;

                        gvStudentList.DataSource = resultList;
                        gvStudentList.DataBind();

                        lblStudentSummary.Text = "Registered student : " + StudentList.Count() + " , Uploaded student : " + StudentCount
                            + " , Not found student : " + notfoundStudent + " , Absent : " + absentStudent
                            + " , " + " Marks exceed : " + exceedStudent + " , Invalid marks : " + invalidMarks;
                        lblStudentSummary2.Text = lblStudentSummary.Text;

                        if (StudentList.Count() != StudentCount)
                        {
                            showAlert("Number of registered student not matched with excel student");
                            return;
                        }
                        else if (notfoundStudent > 0)
                        {
                            showAlert("Some students listed in the Excel file are not registered in this course.");
                            return;
                        }

                        if (resultList.Where(x => x.ValidMarks == 1).Count() > 0)
                        {
                            divSave.Visible = true;
                            divSave2.Visible = true;
                        }
                        else
                        {
                            divSave.Visible = false;
                            divSave2.Visible = false;
                        }

                    }

                }
            }


            // Clean up the temporary file
            File.Delete(savePath);

            #endregion


            lblUploadMessage.ForeColor = System.Drawing.Color.Green;
            lblUploadMessage.Text = "Please review the marks!";

            showAlert("Please review the marks and click the Save Marks button to store the data.");
            return;
        }
        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dataItem = (StudentMarkUploadModel)e.Row.DataItem;
                if (dataItem != null && dataItem.ValidMarks == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral; // Or any color you prefer
                }
            }
        }

        #endregion

        #region Save Uploaded Data

        protected void btnSaveUploadedMark_Click(object sender, EventArgs e)
        {
            try
            {
                #region Variable 

                string HiddenValue = hdnCourseInformation.Value;
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


                #endregion


                if (CourseId == 0 || VersionId == 0 || AcacalId == 0)
                {
                    showAlert("Invalid Course selection.");
                    return;
                }
                var course = ucamContext.Courses.Where(x => x.CourseID == CourseId && x.VersionID == VersionId).FirstOrDefault();
                List<ExamMarkMaster> examMarkMastersToAdd = new List<ExamMarkMaster>();
                List<ExamMarkDetails> examMarkDetailsToAdd = new List<ExamMarkDetails>();

                List<ExamTemplateBasicItemDetails> examTemplateBasicItems = new List<ExamTemplateBasicItemDetails>();

                #region Section And Template Information

                try
                {
                    var TemplateObj = ucamContext.MarksTemplateAndPersonAssigns.FirstOrDefault(x => x.CourseId == CourseId
                    && x.VersionId == VersionId && x.AcacalId == AcacalId);
                    if (TemplateObj != null && TemplateObj.ExamTemplateMasterId != null && TemplateObj.ExamTemplateMasterId != 0)
                    {
                        var ExamTemplateMaster = ExamTemplateMasterManager.GetById(TemplateObj.ExamTemplateMasterId);

                        if (ExamTemplateMaster != null)
                        {
                            examTemplateBasicItems = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(ExamTemplateMaster.ExamTemplateMasterId).ToList();
                        }
                    }

                }
                catch (Exception ex)
                {
                }

                #endregion

                int UpdatedCount = 0;

                List<string> studentCountList = new List<string>();

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    #region Getting Data From Gridview

                    int sl = Convert.ToInt32(gvStudentList.DataKeys[row.RowIndex]["SL"]);
                    string studentRoll = gvStudentList.DataKeys[row.RowIndex]["StudentRoll"].ToString();
                    int historyId = Convert.ToInt32(gvStudentList.DataKeys[row.RowIndex]["HistoryId"]);
                    int ValidMarks = Convert.ToInt32(gvStudentList.DataKeys[row.RowIndex]["ValidMarks"]);
                    int TemplateId = Convert.ToInt32(gvStudentList.DataKeys[row.RowIndex]["TemplateId"]);
                    int PresentStatus = Convert.ToInt32(gvStudentList.DataKeys[row.RowIndex]["PresentStatus"]);
                    decimal Marks = 0;

                    try
                    {
                        Marks = Convert.ToDecimal(gvStudentList.DataKeys[row.RowIndex]["Marks"]);
                    }
                    catch (Exception ex)
                    {
                    }

                    #endregion



                    if (ValidMarks == 1)
                    {
                        var ExamMarkMasterExists = ucamContext.ExamMarkMasters.FirstOrDefault(x => x.ExamTemplateBasicItemId == TemplateId
                        && x.AcaCalId == AcacalId && x.CourseId == CourseId && x.VersionId == VersionId);


                        var tempateItem = examTemplateBasicItems.Where(x => x.ExamTemplateBasicItemId == TemplateId).FirstOrDefault();
                        decimal convertedtoMark = 0.00M, examMarkValue = 0.00M;
                        if (tempateItem != null)
                        {
                            examMarkValue = tempateItem.ExamTemplateBasicItemMark;

                            if (tempateItem.Attribute2 != null)
                            {
                                convertedtoMark = Convert.ToDecimal(tempateItem.Attribute2);
                            }
                        }

                        int ExamMarkMasterId = 0;

                        #region Exam Marks Master Table insert

                        if (ExamMarkMasterExists == null)
                        {
                            UCAMDAL.ExamMarkMaster newExamMarkMaster = new UCAMDAL.ExamMarkMaster
                            {
                                ExamTemplateBasicItemId = TemplateId,
                                AcaCalId = AcacalId,
                                AcaCalSectionId = 0,
                                CourseId = CourseId,
                                VersionId = VersionId,
                                ExamMark = examMarkValue,
                                ExamMarkEntryDate = DateTime.Now,
                                CreatedBy = UserObj.Id,
                                CreatedDate = DateTime.Now
                            };
                            ucamContext.ExamMarkMasters.Add(newExamMarkMaster);
                            ucamContext.SaveChanges();
                            ExamMarkMasterId = newExamMarkMaster.ExamMarkMasterId;
                        }
                        else
                            ExamMarkMasterId = ExamMarkMasterExists.ExamMarkMasterId;

                        #endregion

                        #region Marks Process

                        decimal obtainedMark = 0.00M, obtainedConvertedMark = 0.00M;
                        if (PresentStatus == 1) // Present
                        {
                            obtainedMark = Convert.ToDecimal(Marks);
                            if (convertedtoMark > 0 && examMarkValue > 0)
                            {
                                //obtainedConvertedMark =Convert.ToDecimal(obtainedMark / examMarkValue) * convertedtoMark;
                                //obtainedConvertedMark = decimal.Multiply(decimal.Divide(obtainedMark, examMarkValue), convertedtoMark);
                                obtainedConvertedMark = obtainedMark * (convertedtoMark / examMarkValue);
                            }
                        }

                        #endregion

                        #region Exam Marks Details Table insert
                        var ExamMarkDetailsExists = ucamContext.ExamMarkDetails.FirstOrDefault(x => x.ExamMarkMasterId == ExamMarkMasterId
                        && x.CourseHistoryId == historyId);
                        if (ExamMarkDetailsExists == null)
                        {
                            UCAMDAL.ExamMarkDetail newExamMarkDetails = new UCAMDAL.ExamMarkDetail
                            {
                                ExamMarkMasterId = ExamMarkMasterId,
                                ExamTemplateBasicItemId = TemplateId,
                                CourseHistoryId = historyId,
                                Marks = obtainedMark,
                                ConvertedMark = obtainedConvertedMark,
                                IsFinalSubmit = false,
                                ExamMarkTypeId = PresentStatus,
                                CreatedBy = UserObj.Id,
                                CreatedDate = DateTime.Now
                            };
                            ucamContext.ExamMarkDetails.Add(newExamMarkDetails);
                            ucamContext.SaveChanges();
                            UpdatedCount++;

                            var exists = studentCountList.Where(x => x == historyId.ToString()).FirstOrDefault();
                            if (exists == null)
                                studentCountList.Add(historyId.ToString());

                        }
                        else
                        {
                            // Update existing record
                            ExamMarkDetailsExists.Marks = obtainedMark;
                            ExamMarkDetailsExists.ConvertedMark = obtainedConvertedMark;
                            ExamMarkDetailsExists.ExamMarkTypeId = PresentStatus;
                            ExamMarkDetailsExists.ModifiedBy = UserObj.Id;
                            ExamMarkDetailsExists.ModifiedDate = DateTime.Now;
                            ucamContext.Entry(ExamMarkDetailsExists).State = System.Data.Entity.EntityState.Modified;
                            ucamContext.SaveChanges();
                            UpdatedCount++;

                            var exists = studentCountList.Where(x => x == historyId.ToString()).FirstOrDefault();
                            if (exists == null)
                                studentCountList.Add(historyId.ToString());
                        }
                        #endregion
                    }
                }

                if (UpdatedCount > 0)
                {
                    showAlert(studentCountList.Count + " students " + UpdatedCount + " marks have been successfully saved.");
                    LoadGridView();
                    ClearModalGridView();
                    try
                    {
                        MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Exam marks save", UserObj.LogInID + " save a course marks" +
                            ".Course information : " + course.FormalCode + "_" + course.Title, "", course.FormalCode, _pageId, _pageName, _pageUrl);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    showAlert("No marks were saved. Please ensure there are valid marks to save.");
                    return;
                }


            }
            catch (Exception ex)
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

                        var StudentList = StudentCourseHistoryManager.GetAllByAcaCalIdCourseId(AcacalId, CourseId).ToList();

                        int studentCount = StudentList.Count();

                        foreach (var item in ExamtemlplateItemList)
                        {
                            try
                            {
                                var MarkmasterObj = ExamMarkMasterList.Where(x => x.ExamTemplateBasicItemId == item.ExamTemplateBasicItemId).FirstOrDefault();

                                if (MarkmasterObj != null)
                                {
                                    var marksDetailsList = ucamContext.ExamMarkDetails.Where(x => x.ExamMarkMasterId == MarkmasterObj.ExamMarkMasterId).ToList();
                                    int enteredStudentCount = marksDetailsList.Count();
                                    if (enteredStudentCount == studentCount)
                                    {
                                        //All student marks entered for this item
                                        List<SqlParameter> parameters1 = new List<SqlParameter>();
                                        parameters1.Add(new SqlParameter { ParameterName = "@ExamMarksMasterId", SqlDbType = System.Data.SqlDbType.Int, Value = MarkmasterObj.ExamMarkMasterId });
                                        parameters1.Add(new SqlParameter { ParameterName = "@ModifiedBy", SqlDbType = System.Data.SqlDbType.Int, Value = UserObj.Id });

                                        DataTable dtSubmittedMarks = DataTableManager.GetDataFromQuery("ExamMarksFinalSubmittedByMarksMasterId", parameters1);

                                        if (dtSubmittedMarks != null && dtSubmittedMarks.Rows.Count > 0)
                                            FinalSubmitCount++;
                                        else
                                        {
                                            FinalSubmitCount = 0;
                                            showAlert("You need to enter " + item.ExamTemplateBasicItemName + " marks before final submission.");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        FinalSubmitCount = 0;
                                        showAlert("You need to enter " + item.ExamTemplateBasicItemName + " marks for all students before final submission.");
                                        return;
                                    }
                                }
                                else
                                {
                                    FinalSubmitCount = 0;
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


    }
}