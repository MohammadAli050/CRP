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
    public partial class RptStudentRegSummary : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                if (userObj.RoleID == 9)
                {
                    User user = UserManager.GetById(userObj.Id);
                    Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                    txtRoll.Text = student.Roll;
                    txtRoll.ReadOnly = true;
                }

                FillAcademicCalenderCombo();
            }
        }

        private void FillAcademicCalenderCombo()
        {
            string roll = txtRoll.Text.Trim();
            try
            {
                ddlSession.Items.Clear();
                List<rAcaCalSessionListByRoll> academicCalenderList = AcademicCalenderManager.GetAcaCalSessionListByRoll(roll);

                ddlSession.Items.Add(new ListItem("Select", "0"));
                ddlSession.AppendDataBoundItems = true;

                if (academicCalenderList != null)
                {
                    academicCalenderList = academicCalenderList.OrderByDescending(x => x.Year).ToList();
                    int count = academicCalenderList.Count;
                    foreach (rAcaCalSessionListByRoll academicCalender in academicCalenderList)
                    {
                        ddlSession.Items.Add(new ListItem(academicCalender.TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
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

                List<rOfferedCourseClassRoutine> OCList = ReportManager.GetOfferedCourseClassRoutine(roll, acaCalId);

                if (list.Count != 0)
                {
                    StudentRegSummary.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptStudentProfileRegSummary.rdlc");
                    ReportDataSource rds = new ReportDataSource("RptStudentRegSummary", list);
                    ReportDataSource rds2 = new ReportDataSource("OfferedCourseClassRoutine", OCList);

                    StudentRegSummary.LocalReport.DataSources.Clear();
                    StudentRegSummary.LocalReport.DataSources.Add(rds);
                    StudentRegSummary.LocalReport.DataSources.Add(rds2);
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

            List<rOfferedCourseClassRoutine> OCList = ReportManager.GetOfferedCourseClassRoutine(roll, acaCalId);

            try
            {
                if (list.Count != 0)
                {
                    StudentRegSummary.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptStudentProfileRegSummary.rdlc");
                    ReportDataSource rds = new ReportDataSource("RptStudentRegSummary", list);
                    ReportDataSource rds2 = new ReportDataSource("OfferedCourseClassRoutine", OCList);

                    StudentRegSummary.LocalReport.DataSources.Clear();
                    StudentRegSummary.LocalReport.DataSources.Add(rds);
                    StudentRegSummary.LocalReport.DataSources.Add(rds2);

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