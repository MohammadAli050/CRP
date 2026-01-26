using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    [Serializable]
    public class CourseWavTransfrMaster : Base
    {
        private string _universityName;
        private DateTime _fromDate;      
        private DateTime _toDate;
        private int _studentID;


        //private
        //private int _divisionType;

        public string UniversityName
        {
            get
            {
                return this._universityName;
            }
            set
            {
                this._universityName = value;
            }
        }    
        public DateTime FromDate
        {
            get
            {
                return this._fromDate;
            }
            set
            {
                this._fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return this._toDate;
            }
            set
            {
                this._toDate = value;
            }
        }
        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }      
    }
}
