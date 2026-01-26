using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using BussinessObject;


public partial class StudentManagement_CGPA : BasePage
{
    #region Variables
    private List<AcademicCalender> _trimesterInfos = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Session Consts
    private const string SESSION_PROGRAM = "Program";
    private const string SESSION_COURSE = "Course";
    private const string SESSIONTABLE = "DATA_TABLE";
    private const string SESSIONCURRENTTRIMESTER = "CurrentTrimester";
    private const string SESSION_BATCH = "Batch";
    private const string SESSION_STUDENTS = "STUDENTS";
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (CurrentUser != null)
            {
                if (CurrentUser.RoleID > 0)
                {
                    AuthenticateHome(CurrentUser);
                }
            }
            else
            {
                Response.Redirect("~/Security/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindControls();
                //FillSchCombo();
                //DisableButtons();
                //txtSrch.Focus();
            }
            //btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception exception)
        {
            //Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(DDLProgram.SelectedValue);
        int registrationcalId = Convert.ToInt32(ddlAcaCalenderRegistration.SelectedValue);
        int batchCalID = Convert.ToInt32(ddlAcaCalenderBatch.SelectedValue);
        //int test = 0;
        List<SelectedStudentForGPACalculationEntity> students =
            new List<SelectedStudentForGPACalculationEntity>();
        students = SelectedStudentForGPACalculation_BAO.GetSelectedRegisteredStudents(batchCalID, registrationcalId,
                                                                                      programId);
        if (students == null)
        {
            string message = "No Row selected";
            lblErrorMessage.Visible = true;
            Utilities.ShowMassage(lblErrorMessage, Color.Red, message);
        }
        else
        {
            foreach (SelectedStudentForGPACalculationEntity student in students)
            {
                Session.Add(SESSION_STUDENTS, student);
            }
            BindListGrid(students);
        }

    }

    protected void btnShowGPA_Click(object sender, EventArgs e)
    {
        int numOfSelectedRows = 0;
        List<string> rolls = new List<string>();
        foreach (GridViewRow row in GridViewStudentList.Rows)
        {
            CheckBox check = row.FindControl("chkSelected") as CheckBox;
            if (check.Checked)
            {
                numOfSelectedRows++;
                Label lblRoll = new Label();
                lblRoll = row.FindControl("lblStudentRoll") as Label;
                string roll = lblRoll.Text;
                rolls.Add(roll);
            }
        }
        if (numOfSelectedRows <= 0)
        {
            lblErrorMessage.Visible = true;
            string message = "You must Select a Student atleast.";
            Utilities.ShowMassage(lblErrorMessage,Color.Red,message);
            return;
        }
        else
        {
            int registrationcalId = Convert.ToInt32(ddlAcaCalenderRegistration.SelectedValue);
            SelectedStudentForGPACalculation_BAO.CalculateGPA(rolls, registrationcalId);
        }
    }

    protected void Grid_Student_GPA_Bind(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridStudentListBind(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion


    #region Methods
    private void BindControls()
    {
        try
        {
            FillAcademicCalenderDropDownList();
            FillProgramDropDownList();
        }
        catch (Exception exception)
        {
            Utilities.ShowMassage(lblErrorMessage, Color.Red, exception.Message);
        }
    }

    private void FillProgramDropDownList()
    {
        try
        {
            List<RbProgramEntity> programEntities = FormSaleSubmit_BAO.GetAllPrograms();
            //.......................
            if (programEntities != null && programEntities.Count > 0)
            {
                if (Session[SESSION_PROGRAM] != null)
                {
                    Session.Remove(SESSION_PROGRAM);
                }
                Session[SESSION_PROGRAM] = programEntities;
            }
            //.......................
            if (programEntities == null)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "No Program Found";
                return;
            }

            DDLProgram.DataSource = programEntities;
            DDLProgram.DataTextField = "Shortname";
            DDLProgram.DataValueField = "Id";
            //DDLProgram.DataValueField = "Code";
            DDLProgram.DataBind();
        }
        catch (Exception exception)
        {
            Utilities.ShowMassage(lblErrorMessage, Color.Red, exception.Message);
        }
    }

    private void FillAcademicCalenderDropDownList()
    {
        try
        {
            string currentTrimester = "";
            _trimesterInfos = AcademicCalender.Gets();

            if (_trimesterInfos == null)
            {
                return;
            }

            foreach (AcademicCalender ac in _trimesterInfos)
            {
                if (ac.IsCurrent)
                {
                    currentTrimester = ac.Id.ToString();
                }

                ListItem lei = new ListItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                ddlAcaCalenderBatch.Items.Add(lei);
                ddlAcaCalenderRegistration.Items.Add(lei);
            }

            if (Session[SESSIONCURRENTTRIMESTER] != null)
            {
                Session.Remove(SESSIONCURRENTTRIMESTER);
            }
            Session[SESSIONCURRENTTRIMESTER] = currentTrimester;

            ddlAcaCalenderBatch.SelectedValue = currentTrimester;
            ddlAcaCalenderRegistration.SelectedValue = currentTrimester;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblErrorMessage, ex.Message);
        }
        finally { }
    }

    private void BindListGrid(List<SelectedStudentForGPACalculationEntity> students)
    {
        if (students.Count <= 0)
        {
            string message = "No Row selected";
            Utilities.ShowMassage(lblErrorMessage, Color.Red, message);
        }
        else
        {
            GridViewStudentList.DataSource = students;
            GridViewStudentList.DataBind();
        }
    }
    #endregion





}
