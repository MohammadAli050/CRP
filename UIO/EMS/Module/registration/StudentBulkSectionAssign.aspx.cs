using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StudentBulkSectionAssign : BasePage
{
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         base.CheckPage_Load();
         string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
         User user = UserManager.GetByLogInId(loginID);
         if (user != null)
             userId = user.User_ID;
         if (!IsPostBack)
         {
             ucProgram.LoadDropdownWithUserAccess(userId);
         }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (ucProgram.selectedValue == "0")
            {
                lblMessage.Text = "Please select Program.";
                return;
            }
            else if (ucSession.selectedValue == "0")
            {
                lblMessage.Text = "Please select Session.";
                return;
            }
            else if (ucBatch.selectedValue == "0")
            {
                lblMessage.Text = "Please select batch.";
                return;
            }
            else if (ddlCourse.SelectedValue == "0_0")
            {
                lblMessage.Text = "Please select Course.";
                return;
            }

            LoadGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadGrid()
    {
        string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

        List<RegistrationWorksheet> registrationWorksheetList = RegistrationWorksheetManager.GetAllStudentByProgramSessionBatch(Convert.ToInt32(ucProgram.selectedValue),
                                                                                                                                Convert.ToInt32(ucSession.selectedValue),
                                                                                                                                Convert.ToInt32(ucBatch.selectedValue),
                                                                                                                                Convert.ToInt32(courseAndVersion[0]),
                                                                                                                                Convert.ToInt32(courseAndVersion[1]));

        //if (ddlGender.SelectedValue == "Male")
        //{
        //    registrationWorksheetList = registrationWorksheetList.Where(r => r.Gender.ToLower() == "male").ToList();
        //}
        //else if (ddlGender.SelectedValue == "Female")
        //{
        //    registrationWorksheetList = registrationWorksheetList.Where(r => r.Gender.ToLower() == "female").ToList();
        //}

        if (ddlRoll.SelectedValue == "Odd")
        {
            registrationWorksheetList = registrationWorksheetList.Where(r => (Convert.ToInt32(r.Roll.Substring(r.Roll.Length - 1, 1)) % 2) == 1).ToList();
        }
        else if (ddlRoll.SelectedValue == "Even")
        {
            registrationWorksheetList = registrationWorksheetList.Where(r => (Convert.ToInt32(r.Roll.Substring(r.Roll.Length - 1, 1)) % 2) == 0).ToList();
        }


        if (registrationWorksheetList != null)
        {
            gvStudentList.DataSource = registrationWorksheetList.OrderBy(s => s.Roll).ToList();
            gvStudentList.DataBind();
        }
        else
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        lblCount.Text = registrationWorksheetList.Count().ToString();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCourseCombo();
            ClearGrid();
            ClearSectionDropdown();
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearSectionDropdown()
    {
        ddlSection.Items.Clear();
    }

    private void LoadCourseCombo()
    {
        List<Course> courseList = CourseManager.GetOfferedCourseByProgramSession(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucSession.selectedValue));
        if (courseList != null)
            courseList = courseList.OrderBy(x => x.FormalCode).ToList();
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("-Select-", "0_0"));
        ddlCourse.AppendDataBoundItems = true;
        if (courseList != null)
        {
            foreach (Course course in courseList)
            {
                ddlCourse.Items.Add(new ListItem(course.CourseFullInfo.ToString(), course.CourseID + "_" + course.VersionID));
            }
        }
    }

    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void btnSectionUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            if (ddlSection.SelectedValue == "0")
            {
                lblMessage.Text = "Please select Section.";
                return;
            }
            else if (ddlSection.SelectedValue == "-1")
            {
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    if (ckBox.Checked)
                    {
                        RegistrationWorksheet registrationWorksheet = new RegistrationWorksheet();
                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");

                        registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hdnId.Value));
                        int sectionId = registrationWorksheet.AcaCal_SectionID;

                        registrationWorksheet.SectionName = string.Empty;
                        registrationWorksheet.AcaCal_SectionID = 0;


                        bool isTrue = RegistrationWorksheetManager.Update(registrationWorksheet);

                        if (isTrue)
                        {
                            AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(sectionId);
                            if (academicCalenderSection.Occupied > 0)
                            {
                                academicCalenderSection.Occupied = (academicCalenderSection.Occupied - 1);
                                academicCalenderSection.ModifiedDate = DateTime.Now;
                                academicCalenderSection.ModifiedBy = -2;
                                AcademicCalenderSectionManager.Update(academicCalenderSection);
                            }
                        }
                        // StudentCourseHistoryManager.UpdateSectionBy(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(Id.Value));
                    }
                }
            }
            else
            {
                AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(Convert.ToInt32(ddlSection.SelectedValue));

                string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    if (ckBox.Checked)
                    {
                        RegistrationWorksheet registrationWorksheet = new RegistrationWorksheet();
                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");

                        registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hdnId.Value));

                        registrationWorksheet.SectionName = ddlSection.SelectedItem.Text;
                        registrationWorksheet.AcaCal_SectionID = Convert.ToInt32(ddlSection.SelectedValue);

                        bool isTrue = RegistrationWorksheetManager.Update(registrationWorksheet);

                        if (isTrue)
                        {
                            academicCalenderSection.Occupied = (academicCalenderSection.Occupied + 1);
                            academicCalenderSection.ModifiedDate = DateTime.Now;
                            academicCalenderSection.ModifiedBy = -2;
                            AcademicCalenderSectionManager.Update(academicCalenderSection);
                        }
                    }
                }
            }
            

            LoadGrid();
        }
        catch (Exception)
        {
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
            LoadSectionCombo();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadSectionCombo()
    {
        // DropDownList ddlSection = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlSection");

        string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

        List<AcademicCalenderSection> list = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(Convert.ToInt32(ucSession.selectedValue),
                                                                                                     Convert.ToInt32(courseAndVersion[0]),
                                                                                                     Convert.ToInt32(courseAndVersion[1]));

        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("-Select-", "0"));
        ddlSection.AppendDataBoundItems = true;
        if (list != null)
        {
            foreach (AcademicCalenderSection item in list)
            {
                ddlSection.Items.Add(new ListItem(item.SectionName.ToString(), item.AcaCal_SectionID.ToString()));
            }
        }
        ddlSection.Items.Add(new ListItem("-Remove Section-", "-1"));
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("chkSelect");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }
}