using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleSetList : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        LoadCalenderType();
        //LoadAcademicCalender();
    }

    protected void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();
            //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
            //ddlCalenderType.AppendDataBoundItems = true;

            List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                ddlCalenderType.DataTextField = "Name";
                ddlCalenderType.DataSource = calenderUnitMasterList;
                ddlCalenderType.DataBind();
            }
        }
        catch { }
        finally
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
    }

    protected void LoadAcademicCalender(int calenderTypeId)
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlAcademicCalender.AppendDataBoundItems = true;

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

            if (academicCalenderList.Count > 0 && academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
            }
        }
        catch { }
    }

    #endregion


    #region Event

    protected void btnCreateLink_Click(Object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamScheduleSetCreate.aspx");
        }
        catch { }
    }

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        catch { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);

            List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(acaCalId);
            if (examScheduleSetList.Count > 0 && examScheduleSetList != null)
            {
                gvExamScheduleSetList.DataSource = examScheduleSetList;
                gvExamScheduleSetList.DataBind();
            }
            else
            {
                gvExamScheduleSetList.DataSource = null;
                gvExamScheduleSetList.DataBind();
            }
        }
        catch
        {
            gvExamScheduleSetList.DataSource = null;
            gvExamScheduleSetList.DataBind();
        }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            Response.Redirect("ExamScheduleSetUpdate.aspx?ExamScheduleSet=" + id);
        }
        catch { }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            bool resultDelete = ExamScheduleSetManager.Delete(id);
            if (resultDelete)
            {
                lblMsg.Text = "Delete Successful";
                btnLoad_Click(null, null);
            }
        }
        catch { }
    }

    #endregion
}