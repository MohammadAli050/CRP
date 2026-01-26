using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class StudentEntity : BaseEntity
    {
        #region Variables
        private string _roll;
        //private Prefix _prefix;
        //private string _firstName;
        //private string _middleName;
        //private string _lastName;
        //private string _nickOrOtherName;
        //private DateTime _dOB;
        //private Gender _gender;
        //private MaritalStatus _matrialStatus;
        //private BloodGroup _bloodGroup;
        //private int _religionID;
        //private int _nationalityID;
        //private string _photoPath;
        private int _programID;
        //private Program _program;
        private Nullable<decimal> _totalDue;
        private Nullable<decimal> _totalPaid;
        private Nullable<decimal> _balance;
        private int _tuitionSetUpID;
        private int _waiverSetUpID;
        private int _discountSetUpID;
        private int _relationTypeID;
        private int _relativeID;
        private int _treeMasterID;
        //private TreeMaster _treeMaster;
        private int _major1NodeID;
        private int _major2NodeID;
        private int _major3NodeID;
        private int _minor1NodeID;
        private int _minor2NodeID;
        private int _minor3NodeID;
        //e
        private int _personID;
        //private Person _person;
        private int _accountHeadsId;
        private string _paymentSlNo;
        private string _remarks;
        private string _stdName;
        private string _treeName;
        private bool _isActive;
        private bool _isDeleted;
        private bool _isDiploma;
        #endregion        

        #region Constructor
        public StudentEntity()
        {
            _roll = string.Empty;
            //_prefix = Prefix.NA;
            //_firstName = string.Empty;
            //_middleName = string.Empty;
            //_lastName = string.Empty;
            //_nickOrOtherName = string.Empty;
            //_dOB = DateTime.MinValue;//6
            //_gender = Gender.NA;
            //_matrialStatus = 0;
            //_bloodGroup = 0;
            //_religionID = 0;
            //_nationalityID = 0;
            //_photoPath = string.Empty;
            _programID = 0;//12
            //_program = null;
            _totalDue = 0;
            _totalPaid = 0;
            _balance = 0;
            _tuitionSetUpID = 0;
            _waiverSetUpID = 0;
            _discountSetUpID = 0;
            _relationTypeID = 0;
            _relativeID = 0;
            _treeMasterID = 0;
            //_treeMaster = null;
            _major1NodeID =0;
            _major2NodeID = 0;
            _major3NodeID = 0;
            _minor1NodeID = 0;
            _minor2NodeID = 0;
            _minor3NodeID = 0;

            _personID = 0;
            //_person = null;
            _stdName = string.Empty;
            _treeName = string.Empty;
            _isActive = false;
            _isDeleted = false;
            _isDiploma = false;
            _accountHeadsId = 0;
            _paymentSlNo = string.Empty;
            _remarks = string.Empty;
        }
        #endregion

        #region Properties

        public string Roll
        {
            get
            {
                return this._roll;
            }
            set
            {
                this._roll = value;
            }
        }

        //#region Delete
        //public Prefix StudentPrefix
        //{
        //    get
        //    {
        //        return this._prefix;
        //    }
        //    set
        //    {
        //        this._prefix = value;
        //    }
        //}
        //private SqlParameter StudentPrefixParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = PREFIX_PA;
        //        if (StudentPrefix == Prefix.NA)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = (int)StudentPrefix;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public string FirstName
        //{
        //    get
        //    {
        //        return this._firstName;
        //    }
        //    set
        //    {
        //        this._firstName = value;
        //    }
        //}
        //private SqlParameter FirstNameParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = FIRSTNAME_PA;
        //        if (FirstName == string.Empty)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = FirstName;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public string MiddleName
        //{
        //    get
        //    {
        //        return this._middleName;
        //    }
        //    set
        //    {
        //        this._middleName = value;
        //    }
        //}
        //private SqlParameter MiddleNameParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = MIDDLENAME_PA;
        //        if (MiddleName == string.Empty)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = MiddleName;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public string LastName
        //{
        //    get
        //    {
        //        return this._lastName;
        //    }
        //    set
        //    {
        //        this._lastName = value;
        //    }
        //}
        //private SqlParameter LastNameParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = LASTNAME_PA;
        //        if (LastName == string.Empty)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = LastName;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public string NickOrOtherName
        //{
        //    get
        //    {
        //        return this._nickOrOtherName;
        //    }
        //    set
        //    {
        //        this._nickOrOtherName = value;
        //    }
        //}
        //private SqlParameter NickOrOtherNameParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = NICKOROTHERNAME_PA;
        //        if (NickOrOtherName == string.Empty)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = NickOrOtherName;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public DateTime DOB
        //{
        //    get
        //    {
        //        return this._dOB;
        //    }
        //    set
        //    {
        //        this._dOB = value;
        //    }
        //}
        //private SqlParameter DOBParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = DOBCON_PA;
        //        if (DOB == DateTime.MinValue)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = DOB;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public Gender StudentGender
        //{
        //    get
        //    {
        //        return this._gender;
        //    }
        //    set
        //    {
        //        this._gender = value;
        //    }
        //}
        //private SqlParameter StudentGenderParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = GENDER_PA;
        //        if (StudentGender == Gender.NA)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = (int)StudentGender;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public MaritalStatus StudentMatrialStatus
        //{
        //    get
        //    {
        //        return this._matrialStatus;
        //    }
        //    set
        //    {
        //        this._matrialStatus = value;
        //    }
        //}
        //private SqlParameter MatrialStatusParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = MATRIALSTATUS_PA;
        //        if (StudentMatrialStatus == MaritalStatus.NA)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = (int)StudentMatrialStatus;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public BloodGroup StudentBloodGroup
        //{
        //    get
        //    {
        //        return this._bloodGroup;
        //    }
        //    set
        //    {
        //        this._bloodGroup = value;
        //    }
        //}
        //private SqlParameter StudentBloodGroupParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = BLOODGROUP_PA;
        //        if (StudentBloodGroup == BloodGroup.NA)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = (int)StudentBloodGroup;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public int ReligionID
        //{
        //    get
        //    {
        //        return this._religionID;
        //    }
        //    set
        //    {
        //        this._religionID = value;
        //    }
        //}
        //private SqlParameter ReligionIDParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = RELIGIONID_PA;
        //        if (ReligionID == 0)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = ReligionID;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public int NationalityID
        //{
        //    get
        //    {
        //        return this._nationalityID;
        //    }
        //    set
        //    {
        //        this._nationalityID = value;
        //    }
        //}
        //private SqlParameter NationalityIDParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = NATIONALITYID_PA;
        //        if (NationalityID == 0)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = NationalityID;
        //        }
        //        return sqlParam;
        //    }
        //}

        //public string PhotoPath
        //{
        //    get { return _photoPath; }
        //    set { _photoPath = value; }
        //}
        //private SqlParameter PhotoPathParam
        //{
        //    get
        //    {
        //        SqlParameter sqlParam = new SqlParameter();
        //        sqlParam.ParameterName = PHOTOPATH_PA;
        //        if (PhotoPath == string.Empty)
        //        {
        //            sqlParam.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            sqlParam.Value = PhotoPath;
        //        }
        //        return sqlParam;
        //    }
        //}  
        //#endregion

        public int ProgramID
        {
            get
            {
                return this._programID;
            }
            set
            {
                this._programID = value;
            }
        }
        //public Program StudentProgram
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

        public Nullable< decimal> TotalDue
        {
            get
            {
                return this._totalDue;
            }
            set
            {

                this._totalDue = value;
            }
        }

        public Nullable<decimal> TotalPaid
        {
            get
            {
                return this._totalPaid;
            }
            set
            {
                this._totalPaid = value;
            }
        }

        public Nullable<decimal> Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }

        public int TuitionSetUpID
        {
            get
            {
                return this._tuitionSetUpID;
            }
            set
            {

                this._tuitionSetUpID = value;
            }
        }
        
        public int WaiverSetUpID
        {
            get
            {
                return this._waiverSetUpID;
            }
            set
            {
                this._waiverSetUpID = value;
            }
        }
        
        public int DiscountSetUpID
        {
            get
            {
                return this._discountSetUpID;
            }
            set
            {
                this._discountSetUpID = value;
            }
        }
        
        public int RelationTypeID
        {
            get
            {
                return this._relationTypeID;
            }
            set
            {
                this._relationTypeID = value;
            }
        }
        
        public int RelativeID
        {
            get
            {
                return this._relativeID;
            }
            set
            {
                this._relativeID = value;
            }
        }
        
        public int TreeMasterID
        {
            get { return _treeMasterID; }
            set { _treeMasterID = value; }
        }
        //public TreeMaster StudentTreeMaster
        //{
        //    get
        //    {
        //        if (_treeMaster == null)
        //        {
        //            if (this.TreeMasterID > 0)
        //            {
        //                _treeMaster = TreeMaster.Get(this.TreeMasterID);
        //            }
        //        }
        //        return _treeMaster;
        //    }
        //}

        public int Major1NodeID
        {
            get { return _major1NodeID; }
            set { _major1NodeID = value; }
        }

        public int Major2NodeID
        {
            get { return _major2NodeID; }
            set { _major2NodeID = value; }
        }

        public int Major3NodeID
        {
            get { return _major3NodeID; }
            set { _major3NodeID = value; }
        }

        public int Minor1NodeID
        {
            get { return _minor1NodeID; }
            set { _minor1NodeID = value; }
        }

        public int Minor2NodeID
        {
            get { return _minor2NodeID; }
            set { _minor2NodeID = value; }
        }

        public int Minor3NodeID
        {
            get { return _minor3NodeID; }
            set { _minor3NodeID = value; }
        }
        
        //new column
        public int PersonID
        {
            get
            {
                return this._personID;
            }
            set
            {
                this._personID = value;
            }
        }
        //public Person StuPerson
        //{
        //    get
        //    {
        //        if (_person == null)
        //        {
        //            if (this.PersonID > 0)
        //            {
        //                _person = Person.GetPerson(this.PersonID);
        //            }
        //        }
        //        return _person;
        //    }
        //}


        //public string StdName
        //{
        //    get {
        //        if (this.PersonID > 0)
        //        {
        //            _stdName = Person.GetPerson(this.PersonID).PersonName;
        //        }
        //        return _stdName; }
        //}

        //public string TreeName
        //{
        //    get {
        //        if (this.TreeMasterID > 0)
        //        {
        //            _treeName = TreeMaster.Get(this.TreeMasterID).RootNode.Name;
        //        }
        //        return _treeName; 
        //    }
        //}

        public int AcHeadsId
         {
             get { return _accountHeadsId; }
             set { _accountHeadsId = value; }
         }

        public string PaymentSlNo
         {
             get { return _paymentSlNo; }
             set { _paymentSlNo = value; }
         }

        public bool IsActive
         {
             get { return _isActive; }
             set { _isActive = value; }
         }

        public bool IsDeleted
         {
             get { return _isDeleted; }
             set { _isDeleted = value; }
         }

        public bool IsDiploma
         {
             get { return _isDiploma; }
             set { _isDiploma = value; }
         }

        public string Remarks
         {
             get { return _remarks; }
             set { _remarks = value; }
         }

        #endregion
    }
}
