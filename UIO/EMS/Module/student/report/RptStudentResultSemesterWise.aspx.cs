using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects;
using System.Drawing;

namespace EMS.miu.student.report
{
    public partial class RptStudentResultSemesterWise : BasePage
    {
        int userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;

            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;

            lblMsg.Text = "";

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userId);
                LoadSemesterDropDown();
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMessage("", Color.Red);

                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int SemesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                if (programId != 0)
                {
                    int acaCalId = Convert.ToInt32(ucSession.selectedValue);

                    int batchId = Convert.ToInt32(ucBatch.selectedValue);
                    LoadData(programId, batchId, acaCalId, SemesterNo);

                }
                else
                {
                    ShowMessage("Please Select Program.", Color.Red);
                    ReportViewer.Visible = false;
                }

            }
            catch (Exception)
            {

            }
        }

        protected void ddlSemesterNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));

        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSemesterNo.SelectedIndex = 0;
        }

        private void LoadData(int programId, int batchId, int acaCalId, int SemesterNo)
        {
            try
            {
                List<rCreditGPA> creditGPAList = ExamMarkManager.GetGradeReportCreditGPAByRoll(programId, batchId, "").Where(s => s.Roll != null).ToList();

                if (creditGPAList != null && creditGPAList.Count > 0)
                {
                    string CalenderMasterType = CalenderUnitMasterManager.GetById(ProgramManager.GetById(programId).CalenderUnitMasterID).Name;

                    ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
                    ReportParameter p2 = new ReportParameter("Batch", ucBatch.selectedText);
                    ReportParameter p3 = new ReportParameter("Semester", ddlSemesterNo.SelectedItem.Text);
                    ReportParameter p4 = new ReportParameter("CalenderMasterType", CalenderMasterType);

                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentResultSemesterWise.rdlc");
                    ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
                    ReportDataSource rds = new ReportDataSource("StudentResultDataset", creditGPAList);

                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rds);
                    ReportViewer.Visible = true;
                }
                else
                {
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.Visible = false;
                    ShowMessage("No Data Found!", Color.Red);
                }
            }
            catch (Exception)
            { }
        }

        private void ShowMessage(string Message, Color color)
        {
            lblMsg.Text = Message;
            lblMsg.ForeColor = color;
        }

        private void LoadSemesterDropDown()
        {
            ddlSemesterNo.Items.Clear();
            ddlSemesterNo.Items.Add(new ListItem("All", "0"));
            ddlSemesterNo.Items.Add(new ListItem("1", "1"));
            ddlSemesterNo.Items.Add(new ListItem("2", "2"));
            ddlSemesterNo.Items.Add(new ListItem("3", "3"));
            ddlSemesterNo.Items.Add(new ListItem("4", "4"));
            ddlSemesterNo.Items.Add(new ListItem("5", "5"));
            ddlSemesterNo.Items.Add(new ListItem("6", "6"));
            ddlSemesterNo.Items.Add(new ListItem("7", "7"));
            ddlSemesterNo.Items.Add(new ListItem("8", "8"));
            ddlSemesterNo.Items.Add(new ListItem("9", "9"));
            ddlSemesterNo.Items.Add(new ListItem("10", "10"));
            ddlSemesterNo.Items.Add(new ListItem("11", "11"));
            ddlSemesterNo.Items.Add(new ListItem("12", "12"));

        }

    }
}