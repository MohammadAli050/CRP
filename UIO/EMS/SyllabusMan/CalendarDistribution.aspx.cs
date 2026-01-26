using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using DataAccess;
using System.Data.SqlClient;
using System.Drawing;

public partial class CalenderDistribution : BasePage
{
    #region Variables
    private string[] _clsNameAndID = new string[2];
    private List<BussinessObject.Program> _programs = new List<BussinessObject.Program>();
    private List<Course> _courses = new List<Course>();
    private List<NodeCourse> _node_courses = new List<NodeCourse>();
    private List<Node> _nodes = new List<Node>();

    private List<CalendarUnitMaster> _calendarMasters = null;
    //private List<CalendarUnitDetail> _calendarDetails = null;
    //private CalendarUnitMaster _calendarMaster = null;
    //private CalendarUnitDetail _calendarDetail = null;

    private List<TreeCalendarMaster> _treeCalMasters = null;
    //private List<TreeCalendarDetail> _treeCalDetails = null;
    //private TreeCalendarMaster _treeCalMaster = null;
    private TreeCalendarDetail _treeCalDetail = null;

    private bool _isAddingRoot = false;
    private bool _isAddingNode = false;
    private bool _isAddingCourse = false;
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
                item.Text = "[" + program.Code + "] " + program.ShortName;
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
        ddlTreeMasters.Items.Clear();

        if (ddlPrograms.SelectedIndex >= 0)
        {
            if (Session["Programs"] != null)
            {
                List<Program> programs = (List<Program>)Session["Programs"];
                var program = from prog in programs where prog.Id == Int32.Parse(ddlPrograms.SelectedValue) select prog;

                List<TreeMaster> treeMasters = TreeMaster.GetByProgram(program.ElementAt(0).Id);

                if (treeMasters != null)
                {
                    ddlTreeMasters.Enabled = true;

                    foreach (TreeMaster treeMaster in treeMasters)
                    {
                        ListItem item = new ListItem();
                        item.Value = treeMaster.Id.ToString();
                        item.Text = treeMaster.RootNode.Name;
                        ddlTreeMasters.Items.Add(item);
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

                    ddlTreeMasters.SelectedIndex = 0;
                    ddlTreeMasters_SelectedIndexChanged(null, null);
                }
                else
                {
                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    if (Session["TreeMasters"] != null)
                    {
                        Session.Remove("TreeMasters");
                    }
                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    ddlTreeMasters.Enabled = false;
                    ddlLinkedCalendars.Items.Clear();
                    ddlLinkedCalendars.Enabled = false;

                    ShowMessage("No Tree found for the selected Program", Color.Red);
                }
            }
        }
    }
    private void FillCorsCombo()
    {
        ddlCourses.Items.Clear();

        _node_courses = NodeCourse.GetNodeCoursesByRoot(Int32.Parse(ddlTreeMasters.SelectedValue));//Course.GetActiveMotherCourses();

        if (_node_courses == null)
        {
            return;
        }
        if (_node_courses.Count == 0)
        {
            return;
        }

        _courses = new List<Course>();
        foreach (NodeCourse nc in _node_courses)
        {
            ListItem item = new ListItem();
            item.Value = nc.ChildCourseID.ToString() + "," + nc.ChildVersionID.ToString() + "," + nc.Id.ToString();// +"," + nc.ChildCourse.Credits;
            //item.Value = nc.ChildCourseID.ToString() + "," + nc.ChildVersionID.ToString();
            item.Text = nc.ChildCourse.FullCodeAndCourse + " [ " + nc.ChildCourse.OwnerProgram.ShortName + " ]"; 
            ddlCourses.Items.Add(item);
            _courses.Add(nc.ChildCourse);
        }

        if (Session["Courses"] != null)
        {
            Session.Remove("Courses");
        }
        Session.Add("Courses", _courses);

        if (Session["NodeCourses"] != null)
        {
            Session.Remove("NodeCourses");
        }
        Session.Add("NodeCourses", _node_courses);

        ddlCourses.SelectedIndex = -1;
        ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
    }
    private void FillNodesCombo()
    {
        ddlNodes.Items.Clear();

        _nodes = Node.GetNodesbyRoot(Int32.Parse(ddlTreeMasters.SelectedValue));

        if (_nodes != null && _nodes.Count > 0)
        {
            foreach (Node node in _nodes)
            {
                ListItem item = new ListItem();
                item.Value = node.Id.ToString();
                item.Text = node.Name;
                ddlNodes.Items.Add(item);
            }

            if (Session["Nodes"] != null)
            {
                Session.Remove("Nodes");
            }
            Session.Add("Nodes", _nodes);

            ddlNodes.SelectedIndex = -1;
        }
    }
    private void FillLinkedCalendars()
    {
        ddlLinkedCalendars.Items.Clear();

        if (ddlTreeMasters.SelectedIndex >= 0)
        {
            if (Session["TreeMasters"] != null)
            {
                List<TreeMaster> treeMasters = (List<TreeMaster>)Session["TreeMasters"];
                var treeMaster = from treeMas in treeMasters where treeMas.Id == Int32.Parse(ddlTreeMasters.SelectedValue) select treeMas;

                _treeCalMasters = TreeCalendarMaster.GetByTreeMaster(treeMaster.ElementAt(0).Id);
                Session.Add("TreeMaster", treeMaster.ElementAt(0));

                if (_treeCalMasters != null)
                {
                    ddlLinkedCalendars.Enabled = true;

                    ListItem item = new ListItem();
                    item.Value = 0.ToString();
                    item.Text = "<New Link>";
                    ddlLinkedCalendars.Items.Add(item);

                    foreach (TreeCalendarMaster treeCalMaster in _treeCalMasters)
                    {
                        item = new ListItem();
                        item.Value = treeCalMaster.Id.ToString();
                        item.Text = treeCalMaster.Name + " - " + treeCalMaster.CalendarMaster.Name;
                        ddlLinkedCalendars.Items.Add(item);
                    }

                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    Session.Add("TreeCalMasters", _treeCalMasters);

                    ddlLinkedCalendars.SelectedIndex = 0;
                }
                else
                {
                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    ddlLinkedCalendars.Enabled = false;
                    ShowMessage("No linked calendars were found for the selected tree", Color.Red);
                }
            }
        }
    }
    private void FillCalCombo()
    {
        ddlCalendars.Items.Clear();

        _calendarMasters = BussinessObject.CalendarUnitMaster.GetCalendarMasters();

        if (_calendarMasters != null)
        {
            foreach (CalendarUnitMaster calendarMaster in _calendarMasters)
            {
                ListItem item = new ListItem();
                item.Value = calendarMaster.Id.ToString();
                item.Text = calendarMaster.Name;
                ddlCalendars.Items.Add(item);
            }

            if (Session["CalendarUnitMasters"] != null)
            {
                Session.Remove("CalendarUnitMasters");
            }
            Session.Add("CalendarUnitMasters", _calendarMasters);

            ddlCalendars.SelectedIndex = 0;
        }
    }

    private void ClearEntryControl()
    {
        ddlCalendars.SelectedIndex = 0;
        ddlCourses.SelectedIndex = -1;
        ddlNodes.SelectedIndex = -1;

        pnlCalendar.Enabled = true;
        pnlSrchCriteria.Enabled = true;
        txtCalName.Text = string.Empty;

        pnlControl.Visible = false;

        pnlLinkCal.Visible = false;
        pnlCourse.Visible = false;
        txtNodeLinkName.Text = string.Empty;
        pnlNodes.Visible = false;

        txtNodePriority.Visible = false;
        txtCoursePriority.Visible = false;
    }
    private void LockTreeControl()
    {
        pnlCalendar.Enabled = false;
        pnlSrchCriteria.Enabled = false;
    }
    private void ControlEnablerRoot(bool enable)
    {
        pnlCalendar.Enabled = !enable;
        pnlSrchCriteria.Enabled = !enable;

        ddlCalendars.SelectedIndex = 0;
        ddlCourses.SelectedIndex = -1;
        ddlNodes.SelectedIndex = -1;

        pnlControl.Visible = enable;
        pnlLinkCal.Visible = enable;
        pnlCourse.Visible = !enable;
        pnlNodes.Visible = !enable;
    }
    private void ControlEnablerNode(bool enable)
    {
        pnlCalendar.Enabled = !enable;
        pnlSrchCriteria.Enabled = !enable;

        ddlCalendars.SelectedIndex = 0;
        ddlCourses.SelectedIndex = -1;
        ddlNodes.SelectedIndex = -1;

        pnlControl.Visible = enable;
        pnlLinkCal.Visible = !enable;
        pnlCourse.Visible = !enable;
        pnlNodes.Visible = enable;
        txtNodePriority.Visible = enable;
    }
    private void ControlEnablerCourse(bool enable)
    {
        pnlCalendar.Enabled = !enable;
        pnlSrchCriteria.Enabled = !enable;

        ddlCalendars.SelectedIndex = 0;
        ddlCourses.SelectedIndex = -1;
        ddlNodes.SelectedIndex = -1;

        pnlControl.Visible = enable;
        pnlLinkCal.Visible = !enable;
        pnlCourse.Visible = enable;
        pnlNodes.Visible = !enable;
        txtCoursePriority.Visible = enable;
    }

    private void ShowRoot()
    {
        if (ddlLinkedCalendars.SelectedIndex >= 0)
        {
            if (Session["TreeCalMasters"] != null)
            {
                TreeCalendarMaster treeCalMaster = TreeCalendarMaster.Get(Int32.Parse(ddlLinkedCalendars.SelectedValue));

                if (treeCalMaster != null)
                {
                    if (!pnlCalendar.Enabled)
                    {
                        pnlCalendar.Enabled = true;
                    }

                    LoadRoot(treeCalMaster);

                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    Session.Add("TreeCalMaster", treeCalMaster);
                }
                else
                {
                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                }
            }
        }
    }
    private void LoadRoot(TreeCalendarMaster treeCalMaster)
    {
        tvwCalendar.Nodes.Clear();
        TreeNode treeNode = new TreeNode();
        treeNode.Text = treeCalMaster.CalendarMaster.Name;
        treeNode.Value = "TreeCalMas," + treeCalMaster.Id.ToString();
        treeNode.ExpandAll();

        tvwCalendar.Nodes.Add(treeNode);
    }
    private void LoadNode(TreeNode parentNode, List<TreeCalendarDetail> treeCalDetails)
    {
        foreach (TreeCalendarDetail treeCalDetail in treeCalDetails)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = treeCalDetail.CalendarDetail.Name;
            treeNode.Value = "TreeCalDet," + treeCalDetail.Id.ToString();
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
    private void LoadNode(TreeNode parentNode, List<Cal_Course_Prog_Node> calendarDistributions)
    {
        foreach (Cal_Course_Prog_Node calendarDistribution in calendarDistributions)
        {
            TreeNode treeNode = new TreeNode();
            if (calendarDistribution.CourseID == 0)
            {
                treeNode.Text = calendarDistribution.NodeLinkName + "-" + calendarDistribution.Node.Name;
                treeNode.Value = "CALDISNOD," + calendarDistribution.Id.ToString() + "#" + calendarDistribution.NodeID.ToString();
                treeNode.ExpandAll();
            }
            else
            {
                string programName = calendarDistribution.Course.OwnerProgram == null ? "#" : calendarDistribution.Course.OwnerProgram.ShortName;
                treeNode.Text = calendarDistribution.Course.VersionCode + "-" + calendarDistribution.Course.Title + " [ " + programName + " ]" + " [ " + calendarDistribution.Priority.ToString() + " ] "; 
                treeNode.Value = "CALDISCRS," + calendarDistribution.Id.ToString() + "#" + calendarDistribution.CourseID.ToString() + "#" + calendarDistribution.VersionID.ToString();
                treeNode.ExpandAll();
            }

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
                tvwCalendar.Nodes.Add(treeNode);
            }
            else
            {
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
            treeNode.Value = "SETMAS," + vNodeMaster.SetNo.ToString() + "#" + vNodeMaster.OwnerNodeID.ToString();
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
                treeNode.Value = "VNODSET," + "0";
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
    private void LoadNode(TreeNode parentNode, List<Course> courses)
    {
        foreach (Course course in courses)
        {
            TreeNode node = new TreeNode();
            node.Text = course.VersionCode + "-" + course.Title + " [ " + course.OwnerProgram.ShortName + " ]"; 
            node.Value = "CRS," + course.Id.ToString() + "#" + course.VersionID.ToString();
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

    private void FillChildrenONode(TreeNode treeNode, Node node, TreeMaster treeMaster)
    {
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
                else
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
                else
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
            if (_clsNameAndID[0] == "TreeCalMas")
            {
                #region If Parent is  Tree Calendar Master
                if (Session["TreeMaster"] != null && Session["TreeCalMaster"] != null)
                {
                    TreeCalendarMaster treeCalMaster = TreeCalendarMaster.Get(Int32.Parse(_clsNameAndID[1])); ;
                    List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetail.GetByTreeCalMaster(treeCalMaster.Id);


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
                if (Session["TreeMaster"] != null && Session["TreeCalMaster"] != null)
                {
                    TreeCalendarDetail treeCalDetail = TreeCalendarDetail.Get(Int32.Parse(_clsNameAndID[1])); ;
                    List<Cal_Course_Prog_Node> calendarDistributions = Cal_Course_Prog_Node.GetByTreeCalDet(treeCalDetail.Id);

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
                if (Session["TreeMaster"] != null)
                {
                    TreeMaster treeMaster = (TreeMaster)Session["TreeMaster"];

                    Node node = null;
                    if (_clsNameAndID[0] == "NOD")
                    {
                        #region If Parent is a Node
                        node = Node.GetNode(Int32.Parse(_clsNameAndID[1]));
                        #endregion
                    }
                    else if (_clsNameAndID[0] == "CALDISNOD")
                    {
                        #region If Parent is a Calendar Distribution
                        string[] calDisIDAndNodeID = new string[2];
                        calDisIDAndNodeID = _clsNameAndID[1].Split('#');
                        node = Node.GetNode(Int32.Parse(calDisIDAndNodeID[1]));
                        #endregion
                    }

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

                    string[] setNoAndNodeID = new string[2];
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
                            tvwCalendar.Nodes.Remove(treeNode);
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

    private bool ValidateTreeCalMaster()
    {
        if (ddlCalendars.SelectedIndex < 0)
        {
            ShowMessage("Must select a calendar", Color.Red);
            return false;
        }
        else if (txtCalName.Text.Trim().Length <= 0)
        {
            ShowMessage("Calendar link name can not be empty.", Color.Red);
            return false;
        }
        else
        {
            return true;
        }
    }
    private TreeCalendarMaster RefreshTreeCalMas()
    {
        TreeCalendarMaster treeCalMaster = new TreeCalendarMaster();

        treeCalMaster.Name = txtCalName.Text.Trim();
        treeCalMaster.TreeMasterID = Int32.Parse(ddlTreeMasters.SelectedValue);
        treeCalMaster.CalendarMasterID = Int32.Parse(ddlCalendars.SelectedValue);
        treeCalMaster.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
        treeCalMaster.CreatedDate = DateTime.Now;


        if (treeCalMaster.CalendarMaster.CalendarDetails != null)
        {
            List<TreeCalendarDetail> treeCalDetails = new List<TreeCalendarDetail>();
            TreeCalendarDetail treeCalDetail = null;
            foreach (CalenderUnitDistribution calendarDetail in treeCalMaster.CalendarMaster.CalendarDetails)
            {
                treeCalDetail = new TreeCalendarDetail();
                treeCalDetail.CalendarMasterID = treeCalMaster.CalendarMasterID;
                treeCalDetail.CalendarUnitDistributionID = calendarDetail.Id;
                treeCalDetail.TreeMasterID = treeCalMaster.TreeMasterID;
                treeCalDetail.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                treeCalDetail.CreatedDate = DateTime.Now;
                treeCalDetails.Add(treeCalDetail);
            }
            treeCalMaster.TreeCalendarDetails = treeCalDetails;
        }

        return treeCalMaster;
    }

    private bool ValidateNode()
    {
        int prioriry = 0;
        if (ddlNodes.SelectedIndex < 0)
        {
            ShowMessage("Must select an operator.", Color.Red);

            return false;
        }
        else if (txtNodeLinkName.Text.Trim().Length == 0)
        {
            ShowMessage("Link Name can not empty.", Color.Red);

            return false;
        }
        else if (txtNodePriority.Text.Trim().Length != 0 && !Int32.TryParse(txtNodePriority.Text.Trim(), out prioriry))
        {
            ShowMessage("Priority needs to be only valid integer.", Color.Red);

            return false;
        }
        else
        {
            return true;
        }
    }
    private bool ValidateCourse()
    {
        int prioriry = 0;
        if (ddlCourses.SelectedIndex < 0)
        {
            ShowMessage("Must select a Course", Color.Red);

            return false;
        }
        else if (txtCoursePriority.Text.Trim().Length != 0 && !Int32.TryParse(txtCoursePriority.Text.Trim(), out prioriry))
        {
            ShowMessage("Priority needs to be only valid integer.", Color.Red);

            return false;
        }
        else
        {
            return true;
        }
    }
    private bool ValidateCourseDistribution()
    {
        if (pnlNodes.Visible)
        {
            return ValidateNode();
        }
        else if (pnlCourse.Visible)
        {
            return ValidateCourse();
        }
        else
        {
            return true;
        }
    }
    private void FillControl(Cal_Course_Prog_Node calendarDistribution)
    {
        if (pnlCourse.Visible && calendarDistribution.CourseID > 0 && calendarDistribution.NodeCourseID > 0)
        {
            ddlCourses.SelectedValue = calendarDistribution.CourseID.ToString() + "," + calendarDistribution.VersionID.ToString() + "," + calendarDistribution.NodeCourseID.ToString();
           
            ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
            txtCoursePriority.Text = calendarDistribution.Priority.ToString();
            txtCourseCredits.Text = calendarDistribution.Credits.ToString();
            chkMajorRelated.Checked = calendarDistribution.IsMajorRelated;
            chkMinorRelated.Checked = calendarDistribution.IsMinorRelated;
        }
        

        else if (pnlNodes.Visible && calendarDistribution.NodeID > 0)
        {
            ddlNodes.SelectedValue = calendarDistribution.NodeID.ToString();
            ddlNodes.ToolTip = ddlNodes.SelectedItem.Text;
            txtNodeLinkName.Text = calendarDistribution.NodeLinkName;
            txtNodePriority.Text = calendarDistribution.Priority.ToString();
            chkMajorRelated.Checked = calendarDistribution.IsMajorRelated;
            chkMinorRelated.Checked = calendarDistribution.IsMinorRelated;
        }
    }
    private Cal_Course_Prog_Node RefreshCalendarDistribution()
    {
        Cal_Course_Prog_Node calendarDistribution = null;
        if (Session["CalendarDistribution"] != null)
        {
            calendarDistribution = (Cal_Course_Prog_Node)Session["CalendarDistribution"];
            calendarDistribution.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            calendarDistribution.ModifiedDate = DateTime.Now;
        }
        else
        {
            calendarDistribution = new Cal_Course_Prog_Node();
            calendarDistribution.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            calendarDistribution.CreatedDate = DateTime.Now;
        }

        calendarDistribution.ProgramID = Int32.Parse(ddlPrograms.SelectedValue);
        calendarDistribution.TreeCalendarDetailID = ((TreeCalendarDetail)Session["TreeCalDetail"]).Id;

        if (pnlCourse.Visible)
        {
            int coursePriority = string.IsNullOrEmpty(txtCoursePriority.Text.Trim()) ? 0 : Int32.Parse(txtCoursePriority.Text);
            decimal courseCredit = string.IsNullOrEmpty(txtCourseCredits.Text.Trim()) ? 0 : Convert.ToDecimal(txtCourseCredits.Text.Trim());

            string[] courseIDnVerID = new string[4];
            courseIDnVerID = ddlCourses.SelectedValue.Split(',');
            calendarDistribution.CourseID = Int32.Parse(courseIDnVerID[0]);
            calendarDistribution.VersionID = Int32.Parse(courseIDnVerID[1]);
            calendarDistribution.NodeCourseID = Int32.Parse(courseIDnVerID[2]);
            calendarDistribution.Priority = coursePriority;
            calendarDistribution.Credits = courseCredit;
            calendarDistribution.IsMajorRelated = chkMajorRelated.Checked;
            calendarDistribution.IsMinorRelated = chkMinorRelated.Checked;

        }
        else if (pnlNodes.Visible)
        {
            int nodePriority = string.IsNullOrEmpty(txtNodePriority.Text.Trim()) ? 0 : Int32.Parse(txtNodePriority.Text);
            decimal nodeCredit = string.IsNullOrEmpty(txtNodeCredits.Text.Trim()) ? 0 : Convert.ToDecimal(txtNodeCredits.Text.Trim());

            calendarDistribution.NodeLinkName = txtNodeLinkName.Text.Trim();
            calendarDistribution.NodeID = Int32.Parse(ddlNodes.SelectedValue);
            calendarDistribution.Priority = nodePriority;
            calendarDistribution.Credits = nodeCredit;
            calendarDistribution.IsMajorRelated = chkMajorRelated.Checked;
            calendarDistribution.IsMinorRelated = chkMinorRelated.Checked;

        }

        return calendarDistribution;
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
    private void ShowMessage(string message, Color color)
    {
        lblMsg.Text = string.Empty;
        lblMsg.Text = message;
        lblMsg.ForeColor = color;
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
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ClearEntryControl();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
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

            if (!IsPostBack)
            {
                FillProgCombo();
                FillTreeCombo();
                FillCorsCombo();
                FillNodesCombo();
                FillLinkedCalendars();
                FillCalCombo();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");

            ddlPrograms.Focus();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            tvwCalendar.Nodes.Clear();
            FillTreeCombo();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void ddlTreeMasters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            tvwCalendar.Nodes.Clear();
            FillLinkedCalendars();
            FillCorsCombo();
            FillNodesCombo();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void ddlLinkedCalendars_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (true)
            {
                ClearMessagelbl();

                tvwCalendar.Nodes.Clear();
                ShowRoot();
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void btnAddCalender_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (Session["TreeMaster"] == null)
            {
                ShowMessage(@"This program is not linked to tree, please link a tree first, then link calendar to it.", Color.Red);

                return;
            }

            if (Session["TreeCalMaster"] != null)
            {
                ShowMessage(@"Please select the option New Link, from Linked Calendars", Color.Red);

                return;
            }

            ControlEnablerRoot(true);

            _isAddingRoot = true;
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
            txtCalName.Focus();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnAddNode_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwCalendar.SelectedNode == null)
            {
                ShowMessage("Before trying to link a node please select a calendar detail.", Color.Red);
                return;
            }
            _clsNameAndID = tvwCalendar.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] != "TreeCalDet")
            {
                ShowMessage("A node can only be linked with a calendar detail.", Color.Red);
                return;
            }

            _treeCalDetail = TreeCalendarDetail.Get(Int32.Parse(_clsNameAndID[1]));
            if (Session["TreeCalDetail"] != null)
            {
                Session.Remove("TreeCalDetail");
            }
            Session.Add("TreeCalDetail", _treeCalDetail);

            ControlEnablerNode(true);

            _isAddingNode = true;
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
            txtNodeLinkName.Focus();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnAddCourse_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwCalendar.SelectedNode == null)
            {
                ShowMessage("Before trying to add a course please select a calendar detail.", Color.Red);
                return;
            }
            _clsNameAndID = tvwCalendar.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] != "TreeCalDet")
            {
                ShowMessage("A course can only be linked with a calendar detail.", Color.Red);
                return;
            }

            _treeCalDetail = TreeCalendarDetail.Get(Int32.Parse(_clsNameAndID[1]));
            if (Session["TreeCalDetail"] != null)
            {
                Session.Remove("TreeCalDetail");
            }
            Session.Add("TreeCalDetail", _treeCalDetail);

            ControlEnablerCourse(true);
            _isAddingCourse = true;
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
            ddlCourses.Focus();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (tvwCalendar.SelectedNode == null)
            {
                ShowMessage("Before trying to edit a node/course please select the node/course.", Color.Red);
                return;
            }

            _clsNameAndID = tvwCalendar.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] != "CALDISNOD" && _clsNameAndID[0] != "CALDISCRS")
            {
                ShowMessage("Only a node link or course link can be edited", Color.Red);
                return;
            }

            string[] parentID = new string[2];
            parentID = tvwCalendar.SelectedNode.Parent.Value.Split(new char[] { ',' });

            _treeCalDetail = TreeCalendarDetail.Get(Int32.Parse(parentID[1]));
            if (Session["TreeCalDetail"] != null)
            {
                Session.Remove("TreeCalDetail");
            }
            Session.Add("TreeCalDetail", _treeCalDetail);

            if (_clsNameAndID[0] == "CALDISNOD")
            {
                #region If Parent is a Calendar Distribution linked with Node
                string[] calDisIDAndNodeID = new string[2];
                calDisIDAndNodeID = _clsNameAndID[1].Split('#');
                Node node = Node.GetNode(Int32.Parse(calDisIDAndNodeID[1]));

                Cal_Course_Prog_Node calDist = Cal_Course_Prog_Node.Get(Int32.Parse(calDisIDAndNodeID[0]));
                if (Session["CalendarDistribution"] != null)
                {
                    Session.Remove("CalendarDistribution");
                }
                Session.Add("CalendarDistribution", calDist);

                ControlEnablerNode(true);
                FillControl(calDist);

                _isAddingNode = true;
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
                ddlNodes.Focus();
            }
            else if (_clsNameAndID[0] == "CALDISCRS")
            {
                #region If Parent is a Calendar Distribution linked with Course
                string[] calDisIDAndCourseID = new string[3];
                calDisIDAndCourseID = _clsNameAndID[1].Split('#');
                Course course = Course.GetCourse(Int32.Parse(calDisIDAndCourseID[1]), Int32.Parse(calDisIDAndCourseID[2]));

                Cal_Course_Prog_Node calDist = Cal_Course_Prog_Node.Get(Int32.Parse(calDisIDAndCourseID[0]));
                if (Session["CalendarDistribution"] != null)
                {
                    Session.Remove("CalendarDistribution");
                }
                Session.Add("CalendarDistribution", calDist);

                ControlEnablerCourse(true);
                FillControl(calDist);

                _isAddingCourse = true;
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
                ddlCourses.Focus();
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (tvwCalendar.SelectedNode == null)
            {
                ShowMessage("Before trying to delete a node/course please select the node/course.", Color.Red);
                return;
            }

            _clsNameAndID = tvwCalendar.SelectedNode.Value.Split(',');

            if (_clsNameAndID[0] != "TreeCalMas" && _clsNameAndID[0] != "CALDISNOD" && _clsNameAndID[0] != "CALDISCRS")
            {
                ShowMessage("Only the whole tree or node link or course link can be deleted", Color.Red);
                return;
            }

            if (_clsNameAndID[0] == "TreeCalMas")
            {
                TreeCalendarMaster.Delete(Int32.Parse(_clsNameAndID[1]));
                tvwCalendar.Nodes.Clear();
                FillLinkedCalendars();

                ShowMessage("TreeMaster and the underlying TreeDtails and all of their children has been deleted", Color.Red);

                if (Session["TreeCalMaster"] != null)
                {
                    Session.Remove("TreeCalMaster");
                }
            }
            else if (_clsNameAndID[0] == "CALDISNOD" || _clsNameAndID[0] == "CALDISCRS")
            {
                string[] calDisIDAndNodeID = new string[2];
                calDisIDAndNodeID = _clsNameAndID[1].Split('#');

                Cal_Course_Prog_Node.Delete(Int32.Parse(calDisIDAndNodeID[0]));
                LoadChildrens(tvwCalendar.SelectedNode.Parent);

                if (_clsNameAndID[0] == "CALDISNOD")
                {
                    ShowMessage("Link with node has been deleted.", Color.Red);
                }
                else if (_clsNameAndID[0] == "CALDISCRS")
                {
                    ShowMessage("Link with course has been deleted.", Color.Red);
                }
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
            ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearMessagelbl();
            if (Session["IsAddingRoot"] != null)
            {
                #region Root
                if (Convert.ToBoolean(Session["IsAddingRoot"]))
                {
                    if (ValidateTreeCalMaster())
                    {
                        TreeCalendarMaster treeCalMaster = RefreshTreeCalMas();

                        TreeCalendarMaster.Save(treeCalMaster);

                        ShowMessage("Root Saved", Color.RoyalBlue);

                        FillLinkedCalendars();
                        ShowRoot();
                        ClearEntryControl();

                        ddlLinkedCalendars.Focus();
                    }
                }
                #endregion
            }
            else if (Session["IsAddingNode"] != null)
            {
                #region Node
                if (Convert.ToBoolean(Session["IsAddingNode"]))
                {
                    if (Session["TreeMaster"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateCourseDistribution())
                        {
                            Cal_Course_Prog_Node treeDetail = RefreshCalendarDistribution();

                            Cal_Course_Prog_Node.Save(treeDetail);
                            if (Session["CalendarDistribution"] != null)
                            {
                                ShowMessage("Node Link updated", Color.RoyalBlue);
                                LoadChildrens(tvwCalendar.FindNode(((string)Session["SelectedNode"])).Parent);
                                ClearEntryControl();
                            }
                            else
                            {
                                ShowMessage("Node Linked", Color.RoyalBlue);
                                LoadChildrens(tvwCalendar.FindNode(((string)Session["SelectedNode"])));
                                ClearEntryControl();
                                ControlEnablerNode(true);
                            }


                            if (Session["CalendarDistribution"] != null)
                            {
                                Session.Remove("CalendarDistribution");
                            }
                            tvwCalendar.Focus();
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
                    if (Session["TreeMaster"] != null && Session["SelectedNode"] != null)
                    {
                        if (ValidateCourseDistribution())
                        {
                            Cal_Course_Prog_Node treeDetail = RefreshCalendarDistribution();

                            Cal_Course_Prog_Node.Save(treeDetail);

                            if (Session["CalendarDistribution"] != null)
                            {
                                ShowMessage("Course Link updated", Color.RoyalBlue);
                                LoadChildrens(tvwCalendar.FindNode(((string)Session["SelectedNode"])).Parent);
                                ClearEntryControl();
                            }
                            else
                            {
                                ShowMessage("Course Linked", Color.RoyalBlue);
                                LoadChildrens(tvwCalendar.FindNode(((string)Session["SelectedNode"])));
                                ClearEntryControl();
                                ControlEnablerCourse(true);
                            }


                            if (Session["CalendarDistribution"] != null)
                            {
                                Session.Remove("CalendarDistribution");
                            }
                            tvwCalendar.Focus();
                        }
                    }
                }
                #endregion
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearEntryControl();
            if (ddlLinkedCalendars.Enabled && ddlLinkedCalendars.SelectedIndex == 0)
            {
                ddlLinkedCalendars.Focus();
            }
            else
            {
                tvwCalendar.Focus();
            }

        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void tvwCalendar_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ClearMessagelbl();

            if (tvwCalendar.SelectedNode != null)
            {
                if (Session["SelectedNode"] != null)
                {
                    Session.Remove("SelectedNode");
                }
                Session.Add("SelectedNode", tvwCalendar.SelectedNode.ValuePath);

                LoadChildrens(tvwCalendar.SelectedNode);
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourses.SelectedIndex >= 0)
            {


                ddlCourses.ToolTip = ddlCourses.SelectedItem.Text;
                string[] courseIDnVerID = new string[4];
                courseIDnVerID = ddlCourses.SelectedValue.Split(',');

                Course course = Course.GetCourse(Convert.ToInt32(courseIDnVerID[0]), Convert.ToInt32(courseIDnVerID[1]));
                txtCourseCredits.Text = course.Credits.ToString();// courseIDnVerID[3];
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    #endregion
}
