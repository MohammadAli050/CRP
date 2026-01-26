using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Common;
using BussinessObject;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

public partial class Registration_DeptSetUp : BasePage
{
    #region Variable Declaration
    List<Program> _programs = null;
    Program _program = null;
    DeptRegSetUp _deptRegSetUp = null;
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONPROGRAMS = "PROGRAMS";
    private const string SESSIONPROGRAM = "PROGRAM";
    private const string SESSIONDEPTREGSETUP = "DEPTREGSETUP";
    #endregion

    #endregion

    #region Methods
    private void FillProgCombo()
    {
        ddlPrograms.Items.Clear();

        _programs = BussinessObject.Program.GetPrograms();

        if (_programs != null)
        {
            foreach (BussinessObject.Program program in _programs)
            {
                ListItem item = new ListItem();
                item.Value = program.Id.ToString();
                item.Text = program.ShortName;
                ddlPrograms.Items.Add(item);
            }

            if (Session[SESSIONPROGRAMS] != null)
            {
                Session.Remove(SESSIONPROGRAMS);
            }
            Session.Add(SESSIONPROGRAMS, _programs);

            ddlPrograms.SelectedIndex = 0;
        }
    }
    private void FillGradeCombos()
    {
        ddlMinGrade1.Items.Clear();
        ddlMinGrade2.Items.Clear();
        ddlMinGrade3.Items.Clear();


        int count = 0;
        foreach (object program in BOConstants.Grades.Values)
        {
            ListItem item = new ListItem();
            item.Value = BOConstants.Grades.GetKey(count).ToString();
            item.Text = program.ToString();
            ddlMinGrade1.Items.Add(item);
            ddlMinGrade2.Items.Add(item);
            ddlMinGrade3.Items.Add(item);
            count++;
        }

        ddlMinGrade1.SelectedIndex = 0;
        ddlMinGrade2.SelectedIndex = 0;
        ddlMinGrade3.SelectedIndex = 0;
    }
    private void ControlEnablerDisabler(bool enable)
    {
        pnlDetail.Enabled = enable;
        pnlCtl.Enabled = enable;
    }
    private void RefreshValue()
    {
        if (ddlPrograms.SelectedIndex >= 0)
        {
            _program = Program.GetProgram(Int32.Parse(ddlPrograms.SelectedValue));

            if (Session[SESSIONPROGRAM] != null)
            {
                Session.Remove(SESSIONPROGRAM);
            }

            if (_program != null && _program.Id > 0)
            {
                Session.Add(SESSIONPROGRAM, _program);

                _deptRegSetUp = DeptRegSetUp.GetBProgramID(_program.Id);

                //ClearForm(true);

                if (Session[SESSIONDEPTREGSETUP] != null)
                {
                    Session.Remove(SESSIONDEPTREGSETUP);
                }

                if (_deptRegSetUp != null && _deptRegSetUp.Id > 0)
                {

                    Session.Add(SESSIONDEPTREGSETUP, _deptRegSetUp);

                    #region Locality
                    if (_deptRegSetUp.LocalCGPA1.HasValue)
                    {
                        this.spnLocalCGPA1.Value = _deptRegSetUp.LocalCGPA1.Value;
                    }

                    if (_deptRegSetUp.LocalCGPA2.HasValue)
                    {
                        this.spnLocalCGPA2.Value = _deptRegSetUp.LocalCGPA2.Value;
                    }

                    if (_deptRegSetUp.LocalCGPA3.HasValue)
                    {
                        this.spnLocalCGPA3.Value = _deptRegSetUp.LocalCGPA3.Value;
                    }

                    if (_deptRegSetUp.LocalCredit1.HasValue)
                    {
                        this.spnLocalCredit1.Value = _deptRegSetUp.LocalCredit1.Value;
                    }

                    if (_deptRegSetUp.LocalCredit2.HasValue)
                    {
                        this.spnLocalCredit2.Value = _deptRegSetUp.LocalCredit2.Value;
                    }

                    if (_deptRegSetUp.LocalCredit3.HasValue)
                    {
                        this.spnLocalCredit3.Value = _deptRegSetUp.LocalCredit3.Value;
                    }
                    #endregion

                    #region Mandatory
                    if (_deptRegSetUp.ManCGPA1.HasValue)
                    {
                        this.spnMANCGPA1.Value = _deptRegSetUp.ManCGPA1.Value;
                    }
                    if (_deptRegSetUp.ManCGPA2.HasValue)
                    {
                        this.spnMANCGPA2.Value = _deptRegSetUp.ManCGPA2.Value;
                    }
                    if (_deptRegSetUp.ManCGPA3.HasValue)
                    {
                        this.spnMANCGPA3.Value = _deptRegSetUp.ManCGPA3.Value;
                    }
                    if (_deptRegSetUp.ManCredit1.HasValue)
                    {
                        this.spnMANCredit1.Value = _deptRegSetUp.ManCredit1.Value;
                    }
                    if (_deptRegSetUp.ManCredit2.HasValue)
                    {
                        this.spnMANCredit2.Value = _deptRegSetUp.ManCredit2.Value;
                    }
                    if (_deptRegSetUp.ManCredit3.HasValue)
                    {
                        this.spnMANCredit3.Value = _deptRegSetUp.ManCredit3.Value;
                    }
                    if (_deptRegSetUp.ManRetakeGradeLimit1.Trim().Length > 0)
                    {
                        this.ddlMinGrade1.SelectedIndex = (Int32)BOConstants.Grades.IndexOfValue(_deptRegSetUp.ManRetakeGradeLimit1);
                    }
                    if (_deptRegSetUp.ManRetakeGradeLimit2.Trim().Length > 0)
                    {
                        this.ddlMinGrade2.SelectedIndex = (Int32)BOConstants.Grades.IndexOfValue(_deptRegSetUp.ManRetakeGradeLimit2);
                    }
                    if (_deptRegSetUp.ManRetakeGradeLimit3.Trim().Length > 0)
                    {
                        this.ddlMinGrade3.SelectedIndex = (Int32)BOConstants.Grades.IndexOfValue(_deptRegSetUp.ManRetakeGradeLimit3);
                    }
                    #endregion

                    #region Maximum
                    if (_deptRegSetUp.MaxCGPA1.HasValue)
                    {
                        this.spnMAXCGPA1.Value = _deptRegSetUp.MaxCGPA1.Value;
                    }
                    if (_deptRegSetUp.MaxCGPA2.HasValue)
                    {
                        this.spnMAXCGPA2.Value = _deptRegSetUp.MaxCGPA2.Value;
                    }
                    if (_deptRegSetUp.MaxCGPA3.HasValue)
                    {
                        this.spnMAXCGPA3.Value = _deptRegSetUp.MaxCGPA3.Value;
                    }
                    if (_deptRegSetUp.MaxCredit1.HasValue)
                    {
                        this.spnMAXCredit1.Value = _deptRegSetUp.MaxCredit1.Value;
                    }
                    if (_deptRegSetUp.MaxCredit2.HasValue)
                    {
                        this.spnMAXCredit2.Value = _deptRegSetUp.MaxCredit2.Value;
                    }
                    if (_deptRegSetUp.MaxCredit3.HasValue)
                    {
                        this.spnMAXCredit3.Value = _deptRegSetUp.MaxCredit3.Value;
                    }
                    #endregion

                    #region Project/Thesis
                    if (_deptRegSetUp.ProjCGPA.HasValue)
                    {
                        this.spnProjectCGPA.Value = _deptRegSetUp.ProjCGPA.Value;
                    }

                    if (_deptRegSetUp.ProjectCredit.HasValue)
                    {
                        this.spnProjectCrLimit.Value = _deptRegSetUp.ProjectCredit.Value;
                    }

                    if (_deptRegSetUp.ThesisCGPA.HasValue)
                    {
                        this.spnThesisCGPA.Value = _deptRegSetUp.ThesisCGPA.Value;
                    }

                    if (_deptRegSetUp.ThesisCredit.HasValue)
                    {
                        this.spnThesisCrLimit.Value = _deptRegSetUp.ThesisCredit.Value;
                    }
                    #endregion

                    #region Major

                    if (_deptRegSetUp.MajorCGPA.HasValue)
                    {
                        this.spnMajorCGPA.Value = _deptRegSetUp.MajorCGPA.Value;
                    }

                    if (_deptRegSetUp.MajorCredit.HasValue)
                    {
                        this.spnMajorCrLimit.Value = _deptRegSetUp.MajorCredit.Value;
                    }
                    #endregion

                    #region Probation

                    if (_deptRegSetUp.ProbLock.HasValue)
                    {
                        this.spnProbationLock.Value = _deptRegSetUp.ProbLock.Value;
                    }

                    #endregion

                    #region Auto Pre-Registration
                    if (_deptRegSetUp.AutoPreRegCGPA1.HasValue)
                    {
                        spnAutoPreRegCGPA1.Value = _deptRegSetUp.AutoPreRegCGPA1.Value;
                    }
                    if (_deptRegSetUp.AutoPreRegCredit1.HasValue)
                    {
                        spnAutoPreRegCredit1.Value = _deptRegSetUp.AutoPreRegCredit1.Value;
                    }
                    if (_deptRegSetUp.AutoPreRegCGPA2.HasValue)
                    {
                        spnAutoPreRegCGPA2.Value = _deptRegSetUp.AutoPreRegCGPA2.Value;
                    }
                    if (_deptRegSetUp.AutoPreRegCredit2.HasValue)
                    {
                        spnAutoPreRegCredit2.Value = _deptRegSetUp.AutoPreRegCredit2.Value;
                    }
                    if (_deptRegSetUp.AutoPreRegCGPA3.HasValue)
                    {
                        spnAutoPreRegCGPA3.Value = _deptRegSetUp.AutoPreRegCGPA3.Value;
                    }
                    if (_deptRegSetUp.AutoPreRegCredit3.HasValue)
                    {
                        spnAutoPreRegCredit3.Value = _deptRegSetUp.AutoPreRegCredit3.Value;
                    }
                    #endregion

                    if (_deptRegSetUp.CourseRetakeLimit.HasValue)
                    {
                        this.spnRetakelimit.Value = _deptRegSetUp.CourseRetakeLimit.Value;
                    }
                }
            }
        }
    }
    private void RefreshObject()
    {
        _deptRegSetUp = null;
        if (Session[SESSIONDEPTREGSETUP] == null)
        {
            _deptRegSetUp = new DeptRegSetUp();
        }
        else
        {
            _deptRegSetUp = (DeptRegSetUp)Session[SESSIONDEPTREGSETUP];
        }

        _deptRegSetUp.ProgramID = Int32.Parse(ddlPrograms.SelectedValue);//4

        _deptRegSetUp.LocalCGPA1 = Decimal.Parse(this.spnLocalCGPA1.Value.ToString());//3
        _deptRegSetUp.LocalCredit1 = Decimal.Parse(this.spnLocalCredit1.Value.ToString());//3
        _deptRegSetUp.LocalCGPA2 = Decimal.Parse(this.spnLocalCGPA2.Value.ToString());//3
        _deptRegSetUp.LocalCredit2 = Decimal.Parse(this.spnLocalCredit2.Value.ToString());//3
        _deptRegSetUp.LocalCGPA3 = Decimal.Parse(this.spnLocalCGPA3.Value.ToString());//3
        _deptRegSetUp.LocalCredit3 = Decimal.Parse(this.spnLocalCredit3.Value.ToString());//3

        _deptRegSetUp.ManCGPA1 = Decimal.Parse(this.spnMANCGPA1.Value.ToString());//3
        _deptRegSetUp.ManCredit1 = Decimal.Parse(this.spnMANCredit1.Value.ToString());//3

        if (this.ddlMinGrade1.SelectedIndex > 0)
        {
            _deptRegSetUp.ManRetakeGradeLimit1 = this.ddlMinGrade1.SelectedItem.Text;
        }
        //(GradeType)Enum.Parse(typeof(GradeType),this.ddlMinGrade1.SelectedValue,true);//3
        _deptRegSetUp.ManCGPA2 = Decimal.Parse(this.spnMANCGPA2.Value.ToString());//3
        _deptRegSetUp.ManCredit2 = Decimal.Parse(this.spnMANCredit2.Value.ToString());//3
        if (this.ddlMinGrade2.SelectedIndex > 0)
        {
            _deptRegSetUp.ManRetakeGradeLimit2 = this.ddlMinGrade2.SelectedItem.Text;
        }
        _deptRegSetUp.ManCGPA3 = Decimal.Parse(this.spnMANCGPA3.Value.ToString());//3
        _deptRegSetUp.ManCredit3 = Decimal.Parse(this.spnMANCredit3.Value.ToString());//3
        if (this.ddlMinGrade3.SelectedIndex > 0)
        {
            _deptRegSetUp.ManRetakeGradeLimit3 = this.ddlMinGrade3.SelectedItem.Text;
        }

        _deptRegSetUp.MaxCGPA1 = Decimal.Parse(this.spnMAXCGPA1.Value.ToString());//3
        _deptRegSetUp.MaxCredit1 = Decimal.Parse(this.spnMAXCredit1.Value.ToString());//3
        _deptRegSetUp.MaxCGPA2 = Decimal.Parse(this.spnMAXCGPA2.Value.ToString());//3
        _deptRegSetUp.MaxCredit2 = Decimal.Parse(this.spnMAXCredit2.Value.ToString());//3
        _deptRegSetUp.MaxCGPA3 = Decimal.Parse(this.spnMAXCGPA3.Value.ToString());//3
        _deptRegSetUp.MaxCredit3 = Decimal.Parse(this.spnMAXCredit3.Value.ToString());//3

        _deptRegSetUp.ProjCGPA = Decimal.Parse(this.spnProjectCGPA.Value.ToString());
        _deptRegSetUp.ProjectCredit = Decimal.Parse(this.spnProjectCrLimit.Value.ToString());
        _deptRegSetUp.ThesisCGPA = Decimal.Parse(this.spnThesisCGPA.Value.ToString());
        _deptRegSetUp.ThesisCredit = Decimal.Parse(this.spnThesisCrLimit.Value.ToString());
        _deptRegSetUp.MajorCGPA = Decimal.Parse(this.spnMajorCGPA.Value.ToString());
        _deptRegSetUp.MajorCredit = Decimal.Parse(this.spnMajorCrLimit.Value.ToString());
        _deptRegSetUp.ProbLock = Int32.Parse(this.spnProbationLock.Value.ToString());

        _deptRegSetUp.CourseRetakeLimit = Int32.Parse(this.spnRetakelimit.Value.ToString());//3

        if (_deptRegSetUp.Id == 0)
        {
            _deptRegSetUp.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;//5
            _deptRegSetUp.CreatedDate = DateTime.Now;//6 
        }
        else
        {
            _deptRegSetUp.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;//7
            _deptRegSetUp.ModifiedDate = DateTime.Now;//8 
        }

        _deptRegSetUp.AutoPreRegCGPA1 = Decimal.Parse(this.spnAutoPreRegCGPA1.Value.ToString());
        _deptRegSetUp.AutoPreRegCredit1 = Decimal.Parse(this.spnAutoPreRegCredit1.Value.ToString());
        _deptRegSetUp.AutoPreRegCGPA2 = Decimal.Parse(this.spnAutoPreRegCGPA2.Value.ToString());
        _deptRegSetUp.AutoPreRegCredit2 = Decimal.Parse(this.spnAutoPreRegCredit2.Value.ToString());
        _deptRegSetUp.AutoPreRegCGPA3 = Decimal.Parse(this.spnAutoPreRegCGPA3.Value.ToString());
        _deptRegSetUp.AutoPreRegCredit3 = Decimal.Parse(this.spnAutoPreRegCredit3.Value.ToString());

    }
    private void ClearForm()
    {
        //if (!enable)
        //{
        //    this.ddlPrograms.SelectedIndex = -1; 
        //}

        //this.ddlPrograms.SelectedIndex = 0;
        this.spnLocalCGPA1.Value = 0;
        this.spnLocalCGPA2.Value = 0;
        this.spnLocalCGPA3.Value = 0;
        this.spnLocalCredit1.Value = 0;
        this.spnLocalCredit2.Value = 0;
        this.spnLocalCredit3.Value = 0;

        this.spnMANCGPA1.Value = 0;
        this.spnMANCGPA2.Value = 0;
        this.spnMANCGPA3.Value = 0;
        this.spnMANCredit1.Value = 0;
        this.spnMANCredit2.Value = 0;
        this.spnMANCredit3.Value = 0;

        this.ddlMinGrade1.SelectedIndex = 0;
        this.ddlMinGrade2.SelectedIndex = 0;
        this.ddlMinGrade3.SelectedIndex = 0;

        this.spnMAXCGPA1.Value = 0;
        this.spnMAXCGPA3.Value = 0;
        this.spnMAXCGPA2.Value = 0;
        this.spnMAXCredit1.Value = 0;
        this.spnMAXCredit3.Value = 0;
        this.spnMAXCredit2.Value = 0;

        this.spnProbationLock.Value = 0;
        this.spnProjectCGPA.Value = 0;
        this.spnProjectCrLimit.Value = 0;
        this.spnThesisCGPA.Value = 0;
        this.spnThesisCrLimit.Value = 0;
        this.spnMajorCGPA.Value = 0;
        this.spnMajorCrLimit.Value = 0;

        this.spnRetakelimit.Value = 0;

        //ControlEnablerDisabler(enable);
    }
    #endregion

    #region Events
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
            base.CheckPage_Load();

            if (!IsPostBack)
            {
                FillProgCombo();
                FillGradeCombos();
                ddlPrograms_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (ddlPrograms.SelectedIndex == -1)
                {
                    ControlEnablerDisabler(false);
                }
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (ddlPrograms.SelectedIndex >= 0)
            {
                ClearForm();
                RefreshValue();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            RefreshObject();

            bool isNewDept = true;
            if (_deptRegSetUp.Id > 0)
            {

                isNewDept = false;
            }

            DeptRegSetUp.Save(_deptRegSetUp);

            if (isNewDept)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Department information successfully saved");
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Department information successfully updated");
            }
            //ClearForm(false);
            ddlPrograms.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            ClearForm();
            RefreshValue();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    #endregion
}
