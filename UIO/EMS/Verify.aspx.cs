using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;

namespace EMS.miu.ucam
{
    public partial class Verify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearMessage();
            string StudentId = txtStudentId.Text.Trim();
             int serialNo = 0;
            if (IsDigitsOnly(txtSerialNo.Text))
            serialNo =  string.IsNullOrEmpty(txtSerialNo.Text.Trim()) == true ? 0 : Convert.ToInt32(txtSerialNo.Text.Trim());

            if (txtStudentId.Text.Trim() != "" && (txtSerialNo.Text.Trim() != "" && serialNo == 0) )
            {
                ShowMessage("Please enter either student roll/Id or serial no ");
                return;
            }

            try
            {
                Student std = StudentManager.GetByRollOrSerialNo(StudentId,serialNo);

                if (std != null)
                {
                    if (std.BasicInfo.PhotoPath != null)
                    {
                        imgPhoto.ImageUrl = "~/Upload/Avatar/" + std.BasicInfo.PhotoPath;
                    }
                    else
                    {
                        if (std.BasicInfo.Gender.ToLower() == "female")
                            imgPhoto.ImageUrl = "~/Images/photoGirl.png";
                        else
                            imgPhoto.ImageUrl = "~/Images/photoBoy.png";
                    }
                    txtFatherName.Text = std.BasicInfo.FatherName;
                    txtFullName.Text = std.BasicInfo.FullName;
                    txtMotherName.Text = std.BasicInfo.MotherName;
                    txtCGPA.Text = std.LatestCGPA.ToString();
                    txtDegreeName.Text = std.Program.DegreeName;
                    txtEarnedCredit.Text = std.EarnedCr;
                    txtCompletionSemester.Text = std.CompletionSemester;

                }
                else
                {
                    Student _student = StudentManager.GetByRoll(StudentId);
                    if (_student != null)
                    {
                        ShowMessage("Degree is not yet verified for '"+StudentId.ToString()+"' *contact MIU Exam Office for details inquiry");
                    }
                    else if (serialNo != 0)
                    {
                        ShowMessage("Can not verify certificate serial no. '" + serialNo.ToString()+ "' *contact MIU Exam Office for details");
                    }
                    else
                    {
                        ShowMessage("Invalid Student Roll or ID ");
                    }
                }

            }
            catch (Exception)
            { }

        }

        private void ShowMessage(string message)
        {
            lblMsg.Text = message;
        }
        private void ClearMessage()
        {
            lblMsg.Text = "";
            txtFullName.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtCGPA.Text = "";
            txtCompletionSemester.Text = "";
            txtDegreeName.Text = "";
            txtEarnedCredit.Text = "";
        }
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}