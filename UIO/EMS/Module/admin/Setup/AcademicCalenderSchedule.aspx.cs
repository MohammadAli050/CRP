using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Setup_AcademicCalenderSchedule : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack)
        {
            LoadGrid();
            lblMessage.Visible = true;
            lblMessage.Text = "";
        }

    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        this.ModalPopupExtender1.Show();
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int id = int.Parse(btn.CommandArgument.ToString());
        AcademicCalenderScheduleManager.Delete(id);
        LoadGrid();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int programId =  Convert.ToInt32(ucProgram.selectedValue);
            if ((programId != 0 ) && (sessionId != 0 ))
            {
                Insert();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select a progarm and session";
            }
            btnInsert.CommandArgument = "";
            btnInsert.Text = "Insert";
        }
        catch (Exception ex)
        {
        }
        finally
        {
            ClearField();
            LoadGrid();
            this.ModalPopupExtender1.Show();
        }
    }

    private void Insert()
    {
        try
        {
            AcademicCalenderSchedule acacalshedule = null;
            if (btnInsert.Text != "Update")
            {
                acacalshedule = new AcademicCalenderSchedule();

                acacalshedule.AcademicCalenderID = Convert.ToInt32(ucSession.selectedValue);
                acacalshedule.ProgramId = Convert.ToInt32(ucProgram.selectedValue);
                //DateTime.ParseExact(line[i], "M/d/yyyy h:mm", CultureInfo.InvariantCulture);
                acacalshedule.ClassStartDate = string.IsNullOrEmpty(clrClassStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrClassStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.ClassEndDate = string.IsNullOrEmpty(clrClassEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrClassEnd.Text.Trim(), "dd/MM/yyyy", null); 
                if (!CheckStartDateEndDate(acacalshedule.ClassStartDate, acacalshedule.ClassEndDate)) 
                { 
                    throw new Exception ("Class start date is larger then class end date.");
                }
                acacalshedule.AdvisingStartDate = string.IsNullOrEmpty(clrAdvisingStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdvisingStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AdvisingEnd = string.IsNullOrEmpty(clrAdvisingEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdvisingEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.AdvisingStartDate, acacalshedule.AdvisingEnd))
                {
                    throw new Exception("Advising start date is larger then end date.");
                }
                acacalshedule.MidExamStartDate = string.IsNullOrEmpty(clrMidExamStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMidExamStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.MidExamEndDate = string.IsNullOrEmpty(clrMidExamEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMidExamEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.MidExamStartDate, acacalshedule.MidExamEndDate))
                {
                    throw new Exception("Mid start date is larger thenend date.");
                }
                acacalshedule.FinalExamDate = string.IsNullOrEmpty(clrFinalExamStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrFinalExamStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.FinalEndDate = string.IsNullOrEmpty(clrFinalExamend.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrFinalExamend.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.FinalExamDate, acacalshedule.FinalEndDate))
                {
                    throw new Exception("Final exam start date is larger then end date.");
                }
                acacalshedule.MarkSheetSubmissionLastDate = string.IsNullOrEmpty(clrMarkSheeetSubmission.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMarkSheeetSubmission.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AnswerScriptSubmissionLastDate = string.IsNullOrEmpty(clrAnswerScriptSubmission.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAnswerScriptSubmission.Text.Trim(), "dd/MM/yyyy", null);
                
                acacalshedule.ResultPublicationDate = string.IsNullOrEmpty(clrResultPublicaionDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrResultPublicaionDate.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationPamentDateWithoutFine = string.IsNullOrEmpty(clrRegPaymentWithoutFine.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegPaymentWithoutFine.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.OrientationDate = string.IsNullOrEmpty(clrOrientationDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrOrientationDate.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationPamentDateWithFine = string.IsNullOrEmpty(clrRegPaymentWithFine.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegPaymentWithFine.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationStartDate = string.IsNullOrEmpty(clrRegistrationStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegistrationStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationEndDate = string.IsNullOrEmpty(clrRegistrationEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegistrationEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.RegistrationStartDate, acacalshedule.RegistrationEndDate))
                {
                    throw new Exception("Registration start date is larger then end date.");
                }
                acacalshedule.SessionVacationStartDate = string.IsNullOrEmpty(clrSessionVacationStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrSessionVacationStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.SessionVacationEndDate = string.IsNullOrEmpty(clrSessionVacationEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrSessionVacationEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.SessionVacationStartDate, acacalshedule.SessionVacationEndDate))
                {
                    throw new Exception("Session vacation start date is larger then end date.");
                }
                acacalshedule.AdmissionStartDate = DateTime.ParseExact(clrAdmissionStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AdmissionEndDate = string.IsNullOrEmpty(clrAdmissionEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdmissionEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.AdmissionStartDate, acacalshedule.AdmissionEndDate))
                {
                    throw new Exception("Admission start date is larger then end date.");
                }
                acacalshedule.CreatedBy = -1;
                acacalshedule.CreatedDate = DateTime.Now;
                acacalshedule.ModifiedBy = -1;
                acacalshedule.ModifiedDate = DateTime.Now;

                if (acacalshedule != null)
                {
                    int id = AcademicCalenderScheduleManager.Insert(acacalshedule);
                    if (id > 0)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Academic calender schedule inserted successfully.";
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Academic calender schedule could not inserted successfully.";
                    }
                }
                else 
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Academic calender schedule could not inserted successfully..";
                }
            }
            else
            {
                acacalshedule = AcademicCalenderScheduleManager.GetById(Convert.ToInt32(btnInsert.CommandArgument));

                //acacalshedule.AcademicCalenderScheduleId = Convert.ToInt32(btnInsert.CommandArgument);
                acacalshedule.AcademicCalenderID = Convert.ToInt32(ucSession.selectedValue);
                acacalshedule.ProgramId = Convert.ToInt32(ucProgram.selectedValue);
                acacalshedule.ClassStartDate = string.IsNullOrEmpty(clrClassStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrClassStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.ClassEndDate = string.IsNullOrEmpty(clrClassEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrClassEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.ClassStartDate, acacalshedule.ClassEndDate))
                {
                    throw new Exception("Class start date is larger then class end date.");
                }
                acacalshedule.AdvisingStartDate = string.IsNullOrEmpty(clrAdvisingStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdvisingStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AdvisingEnd = string.IsNullOrEmpty(clrAdvisingEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdvisingEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.AdvisingStartDate, acacalshedule.AdvisingEnd))
                {
                    throw new Exception("Advising start date is larger then end date.");
                }
                acacalshedule.MidExamStartDate = string.IsNullOrEmpty(clrMidExamStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMidExamStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.MidExamEndDate = string.IsNullOrEmpty(clrMidExamEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMidExamEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.MidExamStartDate, acacalshedule.MidExamEndDate))
                {
                    throw new Exception("Mid start date is larger thenend date.");
                }
                acacalshedule.FinalExamDate = string.IsNullOrEmpty(clrFinalExamStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrFinalExamStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.FinalEndDate = string.IsNullOrEmpty(clrFinalExamend.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrFinalExamend.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.FinalExamDate, acacalshedule.FinalEndDate))
                {
                    throw new Exception("Final exam start date is larger then end date.");
                }
                acacalshedule.MarkSheetSubmissionLastDate = string.IsNullOrEmpty(clrMarkSheeetSubmission.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrMarkSheeetSubmission.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AnswerScriptSubmissionLastDate = string.IsNullOrEmpty(clrAnswerScriptSubmission.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAnswerScriptSubmission.Text.Trim(), "dd/MM/yyyy", null);

                acacalshedule.ResultPublicationDate = string.IsNullOrEmpty(clrResultPublicaionDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrResultPublicaionDate.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationPamentDateWithoutFine = string.IsNullOrEmpty(clrRegPaymentWithoutFine.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegPaymentWithoutFine.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.OrientationDate = string.IsNullOrEmpty(clrOrientationDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrOrientationDate.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationPamentDateWithFine = string.IsNullOrEmpty(clrRegPaymentWithFine.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegPaymentWithFine.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationStartDate = string.IsNullOrEmpty(clrRegistrationStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegistrationStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.RegistrationEndDate = string.IsNullOrEmpty(clrRegistrationEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrRegistrationEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.RegistrationStartDate, acacalshedule.RegistrationEndDate))
                {
                    throw new Exception("Registration start date is larger then end date.");
                }
                acacalshedule.SessionVacationStartDate = string.IsNullOrEmpty(clrSessionVacationStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrSessionVacationStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.SessionVacationEndDate = string.IsNullOrEmpty(clrSessionVacationEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrSessionVacationEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.SessionVacationStartDate, acacalshedule.SessionVacationEndDate))
                {
                    throw new Exception("Session vacation start date is larger then end date.");
                }
                acacalshedule.AdmissionStartDate = string.IsNullOrEmpty(clrAdmissionStart.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdmissionStart.Text.Trim(), "dd/MM/yyyy", null);
                acacalshedule.AdmissionEndDate = string.IsNullOrEmpty(clrAdmissionEnd.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(clrAdmissionEnd.Text.Trim(), "dd/MM/yyyy", null);
                if (!CheckStartDateEndDate(acacalshedule.AdmissionStartDate, acacalshedule.AdmissionEndDate))
                {
                    throw new Exception("Admission start date is larger then end date.");
                }
                acacalshedule.ModifiedBy = -1;
                acacalshedule.ModifiedDate = DateTime.Now;

                
                if (AcademicCalenderScheduleManager.Update(acacalshedule))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Academic calender schedule updated successfully.";
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Academic calender schedule could not updated successfully.";
                }

            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = ex.Message;
        }
    }

    private bool CheckStartDateEndDate(DateTime startDate, DateTime endDate) 
    {
        if (startDate < endDate) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());

            AcademicCalenderSchedule acacalshedule = AcademicCalenderScheduleManager.GetById(id);

            clrAdmissionStart.Text =Convert.ToString(acacalshedule.AdmissionStartDate.Date);
            clrAdmissionEnd.Text = Convert.ToString(acacalshedule.AdmissionEndDate.Date);
            clrRegistrationStart.Text = Convert.ToString(acacalshedule.RegistrationStartDate.Date);
            clrRegistrationEnd.Text = Convert.ToString(acacalshedule.RegistrationEndDate.Date);
            clrRegPaymentWithoutFine.Text = Convert.ToString(acacalshedule.RegistrationPamentDateWithoutFine.Date);
            clrRegPaymentWithFine.Text = Convert.ToString(acacalshedule.RegistrationPamentDateWithFine.Date);
            clrOrientationDate.Text = Convert.ToString(acacalshedule.OrientationDate.Date);
            clrResultPublicaionDate.Text = Convert.ToString(acacalshedule.ResultPublicationDate.Date);
            clrClassStart.Text = Convert.ToString(acacalshedule.ClassStartDate.Date);
            clrClassEnd.Text = Convert.ToString(acacalshedule.ClassEndDate.Date);
            clrAdvisingStart.Text = Convert.ToString(acacalshedule.AdvisingStartDate.Date);
            clrAdvisingEnd.Text = Convert.ToString(acacalshedule.AdvisingEnd.Date);
            clrMidExamStart.Text = Convert.ToString(acacalshedule.MidExamStartDate.Date);
            clrMidExamEnd.Text = Convert.ToString(acacalshedule.MidExamEndDate.Date);
            clrFinalExamStart.Text = Convert.ToString(acacalshedule.FinalExamDate.Date);
            clrFinalExamend.Text = Convert.ToString(acacalshedule.FinalEndDate.Date);
            clrMarkSheeetSubmission.Text = Convert.ToString(acacalshedule.MarkSheetSubmissionLastDate.Date);
            clrAnswerScriptSubmission.Text = Convert.ToString(acacalshedule.AnswerScriptSubmissionLastDate.Date);
            clrSessionVacationStart.Text = Convert.ToString(acacalshedule.SessionVacationStartDate.Date);
            clrSessionVacationEnd.Text = Convert.ToString(acacalshedule.SessionVacationEndDate.Date);

            btnInsert.CommandArgument = id.ToString();
            btnInsert.Text = "Update";

            this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {

        }
    }

    private void ClearField()
    {
        ucProgram.selectedValue = "0";
        ucSession.selectedValue = "0";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearField();
        this.ModalPopupExtender1.Show();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearField();
    }
   
    private void LoadGrid()
    {
        List<AcademicCalenderSchedule> list = AcademicCalenderScheduleManager.GetAll();

        gvAcaCalShedule.DataSource = list;
        gvAcaCalShedule.DataBind();
    }

    protected void btnAddAndNext_Click(object sender, EventArgs e)
    {
        try
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            if ((programId != 0) && (sessionId != 0))
            {
                Insert();
            }
            else 
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select a progarm and session";
            }
            btnInsert.CommandArgument = "";
            btnInsert.Text = "Insert";
        }
        catch (Exception ex)
        {
        }
        finally
        {
            ClearField();
            LoadGrid();
            this.ModalPopupExtender1.Show();
        }
    }
}