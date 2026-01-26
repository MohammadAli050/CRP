using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCAMDAL;

namespace EMS.Module.Dashboard
{
    public partial class MainDashboard : BasePage
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
                    LoadDashboardData();
                }
            }
            catch (Exception Ex)
            {
            }
        }


        private void LoadDashboardData()
        {
            LoadStatistics();
            LoadInstitutions();
            LoadPrograms();
            LoadStudents();
            LoadFilters();
        }

        #region Statistics
        private void LoadStatistics()
        {
            try
            {
                var inst = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();
                var prog = ucamContext.Programs.ToList();
                var student = ucamContext.Students.ToList();

                lblTotalInstitutions.Text = inst.Count.ToString();
                lblTotalPrograms.Text = prog.Count.ToString();
                lblTotalStudents.Text = student.Where(x => x.IsDeleted == false || x.IsDeleted == null).Count().ToString();
                lblActiveStudents.Text = student.Where(x => x.IsActive == true && (x.IsDeleted == false || x.IsDeleted == null)).Count().ToString();

            }
            catch (Exception ex)
            {
                ShowError("Error loading statistics: " + ex.Message);
            }
        }
        #endregion

        #region Institutions
        private void LoadInstitutions(string searchText = "")
        {
            try
            {
                using (var context = new UCAMDAL.UCAMEntities())
                {
                    // Start with base query
                    var query = context.Institutions.AsQueryable();

                    // Apply search filter if search text is provided
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query = query.Where(i => i.InstituteName.Contains(searchText) ||
                                                i.InstituteCode.Contains(searchText));
                    }

                    // Order by CreatedDate descending and select required fields
                    var result = query
                        .OrderBy(i => i.InstituteName)
                        .Select(i => new
                        {
                            i.InstituteId,
                            i.InstituteName,
                            i.InstituteCode,
                            i.InstituteAddress,
                            i.ActiveStatus,
                            i.CreatedDate
                        })
                        .ToList();

                    gvInstitutions.DataSource = result;
                    gvInstitutions.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading institutions: " + ex.Message);
            }
        }

        protected void txtSearchInstitution_TextChanged(object sender, EventArgs e)
        {
            LoadInstitutions(txtSearchInstitution.Text.Trim());
        }

        protected void gvInstitutions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInstitutions.PageIndex = e.NewPageIndex;
            LoadInstitutions(txtSearchInstitution.Text.Trim());
        }
        #endregion

        #region Programs
        private void LoadPrograms(int? instituteId = null, string searchText = "")
        {
            try
            {
                using (var context = new UCAMDAL.UCAMEntities())
                {
                    var query = from p in context.Programs
                                join i in context.Institutions on p.InstituteId equals i.InstituteId into institutionGroup
                                from i in institutionGroup.DefaultIfEmpty() // LEFT JOIN
                                join c in context.ProgramTypes on p.ProgramTypeID equals c.ProgramTypeID
                                select new { p, i,c };

                    // Filter by institute ID if provided
                    if (instituteId.HasValue && instituteId.Value > 0)
                    {
                        query = query.Where(x => x.p.InstituteId == instituteId.Value);
                    }

                    // Filter by search text if provided
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query = query.Where(x => x.p.Code.Contains(searchText) ||
                                                x.p.ShortName.Contains(searchText) ||
                                                x.p.DegreeName.Contains(searchText));
                    }

                    // Order and select
                    var result = query
                        .OrderBy(x => x.i.InstituteName).ThenBy(x => x.p.Code)
                        .Select(x => new
                        {
                            x.p.ProgramID,
                            x.p.Code,
                            x.p.ShortName,
                            x.p.DetailName,
                            x.p.DegreeName,
                            x.p.TotalCredit,
                            x.p.Duration,
                            x.p.InstituteId,
                            InstituteName = x.i != null ? x.i.InstituteName : null,
                            ProgramType=x.c.TypeDescription
                        })
                        .ToList();

                    gvPrograms.DataSource = result;
                    gvPrograms.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading programs: " + ex.Message);
            }
        }

        protected void txtSearchProgram_TextChanged(object sender, EventArgs e)
        {
            int? instituteId = null;
            if (!string.IsNullOrEmpty(ddlFilterInstitute.SelectedValue) && ddlFilterInstitute.SelectedValue != "0")
            {
                instituteId = Convert.ToInt32(ddlFilterInstitute.SelectedValue);
            }
            LoadPrograms(instituteId, txtSearchProgram.Text.Trim());
        }

        protected void ddlFilterInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? instituteId = null;
            if (!string.IsNullOrEmpty(ddlFilterInstitute.SelectedValue) && ddlFilterInstitute.SelectedValue != "0")
            {
                instituteId = Convert.ToInt32(ddlFilterInstitute.SelectedValue);
            }
            LoadPrograms(instituteId, txtSearchProgram.Text.Trim());
        }

        protected void gvPrograms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrograms.PageIndex = e.NewPageIndex;
            int? instituteId = null;
            if (!string.IsNullOrEmpty(ddlFilterInstitute.SelectedValue) && ddlFilterInstitute.SelectedValue != "0")
            {
                instituteId = Convert.ToInt32(ddlFilterInstitute.SelectedValue);
            }
            LoadPrograms(instituteId, txtSearchProgram.Text.Trim());
        }
        #endregion

        #region Students
        private void LoadStudents(int? programId = null, string searchText = "")
        {
            try
            {
                using (var context = new UCAMDAL.UCAMEntities())
                {
                    var query = from s in context.Students
                                join p in context.Programs on s.ProgramID equals p.ProgramID into programGroup
                                from p in programGroup.DefaultIfEmpty() // LEFT JOIN
                                join b in context.Batches on s.BatchId equals b.BatchId into batchGroup
                                from b in batchGroup.DefaultIfEmpty()
                                join per in context.People on s.PersonID equals per.PersonID into personGroup
                                from per in personGroup.DefaultIfEmpty()
                                select new { s, p, b, per };
                    // Filter by program ID if provided
                    if (programId.HasValue && programId.Value > 0)
                    {
                        query = query.Where(x => x.s.ProgramID == programId.Value);
                    }
                    // Filter by search text if provided
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query = query.Where(x => x.s.Roll.Contains(searchText) ||
                                                SqlFunctions.StringConvert((double)x.s.StudentID).Contains(searchText));
                    }
                    // Order and select
                    var result = query
                        .OrderBy(x => x.s.Roll)
                        .Select(x => new
                        {
                            x.s.StudentID,
                            x.s.Roll,
                            x.s.PersonID,
                            BatchId = x.b != null ? x.b.BatchNO : null,
                            x.s.ProgramID,
                            x.s.SessionId,
                            x.s.IsActive,
                            x.s.IsCompleted,
                            x.s.IsDeleted,
                            ProgramName = x.p != null ? x.p.ShortName : null,
                            FullName = x.per != null ? x.per.FullName : null
                        })
                        .ToList();
                    gvStudents.DataSource = result;
                    gvStudents.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading students: " + ex.Message);
            }

        }

        protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
        {
            int? programId = null;
            if (!string.IsNullOrEmpty(ddlFilterProgram.SelectedValue) && ddlFilterProgram.SelectedValue != "0")
            {
                programId = Convert.ToInt32(ddlFilterProgram.SelectedValue);
            }
            LoadStudents(programId, txtSearchStudent.Text.Trim());
        }

        protected void ddlFilterProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? programId = null;
            if (!string.IsNullOrEmpty(ddlFilterProgram.SelectedValue) && ddlFilterProgram.SelectedValue != "0")
            {
                programId = Convert.ToInt32(ddlFilterProgram.SelectedValue);
            }
            LoadStudents(programId, txtSearchStudent.Text.Trim());
        }

        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            int? programId = null;
            if (!string.IsNullOrEmpty(ddlFilterProgram.SelectedValue) && ddlFilterProgram.SelectedValue != "0")
            {
                programId = Convert.ToInt32(ddlFilterProgram.SelectedValue);
            }
            LoadStudents(programId, txtSearchStudent.Text.Trim());
        }
        #endregion

        #region Filters
        private void LoadFilters()
        {
            LoadInstituteFilter();
            LoadProgramFilter();
        }

        private void LoadInstituteFilter()
        {
            //without sql connection
            try
            {
                using (var context = new UCAMDAL.UCAMEntities())
                {
                    var institutes = context.Institutions
                        .Where(i => i.ActiveStatus == 1)
                        .OrderBy(i => i.InstituteName)
                        .Select(i => new { i.InstituteId, i.InstituteName })
                        .ToList();
                    ddlFilterInstitute.DataSource = institutes;
                    ddlFilterInstitute.DataTextField = "InstituteName";
                    ddlFilterInstitute.DataValueField = "InstituteId";
                    ddlFilterInstitute.DataBind();
                    ddlFilterInstitute.Items.Insert(0, new ListItem("All Institutes", "0"));
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading institute filter: " + ex.Message);
            }

        }

        private void LoadProgramFilter()
        {

            try
            {
                using (var context = new UCAMDAL.UCAMEntities())
                {
                    var programs = context.Programs
                        .OrderBy(p => p.ShortName)
                        .Select(p => new { p.ProgramID, p.ShortName })
                        .ToList();
                    ddlFilterProgram.DataSource = programs;
                    ddlFilterProgram.DataTextField = "ShortName";
                    ddlFilterProgram.DataValueField = "ProgramID";
                    ddlFilterProgram.DataBind();
                    ddlFilterProgram.Items.Insert(0, new ListItem("All Programs", "0"));
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading program filter: " + ex.Message);
            }

        }
        #endregion

        #region Refresh
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }
        #endregion

        #region Helper Methods
        private void ShowError(string message)
        {
            // You can implement your own error handling mechanism
            // For example, display an error message on the page or log it
            Response.Write("<script>alert('" + message + "');</script>");
        }
        #endregion

        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

    }
}