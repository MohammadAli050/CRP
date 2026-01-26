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

public partial class ExamScheduleDayList : BasePage
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
        ddlExamScheduleSet.Items.Clear();
        ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));

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

                AcademicCalender_Changed(null, null);
            }
        }
        catch { }
    }

    protected void LoadExamScheduleSet(int acaCalId)
    {
        try
        {
            ddlExamScheduleSet.Items.Clear();
            ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
            ddlExamScheduleSet.AppendDataBoundItems = true;

            List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(acaCalId);

            ddlExamScheduleSet.DataSource = examScheduleSetList;
            ddlExamScheduleSet.DataValueField = "Id";
            ddlExamScheduleSet.DataTextField = "SetName";
            ddlExamScheduleSet.DataBind();
        }
        catch { }
    }

    protected void LoadExamScheduleDayList()
    {
        try
        {
            int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);

            List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examScheduleSetId);
            if (examScheduleDayList.Count > 0 && examScheduleDayList != null)
            {
                List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(Convert.ToInt32(ddlAcademicCalender.SelectedValue));
                Hashtable hashExamScheduleSet = new Hashtable();
                foreach (ExamScheduleSet examScheduleSet in examScheduleSetList)
                    hashExamScheduleSet.Add(examScheduleSet.Id, examScheduleSet.SetName);

                foreach (ExamScheduleDay examScheduleDay in examScheduleDayList)
                    examScheduleDay.ExamScheduleSetName = hashExamScheduleSet[examScheduleDay.ExamScheduleSetId] == null ? examScheduleDay.ExamScheduleSetId.ToString() : hashExamScheduleSet[examScheduleDay.ExamScheduleSetId].ToString();

                gvExamScheduleDayList.DataSource = examScheduleDayList;
                gvExamScheduleDayList.DataBind();
            }
            else
            {
                gvExamScheduleDayList.DataSource = null;
                gvExamScheduleDayList.DataBind();
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
            Response.Redirect("ExamScheduleDayCreate.aspx");
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

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadExamScheduleSet(acaCalId);
        }
        catch { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            LoadExamScheduleDayList();
        }
        catch { }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);

                Response.Redirect("ExamScheduleDayUpdate.aspx?ExamScheduleDay=" + id);
            }
            catch { }
        }
        catch { }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);

                bool resultDelete = ExamScheduleDayManager.Delete(id);
                if (resultDelete)
                {
                    lblMsg.Text = "Delete Successful";
                    btnLoad_Click(null, null);
                }
            }
            catch { }
        }
        catch { }
    }

    #endregion
}