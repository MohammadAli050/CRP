using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects.RO;

namespace EMS.miu.result.report
{
    public partial class RptMeritList : BasePage
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
                
                if (programId != 0)
                {
                    int acaCalId = Convert.ToInt32(ucSession.selectedValue);

                    int batchId = Convert.ToInt32(ucBatch.selectedValue);
                    LoadData(programId, batchId, acaCalId);

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

        private void LoadData(int programId, int batchId, int acaCalId)
        {
            try
            {
                List<rStudentMeritList> rStudentMeritList = StudentACUDetailManager.GetMeritListByProgramSessionBatch(programId, acaCalId, batchId);

                if (rStudentMeritList != null && rStudentMeritList.Count > 0)
                {
                    ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
                    ReportParameter p2 = new ReportParameter("Session", ucSession.selectedText);
                    ReportParameter p3 = new ReportParameter("Batch", ucBatch.selectedText);
                    

                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptStudentGPACGPAProgramWise.rdlc");
                    ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3});
                    ReportDataSource rds = new ReportDataSource("StudentMeritListDataset", rStudentMeritList);

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

    }
}