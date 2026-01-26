using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class Student_ACUDetailEntity:BaseEntity
    {
        #region DBColumns
        //StdACUDetailID	    int	            Unchecked
        //StdAcademicCalenderID	int	            Unchecked
        //StudentID	            int	            Unchecked
        //StatusTypeID	        int	            Unchecked
        //SchSetUpID	        int	            Checked
        //CGPA	                money	        Checked
        //GPA	                money	        Checked
        //Description	        varchar(200)	Checked
        //CreatedBy	            int	            Checked
        //CreatedDate	        datetime	    Checked
        //ModifiedBy	        int	            Unchecked
        //ModifiedDate	        datetime	    Unchecked 
        #endregion

        #region Variables
        private int _stdAcademicCalenderID;
        //private Std_AcademicCalender _parent;
        private int _studentID;
        //private Student _linkStudent;
        private int _statusTypeID;
        //private StatusType _linkStatusType;
        private int _schSetUpID;
        private Nullable< decimal> _cGPA;
        private Nullable<decimal> _gPA;


        private string _description;
        #endregion

        #region Constructor
        public Student_ACUDetailEntity():base()
        {
            _stdAcademicCalenderID = 0;
            _studentID = 0;
            _statusTypeID = 0;
            _schSetUpID = 0;
            _cGPA = 0;
            _gPA = 0;
            _description = string.Empty;
        } 
        #endregion

        #region Properties
        public Nullable<decimal> GPA
        {
            get { return _gPA; }
            set { _gPA = value; }
        }

        public Nullable<decimal> CGPA
        {
            get { return _cGPA; }
            set { _cGPA = value; }
        }

        public int SchSetUpID
        {
            get { return _schSetUpID; }
            set { _schSetUpID = value; }
        }

        public int StatusTypeID
        {
            get { return _statusTypeID; }
            set { _statusTypeID = value; }
        }
        //public StatusType LinkStatusType
        //{
        //    get
        //    {
        //        if (_linkStatusType == null)
        //        {
        //            if ((_statusTypeID > 0))
        //            {
        //                _linkStatusType = StatusType.Get(_statusTypeID);
        //            }
        //        }
        //        return _linkStatusType;
        //    }
        //}

        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        //public Student LinkStudent
        //{
        //    get
        //    {
        //        if (_linkStudent == null)
        //        {
        //            if ((_studentID > 0))
        //            {
        //                _linkStudent = Student.GetStudent(_studentID);
        //            }
        //        }
        //        return _linkStudent;
        //    }
        //}

        public int StdAcademicCalenderID
        {
            get { return _stdAcademicCalenderID; }
            set { _stdAcademicCalenderID = value; }
        }
        //public Std_AcademicCalender Parent
        //{
        //    get
        //    {
        //        if (_parent == null)
        //        {
        //            if ((_stdAcademicCalenderID > 0))
        //            {
        //                _parent = Std_AcademicCalender.Get(_stdAcademicCalenderID);
        //            }
        //        }
        //        return _parent;
        //    }
        //}

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion
    }
}
