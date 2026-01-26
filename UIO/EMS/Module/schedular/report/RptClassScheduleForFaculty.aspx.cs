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

namespace EMS.miu.schedular.report
{
    public partial class RptClassScheduleForFaculty : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                ucProgram.LoadDropDownList();
                if (userObj.RoleID == 7 || userObj.RoleID == 8)
                {
                    User user = UserManager.GetById(userObj.Id);
                    Employee employee = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    FillAcademicCalenderCombo();
                    FillFacultyCombo();
                    ddlFaculty.SelectedValue = Convert.ToString(employee.EmployeeID);
                    ddlFaculty.Enabled = false;
                }
                else
                {
                    LoadDropDown();
                }
            }
        }

        private void LoadDropDown()
        {
            FillFacultyCombo();
            FillAcademicCalenderCombo();
        }

        private void FillFacultyCombo()
        {
            List<Employee> employeeList = EmployeeManager.GetAll();
            if (employeeList.Count > 0)
            {
                employeeList = employeeList.OrderBy(o => o.Code).ToList();
                ddlFaculty.DataSource = employeeList;
                ddlFaculty.DataTextField = "Code";
                ddlFaculty.DataValueField = "EmployeeID";
                ddlFaculty.DataBind();
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FillAcademicCalenderCombo()
        {
            try
            {
                ddlSession.Items.Clear();
                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetCustom();

                ddlSession.Items.Add(new ListItem("-Select-", "0"));
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
            int facultyId = Convert.ToInt32(ddlFaculty.SelectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ddlSession.SelectedValue);

            LoadClassScheduleForFaculty(facultyId, programId, sessionId);
        }

        private void LoadClassScheduleForFaculty(int facultyId, int programId, int sessionId)
        {
            List<rClassScheduleForFaculty> list = ClassRoutineManager.GetClassScheduleForFaculty(facultyId, programId, sessionId);

            string program = ucProgram.selectedText;
            string session = ddlSession.SelectedItem.Text;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);

            if (list.Count != 0)
            {
                ClassScheduleFaculty.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptClassScheuleForFaculty.rdlc");
                this.ClassScheduleFaculty.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                ReportDataSource rds = new ReportDataSource("RptClassScheduleForFaculty", list);

                ClassScheduleFaculty.LocalReport.DataSources.Clear();
                ClassScheduleFaculty.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";              

            }
            else
            {
                ShowMessage("NO Data Found. Enter A Valid Program And Session.");
                return;
            }
        }

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            int facultyId = Convert.ToInt32(ddlFaculty.SelectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ddlSession.SelectedValue);

            string program = ucProgram.selectedText;
            string session = ddlSession.SelectedItem.Text;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);

            List<rClassScheduleForFaculty> list = ClassRoutineManager.GetClassScheduleForFaculty(facultyId, programId, sessionId);

            if (list.Count != 0)
            {
                ClassScheduleFaculty.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptClassScheuleForFaculty.rdlc");
                this.ClassScheduleFaculty.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                ReportDataSource rds = new ReportDataSource("RptClassScheduleForFaculty", list);

                ClassScheduleFaculty.LocalReport.DataSources.Clear();
                ClassScheduleFaculty.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = ClassScheduleFaculty.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ClassScheduleFaculty" + ".pdf"), FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                string path = Server.MapPath("~/Upload/ReportPDF/" + "ClassScheduleFaculty" + ".pdf");

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
                ShowMessage("NO Data Found. Enter A Valid Program And Session.");
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