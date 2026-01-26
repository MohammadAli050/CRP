using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using BussinessObject;
using EMS.Module;


namespace EMS.miu.result.report
{
    public partial class RptExamResultReport : BasePage
    {
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

        #endregion

        #region On Change Methods

        protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProgram(UserObj, Convert.ToInt32(ddlInstitute.SelectedValue));
            ucSession.LoadDropDownList(0);
            ClearReportView();
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
            LoadAllCourse();
            ClearReportView();
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
            ClearReportView();
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllCourse();
            ClearReportView();
        }
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReportView();
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReportView();
        }

        private void ClearReportView()
        {
            ExamResultReport.Visible = false;
        }

        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void ResultLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                string course = ddlCourse.SelectedValue;
                string[] courseVersion = course.Split('_');
                int courseId = Convert.ToInt32(courseVersion[0]);
                int versionId = Convert.ToInt32(courseVersion[1]);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);


                List<SqlParameter> parameters1 = new List<SqlParameter>();
                parameters1.Add(new SqlParameter { ParameterName = "@CourseId", SqlDbType = System.Data.SqlDbType.Int, Value = courseId });
                parameters1.Add(new SqlParameter { ParameterName = "@VersionId", SqlDbType = System.Data.SqlDbType.Int, Value = versionId });
                parameters1.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = acaCalId });

                DataTable dtmarksDetails = DataTableManager.GetDataFromQuery("GetDetailsMarksListByCourseVersionAndSession", parameters1);

                if (dtmarksDetails != null && dtmarksDetails.Rows.Count > 0)
                {
                    string InstName = ddlInstitute.SelectedItem.ToString();
                    string ProgramName = ddlProgram.SelectedItem.ToString();
                    string SessionName = ucSession.selectedText;
                    string CourseName = Convert.ToString(ddlCourse.SelectedItem);

                    ReportParameter p1 = new ReportParameter("ProgramName", ProgramName);
                    ReportParameter p2 = new ReportParameter("SessionName", SessionName);
                    ReportParameter p3 = new ReportParameter("CourseName", CourseName);
                    ReportParameter p4 = new ReportParameter("InstituteName", InstName);


                    ExamResultReport.LocalReport.ReportPath = Server.MapPath("~/Module/result/report/RptExamResultReport.rdlc");
                    ExamResultReport.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
                    ReportDataSource rds = new ReportDataSource("ExamResultReport", dtmarksDetails);

                    ExamResultReport.LocalReport.DataSources.Clear();
                    ExamResultReport.LocalReport.DataSources.Add(rds);
                    ExamResultReport.LocalReport.Refresh();
                    ExamResultReport.Visible = true;
                }
                else
                {
                    ExamResultReport.Visible = false;
                    showAlert("No Data Found!");
                }
            }
            catch (Exception ex)
            {
                ExamResultReport.Visible = false;
            }
        }
    }
}