using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.SyllabusMan
{
    public partial class AssociateCourseList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.CheckPage_Load();

                if (!IsPostBack)
                {
                    LoadAssociateCourseList();
                }
            }
            catch
            { }
        }

        private void LoadAssociateCourseList()
        {
            List<Course> courseList = CourseManager.GetAll();
            if (courseList != null)
            {
                courseList = courseList.Where(c =>  c.AssocCourseID!= 0).ToList();

                
                gvAssociateCourseList.DataSource = courseList.OrderBy(c => c.FormalCode).ToList();
                gvAssociateCourseList.DataBind();
            }
        }
    }
}