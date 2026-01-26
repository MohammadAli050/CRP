using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EMS.Module.result
{
    public partial class GradeSheetDownloadNew : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.GradeSheetDownload);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.GradeSheetDownload));
        int userId = 0;

        #region Function

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            #region Log Insert
            try
            {
                LogGeneralManager.Insert(
                        DateTime.Now,
                        BaseAcaCalCurrent.Code,
                        BaseAcaCalCurrent.FullCode,
                        BaseCurrentUserObj.LogInID,
                        "",
                        "",
                        "Page Load",
                        BaseCurrentUserObj.LogInID + " is Loading Grade Sheet Download Page",
                        "normal",
                        _pageId,
                        _pageName,
                        _pageUrl,
                        "");
            }
            catch (Exception ex) { }
            #endregion
            lblMsg.Text = "";

            userId = BaseCurrentUserObj.Id;

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnGenerateGradeSheet);

            if (!IsPostBack)
            {
                LoadSession();
                LoadAffiliatedInstitution();
                LoadExamCenter();
            }
        }
        public void LoadSession()
        {


            List<AcademicCalender> sessionList = new List<AcademicCalender>();

            sessionList = AcademicCalenderManager.GetAll().OrderByDescending(x=>x.AcademicCalenderID).ToList();

            ddlSession.Items.Clear();
            ddlSession.AppendDataBoundItems = true;

            if (sessionList != null)
            {
                // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

                ddlSession.Items.Add(new ListItem("-Select-", "0"));
                ddlSession.DataTextField = "FullCode";
                ddlSession.DataValueField = "AcademicCalenderID";

                ddlSession.DataSource = sessionList;
                ddlSession.DataBind();
            }
        }
        private void GenerateGradeSheet(int programId, int acaCalId, int batchId, int courseId, int versionId, int examCenterId, int institutionId)
        {
            int flagExcept = 0;
            string fileName = string.Empty;
            FileInfo newFile = null;
            try
            {

                List<GradeSheetInfo> studentInfoList = StudentCourseHistoryManager.GetAllRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter(programId, acaCalId, batchId, courseId, versionId, examCenterId, institutionId);

                if (studentInfoList != null && studentInfoList.Count > 0)
                {
                    string programName = "";
                    string CourseNameNew = ddlCourse.SelectedItem.Text;
                    string formalCode = "";
                    string title = "";
                    string ExamCenter = ddlExamCenter.SelectedItem.Text;
                    if (!string.IsNullOrEmpty(CourseNameNew))
                    {
                        formalCode = CourseNameNew.Split('-').First();
                        title = CourseNameNew.Split('-').Last();
                    }

                    fileName = formalCode.Replace("(", string.Empty).Replace(")", string.Empty) + "_" + title.Replace("(", string.Empty).Replace(")", string.Empty); ;


                    FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/ExcelFiles/" + "GradeSheetTemplate.xlsx"));
                    string path = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Grade_Sheet_Files/")).DirectoryName;
                    //@"~\Result\Grade_Sheet_Files\";
                    if (File.Exists(Server.MapPath("~/Module/Result/Grade_Sheet_Files/" + fileName + ".xlsx")))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Module/Result/Grade_Sheet_Files/" + fileName + ".xlsx"));
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    newFile = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Grade_Sheet_Files/" + fileName + ".xlsx"));
                    //new FileInfo(@"~\Result\Grade_Sheet_Files\" + fileName + ".xlsx");

                    using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                    {
                        ExcelWorkbook myWorkbook = excelPackage.Workbook;

                        ExcelWorksheet gradeSheet = myWorkbook.Worksheets["Course"];

                        gradeSheet.Cells[7, 2].Value = programName + " EXAM RESULT";
                        gradeSheet.Cells[8, 4].Value = formalCode;
                        gradeSheet.Cells[9, 4].Value = title;
                        gradeSheet.Cells[10, 4].Value = ExamCenter;

                        if (studentInfoList.Count > 0 && studentInfoList != null)
                        {
                            Dictionary<string, string> dicStudentInfo = new Dictionary<string, string>();

                            int i = 16;
                            studentInfoList = studentInfoList.OrderBy(x => x.Roll).ToList();
                            foreach (GradeSheetInfo GradeSheetInfo in studentInfoList)
                            {
                                gradeSheet.Cells[i, 3].Value = GradeSheetInfo.RegistrationNo;
                                gradeSheet.Cells[i, 4].Value = GradeSheetInfo.SessionName;
                                gradeSheet.Cells[i, 5].Value = GradeSheetInfo.Roll;
                                i++;
                            }
                            int k = i;

                            for (; i < 1216; i++)
                            {
                                gradeSheet.DeleteRow(k);
                            }

                            excelPackage.Save();

                            #region Log Insert
                            LogGeneralManager.Insert(
                                    DateTime.Now,
                                    BaseAcaCalCurrent.Code,
                                    BaseAcaCalCurrent.FullCode,
                                    BaseCurrentUserObj.LogInID,
                                    "",
                                    "",
                                    "Grade Sheet Download",
                                    "Downaloded grade sheet of " + ddlSession.SelectedItem.Text + " " + ddlSession.SelectedItem.Text + " " + ddlCourse.SelectedItem.Text,
                                    "normal",
                                    _pageId,
                                    _pageName,
                                    _pageUrl,
                                    "");

                            #endregion
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "No data Found!";
                }
            }
            catch (Exception ex)
            {
                flagExcept = 1;
                lblMsg.Text = "Error: 101" + ex.Message;
            }
            finally
            {
                //if()
                if (flagExcept == 0)
                {
                    DownloadFile(newFile);
                    //lblMsg.Text = "Check D Drive. File Name- " + fileName;
                }
            }
        }

        private void DownloadFile(FileInfo file)
        {
            if (file.Name.Length > 0)
            {


                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                //Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();

            }
        }

        private void LoadAllCourse()
        {
            try
            {
                //int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ddlSession.SelectedValue);
               // int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int ExamCenterId = Convert.ToInt32(ddlExamCenter.SelectedValue);

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select Course", "0_0"));

                //List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAllCourseByProgramAndSessionBatchFromStudentCourseHistoryTable(programId, sessionId, batchId).OrderBy(d => d.FormalCode).ToList();
                List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(0, sessionId, 0, ExamCenterId).OrderBy(d => d.FormalCode).ToList();

                if (courseList.Count > 0 && courseList != null)
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

        private void LoadExamCenter()
        {
            try
            {
                //old code
                //List<ExamCenter> list = ExamCenterManager.GetAll();

                //ddlExamCenter.Items.Clear();
                //ddlExamCenter.AppendDataBoundItems = true;

                //if (list != null)
                //{
                //    ddlExamCenter.Items.Add(new ListItem("-Select-", "0"));
                //    ddlExamCenter.DataTextField = "ExamCenterName";
                //    ddlExamCenter.DataValueField = "Id";

                //    ddlExamCenter.DataSource = list;
                //    ddlExamCenter.DataBind();
                //}

                //new code 
                List<UserExamCenter> list = UserExamCenterManager.GetAllByUserId(userId);

                ddlExamCenter.Items.Clear();
                ddlExamCenter.AppendDataBoundItems = true;

                if (list != null)
                {
                    ddlExamCenter.Items.Add(new ListItem("-Select-", "0"));
                    ddlExamCenter.DataTextField = "ExamCenterName";
                    ddlExamCenter.DataValueField = "ExamCenterId";

                    ddlExamCenter.DataSource = list;
                    ddlExamCenter.DataBind();
                }
            }
            catch { }
            finally { }
        }

        private void LoadAffiliatedInstitution()
        {
            try
            {
                //List<AffiliatedInstitution> list = AffiliatedInstitutionManager.GetAll();

                //ddlInstitution.Items.Clear();
                //ddlInstitution.AppendDataBoundItems = true;

                //if (list != null)
                //{
                //    ddlInstitution.Items.Add(new ListItem("-Select-", "0"));
                //    ddlInstitution.DataTextField = "Name";
                //    ddlInstitution.DataValueField = "Id";

                //    ddlInstitution.DataSource = list;
                //    ddlInstitution.DataBind();
                //}
            }
            catch { }
            finally { }
        }



        #endregion

        #region Event

        //protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        //    ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        //    LoadAllCourse();
        //}

        //protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        //    LoadAllCourse();
        //}

        //protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int programId = Convert.ToInt32(ucProgram.selectedValue);
        //    int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        //    int batchId = Convert.ToInt32(ucBatch.selectedValue);

        //    LoadAllCourse();
        //}

        protected void ddlExamCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
        }

        protected void btnGenerateGradeSheet_Click(Object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
               // int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ddlSession.SelectedValue);
               // int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int examCenterId = Convert.ToInt32(ddlExamCenter.SelectedValue);
                int institutionId = 0;// Convert.ToInt32(ddlInstitution.SelectedValue);

                if ( sessionId != 0  )
                {
                    if (examCenterId == 0)
                    {
                        lblMsg.Text = "Please select exam center";
                        return;
                    }

                    string CourseNameNew = ddlCourse.SelectedValue;
                    if (!string.IsNullOrEmpty(CourseNameNew) && examCenterId != 0)
                    {
                        int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                        int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
                        if (courseId == 0)
                        {
                            lblMsg.Text = "Please select course";
                            return;
                        }
                        GenerateGradeSheet(0, sessionId, 0, courseId, versionId, examCenterId, institutionId);
                    }
                    else
                    {
                        lblMsg.Text = "Please Select Course & Exam Center.";
                    }
                }
                else
                {
                    lblMsg.Text = "Please Select Program Session Batch.";
                }
            }
            catch (Exception ex)
            { }


            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.AppendCookie(cookie);
        }


        #endregion


    }
}