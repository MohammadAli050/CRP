using CommonUtility;
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

namespace EMS.miu.bill
{
    public partial class StudentScholarship : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            lblMsg.Text = "";

            if (!IsPostBack)
            {
                chkSGPA.Checked = true;
                LoadComboBox();
            }
        }

        #region Function

        protected void LoadComboBox()
        {
            try
            {
                ddlAcademicCalender.Items.Clear();
                ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));

                LoadProgram();
                LoadCalenderType();
            }
            catch { }
            finally { }
        }

        protected void LoadProgram()
        {
            try
            {
                ddlProgram.Items.Clear();
                ddlProgram.AppendDataBoundItems = true;

                List<Program> programList = ProgramManager.GetAll();

                if (programList != null)
                {
                    ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                    ddlProgram.DataValueField = "ProgramID";
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataBind();
                }

                Program_Changed(null, null);
            }
            catch { }
            finally { }
        }

        protected void LoadCalenderType()
        {
            try
            {
                ddlCalenderType.Items.Clear();

                List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

                if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
                {
                    ddlCalenderType.DataSource = calenderUnitMasterList;
                    ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                    ddlCalenderType.DataTextField = "Name";
                    ddlCalenderType.DataBind();
                }
            }
            catch { }
            finally
            {
                CalenderType_Changed(null, null);
            }
        }

        protected void LoadAcademicCalender(int calenderTypeId)
        {
            try
            {
                ddlAcademicCalender.Items.Clear();
                ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
                ddlAcademicCalender.AppendDataBoundItems = true;

                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

                if (academicCalenderList.Count > 0 && academicCalenderList != null)
                {
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                    academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
                    ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                }
            }
            catch { }
            finally
            {
                AcademicCalender_Changed(null, null);
            }
        }

        #endregion

        #region Event

        protected void CalenderType_Changed(Object sender, EventArgs e)
        {
            try
            {
                int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                LoadAcademicCalender(calenderTypeId);
            }
            catch { }
        }

        protected void AcademicCalender_Changed(Object sender, EventArgs e)
        {
            try
            {
            }
            catch { }
           
        }

        protected void Program_Changed(Object sender, EventArgs e)
        {
            try
            {
                ucBatch.LoadDropDownList(Convert.ToInt32(ddlProgram.SelectedValue));
            }
            catch { }
           
        }

        protected void OnClick_LoadMeritList(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);

            LoadStudentMeritList(acaCalId, programId, batchId);
        }

        private void LoadStudentMeritList(int acaCalId, int programId, int batchId)
        {
            string session = ddlAcademicCalender.SelectedItem.Text;
            string program = ddlProgram.SelectedItem.Text;
            string batch = ucBatch.selectedText;

            ReportParameter p1 = new ReportParameter("Show_Session", session);
            ReportParameter p2 = new ReportParameter("Show_Program", program);
            ReportParameter p3 = new ReportParameter("Show_Batch", batch);
            ReportParameter p4 = new ReportParameter("SGPA", chkSGPA.Checked.ToString());
            ReportParameter p5 = new ReportParameter("CGPA", chkCGPA.Checked.ToString());

            List<StudentMeritListForScholarship> list = ScholarshipListManager.GetStudentMeritListForScholarship(acaCalId, programId, batchId);

            if (list.Count != 0)
            {
                StudentMeritListForScholarship.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/XlStudentMeritListForScholarship.rdlc");
                this.StudentMeritListForScholarship.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5});
                ReportDataSource rds = new ReportDataSource("StudentMeritListForScholarship", list);

                StudentMeritListForScholarship.LocalReport.DataSources.Clear();
                StudentMeritListForScholarship.LocalReport.DataSources.Add(rds);
                lblMsg.Text = "";

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = StudentMeritListForScholarship.LocalReport.Render("Excel", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportEXCEL/" + "StudentMeritListForScholarship" + ".xls"), FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                string path = Server.MapPath("~/Upload/ReportEXCEL/" + "StudentMeritListForScholarship" + ".xls");

                WebClient client = new WebClient();   // Open Excel File in Web Browser 

                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    //Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.AddHeader("content-disposition", " filename = StudentMeritListForScholarship.xls");
                    Response.BinaryWrite(buffer);
                } 
            }
            else
            {
                lblMsg.Text = "NO Data Found. Enter Valid Session, Program And Batch";
                return;
            }
        }

        #endregion

    }
}