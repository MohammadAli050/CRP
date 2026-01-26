using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTabControl;
using BussinessObject;
using Common;
using LogicLayer.BusinessLogic;

public partial class SyllabusMan_TreeMaster : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.CurriculumBuilder);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.CurriculumBuilder));
    #region Session
    private const string SESSIONPROGRAMS = "PROGRAMS";
    private const string SESSIONTREEMASTER = "TREEMASTER";
    private const string SESSIONTREEMASTERS = "TREEMASTERS";
    private const string SESSIONCOURSES = "COURSES";
    private const string SESSIONNODES = "NODES";
    private const string SESSIONOPERATORS = "OPERATORS";
    private const string SESSIONCURRENTNODE = "CURRENTNODE";
    private const string SESSIONPARENTNODE = "PARENTNODE";
    private const string SESSIONNODE_COURSE = "NODE_COURSE";
    private const string SESSIONVNODESETMAS = "VNODESETMAS";
    private const string SESSIONISADDINGSET = "ISADDINGSET";
    private const string SESSIONISADDINGCOURSE = "ISADDINGCOURSE";
    private const string SESSIONISADDINGROOT = "ISADDINGROOT";
    private const string SESSIONISADDINGNODE = "ISADDINGNODE";

    private const string SESSIONPREREQCOURSE = "PREREQCOURSE";
    private const string SESSIONPREREQNODE = "PREREQNODE";
    private const string SESSIONPREREQMASTERS = "PREREQMASTERS";
    private const string SESSIONPREREQMASTER = "PREREQMASTER";
    private const string SESSIONPREREQARRLIST = "ARRAYLIST";
    #endregion

    #region Variables
    private string[] _clsNameAndID = new string[2];
    private List<BussinessObject.Program> _programs = new List<BussinessObject.Program>();
    private List<Course> _courses = new List<Course>();
    private List<NodeCourse> _node_Courses = new List<NodeCourse>();
    private List<Node> _nodes = new List<Node>();
    private List<NodeCourse> _nodeCouses = new List<NodeCourse>();
    private List<Operator> _operators = new List<Operator>();
    private List<PreReqDetail> _preReqDetails_Course = new List<PreReqDetail>();
    private List<PreReqDetail> _preReqDetails_Node = new List<PreReqDetail>();
    private List<PreRequisiteMaster> _preReqMasters = new List<PreRequisiteMaster>();
    private PreRequisiteMaster _preReqMaster = new PreRequisiteMaster();

    private bool _isAddingRoot = false;
    private bool _isAddingNode = false;
    private bool _isAddingSet = false;
    private bool _isAddingCourse = false;

    private string strPreReqCourseOccurance = "Amount of taken courses maintaining OR relation";
    private string strPreReqNodeOccurance = "Amount of taken nodes maintaining OR relation";

    #endregion

    #region Methods
    private void FillProgCombo()
    {
        ddlPrograms.Items.Clear();

        _programs = BussinessObject.Program.GetPrograms();

        if (_programs != null)
        {
            foreach (BussinessObject.Program program in _programs)
            {
                ListItem item = new ListItem();
                item.Value = program.Id.ToString();
                item.Text = program.ShortName;
                ddlPrograms.Items.Add(item);
            }

            if (Session["Programs"] != null)
            {
                Session.Remove("Programs");
            }
            Session.Add("Programs", _programs);

            ddlPrograms.SelectedIndex = 0;
        }
    }
    private void FillTreeCombo()
    {
        ddlTree.Items.Clear();

        if (ddlPrograms.SelectedIndex >= 0)
        {
            if (Session["Programs"] != null)
            {
                List<Program> programs = (List<Program>)Session["Programs"];
                var program = from prog in programs where prog.Id == Int32.Parse(ddlPrograms.SelectedValue) select prog;

                List<TreeMaster> treeMasters = TreeMaster.GetByProgram(program.ElementAt(0).Id);

                if (treeMasters != null)
                {
                    ddlTree.Enabled = true;

                    ListItem item = new ListItem();
                    item.Value = 0.ToString();
                    item.Text = "<New Root>";
                    ddlTree.Items.Add(item);

                    foreach (TreeMaster treeMaster in treeMasters)
                    {
                        item = new ListItem();
                        item.Value = treeMaster.Id.ToString();
                        item.Text = treeMaster.RootNode.Name;
                        ddlTree.Items.Add(item);
                    }

                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    if (Session["TreeMasters"] != null)
                    {
                        Session.Remove("TreeMasters");
                    }
                    Session.Add("TreeMasters", treeMasters);

                    ddlTree.SelectedIndex = 0;
                }
                else
                {
                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    ddlTree.Enabled = false;
                    tvwMaster.Nodes.Clear();

                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "No Tree found for the selected program";
                }
            }
        }
    }
    private void FillCorsCombo()
    {
        try
        {

            //ddlCourses.Items.Clear();
            ddlSetCrs.Items.Clear();

            _node_Courses = BussinessObject.NodeCourse.GetNodeCourses();
            //_courses = BussinessObject.Course.GetCourses();

            if (_node_Courses != null)
            {
                foreach (NodeCourse nodeCourse in _node_Courses)
                {
                    ListEditItem item = new ListEditItem();
                    //item.Value = course.Id.ToString() + "," + course.VersionID.ToString();
                    //item.Text = course.FormalCode+"-"+course.VersionCode+"-"+course.Title;
                    //ddlCourses.Items.Add(item);

                    //item = new ListEditItem();

                    try
                    {
                        item.Value = nodeCourse.ChildCourseID.ToString() + "," + nodeCourse.ChildVersionID.ToString();
                        item.Text = nodeCourse.ChildCourse.FormalCode + "-" + nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
                        ddlSetCrs.Items.Add(item);

                        if (_courses == null)
                        {
                            _courses = new List<Course>();
                        }
                        _courses.Add(nodeCourse.ChildCourse);
                    }
                    catch (Exception ex)
                    {
                    }


                }

                if (Session["Courses"] != null)
                {
                    Session.Remove("Courses");
                }
                Session.Add("Courses", _courses);
                if (Session["Node_Courses"] != null)
                {
                    Session.Remove("Node_Courses");
                }
                Session.Add("Node_Courses", _node_Courses);

                //ddlCourses.SelectedIndex = 0;
                ddlSetCrs.SelectedIndex = 0;

                //ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
                ddlSetCrs.ToolTip = ddlSetCrs.SelectedItem.Text;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillNodesCombo()
    {
        ddlNodes.Items.Clear();
        ddlSetNode.Items.Clear();

        _nodes = BussinessObject.Node.GetNodes();

        if (_nodes != null)
        {
            foreach (Node node in _nodes)
            {
                ListItem item = new ListItem();
                item.Value = node.Id.ToString();
                item.Text = node.Name;
                ddlNodes.Items.Add(item);

                ListEditItem dvxitem = new ListEditItem();
                dvxitem.Value = node.Id.ToString();
                dvxitem.Text = node.Name;
                ddlSetNode.Items.Add(dvxitem);
            }

            if (Session["Nodes"] != null)
            {
                Session.Remove("Nodes");
            }
            Session.Add("Nodes", _nodes);

            ddlNodes.SelectedIndex = 0;
            ddlSetNode.SelectedIndex = 0;
        }
    }
    private void FillOperatorCombo()
    {
        ddlOperators.Items.Clear();
        ddlSetOptrs.Items.Clear();

        _operators = Operator.GetOperators();

        if (_operators != null)
        {
            foreach (Operator opertor in _operators)
            {
                ListItem item = new ListItem();
                item.Value = opertor.OperatorID.ToString();
                item.Text = opertor.Name;
                ddlOperators.Items.Add(item);

                item = new ListItem();
                item.Value = opertor.OperatorID.ToString();
                item.Text = opertor.Name;
                ddlSetOptrs.Items.Add(item);
            }

            if (Session["Operators"] != null)
            {
                Session.Remove("Operators");
            }
            Session.Add("Operators", _operators);

            ddlOperators.SelectedIndex = 0;
            ddlSetOptrs.SelectedIndex = 0;
        }
    }

    private void ClearControl()
    {
        lblRequiredUnits.Visible = false;
        spnRequUnits.Value = 0;
        spnRequUnits.Visible = false;

        lblPass.Visible = false;
        spnPass.Value = 0;
        spnPass.Visible = false;

        lblPriority.Visible = false;
        spnPriority.Value = 0;
        spnPriority.Visible = false;

        lblMinCOurse.Visible = false;
        spnMinCourse.Value = 0;
        spnMinCourse.Visible = false;

        lblMaxCoure.Visible = false;
        spnMaxCourse.Value = 0;
        spnMaxCourse.Visible = false;

        lblMinCredits.Visible = false;
        spnMinCredits.Value = 0;
        spnMinCredits.Visible = false;

        lblMaxCredits.Visible = false;
        spnMaxCreds.Value = 0;
        spnMaxCreds.Visible = false;

        chkIsBundle.Visible = false;
        chkIsBundle.Checked = false;

        chkIsActive.Visible = false;
        chkIsActive.Checked = false;

        chkIsAssoc.Visible = false;
        chkIsAssoc.Checked = false;

        chkIsMajor.Visible = false;
        chkIsMajor.Checked = false;

        lblName.Visible = false;
        txtName.Visible = false;
        txtName.Text = string.Empty;
        //txtName.Enabled = false;

        lblNode.Visible = false;
        //lblMakNod.Visible = true;
        ddlNodes.Visible = false;
        if (ddlNodes.Items != null && ddlNodes.Items.Count > 0)
        {
            ddlNodes.SelectedIndex = 0;
        }

        //lblCourse.Visible = false;
        //lblMakCrs.Visible = true;
        //ddlCourses.Visible = false;
        //if (ddlCourses.Items != null && ddlCourses.Items.Count > 0)
        //{
        //    ddlCourses.SelectedIndex = 0;
        //}

        //ctlCoursePick.Visible = false;
        //if (ctlCoursePick.PickedCourse != null)
        //{
        //    ctlCoursePick.Clear();
        //}
        ctlCourseSelect.Visible = false;
        if (ctlCourseSelect.PickedCourse != null)
        {
            ctlCourseSelect.Clear();
        }

        lblOperator.Visible = false;
        //lblMakOprtr.Visible = true;
        ddlOperators.Visible = false;
        if (ddlOperators.Items != null && ddlOperators.Items.Count > 0)
        {
            ddlOperators.SelectedIndex = 0;
        }

        #region VNoteSet
        if (ddlSetCrs.Items != null && ddlSetCrs.Items.Count > 0)
        {
            ddlSetCrs.SelectedIndex = 0;
        }
        if (ddlSetNode.Items != null && ddlSetNode.Items.Count > 0)
        {
            ddlSetNode.SelectedIndex = 0;
        }
        if (ddlSetOptrs.Items != null && ddlSetOptrs.Items.Count > 0)
        {
            ddlSetOptrs.SelectedIndex = 0;
        }
        spnRequUnitsSet.Value = 0;
        spnRequUnitsSet.Visible = false;
        rbtSetNode.Checked = false;
        rbtSetCourse.Checked = true;
        txtVNodeSet.Text = string.Empty;
        txtVNodeSet.ReadOnly = false;
        #endregion

        pnlControl.Visible = false;
        pnlReg.Visible = false;
        pnlSavCan.Visible = false;
        pnlSet.Visible = false;

        pnlUpperCont.Enabled = true;
        pnlTree.Enabled = true;
        ddlPrograms.Enabled = true;
        ddlTree.Enabled = true;
        //ddlPrograms.SelectedIndex = -1;

        lblCaption.Text = string.Empty;
        lblCaption.BackColor = Color.Transparent;

        cboPreReqName.SelectedIndex = -1;

        gdvPreReq.DataSource = new List<PreReqDetail>();
        gdvPreReq.DataBind();

        txtPreReqName.Text = string.Empty;
        speOperatorcourseOccurance.Value = 0;
        speOperatorNodeOccurance.Value = 0;

        pnlPREREQ.Visible = false;
        //lblMsg.Text = string.Empty;
    }
    private void ControlEnablerRoot()
    {
        pnlControl.Visible = true;
        pnlReg.Visible = true;
        pnlSavCan.Visible = true;
        pnlUpperCont.Enabled = false;
        pnlTree.Enabled = false;

        lblName.Visible = true;
        txtName.Text = string.Empty;
        txtName.Visible = true;
        //txtName.Enabled = true;

        lblRequiredUnits.Visible = true;
        spnRequUnits.Value = 0;
        spnRequUnits.Visible = true;

        lblPass.Visible = true;
        spnPass.Value = 0;
        spnPass.Visible = true;

        lblOperator.Visible = true;
        //lblMakOprtr.Visible = true;
        ddlOperators.Visible = true;
        if (ddlOperators.Items != null && ddlOperators.Items.Count > 0)
        {
            ddlOperators.SelectedIndex = 0;
        }
        //lblName.Visible = true;
        ////lblMakName.Visible = true;
        //txtName.Visible = true;

        //lblCourse.Visible = false;
        ////lblMakCrs.Visible = false;
        //ddlCourses.Visible = false;

        //lblNode.Visible = false;
        ////lblMakNod.Visible = false;
        //ddlNodes.Visible = false;

        ddlPrograms.Enabled = false;
        ddlTree.Enabled = false;
    }
    private void ControlEnablerNode()
    {
        pnlControl.Visible = true;
        pnlReg.Visible = true;
        pnlSavCan.Visible = true;
        pnlUpperCont.Enabled = false;
        pnlTree.Enabled = false;

        lblName.Visible = true;
        txtName.Visible = true;
        txtName.Text = string.Empty;
        //txtName.Enabled = true;

        lblMinCOurse.Visible = true;
        spnMinCourse.Value = 0;
        spnMinCourse.Visible = true;

        lblMaxCoure.Visible = true;
        spnMaxCourse.Value = 0;
        spnMaxCourse.Visible = true;

        lblMinCredits.Visible = true;
        spnMinCredits.Value = 0;
        spnMinCredits.Visible = true;

        lblMaxCredits.Visible = true;
        spnMaxCreds.Value = 0;
        spnMaxCreds.Visible = true;

        chkIsBundle.Visible = true;
        chkIsBundle.Checked = false;

        chkIsActive.Visible = true;
        chkIsActive.Checked = true;

        chkIsAssoc.Visible = true;
        chkIsAssoc.Checked = true;

        chkIsMajor.Visible = true;
        chkIsMajor.Checked = false;

        lblOperator.Visible = true;
        //lblMakOprtr.Visible = true;
        ddlOperators.Visible = true;
        if (ddlOperators.Items != null && ddlOperators.Items.Count > 0)
        {
            ddlOperators.SelectedIndex = 0;
        }
        //lblCourse.Visible = false;
        //ddlCourses.Visible = false;

        //lblNode.Visible = false;
        //ddlNodes.Visible = false;

        ddlPrograms.Enabled = false;
        ddlTree.Enabled = false;
    }
    private void ControlEnablerSet()
    {
        pnlControl.Visible = true;
        pnlReg.Visible = false;
        pnlSet.Visible = true;
        pnlSavCan.Visible = true;
        pnlUpperCont.Enabled = false;
        pnlTree.Enabled = false;

        rbtSetCourse.Checked = true;
        rbtSetCourse_CheckedChanged(null, null);

        ddlPrograms.Enabled = false;
        ddlTree.Enabled = false;
    }
    private void ControlEnablerCourse()
    {
        pnlControl.Visible = true;
        pnlReg.Visible = true;
        pnlSavCan.Visible = true;
        pnlUpperCont.Enabled = false;
        pnlTree.Enabled = false;

        lblOperator.Visible = false;
        //lblMakOprtr.Visible = true;
        ddlOperators.Visible = false;
        if (ddlOperators.Items != null && ddlOperators.Items.Count > 0)
        {
            ddlOperators.SelectedIndex = 0;
        }

        //lblCourse.Visible = true;
        //lblMakCrs.Visible = true;
        //ddlCourses.Visible = true;
        //if (ddlCourses.Items != null && ddlCourses.Items.Count > 0)
        //{
        //    ddlCourses.SelectedIndex = 0;
        //}
        ctlCourseSelect.Visible = true;
        if (ctlCourseSelect.PickedCourse != null)
        {
            ctlCourseSelect.Clear();
        }

        lblPriority.Visible = true;
        spnPriority.Value = 0;
        spnPriority.Visible = true;

        //txtName.Enabled = false;

        chkIsActive.Visible = true;
        chkIsActive.Checked = true;
        //ddlCourses.Visible = true;
        //lblOperator.Visible = false;
        ////lblMakOprtr.Visible = false;
        //ddlOperators.Visible = false;

        //lblNode.Visible = false;
        ////lblMakNod.Visible = false;
        //ddlNodes.Visible = false;

        //lblName.Visible = false;
        ////lblMakName.Visible = false;
        //txtName.Visible = false;

        ddlPrograms.Enabled = false;
        ddlTree.Enabled = false;
    }

    private void LoadRoot(List<Node> rootNodes)
    {
        tvwMaster.Nodes.Clear();
        LoadNode(null, rootNodes);
    }
    private void FillChildrenONode(TreeNode treeNode, Node node, TreeMaster treeMaster)
    {
        if (!node.IsLastLevel)
        {
            if (!node.IsVirtual)
            {
                #region ChildNodes
                List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id, treeMaster.Id);

                if (treeDetails == null)
                {
                    treeDetails = TreeDetail.GetByParentNode(node.Id);
                }

                if (treeDetails != null)
                {
                    List<Node> nodes = new List<Node>();
                    foreach (TreeDetail treeDetail in treeDetails)
                    {
                        Node childNode = treeDetail.ChildNode;
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
                List<VNodeSetMaster> vNodeSetMas = node.VNodeSetMasters;
                if (vNodeSetMas != null)
                {
                    LoadNode(treeNode, vNodeSetMas);
                }
                else //if (treeNode.Parent != null)
                {
                    treeNode.ChildNodes.Clear();
                }
                #endregion
            }
        }
        else
        {
            #region Node_Courses
            if (node.Node_Courses != null)
            {
                List<Course> courses = new List<Course>();
                foreach (NodeCourse node_Course in node.Node_Courses)
                {
                    Course childCourse = Course.GetCourse(node_Course.ChildCourseID, node_Course.ChildVersionID);
                    courses.Add(childCourse);
                }
                treeNode.ChildNodes.Clear();
                LoadNode(treeNode, courses);
            }
            #endregion
        }
    }
    private void LoadChildrens(TreeNode treeNode)
    {
        _clsNameAndID = treeNode.Value.Split(',');

        try
        {

            if (_clsNameAndID[0] == "NOD")
            {
                #region If Parent is a Node
                if (Session["TreeMaster"] != null)
                {
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];
                    Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));

                    if (!node.IsLastLevel)
                    {
                        if (!node.IsVirtual)
                        {
                            #region ChildNodes
                            List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id, treeMaster.Id);
                            if (treeDetails != null)
                            {
                                List<Node> nodes = new List<Node>();
                                foreach (TreeDetail treeDetail in treeDetails)
                                {
                                    Node childNode = treeDetail.ChildNode;
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
                            if (node.VNodeSetMasters != null)
                            {
                                treeNode.ChildNodes.Clear();
                                LoadNode(treeNode, node.VNodeSetMasters);
                            }
                            else //if (treeNode.Parent != null)
                            {
                                treeNode.ChildNodes.Clear();
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region Node_Courses
                        if (node.Node_Courses != null)
                        {
                            List<Course> courses = new List<Course>();
                            foreach (NodeCourse node_Course in node.Node_Courses)
                            {
                                Course childCourse = Course.GetCourse(node_Course.ChildCourseID, node_Course.ChildVersionID);
                                courses.Add(childCourse);
                            }
                            treeNode.ChildNodes.Clear();
                            LoadNode(treeNode, courses);
                        }
                        #endregion
                    }
                }
                #endregion
            }
            else if (_clsNameAndID[0] == "SETMAS")
            {
                #region If Parent is a SetMaster
                if (Session["TreeMaster"] != null)
                {
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    string[] setNoAndNodeID = new string[3];
                    setNoAndNodeID = _clsNameAndID[1].Split('#');

                    Node node = Node.GetNode(Int32.Parse(setNoAndNodeID[1]));

                    if (node.VNodeSets != null)
                    {
                        VNodeSetMaster vNodeSetMaster = null;

                        foreach (VNodeSetMaster vNodeSetMasterInner in node.VNodeSetMasters)
                        {
                            if (vNodeSetMasterInner.SetNo == Int32.Parse(setNoAndNodeID[0]))
                            {
                                vNodeSetMaster = vNodeSetMasterInner;
                            }
                        }

                        if (vNodeSetMaster == null)
                        {
                            treeNode.ChildNodes.Clear();
                            tvwMaster.Nodes.Remove(treeNode);
                        }
                        else if (vNodeSetMaster.VNodeSets != null)
                        {
                            treeNode.ChildNodes.Clear();
                            LoadNode(treeNode, vNodeSetMaster.VNodeSets);
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
                #endregion
            }
            else if (_clsNameAndID[0] == "VNODSET")
            {
                #region If Parent is a VnodeSet
                if (Session["TreeMaster"] != null)
                {
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];
                    VNodeSet vNodeSet = VNodeSet.Get(Int32.Parse(_clsNameAndID[1]));

                    if (vNodeSet.OperandNodeID != 0)
                    {
                        Node node = Node.GetNode(vNodeSet.OperandNodeID);
                        FillChildrenONode(treeNode, node, treeMaster);
                    }
                }
                #endregion
            }
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }
    private void LoadNode(TreeNode parentNode, List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = node.Name;
            treeNode.Value = "NOD," + node.Id.ToString();
            treeNode.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMaster.Nodes.Add(treeNode);
                //LoadChildrens(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(treeNode);
            }
        }
    }
    private void LoadNode(TreeNode parentNode, List<VNodeSetMaster> vNodeMasters)
    {
        foreach (VNodeSetMaster vNodeMaster in vNodeMasters)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = vNodeMaster.SetName;
            treeNode.Value = "SETMAS," + vNodeMaster.SetNo.ToString() + "#" + vNodeMaster.OwnerNodeID.ToString() + "#" + vNodeMaster.Id.ToString();
            treeNode.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMaster.Nodes.Add(treeNode);
                //LoadChildrens(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(treeNode);
            }
        }
    }
    private void LoadNode(TreeNode parentNode, List<VNodeSet> vNodesets)
    {
        foreach (VNodeSet vNodeSet in vNodesets)
        {
            TreeNode treeNode = new TreeNode();

            if (!vNodeSet.IsStudntSpec && vNodeSet.OperandNodeID == 0)
            {
                treeNode.Text = vNodeSet.OperandCourse.Title + " - " + vNodeSet.Operator.Name;
                treeNode.Value = "VNODSET," + vNodeSet.Id.ToString();
            }
            else if (!vNodeSet.IsStudntSpec && vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0)
            {
                treeNode.Text = vNodeSet.OperandNode.Name + " - " + vNodeSet.Operator.Name;
                treeNode.Value = "VNODSET," + vNodeSet.Id.ToString();
            }
            else
            {
                treeNode.Text = "Student specific major - " + vNodeSet.Operator.Name;
                treeNode.Value = "VNODSET," + vNodeSet.Id.ToString();
            }

            treeNode.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMaster.Nodes.Add(treeNode);
                //LoadChildrens(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(treeNode);
            }
        }
    }
    private void LoadNode(TreeNode parentNode, IEnumerable<VNodeSet> vNodesets)
    {
        foreach (VNodeSet vNodeSet in vNodesets)
        {
            TreeNode treeNode = new TreeNode();

            if (vNodeSet.OperandNodeID == 0)
            {
                treeNode.Text = vNodeSet.OperandCourse.Title + " - " + vNodeSet.Operator.Name;
                treeNode.Value = "VNODSET," + vNodeSet.Id.ToString();
            }
            else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0)
            {
                treeNode.Text = vNodeSet.OperandNode.Name + " - " + vNodeSet.Operator.Name;
                treeNode.Value = "VNODSET," + vNodeSet.Id.ToString();
            }

            treeNode.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMaster.Nodes.Add(treeNode);
                //LoadChildrens(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(treeNode);
            }
        }
    }
    private void LoadNode(TreeNode parentNode, List<Course> courses)
    {
        foreach (Course course in courses)
        {
            TreeNode node = new TreeNode();
            if (course.OwnerProgram == null)
            {
                node.Text = course.FormalCode + " - " + course.VersionCode + " - " + course.Title + " [ # ]" + " [ " + course.Credits + " ]";
            }
            else
            {
                node.Text = course.FormalCode + " - " + course.VersionCode + " - " + course.Title + " [ " + course.OwnerProgram.ShortName + " ]" + " [ " + course.Credits + " ]";
            }

            node.Value = "CRS," + course.Id.ToString() + "#" + course.VersionID.ToString();
            node.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMaster.Nodes.Add(node);
                //LoadChildrens(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(node);
            }
        }
    }

    private bool ValidateNode()
    {
        if (txtName.Text.Trim().Length <= 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Node name can not be empty";

            return false;
        }
        else if (ddlOperators.SelectedIndex < 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select an operator";

            return false;
        }
        else
        {
            return true;
        }
    }
    private void FillNodeCtl(Node node)
    {
        txtName.Text = node.Name;
        ddlOperators.SelectedValue = node.OperatorID.ToString();
        spnMinCourse.Value = node.MinCourses;
        spnMaxCourse.Value = node.MaxCourses;
        spnMinCredits.Value = node.MinCredit;
        spnMaxCreds.Value = node.MaxCredit;
        chkIsBundle.Checked = node.IsBundle;
        chkIsActive.Checked = node.IsActive;
        chkIsAssoc.Checked = node.IsAssociated;
        chkIsMajor.Checked = node.IsMajor;
    }
    private Node RefreshChildNode()
    {
        Node childNode = null;

        if (Session["CurrentNode"] == null)
        {
            childNode = new Node();
        }
        else
        {
            childNode = (Node)Session["CurrentNode"];
        }

        childNode.Name = txtName.Text.Trim();
        childNode.OperatorID = Int32.Parse(ddlOperators.SelectedValue);

        childNode.MinCourses = Convert.ToInt32(spnMinCourse.Value); ;
        childNode.MaxCourses = Convert.ToInt32(spnMaxCourse.Value); ;
        childNode.MinCredit = Convert.ToDecimal(spnMinCredits.Value); ;
        childNode.MaxCredit = Convert.ToDecimal(spnMaxCreds.Value);
        childNode.IsBundle = chkIsBundle.Checked;
        childNode.IsActive = chkIsActive.Checked;
        childNode.IsAssociated = chkIsAssoc.Checked;
        childNode.IsMajor = chkIsMajor.Checked;

        childNode.PreReqMasters = PreparePrerequisits();
        if (childNode.PreReqMasters == null)
        {
            childNode.HasPreriquisite = false;
        }
        else
        {
            childNode.HasPreriquisite = true;
        }

        return childNode;
    }
    private TreeDetail RefreshTreeDet()
    {
        TreeDetail treeDetail = new TreeDetail();
        treeDetail.TreeMasterID = ((TreeMaster)Session["TreeMaster"]).Id;

        treeDetail.ParentNodeID = ((Node)Session["ParentNode"]).Id; ;

        return treeDetail;
    }


    private bool ValidateRoot()
    {
        if (txtName.Text.Trim().Length <= 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Root name can not be empty";

            return false;
        }
        else
        {
            return true;
        }
    }
    private void FillRootCtl(Node node, TreeMaster treeMaster)
    {
        txtName.Text = node.Name;
        spnRequUnits.Value = treeMaster.ReqCredits;
        spnPass.Value = treeMaster.PassingGPA;
    }
    private Node RefreshRootNode()
    {
        Node rootNode = null;

        if (Session["CurrentNode"] == null)
        {
            rootNode = new Node();
        }
        else
        {
            rootNode = (Node)Session["CurrentNode"];
        }

        rootNode.Name = txtName.Text.Trim();
        rootNode.OperatorID = Int32.Parse(ddlOperators.SelectedValue);

        rootNode.PreReqMasters = PreparePrerequisits();
        if (rootNode.PreReqMasters == null)
        {
            rootNode.HasPreriquisite = false;
        }
        else
        {
            rootNode.HasPreriquisite = true;
        }

        return rootNode;
    }
    private TreeMaster RefreshTreeMas()
    {
        TreeMaster treeMaster = null;
        if (!IsSessionVariableExists("TreeMaster"))
        {
            treeMaster = new TreeMaster();
        }
        else
        {
            treeMaster = (TreeMaster)Session["TreeMaster"];
        }

        treeMaster.OwnerProgramID = Int32.Parse(ddlPrograms.SelectedValue);
        treeMaster.ReqCredits = Convert.ToDecimal(spnRequUnits.Value);
        treeMaster.PassingGPA = Convert.ToDecimal(spnPass.Value);

        return treeMaster;
    }


    private void ShowRoot()
    {
        if (ddlTree.SelectedIndex >= 0)
        {

            if (Session["TreeMasters"] != null)
            {
                TreeMaster treeMaster = TreeMaster.Get(Int32.Parse(ddlTree.SelectedValue));

                if (treeMaster != null)
                {
                    List<Node> rootNodes = new List<Node>();

                    if (treeMaster.RootNode != null)
                    {
                        rootNodes.Add(treeMaster.RootNode);

                        if (!pnlTree.Enabled)
                        {
                            pnlTree.Enabled = true;
                        }

                        LoadRoot(rootNodes);

                        if (Session["TreeMaster"] != null)
                        {
                            Session.Remove("TreeMaster");
                        }
                        Session.Add("TreeMaster", treeMaster);
                    }
                }
                else
                {
                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    tvwMaster.Nodes.Clear();
                }
            }
        }
    }

    private bool ValidateCourse()
    {
        //if (ddlCourses.SelectedIndex < 0)
        if (ctlCourseSelect.PickedCourse == null)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select a Course";

            return false;
        }
        else
        {
            return true;
        }
    }
    private NodeCourse RefreshNodeCourse()
    {
        NodeCourse nodeCourse = null;
        if (Session["Node_Course"] == null)
        {
            nodeCourse = new NodeCourse();
        }
        else
        {
            nodeCourse = (NodeCourse)Session["Node_Course"];
        }

        nodeCourse.ParentNodeID = ((Node)Session["ParentNode"]).Id; ;

        //string[] courseIDnVerID = new string[2];
        //courseIDnVerID = ddlCourses.Value.ToString().Split(',');
        //nodeCourse.ChildCourseID = Int32.Parse(courseIDnVerID[0]);
        //nodeCourse.ChildVersionID = Int32.Parse(courseIDnVerID[1]);
        nodeCourse.ChildCourseID = ctlCourseSelect.PickedCourse.Id;
        nodeCourse.ChildVersionID = ctlCourseSelect.PickedCourse.VersionID;

        nodeCourse.Priority = Convert.ToInt32(spnPriority.Value);
        nodeCourse.IsActive = chkIsActive.Checked;

        nodeCourse.PreReqMasters = PreparePrerequisits();
        if (nodeCourse.PreReqMasters == null)
        {
            nodeCourse.HasPreriquisite = false;
        }
        else if (nodeCourse.PreReqMasters != null)
        {
            nodeCourse.HasPreriquisite = false;
        }
        else
        {
            nodeCourse.HasPreriquisite = true;
        }


        return nodeCourse;
    }
    private void FillNodeCourseCtl(NodeCourse nodeCourse)
    {
        //ddlCourses.Value = nodeCourse.ChildCourseID.ToString() + "," + nodeCourse.ChildVersionID.ToString();
        //ddlCourses.SelectedItem.Text = nodeCourse.ChildCourse.FormalCode + "-" + nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
        //ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
        ctlCourseSelect.PickedCourse = nodeCourse.ChildCourse;

        spnPriority.Value = nodeCourse.Priority;
        chkIsActive.Checked = nodeCourse.IsActive;
    }

    private bool ValidateVNodeSet()
    {
        int setNo = 0;
        if (txtVNodeSet.Text.Trim().Length <= 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Set no can not be empty";

            return false;
        }
        else if (!Int32.TryParse(txtVNodeSet.Text.Trim(), out setNo))
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Set no must be numeric";

            return false;
        }
        else if (rbtSetCourse.Checked && ddlSetCrs.SelectedIndex < 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select an operand Course";

            return false;
        }
        else if (rbtSetNode.Checked && ddlSetNode.SelectedIndex < 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select an operand Node";

            return false;
        }
        else if (!rbtSetNode.Checked && !rbtSetCourse.Checked && !chkStudSpec.Checked)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select either an operand Node or an operand Code or the student specific major option";

            return false;
        }
        else if (ddlSetOptrs.SelectedIndex < 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Must select an operator";

            return false;
        }
        else
        {
            return true;
        }
    }
    private VNodeSetMaster RefreshVNodeSetMas()
    {
        VNodeSetMaster vNodeSetMas = new VNodeSetMaster();

        vNodeSetMas.OwnerNodeID = ((Node)Session["ParentNode"]).Id; ;
        vNodeSetMas.SetNo = Int32.Parse(txtVNodeSet.Text.Trim());
        vNodeSetMas.RequiredUnits = Convert.ToDecimal(spnRequUnitsSet.Value);

        vNodeSetMas.VNodeSets = new List<VNodeSet>();
        vNodeSetMas.VNodeSets.Add(RefreshVNodeSet());

        return vNodeSetMas;
    }
    private VNodeSet RefreshVNodeSet()
    {
        UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);


        VNodeSet vNodeSet = new VNodeSet();

        vNodeSet.OwnerNodeID = ((Node)Session["ParentNode"]).Id; ;

        if (!chkStudSpec.Checked)
        {
            if (rbtSetCourse.Checked)
            {
                string[] courseIDnVerID = new string[2];
                courseIDnVerID = ddlSetCrs.Value.ToString().Split(',');
                vNodeSet.OperandCourseID = Int32.Parse(courseIDnVerID[0]);
                vNodeSet.OperandVersionID = Int32.Parse(courseIDnVerID[1]);

                if (Session["Courses"] != null)
                {
                    _courses = (List<Course>)Session["Courses"];

                    if (Session["Node_Courses"] != null)
                    {
                        _node_Courses = (List<NodeCourse>)Session["Node_Courses"];
                    }
                    Course course = _courses[ddlSetCrs.SelectedIndex];
                    NodeCourse node_course = _node_Courses[ddlSetCrs.SelectedIndex];

                    vNodeSet.NodeCourseID = node_course.Id;
                }
            }
            else if (rbtSetNode.Checked)
            {
                string OpNodeID = ddlSetNode.Value.ToString();
                vNodeSet.OperandNodeID = Int32.Parse(OpNodeID);
            }
        }
        else
        {
            vNodeSet.IsStudntSpec = chkStudSpec.Checked;
        }

        vNodeSet.OperatorID = Int32.Parse(ddlSetOptrs.SelectedValue);

        vNodeSet.SetNo = Int32.Parse(txtVNodeSet.Text.Trim());

        vNodeSet.WildCard = txtWildCard.Text.Trim();


        if (Session["VNodeSetMas"] != null)
        {
            VNodeSetMaster vNodeSetMas = (VNodeSetMaster)Session["VNodeSetMas"];
            vNodeSet.VNodeSetMasID = vNodeSetMas.Id;
        }

        vNodeSet.CreatorID = CurrentUser.Id;
        vNodeSet.CreatedDate = DateTime.Now;

        return vNodeSet;
    }

    private bool IsUnderAVirtualNode(TreeNode treeNode)
    {
        if (treeNode.Value.Split(new char[] { ',' })[0] == "VNODSET")
        {
            return true;
        }
        else
        {
            if (treeNode.Parent != null)
            {
                return IsUnderAVirtualNode(treeNode.Parent);
            }
            else
            {
                return false;
            }
        }
    }

    private void ClearMessagelbl()
    {
        lblMsg.Text = string.Empty;
        lblMsg.ForeColor = Color.Red;
    }

    private void InitializePreReqArea()
    {
        pnlPREREQ.Visible = true;
        pnlPreReqArea.Visible = false;
        //_preReqMasters = PreRequisiteMaster.GetMasters(string.Empty);
        cboPreReqName.Items.Clear();
        cboPreReqName.Items.Add("(New Name)", 0);
        if (_preReqMasters == null || _preReqMasters.Count == 0)
        {
            cboPreReqName.SelectedIndex = 0;
        }
        //else
        //{
        if (Session["CurrentNode"] == null || Session["Node_Course"] != null)
        {
            return;
        }
        FillPreReqMasterCbo();
        //}
    }

    /// <summary>
    ///To be done
    /// </summary>
    private void RefreshPreReqArea()
    {
        pnlPREREQ.Visible = true;

        TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
        _clsNameAndID = treeNode.Value.Split(',');

        if (_clsNameAndID[0] == "CRS")
        {
            string[] parentNodeNameAndID = new string[2];
            parentNodeNameAndID = treeNode.Parent.Value.Split(',');

            string[] childCourseCodeandVersion = new string[2];
            childCourseCodeandVersion = _clsNameAndID[1].Split('#');

            NodeCourse node_course = NodeCourse.GetByParentNode(Int32.Parse(parentNodeNameAndID[1]), Int32.Parse(childCourseCodeandVersion[0]), Int32.Parse(childCourseCodeandVersion[1]));

            //edit by jahid
            //_preReqMasters = PreRequisiteMaster.GetMastersByNode_courseID(node_course.Id);
        }
        else if (_clsNameAndID[0] == "NOD")
        {
            Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));

            //edit by jahid
            //_preReqMasters = PreRequisiteMaster.GetMastersByNode(node.Id);
        }

        cboPreReqName.Items.Clear();
        cboPreReqName.Items.Add("(New Name)", 0);
        if (_preReqMasters == null)
        {
            cboPreReqName.SelectedIndex = 0;
        }
        else
        {
            FillPreReqMasterCbo();
            cboPreReqName.Items.RemoveAt(0);
            cboPreReqName.SelectedIndex = -1;
        }
        pnlPreReqArea.Visible = false;
    }

    private void FillPreReqMasterCbo()
    {
        try
        {

            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
            _clsNameAndID = treeNode.Value.Split(',');

            if (_preReqMasters == null)
            {
                if (_clsNameAndID[0] == "CRS")
                {
                    string[] parentNodeNameAndID = new string[2];
                    parentNodeNameAndID = treeNode.Parent.Value.Split(',');

                    string[] childCourseCodeandVersion = new string[2];
                    childCourseCodeandVersion = _clsNameAndID[1].Split('#');

                    NodeCourse node_course = NodeCourse.GetByParentNode(Int32.Parse(parentNodeNameAndID[1]), Int32.Parse(childCourseCodeandVersion[0]), Int32.Parse(childCourseCodeandVersion[1]));

                    _preReqMasters = PreRequisiteMaster.GetMastersByNode_courseID(node_course.Id);
                }
                else if (_clsNameAndID[0] == "NOD")
                {
                    Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
                    _preReqMasters = PreRequisiteMaster.GetMastersByNode(node.Id);
                }
            }

            if (cboPreReqName.Items == null)
            {
                cboPreReqName.Items.Clear();
                cboPreReqName.Items.Add("(New Name)", 0);
            }
            else if (cboPreReqName.Items.Count == 0)
            {
                cboPreReqName.Items.Clear();
                cboPreReqName.Items.Add("(New Name)", 0);
            }


            if (_preReqMasters == null)
            {
                cboPreReqName.SelectedIndex = 0;
                return;
            }

            RemoveFromSession(SESSIONPREREQMASTERS);
            AddToSession(SESSIONPREREQMASTERS, _preReqMasters);

            cboPreReqName.TextField = "Name";
            cboPreReqName.ValueField = "Id";

            foreach (PreRequisiteMaster pcm in _preReqMasters)
            {
                ListEditItem item = new ListEditItem();
                item.Value = pcm.Id;
                item.Text = pcm.Name;
                cboPreReqName.Items.Add(item);
            }
            cboPreReqName.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private void FillGridCourseCombo(ASPxComboBox combo)
    {
        try
        {
            _nodeCouses = NodeCourse.GetNodeCoursesByRoot(Int32.Parse(ddlTree.SelectedValue));//Course.GetActiveMotherCourses();
            if (_nodeCouses.Count == 0)
            {
                return;
            }
            combo.Items.Clear();
            combo.Items.Add("(None)", 0);
            combo.TextField = "ChildCourse.FullCodeAndCourse";
            combo.ValueField = "Id";

            foreach (NodeCourse nc in _nodeCouses)
            {
                ListEditItem item = new ListEditItem();
                item.Value = nc.Id;
                item.Text = nc.ChildCourse.FullCodeAndCourse;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillGridNodeCombo(ASPxComboBox combo)
    {
        try
        {
            _nodes = Node.GetNodesbyRoot(Int32.Parse(ddlTree.SelectedValue));
            if (_nodes.Count == 0)
            {
                return;
            }
            combo.Items.Clear();
            combo.TextField = "Node.Name";
            combo.ValueField = "Id";
            combo.Items.Add("(None)", 0);

            foreach (Node node in _nodes)
            {
                ListEditItem item = new ListEditItem();
                item.Value = node.Id;
                item.Text = node.Name;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillGridOperatorCombo(ASPxComboBox combo)
    {
        try
        {
            _operators = Operator.GetOperators();
            if (_operators.Count == 0)
            {
                return;
            }
            combo.Items.Clear();
            combo.TextField = "Operator.Name";
            combo.ValueField = "OperatorID";
            combo.Items.Add("(None)", 0);

            foreach (Operator op in _operators)
            {
                ListEditItem item = new ListEditItem();
                item.Value = op.OperatorID;
                item.Text = op.Name;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private List<PreRequisiteMaster> PreparePrerequisits()
    {
        _preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
        _preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];

        if ((_preReqDetails_Node != null && _preReqDetails_Node.Count != 0) || (_preReqDetails_Course != null && _preReqDetails_Course.Count != 0))
        {
            if (IsSessionVariableExists(SESSIONPREREQMASTER))
            {
                _preReqMaster = (PreRequisiteMaster)GetFromSession(SESSIONPREREQMASTER);
            }
            else
            {
                _preReqMaster = new PreRequisiteMaster();
            }
            _preReqMaster.Name = txtPreReqName.Text;
            _preReqMaster.ProgramID = Convert.ToInt32(ddlPrograms.SelectedValue.ToString());

            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
            _clsNameAndID = treeNode.Value.Split(',');

            //if (_clsNameAndID[0] == "CRS" && Session["Node_Course"] != null)
            //{
            //    string[] parentNodeNameAndID = new string[2];
            //    parentNodeNameAndID = treeNode.Parent.Value.Split(',');

            //    string[] childCourseCodeandVersion = new string[2];
            //    childCourseCodeandVersion = _clsNameAndID[1].Split('#');

            //    Node_Course node_course = Node_Course.GetByParentNode(Int32.Parse(parentNodeNameAndID[1]), Int32.Parse(childCourseCodeandVersion[0]), Int32.Parse(childCourseCodeandVersion[1]));
            //    _preReqMaster.NodeCourseID = node_course.Id;
            //}
            //else if (_clsNameAndID[0] == "NOD" && Session["CurrentNode"] != null)
            //{
            //    Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
            //    _preReqMaster.Node_ID = node.Id;
            //}

            _preReqMaster.ReqCredits = 0;
            if (cboPreReqName.SelectedItem.Value.ToString() == "0")
            {
                _preReqMaster.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _preReqMaster.CreatedDate = DateTime.Now;
            }
            else
            {
                _preReqMaster.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _preReqMaster.ModifiedDate = DateTime.Now;
            }

            if (_preReqDetails_Course != null)
            {
                if (Convert.ToInt32(speOperatorcourseOccurance.Value.ToString()) > 0)
                {
                    foreach (PreReqDetail pr in _preReqDetails_Course)
                    {
                        pr.OperatorIDMinOccurance = Convert.ToInt32(speOperatorcourseOccurance.Value.ToString());
                    }
                }
                _preReqMaster.PreReqDetailCourses = _preReqDetails_Course;
            }

            if (_preReqDetails_Node != null)
            {
                if (Convert.ToInt32(speOperatorNodeOccurance.Value.ToString()) > 0)
                {
                    foreach (PreReqDetail pr in _preReqDetails_Node)
                    {
                        pr.OperatorIDMinOccurance = Convert.ToInt32(speOperatorNodeOccurance.Value.ToString());
                    }
                }
                _preReqMaster.PreReqDetailNodes = _preReqDetails_Node;
            }

            if (IsSessionVariableExists(SESSIONPREREQMASTERS))
            {
                _preReqMasters = (List<PreRequisiteMaster>)GetFromSession(SESSIONPREREQMASTERS);
            }

            if (_preReqMasters != null)
            {
                if (cboPreReqName.SelectedItem.Value.ToString() != "0")
                {
                    int index = 0;
                    foreach (PreRequisiteMaster item in _preReqMasters)
                    {
                        if (_preReqMaster.Id == item.Id)
                        {
                            break;
                        }
                        index++;
                    }
                    _preReqMasters.RemoveAt(index);

                    _preReqMasters.Add(_preReqMaster);
                }
                else
                {
                    _preReqMasters.Add(_preReqMaster);
                }
            }
            else
            {
                _preReqMasters = new List<PreRequisiteMaster>();
                _preReqMasters.Add(_preReqMaster);
            }
        }
        else
        {
            if (IsSessionVariableExists(SESSIONPREREQMASTERS))
            {
                _preReqMasters = (List<PreRequisiteMaster>)GetFromSession(SESSIONPREREQMASTERS);
            }
        }

        return _preReqMasters;
    }
    private PreReqDetail RefreshPreReq(OrderedDictionary newValues, string strButtonText)
    {
        PreReqDetail preReq = new PreReqDetail();

        preReq.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
        preReq.CreatedDate = DateTime.Now;

        if (lblOccurance.Text == strPreReqCourseOccurance)
        {
            if (newValues["NodeCourse.ChildCourse.FullCodeAndCourse"] == null || newValues["NodeCourse.ChildCourse.FullCodeAndCourse"].ToString() == string.Empty)
            {
                preReq.PreReqNodeCourseID = 0;
            }
            else
            {
                preReq.PreReqNodeCourseID = Convert.ToInt32(newValues["NodeCourse.ChildCourse.FullCodeAndCourse"].ToString());
            }
            preReq.OperatorIDMinOccurance = Convert.ToInt32(speOperatorcourseOccurance.Value.ToString());
        }
        else if (lblOccurance.Text == strPreReqNodeOccurance)
        {
            if (newValues["Node.Name"] == null || newValues["Node.Name"].ToString() == string.Empty)
            {
                preReq.PreReqNodeID = 0;
            }
            else
            {
                preReq.PreReqNodeID = Convert.ToInt32(newValues["Node.Name"].ToString());
            }
            preReq.OperatorIDMinOccurance = Convert.ToInt32(speOperatorNodeOccurance.Value.ToString());
        }

        TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
        _clsNameAndID = treeNode.Value.Split(',');
        if (_clsNameAndID[0] == "CRS")
        {
            string[] parentNodeNameAndID = new string[2];
            parentNodeNameAndID = treeNode.Parent.Value.Split(',');

            string[] childCourseCodeandVersion = new string[2];
            childCourseCodeandVersion = _clsNameAndID[1].Split('#');

            NodeCourse node_course = NodeCourse.GetByParentNode(Int32.Parse(parentNodeNameAndID[1]), Int32.Parse(childCourseCodeandVersion[0]), Int32.Parse(childCourseCodeandVersion[1]));
            preReq.NodeCourseID = node_course.Id;
        }
        else if (_clsNameAndID[0] == "NOD")
        {
            Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
            preReq.Node_ID = node.Id;
        }

        preReq.ReqCredits = (newValues["ReqCredits"] != null) ? Convert.ToDecimal(newValues["ReqCredits"].ToString()) : 0.00m;
        if (newValues["Operator.Name"] == null || newValues["Operator.Name"].ToString() == string.Empty)
        {
            preReq.OperatorID = 0;
        }
        else
        {
            preReq.OperatorID = Convert.ToInt32(newValues["Operator.Name"].ToString());
        }

        return preReq;
    }

    private void UpdateData(OrderedDictionary newValues, int detailID)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (lblOccurance.Text == strPreReqCourseOccurance)
            {
                PreReqDetail detail = RefreshPreReq(newValues, btnPreReqCourse.Text);
                detail.Id = detailID;
                if (Session[SESSIONPREREQCOURSE] != null)
                {
                    _preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
                }

                if (cboPreReqName.SelectedItem.Value.ToString() == "0")
                {
                    if (detail != null)
                    {
                        _preReqDetails_Course.Add(detail);
                    }
                }
                else
                {
                    if (detail.Id != 0)
                    {
                        int intIndex = 0;
                        foreach (PreReqDetail ec in _preReqDetails_Course)
                        {

                            if (ec.Id == detail.Id)
                            {
                                break;
                            }
                            intIndex++;
                        }
                        _preReqDetails_Course.RemoveAt(intIndex);
                        _preReqDetails_Course.Add(detail);
                    }
                    else
                    {
                        _preReqDetails_Course.Add(detail);
                    }
                }
                BindGrid(SESSIONPREREQCOURSE, _preReqDetails_Course);
            }
            if (lblOccurance.Text == strPreReqNodeOccurance)
            {
                PreReqDetail detail = RefreshPreReq(newValues, btnPreReqCourse.Text);
                detail.Id = detailID;
                if (Session[SESSIONPREREQNODE] != null)
                {
                    _preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];
                }
                if (cboPreReqName.SelectedItem.Value.ToString() == "0")
                {
                    if (detail != null)
                    {
                        _preReqDetails_Node.Add(detail);
                    }
                }
                else
                {
                    if (detail.Id != 0)
                    {
                        int intIndex = 0;
                        foreach (PreReqDetail ec in _preReqDetails_Node)
                        {

                            if (ec.Id == detail.Id)
                            {
                                break;
                            }
                            intIndex++;
                        }
                        _preReqDetails_Node.RemoveAt(intIndex);
                        _preReqDetails_Node.Add(detail);
                    }
                    else
                    {
                        _preReqDetails_Node.Add(detail);
                    }
                }
                BindGrid(SESSIONPREREQNODE, _preReqDetails_Node);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void BindGrid(string strSessionName, List<PreReqDetail> preReqs)
    {
        if (Session[strSessionName] != null)
        {
            Session.Remove(strSessionName);
        }

        gdvPreReq.DataSource = null;
        if (preReqs == null)
        {
            gdvPreReq.DataSource = new List<PreReqDetail>();
        }
        else if (preReqs.Count == 0)
        {
            gdvPreReq.DataSource = new List<PreReqDetail>();
        }
        else
        {
            Session.Add(strSessionName, preReqs);
            gdvPreReq.DataSource = preReqs;

        }

        gdvPreReq.DataBind();

    }

    private void PreReqButtonState(bool state)
    {
        btnPreReqNode.Enabled = state;
        btnPreReqCourse.Enabled = state;
        pnlSavCan.Visible = state;
    }
    #endregion

    #region Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();

            //UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            //if (UIUMSUser.CurrentUser != null)
            //{
            //    if (UIUMSUser.CurrentUser.RoleID > 0)
            //    {
            //        Authenticate(UIUMSUser.CurrentUser);
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Security/Login.aspx");
            //}

            if (!IsPostBack && !IsCallback)
            {
                FillProgCombo();
                FillCorsCombo();
                FillNodesCombo();
                FillOperatorCombo();

                RemoveFromSession(SESSIONPREREQMASTERS);
                RemoveFromSession(SESSIONPREREQMASTER);
                RemoveFromSession(SESSIONPREREQCOURSE);
                RemoveFromSession(SESSIONPREREQNODE);

                ddlPrograms_SelectedIndexChanged(null, null);
                gdvPreReq.DataSource = new List<PreReqDetail>();
                gdvPreReq.DataBind();
                pcTabControl.ActiveTabIndex = 0;
                pnlPREREQ.Visible = false;
                pnlPreReqArea.Visible = false;
            }
            btnDel.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");

            ddlPrograms.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void butAddRoot_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (Session["TreeMaster"] != null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Root already exists";

                return;
            }

            ControlEnablerRoot();
            lblCaption.Text = string.Empty;

            lblCaption.ForeColor = Color.SteelBlue;
            lblCaption.BackColor = Color.AliceBlue;

            lblCaption.Text = "Add Root";


            _isAddingRoot = true;
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            Session.Add("IsAddingRoot", _isAddingRoot);
            txtName.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butAddNode_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwMaster.SelectedNode == null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Before trying to add a node please select a root or parent node";
                return;
            }

            if (IsUnderAVirtualNode(tvwMaster.SelectedNode))
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding node to another node from under a set is not permitted, if you want to add a course/node please add it from actual location.";
                return;
            }

            _clsNameAndID = tvwMaster.SelectedNode.Value.Split(',');


            if (_clsNameAndID[0] != "NOD")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "A node can only be added under a node or root.";
                return;
            }

            if (_clsNameAndID[0] == "CRS")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding Node under a course is not permitted.";
                return;
            }

            Node parentNode = Node.GetNode(Int32.Parse(_clsNameAndID[1]));

            if (parentNode.IsLastLevel == true)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding a node under this node is not permitted, only course can be added under this node.";
                return;
            }
            if (parentNode.IsVirtual == true)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "This is a virtual node, to link nodes to it try adding sets.";
                return;
            }

            if (Session["ParentNode"] != null)
            {
                Session.Remove("ParentNode");
            }
            Session.Add("ParentNode", parentNode);

            ControlEnablerNode();

            lblCaption.Text = string.Empty;

            lblCaption.ForeColor = Color.Crimson;
            lblCaption.BackColor = Color.MistyRose;

            lblCaption.Text = "Add Node";

            _isAddingNode = true;

            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            Session.Add("IsAddingNode", _isAddingNode);

            RemoveFromSession(SESSIONPREREQMASTERS);
            InitializePreReqArea();

            txtName.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butAddSet_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwMaster.SelectedNode == null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Before trying to add a node please select a root or parent node";
                return;
            }
            if (IsUnderAVirtualNode(tvwMaster.SelectedNode))
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding set to another node from under a set is not permitted, if you want to add a node please add it from actual location.";
                return;
            }

            _clsNameAndID = tvwMaster.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] == "CRS")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding set under a course is not permitted.";
                return;
            }

            if (_clsNameAndID[0] != "NOD" && _clsNameAndID[0] != "SETMAS")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "A new set can only be added under a node.";
                return;
            }

            if (Session["VNodeSetMas"] != null)
            {
                Session.Remove("VNodeSetMas");
            }

            Node parentNode = null;

            if (_clsNameAndID[0] == "NOD")
            {
                parentNode = Node.GetNode(Int32.Parse(_clsNameAndID[1]));

                if (parentNode.IsLastLevel == true)
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Adding a set under this node is not permitted, only course can be added under this node.";
                    return;
                }

                TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                if (TreeDetail.IsExist(parentNode.Id, treeMaster.Id))
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Adding a set under this node is not permitted, only nodes can be added under this node.";
                    return;
                }
            }
            else if (_clsNameAndID[0] == "SETMAS")
            {
                string[] setNoAndNodeID = new string[3];
                setNoAndNodeID = _clsNameAndID[1].Split('#');
                VNodeSetMaster vNodeSetMas = VNodeSetMaster.Get(Convert.ToInt32(setNoAndNodeID[2]));

                if (Session["VNodeSetMas"] != null)
                {
                    Session.Remove("VNodeSetMas");
                }
                Session.Add("VNodeSetMas", vNodeSetMas);

                parentNode = Node.GetNode(vNodeSetMas.OwnerNodeID);
            }

            if (Session["ParentNode"] != null)
            {
                Session.Remove("ParentNode");
            }
            Session.Add("ParentNode", parentNode);

            ControlEnablerSet();

            lblCaption.Text = string.Empty;

            lblCaption.ForeColor = Color.DarkTurquoise;
            lblCaption.BackColor = Color.Azure;

            lblCaption.Text = "Add Virtual Node Set";

            _isAddingSet = true;
            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            Session.Add("IsAddingSet", _isAddingSet);

            if (_clsNameAndID[0] == "NOD")
            {
                txtVNodeSet.ReadOnly = false;
                spnRequUnitsSet.Visible = true;
                txtVNodeSet.Focus();
            }
            else if (_clsNameAndID[0] == "SETMAS")
            {
                string[] setNoAndNodeID = new string[3];
                setNoAndNodeID = _clsNameAndID[1].Split('#');
                txtVNodeSet.Text = setNoAndNodeID[0];
                txtVNodeSet.ReadOnly = true;
                spnRequUnitsSet.Visible = false;
                rbtSetCourse.Focus();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butAddCourse_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (tvwMaster.SelectedNode == null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Before trying to add a course please select a root or parent node.";
                return;
            }

            if (IsUnderAVirtualNode(tvwMaster.SelectedNode))
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding course to a node from under a set is not permitted, if you want to add a course please add it from actual location.";
                return;
            }

            _clsNameAndID = tvwMaster.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] == "CRS")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Adding course under another course is not permitted.";
                return;
            }
            else
            {
                foreach (TreeNode childNode in tvwMaster.SelectedNode.ChildNodes)
                {
                    if (childNode.Value.Substring(0, 3).Trim() == "NOD")
                    {
                        lblMsg.Text = string.Empty;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Text = "Adding course under a node, which already has other nodes as it's child, is not permitted.";
                        return;
                    }
                }
            }

            if (_clsNameAndID[0] != "NOD")
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "A course can only be added under a node.";
                return;
            }

            Node parentNode = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
            if (parentNode.IsVirtual == true)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "This is a virtual node, to link courses to it try adding sets.";
                return;
            }

            if (parentNode.IsVirtual == true)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "This is a virtual node, to link nodes/courses to it try adding sets.";
                return;
            }
            if (Session["ParentNode"] != null)
            {
                Session.Remove("ParentNode");
            }
            Session.Add("ParentNode", parentNode);

            ControlEnablerCourse();

            lblCaption.Text = string.Empty;

            lblCaption.ForeColor = Color.DarkGreen;
            lblCaption.BackColor = Color.Honeydew;

            lblCaption.Text = "Add Course";

            _isAddingCourse = true;
            if (Session["Node_Course"] != null)
            {
                Session.Remove("Node_Course");
            }
            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            Session.Add("IsAddingCourse", _isAddingCourse);

            RemoveFromSession(SESSIONPREREQMASTERS);
            InitializePreReqArea();

            modalPopupCourseList.Show();
            LoadProgram();
            ddlProgram_SelectedIndexChanged(null, null);
            //ctlCourseSelect.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (tvwMaster.SelectedNode == null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Please select the desired node first then click the edit button.";
                return;
            }

            if (tvwMaster.SelectedNode.Parent != null)
            {
                string[] parentCodeAndID = new string[2];
                parentCodeAndID = tvwMaster.SelectedNode.Parent.Value.Split(',');

                if (parentCodeAndID[0] == "VNODSET")
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Editing course/node that from under a set is not permitted, if you want to edit a course/node please delete it from actual location.";
                    return;
                }
                else if (IsUnderAVirtualNode(tvwMaster.SelectedNode.Parent))
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Editing course/node that from under a set is not permitted, if you want to edit a course/node please delete it from actual location.";
                    return;
                }
            }

            _clsNameAndID = tvwMaster.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] == "CRS")
            {
                #region Course
                string[] childCodeAndID = new string[2];
                childCodeAndID = _clsNameAndID[1].Split('#');

                int courseID = Int32.Parse(childCodeAndID[0]);
                int versionID = Int32.Parse(childCodeAndID[1]);

                string[] parentCodeAndID = new string[2];
                parentCodeAndID = tvwMaster.SelectedNode.Parent.Value.Split(',');

                int parentNodeID = Int32.Parse(parentCodeAndID[1]);

                Node parentNode = Node.GetNode(parentNodeID);
                if (Session["ParentNode"] != null)
                {
                    Session.Remove("ParentNode");
                }
                Session.Add("ParentNode", parentNode);

                NodeCourse nodeCourse = NodeCourse.GetByParentNode(parentNodeID, courseID, versionID);
                if (Session["Node_Course"] != null)
                {
                    Session.Remove("Node_Course");
                }
                Session.Add("Node_Course", nodeCourse);


                ControlEnablerCourse();
                FillNodeCourseCtl(nodeCourse);

                lblCaption.Text = string.Empty;

                lblCaption.ForeColor = Color.DarkGreen;
                lblCaption.BackColor = Color.Honeydew;

                lblCaption.Text = "Edit Course";

                _isAddingCourse = true;

                if (Session["IsAddingSet"] != null)
                {
                    Session.Remove("IsAddingSet");
                }
                if (Session["IsAddingCourse"] != null)
                {
                    Session.Remove("IsAddingCourse");
                }
                if (Session["IsAddingRoot"] != null)
                {
                    Session.Remove("IsAddingRoot");
                }
                if (Session["IsAddingNode"] != null)
                {
                    Session.Remove("IsAddingNode");
                }
                Session.Add("IsAddingCourse", _isAddingCourse);
                #endregion
                ctlCourseSelect.Focus();
                RefreshPreReqArea();
            }
            else if (_clsNameAndID[0] == "NOD")
            {
                if (tvwMaster.SelectedNode.Parent != null)
                {
                    #region TreeDetail
                    //TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    string[] parentCodeAndID = new string[2];
                    parentCodeAndID = tvwMaster.SelectedNode.Parent.Value.Split(',');
                    int parentNodeID = Int32.Parse(parentCodeAndID[1]);
                    Node parentNode = Node.GetNode(parentNodeID);
                    if (Session["ParentNode"] != null)
                    {
                        Session.Remove("ParentNode");
                    }
                    Session.Add("ParentNode", parentNode);

                    Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
                    if (Session["CurrentNode"] != null)
                    {
                        Session.Remove("CurrentNode");
                    }
                    Session.Add("CurrentNode", node);

                    ControlEnablerNode();
                    FillNodeCtl(node);

                    lblCaption.Text = string.Empty;

                    lblCaption.ForeColor = Color.Crimson;
                    lblCaption.BackColor = Color.MistyRose;

                    lblCaption.Text = "Edit Node";

                    _isAddingNode = true;

                    if (Session["IsAddingSet"] != null)
                    {
                        Session.Remove("IsAddingSet");
                    }
                    if (Session["IsAddingCourse"] != null)
                    {
                        Session.Remove("IsAddingCourse");
                    }
                    if (Session["IsAddingRoot"] != null)
                    {
                        Session.Remove("IsAddingRoot");
                    }
                    if (Session["IsAddingNode"] != null)
                    {
                        Session.Remove("IsAddingNode");
                    }
                    Session.Add("IsAddingNode", _isAddingNode);
                    #endregion
                }
                else
                {
                    #region TreeMaster
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    string childNodeIDs = TreeDetail.GetChildNodeIDByTreeMasterID(treeMaster.Id);

                    Node node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
                    if (Session["CurrentNode"] != null)
                    {
                        Session.Remove("CurrentNode");
                    }
                    Session.Add("CurrentNode", node);

                    ControlEnablerRoot();
                    FillRootCtl(node, treeMaster);

                    lblCaption.Text = string.Empty;

                    lblCaption.ForeColor = Color.SteelBlue;
                    lblCaption.BackColor = Color.AliceBlue;

                    lblCaption.Text = "Edit Root";


                    _isAddingRoot = true;
                    if (Session["IsAddingSet"] != null)
                    {
                        Session.Remove("IsAddingSet");
                    }
                    if (Session["IsAddingCourse"] != null)
                    {
                        Session.Remove("IsAddingCourse");
                    }
                    if (Session["IsAddingRoot"] != null)
                    {
                        Session.Remove("IsAddingRoot");
                    }
                    if (Session["IsAddingNode"] != null)
                    {
                        Session.Remove("IsAddingNode");
                    }
                    Session.Add("IsAddingRoot", _isAddingRoot);
                    #endregion
                }

                RefreshPreReqArea();
                txtName.Focus();
            }
            else if (_clsNameAndID[0] == "SETMAS")
            {
                #region SetMaster
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.SteelBlue;
                lblMsg.Text = "Set is not Editable";
                #endregion
            }
            else if (_clsNameAndID[0] == "VNODSET")
            {
                #region VNodeSet

                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "The set element is not editable.";
                #endregion
            }

        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (tvwMaster.SelectedNode == null)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "Before trying to delete a node/course please select the node/course.";
                return;
            }

            if (tvwMaster.SelectedNode.Parent != null)
            {
                string[] parentCodeAndID = new string[2];
                parentCodeAndID = tvwMaster.SelectedNode.Parent.Value.Split(',');

                if (parentCodeAndID[0] == "VNODSET")
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Deleting course/node that from under a set is not permitted, if you want to delete a course/node please delete it from actual location.";
                    return;
                }
                else if (IsUnderAVirtualNode(tvwMaster.SelectedNode.Parent))
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Deleting course/node that from under a set is not permitted, if you want to delete a course/node please delete it from actual location.";
                    return;
                }
            }

            _clsNameAndID = tvwMaster.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] == "CRS")
            {
                #region Course
                string[] childCodeAndID = new string[2];
                childCodeAndID = _clsNameAndID[1].Split('#');

                int courseID = Int32.Parse(childCodeAndID[0]);
                int versionID = Int32.Parse(childCodeAndID[1]);

                string[] parentCodeAndID = new string[2];
                parentCodeAndID = tvwMaster.SelectedNode.Parent.Value.Split(',');

                int parentNodeID = Int32.Parse(parentCodeAndID[1]);

                Node parentNode = Node.GetNode(parentNodeID);

                if (parentNode.Node_Courses.Count == 1)
                {
                    NodeCourse.DeleteNode_CourseWithParentDeflag(parentNodeID, courseID, versionID);
                }
                else
                {
                    NodeCourse.DeleteNode_Course(parentNodeID, courseID, versionID);
                }

                LoadChildrens(tvwMaster.SelectedNode.Parent);

                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.SteelBlue;
                lblMsg.Text = "Link with node and course has been deleted.";
                #endregion

                #region Log Insert
                LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(courseID, versionID);
                //LogGeneralManager.Insert(
                //        DateTime.Now,
                //        BaseAcaCalCurrent.Code,
                //        BaseAcaCalCurrent.FullCode,
                //        BaseCurrentUserObj.LogInID,
                //        "",
                //        "",
                //        "Link with node and course delete",
                //        BaseCurrentUserObj.LogInID + " deleted link with node " + parentNode.Name + " and course " + courseObj.Title,
                //        "normal",
                //        _pageId,
                //        _pageName,
                //        _pageUrl,
                //        "");

                #endregion
            }
            else if (_clsNameAndID[0] == "NOD")
            {
                if (tvwMaster.SelectedNode.Parent != null)
                {
                    #region TreeDetail
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    TreeDetail.DeleteTreeDetail(treeMaster.Id, Int32.Parse(_clsNameAndID[1]));

                    LoadChildrens(tvwMaster.SelectedNode.Parent);

                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.SteelBlue;
                    lblMsg.Text = "Node and the underlying node and all of their child has been deleted";
                    #endregion
                    #region Log Insert

                    //LogGeneralManager.Insert(
                    //        DateTime.Now,
                    //        BaseAcaCalCurrent.Code,
                    //        BaseAcaCalCurrent.FullCode,
                    //        BaseCurrentUserObj.LogInID,
                    //        "",
                    //        "",
                    //        "Delete TreeDetail",
                    //        BaseCurrentUserObj.LogInID + " deleted TreeDetail and the underlying node and all of their child",
                    //        "normal",
                    //        _pageId,
                    //        _pageName,
                    //        _pageUrl,
                    //        "");

                    #endregion
                }
                else
                {
                    #region TreeMaster
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    string childNodeIDs = TreeDetail.GetChildNodeIDByTreeMasterID(treeMaster.Id);

                    TreeMaster.DeleteTreeMaster(treeMaster.Id, childNodeIDs, treeMaster.RootNodeID);
                    tvwMaster.Nodes.Clear();
                    FillTreeCombo();

                    lblMsg.Text = string.Empty;
                    lblMsg.ForeColor = Color.SteelBlue;
                    lblMsg.Text = "TreeMaster and the underlying TreeDtails and all of their children has been deleted.";

                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    #endregion
                }
            }
            else if (_clsNameAndID[0] == "SETMAS")
            {
                #region SetMaster
                string[] setNoAndNodeID = new string[2];
                setNoAndNodeID = _clsNameAndID[1].Split('#');

                int setNo = Int32.Parse(setNoAndNodeID[0]);
                int ownerNodeID = Int32.Parse(setNoAndNodeID[1]);

                Node ownerNode = Node.GetNode(ownerNodeID);

                if (ownerNode.VNodeSetMasters != null)
                {
                    if (ownerNode.VNodeSetMasters.Count == 1)
                    {
                        VNodeSetMaster.DeleteVNodeSetMasWithDeflag(ownerNodeID, setNo);
                    }
                    else
                    {
                        VNodeSetMaster.DeleteVNodeSetMas(ownerNodeID, setNo);
                    }
                }

                LoadChildrens(tvwMaster.SelectedNode.Parent);

                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.SteelBlue;
                lblMsg.Text = "The entire set has been deleted.";
                #endregion
            }
            else if (_clsNameAndID[0] == "VNODSET")
            {
                #region VNodeSet
                VNodeSet vNodeSet = VNodeSet.Get(Int32.Parse(_clsNameAndID[1]));

                if (vNodeSet.OwnerNode.VNodeSets.Count == 1)
                {
                    VNodeSet.DeleteVNodeWithDeflag(vNodeSet.OwnerNode.Id, vNodeSet.SetNo);
                    LoadChildrens(tvwMaster.SelectedNode.Parent.Parent);
                }
                else
                {
                    VNodeSet.DeleteVNode(Int32.Parse(_clsNameAndID[1]));
                    LoadChildrens(tvwMaster.SelectedNode.Parent.Parent);
                }

                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "The set element has been deleted.";
                #endregion
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                lblMsg.Text = "This element has been referenced in other tables, please delete those references first.";
            }
            else
            {
                lblMsg.Text = SqlEx.Message;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void butSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            ClearMessagelbl();
            if (gdvPreReq.IsEditing)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Saving is not allowed when prerequisit grid is in edit mode.");
                return;
            }


            if (Session["IsAddingRoot"] != null)
            {
                #region Root
                if (Convert.ToBoolean(Session["IsAddingRoot"]))
                {
                    if (ValidateNode())
                    {
                        if (Session["CurrentNode"] == null)
                        {
                            TreeMaster treeMaster = RefreshTreeMas();
                            treeMaster.CreatorID = CurrentUser.Id;
                            treeMaster.CreatedDate = DateTime.Now;

                            Node rootNode = RefreshRootNode();
                            rootNode.CreatorID = CurrentUser.Id;
                            rootNode.CreatedDate = DateTime.Now;

                            TreeMaster.SaveTreeMasterWithRootNode(rootNode, treeMaster);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Root Saved";
                        }
                        else
                        {
                            TreeMaster treeMaster = RefreshTreeMas();
                            treeMaster.ModifierID = CurrentUser.Id;
                            treeMaster.ModifiedDate = DateTime.Now;

                            Node rootNode = RefreshRootNode();
                            rootNode.ModifierID = CurrentUser.Id;
                            rootNode.ModifiedDate = DateTime.Now;

                            TreeMaster.SaveTreeMasterWithRootNode(rootNode, treeMaster);

                            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));

                            treeNode.Text = rootNode.Name;
                            treeNode.Value = "NOD," + rootNode.Id.ToString();
                            treeNode.ExpandAll();

                            ClearControl();

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Node Updated";
                        }

                        FillTreeCombo();
                        ShowRoot();
                        ClearControl();

                        if (Session["CurrentNode"] != null)
                        {
                            Session.Remove("CurrentNode");
                        }

                        ddlTree.Focus();
                    }
                }
                #endregion
            }
            else if (Session["IsAddingNode"] != null)
            {
                #region Node
                if (Convert.ToBoolean(Session["IsAddingNode"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateNode())
                        {
                            if (Session["CurrentNode"] == null)
                            {
                                TreeDetail treeDetail = RefreshTreeDet();
                                treeDetail.CreatorID = CurrentUser.Id;
                                treeDetail.CreatedDate = DateTime.Now;

                                Node childNode = RefreshChildNode();
                                childNode.CreatorID = CurrentUser.Id;
                                childNode.CreatedDate = DateTime.Now;

                                TreeDetail.SaveTreeDetailWithChildNode(childNode, treeDetail);

                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Node Saved";
                            }
                            else
                            {
                                Node childNode = RefreshChildNode();
                                childNode.ModifierID = CurrentUser.Id;
                                childNode.ModifiedDate = DateTime.Now;

                                Node.SaveNode(childNode);

                                TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));

                                treeNode.Text = childNode.Name;
                                treeNode.Value = "NOD," + childNode.Id.ToString();
                                treeNode.ExpandAll();

                                ClearControl();

                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Node Updated";
                            }

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();

                            if (Session["CurrentNode"] != null)
                            {
                                Session.Remove("CurrentNode");
                            }
                            pnlPREREQ.Visible = false;
                            RemoveFromSession(SESSIONPREREQMASTERS);
                            RemoveFromSession(SESSIONPREREQMASTER);
                            RemoveFromSession(SESSIONPREREQNODE);
                            RemoveFromSession(SESSIONPREREQCOURSE);
                            tvwMaster.Focus();
                        }
                    }
                }
                #endregion
            }
            else if (Session["IsAddingSet"] != null)
            {
                #region Set
                if (Convert.ToBoolean(Session["IsAddingSet"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null && Session["VNodeSetMas"] != null)
                    {
                        if (ValidateVNodeSet())
                        {
                            VNodeSet vNodeSet = RefreshVNodeSet();
                            vNodeSet.CreatorID = CurrentUser.Id;
                            vNodeSet.CreatedDate = DateTime.Now;

                            VNodeSet.SaveVNodeSet(vNodeSet);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Virtual Node Set Saved";

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();
                            tvwMaster.Focus();
                        }
                    }
                    else if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateVNodeSet())
                        {
                            VNodeSetMaster vNodeSetMas = RefreshVNodeSetMas();

                            vNodeSetMas.CreatorID = CurrentUser.Id;
                            vNodeSetMas.CreatedDate = DateTime.Now;

                            VNodeSetMaster.Insert(vNodeSetMas);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Virtual Node Set Saved";

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();

                            tvwMaster.Focus();
                        }
                    }
                }
                #endregion
            }
            else if (Session["IsAddingCourse"] != null)
            {
                #region Course
                if (Convert.ToBoolean(Session["IsAddingCourse"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateCourse())
                        {
                            NodeCourse nodeCourse = RefreshNodeCourse();
                            nodeCourse.CreatorID = CurrentUser.Id;
                            nodeCourse.CreatedDate = DateTime.Now;

                            NodeCourse.SaveNode_Course(nodeCourse);

                            if (Session["Node_Course"] == null)
                            {
                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Link with node and course has been saved.";

                                TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
                                _clsNameAndID = treeNode.Value.Split(',');
                                if (_clsNameAndID[0] == "CRS")
                                {

                                    treeNode.Text = nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
                                    treeNode.Value = "CRS," + nodeCourse.ChildCourseID.ToString() + "#" + nodeCourse.ChildVersionID.ToString();
                                    treeNode.ExpandAll();
                                }
                                LoadChildrens(treeNode);
                            }
                            else
                            {
                                ClearControl();
                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Link with node and course has been modified.";

                                TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"])).Parent;
                                LoadChildrens(treeNode);
                            }



                            //TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
                            //_clsNameAndID = treeNode.Value.Split(',');
                            //if (_clsNameAndID[0] == "CRS")
                            //{

                            //    treeNode.Text = nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
                            //    treeNode.Value = "CRS," + nodeCourse.ChildCourseID.ToString() + "#" + nodeCourse.ChildVersionID.ToString();
                            //    treeNode.ExpandAll();
                            //}


                            //treeNode.ExpandAll();
                            ClearControl();
                            pnlPREREQ.Visible = false;
                            RemoveFromSession(SESSIONPREREQMASTERS);
                            RemoveFromSession(SESSIONPREREQMASTER);
                            RemoveFromSession(SESSIONPREREQNODE);
                            RemoveFromSession(SESSIONPREREQCOURSE);

                            if (Session["Node_Course"] != null)
                            {
                                Session.Remove("Node_Course");
                            }
                            tvwMaster.Focus();
                        }
                    }
                }
                #endregion
            }
            Page_Load(null, null);
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();
            ClearControl();
            pnlPREREQ.Visible = false;
            RemoveFromSession(SESSIONPREREQMASTERS);
            RemoveFromSession(SESSIONPREREQMASTER);
            RemoveFromSession(SESSIONPREREQNODE);
            RemoveFromSession(SESSIONPREREQCOURSE);
            if (ddlTree.Enabled && ddlTree.SelectedIndex == 0)
            {
                ddlTree.Focus();
            }
            else
            {
                tvwMaster.Focus();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (Session["TreeMaster"] != null)
            {
                Session.Remove("TreeMaster");
            }
            if (Session["TreeMasters"] != null)
            {
                Session.Remove("TreeMasters");
            }

            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            if (Session["ParentNode"] != null)
            {
                Session.Remove("ParentNode");
            }
            if (Session["SelectedNode"] != null)
            {
                Session.Remove("SelectedNode");
            }
            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["Node_Course"] != null)
            {
                Session.Remove("Node_Course");
            }

            tvwMaster.Nodes.Clear();
            FillTreeCombo();
            ddlTree_SelectedIndexChanged(null, null);
            //ShowRoot();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void tvwMaster_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwMaster.SelectedNode != null)
            {
                if (Session["SelectedNode"] != null)
                {
                    Session.Remove("SelectedNode");
                }
                Session.Add("SelectedNode", tvwMaster.SelectedNode.ValuePath);

                LoadChildrens(tvwMaster.SelectedNode);
                tvwMaster.Focus();
                //tvwMaster.SelectedNode.Expand();

            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void rbtSetCourse_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (rbtSetCourse.Checked)
            {
                rbtSetNode.Checked = false;
                ddlSetNode.Visible = false;
                //lblMakSetOpNod.Visible = false;
                lblOpNode.Visible = false;

                rbtSetCourse.Checked = true;
                ddlSetCrs.Visible = true;
                //lblMakSetOpCrs.Visible = true;
                lblOpCour.Visible = true;
            }
            else
            {
                rbtSetNode.Checked = true;
                ddlSetNode.Visible = true;
                //lblMakSetOpNod.Visible = true;
                lblOpNode.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }
    protected void rbtSetNode_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (rbtSetNode.Checked)
            {
                rbtSetNode.Checked = true;
                ddlSetNode.Visible = true;
                //lblMakSetOpNod.Visible = true;
                lblOpNode.Visible = true;

                rbtSetCourse.Checked = false;
                ddlSetCrs.Visible = false;
                //lblMakSetOpCrs.Visible = false;
                lblOpCour.Visible = false;
                FillNodesCombo();
            }
            else
            {
                rbtSetCourse.Checked = true;
                ddlSetCrs.Visible = true;
                //lblMakSetOpCrs.Visible = true;
                lblOpCour.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    protected void ddlTree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingSet"] != null)
            {
                Session.Remove("IsAddingSet");
            }
            if (Session["IsAddingCourse"] != null)
            {
                Session.Remove("IsAddingCourse");
            }
            if (Session["IsAddingRoot"] != null)
            {
                Session.Remove("IsAddingRoot");
            }
            if (Session["IsAddingNode"] != null)
            {
                Session.Remove("IsAddingNode");
            }
            if (Session["ParentNode"] != null)
            {
                Session.Remove("ParentNode");
            }
            if (Session["SelectedNode"] != null)
            {
                Session.Remove("SelectedNode");
            }
            if (Session["CurrentNode"] != null)
            {
                Session.Remove("CurrentNode");
            }
            if (Session["Node_Course"] != null)
            {
                Session.Remove("Node_Course");
            }

            ClearMessagelbl();
            tvwMaster.Nodes.Clear();

            if (Session["TreeMaster"] != null)
            {
                Session.Remove("TreeMaster");
            }

            //FillTreeCombo();
            ShowRoot();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }

    #region old
    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    if (Session["Programs"] != null)
    //    {
    //        Session.Remove("Programs");
    //    }
    //    if (Session["TreeMaster"] != null)
    //    {
    //        Session.Remove("TreeMaster");
    //    }
    //    if (Session["TreeMasters"] != null)
    //    {
    //        Session.Remove("TreeMasters");
    //    }
    //    if (Session["Courses"] != null)
    //    {
    //        Session.Remove("Courses");
    //    }
    //    if (Session["Nodes"] != null)
    //    {
    //        Session.Remove("Nodes");
    //    }
    //    if (Session["Operators"] != null)
    //    {
    //        Session.Remove("Operators");
    //    }
    //    if (Session["IsAddingSet"] != null)
    //    {
    //        Session.Remove("IsAddingSet");
    //    }
    //    if (Session["IsAddingCourse"] != null)
    //    {
    //        Session.Remove("IsAddingCourse");
    //    }
    //    if (Session["IsAddingRoot"] != null)
    //    {
    //        Session.Remove("IsAddingRoot");
    //    }
    //    if (Session["IsAddingNode"] != null)
    //    {
    //        Session.Remove("IsAddingNode");
    //    }
    //    if (Session["ParentNode"] != null)
    //    {
    //        Session.Remove("ParentNode");
    //    }
    //    if (Session["SelectedNode"] != null)
    //    {
    //        Session.Remove("SelectedNode");
    //    }
    //} 
    #endregion

    protected void chkStudSpec_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStudSpec.Checked)
        {
            rbtSetNode.Visible = false;
            ddlSetNode.Visible = false;
            lblOpNode.Visible = false;

            rbtSetCourse.Visible = false;
            ddlSetCrs.Visible = false;
            lblOpCour.Visible = false;
        }
        else
        {
            rbtSetNode.Visible = true;
            rbtSetCourse.Visible = true;
            rbtSetCourse.Checked = true;
            ddlSetCrs.Visible = true;
            //lblMakSetOpCrs.Visible = true;
            lblOpCour.Visible = true;
        }
    }

    //protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlCourses.SelectedIndex >= 0)
    //        {
    //            ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        lblMsg.Text = string.Empty;
    //        lblMsg.ForeColor = Color.Red;
    //        lblMsg.Text = Ex.Message;
    //    }
    //}
    protected void ddlSetCrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSetCrs.SelectedIndex >= 0)
            {
                ddlSetCrs.ToolTip = ddlSetCrs.SelectedItem.Text;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
        }
    }


    #region Prerequisit Setting
    protected void btnPreReqCourse_Click(object sender, EventArgs e)
    {
        if (txtPreReqName.Text == string.Empty)
        {
            return;
        }
        lblOccurance.Text = strPreReqCourseOccurance;
        pnlPreReqArea.Visible = true;
        if (Session[SESSIONPREREQCOURSE] == null)
        {
            gdvPreReq.DataSource = new List<PreReqDetail>();
        }
        else
        {
            gdvPreReq.DataSource = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
            foreach (PreReqDetail prd in (List<PreReqDetail>)Session[SESSIONPREREQCOURSE])
            {
                if (prd.OperatorIDMinOccurance.ToString() != "0")
                {
                    speOperatorcourseOccurance.Text = prd.OperatorIDMinOccurance.ToString();
                    break;
                }
            }
        }
        gdvPreReq.DataBind();

        GridViewColumn colCourse = (GridViewColumn)gdvPreReq.Columns[1];
        GridViewColumn colNode = (GridViewColumn)gdvPreReq.Columns[2];

        colCourse.Visible = true;
        colNode.Visible = false;

        speOperatorcourseOccurance.Visible = true;
        speOperatorNodeOccurance.Visible = false;
    }
    protected void btnPreReqNode_Click(object sender, EventArgs e)
    {
        if (txtPreReqName.Text == string.Empty)
        {
            return;
        }
        lblOccurance.Text = strPreReqNodeOccurance;
        pnlPreReqArea.Visible = true;
        if (Session[SESSIONPREREQNODE] == null)
        {
            gdvPreReq.DataSource = new List<PreReqDetail>();
        }
        else
        {
            gdvPreReq.DataSource = (List<PreReqDetail>)Session[SESSIONPREREQNODE];
            foreach (PreReqDetail prd in (List<PreReqDetail>)Session[SESSIONPREREQNODE])
            {
                if (prd.OperatorIDMinOccurance.ToString() != "0")
                {
                    speOperatorNodeOccurance.Text = prd.OperatorIDMinOccurance.ToString();
                    break;
                }
            }
        }
        gdvPreReq.DataBind();
        GridViewColumn colCourse = (GridViewColumn)gdvPreReq.Columns[1];
        GridViewColumn colNode = (GridViewColumn)gdvPreReq.Columns[2];
        colCourse.Visible = false;
        colNode.Visible = true;
        speOperatorcourseOccurance.Visible = false;
        speOperatorNodeOccurance.Visible = true;
    }
    #endregion


    protected void gdvPreReq_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Caption == "Course")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridCourseCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Course_OnCallback);
        }
        if (e.Column.Caption == "Node")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridNodeCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Node_OnCallback);
        }

        if (e.Column.Caption == "Operator")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridOperatorCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Operator_OnCallback);
        }
    }
    private void Course_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridCourseCombo(source as ASPxComboBox);
    }
    private void Node_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridNodeCombo(source as ASPxComboBox);
    }
    private void Operator_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridOperatorCombo(source as ASPxComboBox);
    }

    protected void gdvPreReq_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        UpdateData(e.NewValues, 0);
        e.Cancel = true;
        gdvPreReq.CancelEdit();
    }

    protected void gdvPreReq_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int detailID = 0;
        if (e.Keys != null)
        {
            detailID = Convert.ToInt32(e.Keys[0].ToString());
        }
        else
        {
            detailID = 0;
        }
        UpdateData(e.NewValues, detailID);
        e.Cancel = true;
        gdvPreReq.CancelEdit();
    }

    protected void gdvPreReq_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        PreReqButtonState(true);
    }
    protected void gdvPreReq_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        PreReqButtonState(true);
    }
    protected void gdvPreReq_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        PreReqButtonState(false);
    }
    protected void gdvPreReq_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        PreReqButtonState(false);
    }
    protected void gdvPreReq_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        PreReqButtonState(true);
    }
    protected void gdvPreReq_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (lblOccurance.Text == strPreReqCourseOccurance)
        {
            if (e.NewValues["NodeCourse.ChildCourse.FullCodeAndCourse"] != null)
            {
                int intID = Convert.ToInt32(e.NewValues["NodeCourse.ChildCourse.FullCodeAndCourse"].ToString());
                decimal dcReqUnits = 0;
                if (e.NewValues["ReqCredits"] != null)
                {
                    dcReqUnits = Convert.ToDecimal(e.NewValues["ReqCredits"].ToString());
                }
                int intOpID = 0;
                if (e.NewValues["Operator.Name"] != null && e.NewValues["Operator.Name"].ToString() != string.Empty)
                {
                    intOpID = Convert.ToInt32(e.NewValues["Operator.Name"].ToString());
                }
                if (Session[SESSIONPREREQCOURSE] != null)
                {
                    _preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
                    if (_preReqDetails_Course.Count != 0)
                    {
                        foreach (PreReqDetail ec in _preReqDetails_Course)
                        {
                            if (ec.PreReqNodeCourseID == intID && ec.ReqCredits == dcReqUnits && ec.OperatorID == intOpID)
                            {
                                e.RowError = Common.Message.DUPLICATEMESSAGE;
                            }
                        }
                    }
                }
            }
            else
            {
                e.RowError = "A course must be selected";
            }
        }
        else if (lblOccurance.Text == strPreReqNodeOccurance)
        {
            if (e.NewValues["Node.Name"] != null)
            {
                int intID = Convert.ToInt32(e.NewValues["Node.Name"].ToString());
                decimal dcReqUnits = 0;
                if (e.NewValues["ReqCredits"] != null)
                {
                    dcReqUnits = Convert.ToDecimal(e.NewValues["ReqCredits"].ToString());
                }
                int intOpID = 0;
                if (e.NewValues["Operator.Name"] != null && e.NewValues["Operator.Name"].ToString() != string.Empty)
                {
                    intOpID = Convert.ToInt32(e.NewValues["Operator.Name"].ToString());
                }
                if (Session[SESSIONPREREQNODE] != null)
                {
                    _preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];
                    if (_preReqDetails_Node.Count != 0)
                    {
                        foreach (PreReqDetail ec in _preReqDetails_Node)
                        {
                            if (ec.PreReqNodeID == intID && ec.ReqCredits == dcReqUnits && ec.OperatorID == intOpID)
                            {
                                e.RowError = Common.Message.DUPLICATEMESSAGE;
                            }
                        }
                    }
                }
            }
            else
            {
                e.RowError = "A node must be selected";
            }
        }
    }
    protected void gdvPreReq_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            if (lblOccurance.Text == strPreReqCourseOccurance)
            {
                _preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
                e.Cancel = true;
                gdvPreReq.CancelEdit();

                int detailID = 0;
                if (e.Keys != null)
                {
                    detailID = Convert.ToInt32(e.Keys[0].ToString());
                }
                else
                {
                    detailID = 0;
                }

                //int intID = Convert.ToInt32(e.Values["NodeCourse.ChildCourse.FullCodeAndCourse"].ToString());
                int intIndex = 0;
                foreach (PreReqDetail ec in _preReqDetails_Course)
                {

                    if (ec.Id == detailID)
                    {
                        break;
                    }
                    intIndex++;
                }

                _preReqDetails_Course.RemoveAt(intIndex);
                BindGrid(SESSIONPREREQCOURSE, _preReqDetails_Course);
            }
            else if (lblOccurance.Text == strPreReqNodeOccurance)
            {
                _preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];
                e.Cancel = true;
                gdvPreReq.CancelEdit();

                int detailID = 0;
                if (e.Keys != null)
                {
                    detailID = Convert.ToInt32(e.Keys[0].ToString());
                }
                else
                {
                    detailID = 0;
                }

                //int intID = Convert.ToInt32(e.Values["Node.Name"].ToString());
                int intIndex = 0;
                foreach (PreReqDetail ec in _preReqDetails_Node)
                {

                    if (ec.PreReqNodeID == detailID)
                    {
                        break;
                    }
                    intIndex++;
                }
                _preReqDetails_Node.RemoveAt(intIndex);
                BindGrid(SESSIONPREREQNODE, _preReqDetails_Node);
            }

            //_preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
            //_preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];

            if ((_preReqDetails_Node == null || _preReqDetails_Node.Count == 0) && (_preReqDetails_Course == null || _preReqDetails_Course.Count == 0))
            {
                if (IsSessionVariableExists(SESSIONPREREQMASTERS))
                {
                    _preReqMasters = (List<PreRequisiteMaster>)GetFromSession(SESSIONPREREQMASTERS);
                }

                if (_preReqMasters != null)
                {

                    if (IsSessionVariableExists(SESSIONPREREQMASTER))
                    {
                        _preReqMaster = (PreRequisiteMaster)GetFromSession(SESSIONPREREQMASTER);
                    }
                    if (_preReqMaster != null)
                    {
                        if (cboPreReqName.SelectedItem.Value.ToString() != "0")
                        {
                            int index = 0;
                            foreach (PreRequisiteMaster item in _preReqMasters)
                            {
                                if (_preReqMaster.Id == item.Id)
                                {
                                    break;
                                }
                                index++;
                            }
                            _preReqMasters.RemoveAt(index);

                            if (_preReqMasters == null || _preReqMasters.Count == 0)
                            {
                                RemoveFromSession(SESSIONPREREQMASTERS);
                            }
                            else
                            {
                                RemoveFromSession(SESSIONPREREQMASTERS);
                                AddToSession(SESSIONPREREQMASTERS, _preReqMasters);
                            }
                            RemoveFromSession(SESSIONPREREQMASTER);
                        }
                        else
                        {
                            RemoveFromSession(SESSIONPREREQMASTER);
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }
    protected void gdvPreReq_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        try
        {
            PreReqButtonState(true);

            _preReqDetails_Course = (List<PreReqDetail>)Session[SESSIONPREREQCOURSE];
            _preReqDetails_Node = (List<PreReqDetail>)Session[SESSIONPREREQNODE];

            if ((_preReqDetails_Node == null || _preReqDetails_Node.Count == 0) && (_preReqDetails_Course == null || _preReqDetails_Course.Count == 0))
            {
                if (IsSessionVariableExists(SESSIONPREREQMASTERS))
                {
                    _preReqMasters = (List<PreRequisiteMaster>)GetFromSession(SESSIONPREREQMASTERS);
                }

                if (_preReqMasters != null)
                {

                    if (IsSessionVariableExists(SESSIONPREREQMASTER))
                    {
                        _preReqMaster = (PreRequisiteMaster)GetFromSession(SESSIONPREREQMASTER);
                    }
                    if (_preReqMaster != null)
                    {
                        if (cboPreReqName.SelectedItem.Value.ToString() != "0")
                        {
                            int index = 0;
                            foreach (PreRequisiteMaster item in _preReqMasters)
                            {
                                if (_preReqMaster.Id == item.Id)
                                {
                                    break;
                                }
                                index++;
                            }
                            _preReqMasters.RemoveAt(index);

                            if (_preReqMasters == null || _preReqMasters.Count == 0)
                            {
                                RemoveFromSession(SESSIONPREREQMASTERS);
                            }
                            else
                            {
                                RemoveFromSession(SESSIONPREREQMASTERS);
                                AddToSession(SESSIONPREREQMASTERS, _preReqMasters);
                            }
                            RemoveFromSession(SESSIONPREREQMASTER);
                        }
                        else
                        {
                            RemoveFromSession(SESSIONPREREQMASTER);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }


    protected void cboPreReqName_SelectedIndexChanged(object sender, EventArgs e)
    {
        gdvPreReq.DataSource = null;
        gdvPreReq.DataSource = new List<PreReqDetail>();
        if (cboPreReqName.SelectedItem.Text.Trim() != "(New Name)")
        {
            PreRequisiteMaster prereqMas = new PreRequisiteMaster();
            prereqMas = PreRequisiteMaster.Get(Convert.ToInt32(cboPreReqName.Value));
            if (IsSessionVariableExists(SESSIONPREREQMASTER))
            {
                RemoveFromSession(SESSIONPREREQMASTER);
            }
            AddToSession(SESSIONPREREQMASTER, prereqMas);

            if (prereqMas.PreReqDetails != null)
            {
                if (prereqMas.PreReqDetailCourses != null)
                {
                    if (IsSessionVariableExists(SESSIONPREREQCOURSE))
                    {
                        RemoveFromSession(SESSIONPREREQCOURSE);
                    }

                    AddToSession(SESSIONPREREQCOURSE, prereqMas.PreReqDetailCourses);
                }

                if (prereqMas.PreReqDetailNodes != null)
                {
                    if (IsSessionVariableExists(SESSIONPREREQNODE))
                    {
                        RemoveFromSession(SESSIONPREREQNODE);
                    }
                    AddToSession(SESSIONPREREQNODE, prereqMas.PreReqDetailNodes);
                }
            }

            txtPreReqName.Text = prereqMas.Name;
        }
        else
        {
            txtPreReqName.Text = string.Empty;
            RemoveFromSession(SESSIONPREREQMASTER);
            RemoveFromSession(SESSIONPREREQCOURSE);
            RemoveFromSession(SESSIONPREREQNODE);
        }
        pnlPreReqArea.Visible = false;
    }
    #endregion


    #region New Added For Bulk Course Add


    private void LoadProgram()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.AppendDataBoundItems = true;
            ddlProgram.Items.Add(new ListItem("All", "0"));

            var ProgramList =ProgramManager.GetAll();   

            if (ProgramList != null && ProgramList.Any())
            {
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataSource = ProgramList;
                ddlProgram.DataBind();

                ddlProgram.SelectedValue = ddlPrograms.SelectedValue.ToString();

            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        modalPopupCourseList.Show();
        ClearGrid();
        LoadCourseGrid();
    }

    private void ClearGrid()
    {
        try
        {
            gvCourseList.DataSource = null;
            gvCourseList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadCourseGrid()
    {
        try
        {
            List<LogicLayer.BusinessObjects.Course> CourseList = new List<LogicLayer.BusinessObjects.Course>();

            int ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);

            if (ProgramId == 0)
                CourseList = CourseManager.GetAll();
            else
                CourseList = CourseManager.GetAllByProgram(ProgramId);

            if (CourseList != null && CourseList.Any())
            {
                gvCourseList.DataSource = CourseList;
                gvCourseList.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        modalPopupCourseList.Hide();
        butCancel_Click(null, null);
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            ClearMessagelbl();
            if (gdvPreReq.IsEditing)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Saving is not allowed when prerequisit grid is in edit mode.");
                return;
            }


            if (Session["IsAddingRoot"] != null)
            {
                #region Root
                if (Convert.ToBoolean(Session["IsAddingRoot"]))
                {
                    if (ValidateNode())
                    {
                        if (Session["CurrentNode"] == null)
                        {
                            TreeMaster treeMaster = RefreshTreeMas();
                            treeMaster.CreatorID = CurrentUser.Id;
                            treeMaster.CreatedDate = DateTime.Now;

                            Node rootNode = RefreshRootNode();
                            rootNode.CreatorID = CurrentUser.Id;
                            rootNode.CreatedDate = DateTime.Now;

                            TreeMaster.SaveTreeMasterWithRootNode(rootNode, treeMaster);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Root Saved";
                        }
                        else
                        {
                            TreeMaster treeMaster = RefreshTreeMas();
                            treeMaster.ModifierID = CurrentUser.Id;
                            treeMaster.ModifiedDate = DateTime.Now;

                            Node rootNode = RefreshRootNode();
                            rootNode.ModifierID = CurrentUser.Id;
                            rootNode.ModifiedDate = DateTime.Now;

                            TreeMaster.SaveTreeMasterWithRootNode(rootNode, treeMaster);

                            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));

                            treeNode.Text = rootNode.Name;
                            treeNode.Value = "NOD," + rootNode.Id.ToString();
                            treeNode.ExpandAll();

                            ClearControl();

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Node Updated";
                        }

                        FillTreeCombo();
                        ShowRoot();
                        ClearControl();

                        if (Session["CurrentNode"] != null)
                        {
                            Session.Remove("CurrentNode");
                        }

                        ddlTree.Focus();
                    }
                }
                #endregion
            }
            else if (Session["IsAddingNode"] != null)
            {
                #region Node
                if (Convert.ToBoolean(Session["IsAddingNode"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateNode())
                        {
                            if (Session["CurrentNode"] == null)
                            {
                                TreeDetail treeDetail = RefreshTreeDet();
                                treeDetail.CreatorID = CurrentUser.Id;
                                treeDetail.CreatedDate = DateTime.Now;

                                Node childNode = RefreshChildNode();
                                childNode.CreatorID = CurrentUser.Id;
                                childNode.CreatedDate = DateTime.Now;

                                TreeDetail.SaveTreeDetailWithChildNode(childNode, treeDetail);

                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Node Saved";
                            }
                            else
                            {
                                Node childNode = RefreshChildNode();
                                childNode.ModifierID = CurrentUser.Id;
                                childNode.ModifiedDate = DateTime.Now;

                                Node.SaveNode(childNode);

                                TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));

                                treeNode.Text = childNode.Name;
                                treeNode.Value = "NOD," + childNode.Id.ToString();
                                treeNode.ExpandAll();

                                ClearControl();

                                lblMsg.Text = string.Empty;
                                lblMsg.ForeColor = Color.SteelBlue;
                                lblMsg.Text = "Node Updated";
                            }

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();

                            if (Session["CurrentNode"] != null)
                            {
                                Session.Remove("CurrentNode");
                            }
                            pnlPREREQ.Visible = false;
                            RemoveFromSession(SESSIONPREREQMASTERS);
                            RemoveFromSession(SESSIONPREREQMASTER);
                            RemoveFromSession(SESSIONPREREQNODE);
                            RemoveFromSession(SESSIONPREREQCOURSE);
                            tvwMaster.Focus();
                        }
                    }
                }
                #endregion
            }
            else if (Session["IsAddingSet"] != null)
            {
                #region Set
                if (Convert.ToBoolean(Session["IsAddingSet"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null && Session["VNodeSetMas"] != null)
                    {
                        if (ValidateVNodeSet())
                        {
                            VNodeSet vNodeSet = RefreshVNodeSet();
                            vNodeSet.CreatorID = CurrentUser.Id;
                            vNodeSet.CreatedDate = DateTime.Now;

                            VNodeSet.SaveVNodeSet(vNodeSet);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Virtual Node Set Saved";

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();
                            tvwMaster.Focus();
                        }
                    }
                    else if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateVNodeSet())
                        {
                            VNodeSetMaster vNodeSetMas = RefreshVNodeSetMas();

                            vNodeSetMas.CreatorID = CurrentUser.Id;
                            vNodeSetMas.CreatedDate = DateTime.Now;

                            VNodeSetMaster.Insert(vNodeSetMas);

                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Virtual Node Set Saved";

                            LoadChildrens(tvwMaster.FindNode(((string)Session["SelectedNode"])));
                            ClearControl();

                            tvwMaster.Focus();
                        }
                    }
                }
                #endregion
            }
            else if (Session["IsAddingCourse"] != null)
            {
                #region Course
                if (Convert.ToBoolean(Session["IsAddingCourse"]))
                {
                    if (Session["TreeMaster"] != null && Session["ParentNode"] != null && Session["SelectedNode"] != null)
                    {
                        //if (ValidateCourse())
                        //{
                        //NodeCourse nodeCourse = RefreshNodeCourse();
                        //nodeCourse.CreatorID = CurrentUser.Id;
                        //nodeCourse.CreatedDate = DateTime.Now;

                        //NodeCourse.SaveNode_Course(nodeCourse);

                        NodeCourse nodeCourse = null;
                        foreach (GridViewRow row in gvCourseList.Rows)
                        {
                            CheckBox ckBox = (CheckBox)row.FindControl("ChkChecked");
                            HiddenField hdnCourseID = (HiddenField)row.FindControl("hdnCourseID");
                            HiddenField hdnVersionID = (HiddenField)row.FindControl("hdnVersionID");

                            if (ckBox.Checked)
                            {

                                int courseId = Convert.ToInt32(hdnCourseID.Value);
                                int versionId = Convert.ToInt32(hdnVersionID.Value);


                                List<LogicLayer.BusinessObjects.Node_Course> list = Node_CourseManager.GetAll();
                                LogicLayer.BusinessObjects.Node_Course isNodeCourseHave = null;
                                if (list != null && list.Any())
                                    isNodeCourseHave = list.Where(x => x.NodeID == ((Node)Session["ParentNode"]).Id && x.CourseID == courseId && x.VersionID == versionId).FirstOrDefault();

                                if (isNodeCourseHave != null)
                                {
                                }
                                else
                                {
                                    nodeCourse = RefreshNodeCourse(courseId, versionId);
                                    nodeCourse.CreatorID = BaseCurrentUserObj.Id;
                                    nodeCourse.CreatedDate = DateTime.Now;

                                    NodeCourse.SaveNode_Course(nodeCourse);
                                }
                            }
                        }


                        if (Session["Node_Course"] == null)
                        {
                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Link with node and course has been saved.";

                            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
                            _clsNameAndID = treeNode.Value.Split(',');
                            if (_clsNameAndID[0] == "CRS")
                            {

                                treeNode.Text = nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
                                treeNode.Value = "CRS," + nodeCourse.ChildCourseID.ToString() + "#" + nodeCourse.ChildVersionID.ToString();
                                treeNode.ExpandAll();
                            }
                            LoadChildrens(treeNode);
                        }
                        else
                        {
                            ClearControl();
                            lblMsg.Text = string.Empty;
                            lblMsg.ForeColor = Color.SteelBlue;
                            lblMsg.Text = "Link with node and course has been modified.";

                            TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"])).Parent;
                            LoadChildrens(treeNode);
                        }



                        //TreeNode treeNode = tvwMaster.FindNode(((string)Session["SelectedNode"]));
                        //_clsNameAndID = treeNode.Value.Split(',');
                        //if (_clsNameAndID[0] == "CRS")
                        //{

                        //    treeNode.Text = nodeCourse.ChildCourse.VersionCode + "-" + nodeCourse.ChildCourse.Title;
                        //    treeNode.Value = "CRS," + nodeCourse.ChildCourseID.ToString() + "#" + nodeCourse.ChildVersionID.ToString();
                        //    treeNode.ExpandAll();
                        //}


                        //treeNode.ExpandAll();
                        ClearControl();
                        pnlPREREQ.Visible = false;
                        RemoveFromSession(SESSIONPREREQMASTERS);
                        RemoveFromSession(SESSIONPREREQMASTER);
                        RemoveFromSession(SESSIONPREREQNODE);
                        RemoveFromSession(SESSIONPREREQCOURSE);

                        if (Session["Node_Course"] != null)
                        {
                            Session.Remove("Node_Course");
                        }
                        tvwMaster.Focus();
                    }
                    //}
                }
                #endregion
            }
            ddlTree_SelectedIndexChanged(null, null);
            modalPopupCourseList.Hide();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = Ex.Message;
            modalPopupCourseList.Hide();
        }
    }

    private NodeCourse RefreshNodeCourse(int cId, int vId)
    {
        NodeCourse nodeCourse = null;
        if (Session["Node_Course"] == null)
        {
            nodeCourse = new NodeCourse();
        }
        else
        {
            nodeCourse = (NodeCourse)Session["Node_Course"];
        }

        nodeCourse.ParentNodeID = ((Node)Session["ParentNode"]).Id;

        //string[] courseIDnVerID = new string[2];
        //courseIDnVerID = ddlCourses.Value.ToString().Split(',');
        //nodeCourse.ChildCourseID = Int32.Parse(courseIDnVerID[0]);
        //nodeCourse.ChildVersionID = Int32.Parse(courseIDnVerID[1]);
        nodeCourse.ChildCourseID = cId;//ctlCourseSelect.PickedCourse.Id;
        nodeCourse.ChildVersionID = vId;//ctlCourseSelect.PickedCourse.VersionID;

        nodeCourse.Priority = Convert.ToInt32(spnPriority.Value);
        nodeCourse.IsActive = chkIsActive.Checked;

        nodeCourse.PreReqMasters = PreparePrerequisits();
        if (nodeCourse.PreReqMasters == null)
        {
            nodeCourse.HasPreriquisite = false;
        }
        else if (nodeCourse.PreReqMasters != null)
        {
            nodeCourse.HasPreriquisite = false;
        }
        else
        {
            nodeCourse.HasPreriquisite = true;
        }


        return nodeCourse;
    }
}
