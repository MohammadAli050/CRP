using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;

public partial class Test_ExamSetItem : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadExamTypeGirdView();
            UpdateExamSetItem.Visible = false;
            CancelExamSetItem.Visible = false;
            SaveExamSetItemButton.Visible = true;
            LoadExamSetDropDown();
            //int examSetId = Convert.ToInt16(ddlExamSet.SelectedValue);
            LoadExamDropDown();
        }
    }
    
    public void LoadExamTypeGirdView()
    {
        gvExamType.DataSource = ExamSetItemManager.GetAllExamSetItem();
        gvExamType.DataBind();
        //GridRebind();
    }

    public void GridRebind()
    {
        for (int i = 0; i < gvExamType.Rows.Count; i++)
        {
            GridViewRow row = gvExamType.Rows[i];

            int microTestSetId = Convert.ToInt16(row.Cells[1].Text);
            ExamSet examSet = ExamSetManager.GetById(microTestSetId);
            row.Cells[1].Text = examSet.ExamSetName;

            int microTestId = Convert.ToInt16(row.Cells[2].Text);
            Exam exam = ExamManager.GetById(microTestId);
            row.Cells[2].Text = exam.ExamName;

        }
    }

    public void LoadExamSetDropDown()
    {
        ddlExamSet.DataSource = ExamSetManager.GetAll();
        ddlExamSet.DataTextField = "ExamSetName";
        ddlExamSet.DataValueField = "SetId";
        ddlExamSet.DataBind();
    }

    public void LoadExamDropDown() 
    {
        ddlExam.DataSource = ExamManager.GetAll();
        ddlExam.DataTextField = "ExamName";
        ddlExam.DataValueField = "ExamId";
        ddlExam.DataBind();
    }
    protected void SaveExamSetItemButton_Click(object sender, EventArgs e)
    {
        try
        {
            ExamSetItem examSetGroup = new ExamSetItem();
            examSetGroup.ExamId = Convert.ToInt16(ddlExam.SelectedValue);
            examSetGroup.ExamSetId = Convert.ToInt16(ddlExamSet.SelectedValue);
            examSetGroup.CreatedBy = 1;
            examSetGroup.CreatedTime = DateTime.Now;
            examSetGroup.ModifiedBy = 1;
            examSetGroup.ModifiedTime = DateTime.Now;
            if (ExamSetItemManager.GetByExamExamSetId(examSetGroup.ExamSetId, examSetGroup.ExamId))
            {
                int result = ExamSetItemManager.Insert(examSetGroup);

                if (result > 0)
                {
                    lblMsg.Text = "Exam Set Group Entry Successful.";
                    LoadExamTypeGirdView();
                }
                else
                {
                    lblMsg.Text = "Exam Set Group Could Not Insert.";
                }
            }
            else 
            {
                lblMsg.Text = "Exam Set alreay exist.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void UpdateExamSetItem_Click(object sender, EventArgs e)
    {
        try
        {
            int examSetGroupId = Convert.ToInt16(MicroTestId.Value);
            ExamSetItem examSetGroup = new ExamSetItem();
            examSetGroup = ExamSetItemManager.GetById(examSetGroupId);
            examSetGroup.ExamId = Convert.ToInt16(ddlExam.SelectedValue);
            examSetGroup.ExamSetId = Convert.ToInt16(ddlExamSet.SelectedValue);
            examSetGroup.CreatedBy = 1;
            examSetGroup.CreatedTime = DateTime.Now;
            examSetGroup.ModifiedBy = 1;
            examSetGroup.ModifiedTime = DateTime.Now;
            if (ExamSetItemManager.Update(examSetGroup))
            {
                lblMsg.Text = "Exam set group edited successfully";
                LoadExamTypeGirdView();
            }
            else
            {
                lblMsg.Text = "Exam could not edited successfully";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void CancelExamSetItem_Click(object sender, EventArgs e)
    {
        //txtExamType.Text = string.Empty;
        //txtMarks.Text = string.Empty;
        UpdateExamSetItem.Visible = false;
        CancelExamSetItem.Visible = false;
        SaveExamSetItemButton.Visible = true;
    }

    private void LoadAllTextBox(ExamSetItem examSetGroup)
    {
        ddlExam.SelectedValue = Convert.ToString(examSetGroup.ExamId);
        ddlExamSet.SelectedValue = Convert.ToString(examSetGroup.ExamSetId);
    }

    protected void gvExamType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int examSetGroupId = Convert.ToInt16(e.CommandArgument);
        //int microTestSetGroupId = Convert.ToInt16();
        ExamSetItem examSetGroup = new ExamSetItem();
        examSetGroup = ExamSetItemManager.GetById(examSetGroupId);
        MicroTestId.Value = Convert.ToString(examSetGroupId);

        if (e.CommandName == "ExamSetGroupEdit")
        {
            LoadAllTextBox(examSetGroup);
            UpdateExamSetItem.Visible = false;
            CancelExamSetItem.Visible = false;
            SaveExamSetItemButton.Visible = true;
        }
        if (e.CommandName == "ExamSetGroupDelete")
        {
            ExamSetItemManager.Delete(examSetGroupId);
            LoadExamTypeGirdView();
        }
    }
}