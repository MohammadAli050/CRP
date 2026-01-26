using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class Cal_Course_Prog_NodeEntity : BaseEntity
    {
        #region Varibales
        private int _treeCalendarDetailID;
        //private TreeCalendarDetail _treeCalenderDetail;
        ////private int _treeMasterID;
        ////private TreeMaster _treeMaster;
        ////private int _calendarMasterID;
        ////private CalendarUnitMaster _calendarMaster;
        private int _programID;
        //private Program _program;
        private int _nodeID;
        //private Node _node;
        private int _courseID;
        private int _versionID;
        //private Course _course;
        private int _nodeCourseID;
        private int _priority;
        //private Node_Course _nodeCourse;

        private string _nodeLinkName;
        #endregion

        #region Properties

        public int TreeCalendarDetailID
        {
            get { return _treeCalendarDetailID; }
            set { _treeCalendarDetailID = value; }
        }
        //public TreeCalendarDetail TreeCalenderDetail
        //{
        //    get { return _treeCalenderDetail; }
        //    private set { _treeCalenderDetail = value; }
        //}

        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        //public Program Program
        //{
        //    get
        //    {
        //        if (_program == null)
        //        {
        //            if (this.ProgramID > 0)
        //            {
        //                _program = Program.GetProgram(this.ProgramID);
        //            }
        //        }
        //        return _program;
        //    }
        //    private set { _program = value; }
        //}

        public int NodeID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }
        //public Node Node
        //{
        //    get
        //    {
        //        if (_node == null)
        //        {
        //            if (this._nodeID > 0)
        //            {
        //                _node = Node.GetNode(this.NodeID);
        //            }
        //        }
        //        return _node;
        //    }
        //    private set { _node = value; }
        //}

        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        //public Course Course
        //{
        //    get
        //    {
        //        if (_course == null)
        //        {
        //            if ((_courseID > 0) && (_versionID > 0))
        //            {
        //                _course = Course.GetCourse(_courseID, _versionID);
        //            }
        //        }
        //        return _course;
        //    }
        //    private set { _course = value; }
        //}

        public int NodeCourseID
        {
            get { return _nodeCourseID; }
            set { _nodeCourseID = value; }
        }
        
        //public Node_Course NodeCourse
        //{
        //    get
        //    {
        //        if (_nodeCourse == null)
        //        {
        //            if (this._nodeCourseID > 0)
        //            {
        //                _nodeCourse = Node_Course.GetNodeCourse(this.NodeCourseID);
        //            }
        //        }
        //        return _nodeCourse;
        //    }
        //    private set { _nodeCourse = value; }
        //}

        public string NodeLinkName
        {
            get { return _nodeLinkName; }
            set { _nodeLinkName = value; }
        }      

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        #endregion

        #region Constructor
        public Cal_Course_Prog_NodeEntity():base()
        {
            _treeCalendarDetailID = 0;
            //_treeCalenderDetail = null;
            ////_treeMasterID = 0;
            ////_treeMaster = null;
            ////_calendarMasterID = 0;
            ////_calendarMaster = null;
            _programID = 0;
            //_program = null;
            _nodeID = 0;
            //_node = null;
            _courseID = 0;
            _versionID = 0;
            //_course = null;
            _nodeCourseID = 0;
            //_nodeCourse = null;
            _nodeLinkName = string.Empty;
            _priority = 0;
        } 
        #endregion
    }
}
