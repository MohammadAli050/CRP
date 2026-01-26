using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

public partial class StudentRegistration_AutoPreregistrationCount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {
            FillProgramCombo();
        }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            //ddlProgram.Items.Add(new ListItem("Select", "0"));
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

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programID = Convert.ToInt32(ddlProgram.SelectedValue);
        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().Where(x => x.IsCurrent == true).ToList();
        int academicCalenderId = academicCalenderList[0].AcademicCalenderID;
        List<PreregistrationCountDTO> registrationList = PreregistrationCountDTOManager.GetAllByProgAcaCal(programID, academicCalenderId);
        gvCourseCount.DataSource = registrationList;
        gvCourseCount.DataBind();
    }

    protected void btnCourseOffer_Click(object sender, EventArgs e)
    {
        int programID = Convert.ToInt32(ddlProgram.SelectedValue);
        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().Where(x => x.IsCurrent == true).ToList();
        int academicCalenderId = academicCalenderList[0].AcademicCalenderID;

        List<OfferedCourse> offeredCourseList = new List<OfferedCourse>();
        for (int i = 0; i < gvCourseCount.Rows.Count; i++)
        {
            CheckBox chkbox = (CheckBox)gvCourseCount.Rows[i].Cells[0].FindControl("chkOfferedCourse");
            if (chkbox.Checked)
            {
                OfferedCourse offeredCourse = new OfferedCourse();
                offeredCourse.AcademicCalenderID = academicCalenderId;
                offeredCourse.ProgramID = programID;
                offeredCourse.CourseID = Int32.Parse(gvCourseCount.Rows[i].Cells[1].Text.Trim());
                offeredCourse.VersionID = Int32.Parse(gvCourseCount.Rows[i].Cells[2].Text.Trim());
                offeredCourse.CreatedBy = 666;
                offeredCourse.CreatedDate = DateTime.Now;

                int result = OfferedCourseManager.Insert(offeredCourse);

                offeredCourseList.Add(offeredCourse);
            }
        }
    }
}