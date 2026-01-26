using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

public partial class MajorMinorDepartmentSetup : BasePage
{
    BussinessObject.UIUMSUser userObj = null;

    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

    #region Events
   

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        lblCount.Text = "0";
        if (!IsPostBack && !IsCallback)
        {
            LoadInstitute();
            ucProgram.LoadDropdownWithUserAccessAndInstitute(userObj.Id, 0);
            ucBatch.LoadDropDownList(0);
        }
    }
    private void LoadInstitute()
    {
        try
        {
            var InstituteList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();

            ddlInstitute.Items.Clear();
            ddlInstitute.AppendDataBoundItems = true;
            ddlInstitute.Items.Add(new ListItem("Select", "0"));


            if (InstituteList != null && InstituteList.Any())
            {
                ddlInstitute.DataTextField = "InstituteName";
                ddlInstitute.DataValueField = "InstituteId";
                ddlInstitute.DataSource = InstituteList.OrderBy(x => x.InstituteName);
                ddlInstitute.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }


    #region On Changed Method

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucProgram.LoadDropdownWithUserAccessAndInstitute(userObj.Id, Convert.ToInt32(ddlInstitute.SelectedValue));
    }
    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGridView();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGridView();
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }
    private void ClearGridView()
    {
        try
        {
            lblCount.Text = string.Empty;
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStudent.Text.Trim()) && ucProgram.selectedValue == "0" && ucBatch.selectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + "Pleae insert StudentID or program and batch info" + "');", true);
            return;
        }

        int batchId = 0;
        int programId = Convert.ToInt32(ucProgram.selectedValue);

        if (ucBatch.selectedValue == "0")
        {
            if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                batchId = student.BatchId;
            }
        }
        else
        {
             batchId = Convert.ToInt32(ucBatch.selectedValue);
        }

        string roll = txtStudent.Text.Trim();

        List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, batchId, roll);
        studentList = studentList.Where(s => s.IsActive == true).ToList();

        gvStudentList.DataSource = studentList.OrderBy(s => s.Roll).ToList();
        gvStudentList.DataBind();

        lblCount.Text = studentList.Count().ToString();

        if (studentList.Count > 0)
            FillMajorNodeCombo();
    }

    protected void btnMajor1_Click(object sender, EventArgs e)
    {
        try
        {
            int index = 0;
            // int majorNode = Convert.ToInt32(1);
            DropDownList ddlMajor1 = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlMajor1");
            int major1Value = Convert.ToInt32(ddlMajor1.SelectedItem.Value);


            foreach (GridViewRow row in gvStudentList.Rows)
            {
                OfferedCourse offeredCourse = new OfferedCourse();
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");

                if (ckBox.Checked)
                {
                    int dataKey = (int)gvStudentList.DataKeys[index].Value;

                    Student strudent = StudentManager.GetById(dataKey);
                    strudent.Major1NodeID = major1Value;

                    bool result = StudentManager.Update(strudent);
                }
                index++;
            }

            btnLoad_Click(null, null);

            ShowMessage("Data update successfully.");
        }
        catch (Exception)
        {

        }
    }

    private void ShowMessage(string msg)
    {
        showAlert(msg);
    }

    protected void btnMajor2_Click(object sender, EventArgs e)
    {
        try
        {
            int index = 0;
            DropDownList ddlMajor2 = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlMajor2");
            int major2Value = Convert.ToInt32(ddlMajor2.SelectedItem.Value);


            foreach (GridViewRow row in gvStudentList.Rows)
            {
                OfferedCourse offeredCourse = new OfferedCourse();
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");

                if (ckBox.Checked)
                {
                    int dataKey = (int)gvStudentList.DataKeys[index].Value;

                    Student strudent = StudentManager.GetById(dataKey);
                    strudent.Major2NodeID = major2Value;

                    bool result = StudentManager.Update(strudent);
                }
                index++;
            }

            btnLoad_Click(null, null);

            ShowMessage("Data update successfully.");
        }
        catch (Exception)
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
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region Method


    private void FillMajorNodeCombo()
    {
        DropDownList ddlMajor1 = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlMajor1");
        DropDownList ddlMajor2 = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlMajor2");
        
        ddlMajor1.Items.Clear();
        ddlMajor1.AppendDataBoundItems = true;
        ddlMajor1.Items.Add(new ListItem("# Major-1", "0"));

        ddlMajor2.Items.Clear();
        ddlMajor2.AppendDataBoundItems = true;
        ddlMajor2.Items.Add(new ListItem("# Major-2", "0"));

        int batchId = 0;
        
        if (ucBatch.selectedValue == "0")
        {
            if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                batchId = student.BatchId;
            }
        }
        else
        {
            batchId = Convert.ToInt32(ucBatch.selectedValue);
        }
        List<Node> nodeList = NodeManager.GetAllMajorNodeByBatchId(batchId);

        if (nodeList != null)
        {
            ddlMajor1.DataSource = nodeList;
            ddlMajor1.DataBind();

            ddlMajor2.DataSource = nodeList;
            ddlMajor2.DataBind();
        }
    }

    //private void FillAcademicCalenderCombo()
    //{
    //    try
    //    {
    //        ddlAcaCalBatch.Items.Clear();
    //        ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
    //        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
    //        academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


    //        ddlAcaCalBatch.AppendDataBoundItems = true;

    //        if (academicCalenderList != null)
    //        {
    //            ddlAcaCalBatch.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
    //            ddlAcaCalBatch.DataBind();
    //            //int count = academicCalenderList.Count;
    //            //foreach (AcademicCalender academicCalender in academicCalenderList)
    //            //{
    //            //    ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

    //            //}

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally { }
    //}

    #endregion

    protected void ddlAcaCalBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }
}
