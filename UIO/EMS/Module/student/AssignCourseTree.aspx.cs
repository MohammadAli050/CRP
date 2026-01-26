using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCAMDAL;

public partial class AssignCourseTree : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.AssignCourseTree);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.AssignCourseTree));

    BussinessObject.UIUMSUser userObj = null;
    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (!IsPostBack && !IsCallback)
        {
            LoadInstitute();
            DivDistribution.Visible = false;
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
    private void FillCourseTreeCombo(int programID)
    {
        ddlCourseTree.Items.Clear();
        List<TreeMaster> treeMasterList = TreeMasterManager.GetAllProgramID(programID);
        //ddlCourseTree.Items.Add(new ListItem("Select", "0"));
        ddlCourseTree.AppendDataBoundItems = true;

        if (treeMasterList.Count > 0)
        {
            ddlCourseTree.DataSource = treeMasterList.OrderBy(d => d.TreeMasterID).ToList();
            ddlCourseTree.DataBind();

            //if (treeMasterList[0].TreeMasterID > 0)
            FillLinkedCalendars(treeMasterList[0].TreeMasterID);
        }
    }
    private void FillLinkedCalendars(int treeMasterID)
    {
        ddlLinkedCalendars.Items.Clear();
        List<TreeCalendarMaster> treeCalendarMasterList = TreeCalendarMasterManager.GetAllByTreeMasterID(treeMasterID);
        //ddlLinkedCalendars.Items.Add(new ListItem("Select", "0"));
        ddlLinkedCalendars.AppendDataBoundItems = true;

        if (treeCalendarMasterList != null)
        {
            ddlLinkedCalendars.DataSource = treeCalendarMasterList.OrderBy(d => d.TreeCalendarMasterID).ToList();
            ddlLinkedCalendars.DataBind();
            //gvwCollection.Enabled = true;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblCount.Text = string.Empty;

        int programID = Convert.ToInt32(ucProgram.selectedValue);
        int batchID = Convert.ToInt32(ucBatch.selectedValue);

        string roll = string.Empty;

        if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
            roll = txtStudent.Text.Trim();

        if(programID==0)
        {
            showAlert("Please select a program first");
            return;
        }

        FillCourseTreeCombo(programID);

        List<LogicLayer.BusinessObjects.Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programID, batchID, roll);
        if (studentList != null && studentList.Count > 0)
        {
            gvwCollection.DataSource = studentList.OrderBy(x => x.Roll);
            gvwCollection.DataBind();

            DivDistribution.Visible = true;

            CountMethods(studentList);
        }
        else
        {
            ClearGridView();

        }
    }

    private void CountMethods(List<LogicLayer.BusinessObjects.Student> studentList)
    {
        try
        {
            int Total = studentList.Count;
            int Assign = studentList.Where(x => x.TreeCalendarMasterID != 0).Count();
            lblCount.Text = "Total Student : " + Total + ", Curriculum Tree Assigned : " + Assign + ", Pending : " + (Total - Assign);

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

        ddlCourseTree.Items.Clear();
        ddlLinkedCalendars.Items.Clear();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGridView();

        ddlCourseTree.Items.Clear();
        ddlLinkedCalendars.Items.Clear();
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void CourseTree_SelectedIndexChanged(object serder, EventArgs e)
    {
        FillLinkedCalendars(Convert.ToInt32(ddlCourseTree.SelectedValue));
    }

    #endregion


    private void ClearGridView()
    {
        try
        {
            lblCount.Text = string.Empty;
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            DivDistribution.Visible = false;
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsgConfirm.Text = string.Empty;
            ConfirmModalPopUp.Show();
            lblMsgConfirm.Text = "Are you sure you want to assign this distribution ?";
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnConfirmApply_Click(object sender, EventArgs e)
    {
        try
        {
            int treeMasterID = Int32.Parse(ddlCourseTree.SelectedValue);
            int treeCalendarMasterID = Int32.Parse(ddlLinkedCalendars.SelectedValue);
            TreeMaster tm = TreeMasterManager.GetById(treeMasterID);
            TreeCalendarMaster tcm = TreeCalendarMasterManager.GetById(treeCalendarMasterID);

            List<LogicLayer.BusinessObjects.Student> studentsToUpdate = new List<LogicLayer.BusinessObjects.Student>();
            List<string> updatedRolls = new List<string>();

            foreach (GridViewRow row in gvwCollection.Rows)
            {
                CheckBox chk = row.FindControl("chk") as CheckBox;
                Label lblStudentId = row.FindControl("lblStudentID") as Label;

                if (chk != null && chk.Checked && lblStudentId != null)
                {
                    int studentId = Convert.ToInt32(lblStudentId.Text);
                    LogicLayer.BusinessObjects.Student std = StudentManager.GetById(studentId);

                    if (std != null)
                    {
                        std.TreeMasterID = treeMasterID;
                        std.TreeCalendarMasterID = treeCalendarMasterID;
                        std.ModifiedBy = 0;
                        std.ModifiedDate = DateTime.Now;

                        studentsToUpdate.Add(std);
                        updatedRolls.Add(std.Roll);
                    }
                }
            }

            // Perform batch update if supported
            foreach (LogicLayer.BusinessObjects.Student std in studentsToUpdate)
            {
                StudentManager.Update(std);
            }

            // Log once for all updates
            if (tm != null && tcm != null && updatedRolls.Count > 0)
            {
                string rollList = string.Join(", ", updatedRolls);
                InsertLog("Assign Student Syllabus", $"{userObj.LogInID} assigned syllabus '{tm.Node_Name}_{tcm.Name}' to students: {rollList}", null);
            }
            if (studentsToUpdate != null && studentsToUpdate.Any())
            {
                showAlert("Syllabus Updated Successfully");
                btnView_Click(null, null);
            }
            else
            {
                showAlert("No students were selected for update.");
            }
        }
        catch (Exception ex)
        {
            // Consider logging the exception for diagnostics
            InsertLog("Error", $"Exception during syllabus assignment: {ex.Message}", null);
        }

    }



    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            CheckBox chk = (CheckBox)sender;
            chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }

    private void InsertLog(string EventName, string Message, string Roll)
    {
        LogGeneralManager.Insert(
                                  DateTime.Now,
                                  "",
                                  "",
                                 "",
                                  "",//CourseCode
                                  "",//VersionCode
                                  EventName,
                                  Message,
                                  "Normal",
                                  _pageId.ToString(),
                                  _pageName.ToString(),
                                  _pageUrl,
                                  Roll);
    }

    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);

    }


}