using BussinessObject;
using EMS.Module;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCAMDAL;

public partial class Report_RptAdmitCard : BasePage
{

    int userId = 0;

    BussinessObject.UIUMSUser UserObj = null;
    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadInstitute(UserObj);
                LoadProgram(UserObj, 0);
                ucSession.LoadDropDownList(0);
            }
        }
        catch (Exception Ex)
        {
        }
    }

    #region On Load Methods


    private void LoadInstitute(UIUMSUser userObj)
    {
        try
        {
            var InstituteList = new List<UCAMDAL.Institution>();
            var InstList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();
            ddlInstitute.Items.Clear();
            ddlInstitute.AppendDataBoundItems = true;
            ddlInstitute.Items.Add(new ListItem("Select", "0"));

            InstituteList = MisscellaneousCommonMethods.GetInstitutionsByUserId(userObj.Id);

            if (InstituteList != null && InstituteList.Any())
            {
                ddlInstitute.DataTextField = "InstituteName";
                ddlInstitute.DataValueField = "InstituteId";
                ddlInstitute.DataSource = InstituteList.OrderBy(x => x.InstituteName);
                ddlInstitute.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadProgram(UIUMSUser userObj, int InstituteId)
    {
        try
        {
            var InstituteList = new List<UCAMDAL.Institution>();
            var ProgramList = new List<UCAMDAL.Program>();

            ddlProgram.Items.Clear();
            ddlProgram.AppendDataBoundItems = true;
            ddlProgram.Items.Add(new ListItem("Select", "0"));

            ProgramList = MisscellaneousCommonMethods.GetProgramByUserIdAndInstituteId(userObj.Id, InstituteId);

            if (ProgramList != null && ProgramList.Any())
            {
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataSource = ProgramList.OrderBy(x => x.ShortName);
                ddlProgram.DataBind();
            }


        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    #region On Change Methods

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram(UserObj, Convert.ToInt32(ddlInstitute.SelectedValue));
        ucSession.LoadDropDownList(0);
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearReport();
        ucBatch.LoadDropDownListWithAll(Convert.ToInt32(ddlProgram.SelectedValue));
        int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

        ucSession.LoadDropDownList(ProgramId);

        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        LoadRunningStudentList(ProgramId, acaCalId, batchId);
    }
    protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearReport();
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        LoadRunningStudentList(programId, acaCalId, batchId);
    }
    protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearReport();
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        LoadRunningStudentList(programId, acaCalId, batchId);
    }

    private void ClearReport()
    {
        AdmitCard.LocalReport.DataSources.Clear();
    }

    #endregion



    private void LoadRunningStudentList(int programId, int acaCalId, int batchId)
    {
        List<StudentRollOnly> stdList = new List<StudentRollOnly>();
        int BatchId = Convert.ToInt32(ucBatch.selectedValue);

        if (BatchId > 0)
            stdList = StudentManager.GetStudentListRollByProgramBatchSession(acaCalId, programId, batchId);
        else
        {
            var BatchList = BatchManager.GetAllByProgram(programId);
            if (BatchList != null && BatchList.Any())
            {
                foreach(var item in BatchList)
                {
                    var studentList= StudentManager.GetStudentListRollByProgramBatchSession(acaCalId, programId, item.BatchId);
                    if (studentList != null && studentList.Any())
                    {
                        stdList.AddRange(studentList);
                    }
                }
            }
        }
        LoadStudentCheckBoxList(stdList);
    }

    protected void ddlRunningStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    //View Report On Webpage
    protected void buttonView_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        string roll = "";//Convert.ToString(ddlStudent.SelectedValue);

        LoadAdmitCard(programId, acaCalId, batchId, roll);
    }

    private void LoadAdmitCard(int programId, int acaCalId, int batchId, string roll)
    {
        try
        {
            string semester = "";
            //DateTime examStartDate = DateTime.ParseExact(txtExamDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
            //string examDate = examStartDate.ToString("dd-MMM-yy"); 

            int institutionId = Convert.ToInt32(ddlInstitute.SelectedValue);

            List<RptAdmitCard> list2 = ReportManager.GetAdmitCard(programId, acaCalId, batchId, roll, institutionId, 0);
            List<RptAdmitCard> list = new List<RptAdmitCard>();
            if (ddlStudentList.SelectedItem.Text == "All")
            {
                foreach (ListItem item in ddlStudentList.Items)
                {
                    item.Selected = true;
                }
                ddlStudentList.Items[0].Selected = false;
            }

            foreach (ListItem item in ddlStudentList.Items)
            {
                if (item.Selected)
                {
                    list.AddRange(list2.Where(l => l.Roll == item.Value).ToList());
                }
            }

            if (list.Count != 0)
            {
                List<ReportParameter> param1 = new List<ReportParameter>();
                param1.Add(new ReportParameter("ExamControllerName", ""));
                param1.Add(new ReportParameter("Semester", semester));
                param1.Add(new ReportParameter("AcademicSession", ucSession.selectedText));
                param1.Add(new ReportParameter("Program", ddlProgram.SelectedItem.ToString()));


                AdmitCard.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptAdmitCardNew.rdlc");
                AdmitCard.LocalReport.EnableExternalImages = true;
                this.AdmitCard.LocalReport.SetParameters(param1);

                ReportDataSource rds = new ReportDataSource("AdmitCard", list);

                AdmitCard.LocalReport.DataSources.Clear();
                AdmitCard.LocalReport.DataSources.Add(rds);
            }
            else
            {
                ShowMessage("NO Data Found. Enter Valid Program And Batch");
                return;
            }
        }
        catch (Exception)
        { }
    }

    private void ShowMessage(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }

    private void LoadStudentCheckBoxList(List<StudentRollOnly> list)
    {
        try
        {
            ddlStudentList.Items.Clear();
            ddlStudentList.AppendDataBoundItems = true;
            if (list != null)
            {
                ddlStudentList.Items.Add(new ListItem("All", ""));
                ddlStudentList.DataTextField = "Roll";
                ddlStudentList.DataValueField = "Roll";
                ddlStudentList.DataSource = list;
                ddlStudentList.DataBind();
            }
            else
            {
                ddlStudentList.DataSource = null;
                ddlStudentList.DataBind();
            }
        }
        catch (Exception)
        { }
    }


}