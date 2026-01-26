using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using System.Drawing;

public partial class StudentResultUnBlock : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;
        lblCount.Text = "0";

        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucSessionUpto.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        CleareGrid();
    }

    private void CleareGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            int sessionUpto = Convert.ToInt32(ucSessionUpto.selectedValue);
            string roll = txtRoll.Text.Trim();
            decimal amountFrom = string.IsNullOrEmpty(txtAmountFrom.Text) ? 0 : Convert.ToDecimal(txtAmountFrom.Text);
            decimal amountTo = string.IsNullOrEmpty(txtAmountTo.Text) ? 0 : Convert.ToDecimal(txtAmountTo.Text);

            if (programId == 0 && string.IsNullOrEmpty(roll))
            {
                ShowMessage("Please select Program.");
                return;
            }
            //else if (batchId == 0 && string.IsNullOrEmpty(roll))
            //{
            //    ShowMessage("Please select batch.");
            //    return;
            //}
            else
            {
                LoadStudent(programId, batchId, roll, sessionUpto, amountFrom, amountTo);
            }
        }
        catch (Exception)
        {
        }
    }

    private void LoadStudent(int programId, int batchId, string roll, int dueUptoSession, decimal amountFrom, decimal amountTo)
    {
        // List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, acaCalId, roll);
        List<PersonBlockDTO> studentList = PersonBlockManager.GetAllByProgramOrBatchOrRoll(programId, batchId, roll, dueUptoSession);

        if (amountFrom == 0 && amountTo != 0)
            studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();
        if (amountFrom != 0 && amountTo == 0)
            studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();
        if (amountFrom != 0 && amountTo != 0)
            studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();

        if (studentList != null)
        {
            studentList = studentList.OrderBy(s => s.Roll).ToList();

            var c = from b in studentList
                    where (b.IsResultBlock == true)
                    orderby b.Roll
                    select b;
            studentList = c.ToList();

        }
        gvStudentList.DataSource = studentList;
        gvStudentList.DataBind();

        lblCount.Text = studentList.Count().ToString();
        SessionManager.SaveObjToSession<string>(studentList.Count().ToString(), "StudentBloc-count");
    }

    protected void btnBlock_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkBlock");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");

                Student student = StudentManager.GetById(Convert.ToInt32(hiddenId.Value));
                PersonBlock personBlock = new PersonBlock();

                personBlock.PersonId = (int)student.PersonID;
                personBlock.StartDateAndTime = DateTime.Now;
                personBlock.EndDateAndTime = DateTime.Now;
                personBlock.Remarks = txtRemarks.Text;
                personBlock.CreatedBy =userObj.Id;
                personBlock.CreatedDate = DateTime.Now;
                personBlock.ModifiedBy = userObj.Id;
                personBlock.ModifiedDate = DateTime.Now;

                if (ckBox.Checked)
                {
                    int i = PersonBlockManager.Insert(personBlock);
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucBatch.selectedValue,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Person Block",
                                                         userObj.LogInID + " assign person block to " + student.Roll  + " with remarks " + txtRemarks.Text,
                                                         userObj.LogInID + " is blocking ",
                                                          ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                         CommonEnum.PageName.StudentBlock.ToString(),
                                                         _pageUrl,
                                                         student.Roll);
                    #endregion
                }
                else
                {
                    bool b = PersonBlockManager.DeleteByPerson((int)student.PersonID);
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucBatch.selectedValue,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Person Block Remove",
                                                         userObj.LogInID + " remove person block from " + student.Roll,
                                                         userObj.LogInID + " is removing block ",
                                                          ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                         CommonEnum.PageName.StudentBlock.ToString(),
                                                         _pageUrl,
                                                         student.Roll);
                    #endregion
                }

                //student.IsActive = ckBox.Checked;
                //student.Remarks = txtRemarks.Text;

                //bool i = StudentManager.Update(student);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect All";
            }
            else
            {
                chk.Text = "Select All";
            }

            foreach (GridViewRow row in gvStudentList.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("ChkBlock");
                ckBox.Checked = chk.Checked;

            }
        }
        catch (Exception ex)
        {


        }
    }

    #endregion

    #region Methods
    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();

        lblCount.Text = "0";
    }

    private void LoadDropDown()
    {
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
    #endregion

    protected void btnSaveAllHeader_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                                         
            foreach (GridViewRow gvrow in gvStudentList.Rows)
            {
                CheckBox ChkIsAdmitCardBlock = (CheckBox)gvrow.FindControl("ChkIsAdmitCardBlock");
                CheckBox ChkIsRegistrationBlock = (CheckBox)gvrow.FindControl("ChkIsRegistrationBlock");

                CheckBox ChkIsResultBlock = (CheckBox)gvrow.FindControl("ChkIsResultBlock");
                CheckBox ChkIsMasterBlock = (CheckBox)gvrow.FindControl("ChkIsMasterBlock");

                TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");

                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

                Student student = StudentManager.GetById(Convert.ToInt32(hdnId.Value));
                PersonBlock personBlock = PersonBlockManager.GetByPersonId((int)student.PersonID);


                if (personBlock != null)
                {
                    personBlock.StartDateAndTime = DateTime.Now;
                    personBlock.EndDateAndTime = DateTime.Now;
                    personBlock.Remarks = txtRemarks.Text;

                    personBlock.IsAdmitCardBlock = ChkIsAdmitCardBlock.Checked;
                    personBlock.IsRegistrationBlock = ChkIsRegistrationBlock.Checked;

                    personBlock.IsMasterBlock = ChkIsMasterBlock.Checked;
                    personBlock.IsResultBlock = ChkIsResultBlock.Checked;

                    personBlock.CreatedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    personBlock.CreatedDate = DateTime.Now;
                    personBlock.ModifiedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    personBlock.ModifiedDate = DateTime.Now;

                    bool b = PersonBlockManager.Update(personBlock);

                    if (b)
                    {
                        #region Log Insert

                        LogGeneralManager.Insert(
                                                             DateTime.Now,
                                                             "",
                                                             ucBatch.selectedValue,
                                                             userObj.LogInID,
                                                             "",
                                                             "",
                                                             "Person Block Update",
                                                             userObj.LogInID + " assign update person block " + student.Roll + " where  admit card block is " + ChkIsAdmitCardBlock.Checked.ToString()+" ,reg block is "+ChkIsRegistrationBlock.Checked.ToString()+" ,master block is "+ChkIsMasterBlock.Checked.ToString()+" and result block is " + ChkIsResultBlock.Checked.ToString(),
                                                             userObj.LogInID + " is blocking ",
                                                              ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                             CommonEnum.PageName.StudentBlock.ToString(),
                                                             _pageUrl,
                                                             student.Roll);
                        #endregion
                    }
                }
                else
                {
                    personBlock = new PersonBlock();

                    personBlock.PersonId = (int)student.PersonID;
                    personBlock.StartDateAndTime = DateTime.Now;
                    personBlock.EndDateAndTime = DateTime.Now;
                    personBlock.Remarks = txtRemarks.Text;

                    personBlock.IsAdmitCardBlock = ChkIsAdmitCardBlock.Checked;
                    personBlock.IsRegistrationBlock = ChkIsRegistrationBlock.Checked;

                    personBlock.IsMasterBlock = ChkIsMasterBlock.Checked;
                    personBlock.IsResultBlock = ChkIsResultBlock.Checked;

                    personBlock.CreatedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    personBlock.CreatedDate = DateTime.Now;
                    personBlock.ModifiedBy = ((BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    personBlock.ModifiedDate = DateTime.Now;

                    int i = PersonBlockManager.Insert(personBlock);

                    if (i > 0)
                    {
                        #region Log Insert

                        LogGeneralManager.Insert(
                                                             DateTime.Now,
                                                             "",
                                                             ucBatch.selectedValue,
                                                             userObj.LogInID,
                                                             "",
                                                             "",
                                                             "Person Block Insert",
                                                             userObj.LogInID + "  insert person block for " + student.Roll + " where  admit card block is " + ChkIsAdmitCardBlock.Checked.ToString() + " ,reg block is " + ChkIsRegistrationBlock.Checked.ToString() + " ,master block is " + ChkIsMasterBlock.Checked.ToString() + " and result block is " + ChkIsResultBlock.Checked.ToString(),
                                                             userObj.LogInID + " is blocking ",
                                                              ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                             CommonEnum.PageName.StudentBlock.ToString(),
                                                             _pageUrl,
                                                             student.Roll);
                        #endregion
                    }
                }
            }
            btnLoad_Click(sender, e);
        }
        catch (Exception ex)
        {

        }
    }

    protected void lBtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;

            CheckBox ChkIsAdmitCardBlock = (CheckBox)gvrow.FindControl("ChkIsAdmitCardBlock");
            CheckBox ChkIsRegistrationBlock = (CheckBox)gvrow.FindControl("ChkIsRegistrationBlock");

            CheckBox ChkIsResultBlock = (CheckBox)gvrow.FindControl("ChkIsResultBlock");
            CheckBox ChkIsMasterBlock = (CheckBox)gvrow.FindControl("ChkIsMasterBlock");

            TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");

            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            Student student = StudentManager.GetById(Convert.ToInt32(hdnId.Value));
            PersonBlock personBlock = PersonBlockManager.GetByPersonId((int)student.PersonID);
            

            if (personBlock != null)
            {
                personBlock.StartDateAndTime = DateTime.Now;
                personBlock.EndDateAndTime = DateTime.Now;
                personBlock.Remarks = txtRemarks.Text;

                personBlock.IsAdmitCardBlock = ChkIsAdmitCardBlock.Checked;
                personBlock.IsRegistrationBlock = ChkIsRegistrationBlock.Checked;

                personBlock.IsMasterBlock = ChkIsMasterBlock.Checked;
                personBlock.IsResultBlock = ChkIsResultBlock.Checked;

                personBlock.CreatedBy = 1; //BaseCurrentUserObj.Id;
                personBlock.CreatedDate = DateTime.Now;
                personBlock.ModifiedBy = 1;// BaseCurrentUserObj.Id;
                personBlock.ModifiedDate = DateTime.Now;

                bool b = PersonBlockManager.Update(personBlock);

                if (b)
                {
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucBatch.selectedValue,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Person Block Update",
                                                         userObj.LogInID + " assign update person block " + student.Roll + " where  admit card block is " + ChkIsAdmitCardBlock.Checked.ToString() + " ,reg block is " + ChkIsRegistrationBlock.Checked.ToString() + " ,master block is " + ChkIsMasterBlock.Checked.ToString() + " and result block is " + ChkIsResultBlock.Checked.ToString(),
                                                         userObj.LogInID + " is blocking ",
                                                          ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                         CommonEnum.PageName.StudentBlock.ToString(),
                                                         _pageUrl,
                                                         student.Roll);
                    #endregion
                }
            }
            else
            {
                personBlock = new PersonBlock();

                personBlock.PersonId = (int)student.PersonID;
                personBlock.StartDateAndTime = DateTime.Now;
                personBlock.EndDateAndTime = DateTime.Now;
                personBlock.Remarks = txtRemarks.Text;

                personBlock.IsAdmitCardBlock = ChkIsAdmitCardBlock.Checked;
                personBlock.IsRegistrationBlock = ChkIsRegistrationBlock.Checked;

                personBlock.IsMasterBlock = ChkIsMasterBlock.Checked;
                personBlock.IsResultBlock = ChkIsResultBlock.Checked;

                personBlock.CreatedBy = 1; //BaseCurrentUserObj.Id;
                personBlock.CreatedDate = DateTime.Now;
                personBlock.ModifiedBy = 1;// BaseCurrentUserObj.Id;
                personBlock.ModifiedDate = DateTime.Now;

                int i = PersonBlockManager.Insert(personBlock);

                if (i > 0)
                {
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucBatch.selectedValue,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Person Block Insert",
                                                         userObj.LogInID + "  insert person block for " + student.Roll + " where  admit card block is " + ChkIsAdmitCardBlock.Checked.ToString() + " ,reg block is " + ChkIsRegistrationBlock.Checked.ToString() + " ,master block is " + ChkIsMasterBlock.Checked.ToString() + " and result block is " + ChkIsResultBlock.Checked.ToString(),
                                                         userObj.LogInID + " is blocking ",
                                                          ((int)CommonEnum.PageName.StudentBlock).ToString(),
                                                         CommonEnum.PageName.StudentBlock.ToString(),
                                                         _pageUrl,
                                                         student.Roll);
                    #endregion
                }
            }
            btnLoad_Click(sender, e);
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelectAllAdmitCardBlock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsAdmitCardBlock");
                ckBox.Checked = chk.Checked;
            }

            lblCount.Text = SessionManager.GetObjFromSession<string>("StudentBloc-count");
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelectAllRegistrationBlock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsRegistrationBlock");
                ckBox.Checked = chk.Checked;
            }

            lblCount.Text = SessionManager.GetObjFromSession<string>("StudentBloc-count");
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelectAllResultBlock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsResultBlock");
                ckBox.Checked = chk.Checked;
            }

            lblCount.Text = SessionManager.GetObjFromSession<string>("StudentBloc-count");
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelectAllMasterBlock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsMasterBlock");
                ckBox.Checked = chk.Checked;
            }

            lblCount.Text = SessionManager.GetObjFromSession<string>("StudentBloc-count");
        }
        catch (Exception ex)
        {
        }
    }
}