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
    public partial class ExamResultProcess : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamResultProcess);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamResultProcess));
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
                    ucSession.LoadDropDownListWithAllOption(0);
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
            ucSession.LoadDropDownListWithAllOption(0);
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            ucSession.LoadDropDownListWithAllOption(ProgramId);
        }
        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int AcacalId = Convert.ToInt32(ucSession.selectedValue);
                int BatchId = Convert.ToInt32(ucBatch.selectedValue);
                int StudentId = 0;

                if (!string.IsNullOrWhiteSpace(txtStudent.Text))
                {
                    var student = ucamContext.Students.FirstOrDefault(x => x.Roll == txtStudent.Text.Trim());
                    if (student != null)
                    {
                        StudentId = student.StudentID;
                    }
                }


                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                parameters.Add(new SqlParameter { ParameterName = "@BatchId", SqlDbType = System.Data.SqlDbType.Int, Value = BatchId });
                parameters.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                parameters.Add(new SqlParameter { ParameterName = "@StudentId", SqlDbType = System.Data.SqlDbType.Int, Value = StudentId });
                parameters.Add(new SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.Int, Value = UserObj.Id });

                DataTable dtResultProcess = DataTableManager.GetDataFromQuery("ResultProcessByParameters", parameters);

                if (dtResultProcess != null && dtResultProcess.Rows.Count > 0)
                {
                    int count = dtResultProcess.Rows[0][0] != DBNull.Value ? Convert.ToInt32(dtResultProcess.Rows[0][0]) : 0;

                    showAlert("Result Processed Successfully. Total Processed Student Count: " + count);

                }
                else
                {
                    showAlert("No data found for processing.");
                }

                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
            }
            catch (Exception ex)
            {
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
            }
        }
    }
}