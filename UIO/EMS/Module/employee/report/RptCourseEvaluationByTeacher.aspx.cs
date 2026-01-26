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
using CommonUtility;
public partial class RptCourseEvaluationByTeacher : BasePage
{
    public Person teacherInfo;
    public string LoginId;
    public int teacherId;
    public BussinessObject.UIUMSUser CurrentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            CurrentUser = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            UserInPerson uip = UserInPersonManager.GetById(CurrentUser.Id);
            Employee emp = EmployeeManager.GetByPersonId(uip.PersonID);
            teacherInfo = PersonManager.GetById(emp.PersonId);
            teacherId = Convert.ToInt32(emp.EmployeeID);
            
            
            if (!IsPostBack && !IsCallback)
            {
                ShowMassege("");
                
                    LoadComboBox();
            }
        }
        catch (Exception)
        {
            UpdatePanel1.Visible = false;
            ReportViewer1.Visible = false;
            ShowMassege(" Sorry ! You are not eligible for this feature. This Page is only for teachers.");
        }
    }

    private bool IsTeacherEvaluationViewTimeBlock(int acaCalId, int programId, int evaluationTypeId)
    {
        bool isTrue = false;

        List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(acaCalId, programId, evaluationTypeId);
        if (setUpDateForProgramList != null && setUpDateForProgramList.Count > 0)
        {
            SetUpDateForProgram setUpDateForProgram = setUpDateForProgramList[0];

            if (setUpDateForProgram.IsActive == true)
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }
        }

        return isTrue;
    }

    private void LoadComboBox()
    {
        LoadCalenderType();
        LoadProgram();
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

        FillCourseWithSectionList(acaCalId, programId);
    }

    private void FillCourseWithSectionList(int acaCalId, int programId)
    {
        try
        {
            List<AcademicCalenderSectionWithCourse> acaCalSectioList = new List<AcademicCalenderSectionWithCourse>();
            acaCalSectioList = AcademicCalenderSectionManager.GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(acaCalId, programId, teacherId);
            ddlCourseWithSection.DataSource = acaCalSectioList;
            ddlCourseWithSection.DataTextField = "CourseWithSection";
            ddlCourseWithSection.DataValueField = "ValueField";
            ddlCourseWithSection.DataBind();
            ListItem item = new ListItem("-Select Section-", "0");
            ddlCourseWithSection.Items.Insert(0, item);

        }
        catch (Exception ex) { }
    }

    protected void ddlProgram_SelectedIndexChanged(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillCourseWithSectionList(acaCalId, programId);
        ClearRdlc();
    }

    protected void ddlSemester_SelectedIndexChanged(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillCourseWithSectionList(acaCalId, programId);
        ClearRdlc();
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

    protected void GetEvaluationResult_Click(Object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        string[] acaSecIdWithTotal = ddlCourseWithSection.Text.Split('-');
        int acaSecId = Convert.ToInt32(acaSecIdWithTotal[0]);

        if (IsTeacherEvaluationViewTimeBlock(acaCalId, programId, (int)CommonEnum.ActivityType.Evaluation))
        {
            ShowMassege(" Sorry ! Course evaluation view is off now.");

            return;
        }
        else
        {
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
    }

    private void LoadCourseEvaluationResult(int acaCalId, int acaSecId)
    {
        try
        {
            string session = ddlSemester.SelectedItem.Text;
            string programName = ddlProgram.SelectedItem.Text;
            string tracherName = teacherInfo.FullName.ToString();
            string courseName = ddlCourseWithSection.SelectedItem.Text;
            string fileName = tracherName + courseName;
            string[] acaSecIdWithTotal = ddlCourseWithSection.Text.Split('-');
            string totalStudent = acaSecIdWithTotal[1];
            string totalparticipant = acaSecIdWithTotal[2];
            string loginId = CurrentUser.LogInID;

            List<rCourseEvaluationResult> list = CourseEvaluationManager.GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(acaCalId, acaSecId);
            List<rCourseEvaluationComment> list2 = CourseEvaluationManager.GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(acaCalId, acaSecId);

            if (list != null && list.Count > 0)
            {
                ReportParameter p1 = new ReportParameter("Session", session);
                ReportParameter p2 = new ReportParameter("Program", programName);
                ReportParameter p3 = new ReportParameter("Teacher", tracherName);
                ReportParameter p4 = new ReportParameter("Course", courseName);
                ReportParameter p5 = new ReportParameter("TotalStudent", totalStudent);
                ReportParameter p6 = new ReportParameter("TotalParticipant", totalparticipant);
                ReportParameter p7 = new ReportParameter("LoginID", loginId);

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/employee/report/RptCourseEvaluationResult.rdlc");
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7 });
                ReportDataSource rds = new ReportDataSource("CourseEvaluationResultDataSet", list);
                ReportDataSource rds2 = new ReportDataSource("CourseEvaluationCommentDataSet", list2);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rds2);
                ReportViewer1.LocalReport.DisplayName = fileName;
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
