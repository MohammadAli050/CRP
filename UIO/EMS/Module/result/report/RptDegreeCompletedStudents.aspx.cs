using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result.report
{
    public partial class RptDegreeCompletedStudents : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                LoadDropDownList();
            }
        }

        public void LoadDropDownList()
        {
            List<AcademicCalender> sessionList = new List<AcademicCalender>();
            sessionList = AcademicCalenderManager.GetAll();

            sessionList = sessionList.OrderByDescending(l=> l.AcademicCalenderID).ToList();

            ddlFromSession.Items.Clear();
            ddlFromSession.AppendDataBoundItems = true;

            ddlToSession.Items.Clear();
            ddlToSession.AppendDataBoundItems = true;

            if (sessionList != null)
            {
                ddlFromSession.Items.Add(new ListItem("-Select-", "0"));
                ddlFromSession.DataTextField = "FullCode";
                ddlFromSession.DataValueField = "AcademicCalenderID";

                ddlFromSession.DataSource = sessionList;
                ddlFromSession.DataBind();

                ddlToSession.Items.Add(new ListItem("-Select-", "0"));
                ddlToSession.DataTextField = "FullCode";
                ddlToSession.DataValueField = "AcademicCalenderID";

                ddlToSession.DataSource = sessionList;
                ddlToSession.DataBind();
            }
        }

        private void ShowMessage(string msg,Color color)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = color;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int fromSession = Convert.ToInt32(ddlFromSession.SelectedItem.Value);
                int toSession = Convert.ToInt32(ddlToSession.SelectedItem.Value);
                string Range = "";
                List<rDegreeCompletedStudent> list = ReportManager.GetDegreeCompletedStudentCountSessionRange();

                if (list.Count != 0 && list != null)
                {
                    if (fromSession != 0)
                    {
                        list = list.Where(l => l.AcacalID >= fromSession).ToList();
                        Range = "From " + ddlFromSession.SelectedItem.Text;
                    }
                    if (toSession != 0)
                    {
                        list = list.Where(l => l.AcacalID <= toSession).ToList();
                        Range += " To " + ddlToSession.SelectedItem.Text;
                    }

                    ReportParameter p1 = new ReportParameter("Range", Range);
                    DegreeCompletedStudents.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptDegreeCompletedStudents.rdlc");
                    this.DegreeCompletedStudents.LocalReport.SetParameters(new ReportParameter[] { p1 });
                    ReportDataSource rds = new ReportDataSource("DegreeCompleteStudentCountDataSet", list);

                    DegreeCompletedStudents.LocalReport.DataSources.Clear();
                    DegreeCompletedStudents.LocalReport.DataSources.Add(rds);
                    ShowMessage("",Color.Red);
                }
                else
                {
                    ShowMessage("No Data Found", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red); 
            }
        }

        
    }
}