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

namespace EMS.miu.bill.report
{
    public partial class RptFeeSetup : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                //lblCount.Text = "0";
                
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {         
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);

            if (programId == 0 && batchId == 0)
            {
                ShowMessage("Please select Program and Batch.");
                return;
            }

            else if (batchId == 0)
            {
                LoadFeeSetup(programId, batchId);
            }

            else
            {
                LoadFeeSetup(programId, batchId);
            }
                    
        }

        private void LoadFeeSetup(int programId, int batchId)
        {
            List<rFeeSetup> list = new List<rFeeSetup>();
            string program = ucProgram.selectedText;
            string batch = ucBatch.selectedText;

            ReportParameter p1 = new ReportParameter("Show_Program", program);
            ReportParameter p2 = new ReportParameter("Show_Batch", batch);

            List<rFeeSetup> feeSetupList = FeeSetupManager.GetFeeSetup(programId, batchId);
            foreach (rFeeSetup fS in feeSetupList)
            {
                list.Add(fS);
            }
            
            if (list.Count != 0)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptFeeSetup.rdlc");
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                ReportDataSource rds = new ReportDataSource("RptFeeSetup", list);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";
                //lblCount.Text = list.Count().ToString();
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ShowMessage("NO Data Found. Please Enter Valid Program and Batch");
                return;
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