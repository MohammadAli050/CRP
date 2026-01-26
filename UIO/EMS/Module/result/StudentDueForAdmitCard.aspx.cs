using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class StudentDueForAdmitCard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                SessionManager.SaveListToSession<AdmitCardStudentDueDTO>(null, "_admitCardStudentDueDTOList");
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            txtAmount.Text = string.Empty;
            lblMessage.Text = string.Empty;
            if (programId ==0) 
            {
                lblMessage.Text = "Please select a program.";
            }
            if (acaCalId == 0)
            {
                lblMessage.Text = "Please select a session.";
            }
            if (programId > 0 && acaCalId> 0)
            {
                List<AdmitCardStudentDueDTO> admitCardStudentDueDTOList = null; // BillHistoryManager.GetStudentAdmitCardDue(programId, acaCalId).Where(d => d.FinalDue > 0).ToList();
                if (admitCardStudentDueDTOList!= null)
                {
                    loadGvStudentAdmitCardDue(admitCardStudentDueDTOList);
                    SessionManager.SaveListToSession<AdmitCardStudentDueDTO>(null, "_admitCardStudentDueDTOList");
                    SessionManager.SaveListToSession<AdmitCardStudentDueDTO>(admitCardStudentDueDTOList, "_admitCardStudentDueDTOList");
                }
                else 
                {
                    loadGvStudentAdmitCardDue(null);
                }
            }
        }

        private void loadGvStudentAdmitCardDue(List<AdmitCardStudentDueDTO> admitCardStudentDueDTOList) 
        {
            
            GvStudentAdmitCardDue.DataSource = admitCardStudentDueDTOList.ToList();
            GvStudentAdmitCardDue.DataBind();
            GridRebind(admitCardStudentDueDTOList);
        }

        private void GridRebind(List<AdmitCardStudentDueDTO> admitCardStudentDueDTOList)
        {
            for (int i = 0; i < admitCardStudentDueDTOList.Count; i++)
            {
                GridViewRow row = GvStudentAdmitCardDue.Rows[i];
                CheckBox studentCheckd = (CheckBox)row.FindControl("ChkIsAdmitCardBlock");
                if (admitCardStudentDueDTOList[i].IsAdmitCardBlock) 
                {
                    studentCheckd.Checked = true;
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<AdmitCardStudentDueDTO> admitCardStudentDueDTOList = SessionManager.GetListFromSession<AdmitCardStudentDueDTO>("_admitCardStudentDueDTOList");
            if (admitCardStudentDueDTOList != null)
            {
                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (AmountValidation(txtAmount.Text))
                    {
                        admitCardStudentDueDTOList = admitCardStudentDueDTOList.Where(x => x.FinalDue >= Convert.ToDecimal(txtAmount.Text)).ToList();
                    }
                }
            }

            loadGvStudentAdmitCardDue(admitCardStudentDueDTOList);
        }

        protected bool AmountValidation(string examMark)
        {
            try
            {
                if (isNumeric(examMark, System.Globalization.NumberStyles.Float))
                {
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
            finally { }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        protected void chkSelectAllAdmitCardBlock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)GvStudentAdmitCardDue.HeaderRow.FindControl("chkSelectAllAdmitCardBlock");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < GvStudentAdmitCardDue.Rows.Count; i++)
                    {
                        GridViewRow row = GvStudentAdmitCardDue.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("ChkIsAdmitCardBlock");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < GvStudentAdmitCardDue.Rows.Count; i++)
                    {
                        GridViewRow row = GvStudentAdmitCardDue.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("ChkIsAdmitCardBlock");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAdmitCardBlock_Click(object sender, EventArgs e)
        {
            List<AdmitCardStudentDueDTO> admitCardStudentDueDTOList = SessionManager.GetListFromSession<AdmitCardStudentDueDTO>("_admitCardStudentDueDTOList");
            if (admitCardStudentDueDTOList!=null)
            {
                int checkedCounter = 0;
                int blockedCounter = 0;
                for (int j = 0; j < admitCardStudentDueDTOList.Count; j++)
                {

                    GridViewRow row = GvStudentAdmitCardDue.Rows[j];
                    Label studentId = (Label)row.FindControl("lblStudentID");
                    CheckBox studentCheckd = (CheckBox)row.FindControl("ChkIsAdmitCardBlock");

                    if (studentCheckd.Checked)
                    {
                        checkedCounter += 1;
                        Student student = StudentManager.GetById(Convert.ToInt32(studentId.Text));
                        PersonBlock personBlock = PersonBlockManager.GetByPersonId((int)student.PersonID);

                        if (personBlock != null)
                        {
                            personBlock.StartDateAndTime = DateTime.Now;
                            personBlock.EndDateAndTime = DateTime.Now;
                            personBlock.Remarks = "Admit card block";

                            personBlock.IsAdmitCardBlock = true;

                            personBlock.CreatedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            personBlock.CreatedDate = DateTime.Now;
                            personBlock.ModifiedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            personBlock.ModifiedDate = DateTime.Now;

                            bool b = PersonBlockManager.Update(personBlock);
                            if (b) 
                            {
                                blockedCounter += 1;
                                //lblMessage.Text = "Admit card blocked successfully";
                            }
                            
                        }
                        else
                        {
                            personBlock = new PersonBlock();

                            personBlock.PersonId = (int)student.PersonID;
                            personBlock.StartDateAndTime = DateTime.Now;
                            personBlock.EndDateAndTime = DateTime.Now;
                            personBlock.Remarks = "Admit card block";

                            personBlock.IsAdmitCardBlock = true;
                            personBlock.IsRegistrationBlock = false;

                            personBlock.IsMasterBlock = false;
                            personBlock.IsResultBlock = false;

                            personBlock.CreatedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            personBlock.CreatedDate = DateTime.Now;
                            personBlock.ModifiedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            personBlock.ModifiedDate = DateTime.Now;

                            int i = PersonBlockManager.Insert(personBlock);
                            if (i>0)
                            {
                                blockedCounter += 1;
                                //lblMessage.Text = "Admit card blocked successfully";
                            }
                            
                        }
                    }
                }

                if (blockedCounter > 0)
                {
                    if (blockedCounter == checkedCounter)
                    {
                        lblMessage.Text = "Admit card blocked successfully for all checked student.";
                    }
                    else if (blockedCounter < checkedCounter)
                    {
                        lblMessage.Text = "Admit card blocked successfully for " + blockedCounter + " .";
                    }
                    else 
                    { 
                    }
                }
                else 
                {
                    lblMessage.Text = "Admit card could not blocked for any student.";
                }
            }
            else 
            {
                lblMessage.Text = "To block student's admit card, load some student first.";
            }
        }

    }
}