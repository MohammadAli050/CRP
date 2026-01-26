using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Data.Linq;


namespace EMS.GradeSheet
{
    public partial class GradeSheetEntry : BasePage
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

        private const string SESSION_CURRENTTRIMESTER = "CurrentTrimester_GS";
        private const string SESSION_GRADESHEET_DT = "GradeSheet_DT_GS";
        private const string SESSION_GRADESHEET_DT_EDIT = "GradeSheet_DT_Edit_GS";
        private const string SESSION_TEACHER = "Teacher_GS";

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
                    //Close By Sajib
                    //LoadTeacher();
                    //Close By Sajib
                }
                //btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");
            }
            catch (Exception Ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
            }
        }


        //Close By Sajib
        //private void LoadTeacher()
        //{
        //    int userID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
        //    _teacher = Teacher.GetByUserId(userID);

        //    if (Session[SESSION_TEACHER] != null)
        //    {
        //        Session.Remove(SESSION_TEACHER);
        //    }
        //    Session[SESSION_TEACHER] = _teacher;
        //}
        //Close By Sajib

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            FillGrid();
          //  ShowGrid();
        }

        private void FillGrid()
        {
            SplitCourseIdVersionIdSectionId();

            try
            {
                gvImportGradesheet.DataSource = null;
                gvImportGradesheet.DataBind();
                //gvGradesheet.Columns.Clear();
                lblMsg.Text = string.Empty;

                List<Student> stds = new List<Student>();
                stds = Student.GetRegisteredStudent(Convert.ToInt32(ddlAcaCalender.SelectedValue), _intCourseID, _intVersionID, _intSectionID);

                if (stds == null || stds.Count == 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Student not found.");
                }
                else
                {
                    GradeSheet_dt = new DataTable();
                    DataRow row = null;

                    GradeSheet_dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
                    GradeSheet_dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                    GradeSheet_dt.Columns.Add(new DataColumn("Name", typeof(string)));
                    GradeSheet_dt.Columns.Add(new DataColumn("TotalMarks", typeof(decimal)));
                    GradeSheet_dt.Columns.Add(new DataColumn("Grade", typeof(string)));

                    foreach (Student item in stds)
                    {
                        row = GradeSheet_dt.NewRow();
                        row["StudentID"] = item.Id;
                        row["Roll"] = item.Roll;
                        row["Name"] = item.StdName;
                        row["TotalMarks"] = 0.0;
                        row["Grade"] = "-";

                        GradeSheet_dt.Rows.Add(row);
                    }

                    if (Session[SESSION_GRADESHEET_DT] != null)
                    {
                        Session.Remove(SESSION_GRADESHEET_DT);
                    }
                    Session[SESSION_GRADESHEET_DT] = GradeSheet_dt;

                    gvImportGradesheet.DataSource = GradeSheet_dt;
                    gvImportGradesheet.DataBind();

                    //gvImportGradesheet.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShowGrid()
        {
            try
            {
                gvShowGradeSheet.DataSource = null;
                gvShowGradeSheet.DataBind();
                //gvGradesheet.Columns.Clear();
                lblMsg.Text = string.Empty;

                DataTable showDt = new DataTable();
                showDt = GradeSheet_BAO.GetDataTable(Convert.ToInt32(ddlAcaCalender.SelectedValue), _intCourseID, _intVersionID, ((Teacher)Session[SESSION_TEACHER]).Id, _intSectionID);

                if (Session[SESSION_GRADESHEET_DT_EDIT] != null)
                {
                    Session.Remove(SESSION_GRADESHEET_DT_EDIT);
                }
                Session[SESSION_GRADESHEET_DT_EDIT] = showDt;

                if (showDt.Rows.Count != 0 && showDt != null)
                {
                    
                    gvShowGradeSheet.DataSource = showDt;
                    gvShowGradeSheet.DataBind();
                }
                gvShowGradeSheet.Columns[0].Visible = false;
               // gvShowGradeSheet.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        protected void ddlAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
        {            
            FillCourseCombo();
            FillProgCombo();
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCourseCombo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _gsEntities = new List<GradeSheetEntity>();
            RefreshObject();

            int done = 0;
            if (_gsEntities != null || _gsEntities.Count != 0)
            {
                done = GradeSheet_BAO.Save(_gsEntities);

                if (done > 0)
                {
                    ShowGrid();
                    Utilities.ShowMassage(lblMsg, Color.Green, "Data saved successfully.");
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Data can't be saved.");
                }
            }
        }

        private void RefreshObject()
        {
            SplitCourseIdVersionIdSectionId();

            foreach (GridViewRow row in gvImportGradesheet.Rows)
            {
                GradeSheetEntity gsEntity = new GradeSheetEntity();

                gsEntity.AcademicCalenderID = Convert.ToInt32(ddlAcaCalender.SelectedValue);

                gsEntity.StudentID = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtStudentID"))).Text);

                gsEntity.CourseID = _intCourseID;

                gsEntity.VersionID = _intVersionID;

                gsEntity.AcaCal_SectionID = _intSectionID;

                gsEntity.TeacherID = ((Teacher)Session[SESSION_TEACHER]).Id;

                gsEntity.ObtainMarks = Convert.ToDecimal(((System.Web.UI.WebControls.TextBox)
                    (row.Cells[0].FindControl("txtTotalMarks"))).Text);

                gsEntity.Grade = ((System.Web.UI.WebControls.Label)
                    (row.Cells[0].FindControl("lblGrade"))).Text;

                
                List<GradeDetailsEntity> _gdEntities = GradeDetails_BAO.Load(Convert.ToInt32(ddlAcaCalender.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));

                GradeDetailsEntity gde = _gdEntities.Find(p => p.Grade == (String.IsNullOrEmpty(gsEntity.Grade) ? "" : gsEntity.Grade.ToString().Trim()));

                gsEntity.GradeId = gde.Gradeid;
                
                // Convert.ToInt32(((System.Web.UI.WebControls.DropDownList)
                     //(row.Cells[0].FindControl("ddlGradeImp"))).SelectedValue);

                gsEntity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                gsEntity.CreatedDate = DateTime.Now;

                _gsEntities.Add(gsEntity);
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
            int userID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;

            //Close By Sajib
            //_teacher = Teacher.GetByUserId(userID);
            //Close By Sajib

            if (_teacher == null)
            {
                _programs = Program.GetPrograms();
            }
            else
            {
                _programs = Program.GetProgramsByTeacherAndAcaCal(_teacher.Id, Convert.ToInt32(ddlAcaCalender.SelectedValue));
            }

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

            int userID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            //Close By Sajib
            //_teacher = Teacher.GetByUserId(userID);
            //Close By Sajib

            if (_teacher == null)
            {
                _courses = Course.GetCoursesByProgram(Convert.ToInt32(ddlProgram.SelectedValue));
            }
            else
            {
                //_courses = Course.GetCourses(_teacher.Id, Convert.ToInt32(ddlAcaCalender.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));
                // _courses = Course.GetCoursesByTeacher(_teacher.Id, Convert.ToInt32(ddlAcaCalender.SelectedValue));

                _acs = AcademicCalenderSection.GetsByTeacherAndAcaCal(_teacher.Id, Convert.ToInt32(ddlAcaCalender.SelectedValue));
            }


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

        protected void btnImport_Click(object sender, EventArgs e)
        {
            // Before attempting to import the file, verify
            // that the FileUpload control contains a file.
            if (FileUploadExcel.HasFile)
            {
                // Get the name of the Excel spreadsheet to upload.
                string strFileName = Server.HtmlEncode(FileUploadExcel.FileName);

                // Get the extension of the Excel spreadsheet.
                string strExtension = Path.GetExtension(strFileName);

                // Validate the file extension.
                if (strExtension != ".xls" && strExtension != ".xlsx")
                {
                    Response.Write("<script>alert('Please select a Excel spreadsheet to import!');</script>");
                    return;
                }

                // Generate the file name to save.
                string strUploadFileName = "~/UploadExcelFiles/" + DateTime.Now.ToString("yyyyMMddHHmmss") + strExtension;

                // Save the Excel spreadsheet on server.
                FileUploadExcel.SaveAs(Server.MapPath(strUploadFileName));


                string strExcelConn = "";
                // There is no column name In a Excel spreadsheet. 
                // So we specify "HDR=YES" in the connection string to use 
                // the values in the first row as column names. 
                if (strExtension == ".xls")
                {
                    // Excel 97-2003 [FileUploadExcel.PostedFile.FileName / Server.MapPath(strUploadFileName)] / System.IO.Path.GetFullPath(FileUploadExcel.PostedFile.FileName)
                    strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0 ; Data Source= " + Server.MapPath(strUploadFileName) +
                        " ; Extended Properties='Excel 8.0; IMEX=1'";
                }
                else
                {
                    // Excel 2007
                    strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0 ; Data Source= " + Server.MapPath(strUploadFileName) +
                        " ; Extended Properties='Excel 12.0 Xml; IMEX=1'";
                }

                try
                {
                    DataTable dtExcel = RetrieveData(strExcelConn);


                    foreach (DataRow item in ((DataTable)Session[SESSION_GRADESHEET_DT]).Rows)
                    {
                        var distinctRow = (from dr in dtExcel.AsEnumerable()
                                           where dr.Field<string>("Student ID") == item["Roll"].ToString()
                                           select dr);

                        foreach (var row in distinctRow)
                        {
                            item["TotalMarks"] = Convert.ToDouble(row.Field<double>("Total").ToString());
                            item["Grade"] = row.Field<string>("Grade").ToString();
                        }
                    }

                   // DataTable ddt = (DataTable)Session[SESSION_GRADESHEET_DT];

                    gvImportGradesheet.DataSource = null;
                    gvImportGradesheet.DataSource = (DataTable)Session[SESSION_GRADESHEET_DT];
                    gvImportGradesheet.DataBind();
                }
                catch (Exception ex)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "First open the spreadsheet then import");
                }
            }
        }

        // Retrieve data from the Excel spreadsheet.
        protected DataTable RetrieveData(string strConn)
        {
            DataTable dtExcel = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                try
                {
                    conn.Open();

                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [Course - X$A6:Z100]", conn);
             

                    // Fill the DataTable with data from the Excel spreadsheet.
                    da.Fill(dtExcel);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                // Initialize an OleDbDataAdapter object.

            }

            return dtExcel;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            gvImportGradesheet.DataSource = null;
            gvImportGradesheet.DataSource = (DataTable)Session[SESSION_GRADESHEET_DT_EDIT];
            gvImportGradesheet.DataBind();
        }

        protected void ddlGradeImp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvImportGradesheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Control ctrl = e.Row.FindControl("ddlGradeImp");
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

        protected void gvShowGradeSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Control ctrl = e.Row.FindControl("ddlGrade");
                Control ctrlGrade = e.Row.Cells[0].FindControl("lblObtainGrade");

                if (ctrl != null)
                {
                    List<GradeDetailsEntity> _gdEntities = GradeDetails_BAO.Load(Convert.ToInt32(ddlAcaCalender.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));

                    DropDownList dd = ctrl as DropDownList;
                    Label lbGrade = ctrlGrade as Label;

                    dd.DataTextField = "Grade";
                    dd.DataValueField = "ID";
                    dd.DataSource = _gdEntities;// lst;
                    dd.DataBind();

                    GradeDetailsEntity gde = _gdEntities.Find(p => p.Grade == (String.IsNullOrEmpty(lbGrade.Text) ? "" : lbGrade.Text.ToString().Trim()));

                    if (gde != null)
                    {
                        dd.SelectedValue = gde.Id.ToString();
                    }
                }
            }
        }
    }
}
