using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_Exam : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadExamTypeGird();
            UpdateExamButton.Visible = false;
            CancelButton.Visible = false;
            SaveExamButton.Visible = true;
        }
    }

    public void LoadExamTypeGird()
    {
        GridViewExam.DataSource = ExamManager.GetAll();
        GridViewExam.DataBind();
    }

    protected void SaveExamButton_Click(object sender, EventArgs e)
    {
        try
        {
            Exam exam = new Exam();
            exam.ExamName = txtExamName.Text;
            exam.Marks = Convert.ToInt16(txtMarks.Text);
            exam.CreatedBy = 1;
            exam.CreationTime = DateTime.Now;
            exam.ModifiedBy = 1;
            exam.ModificationTime = DateTime.Now;


            if (ExamManager.GetExamByName(exam.ExamName))
            {
                int result = ExamManager.Insert(exam);
                if (result > 0)
                {
                    lblMsg.Text = "Exam entry successful.";
                    LoadExamTypeGird();
                }
                else
                {
                    lblMsg.Text = "Exam could not inserted.";
                }
            }
            else
            {
                lblMsg.Text = "Eaxm already exist.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void UpdateExamButton_Click(object sender, EventArgs e)
    {
        try
        {
            int examId = Convert.ToInt32(hfExamId.Value);
            Exam exam = new Exam();
            exam = ExamManager.GetById(examId);
            exam.ExamName = txtExamName.Text;
            exam.Marks = Convert.ToInt16(txtMarks.Text);
            exam.ModifiedBy = 1;
            exam.ModificationTime = DateTime.Now;
            if (ExamManager.GetExamByName(exam.ExamName))
            {
                if (ExamManager.Update(exam))
                {
                    lblMsg.Text = "Exam edited successfully.";
                    LoadExamTypeGird();
                }
                else
                {
                    lblMsg.Text = "Eaxm could not edited successfully.";
                }
            }
            else
            {
                lblMsg.Text = "Eaxm already exist.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        txtExamName.Text = string.Empty;
        txtMarks.Text = string.Empty;
        UpdateExamButton.Visible = false;
        CancelButton.Visible = false;
        SaveExamButton.Visible = true;
    }

    private void LoadAllTextBox(Exam exam)
    {
        txtExamName.Text = exam.ExamName;
        txtMarks.Text = Convert.ToString(exam.Marks);
    }

    protected void GridViewExam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int examId = Convert.ToInt16(e.CommandArgument);
        Exam exam = new Exam();
        exam = ExamManager.GetById(examId);
        hfExamId.Value = Convert.ToString(examId);

        if (e.CommandName == "ExamEdit")
        {
            LoadAllTextBox(exam);
            UpdateExamButton.Visible = true;
            CancelButton.Visible = true;
            SaveExamButton.Visible = false;
        }
        if (e.CommandName == "ExamDelete")
        {
            ExamManager.Delete(examId);
            LoadExamTypeGird();
        }
    }

    protected void GridViewExam_DataBound(object sender, EventArgs e)
    {

    }
}