using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Scholarship_StudentList : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            FillDropdown();

            ddlFromBatchAcaCal.Enabled = false;
            ddlToBatchAcaCal.Enabled = false;
            ddl100.Enabled = false;
            ddl50.Enabled = false;
            ddl25.Enabled = false;
        }
    }

    protected void FillDropdown()
    {
        FillCombo();
        FillProgramCombo();
        FillSchemeCombo();
    }

    private void FillCombo()
    {
        try
        {

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            if (academicCalenderList.Count > 0)
            {
                academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlAcaCal.Items.Clear();
                ddlFromBatchAcaCal.Items.Clear();
                ddlToBatchAcaCal.Items.Clear();
                ddlAcaCal.Items.Add(new ListItem("-Select-", "0"));
                ddlFromBatchAcaCal.Items.Add(new ListItem("-Select-", "0"));
                ddlToBatchAcaCal.Items.Add(new ListItem("-Select-", "0"));
                ddlAcaCal.AppendDataBoundItems = true;
                ddlFromBatchAcaCal.AppendDataBoundItems = true;
                ddlToBatchAcaCal.AppendDataBoundItems = true;

                AcademicCalender acaCal = academicCalenderList.Where(x => x.IsCurrent == true).SingleOrDefault();
                //if (acaCal != null)
                //    academicCalenderList = academicCalenderList.Where(x => x.AcademicCalenderID <= acaCal.AcademicCalenderID).ToList();

                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCal.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlFromBatchAcaCal.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlToBatchAcaCal.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }
            else
            {
                lblMsg.Text = "Error: 101(Academic Calender not load)";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: 1021";
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("-Select-", "0"));
            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillSchemeCombo()
    {
        try
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("-Select-", "0"));
            ddlScheme.AppendDataBoundItems = true;

            List<SchemeSetup> schemeSetupList = SchemeSetupManager.GetAll();
            if (schemeSetupList != null)
            {
                ddlScheme.DataSource = schemeSetupList.OrderBy(d => d.Id).ToList();
                ddlScheme.DataTextField = "SchemeName";
                ddlScheme.DataValueField = "Id";
                ddlScheme.DataBind();
            }

            ddl100.Items.Clear();
            ddl100.Items.Add(new ListItem("-Select-", "0"));
            ddl100.AppendDataBoundItems = true;
            ddl50.Items.Clear();
            ddl50.Items.Add(new ListItem("-Select-", "0"));
            ddl50.AppendDataBoundItems = true;
            ddl25.Items.Clear();
            ddl25.Items.Add(new ListItem("-Select-", "0"));
            ddl25.AppendDataBoundItems = true;
            for (int i = 1; i <= 100; i++)
            {
                ddl100.Items.Add(new ListItem(i + " %", i.ToString()));
                ddl50.Items.Add(new ListItem(i + " %", i.ToString()));
                ddl25.Items.Add(new ListItem(i + " %", i.ToString()));
            }
        }
        catch { }
        finally { }
    }

    public void DownloadFile(FileInfo file)
    {
        if (file.Name.Length > 0)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
    }

    private void LoadScholarship()
    {
        try
        {
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderBy(x => x.AcademicCalenderID).ToList();

            int acaCalId = Convert.ToInt32(ddlAcaCal.SelectedValue);
            string programCode = ddlProgram.SelectedValue;
            string[] tempFromBatch = ddlFromBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string fromBatch = tempFromBatch[0];
            string[] tempToBatch = ddlToBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string toBatch = tempToBatch[0];
            string fileName = (ddlAcaCal.SelectedItem.Text + ddlProgram.SelectedItem.Text + "(" + fromBatch + "-" + toBatch + ")").Replace(' ', '_');

            AcademicCalender academicCalenderFrom = academicCalenderList[0];
            fromBatch = academicCalenderFrom.Code;
            academicCalenderList = academicCalenderList.Where(x => x.AcademicCalenderID < acaCalId).OrderByDescending(x => x.AcademicCalenderID).ToList();
            AcademicCalender academicCalenderTo = academicCalenderList[0];

            List<ScholarshipList> scholarshipList = ScholarshipListManager.GetAllByParameter(acaCalId, programCode, fromBatch, toBatch);

            if (scholarshipList.Count > 0 && scholarshipList != null)
            {
                scholarshipList = scholarshipList.Where(x => x.CalculateScholarship != null).ToList();
                if (scholarshipList.Count > 0 && scholarshipList != null)
                    scholarshipList = scholarshipList.OrderByDescending(x => Convert.ToInt32(x.ManualScholarship)).ThenByDescending(x => x.GPA).ThenBy(x => x.Roll).ToList();

                gvScholarship.DataSource = scholarshipList;
            }
            gvScholarship.DataBind();

        }
        catch { }
        finally { }
    }

    private void CalculateScholarship()
    {
        try
        {
            List<ScholarshipList> finalScholarshipStudnetList = null;

            string[] tempBatch = ddlAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string batch = tempBatch[0];

            int acaCalId = Convert.ToInt32(ddlAcaCal.SelectedValue);
            string programCode = ddlProgram.SelectedValue;
            string[] tempFromBatch = ddlFromBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string fromBatch = tempFromBatch[0];
            string[] tempToBatch = ddlToBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string toBatch = tempToBatch[0];
            string fileName = (ddlAcaCal.SelectedItem.Text + ddlProgram.SelectedItem.Text + "(" + fromBatch + "-" + toBatch + ")").Replace(' ', '_');

            List<ScholarshipList> scholarshipList = ScholarshipListManager.GetAllByParameter(acaCalId, programCode, fromBatch, toBatch);

            if (scholarshipList.Count > 0 && scholarshipList != null)
            {
                scholarshipList = scholarshipList.Where(x => x.GPA != Convert.ToDecimal(0.0)).ToList();
                if (scholarshipList.Count > 0 && scholarshipList != null)
                {
                    decimal totalCredit = scholarshipList.Sum(x => x.Credit);

                    scholarshipList = scholarshipList.Where(x => (x.PassCredit >= 9 && x.GPA >= Convert.ToDecimal(3.5)) || (x.PassCredit >= 7 && x.GPA >= Convert.ToDecimal(3.5) && batch == x.Roll.Substring(3, 3))).ToList();
                    scholarshipList = scholarshipList.OrderBy(x => x.Id).ToList();

                    if (scholarshipList.Count > 0 && scholarshipList != null)
                    {
                        lblTotalCredit.Text = totalCredit + " Cr(s)";
                        decimal percentage100 = (Convert.ToDecimal(ddl100.SelectedValue) * totalCredit) / Convert.ToDecimal(100);
                        decimal percentage50 = (Convert.ToDecimal(ddl50.SelectedValue) * totalCredit) / Convert.ToDecimal(100);
                        decimal percentage25 = (Convert.ToDecimal(ddl25.SelectedValue) * totalCredit) / Convert.ToDecimal(100);

                        lblNoOfStudent100.Text = percentage100 + " Cr(s)";
                        lblNoOfStudent50.Text = percentage50 + " Cr(s)";
                        lblNoOfStudent25.Text = percentage25 + " Cr(s)";

                        decimal tempPercentage100 = 0, tempPercentage50 = 0, tempPercentage25 = 0;
                        int flag = 1;
                        finalScholarshipStudnetList = new List<ScholarshipList>();
                        foreach (ScholarshipList singleScholarship in scholarshipList)
                        {
                            if (percentage100 > tempPercentage100 && (percentage100 - tempPercentage100) >= singleScholarship.RegisterCredit && flag == 1)
                            {
                                if (singleScholarship.RegisterCredit > 13)
                                    tempPercentage100 += 13;
                                else
                                    tempPercentage100 += singleScholarship.RegisterCredit;

                                singleScholarship.CalculateScholarship = "100";
                                singleScholarship.ManualScholarship = "100";
                                finalScholarshipStudnetList.Add(singleScholarship);
                            }
                            else if(flag == 1)
                                flag = 2;

                            if (percentage50 > tempPercentage50 && (percentage50 - tempPercentage50) >= singleScholarship.RegisterCredit && flag == 2)
                            {
                                if (singleScholarship.RegisterCredit > 13)
                                    tempPercentage50 += 13;
                                else
                                    tempPercentage50 += singleScholarship.RegisterCredit;

                                singleScholarship.CalculateScholarship = "50";
                                singleScholarship.ManualScholarship = "50";
                                finalScholarshipStudnetList.Add(singleScholarship);
                            }
                            else if (flag == 2)
                                flag = 3;

                            if (percentage25 > tempPercentage25 && (percentage25 - tempPercentage25) >= singleScholarship.RegisterCredit && flag == 3)
                            {
                                if (singleScholarship.RegisterCredit > 13)
                                    tempPercentage25 += 13;
                                else
                                    tempPercentage25 += singleScholarship.RegisterCredit;

                                singleScholarship.CalculateScholarship = "25";
                                singleScholarship.ManualScholarship = "25";
                                finalScholarshipStudnetList.Add(singleScholarship);
                            }
                            else if (flag == 3)                                
                                flag = 4;

                            if (flag == 4)
                                break;

                            singleScholarship.ModifiedBy = 100;
                            singleScholarship.ModifiedDate = DateTime.Now;
                            bool resultUpdate = ScholarshipListManager.Update(singleScholarship);
                        }
                        lblApply100.Text = tempPercentage100 + " Cr(s)";
                        lblApply50.Text = tempPercentage50 + " Cr(s)";
                        lblApply25.Text = tempPercentage25 + " Cr(s)";
                    }
                }
            }
            LoadScholarship();
        }
        catch { }
        finally { }
    }

    #endregion

    #region Event

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcaCal.SelectedValue);
            string programCode = ddlProgram.SelectedValue;

            List<ScholarshipList> scholarshipList = ScholarshipListManager.GetAllByAcaCalProg(acaCalId, programCode);
            if (scholarshipList.Count > 0 && scholarshipList != null)
            {
                scholarshipList = scholarshipList.Where(x => x.CalculateScholarship != null).ToList();

                if (scholarshipList.Count > 0 && scholarshipList != null)
                    scholarshipList = scholarshipList.OrderByDescending(x => Convert.ToInt32(x.ManualScholarship)).ThenByDescending(x => x.GPA).ThenBy(x => x.Roll).ToList();
            }
            gvScholarship.DataSource = scholarshipList;
            gvScholarship.DataBind();
        }
        catch { }
        finally { }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        int flagExcept = 0;
        FileInfo newFile = null;
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcaCal.SelectedValue);
            string programCode = ddlProgram.SelectedValue;
            string[] tempFromBatch = ddlFromBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string fromBatch = tempFromBatch[0];
            string[] tempToBatch = ddlToBatchAcaCal.SelectedItem.Text.Remove(0, 1).Split(']');
            string toBatch = tempToBatch[0];
            string fileName = (ddlAcaCal.SelectedItem.Text + ddlProgram.SelectedItem.Text + "(" + fromBatch + "-" + toBatch + ")").Replace(' ', '_');

            List<ScholarshipList> scholarshipList = ScholarshipListManager.GetAll(acaCalId, programCode, fromBatch, toBatch);

            if (scholarshipList.Count > 0 && scholarshipList != null)
            {
                flagExcept = 1;

                FileInfo template = new FileInfo(HttpContext.Current.Server.MapPath("~/Excel_File_Templete/StudentListTemplete.xlsx"));
                string path = @"D:\Scholarship_List\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                newFile = new FileInfo(@"D:\Scholarship_List\" + fileName + ".xlsx");
                using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
                {
                    ExcelWorkbook myWorkbook = excelPackage.Workbook;
                    ExcelWorksheet studentScholarshipList = myWorkbook.Worksheets["List"];

                    int index = 1;
                    foreach (ScholarshipList scholarship in scholarshipList)
                    {
                        studentScholarshipList.Cells[index + 1, 1].Value = index.ToString();
                        studentScholarshipList.Cells[index + 1, 2].Value = scholarship.Roll.ToString() + " ";
                        studentScholarshipList.Cells[index + 1, 3].Value = scholarship.Name.ToString();
                        studentScholarshipList.Cells[index + 1, 4].Value = scholarship.GPA.ToString();
                        studentScholarshipList.Cells[index + 1, 5].Value = scholarship.Credit.ToString();
                        studentScholarshipList.Cells[index + 1, 6].Value = scholarship.PassCredit.ToString();

                        index++;
                    }
                    excelPackage.Save();
                }
            }
        }
        catch (Exception ex)
        {
            flagExcept = 2;
            lblMsg.Text = "Error: 101" + ex.Message;
        }
        finally
        {
            if (flagExcept == 1)
            {
                CalculateScholarship();
                //DownloadFile(newFile);
            }
        }
    }

    protected void ddlScheme_Select(object sender, EventArgs e)
    {
        try
        {
            ddlFromBatchAcaCal.SelectedValue = "0";
            ddlToBatchAcaCal.SelectedValue = "0";
            int schemeIndex = Convert.ToInt32(ddlScheme.SelectedValue);
            SchemeSetup schemeSetup = SchemeSetupManager.GetById(schemeIndex);

            ddl100.SelectedValue = schemeSetup.Percentage100.ToString();
            ddl50.SelectedValue = schemeSetup.Percentage50.ToString();
            ddl25.SelectedValue = schemeSetup.Percentage25.ToString();

            if (schemeSetup.FromBatch != null)
                ddlFromBatchAcaCal.SelectedValue = schemeSetup.FromBatch.ToString();
            if (schemeSetup.ToBatch != null)
                ddlToBatchAcaCal.SelectedValue = schemeSetup.ToBatch.ToString();
            if (schemeSetup.ToBatch == null)
                ddlToBatchAcaCal.SelectedValue = ddlAcaCal.SelectedValue;
        }
        catch { }
        finally { }
    }

    protected void gvDiscountContinuationSetup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            gvScholarship.EditIndex = e.NewEditIndex;

            if (Convert.ToInt32(ddlScheme.SelectedValue) != 0)
                LoadScholarship();
            else
                btnView_Click(null, null);
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvScholarship.EditIndex = -1;

            if (Convert.ToInt32(ddlScheme.SelectedValue) != 0)
                LoadScholarship();
            else
                btnView_Click(null, null);
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvScholarship.Rows[e.RowIndex];

            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            int id = Convert.ToInt32(hfId.Value);
            string manualScholarship = ((TextBox)(row.Cells[8].Controls[0])).Text;

            ScholarshipList scholarship = ScholarshipListManager.GetById(id);
            scholarship.ManualScholarship = manualScholarship;            

            bool resultScholarship = ScholarshipListManager.Update(scholarship);
            if (resultScholarship)
            {
                gvScholarship.EditIndex = -1;
                if (Convert.ToInt32(ddlScheme.SelectedValue) != 0)
                    LoadScholarship();
                else
                    btnView_Click(null, null);
            }
        }
        catch { }
    }

    protected void btnScholarshipBillPosting_Click(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcaCal.SelectedValue);
            string programCode = ddlProgram.SelectedValue;
            List<Program> programList = ProgramManager.GetAll();
            List<AcademicCalender> acaCalList = AcademicCalenderManager.GetAll().Where(x => x.AcademicCalenderID > acaCalId).ToList();
            List<AcademicCalender> tempAcaCalList = acaCalList.Where(x => x.AcademicCalenderID > acaCalId).ToList();
            int semesterAcaCalId = tempAcaCalList.Min(x => x.AcademicCalenderID);

            List<ScholarshipList> scholarshipList = ScholarshipListManager.GetAllByAcaCalProg(acaCalId, programCode);
            if (scholarshipList.Count > 0 && scholarshipList != null)
            {
                scholarshipList = scholarshipList.Where(x => x.CalculateScholarship != null).ToList();
                List<TypeDefinition> typeDefinitionList = TypeDefinitionManager.GetAll();
                TypeDefinition typeDefinition = typeDefinitionList.Where(x => x.Definition.Contains("Scholarship Pre-Trimester Result")).SingleOrDefault();
                foreach (ScholarshipList scholarship in scholarshipList)
                {
                    int insertMaster = 0;
                    Student student = StudentManager.GetByRoll(scholarship.Roll);
                    if (student != null)
                    {
                        StudentDiscountMaster studentDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
                        if (studentDiscountMaster == null)
                        {
                            List<AcademicCalender> tempAcaCalBatchList = acaCalList.Where(x => x.Code == student.Roll.Substring(3, 3)).ToList();
                            AcademicCalender batchAcaCal = acaCalList[0];


                            List<Program> tempProgramList = programList.Where(x => x.Code == student.Roll.Substring(0, 3)).ToList();
                            Program program = tempProgramList[0];

                            StudentDiscountMaster tempStudentDiscountMaster = new StudentDiscountMaster();
                            tempStudentDiscountMaster.StudentId = student.StudentID;
                            tempStudentDiscountMaster.BatchId = batchAcaCal.AcademicCalenderID;
                            tempStudentDiscountMaster.ProgramId = program.ProgramID;
                            tempStudentDiscountMaster.Remarks = "Inserted by Scholarship";
                            tempStudentDiscountMaster.CreatedBy = -1;
                            tempStudentDiscountMaster.CreatedDate = DateTime.Now;
                            tempStudentDiscountMaster.ModifiedBy = -1;
                            tempStudentDiscountMaster.ModifiedDate = DateTime.Now;

                            insertMaster = StudentDiscountMasterManager.Insert(tempStudentDiscountMaster);

                            StudentDiscountCurrentDetails studentDiscountInitialDetails = new StudentDiscountCurrentDetails();
                            studentDiscountInitialDetails.StudentDiscountId = insertMaster;
                            studentDiscountInitialDetails.TypeDefinitionId = typeDefinition.TypeDefinitionID;
                            studentDiscountInitialDetails.TypePercentage = Convert.ToDecimal(scholarship.ManualScholarship);
                            studentDiscountInitialDetails.AcaCalSession = semesterAcaCalId;

                            List<StudentDiscountCurrentDetails> studentDiscountInitialDetailsList = StudentDiscountCurrentDetailsManager.GetByStudentDiscountId(studentDiscountInitialDetails.StudentDiscountId);
                            if (studentDiscountInitialDetailsList.Count > 0 && studentDiscountInitialDetailsList != null)
                            {
                                StudentDiscountCurrentDetails tempStudentDiscountInitialDetails = studentDiscountInitialDetailsList.Where(x => x.TypeDefinitionId == typeDefinition.TypeDefinitionID && x.AcaCalSession == semesterAcaCalId).SingleOrDefault();
                                if (tempStudentDiscountInitialDetails != null)
                                {
                                    tempStudentDiscountInitialDetails.TypePercentage = Convert.ToDecimal(scholarship.ManualScholarship);
                                    bool resultUpdate = StudentDiscountCurrentDetailsManager.Update(tempStudentDiscountInitialDetails);
                                }
                            }
                            else
                            {
                                int resultInsert = StudentDiscountCurrentDetailsManager.Insert(studentDiscountInitialDetails);
                            }
                        }
                        else if (studentDiscountMaster != null)
                        {
                            StudentDiscountCurrentDetails studentDiscountInitialDetails = new StudentDiscountCurrentDetails();
                            studentDiscountInitialDetails.StudentDiscountId = studentDiscountMaster.StudentDiscountId;
                            studentDiscountInitialDetails.TypeDefinitionId = typeDefinition.TypeDefinitionID;
                            studentDiscountInitialDetails.TypePercentage = Convert.ToDecimal(scholarship.ManualScholarship);
                            studentDiscountInitialDetails.AcaCalSession = semesterAcaCalId;

                            List<StudentDiscountCurrentDetails> studentDiscountInitialDetailsList = StudentDiscountCurrentDetailsManager.GetByStudentDiscountId(studentDiscountInitialDetails.StudentDiscountId);
                            if (studentDiscountInitialDetailsList.Count > 0 && studentDiscountInitialDetailsList != null)
                            {
                                StudentDiscountCurrentDetails tempStudentDiscountInitialDetails = studentDiscountInitialDetailsList.Where(x => x.TypeDefinitionId == typeDefinition.TypeDefinitionID && x.AcaCalSession == semesterAcaCalId).SingleOrDefault();
                                if (tempStudentDiscountInitialDetails != null)
                                {
                                    tempStudentDiscountInitialDetails.TypePercentage = Convert.ToDecimal(scholarship.ManualScholarship);
                                    bool resultUpdate = StudentDiscountCurrentDetailsManager.Update(tempStudentDiscountInitialDetails);
                                }
                            }
                            else
                            {
                                int resultInsert = StudentDiscountCurrentDetailsManager.Insert(studentDiscountInitialDetails);
                            }
                        }
                    }
                }
            }
        }
        catch { }
    }

    #endregion
}