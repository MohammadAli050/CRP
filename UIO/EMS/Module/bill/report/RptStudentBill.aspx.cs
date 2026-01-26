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
    public partial class RptStudentBill : BasePage
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
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //View Report On Webpage
        protected void buttonView_Click(object sender, EventArgs e)
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
                LoadStudentBill(programId, acaCalId);
            }
        }

        private void LoadStudentBill(int programId, int acaCalId)
        {
            List<rStudentBill> list = null; //BillHistoryManager.GetStudentBill(programId, acaCalId);

            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);

            try
            {
                if (list.Count != 0)
                {
                    StudentBill.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptStudentBill.rdlc");
                    this.StudentBill.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("StudentBill", list);

                    StudentBill.LocalReport.DataSources.Clear();
                    StudentBill.LocalReport.DataSources.Add(rds);
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

        //View Report In PDF For Print
        protected void buttinPrint_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);

            List<rStudentBill> list = null; // BillHistoryManager.GetStudentBill(programId, acaCalId);

            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);

            try
            {
                if (list.Count != 0)
                {
                    StudentBill.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptStudentBill.rdlc");
                    this.StudentBill.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("StudentBill", list);

                    StudentBill.LocalReport.DataSources.Clear();
                    StudentBill.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = StudentBill.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentBill" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "StudentBill" + ".pdf");

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