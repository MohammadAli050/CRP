using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Result : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblCount.Text = "0";
        //ShowMessage("");
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (Request.QueryString["uid"] != null && Request.QueryString["pwd"] != null && Request.QueryString["mmnu"] != null)
        {
            string uid = Request.QueryString["uid"].ToString();
            string pwd = Request.QueryString["pwd"].ToString();
            string mmnu = Request.QueryString["mmnu"].ToString();

            if (base.IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
            {
                base.RemoveFromSession(Constants.SESSIONMSTRMENUID);
            }
            base.AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(mmnu));

            string dPdw = Utilities.Decrypt(pwd);

            if (BussinessObject.UIUMSUser.Login(uid, dPdw))
                base.AddToSession(Constants.SESSIONCURRENT_USER, userObj);

            //FormsAuthentication.RedirectFromLoginPage(logMain.UserName, false);
        }

        if (!IsPostBack)
        {
            LoadDropDown();




            HttpRuntime.Cache.Remove("StudentCache");
            HttpRuntime.Cache.Remove("StudentCourseHistoryCache");

            StudentManager.GetAll();
            StudentCourseHistoryManager.GetAll();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);

        if (programId == 0)
        {
            ShowMessage("Please select Program.");
            return;
        }
        else if (acaCalId == 0)
        {
            ShowMessage("Please select Trimester.");
            return;
        }

        List<Student> studentList = StudentManager.GetAllByProgramIdBatchId(programId, acaCalId);

        if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
        {
            studentList = studentList.Where(s => s.Roll == txtStudent.Text.Trim()).ToList();
        }

        gvStudentList.DataSource = studentList.OrderBy(s => s.Roll).ToList();
        gvStudentList.DataBind();

        lblCount.Text = studentList.Count().ToString();
    }

    private void ShowMessage(string msg)
    {
        lblMessage.Text = msg;
    }

    protected void btnUpdateGPA_Click(object sender, EventArgs e)
    {
        try
        {
            int index = 0;
            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");

                if (ckBox.Checked)
                {
                    int studentId = (int)gvStudentList.DataKeys[index].Value;
                    int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                    int programId = Convert.ToInt32(ddlProgram.SelectedValue);

                    //Student strudent = StudentManager.GetById(dataKey);
                    //bool result = StudentManager.Update(strudent);

                    int result = StudentACUDetailManager.UpdateByAcaCalRoll(studentId, acaCalId);

                }
                index++;
            }
        }
        catch { }
        finally { }
    }
    #endregion

    #region Method
    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcademicCalenderCombo();
        FillProgramCombo();
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

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
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {


        }
    }

    protected void lBtnResultHistory_Click(object sender, EventArgs e)
    {
        string redirectURL = string.Format("{0}/Admin/StudentResultHistory.aspx", AppPath.ApplicationPath);
        //Response.Redirect(redirectURL, "_blank", "menubar=0,scrollbars=1,width=1100,height=500,top=10");
    }
    #endregion
}