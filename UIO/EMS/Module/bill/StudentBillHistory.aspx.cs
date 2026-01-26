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
    public partial class StudentBillHistory : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {

            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string studentRoll = Convert.ToString(txtStudentRoll.Text);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    lblStudentName.Text = studentObj.BasicInfo.FullName;
                    lblStudentProgram.Text = studentObj.Program.DetailName;
                    List<BillPaymentHistoryMasterDTO> billPaymentHistoryMasterList = BillHistoryManager.GetBillPaymentHistoryMasterByStudentId(studentObj.StudentID);
                    //List<BillPaymentHistoryDTO> billPaymentHistoryList = BillHistoryManager.GetBillPaymentHistoryByStudentId(studentObj.StudentID);
                    if (billPaymentHistoryMasterList != null && billPaymentHistoryMasterList.Count > 0)
                    {
                        lblTotalBill.Text = Convert.ToString(billPaymentHistoryMasterList.Sum(d => d.BillAmount));
                        lblTotalPayment.Text = Convert.ToString(billPaymentHistoryMasterList.Sum(d => d.PaymentAmount));

                        GvBillMaster.Columns[2].FooterText = "Total";
                        GvBillMaster.Columns[4].FooterText = billPaymentHistoryMasterList.AsEnumerable().Select(x => x.BillAmount).Sum().ToString();
                        GvBillMaster.Columns[6].FooterText = billPaymentHistoryMasterList.AsEnumerable().Select(x => x.PaymentAmount).Sum().ToString();

                        GvBillMaster.DataSource = billPaymentHistoryMasterList;
                        GvBillMaster.DataBind();
                    }
                }
                else 
                {
                    lblMsg.Text = "Student not found.";
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void GvBillMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.ModalShowBillHistoryDetailsPopupExtender.Show();
            int billHistoryMasterId = Convert.ToInt32(e.CommandArgument);
            if (billHistoryMasterId > 0)
            {

                BillHistoryMaster billHistoryMasterObj = BillHistoryMasterManager.GetById(billHistoryMasterId);
                if (billHistoryMasterObj != null)
                {
                    LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(billHistoryMasterObj.StudentId);
                    List<BillPaymentHistoryDTO> billPaymentHistoryList = BillHistoryManager.GetBillPaymentHistoryByBillHistoryMasterId(billHistoryMasterObj.BillHistoryMasterId).ToList();
                    if (billPaymentHistoryList != null && billPaymentHistoryList.Count > 0)
                    {
                        GvStudentBillPaymentHistory.Columns[2].FooterText = "Total";
                        GvStudentBillPaymentHistory.Columns[4].FooterText = billPaymentHistoryList.AsEnumerable().Select(x => x.BillAmount).Sum().ToString();
                        GvStudentBillPaymentHistory.Columns[5].FooterText = billPaymentHistoryList.AsEnumerable().Select(x => x.PaymentAmount).Sum().ToString();

                        GvStudentBillPaymentHistory.DataSource = billPaymentHistoryList;
                        GvStudentBillPaymentHistory.DataBind();
                    }
                }
            }
        }
    }
}