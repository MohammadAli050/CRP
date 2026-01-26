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

namespace EMS.miu.registration.report
{
    public partial class RptDeptWiseRegisteredStudent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                LoadYearComboBox();
            }
        }

        private void LoadYearComboBox()
        {
            try
            {
                ddlYear.Items.Clear();
                //ddlYear.Items.Add(new ListItem("Select", "0"));
                //ddlYear.AppendDataBoundItems = true;

                List<rYear> list = AcademicCalenderManager.GetAllYear();

                if (list.Count > 0 && list != null)
                {
                    ddlYear.DataValueField = "Year";
                    ddlYear.DataTextField = "Year";
                    ddlYear.DataSource = list;
                    ddlYear.DataBind();
                }
            }
            catch { }
            
        }

        //View Report On Webpage
        protected void buttonView_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);

            LoadDeptWiseRegisteredStudent(year);
        }

        private void LoadDeptWiseRegisteredStudent(int year)
        {
            string yearId = ddlYear.SelectedValue;
            ReportParameter p1 = new ReportParameter("Year", yearId);

            List<rDeptWiseRegisteredStudent> list = ReportManager.GetDeptWiseRegisteredStudent(year);

            try
            {
                if (list.Count != 0)
                {
                    DeptWiseRegisteredStudent.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptDeptWiseRegisteredStudent.rdlc");
                    this.DeptWiseRegisteredStudent.LocalReport.SetParameters(new ReportParameter[] { p1});
                    ReportDataSource rds = new ReportDataSource("DeptWiseRegisteredStudent", list);

                    DeptWiseRegisteredStudent.LocalReport.DataSources.Clear();
                    DeptWiseRegisteredStudent.LocalReport.DataSources.Add(rds);
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
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            string yearId = ddlYear.SelectedValue;
            ReportParameter p1 = new ReportParameter("Year", yearId);

            List<rDeptWiseRegisteredStudent> list = ReportManager.GetDeptWiseRegisteredStudent(year);

            try
            {
                if (list.Count != 0)
                {
                    DeptWiseRegisteredStudent.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptDeptWiseRegisteredStudent.rdlc");
                    this.DeptWiseRegisteredStudent.LocalReport.SetParameters(new ReportParameter[] { p1 });
                    ReportDataSource rds = new ReportDataSource("DeptWiseRegisteredStudent", list);

                    DeptWiseRegisteredStudent.LocalReport.DataSources.Clear();
                    DeptWiseRegisteredStudent.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = DeptWiseRegisteredStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "DeptWiseRegisteredStudent" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "DeptWiseRegisteredStudent" + ".pdf");

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