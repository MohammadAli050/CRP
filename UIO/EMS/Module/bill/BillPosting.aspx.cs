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
    public partial class BillPosting : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;

            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);

                ddlFeeGroup.Items.Clear();
                ddlFeeGroup.Items.Add(new ListItem("-Select Fee Group-", "0"));
                ddlFeeGroup.AppendDataBoundItems = true;

                //LoadFeeType();
            }
            multiStudentDDLPanel.Visible = true;
            singleStudentPanel.Visible = false;
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

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                if (programId > 0 && batchId > 0)
                {
                    FillFeeType(programId, batchId);
                }
                else 
                {
                    lblMsg.Text = "Please select a program and batch.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Batch Selected Index Changed Error.";
            }
        }

        protected void studentTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (Convert.ToInt32(studentTypeList.SelectedValue) == 1)
                {
                    singleStudentPanel.Visible = true;
                    multiStudentDDLPanel.Visible = false;
                }
                else
                {
                    singleStudentPanel.Visible = false;
                    multiStudentDDLPanel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Student Type Selected Index Changed Error.";
            }
        }

        protected void LoadStudent_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtStudentRoll.Text))
                {
                    string studentRoll = Convert.ToString(txtStudentRoll.Text);
                    LoadStudentByRoll(studentRoll);
                }
                else
                {
                    int sessionId = Convert.ToInt32(ucSession.selectedValue);
                    int programId = Convert.ToInt32(ucProgram.selectedValue);
                    int batchId = Convert.ToInt32(ucBatch.selectedValue);
                    int feeGroupMasterId = Convert.ToInt32(ddlFeeGroup.SelectedValue);

                    if (programId > 0 && batchId > 0 && sessionId > 0 && feeGroupMasterId > 0)
                    {
                        LoadStudent(sessionId, programId, batchId);
                        LoadFeeGroup(feeGroupMasterId);
                    }
                    else 
                    {
                        lblMsg.Text = "Please select program, batch, session and fee group properly.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Batch Selected Index Changed Error.";
            }
        }

        private void FillFeeType(int programId, int batchId)
        {
            try
            {
                List<FeeGroupMaster> feeGroupList = FeeGroupMasterManager.GetAll();

                if (feeGroupList != null && feeGroupList.Count > 0)
                {
                    if (programId > 0 && batchId > 0)
                    {
                        if (programId > 0)
                        {
                            feeGroupList = feeGroupList.Where(d => d.ProgramId == programId).ToList();
                        }
                        if (batchId > 0)
                        {
                            feeGroupList = feeGroupList.Where(d => d.BatchId == batchId).ToList();
                        }
                    }

                    ddlFeeGroup.Items.Clear();
                    ddlFeeGroup.Items.Add(new ListItem("-Select Fee Group-", "0"));
                    ddlFeeGroup.AppendDataBoundItems = true;
                    if (feeGroupList != null && feeGroupList.Count > 0)
                    {
                        ddlFeeGroup.DataSource = feeGroupList;
                        ddlFeeGroup.DataTextField = "FeeGroupName";
                        ddlFeeGroup.DataValueField = "FeeGroupMasterId";
                        ddlFeeGroup.DataBind();
                    }
                }
                else 
                {
                    lblMsg.Text = "No fee group found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Fee Group Drop Down Bind Error.";
            }
        }

        private void LoadStudent(int sessionId, int programId, int batchId)
        {
            try
            {
                lblMsg.Text = string.Empty;
                List<StudentBillCourseCountDTO> studentList = StudentManager.GetStudentForBillPosting(sessionId, programId, batchId);
                if (studentList != null && studentList.Count > 0)
                {
                    GridViewStudent.DataSource = studentList;
                    GridViewStudent.DataBind();
                }
                else 
                {
                    lblMsg.Text = "No student found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Batch Selected Index Changed Error.";
            }
        }

        private void LoadStudentByRoll(string studentRoll)
        {
            throw new NotImplementedException();
        }

        protected void chkAllStudentHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)GridViewStudent.HeaderRow.FindControl("chkAllStudentHeader");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewStudent.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewStudent.Rows[i];
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

        protected void chkAllFee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkAllFee = (CheckBox)GvFeeAmount.HeaderRow.FindControl("chkAllFee");
                if (chkAllFee.Checked)
                {
                    for (int i = 0; i < GvFeeAmount.Rows.Count; i++)
                    {
                        GridViewRow row = GvFeeAmount.Rows[i];
                        CheckBox feeCheckBox = (CheckBox)row.FindControl("feeCheckBox");
                        feeCheckBox.Checked = true;
                    }
                }
                if (!chkAllFee.Checked)
                {
                    for (int i = 0; i < GvFeeAmount.Rows.Count; i++)
                    {
                        GridViewRow row = GvFeeAmount.Rows[i];
                        CheckBox feeCheckBox = (CheckBox)row.FindControl("feeCheckBox");
                        feeCheckBox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnLoadFeeGroup_Click(object sender, EventArgs e)
        {
            try
            {
                int feeGroupMasterId = Convert.ToInt32(ddlFeeGroup.SelectedValue);
                LoadFeeGroup(feeGroupMasterId);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadFeeGroup(int feeGroupMasterId)
        {
            try
            {
                List<FeeGroupDetail> feeGroupDetailList = new List<FeeGroupDetail>();

                if (feeGroupMasterId > 0)
                {
                    feeGroupDetailList = FeeGroupDetailManager.GetByFeeGroupMasterId(feeGroupMasterId); 
                }
                else
                {
                    feeGroupDetailList = null;
                }

                if (feeGroupDetailList != null)
                {
                    GvFeeAmount.DataSource = feeGroupDetailList;
                    GvFeeAmount.DataBind();
                }
                else
                {
                    lblMsg.Text = "No fee item found.";
                }

                //List<LogicLayer.BusinessObjects.FeeGroupDetails> feeSetuplist = null;
                //var feeSetupSessionList = Session["FeesSetupList"];
                //feeSetuplist = feeSetupSessionList as List<LogicLayer.BusinessObjects.FeeGroupDetails>;
                //if (feeSetuplist != null)
                //{
                //    if (feeGroupDetailList != null)
                //    {
                //        for (int i = 0; i < feeGroupDetailList.Count; i++)
                //        {
                //            bool result = CheckListAndObject(feeSetuplist, feeGroupDetailList[i]);
                //            if (result)
                //            {
                //                feeSetuplist.Add(feeGroupDetailList[i]);
                //            }
                //            else
                //            {
                //                lblMsg.Text = "Fee item already added.";
                //            }
                //        }
                //    }
                    
                //    Session["FeesSetupList"] = null;
                //    Session["FeesSetupList"] = feeSetuplist;
                //}
                //else
                //{
                //    gvFeeSetup.DataSource = feeGroupDetailList;
                //    gvFeeSetup.DataBind();
                //    Session["FeesSetupList"] = null;
                //    Session["FeesSetupList"] = feeGroupDetailList;
                //}
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadFeeType() 
        {
            try
            {
                List<FeeType> feeGroupDetailList = FeeTypeManager.GetAll();
                ddlFeeItem.Items.Clear();
                ddlFeeItem.Items.Add(new ListItem("-Select Fee Item-", "0"));
                ddlFeeItem.AppendDataBoundItems = true;

                if (feeGroupDetailList != null && feeGroupDetailList.Count > 0)
                {
                    ddlFeeItem.DataSource = feeGroupDetailList;
                    ddlFeeItem.DataTextField = "FeeName";
                    ddlFeeItem.DataValueField = "FeeTypeId";
                    ddlFeeItem.DataBind();
                }
                else 
                {
                    lblMsg.Text = "No fee item found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnBillPosting_Click(object sender, EventArgs e)
        {
            try
            {
                int studentCounter = CountGridStudent();
                int feesCounter = CountGridFees();
                if (studentCounter >= 1 && feesCounter >= 1)
                {
                    if (CheckAllField())
                    {
                        for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                        {
                            GridViewRow row = GridViewStudent.Rows[i];
                            Label lblStudentId = (Label)row.FindControl("lblStudentId");
                            CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                            if (studentCheckd.Checked == true)
                            {
                                int studentId = Convert.ToInt32(lblStudentId.Text);
                                BillHistoryMaster billHistoryMaster = BillHistoryMasterManager.GetBillDueCountByStudentIdAcaCalId(studentId, Convert.ToInt32(ucSession.selectedValue)).Where(d => d.IsDue == true).FirstOrDefault();
                                if (billHistoryMaster != null)
                                {
                                    //int parentBillHistoryMasterId = InsertBillMaster(0, studentId);
                                    InsertBillMaster(billHistoryMaster.BillHistoryMasterId, studentId);
                                }
                                else
                                {
                                    int parentBillHistoryMasterId = InsertBillMaster(0, studentId);
                                    InsertBillMaster(parentBillHistoryMasterId, studentId);
                                }
                            }
                        }
                    
                    }
                    else
                    {
                        lblMsg.Text = "Please provide all required information.";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select some student and fees to post bill.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private bool CheckAllField()
        {
            if (Convert.ToInt32(ucSession.selectedValue) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int InsertBillMaster(int parentBillHistoryMasterId_New, int studentId)
        {
            int parentBillHistoryMasterId = 0;
            decimal billMasterAmount = CalCulateGridFeesAmount();
            if (billMasterAmount > 0)
            {
                //for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                //{
                //    GridViewRow row = GridViewStudent.Rows[i];
                //    Label lblStudentId = (Label)row.FindControl("lblStudentId");
                //    CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");

                //    if (studentCheckd.Checked == true)
                //    {
                        //int studentId = Convert.ToInt32(Convert.ToString(lblStudentId.Text));
                        LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                        BillHistoryMaster billHistoryMaster = new BillHistoryMaster();
                        billHistoryMaster.StudentId = studentId;
                        billHistoryMaster.BillingDate = DateTime.Now;
                        billHistoryMaster.ReferenceNo = Convert.ToString(BillHistoryMasterManager.GetBillMasterMaxReferenceNo(billHistoryMaster.BillingDate));
                        billHistoryMaster.AcaCalId = Convert.ToInt32(ucSession.selectedValue);

                        if (parentBillHistoryMasterId_New > 0)
                        {
                            billHistoryMaster.ParentBillHistroyMasterId = parentBillHistoryMasterId_New;
                            billHistoryMaster.Amount = billMasterAmount;
                            billHistoryMaster.IsDue = false;
                        }
                        else
                        {
                            billHistoryMaster.ParentBillHistroyMasterId = 0;
                            billHistoryMaster.Amount = 0;
                            billHistoryMaster.IsDue = true;
                        }
                        billHistoryMaster.IsDeleted = false;
                        billHistoryMaster.CreatedBy = userObj.Id;
                        billHistoryMaster.CreatedDate = DateTime.Now;
                        billHistoryMaster.ModifiedBy = userObj.Id;
                        billHistoryMaster.ModifiedDate = DateTime.Now;
                        bool checkBillMasterDuplicate = BillHistoryMasterManager.IsDuplicateBill(billHistoryMaster.StudentId, billHistoryMaster.AcaCalId, billHistoryMaster.Amount, billHistoryMaster.IsDue);
                        int billHistoryMasterInsert = 0;

                        if (billHistoryMaster.IsDue == true)
                        {
                            //if (BillHistoryMasterManager.CheckBillMasterDueBillCount(studentId, Convert.ToInt32(ucSession.selectedValue)) == 0)
                            //{
                                if (checkBillMasterDuplicate)
                                {
                                    billHistoryMasterInsert = BillHistoryMasterManager.Insert(billHistoryMaster);
                                    if (billHistoryMasterInsert > 0)
                                    {
                                        parentBillHistoryMasterId = billHistoryMasterInsert;
                                        InsertBillHistory(studentId, billHistoryMasterInsert, billHistoryMaster.IsDue);
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "Bill already exists.";
                                }
                            //}
                            //else
                            //{
                            //    lblMsg.Text = "Due bill already exists.";
                            //}
                        }
                        else 
                        {
                            if (checkBillMasterDuplicate)
                            {
                                billHistoryMasterInsert = BillHistoryMasterManager.Insert(billHistoryMaster);
                                if (billHistoryMasterInsert > 0)
                                {
                                    parentBillHistoryMasterId = billHistoryMasterInsert;
                                    InsertBillHistory(studentId, billHistoryMasterInsert, billHistoryMaster.IsDue);
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Bill already exists.";
                            }
                        }
                //    }
                //}
            }
            else
            {
                lblMsg.Text = "Please provide some amount for bill posting.";
            }
            return parentBillHistoryMasterId;
        }

        private void InsertBillHistory(int studentId, int billHistoryMasterId, bool isDue)
        {
            try
            {
                int noOfFees = CountGridFees();
                int billInsertCounter = 0;
                for (int j = 0; j < GvFeeAmount.Rows.Count; j++)
                {
                    GridViewRow feeRow = GvFeeAmount.Rows[j];
                    TextBox txtFeeAmount = (TextBox)feeRow.FindControl("txtAmount");
                    Label lblFeeTypeId = (Label)feeRow.FindControl("lblFeeTypeId");
                    Label lblFundTypeId = (Label)feeRow.FindControl("lblFundTypeId");
                    CheckBox feesCheckd = (CheckBox)feeRow.FindControl("feeCheckBox");
                    TextBox txtComment = (TextBox)feeRow.FindControl("txtComment");
                    if (feesCheckd.Checked && !string.IsNullOrEmpty(txtFeeAmount.Text))
                    {
                        double feeAmount = Convert.ToDouble(txtFeeAmount.Text);
                        if (feeAmount > 0)
                        {
                            BillHistory billHistroyObj = new BillHistory();
                            billHistroyObj.StudentId = studentId;
                            if (!isDue)
                            {
                                billHistroyObj.Fees = Convert.ToDecimal(txtFeeAmount.Text);
                            }
                            else 
                            {
                                billHistroyObj.Fees = 0;
                            }
                            billHistroyObj.BillingDate = DateTime.Now;// DateTime.ParseExact(txtBillingDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            billHistroyObj.Remark = Convert.ToString(txtComment.Text);
                            billHistroyObj.FundTypeId = Convert.ToInt32(lblFundTypeId.Text);
                            billHistroyObj.FeeTypeId = Convert.ToInt32(lblFeeTypeId.Text);
                            billHistroyObj.BillHistoryMasterId = Convert.ToInt32(billHistoryMasterId);
                            billHistroyObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                            billHistroyObj.IsDeleted = false;
                            billHistroyObj.CreatedBy = userObj.Id;
                            billHistroyObj.CreatedDate = DateTime.Now;
                            billHistroyObj.ModifiedBy = userObj.Id;
                            billHistroyObj.ModifiedDate = DateTime.Now;
                            
                            int billInsertId = BillHistoryManager.Insert(billHistroyObj);
                            if (billInsertId > 0)
                            {
                                lblMsg.Text = "Bill inserted successfully.";
                                billInsertCounter = billInsertCounter + 1;
                            }
                            else
                            {
                                lblMsg.Text = "Bill could not inserted successfully.";
                            }
                        }
                    }
                    else
                    {
                    }
                }

                if (noOfFees == billInsertCounter)
                {
                    UpdateSessionBillHistoryMaster(studentId, billHistoryMasterId);
                }
                else
                {
                    lblMsg.Text = "Bill could not inserted successfully.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateSessionBillHistoryMaster(int studentId, int billHistoryMasterId)
        {
            int studentCounter = CountGridStudent();
            if (billHistoryMasterId > 0)
            {
                BillHistoryMaster billMaster = BillHistoryMasterManager.GetById(billHistoryMasterId);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                if (studentCounter == 1)
                {
                    
                }
                else if (studentCounter > 1)
                {
                    //lblMsg.Text = "Bill print is not enable for multiple students.";
                }
                else
                {
                    lblMsg.Text = string.Empty;
                }
            }
        }

        private decimal CalCulateGridFeesAmount()
        {
            decimal billAmount = 0;
            for (int i = 0; i < GvFeeAmount.Rows.Count; i++)
            {
                GridViewRow row = GvFeeAmount.Rows[i];
                TextBox txtFee = (TextBox)row.FindControl("txtAmount");
                CheckBox feesCheckd = (CheckBox)row.FindControl("feeCheckBox");
                if (feesCheckd.Checked)
                {
                    if (!string.IsNullOrEmpty(txtFee.Text))
                    {
                        billAmount = billAmount + Convert.ToDecimal(txtFee.Text);
                    }
                }
            }
            return billAmount;
        }

        private int CountGridFees()
        {
            int noOfFees = 0;
            for (int j = 0; j < GvFeeAmount.Rows.Count; j++)
            {
                GridViewRow feeRow = GvFeeAmount.Rows[j];
                CheckBox feesCheckd = (CheckBox)feeRow.FindControl("feeCheckBox");
                TextBox txtFeeAmount = (TextBox)feeRow.FindControl("txtAmount");
                if (feesCheckd.Checked && !string.IsNullOrEmpty(txtFeeAmount.Text))
                {
                    double feeAmount = Convert.ToDouble(txtFeeAmount.Text);
                    if (feeAmount > 0)
                    {
                        noOfFees = noOfFees + 1;
                    }
                }
            }
            return noOfFees;
        }

        private int CountGridStudent()
        {
            int studentCounter = 0;
            for (int i = 0; i < GridViewStudent.Rows.Count; i++)
            {
                GridViewRow row = GridViewStudent.Rows[i];
                Label lblStudentId = (Label)row.FindControl("lblStudentId");
                CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");

                if (studentCheckd.Checked == true)
                {
                    studentCounter = studentCounter + 1;
                }
            }
            return studentCounter;
        }

        protected void btnCalculateFee_Click(object sender, EventArgs e)
        {
            lblAmount2.Text = Convert.ToString(CalCulateGridFeesAmount());
        }
    }
}