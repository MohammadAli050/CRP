using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.BasicSetup
{
    public partial class EquivalentCourseSetup : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.EquivalentCourseUI);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.EquivalentCourseUI));

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            #region Log Insert
            LogGeneralManager.Insert(
                    DateTime.Now,
                    BaseAcaCalCurrent.Code,
                    BaseAcaCalCurrent.FullCode,
                    BaseCurrentUserObj.LogInID,
                    "",
                    "",
                    "Page Load",
                    BaseCurrentUserObj.LogInID + " is Load Page",
                    "normal",
                    _pageId,
                    _pageName,
                    _pageUrl,
                    "");

            #endregion
            if (!IsPostBack)
            {
                LoadGvEquivalentCourse();
                LoadCourseForFilterDDL();
                lblMsg.Text = null;
            }
        }

        private void LoadCourseForFilterDDL()
        {
            try
            {
                ddlCourseForFilter.Items.Clear();
                ddlCourseForFilter.Items.Add(new ListItem("-Select Course-", "0"));

                List<Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.VersionCode + " - " + course.Credits + " - " + course.Title;
                        ddlCourseForFilter.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadCourseAddEquivalanceDDL()
        {
            try
            {
                ddlCourseAddEquivalance.Items.Clear();
                ddlCourseAddEquivalance.Items.Add(new ListItem("-Select Course-", "0"));

                List<Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.VersionCode + " - " + course.Credits + " - " + course.Title;
                        ddlCourseAddEquivalance.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadGvEquivalentCourse()
        {
            List<EquivalentCourseDTO> equivalentCourseList = EquiCourseDetailsManager.GetAllEquivalentCourse().ToList();
            LoadEquivalentCourseGroupList(equivalentCourseList);
        }

        private void LoadEquivalentCourseGroupList(List<EquivalentCourseDTO> equivalentCourseList)
        {
            List<EquivalentCourseDTO> newEquivalentCourseList2 = new List<EquivalentCourseDTO>();

            List<int> currentMasterIdList = new List<int>();
            for (int i = 0; i < equivalentCourseList.Count; i++)
            {
                currentMasterIdList.Add(equivalentCourseList[i].EquiCourseMasterId);
            }
            List<int> newCurrentMasterIdList = currentMasterIdList.Distinct().ToList();
            for (int i = 0; i < newCurrentMasterIdList.Count; i++)
            {
                List<EquivalentCourseDTO> equivalentCourseList3 = equivalentCourseList.Where(d => d.EquiCourseMasterId == newCurrentMasterIdList[i] ).ToList();

                EquivalentCourseDTO newEquivalentCourseObj = equivalentCourseList3.FirstOrDefault();
                EquivalentCourseDTO equivalentCourseObj = new EquivalentCourseDTO();
                equivalentCourseObj.GroupName = newEquivalentCourseObj.GroupName;
                equivalentCourseObj.GroupNo = newEquivalentCourseObj.GroupNo;
                equivalentCourseObj.EquiCourseMasterId = newEquivalentCourseObj.EquiCourseMasterId;
                equivalentCourseObj.VersionCode = null;
                for (int j = 0; j < equivalentCourseList3.Count; j++)
                {
                    if (newEquivalentCourseObj.VersionCode != null)
                    {
                        equivalentCourseObj.VersionCode = equivalentCourseObj.VersionCode + equivalentCourseList3[j].VersionCode + " (" + equivalentCourseList3[j].Credits + ")";
                        if (j < (equivalentCourseList3.Count - 1))
                        {
                            equivalentCourseObj.VersionCode += ", ";
                        }
                    }
                }
                newEquivalentCourseList2.Add(equivalentCourseObj);
            }

            SessionManager.SaveListToSession<EquivalentCourseDTO>(equivalentCourseList, "_equivalentCourseList");
            if (newEquivalentCourseList2 != null)
            {
                GvEquivalentCourse.DataSource = newEquivalentCourseList2.OrderBy(d => d.GroupName).ToList();
                GvEquivalentCourse.DataBind();
            }
            else
            {
                GvEquivalentCourse.DataSource = null;
                GvEquivalentCourse.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            lblMsg.Text = null;
            List<EquivalentCourseDTO> equivalentCourseDetailList = new List<EquivalentCourseDTO>(); //SessionManager.GetListFromSession<EquivalentCourseDTO>("_equivalentCourseList").ToList();
            if (equivalentCourseDetailList != null)
            {
                int programId = 0;
                string CourseNameNew = ddlCourseForFilter.SelectedValue;
                int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
                string vesionCode = null;
                //if (Convert.ToInt32(ddlProgram.SelectedValue)!=0)
                //{
                //    equivalentCourseDetailList = equivalentCourseDetailList.Where(x => x.ProgramId == Convert.ToInt32(ddlProgram.SelectedValue)).ToList();
                //}

                //if (courseId != 0)
                //{
                //    equivalentCourseDetailList = equivalentCourseDetailList.Where(x => x.CourseId == courseId && x.VersionId == versionId).ToList();
                //}
                if (!string.IsNullOrEmpty(txtCourseCodeForFilter.Text))
                {
                    vesionCode = Convert.ToString(txtCourseCodeForFilter.Text);//equivalentCourseDetailList.Where(x => x.VersionCode != null && x.VersionCode.Contains(txtCourseCode.Text)).ToList();
                }

                List<EquivalentCourseDTO> equivalentCourseList = EquiCourseDetailsManager.GetAllEquivalentCourseByParameters(programId, courseId, versionId, vesionCode);
                //SessionManager.SaveListToSession<EquivalentCourseDTO>(null, "_equivalentCourseList");
                //SessionManager.SaveListToSession<EquivalentCourseDTO>(equivalentCourseDetailList, "_equivalentCourseList");
                LoadEquivalentCourseGroupList(equivalentCourseList);
            }
        }

        protected void btnReLoad_Click(object sender, EventArgs e)
        {
            ddlCourseForFilter.SelectedValue = Convert.ToString("0");
            txtCourseCodeForFilter.Text = string.Empty;
            LoadGvEquivalentCourse();
            lblMsg.Text = null;
        }

        protected void btnAddEquivalentGroup_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModalPopupExtender1.Show();
                LoadCourseAddEquivalanceDDL();
                //txtEquivalanceGroupName.Enabled = true;
                //txtEquivalanceRemark.Enabled = true;
                lblMsg.Text = null;
                GvEquivalentCourseDetails.DataSource = null;
                GvEquivalentCourseDetails.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void lnkAddEquivalentCourse_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                lblMessage.Text = string.Empty; 
                this.ModalPopupExtender1.Show();
                LinkButton btn = (LinkButton)sender;
                int equiCourseMasterId = int.Parse(btn.CommandArgument.ToString());
                List<EquivalentCourseDTO> equivalentCourseDetailList = SessionManager.GetListFromSession<EquivalentCourseDTO>("_equivalentCourseList").Where(d => d.EquiCourseMasterId == equiCourseMasterId).ToList();

                EquivalentCourseDTO equivalentCourseDTOObj = equivalentCourseDetailList.Where(d => d.EquiCourseMasterId == equiCourseMasterId).FirstOrDefault();
                if (equivalentCourseDTOObj != null)
                {
                    EquiCourseMaster equiCourseMasterObj = new EquiCourseMaster();
                    equiCourseMasterObj = EquiCourseMasterManager.GetById(equivalentCourseDTOObj.EquiCourseMasterId);
                    if (equiCourseMasterObj != null)
                    {
                        txtEquivalanceGroupName.Text = equiCourseMasterObj.GroupName;
                        txtEquivalanceRemark.Text = equiCourseMasterObj.Remark;
                        hdnEquivalentCourseMasterId.Value = Convert.ToString(equivalentCourseDTOObj.EquiCourseMasterId);
                        lblEquiValanceMasterId.Text = Convert.ToString(equivalentCourseDTOObj.EquiCourseMasterId);
                    }
                }
                //LoadProgram2DDL();
                LoadCourseAddEquivalanceDDL();
                if (equivalentCourseDetailList != null)
                {
                    GvEquivalentCourseDetails.DataSource = equivalentCourseDetailList.Where(d => d.EquiCourseDetailId != 0).OrderBy(d => d.FormalCode).ToList();
                    GvEquivalentCourseDetails.DataBind();
                }
                else
                {
                    GvEquivalentCourseDetails.DataSource = null;
                    GvEquivalentCourseDetails.DataBind();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkRemoveEquivalentGroup_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                int equiCourseMasterId = int.Parse(btn.CommandArgument.ToString());
                bool result = EquiCourseMasterManager.Delete(equiCourseMasterId);
                if (result)
                {
                    lblMsg.Text = "Equivalent group removed successfully.";
                    LoadGvEquivalentCourse();
                }
                else
                {
                    lblMsg.Text = "Equivalent group could not removed.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        private void LoadEquivalentCourseDetailList(int equivalentCourseMasterId)
        {
            List<EquivalentCourseDTO> equivalentCourseDetailList = EquiCourseDetailsManager.GetAllEquivalentCourseByMasterId(equivalentCourseMasterId).Where(d => d.EquiCourseDetailId != null).ToList();
            if (equivalentCourseDetailList != null)
            {
                GvEquivalentCourseDetails.DataSource = equivalentCourseDetailList.OrderBy(d => d.FormalCode).ToList();
                GvEquivalentCourseDetails.DataBind();
            }
            else
            {
                GvEquivalentCourseDetails.DataSource = null;
                GvEquivalentCourseDetails.DataBind();
            }
        }
        
        protected void btnAdd_Click(object sender, EventArgs e) 
        {
            try 
            {
                lblMsg.Text = string.Empty;
                lblMessage.Text = string.Empty; 
                this.ModalPopupExtender1.Show();

                string CourseNameNew = ddlCourseAddEquivalance.SelectedValue;
                int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                if (!string.IsNullOrEmpty(lblEquiValanceMasterId.Text))
                {
                    int equivalanceMasterId = Convert.ToInt32(lblEquiValanceMasterId.Text);
                    if (courseId > 0)
                    {
                        InserEquivalanceDetail(equivalanceMasterId, courseId, versionId);
                    }
                    EditEquivalanceMaster(equivalanceMasterId);
                }
                else if (string.IsNullOrEmpty(lblEquiValanceMasterId.Text))
                {
                    int equivalanceMasterId = InsertEquivalanceMaster();
                    if (equivalanceMasterId > 0 & courseId > 0)
                    {
                        InserEquivalanceDetail(equivalanceMasterId, courseId, versionId);
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void EditEquivalanceMaster(int equivalanceMasterId)
        {
            EquiCourseMaster equiCourseMasterObj = new EquiCourseMaster();
            equiCourseMasterObj = EquiCourseMasterManager.GetById(equivalanceMasterId);
            if (equiCourseMasterObj!= null) 
            {
                if (equiCourseMasterObj.GroupName != Convert.ToString(txtEquivalanceGroupName.Text.Trim()) || equiCourseMasterObj.Remark != Convert.ToString(txtEquivalanceRemark.Text.Trim()))
                {
                    int maxGroupNo = Convert.ToInt32(EquiCourseMasterManager.GetMaxGroupNo().MaxMoneyReceiptNo);
                    if (!string.IsNullOrEmpty(txtEquivalanceGroupName.Text))
                    {
                        equiCourseMasterObj.GroupName = txtEquivalanceGroupName.Text;
                    }
                    else
                    {
                        equiCourseMasterObj.GroupName = Convert.ToString(maxGroupNo + 1);
                    }
                    equiCourseMasterObj.Remark = Convert.ToString(txtEquivalanceRemark.Text);

                    bool update = EquiCourseMasterManager.Update(equiCourseMasterObj);
                    if (update)
                    {
                        lblMessage.Text = "Equivalent group saved successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Equivalent group could not saved.";
                    }
                }
                else
                {
                    lblMessage.Text = "Equivalent group could not saved.";
                }
            }
        }

        private int InsertEquivalanceMaster()
        {
            int result = 0;
            try
            {
                int maxGroupNo = Convert.ToInt32(EquiCourseMasterManager.GetMaxGroupNo().MaxMoneyReceiptNo);
                EquiCourseMaster equiCourseMasterObj = new EquiCourseMaster();
                if (!string.IsNullOrEmpty(txtEquivalanceGroupName.Text))
                {
                    equiCourseMasterObj.GroupName = txtEquivalanceGroupName.Text;
                }
                else
                {
                    equiCourseMasterObj.GroupName = Convert.ToString(maxGroupNo + 1);
                }
                equiCourseMasterObj.GroupNo = maxGroupNo + 1;
                equiCourseMasterObj.CreatedBy = BaseCurrentUserObj.Id;
                equiCourseMasterObj.CreatedDate = DateTime.Now;
                equiCourseMasterObj.ModifiedBy = BaseCurrentUserObj.Id;
                equiCourseMasterObj.ModifiedDate = DateTime.Now;
                equiCourseMasterObj.Remark = Convert.ToString(txtEquivalanceRemark.Text);
                result = EquiCourseMasterManager.Insert(equiCourseMasterObj);
                if (result > 0)
                {
                    lblEquiValanceMasterId.Text = Convert.ToString(result);
                    lblMessage.Text = "Equivalent group created without course successfully.";
                }
                else
                {
                    lblMessage.Text = "Equivalent group could not created.";
                }

                return result;
            }
            catch (Exception ex) 
            {
                lblMessage.Text = ex.Message;
                return result;
            }
        }

        private void InserEquivalanceDetail(int equivalanceMasterId, int courseId, int versionId)
        {
            try
            {
                EquiCourseDetails equiCourseDetailsObj = new EquiCourseDetails();
                equiCourseDetailsObj.CourseId = courseId;
                equiCourseDetailsObj.VersionId = versionId;
                equiCourseDetailsObj.EquiCourseMasterId = equivalanceMasterId;
                equiCourseDetailsObj.ProgramId = Convert.ToInt32(0);
                equiCourseDetailsObj.CreatedBy = BaseCurrentUserObj.Id;
                equiCourseDetailsObj.CreatedDate = DateTime.Now;
                equiCourseDetailsObj.ModifiedBy = BaseCurrentUserObj.Id;
                equiCourseDetailsObj.ModifiedDate = DateTime.Now;

                int result = EquiCourseDetailsManager.Insert(equiCourseDetailsObj);
                if (result > 0)
                {
                    lblMessage.Text = "Equivalent course saved successfully.";
                }
                else
                {
                    lblMessage.Text = "Equivalent course could not saved.";
                }
                LoadEquivalentCourseDetailList(Convert.ToInt32(lblEquiValanceMasterId.Text));
                //LoadGvEquivalentCourse();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Hide();
            ClearAll();
            lblMessage.Text = string.Empty;
            int equivalanceMasterId =Convert.ToInt32(lblEquiValanceMasterId.Text);
            List<EquivalentCourseDTO> equivalentCourseList = EquiCourseDetailsManager.GetAllEquivalentCourse().ToList();
            if (equivalanceMasterId > 0)
            {
                equivalentCourseList = EquiCourseDetailsManager.GetAllEquivalentCourse().Where(d => d.EquiCourseMasterId == equivalanceMasterId).ToList();
            }
            LoadEquivalentCourseGroupList(equivalentCourseList);
            lblEquiValanceMasterId.Text = string.Empty;
        }

        protected void lnkDeleteEquivalentCourse_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModalPopupExtender1.Show();
                LinkButton btn = (LinkButton)sender;
                int equiCourseDetailId = int.Parse(btn.CommandArgument.ToString());
                bool result = EquiCourseDetailsManager.Delete(equiCourseDetailId);
                if (result)
                {
                    lblMessage.Text = "Equivalent course removed successfully.";
                }
                else
                {
                    lblMessage.Text = "Equivalent course could not removed.";
                }
                LoadEquivalentCourseDetailList(Convert.ToInt32(lblEquiValanceMasterId.Text));
                //LoadGvEquivalentCourse();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void ClearAll()
        {
            txtEquivalanceGroupName.Text = string.Empty;
            txtEquivalanceRemark.Text = string.Empty;
            lblMsg.Text = string.Empty;
            lblMessage.Text = string.Empty;
        }

        protected void GvEquivalentCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadGvEquivalentCourse();
            GvEquivalentCourse.PageIndex = e.NewPageIndex;
            GvEquivalentCourse.DataBind();
        }

    }
}