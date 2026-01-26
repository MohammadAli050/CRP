using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptProbationStudentList : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;

            //UpdatePanel3.Visible = false;
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadDropDown();
                ReportViewer1.LocalReport.Refresh();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadDropDown()
    {
        LoadCalenderType();
        FillProgramCombo();
        LoadOrderByCombo();
    }

    private void LoadOrderByCombo()
    {
        ddlOrderBy.Items.Clear();
        ddlOrderBy.Items.Add(new ListItem("Probation[Ascending]", "0"));
        ddlOrderBy.Items.Add(new ListItem("Probation[Descending]", "1"));
        ddlOrderBy.Items.Add(new ListItem("Roll[Ascending]", "2"));
        ddlOrderBy.Items.Add(new ListItem("Roll[Descending]", "3"));
    }


    protected void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();
            //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
            //ddlCalenderType.AppendDataBoundItems = true;

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
            LoadAcademicCalender(calenderTypeId);
        }
    }

    protected void LoadAcademicCalender(int calenderTypeId)
    {
        try
        {
            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalSession.AppendDataBoundItems = true;

            ddlAcaCalSession2.Items.Clear();
            ddlAcaCalSession2.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalSession2.AppendDataBoundItems = true;

            ddlSemesterFrom.Items.Clear();
            ddlSemesterFrom.Items.Add(new ListItem("Select", "0"));
            ddlSemesterFrom.AppendDataBoundItems = true;

            ddlSemesterTo.Items.Clear();
            ddlSemesterTo.Items.Add(new ListItem("Select", "0"));
            ddlSemesterTo.AppendDataBoundItems = true;

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

            if (academicCalenderList.Count > 0 && academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalSession.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlAcaCalSession2.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlSemesterFrom.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlSemesterTo.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                }
                academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
                ddlAcaCalSession.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                ddlAcaCalSession2.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                ddlSemesterFrom.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
                ddlSemesterTo.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();

            }
        }
        catch { }
    }

    private void LoadData(int programId, string orderType)
    {
        try
        {
            //UpdatePanel3.Visible = true;

            List<rProbationStudent> probationList = ProbationManager.GetAllByProgramOrder(programId, orderType);

            if (probationList != null && probationList.Count > 0)
            {
                SessionManager.SaveListToSession<rProbationStudent>(probationList, "rpt_probationList");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ProbationList", probationList);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ProbationList", probationList);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
        }
        catch { }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlOrderBy.Items.Clear();
            ddlOrderBy.Items.Add(new ListItem("Probation[Ascending]", "0"));
            ddlOrderBy.Items.Add(new ListItem("Probation[Descending]", "1"));
            ddlOrderBy.Items.Add(new ListItem("Roll[Ascending]", "2"));
            ddlOrderBy.Items.Add(new ListItem("Roll[Descending]", "3"));

            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalSession2.Items.Clear();
            ddlAcaCalSession2.Items.Add(new ListItem("Select", "0"));

            ddlSemesterFrom.Items.Clear();
            ddlSemesterFrom.Items.Add(new ListItem("Select", "0"));

            ddlSemesterTo.Items.Clear();
            ddlSemesterTo.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            List<AcademicCalender> acaCalList01 = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
            List<AcademicCalender> acaCalList02 = academicCalenderList.Where(x => acaCalList01[0].AcademicCalenderID > x.AcademicCalenderID).ToList();

            ddlAcaCalSession.AppendDataBoundItems = true;
            ddlAcaCalSession2.AppendDataBoundItems = true;
            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalSession.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlAcaCalSession2.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                    ddlSemesterFrom.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlSemesterTo.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }

                ddlAcaCalSession.SelectedValue = acaCalList01[0].AcademicCalenderID.ToString();
                ddlAcaCalSession2.SelectedValue = acaCalList01[0].AcademicCalenderID.ToString();

                ddlSemesterFrom.SelectedValue = acaCalList02[0].AcademicCalenderID.ToString();
                ddlSemesterTo.SelectedValue = acaCalList02[0].AcademicCalenderID.ToString();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgramOrder.Items.Clear();
            //ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();

                ddlProgramOrder.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgramOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    #endregion

    #region Event

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        catch { }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgramOrder.SelectedItem.Value);
        string orderType = ddlOrderBy.SelectedItem.Text;

        LoadData(programId, orderType);
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        int exceptionFlag = 0;
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int FromAcaCalId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);
            int ToAcaCalId = Convert.ToInt32(ddlAcaCalSession2.SelectedItem.Value);
            decimal FromRange = Convert.ToDecimal(From.Text.ToString());
            decimal ToRange = Convert.ToDecimal(To.Text.ToString());
            int FromSemester = Convert.ToInt32(ddlSemesterFrom.SelectedValue);
            int ToSemester = Convert.ToInt32(ddlSemesterTo.SelectedValue);

            List<rProbationStudent> probationStudentList = ProbationManager.GetAll(FromAcaCalId, ToAcaCalId, FromRange, ToRange, programId, FromSemester, ToSemester);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            if (probationStudentList != null && probationStudentList.Count > 0)
            {
                exceptionFlag = 1;
                if(ddlProgramOrder.SelectedValue == "0")        probationStudentList = probationStudentList.OrderBy(x => x.ProbationCount).ToList();
                else if(ddlProgramOrder.SelectedValue == "1")   probationStudentList = probationStudentList.OrderByDescending(x => x.ProbationCount).ToList();
                else if(ddlProgramOrder.SelectedValue == "1")   probationStudentList = probationStudentList.OrderBy(x => x.Roll).ToList();
                else    probationStudentList = probationStudentList.OrderByDescending(x => x.Roll).ToList();

                SessionManager.SaveListToSession<rProbationStudent>(probationStudentList, "rpt_probationList");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ProbationList", probationStudentList);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }  
        }
        catch { }
        finally
        {
            ddlProgramOrder.SelectedValue = ddlProgram.SelectedValue;
            if (exceptionFlag == 0)
                btnView_Click(null, null);
        }
    }

    //protected void btnBlockStudent_Click(object sender, EventArgs e)
    //{
    //    List<rProbationStudent> probationList = SessionManager.GetListFromSession<rProbationStudent>("rpt_probationList");


    //    if (probationList != null && probationList.Count > 0)
    //    {
    //        foreach (rProbationStudent item in probationList)
    //        {
    //            if (item.ProbationCount >= Convert.ToInt32(txtProbationNo.Text))
    //            {
    //                PersonBlock personBlock = new PersonBlock();

    //                personBlock.PersonId = item.PersonId;
    //                personBlock.StartDateAndTime = DateTime.Now;
    //                personBlock.EndDateAndTime = DateTime.Now;
    //                personBlock.Remarks = "Blocked for Probation And Probation No is " + item.ProbationCount.ToString();
    //                personBlock.CreatedBy = 1;
    //                personBlock.CreatedDate = DateTime.Now;
    //                personBlock.ModifiedBy = 1;
    //                personBlock.ModifiedDate = DateTime.Now;

    //                int i = PersonBlockManager.Insert(personBlock);
    //            }
    //        }
    //    }
    //}

    #endregion    
}