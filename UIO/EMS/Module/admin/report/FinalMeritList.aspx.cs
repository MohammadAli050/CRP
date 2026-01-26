using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;

namespace EMS.miu.admin
{
    public partial class FinalMeritList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucFromSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucToSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int fromSessionId = Convert.ToInt32(ucFromSession.selectedValue);
            int toSessionId = Convert.ToInt32(ucToSession.selectedValue);
            List<rFinalMeritList> stdList = ReportManager.GetMeritListByProgramIDAndSessionRange(programId, fromSessionId, toSessionId);

            ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
            ReportParameter p2 = new ReportParameter("FromSemester", ucFromSession.selectedText);
            ReportParameter p3 = new ReportParameter("ToSemester", ucToSession.selectedText);
            ReportParameter p4 = new ReportParameter("PrintDate", DateTime.Now.ToString("dd/M/yyyy"));
            MeritListRptViewer.LocalReport.ReportPath = Server.MapPath("~/miu/admin/report/RptFinalMeritList.rdlc");
            MeritListRptViewer.LocalReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4 });
            ReportDataSource rds = new ReportDataSource("FinalMeritListDataset", stdList);
            MeritListRptViewer.LocalReport.DataSources.Clear();
            MeritListRptViewer.LocalReport.DataSources.Add(rds);
        }
    }
}