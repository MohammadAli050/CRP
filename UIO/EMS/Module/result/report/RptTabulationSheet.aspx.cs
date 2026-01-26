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

namespace EMS.miu.result.report
{
    public partial class RptTabulationSheet : BasePage
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

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userId);
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            int programId = Convert.ToInt32(ucProgram.selectedValue);

        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            LoadBatchListByProgramIdSessionId(programId, acaCalId);
        }

        private void LoadBatchListByProgramIdSessionId(int programId, int acaCalId)
        {
            List<StudentCountProgramBatchWise> list = StudentManager.GetStudentCountProgramBatchWiseByAcaCalIdProgramId(programId, acaCalId);

            ddlBatchList.Items.Clear();
            ddlBatchList.AppendDataBoundItems = true;
            if (list != null)
            {
                ddlBatchList.Items.Add(new ListItem("All", ""));
                ddlBatchList.DataTextField = "BatchWiseCount";
                ddlBatchList.DataValueField = "BatchId";
                ddlBatchList.SelectedIndexChanged += new EventHandler(cbl_manual_clickEvent);
            
                ddlBatchList.DataSource = list;
                ddlBatchList.DataBind();
            }
            else
            {
                ddlBatchList.DataSource = null;
                ddlBatchList.DataBind();
            }
        }

        protected void cbl_manual_clickEvent(object sender, EventArgs e)
        {
            // do something with this click...
        }

        private void LoadStudentCheckBoxList(List<StudentRollOnly> list)
        {
            try
            {
                ddlStudentList.Items.Clear();
                ddlStudentList.AppendDataBoundItems = true;
                if (list != null)
                {
                    ddlStudentList.Items.Add(new ListItem("All", ""));
                    ddlStudentList.DataTextField = "Roll";
                    ddlStudentList.DataValueField = "Roll";
                    ddlStudentList.DataSource = list;
                    ddlStudentList.DataBind();
                }
                else
                {
                    ddlStudentList.DataSource = null;
                    ddlStudentList.DataBind();
                }
            }
            catch (Exception)
            { }
        }

        protected void ddlRunningStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void LoadMajor(int programId)
        {
            List<TabulationSheetMajor> majorList = ExamManager.GetMajor(programId);

            ddlMajor.Items.Clear();
            ddlMajor.AppendDataBoundItems = true;

            if (majorList != null)
            {
                ddlMajor.Items.Add(new ListItem("All", "0"));
                ddlMajor.DataTextField = "Name";
                ddlMajor.DataValueField = "ChildNodeID";


                ddlMajor.DataSource = majorList;
                ddlMajor.DataBind();
            }
        }

        protected void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);

                if (ddlBatchList.SelectedItem.Text == "All")
                {
                    foreach (ListItem item in ddlBatchList.Items)
                    {
                        item.Selected = true;
                    }
                    ddlBatchList.Items[0].Selected = false;
                }

                List<int> batchList = new List<int>();
                foreach (ListItem item in ddlBatchList.Items)
                {
                    if (item.Selected)
                    {
                        batchList.Add(Convert.ToInt32(item.Value));
                    }
                }


                LoadData(programId, acaCalId, batchList);
            }
            catch (Exception)
            { }
        }

        private void LoadData(int programId, int acaCalId, List<int> batchList)
        {
            try
            {
                List<GradeDetails> gradeList = GradeDetailsManager.GetAll();

                List<rStudentTabulationSheet> stdList = new List<rStudentTabulationSheet>();
                List<rStudentTabulationSheet> list = new List<rStudentTabulationSheet>();
                foreach (int item in batchList)
                {
                    list.AddRange(StudentManager.GetStudentTabulationSheetByProgramBatchSession(programId, item, acaCalId));
                }

                List<StudentRollOnly> stdList2 = new List<StudentRollOnly>(); 

                if (ddlStudentList.SelectedItem.Text == "All")
                {
                    foreach (ListItem item in ddlStudentList.Items)
                    {
                        item.Selected = true;
                    }
                    ddlStudentList.Items[0].Selected = false;
                }

                foreach (ListItem item in ddlStudentList.Items)
                {
                    if (item.Selected)
                    {
                        stdList.AddRange(list.Where(l => l.Roll == item.Value).ToList());
                    }
                }
                 

                string courses = "Course Code & Title: ";

                List<rStudentTabulationSheet> courseList = stdList.GroupBy(g => new { g.FormalCode, g.Title })
                         .Select(g => g.First())
                         .Where(g => string.IsNullOrEmpty(g.Title) == false).OrderBy(o=> o.FormalCode).ToList();
                int i = 1;
                foreach (rStudentTabulationSheet item in courseList)
                {
                    courses += i.ToString() + ". " + item.FormalCode + " : " + item.Title + " ";
                    i++;
                }


                if (stdList != null && stdList.Count > 0)
                {
                    ReportParameter p1 = new ReportParameter("Courses", courses);
                    ReportParameter p2 = new ReportParameter("Session", ucSession.selectedText);

                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/result/report/RptTabulationSheet.rdlc");
                    ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("StudentTabulationSheetDataset", stdList);
                    ReportDataSource rds2 = new ReportDataSource("GradeList", gradeList);

                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rds);
                    ReportViewer.LocalReport.DataSources.Add(rds2);
                    ReportViewer.Visible = true;
                }
                else
                {
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.Visible = false;
                }
            }
            catch (Exception)
            { }
        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }

        protected void btnLoadStudentRoll_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);

                if (ddlBatchList.SelectedItem.Text == "All")
                {
                    foreach (ListItem item in ddlBatchList.Items)
                    {
                        item.Selected = true;
                    }
                    ddlBatchList.Items[0].Selected = false;
                }

                List<int> batchList = new List<int>();
                foreach (ListItem item in ddlBatchList.Items)
                {
                    if (item.Selected)
                    {
                        batchList.Add(Convert.ToInt32(item.Value));
                    }
                }

                List<StudentRollOnly> stdList = new List<StudentRollOnly>();
                foreach (int item in batchList)
                {
                    stdList.AddRange(StudentManager.GetStudentListRollByProgramBatchSession(acaCalId, programId, item));
                }

                LoadStudentCheckBoxList(stdList);

            }
            catch (Exception)
            { }
             
        }
         


    }
}