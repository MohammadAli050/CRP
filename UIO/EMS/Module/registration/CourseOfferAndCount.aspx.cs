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

public partial class Admin_CourseOfferAndCount : BasePage
{
    #region Events
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;
        pnlMessage.Visible = false;
        lblCourseCount.Text = "0";

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucTree.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnTreeSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        bool result = false;
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int treeRoot = Convert.ToInt32(ucTree.selectedValue);

            if (programId == 0)
            {
                ShowMessage("Please select Program.");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please select Academic Calender.");
                return;
            }
            else if (treeRoot == 0)
            {
                ShowMessage("Please select Calender Tree.");
                return;
            }

            List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);

            offeredCourseList = offeredCourseList.Where(oc => oc.TreeRootID == treeRoot).ToList();


            if (offeredCourseList.Count() == 0)
            {
                result = GenerateCourseList(programId, acaCalId, treeRoot);
                if (result)
                    ShowMessage("Data generate successfully.");
                else
                    ShowMessage("Data generate not complete. Please generate again or contact with system administrator.");
            }
            else
            {
                pnlMessage.Visible = true;
                lBtnContinueAnyWay.Enabled = true;
                lBtnContinueAnyWay.Visible = true;

                lblMessage.Text = "Course list is already generated. Do you want to continue ?";
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int treeRoot = Convert.ToInt32(ucTree.selectedValue);

            if (programId == 0)
            {
                ShowMessage("Please select Program.");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please select Academic Calender.");
                return;
            }
            else if (treeRoot == 0)
            {
                ShowMessage("Please select Course Tree.");
                return;
            }

            LoadCourseList(programId, acaCalId, treeRoot);
        }
        catch (Exception)
        {
        }
    }

    protected void btnActiveAndOffer_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int treeRoot = Convert.ToInt32(ucTree.selectedValue);

            List<OfferedCourse> offeredCourseList = new List<OfferedCourse>();

            foreach (GridViewRow row in gvOfferedCourse.Rows)
            {
                OfferedCourse offeredCourse = new OfferedCourse();
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");

                if (ckBox.Checked)
                {
                    HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");
                    offeredCourse.OfferID = Convert.ToInt32(hiddenId.Value);

                    HiddenField hdnCourseId = (HiddenField)row.FindControl("hdnCourseId");
                    offeredCourse.CourseID = Convert.ToInt32(hdnCourseId.Value);

                    HiddenField hdnVersionID = (HiddenField)row.FindControl("hdnVersionID");
                    offeredCourse.VersionID = Convert.ToInt32(hdnVersionID.Value);

                    HiddenField hdnNode_CourseID = (HiddenField)row.FindControl("hdnNode_CourseID");
                    offeredCourse.Node_CourseID = Convert.ToInt32(hdnNode_CourseID.Value);

                    TextBox limit = (TextBox)row.FindControl("lblLimit");
                    offeredCourse.Limit = Convert.ToInt32(limit.Text);

                    //TextBox occupied = (TextBox)row.FindControl("lblOccupied");
                    //offeredCourse.Occupied = Convert.ToInt32(occupied.Text);

                    offeredCourse.ProgramID = programId;
                    offeredCourse.AcademicCalenderID = acaCalId;
                    offeredCourse.TreeRootID = treeRoot;

                    offeredCourse.IsActive = true;

                    offeredCourse.CreatedBy = -1;
                    offeredCourse.CreatedDate = DateTime.Now;
                    offeredCourse.ModifiedBy = -1;
                    offeredCourse.ModifiedDate = DateTime.Now;
                }
                else
                {
                    HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");
                    offeredCourse.OfferID = Convert.ToInt32(hiddenId.Value);

                    HiddenField hdnCourseId = (HiddenField)row.FindControl("hdnCourseID");
                    offeredCourse.CourseID = Convert.ToInt32(hdnCourseId.Value);

                    HiddenField hdnVersionID = (HiddenField)row.FindControl("hdnVersionID");
                    offeredCourse.VersionID = Convert.ToInt32(hdnVersionID.Value);

                    HiddenField hdnNode_CourseID = (HiddenField)row.FindControl("hdnNode_CourseID");
                    offeredCourse.Node_CourseID = Convert.ToInt32(hdnNode_CourseID.Value);

                    TextBox limit = (TextBox)row.FindControl("lblLimit");
                    offeredCourse.Limit = Convert.ToInt32(limit.Text);

                    offeredCourse.ProgramID = programId;
                    offeredCourse.AcademicCalenderID = acaCalId;
                    offeredCourse.TreeRootID = treeRoot;

                    offeredCourse.IsActive = false;

                    offeredCourse.CreatedBy = -1;
                    offeredCourse.CreatedDate = DateTime.Now;
                    offeredCourse.ModifiedBy = -1;
                    offeredCourse.ModifiedDate = DateTime.Now;
                }

                offeredCourseList.Add(offeredCourse);
            }

            bool i = OfferedCourseManager.ActiveInactiveList(offeredCourseList);

            LoadCourseList(programId, acaCalId, treeRoot);
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

            foreach (GridViewRow row in gvOfferedCourse.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                ckBox.Checked = chk.Checked;

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void lBtnContinueAnyWay_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int treeRoot = Convert.ToInt32(ucTree.selectedValue);

        bool result = GenerateCourseList(programId, acaCalId, treeRoot);
    }

    protected void lBtnCancel_Click(object sender, EventArgs e)
    {
        pnlMessage.Visible = false;
        lBtnContinueAnyWay.Enabled = false;
        lBtnContinueAnyWay.Visible = false;
        lblMessage.Text = string.Empty;
    }

    #endregion

    #region Methods
    private void ClearGrid()
    {
        gvOfferedCourse.DataSource = null;
        gvOfferedCourse.DataBind();

        lblCourseCount.Text = "0";
    }

    private bool GenerateCourseList(int programId, int acaCalId, int treeRoot)
    {
        bool result = false;

        try
        {
            lBtnCancel_Click(null, null); // hide msg panel;

            TreeMaster node = TreeMasterManager.GetById(treeRoot);
            List<CourseListByNodeDTO> treeCourseList = CourseListByNodeDTOManager.GetAllByRootNodeId(node.RootNodeID);
            List<OfferedCourseDTO> offeredCourseListPrevious = OfferedCourseManager.GetAllDtoObjByProgramAcacalTreeroot(programId, acaCalId, treeRoot);

            bool d = OfferedCourseManager.DeleteByProgramAndAcaCalAndtreeRoot(programId, acaCalId, treeRoot);

            List<OfferedCourse> offeredCourseList = new List<OfferedCourse>();

            foreach (CourseListByNodeDTO item in treeCourseList)
            {
                OfferedCourse offeredCourse = new OfferedCourse();

                OfferedCourseDTO offeredCoursePrevious = offeredCourseListPrevious.Where(
                                                   o => o.AcademicCalenderID == acaCalId &&
                                                   o.CourseID == item.CourseId &&
                                                   o.VersionID == item.VersionId &&
                                                   o.ProgramID == programId &&
                                                   o.Node_CourseID == item.NodeCourseId).ToList().SingleOrDefault();
                if (offeredCoursePrevious != null)
                {
                    offeredCourse.Limit = offeredCoursePrevious.Limit;
                    offeredCourse.Occupied = offeredCoursePrevious.Occupied;
                    offeredCourse.IsActive = offeredCoursePrevious.IsActive;
                }
                else
                {
                    offeredCourse.Limit = 1000;
                    offeredCourse.Occupied = 0;
                    offeredCourse.IsActive = false;
                }

                offeredCourse.ProgramID = programId;
                offeredCourse.AcademicCalenderID = acaCalId;
                offeredCourse.CourseID = item.CourseId;
                offeredCourse.VersionID = item.VersionId;
                offeredCourse.Node_CourseID = item.NodeCourseId;
                offeredCourse.TreeRootID = treeRoot;
                // offeredCourse.Limit = 1000;
                // offeredCourse.Occupied = 0;
                // offeredCourse.IsActive = false;
                offeredCourse.CreatedBy = -1;
                offeredCourse.CreatedDate = DateTime.Now;
                offeredCourse.ModifiedBy = -1;
                offeredCourse.ModifiedDate = DateTime.Now;

                offeredCourseList.Add(offeredCourse);
            }

            int i = OfferedCourseManager.InsertList(offeredCourseList);

            if (i > 0)
            {
                result = true;

                LoadCourseList(programId, acaCalId, treeRoot);
            }
        }
        catch (Exception)
        {
            return false;
        }
        return result;
    }

    private void LoadCourseList(int programId, int acaCalId, int treeRoot)
    {
        List<OfferedCourseDTO> offeredCourseList = OfferedCourseManager.GetAllDtoObjByProgramAcacalTreeroot(programId, acaCalId, treeRoot);
        if (offeredCourseList != null)
        {
            offeredCourseList = offeredCourseList.OrderBy(oc => oc.FormalCode).ToList();

            if (chkIsOffer.Checked)
            {
                offeredCourseList = offeredCourseList.Where(o => o.IsActive == true).OrderBy(oc => oc.FormalCode).ToList();
            }
        }

        gvOfferedCourse.DataSource = offeredCourseList;
        gvOfferedCourse.DataBind();

        lblCourseCount.Text = offeredCourseList.Count().ToString();
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;
        lBtnContinueAnyWay.Enabled = false;
        lBtnContinueAnyWay.Visible = false;
        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
    #endregion
}