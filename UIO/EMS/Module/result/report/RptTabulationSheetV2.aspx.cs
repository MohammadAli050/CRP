using BussinessObject;
using CommonUtility;
using DevExpress.XtraExport;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using EMS.Module;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCAMDAL;


namespace EMS.miu.result.report
{
    public partial class RptTabulationSheetV2 : BasePage
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
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
            ClearReportView();
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReportView();
        }
        private void ClearReportView()
        {
            ReportViewer1.Visible = false;
        }

        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void ResultLoadButton_Click(object sender, EventArgs e)
        {
        }

        protected void ResultLoadButton_Click1(object sender, EventArgs e)
        {
            //try
            //{
            //    int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
            //    int AcacalId = Convert.ToInt32(ucSession.selectedValue);

            //    int InstitutionId = Convert.ToInt32(ddlInstitute.SelectedValue);
            //    var institutionObj = ucamContext.Institutions.Where(x => x.InstituteId == InstitutionId).FirstOrDefault();
            //    string InstitutionName = institutionObj != null ? institutionObj.InstituteName : "";
            //    string base64String = "";
            //    if (institutionObj != null && institutionObj.LogoBytes != null)
            //    {
            //        base64String = institutionObj.LogoBytes;
            //    }

            //    string resultDataView = "";

            //    pnlTabulationSheet.Controls.Add(new LiteralControl(resultDataView));


            //    List<GetSemesterDetailsResultForTabulation_Result> semesterDetailsResultForTabulation_ResultList = new List<GetSemesterDetailsResultForTabulation_Result>();

            //    semesterDetailsResultForTabulation_ResultList = ucamContext.GetSemesterDetailsResultForTabulation(ProgramId, AcacalId).ToList();


            //    if (semesterDetailsResultForTabulation_ResultList != null && semesterDetailsResultForTabulation_ResultList.Any())
            //    {
            //        var acudetailsList = ucamContext.StudentACUDetails.Where(x => x.StdAcademicCalenderID == AcacalId).ToList();

            //        var Gradedetails = GradeDetailsManager.GetAll().Where(x => x.GradeId <= 12).ToList();



            //        resultDataView += "<div class='row'>"
            //                        + "<div class='col-lg-4 col-md-4 col-sm-4'>"

            //        #region Grade Table

            //                         + "<table class='table table-bordered table-black-border' style='border-color:black'>"
            //                         + "<thead>"
            //                         + "<tr>"
            //                           + "<th colspan='3' class='text-center fw-bold text-primary'>GRADING SYSTEM</th>"
            //                           + "</tr>"
            //                           + "<tr>"
            //                           + "<th style='text-align:center;'>Marks</th>"
            //                           + "<th style='text-align:center;'>Grade</th>"
            //                           + "<th style='text-align:center;'>Grade Point</th>"
            //                           + "</tr>"
            //                           + "</thead>"
            //                            + "<tbody>";

            //        foreach (var grade in Gradedetails.OrderBy(x => x.GradeId))
            //        {
            //            try
            //            {
            //                string Marks = "", Grade = "", GradePoint = "";

            //                if (grade.GradeId == 1)// first
            //                {
            //                    Grade = grade.Grade;
            //                    Marks = ">=" + grade.MinMarks.ToString() + "%";
            //                    GradePoint = grade.GradePoint.ToString();
            //                }
            //                else if (grade.GradeId == 11)
            //                {
            //                    Grade = "I";
            //                    GradePoint = "Incomplete";
            //                    Marks = "-";
            //                }
            //                else if (grade.GradeId == 12)
            //                {
            //                    Grade = "W";
            //                    GradePoint = "Withdrawn";
            //                    Marks = "-";
            //                }
            //                else if (grade.GradeId == 10)// last
            //                {
            //                    Grade = grade.Grade;
            //                    Marks = "<" + (grade.MaxMarks + 1).ToString() + "%";
            //                    GradePoint = grade.GradePoint.ToString();
            //                }
            //                else
            //                {
            //                    Grade = grade.Grade;
            //                    Marks = grade.MinMarks.ToString() + "% to <" + (grade.MaxMarks + 1).ToString() + "%";
            //                    GradePoint = grade.GradePoint.ToString();
            //                }
            //                resultDataView += "<tr>"
            //                                + "<td style='text-align:center;'>" + Marks + "</td>"
            //                                + "<td style='text-align:center;'>" + Grade + "</td>"
            //                                + "<td style='text-align:center;'>" + GradePoint + "</td>"
            //                                + "</tr>";

            //            }
            //            catch (Exception ex)
            //            {
            //            }

            //        }


            //        resultDataView += "</tbody></table></div>";

            //        #endregion

            //        #region Report Name And Logo

            //        resultDataView += "<div class='col-lg-4 col-md-4 col-sm-4 text-center'>"
            //                        + "<h4 class='fw-bold text-primary'>BANGLADESH UNIVERSITY OF PROFESSIONALS</h4>"
            //                        + "<h5 class='fw-bold text-primary'>Tabulation Sheet</h5>"
            //                        + "</div>";
            //        #endregion


            //        resultDataView += "</div>";

            //    }


            //}
            //catch (Exception ex)
            //{
            //}
        }



        [WebMethod]
        public static string GetTabulationSheetData(string programId, string acacalId)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            try
            {
                int ProgramId = Convert.ToInt32(programId);
                int AcacalId = Convert.ToInt32(acacalId);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                parameters.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });

                DataTable dtResultProcess = DataTableManager.GetDataFromQuery("GetSemesterDetailsResultForTabulation", parameters);

                if (dtResultProcess != null && dtResultProcess.Rows.Count > 0)
                {
                    List<SqlParameter> parameters2 = new List<SqlParameter>();
                    parameters2.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                    parameters2.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });

                    DataTable dtResultSummary = DataTableManager.GetDataFromQuery("GetSemesterResultSummaryForTabulation", parameters2);

                    response["status"] = "success";
                    response["resultData"] = DataTableMethods.DataTableToJsonConvert(dtResultProcess);
                    response["resultSummary"] = DataTableMethods.DataTableToJsonConvert(dtResultSummary);
                }
                else
                {
                    response["status"] = "error";
                    response["message"] = "No data found!";
                }


            }
            catch (Exception ex)
            {
            }

            return JsonConvert.SerializeObject(response);

        }

    }
}