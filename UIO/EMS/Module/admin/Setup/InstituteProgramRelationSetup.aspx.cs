using BussinessObject;
using EMS.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCAMDAL;

public partial class InstituteProgramRelationSetup : BasePage
{
    UIUMSUser userObj = null;
    UCAMEntities ucamContext = new UCAMEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            hdnInstituteId.Value = hdnProgramId.Value = hdnFacultyId.Value = hdnDeptId.Value = "0";
            LoadInstitute();
            LoadFaculty();
            LoadDepartment();
            LoadProgram();
        }
    }

    #region INSTITUTE
    private void LoadInstitute()
    {
        var list = ucamContext.Institutions.ToList();
        ddlInstitute.Items.Clear();
        ddlInstitute.Items.Add(new ListItem("All", "0"));
        ddlprgInst.Items.Clear();
        ddlprgInst.Items.Add(new ListItem("Select Institute", "0"));

        if (list.Any())
        {
            var sorted = list.OrderBy(x => x.InstituteName).ToList();
            ddlInstitute.DataSource = sorted;
            ddlInstitute.DataTextField = "InstituteName"; ddlInstitute.DataValueField = "InstituteId"; ddlInstitute.DataBind();

            ddlprgInst.DataSource = sorted;
            ddlprgInst.DataTextField = "InstituteName"; ddlprgInst.DataValueField = "InstituteId"; ddlprgInst.DataBind();
            BindInstitueGrid(list);
        }
    }
    private void BindInstitueGrid(List<Institution> list) { gvInstitute.DataSource = list.OrderBy(x => x.InstituteName); gvInstitute.DataBind(); }
    protected void btnInstituteAdd_Click(object sender, EventArgs e) { hdnInstituteId.Value = "0"; txtInstituteName.Text = ""; ModalPopupInstituteAdd.Show(); }
    protected void btnInstituteAddNext_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdnInstituteId.Value);
        Institution inst = (id == 0) ? new Institution() : ucamContext.Institutions.Find(id);
        inst.InstituteName = txtInstituteName.Text.Trim();
        inst.InstituteCode = txtInstituteCode.Text.Trim();
        inst.InstituteAddress = txtInstituteAddress.Text.Trim();
        inst.ActiveStatus = chkIsActive.Checked ? 1 : 0;
        if (fuLogo.HasFile) inst.LogoBytes = Convert.ToBase64String(fuLogo.FileBytes);
        if (id == 0) { inst.CreatedBy = userObj.Id; inst.CreatedDate = DateTime.Now; ucamContext.Institutions.Add(inst); }
        else { inst.ModifiedBy = userObj.Id; inst.ModifiedDate = DateTime.Now; }
        ucamContext.SaveChanges();
        LoadInstitute();
        showAlert("Institute Saved.");
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        var inst = ucamContext.Institutions.Find(id);
        if (inst != null) { hdnInstituteId.Value = id.ToString(); txtInstituteName.Text = inst.InstituteName; txtInstituteCode.Text = inst.InstituteCode; chkIsActive.Checked = inst.ActiveStatus == 1; ModalPopupInstituteAdd.Show(); }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        if (ucamContext.Programs.Any(x => x.InstituteId == id)) { showAlert("Linked to programs."); return; }
        ucamContext.Institutions.Remove(ucamContext.Institutions.Find(id)); ucamContext.SaveChanges(); LoadInstitute();
    }
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlInstitute.SelectedValue);
        BindInstitueGrid(id > 0 ? ucamContext.Institutions.Where(x => x.InstituteId == id).ToList() : ucamContext.Institutions.ToList());
    }
    protected void gvInstitute_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("lblLogo");
            Label lbl = (Label)e.Row.FindControl("lblLogoByte");
            img.ImageUrl = string.IsNullOrEmpty(lbl.Text) ? "~/images/placeholder.png" : "data:image/png;base64," + lbl.Text;
        }
    }
    #endregion

    #region FACULTY
    private void LoadFaculty()
    {
        var list = ucamContext.ProgramWiseFaculties.ToList();
        ddlFaculty.Items.Clear(); ddlFaculty.Items.Add(new ListItem("All Faculty", "0"));
        ddlPrgFaculty.Items.Clear(); ddlPrgFaculty.Items.Add(new ListItem("Select Faculty", "0"));
        if (list.Any())
        {
            var sorted = list.OrderBy(x => x.FacultyName).ToList();
            ddlFaculty.DataSource = sorted; ddlFaculty.DataTextField = "FacultyName"; ddlFaculty.DataValueField = "Id"; ddlFaculty.DataBind();
            ddlPrgFaculty.DataSource = sorted; ddlPrgFaculty.DataTextField = "FacultyName"; ddlPrgFaculty.DataValueField = "Id"; ddlPrgFaculty.DataBind();
            gvFaculty.DataSource = sorted; gvFaculty.DataBind();
        }
    }
    protected void btnAddFaculty_Click(object sender, EventArgs e) { hdnFacultyId.Value = "0"; txtFacultyName.Text = ""; ModalPopupFacultyAdd.Show(); }
    protected void btnFacultySave_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdnFacultyId.Value);
        ProgramWiseFaculty f = (id == 0) ? new ProgramWiseFaculty() : ucamContext.ProgramWiseFaculties.Find(id);
        f.FacultyName = txtFacultyName.Text.Trim();
        if (id == 0) { f.CreatedBy = userObj.Id; f.CreatedDate = DateTime.Now; ucamContext.ProgramWiseFaculties.Add(f); }
        else { f.ModifiedBy = userObj.Id; f.ModifiedDate = DateTime.Now; }
        ucamContext.SaveChanges(); LoadFaculty(); showAlert("Faculty Saved.");
    }
    protected void lbEditFaculty_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        var f = ucamContext.ProgramWiseFaculties.Find(id);
        if (f != null) { hdnFacultyId.Value = id.ToString(); txtFacultyName.Text = f.FacultyName; ModalPopupFacultyAdd.Show(); }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlFaculty.SelectedValue);
        gvFaculty.DataSource = id > 0 ? ucamContext.ProgramWiseFaculties.Where(x => x.Id == id).ToList() : ucamContext.ProgramWiseFaculties.ToList(); gvFaculty.DataBind();
    }
    #endregion

    #region DEPARTMENT
    private void LoadDepartment()
    {
        var list = ucamContext.Departments.ToList();
        ddlDeptSearch.Items.Clear(); ddlDeptSearch.Items.Add(new ListItem("All Dept", "0"));
        ddlPrgDept.Items.Clear(); ddlPrgDept.Items.Add(new ListItem("Select Dept", "0"));
        if (list.Any())
        {
            var sorted = list.OrderBy(x => x.Name).ToList();
            ddlDeptSearch.DataSource = sorted; ddlDeptSearch.DataTextField = "Name"; ddlDeptSearch.DataValueField = "DeptID"; ddlDeptSearch.DataBind();
            ddlPrgDept.DataSource = sorted; ddlPrgDept.DataTextField = "Name"; ddlPrgDept.DataValueField = "DeptID"; ddlPrgDept.DataBind();
            gvDepartment.DataSource = sorted; gvDepartment.DataBind();
        }
    }
    protected void btnAddDept_Click(object sender, EventArgs e) { hdnDeptId.Value = "0"; txtDeptName.Text = ""; ModalPopupDept.Show(); }
    protected void btnDeptSave_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdnDeptId.Value);
        UCAMDAL.Department d = (id == 0) ? new UCAMDAL.Department() : ucamContext.Departments.Find(id);
        d.Code = txtDeptCode.Text; d.Name = txtDeptName.Text; d.DetailedName = txtDeptDetailed.Text;
        if (id == 0) { d.CreatedBy = userObj.Id; d.CreatedDate = DateTime.Now; ucamContext.Departments.Add(d); }
        else { d.ModifiedBy = userObj.Id; d.ModifiedDate = DateTime.Now; }
        ucamContext.SaveChanges(); LoadDepartment(); showAlert("Dept Saved.");
    }
    protected void lbEditDept_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        var d = ucamContext.Departments.Find(id);
        if (d != null) { hdnDeptId.Value = id.ToString(); txtDeptCode.Text = d.Code; txtDeptName.Text = d.Name; ModalPopupDept.Show(); }
    }
    protected void ddlDeptSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlDeptSearch.SelectedValue);
        gvDepartment.DataSource = id > 0 ? ucamContext.Departments.Where(x => x.DeptID == id).ToList() : ucamContext.Departments.ToList(); gvDepartment.DataBind();
    }
    #endregion

    #region PROGRAM
    private void LoadProgram()
    {
        var prgs = ucamContext.Programs.ToList();
        ddlProgram.Items.Clear(); ddlProgram.Items.Add(new ListItem("All Programs", "0"));
        if (prgs.Any())
        {
            ddlProgram.DataSource = prgs.OrderBy(x => x.ShortName);
            ddlProgram.DataTextField = "ShortName"; ddlProgram.DataValueField = "ProgramID"; ddlProgram.DataBind();
            BindProgramGrid(prgs);
        }
    }

    private void BindProgramGrid(List<UCAMDAL.Program> list)
    {
        var facs = ucamContext.ProgramWiseFaculties.ToList();
        var depts = ucamContext.Departments.ToList();

        foreach (var p in list)
        {

            // Using Attribute 4 and 5 for Faculty and Dept display in Grid
            var f = facs.FirstOrDefault(x => x.Id == p.FacultyId); // Ensure FacultyId exists in your DB model
            p.Attribute1 = f != null ? f.FacultyName : "N/A";

            var d = depts.FirstOrDefault(x => x.DeptID == p.DeptID); // Ensure DeptID exists in your DB model
            p.Attribute2 = d != null ? d.Name : "N/A";
        }
        gvProgram.DataSource = list.OrderBy(x => x.Code); gvProgram.DataBind();
    }

    protected void btnAddProgram_Click(object sender, EventArgs e)
    {
        hdnProgramId.Value = "0"; btnProgramAdd.Text = "Save"; ClearProgramModalValue();
        LoadProgramType(); LoadCalenderType(); ModalPopupProgramAdd.Show();
    }

    private void LoadProgramType()
    {

        try
        {
            ddlProgramType.DataSource = ucamContext.ProgramTypes.ToList(); ddlProgramType.DataTextField = "TypeDescription"; ddlProgramType.DataValueField = "ProgramTypeID"; ddlProgramType.DataBind(); ddlProgramType.Items.Insert(0, new ListItem("Select Type", "0"));

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadCalenderType()
    {
        try
        {
            ddlCalender.DataSource = ucamContext.CalenderUnitMasters.ToList(); ddlCalender.DataTextField = "Name"; ddlCalender.DataValueField = "CalenderUnitMasterID"; ddlCalender.DataBind(); ddlCalender.Items.Insert(0, new ListItem("Select Calendar", "0"));

        }
        catch (Exception ex)
        {
        }
    }

    private void ClearProgramModalValue()
    {
        try
        {

            ddlprgInst.SelectedValue = ddlProgramType.SelectedValue = ddlPrgFaculty.SelectedValue = ddlPrgDept.SelectedValue = ddlCalender.SelectedValue = "0";
            TextBoxProgramName.Text = TextBoxDetailedName.Text = TextBoxCode.Text = TextBoxShortName.Text = TextBoxTotalCredit.Text = TextBoxDuration.Text = "";

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnProgramAdd_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdnProgramId.Value);
        UCAMDAL.Program p = (id == 0) ? new UCAMDAL.Program() : ucamContext.Programs.Find(id);

        p.InstituteId = Convert.ToInt32(ddlprgInst.SelectedValue);
        p.ProgramTypeID = Convert.ToInt32(ddlProgramType.SelectedValue);
        p.FacultyId = Convert.ToInt32(ddlPrgFaculty.SelectedValue); // Ensure this field exists in your DB entity
        p.DeptID = Convert.ToInt32(ddlPrgDept.SelectedValue);      // Ensure this field exists in your DB entity
        p.CalenderUnitMasterID = Convert.ToInt32(ddlCalender.SelectedValue);

        p.DegreeName = TextBoxProgramName.Text.Trim();
        p.DetailName = TextBoxDetailedName.Text.Trim();
        p.Code = TextBoxCode.Text.Trim();
        p.ShortName = TextBoxShortName.Text.Trim();
        p.TotalCredit = decimal.Parse(string.IsNullOrEmpty(TextBoxTotalCredit.Text) ? "0" : TextBoxTotalCredit.Text);
        p.Duration = int.Parse(string.IsNullOrEmpty(TextBoxDuration.Text) ? "0" : TextBoxDuration.Text);

        if (id == 0) { p.CreatedBy = userObj.Id; p.CreatedDate = DateTime.Now; ucamContext.Programs.Add(p); }
        else { p.ModifiedBy = userObj.Id; p.ModifiedDate = DateTime.Now; }

        ucamContext.SaveChanges(); LoadProgram(); showAlert("Program Saved.");
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        var p = ucamContext.Programs.Find(id);
        if (p != null)
        {
            hdnProgramId.Value = id.ToString(); btnProgramAdd.Text = "Update";
            LoadProgramType(); LoadCalenderType();

            TextBoxProgramName.Text = p.DegreeName; TextBoxDetailedName.Text = p.DetailName;
            TextBoxCode.Text = p.Code; TextBoxShortName.Text = p.ShortName;
            TextBoxTotalCredit.Text = p.TotalCredit.ToString(); TextBoxDuration.Text = p.Duration.ToString();

            ddlprgInst.SelectedValue = p.InstituteId.ToString();
            ddlProgramType.SelectedValue = p.ProgramTypeID.ToString();
            ddlCalender.SelectedValue = p.CalenderUnitMasterID.ToString();

            // Set Faculty and Dept if they exist in DB
            if (p.FacultyId.HasValue) ddlPrgFaculty.SelectedValue = p.FacultyId.ToString();
            if (p.DeptID != null) ddlPrgDept.SelectedValue = p.DeptID.ToString();

            ModalPopupProgramAdd.Show();
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        ucamContext.Programs.Remove(ucamContext.Programs.Find(id)); ucamContext.SaveChanges(); LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlProgram.SelectedValue);
        BindProgramGrid(id > 0 ? ucamContext.Programs.Where(x => x.ProgramID == id).ToList() : ucamContext.Programs.ToList());
    }
    #endregion

    protected void showAlert(string msg) { ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", "swal('" + msg + "');", true); }
}