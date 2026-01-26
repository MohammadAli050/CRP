using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FeeSetup : BasePage
{

    int _programId = 0;
    int _sessionId = 0;
    int _batchId = 0;
    UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        try
        {
            
            if (!IsPostBack)
            { }
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            _programId = Convert.ToInt32(ucProgram.selectedValue);
            _sessionId = Convert.ToInt32(ucSession.selectedValue);
            _batchId = Convert.ToInt32(ucBatch.selectedValue);

            foreach (GridViewRow row in gvFeeSetup.Rows)
            {
                FeeSetup feeSetup = new FeeSetup();

                HiddenField hiddenId = (HiddenField)row.FindControl("hdnFeeSetupID");
                int feesSetupId = Convert.ToInt32(hiddenId.Value);

                HiddenField hdmBatchID = (HiddenField)row.FindControl("hdmBatchID");
                if (hdmBatchID.Value == "0") { feeSetup.BatchID = _batchId; }
                else { feeSetup.BatchID = Convert.ToInt32(hdmBatchID.Value); }

                HiddenField hdmAcaCalID = (HiddenField)row.FindControl("hdmAcaCalID");
                if (hdmAcaCalID.Value == "0") { feeSetup.AcaCalID = _sessionId; }
                else { feeSetup.AcaCalID = Convert.ToInt32(hdmAcaCalID.Value); }

                HiddenField hdnProgramID = (HiddenField)row.FindControl("hdnProgramID");
                if (hdnProgramID.Value == "0") { feeSetup.ProgramID = _programId; }
                else { feeSetup.ProgramID = Convert.ToInt32(hdnProgramID.Value); }

                HiddenField hdnTypeDefID = (HiddenField)row.FindControl("hdnTypeDefID");
                feeSetup.TypeDefID = Convert.ToInt32(hdnTypeDefID.Value);

                TextBox amount = (TextBox)row.FindControl("txtAmount");
                feeSetup.Amount = Convert.ToDecimal(amount.Text);

                feeSetup.CreatedBy = -1;
                feeSetup.CreatedDate = DateTime.Now;
                feeSetup.ModifiedBy = -1;
                feeSetup.ModifiedDate = DateTime.Now;

                if (feesSetupId == 0)
                {
                    int insertResult = FeeSetupManager.Insert(feeSetup);
                    if (insertResult > 0)
                    {
                        #region Log Insert

                        LogGeneralManager.Insert(
                                                             DateTime.Now,
                                                             "",
                                                             ucSession.selectedText,
                                                             userObj.LogInID,
                                                             "",
                                                             "",
                                                             "Fee Setup Insert",
                                                             userObj.LogInID + " inserted FeesSetup of Type " + feeSetup.Type.Type +" which is "+feeSetup.Type.Definition+" and amount is " +feeSetup.Amount+" Tk for program "+ ucProgram.selectedText,
                                                             userObj.LogInID + " is Load Page",
                                                              ((int)CommonEnum.PageName.FeeSetup).ToString(),
                                                             CommonEnum.PageName.FeeSetup.ToString(),
                                                             _pageUrl,
                                                             "");
                        #endregion
                        lblMsg.Text = "Fees inserted successfully.";
                    }
                    else
                    {
                        lblMsg.Text = "Fees could not inserted successfully.";
                    }

                }
                else
                {
                    feeSetup.FeeSetupID = feesSetupId;
                    bool updateResult = FeeSetupManager.Update(feeSetup);

                    //FeeSetupManager.Delete(feeSetup.FeeSetupID);
                    //FeeSetupManager.Insert(feeSetup);
                    if (updateResult)
                    {
                        lblMsg.Text = "Fees updated successfully.";
                    }
                    else
                    {
                        lblMsg.Text = "Fees could not updated successfully.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            LoadGrid();
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadGrid()
    {
        _programId = Convert.ToInt32(ucProgram.selectedValue);
        _sessionId = Convert.ToInt32(ucSession.selectedValue);
        _batchId = Convert.ToInt32(ucBatch.selectedValue);
        List<FeeSetup> list = FeeSetupManager.GetByProgramSession(_programId, _batchId);

        gvFeeSetup.DataSource = list;
        gvFeeSetup.DataBind();
    }
}