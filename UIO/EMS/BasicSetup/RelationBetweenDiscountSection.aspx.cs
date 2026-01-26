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

namespace EMS.BasicSetup
{
    public partial class RelationBetweenDiscountSection:BasePage
    {
        private List<AcademicCalender> _trimesterInfos = null;
        List<TypeDefinition> _typeDefs = null;
        #region Session Consts
        private const string SESSIONTYPEDEF = "TYPEDEF";
        private const string SESSIONTABLE = "DATATABLE";
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
                if (col.ColumnName.Contains("ID") || col.ColumnName.Contains("DiscountName"))
                {
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);
                }
                else
                {
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, true);
                }
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
                base.CheckPage_Load();
                if (!IsPostBack && !IsCallback)
                {
                    FillAcademicCalenderCombo();
                    FillProgramCombo();
                    btnSave.Attributes.Add("onclick", "return confirm('Do you want to save?');");
                    lblMsg.Text = "";
                    gvDiscount.DataSource = null;
                    gvDiscount.DataBind();
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

                #region Load Discounts into table and Bind with gridview

                List<TypeDefinition> typeDiscountDefs = TypeDefinition.GetTypes("Discount");
                if (typeDiscountDefs == null || typeDiscountDefs.Count == 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Not Found");
                    return;
                }
                #region Making Table

                _typeDefs = TypeDefinition.GetTypes("Section");
                if (_typeDefs == null || _typeDefs.Count == 0)
                {
                    return;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("TypedefDiscountID", typeof(int)));
                dt.Columns.Add(new DataColumn("DiscountName", typeof(string)));

                foreach (TypeDefinition td in _typeDefs)
                {
                    dt.Columns.Add(new DataColumn(td.Definition + "ID", typeof(int)));
                    dt.Columns.Add(new DataColumn(td.Definition, typeof(bool)));
                }

                #endregion
                List<RelationDiscountSectionEntity> sdes = BussinessObject.RelationDiscountSection_BAO.GetRelations(Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString()), Int32.Parse(cboProgram.SelectedItem.Value.ToString()));
                foreach (TypeDefinition ad in typeDiscountDefs)
                {
                    DataRow row = dt.NewRow();
                    row["TypedefDiscountID"] = ad.Id;
                    row["DiscountName"] = ad.Definition;

                    foreach (TypeDefinition td in _typeDefs)
                    {
                        row[td.Definition + "ID"] = td.Id;
                        row[td.Definition] = false;
                        if (sdes != null)
                        {
                            var dsc = from res in sdes
                                      where res.TypeDefDiscountID == ad.Id
                                      && res.TypeDefID == td.Id
                                      select res.TypeDefID;
                            if (dsc.Count() > 0)
                            {
                                row[td.Definition] = true;
                            }
                        }
                    }
                    dt.Rows.Add(row);

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
                    if (col.ColumnName.Contains("ID") || col.ColumnName.Contains("DiscountName"))
                    {
                        bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, false);
                    }
                    else
                    {
                        bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName, true);
                    }

                    //Add the newly created bound field to the GridView.
                    gvDiscount.Columns.Add(bfield);
                }

                gvDiscount.DataSource = dt;
                gvDiscount.DataBind();
                gvDiscount.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gvDiscount.Columns[3].ItemStyle.VerticalAlign = VerticalAlign.Middle;


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
                List<RelationDiscountSectionEntity> sds = new List<RelationDiscountSectionEntity>();

                for (int i = 0; i < gvDiscount.Rows.Count; i++)
                {
                    foreach (TypeDefinition td in _typeDefs)
                    {
                        RelationDiscountSectionEntity sd = new RelationDiscountSectionEntity();
                        foreach (DataControlField col in gvDiscount.Columns)
                        {
                            int index = -1;
                            if (col.HeaderText == td.Definition)
                            {
                                index = gvDiscount.Columns.IndexOf(col);
                                CheckBox chkBox = (CheckBox)gvDiscount.Rows[i].Cells[index].FindControl("Chk" + col.HeaderText);
                                if (chkBox.Checked)
                                {
                                    TextBox txtBox = (TextBox)gvDiscount.Rows[i].Cells[index - 1].FindControl(td.Definition + "ID");
                                    sd.TypeDefID = Int32.Parse(txtBox.Text.Trim());
                                    break;
                                }
                            }
                        }
                        if (sd.TypeDefID != 0)
                        {
                            TextBox txtBox = (TextBox)gvDiscount.Rows[i].Cells[0].FindControl("TypedefDiscountID");
                            sd.TypeDefDiscountID = Int32.Parse(txtBox.Text.Trim());
                            sd.AcaCalID = Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString());
                            sd.ProgramID = Int32.Parse(cboProgram.SelectedItem.Value.ToString());
                            sd.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            sds.Add(sd);
                        }
                    }
                }
                if (BussinessObject.RelationDiscountSection_BAO.Save(sds) != 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Saved Successfully");
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, "Not Saved");
                }
                RemoveFromSession(SESSIONTABLE);
                RemoveFromSession(SESSIONTYPEDEF);
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

        protected void cboAcaCalender_SelectedIndexChanged1(object sender, EventArgs e)
        {
            gvDiscount.DataSource = null;
            gvDiscount.DataBind();
            gvDiscount.Columns.Clear();
            lblMsg.Text = string.Empty;
        }

        protected void cboProgram_SelectedIndexChanged1(object sender, EventArgs e)
        {
            gvDiscount.DataSource = null;
            gvDiscount.DataBind();
            gvDiscount.Columns.Clear();
            lblMsg.Text = string.Empty;
        }
    }
}
