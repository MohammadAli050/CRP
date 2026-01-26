using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using DataAccess;

namespace BussinessObject
{
    public class AssignedTreeNode : Base
    {

        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private FlatAssignedTreeLevels _type;
        public FlatAssignedTreeLevels LevelType
        {
            get { return _type; }
            set { _type = value; }
        }

        private object _dataObj;
        public object DataObj
        {
            get { return _dataObj; }
            set { _dataObj = value; }
        }

        private string _dataObjTypeName;
        public string DataObjTypeName
        {
            get { return _dataObjTypeName; }
            set { _dataObjTypeName = value; }
        }

        private bool? _isVirtual;
        public bool? IsVirtual
        {
            get { return _isVirtual; }
            set { _isVirtual = value; }
        }

        private bool? _isLastLevel;
        public bool? IsLastLevel
        {
            get { return _isLastLevel; }
            set { _isLastLevel = value; }
        }

        private int _dataObjID;
        public int DataObjID
        {
            get { return _dataObjID; }
            set { _dataObjID = value; }
        }

        private int _dataObjID2;
        public int DataObjID2
        {
            get { return _dataObjID2; }
            set { _dataObjID2 = value; }
        }

        private int _dataObjID3;
        public int DataObjID3
        {
            get { return _dataObjID3; }
            set { _dataObjID3 = value; }
        }

        private int? _retakes;
        public int? Retakes
        {
            get { return _retakes; }
            set { _retakes = value; }
        }

        private string _retakeHistory;
        public string RetakeHistory
        {
            get { return _retakeHistory; }
            set { _retakeHistory = value; }
        }

        private int? _priority;
        public int? Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private string _highestGrade;
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

        private bool? _canRetake;
        public bool? CanRetake
        {
            get { return _canRetake; }
            set { _canRetake = value; }
        }

        private bool _isOffered;
        public bool IsOffered
        {
            get { return _isOffered; }
            set { _isOffered = value; }
        }

        private bool _isAssigned;
        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { _isAssigned = value; }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }

        private bool? _isEligible;
        public bool? IsEligible
        {
            get { return _isEligible; }
            set { _isEligible = value; }
        }

        private bool? _hasCompleted;
        public bool? HasCompleted
        {
            get { return _hasCompleted; }
            set { _hasCompleted = value; }
        }

        private bool? _hasMultispan;
        public bool? HasMultispan
        {
            get { return _hasMultispan; }
            set { _hasMultispan = value; }
        }
        private int? _multiSpanMasID;
        public int? MultiSpanMasID
        {
            get { return _multiSpanMasID; }
            set { _multiSpanMasID = value; }
        }
        private bool? _preRequisitDone;
        public bool? PreRequisitDone
        {
            get { return _preRequisitDone; }
            set { _preRequisitDone = value; }
        }

        private List<AcademicCalenderSection> _academicCalSectns;
        public List<AcademicCalenderSection> AcademicCalSectns
        {
            get { return _academicCalSectns; }
            set { _academicCalSectns = value; }
        }
        #endregion

        #region Constructor
        public AssignedTreeNode()
            : base()
        {
            _name = string.Empty;
            _type = FlatAssignedTreeLevels.Node;
            _dataObj = null;
            _dataObjTypeName = null;
            _isVirtual = null;
            _isLastLevel = null;
            _dataObjID = 0;
            _dataObjID2 = 0;
            _retakes = null;
            _priority = null;
            _highestGrade = string.Empty;
            _canRetake = null;
            _isOffered = false;
            _isAssigned = false;
            _isOpen = false;
            _isEligible = null;
            _hasCompleted = null;
            _preRequisitDone = null;
            _hasMultispan = false;
            _multiSpanMasID = null;
        }
        #endregion

        #region Methods
        public static void FlattenCalendarDetail(AssignedTreeNode assignedTreeNode, TreeCalendarDetail treeCalDet)
        {
            assignedTreeNode.Name = treeCalDet.CalendarDetail.Name;
            assignedTreeNode.DataObj = treeCalDet;
            assignedTreeNode.DataObjID = treeCalDet.Id;
            assignedTreeNode.DataObjTypeName = "TreeCalendarDetail";
            assignedTreeNode.LevelType = FlatAssignedTreeLevels.CalendarDetail;
        }

        #region Old
        //public static void FlattenCourse(AssignedTreeNode assignedTreeNode, Course course)
        //{
        //    assignedTreeNode.Name = course.FullCodeAndCourse;
        //    assignedTreeNode.DataObj = course;
        //    assignedTreeNode.DataObjID = course.Id;
        //    assignedTreeNode.DataObjID2 = course.VersionID;
        //    assignedTreeNode.DataObjTypeName = "Course";
        //    assignedTreeNode.LevelType = FlatAssignedTreeLevels.Course;
        //    assignedTreeNode.HighestGrade = BOConstants.Grades[0].ToString();
        //}

        //public static void FlattenCourse(AssignedTreeNode assignedTreeNode, Course course,int node_courseID)
        //{
        //    assignedTreeNode.Name = course.FullCodeAndCourse;
        //    assignedTreeNode.DataObj = course;
        //    assignedTreeNode.DataObjID = course.Id;
        //    assignedTreeNode.DataObjID2 = course.VersionID;
        //    assignedTreeNode.DataObjID3 = node_courseID;
        //    assignedTreeNode.DataObjTypeName = "Course";
        //    assignedTreeNode.LevelType = FlatAssignedTreeLevels.Course;
        //    assignedTreeNode.HighestGrade = BOConstants.Grades[0].ToString();
        //} 
        #endregion

        public static void FlattenCourse(AssignedTreeNode assignedTreeNode, Course course, int node_courseID, int priority)
        {
            assignedTreeNode.Name = course.FullCodeAndCourse;
            assignedTreeNode.DataObj = course;
            assignedTreeNode.DataObjID = course.Id;
            assignedTreeNode.DataObjID2 = course.VersionID;
            assignedTreeNode.DataObjID3 = node_courseID;
            assignedTreeNode.DataObjTypeName = "Course";
            assignedTreeNode.LevelType = FlatAssignedTreeLevels.Course;
            assignedTreeNode.HighestGrade = BOConstants.Grades[0].ToString();
            assignedTreeNode.Priority = priority;
            assignedTreeNode.HasMultispan = course.HasMultipleACUSpan;
            assignedTreeNode.MultiSpanMasID = course.CourseACUSpanMasID;
        }

        public static void FlattenCalCourseProgNode(AssignedTreeNode assignedTreeNode, Cal_Course_Prog_Node cal_course_prog_node)
        {
            assignedTreeNode.Name = cal_course_prog_node.NodeLinkName + " " + cal_course_prog_node.Node.Name;
            assignedTreeNode.DataObj = cal_course_prog_node;
            assignedTreeNode.DataObjID = cal_course_prog_node.Id;
            assignedTreeNode.DataObjTypeName = "Cal_Course_Prog_Node";
            assignedTreeNode.LevelType = FlatAssignedTreeLevels.CalCourseProgNode;
            assignedTreeNode.Priority = cal_course_prog_node.Priority;
        }

        public static void FlattenNode(AssignedTreeNode assignedTreeNode, Node node)
        {
            assignedTreeNode.Name = node.Name;
            assignedTreeNode.DataObj = node;
            assignedTreeNode.DataObjID = node.Id;
            assignedTreeNode.DataObjTypeName = "Node";
            assignedTreeNode.LevelType = FlatAssignedTreeLevels.Node;
        }

        public static List<AssignedTreeNode> GetAssignedTree(Student student)
        {
            List<AssignedTreeNode> assignedTreeNodes = new List<AssignedTreeNode>();

            if (student != null && student.Id != 0)
            {

                TreeMaster treeMaster = null;
                TreeCalendarMaster treeCalMas = null;
                //RemoveFromSession(SESSIONSTUDENT);
                //AddToSession(SESSIONSTUDENT, student);
                treeMaster = TreeMaster.Get(student.TreeMasterID);
                if (treeMaster != null && treeMaster.Id != 0)
                {
                    //RemoveFromSession(SESSIONTREEMASTER);
                    //AddToSession(SESSIONTREEMASTER, treeMaster);
                    if (TreeCalendarMaster.GetByTreeMaster(treeMaster.Id) != null)
                    {
                        if (TreeCalendarMaster.GetByTreeMaster(treeMaster.Id)[0] != null)
                        {
                            treeCalMas = TreeCalendarMaster.GetByTreeMaster(treeMaster.Id)[0];
                            if (treeCalMas != null && treeCalMas.Id != 0)
                            {
                                //RemoveFromSession(SESSIONTREECALMASTER);
                                //AddToSession(SESSIONTREECALMASTER, treeCalMas);

                                DrillDisrtibutedTree(treeCalMas, assignedTreeNodes);
                            }
                        }
                    }
                }
            }

            MapHistoryAndTree(assignedTreeNodes, student);
            MapOffering(assignedTreeNodes, student);
            MapEligibility(assignedTreeNodes, student);
            return assignedTreeNodes;
        }

        public static List<AssignedTreeNode> GetAssignedTree(string roll)
        {
            List<AssignedTreeNode> assignedTreeNodes = new List<AssignedTreeNode>();
            Student student = Student.GetStudentByRoll(roll);

            if (student != null && student.Id != 0)
            {

                TreeMaster treeMaster = null;
                TreeCalendarMaster treeCalMas = null;
                //RemoveFromSession(SESSIONSTUDENT);
                //AddToSession(SESSIONSTUDENT, student);
                treeMaster = TreeMaster.Get(student.TreeMasterID);
                if (treeMaster != null && treeMaster.Id != 0)
                {
                    //RemoveFromSession(SESSIONTREEMASTER);
                    //AddToSession(SESSIONTREEMASTER, treeMaster);
                    if (TreeCalendarMaster.GetByTreeMaster(treeMaster.Id) != null)
                    {
                        if (TreeCalendarMaster.GetByTreeMaster(treeMaster.Id)[0] != null)
                        {
                            treeCalMas = TreeCalendarMaster.GetByTreeMaster(treeMaster.Id)[0];
                            if (treeCalMas != null && treeCalMas.Id != 0)
                            {
                                //RemoveFromSession(SESSIONTREECALMASTER);
                                //AddToSession(SESSIONTREECALMASTER, treeCalMas);

                                DrillDisrtibutedTree(treeCalMas, assignedTreeNodes);
                            }
                        }
                    }
                }
            }

            MapHistoryAndTree(assignedTreeNodes, student);

            return assignedTreeNodes;
        }

        public static List<AssignedTreeNode> GetAssignedTree(TreeCalendarMaster treeCalMas)
        {
            List<AssignedTreeNode> assignedTreeNodes = new List<AssignedTreeNode>();
            if (treeCalMas != null && treeCalMas.Id != 0)
            {
                DrillDisrtibutedTree(treeCalMas, assignedTreeNodes);
            }
            return assignedTreeNodes;
        }

        private static void DrillDisrtibutedTree(TreeCalendarMaster treeCalMas, List<AssignedTreeNode> assignedTreeNodes)
        {
            AssignedTreeNode assignedTreeNode = null;
            int priority = 0;

            foreach (TreeCalendarDetail treeCalDet in treeCalMas.TreeCalendarDetails)
            {
                assignedTreeNode = new AssignedTreeNode();
                FlattenCalendarDetail(assignedTreeNode, treeCalDet);
                assignedTreeNodes.Add(assignedTreeNode);
                foreach (Cal_Course_Prog_Node cal_Course_Prog_Node in treeCalDet.Cal_Course_Prog_Nodes)
                {
                    priority = cal_Course_Prog_Node.Priority;
                    if (cal_Course_Prog_Node.CourseID != 0 && cal_Course_Prog_Node.VersionID != 0)
                    {

                        assignedTreeNode = new AssignedTreeNode();
                        FlattenCourse(assignedTreeNode, cal_Course_Prog_Node.Course, cal_Course_Prog_Node.NodeCourseID, priority);
                        assignedTreeNodes.Add(assignedTreeNode);
                    }
                    else if (cal_Course_Prog_Node.NodeID != 0)
                    {
                        assignedTreeNode = new AssignedTreeNode();
                        FlattenCalCourseProgNode(assignedTreeNode, cal_Course_Prog_Node);
                        assignedTreeNodes.Add(assignedTreeNode);

                        Node node = cal_Course_Prog_Node.Node;

                        if (node.Node_Courses != null && node.IsLastLevel)
                        {
                            foreach (NodeCourse node_course in node.Node_Courses)
                            {
                                assignedTreeNode = new AssignedTreeNode();
                                FlattenCourse(assignedTreeNode, node_course.ChildCourse, node_course.Id, priority);
                                assignedTreeNodes.Add(assignedTreeNode);
                            }
                        }
                        else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
                        {
                            DrillNode(node, assignedTreeNodes, priority);
                        }
                        else if (node.IsVirtual)
                        {
                            foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
                            {
                                foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
                                {
                                    if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
                                    {
                                        assignedTreeNode = new AssignedTreeNode();
                                        FlattenCourse(assignedTreeNode, vNodeSet.OperandCourse, vNodeSet.NodeCourseID, priority);
                                        assignedTreeNodes.Add(assignedTreeNode);
                                    }
                                    else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
                                    {
                                        DrillNode(vNodeSet.OperandNode, assignedTreeNodes, priority);
                                    }
                                }
                            }
                        }

                    }
                }

            }
        }

        #region Old
        //private static void DrillNode(Node node, List<AssignedTreeNode> assignedTreeNodes)
        //{

        //    AssignedTreeNode assignedTreeNode = null;
        //    if (node.Node_Courses != null && node.IsLastLevel && !node.IsVirtual)
        //    {
        //        foreach (Node_Course node_course in node.Node_Courses)
        //        {
        //            //if (node_course.ChildCourse.IsActive)
        //            //{
        //                //drow = dt.NewRow();
        //                //drow["Data"] = node_course.ChildCourse.FullCodeAndCourse;
        //                //dt.Rows.Add(drow);
        //                assignedTreeNode = new AssignedTreeNode();
        //                FlattenCourse(assignedTreeNode, node_course.ChildCourse, node_course.Id);
        //                assignedTreeNodes.Add(assignedTreeNode);
        //            //}
        //        }
        //    }
        //    else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
        //    {
        //        List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id);

        //        foreach (TreeDetail treeDetail in treeDetails)
        //        {
        //            //drow = dt.NewRow();
        //            //drow["Data"] = treeDetail.ChildNode.Name;
        //            //dt.Rows.Add(drow);
        //            assignedTreeNode = new AssignedTreeNode();
        //            FlattenNode(assignedTreeNode, treeDetail.ChildNode);
        //            assignedTreeNodes.Add(assignedTreeNode);
        //            DrillNode(treeDetail.ChildNode, assignedTreeNodes);
        //        }
        //    }
        //    else if (node.Node_Courses == null && !node.IsLastLevel && node.IsVirtual)
        //    {
        //        foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
        //        {
        //            foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
        //            {
        //                if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
        //                {
        //                    //if (vNodeSet.OperandCourse.IsActive)
        //                    //{
        //                        //drow = dt.NewRow();
        //                        //drow["Data"] = vNodeSet.OperandCourse.FullCodeAndCourse;
        //                        //dt.Rows.Add(drow);
        //                        assignedTreeNode = new AssignedTreeNode();
        //                        FlattenCourse(assignedTreeNode, vNodeSet.OperandCourse,vNodeSet.NodeCourseID);
        //                        assignedTreeNodes.Add(assignedTreeNode);
        //                    //}
        //                }
        //                else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
        //                {
        //                    DrillNode(vNodeSet.OperandNode, assignedTreeNodes);
        //                }
        //            }
        //        }
        //    }

        //} 
        #endregion

        private static void DrillNode(Node node, List<AssignedTreeNode> assignedTreeNodes, int priority)
        {

            AssignedTreeNode assignedTreeNode = null;
            if (node.Node_Courses != null && node.IsLastLevel && !node.IsVirtual)
            {
                foreach (NodeCourse node_course in node.Node_Courses)
                {
                    assignedTreeNode = new AssignedTreeNode();
                    FlattenCourse(assignedTreeNode, node_course.ChildCourse, node_course.Id, priority);
                    assignedTreeNodes.Add(assignedTreeNode);
                }
            }
            else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
            {
                List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id);

                foreach (TreeDetail treeDetail in treeDetails)
                {
                    assignedTreeNode = new AssignedTreeNode();
                    FlattenNode(assignedTreeNode, treeDetail.ChildNode);
                    assignedTreeNodes.Add(assignedTreeNode);
                    DrillNode(treeDetail.ChildNode, assignedTreeNodes, priority);
                }
            }
            else if (node.Node_Courses == null && !node.IsLastLevel && node.IsVirtual)
            {
                foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
                {
                    foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
                    {
                        if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
                        {
                            assignedTreeNode = new AssignedTreeNode();
                            FlattenCourse(assignedTreeNode, vNodeSet.OperandCourse, vNodeSet.NodeCourseID, priority);
                            assignedTreeNodes.Add(assignedTreeNode);
                        }
                        else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
                        {
                            DrillNode(vNodeSet.OperandNode, assignedTreeNodes, priority);
                        }
                    }
                }
            }

        }

        private static void MapHistoryAndTree(List<AssignedTreeNode> assignedTreeNodes, Student student)
        {
            List<Student_Course> courseHistories = Student_Course.GetByStudentID(student.Id);
            string strPrevGrade = "";
            if (courseHistories != null)
            {
                foreach (AssignedTreeNode atnode in assignedTreeNodes)
                {
                    if (atnode.LevelType == FlatAssignedTreeLevels.Course)
                    {
                        foreach (Student_Course obj in courseHistories)
                        {
                            if (obj.NodeCourse.ChildCourseID == atnode.DataObjID && obj.NodeCourse.ChildVersionID == atnode.DataObjID2 && obj.NodeCourseID == atnode._dataObjID3)
                            {
                                if (obj.Std_CourseStatuses != null && obj.Std_SortedCourseStatuses != null)
                                {
                                    if (atnode.HighestGrade == "N/A")
                                    {
                                        atnode.HighestGrade = obj.HighestGrade;
                                    }
                                    else if (atnode.HighestGradeOrderKey > obj.HighestGradeOrderKey)
                                    {
                                        strPrevGrade = atnode.HighestGrade;
                                        atnode.HighestGrade = obj.HighestGrade;
                                    }
                                }

                                if (!atnode.Retakes.HasValue)
                                {
                                    atnode.Retakes = 0;
                                }
                                else if (atnode.Retakes.HasValue && atnode.Retakes >= 0)
                                {
                                    atnode.Retakes++;

                                    //Adding retake history
                                    atnode.RetakeHistory = ((atnode.RetakeHistory == "") ? "" : atnode.RetakeHistory) + "\r\n" + atnode.Retakes.ToString() + " . Grade: '" + strPrevGrade + "'  ";
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
        }

        private static void MapOffering(List<AssignedTreeNode> assignedTreeNodes, Student student)
        {
            AcademicCalender nextACU = AcademicCalender.GetNext();
            List<OfferedCourse> offerings = OfferedCourse.GetOfferedCourse(nextACU.Id, Program.GetProgram(student.ProgramID).DeptID, student.ProgramID, 0);

            if (offerings != null)
            {
                foreach (AssignedTreeNode atnode in assignedTreeNodes)
                {
                    if (atnode.LevelType == FlatAssignedTreeLevels.Course)
                    {
                        foreach (OfferedCourse obj in offerings)
                        {
                            if (obj.CourseID == atnode.DataObjID && obj.VersionID == atnode.DataObjID2)
                            {
                                atnode.IsOpen = true;
                            }
                        }
                    }
                }
            }
        }

        private static void MapEligibility(List<AssignedTreeNode> assignedTreeNodes, Student student)
        {
            DeptRegSetUp deptRegSet = DeptRegSetUp.GetBProgramID(student.ProgramID);

            Student_ACUDetail last = Student_ACUDetail.GetMaxByStudentID(student.Id);//last will be null if he is a new student

            decimal locality = 0;
            decimal man = 0;
            decimal max = 0;

            #region Locality
            //if (deptRegSet.LocalCGPA1 != null && deptRegSet.LocalCredit1 != null)
            //{
            //    if (last.CGPA <= deptRegSet.LocalCGPA1 && last.CGPA > deptRegSet.LocalCGPA2)
            //    {
            //        locality = deptRegSet.LocalCredit1.Value;
            //    }
            //}
            //if (deptRegSet.LocalCGPA2 != null && deptRegSet.LocalCredit2 != null)
            //{
            //    if (last.CGPA <= deptRegSet.LocalCGPA2 && last.CGPA > deptRegSet.LocalCGPA3)
            //    {
            //        locality = deptRegSet.LocalCredit2.Value;
            //    }
            //}
            //if (deptRegSet.LocalCGPA3 != null && deptRegSet.LocalCredit3 != null)
            //{
            //    if (last.CGPA <= deptRegSet.LocalCGPA3 && last.CGPA > 0)
            //    {
            //        locality = deptRegSet.LocalCredit3.Value;
            //    }
            //}


            //start----written by saima
            if (last != null && deptRegSet.LocalCGPA1 != null && deptRegSet.LocalCredit1 != null)
            {
                if (last.CGPA >= deptRegSet.LocalCGPA1)
                {
                    locality = deptRegSet.LocalCredit1.Value;
                }
            }
            if (last != null && deptRegSet.LocalCGPA2 != null && deptRegSet.LocalCredit2 != null)
            {
                if (last.CGPA >= deptRegSet.LocalCGPA2 && last.CGPA < deptRegSet.LocalCGPA1)
                {
                    locality = deptRegSet.LocalCredit2.Value;
                }
            }
            if (deptRegSet.LocalCGPA3 != null && deptRegSet.LocalCredit3 != null)
            {
                //if (last == null || (last.CGPA >= deptRegSet.LocalCGPA3 && last.CGPA < deptRegSet.LocalCGPA2))
                //{
                //    locality = deptRegSet.LocalCredit3.Value;
                //}                
                locality = deptRegSet.LocalCredit3.Value;
            }
            //end by saima

            decimal count = 0;

            for (; locality > count; )
            {
                //List<AssignedTreeNode> atnodes = null;
                //    var tempnodes = from innernode in assignedTreeNodes
                //                   where innernode.IsEligible == null
                //                   && innernode.LevelType == FlatAssignedTreeLevels.Course
                //                   && innernode.PreRequisitDone == null
                //                   && innernode.HasCompleted != true
                //                   && innernode.IsAssigned == false
                //                   && innernode.IsOffered == false
                //                   && innernode.Priority != null
                //                   select innernode;
                //    atnodes = tempnodes.ToList<AssignedTreeNode>();

                var elno = from node in assignedTreeNodes
                           where node.Priority == (from innernode in assignedTreeNodes
                                                   where innernode.IsEligible == null
                                                   && innernode.LevelType == FlatAssignedTreeLevels.Course
                                                   && innernode.PreRequisitDone == null
                                                   && innernode.HasCompleted != true
                                                   && innernode.IsAssigned == false
                                                   && innernode.IsOffered == false
                                                   && innernode.Priority != null
                                                   select innernode.Priority).Min()
                                                   && node.LevelType == FlatAssignedTreeLevels.Course
                                                   && node.Priority != null
                           select node;

                if (elno != null && elno.ToList<AssignedTreeNode>().Count > 0)
                {
                    //AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0];

                    //var calno = from node in assignedTreeNodes
                    //            where node.Priority == atnode.Priority
                    //            && node.LevelType == FlatAssignedTreeLevels.CalCourseProgNode
                    //            select node;
                    //AssignedTreeNode ccpNnode = null;

                    //if (calno != null && calno.ToList<AssignedTreeNode>().Count > 0)
                    //{
                    //    ccpNnode = calno.ToList<AssignedTreeNode>()[0];
                    //}

                    //CheckPrerequisite(atnode, student, ccpNnode);
                    //var elinode = from innernode in assignedTreeNodes
                    //              where innernode.IsEligible == true
                    //              && innernode.LevelType == FlatAssignedTreeLevels.Course
                    //              && innernode.PreRequisitDone == true
                    //              && innernode.HasCompleted != true
                    //              && innernode.IsAssigned == false
                    //              && innernode.IsOffered == false
                    //              && innernode.Priority != null
                    //              select innernode;
                    //List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                    //count = 0;
                    //foreach (AssignedTreeNode item in elinodes)
                    //{
                    //    Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                    //    count = count + course.Credits;
                    //}


                    //written by saima
                    //purpose: in course distribution, a node holds multiple courses. the node's priority implies to those courses. as they have same priority, only "AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0]" will not work
                    for (int i = 0; i < elno.ToList<AssignedTreeNode>().Count; i++)
                    {
                        AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[i];

                        var calno = from node in assignedTreeNodes
                                    where node.Priority == atnode.Priority
                                    && node.LevelType == FlatAssignedTreeLevels.CalCourseProgNode
                                    select node;
                        AssignedTreeNode ccpNnode = null;

                        if (calno != null && calno.ToList<AssignedTreeNode>().Count > 0)
                        {
                            ccpNnode = calno.ToList<AssignedTreeNode>()[0];
                        }

                        CheckPrerequisite(atnode, student, ccpNnode);
                        var elinode = from innernode in assignedTreeNodes
                                      where innernode.IsEligible == true
                                      && innernode.LevelType == FlatAssignedTreeLevels.Course
                                      && innernode.PreRequisitDone == true
                                      && innernode.HasCompleted != true
                                      && innernode.IsAssigned == false
                                      && innernode.IsOffered == false
                                      && innernode.Priority != null
                                      select innernode;
                        List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                        count = 0;
                        foreach (AssignedTreeNode item in elinodes)
                        {
                            Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                            count = count + course.Credits;
                        }
                    }
                    //end by saima
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Mandatory
            //if (deptRegSet.ManCGPA1 != null && deptRegSet.ManCredit1 != null)
            //{
            //    if (last.CGPA <= deptRegSet.ManCGPA1 && last.CGPA > deptRegSet.ManCGPA2)
            //    {
            //        man = deptRegSet.ManCredit1.Value;
            //    }
            //}
            //if (deptRegSet.ManCGPA2 != null && deptRegSet.ManCredit2 != null)
            //{
            //    if (last.CGPA <= deptRegSet.ManCGPA2 && last.CGPA > deptRegSet.ManCGPA3)
            //    {
            //        man = deptRegSet.ManCredit2.Value;
            //    }
            //}
            //if (deptRegSet.ManCGPA3 != null && deptRegSet.ManCredit3 != null)
            //{
            //    if (last.CGPA <= deptRegSet.ManCGPA3 && last.CGPA > 0)
            //    {
            //        man = deptRegSet.ManCredit3.Value;
            //    }
            //}

            //written by saima
            if (last != null && deptRegSet.ManCGPA1 != null && deptRegSet.ManCredit1 != null)
            {
                if (last.CGPA >= deptRegSet.ManCGPA1)
                {
                    man = deptRegSet.ManCredit1.Value;
                }
            }
            if (last != null && deptRegSet.ManCGPA2 != null && deptRegSet.ManCredit2 != null)
            {
                if (last.CGPA >= deptRegSet.ManCGPA2 && last.CGPA < deptRegSet.ManCGPA1)
                {
                    man = deptRegSet.ManCredit2.Value;
                }
            }
            if (deptRegSet.ManCGPA3 != null && deptRegSet.ManCredit3 != null)
            {
                //if (last.CGPA >= deptRegSet.ManCGPA3 && last.CGPA < deptRegSet.ManCGPA2)
                //{
                //    man = deptRegSet.ManCredit3.Value;
                //}
                man = deptRegSet.ManCredit3.Value;
            }
            //end by saima

            count = 0;

            for (; man > count; )
            {
                var elno = from node in assignedTreeNodes
                           where node.Priority == (from innernode in assignedTreeNodes
                                                   where innernode.IsEligible == true
                                                   && innernode.LevelType == FlatAssignedTreeLevels.Course
                                                   && innernode.PreRequisitDone == true
                                                   && innernode.HasCompleted != true
                                                   && innernode.IsAssigned == false
                                                   && innernode.IsOffered == false
                                                   && innernode.Priority != null
                                                   select innernode.Priority).Min()
                                                   && node.LevelType == FlatAssignedTreeLevels.Course
                                                   && node.Priority != null
                           select node;

                if (elno != null && elno.ToList<AssignedTreeNode>().Count > 0)
                {
                    //AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0];

                    //atnode.IsAssigned = true;

                    //var elinode = from innernode in assignedTreeNodes
                    //              where innernode.IsEligible == true
                    //              && innernode.LevelType == FlatAssignedTreeLevels.Course
                    //              && innernode.PreRequisitDone == true
                    //              && innernode.HasCompleted != true
                    //              && innernode.IsAssigned == true
                    //              && innernode.IsOffered == false
                    //              && innernode.Priority != null
                    //              select innernode;
                    //List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                    //count = 0;
                    //foreach (AssignedTreeNode item in elinodes)
                    //{
                    //    Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                    //    count = count + course.Credits;
                    //}


                    //written by saima
                    //purpose: in course distribution, a node holds multiple courses. the node's priority implies to those courses. as they have same priority, only "AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0]" will not work
                    for (int i = 0; i < elno.ToList<AssignedTreeNode>().Count; i++)
                    {
                        AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[i];

                        atnode.IsAssigned = true;

                        var elinode = from innernode in assignedTreeNodes
                                      where innernode.IsEligible == true
                                      && innernode.LevelType == FlatAssignedTreeLevels.Course
                                      && innernode.PreRequisitDone == true
                                      && innernode.HasCompleted != true
                                      && innernode.IsAssigned == true
                                      && innernode.IsOffered == false
                                      && innernode.Priority != null
                                      select innernode;
                        List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                        count = 0;
                        foreach (AssignedTreeNode item in elinodes)
                        {
                            Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                            count = count + course.Credits;
                        }
                    }
                    //end by saima
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Max
            //if (deptRegSet.MaxCGPA1 != null && deptRegSet.MaxCredit1 != null)
            //{
            //    if (last.CGPA <= deptRegSet.MaxCGPA1 && last.CGPA > deptRegSet.MaxCGPA2)
            //    {
            //        max = deptRegSet.MaxCredit1.Value;
            //    }
            //}
            //if (deptRegSet.MaxCGPA2 != null && deptRegSet.MaxCredit2 != null)
            //{
            //    if (last.CGPA <= deptRegSet.MaxCGPA2 && last.CGPA > deptRegSet.MaxCGPA3)
            //    {
            //        max = deptRegSet.MaxCredit2.Value;
            //    }
            //}
            //if (deptRegSet.MaxCGPA3 != null && deptRegSet.MaxCredit3 != null)
            //{
            //    if (last.CGPA <= deptRegSet.MaxCGPA3 && last.CGPA > 0)
            //    {
            //        max = deptRegSet.MaxCredit3.Value;
            //    }
            //}


            //written by saima
            if (last != null && deptRegSet.MaxCGPA1 != null && deptRegSet.MaxCredit1 != null)
            {
                if (last.CGPA >= deptRegSet.MaxCGPA1)
                {
                    max = deptRegSet.MaxCredit1.Value;
                }
            }
            if (last != null && deptRegSet.MaxCGPA2 != null && deptRegSet.MaxCredit2 != null)
            {
                if (last.CGPA >= deptRegSet.MaxCGPA2 && last.CGPA < deptRegSet.MaxCGPA1)
                {
                    max = deptRegSet.MaxCredit2.Value;
                }
            }
            if (deptRegSet.MaxCGPA3 != null && deptRegSet.MaxCredit3 != null)
            {
                //if (last.CGPA >= deptRegSet.MaxCGPA3 && last.CGPA < deptRegSet.MaxCGPA2)
                //{
                //    max = deptRegSet.MaxCredit3.Value;
                //}
                max = deptRegSet.MaxCredit3.Value;
            }
            //end by saima

            count = 0;

            for (; max > count; )
            {
                var elno = from node in assignedTreeNodes
                           where node.Priority == (from innernode in assignedTreeNodes
                                                   where innernode.IsEligible == true
                                                   && innernode.LevelType == FlatAssignedTreeLevels.Course
                                                   && innernode.PreRequisitDone == true
                                                   && innernode.HasCompleted != true
                                                   && innernode.IsAssigned == false
                                                   && innernode.IsOpen == true
                                                   && innernode.IsOffered == false
                                                   && innernode.Priority != null
                                                   select innernode.Priority).Min()
                                                   && node.LevelType == FlatAssignedTreeLevels.Course
                                                   && node.Priority != null
                           select node;

                if (elno != null && elno.ToList<AssignedTreeNode>().Count > 0)
                {
                    //AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0];

                    //atnode.IsOffered = true;

                    //var elinode = from innernode in assignedTreeNodes
                    //              where innernode.IsEligible == true
                    //              && innernode.LevelType == FlatAssignedTreeLevels.Course
                    //              && innernode.PreRequisitDone == true
                    //              && innernode.HasCompleted != true
                    //              && innernode.IsAssigned == false
                    //              && innernode.IsOpen == true
                    //              && innernode.IsOffered == true
                    //              && innernode.Priority != null
                    //              select innernode;
                    //List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                    //count = 0;
                    //foreach (AssignedTreeNode item in elinodes)
                    //{
                    //    Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                    //    count = count + course.Credits;
                    //}


                    //written by saima
                    //purpose: in course distribution, a node holds multiple courses. the node's priority implies to those courses. as they have same priority, only "AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[0]" will not work
                    for (int i = 0; i < elno.ToList<AssignedTreeNode>().Count; i++)
                    {
                        AssignedTreeNode atnode = elno.ToList<AssignedTreeNode>()[i];

                        atnode.IsOffered = true;

                        var elinode = from innernode in assignedTreeNodes
                                      where innernode.IsEligible == true
                                      && innernode.LevelType == FlatAssignedTreeLevels.Course
                                      && innernode.PreRequisitDone == true
                                      && innernode.HasCompleted != true
                                      && innernode.IsAssigned == false
                                      && innernode.IsOpen == true
                                      && innernode.IsOffered == true
                                      && innernode.Priority != null
                                      select innernode;
                        List<AssignedTreeNode> elinodes = elinode.ToList<AssignedTreeNode>();
                        count = 0;
                        foreach (AssignedTreeNode item in elinodes)
                        {
                            Course course = Course.GetCourse(item._dataObjID, item.DataObjID2);

                            count = count + course.Credits;
                        }
                    }

                    //end by saima
                }
                else
                {
                    break;
                }
            }
            #endregion
        }

        private static void CheckPrerequisite(AssignedTreeNode assignedTreeNode, Student student, AssignedTreeNode assignedTreeNodeCCPN)
        {
            //bool isNodeValReq = false;
            //bool isNodePreReqDone=false;
            if (assignedTreeNodeCCPN != null)
            {
                Cal_Course_Prog_Node ccpn = Cal_Course_Prog_Node.Get(assignedTreeNodeCCPN.DataObjID);

                if (ccpn.NodeID != 0)
                {
                    //isNodeValReq = true;
                    Node node = Node.GetNode(ccpn.NodeID);

                }
            }

            assignedTreeNode.IsEligible = true;
            assignedTreeNode.PreRequisitDone = true;
        }

        private static bool CheckNodePreReq(Node node, TreeMaster treemas)
        {
            bool valid = true;




            if (node.IsLastLevel)
            {
                //List
            }
            else if (node.IsVirtual)
            {
            }

            return valid;
        }

        private static List<AssignedTreeNode> GetAssigendProgStructTree(Student student)
        {
            List<AssignedTreeNode> assProgStructTree = new List<AssignedTreeNode>();

            //Tree

            return assProgStructTree;
        }

        private static void AutoOpen(List<AssignedTreeNode> assignedTreeNodes, Student student)
        {
            List<Student_Course> courseHistories = Student_Course.GetByStudentID(student.Id);

            TreeMaster treeMaster = TreeMaster.Get(student.TreeMasterID);
            //treeMaster.RootNode.

            foreach (AssignedTreeNode atnode in assignedTreeNodes)
            {
                if (atnode.LevelType == FlatAssignedTreeLevels.Node)
                {
                    Node node = Node.GetNode(atnode._dataObjID);

                    if (node.HasPreriquisite)
                    {
                        if (node.PreReqMasters != null)
                        {
                            foreach (PreRequisiteMaster master in node.PreReqMasters)
                            {
                                foreach (PreReqDetail detail in master.PreReqDetailNodes)
                                {

                                }
                            }
                        }
                    }
                }

                if (atnode.LevelType == FlatAssignedTreeLevels.Course)
                {
                    foreach (Student_Course obj in courseHistories)
                    {
                        if (obj.NodeCourse.ChildCourseID == atnode.DataObjID && obj.NodeCourse.ChildVersionID == atnode.DataObjID2)
                        {


                        }
                    }
                }
            }
        }
        #endregion
    }
}
