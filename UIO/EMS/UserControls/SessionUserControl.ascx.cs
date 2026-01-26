using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_SessionUserControl : System.Web.UI.UserControl
{
    public event EventHandler SessionSelectedIndexChanged;
    public string selectedValue = string.Empty;
    public string selectedText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDropDownList();
            }

            selectedValue = ddlSession.SelectedValue;
            selectedText = ddlSession.SelectedItem.Text;
        }
        catch (Exception)
        {
        }
    }

    internal void SelectedSessionByProgram(int programId, int sessionId)
    {
        LoadDropDownList(programId);
        ddlSession.SelectedValue = sessionId.ToString();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlSession.SelectedValue;
        selectedText = ddlSession.SelectedItem.Text;

        if (SessionSelectedIndexChanged != null)
            SessionSelectedIndexChanged(this, e);
    }

    public void LoadDropDownList()  // changed to public by HS, for use in BillManualEntry
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("-Select-", "0"));
    }

    public void LoadDropDownList(int Id)
    {
        Program program = ProgramManager.GetById(Id);

        List<AcademicCalender> sessionList = new List<AcademicCalender>();
        if (program != null)
            sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);

        ddlSession.Items.Clear();
        ddlSession.AppendDataBoundItems = true;
        ddlSession.Items.Add(new ListItem("-Select-", "0"));

        if (sessionList != null)
        {
            ddlSession.DataTextField = "FullCode";
            ddlSession.DataValueField = "AcademicCalenderID";

            ddlSession.DataSource = sessionList.OrderByDescending(x=>x.AcademicCalenderID);
            ddlSession.DataBind();
        }
    }



    public void LoadDropDownListWithAllOption(int Id)
    {
        Program program = ProgramManager.GetById(Id);

        List<AcademicCalender> sessionList = new List<AcademicCalender>();
        if (program != null)
            sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);

        ddlSession.Items.Clear();
        ddlSession.AppendDataBoundItems = true;
        ddlSession.Items.Add(new ListItem("All", "0"));

        if (sessionList != null)
        {
            ddlSession.DataTextField = "FullCode";
            ddlSession.DataValueField = "AcademicCalenderID";

            ddlSession.DataSource = sessionList.OrderByDescending(x => x.AcademicCalenderID);
            ddlSession.DataBind();
        }
    }


    public void LoadDropDownListByProgramBatch(int programId, int batchId)
    {

        List<AcademicCalender> sessionList = AcademicCalenderManager.AcaCalSessionByProgramIdBatchId(programId, batchId);

        ddlSession.Items.Clear();
        ddlSession.AppendDataBoundItems = true;

        if (sessionList != null)
        {
            // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

            ddlSession.Items.Add(new ListItem("-Select-", "0"));
            ddlSession.DataTextField = "FullCode";
            ddlSession.DataValueField = "AcademicCalenderID";

            ddlSession.DataSource = sessionList;
            ddlSession.DataBind();
        }
    }


    internal void SelectedValue(int id)
    {
        ddlSession.SelectedValue = id.ToString();
    }

    internal void IndexO()
    {
        ddlSession.SelectedIndex = 0;
    }

}