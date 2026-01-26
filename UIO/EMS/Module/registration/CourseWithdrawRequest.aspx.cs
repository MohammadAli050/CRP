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

public partial class Admin_TestCourseWithdraw : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            txtStudentName.Enabled = false;
            txtLastCGPA.Enabled = false;
            txtRegCredit.Enabled = false;
        }
    }    

    protected string LatestCGPA(int studentId)
    {
        try
        {
            StudentACUDetail studentACUDetail = StudentACUDetailManager.GetLatestCGPAByStudentId(studentId);
            if (studentACUDetail != null)
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetById(studentACUDetail.StdAcademicCalenderID);
                if (academicCalender != null)
                    return studentACUDetail.CGPA.ToString() + " [" + academicCalender.Code + "]";
                else
                    return studentACUDetail.CGPA.ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    private void ClearGrid()
    {
        gvCourseDrop.DataSource = null;
        gvCourseDrop.DataBind();
    }
    #endregion

    #region Event
    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            gvCourseDrop.DataSource = null;
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            string roll = txtStudentId.Text;
            Student student = StudentManager.GetByRoll(roll);
            if (student != null)
            {
                Person person = PersonManager.GetById(student.PersonID);
                if (person != null)
                    txtStudentName.Text = person.FullName;

                txtLastCGPA.Text = LatestCGPA(student.StudentID);

                List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(student.StudentID, acaCalId);

                txtRegCredit.Text = studentCourseHistoryList.Sum(o => o.Course.Credits).ToString();
                gvCourseDrop.DataSource = studentCourseHistoryList;
            }
        }
        catch { }
        finally
        {
            gvCourseDrop.DataBind();
        }
    }

    protected void lbRequest_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            string remark = txtRemark.Text;

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(id);
            CourseStatus courseStatusWR = CourseStatusManager.GetByCode("WR");
            CourseStatus courseStatusR = CourseStatusManager.GetByCode("Rn");
            if (studentCourseHistory != null)
            {
                if (studentCourseHistory.CourseStatusID == courseStatusWR.CourseStatusID)
                    studentCourseHistory.CourseStatusID = courseStatusR.CourseStatusID;
                else if (studentCourseHistory.CourseStatusID == courseStatusR.CourseStatusID)
                    studentCourseHistory.CourseStatusID = courseStatusWR.CourseStatusID;
                studentCourseHistory.Remark = remark;
                studentCourseHistory.CourseStatusDate = DateTime.Now;
                studentCourseHistory.CreatedBy = 99;
                studentCourseHistory.CreatedDate = DateTime.Now;
                studentCourseHistory.ModifiedBy = 100;
                studentCourseHistory.ModifiedDate = DateTime.Now;

                bool updateResult = StudentCourseHistoryManager.Update(studentCourseHistory);

                if (updateResult)
                    btnLoad_Click(null, null);
            }
        }
        catch { }
        finally { }
    }

    #endregion
}