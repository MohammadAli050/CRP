using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CourseEntity : BaseEntity
    {
        #region DBCloumns
        //CourseID	                int	            Unchecked//0
        //VersionID	                int	            Unchecked//1
        //FormalCode	            varchar(50)	    Checked//2
        //VersionCode	            varchar(50)	    Checked//3
        //Title	                    varchar(150)	Checked//4
        //AssocCourseID	            int	            Checked//5
        //AssocVersionID	        int	            Checked//6
        //StartAcademicCalenderID	int	            Checked//7
        //ProgramID	                int	            Checked//8
        //CourseContent	            varchar(500)	Checked//9
        //IsCreditCourse	        bit	            Checked//10
        //Credits	                numeric(18, 2)	Checked//11
        //IsSectionMandatory	    bit	            Checked//12
        //HasEquivalents	        bit	            Checked//13
        //HasMultipleACUSpan	    bit	            Checked//14
        //IsActive	                bit	            Checked//15
        //CreatedBy	                int	            Unchecked//16
        //CreatedDate	            datetime	    Unchecked//17
        //ModifiedBy	            int	            Checked//18
        //ModifiedDate	            datetime	    Checked//19
        #endregion

        #region Variables
        private int _versionID;
        private string _formalCode;
        private string _versionCode;

        private string _title;

        private int _assocCourseID;
        private int _assocVersionID;
        //private Course _associatedCourse;

        private int _startACUID;
        //private AcademicCalender _startACU;

        private int _ownerProgID;
        //private Program _ownerProgram;

        private string _courseContent;

        private bool _isCredit;
        private decimal _credits;

        private bool _isSectionMandatory;
        private bool _hasEquivalents;
        private bool _hasMultipleACUSpan;
        private bool _isActive;

        //private List<Course> _equiCourse = null;

        //private List<EquivalentCourse> _equivalents = null;

        private int _courseACUSpanMasID = 0;
        //private CourseACUSpanMas _courseACUSpanMas = null;
        #endregion

        #region Constructor
        public CourseEntity()
            : base()
        {
            _versionID = 0;//0
            _formalCode = string.Empty;//1
            _versionCode = string.Empty;//2
            _title = string.Empty;//3
            _assocCourseID = 0;//4
            _assocVersionID = 0;//5
            //_associatedCourse = null;//6
            _startACUID = 0;//7
            _ownerProgID = 0;//8
            _courseContent = string.Empty;//9
            _isCredit = true;//10
            _credits = 0;//11
            _isSectionMandatory = true;//12
            _hasEquivalents = false;//13
            _hasMultipleACUSpan = false;//14
            _isActive = true;//15
            //_equiCourse = null;//16
            //_courseACUSpanMas = null;//17
            //_equivalents = null;//18
        }
        #endregion

        #region Properties

        #region VersionID
        /// <summary>
        /// Second Part of the primary key
        /// </summary>
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        #endregion

        #region Formal Code
        /// <summary>
        /// Formal Code
        /// </summary>
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }
        #endregion

        #region Version Code
        /// <summary>
        /// Version Code
        /// </summary>
        public string VersionCode
        {
            get { return _versionCode; }
            set { _versionCode = value; }
        }
        #endregion

        #region Title
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        #endregion

        #region Associated Course
        /// <summary>
        /// Associated Course ID
        /// </summary>
        public int AssocCourseID
        {
            get { return _assocCourseID; }
            set { _assocCourseID = value; }
        }
        /// <summary>
        /// Associated Course Version Id
        /// </summary>
        public int AssocVersionID
        {
            get { return _assocVersionID; }
            set { _assocVersionID = value; }
        }
        /// <summary>
        /// Associated Course
        /// </summary>
        //public Course AssociatedCourse
        //{
        //    get
        //    {
        //        if (_associatedCourse == null)
        //        {
        //            if ((_assocCourseID > 0) && (_assocVersionID > 0))
        //            {
        //                _associatedCourse = Course.GetCourse(_assocCourseID, _assocVersionID);
        //            }
        //        }
        //        return _associatedCourse;
        //    }
        //} 
        #endregion

        #region StartACU
        public int StartACUID
        {
            get { return _startACUID; }
            set { _startACUID = value; }
        }
        //public AcademicCalender StartACU
        //{
        //    get
        //    {
        //        if (_startACU == null)
        //        {
        //            if (StartACUID > 0)
        //            {
        //                _startACU = AcademicCalender.Get(StartACUID);
        //            }
        //        }
        //        return _startACU;
        //    }
        //}   
        #endregion

        #region Owner Program
        public int OwnerProgID
        {
            get { return _ownerProgID; }
            set { _ownerProgID = value; }
        }
        //public Program OwnerProgram
        //{
        //    get
        //    {
        //        if (_ownerProgram == null)
        //        {
        //            if (this.OwnerProgID > 0)
        //            {
        //                _ownerProgram = Program.GetProgram(this.OwnerProgID);
        //            }
        //        }
        //        return _ownerProgram;
        //    }
        //} 
        #endregion

        #region Course Content
        public string CourseContent
        {
            get { return _courseContent; }
            set { _courseContent = value; }
        }
        #endregion

        #region Is Credit
        public bool IsCredit
        {
            get { return _isCredit; }
            set { _isCredit = value; }
        }
        #endregion

        #region Credits
        /// <summary>
        /// Credits
        /// </summary>
        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        #endregion

        #region Is Section Mandatory
        #region IsSectionMandatory
        public bool IsSectionMandatory
        {
            get { return _isSectionMandatory; }
            set { _isSectionMandatory = value; }
        }
        #endregion 
        #endregion

        #region Has Equivalents
        public bool HasEquivalents
        {
            get { return _hasEquivalents; }
            set { _hasEquivalents = value; }
        }
        #endregion

        #region Has Multiple ACU Span
        public bool HasMultipleACUSpan
        {
            get { return _hasMultipleACUSpan; }
            set { _hasMultipleACUSpan = value; }
        }
        #endregion

        #region Is Active
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion


        #region Collection of actual courses that are equivalents
        //public List<Course> EquiCourse
        //{
        //    get
        //    {
        //        if (_equiCourse == null)
        //        {
        //            if ((Id > 0) && (VersionID > 0))
        //            {
        //                _equiCourse = EquivalentCourse.GetEquiCourse(Id, VersionID);
        //            }
        //        }                
        //        return _equiCourse;                
        //    }
        //    set
        //    {
        //        _equiCourse = value;
        //    }
        //}
        #endregion

        #region Collection of equicourse objects that contians link to actual courses
        //public List<EquivalentCourse> Equivalents
        //{
        //    get
        //    {
        //        if (_equivalents == null)
        //        {
        //            if ((Id > 0) && (VersionID > 0))
        //            {
        //                _equivalents = EquivalentCourse.GetEquiCourses(Id, VersionID);
        //            }
        //        }
        //        return _equivalents;
        //    }
        //    set { _equivalents = value; }
        //} 
        #endregion

        #region Course ACU Span
        public int CourseACUSpanMasID
        {
            get { return _courseACUSpanMasID; }
        }
        //public CourseACUSpanMas CourseACUSpanInfo
        //{
        //    get
        //    {
        //        if (_courseACUSpanMas == null)
        //        {
        //            if ((Id > 0) && (VersionID > 0))
        //            {
        //                _courseACUSpanMas = CourseACUSpanMas.GetByCourse(Id, VersionID);
        //                if (_courseACUSpanMas != null)
        //                {
        //                    _courseACUSpanMasID = _courseACUSpanMas.Id; 
        //                }
        //            }
        //        }
        //        return _courseACUSpanMas;
        //    }
        //    set 
        //    { 
        //        _courseACUSpanMas = value;
        //        if (_courseACUSpanMas != null)
        //        {
        //            _courseACUSpanMasID = _courseACUSpanMas.Id;
        //        }
        //    }
        //} 
        #endregion

        /// <summary>
        /// Combined formal code and full code
        /// </summary>
        public string FullCode
        {
            get { return _formalCode + "-" + _versionCode; }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                _formalCode = x[0];
                _versionCode = x[1];
            }
        }
        public string FullCodeAndCourse
        {
            get { return _formalCode + "-" + _versionCode + "-" + _title + "-" + _credits.ToString(); }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                _formalCode = x[0];
                _versionCode = x[1];
                _title = x[2];
                _credits = Convert.ToDecimal(x[3]);
            }
        }
        #endregion

    }
}
