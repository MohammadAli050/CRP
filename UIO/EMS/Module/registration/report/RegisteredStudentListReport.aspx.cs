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
    public partial class RegisteredStudentListReport : BasePage
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
            try
            {
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
            }
        }


        protected void buttonView_Click_RSL(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            
            LoadRegisteredStudentList(acaCalId, programId, batchId);
        }

        private void LoadRegisteredStudentList(int acaCalId, int programId, int batchId)
        {
            string session = ucSession.selectedText;
            string program = ucProgram.selectedText;
            string batch = ucBatch.selectedText;

            ReportParameter p1 = new ReportParameter("Session", session);
            ReportParameter p2 = new ReportParameter("Program", program);
            ReportParameter p3 = new ReportParameter("Batch", batch);

            List<rRegisteredStudentList> list = ReportManager.GetRegisteredStudentList(acaCalId, programId, batchId);

            try
            {
                if (list.Count != 0)
                {
                    ProgramWiseRegistrationCount.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptRegisteredStudentList.rdlc");
                    ProgramWiseRegistrationCount.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3});
                    ReportDataSource rds = new ReportDataSource("RegisteredStudentList", list);

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