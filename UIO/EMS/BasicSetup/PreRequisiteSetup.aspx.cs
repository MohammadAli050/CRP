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
    public partial class PreRequisiteSetup : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.PreRequisiteUI);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.PreRequisiteUI));

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
                    "Pre Requisite Setup Page Load",
                    BaseCurrentUserObj.LogInID + " is Load Pre Requisite Setup Page",
                    "normal",
                    _pageId,
                    _pageName,
                    _pageUrl,
                    "");

            #endregion
            if(!IsPostBack)
            {
                LoadGvPreRequisiteSetList();
                LoadCourseDDL();
                LoadMainCourseDDL();
                LoadPreReqCourseDDL();
                LoadProgramDDL();
                lblMsg.Text = null;
                SessionManager.SaveListToSession<PreRequisiteSetDTO>(null, "_preRequisiteSetCourseList");
            }
        }

        private void LoadCourseDDL()
        {
            try
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("-Select Course-", "0"));

                List<Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.VersionCode + " - " + course.Credits + " - " + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        
        private void LoadMainCourseDDL()
        {
            try
            {
                ddlMainCourse.Items.Clear();
                ddlMainCourse.Items.Add(new ListItem("-Select Course-", "0"));

                List<Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.VersionCode + " - " + course.Credits + " - " + course.Title;
                        ddlMainCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadPreReqCourseDDL()
        {
            try
            {
                ddlPreReqCourse.Items.Clear();
                ddlPreReqCourse.Items.Add(new ListItem("-Select Course-", "0"));

                List<Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.VersionCode + " - " + course.Credits + " - " + course.Title;
                        ddlPreReqCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadProgramDDL()
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("-Select Program-", "0"));
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataTextField = "NameAndCode";
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataBind();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            int programId = 0;
            string CourseNameNew = ddlCourse.SelectedValue;
            int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
            int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
            string vesionCode = null;

            if (!string.IsNullOrEmpty(txtCourseCode.Text))
            {
                vesionCode = Convert.ToString(txtCourseCode.Text);
            }

            List<PreRequisiteSetDTO> preRequisiteSetDTOList = PrerequisiteDetailV2Manager.GetAllPreRequisiteSetAndCourses(programId, courseId, versionId, vesionCode);

            GeneratePreRequisiteSetAndCourseList(preRequisiteSetDTOList);
        }

        protected void btnReLoadPreRequisite_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            LoadGvPreRequisiteSetList();
            ddlCourse.SelectedValue = Convert.ToString("0");
            txtCourseCode.Text = string.Empty;
        }   

        private void LoadGvPreRequisiteSetList()
        {

            List<PreRequisiteSetDTO> preRequisiteSetDTOList = PrerequisiteDetailV2Manager.GetAllPreRequisiteSetAndCourses(0, 0, 0, null);

            if (preRequisiteSetDTOList!= null)
            {
                List<PreRequisiteSetDTO> newPreRequisiteSetDTOList = new List<PreRequisiteSetDTO>();
                GeneratePreRequisiteSetAndCourseList(preRequisiteSetDTOList);
            }
            else 
            {
                BindGvPreRequisiteSet(null);
            }
            
        }

        private void GeneratePreRequisiteSetAndCourseList(List<PreRequisiteSetDTO> preRequisiteSetDTOList)
        {
            List<PreRequisiteSetDTO> preRequisiteMasterList = preRequisiteSetDTOList.Where(d=>d.PreRequisiteDetailId < 0).ToList();
            List<PreRequisiteSetDTO> newPreRequisiteSetDTOForGvList = new List<PreRequisiteSetDTO>();
            for (int i = 0; i < preRequisiteMasterList.Count; i++ ) 
            { 
                List<PreRequisiteSetDTO> newPreRequisiteDetailList = new List<PreRequisiteSetDTO>();
                newPreRequisiteDetailList = preRequisiteSetDTOList.Where(d => d.PreRequisiteMasterId == preRequisiteMasterList[i].PreRequisiteMasterId && d.PreRequisiteDetailId > 0).ToList();
                
                preRequisiteMasterList[i].Title = null;
                for (int j = 0; j < newPreRequisiteDetailList.Count; j++)
                {
                    if (newPreRequisiteDetailList[j].NodeName != null)
                    {
                        preRequisiteMasterList[i].Title += "(" + newPreRequisiteDetailList[j].NodeName + ") ";
                    }
                    preRequisiteMasterList[i].Title += newPreRequisiteDetailList[j].VersionCode;
                    if (j < (newPreRequisiteDetailList.Count - 1))
                    {
                        preRequisiteMasterList[i].Title += ", ";
                    }
                }
            }
            //return preRequisiteMasterList;

            if (preRequisiteMasterList != null)
            {
                BindGvPreRequisiteSet(preRequisiteMasterList);
            }
            else
            {
                BindGvPreRequisiteSet(null);
            }
        }

        private void BindGvPreRequisiteSet(List<PreRequisiteSetDTO> preRequisiteSetDTOList) 
        {
            GvPreRequisiteSet.DataSource = preRequisiteSetDTOList;
            GvPreRequisiteSet.DataBind();
        }

        protected void GvPreRequisiteSet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadGvPreRequisiteSetList();
            GvPreRequisiteSet.PageIndex = e.NewPageIndex;
            GvPreRequisiteSet.DataBind();
        }

        protected void lnkAddPreRequisiteSetCourse_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                int preRequisiteMasterId = int.Parse(btn.CommandArgument.ToString());
                if (preRequisiteMasterId > 0)
                {
                    PrerequisiteMasterV2 preRequisiteMasterV2Obj = PrerequisiteMasterV2Manager.GetById(preRequisiteMasterId);
                    if (preRequisiteMasterV2Obj != null)
                    {
                        lblPreRequisiteMasterId.Text = Convert.ToString(preRequisiteMasterV2Obj.PreRequisiteMasterId);
                        Course courseObj = new Course();
                        Node nodeObj = new Node();
                        string mainCourseName = null;

                        if (preRequisiteMasterV2Obj.NodeId > 0)
                        {
                            nodeObj = NodeManager.GetById(preRequisiteMasterV2Obj.NodeId);
                            if (nodeObj != null)
                            {
                                mainCourseName += nodeObj.Name + ' ';
                            }
                        }
                        if (preRequisiteMasterV2Obj.CourseId > 0)
                        {
                            courseObj = CourseManager.GetByCourseIdVersionId(preRequisiteMasterV2Obj.CourseId, preRequisiteMasterV2Obj.VersionId);
                            if (courseObj != null)
                            {
                                mainCourseName += courseObj.VersionCode;
                            }
                        }
                        ddlProgram.SelectedValue = Convert.ToString(preRequisiteMasterV2Obj.ProgramId);
                        lblMainCourse2.Text = mainCourseName;
                        lblPreReqMessage.Text = string.Empty;
                        ddlProgram.Enabled = false;
                        ddlMainCourse.Visible = false;
                        lblMainCourse2.Visible = true;
                        this.ModalPreReqCourseExtender.Show();
                        LoadGvPreRequisiteCourseList(preRequisiteMasterId);
                    }
                    else
                    {
                        lblMsg.Text = "Pre requisite set could not edit.";
                    }
                }
                else 
                {
                    lblMsg.Text = "Pre requisite set could not edit.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkRemovePreRequisiteSet_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                int preRequisiteMasterId = int.Parse(btn.CommandArgument.ToString());
                bool result = PrerequisiteMasterV2Manager.Delete(preRequisiteMasterId);
                if (result)
                {
                    lblMsg.Text = "Pre requisite set removed successfully.";
                    LoadGvPreRequisiteSetList();
                }
                else
                {
                    lblMsg.Text = "Pre requisite set could not removed successfully.";
                }
            }
            catch (Exception ex)
            {

            }
        }

                
        protected void btnAddPreRequisiteSet_Click(object sender, EventArgs e)
        {
            this.ModalPreReqCourseExtender.Show();
            lblMsg.Text = string.Empty;
            GvPreRequisiteCourseDetails.DataSource = null;
            GvPreRequisiteCourseDetails.DataBind();
            lblPreRequisiteMasterId.Text = string.Empty;
            ddlProgram.Enabled = true;
            ddlMainCourse.Visible = true;
            lblMainCourse2.Visible = false;
        }        

        private void LoadGvPreRequisiteCourseList(int preRequisiteMasterId)
        {
            try
            {
                List<PreRequisiteSetDTO> preRequisiteCourseDetailList = PrerequisiteDetailV2Manager.GetAllPreRequisiteDetailCourses(preRequisiteMasterId);
                if (preRequisiteCourseDetailList != null)
                {
                    GvPreRequisiteCourseDetails.DataSource = preRequisiteCourseDetailList;
                    GvPreRequisiteCourseDetails.DataBind();
                }
            }
            catch (Exception ex) 
            {
                lblPreReqMessage.Text = "Pre-Requisite course could not load properly.";
            }
        }



        protected void btnAddPreReqCourse_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModalPreReqCourseExtender.Show();
                if (!string.IsNullOrEmpty(lblPreRequisiteMasterId.Text.Trim()))
                {
                    int preRequisiteMasterId = Convert.ToInt16(lblPreRequisiteMasterId.Text.Trim());
                    if (preRequisiteMasterId > 0)
                    {
                        InsertPreRequisiteDetail(preRequisiteMasterId);
                    }
                    else 
                    {
                        lblPreReqMessage.Text = "Pre-Requisite course could not add.";
                    }
                }
                else if (string.IsNullOrEmpty(lblPreRequisiteMasterId.Text.Trim()))
                {
                    int preRequisiteMasterId = InsertPreRequisiteMaster();
                    if (preRequisiteMasterId > 0)
                    {
                        InsertPreRequisiteDetail(preRequisiteMasterId);
                    }
                    else
                    {
                        lblPreReqMessage.Text = "Pre-Requisite course could not add.";
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
            }
        }

        private int InsertPreRequisiteMaster()
        {
            int preRequisiteMasterId = 0;
            try
            {
                string CourseNameNew = ddlMainCourse.SelectedValue;
                int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                string coureFormalCode = Convert.ToString(ddlMainCourse.SelectedItem);
                string formalCode = Convert.ToString(coureFormalCode.Split('_').First());
                if (courseId > 0)
                {
                    PrerequisiteMasterV2 preRequisiteMasterV2Obj = new PrerequisiteMasterV2();
                    preRequisiteMasterV2Obj.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                    preRequisiteMasterV2Obj.NodeId = 0;
                    if (courseId > 0)
                    {
                        preRequisiteMasterV2Obj.CourseId = courseId;
                        preRequisiteMasterV2Obj.VersionId = versionId;
                    }
                    preRequisiteMasterV2Obj.CreatedBy = BaseCurrentUserObj.Id;
                    preRequisiteMasterV2Obj.CreatedDate = DateTime.Now;
                    preRequisiteMasterV2Obj.ModifiedBy = BaseCurrentUserObj.Id;
                    preRequisiteMasterV2Obj.ModifiedDate = DateTime.Now;

                    preRequisiteMasterId = PrerequisiteMasterV2Manager.Insert(preRequisiteMasterV2Obj);
                    if (preRequisiteMasterId > 0)
                    {
                        lblPreReqMessage.Text = "Pre-Requisite set created successfully.";
                        lblPreRequisiteMasterId.Text = Convert.ToString(preRequisiteMasterId);
                        ddlProgram.Enabled = false;
                        ddlMainCourse.Visible = false;
                        lblMainCourse2.Visible = true;
                        lblMainCourse2.Text = formalCode;
                        //LoadGvPreRequisiteSetList();
                    }
                    else
                    {
                        lblPreReqMessage.Text = "Pre-Requisite set could not created successfully.";
                    }
                }
                else
                {
                    lblPreReqMessage.Text = "Pre-Requisite set created without Pre-Requisite course."; 
                }
                return preRequisiteMasterId;
            }
            catch (Exception ex)
            {
                lblPreReqMessage.Text = ex.Message;
                return preRequisiteMasterId;
            }
        }

        private void InsertPreRequisiteDetail(int preRequisiteMasterId)
        {
            try
            {
                string CourseNameNew = ddlPreReqCourse.SelectedValue;
                int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
                if (courseId > 0)
                {
                    PrerequisiteDetailV2 prerequisiteDetailV2Obj = new PrerequisiteDetailV2();
                    prerequisiteDetailV2Obj.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                    prerequisiteDetailV2Obj.NodeId = 0;
                    prerequisiteDetailV2Obj.PreRequisiteMasterId = Convert.ToInt32(preRequisiteMasterId);
                    if (courseId > 0)
                    {
                        prerequisiteDetailV2Obj.CourseId = courseId;
                        prerequisiteDetailV2Obj.VersionId = versionId;
                    }
                    prerequisiteDetailV2Obj.CreatedBy = BaseCurrentUserObj.Id;
                    prerequisiteDetailV2Obj.CreatedDate = DateTime.Now;
                    prerequisiteDetailV2Obj.ModifiedBy = BaseCurrentUserObj.Id;
                    prerequisiteDetailV2Obj.ModifiedDate = DateTime.Now;

                    int result = PrerequisiteDetailV2Manager.Insert(prerequisiteDetailV2Obj);
                    if (result > 0)
                    {
                        lblPreReqMessage.Text = "Pre-Requisite course created successfully.";
                        LoadGvPreRequisiteCourseList(preRequisiteMasterId);
                    }
                    else
                    {
                        lblPreReqMessage.Text = "Pre-Requisite course could not created successfully.";
                    }
                }
                else
                {
                    lblPreReqMessage.Text = "Please select a Pre-Requisite course.";
                }
            }
            catch (Exception ex)
            {
                lblPreReqMessage.Text = ex.Message;
            }
        }

        protected void btnCancelPreReqCourse_Click(object sender, EventArgs e)
        {
            this.ModalPreReqCourseExtender.Hide();
            lblPreReqMessage.Text = null;
            if (!string.IsNullOrEmpty(lblPreRequisiteMasterId.Text))
            {
                int preRequisiteMasterId = Convert.ToInt32(lblPreRequisiteMasterId.Text);
                List<PreRequisiteSetDTO> preRequisiteSetDTOList = PrerequisiteDetailV2Manager.GetAllPreRequisiteSetAndCourses(0, 0, 0, null).Where(d => d.PreRequisiteMasterId == preRequisiteMasterId).ToList();
                GeneratePreRequisiteSetAndCourseList(preRequisiteSetDTOList);
            }
            else 
            {
                LoadGvPreRequisiteSetList();
            }

            lblPreRequisiteMasterId.Text = string.Empty;
            ddlProgram.SelectedValue = Convert.ToString("0");
            ddlProgram.Enabled = true;
            ddlMainCourse.SelectedValue = Convert.ToString("0");
            ddlMainCourse.Visible = true;
            ddlPreReqCourse.SelectedValue = Convert.ToString("0");
            ddlPreReqCourse.Visible = true;
            lblMainCourse2.Visible = false;
        }

        protected void lnkDeletePreRequisiteCourse_Click(object sender, EventArgs e) 
        {
            try
            {
                this.ModalPreReqCourseExtender.Show();
                LinkButton btn = (LinkButton)sender;
                int preRequisiteMasterId = Convert.ToInt32(lblPreRequisiteMasterId.Text);
                int preRequisiteDetailId = int.Parse(btn.CommandArgument.ToString());
                bool result = PrerequisiteDetailV2Manager.Delete(preRequisiteDetailId);
                if (result)
                {
                    lblPreReqMessage.Text = "Pre-Requisite course removed successfully.";
                    LoadGvPreRequisiteCourseList(preRequisiteMasterId);
                }
                else
                {
                    lblPreReqMessage.Text = "Pre-Requisite course could not removed successfully.";
                    LoadGvPreRequisiteCourseList(preRequisiteMasterId);
                }
            }
            catch (Exception ex) { }
        }

        

        //private List<NodeCoursesDTO> ProcessNodeCourse(int rootNodeId)
        //{
        //    List<NodeCoursesDTO> newNodeCourseList = Node_CourseManager.GetByNodeId(rootNodeId);
        //    if (newNodeCourseList!=null)
        //    {
        //        for (int j = 0; j < newNodeCourseList.Count; j++)
        //        {
        //            //nodeCourseList.Add(newNodeCourseList[j]);
        //        }
        //    }
        //    return newNodeCourseList;
        //}

        //private List<Node> ProcessNode(int rootNodeId)
        //{
        //    List<Node> newNodeList = NodeManager.GetNodeByParentNodeId(rootNodeId);
        //    if (newNodeList != null)
        //    {
        //        for (int j = 0; j < newNodeList.Count; j++)
        //        {
        //            //nodeList.Add(newNodeList[j]);
        //        }
        //    }
        //    return newNodeList;
        //}

        //protected void btnAddPreReqSet_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //this.ModalPopupPreReqSetExtender.Show();
        //        //if (Convert.ToInt32(ddlPreReqSetProgram.SelectedValue) > 0)
        //        //{
        //        //    string CourseNameNew = ddlPreReqSetCourse.SelectedValue;
        //        //    int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
        //        //    int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());
        //        //    if (courseId > 0 || Convert.ToInt32(ddlPreReqSetNode.SelectedValue)>0)
        //        //    {
        //        //        PrerequisiteMasterV2 preRequisiteMasterV2Obj = new PrerequisiteMasterV2();
        //        //        preRequisiteMasterV2Obj.ProgramId = Convert.ToInt32(ddlPreReqSetProgram.SelectedValue);
        //        //        preRequisiteMasterV2Obj.NodeId = Convert.ToInt32(ddlPreReqSetNode.SelectedValue);
        //        //        if (courseId > 0)
        //        //        {
        //        //            preRequisiteMasterV2Obj.CourseId = courseId;
        //        //            preRequisiteMasterV2Obj.VersionId = versionId;
        //        //        }
        //        //        preRequisiteMasterV2Obj.CreatedBy = BaseCurrentUserObj.Id;
        //        //        preRequisiteMasterV2Obj.CreatedDate = DateTime.Now;
        //        //        preRequisiteMasterV2Obj.ModifiedBy = BaseCurrentUserObj.Id;
        //        //        preRequisiteMasterV2Obj.ModifiedDate = DateTime.Now;

        //        //        int result = PrerequisiteMasterV2Manager.Insert(preRequisiteMasterV2Obj);
        //        //        if (result > 0)
        //        //        {
        //        //            lblPreReqSetMessage.Text = "Pre-Requisite set created successfully.";
        //        //            LoadGvPreRequisiteSetList();
        //        //        }
        //        //        else
        //        //        {
        //        //            lblPreReqSetMessage.Text = "Pre-Requisite set could not created successfully.";
        //        //        }
        //        //    }
        //        //    else { lblPreReqSetMessage.Text = "Please select a course/node."; }
        //        //}
        //        //else 
        //        //{
        //        //    lblPreReqSetMessage.Text = "Please select a program.";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void btnPreReqSetCancel_Click(object sender, EventArgs e)
        //{
        //    //this.ModalPopupPreReqSetExtender.Hide();
        //    //lblPreReqSetMessage.Text = null;
        //    //ClearAddGroupPanelAll();
        //}

    }
}