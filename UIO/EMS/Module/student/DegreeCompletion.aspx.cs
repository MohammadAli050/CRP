using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using System.Drawing;

namespace EMS.Module.student
{
    public partial class DegreeCompletion : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.DegreeCompletion);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.DegreeCompletion));
        int userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;
            //pnlMessage.Visible = false;
            //lblCount.Text = "0";

            if (!IsPostBack)
            {
                ////txtPublicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ucProgram.LoadDropdownWithUserAccess(userId);
                ucProgram.selectedValue = "0";
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoad_Click(null, null);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();

                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                string roll = txtRoll.Text.Trim();

                if (programId == 0 && string.IsNullOrEmpty(roll))
                {
                    ShowMessage("Please select Program.");
                    return;
                }

                if (batchId == 0 && string.IsNullOrEmpty(roll))
                {
                    ShowMessage("Please select batch.");
                    return;
                }

                LoadStudent(programId, batchId, roll);

            }
            catch (Exception)
            {
            }
        }

        #region Methods
        private void LoadStudent(int programId, int batchId, string roll)
        {
            List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, batchId, roll);

            List<LogicLayer.BusinessObjects.DegreeCompletion> studentDegree = new List<LogicLayer.BusinessObjects.DegreeCompletion>();

            if (studentList != null)
                studentList = studentList.OrderBy(s => s.Roll).ToList();

            foreach (var item in studentList)
            {
                var perStudentDegree = DegreeCompletionManager.GetByStudentId(item.StudentID);
                var studentTranscriptInfo = StudentTranscriptInfoManager.GetByStudentId(item.StudentID);

                LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = new LogicLayer.BusinessObjects.DegreeCompletion();

                degreeCompletion.StudentId = item.StudentID;
                degreeCompletion.DegreeTranscriptNumber = "";
                degreeCompletion.DegreeCertificateNumber = "";
                degreeCompletion.IsDegreeComplete = false;
                degreeCompletion.Attribute1 = item.Name;
                degreeCompletion.Attribute2 = item.Roll;
                if (studentTranscriptInfo != null)
                {
                    degreeCompletion.PublicationDate = studentTranscriptInfo.PublicationDate;
                    degreeCompletion.StudentTranscriptInfoId = studentTranscriptInfo.Id;
                    degreeCompletion.ExaminationMonth = studentTranscriptInfo.ExaminationMonth;

                }

                if (perStudentDegree != null)
                {

                    degreeCompletion.DegreeTranscriptNumber =
                        string.IsNullOrEmpty(perStudentDegree.DegreeTranscriptNumber)
                            ? "" : perStudentDegree.DegreeTranscriptNumber;

                    degreeCompletion.DegreeTranscriptGenerateDate =
                        perStudentDegree.DegreeTranscriptGenerateDate.HasValue
                            ? perStudentDegree.DegreeTranscriptGenerateDate
                            : null;

                    degreeCompletion.DegreeCertificateNumber =
                        string.IsNullOrEmpty(perStudentDegree.DegreeCertificateNumber)
                            ? "" : perStudentDegree.DegreeCertificateNumber;

                    degreeCompletion.DegreeCertificateGenerateDate =
                        perStudentDegree.DegreeCertificateGenerateDate.HasValue ? perStudentDegree.DegreeCertificateGenerateDate : null;

                    //degreeCompletion.IsDegreeComplete = perStudentDegree.IsDegreeComplete;
                    degreeCompletion.IsDegreeComplete =
                        perStudentDegree.IsDegreeComplete.HasValue ? perStudentDegree.IsDegreeComplete : false;

                    degreeCompletion.Attribute1 = item.Name;
                    degreeCompletion.Attribute2 = item.Roll;
                }


                studentDegree.Add(degreeCompletion);
            }

            if (studentDegree.Any())
            {
                gvStudentList.DataSource = studentDegree;
                gvStudentList.DataBind();
            }
            else
            {
                ShowMessage("No data found.");
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
            }

            //lblCount.Text = studentDegree.Count().ToString();
        }

        private void ClearGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();

            //lblCount.Text = "0";
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Course", "swal('" + msg + "');", true);

        }
        #endregion

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    ckBox.Checked = chk.Checked;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnGenerateTranscript_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 1;
                int maxValue = DegreeCompletionManager.GetAll().Max(x => x.DegreeCompletionId) + 1;

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    TextBox txtTranscript = (TextBox)row.FindControl("txtTrasncript");

                    if (ckBox.Checked)
                    {
                        txtTranscript.Text = maxValue.ToString();
                        count = count + maxValue;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnTrasncriptSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool updateSuccess = false;
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    HiddenField studentId = (HiddenField)row.FindControl("hdnId");
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    TextBox txtTranscript = (TextBox)row.FindControl("txtTrasncript");

                    if (ckBox.Checked && !string.IsNullOrWhiteSpace(txtTranscript.Text))
                    {
                        var isDuplicate = DegreeCompletionManager.GetAll().Any(x => x.DegreeTranscriptNumber == txtTranscript.Text);

                        if (!isDuplicate)
                        {
                            LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(Convert.ToInt32(studentId.Value));

                            if (degreeCompletion == null)
                            {
                                degreeCompletion = new LogicLayer.BusinessObjects.DegreeCompletion();

                                degreeCompletion.StudentId = Convert.ToInt32(studentId.Value);
                                degreeCompletion.DegreeTranscriptNumber = txtTranscript.Text;
                                degreeCompletion.DegreeTranscriptGenerateDate = DateTime.Now;
                                degreeCompletion.CreatedBy = 1;
                                degreeCompletion.CreatedDate = DateTime.Now;

                                int isInsert = DegreeCompletionManager.Insert(degreeCompletion);
                                if (isInsert > 0)
                                {
                                    updateSuccess = true;
                                }
                            }
                            else
                            {
                                //degreeCompletion.StudentId = Convert.ToInt32(studentId.Value);
                                degreeCompletion.DegreeTranscriptNumber = txtTranscript.Text;
                                degreeCompletion.DegreeTranscriptGenerateDate = DateTime.Now;
                                degreeCompletion.ModifiedBy = 1;
                                degreeCompletion.ModifiedDate = DateTime.Now;

                                bool isUpdate = DegreeCompletionManager.Update(degreeCompletion);
                                if (isUpdate)
                                {
                                    updateSuccess = true;
                                }
                            }
                        }
                    }
                }

                if (updateSuccess)
                {
                    ShowMessage("Successful");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnGenerateCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 1;
                int maxValue = DegreeCompletionManager.GetAll().Max(x => x.DegreeCompletionId) + 1;

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    TextBox txtCertificate = (TextBox)row.FindControl("txtCertificate");

                    if (ckBox.Checked)
                    {
                        txtCertificate.Text = maxValue.ToString();
                        count = count + maxValue;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCertificateSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool updateSuccess = false;
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    HiddenField studentId = (HiddenField)row.FindControl("hdnId");
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    TextBox txtCertificate = (TextBox)row.FindControl("txtCertificate");

                    if (ckBox.Checked && !string.IsNullOrWhiteSpace(txtCertificate.Text))
                    {
                        LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(Convert.ToInt32(studentId.Value));

                        if (degreeCompletion == null)
                        {
                            degreeCompletion = new LogicLayer.BusinessObjects.DegreeCompletion();

                            degreeCompletion.StudentId = Convert.ToInt32(studentId.Value);
                            degreeCompletion.DegreeCertificateNumber = txtCertificate.Text;
                            degreeCompletion.DegreeCertificateGenerateDate = DateTime.Now;
                            degreeCompletion.CreatedBy = 1;
                            degreeCompletion.CreatedDate = DateTime.Now;

                            int isInsert = DegreeCompletionManager.Insert(degreeCompletion);
                            if (isInsert > 0)
                            {
                                updateSuccess = true;
                            }
                        }
                        else
                        {
                            degreeCompletion.DegreeCertificateNumber = txtCertificate.Text;
                            degreeCompletion.DegreeCertificateGenerateDate = DateTime.Now;
                            degreeCompletion.ModifiedBy = 1;
                            degreeCompletion.ModifiedDate = DateTime.Now;

                            bool isUpdate = DegreeCompletionManager.Update(degreeCompletion);
                            if (isUpdate)
                            {
                                updateSuccess = true;
                            }
                        }
                    }
                }

                if (updateSuccess)
                {
                    ShowMessage("Successful");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnDegreeCompleteSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    HiddenField studentId = (HiddenField)row.FindControl("hdnId");
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    CheckBox ckDegree = (CheckBox)row.FindControl("chkDegreeComplete");

                    LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(Convert.ToInt32(studentId.Value));

                    if (degreeCompletion == null)
                    {
                        degreeCompletion = new LogicLayer.BusinessObjects.DegreeCompletion();

                        degreeCompletion.StudentId = Convert.ToInt32(studentId.Value);
                        degreeCompletion.IsDegreeComplete = ckDegree.Checked;
                        degreeCompletion.CreatedBy = 1;
                        degreeCompletion.CreatedDate = DateTime.Now;

                        int isInsert = DegreeCompletionManager.Insert(degreeCompletion);

                        if (isInsert > 0)
                        {
                            ShowMessage("Successful");
                        }
                        if (isInsert > 0 && ckDegree.Checked)
                        {
                            Student std = StudentManager.GetById(Convert.ToInt32(studentId.Value));
                            string msg = BaseCurrentUserObj.LogInID + " Save Degree Complete Status to Complete";
                            InsertLog("Degree Completion Status", msg, std.Roll);
                        }
                    }
                    else
                    {
                        bool PrevStatus = Convert.ToBoolean(degreeCompletion.IsDegreeComplete);
                        degreeCompletion.IsDegreeComplete = ckDegree.Checked;
                        degreeCompletion.ModifiedBy = 1;
                        degreeCompletion.ModifiedDate = DateTime.Now;

                        bool isUpdate = DegreeCompletionManager.Update(degreeCompletion);

                        if (isUpdate)
                        {
                            ShowMessage("Successful");

                        }
                        if (isUpdate && (PrevStatus != ckDegree.Checked))
                        {
                            string sta = "InComplete";
                            if (ckDegree.Checked)
                                sta = "Complete";
                            Student std = StudentManager.GetById(Convert.ToInt32(studentId.Value));
                            string msg = BaseCurrentUserObj.LogInID + " Update Degree Complete Status to " + sta;
                            InsertLog("Degree Completion Status", msg, std.Roll);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iInsert = 0;
                int iUpdate = 0;

                foreach (GridViewRow gvrow in gvStudentList.Rows)
                {
                    Label lblRoll = (Label)gvrow.FindControl("lblRoll");
                    TextBox txtPublicationDate = (TextBox)gvrow.FindControl("txtPublicationDate");

                    HiddenField hdnStudentId = (HiddenField)gvrow.FindControl("hdnId");
                    HiddenField hdnTranscriptInfoId = (HiddenField)gvrow.FindControl("hdnTranscriptInfoId");


                    //int Id = Convert.ToInt32(hdnId.Value);
                    int studentId = Convert.ToInt32(hdnStudentId.Value);
                    int transcriptInfoId = Convert.ToInt32(hdnTranscriptInfoId.Value);

                    CheckBox chkSelect = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {

                        if (transcriptInfoId != 0)
                        {
                            StudentTranscriptInfo stdtranscriptInfo = StudentTranscriptInfoManager.GetById(transcriptInfoId);
                            DateTime? publicationDate = null;
                            try
                            {
                                publicationDate = DateTime.ParseExact(txtPublicationDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            }
                            catch (Exception)
                            { }

                            stdtranscriptInfo.PublicationDate = publicationDate;

                            stdtranscriptInfo.ModifiedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.ModifiedDate = DateTime.Now;

                            bool isUpdate = StudentTranscriptInfoManager.Update(stdtranscriptInfo);
                            if (isUpdate)
                            {
                                iUpdate++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Transcript Info Update",
                                                                     BaseCurrentUserObj.LogInID + " updated Publication Date for Roll : " + lblRoll.Text + ", Publication Date: " + txtPublicationDate.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }
                        }
                        else
                        {

                            StudentTranscriptInfo stdtranscriptInfo = new StudentTranscriptInfo();

                            Nullable<DateTime> publicationDate = null;

                            try
                            {
                                publicationDate = DateTime.ParseExact(txtPublicationDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            }
                            catch (Exception)
                            { }

                            stdtranscriptInfo.StudentId = studentId;
                            stdtranscriptInfo.PublicationDate = publicationDate;
                            stdtranscriptInfo.PreparedDate = null;//PreparedDate;

                            stdtranscriptInfo.Attribute1 = null;
                            stdtranscriptInfo.CreatedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.CreatedDate = DateTime.Now;

                            int isInsert = StudentTranscriptInfoManager.Insert(stdtranscriptInfo);
                            if (isInsert > 0)
                            {
                                iInsert++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Transcript Info Insert",
                                                                     BaseCurrentUserObj.LogInID + " saved Publication Date for Roll : " + lblRoll.Text + ", Publication Date: " + txtPublicationDate.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }

                        }

                    }
                }
                btnLoad_Click(null, null);
                ShowMessage("Saved : " + iInsert + ";  Update : " + iUpdate);

            }
            catch (Exception)
            {
            }
        }

        protected void btnSaveExaminationMonth_Click(object sender, EventArgs e)
        {
            try
            {
                int iInsert = 0;
                int iUpdate = 0;

                foreach (GridViewRow gvrow in gvStudentList.Rows)
                {
                    Label lblRoll = (Label)gvrow.FindControl("lblRoll");
                    TextBox txtExaminationMonth = (TextBox)gvrow.FindControl("txtExaminationMonth");

                    HiddenField hdnStudentId = (HiddenField)gvrow.FindControl("hdnId");
                    HiddenField hdnTranscriptInfoId = (HiddenField)gvrow.FindControl("hdnTranscriptInfoId");


                    //int Id = Convert.ToInt32(hdnId.Value);
                    int studentId = Convert.ToInt32(hdnStudentId.Value);
                    int transcriptInfoId = Convert.ToInt32(hdnTranscriptInfoId.Value);

                    CheckBox chkSelect = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {

                        if (transcriptInfoId != 0)
                        {
                            StudentTranscriptInfo stdtranscriptInfo = StudentTranscriptInfoManager.GetById(transcriptInfoId);
                            DateTime? publicationDate = null;


                            stdtranscriptInfo.ExaminationMonth = txtExaminationMonth.Text;

                            stdtranscriptInfo.ModifiedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.ModifiedDate = DateTime.Now;

                            bool isUpdate = StudentTranscriptInfoManager.Update(stdtranscriptInfo);
                            if (isUpdate)
                            {
                                iUpdate++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Examination Month Update",
                                                                     BaseCurrentUserObj.LogInID + " updated Examination Month for Roll : " + lblRoll.Text + ", Month: " + txtExaminationMonth.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }
                        }
                        else
                        {

                            StudentTranscriptInfo stdtranscriptInfo = new StudentTranscriptInfo();
                            stdtranscriptInfo.StudentId = studentId;
                            stdtranscriptInfo.ExaminationMonth = txtExaminationMonth.Text;
                            stdtranscriptInfo.PreparedDate = null;//PreparedDate;

                            stdtranscriptInfo.Attribute1 = null;
                            stdtranscriptInfo.CreatedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.CreatedDate = DateTime.Now;

                            int isInsert = StudentTranscriptInfoManager.Insert(stdtranscriptInfo);
                            if (isInsert > 0)
                            {
                                iInsert++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Examination Month Insert",
                                                                     BaseCurrentUserObj.LogInID + " saved Examination Month for Roll : " + lblRoll.Text + ", Examination Month : " + txtExaminationMonth.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }

                        }

                    }
                }
                btnLoad_Click(null, null);
                ShowMessage("Saved : " + iInsert + ";  Update : " + iUpdate);

            }
            catch (Exception)
            {
            }
        }

        protected void InsertLog(string eventaName, string msg, string Roll)
        {
            LogGeneralManager.Insert(
                                        DateTime.Now,
                                        BaseAcaCalCurrent.Code,
                                        BaseAcaCalCurrent.FullCode,
                                        BaseCurrentUserObj.LogInID,
                                        "",
                                        "",
                                        eventaName,
                                        msg,
                                        "normal",
                                        _pageId.ToString(),
                                        _pageName.ToString(),
                                        _pageUrl,
                                        Roll);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    if (ckBox.Checked)
                    {
                        TextBox txtPublicationDate = (TextBox)row.FindControl("txtPublicationDate");
                        txtPublicationDate.Text = txtApplyPublicationDate.Text;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            btnTrasncriptSave_Click(null, null);
            btnCertificateSave_Click(null, null);
            btnDegreeCompleteSave_Click(null, null);
            try
            {
                int iInsert = 0;
                int iUpdate = 0;

                foreach (GridViewRow gvrow in gvStudentList.Rows)
                {
                    Label lblRoll = (Label)gvrow.FindControl("lblRoll");
                    TextBox txtExaminationMonth = (TextBox)gvrow.FindControl("txtExaminationMonth");
                    TextBox txtPublicationDate = (TextBox)gvrow.FindControl("txtPublicationDate");
                    HiddenField hdnStudentId = (HiddenField)gvrow.FindControl("hdnId");
                    HiddenField hdnTranscriptInfoId = (HiddenField)gvrow.FindControl("hdnTranscriptInfoId");


                    //int Id = Convert.ToInt32(hdnId.Value);
                    int studentId = Convert.ToInt32(hdnStudentId.Value);
                    int transcriptInfoId = Convert.ToInt32(hdnTranscriptInfoId.Value);

                    CheckBox chkSelect = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {

                        if (transcriptInfoId != 0)
                        {
                            StudentTranscriptInfo stdtranscriptInfo = StudentTranscriptInfoManager.GetById(transcriptInfoId);
                            Nullable<DateTime> publicationDate = null;

                            try
                            {
                                publicationDate = DateTime.ParseExact(txtPublicationDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            }
                            catch (Exception)
                            { }

                            stdtranscriptInfo.PublicationDate = publicationDate;
                            stdtranscriptInfo.ExaminationMonth = txtExaminationMonth.Text;

                            stdtranscriptInfo.ModifiedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.ModifiedDate = DateTime.Now;

                            bool isUpdate = StudentTranscriptInfoManager.Update(stdtranscriptInfo);
                            if (isUpdate)
                            {
                                iUpdate++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Examination Month Update",
                                                                     BaseCurrentUserObj.LogInID + " updated Examination Month for Roll : " + lblRoll.Text + ", Month: " + txtExaminationMonth.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }
                        }
                        else
                        {

                            StudentTranscriptInfo stdtranscriptInfo = new StudentTranscriptInfo();
                            Nullable<DateTime> publicationDate = null;

                            try
                            {
                                publicationDate = DateTime.ParseExact(txtPublicationDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            }
                            catch (Exception)
                            { }
                            stdtranscriptInfo.StudentId = studentId;
                            stdtranscriptInfo.PublicationDate = publicationDate;
                            stdtranscriptInfo.ExaminationMonth = txtExaminationMonth.Text;
                            stdtranscriptInfo.PreparedDate = null;//PreparedDate;

                            stdtranscriptInfo.Attribute1 = null;
                            stdtranscriptInfo.CreatedBy = BaseCurrentUserObj.Id;
                            stdtranscriptInfo.CreatedDate = DateTime.Now;

                            int isInsert = StudentTranscriptInfoManager.Insert(stdtranscriptInfo);
                            if (isInsert > 0)
                            {
                                iInsert++;
                                #region Log Insert
                                LogGeneralManager.Insert(
                                                                     DateTime.Now,
                                                                     BaseAcaCalCurrent.Code,
                                                                     BaseAcaCalCurrent.FullCode,
                                                                     BaseCurrentUserObj.LogInID,
                                                                     "",
                                                                     "",
                                                                     "Examination Month Insert",
                                                                     BaseCurrentUserObj.LogInID + " saved Examination Month for Roll : " + lblRoll.Text + ", Examination Month : " + txtExaminationMonth.Text.Trim(),
                                                                     "normal",
                                                                      ((int)CommonEnum.PageName.StudentTranscriptInfoEntry).ToString(),
                                                                     CommonEnum.PageName.StudentTranscriptInfoEntry.ToString(),
                                                                     _pageUrl,
                                                                     lblRoll.Text);
                                #endregion
                            }

                        }

                    }
                }
                btnLoad_Click(null, null);
                ShowMessage("Saved : " + iInsert + ";  Update : " + iUpdate);

            }
            catch (Exception)
            {
            }
        }
    }
}