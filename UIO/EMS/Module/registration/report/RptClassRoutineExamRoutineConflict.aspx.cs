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
    public partial class RptClassRoutineExamRoutineConflict : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                ucProgram.LoadDropDownList();
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);

            if (programId == 0)
            {
                ShowMessage("Please Select A Program.");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please Select A Session.");
                return;
            }
            else
            {
                LoadClassAndExamRoutineConflict(programId, acaCalId);
            }
        }

        private void LoadClassAndExamRoutineConflict(int programId, int acaCalId)
        {
            List<rClassRoutineConflict> CRCList = ReportManager.GetClassRoutineConflict(programId, acaCalId);
            List<rExamRoutineConflict> ERCList = ReportManager.GetExamRoutineConflict(programId, acaCalId);
            List<rClassRoutineConflictPair> CRCPList = ReportManager.GetClassRoutineConflictPair(programId, acaCalId);
            List<rExamRoutineConflictPair> ERCPList = ReportManager.GetExamRoutineConflictPair(programId, acaCalId);

            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);

            try
            {
                if (CRCList.Count != 0 && ERCList.Count != 0)
                {
                    ClassAndExamRoutineConflict.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptClassRoutineExamRoutineConflict.rdlc");
                    this.ClassAndExamRoutineConflict.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("ClassRoutineConflict", CRCList);
                    ReportDataSource rds2 = new ReportDataSource("ExamRoutineConflict", ERCList);
                    ReportDataSource rds3 = new ReportDataSource("ClassRoutineConflictPair", CRCPList);
                    ReportDataSource rds4 = new ReportDataSource("ExamRoutineConflictPair", ERCPList);

                    ClassAndExamRoutineConflict.LocalReport.DataSources.Clear();
                    ClassAndExamRoutineConflict.LocalReport.DataSources.Add(rds);
                    ClassAndExamRoutineConflict.LocalReport.DataSources.Add(rds2);
                    ClassAndExamRoutineConflict.LocalReport.DataSources.Add(rds3);
                    ClassAndExamRoutineConflict.LocalReport.DataSources.Add(rds4);

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