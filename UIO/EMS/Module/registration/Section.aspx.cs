using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

public partial class StudentRegistration_Section : BasePage
{

    private string AddRegistrationWorksheetId = "AddRegistrationWorksheet";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack)
        {
            if (Request.QueryString["acacal"] != null &&
                Request.QueryString["prog"] != null &&
                Request.QueryString["dept"] != null &&
                Request.QueryString["crs"] != null &&
                Request.QueryString["vrs"] != null)
            {

                List<SectionDTO> entitiesDTO = SectionDTOManager.GetAllBy(Convert.ToInt32(Request.QueryString["acacal"]),                                                                           
                                                                            Convert.ToInt32(Request.QueryString["prog"]),
                                                                            Convert.ToInt32(Request.QueryString["crs"]),
                                                                            Convert.ToInt32(Request.QueryString["vrs"]));

                if (entitiesDTO != null && entitiesDTO.Count > 0)
                {
                    GridViewSection.DataSource = null;
                    GridViewSection.DataSource = entitiesDTO;
                    GridViewSection.DataBind();

                    LogicLayer.BusinessObjects.Course course = CourseManager.GetByCourseIdVersionId(Convert.ToInt32(Request.QueryString["crs"]), Convert.ToInt32(Request.QueryString["vrs"]));
                    lblMessage.Text = "Course: \"" + course.Title + "\"";
                }
                else
                {
                    lblMessage.Text = "Section Not Found.";
                }
            }
        }
    }

    protected void GridViewSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LogicLayer.BusinessObjects.AcademicCalenderSection academicCalenderSection = null;

            GridViewRow row = GridViewSection.SelectedRow;
            DataKey dtkey = GridViewSection.SelectedDataKey;
            string sectionId = dtkey.Value.ToString();
            academicCalenderSection = AcademicCalenderSectionManager.GetById(Convert.ToInt32(sectionId));
            if (academicCalenderSection.Capacity > academicCalenderSection.Occupied)
            {
                Label lblSection = (Label)row.FindControl("lblSectionName");
                string sectionName = lblSection.Text.Trim();

                Label lblTimeSlot1 = (Label)row.FindControl("lblTimeSlot1");
                string timeSlot1 = lblTimeSlot1.Text.Trim();

                Label lblTimeSlot2 = (Label)row.FindControl("lblTimeSlot2");
                string timeSlot2 = lblTimeSlot2.Text.Trim();

                Label lblDayOne = (Label)row.FindControl("lblDayOne");
                string dayOne = lblDayOne.Text.Trim();

                Label lblDayTwo = (Label)row.FindControl("lblDayTwo");
                string dayTwo = lblDayTwo.Text.Trim();

                RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(Session[AddRegistrationWorksheetId]));

                registrationWorksheet.SectionName = sectionName + " ( " + dayOne + " " + timeSlot1 + "; " +   dayTwo +" "+ timeSlot2 + " )";
                registrationWorksheet.AcaCal_SectionID = Convert.ToInt32(sectionId);
                registrationWorksheet.ModifiedDate = DateTime.Now;
                registrationWorksheet.CreatedDate = DateTime.Now;
                bool result = RegistrationWorksheetManager.Update(registrationWorksheet);

                if (result)
                {
                    academicCalenderSection.Occupied = (academicCalenderSection.Occupied + 1);
                    academicCalenderSection.ModifiedDate = DateTime.Now;
                    academicCalenderSection.ModifiedBy = -2;
                    AcademicCalenderSectionManager.Update(academicCalenderSection);
                }

                // Check conflict

                List<RegistrationWorksheet> courseList = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(registrationWorksheet.StudentID)
                                                                                     .Where(r => !string.IsNullOrEmpty(r.SectionName)).ToList();
                if (courseList.Count > 0)
                {
                    int index = courseList.FindIndex(c => c.ID == registrationWorksheet.ID);
                    courseList.RemoveAt(index);


                    foreach (RegistrationWorksheet item in courseList)
                    {
                        LogicLayer.BusinessObjects.AcademicCalenderSection sectionTemp = AcademicCalenderSectionManager.GetById(item.AcaCal_SectionID);

                        bool conflict = AcademicCalenderSectionManager.IsSectionConflict(academicCalenderSection.DayOne,
                                                                                         academicCalenderSection.TimeSlotPlanOneID,
                                                                                         academicCalenderSection.DayTwo,
                                                                                         academicCalenderSection.TimeSlotPlanTwoID,
                                                                                         sectionTemp.DayOne,
                                                                                         sectionTemp.TimeSlotPlanOneID,
                                                                                         sectionTemp.DayTwo,
                                                                                         sectionTemp.TimeSlotPlanTwoID);
                        if (conflict)
                        {
                            item.ConflictedCourse = item.ConflictedCourse + registrationWorksheet.FormalCode + "; ";
                            RegistrationWorksheetManager.Update(item);

                            registrationWorksheet.ConflictedCourse = registrationWorksheet.ConflictedCourse + item.FormalCode + "; ";
                            RegistrationWorksheetManager.Update(registrationWorksheet);
                        }
                    }
                }


                //

            }
            else
            {
                SessionManager.SaveObjToSession<String>("Section overflow.", "SectionSeclectionMsg");               
            }

            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
        catch (Exception)
        {


        }

    }
}