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

namespace EMS.miu.bill.report
{
    public partial class RptDailyOrPeriodicaCollectionCreationDateWise : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {

            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
       
        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void buttonView_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string genderId = Convert.ToString(ddlMaleFemale.SelectedValue);
            string fromDate = dateTextBox1.Text.Trim();
            string toDate = dateTextBox2.Text.Trim();

            LoadDailyOrPeriodicalCollection(programId, batchId, genderId, fromDate, toDate);

        }

        private void LoadDailyOrPeriodicalCollection(int programId, int batchId, string genderId, string fromDate, string toDate)
        {
            List<rDailyOrPeriodicalCollection> list = null; //BillHistoryManager.GetDailyOrPeriodicalCollectionCreationDateWise(programId, batchId, genderId, fromDate, toDate);

            string program = ucProgram.selectedText;
            string batch = ucBatch.selectedText;
            string gender = ddlMaleFemale.SelectedItem.Text;
            string fDate = dateTextBox1.Text.Trim();
            string tDate = dateTextBox2.Text.Trim();

            ReportParameter p1 = new ReportParameter("FromDate", fDate);
            ReportParameter p2 = new ReportParameter("ToDate", tDate);
            ReportParameter p3 = new ReportParameter("Program", program);
            ReportParameter p4 = new ReportParameter("Batch", batch);
            ReportParameter p5 = new ReportParameter("Gender", gender);

            try
            {
                if (list.Count != 0)
                {
                    DailyOrPeriodicalCollection.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptDailyOrPeriodicalCollection.rdlc");
                    this.DailyOrPeriodicalCollection.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                    ReportDataSource rds = new ReportDataSource("DailyOrPeriodicalCollection", list);

                    DailyOrPeriodicalCollection.LocalReport.DataSources.Clear();
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {
                   
                    ReportDataSource rds = new ReportDataSource("DailyOrPeriodicalCollection", list);
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Add(rds);
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Clear();

                    ShowMessage("NO Data Found.");
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string genderId = Convert.ToString(ddlMaleFemale.SelectedValue);
            string fromDate = dateTextBox1.Text.Trim();
            string toDate = dateTextBox2.Text.Trim();

            List<rDailyOrPeriodicalCollection> list = null; //BillHistoryManager.GetDailyOrPeriodicalCollection(programId, batchId, genderId, fromDate, toDate);

            string program = ucProgram.selectedText;
            string batch = ucBatch.selectedText;
            string gender = ddlMaleFemale.SelectedItem.Text;
            string fDate = dateTextBox1.Text.Trim();
            string tDate = dateTextBox2.Text.Trim();

            ReportParameter p1 = new ReportParameter("FromDate", fDate);
            ReportParameter p2 = new ReportParameter("ToDate", tDate);
            ReportParameter p3 = new ReportParameter("Program", program);
            ReportParameter p4 = new ReportParameter("Batch", batch);
            ReportParameter p5 = new ReportParameter("Gender", gender);

            try
            {
                if (list.Count != 0)
                {
                    DailyOrPeriodicalCollection.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptDailyOrPeriodicalCollection.rdlc");
                    this.DailyOrPeriodicalCollection.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                    ReportDataSource rds = new ReportDataSource("DailyOrPeriodicalCollection", list);

                    DailyOrPeriodicalCollection.LocalReport.DataSources.Clear();
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = DailyOrPeriodicalCollection.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "DailyOrPeriodicalCollection" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "DailyOrPeriodicalCollection" + ".pdf");

                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                    Byte[] buffer = client.DownloadData(path);
                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", buffer.Length.ToString());
                        Response.BinaryWrite(buffer);
                    }
                }
                else
                {

                    ReportDataSource rds = new ReportDataSource("DailyOrPeriodicalCollection", list);
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Add(rds);
                    DailyOrPeriodicalCollection.LocalReport.DataSources.Clear();

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