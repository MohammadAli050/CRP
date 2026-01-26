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

public partial class ProbationStudentBlock : BasePage
{
    List<PersonBlock> studentProbBlockList = new List<PersonBlock>();
    List<PersonBlock> studentProbUnBlockList = new List<PersonBlock>();

    private const string SESSION_STUDENTPROB_BLOCKLIST = "SESSION_STUDENTPROB_BLOCKLIST";
    private const string SESSION_STUDENTPROB_UNBLOCKLIST = "SESSION_STUDENTPROB_UNBLOCKLIST";

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
          base.CheckPage_Load();

        pnlMessage.Visible = false;
        //lblCount.Text = "0";

        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            LoadGrid();

        }
        catch (Exception)
        {
        }
    }

    private void LoadGrid()
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
        if (programId == 0)
        {
            lblMessage.Text = "Please select Program first.";
            ddlProgram.Focus();
            return;
        }

        int? minProb = null;
        int? maxProb = null;
        int acaCalBatchId = Convert.ToInt32(ucBatch.selectedValue);
        if (!string.IsNullOrEmpty(txtMinProbation.Text))
            minProb = Convert.ToInt32(txtMinProbation.Text.Trim());
        if (!string.IsNullOrEmpty(txtMaxProbation.Text))
            maxProb = Convert.ToInt32(txtMaxProbation.Text.Trim());

        LoadStudent(programId, acaCalBatchId, minProb, maxProb);
    }

    protected void btnBlock_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvStudentList.Rows)
        {
            CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");

            if (ckBox.Checked)
            {
                HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblProbationCount = (Label)row.FindControl("lblProbationCount");
                CheckBox chkIsBlock = (CheckBox)row.FindControl("ChkIsBlock");

                txtRemarks.Text = "Blocked for Probation NO: " + lblProbationCount.Text;

                Student student = StudentManager.GetById(Convert.ToInt32(hiddenId.Value));
                PersonBlock personBlock = PersonBlockManager.GetByPersonId(student.PersonID) == null ? new PersonBlock() : PersonBlockManager.GetByPersonId(student.PersonID);

                personBlock.PersonId = personBlock.PersonId == 0 ? (int)student.PersonID : personBlock.PersonId;
                personBlock.StartDateAndTime = DateTime.Now;
                personBlock.EndDateAndTime = DateTime.Now;
                personBlock.Remarks +=" "+txtRemarks.Text;
                personBlock.CreatedBy = 1;
                personBlock.CreatedDate = DateTime.Now;
                personBlock.ModifiedBy = 1;
                personBlock.ModifiedDate = DateTime.Now;
                personBlock.IsProbationBlock = true;

                studentProbBlockList.Add(personBlock);
                chkIsBlock.Checked = true;
            }          
        }
        SessionManager.SaveListToSession<PersonBlock>(studentProbBlockList, SESSION_STUDENTPROB_BLOCKLIST);
        SessionManager.DeletFromSession(SESSION_STUDENTPROB_UNBLOCKLIST);
    }

    protected void btnUnBlock_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvStudentList.Rows)
        {
            CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");

            if (ckBox.Checked)
            {
                HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                CheckBox chkIsBlock = (CheckBox)row.FindControl("ChkIsBlock");
               // txtRemarks.Text = string.Empty;

                Student student = StudentManager.GetById(Convert.ToInt32(hiddenId.Value));
                PersonBlock personBlock = PersonBlockManager.GetByPersonId(student.PersonID) == null ? new PersonBlock() : PersonBlockManager.GetByPersonId(student.PersonID);

                personBlock.PersonId = personBlock.PersonId == 0 ? (int)student.PersonID : personBlock.PersonId;
                personBlock.StartDateAndTime = DateTime.Now;
                personBlock.EndDateAndTime = DateTime.Now;
                personBlock.Remarks = txtRemarks.Text;
                personBlock.CreatedBy = 1;
                personBlock.CreatedDate = DateTime.Now;
                personBlock.ModifiedBy = 1;
                personBlock.ModifiedDate = DateTime.Now;
                personBlock.IsProbationBlock = false;
                chkIsBlock.Checked = false;
                studentProbUnBlockList.Add(personBlock);
            }           
        }
        SessionManager.SaveListToSession<PersonBlock>(studentProbUnBlockList, SESSION_STUDENTPROB_UNBLOCKLIST);
        SessionManager.DeletFromSession(SESSION_STUDENTPROB_BLOCKLIST);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        studentProbBlockList = SessionManager.GetListFromSession<PersonBlock>( SESSION_STUDENTPROB_BLOCKLIST);
        studentProbUnBlockList = SessionManager.GetListFromSession<PersonBlock>(SESSION_STUDENTPROB_UNBLOCKLIST);

        if (studentProbBlockList != null)
        {
            foreach (PersonBlock item in studentProbBlockList)
            {
                if(PersonBlockManager.GetByPersonId(item.PersonId)==null)
                 PersonBlockManager.Insert(item);
                else
                    PersonBlockManager.Update(item);
            }            
        }
        else if (studentProbUnBlockList != null)
        {
            foreach (PersonBlock item in studentProbUnBlockList)
            {
                if (PersonBlockManager.GetByPersonId(item.PersonId) == null)
                    PersonBlockManager.Insert(item);
                else
                    PersonBlockManager.Update(item);
            }           
        }

        LoadGrid();
    }   

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect";
            }
            else
            {
                chk.Text = "Select";
            }

            foreach (GridViewRow row in gvStudentList.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
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

       // lblCount.Text = "0";
    }

    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillProgramCombo();
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);

    }

    private void LoadStudent(int programId, int acaCalBatchId, int? minProb, int? maxProb)
    {
        List<StudentProbationDTO> studentProbList = StudentManager.GetAllByProbationStatus(programId, acaCalBatchId, minProb, maxProb);

        gvStudentList.DataSource = studentProbList;
        gvStudentList.DataBind();

       // lblCount.Text = studentProbList.Count().ToString();
    }

   

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
    #endregion

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ddlProgram.SelectedValue));
    }

   
}