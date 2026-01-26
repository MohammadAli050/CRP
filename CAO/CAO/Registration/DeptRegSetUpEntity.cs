using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class DeptRegSetUpEntity:BaseEntity
    {
        #region DBColumns
        //DeptRegSetUpID	    int	        Unchecked
        //ProgramID	            int	        Unchecked
        //LocalCGPA1	        money	    Checked
        //LocalCredit1	        money	    Checked
        //LocalCGPA2	        money	    Checked
        //LocalCredit2	        money	    Checked
        //LocalCGPA3	        money	    Checked
        //LocalCredit3	        money	    Checked
        //ManCGPA1	            money	    Checked
        //ManCredit1	        money	    Checked
        //ManRetakeGradeLimit1	int	        Checked
        //ManCGPA2	            money	    Checked
        //ManCredit2	        money	    Checked
        //ManRetakeGradeLimit2	int	        Checked
        //ManCGPA3	            money	    Checked
        //ManCredit3	        money	    Checked
        //ManRetakeGradeLimit3	int	        Checked
        //MaxCGPA1	            money	    Checked
        //MaxCredit1	        money	    Checked
        //MaxCGPA2	            money	    Checked
        //MaxCredit2	        money	    Checked
        //MaxCGPA3	            money	    Checked
        //MaxCredit3	        money	    Checked        
        //ProjectCGPA	        money	    Checked
        //ProjectCredit	        money	    Checked
        //ThesisCGPA	        money	    Checked
        //ThesisCredit	        money	    Checked
        //MajorCGPA	            money	    Checked
        //MajorCredit	        money	    Checked
        //ProbationLock	        int	        Checked
        //CourseRetakeLimit	    int	        Checked
        //CreatedBy	            int	        Unchecked
        //CreatedDate	        datetime	Unchecked
        //ModifiedBy	        int	        Checked
        //ModifiedDate	        datetime	Checked
        #endregion

        #region Variables

        private int _programID;
        //private Program _program;
        private Nullable<decimal> _localCGPA1;
        private Nullable<decimal> _localCredit1;
        private Nullable<decimal> _localCGPA2;
        private Nullable<decimal> _localCredit2;
        private Nullable<decimal> _localCGPA3;
        private Nullable<decimal> _localCredit3;
        private Nullable<decimal> _manCGPA1;
        private Nullable<decimal> _manCredit1;
        private string _manRetakeGradeLimit1;
        private Nullable<decimal> _manCGPA2;
        private Nullable<decimal> _manCredit2;
        private string _manRetakeGradeLimit2;
        private Nullable<decimal> _manCGPA3;
        private Nullable<decimal> _manCredit3;
        private string _manRetakeGradeLimit3;
        private Nullable<decimal> _maxCGPA1;
        private Nullable<decimal> _maxCredit1;
        private Nullable<decimal> _maxCGPA2;
        private Nullable<decimal> _maxCredit2;
        private Nullable<decimal> _maxCGPA3;
        private Nullable<decimal> _maxCredit3;
        private Nullable<int> _courseRetakeLimit;
        private Nullable<decimal> _projCGPA;
        private Nullable<decimal> _projCredit;
        private Nullable<decimal> _thesisCGPA;
        private Nullable<decimal> _thesisCredit;
        private Nullable<decimal> _majorCGPA;
        private Nullable<decimal> _majorCredit;
        private Nullable<int> _probLock;

        #endregion

        #region Constructor
        public DeptRegSetUpEntity():base()
        {
            _programID = 0;
            //_program = null;
            _localCGPA1 = null;
            _localCredit1 = null;
            _localCGPA2 = null;
            _localCredit2 = null;
            _localCGPA3 = null;
            _localCredit3 = null;
            _manCGPA1 = null;
            _manCredit1 = null;
            _manRetakeGradeLimit1 = null;
            _manCGPA2 = null;
            _manCredit2 = null;
            _manRetakeGradeLimit2 = null;
            _manCGPA3 = null;
            _manCredit3 = null;
            _manRetakeGradeLimit3 = null;
            _maxCGPA1 = null;
            _maxCredit1 = null;
            _maxCGPA2 = null;
            _maxCredit2 = null;
            _maxCGPA3 = null;
            _maxCredit3 = null;
            _courseRetakeLimit = null;
            _projCGPA = null;
            _projCredit = null;
            _thesisCGPA = null;
            _thesisCredit = null;
            _majorCGPA = null;
            _majorCredit = null;
            _probLock = null;
        }
        #endregion

        #region Properties
        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        //public Program OwnerProgram
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
        //}

        public Nullable<decimal> LocalCGPA1
        {
            get { return _localCGPA1; }
            set { _localCGPA1 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> LocalCredit1
        {
            get { return _localCredit1; }
            set { _localCredit1 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> LocalCGPA2
        {
            get { return _localCGPA2; }
            set { _localCGPA2 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> LocalCredit2
        {
            get { return _localCredit2; }
            set { _localCredit2 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> LocalCGPA3
        {
            get { return _localCGPA3; }
            set { _localCGPA3 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> LocalCredit3
        {
            get { return _localCredit3; }
            set { _localCredit3 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ManCGPA1
        {
            get { return _manCGPA1; }
            set { _manCGPA1 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ManCredit1
        {
            get { return _manCredit1; }
            set { _manCredit1 = Math.Round(value.Value, 2); }
        }

        public string ManRetakeGradeLimit1
        {
            get { return _manRetakeGradeLimit1; }
            set { _manRetakeGradeLimit1 = value; }
        }

        public Nullable<decimal> ManCGPA2
        {
            get { return _manCGPA2; }
            set { _manCGPA2 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ManCredit2
        {
            get { return _manCredit2; }
            set { _manCredit2 = Math.Round(value.Value, 2); }
        }

        public string ManRetakeGradeLimit2
        {
            get { return _manRetakeGradeLimit2; }
            set { _manRetakeGradeLimit2 = value; }
        }

        public Nullable<decimal> ManCGPA3
        {
            get { return _manCGPA3; }
            set { _manCGPA3 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ManCredit3
        {
            get { return _manCredit3; }
            set { _manCredit3 = Math.Round(value.Value, 2); }
        }

        public string ManRetakeGradeLimit3
        {
            get { return _manRetakeGradeLimit3; }
            set { _manRetakeGradeLimit3 = value; }
        }

        public Nullable<decimal> MaxCGPA1
        {
            get { return _maxCGPA1; }
            set { _maxCGPA1 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MaxCredit1
        {
            get { return _maxCredit1; }
            set { _maxCredit1 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MaxCGPA2
        {
            get { return _maxCGPA2; }
            set { _maxCGPA2 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MaxCredit2
        {
            get { return _maxCredit2; }
            set { _maxCredit2 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MaxCGPA3
        {
            get { return _maxCGPA3; }
            set { _maxCGPA3 = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MaxCredit3
        {
            get { return _maxCredit3; }
            set { _maxCredit3 = Math.Round(value.Value, 2); }
        }

        public Nullable<int> CourseRetakeLimit
        {
            get { return _courseRetakeLimit; }
            set { _courseRetakeLimit = value; }
        }

        public Nullable<decimal> ProjCGPA
        {
            get { return _projCGPA; }
            set { _projCGPA = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ProjectCredit
        {
            get { return _projCredit; }
            set { _projCredit = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ThesisCGPA
        {
            get { return _thesisCGPA; }
            set { _thesisCGPA = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> ThesisCredit
        {
            get { return _thesisCredit; }
            set { _thesisCredit = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MajorCGPA
        {
            get { return _majorCGPA; }
            set { _majorCGPA = Math.Round(value.Value, 2); }
        }

        public Nullable<decimal> MajorCredit
        {
            get { return _majorCredit; }
            set { _majorCredit = Math.Round(value.Value, 2); }
        }

        public Nullable<int> ProbLock
        {
            get { return _probLock; }
            set { _probLock = value; }
        }
        #endregion
    }
}
