using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using System.IO;
using System.Drawing;
using System.Data;
using UCAMDAL;
using System.Reflection;
using ClosedXML.Excel;


namespace EMS.Module.student
{
    public partial class StudentInformationViewUpdateAndMigrateNewStudent : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        UCAMEntities ucamEntites = new UCAMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                DivGridview.Visible = false;
                DivExcelUpload.Visible = false;
                lnkDownloadExcel.Visible = false;

                LoadProgramDDL();
                LoadBatchDDL(0);
            }

        }

        private void LoadProgramDDL()
        {
            try
            {
                List<LogicLayer.BusinessObjects.Program> programList = new List<LogicLayer.BusinessObjects.Program>();
                programList = ProgramManager.GetAll();

                ddlProgram.Items.Clear();
                ddlProgram.AppendDataBoundItems = true;

                if (programList != null)
                {
                    ddlProgram.Items.Add(new ListItem("-Select-", "0"));
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";

                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadBatchDDL(int ProgramId)
        {
            try
            {
                List<LogicLayer.BusinessObjects.Batch> batchList = new List<LogicLayer.BusinessObjects.Batch>();
                batchList = BatchManager.GetAll();

                ddlBatch.Items.Clear();
                ddlBatch.AppendDataBoundItems = true;

                ddlBatch.Items.Add(new ListItem("-Select-", "0"));


                if (batchList != null)
                {
                    batchList = batchList.Where(b => b.ProgramId == ProgramId).ToList();

                    ddlBatch.DataTextField = "BatchExtended";
                    ddlBatch.DataValueField = "BatchId";
                    if (batchList != null)
                    {
                        ddlBatch.DataSource = batchList.OrderByDescending(b => b.BatchNO).ToList(); ;
                        ddlBatch.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                LoadBatchDDL(ProgramId);
                ClearExcelGrid();
            }
            catch (Exception ex)
            {

            }
        }


        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGrid();
            ClearExcelGrid();
        }

        private void ClearGrid()
        {
            lblCount.Text = string.Empty;
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
            DivGridview.Visible = false;
            DivExcelUpload.Visible = false;
            lnkDownloadExcel.Visible = false;

            Session["NotMigratedStudentList"] = null;
            Session["DataTableExcelUpload"] = null;
        }

        protected void lnkViewInformation_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();
                ClearExcelGrid();
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int BatchId = Convert.ToInt32(ddlBatch.SelectedValue);

                if (ProgramId > 0 && BatchId > 0)
                {
                    List<LogicLayer.BusinessObjects.Student> stdList = StudentManager.GetAllByProgramIdBatchId(ProgramId, BatchId);

                    if (stdList != null && stdList.Count > 0)
                    {
                        gvStudentList.DataSource = stdList.OrderBy(x => x.Roll);
                        gvStudentList.DataBind();
                        DivGridview.Visible = true;
                        lblCount.Text = "Total Students : " + stdList.Count;
                    }
                    else
                    {
                        ClearGrid();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkStudentMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();
                ClearExcelGrid();
                DivExcelUpload.Visible = true;
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
                Session["NotMigratedStudentList"] = null;
                if (ExcelUpload.HasFile)
                {
                    string saveFolder = "~/Upload/";
                    string filename = ExcelUpload.FileName;
                    string filePath = Path.Combine(saveFolder, ExcelUpload.FileName);
                    string excelpath = Server.MapPath(filePath);

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
                        //PopulateData(DtTable, excelpath);
                        Session["DataTableExcelUpload"] = DtTable;

                        MyConnection.Close();
                        if (DtTable.Rows.Count > 0)
                        {

                            foreach (var column in DtTable.Columns.Cast<DataColumn>().ToArray())
                            {
                                if (DtTable.AsEnumerable().All(dr => dr.IsNull(column)))
                                    DtTable.Columns.Remove(column);
                            }

                            for (int i = DtTable.Rows.Count - 1; i >= 0; i--)
                            {
                                if (DtTable.Rows[i][0].ToString() == String.Empty)
                                {
                                    DtTable.Rows.RemoveAt(i);
                                }
                            }
                            if (DtTable.Columns.Count == 7)
                            {
                                DataColumnCollection columns = DtTable.Columns;
                                if (columns.Contains("F1"))
                                {
                                    DtTable.Columns["F1"].ColumnName = "SL.No";
                                }
                                if (columns.Contains("F2"))
                                {
                                    DtTable.Columns["F2"].ColumnName = "Program";
                                }
                                if (columns.Contains("F3"))
                                {
                                    DtTable.Columns["F3"].ColumnName = "Reg.No";
                                }
                                if (columns.Contains("F4"))
                                {
                                    DtTable.Columns["F4"].ColumnName = "Roll";
                                }
                                if (columns.Contains("F5"))
                                {
                                    DtTable.Columns["F5"].ColumnName = "Session";
                                }
                                if (columns.Contains("F6"))
                                {
                                    DtTable.Columns["F6"].ColumnName = "Name";
                                }
                                if (columns.Contains("F7"))
                                {
                                    DtTable.Columns["F7"].ColumnName = "Gender";
                                }

                                GVTotalStudentList.DataSource = DtTable;
                                GVTotalStudentList.DataBind();

                                DivTotalStudent.Visible = true;
                                lblTotalStudent.Text = "Total Students List : " + DtTable.Rows.Count;
                            }
                            else
                            {
                                showAlert("Please Upload Excel With Proper Format");

                            }

                            lnkStudentMigrateButton.Visible = true;
                            System.IO.File.Delete(excelpath);
                        }
                    }
                    catch (Exception ex)
                    {
                        showAlert("Please select file with proper format or Change the excel sheet name with Sheet1");
                        System.IO.File.Delete(excelpath);
                    }
                }

                else
                {
                    showAlert("Please Select an Excel File with Proper Format");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkStudentMigrateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NotMigratedStudentList"] = null;
                int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                int BatchId = Convert.ToInt32(ddlBatch.SelectedValue);

                int Total = 0, NewInserted = 0;

                string Message = "";

                List<StudentNotUpload> notUploadedstudentList = new List<StudentNotUpload>();

                if (ProgramId > 0 && BatchId > 0)
                {
                    DataTable StudentList = (DataTable)Session["DataTableExcelUpload"];

                    if (StudentList != null && StudentList.Rows.Count > 0)
                    {
                        Total = StudentList.Rows.Count;

                        #region Data Migration Process


                        foreach (DataRow row in StudentList.Rows)
                        {

                            #region Data Insert into Different Table


                            StudentNotUpload stdObj = new StudentNotUpload();

                            string Institute = "", Reg = "", Roll = "", Name = "", Gender = "";

                            if (!string.IsNullOrEmpty(row[2].ToString()))
                                Institute = row[2].ToString().Trim();

                            if (!string.IsNullOrEmpty(row[2].ToString()))
                                Reg = row[2].ToString().Trim();

                            if (!string.IsNullOrEmpty(row[3].ToString()))
                                Roll = row[3].ToString().Trim();

                            if (!string.IsNullOrEmpty(row[5].ToString()))
                                Name = row[5].ToString().Trim();

                            if (!string.IsNullOrEmpty(row[6].ToString()))
                                Gender = row[6].ToString().Trim();


                            var Student = ucamEntites.Students.Where(x => x.Roll == Roll).FirstOrDefault();

                            if (Student == null) //Insert New Student
                            {
                                var RegistrationInfo = ucamEntites.StudentRegistrations.Where(x => x.RegistrationNo == Reg).FirstOrDefault();

                                if (RegistrationInfo == null)
                                {
                                    int PersonId = 0;

                                    // First Insert Into Person Table 

                                    UCAMDAL.Person newobj = new UCAMDAL.Person();

                                    newobj.FullName = Name;
                                    newobj.Gender = Gender.ToLower() == "m" ? "Male" : "Female";
                                    newobj.CreatedBy = userObj.Id;
                                    newobj.CreatedDate = DateTime.Now;

                                    ucamEntites.People.Add(newobj);
                                    ucamEntites.SaveChanges();

                                    PersonId = newobj.PersonID;

                                    if (PersonId > 0)
                                    {
                                        int StudentId = 0;

                                        LogicLayer.BusinessObjects.Batch batchInfo = BatchManager.GetById(BatchId);

                                        // Insert into Student Table

                                        UCAMDAL.Student NewStudent = new UCAMDAL.Student();

                                        NewStudent.PersonID = PersonId;
                                        NewStudent.Roll = Roll;
                                        NewStudent.ProgramID = ProgramId;
                                        NewStudent.BatchId = BatchId;
                                        NewStudent.SessionId = batchInfo.AcaCalId;
                                        NewStudent.GradeMasterID = 1; // Only One Grading System 
                                        NewStudent.IsActive = true;
                                        NewStudent.CreatedBy = userObj.Id;
                                        NewStudent.CreatedDate = DateTime.Now;

                                        ucamEntites.Students.Add(NewStudent);
                                        ucamEntites.SaveChanges();

                                        StudentId = NewStudent.StudentID;

                                        if (StudentId > 0)
                                        {
                                            // Insert into Student Registration Number Table

                                            UCAMDAL.StudentRegistration stdReg = new UCAMDAL.StudentRegistration();

                                            stdReg.StudentId = StudentId;
                                            stdReg.RegistrationNo = Reg;
                                            stdReg.SessionId = NewStudent.SessionId;
                                            stdReg.CreatedBy = userObj.Id;
                                            stdReg.CreatedDate = DateTime.Now;

                                            ucamEntites.StudentRegistrations.Add(stdReg);
                                            ucamEntites.SaveChanges();


                                            NewInserted = NewInserted + 1;
                                        }

                                    }
                                    else
                                    {
                                        stdObj.RegNo = Reg;
                                        stdObj.Roll = Roll;
                                        stdObj.Name = Name;
                                        stdObj.Reason = "Person Insert Failed";

                                        notUploadedstudentList.Add(stdObj);
                                    }
                                }
                                else
                                {
                                    stdObj.RegNo = Reg;
                                    stdObj.Roll = Roll;
                                    stdObj.Name = Name;
                                    stdObj.Reason = "Registartion Number Already Exists With Another Roll";

                                    notUploadedstudentList.Add(stdObj);
                                }
                            }
                            else // Student Already Exists.
                            {
                                stdObj.RegNo = Reg;
                                stdObj.Roll = Roll;
                                stdObj.Name = Name;
                                stdObj.Reason = "Student Roll Number Already Exists";

                                notUploadedstudentList.Add(stdObj);
                            }

                            #endregion

                        }

                        Message = "Total Students : " + Total;

                        if (NewInserted > 0)
                            Message = Message + ". New Inserted Students : " + NewInserted;

                        if (notUploadedstudentList != null && notUploadedstudentList.Count > 0)
                        {
                            Message = Message + ". Not Migrated Students : " + notUploadedstudentList.Count;

                            lblNotMigratedStudent.Text = "Not Migrated Students : " + notUploadedstudentList.Count;

                            lnkDownloadExcel.Visible = true;

                            DivNotUploadedStudent.Visible = true;

                            GVNotUploadedStudentList.DataSource = notUploadedstudentList;
                            GVNotUploadedStudentList.DataBind();

                            Session["NotMigratedStudentList"] = notUploadedstudentList;
                        }

                        if (notUploadedstudentList == null || notUploadedstudentList.Count == 0)
                        {
                            lnkViewInformation_Click(null, null);
                        }

                        showAlert(Message);

                        #endregion

                        Session["DataTableExcelUpload"] = null;

                    }
                    else
                    {
                        showAlert("No Data Found for Migration");
                        Session["DataTableExcelUpload"] = null;
                    }

                }
                else
                {
                    showAlert("Please Select Program And Batch Before Migration");
                    Session["DataTableExcelUpload"] = null;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public class StudentNotUpload
        {
            public string Institute { get; set; }
            public string RegNo { get; set; }
            public string Roll { get; set; }
            public string Name { get; set; }
            public string Reason { get; set; }
        }

        private void ClearExcelGrid()
        {
            try
            {
                GVTotalStudentList.DataSource = null;
                GVTotalStudentList.DataBind();

                GVNotUploadedStudentList.DataSource = null;
                GVNotUploadedStudentList.DataBind();

                DivTotalStudent.Visible = false;
                DivNotUploadedStudent.Visible = false;

                lnkStudentMigrateButton.Visible = false;


                lnkDownloadExcel.Visible = false;

                lblTotalStudent.Text = string.Empty;
                lblNotMigratedStudent.Text = string.Empty;


            }
            catch (Exception ex)
            {

            }
        }




        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void lnkSampleExcel_Click(object sender, EventArgs e)
        {
            try
            {

                string fileName = "SampleStudentMigrationFile.xlsx";

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

        protected void lnkDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                List<StudentNotUpload> StudentList = (List<StudentNotUpload>)Session["NotMigratedStudentList"];


                if (StudentList != null && StudentList.Count > 0)
                {
                    DataTable stdlist = ListToDataTable(StudentList);

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
                            Response.AddHeader("content-disposition", "attachment;filename=" + "NotUploadedStudentList.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);

                                //HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                                //cookie.Value = "Flag";
                                //cookie.Expires = DateTime.Now.AddDays(1);
                                //Response.AppendCookie(cookie);

                                Response.Flush();
                                Response.SuppressContent = true;
                                // Response.Close();
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


        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }
    }
}