using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
public partial class RptCourseEvaluation : BasePage
{
    public BussinessObject.UIUMSUser CurrentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        CurrentUser = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack && !IsCallback)
        {
            ShowMassege(""); 
            LoadComboBox();
        }
    }

    private void LoadComboBox()
    {
        LoadCalenderType();
        LoadProgram();
        LoadTeacherList();
        LoadCourseWithSection();
    }

    private void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();

            List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                ddlCalenderType.DataTextField = "Name";
                ddlCalenderType.DataSource = calenderUnitMasterList;
                ddlCalenderType.DataBind();
            }
        }
        catch { }
        finally
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadSession(calenderTypeId);
        }
    }

    private void LoadSession(int calenderTypeId)
    {
        List<AcademicCalender> sessionList = AcademicCalenderManager.GetAll(calenderTypeId);
        
        ddlSemester.Items.Clear();
        ddlSemester.AppendDataBoundItems = true;
        if (sessionList != null)
        {
            ddlSemester.Items.Add(new ListItem("Select", "0"));
            ddlSemester.DataTextField = "FullCode";
            ddlSemester.DataValueField = "AcademicCalenderID";
            ddlSemester.DataSource = sessionList;
            ddlSemester.DataBind();
        }
    }

    private void LoadProgram()
    {
        List<Program> programList = new List<Program>();
        programList = ProgramManager.GetAll();

        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;

        if (programList != null)
        {
            ddlProgram.Items.Add(new ListItem("-Select-", "0"));
            ddlProgram.DataTextField = "ShortName";
            ddlProgram.DataValueField = "ProgramID";

            ddlProgram.DataSource = programList;
            ddlProgram.DataBind();
        }
    }

    private void LoadCourseWithSection()
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int teacherID = Convert.ToInt32(ddlTeacher.SelectedValue);

        FillCourseWithSectionList(acaCalId,programId,teacherID);
    }

    private void LoadTeacherList()
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillTeacherListCombo(acaCalId, programId);
    }

    private void FillTeacherListCombo(int acaCalId, int programId)
    {
        try
        {
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("-Select Teacher-", "0"));

            List<TeacherInfo> teacherList = AcademicCalenderSectionManager.GetAllTeacherByAcaCalAndProgram(acaCalId, programId);
            teacherList = teacherList.OrderBy(C => C.TeacherName).ToList();
            if (teacherList.Count > 0 && teacherList != null)
            {
                foreach (TeacherInfo teacher in teacherList)
                {
                    string valueField = teacher.TeacherId.ToString();
                    string textField = teacher.TeacherName;
                    ddlTeacher.Items.Add(new ListItem(textField, valueField));
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    private void FillCourseWithSectionList(int acaCalId,int programId,int teacherId)
    {
        try
        {
            List<AcademicCalenderSectionWithCourse> acaCalSectioList = new List<AcademicCalenderSectionWithCourse>();
            acaCalSectioList = AcademicCalenderSectionManager.GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(acaCalId,programId,teacherId);
            ddlCourseWithSection.DataSource = acaCalSectioList;
            ddlCourseWithSection.DataTextField = "CourseWithSection";
            ddlCourseWithSection.DataValueField = "ValueField";
            ddlCourseWithSection.DataBind();
            ListItem item = new ListItem("-Select Section-", "0");
            ddlCourseWithSection.Items.Insert(0, item);

        }
        catch (Exception ex) { }
    }

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadSession(calenderTypeId);
        }
        catch { }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillTeacherListCombo(acaCalId, programId);
        ClearRdlc();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);

        FillTeacherListCombo(acaCalId, programId);
        ClearRdlc();
    }    

    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int teacherId = Convert.ToInt32(ddlTeacher.SelectedValue);
            FillCourseWithSectionList(acaCalId,programId,teacherId);
            ClearRdlc();

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void GetEvaluationResult_Click(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        string[] acaSecIdWithTotal = ddlCourseWithSection.Text.Split('-');
        int acaSecId = Convert.ToInt32(acaSecIdWithTotal[0]);
        int teacherId = Convert.ToInt32(ddlTeacher.SelectedValue);

        if (acaCalId == 0 || programId == 0 || teacherId == 0 || acaSecId == 0)
        {
            string invalidMassege = "Invalid Selection ! Please Select ";
            int flag = 0;
            if (acaCalId == 0)
            {
                invalidMassege += " A Semester";
                flag = 1;
            }
            if (programId == 0)
            {
                if (flag == 1)
                {
                    invalidMassege += " and";
                }
                invalidMassege += " A Program";
                flag = 1;
            }

            if (teacherId == 0)
            {
                if (flag == 1)
                {
                    invalidMassege += " and";
                }
                invalidMassege += " A Teacher";
            }
            if (acaSecId == 0)
            {
                if (flag == 1)
                {
                    invalidMassege += " and";
                }
                invalidMassege += " A Course With Section";
            }
            ShowMassege(invalidMassege);
            ClearRdlc();

        }
        else
        {
            ShowMassege("");
            LoadCourseEvaluationResult(acaCalId, acaSecId);
        }

    }

    private void LoadCourseEvaluationResult(int acaCalId, int acaSecId)
    {
        try
        {
            string session = ddlSemester.SelectedItem.Text;
            string programName = ddlProgram.SelectedItem.Text;
            string tracherName = ddlTeacher.SelectedItem.Text;
            string courseName = ddlCourseWithSection.SelectedItem.Text;
            string fileName = tracherName + courseName;
            string[] acaSecIdWithTotal = ddlCourseWithSection.Text.Split('-');
            string totalStudent = acaSecIdWithTotal[1];
            string totalparticipant = acaSecIdWithTotal[2];            

            List<rCourseEvaluationResult> list = CourseEvaluationManager.GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(acaCalId, acaSecId);
            List<rCourseEvaluationComment> list2 = CourseEvaluationManager.GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(acaCalId, acaSecId);

            if (list != null && list.Count > 0)
            {
                ReportParameter p1 = new ReportParameter("Session", session);
                ReportParameter p2 = new ReportParameter("Program", programName);
                ReportParameter p3 = new ReportParameter("Teacher", tracherName);
                ReportParameter p4 = new ReportParameter("Course", courseName);
                ReportParameter p5= new ReportParameter("TotalStudent", totalStudent);
                ReportParameter p6 = new ReportParameter("TotalParticipant", totalparticipant);
                ReportParameter p7 = new ReportParameter("LoginID", CurrentUser.LogInID);

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/employee/report/RptCourseEvaluationResult.rdlc");
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7});
                ReportDataSource rds = new ReportDataSource("CourseEvaluationResultDataSet", list);
                ReportDataSource rds2 = new ReportDataSource("CourseEvaluationCommentDataSet", list2);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rds2);
                ReportViewer1.LocalReport.DisplayName=fileName;
                ReportViewer1.Visible = true;
                ShowMassege("");
            }
            else
            {
                ShowMassege("No Data Found Or Somthing Went Wrong !");
                ClearRdlc();
            }
        }
        catch (Exception)
        { }
    }

    private void ClearRdlc()
    {
        ReportDataSource rds = new ReportDataSource(null);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.Visible = false;
    }

    private void ShowMassege(string massege)
    {
        lblMessage.Text = massege;
    }

}
