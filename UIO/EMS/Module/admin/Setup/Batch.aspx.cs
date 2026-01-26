using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Setup_Batch : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.Batch);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.Batch));

    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                LoadBatch();
                LoadProgram();
                LoadSession();
                LoadModalProgram();
            }
        }
        catch (Exception Ex)
        {
        }
    }

    private void LoadModalProgram()
    {

        ddlProgram.Items.Clear();
        List<Program> progList = ProgramManager.GetAll();
        if (progList != null)
        {
            progList = progList.ToList();
            ddlProgram.AppendDataBoundItems = true;
            ddlProgram.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlProgram.DataSource = progList;
            ddlProgram.DataBind();
        }
    }

    private void LoadSession()
    {
        try
        {

            List<LogicLayer.BusinessObjects.AcademicCalender> sessionList = new List<LogicLayer.BusinessObjects.AcademicCalender>();

            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
            int CalenderUnitMasterId = 0;
            if (ProgramId > 0)
            {
                var ProgramObj = ucamContext.Programs.Find(ProgramId);
                if (ProgramObj != null)
                    CalenderUnitMasterId = Convert.ToInt32(ProgramObj.CalenderUnitMasterID);

                sessionList = AcademicCalenderManager.GetAll(CalenderUnitMasterId);

            }
            else
                sessionList = AcademicCalenderManager.GetAll();


            ddlSession.Items.Clear();
            ddlSession.AppendDataBoundItems = true;

            if (sessionList != null)
            {
                ddlSession.Items.Add(new ListItem("-Select-", "0"));
                ddlSession.DataTextField = "FullCode";
                ddlSession.DataValueField = "AcademicCalenderID";

                ddlSession.DataSource = sessionList.OrderByDescending(a => a.AcademicCalenderID);
                ddlSession.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void LoadProgram()
    {

        List<Program> progList = ProgramManager.GetAll();
        DropDownList1.Items.Clear();
        DropDownList1.AppendDataBoundItems = true;
        DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));
        DropDownList1.DataSource = progList;
        DropDownList1.DataBind();
    }


    protected void LoadBatch()
    {
        try
        {
            List<Batch> _batchList = new List<Batch>();

            int ProgramId = 0;

            try
            {
                ProgramId = Convert.ToInt32(DropDownList1.SelectedValue);
            }
            catch (Exception ex)
            {
            }

            if (ProgramId > 0)
            {
                var BatchObj = BatchManager.GetAllByProgram(ProgramId).ToList();
                if (BatchObj != null)
                    _batchList.AddRange(BatchObj);
            }
            else
                _batchList = BatchManager.GetAll();

            if (_batchList != null && _batchList.Count > 0)
            {

                gvBatch.DataSource = _batchList;
                gvBatch.DataBind();
            }
            else
            {
                gvBatch.DataSource = null;
                gvBatch.DataBind();
            }
        }
        catch (Exception ex) { }
    }

    protected void btnAddNewBatch_Click(object sender, EventArgs e)
    {
        // ClearAll();
        btnInsert.Text = "AddNew";
        ModalPopupExtender1.Show();
        ClearAll();
        LoadModalProgram();
        try
        {
            if (Convert.ToInt32(DropDownList1.SelectMethod) > 0)
            {
                ddlProgram.SelectedValue = DropDownList1.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
        }

        LoadSession();

    }

    private void ClearAll()
    {
        LabelMessage.Text = "";
        txtBatchNo.Text = "";
        txtBatchName.Text = "";
    }



    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }


    protected void gvBatch_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnInsert.Text = "Update";
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);
            btnInsert.CommandArgument = id.ToString();
            Batch obj = BatchManager.GetById(id);
            if (obj != null)
            {

                ModalPopupExtender1.Show();
                ddlProgram.SelectedValue = (obj.ProgramId).ToString();
                LoadSession();
                ddlSession.SelectedValue = (obj.AcaCalId).ToString();
                txtBatchNo.Text = (obj.BatchNO).ToString();
                txtBatchName.Text = obj.Attribute1 == null ? "" : obj.Attribute1.ToString();
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlProgram.SelectedValue) == 0 || Convert.ToInt32(ddlSession.SelectedValue) == 0 || string.IsNullOrEmpty(txtBatchNo.Text))
        {
            ModalPopupExtender1.Show();
            LabelMessage.Text = "Fill Up all field";
        }
        else
        {
            List<Batch> batchlist = BatchManager.GetAll().Where(x => x.ProgramId == Convert.ToInt32(ddlProgram.SelectedValue) && x.AcaCalId == Convert.ToInt32(ddlSession.SelectedValue)).ToList();
            if (btnInsert.Text == "AddNew")
            {
                if (batchlist.Count > 0)
                {
                    showAlert("Batch Already Exist.");
                    return;
                }
                else
                {
                    UCAMDAL.Batch obj = new UCAMDAL.Batch();
                    obj.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                    obj.AcaCalId = Convert.ToInt32(ddlSession.SelectedValue);
                    obj.BatchNO = Convert.ToInt32(txtBatchNo.Text);
                    obj.Attribute1 = txtBatchName.Text;
                    obj.CreatedBy = Convert.ToInt32(userObj.Id);
                    obj.CreatedDate = DateTime.Now;

                    ucamContext.Batches.Add(obj);
                    ucamContext.SaveChanges();

                    int c = obj.BatchId;
                    if (c > 0)
                    {
                        LogicLayer.BusinessObjects.AcademicCalender ac = AcademicCalenderManager.GetById(obj.AcaCalId);
                        InsertLog("New Batch Add", userObj.LogInID + " added a new batch in program : " + ddlProgram.SelectedItem.Text.ToString() + " with batch number : " + obj.BatchNO + " and semester : " + ac.FullCode);
                        showAlert("Batch Inserted Successfully.");

                        LoadBatch();
                    }
                    else
                    {
                        showAlert("Program Insert Failed.");
                        return;
                    }
                }
            }
            else if (btnInsert.Text == "Update")
            {
                Batch obj = BatchManager.GetById(Convert.ToInt32(btnInsert.CommandArgument));

                var UpdateObj = ucamContext.Batches.Find(obj.BatchId);
                string PrevInfo = "";
                if (obj != null)
                    PrevInfo = obj.Program.ShortName + "_" + obj.BatchNO + "_" + obj.Session.FullCode;
                UpdateObj.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                UpdateObj.AcaCalId = Convert.ToInt32(ddlSession.SelectedValue);
                UpdateObj.BatchNO = Convert.ToInt32(txtBatchNo.Text);
                UpdateObj.Attribute1 = txtBatchName.Text;
                UpdateObj.ModifiedBy = Convert.ToInt32(BaseCurrentUserObj.Id);
                UpdateObj.ModifiedDate = DateTime.Now;

                ucamContext.SaveChanges();

                LogicLayer.BusinessObjects.AcademicCalender ac = AcademicCalenderManager.GetById(obj.AcaCalId);
                InsertLog("Update Batch", userObj.LogInID + " Update batch in program : " + ddlProgram.SelectedItem.Text.ToString() + " with batch number : " + obj.BatchNO + " and semester : " + ac.FullCode + " With Previous info : " + PrevInfo);

                LoadBatch();
            }
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);
            Batch bt = BatchManager.GetById(id);
            List<Student> stdList = StudentManager.GetAllByProgramOrBatchOrRoll(0, id, "0");
            if (stdList != null && stdList.Count > 0)
            {
                showAlert("Batch Delete Failed.Student exists in this batch");
            }
            else
            {
                BatchManager.Delete(id);
                InsertLog("Delete Batch", userObj.LogInID + " Delete batch in program : " + bt.Program.ShortName + " with batch number : " + bt.BatchNO);

                DropDownList1.SelectedValue = "0";
                LoadBatch();
            }
        }
        catch (Exception ex)
        {
            showAlert(ex.Message);
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadBatch();
        }
        catch (Exception ex) { }
    }

    private void InsertLog(string EventName, string Message)
    {
        LogGeneralManager.Insert(
                                  DateTime.Now,
                                  "",
                                  "",
                                  userObj.LogInID,
                                  "",
                                  "",
                                  EventName,
                                  Message,
                                  "Normal",
                                  _pageId,
                                  _pageName,
                                  _pageUrl,
                                  "");
    }


    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadSession();
            AutoBatchNoAndBatchName();
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AutoBatchNoAndBatchName();
        }
        catch (Exception ex)
        {
        }
    }

    private void AutoBatchNoAndBatchName()
    {
        try
        {

            txtBatchNo.Text = "";
            txtBatchName.Text = "";

            ModalPopupExtender1.Show();

            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
            int AcaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            var batchObj = ucamContext.Batches.Where(b => b.ProgramId == ProgramId && b.AcaCalId == AcaCalId).FirstOrDefault();

            if (batchObj != null)
            {
                txtBatchNo.Text = batchObj.BatchNO.ToString();
                txtBatchName.Text = batchObj.Attribute1 == null ? "" : batchObj.Attribute1.ToString();
            }
            else
            {
                var LastBatch = ucamContext.Batches.Where(b => b.ProgramId == ProgramId && b.AcaCalId <= AcaCalId).OrderByDescending(x => x.AcaCalId).FirstOrDefault();
                if (LastBatch != null)
                {
                    txtBatchNo.Text = (LastBatch.BatchNO + 1).ToString();
                }
                else
                {
                    txtBatchNo.Text = "1";
                }

                var acacObj = AcademicCalenderManager.GetById(AcaCalId);
                if (acacObj != null)
                {
                    txtBatchName.Text = acacObj.Year - 1 + "-" + acacObj.Year;
                }
                else
                    txtBatchName.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
    }
}