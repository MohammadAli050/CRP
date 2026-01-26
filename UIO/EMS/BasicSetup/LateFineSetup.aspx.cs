using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.SyllabusMan
{
    public partial class LateFineSetup : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 base.CheckPage_Load();

                if (!IsPostBack)
                {
                    LoadDropdownList();
                    LoadGrid();
                }
            }
            catch
            { }
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        private void LoadDropdownList()
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            LateFineScheduleManager.Delete(id);
            LoadGrid();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                LateFineSchedule lateFineSchedule = null;

                if (btnInsert.Text != "Update")
                {
                    lateFineSchedule = new LateFineSchedule();

                    lateFineSchedule.ScheduleName = txtScheduleName.Text.Trim();
                    lateFineSchedule.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    lateFineSchedule.ProgramId = Convert.ToInt32(ucProgram.selectedValue);
                    lateFineSchedule.AcademicCalenderId = Convert.ToInt32(ucSession.selectedValue);
                    lateFineSchedule.StartDate = DateTime.ParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lateFineSchedule.EndDate = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lateFineSchedule.CreatedBy = -1;
                    lateFineSchedule.CreatedDate = DateTime.Now;
                    lateFineSchedule.ModifiedBy = -1;
                    lateFineSchedule.ModifiedDate = DateTime.Now;

                    LateFineScheduleManager.Insert(lateFineSchedule);
                }
                else
                {
                    lateFineSchedule = new LateFineSchedule();

                    lateFineSchedule.LateFineScheduleId = Convert.ToInt32(btnInsert.CommandArgument);
                    lateFineSchedule.ScheduleName = txtScheduleName.Text.Trim();
                    lateFineSchedule.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    lateFineSchedule.ProgramId = Convert.ToInt32(ucProgram.selectedValue);
                    lateFineSchedule.AcademicCalenderId = Convert.ToInt32(ucSession.selectedValue);
                    lateFineSchedule.StartDate = DateTime.ParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lateFineSchedule.EndDate = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lateFineSchedule.CreatedBy = -1;
                    lateFineSchedule.CreatedDate = DateTime.Now;
                    lateFineSchedule.ModifiedBy = -1;
                    lateFineSchedule.ModifiedDate = DateTime.Now;

                    LateFineScheduleManager.Update(lateFineSchedule);
                }

                btnInsert.CommandArgument = "";
                btnInsert.Text = "Insert";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CleareField();
                LoadGrid();
            }
        }

        private void CleareField()
        {
            txtScheduleName.Text = string.Empty;
            txtAmount.Text = string.Empty;
            ucProgram.IndexO();
            ucSession.IndexO();  
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
        }

        private void LoadGrid()
        {
            List<LateFineSchedule> list = LateFineScheduleManager.GetAll();

            gvLateFineSchedule.DataSource = list;
            gvLateFineSchedule.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                int id = int.Parse(btn.CommandArgument.ToString());

                LateFineSchedule lateFineSchedule = LateFineScheduleManager.GetById(id);

                txtScheduleName.Text = lateFineSchedule.ScheduleName;
                txtAmount.Text = lateFineSchedule.Amount.ToString();
               
                ucProgram.SelectedValue(lateFineSchedule.ProgramId);
                ucSession.SelectedSessionByProgram(lateFineSchedule.ProgramId, lateFineSchedule.AcademicCalenderId);
                 
                txtStartDate.Text = lateFineSchedule.StartDate.ToString("dd/MM/yyyy");
                txtEndDate.Text = lateFineSchedule.EndDate.ToString("dd/MM/yyyy");

                btnInsert.CommandArgument = id.ToString();
                btnInsert.Text = "Update";

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            CleareField();
            LoadGrid();
        }
    }
}