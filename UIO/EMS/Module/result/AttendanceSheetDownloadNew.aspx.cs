using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.result
{
    public partial class AttendanceSheetDownloadNew : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.AttendanceSheetDownload);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.AttendanceSheetDownload));
        int userId = 0;

        BussinessObject.UIUMSUser UserObj = null;
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

                base.CheckPage_Load();


                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnGenerateAttendanceSheet);

                if (!IsPostBack)
                {
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
            ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
            LoadAllCourse();
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
        }


        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }


        #region Function

        private void GenerateAttendanceSheet(int programId, int acaCalId, int batchId, int courseId, int versionId, int examCenterId, int institutionId)
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
                    string ExamCenter = "";
                    if (!string.IsNullOrEmpty(CourseNameNew))
                    {
                        formalCode = CourseNameNew.Split('-').First();
                        title = CourseNameNew.Split('-').Last();
                    }

                    fileName = formalCode.Replace("(", string.Empty).Replace(")", string.Empty) + "_" + title.Replace("(", string.Empty).Replace(")", string.Empty); 


                    FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/ExcelFiles/" + "AttendanceSheetTemplate.xlsx"));
                    string path = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Attendance_Sheet_Files/")).DirectoryName;
                    //@"~\Result\Attendance_Sheet_Files\";
                    if (File.Exists(Server.MapPath("~/Module/Result/Attendance_Sheet_Files/" + fileName + ".xlsx")))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Module/Result/Attendance_Sheet_Files/" + fileName + ".xlsx"));
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    newFile = new FileInfo(HttpContext.Current.Server.MapPath("~/Module/Result/Attendance_Sheet_Files/" + fileName + ".xlsx"));
                    //new FileInfo(@"~\Result\Attendance_Sheet_Files\" + fileName + ".xlsx");

                    using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                    {
                        ExcelWorkbook myWorkbook = excelPackage.Workbook;

                        ExcelWorksheet gradeSheet = myWorkbook.Worksheets["Course"];

                        gradeSheet.Cells[7, 2].Value = programName + " Attendance Sheet";
                        gradeSheet.Cells[8, 4].Value = formalCode;
                        gradeSheet.Cells[9, 4].Value = title;

                        if (studentInfoList.Count > 0 && studentInfoList != null)
                        {
                            Dictionary<string, string> dicStudentInfo = new Dictionary<string, string>();

                            int i = 13;
                            studentInfoList = studentInfoList.OrderBy(x => x.Roll).ToList();
                            foreach (GradeSheetInfo AttendanceSheetInfo in studentInfoList)
                            {
                                gradeSheet.Cells[i, 3].Value = AttendanceSheetInfo.RegistrationNo;
                                gradeSheet.Cells[i, 4].Value = AttendanceSheetInfo.SessionName;
                                gradeSheet.Cells[i, 5].Value = AttendanceSheetInfo.Roll;
                                i++;
                            }
                            int k = i;

                            for (; i < 1213 + 6; i++)
                            {
                                gradeSheet.DeleteRow(k);
                            }

                            excelPackage.Save();

                        }
                    }
                }
                else
                {
                    showAlert("No data Found!");
                }
            }
            catch (Exception ex)
            {
                flagExcept = 1;
                showAlert("Error: 101" + ex.Message);
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

        static bool IsEmptyRow(Range row)
        {
            foreach (Range cell in row.Cells)
            {
                if (cell.Value2 != null)
                {
                    return false;
                }
            }
            return true;
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
                int programId = Convert.ToInt32(ddlProgram.SelectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int ExamCenterId = 0;

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select Course", "0_0"));

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

        #endregion

        #region Event

        protected void btnGenerateAttendanceSheet_Click(Object sender, EventArgs e)
        {
            try
            {
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int institutionId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int programId = Convert.ToInt32(ddlProgram.SelectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);

                if (sessionId != 0)
                {
                    string CourseNameNew = ddlCourse.SelectedValue;
                    if (!string.IsNullOrEmpty(CourseNameNew))
                    {
                        int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                        int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                        if (courseId == 0)
                        {
                            showAlert("Please select course");
                            return;
                        }
                        GenerateAttendanceSheet(programId, sessionId, batchId, courseId, versionId, 0, institutionId);
                    }
                    else
                    {
                        showAlert("Please Select Course & Exam Center.");
                    }
                }
                else
                {
                    showAlert("Please Select Program Session Batch.");
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