using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using CommonUtility;
using LogicLayer.BusinessObjects.DTO;
using BussinessObject;

public partial class Student_StudentGeneralBill : BasePage
{
    decimal _totalAmount = 0;
    decimal _totalAmountByCollectiveDiscount = 0;
    decimal _totalAmountByIterativeDiscount = 0;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    List<StudentPaymentLadgerDTO> studentBillHistoryDTOList = new List<StudentPaymentLadgerDTO>();
    UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (userObj.RoleID == 9)
        {
            User user = UserManager.GetById(userObj.Id);
            LogicLayer.BusinessObjects.Student student = StudentManager.GetBypersonID(user.Person.PersonID);

            txtStudent.Text = student.Roll;
            txtStudent.Text = student.Roll;
            txtStudent.Enabled = false;
            txtStudent.ReadOnly = true;
        }
        if (Request.QueryString["stdId"] != null && Request.QueryString["acacal"] != null)
        {
            int acaCalId = Convert.ToInt32(Request.QueryString["acacal"]);
            LogicLayer.BusinessObjects.Student student = StudentManager.GetById(Convert.ToInt32(Request.QueryString["stdId"]));
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;
            txtStudent.Enabled = false;
            txtStudent.Text = Convert.ToString(student.Roll);
            //List<BillView> billViewList = BillViewManager.GetBy(Convert.ToInt32(Request.QueryString["stdId"]), Convert.ToInt32(Request.QueryString["acacal"]));
            //gvBillView.DataSource = billViewList;
            //gvBillView.DataBind();
        }
        else if (Request.QueryString["stdId"] != null)
        {
            LogicLayer.BusinessObjects.Student student = StudentManager.GetById(Convert.ToInt32(Request.QueryString["stdId"]));
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;
            txtStudent.Enabled = false;
            txtStudent.Text = Convert.ToString(student.Roll);

            List<BillView> billViewList = BillViewManager.GetBy(Convert.ToInt32(Request.QueryString["stdId"]));
            gvBillView.DataSource = billViewList;
            gvBillView.DataBind();
        }
        else
        {
            //int acaCalId = Convert.ToInt32(Request.QueryString["acacal"]);
            //gvBillView.DataSource = null;
            //gvBillView.DataBind();
        }

        if (!IsPostBack)
        {
            lblMessage.Text = null;
            ddlBillPostingSession.Items.Clear();
            ddlBillPostingSession.Items.Add(new ListItem("-Select-", "0"));
            ddlBillPostingSession.DataBind();
        }
    }

    //private void LoadStudentBill()
    //{
    //    lblMessage.Text = string.Empty;

    //    if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
    //    {
    //        lblMessage.Text = "Insert Student ID.";
    //        lblMessage.Focus();
    //        return;
    //    }
    //    //else if (sessionId == 0)
    //    //{
    //    //    lblMessage.Text = "Please select Session.";
    //    //    lblMessage.Focus();
    //    //    return;
    //    //}

    //    Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());

    //    if (student != null)
    //    {
    //        lblProgram.Text = student.Program.ShortName;
    //        lblBatch.Text = student.Batch.BatchNO.ToString();
    //        lblName.Text = student.BasicInfo.FullName;

    //        List<StudentPaymentLadgerDTO> studentBillHistoryObj = BillHistoryManager.GetByStudentPaymentLadger(student.StudentID);
    //        //List<BillView> billViewList = BillViewManager.GetBy(student.StudentID);
    //        gvBillView.DataSource = studentBillHistoryObj.ToList();
    //        gvBillView.DataBind();

    //        GridRebind(student.StudentID);
    //        //
    //        //lblDueAmount.Text = studentBillHistoryObj.;
    //    }
    //}

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        //int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);
        lblMessage.Text = string.Empty;

        if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
        {
            lblMessage.Text = "Insert Student ID.";
            lblMessage.Focus();
            return;
        }

        LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
        if (student != null)
        {
            BillHistoryGridBind(student);
        }
        else
        {
            gvBillView.DataSource = null;
            gvBillView.DataBind();
        }
    }

    private void BillHistoryGridBind(LogicLayer.BusinessObjects.Student student)
    {
        try
        {
            if (student != null)
            {
                int sessionId = Convert.ToInt32(ddlBillPostingSession.SelectedValue);
                lblProgram.Text = student.Program.ShortName;
                lblBatch.Text = student.Batch.BatchNO.ToString();
                lblName.Text = student.BasicInfo.FullName;

                List<StudentPaymentLadgerDTO> studentBillHistoryList = null; //BillHistoryManager.GetByStudentPaymentLadger(student.StudentID).Where(d => d.IsDeleted != true).OrderBy(d => d.BillingDate).ToList();

                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    studentBillHistoryList = studentBillHistoryList.Where(d => d.CollectionHistoryId == 0).ToList();
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    studentBillHistoryList = studentBillHistoryList.Where(d => d.CollectionHistoryId != 0).ToList();
                }
                if (studentBillHistoryList != null)
                {
                    LoadSession(studentBillHistoryList.Select(s => s.AcaCalId).Distinct().ToList());

                    var newStudentBillHistory = new List<StudentPaymentLadgerDTO>();
                    for (int i = 0; i < studentBillHistoryList.Count; i++)
                    {
                        StudentPaymentLadgerDTO studentPaymentLedgerDtoObj = new StudentPaymentLadgerDTO();
                        studentPaymentLedgerDtoObj.BillHistoryId = studentBillHistoryList[i].BillHistoryId;
                        studentPaymentLedgerDtoObj.StudentId = studentBillHistoryList[i].StudentId;
                        studentPaymentLedgerDtoObj.TypeDefinationId = studentBillHistoryList[i].TypeDefinationId;
                        studentPaymentLedgerDtoObj.AcaCalId = studentBillHistoryList[i].AcaCalId;
                        studentPaymentLedgerDtoObj.Remark = studentBillHistoryList[i].Remark;
                        studentPaymentLedgerDtoObj.BillingDate = studentBillHistoryList[i].BillingDate;
                        studentPaymentLedgerDtoObj.CreatedDate = studentBillHistoryList[i].CreatedDate;
                        studentPaymentLedgerDtoObj.IsDeleted = studentBillHistoryList[i].IsDeleted;
                        studentPaymentLedgerDtoObj.CreatedBy = studentBillHistoryList[i].CreatedBy;
                        studentPaymentLedgerDtoObj.CreatedDate = studentBillHistoryList[i].CreatedDate;
                        studentPaymentLedgerDtoObj.ModifiedBy = studentBillHistoryList[i].ModifiedBy;
                        studentPaymentLedgerDtoObj.ModifiedDate = studentBillHistoryList[i].ModifiedDate;
                        studentPaymentLedgerDtoObj.CollectionHistoryId = studentBillHistoryList[i].CollectionHistoryId;
                        studentPaymentLedgerDtoObj.StudentCourseHistoryId = studentBillHistoryList[i].StudentCourseHistoryId;
                        studentPaymentLedgerDtoObj.FormalCode = studentBillHistoryList[i].FormalCode;
                        studentPaymentLedgerDtoObj.CourseTitle = studentBillHistoryList[i].CourseTitle;
                        studentPaymentLedgerDtoObj.Credits = studentBillHistoryList[i].Credits;
                        studentPaymentLedgerDtoObj.SemesterName = studentBillHistoryList[i].SemesterName;
                        if (studentBillHistoryList[i].Fees > 0)
                        {
                            if (studentBillHistoryList[i].CollectionHistoryId != 0)
                            {
                                studentPaymentLedgerDtoObj.Payment = studentBillHistoryList[i].Fees;
                            }
                            else
                            {
                                studentPaymentLedgerDtoObj.Fees = studentBillHistoryList[i].Fees;
                            }
                        }
                        else
                        {
                            studentPaymentLedgerDtoObj.DiscountAmount = studentBillHistoryList[i].Fees;
                        }
                        newStudentBillHistory.Add(studentPaymentLedgerDtoObj);
                    }

                    if (sessionId != 0)
                    {
                        gvBillView.DataSource = newStudentBillHistory.Where(d => d.AcaCalId == sessionId).OrderBy(d => d.AcaCalId).ToList();
                    }
                    else
                    {
                        gvBillView.DataSource = newStudentBillHistory.OrderBy(d => d.AcaCalId).ToList();
                    }
                    gvBillView.DataBind();
                    SessionManager.SaveListToSession<StudentPaymentLadgerDTO>(newStudentBillHistory, "_studentBillHistoryList");
                }
                GridRebind(student.StudentID);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    public void GridRebind(int studentId)
    {
        OpeningDue openingDueObj = OpeningDueManager.GetByStudentId(studentId);
        decimal totalFees = 0;
        decimal totalDiscount = 0;
        decimal totalPayment = 0;
        decimal totalPaybleAmount = 0;
        decimal currentDues = 0;
        for (int i = 0; i < gvBillView.Rows.Count; i++)
        {
            GridViewRow row = gvBillView.Rows[i];
            Label newLabel = (Label)row.FindControl("lblAmount");
            string fees = Convert.ToString(newLabel.Text);
            totalFees = totalFees + Convert.ToDecimal(fees);

            Label discount = (Label)row.FindControl("lblDiscount");
            decimal discountAmount = Convert.ToDecimal(discount.Text);
            totalDiscount = totalDiscount + discountAmount;

            Label payment = (Label)row.FindControl("lblPayment");
            decimal paymentAmount = Convert.ToDecimal(payment.Text);
            totalPayment = totalPayment + paymentAmount;

            Label currentDue = (Label)row.FindControl("lblCurrentDue"); ;
            currentDues = (totalFees + totalDiscount) - totalPayment;
            currentDue.Text = Convert.ToString(currentDues);
        }

        totalPaybleAmount = (totalFees + totalDiscount) - totalPayment;
        lblTotalPaybleAmount.Text = Convert.ToString(totalPaybleAmount);
        lblTotalBill.Text = Convert.ToString(totalFees);
        lblTotalPaid.Text = Convert.ToString(totalPayment);
        lblTotalDiscount.Text = Convert.ToString(totalDiscount);

        for (int i = 0; i < gvBillView.Rows.Count; i++)
        {
            GridViewRow row = gvBillView.Rows[i];
            LinkButton linkEdit = (LinkButton)row.FindControl("btnEdit");
            LinkButton linkDelete = (LinkButton)row.FindControl("btnDelete");

            if (userObj.RoleID == 13)
            {
                linkEdit.Visible = true;
                linkDelete.Visible = true;
            }
            else
            {
                linkEdit.Visible = false;
                linkDelete.Visible = false;
            }
        }
    }

    private void FillSessionAcademicCalenderCombo()
    {
        //try
        //{
        //    ddlAcaCalSession.Items.Clear();
        //    ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));

        //    List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
        //    academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

        //    ddlAcaCalSession.AppendDataBoundItems = true;

        //    if (academicCalenderList != null)
        //    {
        //        int count = academicCalenderList.Count;
        //        foreach (AcademicCalender academicCalender in academicCalenderList)
        //        {
        //            ddlAcaCalSession.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
        //finally { }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;
            //int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);

            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                lblMessage.Text = "Insert Student ID.";
                lblMessage.Focus();
                return;
            }
            //else if (sessionId == 0)
            //{
            //    lblMessage.Text = "Please select Session.";
            //    lblMessage.Focus();
            //    return;
            //}
            else
            {
                //Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                //if (student == null)
                //{
                //    lblMessage.Text = "Student not found";
                //    return;
                //}

                //lblMessage.Text = BillViewManager.GenerateBill(student, sessionId);

                //List<BillView> billViewList = BillViewManager.GetBy(student.StudentID);
                //gvBillView.DataSource = billViewList.Where(b => b.TrimesterId == sessionId).ToList();
                //gvBillView.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            this.ModalPopupExtender1.Show();
            LinkButton btn = (LinkButton)sender;
            int billHistoryId = int.Parse(btn.CommandArgument.ToString());
            studentBillHistoryDTOList = SessionManager.GetListFromSession<StudentPaymentLadgerDTO>("_studentBillHistoryList");
            StudentPaymentLadgerDTO studentBillHistoryObj = studentBillHistoryDTOList.Where(d => d.BillHistoryId == billHistoryId).FirstOrDefault();
            LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentBillHistoryObj.StudentId);
            LogicLayer.BusinessObjects.TypeDefinition typeDefinitionObj = TypeDefinitionManager.GetById(studentBillHistoryObj.TypeDefinationId);
            lblNewStudentName.Text = studentObj.Name;
            lblStudentRoll.Text = studentObj.Roll;
            lblTypeDefinition.Text = typeDefinitionObj.Definition;
            LoadTrimesterDDL(studentObj.ProgramID);
            if (studentBillHistoryObj.CollectionHistoryId != 0)
            {
                CollectionHistory collectionHistoryObj = CollectionHistoryManager.GetById(studentBillHistoryObj.CollectionHistoryId);
                ddlSession.SelectedValue = Convert.ToString(collectionHistoryObj.AcaCalId);
                txtPaymentAmount.Text = Convert.ToString(studentBillHistoryObj.Payment);
                txtFeeAmount.Enabled = false;
                txtDiscountAmount.Enabled = false;
                UpdateButton.Visible = false;
                lblCourseName.Visible = false;
                lblCourseName.Text = null;
                lblMoneyReceiptNo.Visible = true;
                txtMoneyReceiptNo.Visible = true;
                txtMoneyReceiptNo.Text = collectionHistoryObj.MoneyReciptSerialNo;
                lblComment.Visible = true;
                txtRemark.Visible = true;
                txtRemark.Text = collectionHistoryObj.Comments;
                lblPaymentType.Visible = true;
                rdlPaymentType.Enabled = true;
                rdlPaymentType.Visible = true;
                collectionHistoryObj.PaymentType = Convert.ToString(rdlPaymentType.SelectedValue);
                lblDate.Visible = true;
                DateTextBox.Visible = true;
                //string date =
                DateTextBox.Text = Convert.ToString(collectionHistoryObj.CollectionDate.ToString("dd/MM/yyyy"));// Convert.ToString(DateTime.ParseExact(date.Replace("/", string.Empty), "ddMMyyyy", null));
                PaymentUpdateButton.Visible = true;
                PaymentUpdateButton.CommandArgument = Convert.ToString(studentBillHistoryObj.CollectionHistoryId);
                UpdateButton.CommandArgument = null;
            }
            else
            {
                lblDate.Visible = true;
                DateTextBox.Visible = true;
                DateTextBox.Text = Convert.ToString(studentBillHistoryObj.BillingDate.ToString("dd/MM/yyyy"));
                lblComment.Visible = true;
                txtRemark.Visible = true;
                lblCourseName.Visible = true;
                lblCourseName.Text = studentBillHistoryObj.CourseTitle;
                ddlSession.SelectedValue = Convert.ToString(studentBillHistoryObj.AcaCalId);

                if (studentBillHistoryObj.Fees >= 0 && studentBillHistoryObj.DiscountAmount == 0)
                {
                    txtFeeAmount.Text = Convert.ToString(studentBillHistoryObj.Fees);
                    txtDiscountAmount.Enabled = false;
                    txtPaymentAmount.Enabled = false;
                }
                else
                {
                    txtFeeAmount.Enabled = false;
                    txtPaymentAmount.Enabled = false;
                    txtDiscountAmount.Text = Convert.ToString(studentBillHistoryObj.DiscountAmount);
                }
                UpdateButton.Visible = true;
                PaymentUpdateButton.Visible = false;
                UpdateButton.CommandArgument = Convert.ToString(studentBillHistoryObj.BillHistoryId);
                PaymentUpdateButton.CommandArgument = null;
            }

            if (studentBillHistoryObj.StudentCourseHistoryId > 0)
            {
                StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(studentBillHistoryObj.StudentCourseHistoryId);
                LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(studentCourseHistory.CourseID, studentCourseHistory.VersionID);
                lblCourseName.Text = Convert.ToString(courseObj.Title);
            }
            else
            {
                lblCourseName.Visible = false;
                lblCourseName.Text = null;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void LoadTrimesterDDL(int programId)
    {
        LogicLayer.BusinessObjects.Program program = ProgramManager.GetById(programId);

        List<LogicLayer.BusinessObjects.AcademicCalender> sessionList = new List<LogicLayer.BusinessObjects.AcademicCalender>();
        if (program != null)
            sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);

        ddlSession.Items.Clear();
        ddlSession.AppendDataBoundItems = true;

        if (sessionList != null)
        {
            // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

            ddlSession.Items.Add(new ListItem("-Select-", "0"));
            ddlSession.DataTextField = "FullCode";
            ddlSession.DataValueField = "AcademicCalenderID";

            ddlSession.DataSource = sessionList;
            ddlSession.DataBind();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int billHistoryId = int.Parse(btn.CommandArgument.ToString());
            studentBillHistoryDTOList = SessionManager.GetListFromSession<StudentPaymentLadgerDTO>("_studentBillHistoryList");
            StudentPaymentLadgerDTO studentBillHistoryObj = studentBillHistoryDTOList.Where(d => d.BillHistoryId == billHistoryId).FirstOrDefault();
            if (studentBillHistoryObj.CollectionHistoryId != 0)
            {
                CollectionHistory collectionHistoryObj = CollectionHistoryManager.GetById(studentBillHistoryObj.CollectionHistoryId);
                collectionHistoryObj.IsDeleted = true;
                collectionHistoryObj.ModifiedBy = userObj.Id;
                collectionHistoryObj.ModifiedDate = DateTime.Now;
                bool result = CollectionHistoryManager.Update(collectionHistoryObj);
                if (result)
                {
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ddlSession.SelectedItem.Text,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Collection History Deletion",
                                                         userObj.LogInID + " deleted collection history for student roll" + txtStudent.Text + " where CollectionHistoryID=" + collectionHistoryObj.CollectionHistoryId,
                                                         userObj.LogInID + " is Load Page",
                                                          ((int)CommonEnum.PageName.StudentGeneralBill).ToString(),
                                                         CommonEnum.PageName.StudentGeneralBill.ToString(),
                                                         _pageUrl,
                                                         txtStudent.Text);
                    #endregion
                    lblMessage.Text = "Collection history updated successfully";
                }
                else
                {
                    lblMessage.Text = "Collection history could not updated successfully";
                }
            }
            if (studentBillHistoryObj.BillHistoryId != 0 && studentBillHistoryObj.CollectionHistoryId == 0)
            {
                BillHistory billHistoryObj = BillHistoryManager.GetById(studentBillHistoryObj.BillHistoryId);
                billHistoryObj.IsDeleted = true;
                billHistoryObj.ModifiedBy = userObj.Id;
                billHistoryObj.ModifiedDate = DateTime.Now;
                bool result = BillHistoryManager.Update(billHistoryObj);
                if (result)
                {
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ddlSession.SelectedItem.Text,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Bill History Deletion",
                                                         userObj.LogInID + " deleted billing history for student roll" + txtStudent.Text + " where BillHistoryID=" + billHistoryObj.BillHistoryId + " with type definition " + billHistoryObj.FeeTypeId,
                                                         userObj.LogInID + " is Load Page",
                                                          ((int)CommonEnum.PageName.StudentGeneralBill).ToString(),
                                                         CommonEnum.PageName.StudentGeneralBill.ToString(),
                                                         _pageUrl,
                                                         txtStudent.Text);
                    #endregion
                    lblMessage.Text = "Bill history updated successfully";
                }
                else { lblMessage.Text = "Bill history could not updated successfully"; }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int billHistoryId = Convert.ToInt32(Convert.ToInt32(UpdateButton.CommandArgument));
            BillHistory billHistoryObj = BillHistoryManager.GetById(billHistoryId);
            bool result = false;
            if (txtFeeAmount.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtFeeAmount.Text) >= 0)
                {
                    billHistoryObj.Remark = txtRemark.Text;
                    billHistoryObj.Fees = Convert.ToDecimal(txtFeeAmount.Text);
                    billHistoryObj.AcaCalId = Convert.ToInt32(ddlSession.SelectedValue);
                    billHistoryObj.BillingDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                    billHistoryObj.ModifiedBy = userObj.Id;
                    billHistoryObj.ModifiedDate = DateTime.Now;
                    result = BillHistoryManager.Update(billHistoryObj);
                    if (result)
                    {
                        lblNewMessage.Visible = true;
                        lblNewMessage.Text = "Bill history edited successfully";
                        #region Log Insert

                        LogGeneralManager.Insert(
                                                             DateTime.Now,
                                                             "",
                                                             ddlSession.SelectedItem.Text,
                                                             userObj.LogInID,
                                                             "",
                                                             "",
                                                             "Bill History Edit",
                                                             userObj.LogInID + " edited bill of amount " + txtFeeAmount.Text + " for Academic Calendar " + ddlSession.SelectedItem.Text + " where BillHistoryID=" + billHistoryObj.BillHistoryId + " with type definition " + billHistoryObj.FeeTypeId,
                                                             userObj.LogInID + " is Load Page",
                                                              ((int)CommonEnum.PageName.StudentGeneralBill).ToString(),
                                                             CommonEnum.PageName.StudentGeneralBill.ToString(),
                                                             _pageUrl,
                                                             txtStudent.Text);
                        #endregion
                        LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(billHistoryObj.StudentId);
                        BillHistoryGridBind(studentObj);
                    }
                    else
                    {
                        lblNewMessage.Visible = true;
                        lblNewMessage.Text = "Bill history could not edited successfully";
                    }
                }
                else
                {
                    lblNewMessage.Visible = true;
                    lblNewMessage.Text = "Please provide a non negetive amount as fees.";
                }
            }
            if (txtDiscountAmount.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtDiscountAmount.Text) < 0)
                {
                    billHistoryObj.Fees = Convert.ToDecimal(txtDiscountAmount.Text);
                    billHistoryObj.ModifiedBy = userObj.Id;
                    billHistoryObj.ModifiedDate = DateTime.Now;
                    result = BillHistoryManager.Update(billHistoryObj);
                    if (result)
                    {
                        lblNewMessage.Visible = true;
                        lblNewMessage.Text = "Discount edited successfully";
                        #region Log Insert

                        LogGeneralManager.Insert(
                                                             DateTime.Now,
                                                             "",
                                                             ddlSession.SelectedItem.Text,
                                                             userObj.LogInID,
                                                             "",
                                                             "",
                                                             "Bill History Edit",
                                                             userObj.LogInID + " edited bill with discounted amount " + txtFeeAmount.Text + " for Academic Calendar " + ddlSession.SelectedItem.Text + " where BillHistoryID=" + billHistoryObj.BillHistoryId + " with type definition " + billHistoryObj.FeeTypeId,
                                                             userObj.LogInID + " is Load Page",
                                                              ((int)CommonEnum.PageName.StudentGeneralBill).ToString(),
                                                             CommonEnum.PageName.StudentGeneralBill.ToString(),
                                                             _pageUrl,
                                                             txtStudent.Text);
                        #endregion
                        LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(billHistoryObj.StudentId);
                        BillHistoryGridBind(studentObj);
                    }
                    else
                    {
                        lblNewMessage.Visible = true;
                        lblNewMessage.Text = "Discount could not edited successfully";
                    }
                }
                else
                {
                    lblNewMessage.Visible = true;
                    lblNewMessage.Text = "Please provide a non positive amount for discount.";
                }
            }


            this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            lblNewMessage.Visible = true;
            lblNewMessage.Text = ex.Message;
        }
    }

    protected void btnUpdatePayment_Click(object sender, EventArgs e)
    {
        try
        {
            int collectionHistoryId = Convert.ToInt32(Convert.ToInt32(PaymentUpdateButton.CommandArgument));
            CollectionHistory collectionHistoryObj = CollectionHistoryManager.GetById(collectionHistoryId);
            CollectionHistory collectionHistoryObj2 = CollectionHistoryManager.GetById(collectionHistoryId);
            if (txtPaymentAmount.Text != string.Empty)
            {
                collectionHistoryObj.Amount = Convert.ToDecimal(txtPaymentAmount.Text);
            }
            collectionHistoryObj.AcaCalId = Convert.ToInt32(ddlSession.SelectedValue);
            collectionHistoryObj.MoneyReciptSerialNo = txtMoneyReceiptNo.Text.Trim();
            collectionHistoryObj.Comments = txtRemark.Text;
            collectionHistoryObj.CollectionDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
            collectionHistoryObj.ModifiedBy = userObj.Id;
            collectionHistoryObj.ModifiedDate = DateTime.Now;
            if (txtMoneyReceiptNo.Text.Trim() != collectionHistoryObj2.MoneyReciptSerialNo)
            {
                if (IsDuplicateMoneyReceipt(collectionHistoryObj.MoneyReciptSerialNo, collectionHistoryObj.PaymentType))
                {
                    UpdateCollectionHistory(collectionHistoryObj);
                }
                else
                {
                    lblNewMessage.Text = "Money receipt no is duplicate.";
                }
            }
            else
            {
                UpdateCollectionHistory(collectionHistoryObj);
            }
            this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            lblNewMessage.Visible = true;
            lblNewMessage.Text = ex.Message;
        }
    }

    private void UpdateCollectionHistory(CollectionHistory collectionHistoryObj)
    {
        bool result = CollectionHistoryManager.Update(collectionHistoryObj);
        if (result)
        {
            lblNewMessage.Visible = true;
            lblNewMessage.Text = "Collection history edited successfully";
            LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(collectionHistoryObj.StudentId);
            BillHistoryGridBind(studentObj);
        }
        else
        {
            lblNewMessage.Visible = true;
            lblNewMessage.Text = "Collection history could not edited successfully";
        }
    }

    private bool IsDuplicateMoneyReceipt(string moneyReceiptNo, string paymentType)
    {
        bool isDuplicate = CollectionHistoryManager.IsDuplicateMoneyReceipt(moneyReceiptNo, paymentType);
        return isDuplicate;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblNewMessage.Text = null;
        lblNewMessage.Visible = false;
        txtPaymentAmount.Enabled = true;
        txtPaymentAmount.Text = string.Empty;
        txtFeeAmount.Text = string.Empty;
        txtFeeAmount.Enabled = true;
        txtDiscountAmount.Enabled = true;
        txtDiscountAmount.Text = string.Empty;
        lblMoneyReceiptNo.Visible = false;
        txtMoneyReceiptNo.Visible = false;
        txtMoneyReceiptNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        lblComment.Visible = false;
        txtRemark.Visible = false;
        DateTextBox.Visible = false;
        DateTextBox.Text = string.Empty;
        lblPaymentType.Visible = false;
        rdlPaymentType.Enabled = false;
        rdlPaymentType.Visible = false;
        lblDate.Visible = false;
        lblCourseName.Visible = false;
        lblCourseName.Text = null;
        UpdateButton.Visible = true;
        PaymentUpdateButton.Visible = true;
    }

    protected void gvBillView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    private void LoadSession(List<int> acaCalIdList)
    {

        List<LogicLayer.BusinessObjects.AcademicCalender> sessionList = new List<LogicLayer.BusinessObjects.AcademicCalender>();

        foreach (int item in acaCalIdList)
        {
            LogicLayer.BusinessObjects.AcademicCalender acacal = AcademicCalenderManager.GetById(item);
            sessionList.Add(acacal);

        }
        ddlBillPostingSession.Items.Clear();
        ddlBillPostingSession.Items.Add(new ListItem("-Select-", "0"));
        ddlBillPostingSession.AppendDataBoundItems = true;
        ddlBillPostingSession.DataTextField = "FullCode";
        ddlBillPostingSession.DataValueField = "AcademicCalenderID";

        ddlBillPostingSession.DataSource = sessionList;
        ddlBillPostingSession.DataBind();

    }

    protected void ddlBillPostingSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnLoad_Click(null,null);
    }
}