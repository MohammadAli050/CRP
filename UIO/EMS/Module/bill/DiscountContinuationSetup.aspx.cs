using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DiscountContinuationSetup : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                DiscountPanel.Enabled = false;
                //FillDropdown();
            }
        }
        catch { }
    }

    protected void FillDropdown()
    {
        //FillBatchAcaCalCombo();
        //FillProgramCombo();
        FillDiscountTypeCombo();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }


    private void FillBatchAcaCalCombo()
    {
        try
        {
            //ddlBatchAcaCal.Items.Clear();
            //List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            //if (academicCalenderList.Count > 0)
            //{
            //    academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            //    ddlBatchAcaCal.Items.Add(new ListItem("-Select-", "0"));
            //    ddlBatchAcaCal.AppendDataBoundItems = true;

            //    AcademicCalender acaCal = academicCalenderList.Where(x => x.IsCurrent == true).SingleOrDefault();
            //    if (acaCal != null)
            //        academicCalenderList = academicCalenderList.Where(x => x.AcademicCalenderID <= acaCal.AcademicCalenderID).ToList();

            //    foreach (AcademicCalender academicCalender in academicCalenderList)
            //        ddlBatchAcaCal.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
            //}
            //else
            //{
            //    lblMsg.Text = "Error: 101(Academic Calender not load)";
            //}
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
            //ddlProgram.Items.Clear();
            //List<Program> programList = ProgramManager.GetAll();

            //ddlProgram.Items.Add(new ListItem("-Select-", "0"));
            //ddlProgram.AppendDataBoundItems = true;

            //if (programList != null)
            //{
            //    ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
            //    ddlProgram.DataTextField = "ShortName";
            //    ddlProgram.DataValueField = "ProgramId";
            //    ddlProgram.DataBind();
            //}
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillDiscountTypeCombo()
    {
        try
        {
            ddlDiscountType.Items.Clear();
            List<TypeDefinition> typeDefinitionList = TypeDefinitionManager.GetAll("Discount");

            ddlDiscountType.Items.Add(new ListItem("-Select-", "0"));
            ddlDiscountType.AppendDataBoundItems = true;

            if (typeDefinitionList != null)
            {
                ddlDiscountType.DataSource = typeDefinitionList.OrderBy(d => d.TypeDefinitionID).ToList();
                ddlDiscountType.DataTextField = "Definition";
                ddlDiscountType.DataValueField = "TypeDefinitionID";
                ddlDiscountType.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void Insert()
    {
        try
        {
            HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            string uid = aCookie["UserName"];
            string pwd = aCookie["UserPassword"];
            User user = UserManager.GetByLogInId(uid);

            DiscountContinuationSetup discountContinuationSetup = new DiscountContinuationSetup();
            discountContinuationSetup.BatchAcaCalID = Convert.ToInt32(ucBatch.selectedValue);
            discountContinuationSetup.ProgramID = Convert.ToInt16(ucProgram.selectedValue);
            discountContinuationSetup.TypeDefinitionID = Convert.ToInt32(ddlDiscountType.SelectedValue);
            discountContinuationSetup.MinCredits = Convert.ToDecimal(txtMinCredits.Text);
            discountContinuationSetup.MaxCredits = Convert.ToDecimal(txtMaxCredits.Text);
            discountContinuationSetup.MinCGPA = Convert.ToDecimal(txtMinCGPA.Text);
            discountContinuationSetup.PercentMin = Convert.ToDecimal(txtPercentMin.Text);
            discountContinuationSetup.PercentMax = Convert.ToDecimal(txtPercentMax.Text);
            if (user != null)
                discountContinuationSetup.CreatedBy = user.User_ID;
            else
                discountContinuationSetup.CreatedBy = 0;
            discountContinuationSetup.CreatedDate = DateTime.Now;

            int resultInsert = DiscountContinuationSetupManager.Insert(discountContinuationSetup);
            if (resultInsert > 0)
                LoadDiscountGrid();
        }
        catch { }
    }

    protected bool Update(int id, string minCredits, string maxCredits, string minCGPA, string percentMin, string percentMax)
    {
        try
        {
            DiscountContinuationSetup discountContinuationSetup = DiscountContinuationSetupManager.GetById(id);
            if (discountContinuationSetup != null)
            {
                HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
                string uid = aCookie["UserName"];
                string pwd = aCookie["UserPassword"];
                User user = UserManager.GetByLogInId(uid);

                discountContinuationSetup.MinCredits = Convert.ToDecimal(minCredits);
                discountContinuationSetup.MaxCredits = Convert.ToDecimal(maxCredits);
                discountContinuationSetup.MinCGPA = Convert.ToDecimal(minCGPA);
                discountContinuationSetup.PercentMin = Convert.ToDecimal(percentMin);
                discountContinuationSetup.PercentMax = Convert.ToDecimal(percentMax);
                if (user != null)
                    discountContinuationSetup.ModifiedBy = user.User_ID;
                else
                    discountContinuationSetup.ModifiedBy = 0;
                discountContinuationSetup.ModifiedDate = DateTime.Now;

                bool resultUpdate = DiscountContinuationSetupManager.Update(discountContinuationSetup);

                return resultUpdate;
            }
            return false;
        }
        catch { return false; }
    }

    protected void LoadDiscountGrid()
    {
        try
        {
            gvDiscountContinuationSetup.Visible = true;
            gvDiscountContinuationSetup.Columns[1].Visible = true;

            List<DiscountContinuationSetup> discountContinuationSetupList = DiscountContinuationSetupManager.GetAll(Convert.ToInt32(13), Convert.ToInt32(ucProgram.selectedValue));
            if (discountContinuationSetupList.Count > 0 && discountContinuationSetupList != null)
            {
                gvDiscountContinuationSetup.DataSource = discountContinuationSetupList;
            }
            gvDiscountContinuationSetup.DataBind();
        }
        catch { }
    }

    protected bool CheckDiscountValues(int discountType, string minCredits, string maxCredits, string minCGPA, string percentMin, string percentMax)
    {
        try
        {
            string warning = string.Empty;
            if (discountType == 0)
                warning = "Select Discount";

            if (minCredits == "" || maxCredits == "" || minCGPA == "" || percentMin == "" || percentMax == "")
            {
                if (warning.Length > 0)
                    warning += " and Inupt";
                else
                    warning += "Inupt";
                if (txtMinCredits.Text == "")
                    warning += " [Min Credits]";
                if (txtMaxCredits.Text == "")
                    warning += " [Max Credits]";
                if (txtMinCGPA.Text == "")
                    warning += " [Min CGPA]";
                if (txtPercentMin.Text == "")
                    warning += " [Min Percentage]";
                if (txtPercentMax.Text == "")
                    warning += " [Max Percentage]";
            }
            if (warning.Length > 0)
            {
                lblMsg.Text = warning;
                return false;
            }
            else
                return true;
        }
        catch { return false; }
    }

    #endregion

    #region Event

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string warning = string.Empty;
            int flag = 0;
            //if (Convert.ToInt32(ddlBatchAcaCal.SelectedValue) == 0)
            //{
            //    flag = 1;
            //    warning += " - Batch";
            //}
            //if (Convert.ToInt32(ddlProgram.SelectedValue) == 0)
            //{
            //    flag = 1;
            //    warning += " - Program";
            //}
            if (flag == 1)
                lblMsg.Text = "Please select " + warning + " Dropdown List";
            else
            {
                InitialPanel.Enabled = false;
                DiscountPanel.Enabled = true;

                LoadDiscountGrid();
            }
        }
        catch { }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string warning = string.Empty;
            int flag = 0;
            if (Convert.ToInt32(ucBatch.selectedValue) == 0)
            {
                flag = 1;
                warning += " - Batch";
            }
            if (Convert.ToInt32(ucProgram.selectedValue) == 0)
            {
                flag = 1;
                warning += " - Program";
            }
            if (flag == 1)
                lblMsg.Text = "Please select " + warning + " Dropdown List";
            else
            {
                LoadDiscountGrid();
                gvDiscountContinuationSetup.Columns[1].Visible = false;
            }
        }
        catch { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            InitialPanel.Enabled = true;
            DiscountPanel.Enabled = false;
            gvDiscountContinuationSetup.Visible = false;
        }
        catch { }
    }

    protected void btnbtnRowAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bool checkWarning = CheckDiscountValues(Convert.ToInt32(ddlDiscountType.SelectedValue), txtMinCredits.Text, txtMaxCredits.Text, txtMinCGPA.Text, txtPercentMin.Text, txtPercentMax.Text);
            if (checkWarning)
            {
                List<DiscountContinuationSetup> discountContinuationSetupList = DiscountContinuationSetupManager.GetAll(Convert.ToInt32(ucBatch.selectedValue), Convert.ToInt32(ucProgram.selectedValue));
                if (discountContinuationSetupList.Count > 0 && discountContinuationSetupList != null)
                {
                    DiscountContinuationSetup discountContinuationSetupTemp = discountContinuationSetupList.Where(x => x.TypeDefinitionID == Convert.ToInt32(ddlDiscountType.SelectedValue)).SingleOrDefault();
                    if (discountContinuationSetupTemp != null)
                    {
                        lblMsg.Text = ddlDiscountType.SelectedItem.Text + " already added to the LIST";
                    }
                    else
                    {
                        Insert();
                    }
                }
                else
                {
                    Insert();
                }
            }
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            gvDiscountContinuationSetup.EditIndex = e.NewEditIndex;

            LoadDiscountGrid();
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvDiscountContinuationSetup.EditIndex = -1;

            LoadDiscountGrid();
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvDiscountContinuationSetup.Rows[e.RowIndex];

            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            HiddenField hfTypeDefId = (HiddenField)row.FindControl("hfTypeDefId");
            int id = Convert.ToInt32(hfId.Value);
            int discountId = Convert.ToInt32(hfTypeDefId.Value);
            string minCredits = ((TextBox)(row.Cells[3].Controls[0])).Text;
            string maxCredits = ((TextBox)(row.Cells[4].Controls[0])).Text;
            string minCGPA = ((TextBox)(row.Cells[5].Controls[0])).Text;
            string percentMin = ((TextBox)(row.Cells[6].Controls[0])).Text;
            string percentMax = ((TextBox)(row.Cells[7].Controls[0])).Text;

            bool resultWarning = CheckDiscountValues(discountId, minCredits, maxCredits, minCGPA, percentMin, percentMax);
            if (resultWarning)
            {
                bool resultUpdate = Update(id, minCredits, maxCredits, minCGPA, percentMin, percentMax);
                if (resultUpdate)
                {
                    gvDiscountContinuationSetup.EditIndex = -1;
                    LoadDiscountGrid();
                }
            }
        }
        catch { }
    }

    protected void gvDiscountContinuationSetup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow row = gvDiscountContinuationSetup.Rows[e.RowIndex];

            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            HiddenField hfTypeDefId = (HiddenField)row.FindControl("hfTypeDefId");

            bool resultDelete = DiscountContinuationSetupManager.Delete(Convert.ToInt32(hfId.Value));
            if (resultDelete)
            {
                LoadDiscountGrid();
                lblMsg.Text = "Deleted";
            }
            else
                lblMsg.Text = "Error: 111";
        }
        catch { }
    }

    #endregion
}