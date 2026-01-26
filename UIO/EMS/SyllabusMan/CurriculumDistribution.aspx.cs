using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.SyllabusMan
{
    public partial class CurriculumDistribution : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.CurriculumDistributionNew);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.CurriculumDistributionNew));

        private string[] _clsNameAndID = new string[2];

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                LoadProgram();
                LoadTreeCombo(0);
                LoadTreeCalender(0);
                InvisibleAllPanel();
                LoadAllDropDownWithZero();
                ClearAllFields();
            }
        }

        protected void LoadProgram()
        {
            try
            {
                ddlProgram.Items.Clear();
                ddlProgram.Items.Add(new ListItem("-Select Program-", "0"));

                List<Program> programList = ProgramManager.GetAll();

                ddlProgram.AppendDataBoundItems = true;

                if (programList != null)
                {
                    ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";
                    ddlProgram.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
            finally { }
        }

        private void LoadTreeCombo(int programId)
        {
            try
            {
                ddlTree.Items.Clear();
                ddlTree.Items.Add(new ListItem("-Select Program Tree-", "0"));
                List<TreeMaster> treeMasterList = TreeMasterManager.GetAll();

                treeMasterList = treeMasterList.Where(t => t.ProgramID == programId).ToList();

                ddlTree.AppendDataBoundItems = true;

                if (treeMasterList != null)
                {
                    ddlTree.DataSource = treeMasterList.OrderBy(d => d.TreeMasterID).ToList();
                    ddlTree.DataValueField = "TreeMasterID";
                    ddlTree.DataTextField = "Node_Name";
                    ddlTree.DataBind();
                }

            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
            finally { }
        }

        private void LoadTreeCalender(int treeId)
        {
            try
            {
                ddlCalenderDistribution.Items.Clear();
                ddlCalenderDistribution.Items.Add(new ListItem("-Select Distribution-", "0"));
                List<TreeCalendarMaster> treeCalenderList = TreeCalendarMasterManager.GetAllByTreeMasterID(treeId);

                ddlCalenderDistribution.AppendDataBoundItems = true;

                if (treeCalenderList != null)
                {
                    ddlCalenderDistribution.DataSource = treeCalenderList.OrderBy(d => d.TreeCalendarMasterID).ToList();
                    ddlCalenderDistribution.DataValueField = "TreeCalendarMasterID";
                    ddlCalenderDistribution.DataTextField = "Name";
                    ddlCalenderDistribution.DataBind();
                }

            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
            finally { }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            LoadTreeCombo(programId);
            LoadTreeCalender(0);
            ShowRoot(0);
            InvisibleAllPanel();
            LoadAllDropDownWithZero();
            ClearAllFields();
            btnAddNewDistName.Visible = false;
        }

        protected void ddlTree_SelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int treeId = Convert.ToInt32(ddlTree.SelectedValue);
            LoadTreeCalender(treeId);
            Session["TreeMasterId"] = null;
            Session["TreeMasterId"] = treeId;
            ShowRoot(0);
            InvisibleAllPanel();
            LoadAllDropDownWithZero();
            ClearAllFields();
            btnAddNewDistName.Visible = true;
        }

        protected void ddlCalenderDistribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadTree();
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadTree()
        {
            try
            {
                int calenderDistributionId = Convert.ToInt32(ddlCalenderDistribution.SelectedValue);
                ShowRoot(calenderDistributionId);

                int programId = Convert.ToInt32(ddlProgram.SelectedValue);
                int treeId = Convert.ToInt32(ddlTree.SelectedValue);
                lblProgramId.Text = Convert.ToString(programId);
                lblTreeId.Text = Convert.ToString(treeId);
                lblCalenderDistributionId.Text = Convert.ToString(calenderDistributionId);
                LoadAllFields();
                VisibleAllPanel();
                ClearAllFields();
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void InvisibleAllPanel()
        {
            try
            {
                pnlAddTreeItem.Visible = false;
                pnlEditTreeItem.Visible = false;
                pnlDeleteTreeItem.Visible = false;
                btnReloadTree.Visible = false;
                btnClearFields.Visible = false;
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void VisibleAllPanel()
        {
            try
            {
                pnlAddTreeItem.Visible = true;
                pnlEditTreeItem.Visible = true;
                pnlDeleteTreeItem.Visible = true;
                btnReloadTree.Visible = true;
                btnClearFields.Visible = true;
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearAllFields()
        {
            try
            {
                ClearAddCourseField();
                ClearAddNodeField();
                ClearEditCourseField();
                ClearEditNodeField();
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadAllDropDownWithZero()
        {
            try
            {
                #region Add Tree Item Dropdown Load
                ddlAddCourseTrimester.Items.Clear();
                ddlAddCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlAddCourseTrimester.AppendDataBoundItems = true;

                ddlAddCourse.Items.Clear();
                ddlAddCourse.Items.Add(new ListItem("-Select Course-", "0"));
                ddlAddCourse.AppendDataBoundItems = true;

                ddlAddCourseParentNode.Items.Clear();
                ddlAddCourseParentNode.Items.Add(new ListItem("-Select Node-", "0"));
                ddlAddCourseParentNode.AppendDataBoundItems = true;

                ddlAddNodeTrimester.Items.Clear();
                ddlAddNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlAddNodeTrimester.AppendDataBoundItems = true;

                ddlAddNodeNodes.Items.Clear();
                ddlAddNodeNodes.Items.Add(new ListItem("-Select Node-", "0"));
                ddlAddNodeNodes.AppendDataBoundItems = true;

                ddlAddNodeParentNode.Items.Clear();
                ddlAddNodeParentNode.Items.Add(new ListItem("-Select Node-", "0"));
                ddlAddNodeParentNode.AppendDataBoundItems = true;
                #endregion

                #region Edit Tree Item Load
                ddlEditCourseTrimester.Items.Clear();
                ddlEditCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlEditCourseTrimester.AppendDataBoundItems = true;

                ddlEditCourseOld.Items.Clear();
                ddlEditCourseOld.Items.Add(new ListItem("-Select Course-", "0"));
                ddlEditCourseOld.AppendDataBoundItems = true;

                ddlEditCourseNew.Items.Clear();
                ddlEditCourseNew.Items.Add(new ListItem("-Select Course-", "0"));
                ddlEditCourseNew.AppendDataBoundItems = true;

                ddlEditCourseParentNode.Items.Clear();
                ddlEditCourseParentNode.Items.Add(new ListItem("-Select Node-", "0"));
                ddlEditCourseParentNode.AppendDataBoundItems = true;

                ddlEditNodeTrimester.Items.Clear();
                ddlEditNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlEditNodeTrimester.AppendDataBoundItems = true;

                ddlEditNodeOld.Items.Clear();
                ddlEditNodeOld.Items.Add(new ListItem("-Select Node-", "0"));
                ddlEditNodeOld.AppendDataBoundItems = true;

                ddlEditNodeNew.Items.Clear();
                ddlEditNodeNew.Items.Add(new ListItem("-Select Node-", "0"));
                ddlEditNodeNew.AppendDataBoundItems = true;

                ddlEditNodeParentNode.Items.Clear();
                ddlEditNodeParentNode.Items.Add(new ListItem("-Select Node-", "0"));
                ddlEditNodeParentNode.AppendDataBoundItems = true;
                #endregion

                #region Delete Tree Item Load
                ddlDeleteCourseTrimester.Items.Clear();
                ddlDeleteCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlDeleteCourseTrimester.AppendDataBoundItems = true;

                ddlDeleteCourse.Items.Clear();
                ddlDeleteCourse.Items.Add(new ListItem("-Select Course-", "0"));
                ddlDeleteCourse.AppendDataBoundItems = true;

                ddlDeleteNodeTrimester.Items.Clear();
                ddlDeleteNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));
                ddlDeleteNodeTrimester.AppendDataBoundItems = true;

                ddlDeleteNode.Items.Clear();
                ddlDeleteNode.Items.Add(new ListItem("-Select Node-", "0"));
                ddlDeleteNode.AppendDataBoundItems = true;
                #endregion
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoaddAllDropDownAfterSaveEditDelete()
        {
            ddlAddCourseTrimester.SelectedValue = Convert.ToString("0");
            ddlAddCourse.SelectedValue = Convert.ToString("0");
            ddlAddCourseParentNode.SelectedValue = Convert.ToString("0");
            ddlAddNodeTrimester.SelectedValue = Convert.ToString("0");
            ddlAddNodeNodes.SelectedValue = Convert.ToString("0");
            ddlAddNodeParentNode.SelectedValue = Convert.ToString("0");

            ddlEditCourseTrimester.SelectedValue = Convert.ToString("0");
            ddlEditCourseOld.SelectedValue = Convert.ToString("0");
            ddlEditCourseNew.SelectedValue = Convert.ToString("0");
            ddlEditCourseParentNode.SelectedValue = Convert.ToString("0");
            ddlEditNodeTrimester.SelectedValue = Convert.ToString("0");
            ddlEditNodeOld.SelectedValue = Convert.ToString("0");
            ddlEditNodeNew.SelectedValue = Convert.ToString("0");
            ddlEditNodeParentNode.SelectedValue = Convert.ToString("0");

            ddlDeleteCourseTrimester.SelectedValue = Convert.ToString("0");
            ddlDeleteCourse.SelectedValue = Convert.ToString("0");
            ddlDeleteNodeTrimester.SelectedValue = Convert.ToString("0");
            ddlDeleteNode.SelectedValue = Convert.ToString("0");

            ddlEditCourseOld.Items.Clear();
            ddlEditCourseOld.Items.Add(new ListItem("-Select Course-", "0"));
            ddlEditCourseOld.AppendDataBoundItems = true;

            ddlEditNodeOld.Items.Clear();
            ddlEditNodeOld.Items.Add(new ListItem("-Select Node-", "0"));
            ddlEditNodeOld.AppendDataBoundItems = true;

            ddlDeleteCourse.Items.Clear();
            ddlDeleteCourse.Items.Add(new ListItem("-Select Course-", "0"));
            ddlDeleteCourse.AppendDataBoundItems = true;

            ddlDeleteNode.Items.Clear();
            ddlDeleteNode.Items.Add(new ListItem("-Select Node-", "0"));
            ddlDeleteNode.AppendDataBoundItems = true;
        }

        #region Tree View Bind
        private void ShowRoot(int calenderDistributionId)
        {
            if (calenderDistributionId >= 0)
            {
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(Int32.Parse(ddlCalenderDistribution.SelectedValue));
                LoadRoot(treeCalMaster);
            }
        }

        private void LoadRoot(TreeCalendarMaster treeCalMaster)
        {
            tvwCalendar.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            if (treeCalMaster != null)
            {
                treeNode.Text = treeCalMaster.CalenderUnitName;
                treeNode.Value = "TreeCalMas," + treeCalMaster.TreeCalendarMasterID.ToString();
                treeNode.ExpandAll();
                tvwCalendar.Nodes.Add(treeNode);
            }
        }

        protected void tvwCalendar_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                LoadChildrens(tvwCalendar.SelectedNode);
            }
            catch (Exception Ex)
            {

            }
        }

        private void LoadChildrens(TreeNode treeNode)
        {
            _clsNameAndID = treeNode.Value.Split(',');
            Session["TreeCalDistributionId"] = null;
            Session["TreeCalDistributionId"] = Convert.ToInt32(lblCalenderDistributionId.Text);
            Session["TreeMasterId"] = null;
            Session["TreeMasterId"] = Convert.ToInt32(lblTreeId.Text);

            try
            {
                if (_clsNameAndID[0] == "TreeCalMas")
                {
                    #region If Parent is  Tree Calendar Master
                    if (Session["TreeMasterId"] != null && Session["TreeCalDistributionId"] != null)
                    {
                        TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(Int32.Parse(_clsNameAndID[1])); ;
                        List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);


                        treeNode.ChildNodes.Clear();
                        if (treeCalDetails != null)
                        {
                            LoadNode(treeNode, treeCalDetails);
                        }
                    }
                    #endregion
                }
                else if (_clsNameAndID[0] == "TreeCalDet")
                {
                    #region If Parent is Tree Calendar Detail
                    if (Session["TreeMasterId"] != null && Session["TreeCalDistributionId"] != null)
                    {
                        TreeCalendarDetail treeCalDetail = TreeCalendarDetailManager.GetById(Int32.Parse(_clsNameAndID[1])); ;
                        List<CalCourseProgNode> calendarDistributions = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalDetail.TreeCalendarDetailID);

                        treeNode.ChildNodes.Clear();
                        if (calendarDistributions != null)
                        {
                            LoadNode(treeNode, calendarDistributions);
                        }
                    }
                    #endregion
                }
                else if (_clsNameAndID[0] == "NOD" || _clsNameAndID[0] == "CALDISNOD")
                {
                    #region If Parent is a Node or Calendar Distribution
                    if (Session["TreeMasterId"] != null)
                    {
                        TreeMaster treeMaster = TreeMasterManager.GetById(Convert.ToInt32(Session["TreeMasterId"]));

                        Node node = null;
                        if (_clsNameAndID[0] == "NOD")
                        {
                            #region If Parent is a Node
                            node = NodeManager.GetById(Int32.Parse(_clsNameAndID[1]));
                            #endregion
                        }
                        else if (_clsNameAndID[0] == "CALDISNOD")
                        {
                            #region If Parent is a Calendar Distribution
                            string[] calDisIDAndNodeID = new string[2];
                            calDisIDAndNodeID = _clsNameAndID[1].Split('#');
                            node = NodeManager.GetById(Int32.Parse(calDisIDAndNodeID[1]));
                            #endregion
                        }

                        if (!node.IsLastLevel)
                        {
                            if (node.IsVirtual != null)
                            {
                                if (!Convert.ToBoolean(node.IsVirtual))
                                {
                                    #region ChildNodes
                                    List<TreeDetail> treeDetails = TreeDetailManager.GetByTreeMasterIdParentNodeId(treeMaster.TreeMasterID, node.NodeID);
                                    if (treeDetails != null)
                                    {
                                        List<Node> nodes = new List<Node>();
                                        foreach (TreeDetail treeDetail in treeDetails)
                                        {
                                            Node childNode = NodeManager.GetById(treeDetail.ChildNodeID);
                                            nodes.Add(childNode);
                                        }
                                        treeNode.ChildNodes.Clear();
                                        LoadNode(treeNode, nodes);
                                    }
                                    else //if (treeNode.Parent != null)
                                    {
                                        treeNode.ChildNodes.Clear();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region VNodeSetMasters
                                    List<VNodeSetMaster> vNodeSetMasterList = VNodeSetMasterManager.GetByNodeId(node.NodeID);
                                    if (vNodeSetMasterList != null)
                                    {
                                        treeNode.ChildNodes.Clear();
                                        LoadNode(treeNode, vNodeSetMasterList);
                                    }
                                    else //if (treeNode.Parent != null)
                                    {
                                        treeNode.ChildNodes.Clear();
                                    }
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            #region Node_Courses
                            if (node.NodeID > 0)
                            {
                                List<NodeCoursesDTO> nodeCourseList = Node_CourseManager.GetByNodeId(node.NodeID);
                                if (nodeCourseList != null)
                                {
                                    List<Course> courses = new List<Course>();
                                    foreach (NodeCoursesDTO node_Course in nodeCourseList)
                                    {
                                        Course childCourse = CourseManager.GetByCourseIdVersionId(node_Course.CourseID, node_Course.VersionID);
                                        courses.Add(childCourse);
                                    }
                                    treeNode.ChildNodes.Clear();
                                    LoadNode(treeNode, courses);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else if (_clsNameAndID[0] == "SETMAS")
                {
                    #region If Parent is a SetMaster
                    if (Session["TreeMasterId"] != null)
                    {
                        TreeMaster treeMaster = TreeMasterManager.GetById(Convert.ToInt32(Session["TreeMasterId"]));

                        string[] setNoAndNodeID = new string[2];
                        setNoAndNodeID = _clsNameAndID[1].Split('#');

                        Node node = NodeManager.GetById(Int32.Parse(setNoAndNodeID[1]));
                        if (node != null)
                        {
                            List<VNodeSet> vNodeSetList = VNodeSetManager.GetbyNodeId(node.NodeID);
                            List<VNodeSetMaster> vNodeSetMasterList = VNodeSetMasterManager.GetByNodeId(node.NodeID);
                            if (vNodeSetList != null)
                            {
                                VNodeSetMaster vNodeSetMaster = null;

                                foreach (VNodeSetMaster vNodeSetMasterInner in vNodeSetMasterList)
                                {
                                    if (vNodeSetMasterInner.SetNo == Int32.Parse(setNoAndNodeID[0]))
                                    {
                                        vNodeSetMaster = vNodeSetMasterInner;
                                    }
                                }
                                List<VNodeSet> vNodeSetList2 = VNodeSetManager.GetbyVNodeSetMasterId(vNodeSetMaster.VNodeSetMasterID);
                                if (vNodeSetMaster == null)
                                {
                                    treeNode.ChildNodes.Clear();
                                    tvwCalendar.Nodes.Remove(treeNode);
                                }
                                else if (vNodeSetList2 != null)
                                {
                                    treeNode.ChildNodes.Clear();
                                    LoadNode(treeNode, vNodeSetList2);
                                }
                                else
                                {
                                    treeNode.ChildNodes.Clear();
                                }
                            }
                            else
                            {
                                treeNode.ChildNodes.Clear();
                            }
                        }

                    }
                    #endregion
                }
                else if (_clsNameAndID[0] == "VNODSET")
                {
                    #region If Parent is a VnodeSet
                    if (Session["TreeMasterId"] != null)
                    {
                        TreeMaster treeMaster = TreeMasterManager.GetById(Convert.ToInt32(Session["TreeMasterId"]));
                        if (Convert.ToInt32(_clsNameAndID[1]) != 0)
                        {
                            VNodeSet vNodeSet = VNodeSetManager.GetById(Int32.Parse(_clsNameAndID[1]));

                            if (vNodeSet.OperandNodeID != 0)
                            {
                                Node node = NodeManager.GetById(vNodeSet.OperandNodeID);
                                FillChildrenONode(treeNode, node, treeMaster);
                            }
                        }
                    }
                    #endregion
                }
                else { }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void LoadNode(TreeNode parentNode, List<TreeCalendarDetail> treeCalDetails)
        {
            foreach (TreeCalendarDetail treeCalDetail in treeCalDetails)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = treeCalDetail.CalenderUnitDistributionName;
                treeNode.Value = "TreeCalDet," + treeCalDetail.TreeCalendarDetailID.ToString();
                treeNode.ExpandAll();
                if (parentNode == null)
                {
                    tvwCalendar.Nodes.Add(treeNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(treeNode);
                }
            }
        }

        private void LoadNode(TreeNode parentNode, List<CalCourseProgNode> calendarDistributions)
        {
            try
            {
                foreach (CalCourseProgNode calendarDistribution in calendarDistributions)
                {
                    TreeNode treeNode = new TreeNode();
                    if (calendarDistribution.CalCourseProgNodeCourse != null)
                    {
                        if ((calendarDistribution.CourseID == 0) && calendarDistribution.NodeID > 0)
                        {
                            treeNode.Text = calendarDistribution.NodeLinkName + "-" + calendarDistribution.CalCourseProgNodeNode.Name + " Pr: (" + calendarDistribution.Priority + ")";
                            treeNode.Value = "CALDISNOD," + calendarDistribution.CalCorProgNodeID.ToString() + "#" + calendarDistribution.NodeID.ToString();
                            treeNode.ExpandAll();
                        }
                        else if (calendarDistribution.CourseID > 0 && (calendarDistribution.NodeID == 0 || calendarDistribution.NodeID == null))
                        {
                            treeNode.Text = calendarDistribution.CalCourseProgNodeCourse.FormalCode + " - " + calendarDistribution.CalCourseProgNodeCourse.VersionCode + " - " + calendarDistribution.CalCourseProgNodeCourse.Title + " ( " + calendarDistribution.CalCourseProgNodeCourse.Credits + " )" + " Pr: (" + calendarDistribution.Priority + ")";
                            treeNode.Value = "CALDISCRS," + calendarDistribution.CalCorProgNodeID.ToString() + "#" + calendarDistribution.CourseID.ToString() + "#" + calendarDistribution.VersionID.ToString();
                            treeNode.ExpandAll();
                        }
                        else { }

                        if (parentNode == null)
                        {
                            tvwCalendar.Nodes.Add(treeNode);
                        }
                        else
                        {
                            parentNode.ChildNodes.Add(treeNode);
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadNode(TreeNode parentNode, List<Course> courses)
        {
            foreach (Course course in courses)
            {
                TreeNode node = new TreeNode();
                node.Text = course.FormalCode + " - " + course.VersionCode + " - " + course.Title + " ( " + course.Credits + " ) ";
                node.Value = "CRS," + course.CourseID.ToString() + "#" + course.VersionID.ToString();
                node.ExpandAll();
                if (parentNode == null)
                {
                    tvwCalendar.Nodes.Add(node);
                }
                else
                {
                    parentNode.ChildNodes.Add(node);
                }
            }
        }

        private void LoadNode(TreeNode parentNode, List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = node.Name;
                treeNode.Value = "NOD," + node.NodeID.ToString();
                treeNode.ExpandAll();
                if (parentNode == null)
                {
                    tvwCalendar.Nodes.Add(treeNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(treeNode);
                }
            }
        }

        private void LoadNode(TreeNode parentNode, List<VNodeSetMaster> vNodeSetMasterList)
        {
            foreach (VNodeSetMaster vNodeMaster in vNodeSetMasterList)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = "Set " + Convert.ToString(vNodeMaster.SetNo);
                treeNode.Value = "SETMAS," + vNodeMaster.SetNo.ToString() + "#" + vNodeMaster.NodeID.ToString();
                treeNode.ExpandAll();
                if (parentNode == null)
                {
                    tvwCalendar.Nodes.Add(treeNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(treeNode);
                }
            }
        }

        private void LoadNode(TreeNode parentNode, List<VNodeSet> vNodeSetList)
        {
            foreach (VNodeSet vNodeSet in vNodeSetList)
            {
                TreeNode treeNode = new TreeNode();
                Course courseObj = new Course();
                Operator operatorObj = new Operator();
                Node nodeObj = new Node();
                if (vNodeSet.OperatorID != null && vNodeSet.OperatorID > 0)
                {
                    operatorObj = OperatorManager.GetById(vNodeSet.OperatorID);
                }
                if ((vNodeSet.OperandCourseID != null && vNodeSet.OperandCourseID > 0) && (vNodeSet.OperandVersionID != null && vNodeSet.OperandVersionID > 0))
                {
                    courseObj = CourseManager.GetByCourseIdVersionId(vNodeSet.OperandCourseID, vNodeSet.OperandVersionID);
                }
                if (vNodeSet.OperandNodeID != null && vNodeSet.OperandNodeID > 0)
                {
                    nodeObj = NodeManager.GetById(vNodeSet.OperandNodeID);
                }


                if (!vNodeSet.IsStudntSpec && vNodeSet.OperandNodeID == 0)
                {
                    if (courseObj != null && operatorObj != null)
                    {
                        treeNode.Text = courseObj.Title + " - " + operatorObj.Name;
                        treeNode.Value = "VNODSET," + vNodeSet.VNodeSetID.ToString();
                    }
                }
                else if (!vNodeSet.IsStudntSpec && vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0)
                {
                    if (operatorObj != null && nodeObj != null)
                    {
                        treeNode.Text = nodeObj.Name + " - " + operatorObj.Name;
                        treeNode.Value = "VNODSET," + vNodeSet.VNodeSetID.ToString();
                    }
                }
                else
                {
                    if (operatorObj != null)
                    {
                        treeNode.Text = "Student specific major - " + operatorObj.Name;
                        treeNode.Value = "VNODSET," + "0";
                    }
                }

                treeNode.ExpandAll();
                if (parentNode == null)
                {
                    tvwCalendar.Nodes.Add(treeNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(treeNode);
                }
            }
        }

        private void FillChildrenONode(TreeNode treeNode, Node node, TreeMaster treeMaster)
        {
            if (!node.IsLastLevel)
            {
                if (node.IsVirtual != null)
                {
                    if (!Convert.ToBoolean(node.IsVirtual))
                    {
                        #region ChildNodes
                        List<TreeDetail> treeDetails = TreeDetailManager.GetByTreeMasterIdParentNodeId(treeMaster.TreeMasterID, node.NodeID);
                        if (treeDetails != null)
                        {
                            List<Node> nodes = new List<Node>();
                            foreach (TreeDetail treeDetail in treeDetails)
                            {
                                Node childNode = NodeManager.GetById(treeDetail.ChildNodeID);
                                nodes.Add(childNode);
                            }
                            treeNode.ChildNodes.Clear();
                            LoadNode(treeNode, nodes);
                        }
                        else //if (treeNode.Parent != null)
                        {
                            treeNode.ChildNodes.Clear();
                        }
                        #endregion
                    }
                    else
                    {
                        #region VNodeSetMasters
                        List<VNodeSetMaster> vNodeSetMasterList = VNodeSetMasterManager.GetByNodeId(node.NodeID);
                        if (vNodeSetMasterList != null)
                        {
                            treeNode.ChildNodes.Clear();
                            LoadNode(treeNode, vNodeSetMasterList);
                        }
                        else //if (treeNode.Parent != null)
                        {
                            treeNode.ChildNodes.Clear();
                        }
                        #endregion
                    }
                }
            }
            else
            {
                #region Node_Courses
                if (node.NodeID > 0)
                {
                    List<NodeCoursesDTO> nodeCourseList = Node_CourseManager.GetByNodeId(node.NodeID);
                    if (nodeCourseList != null)
                    {
                        List<Course> courses = new List<Course>();
                        foreach (NodeCoursesDTO node_Course in nodeCourseList)
                        {
                            Course childCourse = CourseManager.GetByCourseIdVersionId(node_Course.CourseID, node_Course.VersionID);
                            courses.Add(childCourse);
                        }
                        treeNode.ChildNodes.Clear();
                        LoadNode(treeNode, courses);
                    }
                }

                #endregion
            }
        }
        #endregion

        #region Add Tree Item
        protected void rbAddTreeItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbAddTreeItem.SelectedValue == "1")
                {
                    pnlAddCourse.Visible = true;
                    pnlAddNode.Visible = false;
                }
                else if (rbAddTreeItem.SelectedValue == "2")
                {
                    pnlAddCourse.Visible = false;
                    pnlAddNode.Visible = true;
                }
                else { }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadAllFields()
        {
            ddlAddCourseTrimesterLoad();
            ddlAddCourseCourseLoad();
            ddlAddCourseParentNodeLoad();

            ddlAddNodeTrimesterLoad();
            ddlAddNodeLoad();
            ddlAddNodeParentNodeLoad();

            ddlEditCourseTrimesterLoad();
            ddlEditCourseCourseLoad();
            ddlEditCourseParentNodeLoad();

            ddlEditNodeTrimesterLoad();
            ddlEditNodeNewLoad();
            ddlEditNodeParentNodeLoad();

            ddlDeleteCourseTrimesterLoad();

            ddlDeleteNodeTrimesterLoad();
        }

        private void ddlAddCourseTrimesterLoad()
        {
            try
            {
                int treeCalenderMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeCalenderMasterId);

                ddlAddCourseTrimester.Items.Clear();
                ddlAddCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlAddCourseTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlAddCourseTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlAddCourseTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlAddCourseTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlAddCourseTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlAddCourseCourseLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                TreeMaster treeMasterObj = TreeMasterManager.GetById(treeMasterId);


                ddlAddCourse.Items.Clear();
                ddlAddCourse.Items.Add(new ListItem("-Select Course-", "0"));

                List<CourseListByNodeDTO> nodeCourseList = CourseListByNodeDTOManager.GetAllByRootNodeId(treeMasterObj.RootNodeID);

                ddlAddCourseTrimester.AppendDataBoundItems = true;

                if (nodeCourseList != null)
                {
                    foreach (CourseListByNodeDTO course in nodeCourseList)
                    {
                        string valueField = course.CourseId + "_" + course.VersionId + "_" + course.NodeCourseId;
                        string textField = course.CourseName;
                        ddlAddCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlAddCourseParentNodeLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlAddCourseParentNode.Items.Clear();
                ddlAddCourseParentNode.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlAddCourseParentNode.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlAddCourseParentNode.DataSource = nodeList.ToList();
                    ddlAddCourseParentNode.DataValueField = "NodeID";
                    ddlAddCourseParentNode.DataTextField = "Name";
                    ddlAddCourseParentNode.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddCourseSave_Click(object sender, EventArgs e)
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlAddCourseTrimester.SelectedValue);
                int programId = Convert.ToInt32(lblProgramId.Text);

                string CourseNameNew = ddlAddCourse.SelectedValue;
                string[] str = ddlAddCourse.SelectedValue.Split('_');
                //int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                //int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                int courseId = Convert.ToInt32(str[0]);
                int versionId = Convert.ToInt32(str[1]);
                int nodeCourseId = Convert.ToInt32(str[2]);
                if (!string.IsNullOrEmpty(txtAddCoursePriority.Text.Trim()))
                {
                    Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                    CalCourseProgNode calCourseProgNodeObj = new CalCourseProgNode();

                    calCourseProgNodeObj.TreeCalendarDetailID = treeCalenderDetailId;
                    calCourseProgNodeObj.OfferedByProgramID = programId;
                    calCourseProgNodeObj.CourseID = courseId;
                    calCourseProgNodeObj.VersionID = versionId;
                    if (courseObj != null)
                    {
                        calCourseProgNodeObj.Credits = courseObj.Credits;
                    }
                    calCourseProgNodeObj.Node_CourseID = nodeCourseId;
                    calCourseProgNodeObj.Priority = Convert.ToInt32(txtAddCoursePriority.Text);
                    calCourseProgNodeObj.TopNodeId = Convert.ToInt32(ddlAddCourseParentNode.SelectedValue);
                    calCourseProgNodeObj.IsMajorRelated = false;
                    calCourseProgNodeObj.CreatedBy = BaseCurrentUserObj.Id;
                    calCourseProgNodeObj.CreatedDate = DateTime.Now;
                    calCourseProgNodeObj.ModifiedBy = BaseCurrentUserObj.Id;
                    calCourseProgNodeObj.ModifiedDate = DateTime.Now;
                    bool checkDuplicateCourse = CalCourseProgNodeManager.CheckCourseCalCourseProgNode(treeMasterId, calenderDistributionId, courseId, versionId, nodeCourseId, calCourseProgNodeObj.Priority);
                    if (checkDuplicateCourse)
                    {
                        int result = CalCourseProgNodeManager.Insert(calCourseProgNodeObj);
                        if (result > 0)
                        {
                            ModalPopupExtender1.Show();
                            lblMsg.Text = "Course added successfully.";
                            ShowRoot(calenderDistributionId);
                            #region Log Insert
                            LogGeneralManager.Insert(
                                    DateTime.Now,
                                    "",
                                    "",
                                    BaseCurrentUserObj.LogInID,
                                    "",
                                    "",
                                    "Course add in curriculum distribution",
                                    BaseCurrentUserObj.LogInID + " attempted to add course in tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlAddCourseTrimester.SelectedItem + ", course name " + ddlAddCourse.SelectedItem,
                                    "normal",
                                    _pageId,
                                    _pageName,
                                    _pageUrl,
                                    "");

                            #endregion
                            ClearAddCourseField();
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            lblMsg.Text = "Course could not added successfully.";
                        }
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Course/Priority already exist.";
                    }
                }
                else
                {
                    ModalPopupExtender1.Show();
                    lblMsg.Text = "Priority can not be null.";
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearAddCourseField()
        {
            txtAddCourseCrComplete.Text = string.Empty;
            txtAddCoursePriority.Text = string.Empty;
            LoaddAllDropDownAfterSaveEditDelete();
        }


        private void ddlAddNodeTrimesterLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeMasterId);

                ddlAddNodeTrimester.Items.Clear();
                ddlAddNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlAddNodeTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlAddNodeTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlAddNodeTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlAddNodeTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlAddNodeTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlAddNodeLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlAddNodeNodes.Items.Clear();
                ddlAddNodeNodes.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlAddNodeNodes.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlAddNodeNodes.DataSource = nodeList.ToList();
                    ddlAddNodeNodes.DataValueField = "NodeID";
                    ddlAddNodeNodes.DataTextField = "Name";
                    ddlAddNodeNodes.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlAddNodeParentNodeLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlAddNodeParentNode.Items.Clear();
                ddlAddNodeParentNode.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlAddNodeParentNode.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlAddNodeParentNode.DataSource = nodeList.ToList();
                    ddlAddNodeParentNode.DataValueField = "NodeID";
                    ddlAddNodeParentNode.DataTextField = "Name";
                    ddlAddNodeParentNode.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddNodeSave_Click(object sender, EventArgs e)
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlAddNodeTrimester.SelectedValue);
                int programId = Convert.ToInt32(lblProgramId.Text);

                int nodeId = Convert.ToInt32(ddlAddNodeNodes.SelectedValue);

                if (!string.IsNullOrEmpty(txtAddNodeName.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(txtAddNodePriority.Text.Trim()))
                    {
                        CalCourseProgNode calCourseProgNodeObj = new CalCourseProgNode();

                        calCourseProgNodeObj.TreeCalendarDetailID = treeCalenderDetailId;
                        calCourseProgNodeObj.OfferedByProgramID = programId;
                        calCourseProgNodeObj.CourseID = 0;
                        calCourseProgNodeObj.VersionID = 0;
                        calCourseProgNodeObj.Node_CourseID = 0;
                        calCourseProgNodeObj.NodeID = nodeId;
                        calCourseProgNodeObj.NodeLinkName = Convert.ToString(txtAddNodeName.Text.Trim());
                        calCourseProgNodeObj.Priority = Convert.ToInt32(txtAddNodePriority.Text);
                        calCourseProgNodeObj.TopNodeId = Convert.ToInt32(ddlAddNodeParentNode.SelectedValue);
                        if (!string.IsNullOrEmpty(txtAddNodeCrComplete.Text))
                        {
                            calCourseProgNodeObj.Credits = Convert.ToInt32(txtAddNodeCrComplete.Text);
                        }

                        if (chkAddNodeIsMajor.Checked)
                        {
                            calCourseProgNodeObj.IsMajorRelated = true;
                        }
                        else
                        {
                            calCourseProgNodeObj.IsMajorRelated = false;
                        }
                        calCourseProgNodeObj.CreatedBy = BaseCurrentUserObj.Id;
                        calCourseProgNodeObj.CreatedDate = DateTime.Now;
                        calCourseProgNodeObj.ModifiedBy = BaseCurrentUserObj.Id;
                        calCourseProgNodeObj.ModifiedDate = DateTime.Now;

                        bool checkDuplicateNode = CalCourseProgNodeManager.CheckNodeCalCourseProgNode(treeMasterId, calenderDistributionId, nodeId, calCourseProgNodeObj.Priority);
                        if (checkDuplicateNode)
                        {
                            int result = CalCourseProgNodeManager.Insert(calCourseProgNodeObj);
                            if (result > 0)
                            {
                                ModalPopupExtender1.Show();
                                lblMsg.Text = "Node added successfully.";
                                ShowRoot(calenderDistributionId);
                                #region Log Insert
                                LogGeneralManager.Insert(
                                        DateTime.Now,
                                        "",
                                        "",
                                        BaseCurrentUserObj.LogInID,
                                        "",
                                        "",
                                        "Node add in curriculum distribution",
                                        BaseCurrentUserObj.LogInID + " attempted to add node in tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlAddNodeTrimester.SelectedItem + ", node name " + ddlAddNodeNodes.SelectedItem,
                                        "normal",
                                        _pageId,
                                        _pageName,
                                        _pageUrl,
                                        "");

                                #endregion
                                ClearAddNodeField();
                            }
                            else
                            {
                                ModalPopupExtender1.Show();
                                lblMsg.Text = "Node could not added successfully.";
                            }
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            lblMsg.Text = "Node/Priority already exist.";
                        }
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Priority can not be null.";
                    }
                }
                else
                {
                    ModalPopupExtender1.Show();
                    lblMsg.Text = "Node name can not be null.";
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearAddNodeField()
        {
            txtAddNodeName.Text = string.Empty;
            txtAddNodeCrComplete.Text = string.Empty;
            txtAddNodePriority.Text = string.Empty;
            chkAddNodeIsMajor.Checked = false;
            LoaddAllDropDownAfterSaveEditDelete();
        }
        #endregion Add Tree Item

        #region Edit Tree Item
        protected void rbEditTreeItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEditTreeItem.SelectedValue == "1")
                {
                    pnlEditCourse.Visible = true;
                    pnlEditNode.Visible = false;
                }
                else if (rbEditTreeItem.SelectedValue == "2")
                {
                    pnlEditCourse.Visible = false;
                    pnlEditNode.Visible = true;
                }
                else { }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlEditCourseTrimesterLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeMasterId);

                ddlEditCourseTrimester.Items.Clear();
                ddlEditCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlEditCourseTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlEditCourseTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlEditCourseTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlEditCourseTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlEditCourseTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlEditCourseCourseLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                TreeMaster treeMasterObj = TreeMasterManager.GetById(treeMasterId);


                ddlEditCourseNew.Items.Clear();
                ddlEditCourseNew.Items.Add(new ListItem("-Select Course-", "0"));

                List<CourseListByNodeDTO> nodeCourseList = CourseListByNodeDTOManager.GetAllByRootNodeId(treeMasterObj.RootNodeID);

                ddlEditCourseNew.AppendDataBoundItems = true;

                if (nodeCourseList != null)
                {
                    foreach (CourseListByNodeDTO course in nodeCourseList)
                    {
                        string valueField = course.CourseId + "_" + course.VersionId + "_" + course.NodeCourseId;
                        string textField = course.CourseName;
                        ddlEditCourseNew.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlEditCourseParentNodeLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlEditCourseParentNode.Items.Clear();
                ddlEditCourseParentNode.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlEditCourseParentNode.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlEditCourseParentNode.DataSource = nodeList.ToList();
                    ddlEditCourseParentNode.DataValueField = "NodeID";
                    ddlEditCourseParentNode.DataTextField = "Name";
                    ddlEditCourseParentNode.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlEditCourseTrimester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int treeCalenderDetailId = Convert.ToInt32(ddlEditCourseTrimester.SelectedValue);
                List<CalCourseProgNode> calCourseProgNodeList = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId);
                ddlEditCourseOld.Items.Clear();
                ddlEditCourseOld.Items.Add(new ListItem("-Select Course-", "0"));

                ddlEditCourseOld.AppendDataBoundItems = true;

                if (calCourseProgNodeList != null)
                {
                    foreach (CalCourseProgNode calCourseProgNode in calCourseProgNodeList)
                    {
                        if (calCourseProgNode.CourseID > 0 && calCourseProgNode.VersionID > 0)
                        {
                            Course courseObj = CourseManager.GetByCourseIdVersionId(calCourseProgNode.CourseID, calCourseProgNode.VersionID);
                            if (courseObj != null)
                            {
                                string valueField = courseObj.CourseID + "_" + courseObj.VersionID + "_" + calCourseProgNode.Node_CourseID;
                                string textField = courseObj.FormalCode + " - " + courseObj.VersionCode + " - " + courseObj.Title + " (Cr: " + courseObj.Credits + ")";
                                ddlEditCourseOld.Items.Add(new ListItem(textField, valueField));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlEditCourseOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CourseNameNew = ddlEditCourseOld.SelectedValue;
                string[] str = ddlEditCourseOld.SelectedValue.Split('_');

                int courseId = Convert.ToInt32(str[0]);
                int versionId = Convert.ToInt32(str[1]);
                int nodeCourseId = Convert.ToInt32(str[2]);

                int treeCalenderDetailId = Convert.ToInt32(ddlEditCourseTrimester.SelectedValue);

                CalCourseProgNode calCourseProgNode = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.CourseID == courseId && d.VersionID == versionId && d.Node_CourseID == nodeCourseId).FirstOrDefault();
                LoadEditCoursePanel(calCourseProgNode);
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadEditCoursePanel(CalCourseProgNode calCourseProgNode)
        {
            try
            {
                txtEditCourseCrComplete.Text = Convert.ToString(calCourseProgNode.Credits);
                txtEditCoursePriority.Text = Convert.ToString(calCourseProgNode.Priority);
                if (calCourseProgNode.TopNodeId != null && calCourseProgNode.TopNodeId > 0)
                {
                    ddlEditCourseParentNode.SelectedValue = Convert.ToString(calCourseProgNode.TopNodeId);
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnEditCourseSave_Click(object sender, EventArgs e)
        {
            try
            {
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlEditCourseTrimester.SelectedValue);
                int programId = Convert.ToInt32(lblProgramId.Text);

                string[] strOld = ddlEditCourseOld.SelectedValue.Split('_');

                int courseIdOld = Convert.ToInt32(strOld[0]);
                int versionIdOld = Convert.ToInt32(strOld[1]);
                int nodeCourseIdOld = Convert.ToInt32(strOld[2]);

                string[] strNew = ddlEditCourseNew.SelectedValue.Split('_');

                int courseIdNew = Convert.ToInt32(strNew[0]);
                int versionIdNew = Convert.ToInt32(strNew[1]);
                int nodeCourseIdNew = Convert.ToInt32(strNew[2]);

                CalCourseProgNode calCourseProgNodeObj = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.CourseID == courseIdOld && d.VersionID == versionIdOld && d.Node_CourseID == nodeCourseIdOld).FirstOrDefault();
                if (calCourseProgNodeObj != null)
                {
                    if (courseIdNew > 0 && versionIdNew > 0)
                    {
                        if (!string.IsNullOrEmpty(txtEditCoursePriority.Text.Trim()))
                        {
                            Course courseObj = CourseManager.GetByCourseIdVersionId(courseIdNew, versionIdNew);
                            calCourseProgNodeObj.CourseID = courseIdNew;
                            calCourseProgNodeObj.VersionID = versionIdNew;
                            calCourseProgNodeObj.Node_CourseID = nodeCourseIdNew;
                            if (courseObj != null)
                            {
                                calCourseProgNodeObj.Credits = courseObj.Credits;
                            }
                            calCourseProgNodeObj.Priority = Convert.ToInt32(txtEditCoursePriority.Text);
                            calCourseProgNodeObj.ModifiedBy = BaseCurrentUserObj.Id;
                            calCourseProgNodeObj.ModifiedDate = DateTime.Now;
                            bool result = CalCourseProgNodeManager.Update(calCourseProgNodeObj);
                            if (result)
                            {
                                ModalPopupExtender1.Show();
                                lblMsg.Text = "Course edited successfully.";
                                ShowRoot(calenderDistributionId);
                                #region Log Insert
                                LogGeneralManager.Insert(
                                        DateTime.Now,
                                        "",
                                        "",
                                        BaseCurrentUserObj.LogInID,
                                        "",
                                        "",
                                        "Course edit in curriculum distribution",
                                        BaseCurrentUserObj.LogInID + "  attempted to edit course in tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlEditCourseTrimester.SelectedItem + ", course name " + ddlEditCourseNew.SelectedItem,
                                        "normal",
                                        _pageId,
                                        _pageName,
                                        _pageUrl,
                                        "");

                                #endregion
                                ClearEditCourseField();
                            }
                            else
                            {
                                ModalPopupExtender1.Show();
                                lblMsg.Text = "Course could not edited successfully.";
                            }
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            lblMsg.Text = "Priority can not be null.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearEditCourseField()
        {
            txtEditCourseCrComplete.Text = string.Empty;
            txtEditCoursePriority.Text = string.Empty;
            ddlEditCourseTrimester.SelectedValue = Convert.ToString("0");
            LoaddAllDropDownAfterSaveEditDelete();
        }

        private void ddlEditNodeTrimesterLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeMasterId);

                ddlEditNodeTrimester.Items.Clear();
                ddlEditNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlEditNodeTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlEditNodeTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlEditNodeTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlEditNodeTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlEditNodeTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlEditNodeNewLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlEditNodeNew.Items.Clear();
                ddlEditNodeNew.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlEditNodeNew.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlEditNodeNew.DataSource = nodeList.ToList();
                    ddlEditNodeNew.DataValueField = "NodeID";
                    ddlEditNodeNew.DataTextField = "Name";
                    ddlEditNodeNew.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlEditNodeParentNodeLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                ddlEditNodeParentNode.Items.Clear();
                ddlEditNodeParentNode.Items.Add(new ListItem("-Select Node-", "0"));

                List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                ddlEditNodeParentNode.AppendDataBoundItems = true;

                if (nodeList != null)
                {
                    ddlEditNodeParentNode.DataSource = nodeList.ToList();
                    ddlEditNodeParentNode.DataValueField = "NodeID";
                    ddlEditNodeParentNode.DataTextField = "Name";
                    ddlEditNodeParentNode.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlEditNodeTrimester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int treeCalenderDetailId = Convert.ToInt32(ddlEditNodeTrimester.SelectedValue);
                List<CalCourseProgNode> calCourseProgNodeList = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId);
                ddlEditNodeOld.Items.Clear();
                ddlEditNodeOld.Items.Add(new ListItem("-Select Node-", "0"));

                ddlEditNodeOld.AppendDataBoundItems = true;

                if (calCourseProgNodeList != null)
                {
                    foreach (CalCourseProgNode calCourseProgNode in calCourseProgNodeList)
                    {
                        if (calCourseProgNode.NodeID > 0)
                        {
                            Node nodeObj = NodeManager.GetById(calCourseProgNode.NodeID);
                            if (nodeObj != null)
                            {
                                string valueField = Convert.ToString(nodeObj.NodeID);
                                string textField = Convert.ToString(calCourseProgNode.NodeLinkName) + Convert.ToString(nodeObj.Name);
                                ddlEditNodeOld.Items.Add(new ListItem(textField, valueField));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlEditNodeOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int treeCalenderDetailId = Convert.ToInt32(ddlEditNodeTrimester.SelectedValue);
                int nodeId = Convert.ToInt32(ddlEditNodeOld.SelectedValue);

                CalCourseProgNode calCourseProgNode = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.NodeID == nodeId).FirstOrDefault();
                LoadEditNodePanel(calCourseProgNode);
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadEditNodePanel(CalCourseProgNode calCourseProgNode)
        {
            try
            {
                txtEditNodeCrComplete.Text = Convert.ToString(calCourseProgNode.Credits);
                txtEditNodePriority.Text = Convert.ToString(calCourseProgNode.Priority);
                txtEditNodeName.Text = Convert.ToString(calCourseProgNode.NodeLinkName);
                chkEditNodeIsMajor.Checked = calCourseProgNode.IsMajorRelated;

                if (calCourseProgNode.TopNodeId != null && calCourseProgNode.TopNodeId > 0)
                {
                    ddlAddNodeParentNode.SelectedValue = Convert.ToString(calCourseProgNode.TopNodeId);
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnEditNodeSave_Click(object sender, EventArgs e)
        {
            try
            {
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlEditNodeTrimester.SelectedValue);
                int oldNodeId = Convert.ToInt32(ddlEditNodeOld.SelectedValue);
                int newNodeId = Convert.ToInt32(ddlEditNodeNew.SelectedValue);

                CalCourseProgNode calCourseProgNodeObj = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.NodeID == oldNodeId).FirstOrDefault();
                if (calCourseProgNodeObj != null)
                {
                    if (newNodeId > 0)
                    {
                        if (!string.IsNullOrEmpty(txtEditNodeName.Text.Trim()))
                        {
                            if (!string.IsNullOrEmpty(txtEditNodePriority.Text.Trim()))
                            {
                                calCourseProgNodeObj.NodeLinkName = Convert.ToString(txtEditNodeName.Text);
                                calCourseProgNodeObj.NodeID = Convert.ToInt32(newNodeId);
                                if (!string.IsNullOrEmpty(txtEditNodeCrComplete.Text))
                                {
                                    calCourseProgNodeObj.Credits = Convert.ToDecimal(txtEditNodeCrComplete.Text);
                                }

                                if (chkEditNodeIsMajor.Checked)
                                {
                                    calCourseProgNodeObj.IsMajorRelated = true;
                                }
                                else { calCourseProgNodeObj.IsMajorRelated = false; }
                                calCourseProgNodeObj.Priority = Convert.ToInt32(txtEditNodePriority.Text);
                                calCourseProgNodeObj.ModifiedBy = BaseCurrentUserObj.Id;
                                calCourseProgNodeObj.ModifiedDate = DateTime.Now;
                                bool result = CalCourseProgNodeManager.Update(calCourseProgNodeObj);
                                if (result)
                                {
                                    ModalPopupExtender1.Show();
                                    lblMsg.Text = "Node edited successfully.";
                                    ShowRoot(calenderDistributionId);

                                    #region Log Insert
                                    LogGeneralManager.Insert(
                                            DateTime.Now,
                                            "",
                                            "",
                                            BaseCurrentUserObj.LogInID,
                                            "",
                                            "",
                                            "Edit node to curriculum distribution",
                                            BaseCurrentUserObj.LogInID + " attempted to edit node in tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlEditNodeTrimester.SelectedItem + ", node name " + ddlEditNodeOld.SelectedItem,
                                            "normal",
                                            _pageId,
                                            _pageName,
                                            _pageUrl,
                                            "");

                                    #endregion
                                    ClearEditNodeField();
                                }
                                else
                                {
                                    ModalPopupExtender1.Show();
                                    lblMsg.Text = "Node could not edited successfully.";
                                }
                            }
                            else
                            {
                                ModalPopupExtender1.Show();
                                lblMsg.Text = "Priority can not be null.";
                            }
                        }
                        else
                        {
                            ModalPopupExtender1.Show();
                            lblMsg.Text = "Node name can not be null.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearEditNodeField()
        {
            txtEditNodeName.Text = string.Empty;
            txtEditNodeCrComplete.Text = string.Empty;
            txtEditNodePriority.Text = string.Empty;
            chkEditNodeIsMajor.Checked = false;
            ddlEditNodeTrimester.SelectedValue = Convert.ToString("0");
            LoaddAllDropDownAfterSaveEditDelete();
        }
        #endregion

        #region Delete Tree Item
        protected void rbDeleteTreeItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbDeleteTreeItem.SelectedValue == "1")
                {
                    pnlDeleteCourse.Visible = true;
                    pnlDeleteNode.Visible = false;
                }
                else if (rbDeleteTreeItem.SelectedValue == "2")
                {
                    pnlDeleteCourse.Visible = false;
                    pnlDeleteNode.Visible = true;
                }
                else { }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        private void ddlDeleteCourseTrimesterLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeMasterId);

                ddlDeleteCourseTrimester.Items.Clear();
                ddlDeleteCourseTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlDeleteCourseTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlDeleteCourseTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlDeleteCourseTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlDeleteCourseTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlDeleteCourseTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeleteCourseTrimester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int treeCalenderDetailId = Convert.ToInt32(ddlDeleteCourseTrimester.SelectedValue);
                List<CalCourseProgNode> calCourseProgNodeList = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId);
                ddlDeleteCourse.Items.Clear();
                ddlDeleteCourse.Items.Add(new ListItem("-Select Course-", "0"));

                ddlDeleteCourse.AppendDataBoundItems = true;

                if (calCourseProgNodeList != null)
                {
                    foreach (CalCourseProgNode calCourseProgNode in calCourseProgNodeList)
                    {
                        if (calCourseProgNode.CourseID > 0 && calCourseProgNode.VersionID > 0)
                        {
                            Course courseObj = CourseManager.GetByCourseIdVersionId(calCourseProgNode.CourseID, calCourseProgNode.VersionID);
                            if (courseObj != null)
                            {
                                string valueField = courseObj.CourseID + "_" + courseObj.VersionID + "_" + calCourseProgNode.Node_CourseID;
                                string textField = courseObj.FormalCode + " - " + courseObj.VersionCode + " - " + courseObj.Title + " (Cr: " + courseObj.Credits + ")";
                                ddlDeleteCourse.Items.Add(new ListItem(textField, valueField));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            try
            {
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlDeleteCourseTrimester.SelectedValue);

                string[] str = ddlDeleteCourse.SelectedValue.Split('_');

                int courseId = Convert.ToInt32(str[0]);
                int versionId = Convert.ToInt32(str[1]);
                int nodeCourseId = Convert.ToInt32(str[2]);

                CalCourseProgNode calCourseProgNodeObj = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.CourseID == courseId && d.VersionID == versionId && d.Node_CourseID == nodeCourseId).FirstOrDefault();
                if (calCourseProgNodeObj != null)
                {
                    bool result = CalCourseProgNodeManager.Delete(calCourseProgNodeObj.CalCorProgNodeID);
                    if (result)
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Course deleted successfully.";
                        ShowRoot(calenderDistributionId);

                        #region Log Insert
                        LogGeneralManager.Insert(
                                DateTime.Now,
                                "",
                                "",
                                BaseCurrentUserObj.LogInID,
                                "",
                                "",
                                "Course delete from curriculum distribution",
                                BaseCurrentUserObj.LogInID + " attempted to delete course from tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlDeleteCourseTrimester.SelectedItem + ", course name " + ddlDeleteCourse.SelectedItem,
                                "normal",
                                _pageId,
                                _pageName,
                                _pageUrl,
                                "");

                        #endregion
                        LoaddAllDropDownAfterSaveEditDelete();
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Course could not deleted successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }


        private void ddlDeleteNodeTrimesterLoad()
        {
            try
            {
                int treeMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeMasterId);

                ddlDeleteNodeTrimester.Items.Clear();
                ddlDeleteNodeTrimester.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlDeleteNodeTrimester.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlDeleteNodeTrimester.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlDeleteNodeTrimester.DataValueField = "TreeCalendarDetailID";
                    ddlDeleteNodeTrimester.DataTextField = "CalenderUnitDistributionName";
                    ddlDeleteNodeTrimester.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeleteNodeTrimester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int treeCalenderDetailId = Convert.ToInt32(ddlDeleteNodeTrimester.SelectedValue);
                List<CalCourseProgNode> calCourseProgNodeList = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId);
                ddlDeleteNode.Items.Clear();
                ddlDeleteNode.Items.Add(new ListItem("-Select Node-", "0"));

                ddlDeleteNode.AppendDataBoundItems = true;

                if (calCourseProgNodeList != null)
                {
                    foreach (CalCourseProgNode calCourseProgNode in calCourseProgNodeList)
                    {
                        if (calCourseProgNode.NodeID > 0)
                        {
                            Node nodeObj = NodeManager.GetById(calCourseProgNode.NodeID);
                            if (nodeObj != null)
                            {
                                string valueField = Convert.ToString(nodeObj.NodeID);
                                string textField = Convert.ToString(calCourseProgNode.NodeLinkName) + Convert.ToString(nodeObj.Name);
                                ddlDeleteNode.Items.Add(new ListItem(textField, valueField));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDeleteNode_Click(object sender, EventArgs e)
        {
            try
            {
                int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                int treeCalenderDetailId = Convert.ToInt32(ddlDeleteNodeTrimester.SelectedValue);

                int nodeId = Convert.ToInt32(ddlDeleteNode.SelectedValue);

                CalCourseProgNode calCourseProgNodeObj = CalCourseProgNodeManager.GetByTreeCalenderDetailId(treeCalenderDetailId).Where(d => d.NodeID == nodeId).FirstOrDefault();
                if (calCourseProgNodeObj != null)
                {
                    bool result = CalCourseProgNodeManager.Delete(calCourseProgNodeObj.CalCorProgNodeID);
                    if (result)
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Node deleted successfully.";
                        ShowRoot(calenderDistributionId);

                        #region Log Insert
                        LogGeneralManager.Insert(
                                DateTime.Now,
                                "",
                                "",
                                BaseCurrentUserObj.LogInID,
                                "",
                                "",
                                "Node delete from curriculum distribution",
                                BaseCurrentUserObj.LogInID + " attempted to delete node from tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlDeleteNodeTrimester.SelectedItem + ", node name " + ddlDeleteNode.SelectedItem,
                                "normal",
                                _pageId,
                                _pageName,
                                _pageUrl,
                                "");

                        #endregion
                        LoaddAllDropDownAfterSaveEditDelete();
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        lblMsg.Text = "Node could not deleted successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Add Tree Distribution
        protected void btnAddNewDistName_Click(object sender, EventArgs e)
        {
            LoadCalendarType();
            lblMsgAddTreeDistributionPopUp.Text = string.Empty;
            txtDistribution.Text = string.Empty;
            int treeMasterId = Convert.ToInt32(ddlTree.SelectedValue);
            if (treeMasterId > 0)
            {
                this.ModalPopupExtenderAddTreeDistribution.Show();
            }
        }

        private void LoadCalendarType()
        {
            this.ModalPopupExtenderAddTreeDistribution.Show();
            try
            {
                ddlCalendarType.Items.Clear();
                ddlCalendarType.Items.Add(new ListItem("-Select Semester Type-", "0"));

                List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

                ddlCalendarType.AppendDataBoundItems = true;

                if (calenderUnitMasterList != null)
                {
                    ddlCalendarType.DataSource = calenderUnitMasterList;
                    ddlCalendarType.DataValueField = "CalenderUnitMasterID";
                    ddlCalendarType.DataTextField = "Name";
                    ddlCalendarType.DataBind();
                }
            }
            catch (Exception ex)
            {
                ModalPopupExtender1.Show();
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSaveDistName_Clicked(object sender, EventArgs e)
        {
            this.ModalPopupExtenderAddTreeDistribution.Show();
            try
            {
                string calenderDistributionName = Convert.ToString(txtDistribution.Text);
                int treeMasterId = Convert.ToInt32(ddlTree.SelectedValue);
                int calendarMasterId = Convert.ToInt32(ddlCalendarType.SelectedValue);

                TreeCalendarMaster treeCalendarMasterObj = new TreeCalendarMaster();
                treeCalendarMasterObj = TreeCalendarMasterManager.GetByTreeCalenderNameTreeMasterId(calenderDistributionName, treeMasterId);
                if (treeCalendarMasterObj == null && treeMasterId > 0 && calendarMasterId > 0)
                {
                    TreeCalendarMaster treeCalendarMasterInsertObj = new TreeCalendarMaster();

                    treeCalendarMasterInsertObj.TreeMasterID = treeMasterId;
                    treeCalendarMasterInsertObj.Name = txtDistribution.Text;
                    treeCalendarMasterInsertObj.CalendarMasterID = calendarMasterId;
                    treeCalendarMasterInsertObj.CreatedBy = BaseCurrentUserObj.Id;
                    treeCalendarMasterInsertObj.CreatedDate = DateTime.Now;
                    treeCalendarMasterInsertObj.ModifiedBy = BaseCurrentUserObj.Id;
                    treeCalendarMasterInsertObj.ModifiedDate = DateTime.Now;
                    int treeCalenderMasterId = TreeCalendarMasterManager.Insert(treeCalendarMasterInsertObj);
                    if (treeCalenderMasterId > 0)
                    {
                        InsertTreeCalenderDetail(treeMasterId, treeCalenderMasterId, calendarMasterId);
                    }
                    lblMsgAddTreeDistributionPopUp.Text = "Inserted";
                }
                else
                {
                    treeCalendarMasterObj.TreeMasterID = treeMasterId;
                    treeCalendarMasterObj.Name = txtDistribution.Text;
                    treeCalendarMasterObj.ModifiedDate = DateTime.Now;
                    bool isUpdated = TreeCalendarMasterManager.Update(treeCalendarMasterObj);
                    lblMsgAddTreeDistributionPopUp.Text = "Updated";
                }
                ddlTree_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { }
        }

        private void InsertTreeCalenderDetail(int treeMasterId, int treeCalenderMasterId, int calendarMasterId)
        {
            List<CalenderUnitDistribution> calenderUnitDistributionList = CalenderUnitDistributionManager.GetAll().Where(d => d.CalenderUnitMasterID == calendarMasterId).ToList();

            if (calenderUnitDistributionList != null)
            {
                for (int i = 0; i < calenderUnitDistributionList.Count; i++)
                {
                    TreeCalendarDetail treeCalendarDetailObj = new TreeCalendarDetail();
                    treeCalendarDetailObj.TreeCalendarMasterID = treeCalenderMasterId;
                    treeCalendarDetailObj.TreeMasterID = treeMasterId;
                    treeCalendarDetailObj.CalendarMasterID = calendarMasterId;
                    treeCalendarDetailObj.CalendarDetailID = calenderUnitDistributionList[i].CalenderUnitDistributionID;
                    treeCalendarDetailObj.CreatedBy = BaseCurrentUserObj.Id;
                    treeCalendarDetailObj.CreatedDate = DateTime.Now;
                    treeCalendarDetailObj.ModifiedBy = BaseCurrentUserObj.Id;
                    treeCalendarDetailObj.ModifiedDate = DateTime.Now;

                    TreeCalendarDetailManager.Insert(treeCalendarDetailObj);
                }
            }

        }

        protected void btnCancelAddTreeDistributionPopUp_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtenderAddTreeDistribution.Hide();
        }
        #endregion

        protected void btnReloadTree_Click(object sender, EventArgs e)
        {
            LoadTree();
        }

        protected void btnClearFields_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            LoaddAllDropDownAfterSaveEditDelete();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                modalPopupCourseList.Show();
                LoadSemester();
                LoadCourseList();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadCourseList()
        {
            try
            {
                gvCourseList.DataSource = null;
                gvCourseList.DataBind();

                int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                TreeMaster treeMasterObj = TreeMasterManager.GetById(treeMasterId);

                List<CourseListByNodeDTO> nodeCourseList = CourseListByNodeDTOManager.GetAllByRootNodeId(treeMasterObj.RootNodeID);

                if (nodeCourseList != null && nodeCourseList.Any())
                {
                    gvCourseList.DataSource = nodeCourseList;
                    gvCourseList.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void LoadSemester()
        {
            try
            {
                int treeCalenderMasterId = Convert.ToInt32(lblCalenderDistributionId.Text);
                TreeCalendarMaster treeCalMaster = TreeCalendarMasterManager.GetById(treeCalenderMasterId);

                ddlSemesterNo.Items.Clear();
                ddlSemesterNo.Items.Add(new ListItem("-Select Trimester-", "0"));

                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetailManager.GetByTreeCalenderMasterId(treeCalMaster.TreeCalendarMasterID);

                ddlSemesterNo.AppendDataBoundItems = true;

                if (treeCalDetails != null)
                {
                    ddlSemesterNo.DataSource = treeCalDetails.OrderBy(d => d.CalendarDetailID).ToList();
                    ddlSemesterNo.DataValueField = "TreeCalendarDetailID";
                    ddlSemesterNo.DataTextField = "CalenderUnitDistributionName";
                    ddlSemesterNo.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            modalPopupCourseList.Hide();
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            try
            {
                int CheckCount = CountCheckedItem();

                if (CheckCount == 0)
                {
                    modalPopupCourseList.Show();
                    showAlert("Please check minimum one course");
                    return;
                }
                if (CheckCount == -1)
                {
                    modalPopupCourseList.Show();
                    showAlert("Please give priority for all checked course");
                    return;
                }

                int Insert = 0, Duplicate = 0;

                for (int i = 0; i < gvCourseList.Rows.Count; i++)
                {
                    try
                    {
                        GridViewRow row = gvCourseList.Rows[i];
                        CheckBox Checkd = (CheckBox)row.FindControl("ChkChecked");
                        TextBox txtPr = (TextBox)row.FindControl("txtPriority");
                        HiddenField hdnCourseId = (HiddenField)row.FindControl("hdnCourseID");
                        HiddenField hdnVersionId = (HiddenField)row.FindControl("hdnVersionID");
                        HiddenField hdnNodeCourseId = (HiddenField)row.FindControl("hdnNodeCourseId");
                        DropDownList ParentNode = (DropDownList)row.FindControl("ddlParentNode");

                        int treeMasterId = Convert.ToInt32(lblTreeId.Text);
                        int calenderDistributionId = Convert.ToInt32(lblCalenderDistributionId.Text);
                        int treeCalenderDetailId = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                        int programId = Convert.ToInt32(lblProgramId.Text);


                        int courseId = Convert.ToInt32(hdnCourseId.Value);
                        int versionId = Convert.ToInt32(hdnVersionId.Value);
                        int nodeCourseId = Convert.ToInt32(hdnNodeCourseId.Value);

                        int ParentNodeId = Convert.ToInt32(ParentNode.SelectedValue);

                        if (!string.IsNullOrEmpty(txtPr.Text.Trim()))
                        {
                            Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                            CalCourseProgNode calCourseProgNodeObj = new CalCourseProgNode();

                            calCourseProgNodeObj.TreeCalendarDetailID = treeCalenderDetailId;
                            calCourseProgNodeObj.OfferedByProgramID = programId;
                            calCourseProgNodeObj.CourseID = courseId;
                            calCourseProgNodeObj.VersionID = versionId;
                            if (courseObj != null)
                            {
                                calCourseProgNodeObj.Credits = courseObj.Credits;
                            }
                            calCourseProgNodeObj.Node_CourseID = nodeCourseId;
                            calCourseProgNodeObj.Priority = Convert.ToInt32(txtPr.Text);
                            calCourseProgNodeObj.TopNodeId = ParentNodeId;
                            calCourseProgNodeObj.IsMajorRelated = false;
                            calCourseProgNodeObj.CreatedBy = BaseCurrentUserObj.Id;
                            calCourseProgNodeObj.CreatedDate = DateTime.Now;
                            calCourseProgNodeObj.ModifiedBy = BaseCurrentUserObj.Id;
                            calCourseProgNodeObj.ModifiedDate = DateTime.Now;
                            bool checkDuplicateCourse = CalCourseProgNodeManager.CheckCourseCalCourseProgNode(treeMasterId, calenderDistributionId, courseId, versionId, nodeCourseId, calCourseProgNodeObj.Priority);
                            if (checkDuplicateCourse)
                            {
                                int result = CalCourseProgNodeManager.Insert(calCourseProgNodeObj);
                                if (result > 0)
                                {
                                    Insert++;

                                    try
                                    {
                                        #region Log Insert
                                        LogGeneralManager.Insert(
                                                DateTime.Now,
                                                "",
                                                "",
                                                BaseCurrentUserObj.LogInID,
                                                "",
                                                "",
                                                "Course add in curriculum distribution",
                                                BaseCurrentUserObj.LogInID + " attempted to add course in tree " + ddlTree.SelectedItem + ", semester/trimester " + ddlAddCourseTrimester.SelectedItem + ", course name " + ddlAddCourse.SelectedItem,
                                                "normal",
                                                _pageId,
                                                _pageName,
                                                _pageUrl,
                                                "");

                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                }
                            }
                            else
                            {
                                Duplicate++;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }

                string msg = "";

                if (Insert > 0)
                    msg = "Course Added Successfully";
                if (Duplicate > 0 && Insert > 0)
                    msg = msg + " and some course has duplicate course / Priority";
                else if (Duplicate > 0)
                    msg = "Some course has duplicate course / Priority";

                showAlert(msg);
                modalPopupCourseList.Hide();

            }
            catch (Exception ex)
            {
            }
        }

        private int CountCheckedItem()
        {
            int Counter = 0, txtPrio = 0;
            for (int i = 0; i < gvCourseList.Rows.Count; i++)
            {
                try
                {
                    GridViewRow row = gvCourseList.Rows[i];
                    CheckBox Checkd = (CheckBox)row.FindControl("ChkChecked");
                    TextBox txtPr = (TextBox)row.FindControl("txtPriority");
                    if (Checkd.Checked == true)
                    {
                        Counter = Counter + 1;
                    }
                    if (!string.IsNullOrEmpty(txtPr.Text))
                    {
                        txtPrio = txtPrio + 1;
                    }

                    if (Counter > 0 && Counter != txtPrio)
                        Counter = -1;

                }
                catch (Exception ex)
                {
                }

            }
            return Counter;
        }

        protected void gvCourseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                int treeMasterId = Convert.ToInt32(lblTreeId.Text);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var ddl = e.Row.FindControl("ddlParentNode") as DropDownList;
                    ddl.Items.Clear();
                    ddl.AppendDataBoundItems = true;
                    List<Node> nodeList = NodeManager.GetNodeByTreeMasterId(treeMasterId);

                    if (nodeList != null && nodeList.Any())
                    {

                        ddl.DataTextField = "Name";
                        ddl.DataValueField = "NodeID";
                        ddl.DataSource = nodeList;
                        ddl.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }




        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }


    }
}