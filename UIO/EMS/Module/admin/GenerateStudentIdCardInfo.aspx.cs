using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.admin
{
    public partial class GenerateStudentIdCardInfo : BasePage
    {
        UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                DateTime dd = DateTime.Now;
                string date = dd.ToString("MM/dd/yyyy");
                ClearAllField();
            }
            lblMsg.Text = null;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllField();
                
                string Roll = txtStudentRoll.Text.Trim(); 

                List<StudentIdCardInfo> list = StudentManager.GetStudentIdCardInfoByRoll(Roll);
                if (list != null && list.Count > 0)
                {
                    pnlInfo.Visible = true;
                    btnGenerateIDCard.Visible = true;
                    txtStudentName.Text = list[0].FullName;
                    txtBloodGroup.Text = list[0].BloodGroup;
                }
                else
                {
                    ShowMessage("No Data Found!",Color.Red);
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

        private void ClearAllField()
        {
            try
            {
                txtStudentName.Text = "";

                txtValidationDate.Text = "";
                txtBloodGroup.Text = "";
                txtStudentId.Text = ""; 
                  
                ShowMessage("", Color.Red);

                pnlInfo.Visible = false;
                btnGenerateIDCard.Visible = false;
            }
            catch (Exception)
            { }
        }

        protected void btnGenerateIDCard_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllField();

                string Roll = txtStudentRoll.Text.Trim();
                string Validity = txtValidationDate.Text.Trim();
                string StudentIDNo = txtStudentId.Text.Trim();
                string Session = txtSession.Text.Trim();

                List<StudentIdCardInfo> list = StudentManager.GetStudentIdCardInfoByRoll(Roll);

                if (list != null && list.Count>0)
                { 
                    ReportViewer.LocalReport.EnableExternalImages = true;
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/miu/admin/StudenIdCard.rdlc");

                    List<ReportParameter> param1 = new List<ReportParameter>();

                    //param1.Add(new ReportParameter("Path", imgUrl));
                    //param1.Add(new ReportParameter("PathSignature", signatureUrl));
                    param1.Add(new ReportParameter("Validity", Validity));
                    param1.Add(new ReportParameter("StudentIDNo", StudentIDNo));
                    param1.Add(new ReportParameter("Session", Session));


                    ReportViewer.LocalReport.SetParameters(param1);


                    ReportDataSource rds = new ReportDataSource("StudentIdCardInfo", list);

                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rds);
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
    }
}