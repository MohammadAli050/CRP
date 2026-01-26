using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

public partial class SectionSelection : BasePage
{
    private string AddRegistrationWorksheetId = "AddRegistrationWorksheet";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            int i = 0;

            if (SessionManager.GetObjFromSession<int>("studentId") != 0)
            {
                LoadStudentCourse(SessionManager.GetObjFromSession<int>("studentId"));
            }
        }

        
    }

    protected void btnSectionAdd_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            Session[AddRegistrationWorksheetId] = id;

            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);
            if (registrationWorksheet.CourseStatusId == 8)
            {
                lblMessage.Text = "Already course has been registered";
            }
            else if (!string.IsNullOrEmpty(registrationWorksheet.SectionName))
            {
                lblMessage.Text = "First remove the section then add again.";
            }
            else
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetActiveRegistrationCalender();

                string acacal = academicCalender.AcademicCalenderID.ToString();
                string prog = registrationWorksheet.ProgramID.ToString();
                string dept = registrationWorksheet.DeptID.ToString();
                string crs = registrationWorksheet.CourseID.ToString();
                string vrs = registrationWorksheet.VersionID.ToString();


                string redirectURL = string.Format("{0}/Student/Section.aspx?acacal={1}&prog={2}&dept={3}&crs={4}&vrs={5}",
                                                    AppPath.ApplicationPath, acacal, prog, dept, crs, vrs);

              //  Response.Redirect(redirectURL, "_blank", "menubar=0,scrollbars=1,width=800,height=400,top=10");
            }
        }
        catch (Exception)
        {
        }
    }
    protected void btnRemoveSection_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);

            if (registrationWorksheet.CourseStatusId == 8)
            {
                lblMessage.Text = "Already course has been registered";
            }
            else
            {
                int sectionID = registrationWorksheet.AcaCal_SectionID;

                registrationWorksheet.SectionName = string.Empty;
                registrationWorksheet.AcaCal_SectionID = 0;
                registrationWorksheet.ModifiedDate = DateTime.Now;
                registrationWorksheet.CreatedDate = DateTime.Now;
                bool result = RegistrationWorksheetManager.Update(registrationWorksheet);

                if (result)
                {
                    AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(sectionID);
                    if (academicCalenderSection.Occupied > 0)
                    {
                        academicCalenderSection.Occupied = (academicCalenderSection.Occupied - 1);
                        academicCalenderSection.ModifiedDate = DateTime.Now;
                        academicCalenderSection.ModifiedBy = -2;
                        AcademicCalenderSectionManager.Update(academicCalenderSection);
                    }
                }

                #region Remove conflict
                List<RegistrationWorksheet> courseList = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(1).Where(r => !string.IsNullOrEmpty(r.SectionName)).ToList();
                // int index = courseList.FindIndex(c => c.ID == registrationWorksheet.ID);

                string removeString = registrationWorksheet.FormalCode + "; ";
                registrationWorksheet.ConflictedCourse = string.Empty;
                RegistrationWorksheetManager.Update(registrationWorksheet);

                //courseList.RemoveAt(index);

                foreach (RegistrationWorksheet item in courseList)
                {
                    LogicLayer.BusinessObjects.AcademicCalenderSection sectionTemp = AcademicCalenderSectionManager.GetById(item.AcaCal_SectionID);

                    item.ConflictedCourse = item.ConflictedCourse.Replace(removeString, "");
                    RegistrationWorksheetManager.Update(item);
                }
                #endregion

                LoadStudentCourse(registrationWorksheet.StudentID);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);

            if (registrationWorksheet.CourseStatusId == 8)
            {
                lblMessage.Text = "Already course has been registered";
            }
            else
            {
                RegistrationWorksheetManager.RegisterCourse(id, 1, 8); //8 = course status registered.
            }
        }
        catch (Exception)
        {
        }
    }
    protected void lBtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            LoadStudentCourse(SessionManager.GetObjFromSession<int>("studentId"));
        }
        catch (Exception)
        {
        }
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtStudent.Text))
            {
                lblMessage.Text = "Insert student Roll";
                return;
            }
            Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
            if (student != null)
            {
                if (AccessAuthentication(BussinessObject.UIUMSUser.CurrentUser, student.Roll.Trim()))
                {
                    lblBatch.Text = "Batch";
                    lblProgram.Text = student.Program.ShortName;

                    SessionManager.SaveObjToSession<int>(student.StudentID, "studentId");

                    LoadStudentCourse(student.StudentID);
                }
                else
                {
                    lblMessage.Text = "Access Permission Denied.";
                }
            }
        }
        catch (Exception ex)
        {


        }
    }


    private void LoadStudentCourse(int id)
    {
        List<RegistrationWorksheet> collection = null;
        collection = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(id);

        if (collection != null)
        {
            gvCourseRegistration.DataSource = collection.ToList();
            gvCourseRegistration.DataBind();
        }

    }
}