using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class Student_CalCourseProgNodeEntity : BaseEntity
    {
        #region Variables and properties

        private int _studentID = 0;
        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        private int _calCourseProgNodeID = 0;
        public int CalCourseProgNodeID
        {
            get { return _calCourseProgNodeID; }
            set { _calCourseProgNodeID = value; }
        }

        private bool _isCompleted = false;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

        private int _originalCalID = 0;
        public int OriginalCalID
        {
            get { return _originalCalID; }
            set { _originalCalID = value; }
        }

        private bool _isAutoAssign = false;
        public bool IsAutoAssign
        {
            get { return _isAutoAssign; }
            set { _isAutoAssign = value; }
        }

        private bool _isAutoOpen = false;
        public bool IsAutoOpen
        {
            get { return _isAutoOpen; }
            set { _isAutoOpen = value; }
        }

        private bool _isRequisitioned = false;
        public bool IsRequisitioned
        {
            get { return _isRequisitioned; }
            set { _isRequisitioned = value; }
        }

        private bool _isMandatory = false;
        public bool IsMandatory
        {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }

        private bool _isManualOpen = false;
        public bool IsManualOpen
        {
            get { return _isManualOpen; }
            set { _isManualOpen = value; }
        }

        private int _treeCalendarDetailID;
        public int TreeCalendarDetailID
        {
            get { return _treeCalendarDetailID; }
            set { _treeCalendarDetailID = value; }
        }

        private int _treeCalendarMasterID;
        public int TreeCalendarMasterID
        {
            get { return _treeCalendarMasterID; }
            set { _treeCalendarMasterID = value; }
        }

        private int _treeMasterID;
        public int TreeMasterID
        {
            get { return _treeMasterID; }
            set { _treeMasterID = value; }
        }

        private string _calendarMasterName;
        public string CalendarMasterName
        {
            get { return _calendarMasterName; }
            set { _calendarMasterName = value; }
        }

        private string _calendarDetailName;
        public string CalendarDetailName
        {
            get { return _calendarDetailName; }
            set { _calendarDetailName = value; }
        }

        private int _courseID;
        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        private int _versionID;
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }

        private int _node_CourseID;
        public int Node_CourseID
        {
            get { return _node_CourseID; }
            set { _node_CourseID = value; }
        }

        private int _nodeID;
        public int NodeID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }

        private string _nodeLinkName;
        public string NodeLinkName
        {
            get { return _nodeLinkName; }
            set { _nodeLinkName = value; }
        }

        private string _formalCode;
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }

        private string _versionCode;
        public string VersionCode
        {
            get { return _versionCode; }
            set { _versionCode = value; }
        }

        private string _courseTitle;
        public string CourseTitle
        {
            get { return _courseTitle; }
            set { _courseTitle = value; }
        }

        private int _acaCal_SectionID;
        public int AcaCal_SectionID
        {
            get { return _acaCal_SectionID; }
            set { _acaCal_SectionID = value; }
        }

        private string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }

        private int _programID;
        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        private int _deptID;
        public int DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private int _retakeNo;
        public int RetakeNo
        {
            get { return _retakeNo; }
            set { _retakeNo = value; }
        }

        private decimal _obtainedGPA;
        public decimal ObtainedGPA
        {
            get { return _obtainedGPA; }
            set { _obtainedGPA = value; }
        }

        private string _obtainedGrade;
        public string ObtainedGrade
        {
            get { return _obtainedGrade; }
            set { _obtainedGrade = value; }
        }

        private int _academicCalenderID;
        public int AcademicCalenderID
        {
            get { return _academicCalenderID; }
            set { _academicCalenderID = value; }
        }

        private int _acaCalYear;
        public int AcaCalYear
        {
            get { return _acaCalYear; }
            set { _acaCalYear = value; }
        }

        private string _batchCode;
        public string BatchCode
        {
            get { return _batchCode; }
            set { _batchCode = value; }
        }

        private string _acaCalTypeName;
        public string AcaCalTypeName
        {
            get { return _acaCalTypeName; }
            set { _acaCalTypeName = value; }
        }

        private bool _chkSection;
        public bool ChkSection
        {
            get { return _chkSection; }
            set { _chkSection = value; }
        }

        private bool _isMultipleACUSpan;
        public bool IsMultipleACUSpan
        {
            get { return _isMultipleACUSpan; }
            set { _isMultipleACUSpan = value; }
        }

        private decimal _courseCredit;
        public decimal CourseCredit
        {
            get { return _courseCredit; }
            set { _courseCredit = value; }
        }

        private decimal _completedCredit;
        public decimal CompletedCredit
        {
            get { return _completedCredit; }
            set { _completedCredit = value; }
        }

        #endregion

        #region Constructor
        public Student_CalCourseProgNodeEntity()
            : base()
        {
            _studentID = 0;
            _calCourseProgNodeID = 0;
            _isCompleted = false;
            _originalCalID = 0;
            _isAutoAssign = false;
            _isAutoOpen = false;
            _isRequisitioned = false;
            _isMandatory = false;
            _isManualOpen = false;
            _acaCalTypeName = string.Empty;
            _acaCalYear = 0;
            _batchCode = string.Empty;
            _calendarDetailName = string.Empty;
            _calendarMasterName = string.Empty;
            _courseTitle = string.Empty;
            _formalCode = string.Empty;
            _nodeLinkName = string.Empty;
            _obtainedGPA = 0m;
            _obtainedGrade = string.Empty;
            _priority = 0;
            _retakeNo = 0;
            _treeCalendarDetailID = 0;
            _treeCalendarMasterID = 0;
            _treeMasterID = 0;
            _versionCode = string.Empty;
            //..........................
            _acaCal_SectionID = 0;
            _sectionName = string.Empty;
            _courseID = 0;
            _versionID = 0;
            _node_CourseID = 0;
            _nodeID = 0;
            _programID = 0;
            _deptID = 0;
            _academicCalenderID = 0;
            //.............................
            _chkSection = false;
            _isMultipleACUSpan = false;
            _courseCredit = 0;
            _completedCredit = 0;
        }
        #endregion
    }
}
