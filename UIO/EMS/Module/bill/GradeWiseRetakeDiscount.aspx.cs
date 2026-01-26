
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GradeWiseRetakeDiscount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            if (!IsPostBack)
            {
                // ClearMessage("", Color.Black);
                LoadDropDown();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    private void LoadDropDown()
    {
        //follow the order of combo loding

        FillSessionCombo();
        FillProgramCombo();
    }

    private void FillSessionCombo()
    {
        try
        {
            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


            ddlAcaCalSession.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalSession.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                }

            }

        }
        catch (Exception ex)
        {
        }
        finally { }
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

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);

            if (programId == 0)
            {
                ShowMessage("Please select Program.", Color.Red);
                return;
            }
            else if (sessionId == 0)
            {
                ShowMessage("Please select Academic Calender.", Color.Red);
                return;
            }

            LoadGradeWiseRetakeDiscount(programId, sessionId);
        }
        catch (Exception)
        {
        }
    }

    private void LoadGradeWiseRetakeDiscount(int programId, int sessionId)
    {
        List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscountList = GradeWiseRetakeDiscountManager.GetAllBy(programId, sessionId);

        gvGradeWiseRetakeDiscount.DataSource = gradeWiseRetakeDiscountList;
        gvGradeWiseRetakeDiscount.DataBind();

        lblCourseCount.Text = gradeWiseRetakeDiscountList.Count().ToString();
    }
    
    private void ClearGrid()
    {
        gvGradeWiseRetakeDiscount.DataSource = null;
        gvGradeWiseRetakeDiscount.DataBind();

        lblCourseCount.Text = "0";
    }

    private void ShowMessage(string msg, Color color)
    {
        pnlMessage.Visible = true;
        lblMessage.Text = msg;
        lblMessage.ForeColor = color;
    }

    private void ClearMessage(string msg, Color color)
    {
        pnlMessage.Visible = false;
        lblMessage.Text = msg;
        lblMessage.ForeColor = color;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);


            List<GradeWiseRetakeDiscount> GradeWiseRetakeDiscountList = new List<GradeWiseRetakeDiscount>();

            foreach (GridViewRow row in gvGradeWiseRetakeDiscount.Rows)
            {
                GradeWiseRetakeDiscount gradeWiseRetakeDiscount = new GradeWiseRetakeDiscount();

                HiddenField hiddenId = (HiddenField)row.FindControl("hdnGradeWiseRetakeDiscountId");
                gradeWiseRetakeDiscount.GradeWiseRetakeDiscountId = Convert.ToInt32(hiddenId.Value);

                HiddenField hdnCourseId = (HiddenField)row.FindControl("hdnGradeId");
                gradeWiseRetakeDiscount.GradeId = Convert.ToInt32(hdnCourseId.Value);

                TextBox retakeDiscount = (TextBox)row.FindControl("txtRetakeDiscount");
                gradeWiseRetakeDiscount.RetakeDiscount = Convert.ToDecimal(retakeDiscount.Text);

                TextBox retakeDiscountOnTrOrWav = (TextBox)row.FindControl("txtRetakeDiscountOnTrOrWav");
                gradeWiseRetakeDiscount.RetakeDiscountOnTrOrWav = Convert.ToDecimal(retakeDiscountOnTrOrWav.Text);

                gradeWiseRetakeDiscount.ProgramId = programId;
                gradeWiseRetakeDiscount.SessionId = sessionId;

                gradeWiseRetakeDiscount.CreatedBy = -1;
                gradeWiseRetakeDiscount.CreatedDate = DateTime.Now;
                gradeWiseRetakeDiscount.ModifiedBy = -1;
                gradeWiseRetakeDiscount.ModifiedDate = DateTime.Now;


                if (Convert.ToInt32(hiddenId.Value) == 0)
                {
                    GradeWiseRetakeDiscountManager.Insert(gradeWiseRetakeDiscount);
                }
                else
                {
                    GradeWiseRetakeDiscountManager.Update(gradeWiseRetakeDiscount);
                }
            }
            LoadGradeWiseRetakeDiscount(programId, sessionId);
        }
        catch (Exception ex)
        {

        }
    }
   
}