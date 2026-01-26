using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Data.Common;
using DataAccess;
using System.Text;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class Student : Base
    {
        #region DBColumns
        //StudentID	        int	            Unchecked //0
        //Roll	            nvarchar(100)	Unchecked //1

        ////Prefix	        int         	Checked //2
        ////FirstName	        varchar(100)	Checked //3
        ////MiddleName	    varchar(100)	Checked //4
        ////LastName	        varchar(100)	Checked //5
        ////NickOrOtherName	varchar(100)	Checked //6
        ////DOB	            datetime	    Checked //7
        ////Gender	        int	            Checked //8
        ////MatrialStatus	    int	            Checked //9
        ////BloodGroup	    int	            Checked //10
        ////ReligionID	    int	            Checked //11
        ////NationalityID	    int	            Checked //12
        ////PhotoPath	        varchar(500)	Checked //13
        ////ProgramID	        int	            Checked //14
        ////TotalDue	        money	        Checked //15
        ////TotalPaid	        money	        Checked //16
        ////Balance	        money	        Checked //17
        ////TuitionSetUpID	int	            Checked //18
        ////WaiverSetUpID	    int	            Checked //19
        ////DiscountSetUpID	int	            Checked //20
        ////RelationTypeID	int	            Checked //21
        ////RelativeID	    int	            Checked //22
        ////TreeMasterID	    int	            Checked //23
        ////Major1NodeID	    int	            Checked //24
        ////Major2NodeID	    int	            Checked //25
        ////Major3NodeID	    int	            Checked //26 
        ////Minor1NodeID	    int	            Checked //27
        ////Minor2NodeID	    int	            Checked //28
        ////Minor3NodeID	    int	            Checked //29
        ////CreatedBy	        int	            Unchecked //30
        ////CreatedDate	    datetime	    Unchecked //31
        ////ModifiedBy	    int	            Checked //32
        ////ModifiedDate	    datetime	    Checked //33

        /*New Column*/
        //PersonID          int 	        Checked //34
        //AccountsHeadCode  varchar(50)	    Checked //35 [deleted]
        //AccountHeadsID    int
        //PaymentSlNo       varchar(50)	    Checked //36        
        //isactive        
        //isDeleted
        //IsDiploma        
        //remarks           varchar(500)	Checked //37
        #endregion

        #region Variables
        private string _roll;
       
        private int _programID;
        private Program _program;
        private Nullable<decimal> _totalDue;
        private Nullable<decimal> _totalPaid;
        private Nullable<decimal> _balance;
        private int _tuitionSetUpID;
        private int _waiverSetUpID;
        private int _discountSetUpID;
        private int _relationTypeID;
        private int _relativeID;
        private int _treeMasterID;
        private TreeMaster _treeMaster;
        private int _major1NodeID;
        private int _major2NodeID;
        private int _major3NodeID;
        private int _minor1NodeID;
        private int _minor2NodeID;
        private int _minor3NodeID;

        //Missing Field
        //private int _createdBy;
        //public int CreatedBy{get { return _createdBy; } set{ _createdBy = value; } }

        //private DateTime _createdDate;
        //public DateTime CreatedDate{get { return _createdDate; } set{ _createdDate = value; } }

        //private int _modifiedBy;
        //public int ModifiedBy { get { return _modifiedBy; } set { _modifiedBy = value; } }

        //private DateTime _modifiedDate;
        //public DateTime ModifiedDate { get { return _modifiedDate; } set { _modifiedDate = value; } }

        //private int _candidateId;
        //public int CandidateId { get { return _candidateId; } set { _candidateId = value; } }

        //private string _history;
        //public string History { get { return _history; } set { _history = value; } }

        //private string _attribute1;
        //public string Attribute1 { get { return _attribute1; } set { _attribute1 = value; } }

        //private string _attribute2;
        //public string Attribute2 { get { return _attribute2; } set { _attribute2 = value; } }

        //private int _treeCalendarMasterID;
        //public int TreeCalendarMasterID { get { return _treeCalendarMasterID; } set { _treeCalendarMasterID = value; } }
        //Missing Field

        //e
        private int _personID;
        private Person _person;
        private int _accountHeadsId;
        private string _paymentSlNo;
        private string _remarks;
        private string _stdName;
        private string _treeName;
        private bool _isActive;
        private bool _isDeleted;
        private bool _isDiploma;

        private bool _pre_English;
        public bool Pre_English
        {
            get { return _pre_English; }
            set { _pre_English = value; }
        }
        private bool _pre_Math;
        public bool Pre_Math
        {
            get { return _pre_Math; }
            set { _pre_Math = value; }
        }
        private bool _isProvisionalAdmission;
        public bool IsProvisionalAdmission
        {
            get { return _isProvisionalAdmission; }
            set { _isProvisionalAdmission = value; }
        }
        private DateTime _validUptoProAdmissionDate;
        public DateTime ValidUptoProAdmissionDate
        {
            get { return _validUptoProAdmissionDate; }
            set { _validUptoProAdmissionDate = value; }
        }

        #endregion

        #region Constructor
        public Student()
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
            _program = null;
            _totalDue = 0;
            _totalPaid = 0;
            _balance = 0;
            _tuitionSetUpID = 0;
            _waiverSetUpID = 0;
            _discountSetUpID = 0;
            _relationTypeID = 0;
            _relativeID = 0;
            _treeMasterID = 0;
            _treeMaster = null;
            _major1NodeID = 0;
            _major2NodeID = 0;
            _major3NodeID = 0;
            _minor1NodeID = 0;
            _minor2NodeID = 0;
            _minor3NodeID = 0;

            _personID = 0;
            _person = null;
            _stdName = string.Empty;
            _treeName = string.Empty;
            _isActive = false;
            _isDeleted = false;
            _isDiploma = false;
            _accountHeadsId = 0;
            _paymentSlNo = string.Empty;
            _remarks = string.Empty;

            _pre_English = false;
            _pre_Math = false;
            IsProvisionalAdmission = false;
            _validUptoProAdmissionDate = DateTime.Now;
        }
        #endregion

        #region Constants

        #region Column Constants
        private const string STUDENTID = "StudentID";//0

        private const string ROLL = "Roll";//1
        private const string ROLL_PA = "@Roll";

        //private const string PREFIX = "Prefix";//2
        //private const string PREFIX_PA = "@Prefix";

        //private const string FIRSTNAME = "FirstName";//3
        //private const string FIRSTNAME_PA = "@FirstName";

        //private const string MIDDLENAME = "MiddleName";//4
        //private const string MIDDLENAME_PA = "@MiddleName";

        //private const string LASTNAME = "LastName";//5
        //private const string LASTNAME_PA = "@LastName";

        //private const string NICKOROTHERNAME = "NickOrOtherName";//6
        //private const string NICKOROTHERNAME_PA = "@NickOrOtherName";

        //private const string DOBCON = "DOB";//7
        //private const string DOBCON_PA = "@DOB";

        //private const string GENDER = "Gender";//8
        //private const string GENDER_PA = "@Gender";

        //private const string MATRIALSTATUS = "MatrialStatus";//9
        //private const string MATRIALSTATUS_PA = "@MatrialStatus";

        //private const string BLOODGROUP = "BloodGroup";//10
        //private const string BLOODGROUP_PA = "@BloodGroup";

        //private const string RELIGIONID = "ReligionID";//11
        //private const string RELIGIONID_PA = "@ReligionID";

        //private const string NATIONALITYID = "NationalityID";//12
        //private const string NATIONALITYID_PA = "@NationalityID";

        //private const string PHOTOPATH = "PhotoPath";//13
        //private const string PHOTOPATH_PA = "@PhotoPath";

        private const string PROGRAMID = "ProgramID";//14
        private const string PROGRAMID_PA = "@ProgramID";

        private const string TOTALDUE = "TotalDue";//15
        private const string TOTALDUE_PA = "@TotalDue";

        private const string TOTALPAID = "TotalPaid";//16
        private const string TOTALPAID_PA = "@TotalPaid";

        private const string BALANCE = "Balance";//17
        private const string BALANCE_PA = "@Balance";

        private const string TUITIONSETUPID = "TuitionSetUpID";//18
        private const string TUITIONSETUPID_PA = "@TuitionSetUpID";

        private const string WAIVERSETUPID = "WaiverSetUpID";//19
        private const string WAIVERSETUPID_PA = "@WaiverSetUpID";

        private const string DISCOUNTSETUPID = "DiscountSetUpID";//20
        private const string DISCOUNTSETUPID_PA = "@DiscountSetUpID";

        private const string RELATIONTYPEID = "RelationTypeID";//21
        private const string RELATIONTYPEID_PA = "@RelationTypeID";

        private const string RELATIVEID = "RelativeID";//22
        private const string RELATIVEID_PA = "@RelativeID";

        private const string TREEMASTERID = "TreeMasterID";//23
        private const string TREEMASTERID_PA = "@TreeMasterID";

        private const string MAJOR1NODEID = "Major1NodeID";//24
        private const string MAJOR1NODEID_PA = "@Major1NodeID";

        private const string MAJOR2NODEID = "Major2NodeID";//25
        private const string MAJOR2NODEID_PA = "@Major2NodeID";

        private const string MAJOR3NODEID = "Major3NodeID";//26
        private const string MAJOR3NODEID_PA = "@Major3NodeID";

        private const string MINOR1NODEID = "Minor1NodeID";//27
        private const string MINOR1NODEID_PA = "@Minor1NodeID";

        private const string MINOR2NODEID = "Minor2NodeID";//28
        private const string MINOR2NODEID_PA = "@Minor2NodeID";

        private const string MINOR3NODEID = "Minor3NodeID";//29
        private const string MINOR3NODEID_PA = "@Minor3NodeID";

        //e
        private const string PERSONID = "PersonID";
        private const string PERSONID_PA = "@PersonID";

        private const string ACCOUNTHEADSID = "AccountHeadsId";
        private const string ACCOUNTHEADSID_PA = "@AccountHeadsId";

        private const string PAYMENTSLNO = "PaymentSlNo";
        private const string PAYMENTSLNO_PA = "@PaymentSlNo";

        private const string REMARKS = "Remarks";
        private const string REMARKS_PA = "@Remarks";

        private const string ISACTIVE = "IsActive";
        private const string ISACTIVE_PA = "@IsActive";

        private const string ISDELETED = "IsDeleted";
        private const string ISDELETED_PA = "@IsDeleted";

        private const string ISDIPLOMA = "IsDiploma";
        private const string ISDIPLOMA_PA = "@IsDiploma";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + STUDENTID + "], "//0
                                        + "[" + ROLL + "], "//1
            //+ "[" + PREFIX + "], "//2
            //+ "[" + FIRSTNAME + "], "//3
            //+ "[" + MIDDLENAME + "], "//4
            //+ "[" + LASTNAME + "], "//5
            //+ "[" + NICKOROTHERNAME + "], "//6
            //+ "[" + DOBCON + "], "//7
            //+ "[" + GENDER + "], "//8
            //+ "[" + MATRIALSTATUS + "], "//9
            //+ "[" + BLOODGROUP + "], "//10
            //+ "[" + RELIGIONID + "], "//11
            //+ "[" + NATIONALITYID + "], "//12
            //+ "[" + PHOTOPATH + "], "//13
                                        + "[" + PROGRAMID + "], "//14
                                        + "[" + TOTALDUE + "], "//15
                                        + "[" + TOTALPAID + "], "//16
                                        + "[" + BALANCE + "], "//17
                                        + "[" + TUITIONSETUPID + "], "//18
                                        + "[" + WAIVERSETUPID + "], "//19
                                        + "[" + DISCOUNTSETUPID + "], "//20
                                        + "[" + RELATIONTYPEID + "], "//21
                                        + "[" + RELATIVEID + "], "//22
                                        + "[" + TREEMASTERID + "], "//23
                                        + "[" + MAJOR1NODEID + "], "//24
                                        + "[" + MAJOR2NODEID + "], "//25
                                        + "[" + MAJOR3NODEID + "], "//26
                                        + "[" + MINOR1NODEID + "], "//27
                                        + "[" + MINOR2NODEID + "], "//28
                                        + "[" + MINOR3NODEID + "], "//29
                                        + "[" + PERSONID + "], "
                                        + "[" + ACCOUNTHEADSID + "], "
                                        + "[" + PAYMENTSLNO + "], "
                                        + "[" + ISACTIVE + "], "
                                        + "[" + ISDELETED + "], "
                                        + "[" + ISDIPLOMA + "], "
                                        + "[" + REMARKS + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + ROLL + "], "//1
            //+ "[" + PREFIX + "], "//2
            //+ "[" + FIRSTNAME + "], "//3
            //+ "[" + MIDDLENAME + "], "//4
            //+ "[" + LASTNAME + "], "//5
            //+ "[" + NICKOROTHERNAME + "], "//6
            //+ "[" + DOBCON + "], "//7
            //+ "[" + GENDER + "], "//8
            //+ "[" + MATRIALSTATUS + "], "//9
            //+ "[" + BLOODGROUP + "], "//10
            //+ "[" + RELIGIONID + "], "//11
            //+ "[" + NATIONALITYID + "], "//12
            //+ "[" + PHOTOPATH + "], "//13
                                        + "[" + PROGRAMID + "], "//14
                                        + "[" + TOTALDUE + "], "//15
                                        + "[" + TOTALPAID + "], "//16
                                        + "[" + BALANCE + "], "//17
                                        + "[" + TUITIONSETUPID + "], "//18
                                        + "[" + WAIVERSETUPID + "], "//19
                                        + "[" + DISCOUNTSETUPID + "], "//20
                                        + "[" + RELATIONTYPEID + "], "//21
                                        + "[" + RELATIVEID + "], "//22
                                        + "[" + TREEMASTERID + "], "//23
                                        + "[" + MAJOR1NODEID + "], "//24
                                        + "[" + MAJOR2NODEID + "], "//25
                                        + "[" + MAJOR3NODEID + "], "//26
                                        + "[" + MINOR1NODEID + "], "//27
                                        + "[" + MINOR2NODEID + "], "//28
                                        + "[" + MINOR3NODEID + "], "//29
                                        + "[" + PERSONID + "], "
                                        + "[" + ACCOUNTHEADSID + "], "
                                        + "[" + PAYMENTSLNO + "], "
                                        + "[" + ISACTIVE + "], "
                                        + "[" + ISDELETED + "], "
                                        + "[" + ISDIPLOMA + "], "
                                        + "[" + REMARKS + "], ";
        #endregion

        private const string TABLENAME = " [Student] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + ALLCOLUMNS
                     + BASEMUSTCOLUMNS + ")"
                     + "VALUES ( "
                     + ID_PA + ", "
                     + ROLL_PA + ", "//1
            //+ PREFIX_PA + ", "//2
            //+ FIRSTNAME_PA + ", "//3
            //+ MIDDLENAME_PA + ", "//4
            //+ LASTNAME_PA + ", "//5
            //+ NICKOROTHERNAME_PA + ", "//6
            //+ DOBCON_PA + ", "//7
            //+ GENDER_PA + ", "//8
            //+ MATRIALSTATUS_PA + ", "//9
            //+ BLOODGROUP_PA + ", "//10
            //+ RELIGIONID_PA + ", "//11
            //+ NATIONALITYID_PA + ", "//12
            //+ PHOTOPATH_PA + ", "//13
                    + PROGRAMID_PA + ", "//14
                    + TOTALDUE_PA + ", "//15
                    + TOTALPAID_PA + ", "//16
                    + BALANCE_PA + ", "//17
                    + TUITIONSETUPID_PA + ", "//18
                    + WAIVERSETUPID_PA + ", "//19
                    + DISCOUNTSETUPID_PA + ", "//20
                    + RELATIONTYPEID_PA + ", "//21
                    + RELATIVEID_PA + ", "//22
                    + TREEMASTERID_PA + ", "//23
                    + MAJOR1NODEID_PA + ", "//24
                    + MAJOR2NODEID_PA + ", "//25
                    + MAJOR3NODEID_PA + ", "//26
                    + MINOR1NODEID_PA + ", "//27
                    + MINOR2NODEID_PA + ", "//28
                    + MINOR3NODEID_PA + ", " //29

                    + PERSONID_PA + ", "
                    + ACCOUNTHEADSID_PA + ", "
                    + PAYMENTSLNO_PA + ", "
                    + ISACTIVE_PA + ", "
                    + ISDELETED_PA + ", "
                    + ISDIPLOMA_PA + ", "
                    + REMARKS_PA + ", "

                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ")";
        #endregion

        #region UPDATE
        private const string UPDATE = " UPDATE " + TABLENAME
                    + "SET [" + ROLL + "] = " + ROLL_PA + ", "//1
            //+ "[" + PREFIX + "] = " + PREFIX_PA + ", "//2
            //+ "[" + FIRSTNAME + "] = " + FIRSTNAME_PA + ", "//3
            //+ "[" + MIDDLENAME + "] = " + MIDDLENAME_PA + ", "//4
            //+ "[" + LASTNAME + "] = " + LASTNAME_PA + ", "//5
            //+ "[" + NICKOROTHERNAME + "] = " + NICKOROTHERNAME_PA + ", "//6
            //+ "[" + DOBCON + "] = " + DOBCON_PA + ", "//7
            //+ "[" + GENDER + "] = " + GENDER_PA + ", "//8
            //+ "[" + MATRIALSTATUS + "] = " + MATRIALSTATUS_PA + ", "//9
            //+ "[" + BLOODGROUP + "] = " + BLOODGROUP_PA + ", "//10
            //+ "[" + RELIGIONID + "] = " + RELIGIONID_PA + ", "//11
            //+ "[" + NATIONALITYID + "] = " + NATIONALITYID_PA + ", "//12
            //+ "[" + PHOTOPATH + "] = " + PHOTOPATH_PA + ", "//13
                    + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "//14
                    + "[" + TOTALDUE + "] = " + TOTALDUE_PA + ", "//15
                    + "[" + TOTALPAID + "] = " + TOTALPAID_PA + ", "//16
                    + "[" + BALANCE + "] = " + BALANCE_PA + ", "//17
                    + "[" + TUITIONSETUPID + "] = " + TUITIONSETUPID_PA + ", "//18
                    + "[" + WAIVERSETUPID + "] = " + WAIVERSETUPID_PA + ", "//19
                    + "[" + DISCOUNTSETUPID + "] = " + DISCOUNTSETUPID_PA + ", "//20
                    + "[" + RELATIONTYPEID + "] = " + RELATIONTYPEID_PA + ", "//21
                    + "[" + RELATIVEID + "] = " + RELATIVEID_PA + ", "//22
                    + "[" + TREEMASTERID + "] = " + TREEMASTERID_PA + ", "//23
                    + "[" + MAJOR1NODEID + "] = " + MAJOR1NODEID_PA + ", "//24
                    + "[" + MAJOR2NODEID + "] = " + MAJOR2NODEID_PA + ", "//25
                    + "[" + MAJOR3NODEID + "] = " + MAJOR3NODEID_PA + ", "//26
                    + "[" + MINOR1NODEID + "] = " + MINOR1NODEID_PA + ", "//27
                    + "[" + MINOR2NODEID + "] = " + MINOR2NODEID_PA + ", "//28
                    + "[" + MINOR3NODEID + "] = " + MINOR3NODEID_PA + ", " //29
                    + "[" + PERSONID + "] = " + PERSONID_PA + ", "
                    + "[" + ACCOUNTHEADSID + "] = " + ACCOUNTHEADSID_PA + ", "
                    + "[" + PAYMENTSLNO + "] = " + PAYMENTSLNO_PA + ", "
                    + "[" + ISACTIVE + "] = " + ISACTIVE_PA + ", "
                    + "[" + ISDELETED + "] = " + ISDELETED_PA + ", "
                    + "[" + ISDIPLOMA + "] = " + ISDIPLOMA_PA + ", "
                    + "[" + REMARKS + "] = " + REMARKS_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
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
        private SqlParameter RollParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ROLL_PA;
                if (Roll == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Roll;
                }
                return sqlParam;
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
        private SqlParameter ProgramIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROGRAMID_PA;
                if (ProgramID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ProgramID;
                }
                return sqlParam;
            }
        }
        public Program StudentProgram
        {
            get
            {
                if (_program == null)
                {
                    if (this.ProgramID > 0)
                    {
                        _program = Program.GetProgram(this.ProgramID);
                    }
                }
                return _program;
            }
        }

        public Nullable<decimal> TotalDue
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
        private SqlParameter TotalDueParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TOTALDUE_PA;
                if (!TotalDue.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = TotalDue.Value;
                }
                return sqlParam;
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
        private SqlParameter TotalPaidParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TOTALPAID_PA;
                if (!TotalPaid.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = TotalPaid.Value;
                }
                return sqlParam;
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
        private SqlParameter BalanceParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = BALANCE_PA;
                if (!Balance.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Balance.Value;
                }
                return sqlParam;
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
        private SqlParameter TuitionSetUpIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TUITIONSETUPID_PA;
                if (TuitionSetUpID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = TuitionSetUpID;
                }
                return sqlParam;
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
        private SqlParameter WaiverSetUpIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = WAIVERSETUPID_PA;
                if (WaiverSetUpID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = WaiverSetUpID;
                }
                return sqlParam;
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
        private SqlParameter DiscountSetUpIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DISCOUNTSETUPID_PA;
                if (DiscountSetUpID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = DiscountSetUpID;
                }
                return sqlParam;
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
        private SqlParameter RelationTypeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = RELATIONTYPEID_PA;
                if (RelationTypeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = RelationTypeID;
                }
                return sqlParam;
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
        private SqlParameter RelativeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = RELATIVEID_PA;
                if (RelativeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = RelativeID;
                }
                return sqlParam;
            }
        }

        public int TreeMasterID
        {
            get { return _treeMasterID; }
            set { _treeMasterID = value; }
        }
        private SqlParameter TreeMasterIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TREEMASTERID_PA;
                if (TreeMasterID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = TreeMasterID;
                }
                return sqlParam;
            }
        }
        public TreeMaster StudentTreeMaster
        {
            get
            {
                if (_treeMaster == null)
                {
                    if (this.TreeMasterID > 0)
                    {
                        _treeMaster = TreeMaster.Get(this.TreeMasterID);
                    }
                }
                return _treeMaster;
            }
        }

        public int Major1NodeID
        {
            get { return _major1NodeID; }
            set { _major1NodeID = value; }
        }
        private SqlParameter Major1NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAJOR1NODEID_PA;
                if (Major1NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Major1NodeID;
                }
                return sqlParam;
            }
        }

        public int Major2NodeID
        {
            get { return _major2NodeID; }
            set { _major2NodeID = value; }
        }
        private SqlParameter Major2NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAJOR2NODEID_PA;
                if (Major2NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Major2NodeID;
                }
                return sqlParam;
            }
        }

        public int Major3NodeID
        {
            get { return _major3NodeID; }
            set { _major3NodeID = value; }
        }
        private SqlParameter Major3NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAJOR3NODEID_PA;
                if (Major3NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Major3NodeID;
                }
                return sqlParam;
            }
        }

        public int Minor1NodeID
        {
            get { return _minor1NodeID; }
            set { _minor1NodeID = value; }
        }
        private SqlParameter Minor1NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MINOR1NODEID_PA;
                if (Minor1NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Minor1NodeID;
                }
                return sqlParam;
            }
        }

        public int Minor2NodeID
        {
            get { return _minor2NodeID; }
            set { _minor2NodeID = value; }
        }
        private SqlParameter Minor2NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MINOR2NODEID_PA;
                if (Minor2NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Minor2NodeID;
                }
                return sqlParam;
            }
        }

        public int Minor3NodeID
        {
            get { return _minor3NodeID; }
            set { _minor3NodeID = value; }
        }
        private SqlParameter Minor3NodeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MINOR3NODEID_PA;
                if (Minor3NodeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Minor3NodeID;
                }
                return sqlParam;
            }
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
        private SqlParameter PersonIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PERSONID_PA;
                if (PersonID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = PersonID;
                }
                return sqlParam;
            }
        }
        public Person StuPerson
        {
            get
            {
                if (_person == null)
                {
                    if (this.PersonID > 0)
                    {
                        _person = Person.GetPerson(this.PersonID);
                    }
                }
                return _person;
            }
        }


        public string StdName
        {
            get
            {
                if (this.PersonID > 0)
                {
                    _stdName = Person.GetPerson(this.PersonID).PersonName;
                }
                return _stdName;
            }
        }

        public string TreeName
        {
            get
            {
                if (this.TreeMasterID > 0)
                {
                    _treeName = TreeMaster.Get(this.TreeMasterID).RootNode.Name;
                }
                return _treeName;
            }
        }

        public int AcHeadsId
        {
            get { return _accountHeadsId; }
            set { _accountHeadsId = value; }
        }
        private SqlParameter AcHeadsIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ACCOUNTHEADSID_PA;
                if (AcHeadsId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AcHeadsId;
                }
                return sqlParam;
            }
        }

        public string PaymentSlNo
        {
            get { return _paymentSlNo; }
            set { _paymentSlNo = value; }
        }
        private SqlParameter PaymentSlNoParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PAYMENTSLNO_PA;
                if (PaymentSlNo == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = PaymentSlNo;
                }
                return sqlParam;
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private SqlParameter IsActiveParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISACTIVE_PA;
                sqlParam.Value = IsActive;

                return sqlParam;
            }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        private SqlParameter IsDeletedParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISDELETED_PA;
                sqlParam.Value = IsDeleted;

                return sqlParam;
            }
        }

        public bool IsDiploma
        {
            get { return _isDiploma; }
            set { _isDiploma = value; }
        }
        private SqlParameter IsDiplomaParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISDIPLOMA_PA;
                sqlParam.Value = IsDiploma;

                return sqlParam;
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private SqlParameter RemarksParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REMARKS_PA;
                if (Remarks == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Remarks;
                }
                return sqlParam;
            }
        }
        #endregion

        #region Functions
        private static Student studentMapper(SQLNullHandler nullHandler)
        {
            Student student = new Student();

            student.Id = nullHandler.GetInt32(STUDENTID);
            student.Roll = nullHandler.GetString(ROLL);
            //student.StudentPrefix = (Prefix)nullHandler.GetInt32(PREFIX);
            //student.FirstName = nullHandler.GetString(FIRSTNAME);
            //student.MiddleName = nullHandler.GetString(MIDDLENAME);
            //student.LastName = nullHandler.GetString(LASTNAME);
            //student.NickOrOtherName = nullHandler.GetString(NICKOROTHERNAME);
            student.ProgramID = nullHandler.GetInt32(PROGRAMID);
            student.TreeMasterID = nullHandler.GetInt32(TREEMASTERID);

            //new column
            student.PersonID = nullHandler.GetInt32(PERSONID);
            student.AcHeadsId = nullHandler.GetInt32(ACCOUNTHEADSID);
            student.PaymentSlNo = nullHandler.GetString(PAYMENTSLNO);
            student.IsActive = nullHandler.GetBoolean(ISACTIVE);
            student.IsDeleted = nullHandler.GetBoolean(ISDELETED);
            student.IsDiploma = nullHandler.GetBoolean(ISDIPLOMA);
            student.Remarks = nullHandler.GetString(REMARKS);
            //

            student.CreatorID = nullHandler.GetInt32(CREATORID);
            student.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            student.ModifierID = nullHandler.GetInt32(MODIFIERID);
            student.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return student;
        }

        private static List<Student> mapStudents(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Student> students = null;

            while (theReader.Read())
            {
                if (students == null)
                {
                    students = new List<Student>();
                }
                Student student = studentMapper(nullHandler);
                students.Add(student);
            }

            return students;
        }

        private static Student mapStudent(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Student student = null;
            if (theReader.Read())
            {
                student = new Student();
                student = studentMapper(nullHandler);
            }

            return student;
        }

        #region Old
        //private static List<Student> imapStudents(SQLNullHandler nullHandle)
        //{
        //    List<Student> students = new List<Student>();
        //    while (theReader.Read())
        //    {
        //        Student student = new Student();
        //        student.StudentID = theReader.GetInt32(theReader.GetOrdinal("StudentID"));
        //        student.Roll = theReader.GetString(theReader.GetOrdinal("Roll"));
        //        student.Prefix = theReader.GetString(theReader.GetOrdinal("Prefix"));
        //        student.FirstName =  theReader.GetString(theReader.GetOrdinal("FirstName");
        //        student.MiddleName = theReader.GetString(theReader.GetOrdinal("MiddleName"));
        //        student.LastName = theReader.GetString(theReader.GetOrdinal("LastName"));
        //        student.NickOrOtherName = theReader.GetString(theReader.GetOrdinal("NickOrOtherName"));
        //        students.Add(student);
        //    }
        //    return students;
        //}
        //private static Student imapStudent(SQLNullHandler nullHandle)
        //{
        //    Student student = new Student();
        //    if (theReader.Read())
        //    {
        //        student.StudentID = theReader.GetInt32(theReader.GetOrdinal("StudentID"));
        //        student.Roll = theReader.GetString(theReader.GetOrdinal("Roll"));
        //        student.Prefix = theReader.GetString(theReader.GetOrdinal("Prefix"));
        //        student.FirstName = theReader.GetString(theReader.GetOrdinal("FirstName"));
        //        student.MiddleName = theReader.GetString(theReader.GetOrdinal("MiddleName"));
        //        student.LastName = theReader.GetString(theReader.GetOrdinal("LastName"));
        //        student.NickOrOtherName = theReader.GetString(theReader.GetOrdinal("NickOrOtherName"));
        //    }
        //    return student;
        //} 
        #endregion

        public static List<Student> GetStudentsByRoll(string parameters)
        {
            List<Student> students = new List<Student>();

            string command = SELECT
                            + "WHERE [" + ROLL + "] IN (" + parameters + ")";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return students;
        }

        public static Student GetStudentByRoll(string roll)
        {
            Student student = new Student();

            string command = SELECT
                            + "WHERE [" + ROLL + "] = " + ROLL_PA;

            SqlParameter sqlParam = new SqlParameter(ROLL_PA, roll);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            student = mapStudent(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return student;
        }

        public static string GetStudentNameByRoll(string roll)
        {
            string name = string.Empty;

            string command = " SELECT     (isnull(Person.FirstName,'')+' '+ isnull(Person.MiddleName,'')+' '+ isnull(Person.LastName,'') +' '+ isnull(Person.NickOrOtherName,'')) as 'NAME' FROM Student INNER JOIN Person ON Student.PersonID = Person.PersonID  "
                            + "WHERE [" + ROLL + "] = " + ROLL_PA;

            SqlParameter sqlParam = new SqlParameter(ROLL_PA, roll);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            while (theReader.Read())
            {
                name = theReader["NAME"].ToString();
            }

            //student = mapStudent(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return name.Trim();
        }

        public static List<Student> GetStudents(string parameters)
        {
            List<Student> students = new List<Student>();

            string command = SELECT
                            + "WHERE [" + ROLL + "] Like '%" + parameters + "%' and IsDeleted = 0 ";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return students;
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            string command = SELECT + " WHERE IsDeleted = 0 ";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return students;
        }

        public static Student GetStudent(int studentID)
        {
            Student student = new Student();

            string command = SELECT
                            + "WHERE [" + STUDENTID + "] = " + ID_PA;

            SqlParameter sqlParam = new SqlParameter(ID_PA, studentID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            student = mapStudent(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return student;
        }

        public static List<Student> GetStudents(int studentID)
        {
            List<Student> students = new List<Student>();

            string command = SELECT
                            + "WHERE  [" + STUDENTID + "] = " + ID_PA;

            SqlParameter studentIdParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { studentIdParam });

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        public static List<Student> GetStudentsByProgID(int progID)
        {
            List<Student> students = new List<Student>();

            string command = SELECT;
            if (progID > 0)
            {
                command = command + "WHERE ProgramID = " + progID;
            }

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        public static List<Student> GetStudentsByProgAndAdminCalID(int progID, int adminCalID)
        {
            List<Student> students = new List<Student>();

            string command = string.Empty;
            command = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetStudentsByProgAdminCalID] @progID = " + progID + ", @adminCalID = " + adminCalID + " SELECT 'Return Value' = @return_value";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        public static List<Student> GetStudentsByProgAndAdminCalIDWithTreemasterId(int progID, int adminCalID)
        {
            List<Student> students = new List<Student>();

            string command = string.Empty;
            command = "select std.* from Admission ad, Student std where ad.AdmissionCalenderID = " + adminCalID + " and ad.StudentID = std.StudentID and std.ProgramID = " + progID + " and std.treemasterid != ''";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        internal static Student GetStudent(int studentID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            Student student = new Student();

            string command = SELECT
                            + "WHERE [" + STUDENTID + "] = " + ID_PA;

            SqlParameter sqlParam = new SqlParameter(ID_PA, studentID);

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            student = mapStudent(theReader);
            theReader.Close();
            //MSSqlConnectionHandler.CloseDbConnection();
            return student;
        }

        internal static int GetMaxStudentID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newStdID = 0;

            string command = "SELECT MAX(" + STUDENTID + ") FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newStdID = 1;
            }
            else if (ob is Int32)
            {
                newStdID = Convert.ToInt32(ob) + 1;
            }

            return newStdID;
        }

        public static int SaveStudent(Student obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                command = INSERT;

                obj.Id = Student.GetMaxStudentID(sqlConn, sqlTran);
                sqlParams = new SqlParameter[] { obj.IDParam,
                                                 obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam };//+ MOIDFIEDDATE_PA + ")";//15
            }
            else
            {

                command = UPDATE
                + " WHERE [" + STUDENTID + "] = " + ID_PA;

                sqlParams = new SqlParameter[] { obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam,
                                                 obj.IDParam};//+ MOIDFIEDDATE_PA + ")";//15
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            return obj.Id;
        }

        public static int SaveStudents(List<Student> students)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Student obj in students)
                {
                    string command = string.Empty;
                    SqlParameter[] sqlParams = null;

                    if (obj.Id == 0)
                    {
                        command = INSERT;
                        obj.Id = Student.GetMaxStudentID(sqlConn, sqlTran);
                        sqlParams = new SqlParameter[] { obj.IDParam,
                                                 obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam };//+ MOIDFIEDDATE_PA + ")";//15
                    }
                    else
                    {

                        command = UPDATE
                        + " WHERE [" + STUDENTID + "] = " + ID_PA;

                        sqlParams = new SqlParameter[] { obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam,
                                                 obj.IDParam};//+ MOIDFIEDDATE_PA + ")";//15
                    }
                    ////counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    if (obj.TreeMasterID != 0 && obj.Id != 0)
                    {
                        Student std = new Student();
                        std = GetStudent(obj.Id, sqlConn, sqlTran);
                        if (std != null)
                        {
                            if (std.TreeMasterID == 0 || std.TreeMasterID != obj.TreeMasterID)
                            {
                                TreeCalendarMaster treeCalMas = TreeCalendarMaster.GetByTreeMasterID(obj.TreeMasterID, sqlConn, sqlTran);
                                List<TreeCalendarDetail> treeCalDetails = TreeCalendarDetail.GetByTreeCalMaster(treeCalMas.Id, sqlConn, sqlTran);

                                List<Cal_Course_Prog_Node> ccpns = new List<Cal_Course_Prog_Node>();
                                foreach (TreeCalendarDetail tcd in treeCalDetails)
                                {
                                    List<Cal_Course_Prog_Node> ccpnsTemp = Cal_Course_Prog_Node.GetByTreeCalDet(tcd.Id, sqlConn, sqlTran);
                                    foreach (Cal_Course_Prog_Node cc in ccpnsTemp)
                                    {
                                        ccpns.Add(cc);
                                    }
                                }
                                // assign data to StudentCalCourseProgNode
                                // AssignStudentToCalCrsProgNode.Save(obj.Id, obj.ModifierID, std.ModifiedDate, ccpns, sqlConn, sqlTran); 
                            }
                            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                        }
                    }
                    else
                    {
                        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    }
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception Ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose();
                }
                throw Ex;
            }
        }

        public static int DeleteStudent(int studentID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "UPDATE Student" + " SET IsActive = 0, IsDeleted = 1 WHERE StudentID = " + studentID;

            //string command = DELETE
            //                + "WHERE [" + STUDENTID + "] =" + studentID.ToString();
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }

        public static int NewStudentRoll(string rollPrefix)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(" + STUDENTID + ") FROM" + TABLENAME
                            + "WHERE [" + ROLL + "] LIKE '" + rollPrefix + "%'";

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return Convert.ToInt32(ob) + 1;
        }

        public static int SaveStudent(Student obj)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.IDParam,
                                                 obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam };//+ MOIDFIEDDATE_PA + ")";//15
            }
            else
            {

                command = UPDATE
                + " WHERE [" + STUDENTID + "] = " + ID_PA;

                sqlParams = new SqlParameter[] { obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam,
                                                 obj.IDParam};//+ MOIDFIEDDATE_PA + ")";//15
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParams);

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }

        public static int SaveStudent(Student student, Person person, Admission admission, AccountsHead accountHead, Voucher voucher, SubmittedPapersEntity submittedPapers)
        {
            int counter = 0;
            try
            {
                int studentId = 0;
                int stdAccountHeadId = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                if (person.Id != 0) //update
                {
                    student.PersonID = Person.savePerson(person, sqlConn, sqlTran);//Data Update in Person table

                    SaveStudentWithPersonID(student, sqlConn, sqlTran);//Data Update in Student table
                }
                else //Insert
                {
                    student.PersonID = Person.savePerson(person, sqlConn, sqlTran);//Data save in Person table
                    admission.PersonId = student.PersonID;//set

                    submittedPapers.Personid = student.PersonID;//set
                    SubmittedPapers_DAO.SaveSubmittedPapers(submittedPapers, sqlConn, sqlTran);

                    #region Student Roll Generation
                    string roll = Program.GetProgramCode(student.ProgramID, sqlConn, sqlTran) + AcademicCalender.GetCode(admission.AdmissionCalId, sqlConn, sqlTran);
                    student.Roll = roll.Trim() + PadSequence(NewStudentRoll(roll, sqlConn, sqlTran)).Trim();
                    #endregion

                    studentId = SaveStudentWithPersonID(student, sqlConn, sqlTran);//Data save in Student table with personID
                    admission.StudentId = studentId;//set

                    Admission.saveAdmission(admission, sqlConn, sqlTran);//Data save in Admission table with studentID and personID

                    accountHead.Name = student.Roll;//set
                    stdAccountHeadId = AccountsHead.saveStdAccount(accountHead, sqlConn, sqlTran);//Data save in Account table

                    updateStdAcHeadsId(studentId, stdAccountHeadId, sqlConn, sqlTran);

                    voucher.SlNo = Voucher.GetMaxSlNo();
                    Voucher.saveVoucherCrAccount(voucher, sqlConn, sqlTran);//Data save in Voucher table(CR amount)

                    voucher.DrAcHeadsId = stdAccountHeadId;
                    Voucher.saveVoucherDrAccount(voucher, sqlConn, sqlTran);//Data save in Voucher table (DR amount)



                    // Candidate_DAO.UpdateCandidateStatus(person.CandidateId, student.Roll, sqlConn, sqlTran);
                }
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                counter = 1;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }

        private static int updateStdAcHeadsId(int studentId, int stuAcHeadsId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            string command = string.Empty;
            command = "UPDATE Student"
                + " SET AccountHeadsID = " + stuAcHeadsId + " WHERE StudentID = " + studentId;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        private static int NewStudentRoll(string roll, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(" + STUDENTID + ") FROM" + TABLENAME
                           + "WHERE [" + ROLL + "] LIKE '" + roll + "%'";

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (Convert.ToInt32(ob) == 0)
            { return 1; }
            else
            {
                return Convert.ToInt32(ob) + 1;
            }
        }

        private static int resetIsactive(int personId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            string command = string.Empty;
            command = "UPDATE Student"
                + " SET IsActive = 0 WHERE IsActive = 1 and PersonID = " + personId;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        private static int resetIsDeleted(int personId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            string command = string.Empty;
            command = "UPDATE Student"
                + " SET IsActive = 0 WHERE PersonID = " + personId;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        private static int SaveStudentWithPersonID(Student obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (obj.IsActive)
                {
                    resetIsactive(obj.PersonID, sqlConn, sqlTran);
                }

                if (obj.IsDeleted)
                {
                    resetIsDeleted(obj.PersonID, sqlConn, sqlTran);
                }

                if (obj.Id == 0)
                {
                    #region Insert
                    obj.Id = GetMaxStudentID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] {obj.IDParam,
                                                 obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam };
                    #endregion
                }
                else
                {
                    #region Update
                    command = UPDATE
                    + " WHERE [" + STUDENTID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam,
                                                 obj.IDParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                if (counter <= 0)
                {
                    return 0;
                }
                else
                {
                    return obj.Id;
                }
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static string PadSequence(int newRollSequence)
        {
            StringBuilder sequence = null;
            if (newRollSequence < 10)
            {
                sequence = new StringBuilder("00" + newRollSequence.ToString());
            }
            else if (newRollSequence >= 10 && newRollSequence < 100)
            {
                sequence = new StringBuilder("0" + newRollSequence.ToString());
            }
            else
            {
                sequence = new StringBuilder(newRollSequence.ToString());
            }
            return sequence.ToString();
        }

        #endregion

        public static int SaveStudents2(List<Student> students)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Student obj in students)
                {
                    string command = string.Empty;
                    SqlParameter[] sqlParams = null;

                    if (obj.Id == 0)
                    {
                        command = INSERT;
                        obj.Id = Student.GetMaxStudentID(sqlConn, sqlTran);
                        sqlParams = new SqlParameter[] { obj.IDParam,
                                                 obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam };//+ MOIDFIEDDATE_PA + ")";//15
                    }
                    else
                    {

                        command = UPDATE
                        + " WHERE [" + STUDENTID + "] = " + ID_PA;

                        sqlParams = new SqlParameter[] { obj.RollParam,
                                                 //obj.StudentPrefixParam,
                                                 //obj.FirstNameParam,
                                                 //obj.MiddleNameParam,
                                                 //obj.LastNameParam,
                                                 //obj.NickOrOtherNameParam,
                                                 //obj.DOBParam,
                                                 //obj.StudentGenderParam,
                                                 //obj.MatrialStatusParam,
                                                 //obj.StudentBloodGroupParam,
                                                 //obj.ReligionIDParam,
                                                 //obj.NationalityIDParam,
                                                 //obj.PhotoPathParam,
                                                 obj.ProgramIDParam,
                                                 obj.TotalDueParam,
                                                 obj.TotalPaidParam,
                                                 obj.BalanceParam,
                                                 obj.TuitionSetUpIDParam,
                                                 obj.WaiverSetUpIDParam,
                                                 obj.DiscountSetUpIDParam,
                                                 obj.RelationTypeIDParam,
                                                 obj.RelativeIDParam,
                                                 obj.TreeMasterIDParam,
                                                 obj.Major1NodeIDParam,
                                                 obj.Major2NodeIDParam,
                                                 obj.Major3NodeIDParam,
                                                 obj.Minor1NodeIDParam,
                                                 obj.Minor2NodeIDParam,
                                                 obj.Minor3NodeIDParam,                                                 
                                                 obj.PersonIDParam,
                                                 obj.AcHeadsIdParam,
                                                 obj.PaymentSlNoParam,
                                                 obj.IsActiveParam,
                                                 obj.IsDeletedParam,
                                                 obj.IsDiplomaParam,
                                                 obj.RemarksParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam,
                                                 obj.IDParam};//+ MOIDFIEDDATE_PA + ")";//15
                    }
                    ////counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    if (obj.TreeMasterID != 0 && obj.Id != 0)
                    {
                        AssignStudentToCalCrsProgNode.Save2(obj.Id, obj.TreeMasterID, sqlConn, sqlTran);
                        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    }
                    else
                    {
                        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    }
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception Ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose();
                }
                throw Ex;
            }
        }

        public static List<Student> GetActiveStudentsByProgAndAdminCalCode(string progCode, string adminCalCode, string roll)
        {
            List<Student> students = new List<Student>();
            string command = string.Empty;

            if (progCode == "0" && adminCalCode == "0")
            {
                command = "select Student.* "
                           + "from Student "
                           + "where Student.IsActive = 1 and Student.Roll like '" + roll + "%' "
                           + "order by Student.Roll";
            }
            else if (adminCalCode == "0" && progCode != "0")
            {
                command = "select Student.* "
                           + "from Student "
                           + "where Student.IsActive = 1 and Student.Roll like '" + roll + "%' "
                           + "and Student.ProgramID = (select Program.ProgramID from Program where Code = '" + progCode + "') "
                           + "order by Student.Roll";
            }
            else if (adminCalCode != "0" && progCode == "0")
            {
                command = "select Student.* from Student, "
                        + "("
                            + "select ad.* from Admission ad "
                            + "where ad.AdmissionCalenderID = (select AcademicCalender.AcademicCalenderID from AcademicCalender where BatchCode = '" + adminCalCode + "') "
                        + ")tbl"
                        + " where Student.StudentID = tbl.StudentID "
                                + "and Student.IsActive = 1 "
                                + "and Student.Roll like '" + roll + "%' "
                           + "order by Student.Roll";
            }
            else
            {
                command = "select Student.* from Student, "
                            + "("
                                + "select ad.* from Admission ad "
                                + "where ad.AdmissionCalenderID = (select AcademicCalender.AcademicCalenderID from AcademicCalender where BatchCode = '" + adminCalCode + "') "
                            + ")tbl"
                            + " where Student.StudentID = tbl.StudentID "
                                    + "and Student.IsActive = 1 "
                                    + "and Student.ProgramID = (select Program.ProgramID from Program where Code = '" + progCode + "') "
                                    + "and Student.Roll like '" + roll + "%' "
                           + "order by Student.Roll";
            }
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return students;
        }

        public static int GenerateWorksheet(List<Student> students)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Student obj in students)
                {
                    string command = " DECLARE	@return_value int EXEC	@return_value = [dbo].[sp_PrepareWorksheet] @StuId = " + obj.Id + ", @TreemasterID = " + obj.TreeMasterID + " SELECT	'Return Value' = @return_value ";

                    counter += (Int32)DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception Ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose();
                }
                throw Ex;
            }
        }

        public static int OpenCourses(List<Student> students)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Student obj in students)
                {
                    string command = " DECLARE	@return_value int EXEC	@return_value = [dbo].[OpenCourse] @StudentID = " + obj.Id + " SELECT	'Return Value' = @return_value ";

                    counter += (Int32)DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception Ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose();
                }
                throw Ex;
            }
        }

        public static List<Student> GetRegisteredStudents(int progID, int adminCalID)
        {
            List<Student> students = new List<Student>();

            string command = string.Empty;
            command = "select distinct( std.StudentID), std.Roll, std.Prefix, std.FirstName, std.MiddleName, std.LastName, std.NickOrOtherName, std.DOB, std.Gender, std.MatrialStatus, std.BloodGroup, std.ReligionID, std.NationalityID, std.PhotoPath, std.ProgramID, std.TotalDue, std.TotalPaid, std.Balance, std.TuitionSetUpID, std.WaiverSetUpID, std.DiscountSetUpID, std.RelationTypeID, std.RelativeID, std.TreeMasterID, std.Major1NodeID, std.Major2NodeID, std.Major3NodeID, std.Minor1NodeID, std.Minor2NodeID, std.Minor3NodeID, std.CreatedBy, std.CreatedDate, std.ModifiedBy, std.ModifiedDate,  std.PersonID, std.PaymentSlNo, std.IsActive, std.IsDeleted, std.IsDiploma, std.Remarks, std.AccountHeadsID, std.CandidateId, std.IsProvisionalAdmission, std.ValidUptoProAdmissionDate, std.Pre_English, std.Pre_Math from  Student std, StudentCourseHistory ch where std.StudentID = ch.StudentID and std.ProgramId = " + progID + "  and ch.AcaCalID = " + adminCalID + " ";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        public static List<Student> GetStudentsByBatch(int progID, int batchID)
        {
            List<Student> students = new List<Student>();

            string command = string.Empty;
            command = "select s.* from dbo.Student as S inner join Admission as A on S.StudentID = A.StudentID where s.ProgramID = " + progID ;
            if(batchID != 0)
            {
                command += " and A.AdmissionCalenderID = " + batchID ;
            }

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }

        public static List<Student> GetRegisteredStudent(int acaCalID, int intCourseID, int intVersionID, int sectionID)
        {
            List<Student> students = new List<Student>();

            string command = " select s.* " +
                        " from dbo.Student as s " +
                        " inner join StudentCourseHistory as ch " +
                        " on ch.StudentID = s.StudentID " +
                        " where ch.AcaCalID = " + acaCalID + " and ch.CourseID = " + intCourseID + "  and ch.VersionID = " +
                        intVersionID + "  and ch.AcaCalSectionID = " + sectionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            students = mapStudents(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return students;
        }
    }
}
