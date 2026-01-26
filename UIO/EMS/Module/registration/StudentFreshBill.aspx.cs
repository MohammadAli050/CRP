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

namespace EMS.miu.registration
{
    public partial class StudentFreshBill : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                //ucProgram.LoadDropDownList();
                if (userObj.RoleID == 9)
                {
                    User user = UserManager.GetById(userObj.Id);
                    Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                    txtStudent.Text = student.Roll;
                    txtStudent.Text = student.Roll;
                    txtStudent.Enabled = false;
                    txtStudent.ReadOnly = true;
                }
            }
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

            if (student != null)
            {
                txtStudent.Text = student.Roll;
                lblProgram.Text = student.Program.ShortName;
                lblBatch.Text = student.Batch.BatchNO.ToString();
                lblName.Text = student.BasicInfo.FullName;

                //AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);
                LoadBillGridList(student.StudentID, sessionId);
                //List<BillHistoryDTO> studentBillHistoryObj = BillHistoryManager.GetByStudentAcacalId(student.StudentID, acaCal.AcademicCalenderID);
                //gvBillView.DataSource = studentBillHistoryObj.ToList();
                //gvBillView.DataBind();
            }
        }

        internal void LoadBillGridList(int studentId, int acaCalId)
        {
            List<BillHistoryDTO> studentBillHistoryObj = null; //BillHistoryManager.GetByStudentAcacalId(studentId, acaCalId);
            gvBillView.DataSource = studentBillHistoryObj.ToList();
            gvBillView.DataBind();
            GridRebind(studentId);
        }

        public void GridRebind(int studentId)
        {
            //OpeningDue openingDueObj = OpeningDueManager.GetByStudentId(studentId);
            decimal totalFees = 0;
            for (int i = 0; i < gvBillView.Rows.Count; i++)
            {
                GridViewRow row = gvBillView.Rows[i];
                Label newLabel = (Label)(gvBillView.Rows[i].FindControl("lblAmount"));
                string fees = Convert.ToString(newLabel.Text);
                totalFees = totalFees + Convert.ToDecimal(fees);
            }
            
            lblTotalPaybleAmount.Text = Convert.ToString( totalFees);
                
           
           
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }


        protected void gvBillView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int billHistoryId = Convert.ToInt32(e.CommandArgument);
            //BillHistory studentBillHistoryObj = BillHistoryManager.GetById(billHistoryId);

            //if (e.CommandName == "EditBillHistory")
            //{
            //    this.ModalPopupExtender1.Show();
            //    LoadStudentBillHistory(studentBillHistoryObj);
            //    LoadBillGridList(studentBillHistoryObj.StudentId, studentBillHistoryObj.AcaCalId);
            //}
            //if (e.CommandName == "DeleteBillHistory")
            //{
            //    BillHistoryManager.Delete(billHistoryId);
            //    LoadBillGridList(studentBillHistoryObj.StudentId, studentBillHistoryObj.AcaCalId);
            //}
        }
       
    }
}