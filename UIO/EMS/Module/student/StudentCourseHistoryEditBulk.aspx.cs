using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentCourseHistoryEditBulk : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    int userId = 0;

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;

        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            int rangeId = Convert.ToInt32(ddlRange.SelectedValue);

            List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByProgramSessionBatch(programId, acaCalId, batchId,rangeId);

            if (studentCourseHistoryList != null && studentCourseHistoryList.Count > 0)
            {
                studentCourseHistoryList = studentCourseHistoryList.OrderByDescending(x => x.Roll).ToList();
                lblResult.Visible = false;

                gvCourseHistry.DataSource = studentCourseHistoryList;
                gvCourseHistry.DataBind();
            }
            else
            {
                lblMsg.Text = "No Course History Found";
            }


        }
        catch { ClearGrid(); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //int semesterNo = Convert.ToInt32(ddlSemesterNoAll.SelectedValue);
            int YearNo = Convert.ToInt32(ddlYearAll.SelectedValue);
            
            GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

            DropDownList ddlYearNo = (DropDownList)gvrow.FindControl("ddlYearNo");
            DropDownList ddlSemesterNo = (DropDownList)gvrow.FindControl("ddlSemesterNo");
            CheckBox ChkIsConsiderGPA = (CheckBox)gvrow.FindControl("ChkIsConsiderGPA");
            //CheckBox chkSemesterNo = (CheckBox)gvrow.FindControl("chkSemesterNo");
            CheckBox chkYearNo = (CheckBox)gvrow.FindControl("chkYearNo");
            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            StudentCourseHistory sch = StudentCourseHistoryManager.GetById(Convert.ToInt32(hdnId.Value));

            if (sch != null)
            {
                sch.SemesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                sch.YearNo = Convert.ToInt32(ddlYearNo.SelectedValue);
                sch.IsConsiderGPA = ChkIsConsiderGPA.Checked == true ? true : false;

                sch.ModifiedBy = BaseCurrentUserObj.Id;
                sch.ModifiedDate = DateTime.Now;

                if (chkYearNo.Checked)
                {
                    //sch.SemesterNo = semesterNo;
                    sch.YearNo = YearNo;
                    
                }

                bool isUpdate = StudentCourseHistoryManager.Update(sch);
                if (isUpdate)
                {
                    #region Log Insert
                    LogGeneralManager.Insert(
                        DateTime.Now,
                        BaseAcaCalCurrent.Code,
                        BaseAcaCalCurrent.FullCode,
                        BaseCurrentUserObj.LogInID,
                        "",
                        "",
                        "Student Course History Edit",
                        BaseCurrentUserObj.LogInID + " Is Edited YearNo : " + sch.YearNo.ToString() +  " IsConsiderGPA : " + ChkIsConsiderGPA.Checked,
                        "normal",
                        ((int)CommonEnum.PageName.StudentCourseHistoryEdit).ToString(),
                        CommonEnum.PageName.StudentCourseHistoryEdit.ToString(),
                        _pageUrl,
                        sch.Roll);
                    #endregion
                    lblMsg.Text = "Successfylly Updated";
                }
            }

            //btnLoad_Click(null, null);

        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            //int semesterNo = Convert.ToInt32(ddlSemesterNoAll.SelectedValue);
            int YearNo = Convert.ToInt32(ddlYearAll.SelectedValue);
            int count = 0;
            foreach (GridViewRow gvrow in gvCourseHistry.Rows)
            {
                DropDownList ddlYearNo = (DropDownList)gvrow.FindControl("ddlYearNo");
                DropDownList ddlSemesterNo = (DropDownList)gvrow.FindControl("ddlSemesterNo");
                CheckBox ChkIsConsiderGPA = (CheckBox)gvrow.FindControl("ChkIsConsiderGPA");
                //CheckBox chkSemesterNo = (CheckBox)gvrow.FindControl("chkSemesterNo");
                CheckBox chkYearNo = (CheckBox)gvrow.FindControl("chkYearNo");
                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

                StudentCourseHistory sch = StudentCourseHistoryManager.GetById(Convert.ToInt32(hdnId.Value));

                if (sch != null)
                { 
                    sch.SemesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                    sch.YearNo = Convert.ToInt32(ddlYearNo.SelectedValue);
                    sch.IsConsiderGPA = ChkIsConsiderGPA.Checked == true ? true : false;

                    sch.ModifiedBy = BaseCurrentUserObj.Id;
                    sch.ModifiedDate = DateTime.Now;

                    if (chkYearNo.Checked)
                    {
                        //sch.SemesterNo = semesterNo;
                        sch.YearNo = YearNo;
                    }

                    bool isUpdate = StudentCourseHistoryManager.Update(sch);
                    if (isUpdate)
                    {
                        #region Log Insert
                        LogGeneralManager.Insert(
                            DateTime.Now,
                            BaseAcaCalCurrent.Code,
                            BaseAcaCalCurrent.FullCode,
                            BaseCurrentUserObj.LogInID,
                            "",
                            "",
                            "Student Course History Edit",
                            BaseCurrentUserObj.LogInID + " Is Edited YearNo : " + sch.YearNo.ToString() + " IsConsiderGPA : " + ChkIsConsiderGPA.Checked,
                            "normal",
                            ((int)CommonEnum.PageName.StudentCourseHistoryEdit).ToString(),
                            CommonEnum.PageName.StudentCourseHistoryEdit.ToString(),
                            _pageUrl,
                            sch.Roll);
                        #endregion
                        count++;
                    }
                }

            }

            btnLoad_Click(null, null);

            lblMsg.Text = "Successfylly Updated : " + count + " Rows ";
        }
        catch (Exception)
        {
        }
    }

    protected void gvRegisteredCourse_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<StudentCourseHistory> studentCourseHistoryList = (List<StudentCourseHistory>)Session["CourseHistoryList"];

            if (studentCourseHistoryList != null && studentCourseHistoryList.Count > 0)
            {
                string sortdirection = string.Empty;
                if (Session["direction"] != null)
                {
                    if (Session["direction"].ToString() == "ASC")
                    {
                        sortdirection = "DESC";
                    }
                    else
                    {
                        sortdirection = "ASC";
                    }
                }
                else
                {
                    sortdirection = "DESC";
                }
                Session["direction"] = sortdirection;
                Sort(studentCourseHistoryList, e.SortExpression.ToString(), sortdirection);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void chkSelectAllIsConsiderGPA_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvCourseHistry.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsConsiderGPA");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelectAllchkSemesterNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvCourseHistry.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("chkSemesterNo");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }


    #endregion

    #region Method


    private void Sort(List<StudentCourseHistory> list, String sortBy, String sortDirection)
    {
        if (sortDirection == "ASC")
        {
            list.Sort(new GenericComparer<StudentCourseHistory>(sortBy, (int)SortDirection.Ascending));
        }
        else
        {
            list.Sort(new GenericComparer<StudentCourseHistory>(sortBy, (int)SortDirection.Descending));
        }
        gvCourseHistry.DataSource = list;
        gvCourseHistry.DataBind();
    }

    private void ClearGrid()
    {
        gvCourseHistry.DataSource = null;
        gvCourseHistry.DataBind();
    }

    #endregion

    protected void chkSelectAllchkYearNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvCourseHistry.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("chkYearNo");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }



}