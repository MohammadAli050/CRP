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
using System.Drawing;

public partial class Admin_CourseDropRequest : BasePage
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
            ucProgram.LoadDropDownList();
            //  Block by MD Sajib Ahmed
            //  
            //if (currentRoleId != 28)
            //{

            //    ddlSession.Enabled = false;
            //}
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
        FillSessionComboBox(Convert.ToInt32(ucProgram.selectedValue));       
    }

    public void FillSessionComboBox(int programId)
    {

        Program program = ProgramManager.GetById(programId);
        int programType = program.ProgramTypeID;
        List<AcademicCalender> sessionList1; 
        List<AcademicCalender> sessionList = new List<AcademicCalender>();
        if (program != null)
            sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);
        sessionList1 = sessionList.Where(l => l.IsActiveRegistration== true).ToList();
        ddlSession.Items.Clear();
        ddlSession.AppendDataBoundItems = true;

        if (sessionList != null)
        {
            // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

            ddlSession.Items.Add(new ListItem("Select All", "0"));
            ddlSession.DataTextField = "FullCode";
            ddlSession.DataValueField = "AcademicCalenderID";

            ddlSession.DataSource = sessionList;
            ddlSession.DataBind();
        }
        ddlSession.SelectedValue = sessionList1[0].AcademicCalenderID.ToString();
    }

               
    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvCourseDrop.DataSource = null;
            int acaCalId = Convert.ToInt32(ddlSession.SelectedItem.Value);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            string roll = txtStudentId.Text;


            if (programId == 0)
            {
                lblMsg.Text = "Please select Program";
                lblMsg.ForeColor = Color.Red; 
                return;
            }
            if (acaCalId == 0)
            {
                lblMsg.Text = "Please select Session";
                lblMsg.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtStudentId.Text.Trim()))
            {
                lblMsg.Text = "Please insert Student's Roll";
                lblMsg.ForeColor = Color.Red;
                return;
            }

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
                gvCourseDrop.DataBind();
            }
        }
        catch { }        
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
            CourseStatus courseStatusDR = CourseStatusManager.GetByCode(CommonEnum.CourseStatus.DR.ToString());
            CourseStatus courseStatusR = CourseStatusManager.GetByCode(CommonEnum.CourseStatus.Rn.ToString());
            if (studentCourseHistory != null)
            {
                if (studentCourseHistory.CourseStatusID == courseStatusDR.CourseStatusID)
                    studentCourseHistory.CourseStatusID = courseStatusR.CourseStatusID;
                else if (studentCourseHistory.CourseStatusID == courseStatusR.CourseStatusID)
                    studentCourseHistory.CourseStatusID = courseStatusDR.CourseStatusID;
                studentCourseHistory.Remark = remark;
                studentCourseHistory.CourseStatusDate = DateTime.Now;

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