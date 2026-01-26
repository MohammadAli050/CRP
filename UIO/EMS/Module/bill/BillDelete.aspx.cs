using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill
{
    public partial class BillDelete : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Program Selected Index Changed Error.";
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadBillForDelete();
        }

        private void LoadBillForDelete()
        {
            List<BillDeleteDTO> billDeleteList = new List<BillDeleteDTO>();
            if (string.IsNullOrEmpty(txtStudentRoll.Text))
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int studentId = 0;
                DateTime? date = null;

                billDeleteList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
            }
            else if (!string.IsNullOrEmpty(txtStudentRoll.Text))
            {
                int programId = 0;
                int sessionId = 0;
                int batchId = 0;
                int studentId = 0;
                DateTime? date = null;

                string studentRoll = Convert.ToString(txtStudentRoll.Text);
                Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    studentId = studentObj.StudentID;
                    billDeleteList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
                }
            }
            else
            {
                int programId = 0;
                int sessionId = 0;
                int batchId = 0;
                int studentId = 0;
                DateTime? date = null;

                billDeleteList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
            }
            GvBillDelete.DataSource = billDeleteList;
            GvBillDelete.DataBind();
        }

        protected void chkAllStudentHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)GvBillDelete.HeaderRow.FindControl("chkAllStudentHeader");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < GvBillDelete.Rows.Count; i++)
                    {
                        GridViewRow row = GvBillDelete.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < GvBillDelete.Rows.Count; i++)
                    {
                        GridViewRow row = GvBillDelete.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GvBillDelete.Rows.Count; i++)
            {
                GridViewRow row = GvBillDelete.Rows[i];
                Label lblStudentId = (Label)row.FindControl("lblStudentId");
                Label lblBillHistoryMasterId = (Label)row.FindControl("lblBillHistoryMasterId");
                CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");

                if (studentCheckd.Checked == true)
                {
                    if (!string.IsNullOrEmpty(lblBillHistoryMasterId.Text)) 
                    {
                        BillHistoryMaster billHistoryMasterObj = BillHistoryMasterManager.GetById(Convert.ToInt32(lblBillHistoryMasterId.Text));
                        if (billHistoryMasterObj != null)
                        {
                            bool resultDeleteBillHistory2 = DeleteBillHistory(billHistoryMasterObj.ParentBillHistroyMasterId);
                            if (resultDeleteBillHistory2)
                            {
                                bool resultDeleteBillHistoryMaster2 = DeleteBillHistoryMaster(billHistoryMasterObj.ParentBillHistroyMasterId);
                                if (resultDeleteBillHistoryMaster2)
                                {
                                    //LoadBillForDelete();
                                }
                            }
                        }
                        bool resultDeleteBillHistory = DeleteBillHistory(Convert.ToInt32(lblBillHistoryMasterId.Text));
                        if (resultDeleteBillHistory)
                        {
                            bool resultDeleteBillHistoryMaster = DeleteBillHistoryMaster(Convert.ToInt32(lblBillHistoryMasterId.Text));
                            if (resultDeleteBillHistoryMaster)
                            {
                                
                            }
                        }
                    }
                }
            }
            LoadBillForDelete();
        }

        private bool DeleteBillHistory(int billHistoryMasterId)
        {
            bool result = BillHistoryManager.DeleteByBillHistoryMasterId(billHistoryMasterId);
            return result;
        }

        private bool DeleteBillHistoryMaster(int billHistoryMasterId)
        {
            bool result = BillHistoryMasterManager.Delete(billHistoryMasterId);
            return result;
        }
    }
}