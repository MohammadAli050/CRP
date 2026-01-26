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

namespace EMS.miu
{
    public partial class AdmittedStudentCount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadDropDownList();
            }
        }

        private void LoadDropDownList()
        {
            //List<AcademicCalender> sessionList = new List<AcademicCalender>();
            //sessionList = AcademicCalenderManager.GetAll();

            //var distinctSessionList = (from a in sessionList.OrderByDescending(c => c.AcademicCalenderID)
            //                           where a.CalenderUnitTypeID.Equals(1) || a.CalenderUnitTypeID.Equals(2) || a.CalenderUnitTypeID.Equals(3)
            //                           select new {a.FullCode}).ToList().Distinct(); //.OrderByDescending(a => a.AcademicCalenderID);

            

            //sessionList = sessionList.OrderByDescending(l => l.AcademicCalenderID).ToList();

            //ddlStartingSession.Items.Clear();
            //ddlStartingSession.AppendDataBoundItems = true;

            //ddlEndingSession.Items.Clear();
            //ddlEndingSession.AppendDataBoundItems = true;

            //if (distinctSessionList != null)
            //{
            //    ddlStartingSession.Items.Add(new ListItem("-Select-", "0"));
            //    ddlStartingSession.DataTextField = "FullCode";
            //    ddlStartingSession.DataValueField = "FullCode";

            //    ddlStartingSession.DataSource = distinctSessionList;
            //    ddlStartingSession.DataBind();

            //    ddlEndingSession.Items.Add(new ListItem("-Select-", "0"));
            //    ddlEndingSession.DataTextField = "FullCode";
            //    ddlEndingSession.DataValueField = "FullCode";

            //    ddlEndingSession.DataSource = distinctSessionList;
            //    ddlEndingSession.DataBind();
            //}
        }

        private void ShowMessage(string msg, Color color)
        {
            lblMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = color;
        }

        protected void ButtonLoad_Click(object sender, EventArgs e)
        {

            //String SessionFrom = ddlStartingSession.SelectedItem.Value.ToString();
            //String SessionTo = ddlEndingSession.SelectedItem.Value.ToString();

            List<LogicLayer.BusinessObjects.RO.rAdmittedStudentCount> list = ReportManager.GetAdmittedStudentCount();

            //ReportParameter p1 = new ReportParameter("FromSession", SessionFrom);
            //ReportParameter p2 = new ReportParameter("ToSession", SessionTo);
            RptAdmittedStudentCount.LocalReport.ReportPath = Server.MapPath("~/miu/RptAdmittedStudentCount.rdlc");
            //this.RptAdmittedStudentCount.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

            ReportDataSource rds = new ReportDataSource("RptAdmittedStudentCountDataSet", list);

            RptAdmittedStudentCount.LocalReport.DataSources.Clear();
            RptAdmittedStudentCount.LocalReport.DataSources.Add(rds);

            //TextBox.Text = ddlStartingSession.SelectedIndex.ToString() + "--" + ddlEndingSession.SelectedIndex.ToString();

        }


    }


}