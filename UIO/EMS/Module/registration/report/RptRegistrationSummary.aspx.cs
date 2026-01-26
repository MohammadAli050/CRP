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

public partial class RptRegistrationSummary : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            LoadAffiliatedInstitution();
            LoadExamCenter();
        }

    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        RegistrationSummary.Visible = false;
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        //ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        RegistrationSummary.Visible = false;
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        RegistrationSummary.Visible = false;
        LoadRunningStudentList(programId, acaCalId, batchId);
    }

    private void LoadRunningStudentList(int programId, int acaCalId, int batchId)
    {
        List<RunningStudent> list = StudentManager.GetRunningStudentByProgramIdAcaCalIdNew(programId, acaCalId, batchId);

        ddlStudent.Items.Clear();
        ddlStudent.AppendDataBoundItems = true;

        if (list != null)
        {
            ddlStudent.Items.Add(new ListItem("All", ""));
            ddlStudent.DataTextField = "Roll";
            ddlStudent.DataValueField = "Roll";


            ddlStudent.DataSource = list;
            ddlStudent.DataBind();
        }
    }

    protected void ddlRunningStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    //View Report On Webpage
    protected void buttonView_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        string roll = Convert.ToString(ddlStudent.SelectedValue);

        LoadAdmitCard(programId, acaCalId, batchId, roll);
    }

    private void LoadAdmitCard(int programId, int acaCalId, int batchId, string roll)
    {
        int institutionId = Convert.ToInt32(ddlInstitution.SelectedValue);
        int examCenterId = Convert.ToInt32(ddlExamCenter.SelectedValue);

        List<RptAdmitCard> list = ReportManager.GetAdmitCard(programId, acaCalId, batchId, roll, institutionId, examCenterId);

        if (list.Count != 0)
        {
            RegistrationSummary.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptRegistrationSummary.rdlc");
            RegistrationSummary.LocalReport.EnableExternalImages = true;
            ReportDataSource rds = new ReportDataSource("AdmitCard", list);

            RegistrationSummary.LocalReport.DataSources.Clear();
            RegistrationSummary.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";
            RegistrationSummary.Visible = true;
        }
        else
        {
            RegistrationSummary.Visible = false;
            ShowMessage("NO Data Found. Enter Valid Program And Batch"); 
        }
    }

    private void LoadExamCenter()
    {
        try
        {
            List<ExamCenter> list = ExamCenterManager.GetAll();

            ddlExamCenter.Items.Clear();
            ddlExamCenter.AppendDataBoundItems = true;

            if (list != null)
            {
                ddlExamCenter.Items.Add(new ListItem("-Select-", "0"));
                ddlExamCenter.DataTextField = "ExamCenterName";
                ddlExamCenter.DataValueField = "Id";

                ddlExamCenter.DataSource = list;
                ddlExamCenter.DataBind();
            }
        }
        catch { }
        finally { }
    }

    private void LoadAffiliatedInstitution()
    {
        try
        {
            List<AffiliatedInstitution> list = AffiliatedInstitutionManager.GetAll();

            ddlInstitution.Items.Clear();
            ddlInstitution.AppendDataBoundItems = true;

            if (list != null)
            {
                ddlInstitution.Items.Add(new ListItem("-Select-", "0"));
                ddlInstitution.DataTextField = "Name";
                ddlInstitution.DataValueField = "Id";

                ddlInstitution.DataSource = list;
                ddlInstitution.DataBind();
            }
        }
        catch { }
        finally { }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;
    }

}