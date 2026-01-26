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

public partial class ExamRoutine_ExamScheduleTimeSlotList : BasePage
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

    protected void LoadExamScheduleTimeSlotList()
    {
        try
        {
            int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);

            List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examScheduleSetId);
            if (examScheduleTimeSlotList.Count > 0 && examScheduleTimeSlotList != null)
            {
                List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(Convert.ToInt32(ddlAcademicCalender.SelectedValue));
                Hashtable hashExamScheduleSet = new Hashtable();
                foreach (ExamScheduleSet examScheduleSet in examScheduleSetList)
                    hashExamScheduleSet.Add(examScheduleSet.Id, examScheduleSet.SetName);

                foreach (ExamScheduleTimeSlot examScheduleTimeSlot in examScheduleTimeSlotList)
                    examScheduleTimeSlot.ExamScheduleSetName = hashExamScheduleSet[examScheduleTimeSlot.ExamScheduleSetId] == null ? examScheduleTimeSlot.ExamScheduleSetId.ToString() : hashExamScheduleSet[examScheduleTimeSlot.ExamScheduleSetId].ToString();

                gvExamScheduleTimeSlot.DataSource = examScheduleTimeSlotList;
                gvExamScheduleTimeSlot.DataBind();
            }
            else
            {
                gvExamScheduleTimeSlot.DataSource = null;
                gvExamScheduleTimeSlot.DataBind();
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
            Response.Redirect("ExamScheduleTimeSlotCreate.aspx");
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
            LoadExamScheduleTimeSlotList();
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

                Response.Redirect("ExamScheduleTimeSlotUpdate.aspx?ExamScheduleTimeSlot=" + id);
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

                bool resultDelete = ExamScheduleTimeSlotManager.Delete(id);
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