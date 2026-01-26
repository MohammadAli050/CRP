using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
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

namespace EMS.StudentManagement
{
    public partial class StudentDiscountWorksheet : BasePage
    {
        private List<Program> _programs = null;
        private List<AcademicCalender> _trimesterInfos = null;
        private List<Student> _students = null;
        private List<TypeDefinition> _typeDefsDis = null;
        private List<StudentDiscountWorksheetEntity> _stdDiscountWorksheets = null;

        #region Session Consts
        private const string SESSIONTYPEDEFDIS = "TYPE_DEF_DIS_SDW";
        private const string SESSIONTABLE = "DATA_TABLE_SDW";
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }

        protected void Page_Init()
        {
            gvStudentDiscountWorksheet.DataSource = null;
            gvStudentDiscountWorksheet.DataBind();
            gvStudentDiscountWorksheet.Columns.Clear();

            if (!IsSessionVariableExists(SESSIONTABLE) || !IsSessionVariableExists(SESSIONTYPEDEFDIS))
            {
                return;
            }

            DataTable dt = (DataTable)GetFromSession(SESSIONTABLE);
            List<TypeDefinition> typeDefsDis = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFDIS);

            foreach (DataColumn col in dt.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                TemplateField bfield = new TemplateField();

                ////Initialize the HeaderText field value.
                //bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName == "Remarks")
                {
                    bfield.HeaderStyle.Width = 50;
                }
                else
                {
                    bfield.HeaderStyle.Width = 20;
                }

                //Initalize the DataField value.
                bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);

                //Add the newly created bound field to the GridView.
                gvStudentDiscountWorksheet.Columns.Add(bfield);
            }

            gvStudentDiscountWorksheet.DataSource = dt;
            gvStudentDiscountWorksheet.DataBind();

            gvStudentDiscountWorksheet.Columns[0].Visible = false;
            gvStudentDiscountWorksheet.Columns[3].Visible = false;
            gvStudentDiscountWorksheet.Columns[4].Visible = false;
            gvStudentDiscountWorksheet.Columns[5].Visible = false;

            foreach (TypeDefinition td in typeDefsDis)
            {
                foreach (DataControlField col in gvStudentDiscountWorksheet.Columns)
                {
                    if (col.HeaderText == td.Definition + "ID")
                    {
                        gvStudentDiscountWorksheet.Columns[gvStudentDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                    }
                }
            }

            if (IsSessionVariableExists(SESSIONTYPEDEFDIS))
            {
                RemoveFromSession(SESSIONTYPEDEFDIS);
            }
            AddToSession(SESSIONTYPEDEFDIS, typeDefsDis);

            //if (IsSessionVariableExists(SESSIONTYPEDEFFEE))
            //{
            //    RemoveFromSession(SESSIONTYPEDEFFEE);
            //}
            //AddToSession(SESSIONTYPEDEFFEE, _typeDefsFee);

            if (IsSessionVariableExists(SESSIONTABLE))
            {
                RemoveFromSession(SESSIONTABLE);
            }
            AddToSession(SESSIONTABLE, dt);
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

                if (!IsPostBack)
                {
                    FillProgCombo();
                    FillAcademicCalenderCombo();
                    LoadStudentCombo(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
                    CleareSession();
                }
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        private void CleareSession()
        {
            RemoveFromSession(SESSIONTYPEDEFDIS);
            RemoveFromSession(SESSIONTABLE);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _typeDefsDis = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFDIS);
                List<StudentDiscountWorksheetEntity> sdwEntities = new List<StudentDiscountWorksheetEntity>();

                for (int i = 0; i < gvStudentDiscountWorksheet.Rows.Count; i++)
                {
                    #region Map data for discount

                    foreach (TypeDefinition td in _typeDefsDis)
                    {
                        StudentDiscountWorksheetEntity sdwEntity = new StudentDiscountWorksheetEntity();
                        foreach (DataControlField col in gvStudentDiscountWorksheet.Columns)
                        {
                            int index = -1;
                            if (col.HeaderText == "StudentID")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.StudentId = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "Roll")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.Roll = txtBox.Text.Trim();
                            }
                            else if (col.HeaderText == "AcaCalSectionID")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.Name = txtBox.Text.Trim();
                            }
                            else if (col.HeaderText == "ProgramId")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.ProgramId = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "AcaCalId")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.AcacalId = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "AdmissionCalId")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.AdmissionCalId = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "TCrPrev")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.TotalCrRegInpreviousSession = Decimal.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "GPAPrev")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.GpaInPreviousSession = Decimal.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "CGPAPrev")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.CgpaUptoPreviousSession = Decimal.Parse(txtBox.Text.Trim());
                            }

                            else if (col.HeaderText == "TCrCur")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.TotalCrRegInCurrentSession = Decimal.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == td.Definition + "ID")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.DiscountTypeId = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == td.Definition + "%")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    sdwEntity.Discountpercentage = Convert.ToDecimal(txtBox.Text.Trim());
                                }
                            }
                            else if (col.HeaderText == "Remarks")
                            {
                                index = gvStudentDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvStudentDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                sdwEntity.Remarks = txtBox.Text.Trim();
                            }
                        }
                        sdwEntity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                        sdwEntity.CreatedDate = DateTime.Now;

                        sdwEntities.Add(sdwEntity);
                    }

                    #endregion
                }

                if (StudentDiscountWorkSheet_BAO.Save(sdwEntities) != 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Saved Successfully");
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Not Saved");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentCombo(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
        }

        private void FillProgCombo()
        {
            ddlProgram.Items.Clear();

            _programs = Program.GetPrograms();

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

                //if (Session["Programs"] != null)
                //{
                //    Session.Remove("Programs");
                //}
                //Session.Add("Programs", _programs);

                ddlProgram.SelectedIndex = 0;
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

                ListItem itemBlank = new ListItem();
                itemBlank.Value = "0";
                itemBlank.Text = "All";
                ddlBatch.Items.Add(itemBlank);

                foreach (AcademicCalender ac in _trimesterInfos)
                {
                    if (ac.IsCurrent)
                    {
                        currentTrimester = ac.Id.ToString();
                    }

                    ListItem lei = new ListItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    ddlBatch.Items.Add(lei);
                }

                //if (Session[SESSIONCURRENTTRIMESTER] != null)
                //{
                //    Session.Remove(SESSIONCURRENTTRIMESTER);
                //}
                //Session[SESSIONCURRENTTRIMESTER] = currentTrimester;

                //ddlAcaCalender.SelectedValue = currentTrimester;
                ddlProgram.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }

        private void LoadStudentCombo(int programId, int batchId)
        {
            ddlStudent.Items.Clear();

            _students = Student.GetStudentsByBatch(programId, batchId);

            if (_students != null)
            {
                ListItem itemBlank = new ListItem();
                itemBlank.Value = "0";
                itemBlank.Text = "All";
                ddlStudent.Items.Add(itemBlank);

                foreach (Student student in _students)
                {
                    ListItem item = new ListItem();
                    item.Value = student.Id.ToString();
                    item.Text = student.Roll + " - " + student.StdName;
                    ddlStudent.Items.Add(item);
                }

                ddlStudent.SelectedIndex = 0;
            }
        }

        protected void btnGenerateSaveLoad_Click(object sender, EventArgs e)
        {
            GenerateAndSaveStudentDiscount();
            LoadStudentDiscount();
        }

        private void GenerateAndSaveStudentDiscount()
        {
            int effectedRow = 0;
            try
            {
                effectedRow = StudentDiscountWorkSheet_BAO.Generate(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlStudent.SelectedValue));
                if (effectedRow > 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Generated...");
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Failed to generate...");
                }
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        private void LoadStudentDiscount()
        {
            gvStudentDiscountWorksheet.DataSource = null;
            gvStudentDiscountWorksheet.DataBind();
            gvStudentDiscountWorksheet.Columns.Clear();
            lblMsg.Text = string.Empty;

            _stdDiscountWorksheets = StudentDiscountWorkSheet_BAO.LoadData(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), ddlStudent.SelectedValue.ToString() == "" ? 0 : Convert.ToInt32(ddlStudent.SelectedValue));

            if (_stdDiscountWorksheets == null)
            {
                return;
            }

            #region Making Table

            _typeDefsDis = TypeDefinition.GetTypes("Discount");
            if (_typeDefsDis == null || _typeDefsDis.Count == 0)
            {
                return;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
            dt.Columns.Add(new DataColumn("Roll", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("ProgramId", typeof(string)));
            dt.Columns.Add(new DataColumn("AcaCalId", typeof(string)));
            dt.Columns.Add(new DataColumn("AdmissionCalId", typeof(string)));
            dt.Columns.Add(new DataColumn("TCrPrev", typeof(string)));
            dt.Columns.Add(new DataColumn("GPAPrev", typeof(string)));
            dt.Columns.Add(new DataColumn("CGPAPrev", typeof(string)));
            dt.Columns.Add(new DataColumn("TCrCur", typeof(string)));
            //dt.Columns.Add(new DataColumn("TotalCrRegInPreviousSession", typeof(string)));
            //dt.Columns.Add(new DataColumn("GpaInPreviousSession", typeof(string)));
            //dt.Columns.Add(new DataColumn("CgpaUptoPreviousSession", typeof(string)));
            //dt.Columns.Add(new DataColumn("TotalCrRegInCurrentSession", typeof(string)));

            #region make discount column
            foreach (TypeDefinition td in _typeDefsDis)
            {
                dt.Columns.Add(new DataColumn(td.Definition + "ID", typeof(int)));
                dt.Columns.Add(new DataColumn(td.Definition + "%", typeof(decimal)));
            }
            #endregion

            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

            #endregion

            #region Bind data to rows
            bool flag = false;
            int n = 0;
            DataRow row = null;

            foreach (StudentDiscountWorksheetEntity sdwe in _stdDiscountWorksheets)
            {
                if (n == sdwe.StudentId)
                {
                    flag = true;
                }
                else if (flag)
                {
                    dt.Rows.Add(row);//add row after all column has filed
                    flag = false;
                }

                n = sdwe.StudentId;

                if (!flag)
                {
                    row = dt.NewRow();
                    row["StudentID"] = sdwe.StudentId;
                    row["Roll"] = sdwe.Roll;
                    row["Name"] = sdwe.Name;
                    row["ProgramId"] = sdwe.ProgramId;
                    row["AcaCalId"] = sdwe.AcacalId;
                    row["AdmissionCalId"] = sdwe.AdmissionCalId;
                    row["TCrPrev"] = sdwe.TotalCrRegInpreviousSession;
                    row["GPAPrev"] = sdwe.GpaInPreviousSession;
                    row["CGPAPrev"] = sdwe.CgpaUptoPreviousSession;
                    row["TCrCur"] = sdwe.TotalCrRegInCurrentSession;
                    row["Remarks"] = sdwe.Remarks;
                    flag = true;
                }

                foreach (TypeDefinition td in _typeDefsDis)
                {
                    if (td.Id == sdwe.DiscountTypeId)
                    {
                        row[td.Definition + "ID"] = sdwe.DiscountTypeId;
                        row[td.Definition + "%"] = sdwe.Discountpercentage;
                    }
                }
                // dt.Rows.Add(row); 
            }
            dt.Rows.Add(row); // add last row.

            if (dt.Rows.Count == 0)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
                return;
            }
            #endregion

            #region Create dynamic template grid
            foreach (DataColumn col in dt.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                TemplateField bfield = new TemplateField();

                ////Initialize the HeaderText field value.
                //bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName == "Remarks")
                {
                    bfield.HeaderStyle.Width = 50;
                }
                else
                {
                    bfield.HeaderStyle.Width = 20;
                }
                //Initalize the DataField value.
                bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);

                //Add the newly created bound field to the GridView.
                gvStudentDiscountWorksheet.Columns.Add(bfield);
            }
            #endregion

            #region Hide column
            gvStudentDiscountWorksheet.DataSource = dt;
            gvStudentDiscountWorksheet.DataBind();

            gvStudentDiscountWorksheet.Columns[0].Visible = false;
            gvStudentDiscountWorksheet.Columns[3].Visible = false;
            gvStudentDiscountWorksheet.Columns[4].Visible = false;
            gvStudentDiscountWorksheet.Columns[5].Visible = false;

            foreach (TypeDefinition td in _typeDefsDis)
            {
                foreach (DataControlField col in gvStudentDiscountWorksheet.Columns)
                {
                    if (col.HeaderText == td.Definition + "ID")
                    {
                        gvStudentDiscountWorksheet.Columns[gvStudentDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                    }
                }
            }
            #endregion

            if (IsSessionVariableExists(SESSIONTYPEDEFDIS))
            {
                RemoveFromSession(SESSIONTYPEDEFDIS);
            }
            AddToSession(SESSIONTYPEDEFDIS, _typeDefsDis);

            //if (IsSessionVariableExists(SESSIONTYPEDEFFEE))
            //{
            //    RemoveFromSession(SESSIONTYPEDEFFEE);
            //}
            //AddToSession(SESSIONTYPEDEFFEE, _typeDefsFee);

            if (IsSessionVariableExists(SESSIONTABLE))
            {
                RemoveFromSession(SESSIONTABLE);
            }
            AddToSession(SESSIONTABLE, dt);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LoadStudentDiscount();
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBatch.SelectedIndex = 0;
            LoadStudentCombo(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
        }
    }
}
