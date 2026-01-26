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

public partial class StudentManagement_DiscountEntry : BasePage
{
    private List<AcademicCalender> _trimesterInfos = null;
    private string[] _dataKey = new string[1] { "Id" };
    List<TypeDefinition> _typeDefs = null;
    #region Session Consts
    private const string SESSIONTYPEDEF = "TYPEDEF_DE";
    private const string SESSIONTABLE = "DATATABLE_DE";
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
        gvDiscount.DataSource = null;
        gvDiscount.DataBind();
        gvDiscount.Columns.Clear();

        if (!IsSessionVariableExists(SESSIONTABLE) || !IsSessionVariableExists(SESSIONTYPEDEF))
        {
            return;
        }
        DataTable dt = (DataTable)GetFromSession(SESSIONTABLE);
        List<TypeDefinition> typeDefs = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEF);

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
            gvDiscount.Columns.Add(bfield);
        }

        gvDiscount.DataSource = dt;
        gvDiscount.DataBind();
        gvDiscount.Columns[0].Visible = false;

        foreach (TypeDefinition td in typeDefs)
        {
            foreach (DataControlField col in gvDiscount.Columns)
            {
                if (col.HeaderText == td.Definition + "ID")
                {
                    gvDiscount.Columns[gvDiscount.Columns.IndexOf(col)].Visible = false;
                }
            }
        }

        //if (IsSessionVariableExists(SESSIONTYPEDEF))
        //{
        //    RemoveFromSession(SESSIONTYPEDEF);
        //}
        //AddToSession(SESSIONTYPEDEF, _typeDefs);

        //if (IsSessionVariableExists(SESSIONTABLE))
        //{
        //    RemoveFromSession(SESSIONTABLE);
        //}
        //AddToSession(SESSIONTABLE, dt);
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
                FillProgramCombo();
                FillEffectiveAcademicCalenderCombo();
                btnSave.Attributes.Add("onclick", "return confirm('Do you want to save?');");
                lblMsg.Text = "";
                gvDiscount.DataSource = null;
                gvDiscount.DataBind();

                RemoveFromSession(SESSIONTYPEDEF);
                RemoveFromSession(SESSIONTABLE);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            _trimesterInfos = AcademicCalender.Gets();
            if (_trimesterInfos == null)
            {
                return;
            }
            foreach (AcademicCalender ac in _trimesterInfos)
            {
                ListEditItem lei = new ListEditItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                cboAcaCalender.Items.Add(lei);
            }
            cboAcaCalender.SelectedIndex = cboAcaCalender.Items.Count - 1;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private void FillEffectiveAcademicCalenderCombo()
    {
        try
        {
            _trimesterInfos = AcademicCalender.Gets();
            if (_trimesterInfos == null)
            {
                return;
            }
            foreach (AcademicCalender ac in _trimesterInfos)
            {
                ListEditItem lei = new ListEditItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                cboEffectiveCal.Items.Add(lei);
            }
            cboEffectiveCal.SelectedIndex = cboAcaCalender.Items.Count - 1;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            cboProgram.Items.Clear();
            lblMsg.Text = string.Empty;
            //gvwCollection.DataSource = null;
            List<Program> _programs = Program.GetPrograms();
            if (_programs == null)
            {
                return;
            }
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

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            gvDiscount.DataSource = null;
            gvDiscount.DataBind();
            gvDiscount.Columns.Clear();
            lblMsg.Text = string.Empty;

            #region Load Students into table and Bind with gridview

            List<Admission> admStds = Admission.GetAdmittedStudentsByAcaCalandProgID(Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()), Convert.ToInt32(cboProgram.SelectedItem.Value.ToString()));
            if (admStds == null || admStds.Count == 0)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
                return;
            }
            #region Making Table

            _typeDefs = TypeDefinition.GetTypes("Discount");
            if (_typeDefs == null || _typeDefs.Count == 0)
            {
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AdmID", typeof(int)));
            dt.Columns.Add(new DataColumn("StudentRoll", typeof(string)));
            dt.Columns.Add(new DataColumn("StudentName", typeof(string)));

            foreach (TypeDefinition td in _typeDefs)
            {
                dt.Columns.Add(new DataColumn(td.Definition + "ID", typeof(int)));
                dt.Columns.Add(new DataColumn(td.Definition + "inPercentage", typeof(decimal)));
            }

            #endregion

            foreach (Admission ad in admStds)
            {
                if (ad.Student != null)
                {
                    DataRow row = dt.NewRow();
                    row["AdmID"] = ad.Id;
                    row["StudentRoll"] = ad.Student.Roll;
                    row["StudentName"] = ad.Person.FirstName + " " + ad.Person.MiddleName + " " + ad.Person.LastName + " " + ad.Person.NickOrOtherName;

                    List<StdDiscountEntity> sdes = BussinessObject.StdDiscount.GetStdDiscounts(ad.Id);

                    foreach (TypeDefinition td in _typeDefs)
                    {
                        row[td.Definition + "ID"] = td.Id;

                        if (sdes != null)
                        {
                            var dsc = from res in sdes
                                      where res.TypeDefID == td.Id
                                      select res.TypePercentage;
                            if (dsc.Count() > 0)
                            {
                                row[td.Definition + "inPercentage"] = dsc.ToList<Decimal>()[0];
                            }
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            if (dt.Rows.Count == 0)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
                return;
            }
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
                gvDiscount.Columns.Add(bfield);
            }

            gvDiscount.DataSource = dt;
            gvDiscount.DataBind();

            gvDiscount.Columns[0].Visible = false;
            foreach (TypeDefinition td in _typeDefs)
            {
                foreach (DataControlField col in gvDiscount.Columns)
                {
                    if (col.HeaderText == td.Definition + "ID")
                    {
                        gvDiscount.Columns[gvDiscount.Columns.IndexOf(col)].Visible = false;
                    }
                }
            }

            #endregion
            if (IsSessionVariableExists(SESSIONTYPEDEF))
            {
                RemoveFromSession(SESSIONTYPEDEF);
            }
            AddToSession(SESSIONTYPEDEF, _typeDefs);

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            _typeDefs = (List<TypeDefinition>)GetFromSession(SESSIONTYPEDEF);
            List<StdDiscountEntity> sds = new List<StdDiscountEntity>();

            for (int i = 0; i < gvDiscount.Rows.Count; i++)
            {
                foreach (TypeDefinition td in _typeDefs)
                {
                    StdDiscountEntity sd = new StdDiscountEntity();
                    foreach (DataControlField col in gvDiscount.Columns)
                    {
                        int index = -1;
                        if (col.HeaderText == "AdmID")
                        {
                            index = gvDiscount.Columns.IndexOf(col);
                            TextBox txtBox = (TextBox)gvDiscount.Rows[i].Cells[index].FindControl(col.HeaderText);
                            sd.AdmID = Int32.Parse(txtBox.Text.Trim());
                        }
                        else if (col.HeaderText == td.Definition + "inPercentage")
                        {
                            index = gvDiscount.Columns.IndexOf(col);
                            TextBox txtBox = (TextBox)gvDiscount.Rows[i].Cells[index].FindControl(col.HeaderText);
                            if (txtBox.Text.Trim() != string.Empty)
                            {
                                sd.TypePercentage = Convert.ToDecimal(txtBox.Text.Trim());
                            }
                        }
                        else if (col.HeaderText == td.Definition + "ID")
                        {
                            index = gvDiscount.Columns.IndexOf(col);
                            TextBox txtBox = (TextBox)gvDiscount.Rows[i].Cells[index].FindControl(col.HeaderText);
                            sd.TypeDefID = Int32.Parse(txtBox.Text.Trim());
                        }
                    }
                    sd.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    sd.CreatedDate = DateTime.Now;
                    sd.EffectiveAcaCalID = Convert.ToInt32(cboEffectiveCal.SelectedItem.Value.ToString());
                    sds.Add(sd);
                }
            }
            if (BussinessObject.StdDiscount.Save(sds) != 0)
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
            Utilities.ShowMassage(lblMsg, Color.Blue, ex.Message);
        }
    }

    protected void cboAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDiscount.DataSource = null;
        gvDiscount.DataBind();
        gvDiscount.Columns.Clear();
        lblMsg.Text = string.Empty;
        if (IsSessionVariableExists(SESSIONTYPEDEF))
        {
            RemoveFromSession(SESSIONTYPEDEF);
        }
        if (IsSessionVariableExists(SESSIONTABLE))
        {
            RemoveFromSession(SESSIONTABLE);
        }
    }

    protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDiscount.DataSource = null;
        gvDiscount.DataBind();
        gvDiscount.Columns.Clear();
        lblMsg.Text = string.Empty;
        if (IsSessionVariableExists(SESSIONTYPEDEF))
        {
            RemoveFromSession(SESSIONTYPEDEF);
        }
        if (IsSessionVariableExists(SESSIONTABLE))
        {
            RemoveFromSession(SESSIONTABLE);
        }
    }
}
