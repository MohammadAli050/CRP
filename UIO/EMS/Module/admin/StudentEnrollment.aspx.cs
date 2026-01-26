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
    public partial class StudentEnrollment : BasePage
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

                string AdmissionTestRoll = txtAdmissionTestRoll.Text.Trim();

                List<CandidatePersonalInfo> personalInfolist = StudentEnrollManager.GetCandidatePersonalInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateAddtionalInfo> additionalInfolist = StudentEnrollManager.GetCandidateAddtionalInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateExamInfo> examInfolist = StudentEnrollManager.GetCandidateExamInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateAddressInfo> addressInfolist = StudentEnrollManager.GetCandidateAddressInfoByAdmissionTestRoll(AdmissionTestRoll);


                if (personalInfolist != null && personalInfolist.Count > 0)
                {
                    pnlInfo.Visible = true;

                    txtStudentName.Text = personalInfolist[0].FirstName;
                    txtBirthDate.Text = personalInfolist[0].BirthDate.ToString("dd-MMM-yy");
                    txtBloodGroup.Text = personalInfolist[0].BloodGroup;
                    txtMeritalStutus.Text = personalInfolist[0].MaritalStatus;
                    txtStudentEmail.Text = personalInfolist[0].Email;
                    txtStudentMobile.Text = personalInfolist[0].Phone;


                    if (additionalInfolist != null && additionalInfolist.Count > 0)
                    {
                        txtFatherName.Text = additionalInfolist[0].FatherName;
                        txtFatherOccupation.Text = additionalInfolist[0].FatherOccupation;
                        txtFatherOfficialAddress.Text = additionalInfolist[0].FatherMailingAddress;
                        txtFathersTel.Text = additionalInfolist[0].FatherLandPhone;
                        txtFathersMob.Text = additionalInfolist[0].FatherMobile;

                        txtMotherName.Text = additionalInfolist[0].MotherName;
                        txtMotherOccupation.Text = additionalInfolist[0].MotherOccupation;
                        txtMotherOfficialAddress.Text = additionalInfolist[0].MotherMailingAddress;
                        txtMothersTel.Text = additionalInfolist[0].MotherLandPhone;
                        txtMotherMob.Text = additionalInfolist[0].MotherMobile;

                        txtLocalGuardianName.Text = additionalInfolist[0].GuardianName;
                        txtRelation.Text = additionalInfolist[0].GuardianRelation;
                        txtLocalTele.Text = additionalInfolist[0].GuardianLandPhone;
                        txtLocalMob.Text = additionalInfolist[0].GuardianMobile;
                        txtLocalEmailAddress.Text = additionalInfolist[0].GuardianEmail;

                    }


                    if (addressInfolist != null && addressInfolist.Count > 0)
                    {
                        addressInfolist = addressInfolist.OrderBy(s => s.AddressTypeId).ToList();
                        if (addressInfolist.Count == 2)
                        {
                            txtPresentAddress.Text = addressInfolist[0].AddressLine;
                            txtPermanentAddress.Text = addressInfolist[1].AddressLine;
                        }
                        else
                        {
                            if (addressInfolist[0].AddressTypeId == 1)
                            {
                                txtPresentAddress.Text = addressInfolist[0].AddressLine;
                            }
                            else
                            {
                                txtPermanentAddress.Text = addressInfolist[0].AddressLine;
                            }
                        }
                    }


                    if (examInfolist != null && examInfolist.Count > 0)
                    {
                        gvExamList.DataSource = examInfolist;
                        gvExamList.DataBind();
                    }
                    else
                    {
                        gvExamList.DataSource = null;
                        gvExamList.DataBind();
                    }


                    //ReportViewer.LocalReport.ReportPath = Server.MapPath("~/miu/admin/AdmissionForm.rdlc");

                    //ReportDataSource rds = new ReportDataSource("PersonalInfo", personalInfolist);
                    //ReportDataSource rds2 = new ReportDataSource("AdditionalInfo", additionalInfolist);
                    //ReportDataSource rds3 = new ReportDataSource("ExamInfo", examInfolist);
                    //ReportDataSource rds4 = new ReportDataSource("AddressInfo", addressInfolist);

                    //ReportViewer.LocalReport.DataSources.Clear();
                    //ReportViewer.LocalReport.DataSources.Add(rds);
                    //ReportViewer.LocalReport.DataSources.Add(rds2);
                    //ReportViewer.LocalReport.DataSources.Add(rds3);
                    //ReportViewer.LocalReport.DataSources.Add(rds4);
                    //ReportViewer.Visible = true;
                }
                else
                {
                    //ReportViewer.LocalReport.DataSources.Clear();
                    //ReportViewer.Visible = false;
                    pnlInfo.Visible = false;
                }

            }
            catch (Exception)
            { }
        }

        protected void btnEnrollStudent_Click(object sender, EventArgs e)
        {
            try
            {
                string AdmissionTestRoll = txtAdmissionTestRoll.Text.Trim();
                List<CandidatePersonalInfo> personalInfolist = StudentEnrollManager.GetCandidatePersonalInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateAddtionalInfo> additionalInfolist = StudentEnrollManager.GetCandidateAddtionalInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateExamInfo> examInfolist = StudentEnrollManager.GetCandidateExamInfoByAdmissionTestRoll(AdmissionTestRoll);
                List<CandidateAddressInfo> addressInfolist = StudentEnrollManager.GetCandidateAddressInfoByAdmissionTestRoll(AdmissionTestRoll);

                bool isRollValid = StudentEnrollManager.IsValidRoll(txtStudentRoll.Text.Trim());

                if (isRollValid == true)
                {

                    if (personalInfolist != null && personalInfolist.Count > 0)
                    {
                        LogicLayer.BusinessObjects.Person person = new LogicLayer.BusinessObjects.Person();

                        person.FullName = personalInfolist[0].FirstName;
                        person.DOB = personalInfolist[0].BirthDate;
                        person.Email = personalInfolist[0].Email;
                        person.Gender = personalInfolist[0].Gender;
                        person.MatrialStatus = personalInfolist[0].MaritalStatus;
                        person.BloodGroup = personalInfolist[0].BloodGroup;
                        person.Phone = personalInfolist[0].Phone;

                        if (additionalInfolist != null && additionalInfolist.Count > 0)
                        {
                            person.FatherName = additionalInfolist[0].FatherName;
                            person.FatherProfession = additionalInfolist[0].FatherOccupation;

                            person.MotherName = additionalInfolist[0].MotherName;
                            person.MotherProfession = additionalInfolist[0].MotherOccupation;

                            person.GuardianName = additionalInfolist[0].GuardianName;

                        }

                        person.CreatedBy = BaseCurrentUserObj.Id;
                        person.CreatedDate = DateTime.Now;

                        int id = PersonManager.Insert(person);

                        if (id != 0)
                        {
                            LogicLayer.BusinessObjects.Student student = new LogicLayer.BusinessObjects.Student();

                            student.Roll = txtStudentRoll.Text.Trim();
                            student.PersonID = id;
                            student.BatchId = personalInfolist[0].BatchId;
                            student.ProgramID = personalInfolist[0].ProgramID;
                            student.SessionId = personalInfolist[0].AcaCalID;
                            student.CandidateId = personalInfolist[0].CandidateID;

                            student.CreatedBy = BaseCurrentUserObj.Id;
                            student.CreatedDate = DateTime.Now;

                            int stdid = StudentManager.Insert(student);
                            if (stdid != 0)
                            {
                                ClearAllField();
                                ShowMessage("Enrolled Successful", Color.Red);
                            }

                        }
                        else
                        {
                            ShowMessage("Unsuccessful! Please Contact with Admin", Color.Red);
                        }

                    }
                    else
                    {
                        ShowMessage("Invalid Enrolment! Contact with Admin", Color.Red);
                    }

                }
                else
                {
                    ShowMessage("Invalid Roll!", Color.Red);
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
                txtBirthDate.Text = "";
                txtBloodGroup.Text = "";
                txtMeritalStutus.Text = "";
                txtStudentEmail.Text = "";
                txtStudentMobile.Text = "";

                txtFatherName.Text = "";
                txtFatherOccupation.Text = "";
                txtFatherOfficialAddress.Text = "";
                txtFathersTel.Text = "";
                txtFathersMob.Text = "";

                txtMotherName.Text = "";
                txtMotherOccupation.Text = "";
                txtMotherOfficialAddress.Text = "";
                txtMothersTel.Text = "";
                txtMotherMob.Text = "";

                txtLocalGuardianName.Text = "";
                txtRelation.Text = "";
                txtLocalTele.Text = "";
                txtLocalMob.Text = "";
                txtLocalEmailAddress.Text = "";



                txtPresentAddress.Text = "";
                txtPermanentAddress.Text = "";

                gvExamList.DataSource = null;
                gvExamList.DataBind();

                ShowMessage("", Color.Red);

                pnlInfo.Visible = false;
            }
            catch (Exception)
            { }
        }
    }
}