using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects.RO;

namespace EMS.miu.bill.report
{
    public partial class RptStudentWiseDue : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack) 
            { 
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string genderType = Convert.ToString(ddlGender.SelectedItem);
            if (genderType == "All") 
            { 
                genderType = null; 
            }
            LoadStudentWiseDue(programId, acaCalId, batchId, genderType);
        }

        private void LoadStudentWiseDue(int programId, int acaCalId, int batchId, string genderType)
        {
            List<rStudentWiseDue> list = null; //BillHistoryManager.GetStudentWiseDue(programId, acaCalId, batchId, genderType);

            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;
            string batch = ucBatch.selectedText;
            string Gender = ddlGender.SelectedItem.Text;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);
            ReportParameter p3 = new ReportParameter("Batch", batch);
            ReportParameter p4 = new ReportParameter("SGender", Gender);


            try
            {
                if (list.Count != 0)
                {
                    StudentWiseDue.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptStudentWiseDue.rdlc");
                    this.StudentWiseDue.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
                    ReportDataSource rds = new ReportDataSource("StudentWiseDue", list);

                    StudentWiseDue.LocalReport.DataSources.Clear();
                    StudentWiseDue.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {

                    ReportDataSource rds = new ReportDataSource("StudentWiseDue", list);
                    StudentWiseDue.LocalReport.DataSources.Add(rds);
                    StudentWiseDue.LocalReport.DataSources.Clear();

                    lblMessage.Text = "No Data Found.";
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}