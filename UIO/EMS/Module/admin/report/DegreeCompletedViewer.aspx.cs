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

namespace EMS.miu.admin.report
{
    public partial class DegreeCompletedViewer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                InitDropDown();
            }
        }

        private void InitDropDown()
        {
            ddlTriProgram.DataSource = ProgramManager.GetAll().Where(x => x.CalenderUnitMasterID == 1).ToList();
            ddlTriProgram.DataValueField = "ProgramID";
            ddlTriProgram.DataTextField = "Code";
            ddlTriProgram.DataBind();
            ddlTriProgram.Items.Insert(0, new ListItem("None", "-1"));
            ddlTriProgram.Items.Insert(0, new ListItem("All", "0"));

            ddlSemProgram.DataSource = ProgramManager.GetAll().Where(x => x.CalenderUnitMasterID == 2).ToList();
            ddlSemProgram.DataValueField = "ProgramID";
            ddlSemProgram.DataTextField = "Code";
            ddlSemProgram.DataBind();
            ddlSemProgram.Items.Insert(0, new ListItem("None", "-1"));
            ddlSemProgram.Items.Insert(0, new ListItem("All", "0"));

            List<AcademicCalender> triSessionList = AcademicCalenderManager.GetAll(1);
            List<AcademicCalender> semSessionList = AcademicCalenderManager.GetAll(2);

            ddlTriFromSession.DataTextField = "FullCode";
            ddlTriFromSession.DataValueField = "AcademicCalenderID";
            ddlTriFromSession.DataSource = triSessionList;
            ddlTriFromSession.DataBind();

            ddlTriToSession.DataTextField = "FullCode";
            ddlTriToSession.DataValueField = "AcademicCalenderID";
            ddlTriToSession.DataSource = triSessionList;
            ddlTriToSession.DataBind();


            ddlSemFromSession.DataTextField = "FullCode";
            ddlSemFromSession.DataValueField = "AcademicCalenderID";
            ddlSemFromSession.DataSource = semSessionList;
            ddlSemFromSession.DataBind();

            ddlSemToSession.DataTextField = "FullCode";
            ddlSemToSession.DataValueField = "AcademicCalenderID";
            ddlSemToSession.DataSource = semSessionList;
            ddlSemToSession.DataBind();

        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int triprogramId = Convert.ToInt32(ddlTriProgram.SelectedValue);
            int trifromSessionId = Convert.ToInt32(ddlTriFromSession.SelectedValue);
            int tritoSessionId = Convert.ToInt32(ddlTriToSession.SelectedValue);

            int semprogramId = Convert.ToInt32(ddlSemProgram.SelectedValue);
            int semfromSessionId = Convert.ToInt32(ddlSemFromSession.SelectedValue);
            int semtoSessionId = Convert.ToInt32(ddlSemToSession.SelectedValue);

            List<rDegreeCompletion> stdList = new List<rDegreeCompletion>();

            if (ddlTriProgram.SelectedValue != "-1")
                stdList.AddRange(ReportManager.GetDegreeCompletionListByProgramIDAndSessionRange(triprogramId, trifromSessionId, tritoSessionId, 1));

            if (ddlSemProgram.SelectedValue != "-1")
                stdList.AddRange(ReportManager.GetDegreeCompletionListByProgramIDAndSessionRange(semprogramId, semfromSessionId, semtoSessionId, 2));

            ReportParameter p1 = new ReportParameter("Program", ddlTriProgram.SelectedItem.Text);
            DegreeCompletionRptViewer.LocalReport.ReportPath = Server.MapPath("~/miu/admin/report/RptDegreeCompletion.rdlc");
            DegreeCompletionRptViewer.LocalReport.SetParameters(new ReportParameter[] { p1 });
            ReportDataSource rds = new ReportDataSource("DegreeCompletionDataset", stdList.OrderBy(x => x.Code));
            DegreeCompletionRptViewer.LocalReport.DataSources.Clear();
            DegreeCompletionRptViewer.LocalReport.DataSources.Add(rds);
        }
    }
}