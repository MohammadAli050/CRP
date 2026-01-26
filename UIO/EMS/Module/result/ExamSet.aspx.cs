using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;

public partial class Test_ExamSet : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadExamSetGirdView();
            UpdateExamSetButton.Visible = false;
            CancelButton.Visible = false;
            SaveExamSetButton.Visible = true;
            LoadCriteriaDropDownList();
        }
    }

    public void LoadExamSetGirdView()
    {
        gvExamSet.DataSource = ExamSetManager.GetAll();
        gvExamSet.DataBind();
        GridRebind();
    }

    public void GridRebind() 
    {
        for (int i = 0; i < gvExamSet.Rows.Count; i++)
        {
            GridViewRow row = gvExamSet.Rows[i];
            int criteriaId = Convert.ToInt16(row.Cells[3].Text);
            CriteriaType criteria = CriteriaTypeManager.GetById(criteriaId);
            row.Cells[3].Text = criteria.CriteriaName;
        }
    }

    public void LoadCriteriaDropDownList()
    {
        ddlCraiteria.DataSource = CriteriaTypeManager.GetAll();
        ddlCraiteria.DataTextField = "CriteriaName";
        ddlCraiteria.DataValueField = "Id";
        ddlCraiteria.DataBind();

        ddlCraiteria.Enabled = false;
        ddlCraiteria.Items.FindByText("Single").Selected = true;
    }

    private void LoadAllTextBox(ExamSet examSet)
    {
        txtExamSetName.Text = examSet.ExamSetName;
        txtMarks.Text = Convert.ToString(examSet.Mark);
        ddlCraiteria.SelectedValue = Convert.ToString(examSet.CriteriaId);
    }

    protected void SaveExamSetButton_Click(object sender, EventArgs e)
    {
        try
        {
            ExamSet examSet = new ExamSet();
            examSet.ExamSetName = txtExamSetName.Text;
            examSet.Mark = Convert.ToInt16(txtMarks.Text);
            examSet.CriteriaId = Convert.ToInt16(ddlCraiteria.SelectedValue);
            examSet.CreatedBy = 1;
            examSet.CreatedTime = DateTime.Now;
            examSet.ModifiedBy = 1;
            examSet.ModifiedTime = DateTime.Now;

            if (ExamSetManager.GetExamSetByName(examSet.ExamSetName))
            {
                int result = ExamSetManager.Insert(examSet);

                if (result > 0)
                {
                    lblMsg.Text = "Exam set entry successful";
                    LoadExamSetGirdView();
                }
                else
                {
                    lblMsg.Text = "Exam set entry is unsuccessful";
                }
            }
            else 
            {
                lblMsg.Text = "Exam set already exist";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void UpdateExamSetButton_Click(object sender, EventArgs e)
    {
        try
        {
            int examSetId = Convert.ToInt16(HiddenExamSetId.Value);
            ExamSet examSet = new ExamSet();
            examSet = ExamSetManager.GetById(examSetId);
            examSet.ExamSetName = txtExamSetName.Text;
            examSet.Mark = Convert.ToInt16(txtMarks.Text);
            examSet.CriteriaId = Convert.ToInt16(ddlCraiteria.SelectedValue);
            examSet.CreatedBy = 1;
            examSet.CreatedTime = DateTime.Now;
            examSet.ModifiedBy = 1;
            examSet.ModifiedTime = DateTime.Now;
            if (ExamSetManager.Update(examSet))
            {
                lblMsg.Text = "Exam set edited successfully";
                LoadExamSetGirdView();
            }
            else
            {
                lblMsg.Text = "Exam set could not edited successfully";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtExamSetName.Text = string.Empty;
        txtMarks.Text = string.Empty;
        UpdateExamSetButton.Visible = false;
        CancelButton.Visible = false;
        SaveExamSetButton.Visible = true;
    }

    protected void gvExamSet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int examSetId = Convert.ToInt16(e.CommandArgument);
        ExamSet examSet = new ExamSet();
        examSet = ExamSetManager.GetById(examSetId);
        HiddenExamSetId.Value = Convert.ToString(examSetId);

        if (e.CommandName == "ExamSetEdit")
        {
            LoadAllTextBox(examSet);
            UpdateExamSetButton.Visible = true;
            CancelButton.Visible = true;
            SaveExamSetButton.Visible = false;
        }
        if (e.CommandName == "ExamSetDelete")
        {
            ExamSetManager.Delete(examSetId);
            LoadExamSetGirdView();
        }
    }
}