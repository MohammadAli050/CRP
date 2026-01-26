using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Common;
public partial class Admin_PreRegistration : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {
            FillProgramListCombo();
            FillBatchListCombo();
        }
    }

    private void FillProgramListCombo()
    {
        try
        {
            programListCombo.Items.Clear();
            programListCombo.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            programListCombo.AppendDataBoundItems = true;

            if (programList != null)
            {
                programListCombo.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                programListCombo.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillBatchListCombo()
    {
        try
        {
            batchListCombo.Items.Clear();
            batchListCombo.Items.Add(new ListItem("Select", "0"));
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            //ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            batchListCombo.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                batchListCombo.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                batchListCombo.DataBind();
                
                //int count = academicCalenderList.Count;
                //foreach (AcademicCalender academicCalender in academicCalenderList)
                //{
                //    batchListCombo.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                //    count = academicCalender.AcademicCalenderID;
                //}
                //batchListCombo.SelectedValue = count.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void loadButton_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(programListCombo.SelectedValue);
        int batchId = Convert.ToInt32(batchListCombo.SelectedValue);
        string ID = searchBox.Text;
        List<RegistrationWorksheet> regWork = RegistrationWorksheetManager.GetPreRegByProgCal(Convert.ToInt32(programListCombo.SelectedValue), Convert.ToInt32(batchListCombo.SelectedValue));

        ResultView.DataSource = regWork;
        ResultView.DataBind();


        if (ID != "")
        {
            //regWork = RegistrationWorksheetManager.GetPreRegByProgCal(Convert.ToInt32(programListCombo.SelectedValue), Convert.ToInt32(batchListCombo.SelectedValue));
            List<RegistrationWorksheet> findByID = regWork.Where(x => x.Roll == ID).ToList();

            ResultView.DataSource = findByID;
            ResultView.DataBind();
        }
    }
   
}