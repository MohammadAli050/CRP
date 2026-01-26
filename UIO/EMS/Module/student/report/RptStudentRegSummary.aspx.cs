using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace EMS.miu.student.report
{
    public partial class RptStudentRegSummary : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;
                    
            if (!IsPostBack)
            {
                FillAcademicCalenderCombo();            
            }
        }

        private void FillAcademicCalenderCombo()
        {
            try
            {
                ddlSession.Items.Clear();
                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetCustom();

                ddlSession.Items.Add(new ListItem("Select", "0"));
                ddlSession.AppendDataBoundItems = true;

                if (academicCalenderList != null)
                {
                    academicCalenderList = academicCalenderList.OrderByDescending(x => x.Year).ToList();
                    int count = academicCalenderList.Count;
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                    {
                        ddlSession.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                        count = academicCalender.AcademicCalenderID;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        //View Report On Webpage
        protected void buttonView_Click(object sender, EventArgs e)
        {
            string roll = txtRoll.Text.Trim();
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            if (roll != "")
            {            
                List<rStudentClassExamSum> list = new List<rStudentClassExamSum>();
                list = ReportManager.GetStudentRegSummary(roll, acaCalId);

                if (list.Count != 0)
                {
                    StudentRegSummary.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentRegSummary.rdlc");
                    ReportDataSource rds = new ReportDataSource("RptStudentRegSummary", list);

                    StudentRegSummary.LocalReport.DataSources.Clear();
                    StudentRegSummary.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";        
                }
                else
                {
                    ShowMessage("No Data Found. Please Enter Valid Student ID And Session");
                    return;
                }
            }
            else
            {
                ShowMessage("Please Enter Student ID And Session");
                return;               
            }
        }

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            string roll = txtRoll.Text.Trim();
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            List<rStudentClassExamSum> list = new List<rStudentClassExamSum>();
            list = ReportManager.GetStudentRegSummary(roll, acaCalId);

            try
            {
                if (list.Count != 0)
                {
                    StudentRegSummary.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentRegSummary.rdlc");
                    ReportDataSource rds = new ReportDataSource("RptStudentRegSummary", list);

                    StudentRegSummary.LocalReport.DataSources.Clear();
                    StudentRegSummary.LocalReport.DataSources.Add(rds);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = StudentRegSummary.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentRegSummary" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "StudentRegSummary" + ".pdf");

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
                    ShowMessage("No Data Found. Please Enter Valid Student ID And Session");
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