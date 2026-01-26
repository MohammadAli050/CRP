using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System.Drawing;

namespace EMS.miu.bill.report
{
    public partial class RptProgramWiseDue : BasePage
    {
        List<AcademicCalender> acaCalList = new List<AcademicCalender>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            { 
                LoadYear();
            }
        }

        private void LoadYear()
        {
            List<rYear> yearList = AcademicCalenderManager.GetAllYear();
            ddlYear.DataSource = yearList;
            ddlYear.DataTextField="Year" ;
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                int year = Convert.ToInt32(ddlYear.SelectedValue);
            }
            catch (Exception ex){ }
        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try 
            {
                string yearName = Convert.ToString(ddlSession.SelectedItem);
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                acaCalList = AcademicCalenderManager.GetAll().Where(d => d.Year == year).ToList();
                AcademicCalender trimesterObj = new AcademicCalender();
                AcademicCalender semesterObj = new AcademicCalender();

                string gender = Convert.ToString(ddlGender.SelectedItem);
                if (gender == "All") { gender = null; }
                int semesterAcaCalId = 0;
                int trimesterAcaCalId = 0;
                if (yearName == "Spring") 
                {
                    trimesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 1).FirstOrDefault();
                    trimesterAcaCalId = trimesterObj.AcademicCalenderID;
                    semesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 4).FirstOrDefault();
                    semesterAcaCalId = semesterObj.AcademicCalenderID;
                }
                else if (yearName == "Summer") 
                {
                    trimesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 2).FirstOrDefault();
                    trimesterAcaCalId = trimesterObj.AcademicCalenderID;
                    semesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 4).FirstOrDefault();
                    //semesterAcaCalId = semesterObj.AcademicCalenderID;
                    semesterAcaCalId = 0;
                }
                else if (yearName == "Fall") 
                {
                    trimesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 3).FirstOrDefault();
                    trimesterAcaCalId = trimesterObj.AcademicCalenderID;
                    semesterObj = acaCalList.Where(d => d.CalenderUnitTypeID == 5).FirstOrDefault();
                    semesterAcaCalId = semesterObj.AcademicCalenderID;
                }
                else { }

                LoadProgramWiseDue(trimesterAcaCalId, semesterAcaCalId, gender);

            }
            catch (Exception ex) { }
        }

        private void LoadProgramWiseDue(int trimesterAcaCalId, int semesterAcaCalId, string gender)
        {
            List<rProgramWiseDue> list = null; //BillHistoryManager.GetProgramWiseDue(trimesterAcaCalId, semesterAcaCalId, gender);

            string session = ddlSession.SelectedItem.Text;
            string year = ddlYear.SelectedItem.Text;
            string Gender = ddlGender.SelectedItem.Text;


            ReportParameter p1 = new ReportParameter("Session", session);
            ReportParameter p2 = new ReportParameter("Year", year);
            ReportParameter p3 = new ReportParameter("SGender", Gender);
           

            try
            {
                if (list.Count != 0)
                {
                    ProgramWiseDue.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptProgramWiseDue.rdlc");
                    this.ProgramWiseDue.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });
                    ReportDataSource rds = new ReportDataSource("ProgramWiseDue", list);

                    ProgramWiseDue.LocalReport.DataSources.Clear();
                    ProgramWiseDue.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {

                    ReportDataSource rds = new ReportDataSource("DailyOrPeriodicalCollection", list);
                    ProgramWiseDue.LocalReport.DataSources.Add(rds);
                    ProgramWiseDue.LocalReport.DataSources.Clear();

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