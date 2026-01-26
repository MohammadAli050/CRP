using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace EMS.miu.admin.report
{
    public partial class CertificateGeneration : BasePage
    {
        BussinessObject.UIUMSUser BaseCurrentUserObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            BaseCurrentUserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!Page.IsPostBack)
            {
                LoadDropDown();
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearAll();
            ReportViewer1.Visible = false;
            try
            {
                string Roll = txtStudentRoll.Text.Trim();
                int Type = Convert.ToInt32(ddlType.SelectedValue);

                try
                {
                    Student std = StudentManager.GetByRoll(Roll);
                    if (std != null)
                    {
                        LoadCertificateInfo(Roll, Type);
                    }
                    else
                    {
                        ShowMessage("Enter A Valid StudentID", Color.Red);
                        return;
                    }
                }
                catch (Exception)
                {
                    ShowMessage("Enter A Valid StudentID", Color.Red);
                    return;
                }
            }
            catch (Exception)
            {
                ShowMessage("Somthing Went Wrong", Color.Red);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int Type = Convert.ToInt32(ddlType.SelectedValue);
            int serial = CertificatesManager.GenerateSerialNo(Type);
            txtSerial.Text = "" + serial.ToString().PadLeft(6, '0');
            txtSerial.ReadOnly = false;
            ReportViewer1.Visible = false;
            btnPreview.Enabled = true;
            btnSaveCancel.Enabled = false;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;


                string Roll = txtStudentRoll.Text.Trim();
                int Type = Convert.ToInt32(ddlType.SelectedValue);
                string duration = "";
                string gander = "";

                bool isPassed = CertificatesManager.EarnedCreditAndRequiredCreditByRoll(Roll);
                if (!isPassed)
                {
                    ShowMessage("Degree Required Credit is incomplete !", Color.Red);
                    txtSerial.Text = "";
                    txtStudentName.Text = "";
                    btnSaveCancel.Enabled = false;
                    btnSaveCancel.Text = "Save/Cancel";
                    btnPreview.Enabled = false;
                    btnGenerate.Enabled = false; 
                    ReportViewer1.Visible = false;
                    return;
                }

                if (CheckValidity(Roll, Convert.ToInt32(txtSerial.Text.Trim()), Type))
                {

                    Certificates _certificates = CertificatesManager.GetAllByRoll(Roll, Type);
                    CertificatesDTO cerDTO = CertificatesManager.CertificatesDTOGetByRoll(Roll);

                    string Name = ToInitcap(cerDTO.FullName);

                    if (cerDTO.Duration == 1)
                        duration = "one";
                    else if (cerDTO.Duration == 2)
                        duration = "two";
                    else if (cerDTO.Duration == 3)
                        duration = "three";
                    else if (cerDTO.Duration == 4)
                        duration = "four";
                    else if (cerDTO.Duration == 5)
                        duration = "five";
                    else if (cerDTO.Duration == 6)
                        duration = "six";

                    gander = cerDTO.Gender == "Male" ? "His" : "Her";

                    string description = "having successfully completed the prescribed " + duration +
                        " - year course of study obtained the degree of " + cerDTO.DegreeName + " in " + cerDTO.ProgramName +
                        " with honor at this University in the " + cerDTO.TypeName + " Semester of " + cerDTO.Year + ". " + gander
                         + " Cumulative Grade Point Average ";

                    string description2 = " was " + cerDTO.CGPA.ToString("#.00") + " in the scale of 4.00.";

                    ReportParameter p1 = new ReportParameter("SerialNo", txtSerial.Text);
                    ReportParameter p2 = new ReportParameter("StudentName", Name);
                    ReportParameter p3 = new ReportParameter("DegreeName", cerDTO.DegreeName);
                    ReportParameter p4 = new ReportParameter("Description", description);
                    ReportParameter p5 = new ReportParameter("Description2", description2);
                    ReportParameter p6 = new ReportParameter("Roll", Roll);

                    if (Type == 1)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/admin/report/RptProvisionalCertificate.rdlc");
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/admin/report/RptMainCertificate.rdlc");
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                    }
                    this.ReportViewer1.LocalReport.DisplayName = Roll;
                    this.ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.Visible = true;

                    btnSaveCancel.Enabled = true;
                    ShowMessage("", Color.Red);
                }
                else
                {
                    ShowMessage("This Serial No Is Invalid !", Color.Red);
                    ReportViewer1.Visible = false;
                    btnSaveCancel.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            string Roll = txtStudentRoll.Text.Trim();
            int Type = Convert.ToInt32(ddlType.SelectedValue);

            Student _student = StudentManager.GetByRoll(Roll);

            if (btnSaveCancel.Text == "Save")
            {
                Certificates _certificates = new Certificates();

                _certificates.SerialNo = Convert.ToInt32(txtSerial.Text);
                _certificates.StudentID = _student.StudentID;
                _certificates.IsCancelled = false;
                _certificates.TypeID = Type;
                _certificates.CreatedBy = BaseCurrentUserObj.Id;
                _certificates.CreatedDate = DateTime.Now;

                int id = CertificatesManager.Insert(_certificates);
                if (id != 0)
                {
                    LoadCertificateInfo(Roll, Type);
                    ShowMessage("Save Successfully", Color.Green);
                }
            }
            else
            {
                Certificates _certificates = CertificatesManager.GetAllByRoll(Roll, Type);
                _certificates.IsCancelled = true;
                _certificates.ModifiedBy = BaseCurrentUserObj.Id;
                _certificates.ModifiedDate = DateTime.Now;
                bool isUpdate = CertificatesManager.Update(_certificates);
                if (isUpdate)
                {
                    ShowMessage("Update Successfully", Color.Green);
                }
            }



        }

        private void LoadCertificateInfo(string Roll, int Type) 
        {
            try
            {
                bool isPassed = CertificatesManager.EarnedCreditAndRequiredCreditByRoll(Roll);

                if (!isPassed)
                {
                    ShowMessage("Degree Required Credit is incomplete !", Color.Red);
                    txtSerial.Text = "";
                    txtStudentName.Text = "";
                    btnSaveCancel.Enabled = false;
                    btnSaveCancel.Text = "Save/Cancel";
                    btnPreview.Enabled = false;
                    btnGenerate.Enabled = false;
                    return;
                }

                Certificates _certificates = CertificatesManager.GetAllByRoll(Roll, Type);
                CertificatesDTO cerDTO = CertificatesManager.CertificatesDTOGetByRoll(Roll);

                if (_certificates != null)
                {
                    txtSerial.Text = "" + _certificates.SerialNo.ToString().PadLeft(6, '0');
                    btnSaveCancel.Text = "Cancel Certificate";

                    txtSerial.ReadOnly = true;
                    btnGenerate.Enabled = false;
                    btnPreview.Enabled = true;
                    btnSaveCancel.Enabled = true;
                }
                else
                {
                    txtSerial.Text = "";
                    txtSerial.ReadOnly = true;
                    btnSaveCancel.Text = "Save";
                    btnGenerate.Enabled = true;
                    btnPreview.Enabled = false;
                    btnSaveCancel.Enabled = false;
                }

                txtStudentName.Text = cerDTO.FullName;
                ShowMessage("", Color.Red);
            }
            catch (Exception)
            { }
        }

        private bool CheckValidity(string Roll,int serial,int TypeID)
        {
            bool isValid = CertificatesManager.CheckValidity(Roll, serial, TypeID);
            return isValid;
        }

        public string ToInitcap(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            char[] charArray = new char[str.Length];
            bool newWord = true;
            for (int i = 0; i < str.Length; ++i)
            {
                Char currentChar = str[i];
                if (Char.IsLetter(currentChar))
                {
                    if (newWord)
                    {
                        newWord = false;
                        currentChar = Char.ToUpper(currentChar);
                    }
                    else
                    {
                        currentChar = Char.ToLower(currentChar);
                    }
                }
                else if (Char.IsWhiteSpace(currentChar))
                {
                    newWord = true;
                }
                charArray[i] = currentChar;
            }
            return new string(charArray);
        }

        private void LoadDropDown()
        {
            ddlType.Items.Add(new ListItem("Provisional", "1"));
            ddlType.Items.Add(new ListItem("Main", "2"));
        }

        private void ShowMessage(string message, Color color)
        {
            lblMessage.Text = string.Empty;
            lblMessage.Text = message;
            lblMessage.ForeColor = color;
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }

        private void ClearAll()
        {
            ShowMessage("", Color.Red);
        }

        //protected void btnCheckValidity_Click(object sender, EventArgs e)
        //{

        //}

    }
}