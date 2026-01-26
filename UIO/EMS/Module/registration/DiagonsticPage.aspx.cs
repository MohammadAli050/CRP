using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration
{
    public partial class DiagonsticPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCalenderType();
                //List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            }
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
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Select", "0"));
                ddlSemester.AppendDataBoundItems = true;

                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

                if (academicCalenderList.Count > 0 && academicCalenderList != null)
                {
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlSemester.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                    academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
                    ddlSemester.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                }
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

        protected void btnQueryExecute_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int queryNo = int.Parse(btn.CommandArgument.ToString());
            int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
            if (queryNo == 1)
            {
                DataTable dt = DiagonsticManager.GetData(queryNo, acaCalId);
                gvDiagonsticData.DataSource = dt;
                gvDiagonsticData.DataBind();
            }
            else if (queryNo == 2)
            {
                DataTable dt = DiagonsticManager.GetData(queryNo, acaCalId);
                gvDiagonsticData.DataSource = dt;
                gvDiagonsticData.DataBind();
            }
        }
    }
}