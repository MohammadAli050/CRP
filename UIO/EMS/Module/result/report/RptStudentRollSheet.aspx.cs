using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
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

namespace EMS.Module.result.report
{
    public partial class RptStudentRollSheet : BasePage
    {
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
            ClearReport();
            ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownList(ProgramId);
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReport();
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearReport();
        }

        private void ClearReport()
        {
            ReportViewer.LocalReport.DataSources.Clear();
        }

        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int institutionId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int programId = Convert.ToInt32(ddlProgram.SelectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);


                List<rStudentRollSheet> stdList = StudentManager.GetStudentRollSheetByProgramBatchSession(programId, batchId, acaCalId, 0, institutionId);


                if (stdList != null && stdList.Count > 0)
                {

                    string courses = "Course Code, Title & Course-wise student count/";

                    List<rStudentRollSheet> courseList = stdList.GroupBy(g => new { g.FormalCode, g.Title })
                             .Select(g => g.First())
                             .Where(g => string.IsNullOrEmpty(g.Title) == false).OrderBy(o => o.FormalCode).ToList();
                    int i = 1;
                    foreach (rStudentRollSheet item in courseList)
                    {
                        courses += i.ToString() + ". " + item.FormalCode + " : " + item.Title 
                            + " ("+ stdList.Where(x=>x.FormalCode==item.FormalCode && x.Title==item.Title).Count()+")"+"/";
                        i++;
                    }
                    List<ReportParameter> param1 = new List<ReportParameter>();
                    param1.Add(new ReportParameter("ExamName", ddlProgram.SelectedItem.ToString() + "-" + ucBatch.selectedText));
                    param1.Add(new ReportParameter("Session", ucSession.selectedText));
                    param1.Add(new ReportParameter("Courses", courses));


                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/result/report/RptStudentRollSheet.rdlc");

                    this.ReportViewer.LocalReport.SetParameters(param1);

                    ReportDataSource rds = new ReportDataSource("StudentRollSheetDataset", stdList);

                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rds);
                    ReportViewer.Visible = true;
                }
                else
                {
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.Visible = false;
                    showAlert("No Data Found!");
                }
            }
            catch (Exception ex)
            { }
        }
    }
}