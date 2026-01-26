using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    public class AssignedStructTreeNode
    {
        #region Properties

        #region Structural
        private string _name;//1
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private FlatAssignedStructTreeLevels _type;//2
        public FlatAssignedStructTreeLevels LevelType
        {
            get { return _type; }
            set { _type = value; }
        }

        private object _dataObj;//3
        public object DataObj
        {
            get { return _dataObj; }
            set { _dataObj = value; }
        }

        private string _dataObjTypeName;//4
        public string DataObjTypeName
        {
            get { return _dataObjTypeName; }
            set { _dataObjTypeName = value; }
        }

        private int? _parentID;//11
        public int? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        private FlatAssignedStructTreeLevels _parentType;//12
        public FlatAssignedStructTreeLevels ParentLevelType
        {
            get { return _parentType; }
            set { _parentType = value; }
        }

        private int _dataObjID;//13
        public int DataObjID
        {
            get { return _dataObjID; }
            set { _dataObjID = value; }
        }

        private int _dataObjID2;//14
        public int DataObjID2
        {
            get { return _dataObjID2; }
            set { _dataObjID2 = value; }
        }

        private FlatAssignedStructTreeLevels _dataObj2Type;//15
        public FlatAssignedStructTreeLevels DataObj2Type
        {
            get { return _dataObj2Type; }
            set { _dataObj2Type = value; }
        }

        private int _dataObjID3;//16
        public int DataObjID3
        {
            get { return _dataObjID3; }
            set { _dataObjID3 = value; }
        }

        private FlatAssignedStructTreeLevels _dataObj3Type;//17
        public FlatAssignedStructTreeLevels DataObj3Type
        {
            get { return _dataObj3Type; }
            set { _dataObj3Type = value; }
        } 
        #endregion

        #region Logical
        private bool? _isVirtual;//5
        public bool? IsVirtual
        {
            get { return _isVirtual; }
            set { _isVirtual = value; }
        }

        private bool? _isLastLevel;//6
        public bool? IsLastLevel
        {
            get { return _isLastLevel; }
            set { _isLastLevel = value; }
        }

        private decimal? _maxCredit;//7
        public decimal? MaxCredit
        {
            get { return _maxCredit; }
            set { _maxCredit = value; }
        }

        private decimal? _minCredit;//8
        public decimal? MinCredit
        {
            get { return _minCredit; }
            set { _minCredit = value; }
        }

        private int? _maxCourses;//9
        public int? MaxCourses
        {
            get { return _maxCourses; }
            set { _maxCourses = value; }
        }

        private int? _minCourses;//10
        public int? MinCourses
        {
            get { return _minCourses; }
            set { _minCourses = value; }
        }

        private int? _priority;//19
        public int? Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private bool _isBundle;//26
        public bool IsBundle
        {
            get { return _isBundle; }
            set { _isBundle = value; }
        }

        private int _operatorID;//27
        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        private Operator _operator;//28
        public Operator Operator
        {
            get
            {
                if (_operator == null)
                {
                    if (this.OperatorID > 0)
                    {
                        _operator = Operator.GetOperator(this.OperatorID);
                    }
                }
                return _operator;
            }
        }

        private bool _isActive;//29
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private decimal? _reqCredit;//30
        public decimal? ReqCredit
        {
            get { return _reqCredit; }
            set { _reqCredit = value; }
        }
        #endregion

        #region StudentSpecific
        private int? _retakes;//18
        public int? Retakes
        {
            get { return _retakes; }
            set { _retakes = value; }
        }

        private string _highestGrade;//20
        public string HighestGrade
        {
            get { return _highestGrade; }
            set { _highestGrade = value; }
        }
        public int HighestGradeOrderKey
        {
            get
            {
                return BOConstants.Grades.IndexOfValue(HighestGrade);
            }
        }

        private bool? _canRetake;//21
        public bool? CanRetake
        {
            get { return _canRetake; }
            set { _canRetake = value; }
        }

        private bool _isOffered;//22
        public bool IsOffered
        {
            get { return _isOffered; }
            set { _isOffered = value; }
        }

        private bool _isEligible;//23
        public bool IsEligible
        {
            get { return _isEligible; }
            set { _isEligible = value; }
        }

        private bool _hasCompleted;//24
        public bool HasCompleted
        {
            get { return _hasCompleted; }
            set { _hasCompleted = value; }
        }

        private bool _preRequisitDone;//25
        public bool PreRequisitDone
        {
            get { return _preRequisitDone; }
            set { _preRequisitDone = value; }
        }

        private bool? _isStudentSpec;//31
        public bool? IsStudentSpec
        {
            get { return _isStudentSpec; }
            set { _isStudentSpec = value; }
        } 
        #endregion

        #endregion

        #region Constructor
        public AssignedStructTreeNode()
            : base()
        {
            _name = string.Empty;//1
            _type = FlatAssignedStructTreeLevels.None;//2
            _dataObj = null;//3
            _dataObjTypeName = null;//4
            _isVirtual = null;//5
            _isLastLevel = null;//6
            _dataObjID = 0;//7
            _dataObjID2 = 0;//8
            _dataObj2Type = FlatAssignedStructTreeLevels.None;//9
            _dataObjID3 = 0;//10
            _dataObj3Type = FlatAssignedStructTreeLevels.None;//11
            _retakes = null;//12
            _priority = null;//13
            _highestGrade = string.Empty;//14
            _canRetake = null;//15
            _isOffered = false;//16
            _isEligible = false;//17
            _hasCompleted = false;//18
            _preRequisitDone = false;//19
            _minCourses = 0;//20
            _maxCourses = 0;//21
            _minCredit = 0;//22
            _maxCredit = 0;//23
            _isBundle = false;//24
            _operatorID = 0;//25
            _parentID = null;//26
            _parentType = FlatAssignedStructTreeLevels.None;//27
            _operator = null;//28
            _isActive = true;//29
            _reqCredit = 0;//30
            _isStudentSpec = null;//31
        } 
        #endregion

        #region Methods
        private static void FlattenDetail(AssignedStructTreeNode assignedTreeNode, TreeDetail treeDet)
        {
            assignedTreeNode.Name = treeDet.ChildNode.Name;
            assignedTreeNode.DataObj = treeDet.ChildNode;
            assignedTreeNode.DataObjID = treeDet.ChildNode.Id;
            assignedTreeNode.DataObjTypeName = "Node";
            assignedTreeNode.LevelType = FlatAssignedStructTreeLevels.Node;
            assignedTreeNode.DataObjID2 = treeDet.Id;
            assignedTreeNode.DataObj2Type = FlatAssignedStructTreeLevels.TreeDetail;
            assignedTreeNode.ParentID = treeDet.ParentNodeID;
            assignedTreeNode.ParentLevelType = FlatAssignedStructTreeLevels.Node;

            assignedTreeNode.IsActive = treeDet.ChildNode.IsActive;
            assignedTreeNode.IsBundle = treeDet.ChildNode.IsBundle;
            assignedTreeNode.IsLastLevel = treeDet.ChildNode.IsLastLevel;
            assignedTreeNode.IsVirtual = treeDet.ChildNode.IsVirtual;

            assignedTreeNode.MaxCourses = treeDet.ChildNode.MaxCourses;
            assignedTreeNode.MinCourses = treeDet.ChildNode.MinCourses;
            assignedTreeNode.MaxCredit = treeDet.ChildNode.MaxCredit;
            assignedTreeNode.MinCredit = treeDet.ChildNode.MinCredit;

            assignedTreeNode.OperatorID = treeDet.ChildNode.OperatorID;

        }
        private static void FlattenMaster(AssignedStructTreeNode assignedTreeNode, TreeMaster treeMas)
        {
            assignedTreeNode.Name = treeMas.RootNode.Name;
            assignedTreeNode.DataObj = treeMas.RootNode;
            assignedTreeNode.DataObjID = treeMas.RootNode.Id;
            assignedTreeNode.DataObjTypeName = "Node";
            assignedTreeNode.LevelType = FlatAssignedStructTreeLevels.RootNode;
            assignedTreeNode.DataObjID2 = treeMas.Id;
            assignedTreeNode.DataObj2Type = FlatAssignedStructTreeLevels.TreeMaster;
        }

        private static void FlattenCourse(AssignedStructTreeNode assignedTreeNode, NodeCourse node_course)
        {
            assignedTreeNode.Name = node_course.ChildCourse.FullCodeAndCourse;
            assignedTreeNode.DataObj = node_course.ChildCourse;
            assignedTreeNode.DataObjID = node_course.ChildCourse.Id;
            assignedTreeNode.DataObjID2 = node_course.ChildCourse.VersionID;
            assignedTreeNode.DataObjTypeName = "Course";
            assignedTreeNode.LevelType = FlatAssignedStructTreeLevels.Course;
            assignedTreeNode.DataObjID3 = node_course.Id;
            assignedTreeNode.DataObj3Type = FlatAssignedStructTreeLevels.NodeCourse;
            assignedTreeNode.ParentID = node_course.ParentNodeID;
            assignedTreeNode.ParentLevelType = FlatAssignedStructTreeLevels.Node;

            assignedTreeNode.HighestGrade = BOConstants.Grades[0].ToString();
            assignedTreeNode.Priority = node_course.Priority;
        }

        private static void FlattenVNodeMaster(AssignedStructTreeNode assignedTreeNode, VNodeSetMaster vNodeMaster)
        {
            assignedTreeNode.Name = vNodeMaster.SetName;
            assignedTreeNode.DataObj = vNodeMaster;
            assignedTreeNode.DataObjID = vNodeMaster.Id;
            assignedTreeNode.DataObjTypeName = "VNodeSetMaster";
            assignedTreeNode.LevelType = FlatAssignedStructTreeLevels.VNodeSetMaster;
            assignedTreeNode.ParentID = vNodeMaster.OwnerNodeID;
            assignedTreeNode.ParentLevelType = FlatAssignedStructTreeLevels.Node;

            assignedTreeNode.ReqCredit = vNodeMaster.RequiredUnits;
        }

        private static void FlattenVNodeSet(AssignedStructTreeNode assignedTreeNode, VNodeSet vNodeSet)
        {

            assignedTreeNode.DataObj = vNodeSet;
            assignedTreeNode.DataObjID = vNodeSet.Id;
            assignedTreeNode.DataObjTypeName = "VNodeSet";
            assignedTreeNode.LevelType = FlatAssignedStructTreeLevels.VnodeSet;
            assignedTreeNode.ParentID = vNodeSet.VNodeSetMasID;
            assignedTreeNode.ParentLevelType = FlatAssignedStructTreeLevels.VNodeSetMaster;

            if (vNodeSet.NodeCourseID != 0 && vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
            {
                assignedTreeNode.Name = vNodeSet.OperandCourse.FullCodeAndCourse;
                assignedTreeNode.DataObjID2 = vNodeSet.NodeCourse.Id;
                assignedTreeNode.DataObj2Type = FlatAssignedStructTreeLevels.NodeCourse;
                assignedTreeNode.IsStudentSpec = false;
            }
            else if (vNodeSet.OperandNodeID != 0)
            {
                assignedTreeNode.Name = vNodeSet.OperandNode.Name;
                assignedTreeNode.DataObjID2 = vNodeSet.OperandNode.Id;
                assignedTreeNode.DataObj2Type = FlatAssignedStructTreeLevels.Node;
                assignedTreeNode.IsStudentSpec = false;
            }
            else if (vNodeSet.IsStudntSpec)
            {
                assignedTreeNode.Name = "Student Specific Mojor";
                assignedTreeNode.IsStudentSpec = true;
            }


            assignedTreeNode.OperatorID = vNodeSet.OperatorID;
        }

        public static List<AssignedStructTreeNode> GetAssignedStructTree(Student student)
        {
            List<AssignedStructTreeNode> assignedTreeNodes = new List<AssignedStructTreeNode>();

            if (student != null && student.Id != 0)
            {

                TreeMaster treeMaster = null;
                treeMaster = TreeMaster.Get(student.TreeMasterID);
                if (treeMaster != null && treeMaster.Id != 0)
                {
                    DrillNode(treeMaster.RootNode, assignedTreeNodes);
                }

                MapHistoryAndTree(assignedTreeNodes, student);
            }

            return assignedTreeNodes;
        }

        public static List<AssignedStructTreeNode> GetAssignedStructTree(string roll)
        {
            Student student = Student.GetStudentByRoll(roll);
            List<AssignedStructTreeNode> assignedTreeNodes = new List<AssignedStructTreeNode>();

            if (student != null && student.Id != 0)
            {

                TreeMaster treeMaster = null;
                treeMaster = TreeMaster.Get(student.TreeMasterID);
                if (treeMaster != null && treeMaster.Id != 0)
                {
                    DrillNode(treeMaster.RootNode, assignedTreeNodes);
                }

                MapHistoryAndTree(assignedTreeNodes, student);
            }

            return assignedTreeNodes;
        }

        public static List<AssignedStructTreeNode> GetAssignedStructTree(TreeMaster treeMaster)
        {
            List<AssignedStructTreeNode> assignedTreeNodes = new List<AssignedStructTreeNode>();

            if (treeMaster != null && treeMaster.Id != 0)
            {
                if (treeMaster != null && treeMaster.Id != 0)
                {
                    DrillNode(treeMaster.RootNode, assignedTreeNodes);
                }
            }

            return assignedTreeNodes;
        }

        #region Left Over
        //private static void DrillDisrtibutedTree(TreeCalendarMaster treeCalMas, List<AssignedTreeNode> assignedTreeNodes)
        //{
        //    AssignedTreeNode assignedTreeNode = null;

        //    foreach (TreeCalendarDetail treeCalDet in treeCalMas.TreeCalendarDetails)
        //    {
        //        assignedTreeNode = new AssignedTreeNode();
        //        FlattenCalendarDetail(assignedTreeNode, treeCalDet);
        //        assignedTreeNodes.Add(assignedTreeNode);
        //        foreach (Cal_Course_Prog_Node cal_Course_Prog_Node in treeCalDet.Cal_Course_Prog_Nodes)
        //        {
        //            if (cal_Course_Prog_Node.CourseID != 0 && cal_Course_Prog_Node.VersionID != 0)
        //            {
        //                assignedTreeNode = new AssignedTreeNode();
        //                FlattenCourse(assignedTreeNode, cal_Course_Prog_Node.Course, cal_Course_Prog_Node.NodeCourseID);
        //                assignedTreeNodes.Add(assignedTreeNode);
        //                //}
        //            }
        //            else if (cal_Course_Prog_Node.NodeID != 0)
        //            {
        //                assignedTreeNode = new AssignedTreeNode();
        //                FlattenCalCourseProgNode(assignedTreeNode, cal_Course_Prog_Node);
        //                assignedTreeNodes.Add(assignedTreeNode);

        //                Node node = cal_Course_Prog_Node.Node;

        //                if (node.Node_Courses != null && node.IsLastLevel)
        //                {
        //                    foreach (Node_Course node_course in node.Node_Courses)
        //                    {
        //                        assignedTreeNode = new AssignedTreeNode();
        //                        FlattenCourse(assignedTreeNode, node_course.ChildCourse, node_course.Id);
        //                        assignedTreeNodes.Add(assignedTreeNode);
        //                    }
        //                }
        //                else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
        //                {
        //                    DrillNode(node, assignedTreeNodes);
        //                }
        //                else if (node.IsVirtual)
        //                {
        //                    foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
        //                    {
        //                        foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
        //                        {
        //                            if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
        //                            {
        //                                assignedTreeNode = new AssignedTreeNode();
        //                                FlattenCourse(assignedTreeNode, vNodeSet.OperandCourse, vNodeSet.NodeCourseID);
        //                                assignedTreeNodes.Add(assignedTreeNode);
        //                            }
        //                            else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
        //                            {
        //                                DrillNode(vNodeSet.OperandNode, assignedTreeNodes);
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //        }

        //    }
        //}
        #endregion

        private static void DrillNode(Node node, List<AssignedStructTreeNode> assignedTreeNodes)
        {

            AssignedStructTreeNode assignedTreeNode = null;
            if (node.Node_Courses != null && node.IsLastLevel && !node.IsVirtual)
            {
                foreach (NodeCourse node_course in node.Node_Courses)
                {
                    assignedTreeNode = new AssignedStructTreeNode();
                    FlattenCourse(assignedTreeNode, node_course);
                    assignedTreeNodes.Add(assignedTreeNode);
                }
            }
            else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
            {
                List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id);

                if (treeDetails != null && treeDetails.Count != 0)
                {
                    foreach (TreeDetail treeDetail in treeDetails)
                    {
                        assignedTreeNode = new AssignedStructTreeNode();
                        FlattenDetail(assignedTreeNode, treeDetail);
                        assignedTreeNodes.Add(assignedTreeNode);
                        DrillNode(treeDetail.ChildNode, assignedTreeNodes);
                    } 
                }
            }
            else if (node.Node_Courses == null && !node.IsLastLevel && node.IsVirtual)
            {
                if (node.VNodeSetMasters != null)
                {
                    foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
                    {
                        assignedTreeNode = new AssignedStructTreeNode();
                        FlattenVNodeMaster(assignedTreeNode, vNodeSetMas);
                        assignedTreeNodes.Add(assignedTreeNode);

                        if (vNodeSetMas.VNodeSets != null)
                        {
                            foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
                            {
                                assignedTreeNode = new AssignedStructTreeNode();
                                FlattenVNodeSet(assignedTreeNode, vNodeSet);
                                assignedTreeNodes.Add(assignedTreeNode);

                                if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
                                {
                                    assignedTreeNode = new AssignedStructTreeNode();
                                    FlattenCourse(assignedTreeNode, vNodeSet.NodeCourse);
                                    assignedTreeNodes.Add(assignedTreeNode);
                                }
                                else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
                                {
                                    DrillNode(vNodeSet.OperandNode, assignedTreeNodes);
                                }
                            } 
                        }
                    } 
                }
            }

        }

        private static void MapHistoryAndTree(List<AssignedStructTreeNode> assignedTreeNodes, Student student)
        {
            List<Student_Course> courseHistories = Student_Course.GetByStudentID(student.Id);

            foreach (AssignedStructTreeNode atnode in assignedTreeNodes)
            {
                if (atnode.LevelType == FlatAssignedStructTreeLevels.Course)
                {
                    foreach (Student_Course obj in courseHistories)
                    {
                        if (obj.NodeCourse.ChildCourseID == atnode.DataObjID && obj.NodeCourse.ChildVersionID == atnode.DataObjID2 && obj.NodeCourseID == atnode.DataObjID3)
                        {
                            if (obj.Std_CourseStatuses != null && obj.Std_SortedCourseStatuses != null)
                            {
                                if (atnode.HighestGrade == "N/A")
                                {
                                    atnode.HighestGrade = obj.HighestGrade;
                                }
                                else if (atnode.HighestGradeOrderKey > obj.HighestGradeOrderKey)
                                {
                                    atnode.HighestGrade = obj.HighestGrade;
                                }
                            }

                            if (atnode.Retakes < obj.RetakeNo)
                            {
                                atnode.Retakes = obj.RetakeNo;
                            }

                            if (atnode.HighestGrade == "N/A" || atnode.HighestGrade == "F")
                            {
                                atnode.HasCompleted = false;
                            }
                            else
                            {
                                atnode.HasCompleted = true;
                            }
                        }
                    }
                }
            }
        }


        //private static List<AssignedTreeNode> GetAssigendProgStructTree(Student student)
        //{
        //    List<AssignedTreeNode> assProgStructTree = new List<AssignedTreeNode>();

        //    //Tree

        //    return assProgStructTree;
        //}

        //private static void AutoOpen(List<AssignedTreeNode> assignedTreeNodes, Student student)
        //{
        //    List<Student_Course> courseHistories = Student_Course.GetByStudentID(student.Id);

        //    TreeMaster treeMaster = TreeMaster.Get(student.TreeMasterID);
        //    //treeMaster.RootNode.

        //    foreach (AssignedTreeNode atnode in assignedTreeNodes)
        //    {
        //        if (atnode.LevelType == FlatAssignedTreeLevels.Node)
        //        {
        //            Node node = Node.GetNode(atnode.DataObjID);

        //            if (node.HasPreriquisite)
        //            {
        //                if (node.PreReqMasters != null)
        //                {
        //                    foreach (PreRequisiteMaster master in node.PreReqMasters)
        //                    {
        //                        foreach (PreReqDetail detail in master.PreReqDetailNodes)
        //                        {

        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (atnode.LevelType == FlatAssignedTreeLevels.Course)
        //        {
        //            foreach (Student_Course obj in courseHistories)
        //            {
        //                if (obj.NodeCourse.ChildCourseID == atnode.DataObjID && obj.NodeCourse.ChildVersionID == atnode.DataObjID2)
        //                {


        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
