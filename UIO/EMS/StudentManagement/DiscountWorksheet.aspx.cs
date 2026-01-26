using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;
using System.Data;

namespace EMS.StudentManagement
{
    public partial class DiscountWorksheet : BasePage
    {
        #region Variables
        private List<DiscountWorksheetEntity> _discountWorksheets = null;
        private List<TypeDefinition> _typeDefsDis = null;
        private List<TypeDefinition> _typeDefsFee = null;
        private List<Program> _programs = null;
        private List<AcademicCalender> _trimesterInfos = null;
        private List<Student> _students = null;
        private List<Course> _courses = null;
        #endregion

        #region Session Consts
        private const string SESSIONTYPEDEFDIS = "TYPE_DEF_DIS";
        private const string SESSIONTYPEDEFFEE = "TYPE_DEF_FEE";
        private const string SESSIONTABLE = "DATA_TABLE";
        private const string SESSIONCURRENTTRIMESTER = "CurrentTrimester";
        #endregion

        #region Events
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }

        protected void Page_Init()
        {
            gvDiscountWorksheet.DataSource = null;
            gvDiscountWorksheet.DataBind();
            gvDiscountWorksheet.Columns.Clear();

            if (!IsSessionVariableExists(SESSIONTABLE) || !IsSessionVariableExists(SESSIONTYPEDEFDIS))
            {
                return;
            }

            DataTable dt = (DataTable)GetFromSession(SESSIONTABLE);
            List<TypeDefinition> typeDefsDis = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFDIS);
            List<TypeDefinition> typeDefsFee = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFFEE);

            foreach (DataColumn col in dt.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                TemplateField bfield = new TemplateField();

                ////Initialize the HeaderText field value.
                //bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);
                bfield.HeaderText = col.ColumnName;
                bfield.HeaderStyle.Width = 100;

                //Initalize the DataField value.
                bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);

                //Add the newly created bound field to the GridView.
                gvDiscountWorksheet.Columns.Add(bfield);
            }

            gvDiscountWorksheet.DataSource = dt;
            gvDiscountWorksheet.DataBind();

            gvDiscountWorksheet.Columns[0].Visible = false;
            gvDiscountWorksheet.Columns[2].Visible = false;
            gvDiscountWorksheet.Columns[3].Visible = false;
            gvDiscountWorksheet.Columns[4].Visible = false;
            gvDiscountWorksheet.Columns[5].Visible = false;
            gvDiscountWorksheet.Columns[6].Visible = false;
            gvDiscountWorksheet.Columns[7].Visible = false;

            gvDiscountWorksheet.Columns[13].Visible = false;
            gvDiscountWorksheet.Columns[14].Visible = false;

            gvDiscountWorksheet.Columns[14].Visible = false;
            gvDiscountWorksheet.Columns[15].Visible = false;

            foreach (TypeDefinition td in typeDefsDis)
            {
                foreach (DataControlField col in gvDiscountWorksheet.Columns)
                {
                    if (col.HeaderText == td.Definition + "ID")
                    {
                        gvDiscountWorksheet.Columns[gvDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                    }
                }
            }

            foreach (TypeDefinition td in typeDefsFee)
            {
                foreach (DataControlField col in gvDiscountWorksheet.Columns)
                {
                    if (col.HeaderText == td.Definition + "ID")
                    {
                        gvDiscountWorksheet.Columns[gvDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                    }
                }
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

                if (!IsPostBack)
                {
                    FillProgCombo();
                    FillAcademicCalenderCombo();
                    CleareSession();
                }
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleareSession();
            CleareGrid();
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStuCombo(Convert.ToInt32(ddlProgram.SelectedValue));
            CleareSession();
            CleareGrid();
        }

        protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCourseCombo(Convert.ToInt32(ddlStudent.SelectedValue));
            CleareSession();
            CleareGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleareSession();
            CleareCombo();
            CleareGrid();
        } 
        #endregion

        #region Methods
        private void FillCourseCombo(int studentId)
        {
            ddlCourse.Items.Clear();

            _courses = Course.GetCoursesByStudentId(studentId);

            if (_courses != null)
            {
                ListItem itemBlank = new ListItem();
                itemBlank.Value = "0";
                itemBlank.Text = "All";
                ddlCourse.Items.Add(itemBlank);

                foreach (Course course in _courses)
                {
                    ListItem item = new ListItem();
                    item.Value = course.Id.ToString() + "," + course.VersionID.ToString();
                    item.Text = course.FormalCode + "-" + course.Title;
                    ddlCourse.Items.Add(item);
                }

                ddlCourse.SelectedIndex = 0;
            }
        }

        private string[] SplitValues(string str)
        {
            return str.Split(new char[] { ',', '-' });
        }

        private void SaveData()
        {
            try
            {
                _typeDefsDis = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFDIS);
                _typeDefsFee = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEFFEE);

                List<DiscountWorksheetEntity> dwEntities = new List<DiscountWorksheetEntity>();

                for (int i = 0; i < gvDiscountWorksheet.Rows.Count; i++)
                {
                    #region Map data for discount
                    foreach (TypeDefinition td in _typeDefsDis)
                    {
                        DiscountWorksheetEntity dwEntity = new DiscountWorksheetEntity();
                        foreach (DataControlField col in gvDiscountWorksheet.Columns)
                        {
                            int index = -1;
                            if (col.HeaderText == "StudentID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.StudentID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "CalCourseProgNodeID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.CalCourseProgNodeID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "AcaCalSectionID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.AcaCalSectionID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "SectionTypeID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.SectionTypeID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "AcaCalID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.AcaCalID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "CourseID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.CourseID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "VersionID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.VersionID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "RetakeNo")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.RetakeNo = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == "PreviousBestGrade")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.PreviousBestGrade = txtBox.Text.Trim().ToString();
                            }

                            else if (col.HeaderText == "ProgramID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.ProgramID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == td.Definition + "ID")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.DiscountTypeID = Int32.Parse(txtBox.Text.Trim());
                            }
                            else if (col.HeaderText == td.Definition + "%")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    dwEntity.DiscountPercentage = Convert.ToDecimal(txtBox.Text.Trim());
                                }
                            }
                            else if (col.HeaderText == "Remarks")
                            {
                                index = gvDiscountWorksheet.Columns.IndexOf(col);
                                TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                dwEntity.Remarks = txtBox.Text.Trim();
                            }
                        }
                        dwEntity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                        dwEntity.CreatedDate = DateTime.Now;

                        dwEntities.Add(dwEntity);
                    }
                    #endregion

                    #region Map data for Fee
                    foreach (TypeDefinition td in _typeDefsFee)
                    {
                        if (td.Type == "Fee_PCA") // Manual check
                        {
                            DiscountWorksheetEntity dwEntity = new DiscountWorksheetEntity();
                            foreach (DataControlField col in gvDiscountWorksheet.Columns)
                            {
                                int index = -1;
                                if (col.HeaderText == "StudentID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.StudentID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "CalCourseProgNodeID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.CalCourseProgNodeID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "AcaCalSectionID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.AcaCalSectionID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "SectionTypeID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.SectionTypeID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "AcaCalID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.AcaCalID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "CourseID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.CourseID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "VersionID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.VersionID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "RetakeNo")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.RetakeNo = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == "PreviousBestGrade")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.PreviousBestGrade = txtBox.Text.Trim().ToString();
                                }

                                else if (col.HeaderText == "ProgramID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.ProgramID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == td.Definition + "ID")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.FeeSetupID = Int32.Parse(txtBox.Text.Trim());
                                }
                                else if (col.HeaderText == td.Definition)
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    if (txtBox.Text.Trim() != string.Empty)
                                    {
                                        dwEntity.Fee = Convert.ToDecimal(txtBox.Text.Trim());
                                    }
                                }
                                else if (col.HeaderText == "Remarks")
                                {
                                    index = gvDiscountWorksheet.Columns.IndexOf(col);
                                    TextBox txtBox = (TextBox)gvDiscountWorksheet.Rows[i].Cells[index].FindControl(col.HeaderText);
                                    dwEntity.Remarks = txtBox.Text.Trim();
                                }
                            }
                            dwEntity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            dwEntity.CreatedDate = DateTime.Now;

                            dwEntities.Add(dwEntity);
                        }
                    }
                    #endregion

                }
                if (DiscountWorksheet_BAO.Save(dwEntities) != 0)
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
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        private void FillStuCombo(int programId)
        {
            ddlStudent.Items.Clear();

            _students = Student.GetStudentsByProgID(programId);

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
                    item.Text = student.Roll;
                    ddlStudent.Items.Add(item);
                }

                ddlStudent.SelectedIndex = 0;
            }
        }

        private void FillGrid()
        {
            #region Split courseId and versionId
            int intCourseID = 0;
            int intVersionID = 0;

            if (ddlCourse.SelectedValue.ToString() != "0" && ddlCourse.SelectedValue.ToString() != "")
            {
                string[] str = SplitValues(ddlCourse.SelectedValue.ToString());
                intCourseID = Convert.ToInt32(str[0]);
                intVersionID = Convert.ToInt32(str[1]);
            }
            #endregion

            try
            {
                gvDiscountWorksheet.DataSource = null;
                gvDiscountWorksheet.DataBind();
                gvDiscountWorksheet.Columns.Clear();
                lblMsg.Text = string.Empty;

                #region Load discount Worksheets

                _discountWorksheets = DiscountWorksheet_BAO.LoadForEdit(
                                                               Convert.ToInt32(ddlAcaCalender.SelectedValue),
                                                               Convert.ToInt32(ddlProgram.SelectedValue),
                                                               ddlStudent.SelectedValue != ""?Convert.ToInt32(ddlStudent.SelectedValue):0,
                                                               //Convert.ToInt32(ddlStudent.SelectedValue),
                                                               intCourseID, intVersionID
                                                            );

                if (_discountWorksheets == null || _discountWorksheets.Count == 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
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
                dt.Columns.Add(new DataColumn("StudentRoll", typeof(string)));
                dt.Columns.Add(new DataColumn("CalCourseProgNodeID", typeof(int)));
                dt.Columns.Add(new DataColumn("AcaCalSectionID", typeof(int)));
                dt.Columns.Add(new DataColumn("SectionTypeID", typeof(int)));
                dt.Columns.Add(new DataColumn("AcaCalID", typeof(int)));
                dt.Columns.Add(new DataColumn("CourseID", typeof(int)));
                dt.Columns.Add(new DataColumn("VersionID", typeof(int)));
                dt.Columns.Add(new DataColumn("FormalCode", typeof(string)));
                dt.Columns.Add(new DataColumn("VersionCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Title", typeof(string)));
                dt.Columns.Add(new DataColumn("RetakeNo", typeof(string)));
                dt.Columns.Add(new DataColumn("PreviousBestGrade", typeof(string)));
                dt.Columns.Add(new DataColumn("ProgramID", typeof(int)));
                dt.Columns.Add(new DataColumn("DiscountWorkSheetId", typeof(int)));                

                #region make discount column
                foreach (TypeDefinition td in _typeDefsDis)
                {
                    dt.Columns.Add(new DataColumn(td.Definition + "ID", typeof(int)));
                    dt.Columns.Add(new DataColumn(td.Definition + "%", typeof(decimal)));
                }
                #endregion

                #region make fee column
                _typeDefsFee = TypeDefinition.GetTypes("Fee");
                if (_typeDefsFee != null || _typeDefsFee.Count != 0)
                {
                    foreach (TypeDefinition td in _typeDefsFee)
                    {
                        if (td.Type == "Fee_PCA") //manual check
                        {
                            dt.Columns.Add(new DataColumn(td.Definition + "ID", typeof(int)));
                            dt.Columns.Add(new DataColumn(td.Definition, typeof(decimal)));
                        }
                    }
                }
                #endregion

                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

                #endregion

                #region Bind data to rows
                bool flag = false;
                int n = 0;
                DataRow row = null;

                foreach (DiscountWorksheetEntity dwe in _discountWorksheets)
                {
                    if (n == dwe.CalCourseProgNodeID)
                    {
                        flag = true;
                    }
                    else if (flag)
                    {
                        dt.Rows.Add(row);//add row after all column has filed
                        flag = false;
                    }

                    n = dwe.CalCourseProgNodeID;

                    if (!flag)
                    {
                        row = dt.NewRow();
                        row["StudentID"] = dwe.StudentID;
                        row["StudentRoll"] = dwe.Roll;
                        row["CalCourseProgNodeID"] = dwe.CalCourseProgNodeID;
                        row["AcaCalSectionID"] = dwe.AcaCalSectionID;
                        row["SectionTypeID"] = dwe.SectionTypeID;
                        row["AcaCalID"] = dwe.AcaCalID;
                        row["CourseID"] = dwe.CourseID;
                        row["VersionID"] = dwe.VersionID;
                        row["FormalCode"] = dwe.FormalCode;
                        row["VersionCode"] = dwe.VersionCode;
                        row["Title"] = dwe.Title;
                        row["RetakeNo"] = dwe.RetakeNo;
                        row["PreviousBestGrade"] = dwe.PreviousBestGrade;
                        row["ProgramID"] = dwe.ProgramID;
                        row["DiscountWorkSheetId"] = dwe.Id;
                        row["Remarks"] = dwe.Remarks;
                    }

                    foreach (TypeDefinition td in _typeDefsDis)
                    {
                        if (td.Id == dwe.DiscountTypeID)
                        {
                            row[td.Definition + "ID"] = dwe.DiscountTypeID;
                            row[td.Definition + "%"] = dwe.DiscountPercentage;
                        }
                    }

                    foreach (TypeDefinition td in _typeDefsFee)
                    {
                        if (td.Id == dwe.FeeSetupID)
                        {
                            row[td.Definition + "ID"] = dwe.FeeSetupID;
                            row[td.Definition] = dwe.Fee;
                        }
                    }
                }
                dt.Rows.Add(row); // add last row.
                #endregion


                if (dt.Rows.Count == 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
                    return;
                }

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
                        bfield.HeaderStyle.Width = 250;
                    }
                    else
                    {
                        bfield.HeaderStyle.Width = 100;
                    }
                    //Initalize the DataField value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);

                    //Add the newly created bound field to the GridView.
                    gvDiscountWorksheet.Columns.Add(bfield);
                }
                #endregion

                #region Hide column
                gvDiscountWorksheet.DataSource = dt;
                gvDiscountWorksheet.DataBind();

                gvDiscountWorksheet.Columns[0].Visible = false;
                gvDiscountWorksheet.Columns[2].Visible = false;
                gvDiscountWorksheet.Columns[3].Visible = false;
                gvDiscountWorksheet.Columns[4].Visible = false;
                gvDiscountWorksheet.Columns[5].Visible = false;
                gvDiscountWorksheet.Columns[6].Visible = false;
                gvDiscountWorksheet.Columns[7].Visible = false;

                gvDiscountWorksheet.Columns[13].Visible = false;
                gvDiscountWorksheet.Columns[14].Visible = false;

                gvDiscountWorksheet.Columns[14].Visible = false;
                gvDiscountWorksheet.Columns[15].Visible = false;

                foreach (TypeDefinition td in _typeDefsDis)
                {
                    foreach (DataControlField col in gvDiscountWorksheet.Columns)
                    {
                        if (col.HeaderText == td.Definition + "ID")
                        {
                            gvDiscountWorksheet.Columns[gvDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                        }
                    }
                }

                foreach (TypeDefinition td in _typeDefsFee)
                {
                    foreach (DataControlField col in gvDiscountWorksheet.Columns)
                    {
                        if (col.HeaderText == td.Definition + "ID")
                        {
                            gvDiscountWorksheet.Columns[gvDiscountWorksheet.Columns.IndexOf(col)].Visible = false;
                        }
                    }
                }
                #endregion

                #endregion

                if (IsSessionVariableExists(SESSIONTYPEDEFDIS))
                {
                    RemoveFromSession(SESSIONTYPEDEFDIS);
                }
                AddToSession(SESSIONTYPEDEFDIS, _typeDefsDis);

                if (IsSessionVariableExists(SESSIONTYPEDEFFEE))
                {
                    RemoveFromSession(SESSIONTYPEDEFFEE);
                }
                AddToSession(SESSIONTYPEDEFFEE, _typeDefsFee);

                if (IsSessionVariableExists(SESSIONTABLE))
                {
                    RemoveFromSession(SESSIONTABLE);
                }
                AddToSession(SESSIONTABLE, dt);

            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        private void FillProgCombo()
        {
            ddlProgram.Items.Clear();

            _programs = Program.GetPrograms();

            if (_programs != null)
            {
                ListItem itemBlank = new ListItem();
                itemBlank.Value = "0";
                itemBlank.Text = "All";
                ddlProgram.Items.Add(itemBlank);

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

                    ListItem lei = new ListItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    ddlAcaCalender.Items.Add(lei);
                }

                if (Session[SESSIONCURRENTTRIMESTER] != null)
                {
                    Session.Remove(SESSIONCURRENTTRIMESTER);
                }
                Session[SESSIONCURRENTTRIMESTER] = currentTrimester;

                ddlAcaCalender.SelectedValue = currentTrimester;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }

        private void CleareGrid()
        {
            gvDiscountWorksheet.DataSource = null;
            gvDiscountWorksheet.DataBind();
        }

        private void CleareCombo()
        {
            ddlAcaCalender.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlProgram.SelectedIndex = 0;
            ddlStudent.SelectedIndex = 0;
        }

        private void CleareSession()
        {
            if (IsSessionVariableExists(SESSIONTYPEDEFDIS))
            {
                RemoveFromSession(SESSIONTYPEDEFDIS);
            }

            if (IsSessionVariableExists(SESSIONTYPEDEFFEE))
            {
                RemoveFromSession(SESSIONTYPEDEFFEE);
            }

            if (IsSessionVariableExists(SESSIONTABLE))
            {
                RemoveFromSession(SESSIONTABLE);
            }

            if (IsSessionVariableExists(SESSIONCURRENTTRIMESTER))
            {
                RemoveFromSession(SESSIONCURRENTTRIMESTER);
            }
        } 
        #endregion
                
    }
}
