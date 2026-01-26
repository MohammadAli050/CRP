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

public partial class ServayByStudent_EvaluationForm : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {   
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                PanelAreaShowHide(1, 1, 1);
                SetStudentId();
                LoadPageData();
            }
        }
        catch { }
    }

    protected void PanelAreaShowHide(int theory, int lab, int bus)
    {
        if (theory != 0)
            pnTheoryCourse.Visible = false;
        if (lab != 0)
            pnLabCourse.Visible = false;
        if (bus != 0)
            pnBussiness.Visible = false;
    }

    protected void SetStudentId()
    {
        try
        {
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            Person person = PersonManager.GetByUserId(user.User_ID);
            Student std = StudentManager.GetBypersonID(person.PersonID);

            Session["StudentId"] = std.StudentID;
            Session["UserId"] = user.User_ID;
            Session["Roll"] = std.Roll;
        }
        catch { }
    }

    protected void LoadPageData()
    {
        #region AcaCal Load
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        Person person = PersonManager.GetByUserId(user.User_ID);
        Student std = StudentManager.GetBypersonID(person.PersonID);
        AcademicCalender acaCal = AcademicCalenderManager.GetIsCurrent(std.Program.CalenderUnitMasterID);
        lblSemester.Text = "[" + acaCal.Code + "] " + UtilityManager.UppercaseFirst(acaCal.CalendarUnitType_TypeName) + " " + acaCal.Year;
        hfSemester.Value = acaCal.AcademicCalenderID.ToString();

        #endregion

        #region Course List

        ddlAcaCalSection.Items.Clear();
        ddlAcaCalSection.Items.Add(new ListItem("Select", "0_0"));

        int studentId = std.StudentID;//Convert.ToInt32(Session["StudentId"]);
        
        if (studentId != 0)
        {
            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll(studentId, acaCal.AcademicCalenderID);
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {

                List<TypeDefinition> typeDefinationList = TypeDefinitionManager.GetAll();
                TypeDefinition typeDefinationForTheory = null;
                TypeDefinition typeDefinationForLab = null;
                if (typeDefinationList.Count > 0 && typeDefinationList != null)
                {
                    typeDefinationForTheory = typeDefinationList.Where(x => x.Type == "Course" && x.Definition == "Theory").FirstOrDefault();
                    typeDefinationForLab = typeDefinationList.Where(x => x.Type == "Course" && x.Definition == "Lab").FirstOrDefault();
                }
        
                Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                {
                    Course course = CourseManager.GetByCourseIdVersionId(acaCalSection.CourseID, acaCalSection.VersionID);
                    if (typeDefinationForTheory != null && typeDefinationForLab != null)
                    {
                        if (course.TypeDefinitionID == typeDefinationForTheory.TypeDefinitionID)
                        {
                            string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();

                            dicAcaCalSec.Add(course.Title + ": " + course.FormalCode + " (" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString() + "_Theory");
                        }
                        else if (course.TypeDefinitionID == typeDefinationForLab.TypeDefinitionID)
                        {
                            string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();

                            dicAcaCalSec.Add(course.Title + ": " + course.FormalCode + " (" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString() + "_Lab");
                        }
                    }
                   
                }
                var acaCalSecList = dicAcaCalSec.OrderBy(x => x.Key).ToList();
                foreach (var temp in acaCalSecList)
                    ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
            }
        }

        #endregion

        #region Expected Grade

        ddlExpectedGrade.Items.Clear();
        ddlExpectedGrade.Items.Add(new ListItem("Select", "0"));

        List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetAll().Where(x=>x.GradeMasterId==std.GradeMasterId).ToList();
        if (gradeDetailsList.Count > 0 && gradeDetailsList != null)
        {
            gradeDetailsList = gradeDetailsList.Where(x => x.Grade != "S").ToList();

            ddlExpectedGrade.AppendDataBoundItems = true;
            ddlExpectedGrade.DataSource = gradeDetailsList;
            ddlExpectedGrade.DataValueField = "Grade";
            ddlExpectedGrade.DataTextField = "Grade";
            ddlExpectedGrade.DataBind();
        }

        #endregion
    }

    protected int InsertEvaluationFormTheory(int studentId)
    {
        try
        {
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);

            EvaluationForm evaluationForm = new EvaluationForm();
            evaluationForm.PersonId = studentId;
            evaluationForm.AcaCalId = Convert.ToInt32(hfSemester.Value);
            evaluationForm.AcaCalSecId = acaCalSecId;
            evaluationForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
            evaluationForm.Q1 = Convert.ToInt32(rbtTheory1.SelectedValue);
            evaluationForm.Q2 = Convert.ToInt32(rbtTheory2.SelectedValue);
            evaluationForm.Q3 = Convert.ToInt32(rbtTheory3.SelectedValue);
            evaluationForm.Q4 = Convert.ToInt32(rbtTheory4.SelectedValue);
            evaluationForm.Q5 = Convert.ToInt32(rbtTheory5.SelectedValue);
            evaluationForm.Q6 = Convert.ToInt32(rbtTheory6.SelectedValue);
            evaluationForm.Q7 = Convert.ToInt32(rbtTheory7.SelectedValue);
            evaluationForm.Q8 = Convert.ToInt32(rbtTheory8.SelectedValue);
            evaluationForm.Q9 = Convert.ToInt32(rbtTheory9.SelectedValue);
            evaluationForm.Q10 = Convert.ToInt32(rbtTheory10.SelectedValue);
            evaluationForm.Q11 = Convert.ToInt32(rbtTheory11.SelectedValue);
            evaluationForm.Q12 = Convert.ToInt32(rbtTheory12.SelectedValue);
            evaluationForm.Q13 = Convert.ToInt32(rbtTheory13.SelectedValue);
            evaluationForm.Q14 = Convert.ToInt32(rbtTheory14.SelectedValue);
            evaluationForm.Q15 = Convert.ToInt32(rbtTheory15.SelectedValue);
            evaluationForm.Q16 = Convert.ToInt32(rbtTheory16.SelectedValue);
            evaluationForm.Q17 = Convert.ToInt32(rbtTheory17.SelectedValue);
            evaluationForm.Comment = txtComments.Text;
            evaluationForm.CreatedBy = Convert.ToInt32(Session["UserId"]);
            evaluationForm.CreatedDate = DateTime.Now;

            int insertResult = EvaluationFormManager.Insert(evaluationForm);

            return insertResult;
        }
        catch { return 0; }
    }

    protected bool UpdateEvaluationFormTheory(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaForm = EvaluationFormManager.GetBy(studentId, acaCalSecId);
            if (evaForm != null)
            {
                evaForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
                evaForm.Q1 = Convert.ToInt32(rbtTheory1.SelectedValue);
                evaForm.Q2 = Convert.ToInt32(rbtTheory2.SelectedValue);
                evaForm.Q3 = Convert.ToInt32(rbtTheory3.SelectedValue);
                evaForm.Q4 = Convert.ToInt32(rbtTheory4.SelectedValue);
                evaForm.Q5 = Convert.ToInt32(rbtTheory5.SelectedValue);
                evaForm.Q6 = Convert.ToInt32(rbtTheory6.SelectedValue);
                evaForm.Q7 = Convert.ToInt32(rbtTheory7.SelectedValue);
                evaForm.Q8 = Convert.ToInt32(rbtTheory8.SelectedValue);
                evaForm.Q9 = Convert.ToInt32(rbtTheory9.SelectedValue);
                evaForm.Q10 = Convert.ToInt32(rbtTheory10.SelectedValue);
                evaForm.Q11 = Convert.ToInt32(rbtTheory11.SelectedValue);
                evaForm.Q12 = Convert.ToInt32(rbtTheory12.SelectedValue);
                evaForm.Q13 = Convert.ToInt32(rbtTheory13.SelectedValue);
                evaForm.Q14 = Convert.ToInt32(rbtTheory14.SelectedValue);
                evaForm.Q15 = Convert.ToInt32(rbtTheory15.SelectedValue);
                evaForm.Q16 = Convert.ToInt32(rbtTheory16.SelectedValue);
                evaForm.Q17 = Convert.ToInt32(rbtTheory17.SelectedValue);
                evaForm.Comment = txtComments.Text;
                evaForm.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                evaForm.ModifiedDate = DateTime.Now;

                bool resultUpdate = EvaluationFormManager.Update(evaForm);

                return resultUpdate;
            }
            return false;
        }
        catch { return false; }
    }

    protected int InsertEvaluationFormLab(int studentId)
    {
        try
        {
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);

            EvaluationForm evaluationForm = new EvaluationForm();
            evaluationForm.PersonId = studentId;
            evaluationForm.AcaCalId = Convert.ToInt32(hfSemester.Value);
            evaluationForm.AcaCalSecId = acaCalSecId;
            evaluationForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
            evaluationForm.Q1 = Convert.ToInt32(rbtLab1.SelectedValue);
            evaluationForm.Q2 = Convert.ToInt32(rbtLab2.SelectedValue);
            evaluationForm.Q3 = Convert.ToInt32(rbtLab3.SelectedValue);
            evaluationForm.Q4 = Convert.ToInt32(rbtLab4.SelectedValue);
            evaluationForm.Q5 = Convert.ToInt32(rbtLab5.SelectedValue);
            evaluationForm.Q6 = Convert.ToInt32(rbtLab6.SelectedValue);
            evaluationForm.Q7 = Convert.ToInt32(rbtLab7.SelectedValue);
            evaluationForm.Q8 = Convert.ToInt32(rbtLab8.SelectedValue);
            evaluationForm.Q9 = Convert.ToInt32(rbtLab9.SelectedValue);
            evaluationForm.Q10 = Convert.ToInt32(rbtLab10.SelectedValue);
            evaluationForm.Q11 = Convert.ToInt32(rbtLab11.SelectedValue);
            evaluationForm.CreatedBy = Convert.ToInt32(Session["UserId"]);
            evaluationForm.CreatedDate = DateTime.Now;

            int insertResult = EvaluationFormManager.Insert(evaluationForm);

            return insertResult;
        }
        catch { return 0; }
    }

    protected bool UpdateEvaluationFormLab(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaForm = EvaluationFormManager.GetBy(studentId, acaCalSecId);
            if (evaForm != null)
            {
                evaForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
                evaForm.Q1 = Convert.ToInt32(rbtLab1.SelectedValue);
                evaForm.Q2 = Convert.ToInt32(rbtLab2.SelectedValue);
                evaForm.Q3 = Convert.ToInt32(rbtLab3.SelectedValue);
                evaForm.Q4 = Convert.ToInt32(rbtLab4.SelectedValue);
                evaForm.Q5 = Convert.ToInt32(rbtLab5.SelectedValue);
                evaForm.Q6 = Convert.ToInt32(rbtLab6.SelectedValue);
                evaForm.Q7 = Convert.ToInt32(rbtLab7.SelectedValue);
                evaForm.Q8 = Convert.ToInt32(rbtLab8.SelectedValue);
                evaForm.Q9 = Convert.ToInt32(rbtLab9.SelectedValue);
                evaForm.Q10 = Convert.ToInt32(rbtLab10.SelectedValue);
                evaForm.Q11 = Convert.ToInt32(rbtLab11.SelectedValue);
                evaForm.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                evaForm.ModifiedDate = DateTime.Now;

                bool resultUpdate = EvaluationFormManager.Update(evaForm);

                return resultUpdate;
            }
            return false;
        }
        catch { return false; }
    }

    protected int InsertEvaluationFormBus(int studentId)
    {
        try
        {
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);

            EvaluationForm evaluationForm = new EvaluationForm();
            evaluationForm.PersonId = studentId;
            evaluationForm.AcaCalId = Convert.ToInt32(hfSemester.Value);
            evaluationForm.AcaCalSecId = acaCalSecId;
            evaluationForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
            evaluationForm.Q1 = Convert.ToInt32(rbtBus1.SelectedValue);
            evaluationForm.Q2 = Convert.ToInt32(rbtBus2.SelectedValue);
            evaluationForm.Q3 = Convert.ToInt32(rbtBus3.SelectedValue);
            evaluationForm.Q4 = Convert.ToInt32(rbtBus4.SelectedValue);
            evaluationForm.Q5 = Convert.ToInt32(rbtBus5.SelectedValue);
            evaluationForm.Q6 = Convert.ToInt32(rbtBus6.SelectedValue);
            evaluationForm.Q7 = Convert.ToInt32(rbtBus7.SelectedValue);
            evaluationForm.Q8 = Convert.ToInt32(rbtBus8.SelectedValue);
            evaluationForm.Q9 = Convert.ToInt32(rbtBus9.SelectedValue);
            evaluationForm.Q10 = Convert.ToInt32(rbtBus10.SelectedValue);
            evaluationForm.Q11 = Convert.ToInt32(rbtBus11.SelectedValue);
            evaluationForm.Comment = txtBusComments.Text;
            evaluationForm.CreatedBy = Convert.ToInt32(Session["UserId"]);
            evaluationForm.CreatedDate = DateTime.Now;

            int insertResult = EvaluationFormManager.Insert(evaluationForm);

            return insertResult;
        }
        catch { return 0; }
    }

    protected bool UpdateEvaluationFormBus(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaForm = EvaluationFormManager.GetBy(studentId, acaCalSecId);
            if (evaForm != null)
            {
                evaForm.ExpectedGrade = ddlExpectedGrade.SelectedValue;
                evaForm.Q1 = Convert.ToInt32(rbtBus1.SelectedValue);
                evaForm.Q2 = Convert.ToInt32(rbtBus2.SelectedValue);
                evaForm.Q3 = Convert.ToInt32(rbtBus3.SelectedValue);
                evaForm.Q4 = Convert.ToInt32(rbtBus4.SelectedValue);
                evaForm.Q5 = Convert.ToInt32(rbtBus5.SelectedValue);
                evaForm.Q6 = Convert.ToInt32(rbtBus6.SelectedValue);
                evaForm.Q7 = Convert.ToInt32(rbtBus7.SelectedValue);
                evaForm.Q8 = Convert.ToInt32(rbtBus8.SelectedValue);
                evaForm.Q9 = Convert.ToInt32(rbtBus9.SelectedValue);
                evaForm.Q10 = Convert.ToInt32(rbtBus10.SelectedValue);
                evaForm.Q11 = Convert.ToInt32(rbtBus11.SelectedValue);
                evaForm.Comment = txtBusComments.Text;
                evaForm.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                evaForm.ModifiedDate = DateTime.Now;

                bool resultUpdate = EvaluationFormManager.Update(evaForm);

                return resultUpdate;
            }
            return false;
        }
        catch { return false; }
    }

    protected void ClearTheoryInputField()
    {
        rbtTheory1.ClearSelection();
        rbtTheory2.ClearSelection();
        rbtTheory3.ClearSelection();
        rbtTheory4.ClearSelection();
        rbtTheory5.ClearSelection();
        rbtTheory6.ClearSelection();
        rbtTheory7.ClearSelection();
        rbtTheory8.ClearSelection();
        rbtTheory9.ClearSelection();
        rbtTheory10.ClearSelection();
        rbtTheory11.ClearSelection();
        rbtTheory12.ClearSelection();
        rbtTheory13.ClearSelection();
        rbtTheory14.ClearSelection();
        rbtTheory15.ClearSelection();
        rbtTheory16.ClearSelection();
        rbtTheory17.ClearSelection();
        txtComments.Text = "";
    }

    protected void ClearLabInputField()
    {
        rbtLab1.ClearSelection();
        rbtLab2.ClearSelection();
        rbtLab3.ClearSelection();
        rbtLab4.ClearSelection();
        rbtLab5.ClearSelection();
        rbtLab6.ClearSelection();
        rbtLab7.ClearSelection();
        rbtLab8.ClearSelection();
        rbtLab9.ClearSelection();
        rbtLab10.ClearSelection();
        rbtLab11.ClearSelection();
        txtComments.Text = "";
    }

    protected void ClearBusInputField()
    {
        rbtBus1.ClearSelection();
        rbtBus2.ClearSelection();
        rbtBus3.ClearSelection();
        rbtBus4.ClearSelection();
        rbtBus5.ClearSelection();
        rbtBus6.ClearSelection();
        rbtBus7.ClearSelection();
        rbtBus8.ClearSelection();
        rbtBus9.ClearSelection();
        rbtBus10.ClearSelection();
        rbtBus11.ClearSelection();
        txtBusComments.Text = "";
    }

    protected void LoadTheoryCourseData(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaFrom = EvaluationFormManager.GetBy(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
            if (evaFrom != null)
            {
                if (evaFrom.ExpectedGrade != null) ddlExpectedGrade.SelectedValue = evaFrom.ExpectedGrade;
                rbtTheory1.SelectedValue = evaFrom.Q1.ToString();
                rbtTheory2.SelectedValue = evaFrom.Q2.ToString();
                rbtTheory3.SelectedValue = evaFrom.Q3.ToString();
                rbtTheory4.SelectedValue = evaFrom.Q4.ToString();
                rbtTheory5.SelectedValue = evaFrom.Q5.ToString();
                rbtTheory6.SelectedValue = evaFrom.Q6.ToString();
                rbtTheory7.SelectedValue = evaFrom.Q7.ToString();
                rbtTheory8.SelectedValue = evaFrom.Q8.ToString();
                rbtTheory9.SelectedValue = evaFrom.Q9.ToString();
                rbtTheory10.SelectedValue = evaFrom.Q10.ToString();
                rbtTheory11.SelectedValue = evaFrom.Q11.ToString();
                rbtTheory12.SelectedValue = evaFrom.Q12.ToString();
                rbtTheory13.SelectedValue = evaFrom.Q13.ToString();
                rbtTheory14.SelectedValue = evaFrom.Q14.ToString();
                rbtTheory15.SelectedValue = evaFrom.Q15.ToString();
                rbtTheory16.SelectedValue = evaFrom.Q16.ToString();
                rbtTheory17.SelectedValue = evaFrom.Q17.ToString();
                txtComments.Text = evaFrom.Comment;

                btnTheorySubmit.Text = "Update";
            }
            else
            {
                ClearTheoryInputField();
                btnTheorySubmit.Text = "Save";
            }
        }
        catch { }
    }

    protected void LoadLabCourseData(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaFrom = EvaluationFormManager.GetBy(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
            if (evaFrom != null)
            {
                rbtLab1.SelectedValue = evaFrom.Q1.ToString();
                rbtLab2.SelectedValue = evaFrom.Q2.ToString();
                rbtLab3.SelectedValue = evaFrom.Q3.ToString();
                rbtLab4.SelectedValue = evaFrom.Q4.ToString();
                rbtLab5.SelectedValue = evaFrom.Q5.ToString();
                rbtLab6.SelectedValue = evaFrom.Q6.ToString();
                rbtLab7.SelectedValue = evaFrom.Q7.ToString();
                rbtLab8.SelectedValue = evaFrom.Q8.ToString();
                rbtLab9.SelectedValue = evaFrom.Q9.ToString();
                rbtLab10.SelectedValue = evaFrom.Q10.ToString();
                rbtLab11.SelectedValue = evaFrom.Q11.ToString();

                btnLabSubmit.Text = "Update";
            }
            else
            {
                ClearLabInputField();
                btnLabSubmit.Text = "Save";
            }
        }
        catch { }
    }

    protected void LoadBusCourseData(int studentId, int acaCalSecId)
    {
        try
        {
            EvaluationForm evaFrom = EvaluationFormManager.GetBy(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
            if (evaFrom != null)
            {
                if (evaFrom.ExpectedGrade != null) ddlExpectedGrade.SelectedValue = evaFrom.ExpectedGrade;
                rbtBus1.SelectedValue = evaFrom.Q1.ToString();
                rbtBus2.SelectedValue = evaFrom.Q2.ToString();
                rbtBus3.SelectedValue = evaFrom.Q3.ToString();
                rbtBus4.SelectedValue = evaFrom.Q4.ToString();
                rbtBus5.SelectedValue = evaFrom.Q5.ToString();
                rbtBus6.SelectedValue = evaFrom.Q6.ToString();
                rbtBus7.SelectedValue = evaFrom.Q7.ToString();
                rbtBus8.SelectedValue = evaFrom.Q8.ToString();
                rbtBus9.SelectedValue = evaFrom.Q9.ToString();
                rbtBus10.SelectedValue = evaFrom.Q10.ToString();
                rbtBus11.SelectedValue = evaFrom.Q11.ToString();

                txtBusComments.Text = evaFrom.Comment;

                btnBusSubmit.Text = "Update";
            }
            else
            {
                ClearBusInputField();
                btnBusSubmit.Text = "Save";
            }
        }
        catch { }
    }

    protected bool CheckSelectedValueForTheory()
    {
        try
        {
            if (Convert.ToInt32(rbtTheory1.SelectedValue) != 0 && Convert.ToInt32(rbtTheory2.SelectedValue) != 0 && Convert.ToInt32(rbtTheory3.SelectedValue) != 0 && Convert.ToInt32(rbtTheory4.SelectedValue) != 0 && Convert.ToInt32(rbtTheory5.SelectedValue) != 0 && Convert.ToInt32(rbtTheory6.SelectedValue) != 0 && Convert.ToInt32(rbtTheory7.SelectedValue) != 0 && Convert.ToInt32(rbtTheory8.SelectedValue) != 0 && Convert.ToInt32(rbtTheory9.SelectedValue) != 0 && Convert.ToInt32(rbtTheory10.SelectedValue) != 0 && Convert.ToInt32(rbtTheory11.SelectedValue) != 0 && Convert.ToInt32(rbtTheory12.SelectedValue) != 0 && Convert.ToInt32(rbtTheory13.SelectedValue) != 0 && Convert.ToInt32(rbtTheory14.SelectedValue) != 0 && Convert.ToInt32(rbtTheory15.SelectedValue) != 0 && Convert.ToInt32(rbtTheory16.SelectedValue) != 0 && Convert.ToInt32(rbtTheory17.SelectedValue) != 0)
                return true;
            else
                return false;
        }
        catch { return false; }
    }

    #endregion

    #region Event

    protected void ddlAcaCalSection_Change(Object sender, EventArgs e)
    {
        try
        {
            ddlExpectedGrade.SelectedValue = "0";

            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');

            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);
            string courseType = acaCalSecValue[1];

            string roll = Session["Roll"].ToString().Substring(0, 3);

            if (acaCalSecId != 0)
            {
               /* if (roll == "111" || roll == "114" || roll == "121" || roll == "112" || roll == "113" || roll == "124")
                {
                    pnTheoryCourse.Visible = false;
                    pnLabCourse.Visible = false;
                    pnBussiness.Visible = true;
                    LoadBusCourseData(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
                }*/
                if(courseType == "Theory")
                {
                    pnTheoryCourse.Visible = true;
                    pnLabCourse.Visible = false;
                    pnBussiness.Visible = false;
                    LoadTheoryCourseData(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
                }
                else if (courseType == "Lab")
                {
                    pnTheoryCourse.Visible = false;
                    pnLabCourse.Visible = true;
                    pnBussiness.Visible = false;
                    LoadLabCourseData(Convert.ToInt32(Session["StudentId"]), acaCalSecId);
                }

                AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSecId);
                if (acaCalSection != null)
                {
                    string facultyName = string.Empty;
                    if (acaCalSection.TeacherOneID != 0)
                    {
                        Employee emp1 = EmployeeManager.GetById(acaCalSection.TeacherOneID);
                        if (emp1 != null)
                        {
                            Person person = PersonManager.GetById(emp1.PersonId);
                            if (person != null)
                                facultyName += person.FullName + " [" + emp1.Code + "]";
                        }
                    }

                    if (acaCalSection.TeacherTwoID != 0)
                    {
                        Employee emp2 = EmployeeManager.GetById(acaCalSection.TeacherTwoID);
                        if (facultyName.Length != 0)
                            facultyName += " / ";
                        if (emp2 != null)
                        {
                            Person person = PersonManager.GetById(emp2.PersonId);
                            if (person != null)
                                facultyName += person.FullName + " [" + emp2.Code + "]";
                        }
                    }
                    lblFacultyName.Text = facultyName;
                }
            }
            else
            {
                ClearTheoryInputField();
                ClearLabInputField();
                lblFacultyName.Text = "";
                lblMsg.Text = "";
                ddlExpectedGrade.SelectedValue = "0";
                PanelAreaShowHide(1, 1, 1);
            }
            ddlAcaCalSection.Focus();
        }
        catch { }
    }

    protected void btnTheorySubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);

            if (btnTheorySubmit.Text == "Save")
            {
                bool warningAlert = CheckSelectedValueForTheory();
                if (!warningAlert)
                {
                    lblMsg.Text = "Please fill inquires";
                    pnMessage.Focus();
                }
                else
                {

                    int resultInsert = InsertEvaluationFormTheory(studentId);
                    if (resultInsert != 0)
                    {
                        lblMsg.Text = "Evaluation Complete Successfullly";
                        lblMsg.Focus();
                        ClearTheoryInputField();
                        PanelAreaShowHide(1, 1, 1);
                    }
                }
            }
            else if (btnTheorySubmit.Text == "Update")
            {
                bool resultUpdate = UpdateEvaluationFormTheory(studentId, acaCalSecId);
                if (resultUpdate)
                {
                    lblMsg.Text = "Updated Evaluation";
                    ddlAcaCalSection.Focus();
                    ClearTheoryInputField();
                    PanelAreaShowHide(1, 1, 1);

                }
            }
        }
        catch { }
    }

    protected void btnLabSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);
            if (btnLabSubmit.Text == "Save")
            {
                int resultInsert = InsertEvaluationFormLab(studentId);
                if (resultInsert != 0)
                {
                    lblMsg.Text = "Evaluation Complete Successfullly";
                    lblMsg.Focus();
                    ClearTheoryInputField();
                    PanelAreaShowHide(1, 1, 1);
                }
            }
            else if (btnTheorySubmit.Text == "Update")
            {
                bool resultUpdate = UpdateEvaluationFormLab(studentId, acaCalSecId);
                if (resultUpdate)
                {
                    lblMsg.Text = "Updated Evaluation";
                    lblMsg.Focus();
                    ClearLabInputField();
                    PanelAreaShowHide(1, 1, 1);
                }
            }
        }
        catch { }
    }

    protected void btnBusSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            string[] acaCalSecValue = ddlAcaCalSection.SelectedValue.Split('_');
            int acaCalSecId = Convert.ToInt32(acaCalSecValue[0]);
            if (btnBusSubmit.Text == "Save")
            {
                int resultInsert = InsertEvaluationFormBus(studentId);
                if (resultInsert != 0)
                {
                    lblMsg.Text = "Evaluation Complete Successfullly";
                    lblMsg.Focus();
                    ClearBusInputField();
                    PanelAreaShowHide(1, 1, 1);
                }
            }
            else if (btnBusSubmit.Text == "Update")
            {
                bool resultUpdate = UpdateEvaluationFormBus(studentId, acaCalSecId);
                if (resultUpdate)
                {
                    lblMsg.Text = "Updated Evaluation";
                    lblMsg.Focus();
                    ClearLabInputField();
                    PanelAreaShowHide(1, 1, 1);
                }
            }
        }
        catch { }
    }

    #endregion
}