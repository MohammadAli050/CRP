using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessObjects;

public partial class StudentRegistration_TransferStudent : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {
            FillAcademicCalenderCombo();
        }
    }

    private string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            //ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
                ddlAcaCalBatch.SelectedValue = count.ToString();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    public void TranserStudent(object sender, EventArgs e)
    {
        int academicCalenderID = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        AcademicCalender academicCalender = AcademicCalenderManager.GetById(academicCalenderID);
        int count = TransferStudentManager.Transfer(academicCalender.Code);
    }
}