using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.miu.bill
{
    public partial class StudentDiscountCurrentPage : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

         [Serializable]
        public class StudentCurrentDiscountLocal
        {
            public int StudentID { get; set; }
            public string Roll { get; set; }
            public string Name { get; set; }
            public int BatchId { get; set; }
            public string BatchCode { get; set; }
            public int ProgramId { get; set; }
            public string Program { get; set; }
            public int StudentDiscountId { get; set; }
            public int StudentDiscountCurrentDetailsId { get; set; }
            public int TypeDefinitionId { get; set; }
            public string DiscountType { get; set; }
            public decimal TypePercentage { get; set; }
            public int AcaCalSession { get; set; }
            public string Comments { get; set; }
            public decimal CGPA { get; set; }
        }

        private bool FlagIsLoad = false;

        List<StudentCurrentDiscountLocal> _studentInitialDiscountLocalList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            base.CheckPage_Load();
            try
            {
                ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
                _scriptMan.AsyncPostBackTimeout = 36000;

                if (!IsPostBack)
                {
                    SessionManager.DeletFromSession("_FlagIsLoad");
                    FillDiscountTypeChkList();
                }
            }
            catch { }
        }

        private void FillDiscountTypeChkList()
        {
            chkTyprDefinition.DataTextField = "Definition";
            chkTyprDefinition.DataValueField = "TypeDefinitionID";
            chkTyprDefinition.DataSource = TypeDefinitionManager.GetAll("Discount").OrderBy(o => o.Priority).ToList();
            chkTyprDefinition.DataBind();

            chkTyprDefinition.SelectedIndex = 0;
        }

        private void CleareGrid()
        {
            gvStudentDiscountInitial.DataSource = null;
            gvStudentDiscountInitial.DataBind();

            SessionManager.DeletFromSession("_FlagIsLoad");
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession1.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            CleareGrid();
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            CleareGrid();
        }

        protected void OnSessionSelectedIndexChanged1(object sender, EventArgs e)
        {
            CleareGrid();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                CleareGrid();

                FlagIsLoad = true;
                SessionManager.SaveObjToSession(FlagIsLoad, "_FlagIsLoad");

                lblCount.Text = "0";
                lblMessage.Text = "";
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int acaCalBatchId = Convert.ToInt32(ucBatch.selectedValue);
                int acaCalSessionId = Convert.ToInt32(ucSession.selectedValue);
                int resultSessionId = Convert.ToInt32(ucSession1.selectedValue);
                string roll = txtStudent.Text.Trim();

                if (acaCalSessionId == 0 && string.IsNullOrEmpty(roll))
                {
                    lblMessage.Text = "Please select Session.";
                    lblMessage.Focus();
                    return;
                }
                else if (programId == 0 && string.IsNullOrEmpty(roll))
                {
                    lblMessage.Text = "Please select Program.";
                    lblMessage.Focus();
                    return;
                }
                else if (resultSessionId == 0 && string.IsNullOrEmpty(roll))
                {
                    lblMessage.Text = "Please select result session.";
                    lblMessage.Focus();
                    return;
                }

                int filterFrom = 0;
                if (!string.IsNullOrEmpty(txtFilterFrom.Text.Trim()))
                    filterFrom = Convert.ToInt32(txtFilterFrom.Text.Trim());

                int filterTo = 0;
                if (!string.IsNullOrEmpty(txtFilterTo.Text.Trim()))
                    filterTo = Convert.ToInt32(txtFilterTo.Text.Trim());

                int selectedCount = chkTyprDefinition.Items.Cast<ListItem>().Count(li => li.Selected);
                if (selectedCount == 0)
                {
                    lblMessage.Text = "Please Select atleast one Discount.";
                    return;
                }

                //string processResult = StudentACUDetailManager.Calculate_GpaCgpa(acaCalSessionId, programId, acaCalBatchId, "");

                //List<StudentDTO> studentDtoList = StudentManager.GetAllDTOByProgramOrBatchOrRoll(programId, acaCalBatchId, roll);
                List<StudentDTO> studentDtoList = StudentManager.GetAllDTOByProgramBatchResultSessionRoll(programId, acaCalBatchId, acaCalSessionId, resultSessionId, roll);
                List<TypeDefinition> typeDefinitionList = TypeDefinitionManager.GetAll("Discount").OrderBy(o => o.Priority).ToList(); ;

                foreach (ListItem item in chkTyprDefinition.Items)
                {
                    if (!item.Selected)
                    {
                        var itemToRemove = typeDefinitionList.SingleOrDefault(r => r.TypeDefinitionID == Convert.ToInt32(item.Value));
                        if (itemToRemove != null)
                            typeDefinitionList.Remove(itemToRemove);
                    }
                }

                if (typeDefinitionList == null || typeDefinitionList.Count == 0)
                {
                    lblMessage.Text = "Error: 1001; No discount found.";
                    return;
                }

                List<StudentCurrentDiscountLocal> studentCurrentDiscountLocalList = new List<StudentCurrentDiscountLocal>();
                StudentCurrentDiscountLocal studentInitialDiscountLocal = null;

                foreach (StudentDTO student in studentDtoList)
                {
                    foreach (TypeDefinition item in typeDefinitionList)
                    {
                        studentInitialDiscountLocal = new StudentCurrentDiscountLocal();
                        studentInitialDiscountLocal.StudentID = student.StudentID;
                        studentInitialDiscountLocal.Roll = student.Roll;
                        studentInitialDiscountLocal.Name = student.Name;
                        studentInitialDiscountLocal.Program = student.Program;
                        studentInitialDiscountLocal.CGPA = student.CGPA;
                        studentInitialDiscountLocal.ProgramId = student.ProgramID;
                        studentInitialDiscountLocal.BatchId = student.BatchId;
                        studentInitialDiscountLocal.BatchCode = student.Batch;
                        studentInitialDiscountLocal.DiscountType = item.Definition;
                        studentInitialDiscountLocal.TypeDefinitionId = item.TypeDefinitionID;
                        studentCurrentDiscountLocalList.Add(studentInitialDiscountLocal);
                    }
                }

                List<StudentDiscountCurrentDetailsDTO> studentDiscountCurrentList = StudentDiscountCurrentDetailsManager.GetAllDiscountCurrentByProgramBatchRoll(programId, acaCalBatchId, acaCalSessionId, roll);

                foreach (StudentDiscountCurrentDetailsDTO item in studentDiscountCurrentList)
                {
                    StudentCurrentDiscountLocal sdiDto = studentCurrentDiscountLocalList.Find(d => d.StudentID == item.StudentID && d.TypeDefinitionId == item.TypeDefinitionId);
                    if (sdiDto != null)
                    {
                        sdiDto.TypePercentage = item.TypePercentage;
                        sdiDto.StudentDiscountId = item.StudentDiscountId;
                        sdiDto.StudentDiscountCurrentDetailsId = item.StudentDiscountCurrentDetailsId;
                        sdiDto.AcaCalSession = item.AcaCalSession;
                        sdiDto.Comments = item.Comments == null ? "" : item.Comments.ToString();
                    }
                }

                studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.OrderBy(d => d.Roll).ToList();

                //if (filterFrom == 0 && filterTo == 0)
                //{
                //    studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.Where(o => o.TypePercentage >= filterFrom && o.TypePercentage <= filterTo).ToList();
                //}
                if (filterFrom > 0 && filterTo == 0)
                {
                    studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.Where(o => o.TypePercentage == filterFrom).ToList();
                }
                else if (filterFrom == 0 && filterTo > 0)
                {
                    studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.Where(o => o.TypePercentage > 0 && o.TypePercentage <= filterTo).ToList();
                }

                if (chkHasComment.Checked)
                {
                    studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.Where(o => !string.IsNullOrEmpty(o.Comments)).ToList();
                }

                SessionManager.SaveListToSession<StudentCurrentDiscountLocal>(studentCurrentDiscountLocalList, "_studentCurrentDiscountLocalList");
                gvStudentDiscountInitial.DataSource = studentCurrentDiscountLocalList;
                gvStudentDiscountInitial.DataBind();
                lblCount.Text = studentCurrentDiscountLocalList.Count.ToString();
                SessionManager.SaveObjToSession<string>(lblCount.Text, " lblCount_Text ");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SendSMS(string PhoneNo, string roll, string msg)
        {
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            bool updated = SMSBasicSetupManager.Update(smsSetup);
            if (!string.IsNullOrEmpty(PhoneNo) && PhoneNo.Count() == 14 && PhoneNo.Contains("+") && smsSetup.RemainingSMS > 0 && smsSetup.WaiverPostingStatus == true)
            {
                //SMSManager.Send(PhoneNo, roll, msg, ResultCallBack);
            }
            else
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), roll, "Number format or setup related error", false);
        }

        void ResultCallBack(string[] data)
        {
            if (data[2].Contains("<status>0</status>"))
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], true);
            }
            else
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], false);
            }
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            smsSetup.RemainingSMS = smsSetup.RemainingSMS - 1;
            bool updated = SMSBasicSetupManager.Update(smsSetup);
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem item in chkTyprDefinition.Items)
                {
                    item.Selected = chkAll.Checked;
                }

                lblCount.Text = SessionManager.GetObjFromSession<string>(" lblCount_Text ");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvStudentDiscountInitial_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                _studentInitialDiscountLocalList = SessionManager.GetListFromSession<StudentCurrentDiscountLocal>("_studentCurrentDiscountLocalList");

                string sortdirection = string.Empty;
                if (Session["direction"] != null)
                {
                    if (Session["direction"].ToString() == "ASC")
                    {
                        sortdirection = "DESC";
                    }
                    else
                    {
                        sortdirection = "ASC";
                    }
                }
                else
                {
                    sortdirection = "DESC";
                }
                Session["direction"] = sortdirection;
                Sort(_studentInitialDiscountLocalList, e.SortExpression.ToString(), sortdirection);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Sort(List<StudentCurrentDiscountLocal> list, String sortBy, String sortDirection)
        {
            if (sortDirection == "ASC")
            {
                list.Sort(new GenericComparer<StudentCurrentDiscountLocal>(sortBy, (int)SortDirection.Ascending));
            }
            else
            {
                list.Sort(new GenericComparer<StudentCurrentDiscountLocal>(sortBy, (int)SortDirection.Descending));
            }
            gvStudentDiscountInitial.DataSource = list;
            gvStudentDiscountInitial.DataBind();
            lblCount.Text = list.Count.ToString();
        }

        protected void lBtnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int acaCalSessionId = Convert.ToInt32(ucSession.selectedValue);
                if (acaCalSessionId == 0)
                {
                    lblMessage.Text = "Please select Session.";
                    lblMessage.Focus();
                    return;
                }

                lblMessage.Text = "";
                try
                {
                    foreach (GridViewRow row in gvStudentDiscountInitial.Rows)
                    {
                        TextBox txtComments = (TextBox)row.FindControl("txtComments");
                        string Comments = txtComments.Text;

                        TextBox txtDiscount = (TextBox)row.FindControl("txtPercentage");
                        decimal discount = Convert.ToDecimal(txtDiscount.Text);

                        HiddenField hdnStudentID = (HiddenField)row.FindControl("hdnStudentID");
                        string studentId = hdnStudentID.Value.ToString();

                        HiddenField hdnTypeDefinitionId = (HiddenField)row.FindControl("hdnTypeDefinitionId");
                        int typeDefinitionId = Convert.ToInt32(hdnTypeDefinitionId.Value.ToString());
                        Label DiscountType = (Label)row.FindControl("txtDiscountType");

                        int stdDiscountMasterId = 0;
                        int stdDiscountInitialId = 0;

                        StudentDiscountCurrentDetails stdDiscountCurrent = null;
                        Student student = StudentManager.GetById(Convert.ToInt32(studentId));

                        StudentDiscountMaster stdDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
                        if (stdDiscountMaster == null)
                        {
                            stdDiscountMaster = new StudentDiscountMaster();
                            stdDiscountMaster.BatchId = student.BatchId;
                            stdDiscountMaster.ProgramId = student.ProgramID;
                            stdDiscountMaster.StudentId = student.StudentID;
                            stdDiscountMaster.CreatedBy = -1;
                            stdDiscountMaster.CreatedDate = DateTime.Now;
                            stdDiscountMaster.ModifiedBy = -1;
                            stdDiscountMaster.ModifiedDate = DateTime.Now;

                            stdDiscountMasterId = StudentDiscountMasterManager.Insert(stdDiscountMaster);
                        }

                        int StudentDiscountId = stdDiscountMasterId == 0 ? stdDiscountMaster.StudentDiscountId : stdDiscountMasterId;

                        stdDiscountCurrent = StudentDiscountCurrentDetailsManager.GetBy(StudentDiscountId, typeDefinitionId, acaCalSessionId);

                        if (stdDiscountCurrent == null)
                        {
                            stdDiscountCurrent = new StudentDiscountCurrentDetails();
                            stdDiscountCurrent.StudentDiscountId = StudentDiscountId;
                            stdDiscountCurrent.TypeDefinitionId = typeDefinitionId;
                            stdDiscountCurrent.TypePercentage = discount;
                            stdDiscountCurrent.Comments = Comments;
                            stdDiscountCurrent.AcaCalSession = acaCalSessionId;

                            stdDiscountInitialId = StudentDiscountCurrentDetailsManager.Insert(stdDiscountCurrent);
                        }
                        else
                        {
                            stdDiscountCurrent.TypePercentage = discount;
                            stdDiscountCurrent.Comments = Comments;
                            bool boo = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);
                        }
                        LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            "",
                                                            ucSession.selectedText,
                                                            userObj.LogInID,
                                                            "",
                                                            "",
                                                            "Student Discount Current Save",
                                                            userObj.LogInID + " saved " + discount + " Percentage discount on " + DiscountType.Text + " with comments " + Comments + " for session " + ucSession.selectedText + " and for result Session " + ucSession1.selectedText,
                                                            userObj.LogInID + " is saved Discount Current",
                                                             ((int)CommonEnum.PageName.StudentDiscountCurrentPage).ToString(),
                                                            CommonEnum.PageName.StudentDiscountCurrentPage.ToString(),
                                                            _pageUrl,
                                                            student.Roll);
                    }

                    lblCount.Text = gvStudentDiscountInitial.Rows.Count.ToString();
                }
                catch (Exception)
                {
                    lblMessage.Text = "Error 101:" + "Please contact with system admin.";
                }

                lblMessage.Text = "Data Update successfully.";
                btnLoad_Click(null, null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int acaCalSessionId = Convert.ToInt32(ucSession.selectedValue);
                if (acaCalSessionId == 0)
                {
                    lblMessage.Text = "Please select Session.";
                    lblMessage.Focus();
                    return;
                }

                lblMessage.Text = "";
                try
                {
                    GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;

                    TextBox txtDiscount = (TextBox)gvrow.FindControl("txtPercentage");
                    decimal Discount = Convert.ToDecimal(txtDiscount.Text);

                    TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
                    string Comments = txtComments.Text;

                    HiddenField hdnStudentID = (HiddenField)gvrow.FindControl("hdnStudentID");
                    string StudentId = hdnStudentID.Value.ToString();

                    HiddenField hdnTypeDefinitionId = (HiddenField)gvrow.FindControl("hdnTypeDefinitionId");
                    int typeDefinitionId = Convert.ToInt32(hdnTypeDefinitionId.Value.ToString());
                    Label DiscountType = (Label)gvrow.FindControl("txtDiscountType");

                    int stdDiscountMasterId = 0;
                    int stdDiscountInitialId = 0;

                    StudentDiscountCurrentDetails stdDiscountCurrent = null;
                    Student student = StudentManager.GetById(Convert.ToInt32(StudentId));

                    StudentDiscountMaster stdDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
                    if (stdDiscountMaster == null)
                    {
                        stdDiscountMaster = new StudentDiscountMaster();
                        stdDiscountMaster.BatchId = student.BatchId;
                        stdDiscountMaster.ProgramId = student.ProgramID;
                        stdDiscountMaster.StudentId = student.StudentID;
                        stdDiscountMaster.CreatedBy = -1;
                        stdDiscountMaster.CreatedDate = DateTime.Now;
                        stdDiscountMaster.ModifiedBy = -1;
                        stdDiscountMaster.ModifiedDate = DateTime.Now;

                        stdDiscountMasterId = StudentDiscountMasterManager.Insert(stdDiscountMaster);
                    }

                    int StudentDiscountId = stdDiscountMasterId == 0 ? stdDiscountMaster.StudentDiscountId : stdDiscountMasterId;

                    stdDiscountCurrent = StudentDiscountCurrentDetailsManager.GetBy(StudentDiscountId, typeDefinitionId, acaCalSessionId);

                    if (stdDiscountCurrent == null)
                    {
                        stdDiscountCurrent = new StudentDiscountCurrentDetails();
                        stdDiscountCurrent.StudentDiscountId = StudentDiscountId;
                        stdDiscountCurrent.TypeDefinitionId = typeDefinitionId;
                        stdDiscountCurrent.TypePercentage = Discount;
                        stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                        stdDiscountCurrent.Comments = Comments;
                        stdDiscountInitialId = StudentDiscountCurrentDetailsManager.Insert(stdDiscountCurrent);
                    }
                    else
                    {
                        stdDiscountCurrent.TypePercentage = Discount;
                        stdDiscountCurrent.Comments = Comments;
                        bool boo = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);
                    }

                    LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        ucSession.selectedText,
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        "Student Discount Current Save",
                                                        userObj.LogInID + " saved " + Discount + " Percentage discount on " + DiscountType.Text + " with comments " + Comments + " for session " + ucSession.selectedText + " and for result Session " + ucSession1.selectedText,
                                                        userObj.LogInID + " is saved Discount Current",
                                                         ((int)CommonEnum.PageName.StudentDiscountCurrentPage).ToString(),
                                                        CommonEnum.PageName.StudentDiscountCurrentPage.ToString(),
                                                        _pageUrl,
                                                        student.Roll);
                    lblMessage.Text = "Data Update successfully.";
                    lblCount.Text = gvStudentDiscountInitial.Rows.Count.ToString();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error 101:" + "Please contact with system admin.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPostDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                lblCount.Text = "0";
                bool isExecute = false;

                int acaCalSessionId = Convert.ToInt32(ucSession.selectedValue);
                if (acaCalSessionId == 0)
                {
                    lblMessage.Text = "Please select Posting Session.";
                    return;
                }

                foreach (GridViewRow row in gvStudentDiscountInitial.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("chkStudent");

                    if (ckBox.Checked)
                    {
                        Label Roll = (Label)row.FindControl("lblRoll");
                        TextBox txtDiscount = (TextBox)row.FindControl("txtPercentage");

                        Label DiscountType = (Label)row.FindControl("txtDiscountType");

                        HiddenField hdnStudentID = (HiddenField)row.FindControl("hdnStudentId");

                        HiddenField hdnBatchId = (HiddenField)row.FindControl("hdnBatchId");
                        HiddenField hdnProgramId = (HiddenField)row.FindControl("hdnProgramId");
                        HiddenField hdnTypeDefinitionId = (HiddenField)row.FindControl("hdnTypeDefinitionId");

                        int typeDefinitionId = Convert.ToInt32(hdnTypeDefinitionId.Value);

                        isExecute = StudentDiscountCurrentDetailsManager.DiscountPostingWaiver(
                                                                                           Convert.ToInt32(hdnStudentID.Value),
                                                                                           Convert.ToInt32(hdnBatchId.Value),
                                                                                           Convert.ToInt32(hdnProgramId.Value),
                                                                                           acaCalSessionId,
                                                                                           Convert.ToInt32(hdnTypeDefinitionId.Value)
                                                                                           );
                        if (isExecute)
                        {
                            LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            "",
                                                            ucSession.selectedText,
                                                            userObj.LogInID,
                                                            "",
                                                            "",
                                                            "Student Discount Current Post",
                                                            userObj.LogInID + " post discount " + txtDiscount.Text + " percentage on " + DiscountType.Text,
                                                            userObj.LogInID + " is post Discount Current",
                                                             ((int)CommonEnum.PageName.StudentDiscountCurrentPage).ToString(),
                                                            CommonEnum.PageName.StudentDiscountCurrentPage.ToString(),
                                                            _pageUrl,
                                                            Roll.Text);
                            PersonBlockDTO person = PersonBlockManager.GetByRoll(Roll.Text);
                            Student _student = StudentManager.GetByRoll(Roll.Text);
                            string msg = "ID-" + Roll.Text + ",your waiver for " + AcademicCalenderManager.GetById(Convert.ToInt32(ucSession.selectedValue)).FullCode + " has been posted.Your current dues is TK. "
                                + person.Dues.ToString() + ".Please pay your dues as early as possible.";
                            SendSMS(_student.BasicInfo.SMSContactSelf, _student.Roll, msg);
                        }
                        else
                        {
                            LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            "",
                                                            ucSession.selectedText,
                                                            userObj.LogInID,
                                                            "",
                                                            "",
                                                            "Student Discount Current Post try",
                                                            userObj.LogInID + " tried to post discount " + txtDiscount.Text + " percentage on " + DiscountType.Text + " but did not Post",
                                                            userObj.LogInID + " is tried post Discount Current",
                                                             ((int)CommonEnum.PageName.StudentDiscountCurrentPage).ToString(),
                                                            CommonEnum.PageName.StudentDiscountCurrentPage.ToString(),
                                                            _pageUrl,
                                                            Roll.Text);
                        }




                    }
                }
                btnLoad_Click(null, null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkAllStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                CheckBox chkHeader = (CheckBox)gvStudentDiscountInitial.HeaderRow.FindControl("chkAllStudentHeadre");
                CheckBox chkFooter = (CheckBox)gvStudentDiscountInitial.FooterRow.FindControl("chkAllStudentFooter");

                chkHeader.Checked = chkFooter.Checked = chk.Checked;

                foreach (GridViewRow row in gvStudentDiscountInitial.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("chkStudent");
                    ckBox.Checked = chk.Checked;
                }

                lblCount.Text = SessionManager.GetObjFromSession<string>(" lblCount_Text ");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                lblCount.Text = "0";
                CleareGrid();

                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                string roll = txtStudent.Text.Trim();
                int resultSessionId = Convert.ToInt32(ucSession1.selectedValue);

                if (sessionId == 0)
                {
                    lblMessage.Text = "Please select Session.";
                    lblMessage.Focus();
                    return;
                }
                else if (programId == 0 && string.IsNullOrEmpty(roll))
                {
                    lblMessage.Text = "Please select program or enter student ID.";
                    lblMessage.Focus();
                    return;
                }
                else
                {
                    string message = " Generate Student Discount Current for Progam " + ucProgram.selectedText + " and batch " + ucBatch.selectedText + " and for Session " + ucSession.selectedText + " and result session " + ucSession1.selectedText;

                    if (roll != null && roll != "")
                    {
                        message += " and for a student whose roll is " + roll;
                    }
                    //List<StudentDTO> studentList = StudentManager.GetAllDTOByProgramOrBatchOrRoll(programId, acaCalBatchId, roll);
                    //List<StudentDTO> studentList = StudentManager.GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(programId, batchId, roll);
                    List<StudentDTO> studentList = StudentManager.GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(programId, batchId, roll, sessionId, resultSessionId);

                    List<StudentDiscountInitialDetailsDTO> studentDiscountInitialList = StudentDiscountInitialDetailsManager.GetAllDiscountInitialByProgramBatchRoll(programId, batchId, roll);

                    if (studentList != null)
                    {
                        foreach (StudentDTO item in studentList)
                        {
                            List<StudentDiscountInitialDetailsDTO> studentDiscountInitials = studentDiscountInitialList.Where(o => o.StudentID == item.StudentID).ToList();

                            if (studentDiscountInitials != null)
                            {
                                StudentDiscountCurrentDetailsManager.Delete(item.StudentID, sessionId);

                                Batch batch = BatchManager.GetById(item.BatchId);

                                if (batch.AcaCalId == sessionId)
                                {
                                    DiscountTransferFromInitialToCurrentForFirstSessionStudent(studentDiscountInitials, sessionId);
                                }
                                else if (item.AcademicCalenderYear < 2013)
                                {
                                    DiscountTransferFromInitialToCurrentRuleBefore2013(studentDiscountInitials, sessionId);
                                }
                                else
                                {
                                    DiscountTransferFromInitialToCurrentRuleFrom2013(studentDiscountInitials, item.CGPA, sessionId);
                                }
                            }
                        }
                    }
                    string selectedValue = "";
                    foreach (ListItem item in chkTyprDefinition.Items)
                    {
                        if (item.Selected)
                        {
                            selectedValue += item.Text + " , ";
                        }
                    }
                    LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        ucSession.selectedText,
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        "Student Discount Current Generate",
                                                        userObj.LogInID + message + " with Discount type " + selectedValue,
                                                        userObj.LogInID + " is Generate Discount Current",
                                                         ((int)CommonEnum.PageName.StudentDiscountCurrentPage).ToString(),
                                                        CommonEnum.PageName.StudentDiscountCurrentPage.ToString(),
                                                        _pageUrl,
                                                        roll);

                    btnLoad_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void DiscountTransferFromInitialToCurrentForFirstSessionStudent(List<StudentDiscountInitialDetailsDTO> studentDiscountInitials, int sessionId)
        {
            foreach (StudentDiscountInitialDetailsDTO item in studentDiscountInitials)
            {
                if (item.TypePercentage > 0)
                {
                    InsertStudentDiscountCurrentDetails(item, sessionId);
                }
            }
        }

        private void DiscountTransferFromInitialToCurrentRuleFrom2013(List<StudentDiscountInitialDetailsDTO> studentDiscountInitials, decimal cgpa, int sessionId)
        {
            foreach (StudentDiscountInitialDetailsDTO item in studentDiscountInitials)
            {
                if (item.TypePercentage > 0)
                {
                    #region Diploma
                    if (item.TypeDefinitionId == (int)CommonEnum.DiscountType.Diploma)
                    {
                        InsertStudentDiscountCurrentDetails(item, sessionId);
                    }
                    #endregion

                    #region Tuition Waiver
                    else if (item.TypeDefinitionId == (int)CommonEnum.DiscountType.TuitionWaiver)
                    {
                        #region Percentage is 100
                        if (item.TypePercentage == 100)
                        {
                            if (cgpa >= (decimal)3.6)
                            {
                                item.TypePercentage = 100;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.50 && cgpa <= (decimal)3.59)
                            {
                                item.TypePercentage = 50;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.40 && cgpa <= (decimal)3.49)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 10;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region Percentage is 50
                        else if (item.TypePercentage == 50)
                        {
                            if (cgpa >= (decimal)3.50)
                            {
                                item.TypePercentage = 50;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.40 && cgpa <= (decimal)3.49)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 10;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region Percentage is 25
                        else if (item.TypePercentage == 25)
                        {
                            if (cgpa >= (decimal)3.40)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 10;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region Percentage is 10
                        else if (item.TypePercentage == 10)
                        {
                            if (cgpa >= (decimal)3.20)
                            {
                                item.TypePercentage = 10;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Special Waiver 100
                    else if (item.TypeDefinitionId == (int)CommonEnum.DiscountType.SpecialWaiver100)
                    {
                        #region TypePercentage == 100
                        if (item.TypePercentage == 100)
                        {
                            if (cgpa >= (decimal)3.5)
                            {
                                item.TypePercentage = 100;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.40 && cgpa <= (decimal)3.49)
                            {
                                item.TypePercentage = 50;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region TypePercentage == 50
                        else if (item.TypePercentage == 50)
                        {
                            if (cgpa >= (decimal)3.40)
                            {
                                item.TypePercentage = 50;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region TypePercentage == 25
                        else if (item.TypePercentage == 25)
                        {
                            if (cgpa >= (decimal)3.20)
                            {
                                item.TypePercentage = 25;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Special Waiver 75
                    else if (item.TypeDefinitionId == (int)CommonEnum.DiscountType.SpecialWaiver75)
                    {
                        #region TypePercentage == 75
                        if (item.TypePercentage == 75)
                        {
                            if (cgpa >= (decimal)3.5)
                            {
                                item.TypePercentage = 75;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.40 && cgpa <= (decimal)3.49)
                            {
                                item.TypePercentage = 40;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 15;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region TypePercentage == 40
                        else if (item.TypePercentage == 40)
                        {
                            if (cgpa >= (decimal)3.40)
                            {
                                item.TypePercentage = 40;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa >= (decimal)3.20 && cgpa <= (decimal)3.39)
                            {
                                item.TypePercentage = 15;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion

                        #region TypePercentage == 15
                        else if (item.TypePercentage == 15)
                        {
                            if (cgpa >= (decimal)3.20)
                            {
                                item.TypePercentage = 15;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                            else if (cgpa < (decimal)3.20)
                            {
                                item.TypePercentage = 0;
                                InsertStudentDiscountCurrentDetails(item, sessionId);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region Others
                    else
                    {
                        InsertStudentDiscountCurrentDetails(item, sessionId);
                    }
                    #endregion
                }
            }
        }

        private void InsertStudentDiscountCurrentDetails(StudentDiscountInitialDetailsDTO item, int sessionId)
        {
            StudentDiscountCurrentDetails studentDiscountCurrentDetails = new StudentDiscountCurrentDetails();
            studentDiscountCurrentDetails.StudentDiscountCurrentDetailsId = 0;
            studentDiscountCurrentDetails.StudentDiscountId = item.StudentDiscountId;
            studentDiscountCurrentDetails.AcaCalSession = sessionId;
            studentDiscountCurrentDetails.Comments = "Data transfer by system process.";
            studentDiscountCurrentDetails.TypeDefinitionId = item.TypeDefinitionId;
            studentDiscountCurrentDetails.TypePercentage = item.TypePercentage;
            StudentDiscountCurrentDetailsManager.Insert(studentDiscountCurrentDetails);
        }

        private void DiscountTransferFromInitialToCurrentRuleBefore2013(List<StudentDiscountInitialDetailsDTO> studentDiscountInitials, int sessionId)
        {
            foreach (StudentDiscountInitialDetailsDTO item in studentDiscountInitials)
            {
                if (item.TypePercentage > 0)
                {
                    InsertStudentDiscountCurrentDetails(item, sessionId);
                }
            }
        }

        protected void btnRuleApply_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                if (programId == 0)
                {
                    lblMessage.Text = "Please select Program.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Focus();
                    return;
                }

                int acaCalSessionId = Convert.ToInt32(ucSession.selectedValue);
                if (acaCalSessionId == 0)
                {
                    lblMessage.Text = "Please select Session.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Focus();
                    return;
                }

                if (!SessionManager.GetObjFromSession<bool>("_FlagIsLoad"))
                {
                    lblMessage.Text = "Please Load data first.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Focus();
                    return;
                }

                lblMessage.Text = "";

                List<StudentCurrentDiscountLocal> studentCurrentDiscountLocalList = SessionManager.GetListFromSession<StudentCurrentDiscountLocal>("_studentCurrentDiscountLocalList");
                studentCurrentDiscountLocalList = studentCurrentDiscountLocalList.Where(o => o.TypePercentage > 0).ToList();

                if (studentCurrentDiscountLocalList != null)
                {
                    List<StudentCurrentDiscountLocal> distinctList = studentCurrentDiscountLocalList.GroupBy(cust => cust.StudentID).Select(grp => grp.First()).ToList();

                    if (distinctList != null)
                    {
                        foreach (StudentCurrentDiscountLocal item in distinctList)
                        {

                            StudentCurrentDiscountLocal objDip = studentCurrentDiscountLocalList.Where(o => o.TypeDefinitionId == (int)CommonEnum.DiscountType.Diploma &&
                                                                                                            o.StudentID == item.StudentID &&
                                                                                                            o.AcaCalSession == acaCalSessionId).SingleOrDefault();


                            if (objDip != null && objDip.TypePercentage > 0)
                            {
                                List<StudentCurrentDiscountLocal> discountWithoutDiploma = studentCurrentDiscountLocalList.Where(o => o.StudentID == item.StudentID && o.TypeDefinitionId != (int)CommonEnum.DiscountType.Diploma).ToList();

                                if (discountWithoutDiploma != null)
                                {
                                    foreach (StudentCurrentDiscountLocal item_1 in discountWithoutDiploma)
                                    {
                                        StudentDiscountCurrentDetails stdDiscountCurrent = new StudentDiscountCurrentDetails();
                                        if (item_1.TypeDefinitionId != (int)CommonEnum.DiscountType.AdmissionFair)
                                        {

                                            stdDiscountCurrent.StudentDiscountCurrentDetailsId = item_1.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrent.StudentDiscountId = item_1.StudentDiscountId;
                                            stdDiscountCurrent.TypeDefinitionId = item_1.TypeDefinitionId;
                                            stdDiscountCurrent.TypePercentage = 0;
                                            stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                                            stdDiscountCurrent.Comments = "0 by diploma condition.";
                                            bool isExecute = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);
                                        }
                                        else
                                        {
                                            stdDiscountCurrent.StudentDiscountCurrentDetailsId = item_1.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrent.StudentDiscountId = item_1.StudentDiscountId;
                                            stdDiscountCurrent.TypeDefinitionId = item_1.TypeDefinitionId;
                                            stdDiscountCurrent.TypePercentage = 0;
                                            stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                                            stdDiscountCurrent.Comments = "Added with Max discount.";
                                            bool isExecute = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);

                                            StudentDiscountCurrentDetails stdDiscountCurrentMaxPercentageObj = new StudentDiscountCurrentDetails();
                                            stdDiscountCurrentMaxPercentageObj.StudentDiscountCurrentDetailsId = objDip.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrentMaxPercentageObj.StudentDiscountId = objDip.StudentDiscountId;
                                            stdDiscountCurrentMaxPercentageObj.TypeDefinitionId = objDip.TypeDefinitionId;
                                            stdDiscountCurrentMaxPercentageObj.AcaCalSession = objDip.AcaCalSession;
                                            decimal totalPercentage = objDip.TypePercentage + item_1.TypePercentage;
                                            if (totalPercentage > Convert.ToDecimal(100))
                                            {
                                                //decimal admissionPercentage = Convert.ToDecimal(100) - objDip.TypePercentage;
                                                stdDiscountCurrentMaxPercentageObj.TypePercentage = Convert.ToDecimal(100);
                                                stdDiscountCurrentMaxPercentageObj.Comments = objDip.Comments + ", Admission Fair " + item_1.TypePercentage;
                                            }
                                            else
                                            {
                                                stdDiscountCurrentMaxPercentageObj.TypePercentage = objDip.TypePercentage + item_1.TypePercentage;
                                                stdDiscountCurrentMaxPercentageObj.Comments = objDip.Comments + ", Admission Fair " + item_1.TypePercentage;
                                            }
                                            bool result = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrentMaxPercentageObj);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                List<StudentCurrentDiscountLocal> studentDiscounts = studentCurrentDiscountLocalList.Where(o => o.StudentID == item.StudentID).ToList();

                                //decimal admFairDiscount = 0;
                                //int admFairTypeDefId = 0;

                                StudentCurrentDiscountLocal admFairStudentCurrentDiscount = new StudentCurrentDiscountLocal();

                                if (studentDiscounts.Exists(o => o.TypeDefinitionId == 35))
                                {

                                    admFairStudentCurrentDiscount = studentDiscounts.Where(o => o.TypeDefinitionId == 35).Single();
                                    //admFairDiscount = studentDiscounts.Find(o => o.TypeDefinitionId == 35).TypePercentage;
                                    //admFairTypeDefId = studentDiscounts.Find(o => o.TypeDefinitionId == 35).TypeDefinitionId;
                                }

                              //  studentDiscounts = studentDiscounts.Where(o => o.TypeDefinitionId != 35).ToList();

                                decimal maxDiscount = 0;
                                int typeDefinitionId = 0;

                                if (studentDiscounts.Where(o => o.TypeDefinitionId != 35).Count() > 0)
                                {                                     
                                    maxDiscount = studentDiscounts.Where(o => o.TypeDefinitionId != 35).Max(o => o.TypePercentage);
                                    typeDefinitionId = studentDiscounts.Find(o => o.TypePercentage == maxDiscount).TypeDefinitionId;
                                }

                                List<StudentCurrentDiscountLocal> studentDiscountWitnoutMaxDiscount = studentDiscounts.Where(o => o.TypeDefinitionId != typeDefinitionId).ToList();
                                StudentCurrentDiscountLocal studentDiscountMaxDiscountObj = studentDiscounts.Where(o => o.TypeDefinitionId == typeDefinitionId).FirstOrDefault();

                                if (studentDiscountWitnoutMaxDiscount.Count > 0 && studentDiscountMaxDiscountObj != null)
                                {
                                    foreach (StudentCurrentDiscountLocal item_1 in studentDiscountWitnoutMaxDiscount)
                                    {
                                        StudentDiscountCurrentDetails stdDiscountCurrent = new StudentDiscountCurrentDetails();
                                        if (item_1.TypeDefinitionId != 35)
                                        {

                                            stdDiscountCurrent.StudentDiscountCurrentDetailsId = item_1.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrent.StudentDiscountId = item_1.StudentDiscountId;
                                            stdDiscountCurrent.TypeDefinitionId = item_1.TypeDefinitionId;
                                            stdDiscountCurrent.TypePercentage = 0;
                                            stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                                            stdDiscountCurrent.Comments = "0 by Max discount condition.";
                                            bool isExecute = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);
                                        }
                                        else
                                        {
                                            stdDiscountCurrent.StudentDiscountCurrentDetailsId = item_1.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrent.StudentDiscountId = item_1.StudentDiscountId;
                                            stdDiscountCurrent.TypeDefinitionId = item_1.TypeDefinitionId;
                                            stdDiscountCurrent.TypePercentage = 0;
                                            stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                                            stdDiscountCurrent.Comments = "Added with Max discount.";
                                            bool isExecute = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);

                                            StudentDiscountCurrentDetails stdDiscountCurrentMaxPercentageObj = new StudentDiscountCurrentDetails();
                                            stdDiscountCurrentMaxPercentageObj.StudentDiscountCurrentDetailsId = studentDiscountMaxDiscountObj.StudentDiscountCurrentDetailsId;
                                            stdDiscountCurrentMaxPercentageObj.StudentDiscountId = studentDiscountMaxDiscountObj.StudentDiscountId;
                                            stdDiscountCurrentMaxPercentageObj.TypeDefinitionId = studentDiscountMaxDiscountObj.TypeDefinitionId;
                                            //stdDiscountCurrentMaxPercentageObj.TypePercentage = studentDiscountMaxDiscountObj.TypePercentage + item_1.TypePercentage;
                                            stdDiscountCurrentMaxPercentageObj.AcaCalSession = studentDiscountMaxDiscountObj.AcaCalSession;
                                            //stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + item_1.TypePercentage;

                                            decimal totalPercentage = studentDiscountMaxDiscountObj.TypePercentage + item_1.TypePercentage;
                                            if (totalPercentage > Convert.ToDecimal(100))
                                            {
                                                //decimal admissionPercentage = Convert.ToDecimal(100) - studentDiscountMaxDiscountObj.TypePercentage;
                                                stdDiscountCurrentMaxPercentageObj.TypePercentage = Convert.ToDecimal(100);
                                                stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + item_1.TypePercentage;
                                            }
                                            else
                                            {
                                                stdDiscountCurrentMaxPercentageObj.TypePercentage = studentDiscountMaxDiscountObj.TypePercentage + item_1.TypePercentage;
                                                stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + item_1.TypePercentage;
                                            }

                                            bool result = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrentMaxPercentageObj);
                                        }
                                    }
                                }
                                else
                                {
                                    if ( studentDiscountMaxDiscountObj != null)
                                    {
                                        StudentDiscountCurrentDetails stdDiscountCurrentMaxPercentageObj = new StudentDiscountCurrentDetails();
                                        stdDiscountCurrentMaxPercentageObj.StudentDiscountCurrentDetailsId = studentDiscountMaxDiscountObj.StudentDiscountCurrentDetailsId;
                                        stdDiscountCurrentMaxPercentageObj.StudentDiscountId = studentDiscountMaxDiscountObj.StudentDiscountId;
                                        stdDiscountCurrentMaxPercentageObj.TypeDefinitionId = studentDiscountMaxDiscountObj.TypeDefinitionId;
                                        //stdDiscountCurrentMaxPercentageObj.TypePercentage = studentDiscountMaxDiscountObj.TypePercentage + item_1.TypePercentage;
                                        stdDiscountCurrentMaxPercentageObj.AcaCalSession = studentDiscountMaxDiscountObj.AcaCalSession;
                                        //stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + item_1.TypePercentage;

                                        decimal totalPercentage = studentDiscountMaxDiscountObj.TypePercentage + admFairStudentCurrentDiscount.TypePercentage;
                                        if (totalPercentage > Convert.ToDecimal(100))
                                        {
                                            //decimal admissionPercentage = Convert.ToDecimal(100) - studentDiscountMaxDiscountObj.TypePercentage;
                                            stdDiscountCurrentMaxPercentageObj.TypePercentage = Convert.ToDecimal(100);
                                            stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + admFairStudentCurrentDiscount.TypePercentage;
                                        }
                                        else
                                        {
                                            stdDiscountCurrentMaxPercentageObj.TypePercentage = studentDiscountMaxDiscountObj.TypePercentage + admFairStudentCurrentDiscount.TypePercentage;
                                            stdDiscountCurrentMaxPercentageObj.Comments = studentDiscountMaxDiscountObj.Comments + ", Admission Fair " + admFairStudentCurrentDiscount.TypePercentage;
                                        }


                                        StudentDiscountCurrentDetails stdDiscountCurrent = new StudentDiscountCurrentDetails();
                                        stdDiscountCurrent.StudentDiscountCurrentDetailsId = admFairStudentCurrentDiscount.StudentDiscountCurrentDetailsId;
                                        stdDiscountCurrent.StudentDiscountId = admFairStudentCurrentDiscount.StudentDiscountId;
                                        stdDiscountCurrent.TypeDefinitionId = admFairStudentCurrentDiscount.TypeDefinitionId;
                                        stdDiscountCurrent.TypePercentage = 0;
                                        stdDiscountCurrent.AcaCalSession = acaCalSessionId;
                                        stdDiscountCurrent.Comments = "Added with Max discount.";
                                        bool isExecute = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrent);


                                        bool result = StudentDiscountCurrentDetailsManager.Update(stdDiscountCurrentMaxPercentageObj);
                                    }
                                }
                            }
                        }
                    }
                }
                btnLoad_Click(null, null);
            }
            catch (Exception)
            {
            }
        }
    }
}