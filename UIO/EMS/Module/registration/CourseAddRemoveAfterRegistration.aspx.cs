using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration
{
    public partial class CourseAddRemoveAfterRegistration : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            lblMessage.Text = string.Empty;

            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                lblMessage.Text = "Insert Student ID.";
                lblMessage.Focus();
                return;
            }
            else if (sessionId == 0)
            {
                lblMessage.Text = "Please select Session.";
                lblMessage.Focus();
                return;
            }

            Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
            AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);
            if (student != null)
            {
                lblProgram.Text = student.Program.ShortName;
                lblBatch.Text = student.Batch.BatchNO.ToString();
                lblName.Text = student.BasicInfo.FullName;

                List<BillHistoryDTO> studentBillHistoryObj = BillHistoryManager.GetByStudentAcacalId(student.StudentID, acaCal.AcademicCalenderID);
                //List<BillView> billViewList = BillViewManager.GetBy(student.StudentID);
                gvBillView.DataSource = studentBillHistoryObj.ToList();
                gvBillView.DataBind();
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearField();
            this.ModalPopupExtender1.Show();
        }

        private void ClearField()
        {
            
        }

        protected void gvBillView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}