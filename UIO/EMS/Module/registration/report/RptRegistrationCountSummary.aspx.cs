using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration.report
{
    public partial class RptRegistrationCountSummary : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                LoadCalenderType();
            }
        }

        protected void LoadCalenderType()
        {
            try
            {
                ddlCalenderType.Items.Clear();
                //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
                //ddlCalenderType.AppendDataBoundItems = true;

                List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

                if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
                {
                    ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                    ddlCalenderType.DataTextField = "Name";
                    ddlCalenderType.DataSource = calenderUnitMasterList;
                    ddlCalenderType.DataBind();
                }
            }
            catch { }
            finally
            {
                int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                LoadAcademicCalender(calenderTypeId);
            }
        }
        protected void CalenderType_Changed(Object sender, EventArgs e)
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        protected void LoadAcademicCalender(int calenderTypeId)
        {
            try
            {
                ddlAcademicCalender.Items.Clear();
                ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
                ddlAcademicCalender.AppendDataBoundItems = true;

                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

                if (academicCalenderList.Count > 0 && academicCalenderList != null)
                {
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                    academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                    ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                }
            }
            catch { }
        }



        protected void buttonView_Click_PWRC(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);

            LoadProgramWiseRegistrationCount(acaCalId);
        }

        private void LoadProgramWiseRegistrationCount(int acaCalId)
        {
            List<rProgramWiseRegistrationCount> list = ReportManager.GetProgramWiseRegistrationCount(acaCalId);
            ReportParameter p1 = new ReportParameter("Session", ddlAcademicCalender.SelectedItem.Text);
            try
            {
                if (list.Count != 0)
                {
                    ProgramWiseRegistrationCount.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptProgramWiseRegistrationCount.rdlc");
                    ProgramWiseRegistrationCount.LocalReport.SetParameters(new ReportParameter[] { p1 });
                    ReportDataSource rds = new ReportDataSource("ProgramWiseRegistrationCount", list);

                    ProgramWiseRegistrationCount.LocalReport.DataSources.Clear();
                    ProgramWiseRegistrationCount.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {
                    ShowMessage("NO Data Found.");
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }

       
    }
}