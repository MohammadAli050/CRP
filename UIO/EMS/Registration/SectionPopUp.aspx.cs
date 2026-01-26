using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BussinessObject;
using System.Collections.Generic;
using Common;

namespace EMS.Registration
{
    public partial class SectionPopUp : BasePage
    {
        private const string FORSECTIONSEARCH = "ForSectionSearch";
        private const string SECTION = "Section";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();
            sccpnEntity = (Student_CalCourseProgNodeEntity)Session[FORSECTIONSEARCH];

            List<AcademicCalenderSection> acsEntities = new List<AcademicCalenderSection>();
            acsEntities = AcademicCalenderSection.GetCourseRoutine(sccpnEntity.AcademicCalenderID, sccpnEntity.DeptID
                , sccpnEntity.ProgramID, sccpnEntity.CourseID, sccpnEntity.VersionID);

            gvSection.DataSource = null;
            gvSection.DataSource = acsEntities;
            gvSection.DataBind();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            AcademicCalenderSection acsEntity = new AcademicCalenderSection();

            foreach (GridViewRow row in gvSection.Rows)
            {
                int i = 0;
                                
                if (((CheckBox)(row.Cells[i].FindControl("chkSelect"))).Checked)
                {
                    acsEntity.Id = Convert.ToInt32(((Label)(row.Cells[i].FindControl("lblId"))).Text);
                    acsEntity.SectionName = ((Label)(row.Cells[i].FindControl("lblSectionName"))).Text;

                    if (Session[SECTION] != null)
                    {
                        Session.Remove(SECTION);
                    }
                    Session[SECTION] = acsEntity; 
                }
            }

            Registration objReg = new Registration();
            //objReg.AddSectionNameTo1stGrid();


        }
    }
}
