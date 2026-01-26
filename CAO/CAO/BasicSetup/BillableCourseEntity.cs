using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class BillableCourseEntity:BaseEntity
    {
        private int _acaCalID;
        public int AcaCalID
        {
            get { return _acaCalID; }
            set { _acaCalID = value; }
        }
        
        private int _billStartFromRetakeNo;
        public int BillStartFromRetakeNo
        {
            get { return _billStartFromRetakeNo; }
            set { _billStartFromRetakeNo = value; }
        }
        
        private bool _isCreditCourse;
        public bool IsCreditCourse
        {
            get { return _isCreditCourse; }
            set { _isCreditCourse = value; }
        }

        private int _programID;
        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        private int _courseID;
        public int CourseID
        {
            get {return _courseID;}
            set {_courseID = value;}
        }

        private int _versionID;
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }

        private string _formalCode;
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private decimal _credits;
        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }

        public BillableCourseEntity()
            : base()
        {
            _acaCalID = 0;
            _billStartFromRetakeNo = 0;
            _isCreditCourse = false;
            _programID = 0;
            _courseID = 0;
            _versionID = 0;
            _title = string.Empty;
            _credits = 0;
        }
    }
}
