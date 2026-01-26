using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    [Serializable]
    public class StdEducationInfo:Base
    {
        private string _dregreeName;
        private string _groupName;
        private string _institutionName;
        private decimal _totalMarks;
        private decimal _obtainedMarks;
        private string _division;
        private decimal _totalCGPA;
        private decimal _obtainedCGPA;
        private int _studentID;


        private int _addressID;

        public string DregreeName
        {
            get
            {
                return this._dregreeName;
            }
            set
            {
                this._dregreeName = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                this._groupName = value;
            }
        }

        public string InstitutionName
        {
            get
            {
                return this._institutionName;
            }
            set
            {
                this._institutionName = value;
            }
        }

        public decimal TotalMarks
        {
            get
            {
                return this._totalMarks;
            }
            set
            {
                this._totalMarks = value;

            }
        }

        public decimal ObtainedMarks
        {
            get
            {
                return this._obtainedMarks;
            }
            set
            {
                this._obtainedMarks = value;
            }
        }

        public string Division
        {
            get
            {
                return this._division;
            }
            set
            {
                this._division = value;
            }
        }

        public decimal TotalCGPA
        {
            get
            {
                return this._totalCGPA;
            }
            set
            {
                this._totalCGPA = value;
            }
        }

        public decimal ObtainedCGPA
        {
            get
            {
                return this._obtainedCGPA;
            }
            set
            {
                this._obtainedCGPA = value;
            }
        }

        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        public int AddressID
        {
            get
            {
                return this._addressID;
            }
            set
            {
                this._addressID = value;
            }
        }

    }
}
