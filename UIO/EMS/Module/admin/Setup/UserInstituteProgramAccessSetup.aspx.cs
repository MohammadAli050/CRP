using CommonUtility;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Math;
using EMS.Module;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInstituteProgramAccessSetup : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.UserInstituteProgramAccessSetup);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.UserInstituteProgramAccessSetup));

    protected void Page_Load(object sender, EventArgs e)
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        var id = _pageUrl.Substring(_pageUrl.LastIndexOf('=') + 1);

        base.CheckPage_Load();

        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (!IsPostBack)
        {
            divUpdateAll.Visible = false;
            divGridView.Visible = false;
            LoadInstitute();
            LoadAllUser();
        }
    }

    private void LoadAllUser()
    {
        try
        {
            var UserList = UserManager.GetAll();
            ddlUser.Items.Clear();
            ddlUser.AppendDataBoundItems = true;
            ddlUser.Items.Add(new ListItem("-Select-", "0"));
            if (UserList != null && UserList.Count > 0)
            {
                ddlUser.Items.AddRange(UserList.Select(x => new ListItem(x.LogInID + "(" + x.Role.RoleName + ")", x.User_ID.ToString())).ToArray());
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadInstitute()
    {
        try
        {
            var InstituteList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();

            ddlInstitute.Items.Clear();
            ddlInstitute.AppendDataBoundItems = true;
            ddlInstitute.Items.Add(new ListItem("All", "0"));

            if (InstituteList != null && InstituteList.Any())
            {
                ddlInstitute.DataTextField = "InstituteName";
                ddlInstitute.DataValueField = "InstituteId";
                ddlInstitute.DataSource = InstituteList.OrderBy(x => x.InstituteName);
                ddlInstitute.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearBasicInfo();
            ClearGridView();
            ClearProgramGridView();
            int UserId = Convert.ToInt32(ddlUser.SelectedValue);
            if (UserId > 0)
            {
                User user = UserManager.GetById(UserId);
                if (user != null)
                {
                    divGridView.Visible = true;
                    LoadBasicInfo(user);

                    BindInstituteGrid(UserId);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearBasicInfo();
            ClearGridView();
            ClearProgramGridView();
            int UserId = Convert.ToInt32(ddlUser.SelectedValue);
            if (UserId > 0)
            {
                User user = UserManager.GetById(UserId);
                if (user != null)
                {
                    divGridView.Visible = true;
                    LoadBasicInfo(user);

                    BindInstituteGrid(UserId);
                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void ClearGridView()
    {
        divUpdateAll.Visible = false;
        gvInstitute.DataSource = null;
        gvInstitute.DataBind();
    }
    private void ClearProgramGridView()
    {
        divUpdateAll.Visible = false;
        gvProgramList.DataSource = null;
        gvProgramList.DataBind();
    }


    private void LoadBasicInfo(User user)
    {
        try
        {
            lblName.Text = user.Person != null ? user.Person.FullName : "";
            lblEmail.Text = user.Person != null ? user.Person.Email : "";
            lblRole.Text = user.Role.RoleName;
            lblMobile.Text = user.Person != null ? user.Person.Phone : "";
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearBasicInfo()
    {
        lblName.Text = string.Empty;
        lblRole.Text = string.Empty;
        lblEmail.Text = string.Empty;
        lblRole.Text = string.Empty;
        divGridView.Visible = false;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnSelect = (Button)sender;

            ClearProgramGridView();

            int instituteId = Convert.ToInt32(btnSelect.CommandArgument);
            // Handle the click event for the selected institute

            int userId = Convert.ToInt32(ddlUser.SelectedValue);

            if (userId > 0 && instituteId > 0)
            {
                BindProgramGridview(userId, instituteId);
            }

        }
        catch (Exception ex)
        {
        }
    }



    private void BindInstituteGrid(int userId)
    {
        try
        {
            int instituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
            var AllInstituteList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1 && (x.InstituteId == instituteId || instituteId == 0)).ToList();

            var userAccessObj = ucamContext.UserAccessPrograms.Where(x => x.User_ID == userId).FirstOrDefault();

            var programList = new List<UCAMDAL.Program>();
            var allProgramList = ucamContext.Programs.ToList();

            #region Program Access


            if (userAccessObj != null)
            {
                var accessProgramList = userAccessObj.AccessPattern != null ? userAccessObj.AccessPattern.Split('-').ToList() : new List<string>();
                if (accessProgramList != null && accessProgramList.Count > 0)
                {
                    foreach (var item in accessProgramList)
                    {
                        int programId = 0;
                        if (int.TryParse(item, out programId))
                        {
                            var program = ucamContext.Programs.Where(x => x.ProgramID == programId).FirstOrDefault();
                            if (program != null)
                            {
                                programList.Add(program);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Institute Wise Program Count

            if (AllInstituteList != null && AllInstituteList.Count > 0)
            {
                foreach (var institute in AllInstituteList)
                {
                    institute.Attribute1 = "0";
                    institute.Attribute2 = "0";
                    int TotalProgram = 0;
                    TotalProgram = allProgramList.Where(x => x.InstituteId == institute.InstituteId).Count();
                    institute.Attribute1 = TotalProgram.ToString();

                    int accessProgram = 0;
                    accessProgram = programList.Where(x => x.InstituteId == institute.InstituteId).Count();
                    institute.Attribute2 = accessProgram.ToString();

                }
            }

            #endregion


            gvInstitute.DataSource = AllInstituteList;
            gvInstitute.DataBind();


        }
        catch (Exception ex)
        {
        }
    }
    private void BindProgramGridview(int userId, int instituteId)
    {
        try
        {

            var ProgramList = ucamContext.Programs.Where(x => x.InstituteId == instituteId).ToList();

            var userAccessObj = ucamContext.UserAccessPrograms.Where(x => x.User_ID == userId).FirstOrDefault();

            var accessedprogramList = new List<UCAMDAL.Program>();

            #region Program Access

            if (userAccessObj != null)
            {
                var accessProgramList = userAccessObj.AccessPattern != null ? userAccessObj.AccessPattern.Split('-').ToList() : new List<string>();
                if (accessProgramList != null && accessProgramList.Count > 0)
                {
                    foreach (var item in accessProgramList)
                    {
                        int programId = 0;
                        if (int.TryParse(item, out programId))
                        {
                            var program = ucamContext.Programs.Where(x => x.ProgramID == programId).FirstOrDefault();
                            if (program != null)
                            {
                                accessedprogramList.Add(program);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Update the Program in ProgramList which exists in Accessed Program List

            if (ProgramList != null && ProgramList.Count > 0)
            {
                foreach (var program in ProgramList)
                {
                    program.Attribute1 = "0";
                    var existsProgram = accessedprogramList.Where(x => x.ProgramID == program.ProgramID).FirstOrDefault();
                    if (existsProgram != null)
                    {
                        program.Attribute1 = "1"; // Mark as selected
                    }
                }
            }
            #endregion

            gvProgramList.DataSource = ProgramList.OrderBy(x => x.ShortName);
            gvProgramList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelect = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkSelect.NamingContainer;

        string programName = "";
        int programId = 0;

        #region Get ProgramId and Name value

        HiddenField hdnProgramId = (HiddenField)row.FindControl("hdnProgramId");
        Label lblProgramName = (Label)row.FindControl("lblPrgName");
        if (hdnProgramId != null)
        {
            programId = Convert.ToInt32(hdnProgramId.Value);
        }

        if (lblProgramName != null)
        {
            programName = lblProgramName.Text;
        }

        #endregion

        int userId = Convert.ToInt32(ddlUser.SelectedValue);
        int InstId = 0;
        var prg = ucamContext.Programs.Find(programId);
        InstId = prg != null && prg.InstituteId != null ? (int)prg.InstituteId : 0;

        if (chkSelect.Checked)
        {
            int result = AccessGive(userId, programId);
            if (result > 0)
            {
                showAlert(programName + " program access given to user successfully.");

                MisscellaneousCommonMethods.InsertLog(userObj.LogInID, "User Institute Program Access Setup", "Program access given to User : " + ddlUser.SelectedItem.ToString()
                    + ", Program : " + programName, "", "", _pageId, _pageName, _pageUrl);
            }

        }
        else
        {

            int result = AccessRemove(userId, programId);
            if (result > 0)
            {
                showAlert(programName + " program access removed from user successfully.");

                MisscellaneousCommonMethods.InsertLog(userObj.LogInID, "User Institute Program Access Setup", "Program access removed from User : " + ddlUser.SelectedItem.ToString()
                    + ", Program : " + programName, "", "", _pageId, _pageName, _pageUrl);
            }

        }


        BindInstituteGrid(userId);
        BindProgramGridview(userId, InstId);
    }

    private int AccessRemove(int userId, int programId)
    {
        int result = 0;
        try
        {
            // Remove programId from the list of selected programs
            var accessObj = ucamContext.UserAccessPrograms.Where(x => x.User_ID == userId).FirstOrDefault();
            if (accessObj != null)
            {
                var accessPattern = accessObj.AccessPattern != null ? accessObj.AccessPattern.Split('-').ToList() : new List<string>();
                if (accessPattern.Contains(programId.ToString()))
                {
                    accessPattern.Remove(programId.ToString());
                    accessObj.AccessPattern = string.Join("-", accessPattern);
                    accessObj.ModifiedBy = userObj.Id;
                    accessObj.ModifiedDate = DateTime.Now;
                    ucamContext.Entry(accessObj).State = System.Data.Entity.EntityState.Modified;
                    ucamContext.SaveChanges();

                    result++;


                }
            }
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    private int AccessGive(int userId, int programId)
    {
        int result = 0;
        try
        {
            var accessObj = ucamContext.UserAccessPrograms.Where(x => x.User_ID == userId).FirstOrDefault();
            if (accessObj == null)
            {
                accessObj = new UCAMDAL.UserAccessProgram();
                accessObj.User_ID = userId;
                accessObj.AccessPattern = programId.ToString();
                accessObj.CreatedBy = userObj.Id;
                accessObj.CreatedDate = DateTime.Now;
                ucamContext.UserAccessPrograms.Add(accessObj);
                ucamContext.SaveChanges();
                result++;
            }
            else
            {
                var accessPattern = accessObj.AccessPattern != null ? accessObj.AccessPattern.Split('-').ToList() : new List<string>();
                if (!accessPattern.Contains(programId.ToString()))
                {
                    accessPattern.Add(programId.ToString());
                    accessObj.AccessPattern = string.Join("-", accessPattern);
                    accessObj.ModifiedBy = userObj.Id;
                    accessObj.ModifiedDate = DateTime.Now;
                    ucamContext.Entry(accessObj).State = System.Data.Entity.EntityState.Modified;
                    ucamContext.SaveChanges();
                    result++;
                }
            }

        }
        catch (Exception ex)
        {
        }
        return result;
    }

    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }

    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        try
        {
            int userId = Convert.ToInt32(ddlUser.SelectedValue);
            int InstituteId = 0;
            if (userId > 0)
            {
                string givelist = "", removedList = "";

                int AccessGiven = 0, AccessRemoved = 0;

                #region Give access to all checked checkbox and also remove access all unchecked checkbox

                foreach (GridViewRow row in gvProgramList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    HiddenField hdnProgramId = (HiddenField)row.FindControl("hdnProgramId");
                    Label lblProgramName = (Label)row.FindControl("lblPrgName");
                    if (hdnProgramId != null && lblProgramName != null)
                    {
                        int programId = Convert.ToInt32(hdnProgramId.Value);
                        if (InstituteId == 0)
                        {
                            var prg = ucamContext.Programs.Find(programId);
                            InstituteId = prg != null && prg.InstituteId != null ? (int)prg.InstituteId : 0;
                        }
                        string programName = lblProgramName.Text;
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            int result = AccessGive(userId, programId);
                            if (result > 0)
                            {
                                AccessGiven++;
                                givelist += programName + ", ";

                            }
                        }
                        else
                        {
                            int result = AccessRemove(userId, programId);
                            if (result > 0)
                            {
                                AccessRemoved++;
                                removedList += programName + ", ";
                            }
                        }
                    }
                }
                if (AccessGiven > 0 || AccessRemoved > 0)
                {
                    string msg = "";
                    if (AccessGiven > 0)
                    {
                        givelist = givelist.TrimEnd(',', ' ');
                        msg += AccessGiven + " program access given to user successfully. (" + givelist + ") ";
                    }
                    if (AccessRemoved > 0)
                    {
                        removedList = removedList.TrimEnd(',', ' ');
                        msg += AccessRemoved + " program access removed from user successfully. (" + removedList + ") ";
                    }
                    MisscellaneousCommonMethods.InsertLog(userObj.LogInID, "User Institute Program Access Setup", "Program access updated to User : " + ddlUser.SelectedItem.ToString()
                        + ", Given Program : " + givelist + ", Removed Program : " + removedList, "", "", _pageId, _pageName, _pageUrl);
                    showAlert(msg);
                }
                else
                {
                    showAlert("No changes were made.");
                }
                BindInstituteGrid(userId);
                BindProgramGridview(userId, InstituteId);

                #endregion

            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)sender;
        divUpdateAll.Visible = true;
        foreach (GridViewRow row in gvProgramList.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            if (chkSelect != null)
            {
                chkSelect.Checked = chkHeader.Checked;
            }
        }
    }
}