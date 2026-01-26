using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BussinessObject;
using Common;
using System.Drawing;
using System.Collections.Generic;

namespace EMS.GradeSheet
{
    public partial class GradeSheetEntryReg : BasePage
    {
        private List<AcademicCalenderSection> _acs = new List<AcademicCalenderSection>();
        private List<AcademicCalender> _trimesterInfos = null;
        private List<Course> _courses = null;
        private List<Program> _programs = null;
        private Teacher _teacher = null;
        private DataTable GradeSheet_dt = null;
        private List<GradeSheetEntity> _gsEntities = null;
        private int _intCourseID = 0;
        private int _intVersionID = 0;
        private int _intSectionID = 0;

        private const string SESSION_CURRENTTRIMESTER = "CurrentTrimester_GSR";
        private const string SESSION_GRADESHEET_DT = "GradeSheet_DT_GSR";
        private const string SESSION_GRADESHEET_DT_EDIT = "GradeSheet_DT_Edit_GSR";
        private const string SESSION_TEACHER = "Teacher_GSR";
        private const string SESSION_GRADESHEET_ID = "GradeSheet_ID";

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
                    FillProgCombo();
                    FillCourseCombo();
                    //LoadTeacher();

                }
                //btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");
            }
            catch (Exception Ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
            }
        }

        private void FillAcademicCalenderCombo()
        {
            try
            {
                string currentTrimester = "";
                _trimesterInfos = AcademicCalender.Gets();

                if (_trimesterInfos == null)
                {
                    return;
                }

                foreach (AcademicCalender ac in _trimesterInfos)
                {
                    if (ac.IsCurrent)
                    {
                        currentTrimester = ac.Id.ToString();
                    }

                    //if (ac.AdmiStartDate <= DateTime.Now && ac.AdmiEndDate >= DateTime.Now)
                    //if (ac.IsNext == true)
                    //{
                    ListItem lei = new ListItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    ddlAcaCalender.Items.Add(lei);
                    //}
                }

                if (Session[SESSION_CURRENTTRIMESTER] != null)
                {
                    Session.Remove(SESSION_CURRENTTRIMESTER);
                }
                Session[SESSION_CURRENTTRIMESTER] = currentTrimester;

                ddlAcaCalender.SelectedValue = currentTrimester;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }

        private void FillProgCombo()
        {
            _programs = Program.GetPrograms();

            ddlProgram.Items.Clear();
            if (_programs != null)
            {
                //ListItem itemBlank = new ListItem();
                //itemBlank.Value = "0";
                //itemBlank.Text = "All";
                //ddlProgram.Items.Add(itemBlank);

                foreach (Program program in _programs)
                {
                    ListItem item = new ListItem();
                    item.Value = program.Id.ToString();
                    item.Text = program.ShortName;
                    ddlProgram.Items.Add(item);
                }

                if (Session["Programs"] != null)
                {
                    Session.Remove("Programs");
                }
                Session.Add("Programs", _programs);

                ddlProgram.SelectedIndex = 0;
            }
        }

        private void FillCourseCombo()
        {
            _acs = new List<AcademicCalenderSection>();

            _acs = AcademicCalenderSection.GetsByTeacherAcacalProgram(Convert.ToInt32(ddlAcaCalender.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));

            ddlCourse.Items.Clear();

            //_courses = Course.GetCoursesByStudentId(1);

            if (_acs != null)
            {
                //ListItem itemBlank = new ListItem();
                //itemBlank.Value = "0";
                //itemBlank.Text = "All";
                //ddlCourse.Items.Add(itemBlank);

                foreach (AcademicCalenderSection acs in _acs)
                {
                    ListItem item = new ListItem();
                    item.Value = acs.ChildCourseID.ToString() + "," + acs.ChildVersionID.ToString() + "," + acs.Id.ToString();
                    item.Text = acs.ChildCourse.FormalCode + " - " + acs.ChildCourse.Title + " - ( " + acs.SectionName + " )";
                    ddlCourse.Items.Add(item);
                }

                //foreach (Course course in _courses)
                //{
                //    ListItem item = new ListItem();
                //    item.Value = course.Id.ToString() + "," + course.VersionID.ToString();
                //    item.Text = course.FormalCode + "-" + course.Title;
                //    ddlCourse.Items.Add(item);
                //}

                ddlCourse.SelectedIndex = 0;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            _gsEntities = new List<GradeSheetEntity>();
            RefreshObject();

            int done = 0;
            if (_gsEntities != null || _gsEntities.Count != 0)
            {

              //  GradeSheet_BAO.UpdateByReg(_gsEntities);
                
                done = GradeSheet_BAO.TransferByReg(_gsEntities);
                if (done != 0)
                {
                    ShowGrid();
                    Utilities.ShowMassage(lblMsg, Color.Green, "Successfully transferred.");

                   // ShowGridByConflictData();
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Data can't be transferred.");
                }
            }
        }

        //private void ShowGridByConflictData()
        //{
        //    SplitCourseIdVersionIdSectionId();

        //    try
        //    {
        //        gvGradesheet.DataSource = null;
        //        gvGradesheet.DataBind();
        //        //gvGradesheet.Columns.Clear();
        //        lblMsg.Text = string.Empty;

        //        DataTable showDt = new DataTable();
        //        showDt = GradeSheet_BAO.GetConflictDataTable(Convert.ToInt32(ddlAcaCalender.SelectedValue), _intCourseID, _intVersionID, _intSectionID);

        //        if (Session[SESSION_GRADESHEET_DT_EDIT] != null)
        //        {
        //            Session.Remove(SESSION_GRADESHEET_DT_EDIT);
        //        }
        //        Session[SESSION_GRADESHEET_DT_EDIT] = showDt;

        //        if (showDt.Rows.Count != 0 && showDt != null)
        //        {
        //            gvGradesheet.DataSource = showDt;
        //            gvGradesheet.DataBind();
        //            Utilities.ShowMassage(lblMsg, Color.Red, "Conflict result found.");
        //        }
        //        else
        //        {
        //            Utilities.ShowMassage(lblMsg, Color.Red, "No Conflict found.");
        //        }

        //        gvGradesheet.Columns[1].Visible = false;
        //        gvGradesheet.Columns[2].Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        //    }
        //}

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCourseCombo();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void ShowGrid()
        {
            SplitCourseIdVersionIdSectionId();

            try
            {
                gvGradesheet.DataSource = null;
                gvGradesheet.DataBind();
                //gvGradesheet.Columns.Clear();
                lblMsg.Text = string.Empty;

                DataTable showDt = new DataTable();
                showDt = GradeSheet_BAO.GetDataTable1(Convert.ToInt32(ddlAcaCalender.SelectedValue), _intCourseID, _intVersionID, _intSectionID);

                if (Session[SESSION_GRADESHEET_DT_EDIT] != null)
                {
                    Session.Remove(SESSION_GRADESHEET_DT_EDIT);
                }
                Session[SESSION_GRADESHEET_DT_EDIT] = showDt;

                if (showDt.Rows.Count != 0 && showDt != null)
                {
                    gvGradesheet.DataSource = showDt;
                    gvGradesheet.DataBind();
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Student not found.");
                }

                gvGradesheet.Columns[1].Visible = false;
                gvGradesheet.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }

            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                CheckBox ch1 = row.FindControl("chkSelect1") as CheckBox;
                CheckBox ch2 = row.FindControl("chkSelect2") as CheckBox;
                CheckBox ch3 = row.FindControl("chkSelect3") as CheckBox;
                CheckBox ch4 = row.FindControl("chkSelect4") as CheckBox;
                CheckBox ch5 = row.FindControl("chkSelect5") as CheckBox;
                CheckBox ch6 = row.FindControl("chkSelect6") as CheckBox;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade1"))).Text) == 0)
                    ch1.Enabled = false;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade2"))).Text) == 0)
                    ch2.Enabled = false;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade3"))).Text) == 0)
                    ch3.Enabled = false;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade4"))).Text) == 0)
                    ch4.Enabled = false;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade5"))).Text) == 0)
                    ch5.Enabled = false;
                if (Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade6"))).Text) == 0)
                    ch6.Enabled = false;
            }

        }

        private string[] SplitValues(string str)
        {
            return str.Split(new char[] { ',', '-' });
        }

        private void SplitCourseIdVersionIdSectionId()
        {
            if (ddlCourse.SelectedValue.ToString() != "0" && ddlCourse.SelectedValue.ToString() != "")
            {
                string[] str = SplitValues(ddlCourse.SelectedValue.ToString());
                _intCourseID = Convert.ToInt32(str[0]);
                _intVersionID = Convert.ToInt32(str[1]);
                _intSectionID = Convert.ToInt32(str[2]);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            _gsEntities = new List<GradeSheetEntity>();
            RefreshObject();

            int done = 0;
            if (_gsEntities != null || _gsEntities.Count != 0)
            {
                done = GradeSheet_BAO.UpdateByReg(_gsEntities);

                if (done > 0)
                {
                    ShowGrid();
                    Utilities.ShowMassage(lblMsg, Color.Green, "Data update successfully.");
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Data can't be updated.");
                }
            }
        }

        private void RefreshObject()
        {
            SplitCourseIdVersionIdSectionId();

            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                GradeSheetEntity gsEntity = new GradeSheetEntity();
                
                gsEntity.Id = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtId"))).Text);

                gsEntity.AcademicCalenderID = Convert.ToInt32(ddlAcaCalender.SelectedValue);

                gsEntity.StudentID = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtStudentID"))).Text);

                gsEntity.CourseID = _intCourseID;

                gsEntity.VersionID = _intVersionID;

                gsEntity.AcaCal_SectionID = _intSectionID;

                gsEntity.ObtainMarks = Convert.ToDecimal(((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtTotalMarks"))).Text);

                gsEntity.Grade = ((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtGrade"))).Text;

                gsEntity.GradeId = Convert.ToInt32(((System.Web.UI.WebControls.Label)
                    (row.Cells[0].FindControl("lblGrade"))).Text);

                gsEntity.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                gsEntity.ModifiedDate = DateTime.Now;

                gsEntity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                gsEntity.CreatedDate = DateTime.Now;

                _gsEntities.Add(gsEntity);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                Control ctrl = row.FindControl("ddlGrade") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label lbl = row.FindControl("lblGrade") as Label;
                        lbl.Text = ddl1.SelectedItem.Text;
                        break;
                    }
                }
            }
        }

        protected void gvGradesheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Control ctrl = e.Row.FindControl("ddlGrade");
                Control ctrlGrade = e.Row.Cells[0].FindControl("lblGrade");

                if (ctrl != null)
                {
                    List<GradeDetailsEntity> _gdEntities = GradeDetails_BAO.Load(Convert.ToInt32(ddlAcaCalender.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));

                    DropDownList dd = ctrl as DropDownList;
                    Label tbGrade = ctrlGrade as Label;

                    dd.DataTextField = "Grade";
                    dd.DataValueField = "ID";
                    dd.DataSource = _gdEntities;// lst;
                    dd.DataBind();

                    GradeDetailsEntity gde = _gdEntities.Find(p => p.Grade == (String.IsNullOrEmpty(tbGrade.Text) ? "" : tbGrade.Text.ToString().Trim()));

                    if (gde != null)
                    {
                        dd.SelectedValue = gde.Id.ToString();
                    }
                }
            }
        }

        //protected void btnConflictResult_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvGradesheet.SelectedRow == null)
        //        {
        //            lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
        //            return;
        //        }

        //        SplitCourseIdVersionIdSectionId();
        //        //string str = gvGradesheet.SelectedRow.Cells[0].FindControl();
        //        int stdId = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)(gvGradesheet.SelectedRow.Cells[0].FindControl("txtStudentID"))).Text);
        //        int gradeSheetId = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)(gvGradesheet.SelectedRow.Cells[0].FindControl("txtId"))).Text);
        //        AddToSession(SESSION_GRADESHEET_ID, gradeSheetId);

        //        List<ConflictResultEntity> cre = new List<ConflictResultEntity>();

        //        cre = ConflictResult_BAO.Load(stdId, _intCourseID, _intVersionID, _intSectionID);

        //        gvConflictedResult.DataSource = null;
        //        gvConflictedResult.DataSource = cre;
        //        gvConflictedResult.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        //protected void btnConsiderGpa_Click(object sender, EventArgs e)
        //{
        //    int id = 0;
        //    int done = 0;

        //    try
        //    {
        //        foreach (GridViewRow row in gvConflictedResult.Rows)
        //        {
        //            if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect"))).Checked))
        //            {
        //                id = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblConflictId"))).Text);
        //            }
        //        }

        //        done = ConflictResult_BAO.Update(id);
        //        done = GradeSheet_BAO.UpdateConflictRetake(Convert.ToInt32(GetFromSession(SESSION_GRADESHEET_ID)));

        //        ShowGridByConflictData();

        //        gvConflictedResult.DataSource = null;
        //        gvConflictedResult.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void gvConflictedResult_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int i=0;
        //    foreach (GridViewRow row in gvConflictedResult.Rows)
        //    {
        //        if (i != gvConflictedResult.SelectedIndex)
        //        {
        //            ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect"))).Checked = false;
        //        }
        //        else
        //        { 
        //            ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect"))).Checked = true; 
        //        }
        //        i++;
        //    }
        //}

        protected void gvGradesheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gvConflictedResult.DataSource = null;
            //gvConflictedResult.DataBind();
        }

        protected void chkSelect1_CheckedChanged(object sender, EventArgs e)
        {
            
             
            foreach (GridViewRow row in gvGradesheet.Rows)
               {
                   
                   if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked))
                   {
                       ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked = false;
                       ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked = false;
                       ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked = false;
                       ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked = false;
                       ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked = false;
                       ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade1"))).Text;
                       ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade1"))).Text;
                  
                   }
                      
                
               }
        }

        protected void chkSelect2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked))
                {
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked = false;
                    ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade2"))).Text;
                    ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade2"))).Text;

                }


            }
        }

        protected void chkSelect3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked))
                {
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked = false;
                    ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade3"))).Text;
                    ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade3"))).Text;

                }
            }
        }

        protected void chkSelect4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked))
                {
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked = false;
                    ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade4"))).Text;
                    ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade4"))).Text;

                }
            }
        }

        
        protected void chkSelect5_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                
                    if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked))
                    {
                        ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked = false;
                        ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked = false;
                        ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked = false;
                        ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked = false;
                        ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked = false;
                        ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade5"))).Text;
                        ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade5"))).Text;

                    }
               
            }
        }

        protected void chkSelect6_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGradesheet.Rows)
            {
                if (Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect6"))).Checked))
                {
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect1"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect2"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect4"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect5"))).Checked = false;
                    ((System.Web.UI.WebControls.CheckBox)(row.Cells[0].FindControl("chkSelect3"))).Checked = false;
                    ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade"))).Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[0].FindControl("txtGrade6"))).Text;
                    ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade"))).Text = ((System.Web.UI.WebControls.Label)(row.Cells[0].FindControl("lblGrade6"))).Text;

                }
            }
        }
    }
}
