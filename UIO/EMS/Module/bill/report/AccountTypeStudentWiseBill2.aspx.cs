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

public partial class AccountTypeStudentWiseBill2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {

            LoadComboBox();
            DateTime fromDate = DateTime.Now;
            string strFromDate = fromDate.ToString("dd/MM/yyyy");
            txtFromDate.Text = strFromDate;
            DateTime toDate = DateTime.Now;
            string strToDate = toDate.ToString("dd/MM/yyyy");
            txtToDate.Text = strToDate;
        }
    }

    private void LoadComboBox()
    {
        FillTypeDefinitionComboBox();
        FillProgramComboBox();
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        FillBatchComboBox(programId);
        FillSessionComboBox(programId);
    }

    public void FillBatchComboBox(int programId)
    {

        Program program = ProgramManager.GetById(programId);

        List<Batch> batchList = new List<Batch>();
        if (program != null)
            batchList = BatchManager.GetAllByProgram(program.ProgramID);

        ddlBatch.Items.Clear();
        ddlBatch.AppendDataBoundItems = true;

        if (batchList != null)
        {
            // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

            ddlBatch.Items.Add(new ListItem("Select All", "0"));
            ddlBatch.DataTextField = "BatchNO";
            ddlBatch.DataValueField = "BatchId";

            ddlBatch.DataSource = batchList;
            ddlBatch.DataBind();
        }
    }

    public void FillSessionComboBox(int programId)
    {

        Program program = ProgramManager.GetById(programId);

        List<AcademicCalender> sessionList = new List<AcademicCalender>();
        if (program != null)
            sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);

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
    }

    private void FillProgramComboBox()
    {
        try
        {
            List<Program> programList = ProgramManager.GetAll();
            ddlProgram.Items.Clear();

            ddlProgram.Items.Add(new ListItem("Select All", "0"));
            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(p => p.ShortName).ToList();
                ddlProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        FillBatchComboBox(programId);
        FillSessionComboBox(programId);
    }

    private void FillTypeDefinitionComboBox()
    {
        try
        {
            List<TypeDefinition> typeDefinationList = TypeDefinitionManager.GetAll();

            ddlTypeDefinition.Items.Add(new ListItem("Select All", "0"));
            ddlTypeDefinition.AppendDataBoundItems = true;

            if (typeDefinationList != null)
            {
                ddlTypeDefinition.DataSource = typeDefinationList.Where(t => t.Type == "Discount").ToList();
                ddlTypeDefinition.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void GetBillHistory_Click(Object sender, EventArgs e)
    {
        int typeDefinationId = Convert.ToInt32(ddlTypeDefinition.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        int batchId = Convert.ToInt32(ddlBatch.SelectedValue);
        int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

        DateTime fromDate = DateTime.ParseExact(txtFromDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
        DateTime toDate = DateTime.ParseExact(txtToDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

        LoadBillHistory(typeDefinationId, programId, batchId, acaCalId, fromDate, toDate);
    }

    protected void LoadBillHistory(int typeDefinationId,int programId,int batchId, int acaCalId,DateTime fromDate, DateTime toDate)
    {

        try
        {
            string definition = ddlTypeDefinition.SelectedItem.Text.Trim().ToString();
            if (definition.Equals("Select All"))
            {
                definition = "All";
            }
            string programName = ddlProgram.SelectedItem.Text.Trim().ToString();
            if (programName.Equals("Select All"))
            {
                programName = "All";
            }
            string session = ddlSession.SelectedItem.Text.Trim().ToString();
            if (session.Equals("Select All"))
            {
                session = "All";
            }
            string postingFromDate = fromDate.ToString("dd/MM/yyyy");
            string postingToDate = toDate.ToString("dd/MM/yyyy");
            string batch = Convert.ToInt32(ddlBatch.SelectedItem.Value) == 0 ? "All" : ddlBatch.SelectedItem.Text;
            if (typeDefinationId == 0)
            {
                definition = "All";
            }

            List<rBillHistoryFeeTypeWise> list = null; //BillHistoryManager.GetStudentBillHistoryDiscountFeeTypeWise(typeDefinationId, programId, batchId, acaCalId, fromDate, toDate);

            if (list != null && list.Count > 0)
            {
                ReportParameter p1 = new ReportParameter("ProgramName", programName);
                ReportParameter p2 = new ReportParameter("Session", session);
                ReportParameter p3 = new ReportParameter("PostingFromDate", postingFromDate);
                ReportParameter p4 = new ReportParameter("PostingToDate", postingToDate);
                ReportParameter p5 = new ReportParameter("Definition", definition);
                ReportParameter p6 = new ReportParameter("Batch", batch);
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5,p6});
                ReportDataSource rds = new ReportDataSource("AccountTypeAndStudentWiseBillDataSet", list);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = null;
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = "No Data Found !";
        }
    }
}
