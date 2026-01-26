using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ProgramUserControl : System.Web.UI.UserControl
{
    public event EventHandler ProgramSelectedIndexChanged;
    public string selectedValue = string.Empty;
    public string selectedText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //LoadDropDownList();               
            }
            selectedValue = ddlProgram.SelectedValue;
            selectedText = ddlProgram.SelectedItem.Text;
        }
        catch (Exception)
        {
        }
    }

    public void LoadDropDownList()
    {
        List<Program> programList = new List<Program>();
        programList = ProgramManager.GetAll();

        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        ddlProgram.Items.Add(new ListItem("-Select-", "0"));

        if (programList != null)
        {
            ddlProgram.DataTextField = "ShortName";
            ddlProgram.DataValueField = "ProgramID";

            ddlProgram.DataSource = programList;
            ddlProgram.DataBind();
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlProgram.SelectedValue;
        selectedText = ddlProgram.SelectedItem.Text;

        if (ProgramSelectedIndexChanged != null)
            ProgramSelectedIndexChanged(this, e);
    }

    internal void SelectedValue(int id)
    {
        ddlProgram.SelectedValue = id.ToString();
    }

    internal void IndexO()
    {
        ddlProgram.SelectedIndex = 0;
    }

    public void LoadDropdownWithUserAccess(int userID)
    {
        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        ddlProgram.Items.Add(new ListItem("-Select-", "0"));
        try
        {
            UserAccessProgram uapObj = UserAccessProgramManager.GetByUserId(userID);
            if (uapObj != null && !string.IsNullOrEmpty(uapObj.AccessPattern))
            {
                List<Program> programList = new List<Program>();
                string[] accessCode = uapObj.AccessPattern.Split('-');
                foreach (string s in accessCode)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        Program program = ProgramManager.GetById(Convert.ToInt32(s));
                        programList.Add(program);
                    }
                }
                if (programList != null)
                {
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";

                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }
            }
            else
            {
                LoadDropDownList();
            }
        }
        catch { LoadDropDownList(); }

    }


    public void LoadDropdownWithUserAccessAndInstitute(int userID, int InstituteId)
    {
        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        try
        {
            UserAccessProgram uapObj = UserAccessProgramManager.GetByUserId(userID);
            if (uapObj != null && !string.IsNullOrEmpty(uapObj.AccessPattern))
            {
                List<Program> programList = new List<Program>();
                string[] accessCode = uapObj.AccessPattern.Split('-');
                foreach (string s in accessCode)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        Program program = ProgramManager.GetById(Convert.ToInt32(s));
                        if (program != null && program.InstituteId == InstituteId)
                            programList.Add(program);
                    }
                }
                if (programList != null)
                {
                    ddlProgram.Items.Add(new ListItem("-Select-", "0"));
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";

                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }
            }
            else
            {
                LoadDropDownListByInstituteId(InstituteId);
            }
        }
        catch { LoadDropDownListByInstituteId(InstituteId); }


    }
    public void LoadDropdownWithUserAccessByCalenderMaster(int userID, int calenderMasterId)
    {
        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        ddlProgram.Items.Add(new ListItem("-Select-", "0"));
        try
        {
            UserAccessProgram uapObj = UserAccessProgramManager.GetByUserId(userID);
            if (uapObj != null && !string.IsNullOrEmpty(uapObj.AccessPattern))
            {
                List<Program> programList = new List<Program>();
                string[] accessCode = uapObj.AccessPattern.Split('-');
                foreach (string s in accessCode)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        Program program = ProgramManager.GetById(Convert.ToInt32(s));
                        if (program != null && program.CalenderUnitMasterID == calenderMasterId)
                            programList.Add(program);
                    }
                }
                if (programList != null)
                {
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";

                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }
            }
            else
            {
                LoadDropDownListByUnitMasterId(calenderMasterId);
            }
        }
        catch (Exception ex) { LoadDropDownListByUnitMasterId(calenderMasterId); }

    }

    public void LoadDropDownListByUnitMasterId(int unitMasterId)
    {
        List<Program> programList = new List<Program>();
        programList = ProgramManager.GetAll().Where(x => x.CalenderUnitMasterID == unitMasterId).ToList();

        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        ddlProgram.Items.Add(new ListItem("-Select-", "0"));

        if (programList != null)
        {
            ddlProgram.DataTextField = "ShortName";
            ddlProgram.DataValueField = "ProgramID";

            ddlProgram.DataSource = programList;
            ddlProgram.DataBind();
        }
    }

    public void LoadDropDownListByInstituteId(int instituteId)
    {
        List<Program> programList = new List<Program>();
        programList = ProgramManager.GetAll().Where(x => x.InstituteId == instituteId).ToList();

        ddlProgram.Items.Clear();
        ddlProgram.AppendDataBoundItems = true;
        ddlProgram.Items.Add(new ListItem("-Select-", "0"));

        if (programList != null)
        {
            ddlProgram.DataTextField = "ShortName";
            ddlProgram.DataValueField = "ProgramID";

            ddlProgram.DataSource = programList;
            ddlProgram.DataBind();
        }
    }
    public void DropDownDisable()
    {
        ddlProgram.Enabled = false;
    }

    public void DropDownEnable()
    {
        ddlProgram.Enabled = true;
    }
}