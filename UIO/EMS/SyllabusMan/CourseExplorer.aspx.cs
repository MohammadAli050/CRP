using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data.SqlClient;
using System.Drawing;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using CommonUtility;
using System.IO;
using OfficeOpenXml;
using System.Data;
using ClosedXML.Excel;

namespace EMS.SyllabusMan
{
    public partial class CourseExplorer : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.CourseExplorer);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.CourseExplorer));

        BussinessObject.UIUMSUser userObj = null;

        private const string SESSION_OLDCOURSE = "SESSION_OLDCOURSE";

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.CheckPage_Load();
                userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

                if (!IsPostBack)
                {
                    ClearExcelGrid();
                    divUpload.Visible = false;
                    divGrid.Visible = false;
                    btnSaveOrUpdate.Enabled = false;
                    ucProgram.LoadDropDownList();
                }
            }
            catch (Exception Ex)
            {
            }
        }

        protected void ucProgram_ProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGrid();
        }

        private void ClearGrid()
        {
            gvCourselists.DataSource = null;
            gvCourselists.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string txtFormalCode = searchFormalCode.Text.Trim();
                string txtTitle = searchTitle.Text.Trim();
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                if (programId > 0)
                {
                    List<Course> _courseList = CourseManager.GetAllByProgram(programId);

                    List<Course> _courseList2 = new List<Course>();
                    foreach (Course c in _courseList)
                    {
                        if (string.IsNullOrEmpty(c.Title))
                        {
                            c.Title = ".";
                        }
                        _courseList2.Add(c);
                    }

                    if (txtFormalCode != "")
                    {
                        _courseList2 = _courseList2.Where(s => s.FormalCode.Contains(txtFormalCode)).ToList();
                    }
                    if (txtTitle != "")
                    {
                        _courseList2 = _courseList2.Where(s => s.Title.Contains(txtTitle)).ToList();
                    }

                    if (_courseList2 != null && _courseList2.Count > 0)
                    {
                        gvCourselists.DataSource = _courseList2;
                        gvCourselists.DataBind();
                        divGrid.Visible = true;
                        divUpload.Visible = false;
                    }
                    else
                    {
                        ShowMessage("No Data Found or Error in Data Loading", Color.Red);
                    }
                }
                else
                {
                    ShowMessage("Please select a program", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
        }

        protected void btnAddNewCourse_Click(object sender, EventArgs e)
        {
            ClearAll();

            int ProgramId = Convert.ToInt32(ucProgram.selectedValue);

            if(ProgramId==0)
            {
                showAlert("Please select a program");
                return;
            }

            btnSaveOrUpdate.Text = "Save";
            List<TypeDefinition> tdlist = TypeDefinitionManager.GetAll().Where(d => d.Type == "Course").ToList();

            FillTypeComboBox((int)0);
            FillProgramDropDown();
            //txtCourseID.ReadOnly = false;
            //txtVersionID.ReadOnly = false;

            ddlIsActive.SelectedValue = "1";
            ddlMultiple.SelectedValue = "0";
            ModalPopupExtender1.Show();
        }

        private void FillProgramDropDown()
        {
            List<Program> programlist = ProgramManager.GetAll();
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("-Select-", "0"));
            ddlProgram.AppendDataBoundItems = true;

            if (programlist != null)
            {
                ddlProgram.DataSource = programlist.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataBind();

                ddlProgram.SelectedValue = ucProgram.selectedValue.ToString();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            ClearAll();
            FillProgramDropDown();
            btnSaveOrUpdate.Text = "Update";
            btnSaveOrUpdate.Enabled = true;
            LinkButton btn = (LinkButton)sender;
            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            int CourseID = Convert.ToInt32(commandArgs[0]);
            int VersionID = Convert.ToInt32(commandArgs[1]);

            Course _course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
            //var courseExtend = CourseExtendOneManager.GetByCourseIdVersionId(CourseID, VersionID);

            SessionManager.SaveObjToSession<string>(_course.FormalCode, SESSION_OLDCOURSE);

            hdnCourseId.Value = _course.CourseID.ToString();
            hdnVersionId.Value = _course.VersionID.ToString();
            ddlProgram.SelectedValue = _course.ProgramID.ToString();
            txtVersionCode.Text = _course.VersionCode.ToString();
            txtFormalCode.Text = _course.FormalCode.ToString();
            txtTranscriptCode.Text = string.IsNullOrEmpty(_course.TranscriptCode) ? "" : _course.TranscriptCode.ToString();
            txtCourseGroup.Text = string.IsNullOrEmpty(_course.CourseGroup) ? "" : _course.CourseGroup.ToString();
            txtCredits.Text = _course.Credits.ToString();
            txtTitle.Text = _course.Title.ToString();
            txtCourseContent.Text = _course.CourseContent == null ? "" : _course.CourseContent.ToString();


            FillTypeComboBox((int)_course.TypeDefinitionID);

            ddlMultiple.SelectedValue = _course.HasMultipleACUSpan == true ? "1" : "0";
            ddlIsActive.SelectedValue = _course.IsActive == true ? "1" : "0";
            ModalPopupExtender1.Show();


        }

        private void FillTypeComboBox(int id)
        {
            try
            {
                List<TypeDefinition> list = TypeDefinitionManager.GetAll("Course");


                if (list != null)
                {
                    ddlCourseType.DataSource = list.OrderBy(d => d.TypeDefinitionID).ToList();
                    ddlCourseType.DataTextField = "Definition";
                    ddlCourseType.DataValueField = "TypeDefinitionID";
                    ddlCourseType.DataBind();
                }

                if (id != 0)
                {
                    //ddlCourseType.SelectedValue = id + "";
                }
                else
                {
                    //ddlCourseType.SelectedValue = "5";
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
            finally { }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            int CourseID = Convert.ToInt32(commandArgs[0]);
            int VersionID = Convert.ToInt32(commandArgs[1]);

            btnLoad_Click(null, null);
        }

        protected void btnSaveOrUpdate_Click(object sender, EventArgs e)
        {
            int pass = 0;
            try
            {

                //int CourseId = Convert.ToInt32(txtCourseID.Text);
                //int VersionID = Convert.ToInt32(txtVersionID.Text);
                string FormalCode = txtFormalCode.Text.Trim();
                string TranscriptCode = txtTranscriptCode.Text.Trim();
                string CourseGroup = txtCourseGroup.Text.Trim();
                string VersionCode = txtVersionCode.Text.Trim();
                decimal credit = Convert.ToDecimal(txtCredits.Text.Trim());
                string Title = txtTitle.Text.Trim();
                string CourseContent = txtCourseContent.Text.Trim();

                pass = 1;

                Course _course;
                if (btnSaveOrUpdate.Text == "Save")
                {
                    _course = new Course();
                }
                else
                {
                    _course = CourseManager.GetByCourseIdVersionId(Convert.ToInt32(hdnCourseId.Value), Convert.ToInt32(hdnVersionId.Value));
                }

                //_course.CourseID = 0;
                //_course.VersionID = 0;
                _course.FormalCode = FormalCode;
                _course.TranscriptCode = TranscriptCode;
                _course.CourseGroup = CourseGroup;
                _course.VersionCode = VersionCode;
                _course.ProgramID = Convert.ToInt32(ddlProgram.SelectedValue);
                _course.Credits = credit;
                _course.Title = Title;
                _course.CourseContent = CourseContent;
                _course.IsActive = ddlIsActive.SelectedItem.Value == "1" ? true : false;
                _course.HasMultipleACUSpan = ddlMultiple.SelectedItem.Value == "1" ? true : false;
                _course.TypeDefinitionID = Convert.ToInt32(ddlCourseType.SelectedItem.Value);
                //_course.BillTypeDefinitionID = 10;

                if (btnSaveOrUpdate.Text == "Save")
                {
                    _course.CreatedBy = BaseCurrentUserObj.Id;
                    _course.CreatedDate = DateTime.Now;
                    //_course.ModifiedBy = BaseCurrentUserObj.Id;
                    //_course.ModifiedDate = DateTime.Now;
                    try
                    {
                        //  Course cours = CourseManager.GetByCourseIdVersionId(_course.CourseID, _course.VersionID);

                        List<Course> coursList = CourseManager.GetAllByVersionCode(_course.VersionCode);

                        //BussinessObject.Course.IsExist(_course.FormalCode);

                        if (coursList.Count() == 0)
                        {
                            int id = CourseManager.Insert(_course);
                            if (id != 0)
                            {

                                ShowMessage("Successfully Saved", Color.Green);

                                #region Log Insert
                                LogGeneralManager.Insert(
                                     DateTime.Now,
                                     "",
                                     BaseAcaCalCurrent.FullCode,
                                     BaseCurrentUserObj.LogInID,
                                     _course.FormalCode,
                                     _course.VersionCode,
                                     "Insert Course",
                                     BaseCurrentUserObj.LogInID + " is insert course Title : " + _course.Title + "; Credits : " + _course.Credits.ToString() + "; Transcript Code : " + _course.TranscriptCode ,
                                     "normal",
                                      ((int)CommonEnum.PageName.CourseExplorer).ToString(),
                                     CommonEnum.PageName.CourseExplorer.ToString(),
                                     _pageUrl,
                                     "");
                                #endregion
                            }
                            else
                            {
                                ShowMessage("Error in Saving !", Color.Red);
                            }
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            ShowPopUpMessage("Version Code is already Exist !", Color.Red);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message, Color.Red);
                    }
                }
                else
                {
                    try
                    {
                        _course.ModifiedBy = BaseCurrentUserObj.Id;
                        _course.ModifiedDate = DateTime.Now;

                        List<Course> coursList = new List<Course>();
                        if (_course.FormalCode != SessionManager.GetObjFromSession<string>(SESSION_OLDCOURSE))
                        {
                            coursList = CourseManager.GetAllByVersionCode(_course.VersionCode);
                        }

                        if (coursList.Count() == 0)
                        {
                            bool result = CourseManager.Update(_course);
                            if (result)
                            {

                                ShowMessage("Successfully Updated", Color.Green);

                                #region Log Insert
                                LogGeneralManager.Insert(
                                    DateTime.Now,
                                    "",
                                    BaseAcaCalCurrent.FullCode,
                                    BaseCurrentUserObj.LogInID,
                                    _course.FormalCode,
                                    _course.VersionCode,
                                    "Update Course",
                                    BaseCurrentUserObj.LogInID + " is updated course Title : " + _course.Title + "; Credits : " + _course.Credits.ToString() + "; Transcript Code : " + _course.TranscriptCode ,
                                    "normal",
                                     ((int)CommonEnum.PageName.CourseExplorer).ToString(),
                                    CommonEnum.PageName.CourseExplorer.ToString(),
                                    _pageUrl,
                                    "");
                                #endregion
                            }
                            else
                            {
                                ShowMessage("Error in Updating !", Color.Red);
                            }
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            ShowPopUpMessage("Version Code is already Exist !", Color.Red);
                        }

                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message, Color.Red);
                    }
                }


                searchFormalCode.Text = FormalCode;
                btnLoad_Click(null, null);

            }
            catch (Exception ex)
            {
                if (pass == 0)
                {
                    ShowPopUpMessage("Invalid Entry . Please Enter Info Properly", Color.Red);
                }
                else
                {
                    ShowPopUpMessage(ex.Message, Color.Red);
                }

                ModalPopupExtender1.Show();
            }
        }


        protected void gvStudentBillView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string txtFormalCode = searchFormalCode.Text.Trim();
                string txtTitle = searchTitle.Text.Trim();
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                List<Course> _courseList = CourseManager.GetAllByProgram(programId);

                List<Course> _courseList2 = new List<Course>();
                foreach (Course c in _courseList)
                {
                    if (string.IsNullOrEmpty(c.Title))
                    {
                        c.Title = ".";
                    }
                    _courseList2.Add(c);
                }

                if (txtFormalCode != "")
                {
                    _courseList2 = _courseList2.Where(s => s.FormalCode.Contains(txtFormalCode)).ToList();
                }
                if (txtTitle != "")
                {
                    _courseList2 = _courseList2.Where(s => s.Title.Contains(txtTitle)).ToList();
                }

                string sortdirection = string.Empty;
                if (Session["direction"] != null)
                {
                    if (Session["direction"].ToString() == "ASC")
                    {
                        sortdirection = "DESC";
                    }
                    else
                    {
                        sortdirection = "ASC";
                    }
                }
                else
                {
                    sortdirection = "DESC";
                }
                Session["direction"] = sortdirection;
                Sort(_courseList2, e.SortExpression.ToString(), sortdirection);
            }
            catch (Exception ex)
            {

            }
        }

        private void Sort(List<Course> list, String sortBy, String sortDirection)
        {
            if (sortDirection == "ASC")
            {
                list.Sort(new GenericComparer<Course>(sortBy, (int)SortDirection.Ascending));
            }
            else
            {
                list.Sort(new GenericComparer<Course>(sortBy, (int)SortDirection.Descending));
            }
            gvCourselists.DataSource = list;
            gvCourselists.DataBind();
        }

        #endregion

        #region Methods

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }

        private void ShowMessage(string message, Color color)
        {
            showAlert(message);
        }

        private void ShowPopUpMessage(string message, Color color)
        {
            lblPopUpMassege.Text = string.Empty;
            lblPopUpMassege.Text = message;
            lblPopUpMassege.ForeColor = color;
        }


        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        private void ClearAll()
        {
            //txtCourseID.Text = "";
            //txtVersionID.Text = "";
            txtFormalCode.Text = "";
            txtVersionCode.Text = "";
            txtCourseContent.Text = "";
            txtTranscriptCode.Text = "";
            txtCourseGroup.Text = "";
            txtCredits.Text = "";
            txtTitle.Text = "";
            ddlIsActive.SelectedValue = "0";
            ddlMultiple.SelectedValue = "0";
            lblPopUpMassege.Text = "";
            chkCheckDuplicate.Checked = false;

        }

        #endregion

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string txtFormalCode = searchFormalCode.Text.Trim();
                string txtTitle = searchTitle.Text.Trim();


                int programId = Convert.ToInt32(ucProgram.selectedValue);
                List<Course> _courseList = CourseManager.GetAllByProgram(programId);

                List<Course> _courseList2 = new List<Course>();
                foreach (Course c in _courseList)
                {
                    if (string.IsNullOrEmpty(c.Title))
                    {
                        c.Title = ".";
                    }
                    _courseList2.Add(c);
                }

                if (txtFormalCode != "")
                {
                    _courseList2 = _courseList2.Where(s => s.FormalCode.Contains(txtFormalCode)).ToList();
                }
                if (txtTitle != "")
                {
                    _courseList2 = _courseList2.Where(s => s.Title.Contains(txtTitle)).ToList();
                }

                if (_courseList2 != null && _courseList2.Count > 0)
                {

                    string filepath = Server.MapPath(@"~\Upload");
                    HttpFileCollection uploadedFiles = Request.Files;
                    string fileName = "CourseExplorer";
                    fileName += DateTime.Now.ToString("dd-MM-yy") + ".xlsx";

                    filepath = filepath + "\\" + fileName;
                    FileInfo newFile = new FileInfo(filepath);

                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add(fileName);

                        int k = 2;
                        worksheet.Cells[1, 1].Value = "Formal Code";
                        worksheet.Cells[1, 2].Value = "Version Code";
                        worksheet.Cells[1, 3].Value = "Title";
                        worksheet.Cells[1, 4].Value = "Credits";
                        worksheet.Cells[1, 5].Value = "Transcript Code";
                        worksheet.Cells[1, 6].Value = "Course Group";
                        worksheet.Cells[1, 7].Value = "Thesis/Project";
                        worksheet.Cells[1, 8].Value = "Course Type";

                        for (int i = 0; i < _courseList2.Count; i++)
                        {
                            worksheet.Cells[k, 1].Value = Convert.ToString(_courseList2[i].FormalCode) + " ";
                            worksheet.Cells[k, 2].Value = Convert.ToString(_courseList2[i].VersionCode) + " ";
                            worksheet.Cells[k, 3].Value = Convert.ToString(_courseList2[i].Title) + " ";
                            worksheet.Cells[k, 4].Value = Convert.ToString(_courseList2[i].Credits) + " ";
                            worksheet.Cells[k, 5].Value = Convert.ToString(_courseList2[i].TranscriptCode) + " ";
                            worksheet.Cells[k, 6].Value = Convert.ToString(_courseList2[i].CourseGroup) + " ";
                            worksheet.Cells[k, 7].Value = Convert.ToString(_courseList2[i].HasMultipleACUSpan.ToString()) + " ";
                            worksheet.Cells[k, 8].Value = Convert.ToString(_courseList2[i].CourseType) + " ";
                            k++;
                        }

                        xlPackage.Save();
                        xlPackage.Dispose();
                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                        response.TransmitFile(filepath);
                        response.Flush();
                        response.Close();
                        //response.End();
                        File.Delete(Server.MapPath("~/Upload/" + fileName));

                    }
                }
                else
                {
                    ShowMessage("No Data Found or Error in Data Loading", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
        }

        protected void chkCheckDuplicate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblPopUpMassege.Text = string.Empty;
                ModalPopupExtender1.Show();

                if (chkCheckDuplicate.Checked)
                {

                    string versionCode = txtVersionCode.Text.Trim();

                    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

                    var ExistingObj = ucamContext.Courses.Where(x => x.VersionCode == versionCode).FirstOrDefault();

                    if (ExistingObj != null)
                    {
                        lblPopUpMassege.Text = "Version Code Already Exists With Another Course With Formal Code : " + ExistingObj.FormalCode;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }




        #region New Methods For Course Upload

        protected void btnCourseMigrateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NotMigratedCourseList"] = null;
                int ProgramId = Convert.ToInt32(ucProgram.selectedValue);

                int Total = 0;


                if (ProgramId > 0)
                {
                    DataTable CourseList = (DataTable)Session["DataTableExcelUpload"];

                    if (CourseList != null && CourseList.Rows.Count > 0)
                    {

                        List<CourseMigrationObj> MigrationCourseList = new List<CourseMigrationObj>();

                        Total = CourseList.Rows.Count;

                        string FormalCode = "", VersionCode = "", Title = "", Credits = "", ThesisProject = "", CourseType = "";

                        #region Data Migration Process

                        var TypeList = TypeDefinitionManager.GetAll().Where(x => x.Type == "Course").ToList();
                        if (TypeList != null && TypeList.Any())
                        {
                            foreach (DataRow row in CourseList.Rows)
                            {
                                try
                                {
                                    string SL = "";

                                    if (!string.IsNullOrEmpty(row[0].ToString()))
                                        SL = row[0].ToString().Trim();

                                    if (SL != "")
                                    {
                                        if (!string.IsNullOrEmpty(row[1].ToString()))
                                            FormalCode = row[1].ToString().Trim();

                                        if (!string.IsNullOrEmpty(row[2].ToString()))
                                            VersionCode = row[2].ToString().Trim();

                                        if (!string.IsNullOrEmpty(row[3].ToString()))
                                            Title = row[3].ToString().Trim();

                                        if (!string.IsNullOrEmpty(row[4].ToString()))
                                            Credits = row[4].ToString().Trim();

                                        if (!string.IsNullOrEmpty(row[5].ToString()))
                                            ThesisProject = row[5].ToString().Trim().ToLower();

                                        if (!string.IsNullOrEmpty(row[6].ToString()))
                                            CourseType = row[6].ToString().Trim();

                                        var CrsType = TypeList.Where(x => x.Definition == CourseType).FirstOrDefault();
                                        if (CrsType != null)
                                        {
                                            bool HasThesisProject = false;
                                            if (ThesisProject == "yes")
                                                HasThesisProject = true;

                                            CourseMigrationObj MigratedObj = EMS.Module.CommonFunctionForCourseMigration.MigrateCourse(ProgramId, FormalCode, VersionCode, Title, Credits, HasThesisProject, CrsType.TypeDefinitionID, userObj.Id, userObj.LogInID, _pageId, _pageName, _pageUrl);

                                            if (MigratedObj != null)
                                            {
                                                MigrationCourseList.Add(MigratedObj);
                                            }
                                        }
                                        else
                                        {
                                            CourseMigrationObj NewObj = new CourseMigrationObj();

                                            NewObj.Status = 0;
                                            NewObj.FormaCode = FormalCode;
                                            NewObj.Title = Title;
                                            NewObj.Reason = "Course Type Not Found";
                                            MigrationCourseList.Add(NewObj);
                                        }
                                    }


                                }
                                catch (Exception ex)
                                {
                                    CourseMigrationObj ExceptionObj = new CourseMigrationObj();

                                    ExceptionObj.Status = 0;
                                    ExceptionObj.FormaCode = FormalCode;
                                    ExceptionObj.Title = Title;
                                    ExceptionObj.Reason = "Exception occurred";

                                    MigrationCourseList.Add(ExceptionObj);

                                }

                            }

                            Session["DataTableExcelUpload"] = null;

                            ClearExcelGrid();

                            #region Bind Grid View

                            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                            cookie.Value = "Flag";
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.AppendCookie(cookie);



                            if (MigrationCourseList != null && MigrationCourseList.Any())
                            {
                                string msg = "";

                                if (MigrationCourseList.Where(x => x.Status == 1).Any())
                                {
                                    var MigratedList = MigrationCourseList.Where(x => x.Status == 1).ToList();

                                    lblTotalCourse.Text = "Total Updated Course List : " + MigratedList.Count();

                                    GVTotalCourseList.DataSource = MigratedList;
                                    GVTotalCourseList.DataBind();

                                    DivTotalCourse.Visible = true;

                                    msg = "Course Information Updated Successfully";

                                }
                                if (MigrationCourseList.Where(x => x.Status == 0).Any())
                                {
                                    var NotMigratedList = MigrationCourseList.Where(x => x.Status == 0).ToList();

                                    lblNotMigratedCourse.Text = "Total Not Updated Course List : " + NotMigratedList.Count();

                                    GVNotUploadedCourseList.DataSource = NotMigratedList;
                                    GVNotUploadedCourseList.DataBind();

                                    DivNotUploadedCourse.Visible = true;

                                    Session["NotMigratedCourseList"] = NotMigratedList;

                                    if (msg == "")
                                        msg = "Some Courses Information is Not Updated";
                                    else
                                        msg = msg + " And Some Course Information is Not Updated";

                                }

                                showAlert(msg);
                                return;

                            }
                            else
                            {
                                showAlert("Something went wrong while uploding the excel file");
                                return;
                            }
                            #endregion

                        }
                        else
                        {
                            showAlert("Course Type Not Created In Database");
                            return;
                        }


                        #endregion



                    }
                    else
                    {
                        showAlert("No Data Found To Upload");
                        Session["DataTableExcelUpload"] = null;
                        HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                        cookie.Value = "Flag";
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.AppendCookie(cookie);
                        return;
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                ClearExcelGrid();
                showAlert("Something went wrong");
                return;
            }
        }


        #endregion

        protected void btnMigrateCourse_Click(object sender, EventArgs e)
        {
            divGrid.Visible = false;
            divUpload.Visible = !divUpload.Visible;
        }

        protected void btnSampleExcel_Click(object sender, EventArgs e)
        {
            try
            {

                string fileName = "SampleCourseMigrationFile.xlsx";

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.TransmitFile(Server.MapPath("~/Upload/SampleExcel/" + fileName));

                Response.Flush();
                Response.SuppressContent = true;
                Response.End();

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExcelUpload_Click(object sender, EventArgs e)
        {
            try
            {
                ClearExcelGrid();
                Session["DataTableExcelUpload"] = null;
                Session["NotMigratedCourseList"] = null;
                if (ExcelUpload.HasFile)
                {
                    string saveFolder = "~/Upload/";
                    string filename = ExcelUpload.FileName;
                    string filePath = Path.Combine(saveFolder, ExcelUpload.FileName);
                    string excelpath = Server.MapPath(filePath);

                    string Extension = Path.GetExtension(filename);

                    if (Extension.ToLower() != ".xlsx" && Extension.ToLower() != ".xls")
                    {
                        showAlert("Please upload the excel file and try again");
                        return;
                    }

                    if (File.Exists(excelpath))
                    {
                        System.IO.File.Delete(excelpath);
                        ExcelUpload.SaveAs(excelpath);
                    }
                    else
                    {
                        ExcelUpload.SaveAs(excelpath);
                    }

                    try
                    {
                        System.Data.OleDb.OleDbConnection MyConnection;
                        System.Data.DataTable DtTable;
                        System.Data.OleDb.OleDbDataAdapter MyCommand; ;
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelpath + ";Extended Properties=Excel 12.0 xml;");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
                        MyCommand.TableMappings.Add("Table", "TestTable");
                        DtTable = new System.Data.DataTable();
                        MyCommand.Fill(DtTable);
                        Session["DataTableExcelUpload"] = DtTable;

                        MyConnection.Close();
                        if (DtTable.Rows.Count > 0)
                        {
                            GVTotalCourseList.DataSource = DtTable;
                            GVTotalCourseList.DataBind();

                            lblTotalCourse.Text = "Total Course List : " + DtTable.Rows.Count;

                            DivTotalCourse.Visible = true;
                        }

                        if (File.Exists(excelpath))
                        {
                            System.IO.File.Delete(excelpath);
                        }

                    }
                    catch (Exception ex)
                    {
                        showAlert("Excel file format is not correct.You can download sample excel file for help.");
                        return;
                    }
                }
                else
                {
                    showAlert("Please select a excel file");
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearExcelGrid()
        {
            try
            {
                DivTotalCourse.Visible = false;
                DivNotUploadedCourse.Visible = false;
                GVTotalCourseList.DataSource = null;
                GVTotalCourseList.DataBind();
                GVNotUploadedCourseList.DataSource = null;
                GVNotUploadedCourseList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                List<CourseMigrationObj> CourseList = (List<CourseMigrationObj>)Session["NotMigratedCourseList"];


                if (CourseList != null && CourseList.Count > 0)
                {
                    DataTable stdlist = CommonUtility.DataTableMethods.ListToDataTable(CourseList);

                    if (stdlist != null && stdlist.Rows.Count > 0)
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {

                            IXLWorksheet sheet2;
                            sheet2 = wb.AddWorksheet(stdlist, "Sheet");

                            sheet2.Table("Table1").ShowAutoFilter = false;

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "NotUploadedCourseList.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);

                                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                                cookie.Value = "Flag";
                                cookie.Expires = DateTime.Now.AddDays(1);
                                Response.AppendCookie(cookie);

                                Response.Flush();
                                Response.SuppressContent = true;
                                Response.End();
                            }
                        }
                        ;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                searchFormalCode.Text = "";
                searchTitle.Text = "";
                ClearGrid();
                btnLoad_Click(null, null);
            }
            catch (Exception ex)
            {
            }
        }



    }
}