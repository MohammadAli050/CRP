using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Common;

public partial class Admin_PreRegistrationByCourse : BasePage
{
    List<Course> courseList;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {
            FillCourseListCombo();
        }
    }

    private void FillCourseListCombo()
    {
        try
        {
            courseListCombo.Items.Clear();

            courseList = CourseManager.GetAllOfferedCourse();

            courseListCombo.AppendDataBoundItems = true;

            foreach (Course offeredCourse in courseList)
            {
                string formalCodeTitle = offeredCourse.CourseID + "_" + offeredCourse.VersionID;
                courseListCombo.Items.Add(new ListItem(offeredCourse.FormalCode, offeredCourse.CourseID + "_" + offeredCourse.VersionID));
            }

            /*if (courseList != null)
            {
                courseListCombo.DataSource = courseList.OrderBy(d => d.CourseID).ToList();
                courseListCombo.DataBind();
            }*/
        }
        catch (Exception ex)
        {

        }
    }
    protected void loadButton_Click(object sender, EventArgs e)
    {
        string[] value=courseListCombo.SelectedValue.Split('_');
        int courseId = Convert.ToInt32(value[0]);
        int versionId =Convert.ToInt32(value[value.Length - 1]);
        string ID = searchBox.Text;
        List<RegistrationWorksheet> regWork = RegistrationWorksheetManager.GetPreRegByCourse(courseId,versionId);

        ResultView.DataSource = regWork;
        ResultView.DataBind();

        if (ID != "")
        {
            //regWork = RegistrationWorksheetManager.GetPreRegByCourse(courseId,versionId);
            List<RegistrationWorksheet> findByID = regWork.Where(x => x.Roll == ID).ToList();

            ResultView.DataSource = findByID;
            ResultView.DataBind();
        }



    }
    
}