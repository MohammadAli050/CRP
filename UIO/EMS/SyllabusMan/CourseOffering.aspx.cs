using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using BussinessObject;
using Common;
using System.Data;
using System.Data.SqlClient;

public partial class SyllabusMan_CourseOffering : BasePage
{
    private const string SESSION_OFFEREDCOURSES = "OFFEREDCOURSES";
    private const string SESSION_SELECTEDCOURSES = "SELECTEDCOURSES";

    private List<AcademicCalender> _trimesterInfos = null;
    private List<TreeMaster> _treeMas = null;
    private string[] _dataKey = new string[1] { "Id" };
    private int intSelectedCourse = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblTotalCourses.Text = "";
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (CurrentUser != null)
            {
                if (CurrentUser.RoleID > 0)
                {
                    AuthenticateHome(CurrentUser);
                }
            }
            else
            {
                Response.Redirect("~/Security/Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                FillAcademicCalenderCombo(); 
                btnSave.Attributes.Add("onclick", "return confirm('Do you want to save the selected course?');");
                lblMsg.Text = "";
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    private void FillAcademicCalenderCombo()
    {
        try
        {
            cboAcaCalender.Items.Clear();

            ListEditItem currentTrimester = new ListEditItem();
            _trimesterInfos = AcademicCalender.Gets();
            if (_trimesterInfos == null)
            {
                return;
            }
            foreach (AcademicCalender ac in _trimesterInfos)
            {
                if (ac.IsCurrent)
                {
                    currentTrimester.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                }
                ListEditItem lei = new ListEditItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                cboAcaCalender.Items.Add(lei);
            }
            //cboAcaCalender.SelectedIndex = cboAcaCalender.Items.Count - 1;
            cboAcaCalender.SelectedIndex = cboAcaCalender.Items.FindByText(currentTrimester.Text).Index;
            cboAcaCalender_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    protected void cboAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDeptCombo();
            cboDept_SelectedIndexChanged(null, null);
            lblMsg.Text = string.Empty;
            gvwCollection.DataSource = null;
            gvwCollection.DataBind(); 
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillDeptCombo()
    {
        try
        {
            cboDept.Items.Clear();
            List<Department> _depts = Department.GetDepts();
            if (_depts == null)
            {
                return;
            }
            cboDept.Items.Add("All", 0);
            foreach (Department dept in _depts)
            {
                ListEditItem item = new ListEditItem();
                item.Value = dept.Id.ToString();
                item.Text = dept.DetailedName;
                cboDept.Items.Add(item);
            }
            cboDept.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        gvwCollection.DataSource = null;
        gvwCollection.DataBind();
            
        if (cboDept.SelectedItem.Value.ToString() == "0")
        {
            FillProgramCombo(string.Empty);
            cboProgram.ReadOnly = true;
        }
        else
        {
            FillProgramCombo(cboDept.SelectedItem.Value.ToString());
            cboProgram.ReadOnly = false;
        }
        cboProgram_SelectedIndexChanged(null, null);
        //LoadCourseOffering();
    }
    private void FillProgramCombo(string str)
    {
        try
        {
            cboProgram.Items.Clear();
            lblMsg.Text = string.Empty;
            gvwCollection.DataSource = null;
            List<Program> _programs = Program.GetProgramsByDeptID(str);
            if (_programs == null)
            {
                pnlCalender.Enabled = false;
                return;
            }
            pnlCalender.Enabled = true;
            cboProgram.Items.Add("All", 0);
            foreach (Program prog in _programs)
            {
                ListEditItem item = new ListEditItem();
                item.Value = prog.Id.ToString();
                item.Text = prog.ShortName;
                cboProgram.Items.Add(item);
            }
            cboProgram.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        gvwCollection.DataSource = null;
        gvwCollection.DataBind(); 
            
        if (cboProgram.SelectedItem.Value.ToString() == "0")
        {
            FillCourseTreeCombo("0");
            cboCourseTree.ReadOnly = true;
        }
        else
        {
            FillCourseTreeCombo(cboProgram.SelectedItem.Value.ToString());
            cboCourseTree.ReadOnly = false;
        }
        cboCourseTree_SelectedIndexChanged(null, null);
        //LoadCourseOffering();
    }
    private void FillCourseTreeCombo(string str)
    {
        try
        {
            lblMsg.Text = string.Empty;
            gvwCollection.DataSource = null;
            cboCourseTree.Items.Clear();
            _treeMas = TreeMaster.GetByProgram(Int32.Parse(str.ToString()));

            if (_treeMas == null)
            {
                cboCourseTree.Text = string.Empty;
                return;
            }
            cboCourseTree.Items.Add("All", 0);
            foreach (TreeMaster tm in _treeMas)
            {
                ListEditItem item = new ListEditItem();
                item.Value = tm.RootNodeID.ToString();
                item.Text = tm.RootNode.Name.ToString();
                cboCourseTree.Items.Add(item);
            }
            cboCourseTree.SelectedIndex = 0;            
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        try
        {
            lblMsg.Text = string.Empty;   
            List<OfferedCourse> offCourses = new List<OfferedCourse>();
            for (int i = 0; i < gvwCollection.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvwCollection.Rows[i].Cells[4].FindControl("chk");
                if (chk.Checked)
                {
                    Course cs = Course.GetCourses(gvwCollection.Rows[i].Cells[1].Text, gvwCollection.Rows[i].Cells[2].Text);

                    OfferedCourse oc = new OfferedCourse();
                    oc.AcaCalID = Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString());
                    oc.DeptID = Int32.Parse(cboDept.SelectedItem.Value.ToString());
                    oc.ProgID = Int32.Parse(cboProgram.SelectedItem.Value.ToString());
                    oc.TreeRootID = Int32.Parse(cboCourseTree.SelectedItem.Value.ToString());
                    oc.CourseID = cs.Id;
                    oc.VersionID = cs.VersionID;
                    oc.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    oc.CreatedDate = DateTime.Now;

                    offCourses.Add(oc);
                }
            }
            if (offCourses.Count == 0)
            {
                lblMsg.Text = "No Courses are offered yet.";
                return;
            }
            if (OfferedCourse.Save(offCourses, Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString())) != 0)
            {
                lblMsg.Text = Common.Message.SUCCESSFULLYSAVED;
            }
            else
            {
                lblMsg.Text = "Courses are not saved successfully";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void cboCourseTree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        gvwCollection.DataSource = null;
        gvwCollection.DataBind(); 
    }
    private void LoadCourseOffering()
    {
        SortedList hs = Course.GetCoursesByDeptProgTree(Int32.Parse(cboDept.SelectedItem.Value.ToString()), (cboProgram.SelectedItem == null) ? 0 : Int32.Parse(cboProgram.SelectedItem.Value.ToString()), (cboCourseTree.SelectedItem == null) ? 0 : Int32.Parse(cboCourseTree.SelectedItem.Value.ToString()));
        if (hs == null || hs.Count == 0)
        {
            hs = new SortedList();
            lblMsg.Text = "Active Courses Are " + Common.Message.NOTFOUND;
        }
        if (hs.Count > 0)
        {
            lblTotalCourses.Text = "  " + hs.Count.ToString();
        }
        gvwCollection.DataSource = hs.Values;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();                         
    }    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        gvwCollection.DataSource = null;
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            chk.Text = " Unselect All ";
        }
        else
        {
            chk.Text = " Select All ";
            intSelectedCourse = 0;
        }
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[4].FindControl("chk"));
            chk.Checked = ((CheckBox)sender).Checked;
            if (chk.Checked)
            {
                rowItem.BackColor = System.Drawing.Color.LightSalmon;
                intSelectedCourse = intSelectedCourse + 1;
            }
            else
            {
                rowItem.BackColor = System.Drawing.Color.Empty;
                intSelectedCourse = 0;
            }
        }

        lblSelectedCourses.Text = "  " + intSelectedCourse;

        if (Session[SESSION_SELECTEDCOURSES] != null)
        {
            Session.Remove(SESSION_SELECTEDCOURSES);
        }       
        Session.Add(SESSION_SELECTEDCOURSES, intSelectedCourse);
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ch.NamingContainer;
        ch.Checked = ((CheckBox)sender).Checked;

        if (ch.Checked)
        {
            row.BackColor = System.Drawing.Color.LightSalmon;
            intSelectedCourse = ((Session[SESSION_SELECTEDCOURSES] != null) ? int.Parse(Session[SESSION_SELECTEDCOURSES].ToString()) : intSelectedCourse) + 1;
            lblSelectedCourses.Text = "  " + intSelectedCourse;
        }
        else
        {
            row.BackColor = System.Drawing.Color.Empty;
            intSelectedCourse = ((Session[SESSION_SELECTEDCOURSES] != null) ? int.Parse(Session[SESSION_SELECTEDCOURSES].ToString()) : intSelectedCourse) - 1;
            lblSelectedCourses.Text = "  " + intSelectedCourse;
        }

        if (Session[SESSION_SELECTEDCOURSES] != null)
        {
            Session.Remove(SESSION_SELECTEDCOURSES);
        }
        Session.Add(SESSION_SELECTEDCOURSES, intSelectedCourse);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        gvwCollection.DataSource = null;
        lblMsg.Text = string.Empty;
        intSelectedCourse = 0;
        LoadCourseOffering();
        List<OfferedCourse> offerCourses = OfferedCourse.GetOfferedCourse(Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString()), Int32.Parse(cboDept.SelectedItem.Value.ToString()), (cboProgram.SelectedItem == null) ? 0 : Int32.Parse(cboProgram.SelectedItem.Value.ToString()), (cboCourseTree.SelectedItem == null) ? 0 : Int32.Parse(cboCourseTree.SelectedItem.Value.ToString()));
        
        if (offerCourses == null)
        {
            if (gvwCollection.Rows.Count != 0)
            {
                lblMsg.Text = "Offered Courses Are " + Common.Message.NOTFOUND;
                lblSelectedCourses.Text = "  " + intSelectedCourse;
            }
            return;
        }
        foreach (OfferedCourse oc in offerCourses)
        {
            string versionCode = oc.Course.VersionCode;
            foreach (GridViewRow rowItem in gvwCollection.Rows)
            {
                if (versionCode == rowItem.Cells[2].Text.Trim())
                {
                    rowItem.BackColor = System.Drawing.Color.LightSalmon;
                    intSelectedCourse = intSelectedCourse + 1;                    
                    CheckBox chk = (CheckBox)(rowItem.Cells[4].FindControl("chk"));
                    chk.Checked = true;
                }
            }
        }

        lblSelectedCourses.Text = "  " + intSelectedCourse;
        if (Session[SESSION_SELECTEDCOURSES] != null)
        {
            Session.Remove(SESSION_SELECTEDCOURSES);
        }
        Session.Add(SESSION_SELECTEDCOURSES, intSelectedCourse);
    }
}
